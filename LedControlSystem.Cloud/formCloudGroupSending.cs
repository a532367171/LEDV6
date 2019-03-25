using LedCommunication;
using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Data;
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
	public class formCloudGroupSending : Form
	{
		public static IList<Screen_Display_Class> screenSendGroup = new List<Screen_Display_Class>();

		private bool isProcessing;

		private int generateDataErrorCount;

		private int failureCount;

		private bool isThread;

		private formMain fm;

		private bool needtoClose;

		private DateTime dtStart;

		private DateTime dtEnd;

		private TimeSpan ts;

		private int thrCount;

		private int RecvTimeout = 120;

		private object thisLock;

		private int StateColor;

		private IContainer components;

		private DataGridView dgvSendState;

		private DataGridViewProgressColumn dataGridViewProgressColumn1;

		private Button btnResend;

		private System.Windows.Forms.Timer tmrSend;

		private DataGridViewTextBoxColumn ScreenNum;

		private DataGridViewTextBoxColumn ScreenSendTime;

		private DataGridViewTextBoxColumn ScreenName;

		private DataGridViewTextBoxColumn BasicInformation;

		private DataGridViewTextBoxColumn SendState;

		private DataGridViewProgressColumn SendProgress;

		private DataGridViewTextBoxColumn ProductID;

		public formCloudGroupSending(formMain fmain)
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

		public void Diplay_lanuage_Text()
		{
			this.Text = formMain.ML.GetStr("formGroupSending_FormText");
			this.btnResend.Text = formMain.ML.GetStr("formGroupSending_SendList_Faults_Resend");
			this.dgvSendState.Columns[0].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_SerialNumber");
			this.dgvSendState.Columns[1].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Time");
			this.dgvSendState.Columns[2].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Name");
			this.dgvSendState.Columns[3].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Signal");
			this.dgvSendState.Columns[4].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Status");
			this.dgvSendState.Columns[5].HeaderText = formMain.ML.GetStr("formGroupSending_DataGridView_Group_Send_Data_State_Schedule");
		}

		private void formCloudGroupSending_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.btnResend.Enabled = false;
			this.generateDataErrorCount = 0;
			this.failureCount = 0;
			this.isProcessing = false;
			this.isThread = true;
			this.needtoClose = false;
			this.thrCount = 0;
			this.thisLock = new object();
			this.tmrSend.Start();
		}

		private void formCloudGroupSending_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
				return;
			}
			try
			{
				this.isThread = false;
				this.tmrSend.Stop();
				if (formCloudGroupSending.screenSendGroup != null)
				{
					for (int i = 0; i < formCloudGroupSending.screenSendGroup.Count; i++)
					{
						formCloudGroupSending.screenSendGroup[i].Dispose();
						formCloudGroupSending.screenSendGroup[i] = null;
					}
					formCloudGroupSending.screenSendGroup.Clear();
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
			if (formCloudGroupSending.screenSendGroup == null || formCloudGroupSending.screenSendGroup.Count == 0)
			{
				this.btnResend.Enabled = true;
				return;
			}
			this.failureCount = 0;
			this.thrCount = 0;
			this.needtoClose = false;
			this.isThread = true;
			Thread thread = new Thread(new ThreadStart(this.Resend));
			thread.Start();
		}

		private void tmrSend_Tick(object sender, EventArgs e)
		{
			if (formCloudGroupSending.screenSendGroup == null || formCloudGroupSending.screenSendGroup.Count == 0)
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

		private void SingleSend(object obj)
		{
			int index = (int)obj;
			Screen_Display_Class sdc = formCloudGroupSending.screenSendGroup[index];
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
					DataGridViewCell dataGridViewCell = this.dgvSendState.Rows[index].Cells[4];
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

		private void SendData(int index)
		{
			if (index < 0)
			{
				return;
			}
			Screen_Display_Class sdc = formCloudGroupSending.screenSendGroup[index];
			LedPanel panel_NO = sdc.Panel_NO;
			new DataOperating();
			DataGridViewCell dgvc = this.dgvSendState.Rows[index].Cells[4];
			DataGridViewCell dgvcProgress = this.dgvSendState.Rows[index].Cells[5];
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
			if (flag)
			{
				sdc.State_Message = formMain.ML.GetStr("formGroupSending_CommunicationMessage_StartSending");
				this.StateColor = 0;
				base.Invoke(new MethodInvoker(delegate
				{
					dgvc.Value = sdc.State_Message;
					dgvc.Style.ForeColor = this.StateOfColor(this.StateColor);
				}));
				Thread.Sleep(500);
				IList<byte[]> list = new protocol_data_integration().Program_Packaging_data_L(sdc.Send_Data, panel_NO.CardAddress, panel_NO.ProtocolVersion, true);
				if (list == null || list.Count == 0)
				{
					this.failureCount++;
					string str = formMain.ML.GetStr("Display_CommunicationFailed");
					this.StateColor = 2;
					sdc.Fault_Message = str;
					sdc.State_Message = str;
					sdc.SendCompleted = true;
				}
				else
				{
					int i = 0;
					bool flag2 = false;
					bool flag3 = false;
					sdc.State_Message = formMain.ML.GetStr("Prompt_NowIsSendingData");
					this.StateColor = 0;
					base.Invoke(new MethodInvoker(delegate
					{
						dgvc.Value = sdc.State_Message;
						dgvc.Style.ForeColor = this.StateOfColor(this.StateColor);
					}));
					Thread.Sleep(500);
					string dataFilePath = string.Format("{0}\\Cloud\\{1}\\{2}", LedCommonConst.ProjectSaveDirectory, LedGlobal.CloudAccount.UserName, panel_NO.ID);
					while (i < 5)
					{
						i++;
						flag2 = new TransDataService().Send(LedGlobal.CloudAccount.SessionID, panel_NO.CloudID, list, dataFilePath, formMain.ML.GetStr("Message_Send_Item_Data"), ref flag3);
						if (flag2)
						{
							break;
						}
					}
					if (!flag2)
					{
						this.failureCount++;
						string str2 = formMain.ML.GetStr("Display_CommunicationFailed");
						this.StateColor = 2;
						sdc.Fault_Message = str2;
						sdc.State_Message = str2;
						sdc.SendCompleted = true;
					}
					else
					{
						sdc.State_Message = formMain.ML.GetStr("Message_Upload_To_Cloud_Success");
						if (flag3)
						{
							Screen_Display_Class expr_366 = sdc;
							expr_366.State_Message = expr_366.State_Message + "，" + formMain.ML.GetStr("Message_Send_Complete_By_Reviewing");
						}
						sdc.SendCompleted = true;
						this.StateColor = 1;
						if (sdc.Send_Data != null)
						{
							sdc.Send_Data.Dispose();
							sdc.Send_Data = null;
						}
					}
				}
				base.Invoke(new MethodInvoker(delegate
				{
					if (this.StateColor == 1)
					{
						dgvcProgress.Value = 100;
					}
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
			while (num < formCloudGroupSending.screenSendGroup.Count && this.isThread)
			{
				Screen_Display_Class screen_Display_Class = formCloudGroupSending.screenSendGroup[num];
				if (Array.IndexOf<string>(array, screen_Display_Class.State_Message) < 0 && screen_Display_Class.Send_Progress != 100)
				{
					screen_Display_Class.Send_Progress = 0;
					screen_Display_Class.ResendCount = 0;
					screen_Display_Class.SendCompleted = false;
					screen_Display_Class.Send_State = 0;
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
			bool arg_AB_0 = this.isThread;
		}

		private bool IsAllSended(ref List<int> indexList)
		{
			bool result = true;
			if (formCloudGroupSending.screenSendGroup != null)
			{
				for (int i = 0; i < formCloudGroupSending.screenSendGroup.Count; i++)
				{
					Screen_Display_Class screen_Display_Class = formCloudGroupSending.screenSendGroup[i];
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
			if (formCloudGroupSending.screenSendGroup != null)
			{
				int i;
				for (i = formCloudGroupSending.screenSendGroup.Count - 1; i > -1; i--)
				{
					Screen_Display_Class sdc = formCloudGroupSending.screenSendGroup[i];
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
			if (this.IsAllSendCompleted() && !this.isProcessing)
			{
				this.tmrSend.Stop();
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
						this.btnResend.Enabled = true;
					}
					this.needtoClose = true;
				}));
			}
		}

		private void EndAllSend()
		{
			this.dtStart = DateTime.Now;
			if (this.isProcessing)
			{
				return;
			}
			this.tmrSend.Stop();
			if (formCloudGroupSending.screenSendGroup != null)
			{
				for (int i = formCloudGroupSending.screenSendGroup.Count - 1; i > -1; i--)
				{
					Screen_Display_Class screen_Display_Class = formCloudGroupSending.screenSendGroup[i];
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
			this.isThread = false;
		}

		public void GetPanel()
		{
			string value = DateTime.Now.ToString();
			foreach (Screen_Display_Class current in formCloudGroupSending.screenSendGroup)
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
					else if (current.Panel_NO.EthernetCommunicaitonMode == LedEthernetCommunicationMode.LocalServer)
					{
						text += current.Panel_NO.NetworkID;
					}
					else
					{
						text += current.Panel_NO.DeviceID;
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
			this.dgvSendState.TabIndex = 36;
			this.dgvSendState.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvSendState_CellFormatting);
			this.dataGridViewProgressColumn1.HeaderText = "发送进度";
			this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
			this.dataGridViewProgressColumn1.Width = 150;
			this.btnResend.Location = new System.Drawing.Point(715, 300);
			this.btnResend.Name = "btnResend";
			this.btnResend.Size = new System.Drawing.Size(104, 30);
			this.btnResend.TabIndex = 37;
			this.btnResend.Text = "通讯失败[重发]";
			this.btnResend.UseVisualStyleBackColor = true;
			this.btnResend.Click += new EventHandler(this.btnResend_Click);
			this.tmrSend.Tick += new EventHandler(this.tmrSend_Tick);
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
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudGroupSending";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "云群组发送";
			base.FormClosing += new FormClosingEventHandler(this.formCloudGroupSending_FormClosing);
			base.Load += new EventHandler(this.formCloudGroupSending_Load);
			((ISupportInitialize)this.dgvSendState).EndInit();
			base.ResumeLayout(false);
		}
	}
}
