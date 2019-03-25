using HelloRemoting;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Content;
using LedModel.Data;
using LedModel.Enum;
using server_interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem.LedControlSystem
{
	public class formSendData : Form
	{
		private static string formID = "formSendData";

		public bool ShowOK;

		public static bool SendALl;

		public static bool Finished;

		private formMain fm;

		private int MaxResendCount = 5;

		private int ResendCount;

		private int totalFrame;

		private DateTime dtStart;

		private Process process;

		private ProcessFirmware processFirmware;

		private ProcessStringLibrary processStringLibrary;

		private bool isUpdataCode;

		private bool isDownloadStringLibrary;

		private bool isReceiveEvent;

		private bool isThread;

		private bool needtoClose;

		private Thread thrReceiveEvent;

		private bool isThreadReceiveEvent;

		private Queue<IPC_SIMPLE_ANSWER> isaQueue;

		private string lastFrame;

		private bool isCmdOver;

		private bool isSending;

		private bool isRestarting;

		private bool isWiFiProductionTest = LedGlobal.IsWiFiProductionTest;

		private IContainer components;

		private ProgressBar pgbStatus;

		private Label lblSendStatus;

		private Label lblTotalFrame;

		private ZhLabel label3;

		private System.Windows.Forms.Timer tmrDaemon;

		private Label lblCurrentFrame;

		private Label label_PortInfo;

		private Label label_Info_OffOn;

		private Label label_Info_Luminance;

		private Label label_Info_Scantype;

		private Label label_info_Panel;

		private Label label4_OffOn;

		private Label label3_Lumiance;

		private Label label2_Scantype;

		private Label label1_PanelParam;

		private Label label_Info_PortInfo;

		private GroupBox zhGroupBox1;

		public static string FormID
		{
			get
			{
				return formSendData.formID;
			}
			set
			{
				formSendData.formID = value;
			}
		}

		public formSendData(bool pIsUpdateCode, bool pIsDownloadStringLibrary, formMain fmain)
		{
			this.InitializeComponent();
			this.isUpdataCode = pIsUpdateCode;
			this.isDownloadStringLibrary = pIsDownloadStringLibrary;
			this.fm = fmain;
		}

		private void formSendData_Load(object sender, EventArgs e)
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
			this.Text = formMain.ML.GetStr("formSendData_FormText");
			this.lblSendStatus.Text = string.Empty;
			call.OnDeviceCmdReturnResult += new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
			formSendData.Finished = false;
			formSendData.SendALl = false;
			this.totalFrame = 0;
			this.ResendCount = 0;
			this.pgbStatus.Value = 0;
			this.lastFrame = "0";
			this.isCmdOver = false;
			this.isReceiveEvent = false;
			this.needtoClose = false;
			this.isSending = false;
			this.isRestarting = false;
			this.isaQueue = new Queue<IPC_SIMPLE_ANSWER>();
			this.isThreadReceiveEvent = true;
			this.thrReceiveEvent = new Thread(new ThreadStart(this.ReceiveEvent));
			this.thrReceiveEvent.IsBackground = true;
			this.thrReceiveEvent.Start();
			this.dtStart = DateTime.Now;
			this.tmrDaemon.Start();
			this.isThread = true;
			new Thread(new ThreadStart(this.InitStart))
			{
				Priority = ThreadPriority.AboveNormal,
				IsBackground = true
			}.Start();
		}

		private void formSendData_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
				return;
			}
			try
			{
				if (this.process != null)
				{
					this.process.Dispose();
					this.process = null;
				}
				if (this.processFirmware != null)
				{
					this.processFirmware.Dispose();
					this.processFirmware = null;
				}
				if (this.processStringLibrary != null)
				{
					this.processStringLibrary.Dispose();
					this.processStringLibrary = null;
				}
				this.isThreadReceiveEvent = false;
				this.isThread = false;
				this.tmrDaemon.Stop();
				call.OnDeviceCmdReturnResult -= new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
				if (this.thrReceiveEvent != null && this.thrReceiveEvent.ThreadState == ThreadState.Stopped)
				{
					this.thrReceiveEvent = null;
				}
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
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
			if (this.isSending)
			{
				return;
			}
			bool flag = false;
			if (this.pgbStatus.Value == 0 || (this.lblCurrentFrame.Text == "0" && this.pgbStatus.Value != 0))
			{
				DateTime now = DateTime.Now;
				if ((now - this.dtStart).TotalSeconds > 30.0 && !this.isRestarting)
				{
					flag = true;
				}
			}
			else if (this.lblCurrentFrame.Text == this.lastFrame)
			{
				DateTime now2 = DateTime.Now;
				if ((now2 - this.dtStart).TotalSeconds > 30.0 && !this.isRestarting)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.tmrDaemon.Stop();
				MessageBox.Show(this, formMain.ML.GetStr("Display_CommunicationFailed"));
				this.needtoClose = true;
				base.Close();
			}
		}

		private void OnDeviceCmdReturn(object sender, DeviceCmdEventArgs arg)
		{
			this.isReceiveEvent = true;
			IPC_SIMPLE_ANSWER isa = arg.isa;
			this.isaQueue.Enqueue(isa);
			this.isReceiveEvent = false;
		}

		private void InitStart()
		{
			bool flag = false;
			for (int i = 0; i < LedCommunicationConst.ProtocolSendVersionList.Length; i++)
			{
				if (formMain.ledsys.SelectedPanel.ProtocolVersion == LedCommunicationConst.ProtocolSendVersionList[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.tmrDaemon.Stop();
					MessageBox.Show(this, formMain.ML.GetStr("formGroups_Related_Data_CommunicationMessage_protocolVersionNumberInconsistent"));
					this.needtoClose = true;
					base.Close();
				}));
				return;
			}
			if (!this.isUpdataCode && !this.isDownloadStringLibrary)
			{
				this.CheckCardType();
				return;
			}
			this.StartSendData();
		}

		private void CheckCardType()
		{
			this.isCmdOver = false;
			int i = 0;
			bool flag = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.lblSendStatus.Text = formMain.ML.GetStr("formSendData_message_Checking_model");
				this.lblSendStatus.Refresh();
			}));
			if (!this.isWiFiProductionTest)
			{
				Thread.Sleep(100);
			}
			while (i < 5)
			{
				i++;
				SEND_CMD_RET_VALUE sEND_CMD_RET_VALUE = SEND_CMD_RET_VALUE.ER_NONE_INIT;
				if (formMain.IServer != null)
				{
					sEND_CMD_RET_VALUE = formMain.IServer.send_cmd_to_device_async(17, null, formMain.ledsys.SelectedPanel.ProductID);
				}
				if (sEND_CMD_RET_VALUE == SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK)
				{
					flag = true;
					break;
				}
				if (sEND_CMD_RET_VALUE != SEND_CMD_RET_VALUE.IPC_communication_FAILED)
				{
					break;
				}
				this.RestartIPCServer();
			}
			if (!flag)
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

		private void StartSendData()
		{
			new Thread(new ThreadStart(this.GenerateAndSendData))
			{
				IsBackground = true
			}.Start();
		}

		private void GenerateAndSendData()
		{
			this.isSending = true;
			this.GenerateData();
			if (!this.isWiFiProductionTest)
			{
				Thread.Sleep(1000);
			}
			this.SendData();
			this.isSending = false;
			this.dtStart = DateTime.Now;
		}

		public void GenerateData()
		{
			if (!this.isThread)
			{
				return;
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.pgbStatus.Value = 0;
				this.lblSendStatus.Text = formMain.ML.GetStr("Prompt_NowIsGeneratingData");
				this.lblSendStatus.Refresh();
			}));
			if (!this.isWiFiProductionTest)
			{
				Thread.Sleep(100);
			}
			if (formMain.ledsys == null)
			{
				return;
			}
			LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
			if (selectedPanel == null)
			{
				return;
			}
			if (this.isUpdataCode)
			{
				this.processFirmware = new ProcessFirmware();
				this.processFirmware.FirmwareBytes = selectedPanel.ToFirmwareBytes(false);
				if (!this.isThread)
				{
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.pgbStatus.Value = 100;
					if (this.processFirmware != null)
					{
						this.totalFrame = this.processFirmware.GetBytesFrame();
						this.lblTotalFrame.Text = this.totalFrame.ToString();
						this.lblTotalFrame.Refresh();
					}
				}));
				Thread.Sleep(100);
				return;
			}
			else
			{
				if (!this.isDownloadStringLibrary)
				{
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
							this.pgbStatus.Value = 30;
						}));
						if (!this.isWiFiProductionTest)
						{
							Thread.Sleep(100);
						}
						this.process.ItemTimerLBytes = selectedPanel.ToItemTimerLByte();
						if (!this.isThread)
						{
							return;
						}
						base.Invoke(new MethodInvoker(delegate
						{
							this.pgbStatus.Value = 50;
						}));
						if (!this.isWiFiProductionTest)
						{
							Thread.Sleep(100);
						}
						this.process.ItemStartLBytes = selectedPanel.ToItemStartLBytes();
						if (!this.isThread)
						{
							return;
						}
						base.Invoke(new MethodInvoker(delegate
						{
							this.pgbStatus.Value = 70;
						}));
						if (!this.isWiFiProductionTest)
						{
							Thread.Sleep(100);
						}
						this.process.ItemLBytes = selectedPanel.ToItemLBytes();
						if (!this.isThread)
						{
							return;
						}
					}
					else
					{
						this.process.PanelBytes = selectedPanel.ToBytes();
						if (!this.isThread)
						{
							return;
						}
						base.Invoke(new MethodInvoker(delegate
						{
							this.pgbStatus.Value = 30;
						}));
						if (!this.isWiFiProductionTest)
						{
							Thread.Sleep(100);
						}
						this.process.BmpDataBytes = selectedPanel.ToItemBmpDataBytes();
						if (!this.isThread)
						{
							return;
						}
						base.Invoke(new MethodInvoker(delegate
						{
							this.pgbStatus.Value = 70;
						}));
						if (!this.isWiFiProductionTest)
						{
							Thread.Sleep(100);
						}
						this.process.ItemBytes = selectedPanel.ToItemBytes();
						if (!this.isThread)
						{
							return;
						}
					}
					string overlengthMsg = string.Empty;
					bool hasContentOverlength = this.HasContentOverlength(selectedPanel, ref overlengthMsg);
					base.Invoke(new MethodInvoker(delegate
					{
						this.pgbStatus.Value = 100;
						if (this.process != null)
						{
							this.totalFrame = this.process.GetBytesFrame();
							this.lblTotalFrame.Text = this.totalFrame.ToString();
							this.lblTotalFrame.Refresh();
						}
						if (hasContentOverlength)
						{
							MessageBox.Show(this, overlengthMsg, formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					}));
					if (!this.isWiFiProductionTest)
					{
						Thread.Sleep(100);
					}
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.pgbStatus.Style = ProgressBarStyle.Marquee;
				}));
				Thread.Sleep(100);
				this.processStringLibrary = new ProcessStringLibrary();
				this.processStringLibrary.BmpDataBytes = selectedPanel.StringLibrary.ToBmpDataBytes();
				if (!this.isThread)
				{
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.pgbStatus.Style = ProgressBarStyle.Blocks;
					this.pgbStatus.Value = 80;
				}));
				Thread.Sleep(100);
				this.processStringLibrary.StartBytes = selectedPanel.StringLibrary.ToBytes();
				if (!this.isThread)
				{
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.pgbStatus.Style = ProgressBarStyle.Blocks;
					this.pgbStatus.Value = 90;
				}));
				Thread.Sleep(100);
				this.processStringLibrary.PanelBytes = selectedPanel.ToLBytes();
				if (!this.isThread)
				{
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.pgbStatus.Value = 100;
					if (this.processStringLibrary != null)
					{
						this.totalFrame = this.processStringLibrary.GetBytesFrame();
						this.lblTotalFrame.Text = this.totalFrame.ToString();
						this.lblTotalFrame.Refresh();
					}
				}));
				Thread.Sleep(100);
				return;
			}
		}

		private void SendData()
		{
			this.dtStart = DateTime.Now;
			this.isCmdOver = false;
			int i = 0;
			bool flag = false;
			bool flag2 = false;
			bool isMemoryOverSize = false;
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
					this.pgbStatus.Value = 0;
					this.lblSendStatus.Text = formMain.ML.GetStr("Prompt_NowIsSendingData");
					this.lblSendStatus.Refresh();
				}));
				if (!this.isWiFiProductionTest)
				{
					Thread.Sleep(100);
				}
				SEND_CMD_RET_VALUE sEND_CMD_RET_VALUE = SEND_CMD_RET_VALUE.ER_NONE_INIT;
				if (this.isUpdataCode)
				{
					if (this.processFirmware != null)
					{
						flag2 = true;
					}
					if (formMain.IServer != null && flag2)
					{
						sEND_CMD_RET_VALUE = formMain.IServer.send_cmd_to_device_async(113, this.processFirmware, formMain.ledsys.SelectedPanel.ProductID);
					}
				}
				else if (this.isDownloadStringLibrary)
				{
					if (this.processStringLibrary != null)
					{
						flag2 = true;
					}
					if (formMain.IServer != null && flag2)
					{
						sEND_CMD_RET_VALUE = formMain.IServer.send_cmd_to_device_async(8, this.processStringLibrary, formMain.ledsys.SelectedPanel.ProductID);
					}
				}
				else
				{
					if (this.process != null)
					{
						if (this.process.GetBytesLength() <= formMain.ledsys.SelectedPanel.GetFlashCapacity())
						{
							flag2 = true;
						}
						else
						{
							isMemoryOverSize = true;
						}
					}
					if (formMain.IServer != null && flag2)
					{
						sEND_CMD_RET_VALUE = formMain.IServer.send_cmd_to_device_async(1, this.process, formMain.ledsys.SelectedPanel.ProductID);
					}
				}
				if (sEND_CMD_RET_VALUE == SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK)
				{
					flag = true;
					break;
				}
				if (sEND_CMD_RET_VALUE == SEND_CMD_RET_VALUE.IPC_communication_FAILED)
				{
					this.RestartIPCServer();
				}
				else
				{
					if (sEND_CMD_RET_VALUE != SEND_CMD_RET_VALUE.ER_NONE_INIT)
					{
						flag2 = true;
						break;
					}
					break;
				}
			}
			if (!flag2)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.tmrDaemon.Stop();
					if (isMemoryOverSize)
					{
						MessageBox.Show(this, formMain.ML.GetStr("Prompt_MemoryOverSize"));
					}
					else
					{
						MessageBox.Show(this, formMain.ML.GetStr("Display_CommunicationFailed"));
					}
					this.needtoClose = true;
					this.Close();
				}));
				return;
			}
			if (!flag)
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

		private void ReceiveEvent()
		{
			while (this.isThreadReceiveEvent)
			{
				if (this.isaQueue != null && this.isaQueue.Count != 0)
				{
					IPC_SIMPLE_ANSWER isa = this.isaQueue.Dequeue();
					if (isa != null)
					{
						if ((byte)isa.cmd_id == 17)
						{
							if (!isa.is_cmd_failed_flag)
							{
								if (isa.is_cmd_over_flag)
								{
									base.Invoke(new MethodInvoker(delegate
									{
										this.pgbStatus.Value = 100;
									}));
									if (!this.isWiFiProductionTest)
									{
										Thread.Sleep(600);
									}
									if (isa.return_object != null && isa.return_object.GetType() == typeof(LedPanel))
									{
										LedPanel ledPanel = isa.return_object as LedPanel;
										if (ledPanel.CardType == formMain.ledsys.SelectedPanel.CardType)
										{
											this.StartSendData();
										}
										else if (!this.isCmdOver)
										{
											this.isCmdOver = true;
											this.isThreadReceiveEvent = false;
											base.Invoke(new MethodInvoker(delegate
											{
												this.tmrDaemon.Stop();
												MessageBox.Show(this, formMain.ML.GetStr("Prompt_ProductModelError"));
												this.needtoClose = true;
												base.Close();
											}));
										}
									}
								}
								else
								{
									base.Invoke(new MethodInvoker(delegate
									{
										this.pgbStatus.Value = 50;
									}));
									if (!this.isWiFiProductionTest)
									{
										Thread.Sleep(300);
									}
								}
							}
							else if (isa.is_cmd_over_flag)
							{
								if (isa.er_code > 20)
								{
									if (this.ResendCount + 1 < this.MaxResendCount)
									{
										this.dtStart = DateTime.Now;
										this.ResendCount++;
										this.CheckCardType();
									}
									else if (!this.isCmdOver)
									{
										this.isCmdOver = true;
										this.isThreadReceiveEvent = false;
										base.Invoke(new MethodInvoker(delegate
										{
											this.pgbStatus.Value = 100;
										}));
										if (!this.isWiFiProductionTest)
										{
											Thread.Sleep(600);
										}
										base.Invoke(new MethodInvoker(delegate
										{
											this.tmrDaemon.Stop();
											MessageBox.Show(this, formMain.ML.GetStr("Prompt_CommFaileder"));
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
									}));
									if (!this.isWiFiProductionTest)
									{
										Thread.Sleep(600);
									}
									base.Invoke(new MethodInvoker(delegate
									{
										this.tmrDaemon.Stop();
										MessageBox.Show(this, formMain.GetSendingCSWDescription("Sending_CSW", isa.er_code));
										this.needtoClose = true;
										this.Close();
									}));
								}
							}
							else
							{
								base.Invoke(new MethodInvoker(delegate
								{
									this.pgbStatus.Value = 50;
								}));
								if (!this.isWiFiProductionTest)
								{
									Thread.Sleep(300);
								}
							}
						}
						if ((byte)isa.cmd_id == 1 || (byte)isa.cmd_id == 113 || (byte)isa.cmd_id == 8)
						{
							base.Invoke(new MethodInvoker(delegate
							{
								this.dtStart = DateTime.Now;
								this.pgbStatus.Value = isa.pos;
								this.lblCurrentFrame.Text = (this.totalFrame * isa.pos / 100).ToString();
								this.lastFrame = this.lblCurrentFrame.Text;
								this.lblCurrentFrame.Refresh();
							}));
							if (!this.isWiFiProductionTest)
							{
								Thread.Sleep(40);
							}
							if (!isa.is_cmd_failed_flag)
							{
								if (isa.is_cmd_over_flag && isa.pos == 100 && !this.isCmdOver)
								{
									this.isCmdOver = true;
									formSendData.SendALl = true;
									formSendData.Finished = true;
									if (this.ShowOK)
									{
										this.isThreadReceiveEvent = false;
										base.Invoke(new MethodInvoker(delegate
										{
											this.tmrDaemon.Stop();
											if (this.isUpdataCode)
											{
												MessageBox.Show(this, formMain.ML.GetStr("formSendData_mssage_UpdateAndWaitOneMinute"));
											}
											else if (this.isDownloadStringLibrary)
											{
												MessageBox.Show(this, formMain.ML.GetStr("Message_Download_String_Library_Success"));
											}
											else
											{
												MessageBox.Show(this, formMain.ML.GetStr("formSendData_mssage_Display_Successed"));
											}
											this.needtoClose = true;
											base.Close();
										}));
									}
									else
									{
										Thread.Sleep(600);
										this.isThreadReceiveEvent = false;
										base.Invoke(new MethodInvoker(delegate
										{
											this.tmrDaemon.Stop();
											this.Refresh();
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
							else if (isa.is_cmd_over_flag)
							{
								if (isa.er_code > 20)
								{
									if (!this.isCmdOver)
									{
										this.isCmdOver = true;
										this.isThreadReceiveEvent = false;
										base.Invoke(new MethodInvoker(delegate
										{
											this.tmrDaemon.Stop();
											MessageBox.Show(this, formMain.ML.GetStr("formSendData_mssage_Display_Failed"));
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
										this.tmrDaemon.Stop();
										MessageBox.Show(this, formMain.GetSendingCSWDescription("Sending_CSW", isa.er_code));
										this.needtoClose = true;
										this.Close();
									}));
								}
							}
						}
						if (this.isaQueue != null && this.isaQueue.Count == 0)
						{
							Thread.Sleep(20);
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

		private void RestartIPCServer()
		{
			this.isRestarting = true;
			string originalText = this.lblSendStatus.Text;
			base.Invoke(new MethodInvoker(delegate
			{
				this.lblSendStatus.Text = formMain.ML.GetStr("formGroups_Related_Data_CommunicationMessage_CommunicationProcessIsBeingRestarted");
				this.lblSendStatus.Refresh();
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
					this.lblSendStatus.Text = formMain.ML.GetStr("formGroups_Related_Data_CommunicationMessage_CommunicationProcessIsStarted");
					this.lblSendStatus.Refresh();
				}));
			}
			else
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.lblSendStatus.Text = formMain.ML.GetStr("formGroups_Related_Data_CommunicationMessage_CommunicationProcessFailedToStart");
					this.lblSendStatus.Refresh();
				}));
			}
			Thread.Sleep(1000);
			base.Invoke(new MethodInvoker(delegate
			{
				this.lblSendStatus.Text = originalText;
				this.lblSendStatus.Refresh();
			}));
			Thread.Sleep(500);
			this.isRestarting = false;
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
			this.components = new Container();
			this.pgbStatus = new ProgressBar();
			this.lblSendStatus = new Label();
			this.lblTotalFrame = new Label();
			this.label3 = new ZhLabel();
			this.tmrDaemon = new System.Windows.Forms.Timer(this.components);
			this.label_Info_PortInfo = new Label();
			this.label_Info_OffOn = new Label();
			this.label_Info_Luminance = new Label();
			this.label_Info_Scantype = new Label();
			this.label_info_Panel = new Label();
			this.label4_OffOn = new Label();
			this.label3_Lumiance = new Label();
			this.label2_Scantype = new Label();
			this.label1_PanelParam = new Label();
			this.label_PortInfo = new Label();
			this.lblCurrentFrame = new Label();
			this.zhGroupBox1 = new GroupBox();
			this.zhGroupBox1.SuspendLayout();
			base.SuspendLayout();
			this.pgbStatus.Location = new System.Drawing.Point(18, 56);
			this.pgbStatus.Name = "pgbStatus";
			this.pgbStatus.Size = new System.Drawing.Size(485, 23);
			this.pgbStatus.TabIndex = 1;
			this.lblSendStatus.AutoSize = true;
			this.lblSendStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblSendStatus.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.lblSendStatus.Location = new System.Drawing.Point(18, 29);
			this.lblSendStatus.Name = "lblSendStatus";
			this.lblSendStatus.Size = new System.Drawing.Size(143, 16);
			this.lblSendStatus.TabIndex = 2;
			this.lblSendStatus.Text = "label_NowStatus";
			this.lblTotalFrame.AutoSize = true;
			this.lblTotalFrame.BackColor = System.Drawing.Color.Transparent;
			this.lblTotalFrame.Location = new System.Drawing.Point(414, 38);
			this.lblTotalFrame.Name = "lblTotalFrame";
			this.lblTotalFrame.Size = new System.Drawing.Size(11, 12);
			this.lblTotalFrame.TabIndex = 3;
			this.lblTotalFrame.Text = "0";
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Location = new System.Drawing.Point(22, 119);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 19);
			this.label3.TabIndex = 4;
			this.label3.TextAlign = TextAlaginType.Right;
			this.tmrDaemon.Interval = 1000;
			this.tmrDaemon.Tick += new EventHandler(this.tmrDaemon_Tick);
			this.label_Info_PortInfo.ForeColor = System.Drawing.Color.Blue;
			this.label_Info_PortInfo.Location = new System.Drawing.Point(83, 85);
			this.label_Info_PortInfo.Name = "label_Info_PortInfo";
			this.label_Info_PortInfo.Size = new System.Drawing.Size(344, 39);
			this.label_Info_PortInfo.TabIndex = 23;
			this.label_Info_PortInfo.Visible = false;
			this.label_Info_OffOn.Location = new System.Drawing.Point(116, 230);
			this.label_Info_OffOn.Name = "label_Info_OffOn";
			this.label_Info_OffOn.Size = new System.Drawing.Size(307, 12);
			this.label_Info_OffOn.TabIndex = 22;
			this.label_Info_OffOn.Visible = false;
			this.label_Info_Luminance.Location = new System.Drawing.Point(79, 209);
			this.label_Info_Luminance.Name = "label_Info_Luminance";
			this.label_Info_Luminance.Size = new System.Drawing.Size(344, 12);
			this.label_Info_Luminance.TabIndex = 21;
			this.label_Info_Luminance.Visible = false;
			this.label_Info_Scantype.Location = new System.Drawing.Point(79, 188);
			this.label_Info_Scantype.Name = "label_Info_Scantype";
			this.label_Info_Scantype.Size = new System.Drawing.Size(344, 12);
			this.label_Info_Scantype.TabIndex = 20;
			this.label_Info_Scantype.Visible = false;
			this.label_info_Panel.Location = new System.Drawing.Point(79, 127);
			this.label_info_Panel.Name = "label_info_Panel";
			this.label_info_Panel.Size = new System.Drawing.Size(344, 54);
			this.label_info_Panel.TabIndex = 19;
			this.label_info_Panel.Visible = false;
			this.label4_OffOn.AutoSize = true;
			this.label4_OffOn.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label4_OffOn.Location = new System.Drawing.Point(20, 230);
			this.label4_OffOn.Name = "label4_OffOn";
			this.label4_OffOn.Size = new System.Drawing.Size(70, 12);
			this.label4_OffOn.TabIndex = 18;
			this.label4_OffOn.Text = "自动开关机";
			this.label4_OffOn.Visible = false;
			this.label3_Lumiance.AutoSize = true;
			this.label3_Lumiance.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label3_Lumiance.Location = new System.Drawing.Point(20, 209);
			this.label3_Lumiance.Name = "label3_Lumiance";
			this.label3_Lumiance.Size = new System.Drawing.Size(57, 12);
			this.label3_Lumiance.TabIndex = 17;
			this.label3_Lumiance.Text = "亮度调节";
			this.label3_Lumiance.Visible = false;
			this.label2_Scantype.AutoSize = true;
			this.label2_Scantype.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label2_Scantype.Location = new System.Drawing.Point(20, 188);
			this.label2_Scantype.Name = "label2_Scantype";
			this.label2_Scantype.Size = new System.Drawing.Size(57, 12);
			this.label2_Scantype.TabIndex = 16;
			this.label2_Scantype.Text = "扫描方式";
			this.label2_Scantype.Visible = false;
			this.label1_PanelParam.AutoSize = true;
			this.label1_PanelParam.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label1_PanelParam.Location = new System.Drawing.Point(20, 127);
			this.label1_PanelParam.Name = "label1_PanelParam";
			this.label1_PanelParam.Size = new System.Drawing.Size(57, 12);
			this.label1_PanelParam.TabIndex = 15;
			this.label1_PanelParam.Text = "基本屏参";
			this.label1_PanelParam.Visible = false;
			this.label_PortInfo.AutoSize = true;
			this.label_PortInfo.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold);
			this.label_PortInfo.ForeColor = System.Drawing.Color.Blue;
			this.label_PortInfo.Location = new System.Drawing.Point(20, 85);
			this.label_PortInfo.Name = "label_PortInfo";
			this.label_PortInfo.Size = new System.Drawing.Size(57, 12);
			this.label_PortInfo.TabIndex = 5;
			this.label_PortInfo.Text = "通讯方式";
			this.label_PortInfo.Visible = false;
			this.lblCurrentFrame.AutoSize = true;
			this.lblCurrentFrame.Location = new System.Drawing.Point(344, 38);
			this.lblCurrentFrame.Name = "lblCurrentFrame";
			this.lblCurrentFrame.Size = new System.Drawing.Size(11, 12);
			this.lblCurrentFrame.TabIndex = 4;
			this.lblCurrentFrame.Text = "0";
			this.zhGroupBox1.Controls.Add(this.label_Info_PortInfo);
			this.zhGroupBox1.Controls.Add(this.lblSendStatus);
			this.zhGroupBox1.Controls.Add(this.label_Info_OffOn);
			this.zhGroupBox1.Controls.Add(this.lblTotalFrame);
			this.zhGroupBox1.Controls.Add(this.label_Info_Luminance);
			this.zhGroupBox1.Controls.Add(this.pgbStatus);
			this.zhGroupBox1.Controls.Add(this.label_Info_Scantype);
			this.zhGroupBox1.Controls.Add(this.lblCurrentFrame);
			this.zhGroupBox1.Controls.Add(this.label_info_Panel);
			this.zhGroupBox1.Controls.Add(this.label_PortInfo);
			this.zhGroupBox1.Controls.Add(this.label4_OffOn);
			this.zhGroupBox1.Controls.Add(this.label1_PanelParam);
			this.zhGroupBox1.Controls.Add(this.label3_Lumiance);
			this.zhGroupBox1.Controls.Add(this.label2_Scantype);
			this.zhGroupBox1.Location = new System.Drawing.Point(8, 7);
			this.zhGroupBox1.Name = "zhGroupBox1";
			this.zhGroupBox1.Size = new System.Drawing.Size(523, 265);
			this.zhGroupBox1.TabIndex = 22;
			this.zhGroupBox1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(537, 276);
			base.Controls.Add(this.zhGroupBox1);
			base.Controls.Add(this.label3);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formSendData";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "发送数据";
			base.FormClosing += new FormClosingEventHandler(this.formSendData_FormClosing);
			base.Load += new EventHandler(this.formSendData_Load);
			this.zhGroupBox1.ResumeLayout(false);
			this.zhGroupBox1.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
