using HelloRemoting;
using LedControlSystem.Properties;
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
	public class formGroupSendingSingle : Form
	{
		private LedCmdType command;

		private object commData;

		private string operation;

		public static IList<Screen_Display_Class> screenSendGroup = new List<Screen_Display_Class>();

		private bool isProcessing;

		private int failureCount;

		private bool isThread;

		private formMain fm;

		private bool isRestarting;

		private bool needtoClose;

		private DateTime dtStart;

		private DateTime dtEnd;

		private TimeSpan ts;

		private Thread thrReceiveEvent;

		private bool isThreadReceiveEvent;

		private Queue<IPC_SIMPLE_ANSWER> isaQueue;

		private bool isReceiveEvent;

		private int thrCount;

		private object thisLock;

		private int RecvTimeout = 120;

		private int StateColor;

		private int MaxResendCount = 5;

		private IContainer components;

		private DataGridView dgvSendState;

		private DataGridViewProgressColumn dataGridViewProgressColumn1;

		private Button btnResend;

		private System.Windows.Forms.Timer tmrSend;

		private Label lblMessage;

		private DataGridViewTextBoxColumn ScreenNum;

		private DataGridViewTextBoxColumn ScreenSendTime;

		private DataGridViewTextBoxColumn ScreenName;

		private DataGridViewTextBoxColumn BasicInformation;

		private DataGridViewTextBoxColumn SendState;

		private DataGridViewProgressColumn SendProgress;

		private DataGridViewTextBoxColumn ProductID;

		public formGroupSendingSingle(LedCmdType pCommand, string pOperation, object pData, formMain fmain)
		{
			this.InitializeComponent();
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.UpdateStyles();
			this.command = pCommand;
			this.operation = pOperation;
			this.commData = pData;
			this.fm = fmain;
			this.Diplay_lanuage_Text();
		}

		public void Diplay_lanuage_Text()
		{
			this.Text = this.operation;
			this.btnResend.Text = formMain.ML.GetStr("formGroupSending_SendList_Faults_Resend");
			this.dgvSendState.Columns[0].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_SerialNumber");
			this.dgvSendState.Columns[1].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Time");
			this.dgvSendState.Columns[2].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Name");
			this.dgvSendState.Columns[3].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Signal");
			this.dgvSendState.Columns[4].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Status");
			this.dgvSendState.Columns[5].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Schedule");
		}

		private void formGroupSendingSingle_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			call.OnDeviceCmdReturnResult += new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
			this.btnResend.Enabled = false;
			this.failureCount = 0;
			this.isProcessing = false;
			this.isThread = true;
			this.isRestarting = false;
			this.needtoClose = false;
			this.thrCount = 0;
			this.thisLock = new object();
			this.isaQueue = new Queue<IPC_SIMPLE_ANSWER>();
			this.isThreadReceiveEvent = true;
			this.thrReceiveEvent = new Thread(new ThreadStart(this.ReceiveEvent));
			this.thrReceiveEvent.IsBackground = true;
			this.thrReceiveEvent.Start();
			Thread thread = new Thread(new ThreadStart(this.Send));
			thread.Start();
		}

		private void formGroupSendingSingle_FormClosing(object sender, FormClosingEventArgs e)
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
				this.tmrSend.Stop();
				if (formGroupSendingSingle.screenSendGroup != null)
				{
					for (int i = 0; i < formGroupSendingSingle.screenSendGroup.Count; i++)
					{
						formGroupSendingSingle.screenSendGroup[i].Dispose();
						formGroupSendingSingle.screenSendGroup[i] = null;
					}
					formGroupSendingSingle.screenSendGroup.Clear();
				}
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

		private void btnResend_Click(object sender, EventArgs e)
		{
			this.btnResend.Enabled = false;
			if (formGroupSendingSingle.screenSendGroup == null || formGroupSendingSingle.screenSendGroup.Count == 0)
			{
				this.btnResend.Enabled = true;
				return;
			}
			this.failureCount = 0;
			this.thrCount = 0;
			this.needtoClose = false;
			this.isThread = true;
			this.isaQueue = new Queue<IPC_SIMPLE_ANSWER>();
			Thread thread = new Thread(new ThreadStart(this.Resend));
			thread.Start();
		}

		private void dgvSendState_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex < 0)
			{
				return;
			}
			if (e.RowIndex >= 0 && !e.FormattingApplied && e.ColumnIndex == 4)
			{
				e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
			}
		}

		private void tmrSend_Tick(object sender, EventArgs e)
		{
			if (formGroupSendingSingle.screenSendGroup == null || formGroupSendingSingle.screenSendGroup.Count == 0)
			{
				this.tmrSend.Stop();
				this.needtoClose = true;
				return;
			}
			List<int> list = new List<int>();
			bool flag = this.IsAllSended(ref list);
			if (flag)
			{
				this.AllSendCompletedMessage();
				return;
			}
			if (this.thrCount > 0)
			{
				return;
			}
			if (list.Count == 0)
			{
				this.dtEnd = DateTime.Now;
				this.ts = this.dtEnd - this.dtStart;
				if (this.ts.TotalSeconds > 30.0)
				{
					this.EndAllSend();
				}
			}
		}

		private void OnDeviceCmdReturn(object sender, DeviceCmdEventArgs arg)
		{
			this.isReceiveEvent = true;
			IPC_SIMPLE_ANSWER isa = arg.isa;
			if (this.isaQueue != null)
			{
				this.isaQueue.Enqueue(isa);
			}
			this.isReceiveEvent = false;
		}

		private void Send()
		{
			if (this.isThread)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.dtStart = DateTime.Now;
					this.tmrSend.Start();
				}));
			}
			for (int i = 0; i < formGroupSendingSingle.screenSendGroup.Count; i++)
			{
				if (!this.isThread)
				{
					return;
				}
				ParameterizedThreadStart start = new ParameterizedThreadStart(this.SingleSend);
				Thread thread = new Thread(start);
				object parameter = i;
				thread.Start(parameter);
				this.thrCount++;
				Thread.Sleep(50);
			}
		}

		private void SingleSend(object obj)
		{
			int num = (int)obj;
			if (num < 0)
			{
				this.thrCount--;
				return;
			}
			Screen_Display_Class sdc = formGroupSendingSingle.screenSendGroup[num];
			LedPanel panel_NO = sdc.Panel_NO;
			DataGridViewCell dgvcState = this.dgvSendState.Rows[num].Cells[4];
			DataGridViewCell dgvcProgress = this.dgvSendState.Rows[num].Cells[5];
			bool flag = false;
			for (int i = 0; i < LedCommunicationConst.ProtocolSendVersionList.Length; i++)
			{
				if (panel_NO.ProtocolVersion == LedCommunicationConst.ProtocolSendVersionList[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				sdc.Send_State = 3;
				sdc.SendCompleted = true;
				sdc.State_Message = formMain.ML.GetStr("formGroupSending_CommunicationMessage_protocolVersionNumberInconsistent");
				this.StateColor = 2;
				base.Invoke(new MethodInvoker(delegate
				{
					dgvcState.Value = sdc.State_Message;
					dgvcState.Style.ForeColor = this.StateOfColor(this.StateColor);
				}));
				Thread.Sleep(500);
				this.thrCount--;
				return;
			}
			if (sdc.ResendCount == 0)
			{
				sdc.State_Message = formMain.ML.GetStr("formGroupSending_CommunicationMessage_StartSending");
				this.StateColor = 0;
				base.Invoke(new MethodInvoker(delegate
				{
					dgvcProgress.Value = 0;
					dgvcState.Value = sdc.State_Message;
					dgvcState.Style.ForeColor = this.StateOfColor(this.StateColor);
					dgvcState.ToolTipText = string.Empty;
					this.dgvSendState.Refresh();
				}));
				Thread.Sleep(800);
			}
			int j = 0;
			bool flag2 = false;
			while (j < 5)
			{
				j++;
				SEND_CMD_RET_VALUE sEND_CMD_RET_VALUE = SEND_CMD_RET_VALUE.ER_NONE_INIT;
				if (formMain.IServer != null)
				{
					sEND_CMD_RET_VALUE = formMain.IServer.send_cmd_to_device_async((byte)this.command, this.commData, panel_NO.ProductID);
				}
				if (sEND_CMD_RET_VALUE == SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK)
				{
					flag2 = true;
					break;
				}
				if (sEND_CMD_RET_VALUE != SEND_CMD_RET_VALUE.IPC_communication_FAILED || sEND_CMD_RET_VALUE != SEND_CMD_RET_VALUE.IPC_communication_FAILED)
				{
					break;
				}
				if (!this.isRestarting)
				{
					this.RestartIPCServer();
				}
				DateTime now = DateTime.Now;
				DateTime now2 = DateTime.Now;
				TimeSpan timeSpan = now2 - now;
				while (this.isRestarting)
				{
					if (timeSpan.TotalSeconds >= 120.0)
					{
						break;
					}
					Thread.Sleep(200);
					now2 = DateTime.Now;
					timeSpan = now2 - now;
				}
			}
			if (!flag2)
			{
				this.failureCount++;
				sdc.Send_State = 3;
				sdc.SendCompleted = true;
				sdc.State_Message = formMain.ML.GetStr("Display_CommunicationFailed");
				this.StateColor = 2;
				base.Invoke(new MethodInvoker(delegate
				{
					dgvcState.Value = sdc.State_Message;
					dgvcState.Style.ForeColor = this.StateOfColor(this.StateColor);
				}));
				Thread.Sleep(500);
			}
			else
			{
				sdc.State_Message = formMain.ML.GetStr("Prompt_NowIsSendingData");
				this.StateColor = 0;
				base.Invoke(new MethodInvoker(delegate
				{
					dgvcState.Value = sdc.State_Message;
					dgvcState.Style.ForeColor = this.StateOfColor(this.StateColor);
				}));
				Thread.Sleep(500);
			}
			this.thrCount--;
		}

		private void Resend()
		{
			string[] array = new string[]
			{
				formMain.ML.GetStr("Prompt_MemoryOverSize"),
				formMain.ML.GetStr("formGroupSending_CommunicationMessage_FailedToGenerateData")
			};
			base.Invoke(new MethodInvoker(delegate
			{
				if (this.isThread)
				{
					this.dtStart = DateTime.Now;
					this.tmrSend.Start();
				}
			}));
			if (!this.isThread)
			{
				return;
			}
			this.isThreadReceiveEvent = true;
			this.thrReceiveEvent = new Thread(new ThreadStart(this.ReceiveEvent));
			this.thrReceiveEvent.IsBackground = true;
			this.thrReceiveEvent.Start();
			for (int i = 0; i < formGroupSendingSingle.screenSendGroup.Count; i++)
			{
				if (!this.isThread)
				{
					return;
				}
				Screen_Display_Class screen_Display_Class = formGroupSendingSingle.screenSendGroup[i];
				if (Array.IndexOf<string>(array, screen_Display_Class.State_Message) < 0 && screen_Display_Class.Send_Progress != 100)
				{
					screen_Display_Class.Send_Progress = 0;
					screen_Display_Class.ResendCount = 0;
					screen_Display_Class.SendCompleted = false;
					screen_Display_Class.Send_State = 0;
					ParameterizedThreadStart start = new ParameterizedThreadStart(this.SingleSend);
					Thread thread = new Thread(start);
					object parameter = i;
					thread.Start(parameter);
					this.thrCount++;
					Thread.Sleep(50);
				}
			}
		}

		public System.Drawing.Color StateOfColor(int colorstate)
		{
			if (colorstate == 2)
			{
				return System.Drawing.Color.Red;
			}
			if (colorstate == 1)
			{
				return System.Drawing.Color.Green;
			}
			return System.Drawing.Color.Black;
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
						string product_id = isa.product_id;
						int rowIndex = -1;
						for (int i = 0; i < formGroupSendingSingle.screenSendGroup.Count; i++)
						{
							if (formGroupSendingSingle.screenSendGroup[i].Panel_NO.ProductID == product_id)
							{
								rowIndex = i;
								break;
							}
						}
						if (rowIndex < 0)
						{
							return;
						}
						formGroupSendingSingle.screenSendGroup[rowIndex].LastRecvTime = DateTime.Now;
						if ((byte)isa.cmd_id == (byte)this.command)
						{
							if (!isa.is_cmd_failed_flag)
							{
								if (isa.is_cmd_over_flag)
								{
									string msg = formMain.ML.GetStr("formSendData_FormText") + formMain.ML.GetStr("Display_Successed");
									this.StateColor = 1;
									formGroupSendingSingle.screenSendGroup[rowIndex].Send_State = 3;
									formGroupSendingSingle.screenSendGroup[rowIndex].ResendCount = 0;
									formGroupSendingSingle.screenSendGroup[rowIndex].State_Message = msg;
									formGroupSendingSingle.screenSendGroup[rowIndex].SendCompleted = true;
									if (formGroupSendingSingle.screenSendGroup[rowIndex].Send_Data != null)
									{
										formGroupSendingSingle.screenSendGroup[rowIndex].Send_Data.Dispose();
										formGroupSendingSingle.screenSendGroup[rowIndex].Send_Data = null;
									}
									base.Invoke(new MethodInvoker(delegate
									{
										this.dgvSendState.Rows[rowIndex].Cells[4].Value = msg;
										this.dgvSendState.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
										this.dgvSendState.Rows[rowIndex].Cells[5].Value = 100;
									}));
									Thread.Sleep(10);
								}
							}
							else if (isa.is_cmd_over_flag)
							{
								if (isa.er_code > 20)
								{
									if (formGroupSendingSingle.screenSendGroup[rowIndex].ResendCount + 1 < this.MaxResendCount)
									{
										formGroupSendingSingle.screenSendGroup[rowIndex].ResendCount++;
										ParameterizedThreadStart start = new ParameterizedThreadStart(this.SingleSend);
										Thread thread = new Thread(start);
										object parameter = rowIndex;
										thread.Start(parameter);
										this.thrCount++;
										Thread.Sleep(50);
									}
									else
									{
										this.failureCount++;
										formGroupSendingSingle.screenSendGroup[rowIndex].Send_State = 3;
										formGroupSendingSingle.screenSendGroup[rowIndex].SendCompleted = true;
										base.Invoke(new MethodInvoker(delegate
										{
											this.dgvSendState.Rows[rowIndex].Cells[4].Value = formMain.ML.GetStr("Prompt_CommFaileder");
											this.StateColor = 2;
											this.dgvSendState.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
										}));
										Thread.Sleep(10);
									}
								}
								else
								{
									this.failureCount++;
									formGroupSendingSingle.screenSendGroup[rowIndex].Send_State = 3;
									formGroupSendingSingle.screenSendGroup[rowIndex].SendCompleted = true;
									base.Invoke(new MethodInvoker(delegate
									{
										this.dgvSendState.Rows[rowIndex].Cells[4].Value = formMain.GetSendingCSWDescription("Sending_CSW", isa.er_code);
										this.StateColor = 2;
										this.dgvSendState.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
									}));
									Thread.Sleep(10);
								}
							}
						}
						Thread.Sleep(5);
					}
				}
			}
			if (this.isaQueue != null)
			{
				this.isaQueue.Clear();
				this.isaQueue = null;
			}
		}

		private bool IsAllSended(ref List<int> indexList)
		{
			bool result = true;
			if (formGroupSendingSingle.screenSendGroup != null)
			{
				for (int i = 0; i < formGroupSendingSingle.screenSendGroup.Count; i++)
				{
					Screen_Display_Class screen_Display_Class = formGroupSendingSingle.screenSendGroup[i];
					if (screen_Display_Class.Send_State != 3)
					{
						if (screen_Display_Class.Send_State == 1)
						{
							indexList.Add(i);
						}
						result = false;
					}
				}
			}
			return result;
		}

		private bool IsAllSendCompleted()
		{
			bool result = true;
			if (formGroupSendingSingle.screenSendGroup != null)
			{
				int i;
				for (i = formGroupSendingSingle.screenSendGroup.Count - 1; i > -1; i--)
				{
					Screen_Display_Class sdc = formGroupSendingSingle.screenSendGroup[i];
					DateTime now = DateTime.Now;
					if (!sdc.SendCompleted)
					{
						DateTime arg_80_0 = sdc.LastRecvTime;
						if ((now - sdc.LastRecvTime).TotalSeconds <= (double)this.RecvTimeout)
						{
							result = false;
							break;
						}
						this.failureCount++;
						string str = formMain.ML.GetStr("Display_CommunicationFailed");
						this.StateColor = 2;
						sdc.Fault_Message = str;
						sdc.State_Message = str;
						sdc.SendCompleted = true;
						base.Invoke(new MethodInvoker(delegate
						{
							this.dgvSendState.Rows[i].Cells[4].Value = sdc.State_Message;
							this.dgvSendState.Rows[i].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
						}));
					}
				}
			}
			return result;
		}

		private void AllSendCompletedMessage()
		{
			if (this.IsAllSendCompleted() && this.isaQueue.Count == 0 && !this.isProcessing)
			{
				this.tmrSend.Stop();
				this.isThreadReceiveEvent = false;
				this.isThread = false;
				base.Invoke(new MethodInvoker(delegate
				{
					if (this.failureCount == 0)
					{
						MessageBox.Show(this, formMain.ML.GetStr("Display_SendData") + formMain.ML.GetStr("Display_Successed"));
					}
					else
					{
						MessageBox.Show(this, formMain.ML.GetStr("Display_SendData") + formMain.ML.GetStr("Display_Completed") + "," + formMain.ML.GetStr("formGroupSending_CommunicationMessage_Partialdeliveryfailed"));
						this.btnResend.Enabled = true;
					}
					this.needtoClose = true;
				}));
			}
		}

		private void EndAllSend()
		{
			this.dtStart = DateTime.Now;
			if (this.isaQueue.Count > 0 || this.isProcessing)
			{
				return;
			}
			this.tmrSend.Stop();
			if (formGroupSendingSingle.screenSendGroup != null)
			{
				for (int i = formGroupSendingSingle.screenSendGroup.Count - 1; i > -1; i--)
				{
					Screen_Display_Class screen_Display_Class = formGroupSendingSingle.screenSendGroup[i];
					if (!screen_Display_Class.SendCompleted)
					{
						screen_Display_Class.SendCompleted = true;
						screen_Display_Class.State_Message = formMain.ML.GetStr("Prompt_CommFaileder");
						this.StateColor = 2;
						this.dgvSendState.Rows[i].Cells[4].Value = screen_Display_Class.State_Message;
						this.dgvSendState.Rows[i].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
					}
				}
			}
			MessageBox.Show(this, formMain.ML.GetStr("Display_SendData") + formMain.ML.GetStr("Display_Completed") + "，" + formMain.ML.GetStr("formGroupSending_CommunicationMessage_Partialdeliveryfailed"));
			this.btnResend.Enabled = true;
			this.needtoClose = true;
			this.isThreadReceiveEvent = false;
			this.isThread = false;
		}

		public void GetPanel()
		{
			string value = DateTime.Now.ToString();
			foreach (Screen_Display_Class current in formGroupSendingSingle.screenSendGroup)
			{
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.dgvSendState);
				dataGridViewRow.Cells[0].Value = current.Screen_Num;
				dataGridViewRow.Cells[1].Value = value;
				dataGridViewRow.Cells[2].Value = current.Panel_NO.TextName;
				string text = current.Panel_NO.CardType.ToString().Replace("_", "-") + "_";
				if (current.Panel_NO.PortType == LedPortType.SerialPort)
				{
					text += current.Panel_NO.SerialPortName;
				}
				else if (current.Panel_NO.PortType == LedPortType.Ethernet)
				{
					if (current.Panel_NO.EthernetCommunicaitonMode == LedEthernetCommunicationMode.Directly)
					{
						text += formMain.ML.GetStr("formpaneledit_radioButton_SendBroadcast");
					}
					else if (current.Panel_NO.EthernetCommunicaitonMode == LedEthernetCommunicationMode.FixedIP)
					{
						text += current.Panel_NO.IPAddress;
					}
					else
					{
						text += current.Panel_NO.NetworkID;
					}
				}
				else
				{
					text += current.Panel_NO.PortType.ToString();
				}
				dataGridViewRow.Cells[3].Value = text;
				dataGridViewRow.Cells[4].Value = current.State_Message;
				dataGridViewRow.Cells[5].Value = current.Send_Progress;
				dataGridViewRow.Cells[6].Value = current.Panel_NO.ProductID;
				this.dgvSendState.Rows.Add(dataGridViewRow);
			}
		}

		private void RestartIPCServer()
		{
			this.isRestarting = true;
			base.Invoke(new MethodInvoker(delegate
			{
				this.lblMessage.Visible = true;
				this.lblMessage.Text = formMain.ML.GetStr("formGroupSending_CommunicationMessage_CommunicationProcessIsBeingRestarted");
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
					this.lblMessage.Text = formMain.ML.GetStr("formGroupSending_CommunicationMessage_CommunicationProcessIsStarted");
				}));
			}
			else
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.lblMessage.Text = formMain.ML.GetStr("formGroupSending_CommunicationMessage_CommunicationProcessFailedToStart");
				}));
			}
			Thread.Sleep(1000);
			base.Invoke(new MethodInvoker(delegate
			{
				this.lblMessage.Visible = false;
			}));
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			this.dgvSendState = new DataGridView();
			this.dataGridViewProgressColumn1 = new DataGridViewProgressColumn();
			this.btnResend = new Button();
			this.tmrSend = new System.Windows.Forms.Timer(this.components);
			this.lblMessage = new Label();
			this.ScreenNum = new DataGridViewTextBoxColumn();
			this.ScreenSendTime = new DataGridViewTextBoxColumn();
			this.ScreenName = new DataGridViewTextBoxColumn();
			this.BasicInformation = new DataGridViewTextBoxColumn();
			this.SendState = new DataGridViewTextBoxColumn();
			this.SendProgress = new DataGridViewProgressColumn();
			this.ProductID = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dgvSendState).BeginInit();
			base.SuspendLayout();
			this.dgvSendState.AllowUserToAddRows = false;
			this.dgvSendState.AllowUserToResizeRows = false;
			this.dgvSendState.BackgroundColor = System.Drawing.SystemColors.Menu;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSendState.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSendState.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSendState.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ScreenNum,
				this.ScreenSendTime,
				this.ScreenName,
				this.BasicInformation,
				this.SendState,
				this.SendProgress,
				this.ProductID
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dgvSendState.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSendState.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.dgvSendState.Location = new System.Drawing.Point(16, 12);
			this.dgvSendState.Name = "dgvSendState";
			this.dgvSendState.ReadOnly = true;
			this.dgvSendState.RowHeadersVisible = false;
			this.dgvSendState.RowTemplate.Height = 23;
			this.dgvSendState.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvSendState.Size = new System.Drawing.Size(803, 275);
			this.dgvSendState.TabIndex = 31;
			this.dgvSendState.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvSendState_CellFormatting);
			this.dataGridViewProgressColumn1.HeaderText = "发送进度";
			this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
			this.dataGridViewProgressColumn1.Width = 150;
			this.btnResend.Location = new System.Drawing.Point(715, 300);
			this.btnResend.Name = "btnResend";
			this.btnResend.Size = new System.Drawing.Size(104, 30);
			this.btnResend.TabIndex = 32;
			this.btnResend.Text = "通讯失败[重发]";
			this.btnResend.UseVisualStyleBackColor = true;
			this.btnResend.Click += new EventHandler(this.btnResend_Click);
			this.tmrSend.Tick += new EventHandler(this.tmrSend_Tick);
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(14, 309);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(0, 12);
			this.lblMessage.TabIndex = 33;
			this.lblMessage.Visible = false;
			this.ScreenNum.HeaderText = "序号";
			this.ScreenNum.Name = "ScreenNum";
			this.ScreenNum.ReadOnly = true;
			this.ScreenNum.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.ScreenNum.Width = 60;
			this.ScreenSendTime.HeaderText = "时间";
			this.ScreenSendTime.Name = "ScreenSendTime";
			this.ScreenSendTime.ReadOnly = true;
			this.ScreenSendTime.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.ScreenSendTime.Width = 120;
			this.ScreenName.HeaderText = "名称";
			this.ScreenName.Name = "ScreenName";
			this.ScreenName.ReadOnly = true;
			this.ScreenName.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.ScreenName.Width = 120;
			this.BasicInformation.HeaderText = "信息";
			this.BasicInformation.Name = "BasicInformation";
			this.BasicInformation.ReadOnly = true;
			this.BasicInformation.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.BasicInformation.Width = 150;
			this.SendState.HeaderText = "状态";
			this.SendState.Name = "SendState";
			this.SendState.ReadOnly = true;
			this.SendState.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.SendState.Width = 200;
			this.SendProgress.HeaderText = "进度";
			this.SendProgress.Name = "SendProgress";
			this.SendProgress.ReadOnly = true;
			this.SendProgress.Width = 150;
			this.ProductID.HeaderText = "产品ID";
			this.ProductID.Name = "ProductID";
			this.ProductID.ReadOnly = true;
			this.ProductID.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(835, 342);
			base.Controls.Add(this.dgvSendState);
			base.Controls.Add(this.btnResend);
			base.Controls.Add(this.lblMessage);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGroupSendingSingle";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "群组设置";
			base.FormClosing += new FormClosingEventHandler(this.formGroupSendingSingle_FormClosing);
			base.Load += new EventHandler(this.formGroupSendingSingle_Load);
			((ISupportInitialize)this.dgvSendState).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
