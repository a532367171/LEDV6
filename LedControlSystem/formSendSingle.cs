using HelloRemoting;
using LedModel;
using LedModel.Const;
using LedModel.Enum;
using server_interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formSendSingle : Form
	{
		private formMain fm;

		public bool ShowOK;

		private object commData;

		private LedCmdType command;

		private string operation;

		private LedPanel panel;

		public static bool LastSendResult;

		public static bool SendFinish;

		public static object LastSendResultObject;

		public static LedCmdType LastSendCmd;

		private bool needtoClose;

		private bool isCmdOver;

		private int MaxResendCount = 5;

		private int ResendCount;

		private int executeProgress;

		private bool isReceiveEvent;

		private Queue<IPC_SIMPLE_ANSWER> isaQueue;

		private bool isThread;

		private Thread thrReceiveEvent;

		private bool isThreadReceiveEvent;

		private DateTime dtStart;

		private bool isRestarting;

		private bool isWiFiProductionTest = LedGlobal.IsWiFiProductionTest;

		private static string formID = "formSendSingle";

		private IContainer components;

		private ProgressBar pgbStatus;

		private System.Windows.Forms.Timer tmrDaemon;

		private GroupBox groupBox1;

		public static string FormID
		{
			get
			{
				return formSendSingle.formID;
			}
			set
			{
				formSendSingle.formID = value;
			}
		}

		public formSendSingle()
		{
			this.InitializeComponent();
		}

		public formSendSingle(object objData, string pOperation, LedPanel pPanel, bool pOk, LedCmdType pCommand, formMain fmain)
		{
			this.InitializeComponent();
			formSendSingle.LastSendResultObject = null;
			this.ShowOK = pOk;
			this.command = pCommand;
			this.commData = objData;
			this.operation = pOperation;
			this.panel = pPanel;
			this.fm = fmain;
			base.Text = pOperation;
			base.Size = new System.Drawing.Size(425, 89);
			base.TopMost = true;
		}

		private void formSendSingle_Load(object sender, EventArgs e)
		{
			formSendSingle.LastSendResult = false;
			formSendSingle.SendFinish = false;
			this.isCmdOver = false;
			this.isReceiveEvent = false;
			this.needtoClose = false;
			this.isRestarting = false;
			this.ResendCount = 0;
			this.pgbStatus.Value = 0;
			this.executeProgress = 0;
			this.isaQueue = new Queue<IPC_SIMPLE_ANSWER>();
			call.OnDeviceCmdReturnResult += new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
			this.tmrDaemon.Start();
			this.isThreadReceiveEvent = true;
			this.thrReceiveEvent = new Thread(new ThreadStart(this.ReceiveEvent));
			this.thrReceiveEvent.IsBackground = true;
			this.thrReceiveEvent.Start();
			this.isThread = true;
			Thread thread = new Thread(new ThreadStart(this.StartSend));
			thread.Start();
		}

		private void formSendSingle_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
				return;
			}
			try
			{
				this.isThread = false;
				this.isThreadReceiveEvent = false;
				this.tmrDaemon.Stop();
				call.OnDeviceCmdReturnResult -= new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
				if (this.thrReceiveEvent != null && this.thrReceiveEvent.ThreadState == ThreadState.Stopped)
				{
					this.thrReceiveEvent = null;
				}
			}
			catch
			{
			}
		}

		private void tmrDaemon_Tick(object sender, EventArgs e)
		{
			if (this.isReceiveEvent)
			{
				return;
			}
			DateTime now = DateTime.Now;
			if ((now - this.dtStart).TotalSeconds > 45.0 && !this.isRestarting)
			{
				this.pgbStatus.Value = 100;
				this.tmrDaemon.Stop();
				MessageBox.Show(this, formMain.ML.GetStr("Display_CommunicationFailed"));
				this.needtoClose = true;
				base.Close();
			}
			if (this.pgbStatus.Value < this.pgbStatus.Maximum - 10)
			{
				this.pgbStatus.PerformStep();
			}
		}

		private void OnDeviceCmdReturn(object sender, DeviceCmdEventArgs arg)
		{
			this.isReceiveEvent = true;
			IPC_SIMPLE_ANSWER isa = arg.isa;
			this.isaQueue.Enqueue(isa);
			this.isReceiveEvent = false;
		}

		private void ReceiveEvent()
		{
			while (this.isThreadReceiveEvent)
			{
				if (this.isaQueue == null || this.isaQueue.Count == 0)
				{
					Thread.Sleep(5);
				}
				else if (this.isReceiveEvent)
				{
					Thread.Sleep(5);
				}
				else
				{
					IPC_SIMPLE_ANSWER isa = this.isaQueue.Dequeue();
					if (isa == null)
					{
						Thread.Sleep(5);
					}
					else
					{
						if ((byte)isa.cmd_id == (byte)this.command)
						{
							if (!isa.is_cmd_failed_flag)
							{
								base.Invoke(new MethodInvoker(delegate
								{
									this.Text = this.operation + "     " + isa.repair_message;
									this.Refresh();
								}));
								if (!this.isWiFiProductionTest)
								{
									Thread.Sleep(20);
								}
								if (isa.is_cmd_over_flag)
								{
									if (!this.isCmdOver)
									{
										this.isCmdOver = true;
										formSendSingle.LastSendResult = true;
										formSendSingle.SendFinish = true;
										formSendSingle.LastSendResultObject = isa.return_object;
										base.Invoke(new MethodInvoker(delegate
										{
											this.pgbStatus.Value = 100;
											this.pgbStatus.Refresh();
										}));
										Thread.Sleep(600);
										this.isThreadReceiveEvent = false;
										if (this.ShowOK)
										{
											base.Invoke(new MethodInvoker(delegate
											{
												this.tmrDaemon.Stop();
												MessageBox.Show(this, this.operation + formMain.ML.GetStr("Display_Successed"));
												this.needtoClose = true;
												base.Close();
											}));
										}
										else
										{
											base.Invoke(new MethodInvoker(delegate
											{
												this.tmrDaemon.Stop();
												if (!this.isWiFiProductionTest)
												{
													Thread.Sleep(1200);
												}
												this.needtoClose = true;
												base.Close();
											}));
										}
									}
								}
								else
								{
									this.executeProgress = 50;
									if (this.executeProgress > this.pgbStatus.Value)
									{
										base.Invoke(new MethodInvoker(delegate
										{
											this.pgbStatus.Value = this.executeProgress;
											this.pgbStatus.Refresh();
										}));
										if (!this.isWiFiProductionTest)
										{
											Thread.Sleep(600);
										}
									}
								}
							}
							else
							{
								formSendSingle.LastSendResult = false;
								base.Invoke(new MethodInvoker(delegate
								{
									this.Text = this.operation + "     " + isa.repair_message;
									this.Refresh();
								}));
								if (!this.isWiFiProductionTest)
								{
									Thread.Sleep(20);
								}
								if (isa.is_cmd_over_flag)
								{
									if (isa.er_code > 20)
									{
										if (this.ResendCount + 1 < this.MaxResendCount)
										{
											this.dtStart = DateTime.Now;
											this.ResendCount++;
											this.StartSend();
										}
										else if (!this.isCmdOver)
										{
											this.isCmdOver = true;
											this.isThreadReceiveEvent = false;
											base.Invoke(new MethodInvoker(delegate
											{
												this.pgbStatus.Value = 100;
												this.tmrDaemon.Stop();
												MessageBox.Show(this, formMain.ML.GetStr("Display_CommunicationFailed"));
												this.needtoClose = true;
												base.Close();
											}));
										}
									}
									else
									{
										this.isThreadReceiveEvent = false;
										base.Invoke(new MethodInvoker(delegate
										{
											this.pgbStatus.Value = 100;
											this.tmrDaemon.Stop();
											if ((byte)isa.cmd_id == 58 || (byte)isa.cmd_id == 60)
											{
												formSendSingle.LastSendResult = true;
											}
											else
											{
												MessageBox.Show(this, formMain.GetSendingCSWDescription("Sending_CSW", isa.er_code));
											}
											this.needtoClose = true;
											this.Close();
										}));
									}
								}
								else
								{
									this.executeProgress = 50;
									if (this.executeProgress > this.pgbStatus.Value)
									{
										base.Invoke(new MethodInvoker(delegate
										{
											this.pgbStatus.Value = this.executeProgress;
											this.pgbStatus.Refresh();
										}));
										if (!this.isWiFiProductionTest)
										{
											Thread.Sleep(600);
										}
									}
								}
							}
						}
						if (!this.isWiFiProductionTest)
						{
							Thread.Sleep(100);
						}
					}
				}
			}
			if (this.isaQueue != null)
			{
				this.isaQueue.Clear();
				this.isaQueue = null;
			}
		}

		private void StartSend()
		{
			bool flag = false;
			int num = 0;
			while (num < LedCommunicationConst.ProtocolSendVersionList.Length && this.isThread)
			{
				if (this.panel.ProtocolVersion == LedCommunicationConst.ProtocolSendVersionList[num])
				{
					flag = true;
					break;
				}
				num++;
			}
			if (!flag && this.isThread)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.tmrDaemon.Stop();
					MessageBox.Show(this, formMain.ML.GetStr("Form_find_device_message_VersionNumber_Inconsistent"));
					this.needtoClose = true;
					base.Close();
				}));
				return;
			}
			this.isCmdOver = false;
			int num2 = 0;
			bool flag2 = false;
			while (num2 < 5 && this.isThread)
			{
				num2++;
				formSendSingle.LastSendResult = false;
				this.isReceiveEvent = false;
				this.dtStart = DateTime.Now;
				SEND_CMD_RET_VALUE sEND_CMD_RET_VALUE = SEND_CMD_RET_VALUE.ER_NONE_INIT;
				if (formMain.IServer != null)
				{
					sEND_CMD_RET_VALUE = formMain.IServer.send_cmd_to_device_async((byte)this.command, this.commData, this.panel.ProductID);
				}
				if (sEND_CMD_RET_VALUE == SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK)
				{
					flag2 = true;
					break;
				}
				if (sEND_CMD_RET_VALUE != SEND_CMD_RET_VALUE.IPC_communication_FAILED)
				{
					break;
				}
				this.RestartIPCServer();
			}
			if (!flag2 && this.isThread)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.tmrDaemon.Stop();
					MessageBox.Show(this, formMain.ML.GetStr("Display_CommunicationFailed"));
					this.needtoClose = true;
					base.Close();
				}));
			}
		}

		private void RestartIPCServer()
		{
			this.isRestarting = true;
			string originalText = this.Text;
			base.Invoke(new MethodInvoker(delegate
			{
				this.Text = this.operation + "     " + formMain.ML.GetStr("Form_find_device_label_msg_restart");
				this.Refresh();
			}));
			Thread.Sleep(500);
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			TimeSpan timeSpan = now2 - now;
			this.fm.RestartIPCServer();
			while ((!this.fm.isIPCServerOK || !this.fm.IsAllPanelOnline()) && timeSpan.TotalSeconds < 45.0)
			{
				now2 = DateTime.Now;
				timeSpan = now2 - now;
				Thread.Sleep(200);
			}
			if (this.fm.isIPCServerOK)
			{
				this.fm.HeartbeatProcessing(false);
				base.Invoke(new MethodInvoker(delegate
				{
					this.Text = this.operation + "     " + formMain.ML.GetStr("Form_find_device_label_msg_Started");
					this.Refresh();
				}));
			}
			else
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.Text = this.operation + "     " + formMain.ML.GetStr("Form_find_device_label_msg_Start_failure");
					this.Refresh();
				}));
			}
			Thread.Sleep(1000);
			base.Invoke(new MethodInvoker(delegate
			{
				this.Text = originalText;
				this.Refresh();
			}));
			Thread.Sleep(500);
			this.isRestarting = false;
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formSendSingle));
			this.pgbStatus = new ProgressBar();
			this.tmrDaemon = new System.Windows.Forms.Timer(this.components);
			this.groupBox1 = new GroupBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.pgbStatus.Location = new System.Drawing.Point(7, 14);
			this.pgbStatus.Name = "pgbStatus";
			this.pgbStatus.Size = new System.Drawing.Size(377, 27);
			this.pgbStatus.Step = 5;
			this.pgbStatus.TabIndex = 1;
			this.tmrDaemon.Interval = 1000;
			this.tmrDaemon.Tick += new EventHandler(this.tmrDaemon_Tick);
			this.groupBox1.Controls.Add(this.pgbStatus);
			this.groupBox1.Location = new System.Drawing.Point(6, -1);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(395, 52);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(419, 63);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formSendSingle";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "设置";
			base.FormClosing += new FormClosingEventHandler(this.formSendSingle_FormClosing);
			base.Load += new EventHandler(this.formSendSingle_Load);
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
