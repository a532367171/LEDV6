using HelloRemoting;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Data;
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
	public class formGroupSending : Form
	{
		public static IList<Screen_Display_Class> screen_send_group = new List<Screen_Display_Class>();

		private bool isProcessing;

		private int generateDataErrorCount;

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

		private int MaxResendCount = 5;

		private static string formID = "formGroupSending";

		private int StateColor;

		private IContainer components;

		private DataGridView Group_Send_Data_State;

		private DataGridViewProgressColumn dataGridViewProgressColumn1;

		private Button SendList_Faults_Resend;

		private System.Windows.Forms.Timer tmrSend;

		private Label lblMsg;

		private DataGridViewTextBoxColumn ScreenNum;

		private DataGridViewTextBoxColumn ScreenSendTime;

		private DataGridViewTextBoxColumn ScreenName;

		private DataGridViewTextBoxColumn BasicInformation;

		private DataGridViewTextBoxColumn SendState;

		private DataGridViewProgressColumn SendProgress;

		private DataGridViewTextBoxColumn ProductID;

		public static string FormID
		{
			get
			{
				return formGroupSending.formID;
			}
			set
			{
				formGroupSending.formID = value;
			}
		}

		public formGroupSending(formMain fmain)
		{
			this.InitializeComponent();
			this.Diplay_lanuage_Text();
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.UpdateStyles();
			this.fm = fmain;
		}

		public formGroupSending()
		{
			this.InitializeComponent();
			this.Diplay_lanuage_Text();
		}

		public void Diplay_lanuage_Text()
		{
			this.Text = formMain.ML.GetStr("formGroupSending_FormText");
			this.SendList_Faults_Resend.Text = formMain.ML.GetStr("formGroupSending_SendList_Faults_Resend");
			this.Group_Send_Data_State.Columns[0].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_SerialNumber");
			this.Group_Send_Data_State.Columns[1].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Time");
			this.Group_Send_Data_State.Columns[2].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Name");
			this.Group_Send_Data_State.Columns[3].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Signal");
			this.Group_Send_Data_State.Columns[4].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Status");
			this.Group_Send_Data_State.Columns[5].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Schedule");
		}

		private void Group_Send_Data_State_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

		private void formGroupSending_Load(object sender, EventArgs e)
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
			this.SendList_Faults_Resend.Enabled = false;
			this.generateDataErrorCount = 0;
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
			Thread thread = new Thread(new ThreadStart(this.CardType));
			thread.Start();
		}

		private void formGroupSending_FormClosing(object sender, FormClosingEventArgs e)
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
				if (formGroupSending.screen_send_group != null)
				{
					for (int i = 0; i < formGroupSending.screen_send_group.Count; i++)
					{
						formGroupSending.screen_send_group[i].Dispose();
						formGroupSending.screen_send_group[i] = null;
					}
					formGroupSending.screen_send_group.Clear();
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

		private void SendList_Faults_Resend_Click(object sender, EventArgs e)
		{
			this.SendList_Faults_Resend.Enabled = false;
			if (formGroupSending.screen_send_group == null || formGroupSending.screen_send_group.Count == 0)
			{
				this.SendList_Faults_Resend.Enabled = true;
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

		private void tmrSend_Tick(object sender, EventArgs e)
		{
			if (formGroupSending.screen_send_group == null || formGroupSending.screen_send_group.Count == 0)
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
				return;
			}
			foreach (int current in list)
			{
				ParameterizedThreadStart start = new ParameterizedThreadStart(this.SingleSend);
				Thread thread = new Thread(start);
				object parameter = current;
				thread.Start(parameter);
				this.thrCount++;
				Thread.Sleep(50);
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

		private void CardType()
		{
			int num = 0;
			while (num < formGroupSending.screen_send_group.Count && this.isThread)
			{
				this.CheckCardType(num);
				num++;
			}
			if (this.isThread)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.dtStart = DateTime.Now;
					this.tmrSend.Start();
				}));
			}
		}

		private void SingleSend(object obj)
		{
			int index = (int)obj;
			Screen_Display_Class sdc = formGroupSending.screen_send_group[index];
			sdc.Send_State = 2;
			bool bFlg = false;
			if (sdc.Send_Data == null)
			{
				LedPanel panel = sdc.Panel_NO;
				Process process = new Process();
				DataOperating dataOpt = new DataOperating();
				string toolTip = string.Empty;
				bFlg = dataOpt.CheckData(panel);
				if (bFlg)
				{
					bFlg = dataOpt.CheckAnimationAndBackground(panel);
					if (bFlg)
					{
						lock (this.thisLock)
						{
							this.isProcessing = true;
							base.Invoke(new MethodInvoker(delegate
							{
								bFlg = dataOpt.GenerateData(panel, ref process);
								this.isProcessing = false;
							}));
							DateTime now = DateTime.Now;
							DateTime now2 = DateTime.Now;
							TimeSpan timeSpan = now2 - now;
							while (this.isProcessing && timeSpan.TotalSeconds < 120.0)
							{
								Thread.Sleep(1000);
								now2 = DateTime.Now;
								timeSpan = now2 - now;
							}
							goto IL_1BC;
						}
					}
					lock (this.thisLock)
					{
						bFlg = dataOpt.GenerateData(panel, ref process);
					}
					IL_1BC:
					if (bFlg)
					{
						sdc.Send_Data = process;
						sdc.State_Message = formMain.ML.GetStr("formGroupSending_CommunicationMessage_GenerateDataSuccessfully");
						this.StateColor = 1;
					}
				}
				if (!bFlg)
				{
					this.generateDataErrorCount++;
					sdc.Send_State = 3;
					sdc.SendCompleted = true;
					if (sdc.Send_Data != null)
					{
						sdc.Send_Data.Dispose();
						sdc.Send_Data = null;
					}
					sdc.Fault_Message = dataOpt.faultMessage;
					sdc.State_Message = formMain.ML.GetStr("formGroupSending_CommunicationMessage_FailedToGenerateData");
					this.StateColor = 2;
					toolTip = dataOpt.faultMessage;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					DataGridViewCell dataGridViewCell = this.Group_Send_Data_State.Rows[index].Cells[4];
					dataGridViewCell.Value = sdc.State_Message;
					dataGridViewCell.ToolTipText = toolTip;
					dataGridViewCell.Style.ForeColor = this.StateOfColor(this.StateColor);
				}));
				Thread.Sleep(500);
			}
			else
			{
				bFlg = true;
			}
			if (bFlg)
			{
				this.SendData(index);
				sdc.Send_State = 3;
			}
			this.thrCount--;
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

		private void CheckCardType(int index)
		{
			if (index < 0)
			{
				return;
			}
			Screen_Display_Class sdc = formGroupSending.screen_send_group[index];
			LedPanel panel_NO = sdc.Panel_NO;
			DataGridViewCell dgvcState = this.Group_Send_Data_State.Rows[index].Cells[4];
			DataGridViewCell dgvcProgress = this.Group_Send_Data_State.Rows[index].Cells[5];
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
				return;
			}
			sdc.State_Message = formMain.ML.GetStr("formGroupSending_CommunicationMessage_CheckTheModelNumber");
			this.StateColor = 0;
			base.Invoke(new MethodInvoker(delegate
			{
				dgvcProgress.Value = 0;
				dgvcState.Value = sdc.State_Message;
				dgvcState.Style.ForeColor = this.StateOfColor(this.StateColor);
				dgvcState.ToolTipText = string.Empty;
				this.Group_Send_Data_State.Refresh();
			}));
			Thread.Sleep(800);
			int j = 0;
			bool flag2 = false;
			while (j < 5)
			{
				j++;
				SEND_CMD_RET_VALUE sEND_CMD_RET_VALUE = SEND_CMD_RET_VALUE.ER_NONE_INIT;
				if (formMain.IServer != null)
				{
					sEND_CMD_RET_VALUE = formMain.IServer.send_cmd_to_device_async(17, null, panel_NO.ProductID);
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
		}

		private void SendData(int index)
		{
			if (index < 0)
			{
				return;
			}
			Screen_Display_Class sdc = formGroupSending.screen_send_group[index];
			LedPanel panel_NO = sdc.Panel_NO;
			new DataOperating();
			DataGridViewCell dgvc = this.Group_Send_Data_State.Rows[index].Cells[4];
			sdc.State_Message = formMain.ML.GetStr("formGroupSending_CommunicationMessage_ReadyToSend");
			this.StateColor = 0;
			base.Invoke(new MethodInvoker(delegate
			{
				dgvc.Value = sdc.State_Message;
				dgvc.Style.ForeColor = this.StateOfColor(this.StateColor);
			}));
			Thread.Sleep(500);
			bool flag = false;
			if (sdc.Send_Data != null)
			{
				if (sdc.Send_Data.GetBytesLength() > panel_NO.GetFlashCapacity() && !formMain.IsforeignTradeMode)
				{
					sdc.Fault_Message = formMain.ML.GetStr("Prompt_MemoryOverSize");
					sdc.State_Message = formMain.ML.GetStr("Prompt_MemoryOverSize");
					this.StateColor = 2;
					sdc.SendCompleted = true;
					base.Invoke(new MethodInvoker(delegate
					{
						dgvc.Value = sdc.State_Message;
						dgvc.Style.ForeColor = this.StateOfColor(this.StateColor);
					}));
					return;
				}
				flag = true;
			}
			if (formMain.IServer != null && flag)
			{
				sdc.State_Message = formMain.ML.GetStr("formGroupSending_CommunicationMessage_StartSending");
				this.StateColor = 0;
				base.Invoke(new MethodInvoker(delegate
				{
					dgvc.Value = sdc.State_Message;
					dgvc.Style.ForeColor = this.StateOfColor(this.StateColor);
				}));
				Thread.Sleep(500);
				int i = 0;
				while (i < 5)
				{
					i++;
					SEND_CMD_RET_VALUE sEND_CMD_RET_VALUE = formMain.IServer.send_cmd_to_device_async(1, sdc.Send_Data, panel_NO.ProductID);
					if (sEND_CMD_RET_VALUE == SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK)
					{
						sdc.State_Message = formMain.ML.GetStr("Prompt_NowIsSendingData");
						this.StateColor = 0;
						break;
					}
					if (sEND_CMD_RET_VALUE != SEND_CMD_RET_VALUE.IPC_communication_FAILED)
					{
						this.failureCount++;
						string str = formMain.ML.GetStr("Display_CommunicationFailed");
						this.StateColor = 2;
						sdc.Fault_Message = str;
						sdc.State_Message = str;
						sdc.SendCompleted = true;
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
				base.Invoke(new MethodInvoker(delegate
				{
					dgvc.Value = sdc.State_Message;
					dgvc.Style.ForeColor = this.StateOfColor(this.StateColor);
				}));
				Thread.Sleep(500);
			}
		}

		private void Resend()
		{
			string[] array = new string[]
			{
				formMain.ML.GetStr("Prompt_MemoryOverSize"),
				formMain.ML.GetStr("formGroupSending_CommunicationMessage_FailedToGenerateData")
			};
			int num = 0;
			while (num < formGroupSending.screen_send_group.Count && this.isThread)
			{
				Screen_Display_Class screen_Display_Class = formGroupSending.screen_send_group[num];
				if (Array.IndexOf<string>(array, screen_Display_Class.State_Message) < 0 && screen_Display_Class.Send_Progress != 100)
				{
					screen_Display_Class.Send_Progress = 0;
					screen_Display_Class.ResendCount = 0;
					screen_Display_Class.SendCompleted = false;
					screen_Display_Class.Send_State = 0;
					this.CheckCardType(num);
					Thread.Sleep(50);
				}
				num++;
			}
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
						for (int i = 0; i < formGroupSending.screen_send_group.Count; i++)
						{
							if (formGroupSending.screen_send_group[i].Panel_NO.ProductID == product_id)
							{
								rowIndex = i;
								break;
							}
						}
						if (rowIndex < 0)
						{
							return;
						}
						formGroupSending.screen_send_group[rowIndex].LastRecvTime = DateTime.Now;
						if ((byte)isa.cmd_id == 17)
						{
							if (!isa.is_cmd_failed_flag)
							{
								if (isa.is_cmd_over_flag)
								{
									if (isa.return_object == null || isa.return_object.GetType() != typeof(LedPanel))
									{
										return;
									}
									LedPanel ledPanel = isa.return_object as LedPanel;
									if (ledPanel.CardType == formGroupSending.screen_send_group[rowIndex].Panel_NO.CardType)
									{
										formGroupSending.screen_send_group[rowIndex].Send_State = 1;
										formGroupSending.screen_send_group[rowIndex].ResendCount = 0;
										formGroupSending.screen_send_group[rowIndex].State_Message = formMain.ML.GetStr("Prompt_NowIsGeneratingData");
										this.StateColor = 0;
										base.Invoke(new MethodInvoker(delegate
										{
											this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Value = formGroupSending.screen_send_group[rowIndex].State_Message;
											this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
										}));
									}
									else
									{
										formGroupSending.screen_send_group[rowIndex].Send_State = 3;
										formGroupSending.screen_send_group[rowIndex].SendCompleted = true;
										base.Invoke(new MethodInvoker(delegate
										{
											this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Value = formMain.ML.GetStr("Prompt_ProductModelError");
											this.StateColor = 2;
											this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
										}));
									}
									Thread.Sleep(10);
								}
							}
							else if (isa.is_cmd_over_flag)
							{
								if (isa.er_code > 20)
								{
									if (formGroupSending.screen_send_group[rowIndex].ResendCount + 1 < this.MaxResendCount)
									{
										formGroupSending.screen_send_group[rowIndex].ResendCount++;
										this.CheckCardType(rowIndex);
									}
									else
									{
										this.failureCount++;
										formGroupSending.screen_send_group[rowIndex].Send_State = 3;
										formGroupSending.screen_send_group[rowIndex].SendCompleted = true;
										base.Invoke(new MethodInvoker(delegate
										{
											this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Value = formMain.ML.GetStr("Prompt_CommFaileder");
											this.StateColor = 2;
											this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
										}));
										Thread.Sleep(10);
									}
								}
								else
								{
									this.failureCount++;
									formGroupSending.screen_send_group[rowIndex].Send_State = 3;
									formGroupSending.screen_send_group[rowIndex].SendCompleted = true;
									base.Invoke(new MethodInvoker(delegate
									{
										this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Value = formMain.GetSendingCSWDescription("Sending_CSW", isa.er_code);
										this.StateColor = 2;
										this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
									}));
									Thread.Sleep(10);
								}
							}
						}
						if ((byte)isa.cmd_id == 1)
						{
							if (formGroupSending.screen_send_group[rowIndex].Send_Progress < isa.pos)
							{
								formGroupSending.screen_send_group[rowIndex].Send_Progress = isa.pos;
								base.Invoke(new MethodInvoker(delegate
								{
									this.Group_Send_Data_State.Rows[rowIndex].Cells[5].Value = isa.pos;
								}));
								Thread.Sleep(10);
							}
							if (!isa.is_cmd_failed_flag)
							{
								if (isa.is_cmd_over_flag)
								{
									if (isa.pos != 100)
									{
										return;
									}
									string msg = formMain.ML.GetStr("formSendData_FormText") + formMain.ML.GetStr("Display_Successed");
									this.StateColor = 1;
									formGroupSending.screen_send_group[rowIndex].State_Message = msg;
									formGroupSending.screen_send_group[rowIndex].SendCompleted = true;
									if (formGroupSending.screen_send_group[rowIndex].Send_Data != null)
									{
										formGroupSending.screen_send_group[rowIndex].Send_Data.Dispose();
										formGroupSending.screen_send_group[rowIndex].Send_Data = null;
									}
									base.Invoke(new MethodInvoker(delegate
									{
										this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Value = msg;
										this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
									}));
									Thread.Sleep(10);
								}
							}
							else if (isa.is_cmd_over_flag)
							{
								if (isa.er_code > 20)
								{
									if (formGroupSending.screen_send_group[rowIndex].ResendCount + 1 < this.MaxResendCount)
									{
										formGroupSending.screen_send_group[rowIndex].ResendCount++;
										formGroupSending.screen_send_group[rowIndex].Send_State = 1;
										if (!this.tmrSend.Enabled)
										{
											this.tmrSend.Start();
										}
									}
									else
									{
										this.failureCount++;
										string msg = formMain.ML.GetStr("Display_SendData") + formMain.ML.GetStr("Display_Failed");
										this.StateColor = 2;
										formGroupSending.screen_send_group[rowIndex].State_Message = msg;
										formGroupSending.screen_send_group[rowIndex].SendCompleted = true;
										base.Invoke(new MethodInvoker(delegate
										{
											this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Value = msg;
											this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
										}));
										Thread.Sleep(10);
									}
								}
								else
								{
									this.failureCount++;
									string msg = formMain.GetSendingCSWDescription("Sending_CSW", isa.er_code);
									this.StateColor = 2;
									formGroupSending.screen_send_group[rowIndex].State_Message = msg;
									formGroupSending.screen_send_group[rowIndex].SendCompleted = true;
									base.Invoke(new MethodInvoker(delegate
									{
										this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Value = msg;
										this.Group_Send_Data_State.Rows[rowIndex].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
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
			if (formGroupSending.screen_send_group != null)
			{
				for (int i = 0; i < formGroupSending.screen_send_group.Count; i++)
				{
					Screen_Display_Class screen_Display_Class = formGroupSending.screen_send_group[i];
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
			if (formGroupSending.screen_send_group != null)
			{
				int i;
				for (i = formGroupSending.screen_send_group.Count - 1; i > -1; i--)
				{
					Screen_Display_Class sdc = formGroupSending.screen_send_group[i];
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
							this.Group_Send_Data_State.Rows[i].Cells[4].Value = sdc.State_Message;
							this.Group_Send_Data_State.Rows[i].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
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
						if (this.generateDataErrorCount > 0)
						{
							MessageBox.Show(this, formMain.ML.GetStr("Display_SendData") + formMain.ML.GetStr("Display_Completed") + "," + formMain.ML.GetStr("formGroupSending_CommunicationMessage_PartialGenerateDataFailed"));
						}
						else
						{
							MessageBox.Show(this, formMain.ML.GetStr("Display_SendData") + formMain.ML.GetStr("Display_Successed"));
						}
					}
					else
					{
						MessageBox.Show(this, formMain.ML.GetStr("Display_SendData") + formMain.ML.GetStr("Display_Completed") + "," + formMain.ML.GetStr("formGroupSending_CommunicationMessage_Partialdeliveryfailed"));
						this.SendList_Faults_Resend.Enabled = true;
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
			if (formGroupSending.screen_send_group != null)
			{
				for (int i = formGroupSending.screen_send_group.Count - 1; i > -1; i--)
				{
					Screen_Display_Class screen_Display_Class = formGroupSending.screen_send_group[i];
					if (!screen_Display_Class.SendCompleted)
					{
						screen_Display_Class.SendCompleted = true;
						screen_Display_Class.State_Message = formMain.ML.GetStr("Prompt_CommFaileder");
						this.StateColor = 2;
						this.Group_Send_Data_State.Rows[i].Cells[4].Value = screen_Display_Class.State_Message;
						this.Group_Send_Data_State.Rows[i].Cells[4].Style.ForeColor = this.StateOfColor(this.StateColor);
					}
				}
			}
			MessageBox.Show(this, formMain.ML.GetStr("Display_SendData") + formMain.ML.GetStr("Display_Completed") + "，" + formMain.ML.GetStr("formGroupSending_CommunicationMessage_Partialdeliveryfailed"));
			this.SendList_Faults_Resend.Enabled = true;
			this.needtoClose = true;
			this.isThreadReceiveEvent = false;
			this.isThread = false;
		}

		public void GetPanel()
		{
			string value = DateTime.Now.ToString();
			foreach (Screen_Display_Class current in formGroupSending.screen_send_group)
			{
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.Group_Send_Data_State);
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
				this.Group_Send_Data_State.Rows.Add(dataGridViewRow);
			}
		}

		private void RestartIPCServer()
		{
			this.isRestarting = true;
			base.Invoke(new MethodInvoker(delegate
			{
				this.lblMsg.Visible = true;
				this.lblMsg.Text = formMain.ML.GetStr("formGroupSending_CommunicationMessage_CommunicationProcessIsBeingRestarted");
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
					this.lblMsg.Text = formMain.ML.GetStr("formGroupSending_CommunicationMessage_CommunicationProcessIsStarted");
				}));
			}
			else
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.lblMsg.Text = formMain.ML.GetStr("formGroupSending_CommunicationMessage_CommunicationProcessFailedToStart");
				}));
			}
			Thread.Sleep(1000);
			base.Invoke(new MethodInvoker(delegate
			{
				this.lblMsg.Visible = false;
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
			this.Group_Send_Data_State = new DataGridView();
			this.dataGridViewProgressColumn1 = new DataGridViewProgressColumn();
			this.SendList_Faults_Resend = new Button();
			this.tmrSend = new System.Windows.Forms.Timer(this.components);
			this.lblMsg = new Label();
			this.ScreenNum = new DataGridViewTextBoxColumn();
			this.ScreenSendTime = new DataGridViewTextBoxColumn();
			this.ScreenName = new DataGridViewTextBoxColumn();
			this.BasicInformation = new DataGridViewTextBoxColumn();
			this.SendState = new DataGridViewTextBoxColumn();
			this.SendProgress = new DataGridViewProgressColumn();
			this.ProductID = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.Group_Send_Data_State).BeginInit();
			base.SuspendLayout();
			this.Group_Send_Data_State.AllowUserToAddRows = false;
			this.Group_Send_Data_State.AllowUserToResizeRows = false;
			this.Group_Send_Data_State.BackgroundColor = System.Drawing.SystemColors.Menu;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.Group_Send_Data_State.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.Group_Send_Data_State.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Group_Send_Data_State.Columns.AddRange(new DataGridViewColumn[]
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
			this.Group_Send_Data_State.DefaultCellStyle = dataGridViewCellStyle2;
			this.Group_Send_Data_State.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.Group_Send_Data_State.Location = new System.Drawing.Point(16, 12);
			this.Group_Send_Data_State.Name = "Group_Send_Data_State";
			this.Group_Send_Data_State.ReadOnly = true;
			this.Group_Send_Data_State.RowHeadersVisible = false;
			this.Group_Send_Data_State.RowTemplate.Height = 23;
			this.Group_Send_Data_State.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.Group_Send_Data_State.Size = new System.Drawing.Size(803, 275);
			this.Group_Send_Data_State.TabIndex = 14;
			this.dataGridViewProgressColumn1.HeaderText = "发送进度";
			this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
			this.dataGridViewProgressColumn1.Width = 150;
			this.SendList_Faults_Resend.Location = new System.Drawing.Point(715, 300);
			this.SendList_Faults_Resend.Name = "SendList_Faults_Resend";
			this.SendList_Faults_Resend.Size = new System.Drawing.Size(104, 30);
			this.SendList_Faults_Resend.TabIndex = 29;
			this.SendList_Faults_Resend.Text = "通讯失败[重发]";
			this.SendList_Faults_Resend.UseVisualStyleBackColor = true;
			this.SendList_Faults_Resend.Click += new EventHandler(this.SendList_Faults_Resend_Click);
			this.tmrSend.Tick += new EventHandler(this.tmrSend_Tick);
			this.lblMsg.AutoSize = true;
			this.lblMsg.Location = new System.Drawing.Point(14, 309);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new System.Drawing.Size(0, 12);
			this.lblMsg.TabIndex = 30;
			this.lblMsg.Visible = false;
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
			base.Controls.Add(this.lblMsg);
			base.Controls.Add(this.SendList_Faults_Resend);
			base.Controls.Add(this.Group_Send_Data_State);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGroupSending";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "群组发送";
			base.FormClosing += new FormClosingEventHandler(this.formGroupSending_FormClosing);
			base.Load += new EventHandler(this.formGroupSending_Load);
			((ISupportInitialize)this.Group_Send_Data_State).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
