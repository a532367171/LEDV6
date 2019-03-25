using LedCommunication;
using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Content;
using LedModel.Data;
using LedModel.Enum;
using LedService.Cloud.Terminal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem.Cloud
{
	public class formCloudSendData : Form
	{
		private const int maxSentCount = 5;

		private LedPanel panel;

		private Process process;

		private IList<byte[]> dataBytes;

		private bool isThread;

		private bool needtoClose;

		private IContainer components;

		private GroupBox zhGroupBox1;

		private Label lblSendStatus;

		private ProgressBar prgStatus;

		private ZhLabel label3;

		public formCloudSendData(LedPanel pPanel)
		{
			this.InitializeComponent();
			this.panel = pPanel;
			this.Text = formMain.ML.GetStr("formCloudSendData_Form_CloudSendData");
		}

		private void formCloudSendData_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.BackColor = Template.GroupBox_BackColor;
			this.lblSendStatus.Text = string.Empty;
			this.prgStatus.Value = 0;
			this.needtoClose = false;
			this.isThread = true;
			new Thread(new ThreadStart(this.GenerateAndSendData))
			{
				IsBackground = true
			}.Start();
		}

		private void formCloudSendData_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
				return;
			}
			try
			{
				if (this.dataBytes != null)
				{
					for (int i = 0; i < this.dataBytes.Count; i++)
					{
						Array.Clear(this.dataBytes[i], 0, this.dataBytes[i].Length);
						this.dataBytes[i] = null;
					}
					this.dataBytes.Clear();
					this.dataBytes = null;
				}
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
			}
			catch
			{
			}
		}

		private void GenerateAndSendData()
		{
			this.GenerateData();
			Thread.Sleep(1000);
			this.SendData();
		}

		public void GenerateData()
		{
			if (!this.isThread)
			{
				return;
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.prgStatus.Value = 0;
				this.lblSendStatus.Text = formMain.ML.GetStr("Prompt_NowIsGeneratingData");
				this.lblSendStatus.Refresh();
			}));
			Thread.Sleep(100);
			if (formMain.ledsys == null)
			{
				return;
			}
			LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
			if (selectedPanel == null)
			{
				return;
			}
			this.process = new Process();
			if (selectedPanel.IsLSeries())
			{
				this.process.PanelBytes = selectedPanel.ToLBytes();
				if (!this.isThread)
				{
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.prgStatus.Value = 20;
				}));
				Thread.Sleep(100);
				this.process.ItemTimerLBytes = selectedPanel.ToItemTimerLByte();
				if (!this.isThread)
				{
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.prgStatus.Value = 40;
				}));
				Thread.Sleep(100);
				this.process.ItemStartLBytes = selectedPanel.ToItemStartLBytes();
				if (!this.isThread)
				{
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.prgStatus.Value = 60;
				}));
				Thread.Sleep(100);
				this.process.ItemLBytes = selectedPanel.ToItemLBytes();
				if (!this.isThread)
				{
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.prgStatus.Value = 80;
				}));
				Thread.Sleep(100);
				this.dataBytes = new protocol_data_integration().Program_Packaging_data_L(this.process, selectedPanel.CardAddress, selectedPanel.ProtocolVersion, true);
				if (!this.isThread)
				{
					return;
				}
			}
			string overlengthMsg = string.Empty;
			bool hasContentOverlength = this.HasContentOverlength(selectedPanel, ref overlengthMsg);
			base.Invoke(new MethodInvoker(delegate
			{
				this.prgStatus.Value = 100;
				if (hasContentOverlength)
				{
					MessageBox.Show(this, overlengthMsg, formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}));
			Thread.Sleep(100);
		}

		private void SendData()
		{
			int i = 0;
			bool flag = false;
			bool flag2 = false;
			bool pending = false;
			while (i < 5)
			{
				if (!this.isThread)
				{
					return;
				}
				i++;
				flag2 = false;
				base.Invoke(new MethodInvoker(delegate
				{
					this.prgStatus.Value = 0;
					this.prgStatus.Style = ProgressBarStyle.Marquee;
					this.lblSendStatus.Text = formMain.ML.GetStr("Prompt_NowIsSendingData");
					this.lblSendStatus.Refresh();
				}));
				Thread.Sleep(600);
				if (this.process != null && this.process.GetBytesLength() <= formMain.ledsys.SelectedPanel.GetFlashCapacity())
				{
					flag2 = true;
				}
				if (this.dataBytes != null && flag2)
				{
					string dataFilePath = string.Format("{0}\\Cloud\\{1}\\{2}", LedCommonConst.ProjectSaveDirectory, LedGlobal.CloudAccount.UserName, this.panel.ID);
					flag = new TransDataService().Send(LedGlobal.CloudAccount.SessionID, this.panel.CloudID, this.dataBytes, dataFilePath, formMain.ML.GetStr("Message_Send_Item_Data"), ref pending);
				}
				if (flag)
				{
					break;
				}
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.prgStatus.Style = ProgressBarStyle.Blocks;
				this.prgStatus.Value = 100;
			}));
			Thread.Sleep(600);
			if (!flag2)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_MemoryOverSize"));
					this.needtoClose = true;
					base.Close();
				}));
				return;
			}
			if (!flag)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					MessageBox.Show(this, formMain.ML.GetStr("Display_CommunicationFailed"));
					this.needtoClose = true;
					base.Close();
				}));
				return;
			}
			base.Invoke(new MethodInvoker(delegate
			{
				string text = formMain.ML.GetStr("Message_Upload_To_Cloud_Success");
				if (pending)
				{
					text = text + "，" + formMain.ML.GetStr("Message_Send_Complete_By_Reviewing");
				}
				MessageBox.Show(this, text);
				this.needtoClose = true;
				this.Close();
			}));
		}

		private bool HasContentOverlength(LedPanel panel, ref string msg)
		{
			bool result = false;
			msg = string.Empty;
			foreach (LedItem current in panel.Items)
			{
				foreach (LedSubarea current2 in current.Subareas)
				{
					foreach (LedContent current3 in current2.Contents)
					{
						if (current3.Type == LedContentType.TextPicture)
						{
							LedPictureText ledPictureText = current3 as LedPictureText;
							if (ledPictureText.EffectsSetting.EntryMode > 2 && ledPictureText.EffectsSetting.EntryMode < 7 && ledPictureText.EffectiveLength > 65536)
							{
								switch (ledPictureText.PictureTextType)
								{
								case LedPictureTextType.Text:
								case LedPictureTextType.MultilineText:
								case LedPictureTextType.Table:
									result = true;
									msg = formMain.ML.GetStr("Message_Data_Has_Exceeded_The_Maximum_Display_Range");
									break;
								}
							}
						}
					}
				}
			}
			return result;
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
			this.zhGroupBox1 = new GroupBox();
			this.lblSendStatus = new Label();
			this.prgStatus = new ProgressBar();
			this.label3 = new ZhLabel();
			this.zhGroupBox1.SuspendLayout();
			base.SuspendLayout();
			this.zhGroupBox1.Controls.Add(this.lblSendStatus);
			this.zhGroupBox1.Controls.Add(this.prgStatus);
			this.zhGroupBox1.Location = new System.Drawing.Point(8, 7);
			this.zhGroupBox1.Name = "zhGroupBox1";
			this.zhGroupBox1.Size = new System.Drawing.Size(523, 265);
			this.zhGroupBox1.TabIndex = 24;
			this.zhGroupBox1.TabStop = false;
			this.lblSendStatus.AutoSize = true;
			this.lblSendStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblSendStatus.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.lblSendStatus.Location = new System.Drawing.Point(18, 29);
			this.lblSendStatus.Name = "lblSendStatus";
			this.lblSendStatus.Size = new System.Drawing.Size(76, 16);
			this.lblSendStatus.TabIndex = 2;
			this.lblSendStatus.Text = "发送状态";
			this.prgStatus.Location = new System.Drawing.Point(18, 56);
			this.prgStatus.Name = "prgStatus";
			this.prgStatus.Size = new System.Drawing.Size(485, 23);
			this.prgStatus.TabIndex = 1;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Location = new System.Drawing.Point(21, 118);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 19);
			this.label3.TabIndex = 23;
			this.label3.TextAlign = TextAlaginType.Right;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(537, 276);
			base.Controls.Add(this.zhGroupBox1);
			base.Controls.Add(this.label3);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudSendData";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "云发送数据";
			base.FormClosing += new FormClosingEventHandler(this.formCloudSendData_FormClosing);
			base.Load += new EventHandler(this.formCloudSendData_Load);
			this.zhGroupBox1.ResumeLayout(false);
			this.zhGroupBox1.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
