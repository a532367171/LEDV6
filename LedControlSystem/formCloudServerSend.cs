using LedCommunication;
using LedControlSystem.CloudServer;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Content;
using LedModel.Data;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formCloudServerSend : Form
	{
		private delegate void SendProgramEventHandler();

		public formMain fmMain;

		public static bool IsLoadedPanelParam;

		private IList<Thread> threadList = new List<Thread>();

		private CloudServerComm login_zoehoo = new CloudServerComm();

		private IList<TerminalData> terminaldatalist = new List<TerminalData>();

		private Dictionary<string, TerminalData> DicTerminalId = new Dictionary<string, TerminalData>();

		private Dictionary<string, SingleCmdRecord> DicSingleCmd = new Dictionary<string, SingleCmdRecord>();

		private Dictionary<string, TransData> DicTransData = new Dictionary<string, TransData>();

		private Dictionary<string, CloudReviewRecord> DicReviewRecord = new Dictionary<string, CloudReviewRecord>();

		public bool IsAdvanced;

		private bool isAnimating;

		private bool isGroupSend;

		private string deviceCodes = string.Empty;

		private string SelectedTerminalId = string.Empty;

		private string SelectedSingleCmdId = string.Empty;

		private string SelectedTransDataId = string.Empty;

		private string SelectedReviewDataId = string.Empty;

		public IList<GroupOfCloudServer> GroupsOfUser = new List<GroupOfCloudServer>();

		public Dictionary<int, GroupOfCloudServer> DicGroupsOfUser = new Dictionary<int, GroupOfCloudServer>();

		public static GroupOfCloudServer SelectedGroupOfUser;

		private System.Threading.Timer threadCheckStatus;

		private IList<string> TransDataIds = new List<string>();

		private formCloudServerSend.SendProgramEventHandler sendCompleteEventHandler;

		private CloudRecordToXml RecordSave = new CloudRecordToXml();

		private int sideWidth;

		private static int MousePX;

		private static int MousePY;

		private IContainer components;

		private DataGridView DgvTerminalDatas;

		private Button buttonRefreash;

		private Button BtnSendProgram;

		private Button BtnDeleteProgramList;

		private DataGridView DgvTransdataIdList;

		private ComboBox CBOSingleCmd;

		private Button BtnSendSingleCmd;

		private Label LblCmd;

		private Label LblDeviceList;

		private ToolStrip ToolStripOperating;

		public Panel panel_Waitting;

		private Label label_Waitting_Picture;

		private PictureBox pictureBox2;

		private Button BtnReSendSingleCmd;

		private DataGridView DgvSingleCmdRecord;

		private Button BtnSingleCmdResendList;

		private ToolStripButton ToolStripBtnGroup;

		private ToolStripButton ToolStripBtnUserInfo;

		private Button BtnGroupSendProgram;

		private Button BtnExplanation;

		private DataGridViewTextBoxColumn SingleCmdRecordID;

		private DataGridViewTextBoxColumn SingleCmdRecordRequest;

		private DataGridViewTextBoxColumn SingleCmdRecordResponse;

		private DataGridViewTextBoxColumn SingleCmdRecordUpdate;

		private ComboBox CBOGroupSend;

		private Label LblGroups;

		private DataGridView DgvReviewList;

		private Button BtnPass;

		private Button BtnReject;

		private Button BtnReviewList;

		private Button BtnDownLoadReview;

		private ToolStripButton ToolStripBtnCardParameter;

		private RadioButton RdoAll;

		private RadioButton RdoPending;

		private RadioButton RdoReject;

		private RadioButton RdoPass;

		private CheckBox ChkSelectedAll;

		private CheckBox ChkOpposite;

		private Label label1;

		private Label label2;

		private Panel panel1;

		private Panel panel2;

		private Panel panel3;

		private Panel PnlReview;

		private ImageList imageListTelescopic;

		private ContextMenuStrip CtmsSelectFunction;

		private ToolStripMenuItem TsmiSelect;

		private ToolStripMenuItem TsmiCancel;

		private ContextMenuStrip CtmsDisplay;

		private ToolStripTextBox TstbDisplayID;

		private PictureBox pictureBoxUpAndDown;

		private ImageList imageListLeftAndRight;

		private PictureBox pictureBoxLeftAndRight;

		private Button BtnProgramSendList;

		private Label label3;

		private Panel panel4;

		private RadioButton RdoSendProgramAll;

		private RadioButton RdoSendProgramComplete;

		private RadioButton RdoSendProgramAbnormal;

		private RadioButton RdoSendProgramWait;

		private DataGridViewTextBoxColumn SourceName;

		private DataGridViewTextBoxColumn ForwardingTime;

		private DataGridViewTextBoxColumn ID;

		private DataGridViewTextBoxColumn Description;

		private DataGridViewTextBoxColumn DataStatus;

		private DataGridViewTextBoxColumn FrameLength;

		private DataGridViewCheckBoxColumn ColumnChkIsSelected;

		private DataGridViewTextBoxColumn TerminalName;

		private DataGridViewTextBoxColumn Stuats;

		private DataGridViewTextBoxColumn Model;

		private DataGridViewTextBoxColumn ColumnWidth;

		private DataGridViewTextBoxColumn ColumnHeight;

		private DataGridViewTextBoxColumn ColumnTerminalId;

		private DataGridViewTextBoxColumn ColumnSendStatus;

		private DataGridViewTextBoxColumn ColumnSendCmd;

		private DataGridViewTextBoxColumn ColumnCmdStatus;

		private DataGridViewTextBoxColumn No;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumnIndex;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumnUpdate;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn ColumnReviewTerminalId;

		public formCloudServerSend()
		{
			this.InitializeComponent();
			this.CBOSingleCmd.Items.Add(formMain.ML.GetStr("CloudServer_Ctrl_Power_On"));
			this.CBOSingleCmd.Items.Add(formMain.ML.GetStr("CloudServer_Ctrl_Power_Off"));
			this.CBOSingleCmd.Items.Add(formMain.ML.GetStr("CloudServer_Ctrl_Timing"));
			this.CBOSingleCmd.Items.Add(formMain.ML.GetStr("CloudServer_Ctrl_Switch"));
			this.CBOSingleCmd.Items.Add(formMain.ML.GetStr("CloudServer_Ctrl_Luminance"));
			this.CBOSingleCmd.Items.Add(formMain.ML.GetStr("CloudServer_Ctrl_Load"));
			formCloudServerSend.IsLoadedPanelParam = false;
		}

		private void DgvTerminalDatas_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			string arg_1B_0 = this.DgvTerminalDatas.Columns[e.ColumnIndex].Name;
			int arg_24_0 = e.RowIndex;
		}

		private void DgvTerminalDatas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex < 0)
			{
				return;
			}
			if (e.RowIndex >= 0 && !e.FormattingApplied && (e.ColumnIndex == 6 || e.ColumnIndex == 2 || e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9))
			{
				e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
			}
		}

		private void buttonRefreash_Click(object sender, EventArgs e)
		{
			IList<TerminalData> list = this.login_zoehoo.API_TerminalList();
			foreach (TerminalData current in list)
			{
				if (this.DicTerminalId.ContainsKey(current.Id))
				{
					int index = this.IndexOfDgvTerminalDatas(6, current.Id);
					if (current.Online)
					{
						this.DgvTerminalDatas.Rows[index].Cells[2].Value = formMain.ML.GetStr("formCloudServerSend_Messge_online");
						this.DgvTerminalDatas.Rows[index].Cells[2].Style.ForeColor = System.Drawing.Color.LimeGreen;
					}
					else
					{
						this.DgvTerminalDatas.Rows[index].Cells[2].Value = formMain.ML.GetStr("formCloudServerSend_Messge_offline");
						this.DgvTerminalDatas.Rows[index].Cells[2].Style.ForeColor = System.Drawing.Color.Red;
					}
					this.DgvTerminalDatas.Rows[index].Cells[1].ToolTipText = this.GetTerminalInfo(current);
					this.DgvTerminalDatas.Rows[index].Cells[4].Value = current.Width;
					this.DgvTerminalDatas.Rows[index].Cells[5].Value = current.Height;
					this.DicTerminalId[current.Id].Copy(current);
				}
				else
				{
					this.DicTerminalId.Add(current.Id, current);
					DataGridViewRow dataGridViewRow = new DataGridViewRow();
					dataGridViewRow.CreateCells(this.DgvTerminalDatas);
					dataGridViewRow.Cells[1].Value = current.TerminalName;
					dataGridViewRow.Cells[10].Value = current.TerminalCode;
					if (current.Online)
					{
						dataGridViewRow.Cells[2].Value = formMain.ML.GetStr("formCloudServerSend_Messge_online");
						dataGridViewRow.Cells[3].Style.ForeColor = System.Drawing.Color.LimeGreen;
					}
					else
					{
						dataGridViewRow.Cells[2].Value = formMain.ML.GetStr("formCloudServerSend_Messge_offline");
						dataGridViewRow.Cells[2].Style.ForeColor = System.Drawing.Color.Red;
					}
					dataGridViewRow.Cells[3].Value = current.ProductModelDescription.Replace("单双色", "");
					dataGridViewRow.Cells[4].Value = current.Width;
					dataGridViewRow.Cells[5].Value = current.Height;
					dataGridViewRow.Cells[6].Value = current.Id;
					dataGridViewRow.Cells[1].ToolTipText = this.GetTerminalInfo(current);
					this.DgvTerminalDatas.Rows.Add(dataGridViewRow);
				}
			}
		}

		public bool IsCurrentOfModel(string id)
		{
			return this.DicTerminalId[id].ProductModelDescription.IndexOf("单双色") > -1;
		}

		public string TerminalInformationOfModel(string id)
		{
			string text = string.Empty;
			if (this.IsCurrentOfModel(id))
			{
				text = this.DicTerminalId[id].ProductModelDescription;
				text = text.Replace("单双色", "").Replace("-", "_");
			}
			return text;
		}

		private void GenerateDataAndSend()
		{
			if (formMain.ledsys.SelectedPanel.Items.Count == 0)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Prompt_NoItem"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			if (this.fmMain.CheckSubareaOverLapping())
			{
				MessageBox.Show(this, this.fmMain.overlappingItem + formMain.ML.GetStr("Prompt_ItemSubareaOverlap"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			if (this.fmMain.isALlItemClosed)
			{
				MessageBox.Show(this, this.fmMain.overlappingItem + formMain.ML.GetStr("Prompt_AllItemClosed"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			if (this.fmMain.hasNullItem)
			{
				MessageBox.Show(this, this.fmMain.overlappingItem + formMain.ML.GetStr("Prompt_HasNullIitem"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			if (this.fmMain.CheckEmptyMarquee())
			{
				MessageBox.Show(this, formMain.ML.GetStr("Prompt_HasEmptyMarquee"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			if (!this.isGroupSend)
			{
				IList<string> list = this.RowsIsSelected();
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (list.Count <= 0)
				{
					MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_DeviceNotSelected"));
					return;
				}
				foreach (string current in list)
				{
					string terminalName = this.DicTerminalId[current].TerminalName;
					string value = string.Empty;
					if (this.DicTerminalId[current].Width != formMain.ledsys.SelectedPanel.Width.ToString() || this.DicTerminalId[current].Height != formMain.ledsys.SelectedPanel.Height.ToString())
					{
						value = formMain.ML.GetStr("formCloudServerSend_Message_PanelSizeError");
						dictionary.Add(terminalName, value);
					}
					else if (!this.IsCurrentOfModel(current))
					{
						value = formMain.ML.GetStr("formCloudServerSend_Messge_CardModeIsNotTwoColor");
						dictionary.Add(terminalName, value);
					}
					else if (this.TerminalInformationOfModel(current) != formMain.ledsys.SelectedPanel.CardType.ToString())
					{
						value = formMain.ML.GetStr("formCloudServerSend_Messge_CardModeError");
						dictionary.Add(terminalName, value);
					}
					else if (formMain.ledsys.SelectedPanel.ForeGray.ToString() != this.DicTerminalId[current].Gray)
					{
						value = formMain.ML.GetStr("formCloudServerSend_Messge_CardGrayError");
						dictionary.Add(terminalName, value);
					}
					else if (formMain.ledsys.SelectedPanel.BackgroundGray.ToString() != this.DicTerminalId[current].BackGray)
					{
						value = formMain.ML.GetStr("formCloudServerSend_Messge_CardGrayError");
						dictionary.Add(terminalName, value);
					}
					else if (formMain.ledsys.SelectedPanel.ColorMode.ToString().Length.ToString() != this.DicTerminalId[current].Color)
					{
						value = formMain.ML.GetStr("formCloudServerSend_Messge_CardColorError");
						dictionary.Add(terminalName, value);
					}
				}
				if (dictionary.Count > 0)
				{
					string text = string.Empty;
					foreach (string current2 in dictionary.Keys)
					{
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							current2,
							",",
							dictionary[current2],
							"\r\n"
						});
					}
					MessageBox.Show(text);
					return;
				}
			}
			Thread thread = new Thread(new ParameterizedThreadStart(this.StartSendData));
			this.threadList.Add(thread);
			thread.Start();
		}

		private void StartSendData(object obj)
		{
			base.Invoke(new MethodInvoker(delegate
			{
				this.label_Waitting_Picture.Text = formMain.ML.GetStr("Prompt_NowIsGeneratingData");
				this.SetWaittingPanelVisable(true);
			}));
			Thread.Sleep(500);
			LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
			for (int i = 0; i < selectedPanel.Items.Count; i++)
			{
				LedItem ledItem = selectedPanel.Items[i];
				for (int j = 0; j < ledItem.Subareas.Count; j++)
				{
					LedSubarea ledSubarea = ledItem.Subareas[j];
					if (ledSubarea.Type == LedSubareaType.Subtitle)
					{
						LedDText ledDText = (LedDText)ledSubarea.SelectedContent;
						if (ledDText.DoNeedDrawingFull)
						{
							ledDText.DrawMode = LedDrawMode.Full;
							ledDText.Draw();
						}
					}
					else if (ledSubarea.Type == LedSubareaType.PictureText)
					{
						for (int k = 0; k < ledSubarea.Contents.Count; k++)
						{
							LedPictureText ledPictureText = (LedPictureText)ledSubarea.Contents[k];
							if (ledPictureText.PictureTextType == LedPictureTextType.MultilineText || ledPictureText.DoNeedDrawingFull || (ledPictureText.LastDrawn != null && ledPictureText.GetSize() != ledPictureText.LastDrawn.Size))
							{
								ledPictureText.DrawMode = LedDrawMode.Full;
								ledPictureText.Draw();
							}
						}
					}
				}
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.CheckAnimationAndBackground();
				this.label_Waitting_Picture.Text = formMain.ML.GetStr("formCloudServerSend_Message_SendingData");
				this.label_Waitting_Picture.Refresh();
			}));
			Thread.Sleep(500);
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			TimeSpan timeSpan = now2 - now;
			while (this.isAnimating)
			{
				if (timeSpan.TotalSeconds >= 120.0)
				{
					break;
				}
				Thread.Sleep(1000);
				now2 = DateTime.Now;
				timeSpan = now2 - now;
			}
			try
			{
				Process process = new Process();
				process.PanelBytes = formMain.ledsys.SelectedPanel.ToBytes();
				process.ItemLBytes = formMain.ledsys.SelectedPanel.ToItemLBytes();
				process.BmpDataBytes = formMain.ledsys.SelectedPanel.ToItemBmpDataBytes();
				process.ItemTimerLBytes = formMain.ledsys.SelectedPanel.ToItemTimerLByte();
				process.ItemStartLBytes = formMain.ledsys.SelectedPanel.ToItemStartLBytes();
				IList<byte[]> sendlist = new List<byte[]>();
				protocol_data_integration protocol_data_integration = new protocol_data_integration();
				sendlist = protocol_data_integration.Program_Packaging_data_L(process, selectedPanel.CardAddress, selectedPanel.ProtocolVersion, true);
				string empty = string.Empty;
				if (!this.isGroupSend)
				{
					this.SendProgramData(1042, sendlist, out empty);
				}
				else
				{
					this.SendGroupProgramData(formCloudServerSend.SelectedGroupOfUser.Id, 1042, sendlist, out empty);
				}
			}
			catch (Exception ex)
			{
				Exception Ex = ex;
				base.Invoke(new MethodInvoker(delegate
				{
					MessageBox.Show(Ex.Message);
				}));
			}
			finally
			{
				this.SetWaittingPanelVisable(false);
			}
		}

		public string ProgramMark()
		{
			new Random(10);
			long ticks = DateTime.Now.Ticks;
			Random random = new Random((int)(ticks & -(long)((ulong)1)) | (int)(ticks >> 32));
			int num = random.Next();
			string dateTimeNow = LedCommon.GetDateTimeNow();
			return dateTimeNow + num.ToString();
		}

		private bool SendProgramData(int datalen, IList<byte[]> sendlist, out string result)
		{
			IList<string> list = this.RowsIsSelected();
			if (list.Count <= 0)
			{
				MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_DeviceNotSelected"));
				result = string.Empty;
				return false;
			}
			string str = "dataBase64=";
			byte[] array = new byte[datalen * sendlist.Count];
			for (int i = 0; i < sendlist.Count; i++)
			{
				Array.Copy(sendlist[i], 0, array, datalen * i, sendlist[i].Length);
			}
			str += HttpUtility.UrlEncode(Convert.ToBase64String(array));
			str = str + "&frameLength=" + datalen.ToString();
			string empty = string.Empty;
			foreach (string current in list)
			{
				string text = this.ProgramMark();
				string body = str + "&description=" + text;
				RecordUpdateProgramMark recordUpdateProgramMark = new RecordUpdateProgramMark();
				recordUpdateProgramMark.Mark = text;
				recordUpdateProgramMark.IsUpdated = true;
				if (this.RecordSave.ProgramMarkUpdate.ContainsKey(current))
				{
					this.RecordSave.ProgramMarkUpdate[current] = recordUpdateProgramMark;
				}
				else
				{
					this.RecordSave.ProgramMarkUpdate.Add(current, recordUpdateProgramMark);
				}
				TerminalResponseResult terminalResponseResult = this.login_zoehoo.API_PostTransdatas_DataBase64(current, body);
				int index = this.IndexOfDgvTerminalDatas(6, current);
				this.DgvTerminalDatas.Rows[index].Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_BtnSendProgram");
				this.DgvTerminalDatas.Rows[index].Cells[7].Style.ForeColor = System.Drawing.Color.Black;
				if (terminalResponseResult.Result && this.sendCompleteEventHandler != null)
				{
					this.sendCompleteEventHandler();
				}
			}
			result = empty;
			return false;
		}

		private bool SendGroupProgramData(string groupId, int datalen, IList<byte[]> sendlist, out string result)
		{
			string text = "dataBase64=";
			byte[] array = new byte[datalen * sendlist.Count];
			for (int i = 0; i < sendlist.Count; i++)
			{
				Array.Copy(sendlist[i], 0, array, datalen * i, sendlist[i].Length);
			}
			text += HttpUtility.UrlEncode(Convert.ToBase64String(array));
			text = text + "&frameLength=" + datalen.ToString();
			string empty = string.Empty;
			if (this.login_zoehoo.API_PostGroupTransdatas_DataBase64(groupId, text, out empty))
			{
				result = empty;
				return true;
			}
			result = empty;
			return false;
		}

		public void SetWaittingPanelVisable(bool pBool)
		{
			foreach (Control control in base.Controls)
			{
				Application.DoEvents();
				if (control.Name != "panel_Waitting")
				{
					control.Enabled = !pBool;
				}
			}
			if (pBool)
			{
				this.panel_Waitting.Location = new System.Drawing.Point((base.Width - this.panel_Waitting.Width) / 2, (base.Height - this.panel_Waitting.Height) / 2);
				this.panel_Waitting.Visible = true;
				this.panel_Waitting.BringToFront();
				this.panel_Waitting.Refresh();
				return;
			}
			this.panel_Waitting.Visible = false;
			this.panel_Waitting.SendToBack();
		}

		private void MakeAnimation(System.Drawing.Size pSize, LedBackground pBackground)
		{
			if (pBackground == null)
			{
				return;
			}
			if (pBackground.Changed && pBackground.Enabled)
			{
				base.TopMost = true;
				formAnimationMaker formAnimationMaker = new formAnimationMaker();
				formAnimationMaker.Make(pSize, pBackground);
				pBackground.Changed = false;
				formAnimationMaker.Dispose();
				base.TopMost = false;
			}
		}

		public void MakeAnimation(LedAnimation pAnimation)
		{
			if (pAnimation.Changed)
			{
				base.TopMost = true;
				formAnimationMaker formAnimationMaker = new formAnimationMaker();
				formAnimationMaker.Make(pAnimation.GetSize(), pAnimation);
				pAnimation.Changed = false;
				formAnimationMaker.Dispose();
				base.TopMost = false;
			}
		}

		private void CheckAnimationAndBackground()
		{
			this.isAnimating = true;
			LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
			for (int i = 0; i < selectedPanel.Items.Count; i++)
			{
				LedItem ledItem = selectedPanel.Items[i];
				this.MakeAnimation(new System.Drawing.Size(selectedPanel.Width, selectedPanel.Height), ledItem.Background);
				for (int j = 0; j < ledItem.Subareas.Count; j++)
				{
					LedSubarea ledSubarea = ledItem.Subareas[j];
					if (ledSubarea.Type == LedSubareaType.Animation)
					{
						LedAnimation ledAnimation = (LedAnimation)ledSubarea.SelectedContent;
						this.MakeAnimation(ledAnimation);
						this.MakeAnimation(ledSubarea.Size, ledAnimation.Background);
					}
					else if (ledSubarea.Type == LedSubareaType.PictureText)
					{
						for (int k = 0; k < ledSubarea.Contents.Count; k++)
						{
							LedPictureText ledPictureText = (LedPictureText)ledSubarea.Contents[k];
							this.MakeAnimation(ledSubarea.Size, ledPictureText.Background);
							if (ledPictureText.PictureTextType == LedPictureTextType.Animation)
							{
								this.MakeAnimation((LedAnimation)ledPictureText);
							}
						}
					}
					else if (ledSubarea.Type == LedSubareaType.Subtitle)
					{
						LedDText ledDText = (LedDText)ledSubarea.SelectedContent;
						this.MakeAnimation(ledSubarea.Size, ledDText.Background);
					}
					else
					{
						this.MakeAnimation(ledSubarea.Size, ledSubarea.SelectedContent.Background);
					}
				}
			}
			this.isAnimating = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.isGroupSend = false;
			this.GenerateDataAndSend();
		}

		private void BtnDeleteProgramList_Click(object sender, EventArgs e)
		{
			foreach (string current in this.TransDataIds)
			{
				this.login_zoehoo.API_DeleteTransdatas(this.SelectedTerminalId, current);
			}
			foreach (DataGridViewRow dataGridViewRow in this.DgvTransdataIdList.SelectedRows)
			{
				this.DgvTransdataIdList.Rows.RemoveAt(dataGridViewRow.Index);
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			IList<TransData> list = this.login_zoehoo.API_GetTransdatas(this.SelectedTerminalId);
			if (list == null)
			{
				return;
			}
			this.DgvTransdataIdList.Rows.Clear();
			this.DicTransData.Clear();
			foreach (TransData current in list)
			{
				this.DicTransData.Add(current.ID, current);
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.DgvTransdataIdList);
				dataGridViewRow.Cells[0].Value = current.ID;
				dataGridViewRow.Cells[1].Value = current.Description;
				if (current.Status == TransDataStatus.WaitingForReception)
				{
					dataGridViewRow.Cells[2].Value = "等待";
				}
				else if (current.Status == TransDataStatus.CompleteReception)
				{
					dataGridViewRow.Cells[2].Value = "完成";
				}
				else
				{
					dataGridViewRow.Cells[2].Value = "异常";
				}
				dataGridViewRow.Cells[3].Value = current.FrameLength;
				this.DgvTransdataIdList.Rows.Add(dataGridViewRow);
			}
		}

		private void formCloudServerSend_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.Text = formMain.ML.GetStr("formCloudServerSend_FormText");
			this.ToolStripBtnCardParameter.Text = formMain.ML.GetStr("formCloudServerSend_DeviceInfo");
			this.ToolStripBtnUserInfo.Text = formMain.ML.GetStr("formCloudServerSend_UserInfo");
			this.ToolStripBtnGroup.Text = formMain.ML.GetStr("formCloudServerSend_GroupInfo");
			this.LblDeviceList.Text = formMain.ML.GetStr("formCloudServerSend_DeviceList");
			this.buttonRefreash.Text = formMain.ML.GetStr("formCloudServerSend_BtnRefresh");
			this.BtnSendProgram.Text = formMain.ML.GetStr("formCloudServerSend_BtnSendProgram");
			this.LblGroups.Text = formMain.ML.GetStr("formCloudServerSend_LblGroupText");
			this.BtnGroupSendProgram.Text = formMain.ML.GetStr("formCloudServerSend_BtnGroupSend");
			this.LblCmd.Text = formMain.ML.GetStr("formCloudServerSend_LblCmdText");
			this.BtnSendSingleCmd.Text = formMain.ML.GetStr("formCloudServerSend_BtnCmdSend");
			this.BtnReviewList.Text = formMain.ML.GetStr("formCloudServerSend_BtnRefresh");
			this.BtnPass.Text = formMain.ML.GetStr("formCloudServerSend_BtnReview_Pass");
			this.BtnReject.Text = formMain.ML.GetStr("formCloudServerSend_BtnReview_Reject");
			this.RdoAll.Text = formMain.ML.GetStr("formCloudServerSend_RdoReview_All");
			this.RdoPending.Text = formMain.ML.GetStr("formCloudServerSend_RdoReview_Wait");
			this.RdoPass.Text = formMain.ML.GetStr("formCloudServerSend_RdoReview_Pass");
			this.RdoReject.Text = formMain.ML.GetStr("formCloudServerSend_RdoReview_Reject");
			this.label1.Text = formMain.ML.GetStr("formCloudServerSend_LblReviewList");
			this.ChkSelectedAll.Text = formMain.ML.GetStr("formCloudServerSend_ChkSelectAll");
			this.ChkOpposite.Text = formMain.ML.GetStr("formCloudServerSend_ChkSelectReverse");
			this.TsmiSelect.Text = formMain.ML.GetStr("formSelectIndex_FormText");
			this.TsmiCancel.Text = formMain.ML.GetStr("formSelectIndex_buttonCancel");
			this.RdoSendProgramAll.Text = formMain.ML.GetStr("formCloudServerSend_RdoReview_All");
			this.RdoSendProgramWait.Text = formMain.ML.GetStr("formCloudServerSend_RdoWaitingForReception");
			this.RdoSendProgramComplete.Text = formMain.ML.GetStr("formCloudServerSend_RdoCompleteReception");
			this.RdoSendProgramAbnormal.Text = formMain.ML.GetStr("formCloudServerSend_RdoDownloadException");
			this.BtnProgramSendList.Text = formMain.ML.GetStr("formCloudServerSend_Dgv_Refresh");
			this.BtnDeleteProgramList.Text = formMain.ML.GetStr("UpdateButton_Delete");
			this.label3.Text = formMain.ML.GetStr("formCloudServerSend_LblForwardDataList");
			this.DgvTerminalDatas.Columns[0].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_Select");
			this.DgvTerminalDatas.Columns[1].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_TerminalName");
			this.DgvTerminalDatas.Columns[10].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_Number");
			this.DgvTerminalDatas.Columns[2].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_State");
			this.DgvTerminalDatas.Columns[3].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_Model");
			this.DgvTerminalDatas.Columns[4].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_Width");
			this.DgvTerminalDatas.Columns[5].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_Height");
			this.DgvTerminalDatas.Columns[7].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_SendStatus");
			this.DgvTerminalDatas.Columns[8].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_SendCmd");
			this.DgvTerminalDatas.Columns[9].HeaderText = formMain.ML.GetStr("formCloudServerSend_Dgv_CmdStatus");
			this.DgvReviewList.Columns[0].HeaderText = formMain.ML.GetStr("formCloudServerSend_DgvReviewList_Code");
			this.DgvReviewList.Columns[2].HeaderText = formMain.ML.GetStr("formCloudServerSend_DgvReviewList_TerminalName");
			this.DgvReviewList.Columns[3].HeaderText = formMain.ML.GetStr("formCloudServerSend_DgvReviewList_Create");
			this.DgvReviewList.Columns[4].HeaderText = formMain.ML.GetStr("formCloudServerSend_DgvReviewList_Update");
			this.DgvReviewList.Columns[6].HeaderText = formMain.ML.GetStr("formCloudServerSend_DgvReviewList_Status");
			this.DgvTransdataIdList.Columns[0].HeaderText = formMain.ML.GetStr("formCloudServerSend_DgvTransdataIdList_TerminalName");
			this.DgvTransdataIdList.Columns[1].HeaderText = formMain.ML.GetStr("formCloudServerSend_DgvTransdataIdList_SendTime");
			this.DgvTransdataIdList.Columns[4].HeaderText = formMain.ML.GetStr("formCloudServerSend_DgvTransdataIdList_Status");
			this.DgvTransdataIdList.Columns[5].HeaderText = formMain.ML.GetStr("formCloudServerSend_DgvTransdataIdList_FrameLength");
			this.sideWidth = base.Width - this.ToolStripOperating.Width;
			this.pictureBoxLeftAndRight.Image = this.imageListLeftAndRight.Images[0];
			this.pictureBoxLeftAndRight.Location = new System.Drawing.Point(this.DgvTerminalDatas.Width + this.DgvTerminalDatas.Location.X + 15, this.DgvTerminalDatas.Location.Y + this.DgvTerminalDatas.Height / 2 - this.pictureBoxLeftAndRight.Height / 2);
			this.PnlReview.Visible = false;
			base.Width = this.pictureBoxLeftAndRight.Location.X + this.pictureBoxLeftAndRight.Width + this.sideWidth + 10;
			this.RecordSave.ReadCloudRecord();
			this.CBOSingleCmd.SelectedIndex = 0;
			this.SetWaittingPanelVisable(false);
			this.terminaldatalist = this.login_zoehoo.API_TerminalList();
			this.DicTerminalId.Clear();
			if (this.terminaldatalist != null)
			{
				foreach (TerminalData current in this.terminaldatalist)
				{
					this.DicTerminalId.Add(current.Id, current);
					DataGridViewRow dataGridViewRow = new DataGridViewRow();
					dataGridViewRow.CreateCells(this.DgvTerminalDatas);
					dataGridViewRow.Cells[1].Value = current.TerminalName;
					dataGridViewRow.Cells[10].Value = current.TerminalCode;
					if (current.Online)
					{
						dataGridViewRow.Cells[2].Value = formMain.ML.GetStr("formCloudServerSend_Messge_online");
						dataGridViewRow.Cells[2].Style.ForeColor = System.Drawing.Color.LimeGreen;
					}
					else
					{
						dataGridViewRow.Cells[2].Value = formMain.ML.GetStr("formCloudServerSend_Messge_offline");
						dataGridViewRow.Cells[2].Style.ForeColor = System.Drawing.Color.Red;
					}
					dataGridViewRow.Cells[3].Value = current.ProductModelDescription.Replace("单双色", "");
					dataGridViewRow.Cells[4].Value = current.Width;
					dataGridViewRow.Cells[5].Value = current.Height;
					dataGridViewRow.Cells[6].Value = current.Id;
					if (this.RecordSave.TerminalRecords.ContainsKey(current.Id))
					{
						if (this.RecordSave.TerminalRecords[current.Id].LastProgram != "")
						{
							if (this.RecordSave.TerminalRecords[current.Id].LastProgramSendStatus == "-3")
							{
								dataGridViewRow.Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_FormatError");
								dataGridViewRow.Cells[7].Style.ForeColor = System.Drawing.Color.Red;
							}
							else if (this.RecordSave.TerminalRecords[current.Id].LastProgramSendStatus == "-2")
							{
								dataGridViewRow.Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_StoreError");
								dataGridViewRow.Cells[7].Style.ForeColor = System.Drawing.Color.Red;
							}
							else if (this.RecordSave.TerminalRecords[current.Id].LastProgramSendStatus == "-1")
							{
								dataGridViewRow.Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_DataError");
								dataGridViewRow.Cells[7].Style.ForeColor = System.Drawing.Color.Red;
							}
							else if (this.RecordSave.TerminalRecords[current.Id].LastProgramSendStatus == "0")
							{
								dataGridViewRow.Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_RdoReview_Reject");
								dataGridViewRow.Cells[7].Style.ForeColor = System.Drawing.Color.Red;
							}
							else if (this.RecordSave.TerminalRecords[current.Id].LastProgramSendStatus == "1")
							{
								dataGridViewRow.Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_RdoReview_Wait");
								dataGridViewRow.Cells[7].Style.ForeColor = System.Drawing.Color.Orange;
							}
							else if (this.RecordSave.TerminalRecords[current.Id].LastProgramSendStatus == "2")
							{
								dataGridViewRow.Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_PassedAndWaitRec");
								dataGridViewRow.Cells[7].Style.ForeColor = System.Drawing.Color.Orange;
							}
							else if (this.RecordSave.TerminalRecords[current.Id].LastProgramSendStatus == "3")
							{
								dataGridViewRow.Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_RecComplete");
								dataGridViewRow.Cells[7].Style.ForeColor = System.Drawing.Color.LimeGreen;
							}
						}
						if (this.RecordSave.TerminalRecords[current.Id].LastSendCmdMark != "")
						{
							dataGridViewRow.Cells[8].Value = this.RecordSave.TerminalRecords[current.Id].LastCommand;
							if (this.RecordSave.TerminalRecords[current.Id].LastCmdSendStatus == "0")
							{
								dataGridViewRow.Cells[9].Value = formMain.ML.GetStr("formCloudServerSend_Messge_CmdUploadFailed");
								dataGridViewRow.Cells[9].Style.ForeColor = System.Drawing.Color.Red;
							}
							else if (this.RecordSave.TerminalRecords[current.Id].LastCmdSendStatus == "1")
							{
								dataGridViewRow.Cells[9].Value = formMain.ML.GetStr("formCloudServerSend_Messge_CmdRecFailed");
								dataGridViewRow.Cells[9].Style.ForeColor = System.Drawing.Color.Red;
							}
							else
							{
								dataGridViewRow.Cells[9].Value = formMain.ML.GetStr("formCloudServerSend_Messge_CmdRecSuccessed");
								dataGridViewRow.Cells[9].Style.ForeColor = System.Drawing.Color.LimeGreen;
							}
						}
					}
					dataGridViewRow.Cells[1].ToolTipText = this.GetTerminalInfo(current);
					this.DgvTerminalDatas.Rows.Add(dataGridViewRow);
				}
			}
			this.GroupsOfUser = this.login_zoehoo.API_GetGroupsList();
			if (this.GroupsOfUser.Count > 0)
			{
				for (int i = 0; i < this.GroupsOfUser.Count; i++)
				{
					this.CBOGroupSend.Items.Add(this.GroupsOfUser[i].GroupName);
					this.DicGroupsOfUser.Add(i, this.GroupsOfUser[i]);
				}
				this.CBOGroupSend.SelectedIndex = 0;
				formCloudServerSend.SelectedGroupOfUser = this.DicGroupsOfUser[0];
			}
			this.RdoSendProgramWait.Checked = true;
			this.RdoPending.Checked = true;
			this.threadCheckStatus = new System.Threading.Timer(new TimerCallback(this.timerSendStatus), null, 0, 3000);
			this.sendCompleteEventHandler = (formCloudServerSend.SendProgramEventHandler)Delegate.Combine(this.sendCompleteEventHandler, new formCloudServerSend.SendProgramEventHandler(this.DataRefreash));
		}

		public void DataRefreash()
		{
			if (base.Width > 900)
			{
				this.BtnProgramSendList_Click(null, null);
				this.BtnReviewList_Click(null, null);
			}
		}

		private void formCloudServerSend_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.threadCheckStatus != null)
			{
				this.threadCheckStatus.Dispose();
			}
			this.sendCompleteEventHandler = (formCloudServerSend.SendProgramEventHandler)Delegate.Remove(this.sendCompleteEventHandler, new formCloudServerSend.SendProgramEventHandler(this.DataRefreash));
		}

		private void BtnSendSingleCmd_Click(object sender, EventArgs e)
		{
			string value = "";
			object sendData = null;
			LedCmdType cmd_Type = LedCmdType.Null;
			IList<string> list = this.RowsIsSelected();
			new Dictionary<string, string>();
			if (list.Count <= 0)
			{
				MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_DeviceNotSelected"));
				return;
			}
			switch (this.CBOSingleCmd.SelectedIndex)
			{
			case 0:
				value = formMain.ML.GetStr("CloudServer_Ctrl_Power_On");
				cmd_Type = LedCmdType.Ctrl_Power_On;
				sendData = null;
				break;
			case 1:
				value = formMain.ML.GetStr("CloudServer_Ctrl_Power_Off");
				cmd_Type = LedCmdType.Ctrl_Power_Off;
				sendData = null;
				break;
			case 2:
				value = formMain.ML.GetStr("CloudServer_Ctrl_Timing");
				cmd_Type = LedCmdType.Ctrl_Timing;
				sendData = null;
				break;
			case 3:
				value = formMain.ML.GetStr("CloudServer_Ctrl_Switch");
				cmd_Type = LedCmdType.Ctrl_Timer_Switch;
				sendData = formMain.ledsys.SelectedPanel.TimerSwitch;
				break;
			case 4:
				value = formMain.ML.GetStr("CloudServer_Ctrl_Luminance");
				cmd_Type = LedCmdType.Ctrl_Luminance;
				sendData = formMain.ledsys.SelectedPanel.Luminance;
				break;
			case 5:
			{
				value = formMain.ML.GetStr("CloudServer_Ctrl_Load");
				bool flag = false;
				foreach (string current in list)
				{
					if (this.TerminalInformationOfModel(current) != formMain.ledsys.SelectedPanel.CardType.ToString())
					{
						flag = true;
					}
				}
				if (flag)
				{
					MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_CardModeError"));
					return;
				}
				cmd_Type = LedCmdType.Send_Panel_Parameter;
				sendData = formMain.ledsys.SelectedPanel;
				break;
			}
			case 6:
				value = formMain.ML.GetStr("CloudServer_Ctrl_ReadParameter");
				cmd_Type = LedCmdType.Read_Panel_Parameter;
				sendData = null;
				break;
			}
			IList<byte[]> list2 = protocol_single_cmd.Send_Pack(0, 0, cmd_Type, sendData, false, null, "", formMain.ledsys.SelectedPanel.ProtocolVersion);
			string str = HttpUtility.UrlEncode(Convert.ToBase64String(list2[0]));
			foreach (string current2 in list)
			{
				string text = "requestBase64=" + str + "&description=";
				string text2 = this.ProgramMark();
				text += text2;
				RecordUpdateCmdMark recordUpdateCmdMark = new RecordUpdateCmdMark();
				recordUpdateCmdMark.Mark = text2;
				recordUpdateCmdMark.IsUpdated = true;
				if (this.RecordSave.CmdMarkUpdate.ContainsKey(current2))
				{
					this.RecordSave.CmdMarkUpdate[current2] = recordUpdateCmdMark;
				}
				else
				{
					this.RecordSave.CmdMarkUpdate.Add(current2, recordUpdateCmdMark);
				}
				int index = this.IndexOfDgvTerminalDatas(6, current2);
				this.DgvTerminalDatas.Rows[index].Cells[8].Value = value;
				this.DgvTerminalDatas.Rows[index].Cells[9].Value = formMain.ML.GetStr("formCloudServerSend_Messge_CmdSend");
				this.DgvTerminalDatas.Rows[index].Cells[9].Style.ForeColor = System.Drawing.Color.Black;
				this.login_zoehoo.API_SendSingleCmd(current2, text);
			}
		}

		private void GroupsOfScanRouting_SelectionChanged(object sender, EventArgs e)
		{
			if (this.DgvTerminalDatas.SelectedRows.Count > 0)
			{
				this.SelectedTerminalId = this.DgvTerminalDatas.SelectedRows[0].Cells["ColumnTerminalId"].Value.ToString();
			}
		}

		private void DgvTransdataIdList_SelectionChanged(object sender, EventArgs e)
		{
			this.TransDataIds.Clear();
			if (this.DgvTransdataIdList.SelectedRows.Count > 0)
			{
				foreach (DataGridViewRow dataGridViewRow in this.DgvTransdataIdList.SelectedRows)
				{
					this.TransDataIds.Add(dataGridViewRow.Cells[2].Value.ToString());
				}
			}
		}

		private void DgvSingleCmdRecord_SelectionChanged(object sender, EventArgs e)
		{
			if (this.DgvSingleCmdRecord.SelectedRows.Count > 0)
			{
				this.SelectedSingleCmdId = this.DicSingleCmd[this.DgvSingleCmdRecord.SelectedRows[0].Cells[0].Value.ToString()].Id;
			}
		}

		private void BtnReSendSingleCmd_Click(object sender, EventArgs e)
		{
			if (this.login_zoehoo.API_ReSendSingleCmd(this.SelectedTerminalId, this.SelectedSingleCmdId))
			{
				MessageBox.Show("指令重发成功");
				return;
			}
			MessageBox.Show("指令重发失败");
		}

		private void BtnSingleCmdResendList_Click(object sender, EventArgs e)
		{
			this.DgvSingleCmdRecord.Rows.Clear();
			this.DicSingleCmd.Clear();
			IList<SingleCmdRecord> list = this.login_zoehoo.API_GetTerminalCmd(this.SelectedTerminalId);
			if (list == null)
			{
				return;
			}
			foreach (SingleCmdRecord current in list)
			{
				this.DicSingleCmd.Add(current.Id, current);
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.DgvSingleCmdRecord);
				dataGridViewRow.Cells[0].Value = current.Id;
				string value = this.SendCmdExplanationDisplay(current.Request);
				dataGridViewRow.Cells[1].Value = value;
				dataGridViewRow.Cells[2].Value = this.SendSingleCmdTranslation(current.Response, current.Request);
				dataGridViewRow.Cells[3].Value = current.UpdatedAt;
				this.DgvSingleCmdRecord.Rows.Add(dataGridViewRow);
			}
		}

		private void ToolStripBtnGroup_Click(object sender, EventArgs e)
		{
			IList<GroupOfCloudServer> groups = new List<GroupOfCloudServer>();
			groups = this.login_zoehoo.API_GetGroupsList();
			new formCloudServerGroupInfo
			{
				Groups = groups
			}.ShowDialog();
		}

		private void ToolStripBtnUserInfo_Click(object sender, EventArgs e)
		{
			new formLoginUser
			{
				LoggedInUser = CloudServerComm.CurrentUser
			}.ShowDialog();
		}

		private void BtnGroupSendProgram_Click(object sender, EventArgs e)
		{
			if (formCloudServerSend.SelectedGroupOfUser != null)
			{
				this.isGroupSend = true;
				this.GenerateDataAndSend();
				return;
			}
			MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_unSelectedGroup"));
		}

		private void BtnExplanation_Click(object sender, EventArgs e)
		{
			if (this.DgvSingleCmdRecord.Rows.Count != 0)
			{
				IList<Unpack_Results> list = new List<Unpack_Results>();
				foreach (DataGridViewRow dataGridViewRow in ((IEnumerable)this.DgvSingleCmdRecord.Rows))
				{
					IList<Unpack_Results> list2 = new List<Unpack_Results>();
					if (dataGridViewRow.Cells[2].Value != null)
					{
						list2 = protocol_single_cmd.Rec_Unpack(Convert.FromBase64String(dataGridViewRow.Cells[2].Value.ToString()), Convert.FromBase64String(dataGridViewRow.Cells[1].Value.ToString()));
					}
					foreach (Unpack_Results current in list2)
					{
						list.Add(current);
					}
				}
				foreach (Unpack_Results current2 in list)
				{
					if (current2.Result_Type == 2)
					{
						Console.WriteLine("数据包--" + this.ReplayCmdExplanationDisplay(current2.Cmd_Type));
					}
					else
					{
						Console.WriteLine("错误包--" + this.ReplayCmdExplanationDisplay(current2.Cmd_Type));
					}
				}
			}
		}

		public string SendSingleCmdTranslation(string replycmd, string sendcmd)
		{
			if (replycmd != null)
			{
				IList<Unpack_Results> list = protocol_single_cmd.Rec_Unpack(Convert.FromBase64String(replycmd), Convert.FromBase64String(sendcmd));
				return this.ReplayCmdExplanationDisplay(list[0].Cmd_Type);
			}
			return null;
		}

		public string ReplayCmdExplanationDisplay(LedCmdType Cmd_Type)
		{
			string str;
			if (Cmd_Type != LedCmdType.Send_Panel_Parameter)
			{
				if (Cmd_Type != LedCmdType.Read_Panel_Parameter)
				{
					switch (Cmd_Type)
					{
					case LedCmdType.Ctrl_Power_On:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Power_On");
						break;
					case LedCmdType.Ctrl_Power_Off:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Power_Off");
						break;
					case LedCmdType.Ctrl_Timing:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Timing");
						break;
					case LedCmdType.Ctrl_Luminance:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Luminance");
						break;
					case LedCmdType.Ctrl_Timer_Switch:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Switch");
						break;
					default:
						str = formMain.ML.GetStr("formCloudServerSend_Messge_CmdOther");
						break;
					}
				}
				else
				{
					str = formMain.ML.GetStr("CloudServer_Ctrl_ReadParameter");
				}
			}
			else
			{
				str = formMain.ML.GetStr("CloudServer_Ctrl_Load");
			}
			return str;
		}

		public string SendCmdExplanationDisplay(string SendData)
		{
			byte[] array = Convert.FromBase64String(SendData);
			LedCmdType ledCmdType = (LedCmdType)Enum.ToObject(typeof(LedCmdType), (int)array[4]);
			LedCmdType ledCmdType2 = ledCmdType;
			string str;
			if (ledCmdType2 != LedCmdType.Send_Panel_Parameter)
			{
				if (ledCmdType2 != LedCmdType.Read_Panel_Parameter)
				{
					switch (ledCmdType2)
					{
					case LedCmdType.Ctrl_Power_On:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Power_On");
						break;
					case LedCmdType.Ctrl_Power_Off:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Power_Off");
						break;
					case LedCmdType.Ctrl_Timing:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Timing");
						break;
					case LedCmdType.Ctrl_Luminance:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Luminance");
						break;
					case LedCmdType.Ctrl_Timer_Switch:
						str = formMain.ML.GetStr("CloudServer_Ctrl_Switch");
						break;
					default:
						str = formMain.ML.GetStr("formCloudServerSend_Messge_CmdOther");
						break;
					}
				}
				else
				{
					str = formMain.ML.GetStr("CloudServer_Ctrl_ReadParameter");
				}
			}
			else
			{
				str = formMain.ML.GetStr("CloudServer_Ctrl_Load");
			}
			return str;
		}

		private void CBOGroupSend_SelectedIndexChanged(object sender, EventArgs e)
		{
			formCloudServerSend.SelectedGroupOfUser = this.DicGroupsOfUser[this.CBOGroupSend.SelectedIndex];
		}

		private void BtnReviewList_Click(object sender, EventArgs e)
		{
			this.DgvReviewList.Rows.Clear();
			this.DicReviewRecord.Clear();
			IList<string> list = this.RowsIsSelected();
			if (list.Count <= 0)
			{
				MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_DeviceNotSelected"));
				return;
			}
			string parameter = this.login_zoehoo.API_GetReviewList();
			Thread.Sleep(500);
			IList<CloudReviewRecord> list2 = this.login_zoehoo.API_GetReviewList(parameter);
			if (list2 == null && list2.Count == 0)
			{
				return;
			}
			foreach (CloudReviewRecord current in list2)
			{
				this.DicReviewRecord.Add(current.Id, current);
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.DgvReviewList);
				dataGridViewRow.Cells[1].Value = current.Id;
				dataGridViewRow.Cells[2].Value = current.TerminalName;
				dataGridViewRow.Cells[3].Value = current.CreateAt;
				dataGridViewRow.Cells[4].Value = current.UpdateAt;
				dataGridViewRow.Cells[5].Value = current.TerminalCode;
				dataGridViewRow.Cells[7].Value = current.TerminalId;
				if (current.Status == "-1")
				{
					dataGridViewRow.Cells[6].Value = formMain.ML.GetStr("formCloudServerSend_RdoReview_Reject");
				}
				else if (current.Status == "0")
				{
					dataGridViewRow.Cells[6].Value = formMain.ML.GetStr("formCloudServerSend_RdoReview_Wait");
				}
				else
				{
					dataGridViewRow.Cells[6].Value = formMain.ML.GetStr("formCloudServerSend_RdoReview_Pass");
				}
				if (list.Contains(current.TerminalId))
				{
					if (this.RdoAll.Checked)
					{
						this.DgvReviewList.Rows.Add(dataGridViewRow);
					}
					if (this.RdoPending.Checked && current.Status == "0")
					{
						this.DgvReviewList.Rows.Add(dataGridViewRow);
					}
					if (this.RdoPass.Checked && current.Status == "1")
					{
						this.DgvReviewList.Rows.Add(dataGridViewRow);
					}
					if (this.RdoReject.Checked && current.Status == "-1")
					{
						this.DgvReviewList.Rows.Add(dataGridViewRow);
					}
				}
			}
			this.DgvReviewList.Sort(this.dataGridViewTextBoxColumn2, ListSortDirection.Descending);
		}

		private void DgvReviewList_SelectionChanged(object sender, EventArgs e)
		{
			if (this.DgvReviewList.SelectedRows.Count > 0)
			{
				this.SelectedReviewDataId = this.DicReviewRecord[this.DgvReviewList.SelectedRows[0].Cells[1].Value.ToString()].Id;
				try
				{
					foreach (DataGridViewRow dataGridViewRow in this.DgvReviewList.SelectedRows)
					{
						if (this.DicReviewRecord[dataGridViewRow.Cells[1].Value.ToString()].Status == "0")
						{
							this.BtnPass.Enabled = true;
							this.BtnReject.Enabled = true;
							return;
						}
					}
					this.BtnPass.Enabled = false;
					this.BtnReject.Enabled = false;
				}
				catch
				{
					Console.WriteLine("..............");
				}
			}
		}

		private void BtnPass_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			foreach (DataGridViewRow dataGridViewRow in this.DgvReviewList.SelectedRows)
			{
				if (this.DicReviewRecord[dataGridViewRow.Cells[1].Value.ToString()].Status == "0" && this.login_zoehoo.API_PutReviewToPass(dataGridViewRow.Cells[1].Value.ToString(), out empty))
				{
					dataGridViewRow.Cells[6].Value = formMain.ML.GetStr("formCloudServerSend_RdoReview_Pass");
				}
			}
		}

		private void BtnReject_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			foreach (DataGridViewRow dataGridViewRow in this.DgvReviewList.SelectedRows)
			{
				if (this.DicReviewRecord[dataGridViewRow.Cells[1].Value.ToString()].Status == "0" && this.login_zoehoo.API_PutReviewToReject(dataGridViewRow.Cells[1].Value.ToString(), out empty))
				{
					dataGridViewRow.Cells[6].Value = formMain.ML.GetStr("formCloudServerSend_RdoReview_Reject");
				}
			}
		}

		private void BtnDownLoadReview_Click(object sender, EventArgs e)
		{
			this.login_zoehoo.API_GetReviewContent(this.SelectedReviewDataId);
		}

		private void ToolStripBtnCardParameter_Click(object sender, EventArgs e)
		{
			IList<string> list = this.RowsIsSelected();
			if (list.Count <= 0)
			{
				MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_DeviceNotSelected"));
				return;
			}
			if (!this.IsCurrentOfModel(list[0]))
			{
				MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_OnlySupportTwoColor"));
				return;
			}
			this.buttonRefreash_Click(null, null);
			Thread.Sleep(500);
			this.IndexOfDgvTerminalDatas(6, list[0]);
			if (!this.DicTerminalId[list[0]].Online)
			{
				MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_DeviceOffLine"));
				return;
			}
			IList<byte[]> list2 = protocol_single_cmd.Send_Pack(0, 0, LedCmdType.Read_Panel_Parameter, null, false, null, "", formMain.ledsys.SelectedPanel.ProtocolVersion);
			string str = HttpUtility.UrlEncode(Convert.ToBase64String(list2[0]));
			string text = "requestBase64=" + str + "&description=";
			new Random(10);
			long ticks = DateTime.Now.Ticks;
			Random random = new Random((int)(ticks & -(long)((ulong)1)) | (int)(ticks >> 32));
			int num = random.Next();
			string dateTimeNow = LedCommon.GetDateTimeNow();
			string text2 = dateTimeNow + num.ToString();
			text += text2;
			new formCloudCardParameter
			{
				terminalId = list[0],
				sendData = text,
				mark = text2
			}.ShowDialog();
			if (formCloudServerSend.IsLoadedPanelParam)
			{
				base.Close();
			}
		}

		public int IndexOfDgvTerminalDatas(int cellIndex, string terminalId)
		{
			foreach (DataGridViewRow dataGridViewRow in ((IEnumerable)this.DgvTerminalDatas.Rows))
			{
				if (dataGridViewRow.Cells[cellIndex].Value.ToString() == terminalId)
				{
					return dataGridViewRow.Index;
				}
			}
			return -1;
		}

		public int IndexOfDgvReviewList(int cellIndex, string terminalId)
		{
			foreach (DataGridViewRow dataGridViewRow in ((IEnumerable)this.DgvReviewList.Rows))
			{
				if (dataGridViewRow.Cells[cellIndex].Value.ToString() == terminalId)
				{
					return dataGridViewRow.Index;
				}
			}
			return -1;
		}

		private void timerSendStatus(object sender)
		{
			try
			{
				foreach (string current in this.RecordSave.ProgramMarkUpdate.Keys)
				{
					if (this.RecordSave.ProgramMarkUpdate[current].IsUpdated && this.login_zoehoo.TerminalIdList.Contains(current))
					{
						TerminalRecord terminalRecord = new TerminalRecord();
						string parameter = this.login_zoehoo.API_GetReviewList();
						Thread.Sleep(500);
						IList<CloudReviewRecord> list = this.login_zoehoo.API_GetReviewList(parameter);
						if (list != null)
						{
							foreach (CloudReviewRecord current2 in list)
							{
								if (current2.Description == this.RecordSave.ProgramMarkUpdate[current].Mark)
								{
									int num = this.IndexOfDgvTerminalDatas(6, current);
									if (num >= 0)
									{
										terminalRecord.TerminalID = current2.TerminalId;
										terminalRecord.TerminalName = this.DicTerminalId[current2.TerminalId].TerminalName;
										terminalRecord.TerminalDesc = this.DicTerminalId[current2.TerminalId].ProductModelDescription;
										terminalRecord.LastProgram = current2.Description;
										terminalRecord.LastSendProgramId = "";
										terminalRecord.LastProgramGetTime = current2.CreateAt;
										if (current2.Status == "0")
										{
											this.DgvTerminalDatas.Rows[num].Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_RdoReview_Wait");
											this.DgvTerminalDatas.Rows[num].Cells[7].Style.ForeColor = System.Drawing.Color.Orange;
											terminalRecord.LastProgramSendStatus = "1";
											this.RecordSave.AddNewTerminalRecord(terminalRecord, true);
										}
										else if (current2.Status == "-1")
										{
											this.DgvTerminalDatas.Rows[num].Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_RdoReview_Reject");
											this.DgvTerminalDatas.Rows[num].Cells[7].Style.ForeColor = System.Drawing.Color.Red;
											terminalRecord.LastProgramSendStatus = "0";
											this.RecordSave.AddNewTerminalRecord(terminalRecord, true);
											this.RecordSave.ProgramMarkUpdate[current].IsUpdated = false;
										}
										else
										{
											this.DgvTerminalDatas.Rows[num].Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_PassedAndWaitRec");
											this.DgvTerminalDatas.Rows[num].Cells[7].Style.ForeColor = System.Drawing.Color.Orange;
											terminalRecord.LastProgramSendStatus = "2";
											this.RecordSave.AddNewTerminalRecord(terminalRecord, true);
										}
									}
								}
							}
						}
						IList<TransData> list2 = this.login_zoehoo.API_GetTransdatas(current);
						if (list2 != null)
						{
							foreach (TransData current3 in list2)
							{
								if (current3.Description == this.RecordSave.ProgramMarkUpdate[current].Mark)
								{
									int num2 = this.IndexOfDgvTerminalDatas(6, current);
									if (num2 >= 0)
									{
										terminalRecord.TerminalID = current;
										terminalRecord.TerminalName = this.DicTerminalId[current].TerminalName;
										terminalRecord.TerminalDesc = this.DicTerminalId[current].ProductModelDescription;
										terminalRecord.LastProgram = current3.Description;
										terminalRecord.LastSendProgramId = current3.ID;
										terminalRecord.LastProgramGetTime = current3.CreatedAt;
										if (current3.Status == TransDataStatus.WaitingForReception)
										{
											this.DgvTerminalDatas.Rows[num2].Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_PassedAndWaitRec");
											this.DgvTerminalDatas.Rows[num2].Cells[7].Style.ForeColor = System.Drawing.Color.Orange;
											terminalRecord.LastProgramSendStatus = "2";
											this.RecordSave.AddNewTerminalRecord(terminalRecord, true);
										}
										else if (current3.Status == TransDataStatus.CompleteReception)
										{
											this.DgvTerminalDatas.Rows[num2].Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_RecComplete");
											this.DgvTerminalDatas.Rows[num2].Cells[7].Style.ForeColor = System.Drawing.Color.LimeGreen;
											terminalRecord.LastProgramSendStatus = "3";
											this.RecordSave.AddNewTerminalRecord(terminalRecord, true);
											this.RecordSave.ProgramMarkUpdate[current].IsUpdated = false;
										}
										else if (current3.Status == TransDataStatus.ExceptionFrameRequest)
										{
											this.DgvTerminalDatas.Rows[num2].Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_DataError");
											this.DgvTerminalDatas.Rows[num2].Cells[7].Style.ForeColor = System.Drawing.Color.Red;
											terminalRecord.LastProgramSendStatus = "-1";
											this.RecordSave.AddNewTerminalRecord(terminalRecord, true);
											this.RecordSave.ProgramMarkUpdate[current].IsUpdated = false;
										}
										else if (current3.Status == TransDataStatus.ExceptionFullStorage)
										{
											this.DgvTerminalDatas.Rows[num2].Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_StoreError");
											this.DgvTerminalDatas.Rows[num2].Cells[7].Style.ForeColor = System.Drawing.Color.Red;
											terminalRecord.LastProgramSendStatus = "-2";
											this.RecordSave.AddNewTerminalRecord(terminalRecord, true);
											this.RecordSave.ProgramMarkUpdate[current].IsUpdated = false;
										}
										else
										{
											this.DgvTerminalDatas.Rows[num2].Cells[7].Value = formMain.ML.GetStr("formCloudServerSend_Messge_FormatError");
											this.DgvTerminalDatas.Rows[num2].Cells[7].Style.ForeColor = System.Drawing.Color.Red;
											terminalRecord.LastProgramSendStatus = "-3";
											this.RecordSave.AddNewTerminalRecord(terminalRecord, true);
											this.RecordSave.ProgramMarkUpdate[current].IsUpdated = false;
										}
									}
								}
							}
						}
					}
				}
				foreach (string current4 in this.RecordSave.CmdMarkUpdate.Keys)
				{
					if (this.RecordSave.CmdMarkUpdate[current4].IsUpdated && this.login_zoehoo.TerminalIdList.Contains(current4))
					{
						TerminalRecord terminalRecord2 = new TerminalRecord();
						IList<SingleCmdRecord> list3 = this.login_zoehoo.API_GetTerminalCmd(current4);
						if (list3 != null)
						{
							foreach (SingleCmdRecord current5 in list3)
							{
								if (current5.Description == this.RecordSave.CmdMarkUpdate[current4].Mark)
								{
									int num3 = this.IndexOfDgvTerminalDatas(6, current4);
									if (num3 >= 0)
									{
										terminalRecord2.TerminalID = current4;
										terminalRecord2.TerminalName = this.DicTerminalId[current4].TerminalName;
										terminalRecord2.TerminalDesc = this.DicTerminalId[current4].ProductModelDescription;
										terminalRecord2.LastSendCmdId = current5.Id;
										terminalRecord2.LastSendCmdMark = this.RecordSave.CmdMarkUpdate[current4].Mark;
										terminalRecord2.LastCmdSendTime = current5.CreatedAt;
										if (current5.Response == null)
										{
											string text = this.SendCmdExplanationDisplay(current5.Request);
											this.DgvTerminalDatas.Rows[num3].Cells[9].Style.ForeColor = System.Drawing.Color.Orange;
											this.DgvTerminalDatas.Rows[num3].Cells[8].Value = text;
											this.DgvTerminalDatas.Rows[num3].Cells[9].Value = formMain.ML.GetStr("formCloudServerSend_Messge_CmdRecFailed");
											terminalRecord2.LastCommand = text;
											terminalRecord2.LastCmdSendStatus = "1";
											this.RecordSave.AddNewTerminalRecord(terminalRecord2, false);
										}
										else
										{
											string text2 = this.SendCmdExplanationDisplay(current5.Request);
											this.DgvTerminalDatas.Rows[num3].Cells[9].Style.ForeColor = System.Drawing.Color.LimeGreen;
											this.DgvTerminalDatas.Rows[num3].Cells[8].Value = text2;
											this.DgvTerminalDatas.Rows[num3].Cells[9].Value = formMain.ML.GetStr("formCloudServerSend_Messge_CmdRecSuccessed");
											terminalRecord2.LastCommand = text2;
											terminalRecord2.LastCmdSendStatus = "2";
											this.RecordSave.AddNewTerminalRecord(terminalRecord2, false);
											this.RecordSave.CmdMarkUpdate[current4].IsUpdated = false;
										}
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		private void ReviewRefresh(bool Ischecked)
		{
			this.RdoAll.Enabled = Ischecked;
			this.RdoPending.Enabled = Ischecked;
			this.RdoPass.Enabled = Ischecked;
			this.RdoReject.Enabled = Ischecked;
			this.BtnReviewList.Enabled = Ischecked;
			if (!Ischecked)
			{
				this.BtnPass.Enabled = Ischecked;
				this.BtnReject.Enabled = Ischecked;
			}
		}

		private void RdoReview_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.ReviewRefresh(false);
				this.BtnReviewList_Click(null, null);
				this.ReviewRefresh(true);
			}
		}

		private void ChkSelectedAll_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				this.ChkSelectedAll.Checked = true;
				this.SelectAll(this.DgvTerminalDatas, true);
				return;
			}
			this.SelectAll(this.DgvTerminalDatas, false);
		}

		private void ChkOpposite_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				this.ChkOpposite.Checked = true;
			}
			this.SelectReverse(this.DgvTerminalDatas);
		}

		private void SelectAll(DataGridView pGrid, bool pResult)
		{
			for (int i = 0; i < pGrid.Rows.Count; i++)
			{
				pGrid.Rows[i].Cells[0].Value = pResult;
			}
		}

		private void SelectReverse(DataGridView pGrid)
		{
			for (int i = 0; i < pGrid.Rows.Count; i++)
			{
				string a = pGrid.Rows[i].Cells[0].EditedFormattedValue.ToString();
				if (a == "True")
				{
					pGrid.Rows[i].Cells[0].Value = false;
				}
				else
				{
					pGrid.Rows[i].Cells[0].Value = true;
				}
			}
		}

		private IList<string> RowsIsSelected()
		{
			IList<string> list = new List<string>();
			for (int i = 0; i < this.DgvTerminalDatas.Rows.Count; i++)
			{
				string a = this.DgvTerminalDatas.Rows[i].Cells["ColumnChkIsSelected"].EditedFormattedValue.ToString();
				if (a == "True")
				{
					list.Add(this.DgvTerminalDatas.Rows[i].Cells["ColumnTerminalId"].Value.ToString());
				}
			}
			return list;
		}

		private void DgvReviewList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, this.DgvReviewList.RowHeadersWidth, e.RowBounds.Height);
			TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), this.DgvReviewList.RowHeadersDefaultCellStyle.Font, bounds, this.DgvReviewList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
		}

		private void formCloudServerSend_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Alt && e.KeyCode == Keys.D)
			{
				if (base.Width < 900)
				{
					base.Width = 1350;
				}
				else
				{
					base.Width = 820;
				}
				this.pictureBoxUpAndDown.Location = new System.Drawing.Point(base.Width / 2, this.pictureBoxUpAndDown.Location.Y);
			}
		}

		private void DgvTerminalDatasMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && e.ColumnIndex > -1 && e.RowIndex > -1 && this.DgvTerminalDatas.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
			{
				this.CtmsSelectFunction.Show(Control.MousePosition.X, Control.MousePosition.Y);
				formCloudServerSend.MousePX = Control.MousePosition.X;
				formCloudServerSend.MousePY = Control.MousePosition.Y;
			}
		}

		private void TsmiSelect_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow dataGridViewRow in this.DgvTerminalDatas.SelectedRows)
			{
				dataGridViewRow.Cells[0].Value = true;
			}
		}

		private void TsmiCancel_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow dataGridViewRow in this.DgvTerminalDatas.SelectedRows)
			{
				dataGridViewRow.Cells[0].Value = false;
			}
		}

		private void DownOrUp_MouseUp(object sender, MouseEventArgs e)
		{
			if (base.Height < 600)
			{
				base.Height = 885;
				this.pictureBoxUpAndDown.Image = this.imageListTelescopic.Images[1];
				this.pictureBoxUpAndDown.Location = new System.Drawing.Point(base.Width / 2, 811);
				this.PnlReview.Location = new System.Drawing.Point(0, 462);
				this.PnlReview.Visible = true;
				return;
			}
			base.Height = 515;
			this.pictureBoxUpAndDown.Image = this.imageListTelescopic.Images[0];
			this.pictureBoxUpAndDown.Location = new System.Drawing.Point(base.Width / 2, 448);
			this.PnlReview.Location = new System.Drawing.Point(0, 492);
			this.PnlReview.Visible = false;
		}

		private void LeftOrRight_MouseUp(object sender, MouseEventArgs e)
		{
			if (base.Width < 900)
			{
				this.pictureBoxLeftAndRight.Image = this.imageListLeftAndRight.Images[1];
				this.PnlReview.Location = new System.Drawing.Point(this.DgvTerminalDatas.Location.X + this.DgvTerminalDatas.Width + 15, this.PnlReview.Location.Y);
				this.pictureBoxLeftAndRight.Location = new System.Drawing.Point(this.PnlReview.Location.X + this.PnlReview.Width, this.DgvTerminalDatas.Location.Y + this.DgvTerminalDatas.Height / 2 - this.pictureBoxLeftAndRight.Height / 2);
				this.PnlReview.Visible = true;
				base.Width = this.pictureBoxLeftAndRight.Location.X + this.pictureBoxLeftAndRight.Width + this.sideWidth + 10;
				return;
			}
			this.pictureBoxLeftAndRight.Image = this.imageListLeftAndRight.Images[0];
			this.pictureBoxLeftAndRight.Location = new System.Drawing.Point(this.DgvTerminalDatas.Location.X + this.DgvTerminalDatas.Width + 15, this.DgvTerminalDatas.Location.Y + this.DgvTerminalDatas.Height / 2 - this.pictureBoxLeftAndRight.Height / 2);
			this.PnlReview.Location = new System.Drawing.Point(this.pictureBoxLeftAndRight.Location.X + 35, this.PnlReview.Location.Y);
			this.PnlReview.Visible = false;
			base.Width = this.pictureBoxLeftAndRight.Location.X + this.pictureBoxLeftAndRight.Width + this.sideWidth + 10;
		}

		private string GetTerminalInfo(TerminalData device)
		{
			string str = string.Empty;
			str = str + formMain.ML.GetStr("formCloudServerSend_DgvTerminalDatas_toolTip_TerminalCode") + device.TerminalCode + "        \r\n";
			str = str + formMain.ML.GetStr("formCloudServerSend_DgvTerminalDatas_toolTip_TotalCapacityStr") + device.TotalCapacityStr + "        \r\n";
			str = str + formMain.ML.GetStr("formCloudServerSend_DgvTerminalDatas_toolTip_FreeCapacityStr") + device.FreeCapacityStr + "        \r\n";
			return str + formMain.ML.GetStr("formCloudServerSend_DgvTerminalDatas_toolTip_UsedCapacityStr") + device.UsedCapacityStr + "        \r\n";
		}

		private void BtnProgramSendList_Click(object sender, EventArgs e)
		{
			IList<string> list = this.RowsIsSelected();
			if (list.Count <= 0)
			{
				MessageBox.Show(formMain.ML.GetStr("formCloudServerSend_Messge_DeviceNotSelected"));
				return;
			}
			IList<TransData> list2 = this.login_zoehoo.API_GetTransdatas(list[0]);
			if (list2 == null)
			{
				return;
			}
			this.DgvTransdataIdList.Rows.Clear();
			this.DicTransData.Clear();
			foreach (TransData current in list2)
			{
				this.DicTransData.Add(current.ID, current);
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.DgvTransdataIdList);
				dataGridViewRow.Cells[0].Value = this.DicTerminalId[this.SelectedTerminalId].TerminalName;
				dataGridViewRow.Cells[1].Value = current.CreatedAt;
				dataGridViewRow.Cells[2].Value = current.ID;
				dataGridViewRow.Cells[3].Value = current.Description;
				if (current.Status == TransDataStatus.WaitingForReception)
				{
					dataGridViewRow.Cells[4].Value = "等待";
				}
				else if (current.Status == TransDataStatus.CompleteReception)
				{
					dataGridViewRow.Cells[4].Value = "完成";
				}
				else
				{
					dataGridViewRow.Cells[4].Value = "异常";
				}
				dataGridViewRow.Cells[5].Value = current.FrameLength;
				if (this.RdoSendProgramAll.Checked)
				{
					this.DgvTransdataIdList.Rows.Add(dataGridViewRow);
				}
				if (this.RdoSendProgramWait.Checked && current.Status == TransDataStatus.WaitingForReception)
				{
					this.DgvTransdataIdList.Rows.Add(dataGridViewRow);
				}
				if (this.RdoSendProgramComplete.Checked && current.Status == TransDataStatus.CompleteReception)
				{
					this.DgvTransdataIdList.Rows.Add(dataGridViewRow);
				}
				if (this.RdoSendProgramAbnormal.Checked && (current.Status == TransDataStatus.ExceptionFrameRequest || current.Status == TransDataStatus.ExceptionFullStorage || current.Status == TransDataStatus.ExceptionWrongFormat))
				{
					this.DgvTransdataIdList.Rows.Add(dataGridViewRow);
				}
			}
		}

		private void RdoSendProgramAll_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.BtnProgramSendList_Click(null, null);
			}
		}

		private void RdoSendProgramWait_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.BtnProgramSendList_Click(null, null);
			}
		}

		private void RdoSendProgramComplete_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.BtnProgramSendList_Click(null, null);
			}
		}

		private void RdoSendProgramAbnormal_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.BtnProgramSendList_Click(null, null);
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
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formCloudServerSend));
			this.DgvTerminalDatas = new DataGridView();
			this.ColumnChkIsSelected = new DataGridViewCheckBoxColumn();
			this.TerminalName = new DataGridViewTextBoxColumn();
			this.Stuats = new DataGridViewTextBoxColumn();
			this.Model = new DataGridViewTextBoxColumn();
			this.ColumnWidth = new DataGridViewTextBoxColumn();
			this.ColumnHeight = new DataGridViewTextBoxColumn();
			this.ColumnTerminalId = new DataGridViewTextBoxColumn();
			this.ColumnSendStatus = new DataGridViewTextBoxColumn();
			this.ColumnSendCmd = new DataGridViewTextBoxColumn();
			this.ColumnCmdStatus = new DataGridViewTextBoxColumn();
			this.No = new DataGridViewTextBoxColumn();
			this.buttonRefreash = new Button();
			this.BtnSendProgram = new Button();
			this.BtnDeleteProgramList = new Button();
			this.DgvTransdataIdList = new DataGridView();
			this.SourceName = new DataGridViewTextBoxColumn();
			this.ForwardingTime = new DataGridViewTextBoxColumn();
			this.ID = new DataGridViewTextBoxColumn();
			this.Description = new DataGridViewTextBoxColumn();
			this.DataStatus = new DataGridViewTextBoxColumn();
			this.FrameLength = new DataGridViewTextBoxColumn();
			this.CBOSingleCmd = new ComboBox();
			this.BtnSendSingleCmd = new Button();
			this.LblCmd = new Label();
			this.LblDeviceList = new Label();
			this.ToolStripOperating = new ToolStrip();
			this.ToolStripBtnUserInfo = new ToolStripButton();
			this.ToolStripBtnGroup = new ToolStripButton();
			this.ToolStripBtnCardParameter = new ToolStripButton();
			this.panel_Waitting = new Panel();
			this.label_Waitting_Picture = new Label();
			this.pictureBox2 = new PictureBox();
			this.BtnReSendSingleCmd = new Button();
			this.DgvSingleCmdRecord = new DataGridView();
			this.SingleCmdRecordID = new DataGridViewTextBoxColumn();
			this.SingleCmdRecordRequest = new DataGridViewTextBoxColumn();
			this.SingleCmdRecordResponse = new DataGridViewTextBoxColumn();
			this.SingleCmdRecordUpdate = new DataGridViewTextBoxColumn();
			this.BtnSingleCmdResendList = new Button();
			this.BtnGroupSendProgram = new Button();
			this.BtnExplanation = new Button();
			this.CBOGroupSend = new ComboBox();
			this.LblGroups = new Label();
			this.DgvReviewList = new DataGridView();
			this.dataGridViewTextBoxColumnIndex = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumnUpdate = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.ColumnReviewTerminalId = new DataGridViewTextBoxColumn();
			this.BtnPass = new Button();
			this.BtnReject = new Button();
			this.BtnReviewList = new Button();
			this.BtnDownLoadReview = new Button();
			this.RdoAll = new RadioButton();
			this.RdoPending = new RadioButton();
			this.RdoReject = new RadioButton();
			this.RdoPass = new RadioButton();
			this.ChkSelectedAll = new CheckBox();
			this.ChkOpposite = new CheckBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.panel1 = new Panel();
			this.panel2 = new Panel();
			this.panel3 = new Panel();
			this.PnlReview = new Panel();
			this.panel4 = new Panel();
			this.RdoSendProgramAll = new RadioButton();
			this.RdoSendProgramComplete = new RadioButton();
			this.RdoSendProgramAbnormal = new RadioButton();
			this.RdoSendProgramWait = new RadioButton();
			this.BtnProgramSendList = new Button();
			this.label3 = new Label();
			this.imageListTelescopic = new ImageList(this.components);
			this.CtmsSelectFunction = new ContextMenuStrip(this.components);
			this.TsmiSelect = new ToolStripMenuItem();
			this.TsmiCancel = new ToolStripMenuItem();
			this.CtmsDisplay = new ContextMenuStrip(this.components);
			this.TstbDisplayID = new ToolStripTextBox();
			this.pictureBoxUpAndDown = new PictureBox();
			this.imageListLeftAndRight = new ImageList(this.components);
			this.pictureBoxLeftAndRight = new PictureBox();
			((ISupportInitialize)this.DgvTerminalDatas).BeginInit();
			((ISupportInitialize)this.DgvTransdataIdList).BeginInit();
			this.ToolStripOperating.SuspendLayout();
			this.panel_Waitting.SuspendLayout();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			((ISupportInitialize)this.DgvSingleCmdRecord).BeginInit();
			((ISupportInitialize)this.DgvReviewList).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.PnlReview.SuspendLayout();
			this.panel4.SuspendLayout();
			this.CtmsSelectFunction.SuspendLayout();
			this.CtmsDisplay.SuspendLayout();
			((ISupportInitialize)this.pictureBoxUpAndDown).BeginInit();
			((ISupportInitialize)this.pictureBoxLeftAndRight).BeginInit();
			base.SuspendLayout();
			this.DgvTerminalDatas.AllowDrop = true;
			this.DgvTerminalDatas.AllowUserToAddRows = false;
			this.DgvTerminalDatas.AllowUserToResizeRows = false;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.DgvTerminalDatas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.DgvTerminalDatas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvTerminalDatas.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ColumnChkIsSelected,
				this.TerminalName,
				this.Stuats,
				this.Model,
				this.ColumnWidth,
				this.ColumnHeight,
				this.ColumnTerminalId,
				this.ColumnSendStatus,
				this.ColumnSendCmd,
				this.ColumnCmdStatus,
				this.No
			});
			this.DgvTerminalDatas.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.DgvTerminalDatas.Location = new System.Drawing.Point(25, 87);
			this.DgvTerminalDatas.Name = "DgvTerminalDatas";
			this.DgvTerminalDatas.RowHeadersVisible = false;
			this.DgvTerminalDatas.RowTemplate.Height = 23;
			this.DgvTerminalDatas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.DgvTerminalDatas.Size = new System.Drawing.Size(751, 471);
			this.DgvTerminalDatas.TabIndex = 8;
			this.DgvTerminalDatas.CellClick += new DataGridViewCellEventHandler(this.DgvTerminalDatas_CellClick);
			this.DgvTerminalDatas.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DgvTerminalDatas_CellFormatting);
			this.DgvTerminalDatas.CellMouseClick += new DataGridViewCellMouseEventHandler(this.DgvTerminalDatasMouseClick);
			this.DgvTerminalDatas.SelectionChanged += new EventHandler(this.GroupsOfScanRouting_SelectionChanged);
			this.ColumnChkIsSelected.HeaderText = "选择";
			this.ColumnChkIsSelected.Name = "ColumnChkIsSelected";
			this.ColumnChkIsSelected.Width = 50;
			this.TerminalName.HeaderText = "终端名称";
			this.TerminalName.Name = "TerminalName";
			this.TerminalName.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.TerminalName.Width = 120;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.Stuats.DefaultCellStyle = dataGridViewCellStyle2;
			this.Stuats.HeaderText = "状态";
			this.Stuats.Name = "Stuats";
			this.Stuats.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Stuats.Width = 80;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.Model.DefaultCellStyle = dataGridViewCellStyle3;
			this.Model.HeaderText = "型号";
			this.Model.Name = "Model";
			this.Model.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Model.Width = 80;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.ColumnWidth.DefaultCellStyle = dataGridViewCellStyle4;
			this.ColumnWidth.HeaderText = "屏宽";
			this.ColumnWidth.Name = "ColumnWidth";
			this.ColumnWidth.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.ColumnWidth.Width = 60;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.ColumnHeight.DefaultCellStyle = dataGridViewCellStyle5;
			this.ColumnHeight.HeaderText = "屏高";
			this.ColumnHeight.Name = "ColumnHeight";
			this.ColumnHeight.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.ColumnHeight.Width = 60;
			this.ColumnTerminalId.HeaderText = "终端ID";
			this.ColumnTerminalId.Name = "ColumnTerminalId";
			this.ColumnTerminalId.Visible = false;
			this.ColumnSendStatus.HeaderText = "节目发送状态";
			this.ColumnSendStatus.Name = "ColumnSendStatus";
			this.ColumnSendStatus.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.ColumnSendStatus.Width = 120;
			this.ColumnSendCmd.HeaderText = "发送命令";
			this.ColumnSendCmd.Name = "ColumnSendCmd";
			this.ColumnSendCmd.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.ColumnSendCmd.Width = 80;
			this.ColumnCmdStatus.HeaderText = "命令状态";
			this.ColumnCmdStatus.Name = "ColumnCmdStatus";
			this.ColumnCmdStatus.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.ColumnCmdStatus.Width = 101;
			this.No.HeaderText = "编号";
			this.No.Name = "No";
			this.No.Width = 120;
			this.buttonRefreash.Image = Resources.refresh_L;
			this.buttonRefreash.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRefreash.Location = new System.Drawing.Point(137, 59);
			this.buttonRefreash.Name = "buttonRefreash";
			this.buttonRefreash.Size = new System.Drawing.Size(72, 24);
			this.buttonRefreash.TabIndex = 10;
			this.buttonRefreash.Text = "刷新";
			this.buttonRefreash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonRefreash.UseVisualStyleBackColor = true;
			this.buttonRefreash.Click += new EventHandler(this.buttonRefreash_Click);
			this.BtnSendProgram.Image = Resources.send_L;
			this.BtnSendProgram.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnSendProgram.Location = new System.Drawing.Point(70, 2);
			this.BtnSendProgram.Name = "BtnSendProgram";
			this.BtnSendProgram.Size = new System.Drawing.Size(91, 24);
			this.BtnSendProgram.TabIndex = 11;
			this.BtnSendProgram.Text = "节目发送";
			this.BtnSendProgram.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnSendProgram.UseVisualStyleBackColor = true;
			this.BtnSendProgram.Click += new EventHandler(this.button1_Click);
			this.BtnDeleteProgramList.Image = Resources.Delete;
			this.BtnDeleteProgramList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnDeleteProgramList.Location = new System.Drawing.Point(294, 2);
			this.BtnDeleteProgramList.Name = "BtnDeleteProgramList";
			this.BtnDeleteProgramList.Size = new System.Drawing.Size(72, 24);
			this.BtnDeleteProgramList.TabIndex = 15;
			this.BtnDeleteProgramList.Text = "删除";
			this.BtnDeleteProgramList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnDeleteProgramList.UseVisualStyleBackColor = true;
			this.BtnDeleteProgramList.Click += new EventHandler(this.BtnDeleteProgramList_Click);
			this.DgvTransdataIdList.AllowUserToAddRows = false;
			this.DgvTransdataIdList.AllowUserToResizeRows = false;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.DgvTransdataIdList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.DgvTransdataIdList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvTransdataIdList.Columns.AddRange(new DataGridViewColumn[]
			{
				this.SourceName,
				this.ForwardingTime,
				this.ID,
				this.Description,
				this.DataStatus,
				this.FrameLength
			});
			this.DgvTransdataIdList.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.DgvTransdataIdList.Location = new System.Drawing.Point(14, 33);
			this.DgvTransdataIdList.Name = "DgvTransdataIdList";
			this.DgvTransdataIdList.RowHeadersVisible = false;
			this.DgvTransdataIdList.RowTemplate.Height = 23;
			this.DgvTransdataIdList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.DgvTransdataIdList.Size = new System.Drawing.Size(405, 195);
			this.DgvTransdataIdList.TabIndex = 8;
			this.DgvTransdataIdList.SelectionChanged += new EventHandler(this.DgvTransdataIdList_SelectionChanged);
			this.SourceName.HeaderText = "终端名称";
			this.SourceName.Name = "SourceName";
			this.SourceName.Width = 150;
			this.ForwardingTime.HeaderText = "发送时间";
			this.ForwardingTime.Name = "ForwardingTime";
			this.ForwardingTime.Width = 150;
			this.ID.HeaderText = "ID";
			this.ID.Name = "ID";
			this.ID.Visible = false;
			this.ID.Width = 280;
			this.Description.HeaderText = "描述";
			this.Description.Name = "Description";
			this.Description.Visible = false;
			this.Description.Width = 70;
			this.DataStatus.HeaderText = "状态";
			this.DataStatus.Name = "DataStatus";
			this.DataStatus.Width = 101;
			this.FrameLength.HeaderText = "帧长度";
			this.FrameLength.Name = "FrameLength";
			this.FrameLength.Width = 81;
			this.CBOSingleCmd.Font = new System.Drawing.Font("宋体", 9f);
			this.CBOSingleCmd.FormattingEnabled = true;
			this.CBOSingleCmd.Location = new System.Drawing.Point(57, 4);
			this.CBOSingleCmd.Name = "CBOSingleCmd";
			this.CBOSingleCmd.Size = new System.Drawing.Size(85, 20);
			this.CBOSingleCmd.TabIndex = 20;
			this.BtnSendSingleCmd.Image = Resources.send_cmd;
			this.BtnSendSingleCmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnSendSingleCmd.Location = new System.Drawing.Point(150, 2);
			this.BtnSendSingleCmd.Name = "BtnSendSingleCmd";
			this.BtnSendSingleCmd.Size = new System.Drawing.Size(91, 24);
			this.BtnSendSingleCmd.TabIndex = 21;
			this.BtnSendSingleCmd.Text = "指令发送";
			this.BtnSendSingleCmd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnSendSingleCmd.UseVisualStyleBackColor = true;
			this.BtnSendSingleCmd.Click += new EventHandler(this.BtnSendSingleCmd_Click);
			this.LblCmd.AutoSize = true;
			this.LblCmd.Font = new System.Drawing.Font("宋体", 9f);
			this.LblCmd.Location = new System.Drawing.Point(9, 7);
			this.LblCmd.Name = "LblCmd";
			this.LblCmd.Size = new System.Drawing.Size(29, 12);
			this.LblCmd.TabIndex = 22;
			this.LblCmd.Text = "指令";
			this.LblDeviceList.AutoSize = true;
			this.LblDeviceList.Font = new System.Drawing.Font("宋体", 10.5f);
			this.LblDeviceList.Location = new System.Drawing.Point(22, 63);
			this.LblDeviceList.Name = "LblDeviceList";
			this.LblDeviceList.Size = new System.Drawing.Size(63, 14);
			this.LblDeviceList.TabIndex = 23;
			this.LblDeviceList.Text = "设备列表";
			this.ToolStripOperating.AutoSize = false;
			this.ToolStripOperating.Items.AddRange(new ToolStripItem[]
			{
				this.ToolStripBtnUserInfo,
				this.ToolStripBtnGroup,
				this.ToolStripBtnCardParameter
			});
			this.ToolStripOperating.Location = new System.Drawing.Point(0, 0);
			this.ToolStripOperating.Name = "ToolStripOperating";
			this.ToolStripOperating.Size = new System.Drawing.Size(814, 51);
			this.ToolStripOperating.TabIndex = 24;
			this.ToolStripOperating.Text = "toolStrip1";
			this.ToolStripBtnUserInfo.Image = Resources.Cloud_User;
			this.ToolStripBtnUserInfo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ToolStripBtnUserInfo.ImageScaling = ToolStripItemImageScaling.None;
			this.ToolStripBtnUserInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripBtnUserInfo.Name = "ToolStripBtnUserInfo";
			this.ToolStripBtnUserInfo.Size = new System.Drawing.Size(59, 48);
			this.ToolStripBtnUserInfo.Text = "用户信息";
			this.ToolStripBtnUserInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ToolStripBtnUserInfo.TextImageRelation = TextImageRelation.ImageAboveText;
			this.ToolStripBtnUserInfo.Click += new EventHandler(this.ToolStripBtnUserInfo_Click);
			this.ToolStripBtnGroup.Image = Resources.Cloud_Users;
			this.ToolStripBtnGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ToolStripBtnGroup.ImageScaling = ToolStripItemImageScaling.None;
			this.ToolStripBtnGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripBtnGroup.Name = "ToolStripBtnGroup";
			this.ToolStripBtnGroup.Size = new System.Drawing.Size(59, 48);
			this.ToolStripBtnGroup.Text = "群组信息";
			this.ToolStripBtnGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ToolStripBtnGroup.TextImageRelation = TextImageRelation.ImageAboveText;
			this.ToolStripBtnGroup.Click += new EventHandler(this.ToolStripBtnGroup_Click);
			this.ToolStripBtnCardParameter.Image = Resources.Cloud_infomation;
			this.ToolStripBtnCardParameter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ToolStripBtnCardParameter.ImageScaling = ToolStripItemImageScaling.None;
			this.ToolStripBtnCardParameter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripBtnCardParameter.Name = "ToolStripBtnCardParameter";
			this.ToolStripBtnCardParameter.Size = new System.Drawing.Size(59, 48);
			this.ToolStripBtnCardParameter.Text = "设备参数";
			this.ToolStripBtnCardParameter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ToolStripBtnCardParameter.TextImageRelation = TextImageRelation.ImageAboveText;
			this.ToolStripBtnCardParameter.Click += new EventHandler(this.ToolStripBtnCardParameter_Click);
			this.panel_Waitting.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel_Waitting.Controls.Add(this.label_Waitting_Picture);
			this.panel_Waitting.Controls.Add(this.pictureBox2);
			this.panel_Waitting.Location = new System.Drawing.Point(255, 248);
			this.panel_Waitting.Name = "panel_Waitting";
			this.panel_Waitting.Size = new System.Drawing.Size(368, 64);
			this.panel_Waitting.TabIndex = 32;
			this.label_Waitting_Picture.ForeColor = System.Drawing.Color.White;
			this.label_Waitting_Picture.Location = new System.Drawing.Point(71, 3);
			this.label_Waitting_Picture.Name = "label_Waitting_Picture";
			this.label_Waitting_Picture.Size = new System.Drawing.Size(294, 58);
			this.label_Waitting_Picture.TabIndex = 1;
			this.label_Waitting_Picture.Text = "正在生成数据.....";
			this.label_Waitting_Picture.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.pictureBox2.Image = Resources.loading;
			this.pictureBox2.Location = new System.Drawing.Point(3, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(63, 58);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			this.BtnReSendSingleCmd.Image = Resources.send_R;
			this.BtnReSendSingleCmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnReSendSingleCmd.Location = new System.Drawing.Point(939, 848);
			this.BtnReSendSingleCmd.Name = "BtnReSendSingleCmd";
			this.BtnReSendSingleCmd.Size = new System.Drawing.Size(55, 24);
			this.BtnReSendSingleCmd.TabIndex = 33;
			this.BtnReSendSingleCmd.Text = "重发";
			this.BtnReSendSingleCmd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnReSendSingleCmd.UseVisualStyleBackColor = true;
			this.BtnReSendSingleCmd.Visible = false;
			this.BtnReSendSingleCmd.Click += new EventHandler(this.BtnReSendSingleCmd_Click);
			this.DgvSingleCmdRecord.AllowUserToAddRows = false;
			this.DgvSingleCmdRecord.AllowUserToResizeRows = false;
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
			this.DgvSingleCmdRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.DgvSingleCmdRecord.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvSingleCmdRecord.Columns.AddRange(new DataGridViewColumn[]
			{
				this.SingleCmdRecordID,
				this.SingleCmdRecordRequest,
				this.SingleCmdRecordResponse,
				this.SingleCmdRecordUpdate
			});
			this.DgvSingleCmdRecord.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.DgvSingleCmdRecord.Location = new System.Drawing.Point(835, 749);
			this.DgvSingleCmdRecord.Name = "DgvSingleCmdRecord";
			this.DgvSingleCmdRecord.RowHeadersVisible = false;
			this.DgvSingleCmdRecord.RowTemplate.Height = 23;
			this.DgvSingleCmdRecord.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.DgvSingleCmdRecord.Size = new System.Drawing.Size(314, 76);
			this.DgvSingleCmdRecord.TabIndex = 34;
			this.DgvSingleCmdRecord.Visible = false;
			this.DgvSingleCmdRecord.SelectionChanged += new EventHandler(this.DgvSingleCmdRecord_SelectionChanged);
			this.SingleCmdRecordID.HeaderText = "ID";
			this.SingleCmdRecordID.Name = "SingleCmdRecordID";
			this.SingleCmdRecordID.Width = 250;
			this.SingleCmdRecordRequest.HeaderText = "单命令请求";
			this.SingleCmdRecordRequest.Name = "SingleCmdRecordRequest";
			this.SingleCmdRecordRequest.Width = 150;
			this.SingleCmdRecordResponse.HeaderText = "单命令回复";
			this.SingleCmdRecordResponse.Name = "SingleCmdRecordResponse";
			this.SingleCmdRecordResponse.Width = 150;
			this.SingleCmdRecordUpdate.HeaderText = "更新时间";
			this.SingleCmdRecordUpdate.Name = "SingleCmdRecordUpdate";
			this.SingleCmdRecordUpdate.Width = 170;
			this.BtnSingleCmdResendList.Image = Resources.list;
			this.BtnSingleCmdResendList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnSingleCmdResendList.Location = new System.Drawing.Point(833, 848);
			this.BtnSingleCmdResendList.Name = "BtnSingleCmdResendList";
			this.BtnSingleCmdResendList.Size = new System.Drawing.Size(91, 24);
			this.BtnSingleCmdResendList.TabIndex = 35;
			this.BtnSingleCmdResendList.Text = "单命令列表";
			this.BtnSingleCmdResendList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnSingleCmdResendList.UseVisualStyleBackColor = true;
			this.BtnSingleCmdResendList.Visible = false;
			this.BtnSingleCmdResendList.Click += new EventHandler(this.BtnSingleCmdResendList_Click);
			this.BtnGroupSendProgram.Image = Resources.send_R;
			this.BtnGroupSendProgram.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnGroupSendProgram.Location = new System.Drawing.Point(149, 2);
			this.BtnGroupSendProgram.Name = "BtnGroupSendProgram";
			this.BtnGroupSendProgram.Size = new System.Drawing.Size(91, 24);
			this.BtnGroupSendProgram.TabIndex = 37;
			this.BtnGroupSendProgram.Text = "群组发送";
			this.BtnGroupSendProgram.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnGroupSendProgram.UseVisualStyleBackColor = true;
			this.BtnGroupSendProgram.Click += new EventHandler(this.BtnGroupSendProgram_Click);
			this.BtnExplanation.Image = Resources.convert;
			this.BtnExplanation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnExplanation.Location = new System.Drawing.Point(1008, 848);
			this.BtnExplanation.Name = "BtnExplanation";
			this.BtnExplanation.Size = new System.Drawing.Size(55, 24);
			this.BtnExplanation.TabIndex = 38;
			this.BtnExplanation.Text = "解析";
			this.BtnExplanation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnExplanation.UseVisualStyleBackColor = true;
			this.BtnExplanation.Visible = false;
			this.BtnExplanation.Click += new EventHandler(this.BtnExplanation_Click);
			this.CBOGroupSend.Font = new System.Drawing.Font("宋体", 9f);
			this.CBOGroupSend.FormattingEnabled = true;
			this.CBOGroupSend.Location = new System.Drawing.Point(57, 4);
			this.CBOGroupSend.Name = "CBOGroupSend";
			this.CBOGroupSend.Size = new System.Drawing.Size(85, 20);
			this.CBOGroupSend.TabIndex = 39;
			this.CBOGroupSend.SelectedIndexChanged += new EventHandler(this.CBOGroupSend_SelectedIndexChanged);
			this.LblGroups.AutoSize = true;
			this.LblGroups.Font = new System.Drawing.Font("宋体", 9f);
			this.LblGroups.Location = new System.Drawing.Point(9, 7);
			this.LblGroups.Name = "LblGroups";
			this.LblGroups.Size = new System.Drawing.Size(29, 12);
			this.LblGroups.TabIndex = 40;
			this.LblGroups.Text = "群组";
			this.DgvReviewList.AllowUserToAddRows = false;
			this.DgvReviewList.AllowUserToResizeRows = false;
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
			this.DgvReviewList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
			this.DgvReviewList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvReviewList.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumnIndex,
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumnUpdate,
				this.dataGridViewTextBoxColumn4,
				this.dataGridViewTextBoxColumn5,
				this.ColumnReviewTerminalId
			});
			this.DgvReviewList.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.DgvReviewList.Location = new System.Drawing.Point(14, 309);
			this.DgvReviewList.Name = "DgvReviewList";
			this.DgvReviewList.RowHeadersVisible = false;
			this.DgvReviewList.RowTemplate.Height = 23;
			this.DgvReviewList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.DgvReviewList.Size = new System.Drawing.Size(405, 195);
			this.DgvReviewList.TabIndex = 43;
			this.DgvReviewList.SelectionChanged += new EventHandler(this.DgvReviewList_SelectionChanged);
			this.dataGridViewTextBoxColumnIndex.HeaderText = "编号";
			this.dataGridViewTextBoxColumnIndex.Name = "dataGridViewTextBoxColumnIndex";
			this.dataGridViewTextBoxColumnIndex.ReadOnly = true;
			this.dataGridViewTextBoxColumnIndex.Visible = false;
			this.dataGridViewTextBoxColumnIndex.Width = 70;
			this.dataGridViewTextBoxColumn1.HeaderText = "审核ID";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Visible = false;
			this.dataGridViewTextBoxColumn1.Width = 220;
			this.dataGridViewTextBoxColumn3.HeaderText = "终端名";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = 150;
			this.dataGridViewTextBoxColumn2.HeaderText = "申请创建时间";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 150;
			this.dataGridViewTextBoxColumnUpdate.HeaderText = "申请答复时间";
			this.dataGridViewTextBoxColumnUpdate.Name = "dataGridViewTextBoxColumnUpdate";
			this.dataGridViewTextBoxColumnUpdate.ReadOnly = true;
			this.dataGridViewTextBoxColumnUpdate.Visible = false;
			this.dataGridViewTextBoxColumnUpdate.Width = 190;
			this.dataGridViewTextBoxColumn4.HeaderText = "终端编号";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Visible = false;
			this.dataGridViewTextBoxColumn4.Width = 160;
			this.dataGridViewTextBoxColumn5.HeaderText = "审核状态";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Width = 101;
			this.ColumnReviewTerminalId.HeaderText = "终端Id";
			this.ColumnReviewTerminalId.Name = "ColumnReviewTerminalId";
			this.ColumnReviewTerminalId.Visible = false;
			this.BtnPass.Image = Resources.pass_button;
			this.BtnPass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnPass.Location = new System.Drawing.Point(14, 538);
			this.BtnPass.Name = "BtnPass";
			this.BtnPass.Size = new System.Drawing.Size(83, 24);
			this.BtnPass.TabIndex = 44;
			this.BtnPass.Text = "申请通过";
			this.BtnPass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnPass.UseVisualStyleBackColor = true;
			this.BtnPass.Click += new EventHandler(this.BtnPass_Click);
			this.BtnReject.Image = Resources.refuse_button;
			this.BtnReject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnReject.Location = new System.Drawing.Point(121, 538);
			this.BtnReject.Name = "BtnReject";
			this.BtnReject.Size = new System.Drawing.Size(83, 24);
			this.BtnReject.TabIndex = 45;
			this.BtnReject.Text = "申请驳回";
			this.BtnReject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnReject.UseVisualStyleBackColor = true;
			this.BtnReject.Click += new EventHandler(this.BtnReject_Click);
			this.BtnReviewList.Image = Resources.refresh_L;
			this.BtnReviewList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnReviewList.Location = new System.Drawing.Point(189, 279);
			this.BtnReviewList.Name = "BtnReviewList";
			this.BtnReviewList.Size = new System.Drawing.Size(72, 24);
			this.BtnReviewList.TabIndex = 46;
			this.BtnReviewList.Text = "刷新";
			this.BtnReviewList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnReviewList.UseVisualStyleBackColor = true;
			this.BtnReviewList.Click += new EventHandler(this.BtnReviewList_Click);
			this.BtnDownLoadReview.Image = Resources.cloud_download;
			this.BtnDownLoadReview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnDownLoadReview.Location = new System.Drawing.Point(1069, 848);
			this.BtnDownLoadReview.Name = "BtnDownLoadReview";
			this.BtnDownLoadReview.Size = new System.Drawing.Size(83, 24);
			this.BtnDownLoadReview.TabIndex = 47;
			this.BtnDownLoadReview.Text = "下载数据";
			this.BtnDownLoadReview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnDownLoadReview.UseVisualStyleBackColor = true;
			this.BtnDownLoadReview.Visible = false;
			this.BtnDownLoadReview.Click += new EventHandler(this.BtnDownLoadReview_Click);
			this.RdoAll.AutoSize = true;
			this.RdoAll.Location = new System.Drawing.Point(17, 511);
			this.RdoAll.Name = "RdoAll";
			this.RdoAll.Size = new System.Drawing.Size(47, 16);
			this.RdoAll.TabIndex = 48;
			this.RdoAll.TabStop = true;
			this.RdoAll.Text = "全部";
			this.RdoAll.UseVisualStyleBackColor = true;
			this.RdoAll.CheckedChanged += new EventHandler(this.RdoReview_CheckedChanged);
			this.RdoPending.AutoSize = true;
			this.RdoPending.ImeMode = ImeMode.NoControl;
			this.RdoPending.Location = new System.Drawing.Point(103, 511);
			this.RdoPending.Name = "RdoPending";
			this.RdoPending.Size = new System.Drawing.Size(59, 16);
			this.RdoPending.TabIndex = 49;
			this.RdoPending.TabStop = true;
			this.RdoPending.Text = "待审核";
			this.RdoPending.UseVisualStyleBackColor = true;
			this.RdoPending.CheckedChanged += new EventHandler(this.RdoReview_CheckedChanged);
			this.RdoReject.AutoSize = true;
			this.RdoReject.ImeMode = ImeMode.NoControl;
			this.RdoReject.Location = new System.Drawing.Point(324, 511);
			this.RdoReject.Name = "RdoReject";
			this.RdoReject.Size = new System.Drawing.Size(83, 16);
			this.RdoReject.TabIndex = 50;
			this.RdoReject.TabStop = true;
			this.RdoReject.Text = "审核未通过";
			this.RdoReject.UseVisualStyleBackColor = true;
			this.RdoReject.CheckedChanged += new EventHandler(this.RdoReview_CheckedChanged);
			this.RdoPass.AutoSize = true;
			this.RdoPass.ImeMode = ImeMode.NoControl;
			this.RdoPass.Location = new System.Drawing.Point(209, 511);
			this.RdoPass.Name = "RdoPass";
			this.RdoPass.Size = new System.Drawing.Size(71, 16);
			this.RdoPass.TabIndex = 51;
			this.RdoPass.TabStop = true;
			this.RdoPass.Text = "审核通过";
			this.RdoPass.UseVisualStyleBackColor = true;
			this.RdoPass.CheckedChanged += new EventHandler(this.RdoReview_CheckedChanged);
			this.ChkSelectedAll.AutoSize = true;
			this.ChkSelectedAll.Location = new System.Drawing.Point(25, 565);
			this.ChkSelectedAll.Name = "ChkSelectedAll";
			this.ChkSelectedAll.Size = new System.Drawing.Size(48, 16);
			this.ChkSelectedAll.TabIndex = 52;
			this.ChkSelectedAll.Text = "全选";
			this.ChkSelectedAll.UseVisualStyleBackColor = true;
			this.ChkSelectedAll.CheckedChanged += new EventHandler(this.ChkSelectedAll_CheckedChanged);
			this.ChkOpposite.AutoSize = true;
			this.ChkOpposite.ImeMode = ImeMode.NoControl;
			this.ChkOpposite.Location = new System.Drawing.Point(120, 565);
			this.ChkOpposite.Name = "ChkOpposite";
			this.ChkOpposite.Size = new System.Drawing.Size(48, 16);
			this.ChkOpposite.TabIndex = 53;
			this.ChkOpposite.Text = "反选";
			this.ChkOpposite.UseVisualStyleBackColor = true;
			this.ChkOpposite.CheckedChanged += new EventHandler(this.ChkOpposite_CheckedChanged);
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 10.5f);
			this.label1.ImeMode = ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(11, 283);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 14);
			this.label1.TabIndex = 54;
			this.label1.Text = "审核列表";
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 9f);
			this.label2.ImeMode = ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(9, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 55;
			this.label2.Text = "节目";
			this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel1.Controls.Add(this.BtnSendProgram);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Location = new System.Drawing.Point(25, 591);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(170, 28);
			this.panel1.TabIndex = 56;
			this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel2.Controls.Add(this.CBOGroupSend);
			this.panel2.Controls.Add(this.BtnGroupSendProgram);
			this.panel2.Controls.Add(this.LblGroups);
			this.panel2.Location = new System.Drawing.Point(469, 591);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(248, 28);
			this.panel2.TabIndex = 57;
			this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel3.Controls.Add(this.BtnSendSingleCmd);
			this.panel3.Controls.Add(this.CBOSingleCmd);
			this.panel3.Controls.Add(this.LblCmd);
			this.panel3.Location = new System.Drawing.Point(207, 591);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(249, 28);
			this.panel3.TabIndex = 57;
			this.PnlReview.BackColor = System.Drawing.Color.Transparent;
			this.PnlReview.Controls.Add(this.panel4);
			this.PnlReview.Controls.Add(this.BtnProgramSendList);
			this.PnlReview.Controls.Add(this.label3);
			this.PnlReview.Controls.Add(this.RdoAll);
			this.PnlReview.Controls.Add(this.RdoPass);
			this.PnlReview.Controls.Add(this.BtnReviewList);
			this.PnlReview.Controls.Add(this.RdoReject);
			this.PnlReview.Controls.Add(this.RdoPending);
			this.PnlReview.Controls.Add(this.DgvReviewList);
			this.PnlReview.Controls.Add(this.BtnPass);
			this.PnlReview.Controls.Add(this.BtnReject);
			this.PnlReview.Controls.Add(this.label1);
			this.PnlReview.Controls.Add(this.DgvTransdataIdList);
			this.PnlReview.Controls.Add(this.BtnDeleteProgramList);
			this.PnlReview.Location = new System.Drawing.Point(816, 54);
			this.PnlReview.Name = "PnlReview";
			this.PnlReview.Size = new System.Drawing.Size(430, 576);
			this.PnlReview.TabIndex = 58;
			this.panel4.Controls.Add(this.RdoSendProgramAll);
			this.panel4.Controls.Add(this.RdoSendProgramComplete);
			this.panel4.Controls.Add(this.RdoSendProgramAbnormal);
			this.panel4.Controls.Add(this.RdoSendProgramWait);
			this.panel4.Location = new System.Drawing.Point(14, 232);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(405, 26);
			this.panel4.TabIndex = 57;
			this.RdoSendProgramAll.AutoSize = true;
			this.RdoSendProgramAll.Location = new System.Drawing.Point(3, 5);
			this.RdoSendProgramAll.Name = "RdoSendProgramAll";
			this.RdoSendProgramAll.Size = new System.Drawing.Size(47, 16);
			this.RdoSendProgramAll.TabIndex = 52;
			this.RdoSendProgramAll.TabStop = true;
			this.RdoSendProgramAll.Text = "全部";
			this.RdoSendProgramAll.UseVisualStyleBackColor = true;
			this.RdoSendProgramAll.CheckedChanged += new EventHandler(this.RdoSendProgramAll_CheckedChanged);
			this.RdoSendProgramComplete.AutoSize = true;
			this.RdoSendProgramComplete.ImeMode = ImeMode.NoControl;
			this.RdoSendProgramComplete.Location = new System.Drawing.Point(195, 5);
			this.RdoSendProgramComplete.Name = "RdoSendProgramComplete";
			this.RdoSendProgramComplete.Size = new System.Drawing.Size(71, 16);
			this.RdoSendProgramComplete.TabIndex = 55;
			this.RdoSendProgramComplete.TabStop = true;
			this.RdoSendProgramComplete.Text = "完成接收";
			this.RdoSendProgramComplete.UseVisualStyleBackColor = true;
			this.RdoSendProgramComplete.CheckedChanged += new EventHandler(this.RdoSendProgramComplete_CheckedChanged);
			this.RdoSendProgramAbnormal.AutoSize = true;
			this.RdoSendProgramAbnormal.ImeMode = ImeMode.NoControl;
			this.RdoSendProgramAbnormal.Location = new System.Drawing.Point(310, 5);
			this.RdoSendProgramAbnormal.Name = "RdoSendProgramAbnormal";
			this.RdoSendProgramAbnormal.Size = new System.Drawing.Size(71, 16);
			this.RdoSendProgramAbnormal.TabIndex = 54;
			this.RdoSendProgramAbnormal.TabStop = true;
			this.RdoSendProgramAbnormal.Text = "下载异常";
			this.RdoSendProgramAbnormal.UseVisualStyleBackColor = true;
			this.RdoSendProgramAbnormal.CheckedChanged += new EventHandler(this.RdoSendProgramAbnormal_CheckedChanged);
			this.RdoSendProgramWait.AutoSize = true;
			this.RdoSendProgramWait.ImeMode = ImeMode.NoControl;
			this.RdoSendProgramWait.Location = new System.Drawing.Point(89, 5);
			this.RdoSendProgramWait.Name = "RdoSendProgramWait";
			this.RdoSendProgramWait.Size = new System.Drawing.Size(71, 16);
			this.RdoSendProgramWait.TabIndex = 53;
			this.RdoSendProgramWait.TabStop = true;
			this.RdoSendProgramWait.Text = "等待接收";
			this.RdoSendProgramWait.UseVisualStyleBackColor = true;
			this.RdoSendProgramWait.CheckedChanged += new EventHandler(this.RdoSendProgramWait_CheckedChanged);
			this.BtnProgramSendList.Image = Resources.refresh_L;
			this.BtnProgramSendList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnProgramSendList.Location = new System.Drawing.Point(189, 2);
			this.BtnProgramSendList.Name = "BtnProgramSendList";
			this.BtnProgramSendList.Size = new System.Drawing.Size(72, 24);
			this.BtnProgramSendList.TabIndex = 55;
			this.BtnProgramSendList.Text = "刷新";
			this.BtnProgramSendList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnProgramSendList.UseVisualStyleBackColor = true;
			this.BtnProgramSendList.Click += new EventHandler(this.BtnProgramSendList_Click);
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("宋体", 10.5f);
			this.label3.ImeMode = ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(11, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91, 14);
			this.label3.TabIndex = 56;
			this.label3.Text = "节目发送列表";
			this.imageListTelescopic.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageListTelescopic.ImageStream");
			this.imageListTelescopic.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTelescopic.Images.SetKeyName(0, "arrow_down.png");
			this.imageListTelescopic.Images.SetKeyName(1, "arrow_up.png");
			this.CtmsSelectFunction.Items.AddRange(new ToolStripItem[]
			{
				this.TsmiSelect,
				this.TsmiCancel
			});
			this.CtmsSelectFunction.Name = "CtmsDisplayTerminalId";
			this.CtmsSelectFunction.Size = new System.Drawing.Size(99, 48);
			this.TsmiSelect.Name = "TsmiSelect";
			this.TsmiSelect.Size = new System.Drawing.Size(98, 22);
			this.TsmiSelect.Text = "选择";
			this.TsmiSelect.Click += new EventHandler(this.TsmiSelect_Click);
			this.TsmiCancel.Name = "TsmiCancel";
			this.TsmiCancel.Size = new System.Drawing.Size(98, 22);
			this.TsmiCancel.Text = "取消";
			this.TsmiCancel.Click += new EventHandler(this.TsmiCancel_Click);
			this.CtmsDisplay.Items.AddRange(new ToolStripItem[]
			{
				this.TstbDisplayID
			});
			this.CtmsDisplay.Name = "CtmsDisplayTerminalId";
			this.CtmsDisplay.RenderMode = ToolStripRenderMode.System;
			this.CtmsDisplay.Size = new System.Drawing.Size(361, 29);
			this.TstbDisplayID.Name = "TstbDisplayID";
			this.TstbDisplayID.Size = new System.Drawing.Size(300, 23);
			this.pictureBoxUpAndDown.BackColor = System.Drawing.SystemColors.Control;
			this.pictureBoxUpAndDown.Image = Resources.arrow_down;
			this.pictureBoxUpAndDown.ImeMode = ImeMode.NoControl;
			this.pictureBoxUpAndDown.InitialImage = null;
			this.pictureBoxUpAndDown.Location = new System.Drawing.Point(773, 776);
			this.pictureBoxUpAndDown.Name = "pictureBoxUpAndDown";
			this.pictureBoxUpAndDown.Size = new System.Drawing.Size(42, 22);
			this.pictureBoxUpAndDown.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxUpAndDown.TabIndex = 59;
			this.pictureBoxUpAndDown.TabStop = false;
			this.pictureBoxUpAndDown.Visible = false;
			this.pictureBoxUpAndDown.MouseUp += new MouseEventHandler(this.DownOrUp_MouseUp);
			this.imageListLeftAndRight.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageListLeftAndRight.ImageStream");
			this.imageListLeftAndRight.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListLeftAndRight.Images.SetKeyName(0, "arrow_left.png");
			this.imageListLeftAndRight.Images.SetKeyName(1, "arrow_right.png");
			this.pictureBoxLeftAndRight.BackColor = System.Drawing.SystemColors.Control;
			this.pictureBoxLeftAndRight.Image = Resources.arrow_left;
			this.pictureBoxLeftAndRight.ImeMode = ImeMode.NoControl;
			this.pictureBoxLeftAndRight.InitialImage = null;
			this.pictureBoxLeftAndRight.Location = new System.Drawing.Point(784, 299);
			this.pictureBoxLeftAndRight.Name = "pictureBoxLeftAndRight";
			this.pictureBoxLeftAndRight.Size = new System.Drawing.Size(22, 42);
			this.pictureBoxLeftAndRight.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBoxLeftAndRight.TabIndex = 60;
			this.pictureBoxLeftAndRight.TabStop = false;
			this.pictureBoxLeftAndRight.MouseUp += new MouseEventHandler(this.LeftOrRight_MouseUp);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(814, 634);
			base.Controls.Add(this.pictureBoxLeftAndRight);
			base.Controls.Add(this.pictureBoxUpAndDown);
			base.Controls.Add(this.PnlReview);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.ChkOpposite);
			base.Controls.Add(this.DgvSingleCmdRecord);
			base.Controls.Add(this.ChkSelectedAll);
			base.Controls.Add(this.BtnDownLoadReview);
			base.Controls.Add(this.BtnExplanation);
			base.Controls.Add(this.BtnSingleCmdResendList);
			base.Controls.Add(this.BtnReSendSingleCmd);
			base.Controls.Add(this.panel_Waitting);
			base.Controls.Add(this.ToolStripOperating);
			base.Controls.Add(this.LblDeviceList);
			base.Controls.Add(this.buttonRefreash);
			base.Controls.Add(this.DgvTerminalDatas);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Icon = Resources.AppIcon;
			base.Name = "formCloudServerSend";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Cloud";
			base.FormClosed += new FormClosedEventHandler(this.formCloudServerSend_FormClosed);
			base.Load += new EventHandler(this.formCloudServerSend_Load);
			base.KeyDown += new KeyEventHandler(this.formCloudServerSend_KeyDown);
			((ISupportInitialize)this.DgvTerminalDatas).EndInit();
			((ISupportInitialize)this.DgvTransdataIdList).EndInit();
			this.ToolStripOperating.ResumeLayout(false);
			this.ToolStripOperating.PerformLayout();
			this.panel_Waitting.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox2).EndInit();
			((ISupportInitialize)this.DgvSingleCmdRecord).EndInit();
			((ISupportInitialize)this.DgvReviewList).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.PnlReview.ResumeLayout(false);
			this.PnlReview.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.CtmsSelectFunction.ResumeLayout(false);
			this.CtmsDisplay.ResumeLayout(false);
			this.CtmsDisplay.PerformLayout();
			((ISupportInitialize)this.pictureBoxUpAndDown).EndInit();
			((ISupportInitialize)this.pictureBoxLeftAndRight).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
