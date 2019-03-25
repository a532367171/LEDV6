using LedCommunication;
using LedControlSystem.Cloud;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Cloud;
using LedModel.Const;
using LedModel.Data;
using LedModel.Enum;
using LedModel.Foundation;
using NativeWifi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem.LedControlSystem
{
	public class formPanelEdit : Form
	{
		private formMain mf;

		private static string formID = "formpaneledit";

		private static bool isLoaded = false;

		private bool OEChanged;

		private bool DataChanged;

		private bool isFromFile;

		private bool isGroup;

		private LedPanel thisPanel;

		private LedPanelCloud thisPanelCloud;

		private TreeNode tnNoGroup;

		private bool isLoadParam;

		private int widthModuleUnit = 16;

		private int heightModuleUnit = 8;

		private int lastScanTypeIndex = -1;

		private int lastRoutingIndex = -1;

		private LedColorMode lastColorMode = LedColorMode.R;

		private bool ModelChanged;

		private bool isWidthModuleKeyDown;

		private bool isHeightModuleKeyDown;

		private bool isNetworkChanged;

		private bool isIPPortChanged;

		private bool isIPAddressChanged;

		private bool isPasswordChanged;

		private bool isMacAddressChanged;

		private bool isCloudIPAddressChanged;

		private bool isCloudIPPortChanged;

		private IList<string> UdiskDirList;

		private string LastMessage = "";

		private int WarningLevel;

		public bool IsConnectedToCorrectWifi;

		private string NowWifiName = string.Empty;

		private WlanClient client;

		private IContainer components;

		private TreeView tvwPanel;

		private ComboBox comboBoxColorMode;

		private Label labelType;

		private ComboBox comboBoxModel;

		private Label label3;

		private Label label2;

		private NumericUpDown numericUpDownHeigth;

		private NumericUpDown numericUpDownWidth;

		private Label label1;

		private TextBox textBoxDescription;

		private Label label4;

		private Label label6;

		private ComboBox comboBoxScanType;

		private Label label7;

		private TextBox textBoxRouting;

		private GroupBox groupBox2;

		private Button manualRoutingSettingButton;

		private RadioButton radioButtonEnthernet;

		private RadioButton radioButtonSerial;

		private Label lblIPAddress;

		private Label label8;

		private Button btnReadPanelParameter;

		private Label onlineVersion;

		private Label label11;

		private Button buttonSave;

		private Button btnLoadPanelParameter;

		private NumericUpDown numericUpDownPanelAddress;

		private ComboBox comboBoxBaudrate;

		private ComboBox comboBoxCom;

		private ContextMenuStrip cmsPanel;

		private ToolStripMenuItem tsmiMoveToGroup;

		private Label productMessageLabel;

		private Label ELMessageLabel;

		private Label label12;

		private ComboBox comboBoxGradation;

		private Label label13;

		private ComboBox comboBoxOE;

		private Label Label_Data;

		private ComboBox comboBoxData;

		private Label label1_Express;

		private ListBox listBox_Express;

		private Button button_Advanced;

		private CheckBox checkBox_OE;

		private CheckBox checkBox_Data;

		private ComboBox comboBox_Express;

		private PictureBox pictureBox2;

		private Label label14_OE;

		private Button button_Add_Advance;

		private Button button_Add_Advance_Mac;

		private Panel panel_Serial;

		private Label label_Baudrate;

		private Label label_PortName;

		private Panel panel_Net;

		private RadioButton radioButtonWifi;

		private Panel panel_USB;

		private Panel panel_Wifi;

		private Button button_Net_Advance;

		private RadioButton radioButtonUSB;

		private RadioButton radioButton_SendBroadcast;

		private RadioButton radioButton_SendByIP;

		private RadioButton radioButtonGPRS;

		private Panel panel_GPRS;

		private Label LblGPRSMessage;

		private GroupBox zhGroupBox2;

		private GroupBox zhGroupBox3;

		private GroupBox zhGroupBox1;

		private Button button_ChangeWifiPassword;

		private Button button_ChangeWifiSSID;

		private Label lblWidthModule;

		private Label lblHeightModule;

		private NumericUpDown nudHeigthModule;

		private NumericUpDown nudWidthModule;

		private RadioButton radioButton_RemoteServer;

		private RadioButton radioButton_localServer;

		private GroupBox zhGroupBoxButton;

		private TextBox txtNetworkID;

		private Label lblNetworkID;

		private TextBox txtIPPort;

		private Label lblPort;

		private TextBox txtPassword;

		private Label lblPassword;

		private Label lblMacAddress;

		private TextBox txtMacAddress;

		private Panel panel_Set;

		private Button Delete_GroupOrPanelButton;

		private Button Add_Group_Button;

		private Button Add_Panel_Button;

		private ToolTip toolTip_panel;

		private ImageList ImgSelect;

		private Label label_SelectUSB;

		private ComboBox UsbListComboBox;

		private Label label_Remind;

		private Panel panel1;

		private Label LblControlCardState;

		private Label LblWifiName;

		private TextBox TxtWifiName;

		private System.Windows.Forms.Timer timerUsbSelect;

		private System.Windows.Forms.Timer timerWifiConnectionStatus;

		private TextBox txtIPAddress;

		private Button btn_GPRS_Net_Advance;

		private Label lblCloudServerUserName;

		private TextBox txtCloudServerUserName;

		private TextBox txtGPRSCloudServerUserName;

		private Label lblGPRSCloudServerUserName;

		private TextBox txtDeviceID;

		private Label lblDeviceID;

		private TextBox txtGPRSDeviceID;

		private Label lblGPRSDeviceID;

		public static string FormID
		{
			get
			{
				return formPanelEdit.formID;
			}
			set
			{
				formPanelEdit.formID = value;
			}
		}

		private bool IsPanelCloud
		{
			get
			{
				return this.thisPanel != null && this.thisPanel.GetType() == typeof(LedPanelCloud);
			}
		}

		public formPanelEdit(formMain pMainForm)
		{
			this.InitializeComponent();
			this.mf = pMainForm;
			formMain.ML.NowFormID = formPanelEdit.formID;
			this.Text = formMain.ML.GetStr("formpaneledit_FormText");
			this.label_SelectUSB.Text = formMain.ML.GetStr("formUSBWrite_label_SelUdisk");
			this.LblWifiName.Text = formMain.ML.GetStr("formpaneledit_label_WiFi_Name");
			this.zhGroupBox1.Text = formMain.ML.GetStr("formpaneledit_GroupBox_Product_Specifications");
			this.label11.Text = formMain.ML.GetStr("formpaneledit_label_OnlineVersionNumber");
			this.btnReadPanelParameter.Text = formMain.ML.GetStr("formpaneledit_button_ReadScreenParameters");
			this.labelType.Text = formMain.ML.GetStr("formpaneledit_label_Type");
			this.label1.Text = formMain.ML.GetStr("formpaneledit_label_ScreenDescription");
			this.label2.Text = formMain.ML.GetStr("formpaneledit_label_width");
			this.label3.Text = formMain.ML.GetStr("formpaneledit_label_Height");
			this.lblWidthModule.Text = formMain.ML.GetStr("formpaneledit_lblWidthModule");
			this.lblHeightModule.Text = formMain.ML.GetStr("formpaneledit_lblHeightModule");
			this.label1_Express.Text = formMain.ML.GetStr("formpaneledit_label_ShortcutConfiguration");
			this.label7.Text = formMain.ML.GetStr("formpaneledit_label_Traces");
			this.button_Advanced.Text = formMain.ML.GetStr("formpaneledit_button_Advanced");
			this.label14_OE.Text = formMain.ML.GetStr("formpaneledit_label_Display_OE");
			this.Label_Data.Text = formMain.ML.GetStr("formpaneledit_label_Data");
			this.label4.Text = formMain.ML.GetStr("formpaneledit_label_ColorMode");
			this.label12.Text = formMain.ML.GetStr("formpaneledit_label_GrayscaleLevel");
			this.groupBox2.Text = formMain.ML.GetStr("formpaneledit_groupBox_CommunicationMethod");
			this.radioButtonSerial.Text = formMain.ML.GetStr("formpaneledit_radioButtonSerial");
			this.radioButtonEnthernet.Text = formMain.ML.GetStr("formpaneledit_radioButtonEnthernet");
			this.radioButtonUSB.Text = formMain.ML.GetStr("formpaneledit_radioButtonUSB");
			this.radioButtonWifi.Text = formMain.ML.GetStr("formpaneledit_radioButtonWifi");
			this.label_PortName.Text = formMain.ML.GetStr("formpaneledit_label_SerialPortName");
			this.label_Baudrate.Text = formMain.ML.GetStr("formpaneledit_label_Baudrate");
			this.label8.Text = formMain.ML.GetStr("formpaneledit_label8");
			this.button_Add_Advance.Text = formMain.ML.GetStr("formpaneledit_button_Add_Advance");
			this.button_ChangeWifiPassword.Text = formMain.ML.GetStr("formpaneledit_button_ChangeWifiPassword");
			this.button_ChangeWifiSSID.Text = formMain.ML.GetStr("formpaneledit_button_ChangeWifiSSID");
			this.LblGPRSMessage.Text = formMain.ML.GetStr("formpaneledit_label_prompt_GPRS_Send");
			this.lblGPRSCloudServerUserName.Text = formMain.ML.GetStr("formpaneledit_label_GPRS_CloudServerUserName");
			this.lblGPRSDeviceID.Text = formMain.ML.GetStr("formpaneledit_label_GPRS_DeviceID");
			this.btn_GPRS_Net_Advance.Text = formMain.ML.GetStr("formpaneledit_button_GPRS_Net_Advance");
			this.radioButton_SendBroadcast.Text = formMain.ML.GetStr("formpaneledit_radioButton_SendBroadcast");
			this.radioButton_SendByIP.Text = formMain.ML.GetStr("formpaneledit_radioButton_SendByIP");
			this.radioButton_localServer.Text = formMain.ML.GetStr("formpaneledit_radioButton_localServer");
			this.radioButtonGPRS.Text = formMain.ML.GetStr("formpaneledit_radioButtonGPRS");
			this.lblIPAddress.Text = formMain.ML.GetStr("formpaneledit_label_IPAddress");
			this.lblPort.Text = formMain.ML.GetStr("formpaneledit_label_Port");
			this.lblNetworkID.Text = formMain.ML.GetStr("formpaneledit_label_NetworkID");
			this.lblPassword.Text = formMain.ML.GetStr("formpaneledit_label_Password");
			this.lblCloudServerUserName.Text = formMain.ML.GetStr("formpaneledit_label_CloudServerUserName");
			this.lblDeviceID.Text = formMain.ML.GetStr("formpaneledit_label_DeviceID");
			this.button_Net_Advance.Text = formMain.ML.GetStr("formpaneledit_button_Net_Advance");
			this.lblMacAddress.Text = formMain.ML.GetStr("formpaneledit_label_MacAddress");
			this.radioButton_RemoteServer.Text = formMain.ML.GetStr("formpaneledit_radioButton_RemoteServer");
			this.button_Add_Advance_Mac.Text = formMain.ML.GetStr("formpaneledit_button_Add_Advance_Mac");
			this.btnLoadPanelParameter.Text = formMain.ML.GetStr("formpaneledit_button_Load");
			this.buttonSave.Text = formMain.ML.GetStr("formpaneledit_button_Save");
			this.label6.Text = formMain.ML.GetStr("formpaneledit_label_ScanningMethod");
			this.tsmiMoveToGroup.Text = formMain.ML.GetStr("formpaneledit_ToolStripMenuItem_Regroup");
			this.toolTip_panel.InitialDelay = 100;
			this.toolTip_panel.ReshowDelay = 100;
			this.toolTip_panel.SetToolTip(this.Add_Group_Button, formMain.ML.GetStr("formpaneledit_ToolStripMenuItem_ADDGroup"));
			this.toolTip_panel.SetToolTip(this.Add_Panel_Button, formMain.ML.GetStr("formpaneledit_ToolStripMenuItem_ADDPanel"));
			this.toolTip_panel.SetToolTip(this.Delete_GroupOrPanelButton, formMain.ML.GetStr("formpaneledit_ToolStripMenuItem_DeletePanel"));
			formMain.str_item_comboBox(this.comboBox_Express, "formpaneledit_comboBox_Express", null);
			formMain.str_item_comboBox(this.comboBoxOE, "formpaneledit_comboBoxOE", null);
			formMain.str_item_comboBox(this.comboBoxData, "formpaneledit_comboBoxData", null);
			formMain.str_item_comboBox(this.comboBoxGradation, "formpaneledit_comboBoxGradation", null);
			for (int i = this.comboBoxGradation.Items.Count - 1; i > 2; i--)
			{
				this.comboBoxGradation.Items.RemoveAt(i);
			}
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.comboBox_Express);
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.comboBoxOE);
			formPanelEdit.AdjustComboBoxDropDownListWidth(this.comboBoxData);
		}

		public formPanelEdit()
		{
			this.InitializeComponent();
		}

		private void SaveWithAndHeight()
		{
		}

		private void Add_Panel_Button_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwPanel.SelectedNode;
			if (selectedNode != null && selectedNode.Tag != null && (selectedNode.Tag.GetType() == typeof(LedGroupCloud) || selectedNode.Tag.GetType() == typeof(LedPanelCloud)))
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Cloud_Group_Cannot_Be_Added_By_Manual"));
				return;
			}
			LedPanel ledPanel = new LedPanel();
			if (formMain.IsforeignTradeMode)
			{
				ledPanel.CardType = LedCardType.ZH_5U1;
				ledPanel.Zoom = 3m;
			}
			this.mf.SetPanelPromptLanguage(Settings.Default.Language, ledPanel);
			formMain.Ledsys.Panels.Add(ledPanel);
			int arg_BF_0 = formMain.ledsys.Panels.Count;
			string str = formMain.ML.GetStr("Display_PanelAbbr");
			for (int i = 1; i < 9999; i++)
			{
				bool flag = false;
				string text = str + i.ToString();
				for (int j = 0; j < formMain.ledsys.Panels.Count; j++)
				{
					if (formMain.ledsys.Panels[j].ValueName == text)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ledPanel.TextName = text;
					ledPanel.ValueName = text;
					break;
				}
			}
			if (selectedNode != null && selectedNode.Tag != null)
			{
				if (selectedNode.Tag.GetType() == typeof(LedGroup))
				{
					ledPanel.Group = ((LedGroup)selectedNode.Tag).ID;
				}
				else
				{
					TreeNode parent = selectedNode.Parent;
					if (parent != null && parent.Tag.GetType() == typeof(LedGroup))
					{
						ledPanel.Group = ((LedGroup)parent.Tag).ID;
					}
				}
			}
			this.isGroup = false;
			this.SetControlEnabled();
			formMain.AddPanelToIPCServer(ledPanel);
			this.LoadPanelToTree(ledPanel);
			this.thisPanel = ledPanel;
			this.LoadPanelParam();
		}

		private string GetPanelDescrible(LedPanel pPenel)
		{
			string result;
			try
			{
				string text = string.Concat(new string[]
				{
					pPenel.TextName,
					"_",
					pPenel.CardType.ToString().Replace("_", "-"),
					"_1/",
					pPenel.RoutingSetting.ScanTypeIndex.ToString(),
					"_"
				});
				if (pPenel.PortType == LedPortType.SerialPort)
				{
					text = text + pPenel.SerialPortName + "/" + pPenel.BaudRate.ToString();
				}
				else if (pPenel.PortType == LedPortType.Ethernet)
				{
					if (pPenel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.Directly)
					{
						text += "Ethernet";
					}
					if (pPenel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.FixedIP)
					{
						text += "Ethernet_";
						text += pPenel.IPAddress.ToString();
					}
					if (pPenel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.LocalServer)
					{
						text += "Ethernet_";
						text += pPenel.NetworkID.ToString();
					}
					if (pPenel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer)
					{
						text += "CloudServer";
					}
				}
				else if (pPenel.PortType == LedPortType.USB)
				{
					text += "USB";
				}
				else if (pPenel.PortType == LedPortType.WiFi)
				{
					text += "WIFI";
				}
				else
				{
					text += "GPRS";
				}
				result = text;
			}
			catch
			{
				result = pPenel.TextName + "_" + pPenel.CardType.ToString().Replace("_", "-");
			}
			return result;
		}

		private void LoadGroup()
		{
			if (formMain.isUseGroup)
			{
				this.tvwPanel.Nodes.Clear();
				LedProject ledsys = formMain.ledsys;
				if (ledsys.Groups != null && ledsys.Groups.Count > 0)
				{
					int i = 0;
					while (i < formMain.ledsys.Groups.Count)
					{
						LedGroup ledGroup = formMain.ledsys.Groups[i];
						if ((LedGlobal.CloudAccount != null && !string.IsNullOrEmpty(LedGlobal.CloudAccount.UserName)) || ledsys.Cloud.LoginState != LedCloudLoginState.Login || string.IsNullOrEmpty(ledsys.Cloud.Account.UserName) || !(ledsys.Cloud.Account.UserName == ledGroup.Name) || ledGroup.CreationMethod != LedCreationMethod.Cloud)
						{
							goto IL_127;
						}
						bool flag = false;
						foreach (LedPanel current in ledsys.Panels)
						{
							if (current.GetType() == typeof(LedPanel) && current.Group == ledGroup.ID)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							goto IL_127;
						}
						IL_347:
						i++;
						continue;
						IL_127:
						TreeNode treeNode = new TreeNode();
						if (ledGroup.CreationMethod == LedCreationMethod.Cloud && LedGlobal.CloudAccount != null && !string.IsNullOrEmpty(LedGlobal.CloudAccount.UserName))
						{
							string arg = string.Empty;
							LedGroupCloud ledGroupCloud = (LedGroupCloud)ledGroup;
							ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
							ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(formMain.ML.GetStr("formMain_TreeView_Node_ContextMenuStrip_Item_Cloud_Group_All"));
							if (ledGroupCloud.SelectedIndex == -2)
							{
								toolStripMenuItem.Checked = true;
								arg = toolStripMenuItem.Text;
							}
							toolStripMenuItem.Tag = 0;
							toolStripMenuItem.Click += new EventHandler(this.tsmiGroupCloud_Click);
							contextMenuStrip.Items.Add(toolStripMenuItem);
							ToolStripSeparator value = new ToolStripSeparator();
							contextMenuStrip.Items.Add(value);
							ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(formMain.ML.GetStr("formMain_TreeView_Node_ContextMenuStrip_Item_Cloud_Group_Ungroup"));
							if (ledGroupCloud.SelectedIndex == -1)
							{
								toolStripMenuItem2.Checked = true;
								arg = toolStripMenuItem2.Text;
							}
							toolStripMenuItem2.Tag = ledGroupCloud.ID;
							toolStripMenuItem2.Click += new EventHandler(this.tsmiGroupCloud_Click);
							contextMenuStrip.Items.Add(toolStripMenuItem2);
							ToolStripSeparator value2 = new ToolStripSeparator();
							contextMenuStrip.Items.Add(value2);
							if (ledGroupCloud.Subgroups != null)
							{
								int num = 0;
								foreach (LedGroup current2 in ledGroupCloud.Subgroups)
								{
									ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(current2.Name);
									if (ledGroupCloud.SelectedIndex == num)
									{
										toolStripMenuItem3.Checked = true;
										arg = toolStripMenuItem3.Text;
									}
									toolStripMenuItem3.Tag = current2.ID;
									toolStripMenuItem3.Click += new EventHandler(this.tsmiGroupCloud_Click);
									contextMenuStrip.Items.Add(toolStripMenuItem3);
									num++;
								}
							}
							treeNode.Text = string.Format("{0}({1})", ledGroup.Name, arg);
							treeNode.ContextMenuStrip = contextMenuStrip;
						}
						else
						{
							treeNode.Text = ledGroup.Name;
						}
						treeNode.Tag = ledGroup;
						this.tvwPanel.Nodes.Add(treeNode);
						goto IL_347;
					}
					return;
				}
				LedGroup ledGroup2 = new LedGroup();
				ledGroup2.Name = formMain.ML.GetStr("Display_Panel_NoGroup");
				ledGroup2.Description = ledGroup2.Name;
				ledsys.AddGroup(ledGroup2);
				this.tnNoGroup = new TreeNode();
				this.tnNoGroup.Text = ledGroup2.Name;
				this.tnNoGroup.Tag = ledGroup2;
				this.tvwPanel.Nodes.Add(this.tnNoGroup);
			}
		}

		private void tsmiGroupCloud_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwPanel.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
			if (toolStripMenuItem.Checked)
			{
				return;
			}
			if (selectedNode.Tag.GetType() != typeof(LedGroupCloud))
			{
				return;
			}
			LedGroupCloud ledGroupCloud = (LedGroupCloud)selectedNode.Tag;
			string text = string.Empty;
			if (toolStripMenuItem.Tag != null)
			{
				string text2 = toolStripMenuItem.Tag.ToString();
				if (text2 != "0")
				{
					text = text2;
				}
			}
			selectedNode.Nodes.Clear();
			foreach (LedPanel current in formMain.ledsys.Panels)
			{
				if (!(current.GetType() != typeof(LedPanelCloud)))
				{
					LedPanelCloud ledPanelCloud = (LedPanelCloud)current;
					if (string.IsNullOrEmpty(text) || ledPanelCloud.Group.Contains(text))
					{
						TreeNode treeNode = new TreeNode(this.GetPanelDescrible(ledPanelCloud));
						treeNode.Tag = ledPanelCloud;
						selectedNode.Nodes.Add(treeNode);
					}
				}
			}
			ledGroupCloud.SelectedGroup = null;
			if (string.IsNullOrEmpty(text))
			{
				ledGroupCloud.SelectedIndex = -2;
			}
			else if (text == ledGroupCloud.ID)
			{
				ledGroupCloud.SelectedIndex = -1;
			}
			else if (ledGroupCloud.Subgroups != null)
			{
				for (int i = 0; i < ledGroupCloud.Subgroups.Count; i++)
				{
					if (ledGroupCloud.Subgroups[i].ID == text)
					{
						ledGroupCloud.SelectedIndex = i;
						ledGroupCloud.SelectedGroup = ledGroupCloud.Subgroups[i];
						break;
					}
				}
			}
			ContextMenuStrip contextMenuStrip = selectedNode.ContextMenuStrip;
			if (contextMenuStrip != null)
			{
				foreach (ToolStripItem toolStripItem in contextMenuStrip.Items)
				{
					if (toolStripItem.GetType() == typeof(ToolStripMenuItem))
					{
						((ToolStripMenuItem)toolStripItem).Checked = false;
					}
				}
			}
			toolStripMenuItem.Checked = true;
			selectedNode.Text = string.Format("{0}({1})", ledGroupCloud.Name, toolStripMenuItem.Text);
			selectedNode.Expand();
		}

		private void PanelGroup_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
			LedGroup ledGroup = (LedGroup)toolStripMenuItem.Tag;
			foreach (TreeNode treeNode in this.tvwPanel.Nodes)
			{
				if (treeNode.Tag.Equals(ledGroup))
				{
					TreeNode selectedNode = this.tvwPanel.SelectedNode;
					this.tvwPanel.SelectedNode.Parent.Nodes.Remove(selectedNode);
					this.thisPanel.Group = ledGroup.Name;
					treeNode.Nodes.Add(selectedNode);
					break;
				}
			}
		}

		private void comboBoxType_DrawItem(object sender, DrawItemEventArgs e)
		{
			System.Drawing.Graphics arg_06_0 = e.Graphics;
			System.Drawing.Rectangle bounds = e.Bounds;
			ComboBox comboBox = (ComboBox)sender;
			if (e.Index < comboBox.Items.Count)
			{
				int arg_31_0 = e.Index;
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				switch (e.Index)
				{
				case 0:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height - 2));
					break;
				case 1:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 2, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width / 2, bounds.Y + 1, bounds.Width / 2, bounds.Height - 2));
					break;
				case 2:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 2, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width / 2, bounds.Y + 1, bounds.Width / 2, bounds.Height - 2));
					break;
				case 3:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 4:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 5:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 6:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 7:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 8:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				}
				e.DrawFocusRectangle();
			}
		}

		private void CheckWidthAndHeight()
		{
		}

		private int getMaxHeightByModel(LedCardType pModel)
		{
			foreach (LedCard current in LedGlobal.LedCardList)
			{
				if (pModel == current.CardType)
				{
					return current.MaxHeight;
				}
			}
			return 128;
		}

		private void SetHeightAndWidthStepByRoutingSetting(LedRoutingSetting pSetting)
		{
			if (this.thisPanel != null)
			{
				pSetting.SettingFileName = LedCommonConst.RoutingSettingFileName;
				if (this.thisPanel.IsLSeries())
				{
					pSetting.SettingFileName = LedCommonConst.RoutingSettingLFileName;
				}
			}
			pSetting.LoadScanFromFile();
			this.numericUpDownHeigth.Minimum = pSetting.DataRows;
			this.numericUpDownHeigth.Increment = pSetting.DataRows;
			this.numericUpDownWidth.Minimum = pSetting.UnitWidth;
			this.numericUpDownWidth.Increment = pSetting.UnitWidth;
			if (this.numericUpDownHeigth.Increment == 0m)
			{
				this.numericUpDownHeigth.Increment = 8m;
			}
			if (this.numericUpDownWidth.Increment == 0m)
			{
				this.numericUpDownWidth.Increment = 16m;
			}
		}

		private void SetHeightAndWidthModule(LedCardType pModel)
		{
			int value = 4;
			int value2 = 21;
			foreach (LedCard current in LedGlobal.LedCardList)
			{
				if (pModel == current.CardType)
				{
					value = current.MaxHeight / this.heightModuleUnit;
					value2 = current.MaxWidth / this.widthModuleUnit;
				}
			}
			this.nudHeigthModule.Minimum = 1m;
			this.nudHeigthModule.Increment = 1m;
			this.nudHeigthModule.Maximum = value;
			this.nudWidthModule.Minimum = 1m;
			this.nudWidthModule.Increment = 1m;
			this.nudWidthModule.Maximum = value2;
		}

		public void ModelCommunicationCorrect(string ledModel)
		{
			if (formMain.IsforeignTradeMode)
			{
				if (ledModel.IndexOf("5U") > -1 && (this.thisPanel.PortType == LedPortType.GPRS || this.thisPanel.PortType == LedPortType.WiFi || this.thisPanel.PortType == LedPortType.Ethernet))
				{
					this.thisPanel.PortType = LedPortType.USB;
				}
				if (ledModel.IndexOf("5W") > -1 && (this.thisPanel.PortType == LedPortType.GPRS || this.thisPanel.PortType == LedPortType.SerialPort || this.thisPanel.PortType == LedPortType.Ethernet))
				{
					this.thisPanel.PortType = LedPortType.WiFi;
				}
				if (ledModel.IndexOf("5G") > -1 && (this.thisPanel.PortType == LedPortType.WiFi || this.thisPanel.PortType == LedPortType.Ethernet))
				{
					this.thisPanel.PortType = LedPortType.GPRS;
				}
				if (ledModel.IndexOf("E") > -1 && ledModel.IndexOf("L") > -1 && (this.thisPanel.PortType == LedPortType.WiFi || this.thisPanel.PortType == LedPortType.GPRS))
				{
					this.thisPanel.PortType = LedPortType.USB;
				}
				if (ledModel.IndexOf("M") > -1 && ledModel.IndexOf("L") > -1 && (this.thisPanel.PortType == LedPortType.GPRS || this.thisPanel.PortType == LedPortType.WiFi || this.thisPanel.PortType == LedPortType.Ethernet || this.thisPanel.PortType == LedPortType.SerialPort))
				{
					this.thisPanel.PortType = LedPortType.USB;
				}
			}
		}

		private void LoadPanelParam()
		{
			if (this.thisPanel == null)
			{
				return;
			}
			int num = 0;
			try
			{
				string text = this.thisPanel.CardType.ToString();
				int scanTypeIndex = this.thisPanel.RoutingSetting.ScanTypeIndex;
				int routingIndex = this.thisPanel.RoutingSetting.RoutingIndex;
				bool flag = false;
				this.isLoadParam = true;
				num = 1;
				this.btnLoadPanelParameter.Enabled = false;
				this.radioButtonWifi.Visible = false;
				this.radioButtonEnthernet.Visible = false;
				this.radioButtonUSB.Visible = false;
				this.radioButtonGPRS.Visible = false;
				this.radioButtonSerial.Visible = false;
				this.numericUpDownWidth.Width = 129;
				this.numericUpDownHeigth.Width = 129;
				this.numericUpDownWidth.Enabled = true;
				this.numericUpDownHeigth.Enabled = true;
				this.nudWidthModule.Visible = false;
				this.nudHeigthModule.Visible = false;
				this.lblWidthModule.Visible = false;
				this.lblHeightModule.Visible = false;
				this.comboBox_Express.Enabled = true;
				this.comboBoxColorMode.Enabled = true;
				this.label12.Visible = false;
				this.comboBoxGradation.Visible = false;
				this.label_Remind.Text = string.Empty;
				this.HideControls();
				num = 2;
				this.ModelCommunicationCorrect(text);
				if (text.IndexOf("U") > 0)
				{
					this.radioButtonSerial.Visible = true;
					this.radioButtonUSB.Visible = true;
					if (text.IndexOf("Uc") > 0 || text.IndexOf("Um") > 0 || text.IndexOf("Un") > 0 || text.IndexOf("YU") > 0 || (text.IndexOf("G") > 0 && text.IndexOf("L") > 0))
					{
						this.radioButtonSerial.Visible = false;
					}
				}
				if (text.IndexOf("E") > 0)
				{
					this.radioButtonSerial.Visible = true;
					this.radioButtonUSB.Visible = true;
					this.radioButtonEnthernet.Visible = true;
					this.radioButtonEnthernet.BringToFront();
				}
				if (text.IndexOf("W") > 0)
				{
					if (this.thisPanel.PortType == LedPortType.SerialPort || this.thisPanel.PortType == LedPortType.GPRS || this.thisPanel.PortType == LedPortType.Ethernet)
					{
						this.thisPanel.PortType = LedPortType.USB;
					}
					this.radioButtonUSB.Visible = true;
					this.radioButtonWifi.Visible = true;
				}
				if (text.IndexOf("D") > 0)
				{
					this.numericUpDownWidth.Width /= 2;
					this.numericUpDownHeigth.Width /= 2;
					this.numericUpDownWidth.Enabled = false;
					this.numericUpDownHeigth.Enabled = false;
					this.nudWidthModule.Visible = true;
					this.nudHeigthModule.Visible = true;
					this.lblWidthModule.Visible = true;
					this.lblHeightModule.Visible = true;
					this.comboBox_Express.Enabled = false;
					this.comboBoxColorMode.Enabled = false;
					this.radioButtonSerial.Visible = true;
					this.radioButtonUSB.Visible = true;
					flag = true;
				}
				if (text.IndexOf("M") > 0)
				{
					this.label12.Visible = true;
					this.comboBoxGradation.Visible = true;
					this.radioButtonUSB.Visible = true;
				}
				if (text.IndexOf("S") > 0)
				{
					this.comboBox_Express.Enabled = false;
					this.comboBoxColorMode.Enabled = false;
					this.radioButtonSerial.Visible = true;
					this.radioButtonUSB.Visible = true;
				}
				if (text.IndexOf("PP") > 0)
				{
					this.radioButtonUSB.Visible = true;
					this.radioButtonWifi.Visible = true;
					this.btnLoadPanelParameter.Enabled = true;
				}
				if (text.IndexOf("A") > 0)
				{
					this.radioButtonSerial.Visible = true;
				}
				if (text.IndexOf("G") > 0)
				{
					if (text.IndexOf("L") > 0)
					{
						this.thisPanel.GPRSCommunicaitonMode = LedGPRSCommunicationMode.CloudServer;
					}
					if (this.thisPanel.GPRSCommunicaitonMode == LedGPRSCommunicationMode.GprsServer)
					{
						this.radioButtonSerial.Visible = true;
					}
					this.radioButtonUSB.Visible = true;
					this.radioButtonGPRS.Visible = true;
					this.radioButtonGPRS.BringToFront();
				}
				if (text.IndexOf("YU") > 0)
				{
					this.radioButtonSerial.Visible = false;
					this.radioButtonUSB.Visible = true;
					this.radioButtonUSB.BringToFront();
					this.radioButtonGPRS.Visible = true;
					this.radioButtonGPRS.BringToFront();
				}
				num = 4;
				this.comboBoxModel.SelectedIndex = LedCommon.GetCardListIndex(this.thisPanel.CardType);
				if (this.thisPanel.IsLSeries() || this.thisPanel.CardType.ToString().IndexOf("YU") > 0)
				{
					this.thisPanel.ProtocolVersion = LedCommunicationConst.ProtocolSendVersionList[LedCommunicationConst.ProtocolSendVersionList.Length - 1];
				}
				this.lastScanTypeIndex = scanTypeIndex;
				this.lastRoutingIndex = routingIndex;
				this.lastColorMode = this.thisPanel.ColorMode;
				num = 5;
				this.textBoxDescription.Text = this.thisPanel.TextName;
				num = 6;
				this.SetHeightAndWidthStepByRoutingSetting(this.thisPanel.RoutingSetting);
				scanTypeIndex = this.thisPanel.RoutingSetting.ScanTypeIndex;
				routingIndex = this.thisPanel.RoutingSetting.RoutingIndex;
				this.numericUpDownWidth.Maximum = this.thisPanel.GetMaxWidth();
				this.numericUpDownHeigth.Maximum = this.getMaxHeightByModel(this.thisPanel.CardType);
				this.thisPanel.Height = this.thisPanel.Height / (int)this.numericUpDownHeigth.Increment * (int)this.numericUpDownHeigth.Increment;
				this.thisPanel.Width = this.thisPanel.Width / (int)this.numericUpDownWidth.Increment * (int)this.numericUpDownWidth.Increment;
				if (this.thisPanel.Height > this.numericUpDownHeigth.Maximum)
				{
					this.thisPanel.Height = (int)this.numericUpDownHeigth.Maximum;
				}
				if (this.thisPanel.Width > this.numericUpDownWidth.Maximum)
				{
					this.thisPanel.Width = (int)this.numericUpDownWidth.Maximum;
				}
				if (this.thisPanel.Width * this.thisPanel.Height > this.thisPanel.GetMaxArea())
				{
					this.thisPanel.Width = this.thisPanel.GetMaxArea() / this.thisPanel.Height / (int)this.numericUpDownWidth.Increment * (int)this.numericUpDownWidth.Increment;
				}
				if (this.thisPanel.Width < (int)this.numericUpDownWidth.Increment)
				{
					this.thisPanel.Width = (int)this.numericUpDownWidth.Increment;
				}
				if (this.thisPanel.Height < (int)this.numericUpDownHeigth.Increment)
				{
					this.thisPanel.Height = (int)this.numericUpDownHeigth.Increment;
				}
				this.numericUpDownWidth.Value = this.thisPanel.Width;
				this.numericUpDownHeigth.Value = this.thisPanel.Height;
				if (flag)
				{
					this.SetHeightAndWidthModule(this.thisPanel.CardType);
					int value = this.thisPanel.Height / this.heightModuleUnit;
					int value2 = this.thisPanel.Width / this.widthModuleUnit;
					if (value > this.nudHeigthModule.Maximum)
					{
						this.nudHeigthModule.Value = this.nudHeigthModule.Maximum;
					}
					if (value2 > this.nudWidthModule.Maximum)
					{
						this.nudWidthModule.Value = this.nudWidthModule.Maximum;
					}
					value = this.thisPanel.Height / this.heightModuleUnit;
					value2 = this.thisPanel.Width / this.widthModuleUnit;
					this.nudWidthModule.Value = value2;
					this.nudHeigthModule.Value = value;
				}
				num = 7;
				if (scanTypeIndex == 4 && routingIndex == 0)
				{
					this.comboBox_Express.SelectedIndex = 0;
				}
				else if (scanTypeIndex == 4 && routingIndex == 35)
				{
					this.comboBox_Express.SelectedIndex = 1;
				}
				else if (scanTypeIndex == 1 && routingIndex == 1)
				{
					this.comboBox_Express.SelectedIndex = 2;
				}
				else if (scanTypeIndex == 1 && routingIndex == 2)
				{
					this.comboBox_Express.SelectedIndex = 3;
				}
				else if (scanTypeIndex == 4 && routingIndex == 1)
				{
					this.comboBox_Express.SelectedIndex = 4;
				}
				else if (scanTypeIndex == 16 && routingIndex == 0)
				{
					this.comboBox_Express.SelectedIndex = 5;
				}
				else if (scanTypeIndex == 4 && routingIndex == 12)
				{
					this.comboBox_Express.SelectedIndex = 6;
				}
				else if (scanTypeIndex == 3 && routingIndex == 0)
				{
					this.comboBox_Express.SelectedIndex = 7;
				}
				else if (scanTypeIndex == 16 && routingIndex == 1)
				{
					this.comboBox_Express.SelectedIndex = 8;
				}
				else if (scanTypeIndex == 4 && routingIndex == 36)
				{
					this.comboBox_Express.SelectedIndex = 9;
				}
				else
				{
					this.comboBox_Express.SelectedIndex = -1;
				}
				num = 8;
				this.textBoxRouting.Text = formRouting.GetRoutingString(scanTypeIndex, routingIndex);
				num = 9;
				this.thisPanel.RoutingSetting.SettingFileName = LedCommonConst.RoutingSettingFileName;
				if (this.thisPanel.IsLSeries())
				{
					this.thisPanel.RoutingSetting.SettingFileName = LedCommonConst.RoutingSettingLFileName;
				}
				byte[] array = this.thisPanel.RoutingSetting.LoadScanFromFile();
				if (this.isFromFile && !this.OEChanged && !this.ModelChanged)
				{
					if (array != null && array.Length > 0 && array[0] == 1)
					{
						this.thisPanel.OEPolarity = 1;
						this.comboBoxOE.SelectedIndex = 1;
						this.checkBox_OE.Checked = true;
					}
					else
					{
						this.thisPanel.OEPolarity = 0;
						this.comboBoxOE.SelectedIndex = 0;
						this.checkBox_OE.Checked = false;
					}
				}
				else
				{
					this.comboBoxOE.SelectedIndex = (int)this.thisPanel.OEPolarity;
					this.checkBox_OE.Checked = Convert.ToBoolean(this.thisPanel.OEPolarity);
				}
				num = 10;
				if (this.isFromFile && !this.DataChanged && !this.ModelChanged)
				{
					if (array != null && array.Length > 1 && array[1] == 1)
					{
						this.thisPanel.DataPolarity = 1;
						this.comboBoxData.SelectedIndex = 1;
						this.checkBox_Data.Checked = true;
					}
					else
					{
						this.thisPanel.DataPolarity = 0;
						this.comboBoxData.SelectedIndex = 0;
						this.checkBox_Data.Checked = false;
					}
				}
				else
				{
					this.isFromFile = true;
					this.ModelChanged = false;
					this.comboBoxData.SelectedIndex = (int)this.thisPanel.DataPolarity;
					this.checkBox_Data.Checked = Convert.ToBoolean(this.thisPanel.DataPolarity);
				}
				num = 11;
				this.comboBoxColorMode.SelectedIndex = this.thisPanel.ColorMode - LedColorMode.R;
				num = 12;
				this.comboBoxGradation.SelectedIndex = this.thisPanel.ForeGray - 1;
				num = 13;
				switch (this.thisPanel.PortType)
				{
				case LedPortType.SerialPort:
					this.radioButtonSerial.Checked = true;
					this.panel_Serial.BringToFront();
					this.panel_Serial.Enabled = true;
					this.btnLoadPanelParameter.Enabled = true;
					break;
				case LedPortType.Ethernet:
					this.txtIPAddress.Text = this.thisPanel.IPAddress;
					this.txtMacAddress.Text = this.thisPanel.MACAddress;
					this.txtIPPort.Text = this.thisPanel.IPPort.ToString();
					this.txtNetworkID.Text = this.thisPanel.NetworkID;
					this.txtPassword.Text = this.thisPanel.AuthorityPassword;
					this.txtCloudServerUserName.Text = this.thisPanel.CloudServerUserName;
					this.txtDeviceID.Text = this.thisPanel.DeviceID;
					this.txtPassword.Enabled = Convert.ToBoolean((int)this.thisPanel.AuthorityPasswordMode);
					if (this.thisPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.Directly)
					{
						this.radioButton_SendBroadcast.Checked = true;
						this.Broadcast_display();
					}
					else if (this.thisPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.FixedIP)
					{
						this.radioButton_SendByIP.Checked = true;
						this.SendByIP_display();
					}
					else if (this.thisPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.LocalServer)
					{
						this.radioButton_localServer.Checked = true;
						this.localServer_display();
					}
					else
					{
						this.radioButton_RemoteServer.Checked = true;
						this.CloudServer_display();
					}
					if ((!(this.thisPanel.GetType() == typeof(LedPanel)) || this.thisPanel.EthernetCommunicaitonMode != LedEthernetCommunicationMode.CloudServer) && (!this.IsPanelCloud || this.thisPanel.State != LedPanelState.Offline))
					{
						this.btnLoadPanelParameter.Enabled = true;
					}
					this.radioButtonEnthernet.Checked = true;
					this.panel_Net.BringToFront();
					break;
				case LedPortType.USB:
					this.panel_USB.BringToFront();
					this.label_SelectUSB.Visible = false;
					this.UsbListComboBox.Visible = false;
					this.label_Remind.Visible = false;
					this.panel_USB.Visible = true;
					this.panel_USB.BringToFront();
					this.radioButtonUSB.Checked = true;
					break;
				case LedPortType.WiFi:
					this.LblWifiName.Visible = false;
					this.TxtWifiName.Visible = false;
					this.LblControlCardState.Visible = false;
					this.panel_Wifi.Visible = true;
					this.panel_Wifi.BringToFront();
					this.radioButtonWifi.Checked = true;
					break;
				case LedPortType.GPRS:
					this.radioButtonGPRS.Checked = true;
					this.txtGPRSCloudServerUserName.Text = this.thisPanel.CloudServerUserName;
					this.txtGPRSDeviceID.Text = this.thisPanel.DeviceID;
					this.panel_GPRS.BringToFront();
					if (this.thisPanel.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer)
					{
						this.LblGPRSMessage.Text = formMain.ML.GetStr("formpaneledit_label_prompt_CloudServer_Send");
						this.btn_GPRS_Net_Advance.Visible = true;
						this.lblGPRSCloudServerUserName.Visible = true;
						this.lblGPRSDeviceID.Visible = true;
						this.txtGPRSCloudServerUserName.Visible = true;
						this.txtGPRSDeviceID.Visible = true;
						if (this.thisPanel.GetType() != typeof(LedPanel) && this.thisPanel.State == LedPanelState.Online)
						{
							this.btnLoadPanelParameter.Enabled = true;
						}
					}
					else
					{
						this.LblGPRSMessage.Text = formMain.ML.GetStr("formpaneledit_label_prompt_GPRS_Send");
						this.btn_GPRS_Net_Advance.Visible = false;
						this.lblGPRSCloudServerUserName.Visible = false;
						this.lblGPRSDeviceID.Visible = false;
						this.txtGPRSCloudServerUserName.Visible = false;
						this.txtGPRSDeviceID.Visible = false;
					}
					break;
				}
				num = 14;
				this.numericUpDownPanelAddress.Value = this.thisPanel.CardAddress;
				this.comboBoxBaudrate.Text = this.thisPanel.BaudRate.ToString();
				formPanelEdit.isLoaded = false;
				if (this.comboBoxCom.Items.Count > 0)
				{
					string serialPortName = this.thisPanel.SerialPortName;
					int num2 = -1;
					for (int i = 0; i < this.comboBoxCom.Items.Count; i++)
					{
						string b = this.comboBoxCom.Items[i].ToString();
						if (serialPortName == b)
						{
							num2 = i;
							break;
						}
					}
					this.comboBoxCom.SelectedIndex = ((num2 > -1) ? num2 : 0);
					if (num2 == -1)
					{
						this.thisPanel.SerialPortName = this.comboBoxCom.Text;
					}
				}
				num = 15;
				if (this.thisPanel.MACAddress.Length < 12)
				{
					this.thisPanel.MACAddress = this.thisPanel.MACAddress.PadLeft(12, 'F');
				}
				num = 16;
				string a;
				if ((a = text) != null)
				{
					if (a == "ZH_U0")
					{
						this.pictureBox2.Image = Resources.V3_ZH_U0;
						goto IL_10F1;
					}
					if (a == "ZH_U1")
					{
						this.pictureBox2.Image = Resources.V3_ZH_U1;
						goto IL_10F1;
					}
					if (a == "ZH_U2")
					{
						this.pictureBox2.Image = Resources.V3_ZH_U2;
						goto IL_10F1;
					}
					if (a == "ZH_U3")
					{
						this.pictureBox2.Image = Resources.V3_ZH_U3;
						goto IL_10F1;
					}
				}
				this.pictureBox2.Image = null;
				IL_10F1:
				if (this.pictureBox2.Image != null)
				{
					if (this.pictureBox2.Image.Width < this.pictureBox2.Width && this.pictureBox2.Image.Height < this.pictureBox2.Height)
					{
						this.pictureBox2.SizeMode = PictureBoxSizeMode.Normal;
					}
					else
					{
						this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
					}
				}
				num = 17;
				if (this.thisPanel.IsLSeries() && this.thisPanel.CardType.ToString().IndexOf("ZH_E") > -1)
				{
					string text2 = this.thisPanel.CardType.ToString().Replace("_", "-");
					this.ELMessageLabel.Text = string.Format(formMain.ML.GetStr("formpaneledit_message_LmodeDescription"), new object[]
					{
						text2,
						text2.ToString().Replace("L", ""),
						text2.ToString().Replace("L", ""),
						text2.ToString().Replace("L", "")
					});
				}
				else
				{
					this.ELMessageLabel.Text = "";
				}
				this.productMessageLabel.Text = formMain.GetProductMessageByModel(this.thisPanel.CardType, this.thisPanel.IsLSeries());
				this.ELMessageLabel.ForeColor = System.Drawing.Color.Red;
				this.isLoadParam = false;
				num = 18;
				if (this.thisPanel.IsLSeries())
				{
					this.radioButton_localServer.Visible = true;
				}
				else
				{
					this.radioButton_localServer.Visible = false;
				}
				if (this.radioButtonUSB.Checked || this.radioButtonGPRS.Checked)
				{
					this.btnReadPanelParameter.Enabled = false;
				}
				else if (this.IsPanelCloud)
				{
					this.btnReadPanelParameter.Enabled = false;
				}
				else
				{
					this.btnReadPanelParameter.Enabled = true;
				}
				this.onlineVersion.Text = string.Concat(new object[]
				{
					this.thisPanel.CardType.ToString().Replace("_", "-"),
					" V",
					this.thisPanel.MainVersion,
					".",
					this.thisPanel.HardwareVersion.ToString("D2"),
					".",
					this.thisPanel.ProgramVersion.ToString("D2")
				});
				if (this.tvwPanel != null && this.tvwPanel.Nodes.Count > 0)
				{
					string panelDescrible = this.GetPanelDescrible(this.thisPanel);
					TreeNode treeNode = null;
					foreach (TreeNode tnParent in this.tvwPanel.Nodes)
					{
						treeNode = this.FindNode(tnParent, this.thisPanel);
						if (treeNode != null)
						{
							break;
						}
					}
					if (treeNode != null)
					{
						treeNode.Text = panelDescrible;
						if (this.tvwPanel.SelectedNode != treeNode)
						{
							this.tvwPanel.SelectedNode = treeNode;
						}
					}
					else if (this.tvwPanel.SelectedNode != null)
					{
						this.tvwPanel.SelectedNode.Text = panelDescrible;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message + "," + num.ToString());
			}
		}

		private void LoadPanelCloudParam()
		{
			if (this.thisPanelCloud == null)
			{
				return;
			}
			this.isLoadParam = true;
			int selectedIndex = this.comboBoxColorMode.SelectedIndex;
			int arg_27_0 = this.comboBox_Express.SelectedIndex;
			int scanTypeIndex = this.thisPanelCloud.RoutingSetting.ScanTypeIndex;
			int routingIndex = this.thisPanelCloud.RoutingSetting.RoutingIndex;
			if (scanTypeIndex == 4 && routingIndex == 0)
			{
				this.comboBox_Express.SelectedIndex = 0;
			}
			else if (scanTypeIndex == 4 && routingIndex == 35)
			{
				this.comboBox_Express.SelectedIndex = 1;
			}
			else if (scanTypeIndex == 1 && routingIndex == 1)
			{
				this.comboBox_Express.SelectedIndex = 2;
			}
			else if (scanTypeIndex == 1 && routingIndex == 2)
			{
				this.comboBox_Express.SelectedIndex = 3;
			}
			else if (scanTypeIndex == 4 && routingIndex == 1)
			{
				this.comboBox_Express.SelectedIndex = 4;
			}
			else if (scanTypeIndex == 16 && routingIndex == 0)
			{
				this.comboBox_Express.SelectedIndex = 5;
			}
			else if (scanTypeIndex == 4 && routingIndex == 12)
			{
				this.comboBox_Express.SelectedIndex = 6;
			}
			else if (scanTypeIndex == 3 && routingIndex == 0)
			{
				this.comboBox_Express.SelectedIndex = 7;
			}
			else if (scanTypeIndex == 16 && routingIndex == 1)
			{
				this.comboBox_Express.SelectedIndex = 8;
			}
			else if (scanTypeIndex == 4 && routingIndex == 36)
			{
				this.comboBox_Express.SelectedIndex = 9;
			}
			else
			{
				this.comboBox_Express.SelectedIndex = -1;
			}
			LedColorMode colorMode = selectedIndex + LedColorMode.R;
			this.thisPanelCloud.ColorMode = colorMode;
			this.textBoxRouting.Text = formRouting.GetRoutingString(scanTypeIndex, routingIndex);
			this.thisPanelCloud.RoutingSetting.SettingFileName = LedCommonConst.RoutingSettingFileName;
			if (this.thisPanelCloud.IsLSeries())
			{
				this.thisPanelCloud.RoutingSetting.SettingFileName = LedCommonConst.RoutingSettingLFileName;
			}
			byte[] array = this.thisPanelCloud.RoutingSetting.LoadScanFromFile();
			if (!this.OEChanged)
			{
				if (array[0] == 1)
				{
					this.comboBoxOE.SelectedIndex = 1;
					this.checkBox_OE.Checked = true;
				}
				else
				{
					this.comboBoxOE.SelectedIndex = 0;
					this.checkBox_OE.Checked = false;
				}
			}
			if (!this.DataChanged)
			{
				if (array[1] == 1)
				{
					this.comboBoxData.SelectedIndex = 1;
					this.checkBox_Data.Checked = true;
				}
				else
				{
					this.comboBoxData.SelectedIndex = 0;
					this.checkBox_Data.Checked = false;
				}
			}
			int num = (int)this.numericUpDownWidth.Value;
			int num2 = (int)this.numericUpDownHeigth.Value;
			int maxWidth = this.thisPanelCloud.GetMaxWidth();
			int maxHeight = this.thisPanelCloud.GetMaxHeight();
			int maxArea = this.thisPanelCloud.GetMaxArea();
			this.numericUpDownWidth.Minimum = this.thisPanelCloud.RoutingSetting.UnitWidth;
			this.numericUpDownWidth.Increment = this.thisPanelCloud.RoutingSetting.UnitWidth;
			this.numericUpDownWidth.Maximum = maxWidth;
			this.numericUpDownHeigth.Minimum = this.thisPanelCloud.RoutingSetting.DataRows;
			this.numericUpDownHeigth.Increment = this.thisPanelCloud.RoutingSetting.DataRows;
			this.numericUpDownHeigth.Maximum = maxHeight;
			if (this.numericUpDownWidth.Increment == 0m)
			{
				this.numericUpDownWidth.Increment = 16m;
			}
			if (this.numericUpDownHeigth.Increment == 0m)
			{
				this.numericUpDownHeigth.Increment = 8m;
			}
			if (maxWidth < num)
			{
				num = maxWidth;
			}
			else if (num < (int)this.numericUpDownWidth.Increment)
			{
				num = (int)this.numericUpDownWidth.Increment;
			}
			if (maxHeight < num2)
			{
				num2 = maxHeight;
			}
			else if (num2 < (int)this.numericUpDownHeigth.Increment)
			{
				num2 = (int)this.numericUpDownHeigth.Increment;
			}
			if (num * num2 > maxArea)
			{
				num = maxArea / num2 / (int)this.numericUpDownWidth.Increment * (int)this.numericUpDownWidth.Increment;
			}
			this.numericUpDownWidth.Value = num;
			this.numericUpDownHeigth.Value = num2;
			if (this.tvwPanel != null && this.tvwPanel.Nodes.Count > 0)
			{
				string panelDescrible = this.GetPanelDescrible(this.thisPanel);
				TreeNode treeNode = null;
				foreach (TreeNode tnParent in this.tvwPanel.Nodes)
				{
					treeNode = this.FindNode(tnParent, this.thisPanel);
					if (treeNode != null)
					{
						break;
					}
				}
				if (treeNode != null)
				{
					treeNode.Text = panelDescrible;
					if (this.tvwPanel.SelectedNode != treeNode)
					{
						this.tvwPanel.SelectedNode = treeNode;
					}
				}
				else if (this.tvwPanel.SelectedNode != null)
				{
					this.tvwPanel.SelectedNode.Text = panelDescrible;
				}
			}
			this.isLoadParam = false;
		}

		private void SyncPanelCloud(LedPanel panel)
		{
			if (panel.GetType() == typeof(LedPanelCloud))
			{
				if (this.thisPanelCloud == null)
				{
					this.thisPanelCloud = new LedPanelCloud();
				}
				this.thisPanelCloud.Copy(panel);
				this.thisPanelCloud.DeviceID = panel.DeviceID;
				this.thisPanelCloud.CloudID = panel.CloudID;
				this.thisPanelCloud.CloudServerUserName = panel.CloudServerUserName;
			}
		}

		private TreeNode FindNode(TreeNode tnParent, string strValue)
		{
			if (tnParent == null)
			{
				return null;
			}
			if (tnParent.Text == strValue)
			{
				return tnParent;
			}
			if (tnParent.Nodes.Count == 0)
			{
				return null;
			}
			TreeNode treeNode = tnParent;
			TreeNode treeNode2 = treeNode.FirstNode;
			IL_F6:
			while (treeNode2 != null && treeNode2 != tnParent)
			{
				while (treeNode2 != null)
				{
					string[] array = treeNode2.Text.Split(new char[]
					{
						'_'
					});
					string[] array2 = strValue.Split(new char[]
					{
						'_'
					});
					if (array.Length >= 3 && array2.Length >= 3 && array[0] == array2[0] && array[1] == array2[1])
					{
						treeNode2.Text = strValue;
						return treeNode2;
					}
					if (treeNode2.Text == strValue)
					{
						return treeNode2;
					}
					if (treeNode2.Nodes.Count > 0)
					{
						treeNode = treeNode2;
						treeNode2 = treeNode2.FirstNode;
					}
					else if (treeNode2 != treeNode.LastNode)
					{
						treeNode2 = treeNode2.NextNode;
					}
					else
					{
						//IL_DE:
						while (treeNode2 != tnParent && treeNode2 == treeNode.LastNode)
						{
							treeNode2 = treeNode;
							treeNode = treeNode.Parent;
						}
						if (treeNode2 != tnParent)
						{
							treeNode2 = treeNode2.NextNode;
							goto IL_F6;
						}
						goto IL_F6;
					}
				}
				//goto IL_DE;
			}
			return null;
		}

		private TreeNode FindNode(TreeNode tnParent, LedPanel panel)
		{
			TreeNode result = null;
			if (tnParent == null || tnParent.Tag == null)
			{
				return result;
			}
			if (panel == null)
			{
				return result;
			}
			if ((tnParent.GetType() == typeof(LedPanel) || tnParent.GetType() == typeof(LedPanelCloud)) && ((LedPanel)tnParent.Tag).ID == panel.ID)
			{
				result = tnParent;
				return result;
			}
			if (tnParent.Nodes.Count == 0)
			{
				return result;
			}
			foreach (TreeNode treeNode in tnParent.Nodes)
			{
				if (treeNode != null && treeNode.Tag != null && (!(treeNode.Tag.GetType() != typeof(LedPanel)) || !(treeNode.Tag.GetType() != typeof(LedPanelCloud))))
				{
					LedPanel ledPanel = treeNode.Tag as LedPanel;
					if (ledPanel.ID == panel.ID)
					{
						result = treeNode;
					}
				}
			}
			return result;
		}

		private void HideControls()
		{
		}

		private void SavePanelParam()
		{
			if (this.isLoadParam)
			{
				return;
			}
			if (this.thisPanel != null)
			{
				this.thisPanel.TextName = this.textBoxDescription.Text;
				if (!string.IsNullOrEmpty(this.comboBoxCom.Text))
				{
					this.thisPanel.SerialPortName = this.comboBoxCom.Text;
				}
				this.thisPanel.ColorMode = this.comboBoxColorMode.SelectedIndex + LedColorMode.R;
				if (this.radioButtonEnthernet.Checked)
				{
					this.thisPanel.PortType = LedPortType.Ethernet;
					this.SetIPAddress();
					this.SetMacAddress();
				}
				if (this.radioButtonSerial.Checked)
				{
					this.thisPanel.PortType = LedPortType.SerialPort;
				}
			}
			this.ReLoadPanel();
		}

		private void SetMacAddress()
		{
			if (string.IsNullOrEmpty(formPanelEdit.FromStringToMac(this.txtMacAddress.Text)))
			{
				MessageBox.Show(this, formMain.GetStr("NETCARD_message_SetMac_Error"));
				this.txtMacAddress.Focus();
			}
		}

		private void SetIPAddress()
		{
			if (!Regex.IsMatch(this.txtIPAddress.Text.Trim(), "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$"))
			{
				MessageBox.Show(this, formMain.GetStr("Prompt_WrongIP"));
				this.txtIPAddress.Focus();
			}
		}

		private void LoadPanelToTree(LedPanel lp)
		{
			TreeNode treeNode = new TreeNode();
			if (lp.GetType() == typeof(LedPanel))
			{
				treeNode.ContextMenuStrip = this.cmsPanel;
			}
			treeNode.Text = this.GetPanelDescrible(lp);
			treeNode.Tag = lp;
			if (formMain.isUseGroup)
			{
				bool flag = false;
				foreach (TreeNode treeNode2 in this.tvwPanel.Nodes)
				{
					try
					{
						if (treeNode2.Tag != null)
						{
							if (treeNode2.Tag.GetType() != typeof(LedGroup) && treeNode2.Tag.GetType() != typeof(LedGroupCloud))
							{
								return;
							}
							if (lp.GetType() == typeof(LedPanelCloud))
							{
								LedPanelCloud ledPanelCloud = (LedPanelCloud)lp;
								if (treeNode2.Tag.GetType() == typeof(LedGroupCloud))
								{
									LedGroupCloud ledGroupCloud = (LedGroupCloud)treeNode2.Tag;
									string text = string.Empty;
									if (ledGroupCloud.SelectedIndex == -1)
									{
										text = ledGroupCloud.ID;
									}
									else if (ledGroupCloud.SelectedIndex > -1 && ledGroupCloud.SelectedGroup != null)
									{
										text = ledGroupCloud.SelectedGroup.ID;
									}
									if (string.IsNullOrEmpty(text) || ledPanelCloud.Group.Contains(text))
									{
										treeNode2.Nodes.Add(treeNode);
										flag = true;
										treeNode2.Expand();
									}
								}
							}
							else if (((LedGroup)treeNode2.Tag).ID.Equals(lp.Group))
							{
								treeNode2.Nodes.Add(treeNode);
								flag = true;
								treeNode2.Expand();
							}
						}
					}
					catch
					{
					}
				}
				if (!flag)
				{
					if (this.tnNoGroup != null && this.tnNoGroup.Tag != null && this.tnNoGroup.Tag.GetType() == typeof(LedGroup))
					{
						lp.Group = ((LedGroup)this.tnNoGroup.Tag).ID;
					}
					if (this.tnNoGroup != null)
					{
						this.tnNoGroup.Nodes.Add(treeNode);
					}
				}
			}
			else
			{
				this.tvwPanel.Nodes.Add(treeNode);
			}
			this.tvwPanel.SelectedNode = treeNode;
		}

		private void tvwPanel_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (this.tvwPanel.GetNodeAt(e.Location) == null)
				{
					return;
				}
				if (this.tvwPanel.GetNodeAt(e.Location).Equals(this.tvwPanel.SelectedNode))
				{
					return;
				}
				this.tvwPanel.SelectedNode = this.tvwPanel.GetNodeAt(e.Location);
				TreeNode selectedNode = this.tvwPanel.SelectedNode;
				if (formMain.isUseGroup)
				{
					if (selectedNode.Level == 1)
					{
						this.thisPanel = (LedPanel)selectedNode.Tag;
						this.SyncPanelCloud(this.thisPanel);
						this.isGroup = false;
						this.isFromFile = false;
						this.OEChanged = false;
						this.DataChanged = false;
					}
					else
					{
						this.isGroup = true;
					}
				}
				else
				{
					this.thisPanel = (LedPanel)selectedNode.Tag;
					this.SyncPanelCloud(this.thisPanel);
					this.isGroup = false;
					this.isFromFile = false;
					this.OEChanged = false;
					this.DataChanged = false;
				}
				this.onlineVersion.Text = "--";
				this.SetControlEnabled();
			}
			catch
			{
				return;
			}
			if (!this.isGroup)
			{
				bool isPanelCloud = this.IsPanelCloud;
				this.comboBoxModel.Enabled = !isPanelCloud;
				this.textBoxDescription.Enabled = !isPanelCloud;
				this.groupBox2.Enabled = !isPanelCloud;
				this.panel_Net.Enabled = !isPanelCloud;
				this.panel_GPRS.Enabled = !isPanelCloud;
				this.LoadPanelParam();
				this.Panel_Change_MACandIPDisplay();
			}
		}

		public void Panel_Change_MACandIPDisplay()
		{
			try
			{
				IPAddress none = IPAddress.None;
				string ipString = this.txtIPAddress.Text.Trim();
				if (IPAddress.TryParse(ipString, out none))
				{
					this.txtIPAddress.ForeColor = System.Drawing.Color.Black;
				}
				else
				{
					this.txtIPAddress.ForeColor = System.Drawing.Color.Red;
				}
			}
			catch
			{
			}
			try
			{
				string value = formPanelEdit.FromStringToMac(this.txtMacAddress.Text.Trim());
				if (string.IsNullOrEmpty(value))
				{
					this.txtMacAddress.ForeColor = System.Drawing.Color.Red;
				}
				else
				{
					this.txtMacAddress.ForeColor = System.Drawing.Color.Black;
				}
			}
			catch
			{
			}
		}

		private void tvwPanel_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;
		}

		private void SetControlEnabled()
		{
			this.zhGroupBox1.Enabled = !this.isGroup;
			foreach (Control control in this.zhGroupBox3.Controls)
			{
				if (control.Name != "zhGroupBoxButton")
				{
					control.Enabled = !this.isGroup;
				}
			}
			if (this.isGroup)
			{
				this.btnLoadPanelParameter.Enabled = false;
			}
		}

		private void Delete_GroupOrPanelButton_Click(object sender, EventArgs e)
		{
			LedProject ledsys = formMain.ledsys;
			if (ledsys == null)
			{
				return;
			}
			TreeNode selectedNode = this.tvwPanel.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			if (selectedNode.Tag == null)
			{
				return;
			}
			TreeNode prevNode = selectedNode.PrevNode;
			TreeNode nextNode = selectedNode.NextNode;
			TreeNode treeNode = null;
			if (selectedNode.Tag.GetType() == typeof(LedGroupCloud))
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Cannot_Delete_The_Group"));
				return;
			}
			if (selectedNode.Tag.GetType() == typeof(LedGroup))
			{
				if (ledsys.Groups.Count == 1)
				{
					return;
				}
				LedGroup ledGroup = (LedGroup)selectedNode.Tag;
				IList<LedPanel> list = new List<LedPanel>();
				for (int i = 0; i < ledsys.Panels.Count; i++)
				{
					LedPanel ledPanel = ledsys.Panels[i];
					if (ledPanel.Group != ledGroup.Name)
					{
						list.Add(ledPanel);
					}
					else
					{
						formMain.DeletePanelFromIPCServer(ledPanel.ProductID);
					}
				}
				if (list.Count == 0)
				{
					return;
				}
				ledsys.Panels = list;
				ledsys.SelectedPanel = list[list.Count - 1];
				ledsys.SelectedIndex = list.Count - 1;
				ledsys.RemoveGroup(ledGroup);
				if (nextNode != null)
				{
					treeNode = nextNode;
				}
				else if (prevNode != null)
				{
					treeNode = prevNode;
				}
				this.isGroup = true;
			}
			else
			{
				if (ledsys.Panels.Count == 1)
				{
					return;
				}
				LedPanel ledPanel2 = null;
				if (nextNode != null)
				{
					treeNode = nextNode;
				}
				else if (prevNode != null)
				{
					treeNode = prevNode;
				}
				if (treeNode != null)
				{
					ledPanel2 = (LedPanel)treeNode.Tag;
				}
				else
				{
					TreeNode parent = selectedNode.Parent;
					if (parent != null && parent.Tag.GetType() == typeof(LedGroup))
					{
						treeNode = parent;
					}
				}
				formMain.DeletePanelFromIPCServer(this.thisPanel.ProductID);
				ledsys.RemovePanel(this.thisPanel);
				this.thisPanel = ledPanel2;
				if (ledPanel2 == null)
				{
					this.isGroup = true;
				}
				else
				{
					this.isGroup = false;
				}
			}
			if (treeNode != null)
			{
				this.tvwPanel.SelectedNode = treeNode;
			}
			selectedNode.Remove();
			this.SetControlEnabled();
			if (!this.isGroup)
			{
				this.isFromFile = false;
				this.LoadPanelParam();
			}
		}

		private void DeletePanelFolder(string pPanelDir)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(pPanelDir);
			if (directoryInfo.Exists)
			{
				try
				{
					directoryInfo.Delete(true);
				}
				catch
				{
				}
			}
		}

		private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
		{
		}

		private void formPanelEdit_Load(object sender, EventArgs e)
		{
			this.lblMacAddress.Visible = false;
			this.txtMacAddress.Visible = false;
			this.txtCloudServerUserName.Enabled = false;
			this.txtGPRSCloudServerUserName.Enabled = false;
			this.txtDeviceID.Enabled = false;
			this.txtGPRSDeviceID.Enabled = false;
			base.Height = this.zhGroupBox3.Height + 43;
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.numericUpDownHeigth.LostFocus += new EventHandler(this.numericUpDownHeigth_LostFocus);
			this.numericUpDownWidth.LostFocus += new EventHandler(this.numericUpDownWidth_LostFocus);
			this.zhGroupBox3.Controls.Add(this.panel_Net);
			this.zhGroupBox3.Controls.Add(this.panel_USB);
			this.zhGroupBox3.Controls.Add(this.panel_Wifi);
			this.zhGroupBox3.Controls.Add(this.panel_GPRS);
			this.panel_Net.Location = this.panel_Serial.Location;
			this.panel_USB.Location = this.panel_Serial.Location;
			this.panel_Wifi.Location = this.panel_Serial.Location;
			this.panel_GPRS.Location = this.panel_Serial.Location;
			this.BackColor = Template.GroupBox_BackColor;
			this.tvwPanel.BackColor = Template.GroupBox_BackColor;
			if (this.mf == null)
			{
				return;
			}
			try
			{
				this.comboBoxCom.Items.Clear();
				string[] portNames = SerialPort.GetPortNames();
				string[] array = portNames;
				for (int i = 0; i < array.Length; i++)
				{
					string item = array[i];
					this.comboBoxCom.Items.Add(item);
				}
			}
			catch
			{
			}
			foreach (LedCard current in LedGlobal.LedCardList)
			{
				this.comboBoxModel.Items.Add(current.CardType.ToString().Replace("_", "-"));
			}
			this.tvwPanel.Nodes.Clear();
			this.LoadGroup();
			foreach (LedPanel current2 in formMain.Ledsys.Panels)
			{
				if (!(current2.GetType() == typeof(LedPanelCloud)) || this.mf.isCloudLogin)
				{
					this.LoadPanelToTree(current2);
				}
			}
			this.thisPanel = formMain.Ledsys.SelectedPanel;
			this.SyncPanelCloud(this.thisPanel);
			bool isPanelCloud = this.IsPanelCloud;
			this.comboBoxModel.Enabled = !isPanelCloud;
			this.textBoxDescription.Enabled = !isPanelCloud;
			this.groupBox2.Enabled = !isPanelCloud;
			this.panel_Net.Enabled = !isPanelCloud;
			this.panel_GPRS.Enabled = !isPanelCloud;
			this.LoadPanelParam();
		}

		private void numericUpDownWidth_LostFocus(object sender, EventArgs e)
		{
			try
			{
				bool isPanelCloud = this.IsPanelCloud;
				int num = (int)this.numericUpDownWidth.Value;
				int num2 = (int)this.numericUpDownHeigth.Value;
				if (num * (isPanelCloud ? num2 : this.thisPanel.Height) > this.thisPanel.GetMaxArea())
				{
					num = this.thisPanel.GetMaxArea() / (isPanelCloud ? num2 : this.thisPanel.Height);
					num = num / (int)this.numericUpDownWidth.Increment * (int)this.numericUpDownWidth.Increment;
					if (num > this.thisPanel.GetMaxWidth())
					{
						num = this.thisPanel.GetMaxWidth();
					}
				}
				else if (num > this.thisPanel.GetMaxWidth())
				{
					num = num / (int)this.numericUpDownWidth.Increment * (int)this.numericUpDownWidth.Increment;
				}
				else
				{
					num = num / (int)this.numericUpDownWidth.Increment * (int)this.numericUpDownWidth.Increment;
					if (!isPanelCloud)
					{
						this.thisPanel.Width = num;
					}
				}
				if (num < this.numericUpDownWidth.Minimum)
				{
					num = (int)this.numericUpDownWidth.Minimum;
				}
				this.numericUpDownWidth.Value = num;
				if (!isPanelCloud)
				{
					this.thisPanel.Width = num;
				}
			}
			catch
			{
			}
		}

		private void numericUpDownHeigth_LostFocus(object sender, EventArgs e)
		{
			try
			{
				bool isPanelCloud = this.IsPanelCloud;
				int num = (int)this.numericUpDownHeigth.Value;
				int num2 = (int)this.numericUpDownWidth.Value;
				if (num * (isPanelCloud ? num2 : this.thisPanel.Width) > this.thisPanel.GetMaxArea())
				{
					num = this.thisPanel.GetMaxArea() / (isPanelCloud ? num2 : this.thisPanel.Width);
					num = num / (int)this.numericUpDownHeigth.Increment * (int)this.numericUpDownHeigth.Increment;
					if (num > this.thisPanel.GetMaxHeight())
					{
						num = this.thisPanel.GetMaxHeight();
					}
				}
				else
				{
					num = num / (int)this.numericUpDownHeigth.Increment * (int)this.numericUpDownHeigth.Increment;
				}
				if (num < this.numericUpDownHeigth.Minimum)
				{
					num = (int)this.numericUpDownHeigth.Minimum;
				}
				this.numericUpDownHeigth.Value = num;
				if (!isPanelCloud)
				{
					this.thisPanel.Height = num;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (!this.isGroup && !this.IsPanelCloud)
			{
				this.SavePanelParam();
			}
			if (this.thisPanel != null)
			{
				formMain.ledsys.SelectedPanel = this.thisPanel;
			}
			base.Close();
		}

		private bool CheckNetworkID()
		{
			bool flag = true;
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			for (int i = 0; i < formMain.ledsys.Panels.Count; i++)
			{
				LedPanel ledPanel = formMain.ledsys.Panels[i];
				if (!(ledPanel.GetType() == typeof(LedPanelCloud)) && ledPanel.PortType == LedPortType.Ethernet && ledPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.LocalServer)
				{
					if (dictionary.ContainsKey(ledPanel.NetworkID))
					{
						List<string> list = dictionary[ledPanel.NetworkID];
						list.Add(ledPanel.TextName);
						flag = false;
					}
					else
					{
						List<string> list2 = new List<string>();
						list2.Add(ledPanel.TextName);
						dictionary.Add(ledPanel.NetworkID, list2);
					}
				}
			}
			if (!flag)
			{
				string text = string.Empty;
				int num = 0;
				foreach (KeyValuePair<string, List<string>> current in dictionary)
				{
					List<string> value = current.Value;
					if (value.Count > 1)
					{
						string arg_147_0 = text;
						string arg_142_0 = formMain.ML.GetStr("Message_Panel_Use_Local_Server_Has_Repeat_NetworkID_Item");
						object[] array = new object[4];
						object[] arg_116_0 = array;
						int arg_116_1 = 0;
						int num2;
						num = (num2 = num + 1);
						arg_116_0[arg_116_1] = num2.ToString();
						array[1] = current.Key;
						array[2] = string.Join("、", value.ToArray());
						array[3] = "\r\n";
						text = arg_147_0 + string.Format(arg_142_0, array);
					}
				}
				MessageBox.Show(this, string.Format(formMain.ML.GetStr("Message_Panel_Use_Local_Server_Has_Repeat_NetworkID"), "\r\n\r\n", text));
			}
			return flag;
		}

		private void ReLoadPanel()
		{
			TreeNodeCollection nodes = this.tvwPanel.Nodes;
			if (formMain.isUseGroup)
			{
				IEnumerator enumerator = nodes.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						TreeNode treeNode = (TreeNode)enumerator.Current;
						treeNode.Nodes.Clear();
					}
					goto IL_61;
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			this.tvwPanel.Nodes.Clear();
			IL_61:
			foreach (LedPanel current in formMain.Ledsys.Panels)
			{
				this.LoadPanelToTree(current);
			}
		}

		private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.thisPanel.ProtocolVersion = 49;
			this.thisPanel.CardType = LedGlobal.LedCardList[comboBox.SelectedIndex].CardType;
			string text = this.thisPanel.CardType.ToString();
			if (text.IndexOf("A") > 0)
			{
				this.thisPanel.PortType = LedPortType.SerialPort;
			}
			else if (text.IndexOf("U") > 0)
			{
				this.thisPanel.PortType = LedPortType.USB;
			}
			else if (text.IndexOf("M") > 0)
			{
				this.thisPanel.PortType = LedPortType.USB;
			}
			else if (text.IndexOf("S") > 0)
			{
				this.thisPanel.PortType = LedPortType.USB;
			}
			else if (text.IndexOf("E") > 0)
			{
				this.thisPanel.PortType = LedPortType.Ethernet;
				if (this.thisPanel.IsLSeries())
				{
					this.thisPanel.ProtocolVersion = LedCommunicationConst.ProtocolSendVersionList[2];
				}
				else if (text.IndexOf("I") > 0)
				{
					this.thisPanel.ProtocolVersion = LedCommunicationConst.ProtocolSendVersionList[0];
				}
			}
			else if (text.IndexOf("W") > 0)
			{
				this.thisPanel.PortType = LedPortType.WiFi;
			}
			else if (text.IndexOf("PP") > 0)
			{
				this.thisPanel.PortType = LedPortType.WiFi;
			}
			else if (text.IndexOf("G") > 0)
			{
				this.thisPanel.PortType = LedPortType.GPRS;
				if (text.IndexOf("L") > 0)
				{
					this.thisPanel.GPRSCommunicaitonMode = LedGPRSCommunicationMode.CloudServer;
				}
				else
				{
					this.thisPanel.GPRSCommunicaitonMode = LedGPRSCommunicationMode.GprsServer;
				}
			}
			else if (text.IndexOf("YU") > 0)
			{
				this.thisPanel.PortType = LedPortType.GPRS;
				this.thisPanel.GPRSCommunicaitonMode = LedGPRSCommunicationMode.CloudServer;
			}
			this.CheckWidthAndHeight();
			this.thisPanel.MACAddress = "FFFFFFFFFFFF";
			this.thisPanel.MainVersion = 0;
			this.thisPanel.HardwareVersion = 0;
			this.thisPanel.ProgramVersion = 0;
			if (text.IndexOf("M") < 0)
			{
				this.thisPanel.ForeGray = (this.thisPanel.BackgroundGray = 1);
			}
			if (text.IndexOf("E") < 0)
			{
				this.thisPanel.State = LedPanelState.Online;
			}
			formMain.ModifyPanelFromIPCServer(this.thisPanel);
			this.ModelChanged = true;
			this.LoadPanelParam();
		}

		private void ChangeCardType()
		{
			this.LoadPanelParam();
		}

		private void comboBoxBaudrate_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.thisPanel.SerialPortName = comboBox.Text.Trim();
			formMain.ModifyPanelFromIPCServer(this.thisPanel);
			this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
		}

		private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (this.isLoadParam)
			{
				return;
			}
			try
			{
				bool isPanelCloud = this.IsPanelCloud;
				int num = (int)this.numericUpDownWidth.Value;
				int num2 = (int)this.numericUpDownHeigth.Value;
				if (num * (isPanelCloud ? num2 : this.thisPanel.Height) > this.thisPanel.GetMaxArea())
				{
					numericUpDown.Value = this.thisPanel.Width;
				}
				else if (num > this.thisPanel.GetMaxWidth())
				{
					numericUpDown.Value = this.thisPanel.GetMaxWidth();
					if (!isPanelCloud)
					{
						this.thisPanel.Width = this.thisPanel.GetMaxWidth();
					}
				}
				else
				{
					num = num / (int)this.numericUpDownWidth.Increment * (int)this.numericUpDownWidth.Increment;
					if (!isPanelCloud)
					{
						this.thisPanel.Width = num;
					}
				}
			}
			catch
			{
			}
		}

		private void numericUpDownHeigth_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (this.isLoadParam)
			{
				return;
			}
			try
			{
				bool isPanelCloud = this.IsPanelCloud;
				int num = (int)this.numericUpDownHeigth.Value;
				int num2 = (int)this.numericUpDownWidth.Value;
				if (num * (isPanelCloud ? num2 : this.thisPanel.Width) > this.thisPanel.GetMaxArea())
				{
					numericUpDown.Value = this.thisPanel.Height;
				}
				else
				{
					num = num / (int)this.numericUpDownHeigth.Increment * (int)this.numericUpDownHeigth.Increment;
					if (!isPanelCloud)
					{
						this.thisPanel.Height = num;
					}
				}
			}
			catch
			{
			}
		}

		private void nudWidthModule_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (this.isLoadParam)
			{
				return;
			}
			if (!numericUpDown.Focused && !this.isWidthModuleKeyDown)
			{
				return;
			}
			this.isWidthModuleKeyDown = false;
			try
			{
				int num = (int)numericUpDown.Value * this.widthModuleUnit;
				if (num * this.thisPanel.Height > this.thisPanel.GetMaxArea())
				{
					numericUpDown.Value = this.thisPanel.Width / this.widthModuleUnit;
				}
				else
				{
					this.numericUpDownWidth.Value = num;
					this.thisPanel.Width = num;
				}
			}
			catch
			{
			}
		}

		private void nudHeigthModule_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (this.isLoadParam)
			{
				return;
			}
			if (!numericUpDown.Focused && !this.isHeightModuleKeyDown)
			{
				return;
			}
			this.isHeightModuleKeyDown = false;
			try
			{
				int num = (int)numericUpDown.Value * this.heightModuleUnit;
				if (num * this.thisPanel.Width > this.thisPanel.GetMaxArea())
				{
					numericUpDown.Value = this.thisPanel.Height / this.heightModuleUnit;
				}
				else
				{
					this.numericUpDownHeigth.Value = num;
					this.thisPanel.Height = num;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message);
			}
		}

		private void nudWidthModule_KeyDown(object sender, KeyEventArgs e)
		{
			this.isWidthModuleKeyDown = true;
		}

		private void nudHeigthModule_KeyDown(object sender, KeyEventArgs e)
		{
			this.isHeightModuleKeyDown = true;
		}

		private void manualRoutingSettingButton_Click(object sender, EventArgs e)
		{
			formCheckCode formCheckCode = new formCheckCode();
			if (formCheckCode.Check("888", false))
			{
				formRouting formRouting = new formRouting();
				formRouting.Edit(this.thisPanel.RoutingSetting, this.thisPanel, this.mf);
				formRouting.ShowDialog(this);
				this.textBoxRouting.Text = this.thisPanel.RoutingSetting.TypeDescription;
				this.LoadPanelParam();
			}
		}

		private void Add_Group_Button_Click(object sender, EventArgs e)
		{
			formGroup formGroup = new formGroup();
			if (formGroup.ShowDialog(this) == DialogResult.OK)
			{
				LedGroup group = formGroup.group;
				formMain.ledsys.AddGroup(group);
				try
				{
					TreeNode treeNode = new TreeNode(group.Name);
					treeNode.Tag = group;
					this.tvwPanel.Nodes.Add(treeNode);
					this.tvwPanel.ExpandAll();
					this.tvwPanel.SelectedNode = treeNode;
				}
				catch
				{
				}
				this.isGroup = true;
				this.SetControlEnabled();
			}
		}

		private void comboBoxColorMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			if (this.IsPanelCloud)
			{
				this.LoadPanelCloudParam();
				return;
			}
			this.ResetColorList(comboBox.SelectedIndex);
		}

		private void ResetColorList(int pNumber)
		{
			IList<System.Drawing.Color> list = new List<System.Drawing.Color>();
			switch (pNumber)
			{
			case 0:
				list.Add(System.Drawing.Color.Red);
				break;
			case 1:
				list.Add(System.Drawing.Color.Red);
				list.Add(System.Drawing.Color.Green);
				break;
			case 2:
				list.Add(System.Drawing.Color.Green);
				list.Add(System.Drawing.Color.Red);
				break;
			case 3:
				list.Add(System.Drawing.Color.Red);
				list.Add(System.Drawing.Color.Green);
				list.Add(System.Drawing.Color.Blue);
				break;
			case 4:
				list.Add(System.Drawing.Color.Red);
				list.Add(System.Drawing.Color.Blue);
				list.Add(System.Drawing.Color.Green);
				break;
			case 5:
				list.Add(System.Drawing.Color.Green);
				list.Add(System.Drawing.Color.Red);
				list.Add(System.Drawing.Color.Blue);
				break;
			case 6:
				list.Add(System.Drawing.Color.Green);
				list.Add(System.Drawing.Color.Blue);
				list.Add(System.Drawing.Color.Red);
				break;
			case 7:
				list.Add(System.Drawing.Color.Blue);
				list.Add(System.Drawing.Color.Red);
				list.Add(System.Drawing.Color.Green);
				break;
			case 8:
				list.Add(System.Drawing.Color.Blue);
				list.Add(System.Drawing.Color.Green);
				list.Add(System.Drawing.Color.Red);
				break;
			}
			this.thisPanel.ColorMode = pNumber + LedColorMode.R;
			int num = (int)this.numericUpDownHeigth.Value;
			int num2 = (int)this.numericUpDownWidth.Value;
			if (this.thisPanel.GetMaxWidth() < num2)
			{
				this.thisPanel.Width = this.thisPanel.GetMaxWidth();
			}
			if (this.thisPanel.GetMaxHeight() < num)
			{
				this.thisPanel.Height = this.thisPanel.GetMaxHeight();
			}
			if (this.thisPanel.Width * this.thisPanel.Height > this.thisPanel.GetMaxArea())
			{
				this.thisPanel.Width = this.thisPanel.GetMaxArea() / this.thisPanel.Height / 16 * 16;
			}
			this.SavePanelParam();
			this.LoadPanelParam();
		}

		private void numericUpDownPanelAddress_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.thisPanel.CardAddress = (int)numericUpDown.Value;
			formMain.ModifyPanelFromIPCServer(this.thisPanel);
		}

		public void btnLoadPanelParameter_Click(object sender, EventArgs e)
		{
			bool isPanelCloud = this.IsPanelCloud;
			if (isPanelCloud)
			{
				string cloudServerPassword = Settings.Default.CloudServerPassword;
				formCheckCode formCheckCode = new formCheckCode();
				if (!formCheckCode.Check(cloudServerPassword, true))
				{
					return;
				}
			}
			formPanelEdit.isLoaded = true;
			if (this.thisPanel.PortType == LedPortType.Ethernet && this.thisPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.Directly && MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Prompt_Load_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				formPanelEdit.isLoaded = false;
				return;
			}
			if (this.thisPanel.PortType == LedPortType.USB)
			{
				this.LoadPanelParamOfUSB();
			}
			else if (this.thisPanel.PortType == LedPortType.WiFi)
			{
				this.LoadPanelParamOfWIFI();
			}
			else if (isPanelCloud)
			{
				this.thisPanelCloud.Width = (int)this.numericUpDownWidth.Value;
				this.thisPanelCloud.Height = (int)this.numericUpDownHeigth.Value;
				this.thisPanelCloud.OEPolarity = (byte)this.comboBoxOE.SelectedIndex;
				this.thisPanelCloud.DataPolarity = (byte)this.comboBoxData.SelectedIndex;
				this.mf.CloudSendSingleCmdStart(LedCmdType.Send_Panel_Parameter, this.thisPanelCloud, this.btnLoadPanelParameter.Text, this.thisPanelCloud, false, this);
				if (formCloudSendSingle.LastSendResult)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_LoadSuccessed"));
					this.thisPanel.Width = this.thisPanelCloud.Width;
					this.thisPanel.Height = this.thisPanelCloud.Height;
					this.thisPanel.OEPolarity = this.thisPanelCloud.OEPolarity;
					this.thisPanel.DataPolarity = this.thisPanelCloud.DataPolarity;
					this.thisPanel.ColorMode = this.thisPanelCloud.ColorMode;
					this.thisPanel.RoutingSetting.ScanTypeIndex = this.thisPanelCloud.RoutingSetting.ScanTypeIndex;
					this.thisPanel.RoutingSetting.RoutingIndex = this.thisPanelCloud.RoutingSetting.RoutingIndex;
				}
			}
			else
			{
				this.mf.SendSingleCmdStart(LedCmdType.Send_Panel_Parameter, this.thisPanel, this.btnLoadPanelParameter.Text, this.thisPanel, false, this);
				if (formSendSingle.LastSendResult)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_LoadSuccessed"));
				}
			}
			formPanelEdit.isLoaded = false;
		}

		private void textBoxDescription_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			this.thisPanel.TextName = this.textBoxDescription.Text;
			formMain.ModifyPanelFromIPCServer(this.thisPanel);
			this.SavePanelParam();
		}

		private void comboBoxBaudrate_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.thisPanel.BaudRate = int.Parse(this.comboBoxBaudrate.Text);
			formMain.ModifyPanelFromIPCServer(this.thisPanel);
			this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
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
				this.thisPanel.OEPolarity = Convert.ToByte(true);
				return;
			}
			this.thisPanel.OEPolarity = Convert.ToByte(false);
		}

		private void comboBoxData_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (comboBox.SelectedIndex == 1)
			{
				this.thisPanel.DataPolarity = Convert.ToByte(true);
				return;
			}
			this.thisPanel.DataPolarity = Convert.ToByte(false);
		}

		private void btnReadPanelParameter_Click(object sender, EventArgs e)
		{
			if (this.thisPanel.PortType == LedPortType.GPRS)
			{
				return;
			}
			if (this.thisPanel.PortType == LedPortType.Ethernet && this.thisPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer)
			{
				return;
			}
			if (this.thisPanel.PortType == LedPortType.Ethernet && this.thisPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.Directly && MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Prompt_GetInfo_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			this.mf.SendSingleCmdStart(LedCmdType.Read_Panel_Parameter, null, this.btnReadPanelParameter.Text, this.thisPanel, false, this);
			if (formSendSingle.LastSendResult && formSendSingle.LastSendResultObject != null && formSendSingle.LastSendResultObject.GetType() == typeof(LedPanel))
			{
				LedPanel ledPanel = (LedPanel)formSendSingle.LastSendResultObject;
				if (ledPanel.CardType != this.thisPanel.CardType && MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_model_Not_Match") + ledPanel.CardType.ToString() + "\n        " + formMain.ML.GetStr("NETCARD_message_ContinueLoading"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
				{
					return;
				}
				this.thisPanel.Copy(ledPanel);
				TreeNode selectedNode = this.tvwPanel.SelectedNode;
				selectedNode.Tag = this.thisPanel;
				byte oEPolarity = this.thisPanel.OEPolarity;
				byte dataPolarity = this.thisPanel.DataPolarity;
				byte lTPolarity = this.thisPanel.LTPolarity;
				int scanFrequency = this.thisPanel.ScanFrequency;
				int blankingTime = this.thisPanel.BlankingTime;
				this.LoadPanelParam();
				this.thisPanel.OEPolarity = oEPolarity;
				this.thisPanel.DataPolarity = dataPolarity;
				this.thisPanel.LTPolarity = lTPolarity;
				this.thisPanel.ScanFrequency = scanFrequency;
				this.thisPanel.BlankingTime = blankingTime;
				if (this.thisPanel.OEPolarity == 1)
				{
					this.comboBoxOE.SelectedIndex = 1;
				}
				else
				{
					this.comboBoxOE.SelectedIndex = 0;
				}
				if (this.thisPanel.DataPolarity == 1)
				{
					this.comboBoxData.SelectedIndex = 1;
				}
				else
				{
					this.comboBoxData.SelectedIndex = 0;
				}
				this.onlineVersion.Text = string.Concat(new object[]
				{
					ledPanel.CardType.ToString().Replace("_", "-"),
					" V",
					ledPanel.MainVersion,
					".",
					ledPanel.HardwareVersion.ToString("D2"),
					".",
					ledPanel.ProgramVersion.ToString("D2")
				});
			}
		}

		public static string getVersionFromByte_UDP(byte[] pData)
		{
			string result;
			try
			{
				if (pData.Length < 24)
				{
					result = "Error..";
				}
				else
				{
					string text = "";
					byte arg_1C_0 = pData[9];
					byte arg_21_0 = pData[10];
					for (int i = 0; i < 8; i++)
					{
						if (pData[i + 21] > 32)
						{
							text += (char)pData[i + 21];
						}
					}
					text += " V";
					int num = (int)pData[30] + (int)pData[31] * 256;
					text = text + pData[29].ToString() + ".";
					text = text + num.ToString("D2") + ".";
					text += ((int)pData[32] + (int)pData[33] * 256).ToString("D2");
					result = text;
				}
			}
			catch
			{
				result = "Error..";
			}
			return result;
		}

		private void formPanelEdit_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.CheckNetworkID())
			{
				e.Cancel = true;
			}
			this.timerUsbSelect.Stop();
			this.timerWifiConnectionStatus.Stop();
		}

		private void comboBoxGradation_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.IsPanelCloud)
			{
				return;
			}
			this.thisPanel.ForeGray = (this.thisPanel.BackgroundGray = comboBox.SelectedIndex + 1);
		}

		private void radioButtonUSB_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.thisPanel.PortType = LedPortType.USB;
				this.thisPanel.State = LedPanelState.Online;
			}
			formMain.ModifyPanelFromIPCServer(this.thisPanel);
			this.LoadPanelParam();
		}

		public static string FromStringToMac(string pValue)
		{
			string result;
			try
			{
				if (pValue == null)
				{
					result = "";
				}
				else if (pValue.Length != 12)
				{
					result = "";
				}
				else
				{
					for (int i = 0; i < 6; i++)
					{
						Convert.ToByte(pValue.Substring(i * 2, 2), 16);
					}
					result = pValue.ToUpper();
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string ToMaskedTextBoxIPAddress(string pValue)
		{
			string text = string.Empty;
			try
			{
				if (!string.IsNullOrEmpty(pValue))
				{
					IPAddress iPAddress = IPAddress.Parse(pValue);
					string[] array = iPAddress.ToString().Split(new char[]
					{
						'.'
					});
					for (int i = 0; i < array.Length; i++)
					{
						text = text + array[i] + LedCommon.Space(3 - array[i].Length);
						if (i != array.Length - 1)
						{
							text += ".";
						}
					}
				}
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		private void radioButtonSerial_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.thisPanel.PortType = LedPortType.SerialPort;
				this.thisPanel.State = LedPanelState.Online;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
			}
			this.LoadPanelParam();
		}

		private void comboBoxScanType_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (comboBox.SelectedIndex == -1)
			{
				return;
			}
			switch (comboBox.SelectedIndex)
			{
			case 0:
				this.thisPanel.RoutingSetting.ScanTypeIndex = 4;
				this.thisPanel.RoutingSetting.RoutingIndex = 0;
				this.thisPanel.OEPolarity = 1;
				break;
			case 1:
				this.thisPanel.RoutingSetting.ScanTypeIndex = 8;
				this.thisPanel.RoutingSetting.RoutingIndex = 0;
				this.thisPanel.OEPolarity = 1;
				break;
			case 2:
				this.thisPanel.RoutingSetting.ScanTypeIndex = 16;
				this.thisPanel.RoutingSetting.RoutingIndex = 0;
				this.thisPanel.OEPolarity = 0;
				break;
			}
			this.LoadPanelParam();
		}

		private void comboBoxOE_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.OEChanged = true;
			if (this.IsPanelCloud)
			{
				return;
			}
			if (comboBox.SelectedIndex == 1)
			{
				this.thisPanel.OEPolarity = Convert.ToByte(true);
				return;
			}
			this.thisPanel.OEPolarity = Convert.ToByte(false);
		}

		private void comboBoxData_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.DataChanged = true;
			if (this.IsPanelCloud)
			{
				return;
			}
			if (comboBox.SelectedIndex == 1)
			{
				this.thisPanel.DataPolarity = Convert.ToByte(true);
				return;
			}
			this.thisPanel.DataPolarity = Convert.ToByte(false);
		}

		private void button_Advanced_Click(object sender, EventArgs e)
		{
			formCheckCode formCheckCode = new formCheckCode();
			if (formCheckCode.Check("888", false))
			{
				formRouting formRouting = new formRouting();
				if (this.IsPanelCloud)
				{
					formRouting.Edit(this.thisPanelCloud.RoutingSetting, this.thisPanelCloud, this.mf);
					formRouting.ShowDialog(this);
					this.LoadPanelCloudParam();
					return;
				}
				formRouting.Edit(this.thisPanel.RoutingSetting, this.thisPanel, this.mf);
				formRouting.ShowDialog(this);
				this.LoadPanelParam();
			}
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				this.thisPanel.OEPolarity = Convert.ToByte(true);
				return;
			}
			this.thisPanel.OEPolarity = Convert.ToByte(false);
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				this.thisPanel.DataPolarity = Convert.ToByte(true);
				return;
			}
			this.thisPanel.DataPolarity = Convert.ToByte(false);
		}

		private void listBox_Express_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBox listBox = (ListBox)sender;
			if (!listBox.Focused)
			{
				return;
			}
			if (formPanelEdit.isLoaded)
			{
				return;
			}
			switch (listBox.SelectedIndex)
			{
			case -1:
				return;
			case 0:
				this.thisPanel.RoutingSetting.ScanTypeIndex = 4;
				this.thisPanel.RoutingSetting.RoutingIndex = 0;
				break;
			case 1:
				this.thisPanel.RoutingSetting.ScanTypeIndex = 4;
				this.thisPanel.RoutingSetting.RoutingIndex = 1;
				break;
			case 2:
				this.thisPanel.RoutingSetting.ScanTypeIndex = 16;
				this.thisPanel.RoutingSetting.RoutingIndex = 0;
				break;
			}
			this.LoadPanelParam();
		}

		private void comboBox_Express_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			LedRoutingSetting ledRoutingSetting = this.IsPanelCloud ? this.thisPanelCloud.RoutingSetting : this.thisPanel.RoutingSetting;
			switch (comboBox.SelectedIndex)
			{
			case 0:
				ledRoutingSetting.ScanTypeIndex = 4;
				ledRoutingSetting.RoutingIndex = 0;
				break;
			case 1:
				ledRoutingSetting.ScanTypeIndex = 4;
				ledRoutingSetting.RoutingIndex = 35;
				break;
			case 2:
				ledRoutingSetting.ScanTypeIndex = 1;
				ledRoutingSetting.RoutingIndex = 1;
				break;
			case 3:
				ledRoutingSetting.ScanTypeIndex = 1;
				ledRoutingSetting.RoutingIndex = 2;
				break;
			case 4:
				ledRoutingSetting.ScanTypeIndex = 4;
				ledRoutingSetting.RoutingIndex = 1;
				break;
			case 5:
				ledRoutingSetting.ScanTypeIndex = 16;
				ledRoutingSetting.RoutingIndex = 0;
				break;
			case 6:
				ledRoutingSetting.ScanTypeIndex = 4;
				ledRoutingSetting.RoutingIndex = 12;
				break;
			case 7:
				ledRoutingSetting.ScanTypeIndex = 3;
				ledRoutingSetting.RoutingIndex = 0;
				break;
			case 8:
				ledRoutingSetting.ScanTypeIndex = 16;
				ledRoutingSetting.RoutingIndex = 1;
				break;
			case 9:
				ledRoutingSetting.ScanTypeIndex = 4;
				ledRoutingSetting.RoutingIndex = 36;
				break;
			}
			if (comboBox.SelectedIndex > -1)
			{
				if (this.IsPanelCloud)
				{
					this.LoadPanelCloudParam();
					return;
				}
				this.LoadPanelParam();
			}
		}

		private void button_Add_Advance_Click(object sender, EventArgs e)
		{
			formChangeAddress formChangeAddress = new formChangeAddress(this.thisPanel, this.mf);
			formChangeAddress.ShowDialog();
			this.comboBoxBaudrate.Text = this.thisPanel.BaudRate.ToString();
			this.numericUpDownPanelAddress.Value = this.thisPanel.CardAddress;
		}

		private void radioButtonEnthernet_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				if (this.thisPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.LocalServer)
				{
					this.thisPanel.State = LedPanelState.Offline;
				}
				else
				{
					this.thisPanel.State = LedPanelState.Online;
				}
				this.thisPanel.PortType = LedPortType.Ethernet;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
			}
			this.comboBoxCom.Enabled = radioButton.Checked;
			this.label8.Visible = true;
			this.numericUpDownPanelAddress.Visible = true;
			this.LoadPanelParam();
		}

		private void button_Add_Advance_Mac_Click(object sender, EventArgs e)
		{
		}

		private void button_Net_Advance_Click(object sender, EventArgs e)
		{
			formCheckCode formCheckCode = new formCheckCode();
			if (formCheckCode.Check("888", false))
			{
				formAdvancedSettings formAdvancedSettings = new formAdvancedSettings(this.thisPanel, this.mf);
				if (this.radioButton_SendBroadcast.Checked)
				{
					formAdvancedSettings.SetPanel_Sizeandcontent(LedEthernetCommunicationMode.Directly, this.thisPanel.IsLSeries());
				}
				else if (this.radioButton_SendByIP.Checked)
				{
					formAdvancedSettings.SetPanel_Sizeandcontent(LedEthernetCommunicationMode.FixedIP, this.thisPanel.IsLSeries());
				}
				else if (this.radioButton_localServer.Checked)
				{
					formAdvancedSettings.SetPanel_Sizeandcontent(LedEthernetCommunicationMode.LocalServer, true);
				}
				else if (this.radioButton_RemoteServer.Checked)
				{
					formAdvancedSettings.SetPanel_Sizeandcontent(LedEthernetCommunicationMode.CloudServer, true);
				}
				formAdvancedSettings.ShowDialog();
				this.txtIPAddress.Text = this.thisPanel.IPAddress;
				this.txtMacAddress.Text = this.thisPanel.MACAddress;
				this.txtIPPort.Text = this.thisPanel.IPPort.ToString();
				this.txtNetworkID.Text = this.thisPanel.NetworkID;
				this.txtPassword.Text = this.thisPanel.AuthorityPassword;
				this.txtPassword.Enabled = Convert.ToBoolean((int)this.thisPanel.AuthorityPasswordMode);
				this.txtCloudServerUserName.Text = this.thisPanel.CloudServerUserName;
			}
		}

		private void SendByIP_display()
		{
			if (this.thisPanel != null)
			{
				if (this.thisPanel.IsLSeries())
				{
					this.lblIPAddress.Visible = true;
					this.txtIPAddress.Visible = true;
					this.lblPort.Visible = true;
					this.txtIPPort.Visible = true;
					this.lblPassword.Visible = false;
					this.txtPassword.Visible = false;
					this.lblNetworkID.Visible = false;
					this.txtNetworkID.Visible = false;
					this.lblMacAddress.Visible = false;
					this.txtMacAddress.Visible = false;
					this.lblCloudServerUserName.Visible = false;
					this.txtCloudServerUserName.Visible = false;
					this.lblDeviceID.Visible = false;
					this.txtDeviceID.Visible = false;
					this.button_Net_Advance.Visible = true;
					this.lblIPAddress.Location = new System.Drawing.Point(this.lblIPAddress.Location.X, 65);
					this.txtIPAddress.Location = new System.Drawing.Point(this.txtIPAddress.Location.X, 62);
					this.lblPort.Location = new System.Drawing.Point(this.lblPort.Location.X, 95);
					this.txtIPPort.Location = new System.Drawing.Point(this.txtIPPort.Location.X, 92);
					this.lblPassword.Location = new System.Drawing.Point(this.lblPassword.Location.X, 95);
					this.txtPassword.Location = new System.Drawing.Point(this.txtPassword.Location.X, 92);
					return;
				}
				this.lblIPAddress.Visible = true;
				this.txtIPAddress.Visible = true;
				this.lblMacAddress.Visible = true;
				this.txtMacAddress.Visible = true;
				this.lblPassword.Visible = false;
				this.txtPassword.Visible = false;
				this.lblPort.Visible = false;
				this.txtIPPort.Visible = false;
				this.lblNetworkID.Visible = false;
				this.txtNetworkID.Visible = false;
				this.lblIPAddress.Location = new System.Drawing.Point(this.lblIPAddress.Location.X, 35);
				this.txtIPAddress.Location = new System.Drawing.Point(this.txtIPAddress.Location.X, 34);
				this.lblMacAddress.Location = new System.Drawing.Point(this.lblMacAddress.Location.X, 65);
				this.txtMacAddress.Location = new System.Drawing.Point(this.txtMacAddress.Location.X, 62);
			}
		}

		private void radioButton_SendByIP_CheckedChanged(object sender, EventArgs e)
		{
			this.SendByIP_display();
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.btnLoadPanelParameter.Enabled = true;
				this.thisPanel.State = LedPanelState.Online;
				this.thisPanel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.FixedIP;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
				this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
			}
		}

		private void Broadcast_display()
		{
			if (this.thisPanel != null)
			{
				if (this.thisPanel.IsLSeries())
				{
					this.lblIPAddress.Visible = false;
					this.txtIPAddress.Visible = false;
					this.lblPassword.Visible = false;
					this.txtPassword.Visible = false;
					this.lblPort.Visible = false;
					this.txtIPPort.Visible = false;
					this.lblNetworkID.Visible = false;
					this.txtNetworkID.Visible = false;
					this.lblCloudServerUserName.Visible = false;
					this.txtCloudServerUserName.Visible = false;
					this.lblDeviceID.Visible = false;
					this.txtDeviceID.Visible = false;
					this.button_Net_Advance.Visible = true;
					this.lblMacAddress.Location = new System.Drawing.Point(this.lblMacAddress.Location.X, 65);
					this.txtMacAddress.Location = new System.Drawing.Point(this.txtMacAddress.Location.X, 62);
					return;
				}
				this.lblIPAddress.Visible = false;
				this.txtIPAddress.Visible = false;
				this.lblMacAddress.Visible = true;
				this.txtMacAddress.Visible = true;
				this.lblPassword.Visible = false;
				this.txtPassword.Visible = false;
				this.lblPort.Visible = false;
				this.txtIPPort.Visible = false;
				this.lblNetworkID.Visible = false;
				this.txtNetworkID.Visible = false;
				this.lblMacAddress.Location = new System.Drawing.Point(this.lblMacAddress.Location.X, 65);
				this.txtMacAddress.Location = new System.Drawing.Point(this.txtMacAddress.Location.X, 62);
			}
		}

		private void radioButton_SendBroadcast_CheckedChanged(object sender, EventArgs e)
		{
			this.Broadcast_display();
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.btnLoadPanelParameter.Enabled = true;
				this.thisPanel.State = LedPanelState.Online;
				this.thisPanel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.Directly;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
				this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
			}
		}

		private void localServer_display()
		{
			if (this.thisPanel != null)
			{
				this.lblNetworkID.Visible = true;
				this.txtNetworkID.Visible = true;
				this.lblIPAddress.Visible = false;
				this.txtIPAddress.Visible = false;
				this.lblPort.Visible = false;
				this.txtIPPort.Visible = false;
				this.lblPassword.Visible = false;
				this.txtPassword.Visible = false;
				this.lblMacAddress.Visible = false;
				this.txtMacAddress.Visible = false;
				this.lblCloudServerUserName.Visible = false;
				this.txtCloudServerUserName.Visible = false;
				this.lblDeviceID.Visible = false;
				this.txtDeviceID.Visible = false;
				this.button_Net_Advance.Visible = true;
				this.lblNetworkID.Location = new System.Drawing.Point(this.lblNetworkID.Location.X, 65);
				this.txtNetworkID.Location = new System.Drawing.Point(this.txtNetworkID.Location.X, 62);
				this.lblPassword.Location = new System.Drawing.Point(this.lblPassword.Location.X, 95);
				this.txtPassword.Location = new System.Drawing.Point(this.txtPassword.Location.X, 92);
			}
		}

		public void CloudServer_display()
		{
			if (this.thisPanel != null && this.thisPanel.IsLSeries())
			{
				this.lblNetworkID.Visible = false;
				this.txtNetworkID.Visible = false;
				this.lblIPAddress.Visible = false;
				this.txtIPAddress.Visible = false;
				this.lblPort.Visible = false;
				this.txtIPPort.Visible = false;
				this.lblPassword.Visible = false;
				this.txtPassword.Visible = false;
				this.lblMacAddress.Visible = false;
				this.txtMacAddress.Visible = false;
				this.lblCloudServerUserName.Visible = true;
				this.txtCloudServerUserName.Visible = true;
				this.lblDeviceID.Visible = true;
				this.txtDeviceID.Visible = true;
				this.button_Net_Advance.Visible = true;
			}
		}

		private void radioButton_localServer_CheckedChanged(object sender, EventArgs e)
		{
			this.localServer_display();
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.btnLoadPanelParameter.Enabled = true;
				this.thisPanel.State = LedPanelState.Offline;
				this.thisPanel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.LocalServer;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
				this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
			}
		}

		private void radioButton_RemoteServer_CheckedChanged(object sender, EventArgs e)
		{
			this.CloudServer_display();
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.btnLoadPanelParameter.Enabled = false;
				this.thisPanel.State = LedPanelState.Online;
				this.thisPanel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.CloudServer;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
				this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
			}
		}

		private void radioButtonGPRS_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.thisPanel.PortType = LedPortType.GPRS;
				this.thisPanel.State = LedPanelState.Online;
				if (this.thisPanel.CardType.ToString().IndexOf("YU") > 0)
				{
					this.thisPanel.GPRSCommunicaitonMode = LedGPRSCommunicationMode.CloudServer;
				}
				else
				{
					this.thisPanel.GPRSCommunicaitonMode = LedGPRSCommunicationMode.GprsServer;
				}
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
			}
			this.LoadPanelParam();
		}

		private void radioButtonWifi_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.thisPanel.PortType = LedPortType.WiFi;
				this.thisPanel.State = LedPanelState.Online;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
			}
			this.LoadPanelParam();
		}

		private void button_ChangeWifiPassword_Click(object sender, EventArgs e)
		{
		}

		private void button_ChangeWifiSSID_Click(object sender, EventArgs e)
		{
		}

		private void txtNetworkID_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.thisPanel != null)
			{
				this.isNetworkChanged = true;
				this.thisPanel.NetworkID = textBox.Text;
				this.thisPanel.State = LedPanelState.Offline;
				this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
			}
		}

		private void txtNetworkID_Leave(object sender, EventArgs e)
		{
			if (this.isNetworkChanged)
			{
				this.isNetworkChanged = false;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
			}
		}

		private void txtIPPort_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.thisPanel != null)
			{
				int iPPort = 0;
				bool flag = int.TryParse(textBox.Text, out iPPort);
				if (flag)
				{
					this.isIPPortChanged = true;
					this.thisPanel.IPPort = iPPort;
				}
			}
		}

		private void txtIPPort_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void txtIPPort_Leave(object sender, EventArgs e)
		{
			if (this.isIPPortChanged)
			{
				this.isIPPortChanged = false;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
			}
		}

		private void txtIPAddress_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			string text = textBox.Text.Trim();
			if (text.Split(new char[]
			{
				'.'
			}, StringSplitOptions.RemoveEmptyEntries).Length != 4)
			{
				textBox.ForeColor = System.Drawing.Color.Red;
				return;
			}
			IPAddress none = IPAddress.None;
			bool flag = IPAddress.TryParse(text, out none);
			if (flag)
			{
				textBox.ForeColor = System.Drawing.Color.Black;
				if (this.thisPanel != null)
				{
					this.isIPAddressChanged = true;
					this.thisPanel.IPAddress = text;
					this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
					return;
				}
			}
			else
			{
				textBox.ForeColor = System.Drawing.Color.Red;
			}
		}

		private void txtIPAddress_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		private void txtIPAddress_Leave(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (this.isIPAddressChanged)
			{
				this.isIPAddressChanged = false;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
				return;
			}
			if (textBox.ForeColor == System.Drawing.Color.Red)
			{
				MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_IPAddress_SetFault"));
				textBox.SelectAll();
				textBox.Focus();
			}
		}

		private void txtPassword_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.thisPanel != null)
			{
				this.isPasswordChanged = true;
				this.thisPanel.AuthorityPassword = textBox.Text;
			}
		}

		private void txtPassword_Leave(object sender, EventArgs e)
		{
			if (this.isPasswordChanged)
			{
				this.isPasswordChanged = false;
				formMain.SetPanelPasswordToIPCServer(this.thisPanel);
			}
		}

		private void txtMacAddress_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			try
			{
				string text = formPanelEdit.FromStringToMac(textBox.Text.Trim());
				if (string.IsNullOrEmpty(text))
				{
					textBox.ForeColor = System.Drawing.Color.Red;
				}
				else
				{
					textBox.ForeColor = System.Drawing.Color.Black;
					if (this.thisPanel != null)
					{
						this.isMacAddressChanged = true;
						this.thisPanel.MACAddress = text;
					}
				}
			}
			catch
			{
			}
		}

		private void txtMacAddress_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void txtMacAddress_Leave(object sender, EventArgs e)
		{
			if (this.isMacAddressChanged)
			{
				this.isMacAddressChanged = false;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
			}
		}

		public static void AdjustComboBoxDropDownListWidth(object comboBox)
		{
			System.Drawing.Graphics graphics = null;
			try
			{
				ComboBox comboBox2 = null;
				if (comboBox is ComboBox)
				{
					comboBox2 = (ComboBox)comboBox;
				}
				else
				{
					if (!(comboBox is ToolStripComboBox))
					{
						return;
					}
					comboBox2 = ((ToolStripComboBox)comboBox).ComboBox;
				}
				int num = comboBox2.Width;
				graphics = comboBox2.CreateGraphics();
				System.Drawing.Font font = comboBox2.Font;
				int num2 = (comboBox2.Items.Count > comboBox2.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;
				foreach (object current in comboBox2.Items)
				{
					if (current != null)
					{
						int num3 = (int)graphics.MeasureString(current.ToString().Trim(), font).Width + num2;
						if (num < num3)
						{
							num = num3;
						}
					}
				}
				comboBox2.DropDownWidth = num;
			}
			catch
			{
			}
			finally
			{
				if (graphics != null)
				{
					graphics.Dispose();
				}
			}
		}

		private void txtCloudIPAddress_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			string text = textBox.Text.Trim();
			if (text.Split(new char[]
			{
				'.'
			}, StringSplitOptions.RemoveEmptyEntries).Length != 4)
			{
				textBox.ForeColor = System.Drawing.Color.Red;
				return;
			}
			IPAddress none = IPAddress.None;
			bool flag = IPAddress.TryParse(text, out none);
			if (flag)
			{
				textBox.ForeColor = System.Drawing.Color.Black;
				this.isCloudIPAddressChanged = true;
				this.thisPanel.CloudServerIPAddress = text;
				this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
				return;
			}
			textBox.ForeColor = System.Drawing.Color.Red;
		}

		private void txtCloudIPAddress_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		private void txtCloudIPAddress_Leave(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (this.isCloudIPAddressChanged)
			{
				this.isCloudIPAddressChanged = false;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
				return;
			}
			if (textBox.ForeColor == System.Drawing.Color.Red)
			{
				MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_IPAddress_SetFault"));
				textBox.SelectAll();
				textBox.Focus();
			}
		}

		private void TxtCloudPort_Leave(object sender, EventArgs e)
		{
			if (this.isCloudIPPortChanged)
			{
				this.isCloudIPPortChanged = false;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
			}
		}

		private void TxtCloudPort_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void TxtCloudPort_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.thisPanel != null)
			{
				int cloudServerIPPort = 0;
				bool flag = int.TryParse(textBox.Text, out cloudServerIPPort);
				if (flag)
				{
					this.isCloudIPPortChanged = true;
					this.thisPanel.CloudServerIPPort = cloudServerIPPort;
				}
			}
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
			if (this.UdiskDirList.Count == 0)
			{
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
				this.btnLoadPanelParameter.Enabled = this.IsNTFS(this.UdiskDirList[this.UsbListComboBox.SelectedIndex]);
			}
		}

		private void timerUsbSelect_Tick(object sender, EventArgs e)
		{
			this.updataUdiskList();
		}

		private bool IsNTFS(string pDisk)
		{
			bool result;
			try
			{
				DriveInfo driveInfo = new DriveInfo(pDisk);
				if (driveInfo.DriveFormat.ToUpper().IndexOf("FAT") >= 0)
				{
					if (driveInfo.DriveFormat.ToUpper() == "FAT32")
					{
						if (this.LastMessage == "notntfs")
						{
							this.label_Remind.Text = "";
						}
					}
					else if (this.WarningLevel < 2)
					{
						this.label_Remind.Text = formMain.ML.GetStr("USB_NotFAT32");
						this.label_Remind.ForeColor = System.Drawing.Color.Orange;
					}
					result = true;
				}
				else
				{
					this.label_Remind.Text = formMain.ML.GetStr("USB_NotSupportFormat");
					this.label_Remind.ForeColor = System.Drawing.Color.Red;
					this.LastMessage = "notntfs";
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void LoadPanelParamOfUSB()
		{
			this.timerUsbSelect.Stop();
			try
			{
				this.label_Remind.Text = formMain.ML.GetStr("USB_SavingData");
				Thread.Sleep(500);
				this.label_Remind.ForeColor = System.Drawing.Color.Black;
				this.WarningLevel = 0;
				if (this.UsbListComboBox.SelectedIndex == -1)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_NoFlashDisk"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
					this.label_Remind.Text = string.Empty;
				}
				else
				{
					base.Enabled = false;
					ProcessUSB processUSB = new ProcessUSB();
					if (this.thisPanel.IsLSeries())
					{
						processUSB.PanelBytes = this.thisPanel.ToLBytes();
					}
					else
					{
						processUSB.PanelBytes = this.thisPanel.ToBytes();
						processUSB.BmpDataBytes = this.thisPanel.ToItemBmpDataBytes();
						processUSB.ItemBytes = this.thisPanel.ToItemBytes();
						processUSB.TimerSwitchBytes = this.thisPanel.TimerSwitch.ToBytes();
						processUSB.LuminanceBytes = this.thisPanel.Luminance.ToBytes();
					}
					processUSB.ProtocolVersion = this.thisPanel.ProtocolVersion;
					protocol_data_integration protocol_data_integration = new protocol_data_integration();
					byte[] array = protocol_data_integration.WritingData_USB_Pack_Test(processUSB, false);
					if (array == null)
					{
						this.UsbDataMessagePrompt("USB_Save_Failed", 2, System.Drawing.Color.Red);
					}
					else if (array.Length > this.thisPanel.GetFlashCapacity())
					{
						this.UsbDataMessagePrompt("Prompt_MemeoryOverSize", 2, System.Drawing.Color.Red);
					}
					else
					{
						string path = string.Empty;
						if (formMain.IsforeignTradeMode)
						{
							string text = this.thisPanel.CardType.ToString();
							if (text.IndexOf("ZH_5W") > -1)
							{
								text = "ZH-5WX";
							}
							else
							{
								text = "ZH-XXL";
							}
							path = this.UdiskDirList[this.UsbListComboBox.SelectedIndex] + "ZH_LED\\" + text + "\\led_data.zh";
							Directory.CreateDirectory(this.UdiskDirList[this.UsbListComboBox.SelectedIndex] + "ZH_LED\\" + text);
						}
						else
						{
							path = this.UdiskDirList[this.UsbListComboBox.SelectedIndex] + "ledV3.zh3";
						}
						int i = 10;
						while (i > 0)
						{
							try
							{
								i--;
								FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
								DriveInfo driveInfo = new DriveInfo(this.UdiskDirList[this.UsbListComboBox.SelectedIndex]);
								if ((long)array.Length > driveInfo.TotalFreeSpace)
								{
									fileStream.Close();
									this.UsbDataMessagePrompt("formUSBWrite_Message_FileOverUsbSize", 2, System.Drawing.Color.Red);
									return;
								}
								fileStream.Write(array, 0, array.Length);
								fileStream.Close();
								break;
							}
							catch
							{
								if (i == 0)
								{
									this.UsbDataMessagePrompt("USB_Save_Failed", 2, System.Drawing.Color.Red);
									return;
								}
								Thread.Sleep(200);
							}
						}
						Thread.Sleep(500);
						if (!File.Exists(path))
						{
							this.UsbDataMessagePrompt("USB_Save_Failed", 2, System.Drawing.Color.Red);
						}
						this.UsbDataMessagePrompt("USB_Save_Success", 3, System.Drawing.Color.Blue);
					}
				}
			}
			catch
			{
				this.UsbDataMessagePrompt("USB_Save_Failed", 2, System.Drawing.Color.Red);
			}
		}

		private void UsbDataMessagePrompt(string remind, int remindLevel, System.Drawing.Color ColorOfText)
		{
			this.label_Remind.ForeColor = ColorOfText;
			this.WarningLevel = remindLevel;
			this.label_Remind.Text = formMain.ML.GetStr(remind);
			this.btnLoadPanelParameter.Enabled = true;
			base.Enabled = true;
			Thread.Sleep(1000);
			this.timerUsbSelect.Start();
			this.LastMessage = "save";
		}

		private void LoadPanelParamOfWIFI()
		{
			if (formMain.IServer != null)
			{
				formMain.IServer.Enable_HEART_Processing(false);
			}
			try
			{
				bool flag2;
				if (!this.IsConnectedToCorrectWifi)
				{
					bool flag = new formConnectToWIFI
					{
						IsOnlyConntectToWifi = true
					}.ConnectToWifi(this.mf);
					if (flag)
					{
						flag2 = true;
					}
					else
					{
						flag2 = false;
						MessageBox.Show(formMain.ML.GetStr("formPanelEditForForeignTrade_WIFI_CancelConnection"));
					}
				}
				else
				{
					flag2 = true;
				}
				if (flag2)
				{
					this.mf.SendSingleCmdStart(LedCmdType.Send_Panel_Parameter, this.thisPanel, this.btnLoadPanelParameter.Text, this.thisPanel, false, this);
					if (formSendSingle.LastSendResult)
					{
						MessageBox.Show(this, formMain.ML.GetStr("Prompt_LoadSuccessed"));
						return;
					}
				}
			}
			catch
			{
				MessageBox.Show(formMain.ML.GetStr("WIFI_AccessError"));
			}
			if (formMain.IServer != null)
			{
				formMain.IServer.Enable_HEART_Processing(true);
			}
		}

		private void timerWifiConnectionStatus_Tick(object sender, EventArgs e)
		{
			try
			{
				this.TxtWifiName.Text = (this.NowWifiName = this.GetCurrentConnection());
				if (this.NowWifiName.StartsWith("ZH-") || this.NowWifiName.StartsWith("ZH-PP") || this.NowWifiName.StartsWith("ZH_PP"))
				{
					this.LblControlCardState.Text = formMain.ML.GetStr("formPanelEditForForeignTrade_WIFIName_MatchCard");
					this.LblControlCardState.ForeColor = System.Drawing.Color.Green;
					this.IsConnectedToCorrectWifi = true;
				}
				else
				{
					this.LblControlCardState.Text = formMain.ML.GetStr("formPanelEditForForeignTrade_WIFIName_NotMatchCard");
					this.LblControlCardState.ForeColor = System.Drawing.Color.Red;
					this.IsConnectedToCorrectWifi = false;
				}
			}
			catch
			{
			}
		}

		public string GetCurrentConnection()
		{
			if (this.client == null)
			{
				this.client = new WlanClient();
			}
			try
			{
				WlanClient.WlanInterface[] interfaces = this.client.Interfaces;
				for (int i = 0; i < interfaces.Length; i++)
				{
					WlanClient.WlanInterface wlanInterface = interfaces[i];
					Wlan.WlanAvailableNetwork[] availableNetworkList = wlanInterface.GetAvailableNetworkList((Wlan.WlanGetAvailableNetworkFlags)0);
					Wlan.WlanAvailableNetwork[] array = availableNetworkList;
					for (int j = 0; j < array.Length; j++)
					{
						Wlan.WlanAvailableNetwork arg_47_0 = array[j];
						if (wlanInterface.InterfaceState == Wlan.WlanInterfaceState.Connected && wlanInterface.CurrentConnection.isState == Wlan.WlanInterfaceState.Connected)
						{
							string result = wlanInterface.CurrentConnection.profileName;
							return result;
						}
					}
				}
			}
			catch
			{
				string result = string.Empty;
				return result;
			}
			return string.Empty;
		}

		private void btn_GPRS_Net_Advance_Click(object sender, EventArgs e)
		{
			formCheckCode formCheckCode = new formCheckCode();
			if (formCheckCode.Check("888", false))
			{
				formGPRSAdvancedSettings formGPRSAdvancedSettings = new formGPRSAdvancedSettings(this.thisPanel);
				formGPRSAdvancedSettings.ShowDialog();
				this.txtGPRSCloudServerUserName.Text = this.thisPanel.CloudServerUserName;
			}
		}

		private void tsmiMoveToGroup_Click(object sender, EventArgs e)
		{
			formGroupSelect formGroupSelect = new formGroupSelect(this.thisPanel);
			if (formGroupSelect.ShowDialog(this) == DialogResult.OK)
			{
				TreeNode selectedNode = this.tvwPanel.SelectedNode;
				foreach (TreeNode treeNode in this.tvwPanel.Nodes)
				{
					if (treeNode != null && treeNode.Tag != null && !(treeNode.Tag.GetType() != typeof(LedGroup)))
					{
						LedGroup ledGroup = treeNode.Tag as LedGroup;
						if (ledGroup.ID == this.thisPanel.Group)
						{
							TreeNode treeNode2 = (TreeNode)selectedNode.Clone();
							treeNode.Nodes.Add(treeNode2);
							selectedNode.Remove();
							this.tvwPanel.SelectedNode = treeNode2;
							break;
						}
					}
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPanelEdit));

            //ComponentResourceManager resources = new ComponentResourceManager(typeof(formPanelEdit));
			this.tvwPanel = new TreeView();
			this.panel_GPRS = new Panel();
			this.txtGPRSDeviceID = new TextBox();
			this.lblGPRSDeviceID = new Label();
			this.txtGPRSCloudServerUserName = new TextBox();
			this.lblGPRSCloudServerUserName = new Label();
			this.btn_GPRS_Net_Advance = new Button();
			this.LblGPRSMessage = new Label();
			this.panel_Wifi = new Panel();
			this.LblControlCardState = new Label();
			this.TxtWifiName = new TextBox();
			this.LblWifiName = new Label();
			this.button_ChangeWifiSSID = new Button();
			this.button_ChangeWifiPassword = new Button();
			this.panel_USB = new Panel();
			this.label_Remind = new Label();
			this.label_SelectUSB = new Label();
			this.UsbListComboBox = new ComboBox();
			this.panel_Net = new Panel();
			this.lblDeviceID = new Label();
			this.lblCloudServerUserName = new Label();
			this.txtMacAddress = new TextBox();
			this.lblMacAddress = new Label();
			this.txtPassword = new TextBox();
			this.txtIPPort = new TextBox();
			this.radioButton_RemoteServer = new RadioButton();
			this.lblPassword = new Label();
			this.txtNetworkID = new TextBox();
			this.radioButton_localServer = new RadioButton();
			this.radioButton_SendByIP = new RadioButton();
			this.radioButton_SendBroadcast = new RadioButton();
			this.button_Net_Advance = new Button();
			this.lblPort = new Label();
			this.lblNetworkID = new Label();
			this.lblIPAddress = new Label();
			this.txtDeviceID = new TextBox();
			this.txtCloudServerUserName = new TextBox();
			this.txtIPAddress = new TextBox();
			this.ImgSelect = new ImageList(this.components);
			this.button_Add_Advance_Mac = new Button();
			this.pictureBox2 = new PictureBox();
			this.label13 = new Label();
			this.productMessageLabel = new Label();
			this.ELMessageLabel = new Label();
			this.panel_Serial = new Panel();
			this.label_Baudrate = new Label();
			this.label_PortName = new Label();
			this.button_Add_Advance = new Button();
			this.comboBoxBaudrate = new ComboBox();
			this.comboBoxCom = new ComboBox();
			this.label8 = new Label();
			this.numericUpDownPanelAddress = new NumericUpDown();
			this.btnReadPanelParameter = new Button();
			this.onlineVersion = new Label();
			this.label11 = new Label();
			this.buttonSave = new Button();
			this.btnLoadPanelParameter = new Button();
			this.groupBox2 = new GroupBox();
			this.radioButtonUSB = new RadioButton();
			this.radioButtonWifi = new RadioButton();
			this.radioButtonSerial = new RadioButton();
			this.radioButtonEnthernet = new RadioButton();
			this.radioButtonGPRS = new RadioButton();
			this.manualRoutingSettingButton = new Button();
			this.label7 = new Label();
			this.textBoxRouting = new TextBox();
			this.label6 = new Label();
			this.comboBoxScanType = new ComboBox();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.numericUpDownHeigth = new NumericUpDown();
			this.numericUpDownWidth = new NumericUpDown();
			this.label1 = new Label();
			this.textBoxDescription = new TextBox();
			this.comboBoxModel = new ComboBox();
			this.labelType = new Label();
			this.comboBoxColorMode = new ComboBox();
			this.cmsPanel = new ContextMenuStrip(this.components);
			this.tsmiMoveToGroup = new ToolStripMenuItem();
			this.label14_OE = new Label();
			this.comboBox_Express = new ComboBox();
			this.comboBoxOE = new ComboBox();
			this.button_Advanced = new Button();
			this.label1_Express = new Label();
			this.label12 = new Label();
			this.Label_Data = new Label();
			this.comboBoxGradation = new ComboBox();
			this.comboBoxData = new ComboBox();
			this.checkBox_OE = new CheckBox();
			this.checkBox_Data = new CheckBox();
			this.listBox_Express = new ListBox();
			this.zhGroupBox2 = new GroupBox();
			this.panel_Set = new Panel();
			this.Delete_GroupOrPanelButton = new Button();
			this.Add_Group_Button = new Button();
			this.Add_Panel_Button = new Button();
			this.zhGroupBox3 = new GroupBox();
			this.zhGroupBoxButton = new GroupBox();
			this.lblWidthModule = new Label();
			this.lblHeightModule = new Label();
			this.nudHeigthModule = new NumericUpDown();
			this.nudWidthModule = new NumericUpDown();
			this.zhGroupBox1 = new GroupBox();
			this.toolTip_panel = new ToolTip(this.components);
			this.panel1 = new Panel();
			this.timerUsbSelect = new System.Windows.Forms.Timer(this.components);
			this.timerWifiConnectionStatus = new System.Windows.Forms.Timer(this.components);
			this.panel_GPRS.SuspendLayout();
			this.panel_Wifi.SuspendLayout();
			this.panel_USB.SuspendLayout();
			this.panel_Net.SuspendLayout();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			this.panel_Serial.SuspendLayout();
			((ISupportInitialize)this.numericUpDownPanelAddress).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.numericUpDownHeigth).BeginInit();
			((ISupportInitialize)this.numericUpDownWidth).BeginInit();
			this.cmsPanel.SuspendLayout();
			this.zhGroupBox2.SuspendLayout();
			this.panel_Set.SuspendLayout();
			this.zhGroupBox3.SuspendLayout();
			this.zhGroupBoxButton.SuspendLayout();
			((ISupportInitialize)this.nudHeigthModule).BeginInit();
			((ISupportInitialize)this.nudWidthModule).BeginInit();
			this.zhGroupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.tvwPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.tvwPanel.BorderStyle = BorderStyle.None;
			this.tvwPanel.HideSelection = false;
			this.tvwPanel.Location = new System.Drawing.Point(5, 49);
			this.tvwPanel.Name = "tvwPanel";
			this.tvwPanel.Size = new System.Drawing.Size(177, 545);
			this.tvwPanel.TabIndex = 0;
			this.tvwPanel.KeyDown += new KeyEventHandler(this.tvwPanel_KeyDown);
			this.tvwPanel.MouseDown += new MouseEventHandler(this.tvwPanel_MouseDown);
			this.panel_GPRS.Controls.Add(this.txtGPRSDeviceID);
			this.panel_GPRS.Controls.Add(this.lblGPRSDeviceID);
			this.panel_GPRS.Controls.Add(this.txtGPRSCloudServerUserName);
			this.panel_GPRS.Controls.Add(this.lblGPRSCloudServerUserName);
			this.panel_GPRS.Controls.Add(this.btn_GPRS_Net_Advance);
			this.panel_GPRS.Controls.Add(this.LblGPRSMessage);
			this.panel_GPRS.Location = new System.Drawing.Point(8, 342);
			this.panel_GPRS.Name = "panel_GPRS";
			this.panel_GPRS.Size = new System.Drawing.Size(284, 120);
			this.panel_GPRS.TabIndex = 55;
			this.txtGPRSDeviceID.Location = new System.Drawing.Point(83, 92);
			this.txtGPRSDeviceID.Name = "txtGPRSDeviceID";
			this.txtGPRSDeviceID.Size = new System.Drawing.Size(129, 21);
			this.txtGPRSDeviceID.TabIndex = 84;
			this.lblGPRSDeviceID.Location = new System.Drawing.Point(7, 93);
			this.lblGPRSDeviceID.Name = "lblGPRSDeviceID";
			this.lblGPRSDeviceID.Size = new System.Drawing.Size(70, 17);
			this.lblGPRSDeviceID.TabIndex = 83;
			this.lblGPRSDeviceID.Text = "设备ID";
			this.lblGPRSDeviceID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtGPRSCloudServerUserName.Location = new System.Drawing.Point(83, 62);
			this.txtGPRSCloudServerUserName.Name = "txtGPRSCloudServerUserName";
			this.txtGPRSCloudServerUserName.Size = new System.Drawing.Size(129, 21);
			this.txtGPRSCloudServerUserName.TabIndex = 82;
			this.lblGPRSCloudServerUserName.Location = new System.Drawing.Point(7, 65);
			this.lblGPRSCloudServerUserName.Name = "lblGPRSCloudServerUserName";
			this.lblGPRSCloudServerUserName.Size = new System.Drawing.Size(70, 17);
			this.lblGPRSCloudServerUserName.TabIndex = 81;
			this.lblGPRSCloudServerUserName.Text = "用户名";
			this.lblGPRSCloudServerUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_GPRS_Net_Advance.Location = new System.Drawing.Point(218, 61);
			this.btn_GPRS_Net_Advance.Name = "btn_GPRS_Net_Advance";
			this.btn_GPRS_Net_Advance.Size = new System.Drawing.Size(61, 42);
			this.btn_GPRS_Net_Advance.TabIndex = 56;
			this.btn_GPRS_Net_Advance.Text = "控制卡网络配置";
			this.btn_GPRS_Net_Advance.UseVisualStyleBackColor = true;
			this.btn_GPRS_Net_Advance.Click += new EventHandler(this.btn_GPRS_Net_Advance_Click);
			this.LblGPRSMessage.Location = new System.Drawing.Point(4, 5);
			this.LblGPRSMessage.Name = "LblGPRSMessage";
			this.LblGPRSMessage.Size = new System.Drawing.Size(275, 42);
			this.LblGPRSMessage.TabIndex = 55;
			this.LblGPRSMessage.Text = "如需加载屏参请到GPRS发送界面";
			this.panel_Wifi.Controls.Add(this.LblControlCardState);
			this.panel_Wifi.Controls.Add(this.TxtWifiName);
			this.panel_Wifi.Controls.Add(this.LblWifiName);
			this.panel_Wifi.Location = new System.Drawing.Point(8, 213);
			this.panel_Wifi.Name = "panel_Wifi";
			this.panel_Wifi.Size = new System.Drawing.Size(284, 120);
			this.panel_Wifi.TabIndex = 55;
			this.LblControlCardState.Location = new System.Drawing.Point(43, 55);
			this.LblControlCardState.Name = "LblControlCardState";
			this.LblControlCardState.Size = new System.Drawing.Size(204, 27);
			this.LblControlCardState.TabIndex = 57;
			this.TxtWifiName.Location = new System.Drawing.Point(43, 28);
			this.TxtWifiName.Name = "TxtWifiName";
			this.TxtWifiName.Size = new System.Drawing.Size(179, 21);
			this.TxtWifiName.TabIndex = 1;
			this.LblWifiName.Location = new System.Drawing.Point(43, 9);
			this.LblWifiName.Name = "LblWifiName";
			this.LblWifiName.Size = new System.Drawing.Size(179, 17);
			this.LblWifiName.TabIndex = 32;
			this.LblWifiName.Text = "Wi-Fi名称";
			this.LblWifiName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button_ChangeWifiSSID.Location = new System.Drawing.Point(41, 46);
			this.button_ChangeWifiSSID.Name = "button_ChangeWifiSSID";
			this.button_ChangeWifiSSID.Size = new System.Drawing.Size(193, 23);
			this.button_ChangeWifiSSID.TabIndex = 1;
			this.button_ChangeWifiSSID.Text = "修改网络名称";
			this.button_ChangeWifiSSID.UseVisualStyleBackColor = true;
			this.button_ChangeWifiSSID.Click += new EventHandler(this.button_ChangeWifiSSID_Click);
			this.button_ChangeWifiPassword.Location = new System.Drawing.Point(41, 21);
			this.button_ChangeWifiPassword.Name = "button_ChangeWifiPassword";
			this.button_ChangeWifiPassword.Size = new System.Drawing.Size(193, 23);
			this.button_ChangeWifiPassword.TabIndex = 0;
			this.button_ChangeWifiPassword.Text = "修改网络密码";
			this.button_ChangeWifiPassword.UseVisualStyleBackColor = true;
			this.button_ChangeWifiPassword.Click += new EventHandler(this.button_ChangeWifiPassword_Click);
			this.panel_USB.Controls.Add(this.label_Remind);
			this.panel_USB.Controls.Add(this.label_SelectUSB);
			this.panel_USB.Controls.Add(this.UsbListComboBox);
			this.panel_USB.Location = new System.Drawing.Point(298, 213);
			this.panel_USB.Name = "panel_USB";
			this.panel_USB.Size = new System.Drawing.Size(284, 120);
			this.panel_USB.TabIndex = 54;
			this.label_Remind.Location = new System.Drawing.Point(43, 55);
			this.label_Remind.Name = "label_Remind";
			this.label_Remind.Size = new System.Drawing.Size(204, 61);
			this.label_Remind.TabIndex = 57;
			this.label_SelectUSB.Location = new System.Drawing.Point(43, 9);
			this.label_SelectUSB.Name = "label_SelectUSB";
			this.label_SelectUSB.Size = new System.Drawing.Size(179, 17);
			this.label_SelectUSB.TabIndex = 56;
			this.label_SelectUSB.Text = "选择U盘";
			this.label_SelectUSB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.UsbListComboBox.Cursor = Cursors.Default;
			this.UsbListComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.UsbListComboBox.FormattingEnabled = true;
			this.UsbListComboBox.ImeMode = ImeMode.On;
			this.UsbListComboBox.Location = new System.Drawing.Point(43, 29);
			this.UsbListComboBox.Name = "UsbListComboBox";
			this.UsbListComboBox.Size = new System.Drawing.Size(179, 20);
			this.UsbListComboBox.TabIndex = 55;
			this.panel_Net.Controls.Add(this.lblDeviceID);
			this.panel_Net.Controls.Add(this.lblCloudServerUserName);
			this.panel_Net.Controls.Add(this.txtMacAddress);
			this.panel_Net.Controls.Add(this.lblMacAddress);
			this.panel_Net.Controls.Add(this.txtPassword);
			this.panel_Net.Controls.Add(this.txtIPPort);
			this.panel_Net.Controls.Add(this.radioButton_RemoteServer);
			this.panel_Net.Controls.Add(this.lblPassword);
			this.panel_Net.Controls.Add(this.txtNetworkID);
			this.panel_Net.Controls.Add(this.radioButton_localServer);
			this.panel_Net.Controls.Add(this.radioButton_SendByIP);
			this.panel_Net.Controls.Add(this.radioButton_SendBroadcast);
			this.panel_Net.Controls.Add(this.button_Net_Advance);
			this.panel_Net.Controls.Add(this.lblPort);
			this.panel_Net.Controls.Add(this.lblNetworkID);
			this.panel_Net.Controls.Add(this.lblIPAddress);
			this.panel_Net.Controls.Add(this.txtDeviceID);
			this.panel_Net.Controls.Add(this.txtCloudServerUserName);
			this.panel_Net.Controls.Add(this.txtIPAddress);
			this.panel_Net.Location = new System.Drawing.Point(8, 471);
			this.panel_Net.Name = "panel_Net";
			this.panel_Net.Size = new System.Drawing.Size(284, 120);
			this.panel_Net.TabIndex = 53;
			this.lblDeviceID.Location = new System.Drawing.Point(7, 93);
			this.lblDeviceID.Name = "lblDeviceID";
			this.lblDeviceID.Size = new System.Drawing.Size(70, 17);
			this.lblDeviceID.TabIndex = 81;
			this.lblDeviceID.Text = "设备ID";
			this.lblDeviceID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblCloudServerUserName.Location = new System.Drawing.Point(7, 65);
			this.lblCloudServerUserName.Name = "lblCloudServerUserName";
			this.lblCloudServerUserName.Size = new System.Drawing.Size(70, 17);
			this.lblCloudServerUserName.TabIndex = 79;
			this.lblCloudServerUserName.Text = "用户名";
			this.lblCloudServerUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtMacAddress.Location = new System.Drawing.Point(83, 92);
			this.txtMacAddress.Name = "txtMacAddress";
			this.txtMacAddress.Size = new System.Drawing.Size(130, 21);
			this.txtMacAddress.TabIndex = 63;
			this.txtMacAddress.Visible = false;
			this.txtMacAddress.TextChanged += new EventHandler(this.txtMacAddress_TextChanged);
			this.txtMacAddress.KeyDown += new KeyEventHandler(this.txtMacAddress_KeyDown);
			this.txtMacAddress.Leave += new EventHandler(this.txtMacAddress_Leave);
			this.lblMacAddress.Location = new System.Drawing.Point(7, 95);
			this.lblMacAddress.Name = "lblMacAddress";
			this.lblMacAddress.Size = new System.Drawing.Size(70, 17);
			this.lblMacAddress.TabIndex = 68;
			this.lblMacAddress.Text = "MAC地址";
			this.lblMacAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtPassword.Location = new System.Drawing.Point(83, 92);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(120, 21);
			this.txtPassword.TabIndex = 63;
			this.txtPassword.TextChanged += new EventHandler(this.txtPassword_TextChanged);
			this.txtPassword.Leave += new EventHandler(this.txtPassword_Leave);
			this.txtIPPort.Location = new System.Drawing.Point(83, 62);
			this.txtIPPort.Name = "txtIPPort";
			this.txtIPPort.Size = new System.Drawing.Size(130, 21);
			this.txtIPPort.TabIndex = 66;
			this.txtIPPort.TextChanged += new EventHandler(this.txtIPPort_TextChanged);
			this.txtIPPort.KeyPress += new KeyPressEventHandler(this.txtIPPort_KeyPress);
			this.txtIPPort.Leave += new EventHandler(this.txtIPPort_Leave);
			this.radioButton_RemoteServer.AutoSize = true;
			this.radioButton_RemoteServer.Location = new System.Drawing.Point(113, 31);
			this.radioButton_RemoteServer.Name = "radioButton_RemoteServer";
			this.radioButton_RemoteServer.Size = new System.Drawing.Size(71, 16);
			this.radioButton_RemoteServer.TabIndex = 62;
			this.radioButton_RemoteServer.TabStop = true;
			this.radioButton_RemoteServer.Text = "云服务器";
			this.radioButton_RemoteServer.UseVisualStyleBackColor = true;
			this.radioButton_RemoteServer.CheckedChanged += new EventHandler(this.radioButton_RemoteServer_CheckedChanged);
			this.lblPassword.Location = new System.Drawing.Point(7, 95);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(70, 17);
			this.lblPassword.TabIndex = 36;
			this.lblPassword.Text = "密码";
			this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtNetworkID.Location = new System.Drawing.Point(83, 62);
			this.txtNetworkID.Name = "txtNetworkID";
			this.txtNetworkID.Size = new System.Drawing.Size(130, 21);
			this.txtNetworkID.TabIndex = 64;
			this.txtNetworkID.TextChanged += new EventHandler(this.txtNetworkID_TextChanged);
			this.txtNetworkID.Leave += new EventHandler(this.txtNetworkID_Leave);
			this.radioButton_localServer.AutoSize = true;
			this.radioButton_localServer.Location = new System.Drawing.Point(6, 31);
			this.radioButton_localServer.Name = "radioButton_localServer";
			this.radioButton_localServer.Size = new System.Drawing.Size(83, 16);
			this.radioButton_localServer.TabIndex = 61;
			this.radioButton_localServer.TabStop = true;
			this.radioButton_localServer.Text = "本地服务器";
			this.radioButton_localServer.UseVisualStyleBackColor = true;
			this.radioButton_localServer.CheckedChanged += new EventHandler(this.radioButton_localServer_CheckedChanged);
			this.radioButton_SendByIP.AutoSize = true;
			this.radioButton_SendByIP.Location = new System.Drawing.Point(113, 5);
			this.radioButton_SendByIP.Name = "radioButton_SendByIP";
			this.radioButton_SendByIP.Size = new System.Drawing.Size(59, 16);
			this.radioButton_SendByIP.TabIndex = 58;
			this.radioButton_SendByIP.TabStop = true;
			this.radioButton_SendByIP.Text = "指定IP";
			this.radioButton_SendByIP.UseVisualStyleBackColor = true;
			this.radioButton_SendByIP.CheckedChanged += new EventHandler(this.radioButton_SendByIP_CheckedChanged);
			this.radioButton_SendBroadcast.AutoSize = true;
			this.radioButton_SendBroadcast.Location = new System.Drawing.Point(6, 5);
			this.radioButton_SendBroadcast.Name = "radioButton_SendBroadcast";
			this.radioButton_SendBroadcast.Size = new System.Drawing.Size(71, 16);
			this.radioButton_SendBroadcast.TabIndex = 57;
			this.radioButton_SendBroadcast.TabStop = true;
			this.radioButton_SendBroadcast.Text = "单机直连";
			this.radioButton_SendBroadcast.UseVisualStyleBackColor = true;
			this.radioButton_SendBroadcast.CheckedChanged += new EventHandler(this.radioButton_SendBroadcast_CheckedChanged);
			this.button_Net_Advance.Location = new System.Drawing.Point(220, 5);
			this.button_Net_Advance.Name = "button_Net_Advance";
			this.button_Net_Advance.Size = new System.Drawing.Size(61, 42);
			this.button_Net_Advance.TabIndex = 56;
			this.button_Net_Advance.Text = "控制卡网络配置";
			this.button_Net_Advance.UseVisualStyleBackColor = true;
			this.button_Net_Advance.Click += new EventHandler(this.button_Net_Advance_Click);
			this.lblPort.Location = new System.Drawing.Point(7, 65);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(70, 17);
			this.lblPort.TabIndex = 65;
			this.lblPort.Text = "端口";
			this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblNetworkID.Location = new System.Drawing.Point(7, 65);
			this.lblNetworkID.Name = "lblNetworkID";
			this.lblNetworkID.Size = new System.Drawing.Size(70, 17);
			this.lblNetworkID.TabIndex = 51;
			this.lblNetworkID.Text = "网络ID";
			this.lblNetworkID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblIPAddress.Location = new System.Drawing.Point(7, 63);
			this.lblIPAddress.Name = "lblIPAddress";
			this.lblIPAddress.Size = new System.Drawing.Size(70, 17);
			this.lblIPAddress.TabIndex = 31;
			this.lblIPAddress.Text = "IP地址";
			this.lblIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtDeviceID.Location = new System.Drawing.Point(83, 92);
			this.txtDeviceID.Name = "txtDeviceID";
			this.txtDeviceID.Size = new System.Drawing.Size(129, 21);
			this.txtDeviceID.TabIndex = 82;
			this.txtCloudServerUserName.Location = new System.Drawing.Point(83, 62);
			this.txtCloudServerUserName.Name = "txtCloudServerUserName";
			this.txtCloudServerUserName.Size = new System.Drawing.Size(129, 21);
			this.txtCloudServerUserName.TabIndex = 80;
			this.txtIPAddress.Location = new System.Drawing.Point(83, 62);
			this.txtIPAddress.Name = "txtIPAddress";
			this.txtIPAddress.Size = new System.Drawing.Size(130, 21);
			this.txtIPAddress.TabIndex = 77;
			this.txtIPAddress.TextChanged += new EventHandler(this.txtIPAddress_TextChanged);
			this.txtIPAddress.KeyPress += new KeyPressEventHandler(this.txtIPAddress_KeyPress);
			this.txtIPAddress.Leave += new EventHandler(this.txtIPAddress_Leave);
			this.ImgSelect.ImageStream = (ImageListStreamer)resources.GetObject("ImgSelect.ImageStream");
			this.ImgSelect.TransparentColor = System.Drawing.Color.Transparent;
			this.ImgSelect.Images.SetKeyName(0, "up.png");
			this.ImgSelect.Images.SetKeyName(1, "down.png");
			this.button_Add_Advance_Mac.Location = new System.Drawing.Point(387, 455);
			this.button_Add_Advance_Mac.Name = "button_Add_Advance_Mac";
			this.button_Add_Advance_Mac.Size = new System.Drawing.Size(46, 23);
			this.button_Add_Advance_Mac.TabIndex = 55;
			this.button_Add_Advance_Mac.Text = "设置";
			this.button_Add_Advance_Mac.UseVisualStyleBackColor = true;
			this.button_Add_Advance_Mac.Visible = false;
			this.button_Add_Advance_Mac.Click += new EventHandler(this.button_Add_Advance_Mac_Click);
			this.pictureBox2.BackColor = System.Drawing.Color.Gray;
			this.pictureBox2.Location = new System.Drawing.Point(6, 13);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(464, 194);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 51;
			this.pictureBox2.TabStop = false;
			this.label13.Enabled = false;
			this.label13.Location = new System.Drawing.Point(479, 236);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(102, 17);
			this.label13.TabIndex = 50;
			this.label13.Text = "OE极性";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label13.Visible = false;
			this.productMessageLabel.BackColor = System.Drawing.Color.Transparent;
			this.productMessageLabel.Location = new System.Drawing.Point(6, 249);
			this.productMessageLabel.Name = "productMessageLabel";
			this.productMessageLabel.Size = new System.Drawing.Size(464, 345);
			this.productMessageLabel.TabIndex = 0;
			this.ELMessageLabel.BackColor = System.Drawing.Color.Transparent;
			this.ELMessageLabel.Location = new System.Drawing.Point(6, 209);
			this.ELMessageLabel.Name = "ELMessageLabel";
			this.ELMessageLabel.Size = new System.Drawing.Size(464, 40);
			this.ELMessageLabel.TabIndex = 0;
			this.panel_Serial.Controls.Add(this.label_Baudrate);
			this.panel_Serial.Controls.Add(this.label_PortName);
			this.panel_Serial.Controls.Add(this.button_Add_Advance);
			this.panel_Serial.Controls.Add(this.comboBoxBaudrate);
			this.panel_Serial.Controls.Add(this.comboBoxCom);
			this.panel_Serial.Controls.Add(this.label8);
			this.panel_Serial.Controls.Add(this.numericUpDownPanelAddress);
			this.panel_Serial.Location = new System.Drawing.Point(5, 431);
			this.panel_Serial.Name = "panel_Serial";
			this.panel_Serial.Size = new System.Drawing.Size(284, 120);
			this.panel_Serial.TabIndex = 52;
			this.label_Baudrate.Location = new System.Drawing.Point(7, 53);
			this.label_Baudrate.Name = "label_Baudrate";
			this.label_Baudrate.Size = new System.Drawing.Size(83, 17);
			this.label_Baudrate.TabIndex = 32;
			this.label_Baudrate.Text = "波特率";
			this.label_Baudrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label_PortName.Location = new System.Drawing.Point(9, 8);
			this.label_PortName.Name = "label_PortName";
			this.label_PortName.Size = new System.Drawing.Size(83, 17);
			this.label_PortName.TabIndex = 31;
			this.label_PortName.Text = "串口";
			this.label_PortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button_Add_Advance.Location = new System.Drawing.Point(235, 92);
			this.button_Add_Advance.Name = "button_Add_Advance";
			this.button_Add_Advance.Size = new System.Drawing.Size(46, 21);
			this.button_Add_Advance.TabIndex = 53;
			this.button_Add_Advance.Text = "设置";
			this.button_Add_Advance.UseVisualStyleBackColor = true;
			this.button_Add_Advance.Click += new EventHandler(this.button_Add_Advance_Click);
			this.comboBoxBaudrate.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxBaudrate.FormattingEnabled = true;
			this.comboBoxBaudrate.Items.AddRange(new object[]
			{
				"9600",
				"38400",
				"115200"
			});
			this.comboBoxBaudrate.Location = new System.Drawing.Point(98, 50);
			this.comboBoxBaudrate.Name = "comboBoxBaudrate";
			this.comboBoxBaudrate.Size = new System.Drawing.Size(134, 20);
			this.comboBoxBaudrate.TabIndex = 30;
			this.comboBoxBaudrate.SelectedIndexChanged += new EventHandler(this.comboBoxBaudrate_SelectedIndexChanged_1);
			this.comboBoxCom.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxCom.FormattingEnabled = true;
			this.comboBoxCom.Location = new System.Drawing.Point(98, 7);
			this.comboBoxCom.Name = "comboBoxCom";
			this.comboBoxCom.Size = new System.Drawing.Size(134, 20);
			this.comboBoxCom.TabIndex = 29;
			this.comboBoxCom.SelectedIndexChanged += new EventHandler(this.comboBoxBaudrate_SelectedIndexChanged);
			this.label8.Location = new System.Drawing.Point(7, 93);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(85, 17);
			this.label8.TabIndex = 29;
			this.label8.Text = "显示屏地址";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.numericUpDownPanelAddress.Location = new System.Drawing.Point(98, 92);
			NumericUpDown arg_1E46_0 = this.numericUpDownPanelAddress;
			int[] array = new int[4];
			array[0] = 65535;
			arg_1E46_0.Maximum = new decimal(array);
			NumericUpDown arg_1E62_0 = this.numericUpDownPanelAddress;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_1E62_0.Minimum = new decimal(array2);
			this.numericUpDownPanelAddress.Name = "numericUpDownPanelAddress";
			this.numericUpDownPanelAddress.Size = new System.Drawing.Size(134, 21);
			this.numericUpDownPanelAddress.TabIndex = 41;
			NumericUpDown arg_1EB5_0 = this.numericUpDownPanelAddress;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_1EB5_0.Value = new decimal(array3);
			this.numericUpDownPanelAddress.ValueChanged += new EventHandler(this.numericUpDownPanelAddress_ValueChanged);
			this.btnReadPanelParameter.Location = new System.Drawing.Point(175, 20);
			this.btnReadPanelParameter.Name = "btnReadPanelParameter";
			this.btnReadPanelParameter.Size = new System.Drawing.Size(73, 23);
			this.btnReadPanelParameter.TabIndex = 38;
			this.btnReadPanelParameter.Text = "回读屏参";
			this.btnReadPanelParameter.UseVisualStyleBackColor = true;
			this.btnReadPanelParameter.Click += new EventHandler(this.btnReadPanelParameter_Click);
			this.onlineVersion.Location = new System.Drawing.Point(56, 45);
			this.onlineVersion.Name = "onlineVersion";
			this.onlineVersion.Size = new System.Drawing.Size(191, 20);
			this.onlineVersion.TabIndex = 37;
			this.onlineVersion.Text = "--";
			this.onlineVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label11.Location = new System.Drawing.Point(20, 25);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(85, 17);
			this.label11.TabIndex = 36;
			this.label11.Text = "联机版本号";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonSave.Location = new System.Drawing.Point(182, 14);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(60, 23);
			this.buttonSave.TabIndex = 35;
			this.buttonSave.Text = "关闭";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new EventHandler(this.buttonSave_Click);
			this.btnLoadPanelParameter.Location = new System.Drawing.Point(32, 14);
			this.btnLoadPanelParameter.Name = "btnLoadPanelParameter";
			this.btnLoadPanelParameter.Size = new System.Drawing.Size(60, 23);
			this.btnLoadPanelParameter.TabIndex = 34;
			this.btnLoadPanelParameter.Text = "加载";
			this.btnLoadPanelParameter.UseVisualStyleBackColor = true;
			this.btnLoadPanelParameter.Click += new EventHandler(this.btnLoadPanelParameter_Click);
			this.groupBox2.Controls.Add(this.radioButtonUSB);
			this.groupBox2.Controls.Add(this.radioButtonWifi);
			this.groupBox2.Controls.Add(this.radioButtonSerial);
			this.groupBox2.Controls.Add(this.radioButtonEnthernet);
			this.groupBox2.Controls.Add(this.radioButtonGPRS);
			this.groupBox2.Location = new System.Drawing.Point(5, 384);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(281, 42);
			this.groupBox2.TabIndex = 27;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "通讯方式";
			this.radioButtonUSB.AutoSize = true;
			this.radioButtonUSB.Checked = true;
			this.radioButtonUSB.Location = new System.Drawing.Point(162, 19);
			this.radioButtonUSB.Name = "radioButtonUSB";
			this.radioButtonUSB.Size = new System.Drawing.Size(41, 16);
			this.radioButtonUSB.TabIndex = 0;
			this.radioButtonUSB.TabStop = true;
			this.radioButtonUSB.Text = "U盘";
			this.radioButtonUSB.UseVisualStyleBackColor = true;
			this.radioButtonUSB.CheckedChanged += new EventHandler(this.radioButtonUSB_CheckedChanged);
			this.radioButtonWifi.AutoSize = true;
			this.radioButtonWifi.Location = new System.Drawing.Point(228, 19);
			this.radioButtonWifi.Name = "radioButtonWifi";
			this.radioButtonWifi.Size = new System.Drawing.Size(47, 16);
			this.radioButtonWifi.TabIndex = 29;
			this.radioButtonWifi.TabStop = true;
			this.radioButtonWifi.Text = "WiFi";
			this.radioButtonWifi.UseVisualStyleBackColor = true;
			this.radioButtonWifi.CheckedChanged += new EventHandler(this.radioButtonWifi_CheckedChanged);
			this.radioButtonSerial.AutoSize = true;
			this.radioButtonSerial.Location = new System.Drawing.Point(6, 20);
			this.radioButtonSerial.Name = "radioButtonSerial";
			this.radioButtonSerial.Size = new System.Drawing.Size(47, 16);
			this.radioButtonSerial.TabIndex = 0;
			this.radioButtonSerial.TabStop = true;
			this.radioButtonSerial.Text = "串口";
			this.radioButtonSerial.UseVisualStyleBackColor = true;
			this.radioButtonSerial.CheckedChanged += new EventHandler(this.radioButtonSerial_CheckedChanged);
			this.radioButtonEnthernet.AutoSize = true;
			this.radioButtonEnthernet.Location = new System.Drawing.Point(78, 19);
			this.radioButtonEnthernet.Name = "radioButtonEnthernet";
			this.radioButtonEnthernet.Size = new System.Drawing.Size(59, 16);
			this.radioButtonEnthernet.TabIndex = 1;
			this.radioButtonEnthernet.TabStop = true;
			this.radioButtonEnthernet.Text = "以太网";
			this.radioButtonEnthernet.UseVisualStyleBackColor = true;
			this.radioButtonEnthernet.CheckedChanged += new EventHandler(this.radioButtonEnthernet_CheckedChanged);
			this.radioButtonGPRS.AutoSize = true;
			this.radioButtonGPRS.Location = new System.Drawing.Point(80, 19);
			this.radioButtonGPRS.Name = "radioButtonGPRS";
			this.radioButtonGPRS.Size = new System.Drawing.Size(53, 16);
			this.radioButtonGPRS.TabIndex = 30;
			this.radioButtonGPRS.TabStop = true;
			this.radioButtonGPRS.Text = "GPRS ";
			this.radioButtonGPRS.UseVisualStyleBackColor = true;
			this.radioButtonGPRS.CheckedChanged += new EventHandler(this.radioButtonGPRS_CheckedChanged);
			this.manualRoutingSettingButton.Enabled = false;
			this.manualRoutingSettingButton.Location = new System.Drawing.Point(804, 215);
			this.manualRoutingSettingButton.Name = "manualRoutingSettingButton";
			this.manualRoutingSettingButton.Size = new System.Drawing.Size(61, 23);
			this.manualRoutingSettingButton.TabIndex = 26;
			this.manualRoutingSettingButton.Text = "...";
			this.manualRoutingSettingButton.UseVisualStyleBackColor = true;
			this.manualRoutingSettingButton.Visible = false;
			this.manualRoutingSettingButton.Click += new EventHandler(this.manualRoutingSettingButton_Click);
			this.label7.Location = new System.Drawing.Point(20, 195);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(85, 17);
			this.label7.TabIndex = 25;
			this.label7.Text = "走线方式";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBoxRouting.Location = new System.Drawing.Point(111, 197);
			this.textBoxRouting.Multiline = true;
			this.textBoxRouting.Name = "textBoxRouting";
			this.textBoxRouting.ReadOnly = true;
			this.textBoxRouting.Size = new System.Drawing.Size(129, 62);
			this.textBoxRouting.TabIndex = 24;
			this.label6.Enabled = false;
			this.label6.Location = new System.Drawing.Point(778, 174);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(85, 17);
			this.label6.TabIndex = 23;
			this.label6.Text = "扫描方式";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label6.Visible = false;
			this.comboBoxScanType.Enabled = false;
			this.comboBoxScanType.FormattingEnabled = true;
			this.comboBoxScanType.ImeMode = ImeMode.On;
			this.comboBoxScanType.Items.AddRange(new object[]
			{
				"1/4",
				"1/8",
				"1/16"
			});
			this.comboBoxScanType.Location = new System.Drawing.Point(824, 173);
			this.comboBoxScanType.Name = "comboBoxScanType";
			this.comboBoxScanType.Size = new System.Drawing.Size(129, 20);
			this.comboBoxScanType.TabIndex = 22;
			this.comboBoxScanType.Visible = false;
			this.comboBoxScanType.SelectedIndexChanged += new EventHandler(this.comboBoxScanType_SelectedIndexChanged);
			this.label4.Location = new System.Drawing.Point(20, 337);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 17);
			this.label4.TabIndex = 18;
			this.label4.Text = "色彩模式";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label3.Location = new System.Drawing.Point(20, 142);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 17);
			this.label3.TabIndex = 17;
			this.label3.Text = "高度";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label2.Location = new System.Drawing.Point(20, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 17);
			this.label2.TabIndex = 16;
			this.label2.Text = "宽度";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			NumericUpDown arg_28A6_0 = this.numericUpDownHeigth;
			int[] array4 = new int[4];
			array4[0] = 8;
			arg_28A6_0.Increment = new decimal(array4);
			this.numericUpDownHeigth.Location = new System.Drawing.Point(111, 142);
			NumericUpDown arg_28E0_0 = this.numericUpDownHeigth;
			int[] array5 = new int[4];
			array5[0] = 10000;
			arg_28E0_0.Maximum = new decimal(array5);
			NumericUpDown arg_28FF_0 = this.numericUpDownHeigth;
			int[] array6 = new int[4];
			array6[0] = 8;
			arg_28FF_0.Minimum = new decimal(array6);
			this.numericUpDownHeigth.Name = "numericUpDownHeigth";
			this.numericUpDownHeigth.Size = new System.Drawing.Size(129, 21);
			this.numericUpDownHeigth.TabIndex = 15;
			NumericUpDown arg_2953_0 = this.numericUpDownHeigth;
			int[] array7 = new int[4];
			array7[0] = 16;
			arg_2953_0.Value = new decimal(array7);
			this.numericUpDownHeigth.ValueChanged += new EventHandler(this.numericUpDownHeigth_ValueChanged);
			NumericUpDown arg_298A_0 = this.numericUpDownWidth;
			int[] array8 = new int[4];
			array8[0] = 16;
			arg_298A_0.Increment = new decimal(array8);
			this.numericUpDownWidth.Location = new System.Drawing.Point(111, 116);
			NumericUpDown arg_29C1_0 = this.numericUpDownWidth;
			int[] array9 = new int[4];
			array9[0] = 20000;
			arg_29C1_0.Maximum = new decimal(array9);
			NumericUpDown arg_29E1_0 = this.numericUpDownWidth;
			int[] array10 = new int[4];
			array10[0] = 16;
			arg_29E1_0.Minimum = new decimal(array10);
			this.numericUpDownWidth.Name = "numericUpDownWidth";
			this.numericUpDownWidth.Size = new System.Drawing.Size(129, 21);
			this.numericUpDownWidth.TabIndex = 14;
			NumericUpDown arg_2A35_0 = this.numericUpDownWidth;
			int[] array11 = new int[4];
			array11[0] = 32;
			arg_2A35_0.Value = new decimal(array11);
			this.numericUpDownWidth.ValueChanged += new EventHandler(this.numericUpDownWidth_ValueChanged);
			this.label1.Location = new System.Drawing.Point(20, 90);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 17);
			this.label1.TabIndex = 13;
			this.label1.Text = "屏幕描述";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBoxDescription.Location = new System.Drawing.Point(111, 90);
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.Size = new System.Drawing.Size(129, 21);
			this.textBoxDescription.TabIndex = 12;
			this.textBoxDescription.TextChanged += new EventHandler(this.textBoxDescription_TextChanged);
			this.comboBoxModel.DropDownHeight = 350;
			this.comboBoxModel.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxModel.FormattingEnabled = true;
			this.comboBoxModel.IntegralHeight = false;
			this.comboBoxModel.Location = new System.Drawing.Point(111, 66);
			this.comboBoxModel.Name = "comboBoxModel";
			this.comboBoxModel.Size = new System.Drawing.Size(129, 20);
			this.comboBoxModel.TabIndex = 11;
			this.comboBoxModel.SelectedIndexChanged += new EventHandler(this.comboBoxModel_SelectedIndexChanged);
			this.labelType.Location = new System.Drawing.Point(20, 69);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(85, 17);
			this.labelType.TabIndex = 10;
			this.labelType.Text = "型号";
			this.labelType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.comboBoxColorMode.DrawMode = DrawMode.OwnerDrawFixed;
			this.comboBoxColorMode.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxColorMode.FormattingEnabled = true;
			this.comboBoxColorMode.ImeMode = ImeMode.On;
			this.comboBoxColorMode.Items.AddRange(new object[]
			{
				"1",
				"1",
				"1",
				"1",
				"1",
				"1",
				"1",
				"1",
				"1"
			});
			this.comboBoxColorMode.Location = new System.Drawing.Point(111, 334);
			this.comboBoxColorMode.Name = "comboBoxColorMode";
			this.comboBoxColorMode.Size = new System.Drawing.Size(129, 22);
			this.comboBoxColorMode.TabIndex = 8;
			this.comboBoxColorMode.DrawItem += new DrawItemEventHandler(this.comboBoxType_DrawItem);
			this.comboBoxColorMode.SelectedIndexChanged += new EventHandler(this.comboBoxColorMode_SelectedIndexChanged);
			this.cmsPanel.Items.AddRange(new ToolStripItem[]
			{
				this.tsmiMoveToGroup
			});
			this.cmsPanel.Name = "cmsPanel";
			this.cmsPanel.Size = new System.Drawing.Size(146, 26);
			this.tsmiMoveToGroup.Name = "tsmiMoveToGroup";
			this.tsmiMoveToGroup.Size = new System.Drawing.Size(145, 22);
			this.tsmiMoveToGroup.Text = "移动分组到...";
			this.tsmiMoveToGroup.Click += new EventHandler(this.tsmiMoveToGroup_Click);
			this.label14_OE.Location = new System.Drawing.Point(3, 285);
			this.label14_OE.Name = "label14_OE";
			this.label14_OE.Size = new System.Drawing.Size(102, 17);
			this.label14_OE.TabIndex = 50;
			this.label14_OE.Text = "OE极性";
			this.label14_OE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.comboBox_Express.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_Express.FormattingEnabled = true;
			this.comboBox_Express.Items.AddRange(new object[]
			{
				"常规P10",
				"P10表贴双反",
				"P10恒流 12 SM16188B",
				"P10恒流 08 SM16188B",
				"常规P16",
				"常规户内",
				"强力P10",
				"常规P13.33全彩",
				"户内表贴",
				"P10 6650"
			});
			this.comboBox_Express.Location = new System.Drawing.Point(111, 169);
			this.comboBox_Express.Name = "comboBox_Express";
			this.comboBox_Express.Size = new System.Drawing.Size(129, 20);
			this.comboBox_Express.TabIndex = 49;
			this.comboBox_Express.SelectedIndexChanged += new EventHandler(this.comboBox_Express_SelectedIndexChanged);
			this.comboBoxOE.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxOE.FormattingEnabled = true;
			this.comboBoxOE.ImeMode = ImeMode.On;
			this.comboBoxOE.Items.AddRange(new object[]
			{
				"低电平有效",
				"高电平有效"
			});
			this.comboBoxOE.Location = new System.Drawing.Point(111, 285);
			this.comboBoxOE.Name = "comboBoxOE";
			this.comboBoxOE.Size = new System.Drawing.Size(129, 20);
			this.comboBoxOE.TabIndex = 49;
			this.comboBoxOE.SelectedIndexChanged += new EventHandler(this.comboBoxOE_SelectedIndexChanged_1);
			this.button_Advanced.Location = new System.Drawing.Point(111, 257);
			this.button_Advanced.Name = "button_Advanced";
			this.button_Advanced.Size = new System.Drawing.Size(129, 23);
			this.button_Advanced.TabIndex = 48;
			this.button_Advanced.Text = "高级设置";
			this.button_Advanced.UseVisualStyleBackColor = true;
			this.button_Advanced.Click += new EventHandler(this.button_Advanced_Click);
			this.label1_Express.Location = new System.Drawing.Point(20, 169);
			this.label1_Express.Name = "label1_Express";
			this.label1_Express.Size = new System.Drawing.Size(85, 17);
			this.label1_Express.TabIndex = 45;
			this.label1_Express.Text = "快捷配置";
			this.label1_Express.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label12.Location = new System.Drawing.Point(20, 363);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(85, 17);
			this.label12.TabIndex = 42;
			this.label12.Text = "灰度级别";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Label_Data.Location = new System.Drawing.Point(3, 311);
			this.Label_Data.Name = "Label_Data";
			this.Label_Data.Size = new System.Drawing.Size(102, 17);
			this.Label_Data.TabIndex = 47;
			this.Label_Data.Text = "数据极性";
			this.Label_Data.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.comboBoxGradation.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxGradation.FormattingEnabled = true;
			this.comboBoxGradation.ImeMode = ImeMode.On;
			this.comboBoxGradation.Items.AddRange(new object[]
			{
				"无灰度",
				"4级灰度",
				"8级灰度",
				"16级灰度",
				"32级灰度",
				"64级灰度",
				"128级灰度",
				"256级灰度"
			});
			this.comboBoxGradation.Location = new System.Drawing.Point(111, 361);
			this.comboBoxGradation.Name = "comboBoxGradation";
			this.comboBoxGradation.Size = new System.Drawing.Size(129, 20);
			this.comboBoxGradation.TabIndex = 43;
			this.comboBoxGradation.SelectedIndexChanged += new EventHandler(this.comboBoxGradation_SelectedIndexChanged);
			this.comboBoxData.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxData.FormattingEnabled = true;
			this.comboBoxData.ImeMode = ImeMode.On;
			this.comboBoxData.Items.AddRange(new object[]
			{
				"低电平有效",
				"高电平有效"
			});
			this.comboBoxData.Location = new System.Drawing.Point(111, 309);
			this.comboBoxData.Name = "comboBoxData";
			this.comboBoxData.Size = new System.Drawing.Size(129, 20);
			this.comboBoxData.TabIndex = 48;
			this.comboBoxData.SelectedIndexChanged += new EventHandler(this.comboBoxData_SelectedIndexChanged_1);
			this.checkBox_OE.AutoSize = true;
			this.checkBox_OE.Enabled = false;
			this.checkBox_OE.Location = new System.Drawing.Point(819, 245);
			this.checkBox_OE.Name = "checkBox_OE";
			this.checkBox_OE.Size = new System.Drawing.Size(60, 16);
			this.checkBox_OE.TabIndex = 47;
			this.checkBox_OE.Text = "OE极性";
			this.checkBox_OE.UseVisualStyleBackColor = true;
			this.checkBox_OE.Visible = false;
			this.checkBox_OE.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
			this.checkBox_Data.AutoSize = true;
			this.checkBox_Data.Enabled = false;
			this.checkBox_Data.Location = new System.Drawing.Point(908, 245);
			this.checkBox_Data.Name = "checkBox_Data";
			this.checkBox_Data.Size = new System.Drawing.Size(72, 16);
			this.checkBox_Data.TabIndex = 46;
			this.checkBox_Data.Text = "数据极性";
			this.checkBox_Data.UseVisualStyleBackColor = true;
			this.checkBox_Data.Visible = false;
			this.checkBox_Data.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
			this.listBox_Express.Enabled = false;
			this.listBox_Express.FormattingEnabled = true;
			this.listBox_Express.ItemHeight = 12;
			this.listBox_Express.Items.AddRange(new object[]
			{
				"常规P10",
				"常规P16",
				"常规户内",
				"常规P16全彩(门头全彩)"
			});
			this.listBox_Express.Location = new System.Drawing.Point(824, 319);
			this.listBox_Express.Name = "listBox_Express";
			this.listBox_Express.Size = new System.Drawing.Size(129, 52);
			this.listBox_Express.TabIndex = 44;
			this.listBox_Express.Visible = false;
			this.listBox_Express.SelectedIndexChanged += new EventHandler(this.listBox_Express_SelectedIndexChanged);
			this.zhGroupBox2.Controls.Add(this.panel_Set);
			this.zhGroupBox2.Controls.Add(this.tvwPanel);
			this.zhGroupBox2.Location = new System.Drawing.Point(7, 2);
			this.zhGroupBox2.Name = "zhGroupBox2";
			this.zhGroupBox2.Size = new System.Drawing.Size(188, 600);
			this.zhGroupBox2.TabIndex = 48;
			this.zhGroupBox2.TabStop = false;
			this.panel_Set.Controls.Add(this.Delete_GroupOrPanelButton);
			this.panel_Set.Controls.Add(this.Add_Group_Button);
			this.panel_Set.Controls.Add(this.Add_Panel_Button);
			this.panel_Set.Location = new System.Drawing.Point(5, 13);
			this.panel_Set.Name = "panel_Set";
			this.panel_Set.Size = new System.Drawing.Size(177, 30);
			this.panel_Set.TabIndex = 64;
			this.Delete_GroupOrPanelButton.BackgroundImageLayout = ImageLayout.Stretch;
			this.Delete_GroupOrPanelButton.FlatAppearance.BorderSize = 0;
			this.Delete_GroupOrPanelButton.FlatStyle = FlatStyle.Flat;
			this.Delete_GroupOrPanelButton.Image = Resources.cross;
			this.Delete_GroupOrPanelButton.Location = new System.Drawing.Point(96, 5);
			this.Delete_GroupOrPanelButton.Name = "Delete_GroupOrPanelButton";
			this.Delete_GroupOrPanelButton.Size = new System.Drawing.Size(24, 24);
			this.Delete_GroupOrPanelButton.TabIndex = 54;
			this.Delete_GroupOrPanelButton.UseVisualStyleBackColor = true;
			this.Delete_GroupOrPanelButton.Click += new EventHandler(this.Delete_GroupOrPanelButton_Click);
			this.Add_Group_Button.BackgroundImageLayout = ImageLayout.Stretch;
			this.Add_Group_Button.FlatAppearance.BorderSize = 0;
			this.Add_Group_Button.FlatStyle = FlatStyle.Flat;
			this.Add_Group_Button.Image = Resources.folder_horizontal;
			this.Add_Group_Button.Location = new System.Drawing.Point(8, 4);
			this.Add_Group_Button.Name = "Add_Group_Button";
			this.Add_Group_Button.Size = new System.Drawing.Size(24, 24);
			this.Add_Group_Button.TabIndex = 52;
			this.Add_Group_Button.UseVisualStyleBackColor = true;
			this.Add_Group_Button.Click += new EventHandler(this.Add_Group_Button_Click);
			this.Add_Panel_Button.BackgroundImageLayout = ImageLayout.Stretch;
			this.Add_Panel_Button.FlatAppearance.BorderSize = 0;
			this.Add_Panel_Button.FlatStyle = FlatStyle.Flat;
			this.Add_Panel_Button.Image = Resources.monitor;
			this.Add_Panel_Button.Location = new System.Drawing.Point(52, 4);
			this.Add_Panel_Button.Name = "Add_Panel_Button";
			this.Add_Panel_Button.Size = new System.Drawing.Size(24, 24);
			this.Add_Panel_Button.TabIndex = 53;
			this.Add_Panel_Button.UseVisualStyleBackColor = true;
			this.Add_Panel_Button.Click += new EventHandler(this.Add_Panel_Button_Click);
			this.zhGroupBox3.Controls.Add(this.zhGroupBoxButton);
			this.zhGroupBox3.Controls.Add(this.lblWidthModule);
			this.zhGroupBox3.Controls.Add(this.lblHeightModule);
			this.zhGroupBox3.Controls.Add(this.nudHeigthModule);
			this.zhGroupBox3.Controls.Add(this.nudWidthModule);
			this.zhGroupBox3.Controls.Add(this.btnReadPanelParameter);
			this.zhGroupBox3.Controls.Add(this.panel_Serial);
			this.zhGroupBox3.Controls.Add(this.groupBox2);
			this.zhGroupBox3.Controls.Add(this.label14_OE);
			this.zhGroupBox3.Controls.Add(this.label4);
			this.zhGroupBox3.Controls.Add(this.comboBox_Express);
			this.zhGroupBox3.Controls.Add(this.label3);
			this.zhGroupBox3.Controls.Add(this.comboBoxOE);
			this.zhGroupBox3.Controls.Add(this.label2);
			this.zhGroupBox3.Controls.Add(this.button_Advanced);
			this.zhGroupBox3.Controls.Add(this.label1_Express);
			this.zhGroupBox3.Controls.Add(this.numericUpDownHeigth);
			this.zhGroupBox3.Controls.Add(this.label12);
			this.zhGroupBox3.Controls.Add(this.Label_Data);
			this.zhGroupBox3.Controls.Add(this.numericUpDownWidth);
			this.zhGroupBox3.Controls.Add(this.comboBoxGradation);
			this.zhGroupBox3.Controls.Add(this.label1);
			this.zhGroupBox3.Controls.Add(this.onlineVersion);
			this.zhGroupBox3.Controls.Add(this.comboBoxData);
			this.zhGroupBox3.Controls.Add(this.textBoxDescription);
			this.zhGroupBox3.Controls.Add(this.label7);
			this.zhGroupBox3.Controls.Add(this.comboBoxModel);
			this.zhGroupBox3.Controls.Add(this.label11);
			this.zhGroupBox3.Controls.Add(this.labelType);
			this.zhGroupBox3.Controls.Add(this.textBoxRouting);
			this.zhGroupBox3.Controls.Add(this.comboBoxColorMode);
			this.zhGroupBox3.Location = new System.Drawing.Point(198, 2);
			this.zhGroupBox3.Name = "zhGroupBox3";
			this.zhGroupBox3.Size = new System.Drawing.Size(292, 600);
			this.zhGroupBox3.TabIndex = 49;
			this.zhGroupBox3.TabStop = false;
			this.zhGroupBoxButton.Controls.Add(this.btnLoadPanelParameter);
			this.zhGroupBoxButton.Controls.Add(this.buttonSave);
			this.zhGroupBoxButton.Location = new System.Drawing.Point(5, 551);
			this.zhGroupBoxButton.Name = "zhGroupBoxButton";
			this.zhGroupBoxButton.Size = new System.Drawing.Size(281, 43);
			this.zhGroupBoxButton.TabIndex = 58;
			this.zhGroupBoxButton.TabStop = false;
			this.lblWidthModule.Location = new System.Drawing.Point(243, 120);
			this.lblWidthModule.Name = "lblWidthModule";
			this.lblWidthModule.Size = new System.Drawing.Size(44, 17);
			this.lblWidthModule.TabIndex = 57;
			this.lblWidthModule.Text = "(模组)";
			this.lblWidthModule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblHeightModule.Location = new System.Drawing.Point(243, 143);
			this.lblHeightModule.Name = "lblHeightModule";
			this.lblHeightModule.Size = new System.Drawing.Size(44, 17);
			this.lblHeightModule.TabIndex = 56;
			this.lblHeightModule.Text = "(模组)";
			this.lblHeightModule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			NumericUpDown arg_3C6D_0 = this.nudHeigthModule;
			int[] array12 = new int[4];
			array12[0] = 8;
			arg_3C6D_0.Increment = new decimal(array12);
			this.nudHeigthModule.Location = new System.Drawing.Point(176, 142);
			NumericUpDown arg_3CAA_0 = this.nudHeigthModule;
			int[] array13 = new int[4];
			array13[0] = 10000;
			arg_3CAA_0.Maximum = new decimal(array13);
			NumericUpDown arg_3CC9_0 = this.nudHeigthModule;
			int[] array14 = new int[4];
			array14[0] = 8;
			arg_3CC9_0.Minimum = new decimal(array14);
			this.nudHeigthModule.Name = "nudHeigthModule";
			this.nudHeigthModule.Size = new System.Drawing.Size(64, 21);
			this.nudHeigthModule.TabIndex = 55;
			NumericUpDown arg_3D1A_0 = this.nudHeigthModule;
			int[] array15 = new int[4];
			array15[0] = 16;
			arg_3D1A_0.Value = new decimal(array15);
			this.nudHeigthModule.ValueChanged += new EventHandler(this.nudHeigthModule_ValueChanged);
			this.nudHeigthModule.KeyDown += new KeyEventHandler(this.nudHeigthModule_KeyDown);
			NumericUpDown arg_3D68_0 = this.nudWidthModule;
			int[] array16 = new int[4];
			array16[0] = 16;
			arg_3D68_0.Increment = new decimal(array16);
			this.nudWidthModule.Location = new System.Drawing.Point(176, 116);
			NumericUpDown arg_3DA2_0 = this.nudWidthModule;
			int[] array17 = new int[4];
			array17[0] = 20000;
			arg_3DA2_0.Maximum = new decimal(array17);
			NumericUpDown arg_3DC2_0 = this.nudWidthModule;
			int[] array18 = new int[4];
			array18[0] = 16;
			arg_3DC2_0.Minimum = new decimal(array18);
			this.nudWidthModule.Name = "nudWidthModule";
			this.nudWidthModule.Size = new System.Drawing.Size(64, 21);
			this.nudWidthModule.TabIndex = 54;
			NumericUpDown arg_3E13_0 = this.nudWidthModule;
			int[] array19 = new int[4];
			array19[0] = 32;
			arg_3E13_0.Value = new decimal(array19);
			this.nudWidthModule.ValueChanged += new EventHandler(this.nudWidthModule_ValueChanged);
			this.nudWidthModule.KeyDown += new KeyEventHandler(this.nudWidthModule_KeyDown);
			this.zhGroupBox1.Controls.Add(this.button_Add_Advance_Mac);
			this.zhGroupBox1.Controls.Add(this.panel_GPRS);
			this.zhGroupBox1.Controls.Add(this.panel_Net);
			this.zhGroupBox1.Controls.Add(this.panel_USB);
			this.zhGroupBox1.Controls.Add(this.pictureBox2);
			this.zhGroupBox1.Controls.Add(this.panel_Wifi);
			this.zhGroupBox1.Controls.Add(this.productMessageLabel);
			this.zhGroupBox1.Controls.Add(this.ELMessageLabel);
			this.zhGroupBox1.Controls.Add(this.label13);
			this.zhGroupBox1.Location = new System.Drawing.Point(496, 2);
			this.zhGroupBox1.Name = "zhGroupBox1";
			this.zhGroupBox1.Size = new System.Drawing.Size(478, 600);
			this.zhGroupBox1.TabIndex = 50;
			this.zhGroupBox1.TabStop = false;
			this.zhGroupBox1.Text = "产品规格";
			this.panel1.Controls.Add(this.button_ChangeWifiSSID);
			this.panel1.Controls.Add(this.button_ChangeWifiPassword);
			this.panel1.Location = new System.Drawing.Point(996, 344);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(284, 120);
			this.panel1.TabIndex = 56;
			this.timerUsbSelect.Interval = 1000;
			this.timerUsbSelect.Tick += new EventHandler(this.timerUsbSelect_Tick);
			this.timerWifiConnectionStatus.Interval = 1000;
			this.timerWifiConnectionStatus.Tick += new EventHandler(this.timerWifiConnectionStatus_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(977, 605);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.zhGroupBox1);
			base.Controls.Add(this.zhGroupBox3);
			base.Controls.Add(this.zhGroupBox2);
			base.Controls.Add(this.listBox_Express);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.manualRoutingSettingButton);
			base.Controls.Add(this.comboBoxScanType);
			base.Controls.Add(this.checkBox_OE);
			base.Controls.Add(this.checkBox_Data);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formPanelEdit";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "显示屏设置";
			base.FormClosing += new FormClosingEventHandler(this.formPanelEdit_FormClosing);
			base.Load += new EventHandler(this.formPanelEdit_Load);
			this.panel_GPRS.ResumeLayout(false);
			this.panel_GPRS.PerformLayout();
			this.panel_Wifi.ResumeLayout(false);
			this.panel_Wifi.PerformLayout();
			this.panel_USB.ResumeLayout(false);
			this.panel_Net.ResumeLayout(false);
			this.panel_Net.PerformLayout();
			((ISupportInitialize)this.pictureBox2).EndInit();
			this.panel_Serial.ResumeLayout(false);
			((ISupportInitialize)this.numericUpDownPanelAddress).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.numericUpDownHeigth).EndInit();
			((ISupportInitialize)this.numericUpDownWidth).EndInit();
			this.cmsPanel.ResumeLayout(false);
			this.zhGroupBox2.ResumeLayout(false);
			this.panel_Set.ResumeLayout(false);
			this.zhGroupBox3.ResumeLayout(false);
			this.zhGroupBox3.PerformLayout();
			this.zhGroupBoxButton.ResumeLayout(false);
			((ISupportInitialize)this.nudHeigthModule).EndInit();
			((ISupportInitialize)this.nudWidthModule).EndInit();
			this.zhGroupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
