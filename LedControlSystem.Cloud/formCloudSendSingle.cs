using LedCommunication;
using LedControlSystem.LedControlSystem;
using LedModel;
using LedModel.Cloud;
using LedModel.Enum;
using LedService.Cloud.Terminal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.Cloud
{
	public class formCloudSendSingle : Form
	{
		private const int maxSendCount = 5;

		private formMain frmMain;

		public bool ShowOK;

		private object commData;

		private LedCmdType command;

		private string operation;

		private LedPanelCloud panelCloud;

		public static bool LastSendResult;

		public static LedCmdType LastSendCmd;

		private bool needtoClose;

		private IContainer components;

		private GroupBox grpInfo;

		private ProgressBar prgStatus;

		public formCloudSendSingle(object objData, string pOperation, LedPanelCloud pPanelCloud, bool pOk, LedCmdType pCommand, formMain fmain)
		{
			this.InitializeComponent();
			this.ShowOK = pOk;
			this.command = pCommand;
			this.commData = objData;
			this.operation = pOperation;
			this.panelCloud = pPanelCloud;
			this.frmMain = fmain;
			base.Text = pOperation;
			base.Size = new System.Drawing.Size(425, 89);
			base.TopMost = true;
		}

		private void formCloudSendSingle_Load(object sender, EventArgs e)
		{
			formCloudSendSingle.LastSendResult = false;
			this.needtoClose = false;
			this.prgStatus.Value = 0;
			Thread thread = new Thread(new ThreadStart(this.StartSend));
			thread.Start();
		}

		private void formCloudSendSingle_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
			}
		}

		private void StartSend()
		{
			int i = 0;
			bool flag = false;
			string empty = string.Empty;
			formCloudSendSingle.LastSendResult = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.Text = this.operation + "     " + formMain.ML.GetStr("Prompt_NowIsGeneratingData");
				this.Refresh();
			}));
			Thread.Sleep(500);
			IList<byte[]> list = protocol_single_cmd.Send_Pack(0, 0, this.command, this.commData, false, null, "", this.panelCloud.ProtocolVersion);
			if (list == null || list.Count == 0)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					MessageBox.Show(this, this.operation + formMain.ML.GetStr("Display_Failed"));
					this.needtoClose = true;
					base.Close();
				}));
				return;
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.Text = this.operation + "     " + formMain.ML.GetStr("Message_Generate_Data_Complete");
				this.prgStatus.Value = 50;
				this.Refresh();
			}));
			Thread.Sleep(500);
			base.Invoke(new MethodInvoker(delegate
			{
				this.Text = this.operation + "     " + formMain.ML.GetStr("Prompt_NowIsSendingData");
				this.Refresh();
			}));
			Thread.Sleep(500);
			while (i < 5)
			{
				i++;
				empty = string.Empty;
				bool flag2 = new SingleCommandService().Send(LedGlobal.CloudAccount.SessionID, this.panelCloud.CloudID, list[0], this.operation, ref empty);
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.Text = this.operation;
				this.Refresh();
			}));
			Thread.Sleep(500);
			if (!flag)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.prgStatus.Value = 100;
					MessageBox.Show(this, formMain.ML.GetStr("Display_CommunicationFailed"));
					this.needtoClose = true;
					base.Close();
				}));
				return;
			}
			bool isResponseOK = false;
			if (this.panelCloud.State == LedPanelState.Online)
			{
				isResponseOK = this.GetResponse(empty);
			}
			else
			{
				isResponseOK = true;
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.prgStatus.Value = 100;
				if (isResponseOK)
				{
					formCloudSendSingle.LastSendResult = true;
					if (this.ShowOK)
					{
						MessageBox.Show(this, this.operation + formMain.ML.GetStr("Display_Successed"));
					}
					else
					{
						Thread.Sleep(1200);
					}
				}
				else
				{
					MessageBox.Show(this, formMain.ML.GetStr("Display_CommunicationFailed"));
				}
				this.needtoClose = true;
				this.Close();
			}));
		}

		private bool GetResponse(string id)
		{
			bool flag = false;
			DateTime now = DateTime.Now;
			while ((DateTime.Now - now).TotalSeconds < 10.0)
			{
				SingleCommandInfo singleCommandInfo = new SingleCommandService().Get(LedGlobal.CloudAccount.SessionID, this.panelCloud.CloudID, id);
				if (singleCommandInfo != null)
				{
					byte[] receiveData = singleCommandInfo.ReceiveData;
					byte[] sendData = singleCommandInfo.SendData;
					if (receiveData != null && receiveData.Length > 0 && sendData != null && sendData.Length > 0)
					{
						IList<Unpack_Results> list = protocol_single_cmd.Rec_Unpack(receiveData, sendData);
						if (list != null && list.Count > 0 && list[0].Result_Type == 2 && list[0].Info_Erroneous_Data == info_error_type.Null)
						{
							if (list[0].Cmd_Type != LedCmdType.Send_Panel_Parameter)
							{
								flag = true;
								break;
							}
							TerminalInfo terminalInfo = new TerminalService().Get(LedGlobal.CloudAccount.SessionID, this.panelCloud.DeviceID);
							if (terminalInfo != null && this.panelCloud.IsEquals(terminalInfo.ToParameterArray()))
							{
								flag = true;
								break;
							}
							break;
						}
					}
				}
				if (flag)
				{
					break;
				}
				Thread.Sleep(1000);
			}
			return flag;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.grpInfo = new GroupBox();
			this.prgStatus = new ProgressBar();
			this.grpInfo.SuspendLayout();
			base.SuspendLayout();
			this.grpInfo.Controls.Add(this.prgStatus);
			this.grpInfo.Location = new System.Drawing.Point(6, -1);
			this.grpInfo.Name = "grpInfo";
			this.grpInfo.Size = new System.Drawing.Size(395, 52);
			this.grpInfo.TabIndex = 24;
			this.grpInfo.TabStop = false;
			this.prgStatus.Location = new System.Drawing.Point(7, 14);
			this.prgStatus.Name = "prgStatus";
			this.prgStatus.Size = new System.Drawing.Size(377, 27);
			this.prgStatus.Step = 5;
			this.prgStatus.TabIndex = 1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(419, 63);
			base.Controls.Add(this.grpInfo);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudSendSingle";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "云设置";
			base.FormClosing += new FormClosingEventHandler(this.formCloudSendSingle_FormClosing);
			base.Load += new EventHandler(this.formCloudSendSingle_Load);
			this.grpInfo.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
