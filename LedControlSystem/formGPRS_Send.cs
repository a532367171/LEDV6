using LedCommunication;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Content;
using LedModel.Data;
using LedModel.Enum;
using LedModel.Foundation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace LedControlSystem.LedControlSystem
{
	public class formGPRS_Send : Form
	{
		private static string formID = "formGPRS_Send";

		public formMain fm;

		public static string xmlPath = Application.StartupPath + "\\GPRSStatus.xml";

		private int IDIndex = 9;

		private int ModelIndex = 6;

		public static IList<string> GroupList = new List<string>();

		private bool isAllSendToDevice;

		private bool nowAllowUpdate = true;

		private IList<int> compareIndex = new List<int>();

		private int[] checkReoutingIndex = new int[]
		{
			13,
			14,
			15,
			16,
			17
		};

		private IList<Thread> threadList = new List<Thread>();

		private bool isAnimating;

		private bool isSending;

		private string deviceCodes = string.Empty;

		private IList<LedPanel> gprsPanelList = new List<LedPanel>();

		private bool isDownloadSingle = true;

		private int CheckTime;

		private IContainer components;

		private MenuStrip menuStrip1;

		private ListBox listBox_Local;

		private DataGridView dataGridView_DeviceList;

		private GroupBox groupBox_Local;

		private GroupBox groupBox_Remoteto;

		private Button button_Send;

		private ToolStripMenuItem FileToolStripMenuItem;

		private ToolStripMenuItem changePasswordToolStripMenuItem;

		private ToolStripMenuItem getUserDevieceListToolStripMenuItem;

		private Button button_ReloadDeviceList;

		private Button button_Timing;

		private Button button_Lumance;

		private Button button_Start;

		private Button button_Off;

		private GroupBox groupBox_DeviceControl;

		private Button button_Auto;

		private Button button_BindNewDevice;

		private Button button_Load;

		private Button button_ChangePassword;

		private Button button_WorkingStatus;

		private Button button1;

		private ComboBox comboBox_SingleCommand;

		private Label label1;

		private Button button_EditInfo;

		private Button button_SaveRelationship;

		private Button button_ChangeAdvancedPassword;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem ChangeAdvPassordToolStripMenuItem;

		private System.Windows.Forms.Timer timer1;

		private System.Windows.Forms.Timer timer_Single;

		private CheckBox checkBox_SelectAll;

		private CheckBox checkBox_Reverse;

		private ComboBox comboBox_GroupLis;

		private Label label_Group;

		private Label label2;

		private Label label3;

		private Button button_SelectGroup;

		private Button button_SendAll;

		private Button button2;

		private Label label_dataCRC;

		private DataGridViewCheckBoxColumn Column_Check;

		private DataGridViewTextBoxColumn col_Description;

		private DataGridViewTextBoxColumn col_Status;

		private DataGridViewTextBoxColumn Col_SendPanel;

		private DataGridViewTextBoxColumn Column_SendStatus;

		private DataGridViewTextBoxColumn col_Signal_strength;

		private DataGridViewTextBoxColumn col_Model;

		private DataGridViewTextBoxColumn col_Width;

		private DataGridViewTextBoxColumn col_Height;

		private DataGridViewTextBoxColumn col_ID;

		private DataGridViewTextBoxColumn col_DeviceCode;

		private DataGridViewTextBoxColumn col_PhoneNumber;

		private DataGridViewTextBoxColumn Col_IP;

		private DataGridViewTextBoxColumn col_Routing;

		private DataGridViewTextBoxColumn Column_LastSend;

		private DataGridViewTextBoxColumn Column_DeviceVerion;

		private DataGridViewTextBoxColumn Col_SingleCommand;

		public Panel panel_Waitting;

		private Label label_Waitting_Picture;

		private PictureBox pictureBox2;

		private Button btnDeleteGroup;

		private ComboBox cmbSearchType;

		private TextBox txtSearchKey;

		private Button btnSearch;

		public static string FormID
		{
			get
			{
				return formGPRS_Send.formID;
			}
			set
			{
				formGPRS_Send.formID = value;
			}
		}

		public formGPRS_Send()
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formGPRS_Send.formID;
			this.Text = formMain.ML.GetStr("formGPRS_Send_FormText");
			this.groupBox_Local.Text = formMain.ML.GetStr("formGPRS_Send_groupBox_Local");
			this.label_Group.Text = formMain.ML.GetStr("formGPRS_Send_label_Group");
			this.button_SelectGroup.Text = formMain.ML.GetStr("formGPRS_Send_button_SelectGroup");
			this.groupBox_Remoteto.Text = formMain.ML.GetStr("formGPRS_Send_groupBox_Remoteto");
			this.button_ChangePassword.Text = formMain.ML.GetStr("formGPRS_Send_button_ChangePassword");
			this.button_ChangeAdvancedPassword.Text = formMain.ML.GetStr("formGPRS_Send_button_ChangeAdvancedPassword");
			this.button_BindNewDevice.Text = formMain.ML.GetStr("formGPRS_Send_button_BindNewDevice");
			this.checkBox_SelectAll.Text = formMain.ML.GetStr("formGPRS_Send_checkBox_SelectAll");
			this.checkBox_Reverse.Text = formMain.ML.GetStr("formGPRS_Send_checkBox_Reverse");
			this.button_SaveRelationship.Text = formMain.ML.GetStr("formGPRS_Send_button_SaveRelationship");
			this.button_ReloadDeviceList.Text = formMain.ML.GetStr("formGPRS_Send_button_ReloadDeviceList");
			this.label1.Text = formMain.ML.GetStr("formGPRS_Send_label_SingleCommand");
			this.button1.Text = formMain.ML.GetStr("formGPRS_Send_button_Send_Command");
			this.btnDeleteGroup.Text = formMain.ML.GetStr("formGPRS_Send_button_Delete_Group");
			this.button_EditInfo.Text = formMain.ML.GetStr("formGPRS_Send_button_EditInfo");
			this.button_SendAll.Text = formMain.ML.GetStr("formGPRS_Send_button_SendAll");
			this.button_Send.Text = formMain.ML.GetStr("formGPRS_Send_button_Send");
			this.label2.Text = formMain.ML.GetStr("formGPRS_Send_label_Online");
			this.label3.Text = formMain.ML.GetStr("formGPRS_Send_label_Online");
			this.label_dataCRC.Text = formMain.ML.GetStr("formGPRS_Send_label_dataCRC");
			this.groupBox_DeviceControl.Text = formMain.ML.GetStr("formGPRS_Send_groupBox_DeviceControl");
			this.button_Start.Text = formMain.ML.GetStr("formGPRS_Send_button_Start");
			this.button_Off.Text = formMain.ML.GetStr("formGPRS_Send_button_Off");
			this.button_Timing.Text = formMain.ML.GetStr("formGPRS_Send_button_Timing");
			this.button_WorkingStatus.Text = formMain.ML.GetStr("formGPRS_Send_button_WorkingStatus");
			this.button_Load.Text = formMain.ML.GetStr("formGPRS_Send_button_Load");
			this.button_Auto.Text = formMain.ML.GetStr("formGPRS_Send_button_Auto");
			this.button_Lumance.Text = formMain.ML.GetStr("formGPRS_Send_button_Lumance");
			this.FileToolStripMenuItem.Text = formMain.ML.GetStr("formGPRS_Send_ToolStripMenuItem_File");
			this.changePasswordToolStripMenuItem.Text = formMain.ML.GetStr("formGPRS_Send_ToolStripMenuItem_changePassword");
			this.getUserDevieceListToolStripMenuItem.Text = formMain.ML.GetStr("formGPRS_Send_getUserDevieceList_ToolStripMenuItem");
			this.toolStripMenuItem1.Text = formMain.ML.GetStr("formGPRS_Send_confirmAdvancedPassword_ToolStripMenuItem");
			this.ChangeAdvPassordToolStripMenuItem.Text = formMain.ML.GetStr("formGPRS_Send_ToolStripMenuItem_changeAdvancedPassword");
			formMain.str_item_comboBox(this.comboBox_SingleCommand, "formGPRS_Send_comboBox_SingleCommand_specific", null);
			this.comboBox_SingleCommand.Items.RemoveAt(2);
			formMain.str_item_comboBox(this.cmbSearchType, "formGPRS_Send_comboBox_Search_Type", null);
			this.dataGridView_DeviceList.Columns[0].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_Select");
			this.dataGridView_DeviceList.Columns[1].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_DeviceDescription");
			this.dataGridView_DeviceList.Columns[2].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_Status");
			this.dataGridView_DeviceList.Columns[3].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_DataDescription");
			this.dataGridView_DeviceList.Columns[4].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_SendStatus");
			this.dataGridView_DeviceList.Columns[5].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_SignalStrength");
			this.dataGridView_DeviceList.Columns[6].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_CardModel");
			this.dataGridView_DeviceList.Columns[7].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_Width");
			this.dataGridView_DeviceList.Columns[8].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_High");
			this.dataGridView_DeviceList.Columns[9].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_DeviceID");
			this.dataGridView_DeviceList.Columns[10].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_DeviceNO");
			this.dataGridView_DeviceList.Columns[11].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_TelephoneNumber");
			this.dataGridView_DeviceList.Columns[12].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_IP");
			this.dataGridView_DeviceList.Columns[13].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_Trace");
			this.dataGridView_DeviceList.Columns[14].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_SendTime");
			this.dataGridView_DeviceList.Columns[15].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_DeviceVersion");
			this.dataGridView_DeviceList.Columns[16].HeaderText = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_SingleCommand");
		}

		private void dataGridView_DeviceList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex < 0)
			{
				return;
			}
			if (e.RowIndex >= 0 && !e.FormattingApplied && (e.ColumnIndex == 2 || e.ColumnIndex == 4))
			{
				e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
			}
		}

		public static bool AddGroup(string pGroup)
		{
			for (int i = 0; i < formGPRS_Send.GroupList.Count; i++)
			{
				if (formGPRS_Send.GroupList[i] == pGroup)
				{
					return false;
				}
			}
			formGPRS_Send.GroupList.Add(pGroup);
			return true;
		}

		public static bool AddGrpupByDescription(string pDescription)
		{
			int num = pDescription.IndexOf("_");
			if (num > 0)
			{
				string pGroup = pDescription.Substring(0, num);
				return formGPRS_Send.AddGroup(pGroup);
			}
			return false;
		}

		public static byte[] CRC(byte[] message)
		{
			ushort num = 65535;
			for (int i = 0; i < message.Length; i++)
			{
				num ^= (ushort)message[i];
				for (int j = 0; j < 8; j++)
				{
					char c = (char)(num & 1);
					num = (ushort)(num >> 1 & 32767);
					if (c == '\u0001')
					{
						num ^= 40961;
					}
				}
			}
			message[message.Length - 3] = (byte)(num & 255);
			message[message.Length - 2] = (byte)(num >> 8 & 255);
			return message;
		}

		public static int CRCCheck(byte[] message)
		{
			ushort num = 65535;
			for (int i = 0; i < message.Length; i++)
			{
				num ^= (ushort)message[i];
				for (int j = 0; j < 8; j++)
				{
					char c = (char)(num & 1);
					num = (ushort)(num >> 1 & 32767);
					if (c == '\u0001')
					{
						num ^= 40961;
					}
				}
			}
			return (int)num;
		}

		private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new formGPRS_ChangePassword
			{
				IsUser = true
			}.ChangePassword("", "", this);
		}

		private void button2_Click(object sender, EventArgs e)
		{
		}

		private void getUserDevieceListToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void GetDeviceList(bool pClear)
		{
			int num = 0;
			int num2 = 0;
			string arg_09_0 = string.Empty;
			try
			{
				this.isAllSendToDevice = true;
				JArray jArray = GprsAdministrator.API_GetUserDeviceList();
				if (jArray == null)
				{
					this.nowAllowUpdate = false;
					this.timer_Single.Stop();
					this.timer1.Stop();
					MessageBox.Show(this, formMain.ML.GetStr("GPRS_CannotConnectionServer"));
				}
				else if (jArray.Count != 0)
				{
					if (pClear)
					{
						this.dataGridView_DeviceList.Rows.Clear();
						for (int i = 0; i < jArray.Count; i++)
						{
							num2 = jArray.Count;
							DataGridViewRow dataGridViewRow = new DataGridViewRow();
							dataGridViewRow.CreateCells(this.dataGridView_DeviceList);
							dataGridViewRow.Cells[1].Value = jArray[i]["description"];
							dataGridViewRow.Cells[2].Value = this.IsOnline(jArray[i]["online"].ToString());
							if (jArray[i]["online"].ToString().IndexOf("rue") > 0)
							{
								num++;
							}
							dataGridViewRow.Cells[2].Style.ForeColor = this.IsOnlineColor(jArray[i]["online"].ToString());
							if (this.IsLastSendData(jArray[i]["id"].ToString()))
							{
								dataGridViewRow.Cells[4].Value = this.fromSendStatusToString(jArray[i]["lastSendStatus"].ToString());
								dataGridViewRow.Cells[4].Style.ForeColor = this.fromSendStatusToColor(jArray[i]["lastSendStatus"].ToString());
							}
							int num3 = jArray[i]["signalStrength"].ToObject<int>() * 100 / 31;
							if (num3 > 100)
							{
								dataGridViewRow.Cells[5].Value = 99;
							}
							else
							{
								dataGridViewRow.Cells[5].Value = num3;
							}
							dataGridViewRow.Cells[6].Value = jArray[i]["deviceModel"];
							dataGridViewRow.Cells[7].Value = jArray[i]["width"];
							dataGridViewRow.Cells[8].Value = jArray[i]["height"];
							dataGridViewRow.Cells[9].Value = jArray[i]["id"];
							dataGridViewRow.Cells[10].Value = jArray[i]["terminalCode"];
							dataGridViewRow.Cells[11].Value = jArray[i]["phoneNumber"];
							dataGridViewRow.Cells[12].Value = jArray[i]["ip"];
							dataGridViewRow.Cells[13].Value = jArray[i]["routingDescription"];
							dataGridViewRow.Cells[14].Value = jArray[i]["lastSendDatetime"];
							dataGridViewRow.Cells[15].Value = jArray[i]["deviceVersion"].ToString();
							dataGridViewRow.Cells[15].Value = this.TranslateVersion(dataGridViewRow.Cells[15].Value.ToString());
							this.dataGridView_DeviceList.Rows.Add(dataGridViewRow);
						}
						this.dataGridView_DeviceList.Sort(this.dataGridView_DeviceList.Columns[1], ListSortDirection.Ascending);
					}
					else
					{
						for (int j = 0; j < jArray.Count; j++)
						{
							for (int k = 0; k < this.dataGridView_DeviceList.Rows.Count; k++)
							{
								if (this.dataGridView_DeviceList.Rows[k].Cells[10].Value.ToString() == jArray[j]["terminalCode"].ToString())
								{
									this.dataGridView_DeviceList.Rows[k].Cells[1].Value = jArray[j]["description"];
									this.dataGridView_DeviceList.Rows[k].Cells[2].Value = this.IsOnline(jArray[j]["online"].ToString());
									if (this.dataGridView_DeviceList.Rows[k].Visible)
									{
										num2++;
										if (jArray[j]["online"].ToString().IndexOf("rue") > 0)
										{
											num++;
										}
									}
									this.dataGridView_DeviceList.Rows[k].Cells[2].Style.ForeColor = this.IsOnlineColor(jArray[j]["online"].ToString());
									if (this.IsLastSendData(jArray[j]["id"].ToString()))
									{
										this.dataGridView_DeviceList.Rows[k].Cells[4].Value = this.fromSendStatusToString(jArray[j]["lastSendStatus"].ToString());
										this.dataGridView_DeviceList.Rows[k].Cells[4].Style.ForeColor = this.fromSendStatusToColor(jArray[j]["lastSendStatus"].ToString());
									}
									int num4 = jArray[j]["signalStrength"].ToObject<int>() * 100 / 31;
									if (num4 > 100)
									{
										this.dataGridView_DeviceList.Rows[k].Cells[5].Value = 99;
									}
									else
									{
										this.dataGridView_DeviceList.Rows[k].Cells[5].Value = num4;
									}
									this.dataGridView_DeviceList.Rows[k].Cells[6].Value = jArray[j]["deviceModel"];
									this.dataGridView_DeviceList.Rows[k].Cells[7].Value = jArray[j]["width"];
									this.dataGridView_DeviceList.Rows[k].Cells[8].Value = jArray[j]["height"];
									this.dataGridView_DeviceList.Rows[k].Cells[9].Value = jArray[j]["id"];
									this.dataGridView_DeviceList.Rows[k].Cells[10].Value = jArray[j]["terminalCode"];
									this.dataGridView_DeviceList.Rows[k].Cells[11].Value = jArray[j]["phoneNumber"];
									this.dataGridView_DeviceList.Rows[k].Cells[12].Value = jArray[j]["ip"];
									this.dataGridView_DeviceList.Rows[k].Cells[13].Value = jArray[j]["routingDescription"];
									this.dataGridView_DeviceList.Rows[k].Cells[14].Value = jArray[j]["lastSendDatetime"];
									this.dataGridView_DeviceList.Rows[k].Cells[15].Value = jArray[j]["deviceVersion"].ToString();
									this.dataGridView_DeviceList.Rows[k].Cells[15].Value = this.TranslateVersion(this.dataGridView_DeviceList.Rows[k].Cells[15].Value.ToString());
								}
							}
						}
					}
					this.label3.Text = num.ToString() + "/" + num2.ToString();
					if (pClear)
					{
						this.LoadRelationship(formMain.ledsys.SelectedPanel, this.dataGridView_DeviceList);
					}
				}
			}
			catch
			{
			}
		}

		private string fromSendStatusToString(string pStatus)
		{
			return formMain.ML.GetStr("GPRS_SendStatus" + pStatus.Trim());
		}

		private System.Drawing.Color fromSendStatusToColor(string pStatus)
		{
			if (pStatus == "0")
			{
				return System.Drawing.Color.Red;
			}
			if (pStatus == "1")
			{
				this.isAllSendToDevice = false;
				return System.Drawing.Color.Blue;
			}
			if (pStatus == "2")
			{
				return System.Drawing.Color.FromArgb(0, 255, 0);
			}
			if (pStatus == "3")
			{
				return System.Drawing.Color.Red;
			}
			return System.Drawing.Color.Black;
		}

		private bool CheckRoutingData(string pDeviceID, LedPanel pPanel)
		{
			IList<string> list = new List<string>();
			byte[] array = pPanel.ToBytes();
			formGPRS_Send.CRCCheck(array);
			string[] array2 = pDeviceID.Split(new char[]
			{
				','
			});
			string[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				string text = array3[i];
				if (text != "")
				{
					byte[] pParam = GprsAdministrator.API_DownLoadRoutingData(text);
					if (!this.CheckPanleParam(array, pParam))
					{
						list.Add(text);
					}
				}
			}
			if (list.Count <= 0)
			{
				return true;
			}
			if (MessageBox.Show(this, formMain.ML.GetStr("GRPS_ParamDiffrent"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return false;
			}
			formGPRS_CheckAdvancedPassword formGPRS_CheckAdvancedPassword = new formGPRS_CheckAdvancedPassword();
			if (formGPRS_CheckAdvancedPassword.Check(this))
			{
				foreach (string current in list)
				{
					GprsAdministrator.API_UPLoadRoutingData(current, array);
				}
				return true;
			}
			return false;
		}

		private void UpdateRoutingData(string pDeviceID, LedPanel pPanel)
		{
			string[] array = pDeviceID.Split(new char[]
			{
				','
			});
			byte[] pRoutingData = pPanel.ToBytes();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text != "")
				{
					GprsAdministrator.API_UPLoadRoutingData(text, pRoutingData);
				}
			}
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string text2 = array3[j];
				if (text2 != "")
				{
					GprsAdministrator.API_DownLoadRoutingData(text2);
				}
			}
		}

		private string IsOnline(string pStr)
		{
			if (pStr == "True")
			{
				return formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_Online");
			}
			return formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_Offline");
		}

		private System.Drawing.Color IsOnlineColor(string pStr)
		{
			if (pStr == "True")
			{
				return System.Drawing.Color.FromArgb(0, 255, 0);
			}
			return System.Drawing.Color.Red;
		}

		private string GetSelectedDeviceCode()
		{
			string text = "";
			for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
			{
				if (this.dataGridView_DeviceList.Rows[i].Visible)
				{
					object value = this.dataGridView_DeviceList.Rows[i].Cells[0].Value;
					if (value != null && value.ToString() == "True")
					{
						text = text + this.dataGridView_DeviceList.Rows[i].Cells[this.IDIndex].Value.ToString() + ",";
					}
				}
			}
			return text;
		}

		private string GetSelectedDeviceCode_model()
		{
			string text = "";
			for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
			{
				if (this.dataGridView_DeviceList.Rows[i].Visible)
				{
					object value = this.dataGridView_DeviceList.Rows[i].Cells[0].Value;
					if (value != null && value.ToString() == "True")
					{
						text = text + this.dataGridView_DeviceList.Rows[i].Cells[this.ModelIndex].Value.ToString() + ",";
					}
				}
			}
			return text;
		}

		private string TranslateVersion(object pValue)
		{
			string result;
			try
			{
				if (pValue == null || string.IsNullOrEmpty(pValue.ToString()))
				{
					result = "";
				}
				else
				{
					string text = pValue.ToString();
					string text2 = "V";
					byte b = byte.Parse(text.Substring(0, text.Length - 8), NumberStyles.HexNumber);
					byte b2 = byte.Parse(text.Substring(1, 2), NumberStyles.HexNumber);
					byte b3 = byte.Parse(text.Substring(3, 2), NumberStyles.HexNumber);
					byte b4 = byte.Parse(text.Substring(5, 2), NumberStyles.HexNumber);
					byte b5 = byte.Parse(text.Substring(7, 2), NumberStyles.HexNumber);
					int num = (int)(b4 + b5);
					text2 = text2 + b.ToString() + ".";
					text2 = text2 + b2.ToString("D2") + ".";
					text2 = text2 + b3.ToString("D2") + ".";
					text2 += num.ToString("D3");
					result = text2;
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private bool CheckPanleParam(byte[] pParam1, byte[] pParam2)
		{
			if (pParam2 == null)
			{
				return false;
			}
			if (pParam2.Length != pParam1.Length)
			{
				return false;
			}
			for (int i = 0; i < this.checkReoutingIndex.Length; i++)
			{
				if (pParam1[this.checkReoutingIndex[i]] != pParam2[this.checkReoutingIndex[i]])
				{
					return false;
				}
			}
			for (int j = 18; j < 111; j++)
			{
				if (pParam1[j] != pParam2[j])
				{
					return false;
				}
			}
			return true;
		}

		private void button_Send_Click(object sender, EventArgs e)
		{
			string selectedDeviceCode_model = this.GetSelectedDeviceCode_model();
			string[] array = selectedDeviceCode_model.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != "" && array[i] != string.Empty)
				{
					string a = array[i].Replace("-", "_");
					if (a != formMain.ledsys.SelectedPanel.CardType.ToString())
					{
						MessageBox.Show(formMain.ML.GetStr("formGPRS_Send_message_SomeModels_Not_Match"));
						return;
					}
				}
			}
			this.label_dataCRC.Text = "";
			if (this.isSending)
			{
				return;
			}
			this.isSending = true;
			this.isAnimating = false;
			this.deviceCodes = this.GetSelectedDeviceCode();
			if (this.deviceCodes == "")
			{
				MessageBox.Show(this, formMain.ML.GetStr("GPRS_PleaseSelectDevice"));
				this.isSending = false;
				return;
			}
			if (!this.CheckRoutingData(this.deviceCodes, formMain.ledsys.SelectedPanel))
			{
				this.isSending = false;
				return;
			}
			if (formMain.ledsys.SelectedPanel.Items.Count == 0)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Prompt_NoItem"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				this.isSending = false;
				return;
			}
			if (this.fm.CheckSubareaOverLapping())
			{
				MessageBox.Show(this, this.fm.overlappingItem + formMain.ML.GetStr("Prompt_ItemSubareaOverlap"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				this.isSending = false;
				return;
			}
			if (this.fm.isALlItemClosed)
			{
				MessageBox.Show(this, this.fm.overlappingItem + formMain.ML.GetStr("Prompt_AllItemClosed"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				this.isSending = false;
				return;
			}
			if (this.fm.hasNullItem)
			{
				MessageBox.Show(this, this.fm.overlappingItem + formMain.ML.GetStr("Prompt_HasNullIitem"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				this.isSending = false;
				return;
			}
			if (this.fm.CheckEmptyMarquee())
			{
				MessageBox.Show(this, formMain.ML.GetStr("Prompt_HasEmptyMarquee"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				this.isSending = false;
				return;
			}
			if (this.fm.CheckLunarSubarea())
			{
				MessageBox.Show(this, formMain.ML.GetStr("Prompt_NotSupportLunarCalendar"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				this.isSending = false;
				return;
			}
			Thread thread = new Thread(new ThreadStart(this.StartSendData));
			this.threadList.Add(thread);
			thread.Start();
		}

		private void StartSendData()
		{
			int num = 1024;
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
				this.label_Waitting_Picture.Text = formMain.ML.GetStr("Prompt_NowIsUploadingData");
				this.label_Waitting_Picture.Refresh();
			}));
			Thread.Sleep(500);
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			TimeSpan timeSpan = now2 - now;
			while (this.isAnimating && timeSpan.TotalSeconds < 120.0)
			{
				Thread.Sleep(1000);
				now2 = DateTime.Now;
				timeSpan = now2 - now;
			}
			string text = "";
			try
			{
				text = this.deviceCodes;
				Process process = new Process();
				process.PanelBytes = formMain.ledsys.SelectedPanel.ToBytes();
				process.BmpDataBytes = formMain.ledsys.SelectedPanel.ToItemBmpDataBytes();
				process.ItemBytes = formMain.ledsys.SelectedPanel.ToItemBytes();
				IList<byte> list = new List<byte>();
				IList<byte[]> list2 = protocol_GPRS.Send_Pack_GPRS(formMain.ledsys.SelectedPanel.CardAddress, 0, LedCmdType.Send_Begin, process, formMain.ledsys.SelectedPanel.ProtocolVersion);
				if (list2 != null && list2.Count > 0)
				{
					for (int l = 0; l < list2.Count; l++)
					{
						for (int m = 0; m < list2[l].Length; m++)
						{
							list.Add(list2[l][m]);
						}
					}
				}
				byte[] array = new byte[list.Count];
				list.CopyTo(array, 0);
				FileStream fileStream = new FileStream(Application.StartupPath + "\\GPRS.zhd", FileMode.Create, FileAccess.Write);
				fileStream.Write(array, 0, array.Length);
				fileStream.Close();
				string[] array2 = text.Split(new char[]
				{
					','
				});
				string[] array3 = array2;
				for (int n = 0; n < array3.Length; n++)
				{
					string text2 = array3[n];
					if (text2.Length > 0)
					{
						GprsAdministrator.API_UPLoadFile2(text2, Application.StartupPath + "\\GPRS.zhd", num + 28);
					}
				}
			}
			catch (Exception ex)
			{
				Exception Ex = ex;
				formGPRS_Send formGPRS_Send = this;
				base.Invoke(new MethodInvoker(delegate
				{
					MessageBox.Show(formGPRS_Send, Ex.Message);
				}));
			}
			finally
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.SetWaittingPanelVisable(false);
				}));
				Thread.Sleep(200);
			}
			formGPRS_Send.UpdateGPRSInfo(formMain.ledsys.SelectedPanel, text);
			this.UpdateRoutingData(text, formMain.ledsys.SelectedPanel);
			this.UpdateSendPanel(text, formMain.ledsys.SelectedPanel);
			base.Invoke(new MethodInvoker(delegate
			{
				this.UpdateSingleCommand();
			}));
			this.GetDeviceList(false);
			base.Invoke(new MethodInvoker(delegate
			{
				this.timer1.Start();
			}));
			this.isSending = false;
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

		public static void UpdateGPRSInfo(LedPanel pPanel, string pEntityID)
		{
			string[] array = pEntityID.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text != "" && text.Length > 0)
				{
					string text2 = "&entity.id=" + text;
					text2 = text2 + "&entity.width=" + pPanel.Width.ToString();
					text2 = text2 + "&entity.height=" + pPanel.Height.ToString();
					string text3;
					if (pPanel.ColorMode == LedColorMode.R)
					{
						text3 = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_ColorSingle") + ",";
					}
					else if (pPanel.ColorMode == LedColorMode.RG || pPanel.ColorMode == LedColorMode.GR)
					{
						text3 = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_ColorDouble") + ",";
					}
					else
					{
						text3 = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_ColorThree") + ",";
					}
					if (pPanel.OEPolarity == 1)
					{
						text3 = text3 + formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_OEHigh") + ",";
					}
					else
					{
						text3 = text3 + formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_OELow") + ",";
					}
					if (pPanel.DataPolarity == 1)
					{
						text3 = text3 + formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_DataHigh") + ",";
					}
					else
					{
						text3 = text3 + formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_DataLow") + ",";
					}
					text3 += pPanel.RoutingSetting.TypeDescription;
					text2 = text2 + "&entity.routingDescription=" + text3;
					GprsAdministrator.API_UserUpdateDeviceInfo(text2);
				}
			}
		}

		public static void UpdateGPRSDescInfo(LedPanel pPanel, string pEntityID, string pDes, string pGroup)
		{
			if (pGroup != null && pGroup.Length > 0)
			{
				pDes = pGroup + "_" + pDes;
			}
			string text = "&entity.id=" + pEntityID;
			text = text + "&entity.description=" + pDes;
			GprsAdministrator.API_UserUpdateDeviceInfo(text);
		}

		private void button_ReloadDeviceList_Click(object sender, EventArgs e)
		{
			this.GetDeviceList(false);
		}

		private void formGPRS_Send_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.panel_Waitting.Visible = false;
			this.panel_Waitting.SendToBack();
			this.GetDeviceList(true);
			this.GetGroup();
			this.timer1.Start();
			this.LoadLocalDeviceList();
			this.CheckGPRSXmlFile();
			this.UpdateSingleCommand();
			this.timer_Single.Start();
			try
			{
				this.comboBox_SingleCommand.SelectedIndex = 0;
			}
			catch
			{
			}
			this.compareIndex.Add(0);
			this.compareIndex.Add(1);
			this.compareIndex.Add(2);
			this.compareIndex.Add(3);
			this.compareIndex.Add(4);
			this.compareIndex.Add(5);
			this.compareIndex.Add(6);
			this.compareIndex.Add(7);
			for (int i = 13; i < 111; i++)
			{
				this.compareIndex.Add(i);
			}
			this.compareIndex.Add(120);
			this.compareIndex.Add(121);
			this.compareIndex.Add(122);
		}

		private void LoadLocalDeviceList()
		{
			int num = 0;
			for (int i = 0; i < formMain.ledsys.Panels.Count; i++)
			{
				if (formMain.ledsys.Panels[i].CardType.ToString().IndexOf("G") > 0 && formMain.ledsys.Panels[i].PortType == LedPortType.GPRS && formMain.ledsys.Panels[i].GPRSCommunicaitonMode == LedGPRSCommunicationMode.GprsServer)
				{
					this.gprsPanelList.Add(formMain.ledsys.Panels[i]);
					this.listBox_Local.Items.Add(formMain.ledsys.Panels[i].CardType.ToString() + "-" + formMain.ledsys.Panels[i].TextName);
					if (formMain.ledsys.SelectedIndex == i)
					{
						num = this.listBox_Local.Items.Count - 1;
					}
				}
			}
			if (this.listBox_Local.Items.Count > num)
			{
				this.listBox_Local.SelectedIndex = num;
			}
		}

		private void button_BindNewDevice_Click(object sender, EventArgs e)
		{
			formGPRSAdminLogin.bindPassword = "";
			formGPRSAdminLogin.bindUserName = "";
			new formGPRSAdminLogin
			{
				IsForBind = true,
				Text = this.button_BindNewDevice.Text
			}.ShowDialog(this);
			if (formGPRSAdminLogin.bindUserName == "" | formGPRSAdminLogin.bindPassword == "")
			{
				return;
			}
			if (GprsAdministrator.API_UserBindNewDevice(formGPRSAdminLogin.bindUserName, formGPRSAdminLogin.bindPassword))
			{
				MessageBox.Show(this, formMain.ML.GetStr("GPRS_bindSuccess"));
				this.GetDeviceList(true);
				return;
			}
			MessageBox.Show(this, formMain.ML.GetStr("GPRS_bindFailed") + GprsAdministrator.Message);
		}

		private void button_Timing_Click(object sender, EventArgs e)
		{
			string text = this.dataGridView_DeviceList.CurrentRow.Cells[0].Value.ToString();
			this.dataGridView_DeviceList.CurrentRow.Cells[1].Value.ToString();
			byte[] pCmdData = new byte[1];
			IList<byte[]> list = protocol_GPRS.Send_Pack_GPRS(formMain.ledsys.SelectedPanel.CardAddress, 0, LedCmdType.Ctrl_Timing, null, formMain.ledsys.SelectedPanel.ProtocolVersion);
			if (list != null && list.Count > 0)
			{
				pCmdData = list[0];
			}
			GprsAdministrator.API_SingleCommand(text, pCmdData);
			int num = 0;
			int num2 = 8;
			while (true)
			{
				Thread.Sleep(5000);
				num++;
				if (num >= num2)
				{
					break;
				}
				byte[] array = GprsAdministrator.API_DownSingleCommand(text, LedCmdType.Ctrl_Timing);
				if (array != null && array.Length >= 10)
				{
					goto Block_5;
				}
			}
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendFailed"));
			return;
			Block_5:
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendSuccessed"));
		}

		private void button_Start_Click(object sender, EventArgs e)
		{
			string text = this.dataGridView_DeviceList.CurrentRow.Cells[0].Value.ToString();
			this.dataGridView_DeviceList.CurrentRow.Cells[1].Value.ToString();
			byte[] pCmdData = new byte[1];
			IList<byte[]> list = protocol_GPRS.Send_Pack_GPRS(formMain.ledsys.SelectedPanel.CardAddress, 0, LedCmdType.Ctrl_Power_On, null, formMain.ledsys.SelectedPanel.ProtocolVersion);
			if (list != null && list.Count > 0)
			{
				pCmdData = list[0];
			}
			GprsAdministrator.API_SingleCommand(text, pCmdData);
			int num = 0;
			int num2 = 40;
			while (true)
			{
				Thread.Sleep(1000);
				num++;
				if (num >= num2)
				{
					break;
				}
				byte[] array = GprsAdministrator.API_DownSingleCommand(text, LedCmdType.Ctrl_Power_On);
				if (array != null && array.Length >= 10)
				{
					goto Block_5;
				}
			}
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendFailed"));
			return;
			Block_5:
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendSuccessed"));
		}

		private void button_Off_Click(object sender, EventArgs e)
		{
			string text = this.dataGridView_DeviceList.CurrentRow.Cells[0].Value.ToString();
			this.dataGridView_DeviceList.CurrentRow.Cells[1].Value.ToString();
			byte[] pCmdData = new byte[1];
			IList<byte[]> list = protocol_GPRS.Send_Pack_GPRS(formMain.ledsys.SelectedPanel.CardAddress, 0, LedCmdType.Ctrl_Power_Off, null, formMain.ledsys.SelectedPanel.ProtocolVersion);
			if (list != null && list.Count > 0)
			{
				pCmdData = list[0];
			}
			GprsAdministrator.API_SingleCommand(text, pCmdData);
			int num = 0;
			int num2 = 8;
			while (true)
			{
				Thread.Sleep(5000);
				num++;
				if (num >= num2)
				{
					break;
				}
				byte[] array = GprsAdministrator.API_DownSingleCommand(text, LedCmdType.Ctrl_Power_Off);
				if (array != null && array.Length >= 10)
				{
					goto Block_5;
				}
			}
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendFailed"));
			return;
			Block_5:
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendSuccessed"));
		}

		private void button_ChangePassword_Click(object sender, EventArgs e)
		{
			new formGPRS_ChangePassword
			{
				IsUser = true
			}.ChangePassword("", "", this);
		}

		private void button_Load_Click(object sender, EventArgs e)
		{
			string text = this.dataGridView_DeviceList.CurrentRow.Cells[0].Value.ToString();
			this.dataGridView_DeviceList.CurrentRow.Cells[1].Value.ToString();
			byte[] pCmdData = new byte[1];
			IList<byte[]> list = protocol_GPRS.Send_Pack_GPRS(formMain.ledsys.SelectedPanel.CardAddress, 0, LedCmdType.Send_Panel_Parameter, formMain.ledsys.SelectedPanel, formMain.ledsys.SelectedPanel.ProtocolVersion);
			if (list != null && list.Count > 0)
			{
				pCmdData = list[0];
			}
			GprsAdministrator.API_SingleCommand(text, pCmdData);
			int num = 0;
			int num2 = 40;
			while (true)
			{
				Thread.Sleep(1000);
				num++;
				if (num >= num2)
				{
					break;
				}
				byte[] array = GprsAdministrator.API_DownSingleCommand(text, LedCmdType.Send_Panel_Parameter);
				if (array != null && array.Length >= 10)
				{
					goto Block_5;
				}
			}
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendFailed"));
			return;
			Block_5:
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendSuccessed"));
		}

		private void button_Auto_Click(object sender, EventArgs e)
		{
			formMain.ledsys.SelectedPanel.TimerSwitch.ToBytes();
			string text = this.dataGridView_DeviceList.CurrentRow.Cells[0].Value.ToString();
			this.dataGridView_DeviceList.CurrentRow.Cells[1].Value.ToString();
			byte[] pCmdData = new byte[1];
			IList<byte[]> list = protocol_GPRS.Send_Pack_GPRS(formMain.ledsys.SelectedPanel.CardAddress, 0, LedCmdType.Ctrl_Timer_Switch, formMain.ledsys.SelectedPanel.TimerSwitch, formMain.ledsys.SelectedPanel.ProtocolVersion);
			if (list != null && list.Count > 0)
			{
				pCmdData = list[0];
			}
			GprsAdministrator.API_SingleCommand(text, pCmdData);
			int num = 0;
			int num2 = 40;
			while (true)
			{
				Thread.Sleep(1000);
				num++;
				if (num >= num2)
				{
					break;
				}
				byte[] array = GprsAdministrator.API_DownSingleCommand(text, LedCmdType.Ctrl_Timer_Switch);
				if (array != null && array.Length >= 10)
				{
					goto Block_5;
				}
			}
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendFailed"));
			return;
			Block_5:
			MessageBox.Show(this, formMain.ML.GetStr("Display_Source_SendSuccessed"));
		}

		private void button_AutoLumince_Click(object sender, EventArgs e)
		{
		}

		private void button_Lumance_Click(object sender, EventArgs e)
		{
			new formLuminance(formMain.ledsys.SelectedPanel.Luminance, this.fm, "", false)
			{
				isGPRS = true,
				tempDeviceCode = this.dataGridView_DeviceList.CurrentRow.Cells[1].Value.ToString(),
				tempDeviceID = this.dataGridView_DeviceList.CurrentRow.Cells[0].Value.ToString()
			}.ShowDialog(this);
		}

		private void button_WorkingStatus_Click(object sender, EventArgs e)
		{
			string text = this.dataGridView_DeviceList.CurrentRow.Cells[0].Value.ToString();
			this.dataGridView_DeviceList.CurrentRow.Cells[1].Value.ToString();
			byte[] pCmdData = new byte[1];
			IList<byte[]> list = protocol_GPRS.Send_Pack_GPRS(formMain.ledsys.SelectedPanel.CardAddress, 0, LedCmdType.Ctrl_GPRS_Working_State, null, formMain.ledsys.SelectedPanel.ProtocolVersion);
			if (list != null && list.Count > 0)
			{
				pCmdData = list[0];
			}
			GprsAdministrator.API_SingleCommand(text, pCmdData);
			int num = 0;
			int num2 = 8;
			while (true)
			{
				Thread.Sleep(5000);
				num++;
				if (num >= num2)
				{
					break;
				}
				byte[] array = GprsAdministrator.API_DownSingleCommand(text, LedCmdType.Ctrl_GPRS_Working_State);
				if (array != null && array.Length >= 10)
				{
					goto Block_5;
				}
			}
			MessageBox.Show(this, formMain.ML.GetStr("GPRS_Message_Status_Abnormal"));
			return;
			Block_5:
			MessageBox.Show(this, formMain.ML.GetStr("GPRS_Message_Working_Normal"));
		}

		private void listBox_Local_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				formMain.ledsys.SelectedPanel = this.gprsPanelList[this.listBox_Local.SelectedIndex];
				this.LoadRelationship(formMain.ledsys.SelectedPanel, this.dataGridView_DeviceList);
			}
			catch
			{
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			string pSuccess = "";
			string pFailed = "";
			LedCmdType pCmd = LedCmdType.Ctrl_GPRS_Working_State;
			object pData = null;
			switch (this.comboBox_SingleCommand.SelectedIndex)
			{
			case 0:
				pSuccess = "开机成功";
				pFailed = "开机失败";
				pCmd = LedCmdType.Ctrl_Power_On;
				pData = null;
				break;
			case 1:
				pSuccess = "关机成功";
				pFailed = "关机失败";
				pCmd = LedCmdType.Ctrl_Power_Off;
				pData = null;
				break;
			case 2:
				pSuccess = "加载成功";
				pFailed = "加载失败";
				pCmd = LedCmdType.Send_Panel_Parameter;
				pData = formMain.ledsys.SelectedPanel;
				break;
			case 3:
				pSuccess = "亮度调节成功";
				pFailed = "亮度调节失败";
				pCmd = LedCmdType.Ctrl_Luminance;
				pData = formMain.ledsys.SelectedPanel.Luminance;
				break;
			case 4:
				pSuccess = "定时开关机设置成功";
				pFailed = "定时开关机设置失败";
				pCmd = LedCmdType.Ctrl_Timer_Switch;
				pData = formMain.ledsys.SelectedPanel.TimerSwitch;
				break;
			case 5:
				pSuccess = "工作中...";
				pFailed = "状态异常";
				pCmd = LedCmdType.Ctrl_GPRS_Working_State;
				pData = null;
				break;
			}
			string selectedDeviceCode = this.GetSelectedDeviceCode();
			if (selectedDeviceCode == "")
			{
				MessageBox.Show(this, formMain.ML.GetStr("GPRS_PleaseSelectDevice"));
				return;
			}
			formGPRS_Send.SendSingleCommand(selectedDeviceCode, pSuccess, pFailed, pCmd, pData, this.dataGridView_DeviceList);
			this.UpdateSingleCommand();
			this.isDownloadSingle = true;
			this.timer_Single.Start();
		}

		public static void SendSingleCommand(string deviceList, string pSuccess, string pFailed, LedCmdType pCmd, object pData, DataGridView pGrid)
		{
			string[] array = deviceList.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			string text = "";
			string text2 = "";
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text3 = array2[i];
				byte[] pCmdData = new byte[1];
				IList<byte[]> list = protocol_GPRS.Send_Pack_GPRS(formMain.ledsys.SelectedPanel.CardAddress, 0, pCmd, pData, formMain.ledsys.SelectedPanel.ProtocolVersion);
				if (list != null && list.Count > 0)
				{
					pCmdData = list[0];
				}
				if (GprsAdministrator.API_SingleCommand(text3, pCmdData))
				{
					text = text + text3 + ",";
				}
				else
				{
					text2 = text2 + text3 + ",";
				}
			}
			formGPRS_Send.UpdateSingelInfo(text, text2, pCmd);
		}

		private void comboBox_SingleCommand_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void button_EditInfo_Click(object sender, EventArgs e)
		{
			new formGPRS_UserDetail
			{
				Dr = this.dataGridView_DeviceList.CurrentRow
			}.ShowDialog(this);
			this.GetDeviceList(false);
			this.GetGroup();
		}

		private void GetGroup()
		{
			formGPRS_Send.GroupList.Clear();
			for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
			{
				formGPRS_Send.AddGrpupByDescription(this.dataGridView_DeviceList.Rows[i].Cells[1].Value.ToString());
			}
			this.comboBox_GroupLis.Items.Clear();
			this.comboBox_GroupLis.Items.Add(formMain.ML.GetStr("GPRS_Group_All"));
			this.comboBox_GroupLis.Items.Add(formMain.ML.GetStr("GPRS_Group_UnGroup"));
			foreach (string current in formGPRS_Send.GroupList)
			{
				this.comboBox_GroupLis.Items.Add(current);
			}
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			formGPRS_CheckAdvancedPassword formGPRS_CheckAdvancedPassword = new formGPRS_CheckAdvancedPassword();
			MessageBox.Show(this, formGPRS_CheckAdvancedPassword.Check(this).ToString());
		}

		private void ChangeAdvPassordToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void button_ChangeAdvancedPassword_Click(object sender, EventArgs e)
		{
		}

		private void button_SaveRelationship_Click(object sender, EventArgs e)
		{
			this.SaveRelationship(formMain.ledsys.SelectedPanel, this.dataGridView_DeviceList);
		}

		private void SaveRelationship(LedPanel pPanel, DataGridView pGrid)
		{
			pPanel.GPRSDeviceID = this.GetSelectedDeviceCode();
		}

		private void LoadRelationship(LedPanel pPanel, DataGridView pGrid)
		{
			string[] array = pPanel.GPRSDeviceID.Split(new char[]
			{
				','
			});
			for (int i = 0; i < pGrid.Rows.Count; i++)
			{
				string a = pGrid.Rows[i].Cells[this.IDIndex].Value.ToString();
				pGrid.Rows[i].Cells[0].Value = false;
				for (int j = 0; j < array.Length; j++)
				{
					if (a == array[j])
					{
						pGrid.Rows[i].Cells[0].Value = true;
					}
				}
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.nowAllowUpdate)
			{
				this.GetDeviceList(false);
			}
			bool arg_15_0 = this.isAllSendToDevice;
		}

		private bool CheckSingleCommand()
		{
			return true;
		}

		private bool IsLastSendData(string pID)
		{
			bool result;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(formGPRS_Send.xmlPath);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/GPRS/GprsInfo[@ID='" + pID + "']");
				if (xmlNode.Attributes["LastOperation"] != null && xmlNode.Attributes["LastOperation"].Value == "data")
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void CheckGPRSXmlFile()
		{
			if (!File.Exists(formGPRS_Send.xmlPath))
			{
				this.GenerateGPRSStatusFile();
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(formGPRS_Send.xmlPath);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/GPRS");
			XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/GPRS/GprsInfo");
			for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
			{
				if (xmlDocument.SelectSingleNode("/GPRS/GprsInfo[@ID='" + this.dataGridView_DeviceList.Rows[i].Cells[this.IDIndex].Value.ToString() + "']") == null)
				{
					if (xmlNode2 != null)
					{
						XmlNode xmlNode3 = xmlNode2.Clone();
						XmlElement xmlElement = (XmlElement)xmlNode3;
						xmlElement.SetAttribute("ID", this.dataGridView_DeviceList.Rows[i].Cells[this.IDIndex].Value.ToString());
						xmlNode.AppendChild(xmlNode3);
					}
					else
					{
						XmlElement xmlElement2 = xmlDocument.CreateElement("GprsInfo");
						xmlElement2.SetAttribute("ID", this.dataGridView_DeviceList.Rows[i].Cells[this.IDIndex].Value.ToString());
						xmlElement2.SetAttribute("LastCommand", "");
						xmlElement2.SetAttribute("LastCommandResult", "");
						xmlElement2.SetAttribute("LastSendPanelName", "");
						xmlNode.AppendChild(xmlElement2);
					}
				}
			}
			xmlDocument.Save(formGPRS_Send.xmlPath);
		}

		public void GenerateGPRSStatusFile()
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement newChild = xmlDocument.CreateElement("GPRS");
				xmlDocument.AppendChild(newChild);
				xmlDocument.Save(formGPRS_Send.xmlPath);
			}
			catch
			{
			}
		}

		private void UpdateSingleCommand()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(formGPRS_Send.xmlPath);
			for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
			{
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/GPRS/GprsInfo[@ID='" + this.dataGridView_DeviceList.Rows[i].Cells[this.IDIndex].Value.ToString() + "']");
				try
				{
					if (xmlNode != null)
					{
						if (xmlNode.Attributes["LastOperation"] != null && xmlNode.Attributes["LastOperation"].Value == "single")
						{
							if (xmlNode.Attributes["LastCommand"] != null && xmlNode.Attributes["LastCommandResult"] != null)
							{
								this.dataGridView_DeviceList.Rows[i].Cells[4].Value = formMain.ML.GetStr("GPRS_" + xmlNode.Attributes["LastCommand"].Value) + formMain.ML.GetStr(xmlNode.Attributes["LastCommandResult"].Value);
							}
							if (xmlNode.Attributes["LastCommandResult"] != null)
							{
								this.dataGridView_DeviceList.Rows[i].Cells[4].Style.ForeColor = this.GetSingleCommandColor(xmlNode.Attributes["LastCommandResult"].Value);
							}
						}
						if (xmlNode.Attributes["LastSendPanelName"] != null)
						{
							this.dataGridView_DeviceList.Rows[i].Cells[3].Value = xmlNode.Attributes["LastSendPanelName"].Value;
						}
					}
				}
				catch
				{
				}
			}
		}

		private System.Drawing.Color GetSingleCommandColor(string pStr)
		{
			if (pStr == "GPRS_Single_Failed")
			{
				return System.Drawing.Color.Red;
			}
			if (pStr == "GPRS_Single_OK")
			{
				return System.Drawing.Color.Blue;
			}
			return System.Drawing.Color.FromArgb(0, 255, 0);
		}

		public void UpdateSendPanel(string pDeviceList, LedPanel pPanel)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(formGPRS_Send.xmlPath);
			string[] array = pDeviceList.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text.Trim().Length > 0)
				{
					try
					{
						XmlNode xmlNode = xmlDocument.SelectSingleNode("/GPRS/GprsInfo[@ID='" + text + "']");
						XmlElement xmlElement = (XmlElement)xmlNode;
						xmlElement.SetAttribute("LastSendPanelName", pPanel.TextName);
						xmlElement.SetAttribute("LastOperation", "data");
					}
					catch
					{
					}
				}
			}
			xmlDocument.Save(formGPRS_Send.xmlPath);
		}

		public void UpdateSendPanel(string pDeviceList)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(formGPRS_Send.xmlPath);
			string[] array = pDeviceList.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text.Trim().Length > 0)
				{
					XmlNode xmlNode = xmlDocument.SelectSingleNode("/GPRS/GprsInfo[@ID='" + text + "']");
					XmlElement xmlElement = (XmlElement)xmlNode;
					xmlElement.SetAttribute("LastSendPanelName", "群发");
					xmlElement.SetAttribute("LastOperation", "data");
				}
			}
			xmlDocument.Save(formGPRS_Send.xmlPath);
		}

		private static void UpdateSingelInfo(string successID, string failedDevice, LedCmdType pCmd)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(formGPRS_Send.xmlPath);
			string[] array = successID.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text.Length > 0)
				{
					XmlNode xmlNode = xmlDocument.SelectSingleNode("/GPRS/GprsInfo[@ID='" + text + "']");
					if (xmlNode != null)
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						xmlElement.SetAttribute("LastCommand", pCmd.ToString());
						xmlElement.SetAttribute("LastCommandResult", "GPRS_Single_OK");
						xmlElement.SetAttribute("LastCommandByte", ((byte)pCmd).ToString());
						xmlElement.SetAttribute("LastCommandTryTime", "0");
						xmlElement.SetAttribute("LastOperation", "single");
						if (formMain.ledsys.SelectedPanel != null)
						{
							xmlElement.SetAttribute("LastSendPanelName", formMain.ledsys.SelectedPanel.TextName);
						}
					}
				}
			}
			array = failedDevice.Split(new char[]
			{
				','
			});
			string[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				string text2 = array3[j];
				if (text2.Length > 0)
				{
					XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/GPRS/GprsInfo[@ID='" + text2 + "']");
					if (xmlNode2 != null)
					{
						XmlElement xmlElement2 = (XmlElement)xmlNode2;
						xmlElement2.SetAttribute("LastCommand", pCmd.ToString());
						xmlElement2.SetAttribute("LastCommandResult", "GPRS_Single_Failed");
						xmlElement2.SetAttribute("LastCommandByte", ((byte)pCmd).ToString());
						xmlElement2.SetAttribute("LastCommandTryTime", "0");
						xmlElement2.SetAttribute("LastOperation", "single");
						if (formMain.ledsys.SelectedPanel != null)
						{
							xmlElement2.SetAttribute("LastSendPanelName", formMain.ledsys.SelectedPanel.TextName);
						}
					}
				}
			}
			xmlDocument.Save(formGPRS_Send.xmlPath);
		}

		private void timer_Single_Tick(object sender, EventArgs e)
		{
			if (this.isDownloadSingle)
			{
				this.CheckTime = 0;
				this.DownloadSingleCommand();
			}
		}

		private void DownloadSingleCommand()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(formGPRS_Send.xmlPath);
			int num = 0;
			for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
			{
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/GPRS/GprsInfo[@ID='" + this.dataGridView_DeviceList.Rows[i].Cells[this.IDIndex].Value.ToString() + "']");
				if (xmlNode != null)
				{
					int num2 = 0;
					string value = xmlNode.Attributes["LastCommandResult"].Value;
					try
					{
						if (xmlNode.Attributes["LastCommandTryTime"] != null)
						{
							num2 = int.Parse(xmlNode.Attributes["LastCommandTryTime"].Value);
						}
					}
					catch
					{
						num2 = 0;
					}
					if (value == "GPRS_Single_OK" && num2 < 10)
					{
						num++;
						LedCmdType pCMD = (LedCmdType)Enum.Parse(typeof(LedCmdType), xmlNode.Attributes["LastCommand"].Value);
						byte[] array = GprsAdministrator.API_DownSingleCommand(xmlNode.Attributes["ID"].Value, pCMD);
						if (array == null || array.Length < 10)
						{
							num2++;
							XmlElement xmlElement = (XmlElement)xmlNode;
							xmlElement.SetAttribute("LastCommandTryTime", num2.ToString());
						}
						else
						{
							XmlElement xmlElement2 = (XmlElement)xmlNode;
							xmlElement2.SetAttribute("LastCommandResult", "GPRS_Single_Success");
							num--;
						}
					}
				}
			}
			xmlDocument.Save(formGPRS_Send.xmlPath);
			this.UpdateSingleCommand();
			if (num == 0)
			{
				this.isDownloadSingle = false;
				this.timer_Single.Stop();
			}
			if (this.CheckTime > 10)
			{
				this.isDownloadSingle = false;
				this.timer_Single.Stop();
			}
		}

		private void checkBox_SelectAll_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				this.checkBox_Reverse.Checked = false;
				this.SelectAll(this.dataGridView_DeviceList, true);
				return;
			}
			this.SelectAll(this.dataGridView_DeviceList, false);
		}

		private void SelectAll(DataGridView pGrid, bool pResult)
		{
			for (int i = 0; i < pGrid.Rows.Count; i++)
			{
				if (pGrid.Rows[i].Visible)
				{
					pGrid.Rows[i].Cells[0].Value = pResult;
				}
			}
		}

		private void SelectReverse(DataGridView pGrid)
		{
			for (int i = 0; i < pGrid.Rows.Count; i++)
			{
				pGrid.Rows[i].Cells[0].Value = !(bool)pGrid.Rows[i].Cells[0].Value;
			}
		}

		private void checkBox_Reverse_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				this.checkBox_SelectAll.Checked = false;
			}
			this.SelectReverse(this.dataGridView_DeviceList);
		}

		private void formGPRS_Send_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.isSending)
			{
				e.Cancel = true;
				return;
			}
			this.timer_Single.Stop();
			this.timer1.Stop();
		}

		private void comboBox_GroupLis_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = this.comboBox_GroupLis.Text;
			if (text == null || text.Length < 1)
			{
				return;
			}
			if (this.comboBox_GroupLis.SelectedIndex == 0)
			{
				for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
				{
					this.dataGridView_DeviceList.Rows[i].Visible = true;
				}
				this.GetDeviceList(false);
				return;
			}
			if (this.comboBox_GroupLis.SelectedIndex == 1)
			{
				for (int j = 0; j < this.dataGridView_DeviceList.Rows.Count; j++)
				{
					if (this.dataGridView_DeviceList.Rows[j].Cells[1].Value.ToString().IndexOf('_') < 0)
					{
						this.dataGridView_DeviceList.Rows[j].Visible = true;
					}
					else
					{
						this.dataGridView_DeviceList.Rows[j].Visible = false;
						this.dataGridView_DeviceList.Rows[j].Cells[0].Value = false;
					}
				}
				this.GetDeviceList(false);
				return;
			}
			for (int k = 0; k < this.dataGridView_DeviceList.Rows.Count; k++)
			{
				if (this.dataGridView_DeviceList.Rows[k].Cells[1].Value.ToString().StartsWith(text + "_"))
				{
					this.dataGridView_DeviceList.Rows[k].Visible = true;
				}
				else
				{
					this.dataGridView_DeviceList.Rows[k].Visible = false;
					this.dataGridView_DeviceList.Rows[k].Cells[0].Value = false;
				}
			}
			this.GetDeviceList(false);
		}

		private void button_SelectGroup_Click(object sender, EventArgs e)
		{
			string text = this.comboBox_GroupLis.Text;
			for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
			{
				if (this.dataGridView_DeviceList.Rows[i].Cells[1].Value.ToString().StartsWith(text + "_"))
				{
					this.dataGridView_DeviceList.Rows[i].Cells[0].Value = true;
				}
				else
				{
					this.dataGridView_DeviceList.Rows[i].Cells[0].Value = false;
				}
			}
		}

		private void button_SendAll_Click(object sender, EventArgs e)
		{
			IList<GprsContentInfo> sendList = this.getSendList();
			if (sendList.Count == 0)
			{
				MessageBox.Show(formMain.ML.GetStr("GPRS_Message_Select_Device"));
				return;
			}
			formGprsSendAll formGprsSendAll = new formGprsSendAll(this.getSendList(), this);
			formGprsSendAll.ShowDialog(this);
		}

		private IList<GprsContentInfo> getSendList()
		{
			IList<GprsContentInfo> list = new List<GprsContentInfo>();
			for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
			{
				if (this.dataGridView_DeviceList.Rows[i].Visible)
				{
					object value = this.dataGridView_DeviceList.Rows[i].Cells[0].Value;
					if (value != null && value.ToString() == "True")
					{
						GprsContentInfo gprsContentInfo = new GprsContentInfo();
						list.Add(gprsContentInfo);
						gprsContentInfo.Id = this.dataGridView_DeviceList.Rows[i].Cells[this.IDIndex].Value.ToString();
						gprsContentInfo.Height = int.Parse(this.dataGridView_DeviceList.Rows[i].Cells[8].Value.ToString());
						gprsContentInfo.Width = int.Parse(this.dataGridView_DeviceList.Rows[i].Cells[7].Value.ToString());
						gprsContentInfo.LedModel = this.dataGridView_DeviceList.Rows[i].Cells[6].Value.ToString();
						gprsContentInfo.TerminalCode = this.dataGridView_DeviceList.Rows[i].Cells[10].Value.ToString();
						gprsContentInfo.Description = this.dataGridView_DeviceList.Rows[i].Cells[1].Value.ToString();
					}
				}
			}
			return list;
		}

		private void button2_Click_1(object sender, EventArgs e)
		{
			byte[] array = new byte[2048000];
			Random random = new Random(255);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)random.Next();
			}
			MessageBox.Show(HttpUtility.UrlEncode(Convert.ToBase64String(array).Length.ToString()));
		}

		private void btnDeleteGroup_Click(object sender, EventArgs e)
		{
			if (this.comboBox_GroupLis.SelectedIndex == -1)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Please_Select_Group"));
				return;
			}
			string text = this.comboBox_GroupLis.Text;
			if (formMain.ML.GetStr("GPRS_Group_All") == text || formMain.ML.GetStr("GPRS_Group_UnGroup") == text)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Cannot_Delete_The_Group"));
				return;
			}
			if (MessageBox.Show(this, formMain.ML.GetStr("Message_Delete_The_Group_Will_Regroup_Devices_Into_Ungroup_Continue_Or_Not"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
			{
				return;
			}
			formCheckCode formCheckCode = new formCheckCode();
			if (!formCheckCode.Check(GprsAdministrator.Entity_Password, false))
			{
				return;
			}
			new Thread(new ThreadStart(this.DeleteGroup))
			{
				IsBackground = true
			}.Start();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			if (this.cmbSearchType.SelectedIndex == -1)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Please_Select_Search_Type"));
				return;
			}
			if (string.IsNullOrEmpty(this.txtSearchKey.Text))
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Please_Input_Search_key"));
				return;
			}
			string text = this.txtSearchKey.Text;
			for (int i = this.dataGridView_DeviceList.RowCount - 1; i > -1; i--)
			{
				DataGridViewRow dataGridViewRow = this.dataGridView_DeviceList.Rows[i];
				string text2 = dataGridViewRow.Cells[1].Value.ToString();
				if (this.cmbSearchType.SelectedIndex == 1)
				{
					text2 = dataGridViewRow.Cells[10].Value.ToString();
				}
				if (text2.Contains(text))
				{
					dataGridViewRow.Visible = true;
				}
				else
				{
					dataGridViewRow.Visible = false;
				}
			}
		}

		private void DeleteGroup()
		{
			base.Invoke(new MethodInvoker(delegate
			{
				this.label_Waitting_Picture.Text = formMain.ML.GetStr("Prompt_NowIsDeletingGroup");
				this.SetWaittingPanelVisable(true);
			}));
			Thread.Sleep(500);
			int groupIndex = this.comboBox_GroupLis.SelectedIndex;
			string text = this.comboBox_GroupLis.Text;
			string text2 = string.Format("{0}_", text);
			for (int i = 0; i < this.dataGridView_DeviceList.RowCount; i++)
			{
				DataGridViewRow dataGridViewRow = this.dataGridView_DeviceList.Rows[i];
				string text3 = dataGridViewRow.Cells[1].Value.ToString();
				string pEntityID = dataGridViewRow.Cells[9].Value.ToString();
				if (text3.StartsWith(text2))
				{
					formGPRS_Send.UpdateGPRSDescInfo(formMain.ledsys.SelectedPanel, pEntityID, text3.Substring(text2.Length), string.Empty);
				}
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.comboBox_GroupLis.SelectedIndex = 0;
				this.comboBox_GroupLis.Items.RemoveAt(groupIndex);
				this.SetWaittingPanelVisable(false);
			}));
			Thread.Sleep(500);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formGPRS_Send));
			this.menuStrip1 = new MenuStrip();
			this.FileToolStripMenuItem = new ToolStripMenuItem();
			this.changePasswordToolStripMenuItem = new ToolStripMenuItem();
			this.getUserDevieceListToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.ChangeAdvPassordToolStripMenuItem = new ToolStripMenuItem();
			this.listBox_Local = new ListBox();
			this.dataGridView_DeviceList = new DataGridView();
			this.Column_Check = new DataGridViewCheckBoxColumn();
			this.col_Description = new DataGridViewTextBoxColumn();
			this.col_Status = new DataGridViewTextBoxColumn();
			this.Col_SendPanel = new DataGridViewTextBoxColumn();
			this.Column_SendStatus = new DataGridViewTextBoxColumn();
			this.col_Signal_strength = new DataGridViewTextBoxColumn();
			this.col_Model = new DataGridViewTextBoxColumn();
			this.col_Width = new DataGridViewTextBoxColumn();
			this.col_Height = new DataGridViewTextBoxColumn();
			this.col_ID = new DataGridViewTextBoxColumn();
			this.col_DeviceCode = new DataGridViewTextBoxColumn();
			this.col_PhoneNumber = new DataGridViewTextBoxColumn();
			this.Col_IP = new DataGridViewTextBoxColumn();
			this.col_Routing = new DataGridViewTextBoxColumn();
			this.Column_LastSend = new DataGridViewTextBoxColumn();
			this.Column_DeviceVerion = new DataGridViewTextBoxColumn();
			this.Col_SingleCommand = new DataGridViewTextBoxColumn();
			this.groupBox_Local = new GroupBox();
			this.groupBox_Remoteto = new GroupBox();
			this.button_Send = new Button();
			this.button_ReloadDeviceList = new Button();
			this.button_Timing = new Button();
			this.button_Lumance = new Button();
			this.button_Start = new Button();
			this.button_Off = new Button();
			this.groupBox_DeviceControl = new GroupBox();
			this.button_WorkingStatus = new Button();
			this.button_Load = new Button();
			this.button_Auto = new Button();
			this.button1 = new Button();
			this.comboBox_SingleCommand = new ComboBox();
			this.button_BindNewDevice = new Button();
			this.button_ChangePassword = new Button();
			this.label1 = new Label();
			this.button_EditInfo = new Button();
			this.button_SaveRelationship = new Button();
			this.button_ChangeAdvancedPassword = new Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer_Single = new System.Windows.Forms.Timer(this.components);
			this.checkBox_SelectAll = new CheckBox();
			this.checkBox_Reverse = new CheckBox();
			this.comboBox_GroupLis = new ComboBox();
			this.label_Group = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.button_SelectGroup = new Button();
			this.button_SendAll = new Button();
			this.button2 = new Button();
			this.label_dataCRC = new Label();
			this.panel_Waitting = new Panel();
			this.label_Waitting_Picture = new Label();
			this.pictureBox2 = new PictureBox();
			this.btnDeleteGroup = new Button();
			this.cmbSearchType = new ComboBox();
			this.txtSearchKey = new TextBox();
			this.btnSearch = new Button();
			this.menuStrip1.SuspendLayout();
			((ISupportInitialize)this.dataGridView_DeviceList).BeginInit();
			this.groupBox_Local.SuspendLayout();
			this.groupBox_Remoteto.SuspendLayout();
			this.groupBox_DeviceControl.SuspendLayout();
			this.panel_Waitting.SuspendLayout();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			base.SuspendLayout();
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.FileToolStripMenuItem
			});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(948, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			this.FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.changePasswordToolStripMenuItem,
				this.getUserDevieceListToolStripMenuItem,
				this.toolStripMenuItem1,
				this.ChangeAdvPassordToolStripMenuItem
			});
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			this.FileToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.FileToolStripMenuItem.Text = "文件";
			this.FileToolStripMenuItem.Visible = false;
			this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
			this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.changePasswordToolStripMenuItem.Text = "更改密码";
			this.changePasswordToolStripMenuItem.Click += new EventHandler(this.changePasswordToolStripMenuItem_Click);
			this.getUserDevieceListToolStripMenuItem.Name = "getUserDevieceListToolStripMenuItem";
			this.getUserDevieceListToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.getUserDevieceListToolStripMenuItem.Text = "获取用户设备列表";
			this.getUserDevieceListToolStripMenuItem.Click += new EventHandler(this.getUserDevieceListToolStripMenuItem_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItem1.Text = "验证高级密码";
			this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
			this.ChangeAdvPassordToolStripMenuItem.Name = "ChangeAdvPassordToolStripMenuItem";
			this.ChangeAdvPassordToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.ChangeAdvPassordToolStripMenuItem.Text = "修改高级密码";
			this.ChangeAdvPassordToolStripMenuItem.Click += new EventHandler(this.ChangeAdvPassordToolStripMenuItem_Click);
			this.listBox_Local.FormattingEnabled = true;
			this.listBox_Local.ItemHeight = 12;
			this.listBox_Local.Location = new System.Drawing.Point(6, 20);
			this.listBox_Local.Name = "listBox_Local";
			this.listBox_Local.Size = new System.Drawing.Size(170, 352);
			this.listBox_Local.TabIndex = 1;
			this.listBox_Local.SelectedIndexChanged += new EventHandler(this.listBox_Local_SelectedIndexChanged);
			this.dataGridView_DeviceList.AllowUserToAddRows = false;
			this.dataGridView_DeviceList.AllowUserToDeleteRows = false;
			this.dataGridView_DeviceList.AllowUserToResizeRows = false;
			this.dataGridView_DeviceList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.dataGridView_DeviceList.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridView_DeviceList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_DeviceList.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column_Check,
				this.col_Description,
				this.col_Status,
				this.Col_SendPanel,
				this.Column_SendStatus,
				this.col_Signal_strength,
				this.col_Model,
				this.col_Width,
				this.col_Height,
				this.col_ID,
				this.col_DeviceCode,
				this.col_PhoneNumber,
				this.Col_IP,
				this.col_Routing,
				this.Column_LastSend,
				this.Column_DeviceVerion,
				this.Col_SingleCommand
			});
			this.dataGridView_DeviceList.Location = new System.Drawing.Point(6, 20);
			this.dataGridView_DeviceList.Name = "dataGridView_DeviceList";
			this.dataGridView_DeviceList.RowHeadersVisible = false;
			this.dataGridView_DeviceList.RowTemplate.Height = 23;
			this.dataGridView_DeviceList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_DeviceList.Size = new System.Drawing.Size(736, 354);
			this.dataGridView_DeviceList.TabIndex = 2;
			this.Column_Check.HeaderText = "选中";
			this.Column_Check.Name = "Column_Check";
			this.Column_Check.Width = 50;
			this.col_Description.HeaderText = "设备描述";
			this.col_Description.Name = "col_Description";
			this.col_Description.ReadOnly = true;
			this.col_Status.HeaderText = "状态";
			this.col_Status.Name = "col_Status";
			this.col_Status.ReadOnly = true;
			this.Col_SendPanel.HeaderText = "数据描述";
			this.Col_SendPanel.Name = "Col_SendPanel";
			this.Col_SendPanel.ReadOnly = true;
			this.Column_SendStatus.HeaderText = "发送状态";
			this.Column_SendStatus.Name = "Column_SendStatus";
			this.Column_SendStatus.ReadOnly = true;
			this.Column_SendStatus.Width = 130;
			this.col_Signal_strength.HeaderText = "信号强度";
			this.col_Signal_strength.Name = "col_Signal_strength";
			this.col_Signal_strength.Width = 80;
			this.col_Model.HeaderText = "卡型号";
			this.col_Model.Name = "col_Model";
			this.col_Model.ReadOnly = true;
			this.col_Width.DataPropertyName = "col_Width";
			this.col_Width.HeaderText = "宽";
			this.col_Width.Name = "col_Width";
			this.col_Width.ReadOnly = true;
			this.col_Height.HeaderText = "高";
			this.col_Height.Name = "col_Height";
			this.col_Height.ReadOnly = true;
			this.col_ID.HeaderText = "设备ID";
			this.col_ID.Name = "col_ID";
			this.col_ID.ReadOnly = true;
			this.col_ID.Visible = false;
			this.col_DeviceCode.HeaderText = "设备编号";
			this.col_DeviceCode.Name = "col_DeviceCode";
			this.col_DeviceCode.ReadOnly = true;
			this.col_DeviceCode.Width = 110;
			this.col_PhoneNumber.HeaderText = "电话号码";
			this.col_PhoneNumber.Name = "col_PhoneNumber";
			this.col_PhoneNumber.ReadOnly = true;
			this.col_PhoneNumber.Visible = false;
			this.Col_IP.HeaderText = "IP";
			this.Col_IP.Name = "Col_IP";
			this.Col_IP.ReadOnly = true;
			this.Col_IP.Visible = false;
			this.col_Routing.HeaderText = "走线类型";
			this.col_Routing.Name = "col_Routing";
			this.col_Routing.ReadOnly = true;
			this.col_Routing.Width = 120;
			this.Column_LastSend.HeaderText = "发送时间";
			this.Column_LastSend.Name = "Column_LastSend";
			this.Column_LastSend.ReadOnly = true;
			this.Column_LastSend.Width = 130;
			this.Column_DeviceVerion.HeaderText = "设备版本";
			this.Column_DeviceVerion.Name = "Column_DeviceVerion";
			this.Column_DeviceVerion.ReadOnly = true;
			this.Col_SingleCommand.HeaderText = "单条命令";
			this.Col_SingleCommand.Name = "Col_SingleCommand";
			this.Col_SingleCommand.ReadOnly = true;
			this.Col_SingleCommand.Visible = false;
			this.groupBox_Local.Controls.Add(this.listBox_Local);
			this.groupBox_Local.Location = new System.Drawing.Point(0, 27);
			this.groupBox_Local.Name = "groupBox_Local";
			this.groupBox_Local.Size = new System.Drawing.Size(182, 380);
			this.groupBox_Local.TabIndex = 3;
			this.groupBox_Local.TabStop = false;
			this.groupBox_Local.Text = "本地工程";
			this.groupBox_Remoteto.Controls.Add(this.dataGridView_DeviceList);
			this.groupBox_Remoteto.Location = new System.Drawing.Point(188, 27);
			this.groupBox_Remoteto.Name = "groupBox_Remoteto";
			this.groupBox_Remoteto.Size = new System.Drawing.Size(748, 380);
			this.groupBox_Remoteto.TabIndex = 4;
			this.groupBox_Remoteto.TabStop = false;
			this.groupBox_Remoteto.Text = "服务器设备列表";
			this.button_Send.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.button_Send.Location = new System.Drawing.Point(867, 408);
			this.button_Send.Name = "button_Send";
			this.button_Send.Size = new System.Drawing.Size(75, 51);
			this.button_Send.TabIndex = 5;
			this.button_Send.Text = "发送数据";
			this.button_Send.UseVisualStyleBackColor = false;
			this.button_Send.Click += new EventHandler(this.button_Send_Click);
			this.button_ReloadDeviceList.Location = new System.Drawing.Point(404, 410);
			this.button_ReloadDeviceList.Name = "button_ReloadDeviceList";
			this.button_ReloadDeviceList.Size = new System.Drawing.Size(70, 23);
			this.button_ReloadDeviceList.TabIndex = 7;
			this.button_ReloadDeviceList.Text = "刷新";
			this.button_ReloadDeviceList.UseVisualStyleBackColor = true;
			this.button_ReloadDeviceList.Click += new EventHandler(this.button_ReloadDeviceList_Click);
			this.button_Timing.Location = new System.Drawing.Point(254, 13);
			this.button_Timing.Name = "button_Timing";
			this.button_Timing.Size = new System.Drawing.Size(75, 23);
			this.button_Timing.TabIndex = 8;
			this.button_Timing.Text = "校时";
			this.button_Timing.UseVisualStyleBackColor = true;
			this.button_Timing.Click += new EventHandler(this.button_Timing_Click);
			this.button_Lumance.Location = new System.Drawing.Point(254, 44);
			this.button_Lumance.Name = "button_Lumance";
			this.button_Lumance.Size = new System.Drawing.Size(75, 23);
			this.button_Lumance.TabIndex = 9;
			this.button_Lumance.Text = "亮度";
			this.button_Lumance.UseVisualStyleBackColor = true;
			this.button_Lumance.Click += new EventHandler(this.button_Lumance_Click);
			this.button_Start.Location = new System.Drawing.Point(63, 13);
			this.button_Start.Name = "button_Start";
			this.button_Start.Size = new System.Drawing.Size(75, 23);
			this.button_Start.TabIndex = 10;
			this.button_Start.Text = "开机";
			this.button_Start.UseVisualStyleBackColor = true;
			this.button_Start.Click += new EventHandler(this.button_Start_Click);
			this.button_Off.Location = new System.Drawing.Point(161, 13);
			this.button_Off.Name = "button_Off";
			this.button_Off.Size = new System.Drawing.Size(75, 23);
			this.button_Off.TabIndex = 11;
			this.button_Off.Text = "关机";
			this.button_Off.UseVisualStyleBackColor = true;
			this.button_Off.Click += new EventHandler(this.button_Off_Click);
			this.groupBox_DeviceControl.Controls.Add(this.button_WorkingStatus);
			this.groupBox_DeviceControl.Controls.Add(this.button_Load);
			this.groupBox_DeviceControl.Controls.Add(this.button_Auto);
			this.groupBox_DeviceControl.Controls.Add(this.button_Timing);
			this.groupBox_DeviceControl.Controls.Add(this.button_Off);
			this.groupBox_DeviceControl.Controls.Add(this.button_Lumance);
			this.groupBox_DeviceControl.Controls.Add(this.button_Start);
			this.groupBox_DeviceControl.Location = new System.Drawing.Point(591, 491);
			this.groupBox_DeviceControl.Name = "groupBox_DeviceControl";
			this.groupBox_DeviceControl.Size = new System.Drawing.Size(61, 11);
			this.groupBox_DeviceControl.TabIndex = 12;
			this.groupBox_DeviceControl.TabStop = false;
			this.groupBox_DeviceControl.Text = "设备控制";
			this.groupBox_DeviceControl.Visible = false;
			this.button_WorkingStatus.Location = new System.Drawing.Point(345, 13);
			this.button_WorkingStatus.Name = "button_WorkingStatus";
			this.button_WorkingStatus.Size = new System.Drawing.Size(75, 23);
			this.button_WorkingStatus.TabIndex = 14;
			this.button_WorkingStatus.Text = "工作状态";
			this.button_WorkingStatus.UseVisualStyleBackColor = true;
			this.button_WorkingStatus.Click += new EventHandler(this.button_WorkingStatus_Click);
			this.button_Load.Location = new System.Drawing.Point(63, 44);
			this.button_Load.Name = "button_Load";
			this.button_Load.Size = new System.Drawing.Size(75, 23);
			this.button_Load.TabIndex = 13;
			this.button_Load.Text = "加载";
			this.button_Load.UseVisualStyleBackColor = true;
			this.button_Load.Click += new EventHandler(this.button_Load_Click);
			this.button_Auto.Location = new System.Drawing.Point(161, 44);
			this.button_Auto.Name = "button_Auto";
			this.button_Auto.Size = new System.Drawing.Size(75, 23);
			this.button_Auto.TabIndex = 12;
			this.button_Auto.Text = "定时开关机";
			this.button_Auto.UseVisualStyleBackColor = true;
			this.button_Auto.Click += new EventHandler(this.button_Auto_Click);
			this.button1.Location = new System.Drawing.Point(678, 410);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(102, 23);
			this.button1.TabIndex = 16;
			this.button1.Text = "发送命令";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click_1);
			this.comboBox_SingleCommand.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_SingleCommand.FormattingEnabled = true;
			this.comboBox_SingleCommand.Items.AddRange(new object[]
			{
				"开机",
				"关机",
				"校时",
				"加载",
				"亮度调整",
				"定时开关机",
				"检测工作状态"
			});
			this.comboBox_SingleCommand.Location = new System.Drawing.Point(570, 411);
			this.comboBox_SingleCommand.Name = "comboBox_SingleCommand";
			this.comboBox_SingleCommand.Size = new System.Drawing.Size(102, 20);
			this.comboBox_SingleCommand.TabIndex = 15;
			this.comboBox_SingleCommand.SelectedIndexChanged += new EventHandler(this.comboBox_SingleCommand_SelectedIndexChanged);
			this.button_BindNewDevice.Location = new System.Drawing.Point(111, 410);
			this.button_BindNewDevice.Name = "button_BindNewDevice";
			this.button_BindNewDevice.Size = new System.Drawing.Size(79, 23);
			this.button_BindNewDevice.TabIndex = 13;
			this.button_BindNewDevice.Text = "添加设备";
			this.button_BindNewDevice.UseVisualStyleBackColor = true;
			this.button_BindNewDevice.Click += new EventHandler(this.button_BindNewDevice_Click);
			this.button_ChangePassword.Location = new System.Drawing.Point(6, 410);
			this.button_ChangePassword.Name = "button_ChangePassword";
			this.button_ChangePassword.Size = new System.Drawing.Size(99, 23);
			this.button_ChangePassword.TabIndex = 15;
			this.button_ChangePassword.Text = "修改用户密码";
			this.button_ChangePassword.UseVisualStyleBackColor = true;
			this.button_ChangePassword.Click += new EventHandler(this.button_ChangePassword_Click);
			this.label1.Location = new System.Drawing.Point(469, 411);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 23);
			this.label1.TabIndex = 17;
			this.label1.Text = "单条控制命令";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button_EditInfo.Location = new System.Drawing.Point(678, 436);
			this.button_EditInfo.Name = "button_EditInfo";
			this.button_EditInfo.Size = new System.Drawing.Size(102, 23);
			this.button_EditInfo.TabIndex = 18;
			this.button_EditInfo.Text = "编辑设备资料";
			this.button_EditInfo.UseVisualStyleBackColor = true;
			this.button_EditInfo.Click += new EventHandler(this.button_EditInfo_Click);
			this.button_SaveRelationship.Location = new System.Drawing.Point(328, 410);
			this.button_SaveRelationship.Name = "button_SaveRelationship";
			this.button_SaveRelationship.Size = new System.Drawing.Size(70, 23);
			this.button_SaveRelationship.TabIndex = 19;
			this.button_SaveRelationship.Text = "保存关联";
			this.button_SaveRelationship.UseVisualStyleBackColor = true;
			this.button_SaveRelationship.Click += new EventHandler(this.button_SaveRelationship_Click);
			this.button_ChangeAdvancedPassword.Location = new System.Drawing.Point(6, 439);
			this.button_ChangeAdvancedPassword.Name = "button_ChangeAdvancedPassword";
			this.button_ChangeAdvancedPassword.Size = new System.Drawing.Size(99, 23);
			this.button_ChangeAdvancedPassword.TabIndex = 20;
			this.button_ChangeAdvancedPassword.Text = "修改高级密码";
			this.button_ChangeAdvancedPassword.UseVisualStyleBackColor = true;
			this.button_ChangeAdvancedPassword.Visible = false;
			this.button_ChangeAdvancedPassword.Click += new EventHandler(this.button_ChangeAdvancedPassword_Click);
			this.timer1.Interval = 10000;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.timer_Single.Interval = 5000;
			this.timer_Single.Tick += new EventHandler(this.timer_Single_Tick);
			this.checkBox_SelectAll.AutoSize = true;
			this.checkBox_SelectAll.Location = new System.Drawing.Point(204, 414);
			this.checkBox_SelectAll.Name = "checkBox_SelectAll";
			this.checkBox_SelectAll.Size = new System.Drawing.Size(48, 16);
			this.checkBox_SelectAll.TabIndex = 21;
			this.checkBox_SelectAll.Text = "全选";
			this.checkBox_SelectAll.UseVisualStyleBackColor = true;
			this.checkBox_SelectAll.CheckedChanged += new EventHandler(this.checkBox_SelectAll_CheckedChanged);
			this.checkBox_Reverse.AutoSize = true;
			this.checkBox_Reverse.Location = new System.Drawing.Point(268, 414);
			this.checkBox_Reverse.Name = "checkBox_Reverse";
			this.checkBox_Reverse.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Reverse.TabIndex = 22;
			this.checkBox_Reverse.Text = "反选";
			this.checkBox_Reverse.UseVisualStyleBackColor = true;
			this.checkBox_Reverse.CheckedChanged += new EventHandler(this.checkBox_Reverse_CheckedChanged);
			this.comboBox_GroupLis.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_GroupLis.FormattingEnabled = true;
			this.comboBox_GroupLis.Location = new System.Drawing.Point(249, 4);
			this.comboBox_GroupLis.Name = "comboBox_GroupLis";
			this.comboBox_GroupLis.Size = new System.Drawing.Size(121, 20);
			this.comboBox_GroupLis.TabIndex = 23;
			this.comboBox_GroupLis.SelectedIndexChanged += new EventHandler(this.comboBox_GroupLis_SelectedIndexChanged);
			this.label_Group.AutoSize = true;
			this.label_Group.Location = new System.Drawing.Point(196, 9);
			this.label_Group.Name = "label_Group";
			this.label_Group.Size = new System.Drawing.Size(29, 12);
			this.label_Group.TabIndex = 24;
			this.label_Group.Text = "分组";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(211, 444);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 12);
			this.label2.TabIndex = 26;
			this.label2.Text = "在线/总数";
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(311, 444);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 12);
			this.label3.TabIndex = 27;
			this.label3.Text = "在线/总数";
			this.button_SelectGroup.Location = new System.Drawing.Point(378, 1);
			this.button_SelectGroup.Name = "button_SelectGroup";
			this.button_SelectGroup.Size = new System.Drawing.Size(75, 23);
			this.button_SelectGroup.TabIndex = 25;
			this.button_SelectGroup.Text = "选中分组";
			this.button_SelectGroup.UseVisualStyleBackColor = true;
			this.button_SelectGroup.Visible = false;
			this.button_SelectGroup.Click += new EventHandler(this.button_SelectGroup_Click);
			this.button_SendAll.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.button_SendAll.Location = new System.Drawing.Point(786, 407);
			this.button_SendAll.Name = "button_SendAll";
			this.button_SendAll.Size = new System.Drawing.Size(75, 52);
			this.button_SendAll.TabIndex = 28;
			this.button_SendAll.Text = "群组发送";
			this.button_SendAll.UseVisualStyleBackColor = false;
			this.button_SendAll.Click += new EventHandler(this.button_SendAll_Click);
			this.button2.Location = new System.Drawing.Point(570, 439);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 29;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Visible = false;
			this.button2.Click += new EventHandler(this.button2_Click_1);
			this.label_dataCRC.AutoSize = true;
			this.label_dataCRC.Location = new System.Drawing.Point(402, 444);
			this.label_dataCRC.Name = "label_dataCRC";
			this.label_dataCRC.Size = new System.Drawing.Size(53, 12);
			this.label_dataCRC.TabIndex = 30;
			this.label_dataCRC.Text = "数据校验";
			this.label_dataCRC.Visible = false;
			this.panel_Waitting.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel_Waitting.Controls.Add(this.label_Waitting_Picture);
			this.panel_Waitting.Controls.Add(this.pictureBox2);
			this.panel_Waitting.Location = new System.Drawing.Point(290, 200);
			this.panel_Waitting.Name = "panel_Waitting";
			this.panel_Waitting.Size = new System.Drawing.Size(368, 64);
			this.panel_Waitting.TabIndex = 31;
			this.label_Waitting_Picture.ForeColor = System.Drawing.Color.White;
			this.label_Waitting_Picture.Location = new System.Drawing.Point(71, 3);
			this.label_Waitting_Picture.Name = "label_Waitting_Picture";
			this.label_Waitting_Picture.Size = new System.Drawing.Size(294, 58);
			this.label_Waitting_Picture.TabIndex = 1;
			this.label_Waitting_Picture.Text = "正在生成数据.....";
			this.label_Waitting_Picture.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.pictureBox2.Image = (System.Drawing.Image)componentResourceManager.GetObject("pictureBox2.Image");
			this.pictureBox2.Location = new System.Drawing.Point(3, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(63, 58);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			this.btnDeleteGroup.Location = new System.Drawing.Point(378, 2);
			this.btnDeleteGroup.Name = "btnDeleteGroup";
			this.btnDeleteGroup.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteGroup.TabIndex = 32;
			this.btnDeleteGroup.Text = "删除分组";
			this.btnDeleteGroup.UseVisualStyleBackColor = true;
			this.btnDeleteGroup.Click += new EventHandler(this.btnDeleteGroup_Click);
			this.cmbSearchType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbSearchType.FormattingEnabled = true;
			this.cmbSearchType.Items.AddRange(new object[]
			{
				"描述",
				"编号"
			});
			this.cmbSearchType.Location = new System.Drawing.Point(718, 6);
			this.cmbSearchType.Name = "cmbSearchType";
			this.cmbSearchType.Size = new System.Drawing.Size(50, 20);
			this.cmbSearchType.TabIndex = 33;
			this.txtSearchKey.Location = new System.Drawing.Point(774, 6);
			this.txtSearchKey.Name = "txtSearchKey";
			this.txtSearchKey.Size = new System.Drawing.Size(124, 21);
			this.txtSearchKey.TabIndex = 34;
			this.btnSearch.Image = Resources.Find_Device;
			this.btnSearch.Location = new System.Drawing.Point(904, 4);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(32, 23);
			this.btnSearch.TabIndex = 35;
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(948, 465);
			base.Controls.Add(this.btnSearch);
			base.Controls.Add(this.txtSearchKey);
			base.Controls.Add(this.cmbSearchType);
			base.Controls.Add(this.btnDeleteGroup);
			base.Controls.Add(this.panel_Waitting);
			base.Controls.Add(this.label_dataCRC);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button_SendAll);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.button_SelectGroup);
			base.Controls.Add(this.label_Group);
			base.Controls.Add(this.comboBox_GroupLis);
			base.Controls.Add(this.checkBox_Reverse);
			base.Controls.Add(this.checkBox_SelectAll);
			base.Controls.Add(this.button_ChangeAdvancedPassword);
			base.Controls.Add(this.button_SaveRelationship);
			base.Controls.Add(this.button_EditInfo);
			base.Controls.Add(this.button_ChangePassword);
			base.Controls.Add(this.button_BindNewDevice);
			base.Controls.Add(this.groupBox_DeviceControl);
			base.Controls.Add(this.button_ReloadDeviceList);
			base.Controls.Add(this.button_Send);
			base.Controls.Add(this.groupBox_Remoteto);
			base.Controls.Add(this.groupBox_Local);
			base.Controls.Add(this.menuStrip1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.comboBox_SingleCommand);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "formGPRS_Send";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "GPRS发送";
			base.FormClosing += new FormClosingEventHandler(this.formGPRS_Send_FormClosing);
			base.Load += new EventHandler(this.formGPRS_Send_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((ISupportInitialize)this.dataGridView_DeviceList).EndInit();
			this.groupBox_Local.ResumeLayout(false);
			this.groupBox_Remoteto.ResumeLayout(false);
			this.groupBox_DeviceControl.ResumeLayout(false);
			this.panel_Waitting.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox2).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
