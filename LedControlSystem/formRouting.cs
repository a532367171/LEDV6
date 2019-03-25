using LedCommunication;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Data;
using LedModel.Enum;
using LedModel.Foundation;
using LedResources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem.LedControlSystem
{
	public class formRouting : Form
	{
		private static string formID = "formRouting";

		private formMain fm_Main;

		private LedPanel thispanel;

		private LedRoutingSetting routingSetting;

		private bool isLoad;

		private string xml_index_scans_Prefix = "Scan";

		private string language_routing_Prefix = "ScanTypeList";

		private byte[] dispppearTimeList = new byte[]
		{
			0,
			64,
			128,
			192,
			255
		};

		private int[] refreshScreen = new int[]
		{
			255,
			224,
			196,
			128,
			64,
			32,
			0
		};

		private IList<string> UdiskDirList;

		private int thispanel_Heigth;

		private int thispanel_width;

		private bool isLoaded;

		private IContainer components;

		private CheckBox manualSettingCheckBox;

		private ComboBox routingSettingComboBox;

		private PictureBox pictureBox1;

		private Label label8selectRouting;

		private ComboBox bigLineTurnNumComboBox;

		private Label label7selectRouting;

		private ComboBox bigLineColumsComboBox;

		private Label label6selectRouting;

		private ComboBox bigLineRoutingComboBox;

		private Label label5selectRouting;

		private ComboBox smallLineTurnNumComboBox;

		private Label label4selectRouting;

		private ComboBox smallLineColumsComboBox;

		private Label label3selectRouting;

		private ComboBox smallLinesmallLineRoutingComboBox;

		private Label label2selectRouting;

		private ComboBox dataRowsComboBox;

		private Label label1selectRouting;

		private ComboBox scanTypeComboBox;

		private Label label12selectRouting;

		private ComboBox ltPolarityComboBox;

		private Label label11selectRouting;

		private ComboBox ckPolarityComboBox;

		private Label label10selectRouting;

		private ComboBox dataPolarityComboBox;

		private Label label9selectRouting;

		private ComboBox oePolarityComboBox;

		private Label label13selectRouting;

		private ComboBox comboBox1;

		private Label selectRoutinglabel15;

		private ComboBox rowOrderPolarityComboBox;

		private Label selectRoutinglabel14;

		private ComboBox abcdPolarityComboBox;

		private Label selectRoutinglabel17;

		private ComboBox routingScanRateComboBox;

		private Label selectRoutinglabel16;

		private ComboBox routingDispTimeComboBox;

		private RadioButton radioButtonScan6;

		private RadioButton radioButtonScan4;

		private RadioButton radioButtonScan2;

		private RadioButton radioButtonScanStatic;

		private RadioButton radioButtonScan16;

		private RadioButton radioButtonScan8;

		private RadioButton radioButtonScan5;

		private RadioButton radioButtonScan3;

		private CheckBox checkBox_RGConnect;

		private GroupBox routingParamGroupBox;

		private Label label2;

		private ComboBox routingLtComboBox;

		private Label label1;

		private ComboBox routingRefreshScreenComboBox;

		private GroupBox selectRoutingLabel1;

		private TabControl tabControl_ScanRouting;

		private TabPage tabPage_ToSelectScanRouting;

		private TabPage tabPage_Scannings;

		private GroupBox groupBoxSend;

		private Label label_communication;

		private Label LblScan;

		private Button button_StopScan;

		private Button button_StartScan;

		private Label LblComm;

		private GroupBox groupBox_UsbSave;

		private Label label_SelectFlash;

		private ComboBox UsbListComboBox;

		private Label label_Remind;

		private Label SelUdiskLabel;

		private Button UsbSaveButton;

		private CheckBox checkBox_Reverse;

		private CheckBox checkBox_SelectAll;

		private DataGridView GroupsOfScanRouting;

		private DataGridViewCheckBoxColumn IsChecked;

		private DataGridViewTextBoxColumn Index;

		private DataGridViewTextBoxColumn ScanRouting;

		private GroupBox groupBoxScans;

		private RadioButton radioButtonScans6;

		private RadioButton radioButtonScans5;

		private RadioButton radioButtonScans3;

		private RadioButton radioButtonScansStatic;

		private RadioButton radioButtonScans16;

		private RadioButton radioButtonScans8;

		private RadioButton radioButtonScans4;

		private RadioButton radioButtonScans2;

		private System.Windows.Forms.Timer timer1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem toolStripMenuItem_SelectRouting;

		private ToolStripMenuItem toolStripMenuItem_Cancel;

		private RadioButton radioButtonAllScans;

		private DataGridViewTextBoxColumn ColumnScanType;

		private DataGridViewTextBoxColumn ColumnIndex;

		private Button button_continue;

		private Button button_pause;

		private Button button_nextScan;

		private Button button_lastScan;

		private Button buttonLoad;

		private GroupBox groupBox2;

		private RadioButton radioButtonScan10;

		private RadioButton radioButtonScans10;

		public static string FormID
		{
			get
			{
				return formRouting.formID;
			}
			set
			{
				formRouting.formID = value;
			}
		}

		public formRouting()
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formRouting.formID;
			this.Text = formMain.ML.GetStr("formRouting_FormText");
			this.radioButtonScanStatic.Text = formMain.ML.GetStr("formRouting_radioButton_ScanStatic");
			this.radioButtonScan2.Text = formMain.ML.GetStr("formRouting_radioButton_Scan2");
			this.radioButtonScan4.Text = formMain.ML.GetStr("formRouting_radioButton_Scan4");
			this.radioButtonScan6.Text = formMain.ML.GetStr("formRouting_radioButton_Scan6");
			this.radioButtonScan8.Text = formMain.ML.GetStr("formRouting_radioButton_Scan8");
			this.radioButtonScan10.Text = formMain.ML.GetStr("formRouting_radioButton_Scan10");
			this.radioButtonScan16.Text = formMain.ML.GetStr("formRouting_radioButton_Scan16");
			this.radioButtonScan3.Text = formMain.ML.GetStr("formRouting_radioButtonScan3");
			this.radioButtonScan5.Text = formMain.ML.GetStr("formRouting_radioButton_Scan5");
			this.manualSettingCheckBox.Text = formMain.ML.GetStr("formRouting_CheckBox_manualSetting");
			this.selectRoutinglabel16.Text = formMain.ML.GetStr("formRouting_label_ScanningFrequency");
			this.selectRoutinglabel17.Text = formMain.ML.GetStr("formRouting_label_DisappearanceTime");
			this.checkBox_RGConnect.Text = formMain.ML.GetStr("formRouting_checkBox_RGConnect");
			this.selectRoutingLabel1.Text = formMain.ML.GetStr("formRouting_selectRoutingLabel1");
			this.label1.Text = formMain.ML.GetStr("formRouting_label_RefreshRate");
			this.groupBoxScans.Text = formMain.ML.GetStr("formpaneledit_label_ScanningMethod");
			this.radioButtonScansStatic.Text = formMain.ML.GetStr("formRouting_Scannings_RadioButton_Static");
			this.radioButtonAllScans.Text = formMain.ML.GetStr("formRouting_Scannings_RadioButton_ALL");
			this.checkBox_SelectAll.Text = formMain.ML.GetStr("formRouting_Scannings_CheckBox_ALL");
			this.checkBox_Reverse.Text = formMain.ML.GetStr("formRouting_Scannings_CheckBox_opposite");
			this.buttonLoad.Text = formMain.ML.GetStr("formRouting_Scannings_BtnLoad");
			this.groupBoxSend.Text = formMain.ML.GetStr("Display_SendData");
			this.LblComm.Text = formMain.ML.GetStr("formRouting_Scannings_Label_CommMode");
			this.LblScan.Text = formMain.ML.GetStr("formRouting_Scannings_Label_ScanOperation");
			this.button_StartScan.Text = formMain.ML.GetStr("formRouting_Scannings_Btn_StartScanning");
			this.button_pause.Text = formMain.ML.GetStr("formRouting_Scannings_Btn_TimeOut");
			this.button_continue.Text = formMain.ML.GetStr("formRouting_Scannings_Btn_CarryOn");
			this.button_lastScan.Text = formMain.ML.GetStr("formRouting_Scannings_Btn_Previous");
			this.button_nextScan.Text = formMain.ML.GetStr("formRouting_Scannings_Btn_Next");
			this.button_StopScan.Text = formMain.ML.GetStr("formRouting_Scannings_Btn_EndScan");
			this.groupBox_UsbSave.Text = formMain.ML.GetStr("formpaneledit_radioButtonUSB");
			this.SelUdiskLabel.Text = formMain.ML.GetStr("formUSBWrite_label_SelUdisk");
			this.UsbSaveButton.Text = formMain.ML.GetStr("formUSBWrite_ButtonUsbSave");
			this.GroupsOfScanRouting.Columns[0].HeaderText = formMain.ML.GetStr("formRouting_Scannings_GroupsOfScanRouting_check");
			this.GroupsOfScanRouting.Columns[1].HeaderText = formMain.ML.GetStr("formRouting_Scannings_GroupsOfScanRouting_number");
			this.GroupsOfScanRouting.Columns[2].HeaderText = formMain.ML.GetStr("formRouting_Scannings_GroupsOfScanRouting_routing");
			this.tabControl_ScanRouting.TabPages[0].Text = formMain.ML.GetStr("formRouting_Scannings_TabControl_Set");
			this.tabControl_ScanRouting.TabPages[1].Text = formMain.ML.GetStr("formRouting_Scannings_TabControl_Scan");
			formMain.str_item_comboBox(this.routingScanRateComboBox, "formRouting_routingScanRateComboBox", null);
			formMain.str_item_comboBox(this.routingDispTimeComboBox, "formRouting_routingDispTimeComboBox", null);
			formMain.str_item_comboBox(this.routingLtComboBox, "formRouting_routingLtComboBox", null);
			formMain.str_item_comboBox(this.routingRefreshScreenComboBox, "formRouting_routingRefreshScreenComboBox", null);
			base.Size = base.Size;
		}

		public void Edit(LedRoutingSetting pSetting, LedPanel pPanel, formMain fm_main)
		{
			this.fm_Main = fm_main;
			this.routingSetting = pSetting;
			this.thispanel = pPanel;
			this.thispanel_Heigth = this.thispanel.Height;
			this.thispanel_width = this.thispanel.Width;
			this.CommunicationDisplay();
			this.ShowPram();
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void CommunicationDisplay()
		{
			string text = "";
			if (this.thispanel != null)
			{
				if (this.thispanel.PortType == LedPortType.Ethernet)
				{
					bool flag = false;
					switch (this.thispanel.EthernetCommunicaitonMode)
					{
					case LedEthernetCommunicationMode.Directly:
						text = formMain.ML.GetStr("formpaneledit_radioButton_SendBroadcast");
						break;
					case LedEthernetCommunicationMode.FixedIP:
						text = formMain.ML.GetStr("formpaneledit_radioButton_SendByIP");
						break;
					case LedEthernetCommunicationMode.LocalServer:
						text = formMain.ML.GetStr("formpaneledit_radioButton_localServer");
						break;
					case LedEthernetCommunicationMode.CloudServer:
						text = formMain.ML.GetStr("formpaneledit_radioButton_RemoteServer");
						flag = true;
						break;
					}
					this.label_communication.Text = text;
					if (flag)
					{
						this.label_communication.ForeColor = System.Drawing.Color.Red;
						return;
					}
					this.label_communication.ForeColor = System.Drawing.Color.Green;
					return;
				}
				else
				{
					if (this.thispanel.PortType == LedPortType.SerialPort)
					{
						text = formMain.ML.GetStr("formpaneledit_label_SerialPortName");
						this.label_communication.Text = text + "     " + formMain.ML.GetStr("formpaneledit_label_Baudrate") + this.thispanel.BaudRate.ToString();
						this.label_communication.ForeColor = System.Drawing.Color.Green;
						return;
					}
					if (this.thispanel.PortType == LedPortType.GPRS || this.thispanel.PortType == LedPortType.USB || this.thispanel.PortType == LedPortType.WiFi)
					{
						text = this.thispanel.PortType.ToString();
						this.label_communication.Text = text;
						this.label_communication.ForeColor = System.Drawing.Color.Red;
					}
				}
			}
		}

		public static string GetRoutingString(int pScanType, int pRoutingIndex)
		{
			string str;
			string text;
			if (pScanType == 1)
			{
				str = "ScanTypeList1";
				text = "1";
			}
			else if (pScanType == 2)
			{
				str = "ScanTypeList2";
				text = "2";
			}
			else if (pScanType == 3)
			{
				str = "ScanTypeList3";
				text = "3";
			}
			else if (pScanType == 4)
			{
				str = "ScanTypeList4";
				text = "4";
			}
			else if (pScanType == 5)
			{
				str = "ScanTypeList5";
				text = "5";
			}
			else if (pScanType == 6)
			{
				str = "ScanTypeList6";
				text = "6";
			}
			else if (pScanType == 8)
			{
				str = "ScanTypeList8";
				text = "8";
			}
			else if (pScanType == 10)
			{
				str = "ScanTypeList10";
				text = "10";
			}
			else if (pScanType == 16)
			{
				str = "ScanTypeList16";
				text = "16";
			}
			else
			{
				str = "ScanTypeList32";
				text = "32";
			}
			if (MulitLanguageFresher.Language_Display.ContainsKey(str + "_" + pRoutingIndex.ToString()))
			{
				return formMain.ML.GetStr(str + "_" + pRoutingIndex.ToString());
			}
			return formMain.ML.GetStr(string.Concat(new string[]
			{
				MulitLanguageFresher.file_name_routingL,
				"_",
				text,
				"_",
				pRoutingIndex.ToString()
			}));
		}

		private void ShowPram()
		{
			this.isLoad = true;
			this.routingLtComboBox.SelectedIndex = (int)(this.thispanel.LTPolarity - 1);
			if (this.routingSetting.PresetType == LedRoutingType.None)
			{
				this.routingSettingComboBox.Enabled = false;
				this.routingParamGroupBox.Enabled = true;
				this.routingSettingComboBox.Text = formMain.ML.GetStr("Display_CustomeSetting");
				this.manualSettingCheckBox.Checked = true;
			}
			else
			{
				this.routingSettingComboBox.SelectedIndex = (int)this.routingSetting.PresetType;
				this.routingSettingComboBox.Enabled = true;
				this.routingParamGroupBox.Enabled = false;
				this.manualSettingCheckBox.Checked = false;
			}
			this.scanTypeComboBox.Text = "1/" + this.routingSetting.ScanType.ToString();
			this.dataRowsComboBox.Text = this.routingSetting.DataRows.ToString();
			this.oePolarityComboBox.SelectedIndex = (int)this.thispanel.OEPolarity;
			this.dataPolarityComboBox.SelectedIndex = (int)this.thispanel.DataPolarity;
			this.ckPolarityComboBox.SelectedIndex = (int)this.routingSetting.CKPolarity;
			this.abcdPolarityComboBox.SelectedIndex = (int)this.routingSetting.ABCDPolarity;
			this.rowOrderPolarityComboBox.SelectedIndex = (int)this.routingSetting.RowOrder;
			this.routingScanRateComboBox.SelectedIndex = this.thispanel.ScanFrequency;
			for (int i = 0; i < this.dispppearTimeList.Length; i++)
			{
				if ((int)this.dispppearTimeList[i] == this.thispanel.BlankingTime)
				{
					this.routingDispTimeComboBox.SelectedIndex = i;
				}
			}
			if (this.thispanel.RoutingSetting.ScanTypeIndex == 1)
			{
				this.radioButtonScanStatic.Checked = true;
			}
			else if (this.thispanel.RoutingSetting.ScanTypeIndex == 2)
			{
				this.radioButtonScan2.Checked = true;
			}
			else if (this.thispanel.RoutingSetting.ScanTypeIndex == 3)
			{
				this.radioButtonScan3.Checked = true;
			}
			else if (this.thispanel.RoutingSetting.ScanTypeIndex == 4)
			{
				this.radioButtonScan4.Checked = true;
			}
			else if (this.thispanel.RoutingSetting.ScanTypeIndex == 5)
			{
				this.radioButtonScan5.Checked = true;
			}
			else if (this.thispanel.RoutingSetting.ScanTypeIndex == 6)
			{
				this.radioButtonScan6.Checked = true;
			}
			else if (this.thispanel.RoutingSetting.ScanTypeIndex == 8)
			{
				this.radioButtonScan8.Checked = true;
			}
			else if (this.thispanel.RoutingSetting.ScanTypeIndex == 10)
			{
				this.radioButtonScan10.Checked = true;
			}
			else if (this.thispanel.RoutingSetting.ScanTypeIndex == 16)
			{
				this.radioButtonScan16.Checked = true;
			}
			else
			{
				this.radioButtonScanStatic.Checked = true;
			}
			this.routingSettingComboBox.Items.Clear();
			this.Get_Routing_comboBox(this.thispanel.RoutingSetting.ScanTypeIndex.ToString());
			this.thispanel.CardType.ToString();
			if (this.thispanel.RoutingSetting.RoutingIndex > this.routingSettingComboBox.Items.Count - 1)
			{
				this.routingSettingComboBox.SelectedIndex = this.routingSettingComboBox.Items.Count - 1;
			}
			else
			{
				this.routingSettingComboBox.SelectedIndex = this.thispanel.RoutingSetting.RoutingIndex;
			}
			this.checkBox_RGConnect.Checked = this.routingSetting.RGConnect;
			for (int j = 0; j < this.refreshScreen.Length; j++)
			{
				if (this.refreshScreen[j] == this.thispanel.RefreshFrequency)
				{
					this.routingRefreshScreenComboBox.SelectedIndex = j;
					this.isLoad = false;
					return;
				}
			}
			this.isLoad = false;
		}

		private void Get_Routing_comboBox(string xml_index)
		{
			try
			{
				if (this.thispanel.IsLSeries())
				{
					using (List<string>.Enumerator enumerator = MulitLanguageFresher.routing_xml_L[this.xml_index_scans_Prefix + "_" + xml_index].GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string current = enumerator.Current;
							if (MulitLanguageFresher.Language_Display.ContainsKey(this.language_routing_Prefix + xml_index + "_" + current))
							{
								this.routingSettingComboBox.Items.Add(formMain.ML.GetStr(this.language_routing_Prefix + xml_index + "_" + current));
							}
							else
							{
								this.routingSettingComboBox.Items.Add(formMain.ML.GetStr(string.Concat(new string[]
								{
									MulitLanguageFresher.file_name_routingL,
									"_",
									xml_index,
									"_",
									current
								})));
							}
						}
						goto IL_1D9;
					}
				}
				foreach (string current2 in MulitLanguageFresher.routing_xml[this.xml_index_scans_Prefix + "_" + xml_index])
				{
					if (MulitLanguageFresher.Language_Display.ContainsKey(this.language_routing_Prefix + xml_index + "_" + current2))
					{
						this.routingSettingComboBox.Items.Add(formMain.ML.GetStr(this.language_routing_Prefix + xml_index + "_" + current2));
					}
					else
					{
						this.routingSettingComboBox.Items.Add(formMain.ML.GetStr(string.Concat(new string[]
						{
							MulitLanguageFresher.file_name_routingL,
							"_",
							xml_index,
							"_",
							current2
						})));
					}
				}
				IL_1D9:;
			}
			catch
			{
			}
		}

		private string getAllRouting(string routingName, int length)
		{
			string text = "";
			for (int i = 0; i < length; i++)
			{
				text = text + formMain.ML.GetStr(routingName + "_" + i.ToString()) + "|";
			}
			return text;
		}

		private void routingSettingComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.isLoad)
			{
				return;
			}
			ComboBox comboBox = (ComboBox)sender;
			if (this.routingSettingComboBox.SelectedIndex == -1)
			{
				return;
			}
			this.thispanel.RoutingSetting.RoutingIndex = this.routingSettingComboBox.SelectedIndex;
			this.thispanel.RoutingSetting.TypeDescription = comboBox.Text;
			if (this.routingSetting.ScanTypeIndex == 4 && this.routingSetting.RoutingIndex == 3)
			{
				this.thispanel.OEPolarity = 0;
				this.thispanel.DataPolarity = 0;
			}
			if (this.routingSetting.ScanTypeIndex == 8 && this.routingSetting.RoutingIndex == 0)
			{
				this.thispanel.RoutingSetting.RGConnect = true;
				this.checkBox_RGConnect.Checked = true;
				return;
			}
			this.thispanel.RoutingSetting.RGConnect = false;
			this.checkBox_RGConnect.Checked = false;
		}

		private void manualSettingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.manualSettingCheckBox.Checked)
			{
				this.routingSetting.TypeDescription = formMain.ML.GetStr("Display_CustomeSetting");
				this.routingParamGroupBox.Enabled = true;
				this.ShowPram();
				formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
				return;
			}
			this.routingParamGroupBox.Enabled = false;
			this.routingSettingComboBox.Enabled = true;
		}

		private void formRouting_SizeChanged(object sender, EventArgs e)
		{
		}

		private void formRouting_Load(object sender, EventArgs e)
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
			this.routingLtComboBox.SelectedIndex = (int)(this.thispanel.LTPolarity - 1);
			this.tabControl_ScanRouting.Height = this.selectRoutingLabel1.Top + this.selectRoutingLabel1.Height + 45;
			base.Height = this.tabControl_ScanRouting.Top + this.tabControl_ScanRouting.Height + 40;
			this.timer1.Start();
		}

		private void formRouting_TextChanged(object sender, EventArgs e)
		{
			base.Text = this.Text;
		}

		private void routingParamGroupBox_Paint(object sender, PaintEventArgs e)
		{
		}

		private void routingScanRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.thispanel.ScanFrequency = comboBox.SelectedIndex;
		}

		private void routingDispTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.thispanel.BlankingTime = (int)this.dispppearTimeList[comboBox.SelectedIndex];
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.thispanel.RefreshFrequency = this.refreshScreen[comboBox.SelectedIndex];
		}

		private void radioButtonScab4_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.routingSettingComboBox.Items.Clear();
				this.Get_Routing_comboBox("4");
				this.routingSetting.ScanTypeIndex = 4;
				this.routingSetting.RoutingIndex = 0;
				this.routingSettingComboBox.SelectedIndex = 0;
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void radioButtonScan8_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.routingSettingComboBox.Items.Clear();
				this.Get_Routing_comboBox("8");
				this.routingSetting.ScanTypeIndex = 8;
				this.routingSetting.RoutingIndex = 0;
				this.routingSettingComboBox.SelectedIndex = 0;
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void radioButtonScan10_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.routingSettingComboBox.Items.Clear();
				this.Get_Routing_comboBox("10");
				this.routingSetting.ScanTypeIndex = 10;
				this.routingSetting.RoutingIndex = 0;
				this.routingSettingComboBox.SelectedIndex = 0;
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void radioButtonScan16_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.routingSettingComboBox.Items.Clear();
				this.Get_Routing_comboBox("16");
				this.routingSetting.ScanTypeIndex = 16;
				this.routingSetting.RoutingIndex = 0;
				this.routingSettingComboBox.SelectedIndex = 0;
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void comboBoxOE_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (comboBox.SelectedIndex == 1)
			{
				this.thispanel.OEPolarity = Convert.ToByte(true);
				return;
			}
			this.thispanel.OEPolarity = Convert.ToByte(false);
		}

		private void comboBoxData_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (comboBox.SelectedIndex == 1)
			{
				this.thispanel.DataPolarity = Convert.ToByte(true);
				return;
			}
			this.thispanel.DataPolarity = Convert.ToByte(false);
		}

		private void radioButtonScan6_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.routingSettingComboBox.Items.Clear();
				this.Get_Routing_comboBox("6");
				this.routingSetting.ScanTypeIndex = 6;
				this.routingSetting.RoutingIndex = 0;
				this.routingSettingComboBox.SelectedIndex = 0;
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void radioButtonScan3_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.routingSettingComboBox.Items.Clear();
				this.Get_Routing_comboBox("3");
				this.routingSetting.ScanTypeIndex = 3;
				this.routingSetting.RoutingIndex = 0;
				this.routingSettingComboBox.SelectedIndex = 0;
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void checkBox_RGConnect_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			this.routingSetting.RGConnect = checkBox.Checked;
		}

		private void routingLtComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.thispanel.LTPolarity = (byte)(comboBox.SelectedIndex + 1);
		}

		private void radioButtonScanStatic_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.routingSettingComboBox.Items.Clear();
				this.Get_Routing_comboBox("1");
				this.routingSetting.ScanTypeIndex = 1;
				this.routingSetting.RoutingIndex = 0;
				this.routingSettingComboBox.SelectedIndex = 0;
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void radioButtonScan5_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.routingSettingComboBox.Items.Clear();
				this.Get_Routing_comboBox("5");
				this.routingSetting.ScanTypeIndex = 5;
				this.routingSetting.RoutingIndex = 0;
				this.routingSettingComboBox.SelectedIndex = 0;
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void radioButtonScan2_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.isLoad)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.routingSettingComboBox.Items.Clear();
				this.Get_Routing_comboBox("2");
				this.routingSetting.ScanTypeIndex = 2;
				this.routingSetting.RoutingIndex = 0;
				this.routingSettingComboBox.SelectedIndex = 0;
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.routingSettingComboBox);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.updataUdiskList();
		}

		private bool IsNTFS(string pDisk)
		{
			DriveInfo driveInfo = new DriveInfo(pDisk);
			if (driveInfo.DriveFormat.ToUpper().IndexOf("FAT") >= 0)
			{
				this.label_Remind.Text = "";
				this.label_Remind.ForeColor = System.Drawing.Color.Black;
				if (driveInfo.DriveFormat.ToUpper() == "FAT32")
				{
					this.label_Remind.Text = "";
				}
				else
				{
					this.label_Remind.Text = formMain.ML.GetStr("USB_NotFAT32");
				}
				return true;
			}
			this.label_Remind.Text = formMain.ML.GetStr("USB_NotSupportFormat");
			this.label_Remind.ForeColor = System.Drawing.Color.Red;
			return false;
		}

		public void updataUdiskList()
		{
			int selectedIndex = this.UsbListComboBox.SelectedIndex;
			DriveInfo[] drives = DriveInfo.GetDrives();
			new List<string>();
			this.UdiskDirList = new List<string>();
			IList<string> list = new List<string>();
			int num = 0;
			for (int i = 0; i < drives.Length; i++)
			{
				if (drives[i].DriveType == DriveType.Removable)
				{
					try
					{
						string text = drives[i].VolumeLabel;
						if (text == "" || text == null)
						{
							text = formMain.ML.GetStr("Prompt_RemovableDisk");
						}
						list.Add(text + "(" + drives[i].Name.Remove(1) + ")");
						this.UdiskDirList.Add(drives[i].Name);
						num++;
					}
					catch
					{
					}
				}
			}
			if (list.Count != this.UsbListComboBox.Items.Count)
			{
				this.UsbListComboBox.Items.Clear();
				foreach (string current in list)
				{
					this.UsbListComboBox.Items.Add(current);
				}
			}
			if (this.UdiskDirList.Count >= selectedIndex + 1)
			{
				this.UsbListComboBox.SelectedIndex = selectedIndex;
			}
			else if (this.UdiskDirList.Count > 0)
			{
				this.UsbListComboBox.SelectedIndex = 0;
			}
			bool flag = false;
			if (this.UdiskDirList.Count > 1)
			{
				this.label_SelectFlash.Visible = true;
			}
			else
			{
				this.label_SelectFlash.Visible = false;
			}
			if (this.UdiskDirList.Count == 0)
			{
				this.UsbSaveButton.Enabled = false;
				this.label_Remind.Text = "";
			}
			else
			{
				flag = true;
			}
			if (this.UdiskDirList.Count == 1)
			{
				this.UsbListComboBox.SelectedIndex = 0;
			}
			if (flag)
			{
				if (this.UsbListComboBox.SelectedIndex == -1)
				{
					this.UsbListComboBox.SelectedIndex = 0;
				}
				this.UsbSaveButton.Enabled = this.IsNTFS(this.UdiskDirList[this.UsbListComboBox.SelectedIndex]);
			}
		}

		private void formScanRouting_Load(object sender, EventArgs e)
		{
			this.timer1.Start();
		}

		private void Get_Routing_dataGridview(string xml_index)
		{
			try
			{
				if (this.thispanel.IsLSeries())
				{
					using (List<string>.Enumerator enumerator = MulitLanguageFresher.routing_xml_L[this.xml_index_scans_Prefix + "_" + xml_index].GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string current = enumerator.Current;
							DataGridViewRow dataGridViewRow = new DataGridViewRow();
							dataGridViewRow.CreateCells(this.GroupsOfScanRouting);
							dataGridViewRow.Cells[1].Value = (this.GroupsOfScanRouting.Rows.Count + 1).ToString();
							if (MulitLanguageFresher.Language_Display.ContainsKey(this.language_routing_Prefix + xml_index + "_" + current))
							{
								dataGridViewRow.Cells[2].Value = formMain.ML.GetStr(this.language_routing_Prefix + xml_index + "_" + current);
							}
							else
							{
								dataGridViewRow.Cells[2].Value = formMain.ML.GetStr(string.Concat(new string[]
								{
									MulitLanguageFresher.file_name_routingL,
									"_",
									xml_index,
									"_",
									current
								}));
							}
							try
							{
								dataGridViewRow.Cells[3].Value = int.Parse(xml_index);
								dataGridViewRow.Cells[4].Value = int.Parse(current);
							}
							catch
							{
							}
							this.GroupsOfScanRouting.Rows.Add(dataGridViewRow);
						}
						goto IL_2FB;
					}
				}
				foreach (string current2 in MulitLanguageFresher.routing_xml[this.xml_index_scans_Prefix + "_" + xml_index])
				{
					DataGridViewRow dataGridViewRow2 = new DataGridViewRow();
					dataGridViewRow2.CreateCells(this.GroupsOfScanRouting);
					dataGridViewRow2.Cells[1].Value = (this.GroupsOfScanRouting.Rows.Count + 1).ToString();
					if (MulitLanguageFresher.Language_Display.ContainsKey(this.language_routing_Prefix + xml_index + "_" + current2))
					{
						dataGridViewRow2.Cells[2].Value = formMain.ML.GetStr(this.language_routing_Prefix + xml_index + "_" + current2);
					}
					else
					{
						dataGridViewRow2.Cells[2].Value = formMain.ML.GetStr(string.Concat(new string[]
						{
							MulitLanguageFresher.file_name_routingL,
							"_",
							xml_index,
							"_",
							current2
						}));
					}
					try
					{
						dataGridViewRow2.Cells[3].Value = int.Parse(xml_index);
						dataGridViewRow2.Cells[4].Value = int.Parse(current2);
					}
					catch
					{
					}
					this.GroupsOfScanRouting.Rows.Add(dataGridViewRow2);
				}
				IL_2FB:;
			}
			catch
			{
			}
		}

		private void SetDataGridView(string scans)
		{
			this.checkBox_SelectAll.Checked = false;
			this.checkBox_Reverse.Checked = false;
			this.GroupsOfScanRouting.Rows.Clear();
			this.Get_Routing_dataGridview(scans);
		}

		private void radioButtonScans2_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDataGridView("2");
		}

		private void radioButtonScans3_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDataGridView("3");
		}

		private void radioButtonScans4_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDataGridView("4");
		}

		private void radioButtonScans5_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDataGridView("5");
		}

		private void radioButtonScans6_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDataGridView("6");
		}

		private void radioButtonScans8_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDataGridView("8");
		}

		private void radioButtonScans10_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDataGridView("10");
		}

		private void radioButtonScans16_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDataGridView("16");
		}

		private void radioButtonScansStatic_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDataGridView("1");
		}

		private void radioButtonAllScans_CheckedChanged(object sender, EventArgs e)
		{
			this.checkBox_SelectAll.Checked = false;
			this.checkBox_Reverse.Checked = false;
			this.GroupsOfScanRouting.Rows.Clear();
			this.Get_Routing_dataGridview("1");
			this.Get_Routing_dataGridview("2");
			this.Get_Routing_dataGridview("3");
			this.Get_Routing_dataGridview("4");
			this.Get_Routing_dataGridview("5");
			this.Get_Routing_dataGridview("6");
			this.Get_Routing_dataGridview("8");
			this.Get_Routing_dataGridview("10");
			this.Get_Routing_dataGridview("16");
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
				this.SelectAll(this.GroupsOfScanRouting, true);
				return;
			}
			this.SelectAll(this.GroupsOfScanRouting, false);
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
			this.SelectReverse(this.GroupsOfScanRouting);
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
			try
			{
				for (int i = 0; i < pGrid.Rows.Count; i++)
				{
					if (pGrid.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
					{
						((DataGridViewCheckBoxCell)pGrid.Rows[i].Cells[0]).Value = false;
					}
					else
					{
						((DataGridViewCheckBoxCell)pGrid.Rows[i].Cells[0]).Value = true;
					}
				}
			}
			catch
			{
			}
		}

		private void UsbListComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void UsbSaveButton_Click(object sender, EventArgs e)
		{
			this.timer1.Stop();
			try
			{
				this.UsbSaveButton.Enabled = false;
				base.Enabled = false;
				ProcessUSB processUSB = new ProcessUSB();
				if (this.thispanel.IsLSeries())
				{
					processUSB.ScanTypeBytes = this.ScansData();
					if (processUSB.ScanTypeBytes.Count < 1)
					{
						MessageBox.Show(formMain.ML.GetStr("formRouting_Scannings_Message_UnSelectedScanTypes"));
						this.UsbSaveButton.Enabled = true;
						base.Enabled = true;
						return;
					}
				}
				this.label_Remind.Text = formMain.ML.GetStr("USB_SavingData");
				Thread.Sleep(500);
				this.label_Remind.ForeColor = System.Drawing.Color.Black;
				if (this.UsbListComboBox.SelectedIndex == -1)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_NoFlashDisk"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
					this.UsbSaveButton.Enabled = true;
					base.Enabled = true;
				}
				else
				{
					processUSB.ProtocolVersion = this.thispanel.ProtocolVersion;
					protocol_data_integration protocol_data_integration = new protocol_data_integration();
					byte[] array = protocol_data_integration.WritingData_USB_Pack(processUSB, false, false);
					if (array == null)
					{
						base.Enabled = true;
						this.label_Remind.ForeColor = System.Drawing.Color.Red;
						this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Failed");
						this.UsbSaveButton.Enabled = true;
						base.Enabled = true;
						Thread.Sleep(1000);
						this.timer1.Start();
					}
					else if (array.Length > this.thispanel.GetFlashCapacity())
					{
						MessageBox.Show(this, formMain.ML.GetStr("Prompt_MemeoryOverSize"));
						base.Enabled = true;
						this.UsbSaveButton.Enabled = true;
						this.label_Remind.Text = formMain.ML.GetStr("Prompt_MemeoryOverSize");
						Thread.Sleep(1000);
						this.timer1.Start();
					}
					else
					{
						int i = 10;
						while (i > 0)
						{
							try
							{
								i--;
								FileStream fileStream = new FileStream(this.UdiskDirList[this.UsbListComboBox.SelectedIndex] + "ledV3.zh3", FileMode.Create, FileAccess.Write);
								fileStream.Write(array, 0, array.Length);
								fileStream.Close();
								break;
							}
							catch (Exception ex)
							{
								if (i == 0)
								{
									this.UsbSaveButton.Enabled = true;
									base.Enabled = true;
									this.label_Remind.ForeColor = System.Drawing.Color.Red;
									this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Failed") + "(" + ex.Message + ")";
									base.Enabled = true;
									Thread.Sleep(1000);
									this.timer1.Start();
									return;
								}
								Thread.Sleep(200);
							}
						}
						Thread.Sleep(500);
						if (!File.Exists(this.UdiskDirList[this.UsbListComboBox.SelectedIndex] + "\\ledV3.zh3"))
						{
							MessageBox.Show(this, formMain.ML.GetStr("Prompt_DataDownloadFailed"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
							this.UsbSaveButton.Enabled = true;
							base.Enabled = true;
						}
						else
						{
							this.label_Remind.ForeColor = System.Drawing.Color.Blue;
							this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Success");
							this.UsbSaveButton.Enabled = true;
							base.Enabled = true;
							base.Enabled = true;
							Thread.Sleep(1000);
							this.timer1.Start();
						}
					}
				}
			}
			catch (Exception ex2)
			{
				this.label_Remind.ForeColor = System.Drawing.Color.Red;
				this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Failed") + "(" + ex2.Message + ")";
				this.UsbSaveButton.Enabled = true;
				base.Enabled = true;
				Thread.Sleep(1000);
				this.timer1.Start();
			}
		}

		private void ScanRoutingMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && e.ColumnIndex > -1 && e.RowIndex > -1 && this.GroupsOfScanRouting.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
			{
				this.contextMenuStrip1.Show(Control.MousePosition.X, Control.MousePosition.Y);
			}
		}

		private void timer1_Tick_1(object sender, EventArgs e)
		{
			this.updataUdiskList();
		}

		private void toolStripMenuItem_SelectRouting_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow dataGridViewRow in this.GroupsOfScanRouting.SelectedRows)
			{
				dataGridViewRow.Cells[0].Value = true;
			}
		}

		private void toolStripMenuItem_Cancel_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow dataGridViewRow in this.GroupsOfScanRouting.SelectedRows)
			{
				dataGridViewRow.Cells[0].Value = false;
			}
		}

		private IList<byte[]> ScansData()
		{
			IList<byte[]> list = new List<byte[]>();
			if (this.GroupsOfScanRouting.Rows.Count > 0)
			{
				LedRoutingSetting ledRoutingSetting = new LedRoutingSetting();
				ledRoutingSetting.SettingFileName = this.thispanel.RoutingSetting.SettingFileName;
				foreach (DataGridViewRow dataGridViewRow in ((IEnumerable)this.GroupsOfScanRouting.Rows))
				{
					if ((bool)dataGridViewRow.Cells[0].EditedFormattedValue)
					{
						ledRoutingSetting.ScanTypeIndex = (int)dataGridViewRow.Cells[3].Value;
						ledRoutingSetting.RoutingIndex = (int)dataGridViewRow.Cells[4].Value;
						ledRoutingSetting.LoadScanFromFile();
						this.thispanel.RoutingSetting = ledRoutingSetting;
						this.thispanel.Width = this.thispanel_width;
						this.thispanel.Height = this.thispanel_Heigth;
						this.SetHeigthWidth(this.thispanel, this.thispanel.RoutingSetting);
						if (this.thispanel.IsLSeries())
						{
							list.Add(this.thispanel.ToLBytes());
						}
						else
						{
							list.Add(this.thispanel.ToBytes());
						}
					}
				}
			}
			return list;
		}

		private void SetHeigthWidth(LedPanel onepanel, LedRoutingSetting pSetting)
		{
			int dataRows = (int)pSetting.DataRows;
			int unitWidth = (int)pSetting.UnitWidth;
			int maxWidth = onepanel.GetMaxWidth();
			int maxHeight = onepanel.GetMaxHeight();
			onepanel.GetMaxArea();
			onepanel.Height = onepanel.Height / dataRows * dataRows;
			onepanel.Width = onepanel.Width / unitWidth * unitWidth;
			if (maxHeight < onepanel.Height)
			{
				onepanel.Height = maxHeight;
			}
			if (maxWidth < onepanel.Width)
			{
				onepanel.Width = maxWidth;
			}
			if (onepanel.Width * onepanel.Height > onepanel.GetMaxArea())
			{
				onepanel.Width = onepanel.GetMaxArea() / onepanel.Height / unitWidth * unitWidth;
			}
			if (dataRows > onepanel.Height)
			{
				onepanel.Height = dataRows;
			}
			if (unitWidth > onepanel.Width)
			{
				onepanel.Height = unitWidth;
			}
			int num = 0;
			while (true)
			{
				num++;
				if (onepanel.Width >= 32)
				{
					break;
				}
				onepanel.Width = num * unitWidth;
			}
			num = 0;
			while (true)
			{
				num++;
				if (onepanel.Height >= 16)
				{
					break;
				}
				onepanel.Height = num * dataRows;
			}
		}

		private void button_StartScan_Click(object sender, EventArgs e)
		{
			IList<byte[]> list = new List<byte[]>();
			list = this.ScansData();
			if (list.Count == 0)
			{
				MessageBox.Show(formMain.ML.GetStr("formRouting_Scannings_Message_UnSelectedScanTypes"));
				return;
			}
			if (this.thispanel.PortType == LedPortType.GPRS || this.thispanel.PortType == LedPortType.USB || this.thispanel.PortType == LedPortType.WiFi || this.thispanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer)
			{
				return;
			}
			this.fm_Main.SendSingleCmdStart(LedCmdType.Send_ScanType, list, formMain.ML.GetStr("formRouting_Scannings_Message_SendScanTypes"), this.thispanel, false, this);
			if (formSendSingle.LastSendResult)
			{
				this.fm_Main.SendSingleCmdStart(LedCmdType.Send_End, null, formMain.ML.GetStr("formRouting_Scannings_Message_SendScanTypes"), this.thispanel, false, this);
			}
		}

		private bool SendCmd_ScanSettings(string setting_index, string cmdtitle)
		{
			if (this.thispanel.PortType == LedPortType.GPRS || this.thispanel.PortType == LedPortType.USB || this.thispanel.PortType == LedPortType.WiFi)
			{
				return false;
			}
			this.fm_Main.SendSingleCmdStart(LedCmdType.Send_ScanTypeSetting, setting_index, "", this.thispanel, false, this);
			return formSendSingle.LastSendResult;
		}

		private void button_StopScan_Click(object sender, EventArgs e)
		{
			this.SendCmd_ScanSettings("4", formMain.ML.GetStr("formRouting_Scannings_Message_StopScanning"));
		}

		private void button_pause_Click(object sender, EventArgs e)
		{
			this.SendCmd_ScanSettings("0", formMain.ML.GetStr("formRouting_Scannings_Message_PauseScanning"));
		}

		private void button_continue_Click(object sender, EventArgs e)
		{
			this.SendCmd_ScanSettings("1", formMain.ML.GetStr("formRouting_Scannings_Message_ContinueScanning"));
		}

		private void button_nextScan_Click(object sender, EventArgs e)
		{
			this.SendCmd_ScanSettings("2", formMain.ML.GetStr("formRouting_Scannings_Btn_Next"));
		}

		private void button_lastScan_Click(object sender, EventArgs e)
		{
			this.SendCmd_ScanSettings("3", formMain.ML.GetStr("formRouting_Scannings_Btn_Previous"));
		}

		private void formRouting_close(object sender, FormClosedEventArgs e)
		{
			if (!this.isLoaded)
			{
				this.thispanel.RoutingSetting = this.routingSetting;
				this.thispanel.Height = this.thispanel_Heigth;
				this.thispanel.Width = this.thispanel_width;
			}
		}

		private void buttonLoad_Click(object sender, EventArgs e)
		{
			this.isLoaded = false;
			int num = 0;
			int index = 0;
			foreach (DataGridViewRow dataGridViewRow in ((IEnumerable)this.GroupsOfScanRouting.Rows))
			{
				if (num > 1)
				{
					MessageBox.Show(formMain.ML.GetStr("formRouting_Scannings_Message_SelectedManyNeedOne"));
					return;
				}
				if ((bool)dataGridViewRow.Cells[0].EditedFormattedValue)
				{
					num++;
					index = dataGridViewRow.Index;
				}
			}
			if (num == 0)
			{
				MessageBox.Show(formMain.ML.GetStr("formRouting_Scannings_Message_SelectedNoneNeedOne"));
				return;
			}
			LedRoutingSetting ledRoutingSetting = new LedRoutingSetting();
			ledRoutingSetting.SettingFileName = this.thispanel.RoutingSetting.SettingFileName;
			ledRoutingSetting.ScanTypeIndex = (int)this.GroupsOfScanRouting.Rows[index].Cells[3].Value;
			ledRoutingSetting.RoutingIndex = (int)this.GroupsOfScanRouting.Rows[index].Cells[4].Value;
			ledRoutingSetting.LoadScanFromFile();
			this.thispanel.RoutingSetting = ledRoutingSetting;
			this.thispanel.Width = this.thispanel_width;
			this.thispanel.Height = this.thispanel_Heigth;
			this.SetHeigthWidth(this.thispanel, this.thispanel.RoutingSetting);
			this.fm_Main.SendSingleCmdStart(LedCmdType.Send_Panel_Parameter, this.thispanel, this.buttonLoad.Text, this.thispanel, false, this);
			if (formSendSingle.LastSendResult)
			{
				this.isLoaded = true;
				MessageBox.Show(this, formMain.ML.GetStr("Prompt_LoadSuccessed"));
			}
		}

		private void tabControl_ScanRouting_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.tabControl_ScanRouting.SelectedIndex == 1)
			{
				this.tabControl_ScanRouting.Height = this.groupBox_UsbSave.Top + this.groupBox_UsbSave.Height + 45;
				base.Height = this.tabControl_ScanRouting.Top + this.tabControl_ScanRouting.Height + 40;
				return;
			}
			this.tabControl_ScanRouting.Height = this.selectRoutingLabel1.Top + this.selectRoutingLabel1.Height + 45;
			base.Height = this.tabControl_ScanRouting.Top + this.tabControl_ScanRouting.Height + 40;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formRouting));
			this.manualSettingCheckBox = new CheckBox();
			this.routingSettingComboBox = new ComboBox();
			this.selectRoutinglabel15 = new Label();
			this.rowOrderPolarityComboBox = new ComboBox();
			this.selectRoutinglabel14 = new Label();
			this.abcdPolarityComboBox = new ComboBox();
			this.label13selectRouting = new Label();
			this.comboBox1 = new ComboBox();
			this.label12selectRouting = new Label();
			this.ltPolarityComboBox = new ComboBox();
			this.label11selectRouting = new Label();
			this.ckPolarityComboBox = new ComboBox();
			this.label10selectRouting = new Label();
			this.dataPolarityComboBox = new ComboBox();
			this.label9selectRouting = new Label();
			this.oePolarityComboBox = new ComboBox();
			this.label8selectRouting = new Label();
			this.bigLineTurnNumComboBox = new ComboBox();
			this.label7selectRouting = new Label();
			this.bigLineColumsComboBox = new ComboBox();
			this.label6selectRouting = new Label();
			this.bigLineRoutingComboBox = new ComboBox();
			this.label5selectRouting = new Label();
			this.smallLineTurnNumComboBox = new ComboBox();
			this.label4selectRouting = new Label();
			this.smallLineColumsComboBox = new ComboBox();
			this.label3selectRouting = new Label();
			this.smallLinesmallLineRoutingComboBox = new ComboBox();
			this.label2selectRouting = new Label();
			this.dataRowsComboBox = new ComboBox();
			this.label1selectRouting = new Label();
			this.scanTypeComboBox = new ComboBox();
			this.pictureBox1 = new PictureBox();
			this.checkBox_RGConnect = new CheckBox();
			this.radioButtonScan5 = new RadioButton();
			this.radioButtonScan3 = new RadioButton();
			this.radioButtonScan16 = new RadioButton();
			this.radioButtonScan8 = new RadioButton();
			this.radioButtonScan6 = new RadioButton();
			this.radioButtonScan4 = new RadioButton();
			this.radioButtonScan2 = new RadioButton();
			this.radioButtonScanStatic = new RadioButton();
			this.selectRoutinglabel17 = new Label();
			this.routingScanRateComboBox = new ComboBox();
			this.selectRoutinglabel16 = new Label();
			this.routingDispTimeComboBox = new ComboBox();
			this.routingParamGroupBox = new GroupBox();
			this.label2 = new Label();
			this.routingLtComboBox = new ComboBox();
			this.selectRoutingLabel1 = new GroupBox();
			this.routingRefreshScreenComboBox = new ComboBox();
			this.label1 = new Label();
			this.tabControl_ScanRouting = new TabControl();
			this.tabPage_ToSelectScanRouting = new TabPage();
			this.tabPage_Scannings = new TabPage();
			this.groupBox_UsbSave = new GroupBox();
			this.label_SelectFlash = new Label();
			this.UsbListComboBox = new ComboBox();
			this.label_Remind = new Label();
			this.SelUdiskLabel = new Label();
			this.UsbSaveButton = new Button();
			this.groupBoxSend = new GroupBox();
			this.button_nextScan = new Button();
			this.button_lastScan = new Button();
			this.button_continue = new Button();
			this.button_pause = new Button();
			this.label_communication = new Label();
			this.LblScan = new Label();
			this.button_StopScan = new Button();
			this.button_StartScan = new Button();
			this.LblComm = new Label();
			this.groupBox2 = new GroupBox();
			this.buttonLoad = new Button();
			this.checkBox_Reverse = new CheckBox();
			this.GroupsOfScanRouting = new DataGridView();
			this.IsChecked = new DataGridViewCheckBoxColumn();
			this.Index = new DataGridViewTextBoxColumn();
			this.ScanRouting = new DataGridViewTextBoxColumn();
			this.ColumnScanType = new DataGridViewTextBoxColumn();
			this.ColumnIndex = new DataGridViewTextBoxColumn();
			this.checkBox_SelectAll = new CheckBox();
			this.groupBoxScans = new GroupBox();
			this.radioButtonAllScans = new RadioButton();
			this.radioButtonScans6 = new RadioButton();
			this.radioButtonScans5 = new RadioButton();
			this.radioButtonScans3 = new RadioButton();
			this.radioButtonScansStatic = new RadioButton();
			this.radioButtonScans16 = new RadioButton();
			this.radioButtonScans8 = new RadioButton();
			this.radioButtonScans4 = new RadioButton();
			this.radioButtonScans2 = new RadioButton();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.toolStripMenuItem_SelectRouting = new ToolStripMenuItem();
			this.toolStripMenuItem_Cancel = new ToolStripMenuItem();
			this.radioButtonScan10 = new RadioButton();
			this.radioButtonScans10 = new RadioButton();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.routingParamGroupBox.SuspendLayout();
			this.selectRoutingLabel1.SuspendLayout();
			this.tabControl_ScanRouting.SuspendLayout();
			this.tabPage_ToSelectScanRouting.SuspendLayout();
			this.tabPage_Scannings.SuspendLayout();
			this.groupBox_UsbSave.SuspendLayout();
			this.groupBoxSend.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.GroupsOfScanRouting).BeginInit();
			this.groupBoxScans.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.manualSettingCheckBox.AutoSize = true;
			this.manualSettingCheckBox.Location = new System.Drawing.Point(13, 246);
			this.manualSettingCheckBox.Name = "manualSettingCheckBox";
			this.manualSettingCheckBox.Size = new System.Drawing.Size(144, 16);
			this.manualSettingCheckBox.TabIndex = 0;
			this.manualSettingCheckBox.Text = "手工配置走线方式参数";
			this.manualSettingCheckBox.UseVisualStyleBackColor = true;
			this.manualSettingCheckBox.Visible = false;
			this.manualSettingCheckBox.CheckedChanged += new EventHandler(this.manualSettingCheckBox_CheckedChanged);
			this.routingSettingComboBox.DropDownHeight = 500;
			this.routingSettingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.routingSettingComboBox.FormattingEnabled = true;
			this.routingSettingComboBox.IntegralHeight = false;
			this.routingSettingComboBox.Items.AddRange(new object[]
			{
				"1、常规P10  1/4扫 上蛇形 一路数据带16行 8列折行",
				"2、常规P16  1/4扫 上蛇形 一路数据带8行 8列折行",
				"3、常规P10  1/4扫 上蛇形 一路数据带16行 8列折行 无138译码",
				"4、P10  1/4扫 上蛇形 一路数据带16行 8列小折行（上蛇形），32列大折行（上蛇形）",
				"5、1/8扫 直线形 一路数据带8行",
				"6、1/16扫 直线形 一路数据带16行"
			});
			this.routingSettingComboBox.Location = new System.Drawing.Point(13, 203);
			this.routingSettingComboBox.Name = "routingSettingComboBox";
			this.routingSettingComboBox.Size = new System.Drawing.Size(524, 20);
			this.routingSettingComboBox.TabIndex = 1;
			this.routingSettingComboBox.SelectedIndexChanged += new EventHandler(this.routingSettingComboBox_SelectedIndexChanged);
			this.selectRoutinglabel15.AutoSize = true;
			this.selectRoutinglabel15.Location = new System.Drawing.Point(6, 327);
			this.selectRoutinglabel15.Name = "selectRoutinglabel15";
			this.selectRoutinglabel15.Size = new System.Drawing.Size(41, 12);
			this.selectRoutinglabel15.TabIndex = 29;
			this.selectRoutinglabel15.Text = "行顺序";
			this.rowOrderPolarityComboBox.FormattingEnabled = true;
			this.rowOrderPolarityComboBox.Items.AddRange(new object[]
			{
				"0",
				"1"
			});
			this.rowOrderPolarityComboBox.Location = new System.Drawing.Point(122, 325);
			this.rowOrderPolarityComboBox.Name = "rowOrderPolarityComboBox";
			this.rowOrderPolarityComboBox.Size = new System.Drawing.Size(99, 20);
			this.rowOrderPolarityComboBox.TabIndex = 28;
			this.selectRoutinglabel14.AutoSize = true;
			this.selectRoutinglabel14.Location = new System.Drawing.Point(6, 305);
			this.selectRoutinglabel14.Name = "selectRoutinglabel14";
			this.selectRoutinglabel14.Size = new System.Drawing.Size(53, 12);
			this.selectRoutinglabel14.TabIndex = 27;
			this.selectRoutinglabel14.Text = "ABCD极性";
			this.abcdPolarityComboBox.FormattingEnabled = true;
			this.abcdPolarityComboBox.Items.AddRange(new object[]
			{
				"0",
				"1"
			});
			this.abcdPolarityComboBox.Location = new System.Drawing.Point(122, 303);
			this.abcdPolarityComboBox.Name = "abcdPolarityComboBox";
			this.abcdPolarityComboBox.Size = new System.Drawing.Size(99, 20);
			this.abcdPolarityComboBox.TabIndex = 26;
			this.label13selectRouting.AutoSize = true;
			this.label13selectRouting.Location = new System.Drawing.Point(6, 194);
			this.label13selectRouting.Name = "label13selectRouting";
			this.label13selectRouting.Size = new System.Drawing.Size(47, 12);
			this.label13selectRouting.TabIndex = 25;
			this.label13selectRouting.Text = "138译码";
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[]
			{
				"有",
				"无"
			});
			this.comboBox1.Location = new System.Drawing.Point(122, 192);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(99, 20);
			this.comboBox1.TabIndex = 24;
			this.label12selectRouting.AutoSize = true;
			this.label12selectRouting.Location = new System.Drawing.Point(6, 282);
			this.label12selectRouting.Name = "label12selectRouting";
			this.label12selectRouting.Size = new System.Drawing.Size(41, 12);
			this.label12selectRouting.TabIndex = 23;
			this.label12selectRouting.Text = "LT极性";
			this.ltPolarityComboBox.FormattingEnabled = true;
			this.ltPolarityComboBox.Items.AddRange(new object[]
			{
				"下降沿",
				"上升沿"
			});
			this.ltPolarityComboBox.Location = new System.Drawing.Point(122, 280);
			this.ltPolarityComboBox.Name = "ltPolarityComboBox";
			this.ltPolarityComboBox.Size = new System.Drawing.Size(99, 20);
			this.ltPolarityComboBox.TabIndex = 22;
			this.label11selectRouting.AutoSize = true;
			this.label11selectRouting.Location = new System.Drawing.Point(6, 260);
			this.label11selectRouting.Name = "label11selectRouting";
			this.label11selectRouting.Size = new System.Drawing.Size(41, 12);
			this.label11selectRouting.TabIndex = 21;
			this.label11selectRouting.Text = "CK极性";
			this.ckPolarityComboBox.FormattingEnabled = true;
			this.ckPolarityComboBox.Items.AddRange(new object[]
			{
				"下降沿",
				"上升沿"
			});
			this.ckPolarityComboBox.Location = new System.Drawing.Point(122, 258);
			this.ckPolarityComboBox.Name = "ckPolarityComboBox";
			this.ckPolarityComboBox.Size = new System.Drawing.Size(99, 20);
			this.ckPolarityComboBox.TabIndex = 20;
			this.label10selectRouting.AutoSize = true;
			this.label10selectRouting.Location = new System.Drawing.Point(6, 238);
			this.label10selectRouting.Name = "label10selectRouting";
			this.label10selectRouting.Size = new System.Drawing.Size(53, 12);
			this.label10selectRouting.TabIndex = 19;
			this.label10selectRouting.Text = "数据极性";
			this.dataPolarityComboBox.FormattingEnabled = true;
			this.dataPolarityComboBox.Items.AddRange(new object[]
			{
				"高电平有效",
				"低电平有效"
			});
			this.dataPolarityComboBox.Location = new System.Drawing.Point(122, 236);
			this.dataPolarityComboBox.Name = "dataPolarityComboBox";
			this.dataPolarityComboBox.Size = new System.Drawing.Size(99, 20);
			this.dataPolarityComboBox.TabIndex = 18;
			this.label9selectRouting.AutoSize = true;
			this.label9selectRouting.Location = new System.Drawing.Point(6, 216);
			this.label9selectRouting.Name = "label9selectRouting";
			this.label9selectRouting.Size = new System.Drawing.Size(41, 12);
			this.label9selectRouting.TabIndex = 17;
			this.label9selectRouting.Text = "OE极性";
			this.oePolarityComboBox.FormattingEnabled = true;
			this.oePolarityComboBox.Items.AddRange(new object[]
			{
				"高电平有效",
				"低电平有效"
			});
			this.oePolarityComboBox.Location = new System.Drawing.Point(122, 214);
			this.oePolarityComboBox.Name = "oePolarityComboBox";
			this.oePolarityComboBox.Size = new System.Drawing.Size(99, 20);
			this.oePolarityComboBox.TabIndex = 16;
			this.label8selectRouting.AutoSize = true;
			this.label8selectRouting.Location = new System.Drawing.Point(6, 173);
			this.label8selectRouting.Name = "label8selectRouting";
			this.label8selectRouting.Size = new System.Drawing.Size(65, 12);
			this.label8selectRouting.TabIndex = 15;
			this.label8selectRouting.Text = "大折行折数";
			this.bigLineTurnNumComboBox.FormattingEnabled = true;
			this.bigLineTurnNumComboBox.Location = new System.Drawing.Point(122, 171);
			this.bigLineTurnNumComboBox.Name = "bigLineTurnNumComboBox";
			this.bigLineTurnNumComboBox.Size = new System.Drawing.Size(99, 20);
			this.bigLineTurnNumComboBox.TabIndex = 14;
			this.label7selectRouting.AutoSize = true;
			this.label7selectRouting.Location = new System.Drawing.Point(6, 150);
			this.label7selectRouting.Name = "label7selectRouting";
			this.label7selectRouting.Size = new System.Drawing.Size(65, 12);
			this.label7selectRouting.TabIndex = 13;
			this.label7selectRouting.Text = "大折行列数";
			this.bigLineColumsComboBox.FormattingEnabled = true;
			this.bigLineColumsComboBox.Location = new System.Drawing.Point(122, 148);
			this.bigLineColumsComboBox.Name = "bigLineColumsComboBox";
			this.bigLineColumsComboBox.Size = new System.Drawing.Size(99, 20);
			this.bigLineColumsComboBox.TabIndex = 12;
			this.label6selectRouting.AutoSize = true;
			this.label6selectRouting.Location = new System.Drawing.Point(6, 127);
			this.label6selectRouting.Name = "label6selectRouting";
			this.label6selectRouting.Size = new System.Drawing.Size(89, 12);
			this.label6selectRouting.TabIndex = 11;
			this.label6selectRouting.Text = "大折行走线类型";
			this.bigLineRoutingComboBox.FormattingEnabled = true;
			this.bigLineRoutingComboBox.Items.AddRange(new object[]
			{
				"直线型",
				"上蛇行",
				"下蛇行"
			});
			this.bigLineRoutingComboBox.Location = new System.Drawing.Point(122, 125);
			this.bigLineRoutingComboBox.Name = "bigLineRoutingComboBox";
			this.bigLineRoutingComboBox.Size = new System.Drawing.Size(99, 20);
			this.bigLineRoutingComboBox.TabIndex = 10;
			this.label5selectRouting.AutoSize = true;
			this.label5selectRouting.Location = new System.Drawing.Point(6, 105);
			this.label5selectRouting.Name = "label5selectRouting";
			this.label5selectRouting.Size = new System.Drawing.Size(65, 12);
			this.label5selectRouting.TabIndex = 9;
			this.label5selectRouting.Text = "小折行折数";
			this.smallLineTurnNumComboBox.FormattingEnabled = true;
			this.smallLineTurnNumComboBox.Location = new System.Drawing.Point(122, 103);
			this.smallLineTurnNumComboBox.Name = "smallLineTurnNumComboBox";
			this.smallLineTurnNumComboBox.Size = new System.Drawing.Size(99, 20);
			this.smallLineTurnNumComboBox.TabIndex = 8;
			this.label4selectRouting.AutoSize = true;
			this.label4selectRouting.Location = new System.Drawing.Point(6, 83);
			this.label4selectRouting.Name = "label4selectRouting";
			this.label4selectRouting.Size = new System.Drawing.Size(65, 12);
			this.label4selectRouting.TabIndex = 7;
			this.label4selectRouting.Text = "小折行列数";
			this.smallLineColumsComboBox.FormattingEnabled = true;
			this.smallLineColumsComboBox.Items.AddRange(new object[]
			{
				"2",
				"4",
				"8",
				"16",
				"32",
				"64"
			});
			this.smallLineColumsComboBox.Location = new System.Drawing.Point(122, 81);
			this.smallLineColumsComboBox.Name = "smallLineColumsComboBox";
			this.smallLineColumsComboBox.Size = new System.Drawing.Size(99, 20);
			this.smallLineColumsComboBox.TabIndex = 6;
			this.label3selectRouting.AutoSize = true;
			this.label3selectRouting.Location = new System.Drawing.Point(6, 61);
			this.label3selectRouting.Name = "label3selectRouting";
			this.label3selectRouting.Size = new System.Drawing.Size(89, 12);
			this.label3selectRouting.TabIndex = 5;
			this.label3selectRouting.Text = "小折行走线类型";
			this.smallLinesmallLineRoutingComboBox.FormattingEnabled = true;
			this.smallLinesmallLineRoutingComboBox.Items.AddRange(new object[]
			{
				"直线型",
				"上蛇行",
				"下蛇行"
			});
			this.smallLinesmallLineRoutingComboBox.Location = new System.Drawing.Point(122, 59);
			this.smallLinesmallLineRoutingComboBox.Name = "smallLinesmallLineRoutingComboBox";
			this.smallLinesmallLineRoutingComboBox.Size = new System.Drawing.Size(99, 20);
			this.smallLinesmallLineRoutingComboBox.TabIndex = 4;
			this.label2selectRouting.AutoSize = true;
			this.label2selectRouting.Location = new System.Drawing.Point(6, 39);
			this.label2selectRouting.Name = "label2selectRouting";
			this.label2selectRouting.Size = new System.Drawing.Size(53, 12);
			this.label2selectRouting.TabIndex = 3;
			this.label2selectRouting.Text = "数据行数";
			this.dataRowsComboBox.FormattingEnabled = true;
			this.dataRowsComboBox.Items.AddRange(new object[]
			{
				"8",
				"16",
				"32",
				"64"
			});
			this.dataRowsComboBox.Location = new System.Drawing.Point(122, 37);
			this.dataRowsComboBox.Name = "dataRowsComboBox";
			this.dataRowsComboBox.Size = new System.Drawing.Size(99, 20);
			this.dataRowsComboBox.TabIndex = 2;
			this.label1selectRouting.AutoSize = true;
			this.label1selectRouting.Location = new System.Drawing.Point(6, 17);
			this.label1selectRouting.Name = "label1selectRouting";
			this.label1selectRouting.Size = new System.Drawing.Size(53, 12);
			this.label1selectRouting.TabIndex = 1;
			this.label1selectRouting.Text = "扫描方式";
			this.scanTypeComboBox.FormattingEnabled = true;
			this.scanTypeComboBox.Items.AddRange(new object[]
			{
				"1/4",
				"1/8",
				"1/16"
			});
			this.scanTypeComboBox.Location = new System.Drawing.Point(122, 15);
			this.scanTypeComboBox.Name = "scanTypeComboBox";
			this.scanTypeComboBox.Size = new System.Drawing.Size(99, 20);
			this.scanTypeComboBox.TabIndex = 0;
			this.pictureBox1.Location = new System.Drawing.Point(-6, 65);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(379, 281);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			this.checkBox_RGConnect.AutoSize = true;
			this.checkBox_RGConnect.Location = new System.Drawing.Point(224, 400);
			this.checkBox_RGConnect.Name = "checkBox_RGConnect";
			this.checkBox_RGConnect.Size = new System.Drawing.Size(60, 16);
			this.checkBox_RGConnect.TabIndex = 45;
			this.checkBox_RGConnect.Text = "RG短接";
			this.checkBox_RGConnect.UseVisualStyleBackColor = true;
			this.checkBox_RGConnect.Visible = false;
			this.checkBox_RGConnect.CheckedChanged += new EventHandler(this.checkBox_RGConnect_CheckedChanged);
			this.radioButtonScan5.AutoSize = true;
			this.radioButtonScan5.Location = new System.Drawing.Point(224, 83);
			this.radioButtonScan5.Name = "radioButtonScan5";
			this.radioButtonScan5.Size = new System.Drawing.Size(53, 16);
			this.radioButtonScan5.TabIndex = 44;
			this.radioButtonScan5.TabStop = true;
			this.radioButtonScan5.Text = "1/5扫";
			this.radioButtonScan5.UseVisualStyleBackColor = true;
			this.radioButtonScan5.CheckedChanged += new EventHandler(this.radioButtonScan5_CheckedChanged);
			this.radioButtonScan3.AutoSize = true;
			this.radioButtonScan3.Location = new System.Drawing.Point(441, 26);
			this.radioButtonScan3.Name = "radioButtonScan3";
			this.radioButtonScan3.Size = new System.Drawing.Size(53, 16);
			this.radioButtonScan3.TabIndex = 43;
			this.radioButtonScan3.TabStop = true;
			this.radioButtonScan3.Text = "1/3扫";
			this.radioButtonScan3.UseVisualStyleBackColor = true;
			this.radioButtonScan3.CheckedChanged += new EventHandler(this.radioButtonScan3_CheckedChanged);
			this.radioButtonScan16.AutoSize = true;
			this.radioButtonScan16.Location = new System.Drawing.Point(441, 144);
			this.radioButtonScan16.Name = "radioButtonScan16";
			this.radioButtonScan16.Size = new System.Drawing.Size(59, 16);
			this.radioButtonScan16.TabIndex = 42;
			this.radioButtonScan16.TabStop = true;
			this.radioButtonScan16.Text = "1/16扫";
			this.radioButtonScan16.UseVisualStyleBackColor = true;
			this.radioButtonScan16.CheckedChanged += new EventHandler(this.radioButtonScan16_CheckedChanged);
			this.radioButtonScan8.AutoSize = true;
			this.radioButtonScan8.Location = new System.Drawing.Point(13, 144);
			this.radioButtonScan8.Name = "radioButtonScan8";
			this.radioButtonScan8.Size = new System.Drawing.Size(53, 16);
			this.radioButtonScan8.TabIndex = 41;
			this.radioButtonScan8.TabStop = true;
			this.radioButtonScan8.Text = "1/8扫";
			this.radioButtonScan8.UseVisualStyleBackColor = true;
			this.radioButtonScan8.CheckedChanged += new EventHandler(this.radioButtonScan8_CheckedChanged);
			this.radioButtonScan6.AutoSize = true;
			this.radioButtonScan6.Location = new System.Drawing.Point(441, 83);
			this.radioButtonScan6.Name = "radioButtonScan6";
			this.radioButtonScan6.Size = new System.Drawing.Size(53, 16);
			this.radioButtonScan6.TabIndex = 40;
			this.radioButtonScan6.TabStop = true;
			this.radioButtonScan6.Text = "1/6扫";
			this.radioButtonScan6.UseVisualStyleBackColor = true;
			this.radioButtonScan6.CheckedChanged += new EventHandler(this.radioButtonScan6_CheckedChanged);
			this.radioButtonScan4.AutoSize = true;
			this.radioButtonScan4.Location = new System.Drawing.Point(13, 83);
			this.radioButtonScan4.Name = "radioButtonScan4";
			this.radioButtonScan4.Size = new System.Drawing.Size(53, 16);
			this.radioButtonScan4.TabIndex = 39;
			this.radioButtonScan4.TabStop = true;
			this.radioButtonScan4.Text = "1/4扫";
			this.radioButtonScan4.UseVisualStyleBackColor = true;
			this.radioButtonScan4.CheckedChanged += new EventHandler(this.radioButtonScab4_CheckedChanged);
			this.radioButtonScan2.AutoSize = true;
			this.radioButtonScan2.Location = new System.Drawing.Point(224, 26);
			this.radioButtonScan2.Name = "radioButtonScan2";
			this.radioButtonScan2.Size = new System.Drawing.Size(53, 16);
			this.radioButtonScan2.TabIndex = 38;
			this.radioButtonScan2.TabStop = true;
			this.radioButtonScan2.Text = "1/2扫";
			this.radioButtonScan2.UseVisualStyleBackColor = true;
			this.radioButtonScan2.CheckedChanged += new EventHandler(this.radioButtonScan2_CheckedChanged);
			this.radioButtonScanStatic.AutoSize = true;
			this.radioButtonScanStatic.Location = new System.Drawing.Point(13, 26);
			this.radioButtonScanStatic.Name = "radioButtonScanStatic";
			this.radioButtonScanStatic.Size = new System.Drawing.Size(47, 16);
			this.radioButtonScanStatic.TabIndex = 37;
			this.radioButtonScanStatic.TabStop = true;
			this.radioButtonScanStatic.Text = "静态";
			this.radioButtonScanStatic.UseVisualStyleBackColor = true;
			this.radioButtonScanStatic.CheckedChanged += new EventHandler(this.radioButtonScanStatic_CheckedChanged);
			this.selectRoutinglabel17.Location = new System.Drawing.Point(107, 334);
			this.selectRoutinglabel17.Name = "selectRoutinglabel17";
			this.selectRoutinglabel17.Size = new System.Drawing.Size(111, 12);
			this.selectRoutinglabel17.TabIndex = 34;
			this.selectRoutinglabel17.Text = "消隐时间";
			this.selectRoutinglabel17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.routingScanRateComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.routingScanRateComboBox.FormattingEnabled = true;
			this.routingScanRateComboBox.Items.AddRange(new object[]
			{
				"最快",
				"稍快",
				"快",
				"正常",
				"慢",
				"稍慢",
				"很慢"
			});
			this.routingScanRateComboBox.Location = new System.Drawing.Point(224, 293);
			this.routingScanRateComboBox.Name = "routingScanRateComboBox";
			this.routingScanRateComboBox.Size = new System.Drawing.Size(129, 20);
			this.routingScanRateComboBox.TabIndex = 33;
			this.routingScanRateComboBox.SelectedIndexChanged += new EventHandler(this.routingScanRateComboBox_SelectedIndexChanged);
			this.selectRoutinglabel16.Location = new System.Drawing.Point(107, 296);
			this.selectRoutinglabel16.Name = "selectRoutinglabel16";
			this.selectRoutinglabel16.Size = new System.Drawing.Size(111, 12);
			this.selectRoutinglabel16.TabIndex = 32;
			this.selectRoutinglabel16.Text = "扫描频率";
			this.selectRoutinglabel16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.routingDispTimeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.routingDispTimeComboBox.FormattingEnabled = true;
			this.routingDispTimeComboBox.Items.AddRange(new object[]
			{
				"最短",
				"稍短",
				"正常",
				"稍长",
				"最长"
			});
			this.routingDispTimeComboBox.Location = new System.Drawing.Point(224, 331);
			this.routingDispTimeComboBox.Name = "routingDispTimeComboBox";
			this.routingDispTimeComboBox.Size = new System.Drawing.Size(129, 20);
			this.routingDispTimeComboBox.TabIndex = 5;
			this.routingDispTimeComboBox.SelectedIndexChanged += new EventHandler(this.routingDispTimeComboBox_SelectedIndexChanged);
			this.routingParamGroupBox.Controls.Add(this.selectRoutinglabel15);
			this.routingParamGroupBox.Controls.Add(this.label5selectRouting);
			this.routingParamGroupBox.Controls.Add(this.label6selectRouting);
			this.routingParamGroupBox.Controls.Add(this.bigLineTurnNumComboBox);
			this.routingParamGroupBox.Controls.Add(this.rowOrderPolarityComboBox);
			this.routingParamGroupBox.Controls.Add(this.label8selectRouting);
			this.routingParamGroupBox.Controls.Add(this.scanTypeComboBox);
			this.routingParamGroupBox.Controls.Add(this.label7selectRouting);
			this.routingParamGroupBox.Controls.Add(this.selectRoutinglabel14);
			this.routingParamGroupBox.Controls.Add(this.oePolarityComboBox);
			this.routingParamGroupBox.Controls.Add(this.label1selectRouting);
			this.routingParamGroupBox.Controls.Add(this.bigLineColumsComboBox);
			this.routingParamGroupBox.Controls.Add(this.abcdPolarityComboBox);
			this.routingParamGroupBox.Controls.Add(this.label9selectRouting);
			this.routingParamGroupBox.Controls.Add(this.dataRowsComboBox);
			this.routingParamGroupBox.Controls.Add(this.bigLineRoutingComboBox);
			this.routingParamGroupBox.Controls.Add(this.label13selectRouting);
			this.routingParamGroupBox.Controls.Add(this.dataPolarityComboBox);
			this.routingParamGroupBox.Controls.Add(this.label2selectRouting);
			this.routingParamGroupBox.Controls.Add(this.label10selectRouting);
			this.routingParamGroupBox.Controls.Add(this.comboBox1);
			this.routingParamGroupBox.Controls.Add(this.smallLineTurnNumComboBox);
			this.routingParamGroupBox.Controls.Add(this.smallLinesmallLineRoutingComboBox);
			this.routingParamGroupBox.Controls.Add(this.ckPolarityComboBox);
			this.routingParamGroupBox.Controls.Add(this.label12selectRouting);
			this.routingParamGroupBox.Controls.Add(this.label4selectRouting);
			this.routingParamGroupBox.Controls.Add(this.label3selectRouting);
			this.routingParamGroupBox.Controls.Add(this.label11selectRouting);
			this.routingParamGroupBox.Controls.Add(this.ltPolarityComboBox);
			this.routingParamGroupBox.Controls.Add(this.smallLineColumsComboBox);
			this.routingParamGroupBox.Location = new System.Drawing.Point(795, 48);
			this.routingParamGroupBox.Name = "routingParamGroupBox";
			this.routingParamGroupBox.Size = new System.Drawing.Size(261, 363);
			this.routingParamGroupBox.TabIndex = 23;
			this.routingParamGroupBox.TabStop = false;
			this.routingParamGroupBox.Text = "groupBox1";
			this.label2.Location = new System.Drawing.Point(107, 371);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111, 12);
			this.label2.TabIndex = 47;
			this.label2.Text = "LT";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.routingLtComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.routingLtComboBox.FormattingEnabled = true;
			this.routingLtComboBox.Items.AddRange(new object[]
			{
				"上升沿",
				"下降沿",
				"高电平",
				"低电平"
			});
			this.routingLtComboBox.Location = new System.Drawing.Point(224, 368);
			this.routingLtComboBox.Name = "routingLtComboBox";
			this.routingLtComboBox.Size = new System.Drawing.Size(129, 20);
			this.routingLtComboBox.TabIndex = 46;
			this.routingLtComboBox.SelectedIndexChanged += new EventHandler(this.routingLtComboBox_SelectedIndexChanged);
			this.selectRoutingLabel1.Controls.Add(this.radioButtonScan10);
			this.selectRoutingLabel1.Controls.Add(this.routingScanRateComboBox);
			this.selectRoutingLabel1.Controls.Add(this.label2);
			this.selectRoutingLabel1.Controls.Add(this.radioButtonScan6);
			this.selectRoutingLabel1.Controls.Add(this.routingLtComboBox);
			this.selectRoutingLabel1.Controls.Add(this.radioButtonScanStatic);
			this.selectRoutingLabel1.Controls.Add(this.selectRoutinglabel17);
			this.selectRoutingLabel1.Controls.Add(this.radioButtonScan2);
			this.selectRoutingLabel1.Controls.Add(this.checkBox_RGConnect);
			this.selectRoutingLabel1.Controls.Add(this.radioButtonScan4);
			this.selectRoutingLabel1.Controls.Add(this.selectRoutinglabel16);
			this.selectRoutingLabel1.Controls.Add(this.radioButtonScan8);
			this.selectRoutingLabel1.Controls.Add(this.routingDispTimeComboBox);
			this.selectRoutingLabel1.Controls.Add(this.radioButtonScan5);
			this.selectRoutingLabel1.Controls.Add(this.radioButtonScan16);
			this.selectRoutingLabel1.Controls.Add(this.manualSettingCheckBox);
			this.selectRoutingLabel1.Controls.Add(this.routingSettingComboBox);
			this.selectRoutingLabel1.Controls.Add(this.radioButtonScan3);
			this.selectRoutingLabel1.Location = new System.Drawing.Point(16, 14);
			this.selectRoutingLabel1.Name = "selectRoutingLabel1";
			this.selectRoutingLabel1.Size = new System.Drawing.Size(551, 437);
			this.selectRoutingLabel1.TabIndex = 25;
			this.selectRoutingLabel1.TabStop = false;
			this.selectRoutingLabel1.Text = "选择走线方式";
			this.routingRefreshScreenComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.routingRefreshScreenComboBox.FormattingEnabled = true;
			this.routingRefreshScreenComboBox.Items.AddRange(new object[]
			{
				"最快",
				"快",
				"正常",
				"稍慢",
				"慢",
				"很慢",
				"最慢"
			});
			this.routingRefreshScreenComboBox.Location = new System.Drawing.Point(918, 426);
			this.routingRefreshScreenComboBox.Name = "routingRefreshScreenComboBox";
			this.routingRefreshScreenComboBox.Size = new System.Drawing.Size(129, 20);
			this.routingRefreshScreenComboBox.TabIndex = 36;
			this.routingRefreshScreenComboBox.Visible = false;
			this.routingRefreshScreenComboBox.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
			this.label1.Location = new System.Drawing.Point(801, 431);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 12);
			this.label1.TabIndex = 35;
			this.label1.Text = "刷屏频率";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label1.Visible = false;
			this.tabControl_ScanRouting.Controls.Add(this.tabPage_ToSelectScanRouting);
			this.tabControl_ScanRouting.Controls.Add(this.tabPage_Scannings);
			this.tabControl_ScanRouting.Location = new System.Drawing.Point(8, 12);
			this.tabControl_ScanRouting.Name = "tabControl_ScanRouting";
			this.tabControl_ScanRouting.SelectedIndex = 0;
			this.tabControl_ScanRouting.Size = new System.Drawing.Size(590, 630);
			this.tabControl_ScanRouting.TabIndex = 37;
			this.tabControl_ScanRouting.SelectedIndexChanged += new EventHandler(this.tabControl_ScanRouting_SelectedIndexChanged);
			this.tabPage_ToSelectScanRouting.Controls.Add(this.selectRoutingLabel1);
			this.tabPage_ToSelectScanRouting.Location = new System.Drawing.Point(4, 22);
			this.tabPage_ToSelectScanRouting.Name = "tabPage_ToSelectScanRouting";
			this.tabPage_ToSelectScanRouting.Padding = new Padding(3);
			this.tabPage_ToSelectScanRouting.Size = new System.Drawing.Size(582, 604);
			this.tabPage_ToSelectScanRouting.TabIndex = 0;
			this.tabPage_ToSelectScanRouting.Text = "设置";
			this.tabPage_ToSelectScanRouting.UseVisualStyleBackColor = true;
			this.tabPage_Scannings.Controls.Add(this.groupBox_UsbSave);
			this.tabPage_Scannings.Controls.Add(this.groupBoxSend);
			this.tabPage_Scannings.Controls.Add(this.groupBox2);
			this.tabPage_Scannings.Controls.Add(this.groupBoxScans);
			this.tabPage_Scannings.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Scannings.Name = "tabPage_Scannings";
			this.tabPage_Scannings.Padding = new Padding(3);
			this.tabPage_Scannings.Size = new System.Drawing.Size(582, 604);
			this.tabPage_Scannings.TabIndex = 1;
			this.tabPage_Scannings.Text = "扫描";
			this.tabPage_Scannings.UseVisualStyleBackColor = true;
			this.groupBox_UsbSave.Controls.Add(this.label_SelectFlash);
			this.groupBox_UsbSave.Controls.Add(this.UsbListComboBox);
			this.groupBox_UsbSave.Controls.Add(this.label_Remind);
			this.groupBox_UsbSave.Controls.Add(this.SelUdiskLabel);
			this.groupBox_UsbSave.Controls.Add(this.UsbSaveButton);
			this.groupBox_UsbSave.Location = new System.Drawing.Point(14, 483);
			this.groupBox_UsbSave.Name = "groupBox_UsbSave";
			this.groupBox_UsbSave.Size = new System.Drawing.Size(554, 100);
			this.groupBox_UsbSave.TabIndex = 26;
			this.groupBox_UsbSave.TabStop = false;
			this.groupBox_UsbSave.Text = "U盘通讯";
			this.label_SelectFlash.Location = new System.Drawing.Point(104, 18);
			this.label_SelectFlash.Name = "label_SelectFlash";
			this.label_SelectFlash.Size = new System.Drawing.Size(265, 15);
			this.label_SelectFlash.TabIndex = 24;
			this.UsbListComboBox.Cursor = Cursors.Default;
			this.UsbListComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.UsbListComboBox.FormattingEnabled = true;
			this.UsbListComboBox.ImeMode = ImeMode.On;
			this.UsbListComboBox.Location = new System.Drawing.Point(100, 41);
			this.UsbListComboBox.Name = "UsbListComboBox";
			this.UsbListComboBox.Size = new System.Drawing.Size(338, 20);
			this.UsbListComboBox.TabIndex = 20;
			this.UsbListComboBox.SelectedIndexChanged += new EventHandler(this.UsbListComboBox_SelectedIndexChanged);
			this.label_Remind.Location = new System.Drawing.Point(100, 69);
			this.label_Remind.Name = "label_Remind";
			this.label_Remind.Size = new System.Drawing.Size(332, 19);
			this.label_Remind.TabIndex = 23;
			this.SelUdiskLabel.Location = new System.Drawing.Point(6, 41);
			this.SelUdiskLabel.Name = "SelUdiskLabel";
			this.SelUdiskLabel.Size = new System.Drawing.Size(92, 17);
			this.SelUdiskLabel.TabIndex = 21;
			this.SelUdiskLabel.Text = "选择U盘：";
			this.SelUdiskLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.UsbSaveButton.Enabled = false;
			this.UsbSaveButton.Location = new System.Drawing.Point(444, 40);
			this.UsbSaveButton.Name = "UsbSaveButton";
			this.UsbSaveButton.Size = new System.Drawing.Size(64, 23);
			this.UsbSaveButton.TabIndex = 22;
			this.UsbSaveButton.Text = "保存";
			this.UsbSaveButton.UseVisualStyleBackColor = true;
			this.UsbSaveButton.Click += new EventHandler(this.UsbSaveButton_Click);
			this.groupBoxSend.Controls.Add(this.button_nextScan);
			this.groupBoxSend.Controls.Add(this.button_lastScan);
			this.groupBoxSend.Controls.Add(this.button_continue);
			this.groupBoxSend.Controls.Add(this.button_pause);
			this.groupBoxSend.Controls.Add(this.label_communication);
			this.groupBoxSend.Controls.Add(this.LblScan);
			this.groupBoxSend.Controls.Add(this.button_StopScan);
			this.groupBoxSend.Controls.Add(this.button_StartScan);
			this.groupBoxSend.Controls.Add(this.LblComm);
			this.groupBoxSend.Location = new System.Drawing.Point(14, 380);
			this.groupBoxSend.Name = "groupBoxSend";
			this.groupBoxSend.Size = new System.Drawing.Size(554, 97);
			this.groupBoxSend.TabIndex = 27;
			this.groupBoxSend.TabStop = false;
			this.groupBoxSend.Text = "发送数据";
			this.button_nextScan.Location = new System.Drawing.Point(374, 52);
			this.button_nextScan.Name = "button_nextScan";
			this.button_nextScan.Size = new System.Drawing.Size(64, 23);
			this.button_nextScan.TabIndex = 98;
			this.button_nextScan.Text = "下一个";
			this.button_nextScan.UseVisualStyleBackColor = true;
			this.button_nextScan.Click += new EventHandler(this.button_nextScan_Click);
			this.button_lastScan.Location = new System.Drawing.Point(304, 52);
			this.button_lastScan.Name = "button_lastScan";
			this.button_lastScan.Size = new System.Drawing.Size(64, 23);
			this.button_lastScan.TabIndex = 97;
			this.button_lastScan.Text = "上一个";
			this.button_lastScan.UseVisualStyleBackColor = true;
			this.button_lastScan.Click += new EventHandler(this.button_lastScan_Click);
			this.button_continue.Location = new System.Drawing.Point(234, 52);
			this.button_continue.Name = "button_continue";
			this.button_continue.Size = new System.Drawing.Size(64, 23);
			this.button_continue.TabIndex = 96;
			this.button_continue.Text = "继续";
			this.button_continue.UseVisualStyleBackColor = true;
			this.button_continue.Click += new EventHandler(this.button_continue_Click);
			this.button_pause.Location = new System.Drawing.Point(167, 52);
			this.button_pause.Name = "button_pause";
			this.button_pause.Size = new System.Drawing.Size(64, 23);
			this.button_pause.TabIndex = 95;
			this.button_pause.Text = "暂停";
			this.button_pause.UseVisualStyleBackColor = true;
			this.button_pause.Click += new EventHandler(this.button_pause_Click);
			this.label_communication.BackColor = System.Drawing.Color.Transparent;
			this.label_communication.Location = new System.Drawing.Point(170, 21);
			this.label_communication.Name = "label_communication";
			this.label_communication.Size = new System.Drawing.Size(215, 17);
			this.label_communication.TabIndex = 94;
			this.label_communication.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LblScan.Location = new System.Drawing.Point(6, 55);
			this.LblScan.Name = "LblScan";
			this.LblScan.Size = new System.Drawing.Size(92, 17);
			this.LblScan.TabIndex = 93;
			this.LblScan.Text = "扫描操作：";
			this.LblScan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button_StopScan.Location = new System.Drawing.Point(444, 52);
			this.button_StopScan.Name = "button_StopScan";
			this.button_StopScan.Size = new System.Drawing.Size(64, 23);
			this.button_StopScan.TabIndex = 92;
			this.button_StopScan.Text = "结束扫描";
			this.button_StopScan.UseVisualStyleBackColor = true;
			this.button_StopScan.Click += new EventHandler(this.button_StopScan_Click);
			this.button_StartScan.Location = new System.Drawing.Point(100, 52);
			this.button_StartScan.Name = "button_StartScan";
			this.button_StartScan.Size = new System.Drawing.Size(64, 23);
			this.button_StartScan.TabIndex = 91;
			this.button_StartScan.Text = "开始扫描";
			this.button_StartScan.UseVisualStyleBackColor = true;
			this.button_StartScan.Click += new EventHandler(this.button_StartScan_Click);
			this.LblComm.Location = new System.Drawing.Point(77, 21);
			this.LblComm.Name = "LblComm";
			this.LblComm.Size = new System.Drawing.Size(85, 17);
			this.LblComm.TabIndex = 90;
			this.LblComm.Text = "通讯模式：";
			this.LblComm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.groupBox2.Controls.Add(this.buttonLoad);
			this.groupBox2.Controls.Add(this.checkBox_Reverse);
			this.groupBox2.Controls.Add(this.GroupsOfScanRouting);
			this.groupBox2.Controls.Add(this.checkBox_SelectAll);
			this.groupBox2.Location = new System.Drawing.Point(14, 90);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(554, 280);
			this.groupBox2.TabIndex = 38;
			this.groupBox2.TabStop = false;
			this.buttonLoad.Location = new System.Drawing.Point(479, 11);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(64, 23);
			this.buttonLoad.TabIndex = 87;
			this.buttonLoad.Text = "加载";
			this.buttonLoad.UseVisualStyleBackColor = true;
			this.buttonLoad.Click += new EventHandler(this.buttonLoad_Click);
			this.checkBox_Reverse.AutoSize = true;
			this.checkBox_Reverse.Location = new System.Drawing.Point(108, 17);
			this.checkBox_Reverse.Name = "checkBox_Reverse";
			this.checkBox_Reverse.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Reverse.TabIndex = 7;
			this.checkBox_Reverse.Text = "反选";
			this.checkBox_Reverse.UseVisualStyleBackColor = true;
			this.checkBox_Reverse.CheckedChanged += new EventHandler(this.checkBox_Reverse_CheckedChanged);
			this.GroupsOfScanRouting.AllowUserToAddRows = false;
			this.GroupsOfScanRouting.AllowUserToResizeRows = false;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.GroupsOfScanRouting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.GroupsOfScanRouting.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.GroupsOfScanRouting.Columns.AddRange(new DataGridViewColumn[]
			{
				this.IsChecked,
				this.Index,
				this.ScanRouting,
				this.ColumnScanType,
				this.ColumnIndex
			});
			this.GroupsOfScanRouting.Location = new System.Drawing.Point(15, 40);
			this.GroupsOfScanRouting.Name = "GroupsOfScanRouting";
			this.GroupsOfScanRouting.RowHeadersVisible = false;
			this.GroupsOfScanRouting.RowTemplate.Height = 23;
			this.GroupsOfScanRouting.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.GroupsOfScanRouting.Size = new System.Drawing.Size(528, 234);
			this.GroupsOfScanRouting.TabIndex = 5;
			this.GroupsOfScanRouting.CellMouseClick += new DataGridViewCellMouseEventHandler(this.ScanRoutingMouseClick);
			this.IsChecked.HeaderText = "选择";
			this.IsChecked.Name = "IsChecked";
			this.IsChecked.Width = 60;
			this.Index.HeaderText = "序号";
			this.Index.Name = "Index";
			this.Index.Width = 60;
			this.ScanRouting.HeaderText = "走线方式";
			this.ScanRouting.Name = "ScanRouting";
			this.ScanRouting.Width = 405;
			this.ColumnScanType.HeaderText = "走线方式";
			this.ColumnScanType.Name = "ColumnScanType";
			this.ColumnScanType.Visible = false;
			this.ColumnScanType.Width = 5;
			this.ColumnIndex.HeaderText = "索引";
			this.ColumnIndex.Name = "ColumnIndex";
			this.ColumnIndex.Visible = false;
			this.ColumnIndex.Width = 5;
			this.checkBox_SelectAll.AutoSize = true;
			this.checkBox_SelectAll.Location = new System.Drawing.Point(16, 17);
			this.checkBox_SelectAll.Name = "checkBox_SelectAll";
			this.checkBox_SelectAll.Size = new System.Drawing.Size(48, 16);
			this.checkBox_SelectAll.TabIndex = 6;
			this.checkBox_SelectAll.Text = "全选";
			this.checkBox_SelectAll.UseVisualStyleBackColor = true;
			this.checkBox_SelectAll.CheckedChanged += new EventHandler(this.checkBox_SelectAll_CheckedChanged);
			this.groupBoxScans.Controls.Add(this.radioButtonScans10);
			this.groupBoxScans.Controls.Add(this.radioButtonAllScans);
			this.groupBoxScans.Controls.Add(this.radioButtonScans6);
			this.groupBoxScans.Controls.Add(this.radioButtonScans5);
			this.groupBoxScans.Controls.Add(this.radioButtonScans3);
			this.groupBoxScans.Controls.Add(this.radioButtonScansStatic);
			this.groupBoxScans.Controls.Add(this.radioButtonScans16);
			this.groupBoxScans.Controls.Add(this.radioButtonScans8);
			this.groupBoxScans.Controls.Add(this.radioButtonScans4);
			this.groupBoxScans.Controls.Add(this.radioButtonScans2);
			this.groupBoxScans.Location = new System.Drawing.Point(14, 8);
			this.groupBoxScans.Name = "groupBoxScans";
			this.groupBoxScans.Size = new System.Drawing.Size(554, 76);
			this.groupBoxScans.TabIndex = 4;
			this.groupBoxScans.TabStop = false;
			this.groupBoxScans.Text = "扫描方式";
			this.radioButtonAllScans.AutoSize = true;
			this.radioButtonAllScans.Location = new System.Drawing.Point(483, 49);
			this.radioButtonAllScans.Name = "radioButtonAllScans";
			this.radioButtonAllScans.Size = new System.Drawing.Size(47, 16);
			this.radioButtonAllScans.TabIndex = 8;
			this.radioButtonAllScans.TabStop = true;
			this.radioButtonAllScans.Text = "全部";
			this.radioButtonAllScans.UseVisualStyleBackColor = true;
			this.radioButtonAllScans.CheckedChanged += new EventHandler(this.radioButtonAllScans_CheckedChanged);
			this.radioButtonScans6.AutoSize = true;
			this.radioButtonScans6.Location = new System.Drawing.Point(43, 49);
			this.radioButtonScans6.Name = "radioButtonScans6";
			this.radioButtonScans6.Size = new System.Drawing.Size(41, 16);
			this.radioButtonScans6.TabIndex = 7;
			this.radioButtonScans6.TabStop = true;
			this.radioButtonScans6.Text = "1/6";
			this.radioButtonScans6.UseVisualStyleBackColor = true;
			this.radioButtonScans6.CheckedChanged += new EventHandler(this.radioButtonScans6_CheckedChanged);
			this.radioButtonScans5.AutoSize = true;
			this.radioButtonScans5.Location = new System.Drawing.Point(483, 21);
			this.radioButtonScans5.Name = "radioButtonScans5";
			this.radioButtonScans5.Size = new System.Drawing.Size(41, 16);
			this.radioButtonScans5.TabIndex = 6;
			this.radioButtonScans5.TabStop = true;
			this.radioButtonScans5.Text = "1/5";
			this.radioButtonScans5.UseVisualStyleBackColor = true;
			this.radioButtonScans5.CheckedChanged += new EventHandler(this.radioButtonScans5_CheckedChanged);
			this.radioButtonScans3.AutoSize = true;
			this.radioButtonScans3.Location = new System.Drawing.Point(263, 21);
			this.radioButtonScans3.Name = "radioButtonScans3";
			this.radioButtonScans3.Size = new System.Drawing.Size(41, 16);
			this.radioButtonScans3.TabIndex = 5;
			this.radioButtonScans3.TabStop = true;
			this.radioButtonScans3.Text = "1/3";
			this.radioButtonScans3.UseVisualStyleBackColor = true;
			this.radioButtonScans3.CheckedChanged += new EventHandler(this.radioButtonScans3_CheckedChanged);
			this.radioButtonScansStatic.AutoSize = true;
			this.radioButtonScansStatic.Location = new System.Drawing.Point(43, 21);
			this.radioButtonScansStatic.Name = "radioButtonScansStatic";
			this.radioButtonScansStatic.Size = new System.Drawing.Size(47, 16);
			this.radioButtonScansStatic.TabIndex = 4;
			this.radioButtonScansStatic.TabStop = true;
			this.radioButtonScansStatic.Text = "静态";
			this.radioButtonScansStatic.UseVisualStyleBackColor = true;
			this.radioButtonScansStatic.CheckedChanged += new EventHandler(this.radioButtonScansStatic_CheckedChanged);
			this.radioButtonScans16.AutoSize = true;
			this.radioButtonScans16.Location = new System.Drawing.Point(373, 49);
			this.radioButtonScans16.Name = "radioButtonScans16";
			this.radioButtonScans16.Size = new System.Drawing.Size(47, 16);
			this.radioButtonScans16.TabIndex = 3;
			this.radioButtonScans16.TabStop = true;
			this.radioButtonScans16.Text = "1/16";
			this.radioButtonScans16.UseVisualStyleBackColor = true;
			this.radioButtonScans16.CheckedChanged += new EventHandler(this.radioButtonScans16_CheckedChanged);
			this.radioButtonScans8.AutoSize = true;
			this.radioButtonScans8.Location = new System.Drawing.Point(153, 49);
			this.radioButtonScans8.Name = "radioButtonScans8";
			this.radioButtonScans8.Size = new System.Drawing.Size(41, 16);
			this.radioButtonScans8.TabIndex = 2;
			this.radioButtonScans8.TabStop = true;
			this.radioButtonScans8.Text = "1/8";
			this.radioButtonScans8.UseVisualStyleBackColor = true;
			this.radioButtonScans8.CheckedChanged += new EventHandler(this.radioButtonScans8_CheckedChanged);
			this.radioButtonScans4.AutoSize = true;
			this.radioButtonScans4.Location = new System.Drawing.Point(373, 21);
			this.radioButtonScans4.Name = "radioButtonScans4";
			this.radioButtonScans4.Size = new System.Drawing.Size(41, 16);
			this.radioButtonScans4.TabIndex = 1;
			this.radioButtonScans4.TabStop = true;
			this.radioButtonScans4.Text = "1/4";
			this.radioButtonScans4.UseVisualStyleBackColor = true;
			this.radioButtonScans4.CheckedChanged += new EventHandler(this.radioButtonScans4_CheckedChanged);
			this.radioButtonScans2.AutoSize = true;
			this.radioButtonScans2.Location = new System.Drawing.Point(153, 21);
			this.radioButtonScans2.Name = "radioButtonScans2";
			this.radioButtonScans2.Size = new System.Drawing.Size(41, 16);
			this.radioButtonScans2.TabIndex = 0;
			this.radioButtonScans2.TabStop = true;
			this.radioButtonScans2.Text = "1/2";
			this.radioButtonScans2.UseVisualStyleBackColor = true;
			this.radioButtonScans2.CheckedChanged += new EventHandler(this.radioButtonScans2_CheckedChanged);
			this.timer1.Tick += new EventHandler(this.timer1_Tick_1);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem_SelectRouting,
				this.toolStripMenuItem_Cancel
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
			this.toolStripMenuItem_SelectRouting.Name = "toolStripMenuItem_SelectRouting";
			this.toolStripMenuItem_SelectRouting.Size = new System.Drawing.Size(100, 22);
			this.toolStripMenuItem_SelectRouting.Text = "选择";
			this.toolStripMenuItem_SelectRouting.Click += new EventHandler(this.toolStripMenuItem_SelectRouting_Click);
			this.toolStripMenuItem_Cancel.Name = "toolStripMenuItem_Cancel";
			this.toolStripMenuItem_Cancel.Size = new System.Drawing.Size(100, 22);
			this.toolStripMenuItem_Cancel.Text = "取消";
			this.toolStripMenuItem_Cancel.Click += new EventHandler(this.toolStripMenuItem_Cancel_Click);
			this.radioButtonScan10.AutoSize = true;
			this.radioButtonScan10.Location = new System.Drawing.Point(224, 144);
			this.radioButtonScan10.Name = "radioButtonScan10";
			this.radioButtonScan10.Size = new System.Drawing.Size(59, 16);
			this.radioButtonScan10.TabIndex = 48;
			this.radioButtonScan10.TabStop = true;
			this.radioButtonScan10.Text = "1/10扫";
			this.radioButtonScan10.UseVisualStyleBackColor = true;
			this.radioButtonScan10.CheckedChanged += new EventHandler(this.radioButtonScan10_CheckedChanged);
			this.radioButtonScans10.AutoSize = true;
			this.radioButtonScans10.Location = new System.Drawing.Point(263, 49);
			this.radioButtonScans10.Name = "radioButtonScans10";
			this.radioButtonScans10.Size = new System.Drawing.Size(47, 16);
			this.radioButtonScans10.TabIndex = 9;
			this.radioButtonScans10.TabStop = true;
			this.radioButtonScans10.Text = "1/10";
			this.radioButtonScans10.UseVisualStyleBackColor = true;
			this.radioButtonScans10.CheckedChanged += new EventHandler(this.radioButtonScans10_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(604, 649);
			base.Controls.Add(this.tabControl_ScanRouting);
			base.Controls.Add(this.routingRefreshScreenComboBox);
			base.Controls.Add(this.routingParamGroupBox);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formRouting";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "设置走线方式";
			base.FormClosed += new FormClosedEventHandler(this.formRouting_close);
			base.Load += new EventHandler(this.formRouting_Load);
			base.SizeChanged += new EventHandler(this.formRouting_SizeChanged);
			base.TextChanged += new EventHandler(this.formRouting_TextChanged);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.routingParamGroupBox.ResumeLayout(false);
			this.routingParamGroupBox.PerformLayout();
			this.selectRoutingLabel1.ResumeLayout(false);
			this.selectRoutingLabel1.PerformLayout();
			this.tabControl_ScanRouting.ResumeLayout(false);
			this.tabPage_ToSelectScanRouting.ResumeLayout(false);
			this.tabPage_Scannings.ResumeLayout(false);
			this.groupBox_UsbSave.ResumeLayout(false);
			this.groupBoxSend.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.GroupsOfScanRouting).EndInit();
			this.groupBoxScans.ResumeLayout(false);
			this.groupBoxScans.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
