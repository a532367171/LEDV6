using LedCommunication;
using LedControlSystem.Properties;
using LedModel;
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
using System.Threading;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem.LedControlSystem
{
	public class formPanelEditForForeignTrade : Form
	{
		private formMain mf;

		private static string formID = "formPanelEditForForeignTrade";

		private bool isGroup;

		private bool isFromFile;

		private LedPanel thisPanel;

		private TreeNode tnNoGroup;

		private bool isLoadParam;

		private int widthModuleUnit = 16;

		private int heightModuleUnit = 8;

		private int lastScanTypeIndex = -1;

		private int lastRoutingIndex = -1;

		private LedColorMode lastColorMode = LedColorMode.R;

		private bool isWidthModuleKeyDown;

		private bool isHeightModuleKeyDown;

		private IList<string> UdiskDirList;

		private string LastMessage = "";

		public bool IsConnectedToCorrectWifi;

		private string NowWifiName = string.Empty;

		private WlanClient client;

		private IContainer components;

		private TreeView tvwPanel;

		private ComboBox comboBoxColorMode;

		private Label label3;

		private Label label2;

		private NumericUpDown numericUpDownHeigth;

		private NumericUpDown numericUpDownWidth;

		private Label label4;

		private GroupBox groupBox2;

		private RadioButton radioButtonSerial;

		private Label label8;

		private NumericUpDown numericUpDownPanelAddress;

		private ComboBox comboBoxBaudrate;

		private ComboBox comboBoxCom;

		private Button button_Add_Advance;

		private Panel panel_Serial;

		private Label label_Baudrate;

		private Label label_PortName;

		private RadioButton radioButtonWifi;

		private RadioButton radioButtonUSB;

		private GroupBox zhGroupBox2;

		private GroupBox zhGroupBox3;

		private Label lblHeightModule;

		private NumericUpDown nudHeigthModule;

		private NumericUpDown nudWidthModule;

		private ToolTip toolTip_panel;

		private Label lblWidthModule;

		private Panel panel_USB;

		private Button buttonSave;

		private Button button_Advanced;

		private GroupBox groupBox1;

		private Panel panel_WIFI;

		private Label label_SelectUSB;

		private ComboBox UsbListComboBox;

		private System.Windows.Forms.Timer timerUsbSelect;

		private Label label_Remind;

		private TextBox TxtWifiName;

		private Label LblWifiName;

		private System.Windows.Forms.Timer timerWifiConnectionStatus;

		private Label LblControlCardState;

		private RadioButton radioButtonEnthernet;

		private RadioButton radioButtonGPRS;

		private Panel panel_Net;

		private ComboBox CboSelectIPorDomain;

		private TextBox TxtCloudServerDoname;

		private MaskedTextBox maskedTextBoxCloudIPAddress;

		private TextBox TxtCloudPort;

		private Label LblCloudPort;

		private TextBox txtMacAddress;

		private Label lblMacAddress;

		private TextBox txtPassword;

		private TextBox txtIPPort;

		private RadioButton radioButton_RemoteServer;

		private Label lblPassword;

		private Label lblPort;

		private TextBox txtNetworkID;

		private Label lblNetworkID;

		private MaskedTextBox maskedTextBoxIPAddress;

		private RadioButton radioButton_localServer;

		private RadioButton radioButton_SendByIP;

		private RadioButton radioButton_SendBroadcast;

		private Label lblIPAddress;

		private Panel panel_GPRS;

		private Label LblGPRSMessage;

		private Panel PnlCloudServer;

		private Panel PnlSingle;

		private Panel PnlLocalServer;

		private Panel PnlFixedIP;

		private Button buttonLoad;

		public static string FormID
		{
			get
			{
				return formPanelEditForForeignTrade.formID;
			}
			set
			{
				formPanelEditForForeignTrade.formID = value;
			}
		}

		public formPanelEditForForeignTrade(formMain pMainForm)
		{
			this.InitializeComponent();
			this.mf = pMainForm;
			formMain.ML.NowFormID = formPanelEditForForeignTrade.formID;
			this.Text = formMain.ML.GetStr("formpaneledit_FormText");
			this.label_SelectUSB.Text = formMain.ML.GetStr("formUSBWrite_label_SelUdisk");
			this.LblWifiName.Text = formMain.ML.GetStr("formpaneledit_label_WiFi_Name");
			this.label2.Text = formMain.ML.GetStr("formpaneledit_label_width");
			this.label3.Text = formMain.ML.GetStr("formpaneledit_label_Height");
			this.lblWidthModule.Text = formMain.ML.GetStr("formpaneledit_lblWidthModule");
			this.lblHeightModule.Text = formMain.ML.GetStr("formpaneledit_lblHeightModule");
			this.groupBox2.Text = formMain.ML.GetStr("formpaneledit_groupBox_CommunicationMethod");
			this.radioButtonSerial.Text = formMain.ML.GetStr("formpaneledit_radioButtonSerial");
			this.radioButtonEnthernet.Text = formMain.ML.GetStr("formpaneledit_radioButtonEnthernet");
			this.radioButtonUSB.Text = formMain.ML.GetStr("formpaneledit_radioButtonUSB");
			this.radioButtonWifi.Text = formMain.ML.GetStr("formpaneledit_radioButtonWifi");
			this.label_PortName.Text = formMain.ML.GetStr("formpaneledit_label_SerialPortName");
			this.label_Baudrate.Text = formMain.ML.GetStr("formpaneledit_label_Baudrate");
			this.label8.Text = formMain.ML.GetStr("formpaneledit_label8");
			this.button_Add_Advance.Text = formMain.ML.GetStr("formpaneledit_button_Add_Advance");
			this.LblGPRSMessage.Text = formMain.ML.GetStr("formpaneledit_label_prompt_GPRS_Send");
			this.radioButton_SendBroadcast.Text = formMain.ML.GetStr("formpaneledit_radioButton_SendBroadcast");
			this.radioButton_SendByIP.Text = formMain.ML.GetStr("formpaneledit_radioButton_SendByIP");
			this.radioButton_localServer.Text = formMain.ML.GetStr("formpaneledit_radioButton_localServer");
			this.radioButtonGPRS.Text = formMain.ML.GetStr("formpaneledit_radioButtonGPRS");
			this.CboSelectIPorDomain.Text = formMain.ML.GetStr("formpaneledit_RdoDomainName");
			this.lblIPAddress.Text = formMain.ML.GetStr("formpaneledit_label_IPAddress");
			this.lblPort.Text = formMain.ML.GetStr("formpaneledit_label_Port");
			this.lblNetworkID.Text = formMain.ML.GetStr("formpaneledit_label_NetworkID");
			this.lblPassword.Text = formMain.ML.GetStr("formpaneledit_label_Password");
			this.lblMacAddress.Text = formMain.ML.GetStr("formpaneledit_label_MacAddress");
			this.radioButton_RemoteServer.Text = formMain.ML.GetStr("formpaneledit_radioButton_RemoteServer");
			this.buttonLoad.Text = formMain.ML.GetStr("formpaneledit_button_Load");
			this.buttonSave.Text = formMain.ML.GetStr("formpaneledit_button_Save");
			this.LblCloudPort.Text = formMain.ML.GetStr("formpaneledit_label_Port");
			this.button_Advanced.Text = formMain.ML.GetStr("formpaneledit_button_Advanced");
			this.label4.Text = formMain.ML.GetStr("formpaneledit_label_ColorMode");
			this.toolTip_panel.InitialDelay = 100;
			this.toolTip_panel.ReshowDelay = 100;
			this.CboSelectIPorDomain.Items[0] = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_IP");
			this.CboSelectIPorDomain.Items[1] = formMain.ML.GetStr("formpaneledit_RdoDomainName");
			this.toolTip_panel.InitialDelay = 100;
			this.toolTip_panel.ReshowDelay = 100;
		}

		public formPanelEditForForeignTrade()
		{
			this.InitializeComponent();
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
					for (int i = 0; i < formMain.ledsys.Groups.Count; i++)
					{
						TreeNode treeNode = new TreeNode();
						treeNode.Text = formMain.ledsys.Groups[i].Name;
						treeNode.Tag = formMain.ledsys.Groups[i];
						this.tvwPanel.Nodes.Add(treeNode);
						new ToolStripMenuItem(formMain.ledsys.Groups[i].Name)
						{
							ToolTipText = formMain.ledsys.Groups[i].Name,
							Tag = formMain.ledsys.Groups[i]
						}.Click += new EventHandler(this.PanelGroup_Click);
					}
					return;
				}
				LedGroup ledGroup = new LedGroup();
				ledGroup.Name = formMain.ML.GetStr("Display_Panel_NoGroup");
				ledGroup.Description = ledGroup.Name;
				ledsys.AddGroup(ledGroup);
				this.tnNoGroup = new TreeNode();
				this.tnNoGroup.Text = ledGroup.Name;
				this.tnNoGroup.Tag = ledGroup;
				this.tvwPanel.Nodes.Add(this.tnNoGroup);
			}
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
				this.buttonLoad.Enabled = false;
				this.numericUpDownWidth.Width = 136;
				this.numericUpDownHeigth.Width = 136;
				this.numericUpDownWidth.Enabled = true;
				this.numericUpDownHeigth.Enabled = true;
				this.nudWidthModule.Visible = false;
				this.nudHeigthModule.Visible = false;
				this.lblWidthModule.Visible = false;
				this.lblHeightModule.Visible = false;
				this.comboBoxColorMode.Enabled = true;
				this.label_Remind.Text = string.Empty;
				num = 2;
				if (text.IndexOf("U") > 0 && text.IndexOf("Uc") <= 0 && text.IndexOf("Um") <= 0 && text.IndexOf("Un") <= 0 && text.IndexOf("YU") <= 0 && text.IndexOf("G") > 0)
				{
					text.IndexOf("L");
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
					this.buttonLoad.Enabled = true;
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
					this.comboBoxColorMode.Enabled = false;
					flag = true;
				}
				if (text.IndexOf("M") > 0)
				{
					this.radioButtonUSB.Visible = true;
				}
				if (text.IndexOf("S") > 0)
				{
					this.comboBoxColorMode.Enabled = false;
					this.radioButtonSerial.Visible = true;
					this.radioButtonUSB.Visible = true;
				}
				if (text.IndexOf("PP") > 0)
				{
					this.buttonLoad.Enabled = true;
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
				if (this.thisPanel.IsLSeries() || this.thisPanel.CardType.ToString().IndexOf("YU") > 0)
				{
					this.thisPanel.ProtocolVersion = LedCommunicationConst.ProtocolSendVersionList[LedCommunicationConst.ProtocolSendVersionList.Length - 1];
				}
				this.lastScanTypeIndex = scanTypeIndex;
				this.lastRoutingIndex = routingIndex;
				this.lastColorMode = this.thisPanel.ColorMode;
				num = 6;
				this.SetHeightAndWidthStepByRoutingSetting(this.thisPanel.RoutingSetting);
				scanTypeIndex = this.thisPanel.RoutingSetting.ScanTypeIndex;
				routingIndex = this.thisPanel.RoutingSetting.RoutingIndex;
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
				num = 9;
				this.thisPanel.RoutingSetting.SettingFileName = LedCommonConst.RoutingSettingFileName;
				if (this.thisPanel.IsLSeries())
				{
					this.thisPanel.RoutingSetting.SettingFileName = LedCommonConst.RoutingSettingLFileName;
				}
				byte[] array = this.thisPanel.RoutingSetting.LoadScanFromFile();
				if (this.isFromFile)
				{
					this.isFromFile = false;
					if (array[0] == 1)
					{
						this.thisPanel.OEPolarity = 1;
					}
					else
					{
						this.thisPanel.OEPolarity = 0;
					}
					num = 10;
					if (array[1] == 1)
					{
						this.thisPanel.DataPolarity = 1;
					}
					else
					{
						this.thisPanel.DataPolarity = 0;
					}
				}
				if (this.tvwPanel != null && this.tvwPanel.Nodes.Count > 0)
				{
					string panelDescrible = this.GetPanelDescrible(this.thisPanel);
					TreeNode treeNode = null;
					foreach (TreeNode tnParent in this.tvwPanel.Nodes)
					{
						treeNode = this.FindNode(tnParent, panelDescrible);
						if (treeNode != null)
						{
							break;
						}
					}
					if (treeNode != null)
					{
						this.tvwPanel.SelectedNode = treeNode;
					}
					else if (this.tvwPanel.SelectedNode != null)
					{
						this.tvwPanel.SelectedNode.Text = panelDescrible;
					}
				}
				num = 11;
				this.comboBoxColorMode.SelectedIndex = this.thisPanel.ColorMode - LedColorMode.R;
				num = 13;
				switch (this.thisPanel.PortType)
				{
				case LedPortType.SerialPort:
					this.radioButtonSerial.Checked = true;
					this.panel_Serial.BringToFront();
					this.panel_Serial.Enabled = true;
					this.buttonLoad.Enabled = true;
					break;
				case LedPortType.Ethernet:
					this.maskedTextBoxIPAddress.Text = formPanelEditForForeignTrade.ToMaskedTextBoxIPAddress(this.thisPanel.IPAddress);
					this.txtMacAddress.Text = this.thisPanel.MACAddress;
					this.txtIPPort.Text = this.thisPanel.IPPort.ToString();
					this.txtNetworkID.Text = this.thisPanel.NetworkID;
					this.txtPassword.Text = this.thisPanel.AuthorityPassword;
					this.maskedTextBoxCloudIPAddress.Text = formPanelEditForForeignTrade.ToMaskedTextBoxIPAddress(this.thisPanel.CloudServerIPAddress);
					this.TxtCloudServerDoname.Text = this.thisPanel.CloudServerDomainName;
					this.TxtCloudPort.Text = this.thisPanel.CloudServerIPPort.ToString();
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
					this.buttonLoad.Enabled = true;
					this.radioButtonEnthernet.Checked = true;
					this.panel_Net.BringToFront();
					break;
				case LedPortType.USB:
					this.panel_USB.BringToFront();
					this.panel_USB.Visible = true;
					this.buttonLoad.Enabled = true;
					this.radioButtonUSB.Checked = true;
					break;
				case LedPortType.WiFi:
					this.panel_WIFI.BringToFront();
					this.panel_WIFI.Visible = true;
					this.buttonLoad.Enabled = true;
					this.radioButtonWifi.Checked = true;
					break;
				case LedPortType.GPRS:
					this.radioButtonGPRS.Checked = true;
					this.panel_GPRS.BringToFront();
					if (this.thisPanel.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer)
					{
						this.LblGPRSMessage.Text = formMain.ML.GetStr("formpaneledit_label_prompt_CloudServer_Send");
					}
					else
					{
						this.LblGPRSMessage.Text = formMain.ML.GetStr("formpaneledit_label_prompt_GPRS_Send");
					}
					break;
				}
				num = 14;
				this.numericUpDownPanelAddress.Value = this.thisPanel.CardAddress;
				this.comboBoxBaudrate.Text = this.thisPanel.BaudRate.ToString();
				this.comboBoxCom.Text = this.thisPanel.SerialPortName.ToString();
				num = 15;
				if (this.thisPanel.MACAddress.Length < 12)
				{
					this.thisPanel.MACAddress = this.thisPanel.MACAddress.PadLeft(12, 'F');
				}
				num = 16;
				num = 17;
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
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message + "," + num.ToString());
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
			IL_8E:
			while (treeNode2 != null && treeNode2 != tnParent)
			{
				while (treeNode2 != null)
				{
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
						//IL_76:
						while (treeNode2 != tnParent && treeNode2 == treeNode.LastNode)
						{
							treeNode2 = treeNode;
							treeNode = treeNode.Parent;
						}
						if (treeNode2 != tnParent)
						{
							treeNode2 = treeNode2.NextNode;
							goto IL_8E;
						}
						goto IL_8E;
					}
				}
				//goto IL_76;
			}
			return null;
		}

		private void SavePanelParam()
		{
			if (this.isLoadParam)
			{
				return;
			}
			if (this.thisPanel != null)
			{
				this.thisPanel.SerialPortName = this.comboBoxCom.Text;
				this.thisPanel.ColorMode = this.comboBoxColorMode.SelectedIndex + LedColorMode.R;
				if (this.radioButtonSerial.Checked)
				{
					this.thisPanel.PortType = LedPortType.SerialPort;
				}
			}
			this.ReLoadPanel();
		}

		private void LoadPanelToTree(LedPanel lp)
		{
			TreeNode treeNode = new TreeNode();
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
							if (treeNode2.Tag.GetType() != typeof(LedGroup))
							{
								return;
							}
							if (((LedGroup)treeNode2.Tag).Name.Equals(lp.Group))
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
						lp.Group = ((LedGroup)this.tnNoGroup.Tag).Name;
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
						this.isGroup = false;
					}
					else
					{
						this.isGroup = true;
					}
				}
				else
				{
					this.thisPanel = (LedPanel)selectedNode.Tag;
					this.isGroup = false;
				}
				this.SetControlEnabled();
			}
			catch
			{
				return;
			}
			if (!this.isGroup)
			{
				this.LoadPanelParam();
			}
		}

		private void tvwPanel_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;
		}

		private void SetControlEnabled()
		{
			foreach (Control control in this.zhGroupBox3.Controls)
			{
				if (control.Name != "zhGroupBoxButton")
				{
					control.Enabled = !this.isGroup;
				}
			}
			if (this.isGroup)
			{
				this.buttonLoad.Enabled = false;
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

		private void formPanelEditForForeignTrade_Load(object sender, EventArgs e)
		{
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
			this.zhGroupBox3.Controls.Add(this.panel_USB);
			this.zhGroupBox3.Controls.Add(this.panel_WIFI);
			this.zhGroupBox3.Controls.Add(this.panel_Net);
			this.zhGroupBox3.Controls.Add(this.panel_GPRS);
			this.panel_USB.Location = this.panel_Serial.Location;
			this.panel_USB.Size = this.panel_Serial.Size;
			this.panel_WIFI.Location = this.panel_Serial.Location;
			this.panel_WIFI.Size = this.panel_Serial.Size;
			this.panel_Net.Location = this.panel_Serial.Location;
			this.panel_Net.Size = this.panel_Serial.Size;
			this.panel_GPRS.Location = this.panel_Serial.Location;
			this.panel_GPRS.Size = this.panel_Serial.Size;
			this.panel_Net.Controls.Add(this.PnlCloudServer);
			this.panel_Net.Controls.Add(this.PnlSingle);
			this.panel_Net.Controls.Add(this.PnlLocalServer);
			this.PnlCloudServer.Size = this.PnlFixedIP.Size;
			this.PnlCloudServer.Location = this.PnlFixedIP.Location;
			this.PnlSingle.Size = this.PnlFixedIP.Size;
			this.PnlSingle.Location = this.PnlFixedIP.Location;
			this.PnlLocalServer.Size = this.PnlFixedIP.Size;
			this.PnlLocalServer.Location = this.PnlFixedIP.Location;
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
			this.tvwPanel.Nodes.Clear();
			this.LoadGroup();
			foreach (LedPanel current in formMain.Ledsys.Panels)
			{
				this.LoadPanelToTree(current);
			}
			this.thisPanel = formMain.Ledsys.SelectedPanel;
			this.LoadPanelParam();
		}

		private void numericUpDownWidth_LostFocus(object sender, EventArgs e)
		{
			try
			{
				int width = (int)this.numericUpDownWidth.Value;
				this.thisPanel.Width = width;
			}
			catch
			{
			}
		}

		private void numericUpDownHeigth_LostFocus(object sender, EventArgs e)
		{
			try
			{
				int height = (int)this.numericUpDownHeigth.Value;
				this.thisPanel.Height = height;
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (!this.isGroup)
			{
				this.SavePanelParam();
			}
			base.Close();
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
			formMain.ModifyPanelFromIPCServer(this.thisPanel);
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
				int num = (int)this.numericUpDownWidth.Value;
				if (num > 8192)
				{
					numericUpDown.Value = 8192m;
					this.thisPanel.Width = 8192;
				}
				else if (num < 8)
				{
					numericUpDown.Value = 8m;
					this.thisPanel.Width = 8;
				}
				else
				{
					this.thisPanel.Width = num;
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
				int num = (int)this.numericUpDownHeigth.Value;
				if (num > 512)
				{
					numericUpDown.Value = 512m;
					this.thisPanel.Height = 512;
				}
				else if (num < 8)
				{
					numericUpDown.Value = 8m;
					this.thisPanel.Height = 8;
				}
				else
				{
					this.thisPanel.Height = num;
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
				this.LoadPanelParam();
			}
		}

		private void comboBoxColorMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
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
			if (numericUpDown.Focused)
			{
				this.thisPanel.CardAddress = (int)numericUpDown.Value;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
			}
		}

		public void buttonLoad_Click(object sender, EventArgs e)
		{
			if (this.thisPanel.PortType == LedPortType.Ethernet && this.thisPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.Directly && MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Prompt_Load_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			if (this.thisPanel.PortType == LedPortType.USB)
			{
				this.LoadPanelParamOfUSB();
				return;
			}
			if (this.thisPanel.PortType == LedPortType.WiFi)
			{
				this.LoadPanelParamOfWIFI();
				return;
			}
			this.mf.SendSingleCmdStart(LedCmdType.Send_Panel_Parameter, this.thisPanel, this.buttonLoad.Text, this.thisPanel, false, this);
			if (formSendSingle.LastSendResult)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Prompt_LoadSuccessed"));
			}
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

		private void formPanelEditForForeignTrade_Load_FormClosing(object sender, FormClosingEventArgs e)
		{
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
					result = pValue;
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

		private void button_Advanced_Click(object sender, EventArgs e)
		{
			formMain.IsCompleteSet = true;
			base.Close();
		}

		private void button_Add_Advance_Click(object sender, EventArgs e)
		{
			formChangeAddress formChangeAddress = new formChangeAddress(this.thisPanel, this.mf);
			formChangeAddress.ShowDialog();
			this.comboBoxBaudrate.Text = this.thisPanel.BaudRate.ToString();
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
				this.buttonLoad.Enabled = false;
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
				this.buttonLoad.Enabled = this.IsNTFS(this.UdiskDirList[this.UsbListComboBox.SelectedIndex]);
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
					else
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
				if (this.UsbListComboBox.SelectedIndex == -1)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_NoFlashDisk"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
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
					byte[] array = protocol_data_integration.WritingData_USB_Pack_Protocol(processUSB, false);
					if (array == null)
					{
						base.Enabled = true;
						this.label_Remind.ForeColor = System.Drawing.Color.Red;
						this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Failed");
						Thread.Sleep(1000);
						this.timerUsbSelect.Start();
						this.LastMessage = "save";
					}
					else if (array.Length > this.thisPanel.GetFlashCapacity())
					{
						MessageBox.Show(this, formMain.ML.GetStr("Prompt_MemeoryOverSize"));
						base.Enabled = true;
						Thread.Sleep(1000);
						this.timerUsbSelect.Start();
						this.LastMessage = "save";
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
									this.label_Remind.ForeColor = System.Drawing.Color.Red;
									this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Failed") + "(" + ex.Message + ")";
									base.Enabled = true;
									Thread.Sleep(1000);
									this.timerUsbSelect.Start();
									this.LastMessage = "save";
									return;
								}
								Thread.Sleep(200);
							}
						}
						Thread.Sleep(500);
						if (!File.Exists(this.UdiskDirList[this.UsbListComboBox.SelectedIndex] + "\\ledV3.zh3"))
						{
							MessageBox.Show(this, formMain.ML.GetStr("Prompt_DataDownloadFailed"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
						}
						this.label_Remind.ForeColor = System.Drawing.Color.Blue;
						this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Success");
						this.buttonLoad.Enabled = true;
						base.Enabled = true;
						Thread.Sleep(1000);
						this.timerUsbSelect.Start();
						this.LastMessage = "save";
					}
				}
			}
			catch (Exception ex2)
			{
				this.buttonLoad.Enabled = true;
				this.label_Remind.ForeColor = System.Drawing.Color.Red;
				this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Failed") + "(" + ex2.Message + ")";
				base.Enabled = true;
				Thread.Sleep(1000);
				this.timerUsbSelect.Start();
				this.LastMessage = "save";
			}
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
					this.mf.SendSingleCmdStart(LedCmdType.Send_Panel_Parameter, this.thisPanel, this.buttonLoad.Text, this.thisPanel, false, this);
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
			this.TxtWifiName.Text = (this.NowWifiName = this.GetCurrentConnection());
			if (this.NowWifiName.StartsWith("ZH-") || this.NowWifiName.StartsWith("ZH_PP"))
			{
				this.LblControlCardState.Text = formMain.ML.GetStr("formPanelEditForForeignTrade_WIFIName_MatchCard");
				this.LblControlCardState.ForeColor = System.Drawing.Color.Green;
				this.IsConnectedToCorrectWifi = true;
				return;
			}
			this.LblControlCardState.Text = formMain.ML.GetStr("formPanelEditForForeignTrade_WIFIName_NotMatchCard");
			this.LblControlCardState.ForeColor = System.Drawing.Color.Red;
			this.IsConnectedToCorrectWifi = false;
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
				this.thisPanel.State = LedPanelState.Online;
				this.thisPanel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.Directly;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
				this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
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
				this.thisPanel.State = LedPanelState.Online;
				this.thisPanel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.FixedIP;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
				this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
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
				this.thisPanel.State = LedPanelState.Online;
				this.thisPanel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.CloudServer;
				formMain.ModifyPanelFromIPCServer(this.thisPanel);
				this.tvwPanel.SelectedNode.Text = this.GetPanelDescrible(this.thisPanel);
			}
		}

		private void Broadcast_display()
		{
			if (this.thisPanel != null)
			{
				this.CboSelectIPorDomain.SelectedIndex = 0;
				if (this.thisPanel.IsLSeries())
				{
					this.PnlCloudServer.Visible = false;
					this.PnlFixedIP.Visible = false;
					this.PnlLocalServer.Visible = false;
					this.PnlSingle.Visible = true;
					this.PnlSingle.BringToFront();
					return;
				}
				this.lblIPAddress.Visible = false;
				this.maskedTextBoxIPAddress.Visible = false;
				this.lblMacAddress.Visible = true;
				this.txtMacAddress.Visible = true;
				this.lblPassword.Visible = false;
				this.txtPassword.Visible = false;
				this.lblPort.Visible = false;
				this.txtIPPort.Visible = false;
				this.lblNetworkID.Visible = false;
				this.txtNetworkID.Visible = false;
				this.txtMacAddress.Location = this.TxtCloudServerDoname.Location;
				this.lblMacAddress.Location = this.CboSelectIPorDomain.Location;
			}
		}

		private void SendByIP_display()
		{
			if (this.thisPanel != null)
			{
				if (this.thisPanel.IsLSeries())
				{
					this.PnlCloudServer.Visible = false;
					this.PnlFixedIP.Visible = true;
					this.PnlLocalServer.Visible = false;
					this.PnlSingle.Visible = false;
					this.PnlFixedIP.BringToFront();
					return;
				}
				this.lblIPAddress.Visible = true;
				this.maskedTextBoxIPAddress.Visible = true;
				this.lblMacAddress.Visible = true;
				this.txtMacAddress.Visible = true;
				this.lblPassword.Visible = false;
				this.txtPassword.Visible = false;
				this.lblPort.Visible = false;
				this.txtIPPort.Visible = false;
				this.lblNetworkID.Visible = false;
				this.txtNetworkID.Visible = false;
				this.lblIPAddress.Location = new System.Drawing.Point(this.lblIPAddress.Location.X, 35);
				this.maskedTextBoxIPAddress.Location = new System.Drawing.Point(this.maskedTextBoxIPAddress.Location.X, 34);
				this.lblMacAddress.Location = new System.Drawing.Point(this.lblMacAddress.Location.X, 65);
				this.txtMacAddress.Location = new System.Drawing.Point(this.txtMacAddress.Location.X, 62);
			}
		}

		private void localServer_display()
		{
			if (this.thisPanel != null)
			{
				this.PnlCloudServer.Visible = false;
				this.PnlFixedIP.Visible = false;
				this.PnlLocalServer.Visible = true;
				this.PnlSingle.Visible = false;
				this.PnlLocalServer.BringToFront();
			}
		}

		public void CloudServer_display()
		{
			if (this.thisPanel != null && this.thisPanel.IsLSeries())
			{
				this.PnlCloudServer.Visible = true;
				this.PnlFixedIP.Visible = false;
				this.PnlLocalServer.Visible = false;
				this.PnlSingle.Visible = false;
				this.PnlCloudServer.BringToFront();
				if (this.thisPanel.CloudServerAccessMode == LedCloudServerAccessMode.Domain)
				{
					this.maskedTextBoxCloudIPAddress.Visible = false;
					this.TxtCloudServerDoname.Visible = true;
					this.CboSelectIPorDomain.Text = formMain.ML.GetStr("formpaneledit_RdoDomainName");
					return;
				}
				if (this.thisPanel.CloudServerAccessMode == LedCloudServerAccessMode.Ip)
				{
					this.maskedTextBoxCloudIPAddress.Visible = true;
					this.TxtCloudServerDoname.Visible = false;
					this.CboSelectIPorDomain.Text = formMain.ML.GetStr("formGPRS_Send_DataGridView_dataGridView_DeviceList_IP");
				}
			}
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
			this.LoadPanelParam();
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

		private void CboSelectIPorDomain_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (comboBox.SelectedIndex == 0)
			{
				this.TxtCloudServerDoname.Visible = false;
				this.maskedTextBoxCloudIPAddress.Visible = true;
				return;
			}
			if (comboBox.SelectedIndex == 1)
			{
				this.TxtCloudServerDoname.Visible = true;
				this.maskedTextBoxCloudIPAddress.Visible = false;
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
			this.tvwPanel = new TreeView();
			this.panel_Serial = new Panel();
			this.label_Baudrate = new Label();
			this.label_PortName = new Label();
			this.button_Add_Advance = new Button();
			this.comboBoxBaudrate = new ComboBox();
			this.comboBoxCom = new ComboBox();
			this.label8 = new Label();
			this.numericUpDownPanelAddress = new NumericUpDown();
			this.groupBox2 = new GroupBox();
			this.radioButtonGPRS = new RadioButton();
			this.radioButtonEnthernet = new RadioButton();
			this.radioButtonUSB = new RadioButton();
			this.radioButtonWifi = new RadioButton();
			this.radioButtonSerial = new RadioButton();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.numericUpDownHeigth = new NumericUpDown();
			this.numericUpDownWidth = new NumericUpDown();
			this.comboBoxColorMode = new ComboBox();
			this.zhGroupBox2 = new GroupBox();
			this.zhGroupBox3 = new GroupBox();
			this.groupBox1 = new GroupBox();
			this.buttonSave = new Button();
			this.button_Advanced = new Button();
			this.lblWidthModule = new Label();
			this.lblHeightModule = new Label();
			this.nudHeigthModule = new NumericUpDown();
			this.nudWidthModule = new NumericUpDown();
			this.panel_USB = new Panel();
			this.label_Remind = new Label();
			this.label_SelectUSB = new Label();
			this.UsbListComboBox = new ComboBox();
			this.toolTip_panel = new ToolTip(this.components);
			this.panel_WIFI = new Panel();
			this.LblControlCardState = new Label();
			this.LblWifiName = new Label();
			this.TxtWifiName = new TextBox();
			this.timerUsbSelect = new System.Windows.Forms.Timer(this.components);
			this.timerWifiConnectionStatus = new System.Windows.Forms.Timer(this.components);
			this.panel_Net = new Panel();
			this.PnlFixedIP = new Panel();
			this.lblIPAddress = new Label();
			this.maskedTextBoxIPAddress = new MaskedTextBox();
			this.txtIPPort = new TextBox();
			this.lblPort = new Label();
			this.radioButton_RemoteServer = new RadioButton();
			this.radioButton_localServer = new RadioButton();
			this.radioButton_SendByIP = new RadioButton();
			this.radioButton_SendBroadcast = new RadioButton();
			this.CboSelectIPorDomain = new ComboBox();
			this.TxtCloudServerDoname = new TextBox();
			this.maskedTextBoxCloudIPAddress = new MaskedTextBox();
			this.TxtCloudPort = new TextBox();
			this.LblCloudPort = new Label();
			this.txtMacAddress = new TextBox();
			this.lblMacAddress = new Label();
			this.txtPassword = new TextBox();
			this.lblPassword = new Label();
			this.txtNetworkID = new TextBox();
			this.lblNetworkID = new Label();
			this.panel_GPRS = new Panel();
			this.LblGPRSMessage = new Label();
			this.PnlCloudServer = new Panel();
			this.PnlSingle = new Panel();
			this.PnlLocalServer = new Panel();
			this.buttonLoad = new Button();
			this.panel_Serial.SuspendLayout();
			((ISupportInitialize)this.numericUpDownPanelAddress).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.numericUpDownHeigth).BeginInit();
			((ISupportInitialize)this.numericUpDownWidth).BeginInit();
			this.zhGroupBox2.SuspendLayout();
			this.zhGroupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.nudHeigthModule).BeginInit();
			((ISupportInitialize)this.nudWidthModule).BeginInit();
			this.panel_USB.SuspendLayout();
			this.panel_WIFI.SuspendLayout();
			this.panel_Net.SuspendLayout();
			this.PnlFixedIP.SuspendLayout();
			this.panel_GPRS.SuspendLayout();
			this.PnlCloudServer.SuspendLayout();
			this.PnlSingle.SuspendLayout();
			this.PnlLocalServer.SuspendLayout();
			base.SuspendLayout();
			this.tvwPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.tvwPanel.BorderStyle = BorderStyle.None;
			this.tvwPanel.HideSelection = false;
			this.tvwPanel.Location = new System.Drawing.Point(5, 13);
			this.tvwPanel.Name = "tvwPanel";
			this.tvwPanel.Size = new System.Drawing.Size(210, 559);
			this.tvwPanel.TabIndex = 0;
			this.tvwPanel.KeyDown += new KeyEventHandler(this.tvwPanel_KeyDown);
			this.tvwPanel.MouseDown += new MouseEventHandler(this.tvwPanel_MouseDown);
			this.panel_Serial.Controls.Add(this.label_Baudrate);
			this.panel_Serial.Controls.Add(this.label_PortName);
			this.panel_Serial.Controls.Add(this.button_Add_Advance);
			this.panel_Serial.Controls.Add(this.comboBoxBaudrate);
			this.panel_Serial.Controls.Add(this.comboBoxCom);
			this.panel_Serial.Controls.Add(this.label8);
			this.panel_Serial.Controls.Add(this.numericUpDownPanelAddress);
			this.panel_Serial.Location = new System.Drawing.Point(3, 336);
			this.panel_Serial.Name = "panel_Serial";
			this.panel_Serial.Size = new System.Drawing.Size(298, 183);
			this.panel_Serial.TabIndex = 52;
			this.label_Baudrate.Location = new System.Drawing.Point(65, 71);
			this.label_Baudrate.Name = "label_Baudrate";
			this.label_Baudrate.Size = new System.Drawing.Size(134, 17);
			this.label_Baudrate.TabIndex = 32;
			this.label_Baudrate.Text = "波特率";
			this.label_Baudrate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label_PortName.Location = new System.Drawing.Point(65, 12);
			this.label_PortName.Name = "label_PortName";
			this.label_PortName.Size = new System.Drawing.Size(134, 17);
			this.label_PortName.TabIndex = 31;
			this.label_PortName.Text = "串口";
			this.label_PortName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button_Add_Advance.Location = new System.Drawing.Point(202, 151);
			this.button_Add_Advance.Name = "button_Add_Advance";
			this.button_Add_Advance.Size = new System.Drawing.Size(46, 21);
			this.button_Add_Advance.TabIndex = 53;
			this.button_Add_Advance.Text = "设置";
			this.button_Add_Advance.UseVisualStyleBackColor = true;
			this.button_Add_Advance.Visible = false;
			this.button_Add_Advance.Click += new EventHandler(this.button_Add_Advance_Click);
			this.comboBoxBaudrate.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxBaudrate.FormattingEnabled = true;
			this.comboBoxBaudrate.Items.AddRange(new object[]
			{
				"9600",
				"38400",
				"115200"
			});
			this.comboBoxBaudrate.Location = new System.Drawing.Point(65, 91);
			this.comboBoxBaudrate.Name = "comboBoxBaudrate";
			this.comboBoxBaudrate.Size = new System.Drawing.Size(134, 20);
			this.comboBoxBaudrate.TabIndex = 30;
			this.comboBoxBaudrate.SelectedIndexChanged += new EventHandler(this.comboBoxBaudrate_SelectedIndexChanged_1);
			this.comboBoxCom.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxCom.FormattingEnabled = true;
			this.comboBoxCom.Location = new System.Drawing.Point(65, 32);
			this.comboBoxCom.Name = "comboBoxCom";
			this.comboBoxCom.Size = new System.Drawing.Size(134, 20);
			this.comboBoxCom.TabIndex = 29;
			this.comboBoxCom.SelectedIndexChanged += new EventHandler(this.comboBoxBaudrate_SelectedIndexChanged);
			this.label8.Location = new System.Drawing.Point(65, 131);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(134, 17);
			this.label8.TabIndex = 29;
			this.label8.Text = "显示屏地址";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label8.Visible = false;
			this.numericUpDownPanelAddress.Location = new System.Drawing.Point(65, 151);
			NumericUpDown arg_870_0 = this.numericUpDownPanelAddress;
			int[] array = new int[4];
			array[0] = 255;
			arg_870_0.Maximum = new decimal(array);
			this.numericUpDownPanelAddress.Name = "numericUpDownPanelAddress";
			this.numericUpDownPanelAddress.Size = new System.Drawing.Size(134, 21);
			this.numericUpDownPanelAddress.TabIndex = 41;
			this.numericUpDownPanelAddress.Visible = false;
			this.numericUpDownPanelAddress.ValueChanged += new EventHandler(this.numericUpDownPanelAddress_ValueChanged);
			this.groupBox2.Controls.Add(this.radioButtonGPRS);
			this.groupBox2.Controls.Add(this.radioButtonEnthernet);
			this.groupBox2.Controls.Add(this.radioButtonUSB);
			this.groupBox2.Controls.Add(this.radioButtonWifi);
			this.groupBox2.Controls.Add(this.radioButtonSerial);
			this.groupBox2.Location = new System.Drawing.Point(5, 253);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(296, 72);
			this.groupBox2.TabIndex = 27;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "通讯方式";
			this.radioButtonGPRS.AutoSize = true;
			this.radioButtonGPRS.Location = new System.Drawing.Point(105, 50);
			this.radioButtonGPRS.Name = "radioButtonGPRS";
			this.radioButtonGPRS.Size = new System.Drawing.Size(53, 16);
			this.radioButtonGPRS.TabIndex = 31;
			this.radioButtonGPRS.TabStop = true;
			this.radioButtonGPRS.Text = "GPRS ";
			this.radioButtonGPRS.UseVisualStyleBackColor = true;
			this.radioButtonGPRS.CheckedChanged += new EventHandler(this.radioButtonGPRS_CheckedChanged);
			this.radioButtonEnthernet.AutoSize = true;
			this.radioButtonEnthernet.Location = new System.Drawing.Point(193, 20);
			this.radioButtonEnthernet.Name = "radioButtonEnthernet";
			this.radioButtonEnthernet.Size = new System.Drawing.Size(59, 16);
			this.radioButtonEnthernet.TabIndex = 30;
			this.radioButtonEnthernet.TabStop = true;
			this.radioButtonEnthernet.Text = "以太网";
			this.radioButtonEnthernet.UseVisualStyleBackColor = true;
			this.radioButtonEnthernet.CheckedChanged += new EventHandler(this.radioButtonEnthernet_CheckedChanged);
			this.radioButtonUSB.AutoSize = true;
			this.radioButtonUSB.Checked = true;
			this.radioButtonUSB.Location = new System.Drawing.Point(105, 20);
			this.radioButtonUSB.Name = "radioButtonUSB";
			this.radioButtonUSB.Size = new System.Drawing.Size(41, 16);
			this.radioButtonUSB.TabIndex = 0;
			this.radioButtonUSB.TabStop = true;
			this.radioButtonUSB.Text = "U盘";
			this.radioButtonUSB.UseVisualStyleBackColor = true;
			this.radioButtonUSB.CheckedChanged += new EventHandler(this.radioButtonUSB_CheckedChanged);
			this.radioButtonWifi.AutoSize = true;
			this.radioButtonWifi.Location = new System.Drawing.Point(10, 50);
			this.radioButtonWifi.Name = "radioButtonWifi";
			this.radioButtonWifi.Size = new System.Drawing.Size(47, 16);
			this.radioButtonWifi.TabIndex = 29;
			this.radioButtonWifi.TabStop = true;
			this.radioButtonWifi.Text = "WiFi";
			this.radioButtonWifi.UseVisualStyleBackColor = true;
			this.radioButtonWifi.CheckedChanged += new EventHandler(this.radioButtonWifi_CheckedChanged);
			this.radioButtonSerial.AutoSize = true;
			this.radioButtonSerial.Location = new System.Drawing.Point(10, 20);
			this.radioButtonSerial.Name = "radioButtonSerial";
			this.radioButtonSerial.Size = new System.Drawing.Size(47, 16);
			this.radioButtonSerial.TabIndex = 0;
			this.radioButtonSerial.TabStop = true;
			this.radioButtonSerial.Text = "串口";
			this.radioButtonSerial.UseVisualStyleBackColor = true;
			this.radioButtonSerial.CheckedChanged += new EventHandler(this.radioButtonSerial_CheckedChanged);
			this.label4.Location = new System.Drawing.Point(68, 139);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(143, 17);
			this.label4.TabIndex = 18;
			this.label4.Text = "色彩模式";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label3.Location = new System.Drawing.Point(68, 77);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(141, 17);
			this.label3.TabIndex = 17;
			this.label3.Text = "高度";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Location = new System.Drawing.Point(68, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(141, 17);
			this.label2.TabIndex = 16;
			this.label2.Text = "宽度";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.numericUpDownHeigth.Location = new System.Drawing.Point(68, 97);
			NumericUpDown arg_DDE_0 = this.numericUpDownHeigth;
			int[] array2 = new int[4];
			array2[0] = 10000;
			arg_DDE_0.Maximum = new decimal(array2);
			NumericUpDown arg_DFA_0 = this.numericUpDownHeigth;
			int[] array3 = new int[4];
			array3[0] = 8;
			arg_DFA_0.Minimum = new decimal(array3);
			this.numericUpDownHeigth.Name = "numericUpDownHeigth";
			this.numericUpDownHeigth.Size = new System.Drawing.Size(136, 21);
			this.numericUpDownHeigth.TabIndex = 15;
			NumericUpDown arg_E4E_0 = this.numericUpDownHeigth;
			int[] array4 = new int[4];
			array4[0] = 16;
			arg_E4E_0.Value = new decimal(array4);
			this.numericUpDownHeigth.ValueChanged += new EventHandler(this.numericUpDownHeigth_ValueChanged);
			this.numericUpDownWidth.Location = new System.Drawing.Point(68, 40);
			NumericUpDown arg_E9C_0 = this.numericUpDownWidth;
			int[] array5 = new int[4];
			array5[0] = 20000;
			arg_E9C_0.Maximum = new decimal(array5);
			NumericUpDown arg_EBB_0 = this.numericUpDownWidth;
			int[] array6 = new int[4];
			array6[0] = 8;
			arg_EBB_0.Minimum = new decimal(array6);
			this.numericUpDownWidth.Name = "numericUpDownWidth";
			this.numericUpDownWidth.Size = new System.Drawing.Size(136, 21);
			this.numericUpDownWidth.TabIndex = 14;
			NumericUpDown arg_F0F_0 = this.numericUpDownWidth;
			int[] array7 = new int[4];
			array7[0] = 32;
			arg_F0F_0.Value = new decimal(array7);
			this.numericUpDownWidth.ValueChanged += new EventHandler(this.numericUpDownWidth_ValueChanged);
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
			this.comboBoxColorMode.Location = new System.Drawing.Point(68, 159);
			this.comboBoxColorMode.Name = "comboBoxColorMode";
			this.comboBoxColorMode.Size = new System.Drawing.Size(134, 22);
			this.comboBoxColorMode.TabIndex = 8;
			this.comboBoxColorMode.DrawItem += new DrawItemEventHandler(this.comboBoxType_DrawItem);
			this.comboBoxColorMode.SelectedIndexChanged += new EventHandler(this.comboBoxColorMode_SelectedIndexChanged);
			this.zhGroupBox2.Controls.Add(this.tvwPanel);
			this.zhGroupBox2.Location = new System.Drawing.Point(7, 2);
			this.zhGroupBox2.Name = "zhGroupBox2";
			this.zhGroupBox2.Size = new System.Drawing.Size(221, 578);
			this.zhGroupBox2.TabIndex = 48;
			this.zhGroupBox2.TabStop = false;
			this.zhGroupBox3.Controls.Add(this.groupBox1);
			this.zhGroupBox3.Controls.Add(this.button_Advanced);
			this.zhGroupBox3.Controls.Add(this.lblWidthModule);
			this.zhGroupBox3.Controls.Add(this.lblHeightModule);
			this.zhGroupBox3.Controls.Add(this.nudHeigthModule);
			this.zhGroupBox3.Controls.Add(this.nudWidthModule);
			this.zhGroupBox3.Controls.Add(this.panel_Serial);
			this.zhGroupBox3.Controls.Add(this.groupBox2);
			this.zhGroupBox3.Controls.Add(this.label4);
			this.zhGroupBox3.Controls.Add(this.label3);
			this.zhGroupBox3.Controls.Add(this.label2);
			this.zhGroupBox3.Controls.Add(this.numericUpDownHeigth);
			this.zhGroupBox3.Controls.Add(this.numericUpDownWidth);
			this.zhGroupBox3.Controls.Add(this.comboBoxColorMode);
			this.zhGroupBox3.Location = new System.Drawing.Point(234, 2);
			this.zhGroupBox3.Name = "zhGroupBox3";
			this.zhGroupBox3.Size = new System.Drawing.Size(307, 578);
			this.zhGroupBox3.TabIndex = 49;
			this.zhGroupBox3.TabStop = false;
			this.groupBox1.Controls.Add(this.buttonSave);
			this.groupBox1.Location = new System.Drawing.Point(5, 526);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(296, 40);
			this.groupBox1.TabIndex = 52;
			this.groupBox1.TabStop = false;
			this.buttonSave.Location = new System.Drawing.Point(174, 11);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(60, 23);
			this.buttonSave.TabIndex = 59;
			this.buttonSave.Text = "关闭";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new EventHandler(this.buttonSave_Click);
			this.button_Advanced.Location = new System.Drawing.Point(68, 210);
			this.button_Advanced.Name = "button_Advanced";
			this.button_Advanced.Size = new System.Drawing.Size(134, 23);
			this.button_Advanced.TabIndex = 60;
			this.button_Advanced.Text = "高级设置";
			this.button_Advanced.UseVisualStyleBackColor = true;
			this.button_Advanced.Click += new EventHandler(this.button_Advanced_Click);
			this.lblWidthModule.Location = new System.Drawing.Point(212, 44);
			this.lblWidthModule.Name = "lblWidthModule";
			this.lblWidthModule.Size = new System.Drawing.Size(44, 17);
			this.lblWidthModule.TabIndex = 57;
			this.lblWidthModule.Text = "(模组)";
			this.lblWidthModule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblHeightModule.Location = new System.Drawing.Point(212, 98);
			this.lblHeightModule.Name = "lblHeightModule";
			this.lblHeightModule.Size = new System.Drawing.Size(44, 17);
			this.lblHeightModule.TabIndex = 56;
			this.lblHeightModule.Text = "(模组)";
			this.lblHeightModule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			NumericUpDown arg_1480_0 = this.nudHeigthModule;
			int[] array8 = new int[4];
			array8[0] = 8;
			arg_1480_0.Increment = new decimal(array8);
			this.nudHeigthModule.Location = new System.Drawing.Point(132, 97);
			NumericUpDown arg_14BA_0 = this.nudHeigthModule;
			int[] array9 = new int[4];
			array9[0] = 10000;
			arg_14BA_0.Maximum = new decimal(array9);
			NumericUpDown arg_14D9_0 = this.nudHeigthModule;
			int[] array10 = new int[4];
			array10[0] = 8;
			arg_14D9_0.Minimum = new decimal(array10);
			this.nudHeigthModule.Name = "nudHeigthModule";
			this.nudHeigthModule.Size = new System.Drawing.Size(72, 21);
			this.nudHeigthModule.TabIndex = 55;
			NumericUpDown arg_152A_0 = this.nudHeigthModule;
			int[] array11 = new int[4];
			array11[0] = 16;
			arg_152A_0.Value = new decimal(array11);
			this.nudHeigthModule.ValueChanged += new EventHandler(this.nudHeigthModule_ValueChanged);
			this.nudHeigthModule.KeyDown += new KeyEventHandler(this.nudHeigthModule_KeyDown);
			NumericUpDown arg_1578_0 = this.nudWidthModule;
			int[] array12 = new int[4];
			array12[0] = 16;
			arg_1578_0.Increment = new decimal(array12);
			this.nudWidthModule.Location = new System.Drawing.Point(132, 40);
			NumericUpDown arg_15B2_0 = this.nudWidthModule;
			int[] array13 = new int[4];
			array13[0] = 20000;
			arg_15B2_0.Maximum = new decimal(array13);
			NumericUpDown arg_15D2_0 = this.nudWidthModule;
			int[] array14 = new int[4];
			array14[0] = 16;
			arg_15D2_0.Minimum = new decimal(array14);
			this.nudWidthModule.Name = "nudWidthModule";
			this.nudWidthModule.Size = new System.Drawing.Size(72, 21);
			this.nudWidthModule.TabIndex = 54;
			NumericUpDown arg_1623_0 = this.nudWidthModule;
			int[] array15 = new int[4];
			array15[0] = 32;
			arg_1623_0.Value = new decimal(array15);
			this.nudWidthModule.ValueChanged += new EventHandler(this.nudWidthModule_ValueChanged);
			this.nudWidthModule.KeyDown += new KeyEventHandler(this.nudWidthModule_KeyDown);
			this.panel_USB.Controls.Add(this.label_Remind);
			this.panel_USB.Controls.Add(this.label_SelectUSB);
			this.panel_USB.Controls.Add(this.UsbListComboBox);
			this.panel_USB.Location = new System.Drawing.Point(877, 31);
			this.panel_USB.Name = "panel_USB";
			this.panel_USB.Size = new System.Drawing.Size(248, 86);
			this.panel_USB.TabIndex = 54;
			this.label_Remind.Location = new System.Drawing.Point(32, 55);
			this.label_Remind.Name = "label_Remind";
			this.label_Remind.Size = new System.Drawing.Size(204, 101);
			this.label_Remind.TabIndex = 56;
			this.label_Remind.Visible = false;
			this.label_SelectUSB.Location = new System.Drawing.Point(32, 9);
			this.label_SelectUSB.Name = "label_SelectUSB";
			this.label_SelectUSB.Size = new System.Drawing.Size(169, 17);
			this.label_SelectUSB.TabIndex = 54;
			this.label_SelectUSB.Text = "选择U盘";
			this.label_SelectUSB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label_SelectUSB.Visible = false;
			this.UsbListComboBox.Cursor = Cursors.Default;
			this.UsbListComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.UsbListComboBox.FormattingEnabled = true;
			this.UsbListComboBox.ImeMode = ImeMode.On;
			this.UsbListComboBox.Location = new System.Drawing.Point(32, 29);
			this.UsbListComboBox.Name = "UsbListComboBox";
			this.UsbListComboBox.Size = new System.Drawing.Size(179, 20);
			this.UsbListComboBox.TabIndex = 5;
			this.UsbListComboBox.Visible = false;
			this.panel_WIFI.Controls.Add(this.LblControlCardState);
			this.panel_WIFI.Controls.Add(this.LblWifiName);
			this.panel_WIFI.Controls.Add(this.TxtWifiName);
			this.panel_WIFI.Location = new System.Drawing.Point(877, 429);
			this.panel_WIFI.Name = "panel_WIFI";
			this.panel_WIFI.Size = new System.Drawing.Size(248, 98);
			this.panel_WIFI.TabIndex = 55;
			this.LblControlCardState.Location = new System.Drawing.Point(32, 55);
			this.LblControlCardState.Name = "LblControlCardState";
			this.LblControlCardState.Size = new System.Drawing.Size(204, 27);
			this.LblControlCardState.TabIndex = 57;
			this.LblControlCardState.Visible = false;
			this.LblWifiName.Location = new System.Drawing.Point(32, 9);
			this.LblWifiName.Name = "LblWifiName";
			this.LblWifiName.Size = new System.Drawing.Size(134, 17);
			this.LblWifiName.TabIndex = 32;
			this.LblWifiName.Text = "Wi-Fi名称";
			this.LblWifiName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LblWifiName.Visible = false;
			this.TxtWifiName.Location = new System.Drawing.Point(32, 28);
			this.TxtWifiName.Name = "TxtWifiName";
			this.TxtWifiName.Size = new System.Drawing.Size(179, 21);
			this.TxtWifiName.TabIndex = 1;
			this.TxtWifiName.Visible = false;
			this.timerUsbSelect.Interval = 1000;
			this.timerUsbSelect.Tick += new EventHandler(this.timerUsbSelect_Tick);
			this.timerWifiConnectionStatus.Interval = 1000;
			this.timerWifiConnectionStatus.Tick += new EventHandler(this.timerWifiConnectionStatus_Tick);
			this.panel_Net.Controls.Add(this.PnlFixedIP);
			this.panel_Net.Controls.Add(this.radioButton_RemoteServer);
			this.panel_Net.Controls.Add(this.radioButton_localServer);
			this.panel_Net.Controls.Add(this.radioButton_SendByIP);
			this.panel_Net.Controls.Add(this.radioButton_SendBroadcast);
			this.panel_Net.Location = new System.Drawing.Point(570, 144);
			this.panel_Net.Name = "panel_Net";
			this.panel_Net.Size = new System.Drawing.Size(298, 183);
			this.panel_Net.TabIndex = 56;
			this.PnlFixedIP.Controls.Add(this.lblIPAddress);
			this.PnlFixedIP.Controls.Add(this.maskedTextBoxIPAddress);
			this.PnlFixedIP.Controls.Add(this.txtIPPort);
			this.PnlFixedIP.Controls.Add(this.lblPort);
			this.PnlFixedIP.Location = new System.Drawing.Point(7, 80);
			this.PnlFixedIP.Name = "PnlFixedIP";
			this.PnlFixedIP.Size = new System.Drawing.Size(271, 85);
			this.PnlFixedIP.TabIndex = 61;
			this.lblIPAddress.Location = new System.Drawing.Point(12, 14);
			this.lblIPAddress.Name = "lblIPAddress";
			this.lblIPAddress.Size = new System.Drawing.Size(70, 17);
			this.lblIPAddress.TabIndex = 31;
			this.lblIPAddress.Text = "IP地址";
			this.lblIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.maskedTextBoxIPAddress.Location = new System.Drawing.Point(96, 13);
			this.maskedTextBoxIPAddress.Mask = "###.###.###.###";
			this.maskedTextBoxIPAddress.Name = "maskedTextBoxIPAddress";
			this.maskedTextBoxIPAddress.PromptChar = ' ';
			this.maskedTextBoxIPAddress.Size = new System.Drawing.Size(130, 21);
			this.maskedTextBoxIPAddress.TabIndex = 63;
			this.txtIPPort.Location = new System.Drawing.Point(96, 55);
			this.txtIPPort.Name = "txtIPPort";
			this.txtIPPort.Size = new System.Drawing.Size(130, 21);
			this.txtIPPort.TabIndex = 66;
			this.lblPort.Location = new System.Drawing.Point(12, 58);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(70, 17);
			this.lblPort.TabIndex = 65;
			this.lblPort.Text = "端口";
			this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioButton_RemoteServer.AutoSize = true;
			this.radioButton_RemoteServer.Location = new System.Drawing.Point(195, 43);
			this.radioButton_RemoteServer.Name = "radioButton_RemoteServer";
			this.radioButton_RemoteServer.Size = new System.Drawing.Size(83, 16);
			this.radioButton_RemoteServer.TabIndex = 62;
			this.radioButton_RemoteServer.TabStop = true;
			this.radioButton_RemoteServer.Text = "远程服务器";
			this.radioButton_RemoteServer.UseVisualStyleBackColor = true;
			this.radioButton_RemoteServer.CheckedChanged += new EventHandler(this.radioButton_RemoteServer_CheckedChanged);
			this.radioButton_localServer.AutoSize = true;
			this.radioButton_localServer.Location = new System.Drawing.Point(12, 43);
			this.radioButton_localServer.Name = "radioButton_localServer";
			this.radioButton_localServer.Size = new System.Drawing.Size(83, 16);
			this.radioButton_localServer.TabIndex = 61;
			this.radioButton_localServer.TabStop = true;
			this.radioButton_localServer.Text = "本地服务器";
			this.radioButton_localServer.UseVisualStyleBackColor = true;
			this.radioButton_localServer.CheckedChanged += new EventHandler(this.radioButton_localServer_CheckedChanged);
			this.radioButton_SendByIP.AutoSize = true;
			this.radioButton_SendByIP.Location = new System.Drawing.Point(195, 9);
			this.radioButton_SendByIP.Name = "radioButton_SendByIP";
			this.radioButton_SendByIP.Size = new System.Drawing.Size(59, 16);
			this.radioButton_SendByIP.TabIndex = 58;
			this.radioButton_SendByIP.TabStop = true;
			this.radioButton_SendByIP.Text = "指定IP";
			this.radioButton_SendByIP.UseVisualStyleBackColor = true;
			this.radioButton_SendByIP.CheckedChanged += new EventHandler(this.radioButton_SendByIP_CheckedChanged);
			this.radioButton_SendBroadcast.AutoSize = true;
			this.radioButton_SendBroadcast.Location = new System.Drawing.Point(12, 9);
			this.radioButton_SendBroadcast.Name = "radioButton_SendBroadcast";
			this.radioButton_SendBroadcast.Size = new System.Drawing.Size(71, 16);
			this.radioButton_SendBroadcast.TabIndex = 57;
			this.radioButton_SendBroadcast.TabStop = true;
			this.radioButton_SendBroadcast.Text = "单机直连";
			this.radioButton_SendBroadcast.UseVisualStyleBackColor = true;
			this.radioButton_SendBroadcast.CheckedChanged += new EventHandler(this.radioButton_SendBroadcast_CheckedChanged);
			this.CboSelectIPorDomain.FormattingEnabled = true;
			this.CboSelectIPorDomain.Items.AddRange(new object[]
			{
				"IP",
				"域名"
			});
			this.CboSelectIPorDomain.Location = new System.Drawing.Point(12, 14);
			this.CboSelectIPorDomain.Name = "CboSelectIPorDomain";
			this.CboSelectIPorDomain.Size = new System.Drawing.Size(73, 20);
			this.CboSelectIPorDomain.TabIndex = 56;
			this.CboSelectIPorDomain.SelectedIndexChanged += new EventHandler(this.CboSelectIPorDomain_SelectedIndexChanged);
			this.TxtCloudServerDoname.Enabled = false;
			this.TxtCloudServerDoname.Location = new System.Drawing.Point(96, 13);
			this.TxtCloudServerDoname.Name = "TxtCloudServerDoname";
			this.TxtCloudServerDoname.Size = new System.Drawing.Size(130, 21);
			this.TxtCloudServerDoname.TabIndex = 76;
			this.TxtCloudServerDoname.Visible = false;
			this.maskedTextBoxCloudIPAddress.Enabled = false;
			this.maskedTextBoxCloudIPAddress.Location = new System.Drawing.Point(96, 13);
			this.maskedTextBoxCloudIPAddress.Mask = "###.###.###.###";
			this.maskedTextBoxCloudIPAddress.Name = "maskedTextBoxCloudIPAddress";
			this.maskedTextBoxCloudIPAddress.PromptChar = ' ';
			this.maskedTextBoxCloudIPAddress.Size = new System.Drawing.Size(130, 21);
			this.maskedTextBoxCloudIPAddress.TabIndex = 73;
			this.TxtCloudPort.Enabled = false;
			this.TxtCloudPort.Location = new System.Drawing.Point(96, 55);
			this.TxtCloudPort.Name = "TxtCloudPort";
			this.TxtCloudPort.Size = new System.Drawing.Size(130, 21);
			this.TxtCloudPort.TabIndex = 71;
			this.LblCloudPort.Location = new System.Drawing.Point(12, 58);
			this.LblCloudPort.Name = "LblCloudPort";
			this.LblCloudPort.Size = new System.Drawing.Size(70, 17);
			this.LblCloudPort.TabIndex = 72;
			this.LblCloudPort.Text = "端口";
			this.LblCloudPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtMacAddress.Enabled = false;
			this.txtMacAddress.Location = new System.Drawing.Point(96, 13);
			this.txtMacAddress.Name = "txtMacAddress";
			this.txtMacAddress.Size = new System.Drawing.Size(130, 21);
			this.txtMacAddress.TabIndex = 63;
			this.lblMacAddress.Location = new System.Drawing.Point(12, 14);
			this.lblMacAddress.Name = "lblMacAddress";
			this.lblMacAddress.Size = new System.Drawing.Size(70, 17);
			this.lblMacAddress.TabIndex = 68;
			this.lblMacAddress.Text = "MAC地址";
			this.lblMacAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtPassword.Location = new System.Drawing.Point(96, 55);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(130, 21);
			this.txtPassword.TabIndex = 63;
			this.txtPassword.Visible = false;
			this.lblPassword.Location = new System.Drawing.Point(12, 58);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(70, 17);
			this.lblPassword.TabIndex = 36;
			this.lblPassword.Text = "密码";
			this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblPassword.Visible = false;
			this.txtNetworkID.Location = new System.Drawing.Point(96, 13);
			this.txtNetworkID.Name = "txtNetworkID";
			this.txtNetworkID.Size = new System.Drawing.Size(130, 21);
			this.txtNetworkID.TabIndex = 64;
			this.lblNetworkID.Location = new System.Drawing.Point(12, 14);
			this.lblNetworkID.Name = "lblNetworkID";
			this.lblNetworkID.Size = new System.Drawing.Size(70, 17);
			this.lblNetworkID.TabIndex = 51;
			this.lblNetworkID.Text = "网络ID";
			this.lblNetworkID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.panel_GPRS.Controls.Add(this.LblGPRSMessage);
			this.panel_GPRS.Location = new System.Drawing.Point(899, 316);
			this.panel_GPRS.Name = "panel_GPRS";
			this.panel_GPRS.Size = new System.Drawing.Size(248, 102);
			this.panel_GPRS.TabIndex = 57;
			this.LblGPRSMessage.Location = new System.Drawing.Point(5, 9);
			this.LblGPRSMessage.Name = "LblGPRSMessage";
			this.LblGPRSMessage.Size = new System.Drawing.Size(238, 70);
			this.LblGPRSMessage.TabIndex = 55;
			this.LblGPRSMessage.Text = "如需加载屏参请到GPRS发送界面";
			this.PnlCloudServer.Controls.Add(this.TxtCloudServerDoname);
			this.PnlCloudServer.Controls.Add(this.CboSelectIPorDomain);
			this.PnlCloudServer.Controls.Add(this.TxtCloudPort);
			this.PnlCloudServer.Controls.Add(this.LblCloudPort);
			this.PnlCloudServer.Controls.Add(this.maskedTextBoxCloudIPAddress);
			this.PnlCloudServer.Location = new System.Drawing.Point(577, 333);
			this.PnlCloudServer.Name = "PnlCloudServer";
			this.PnlCloudServer.Size = new System.Drawing.Size(271, 85);
			this.PnlCloudServer.TabIndex = 58;
			this.PnlSingle.Controls.Add(this.txtMacAddress);
			this.PnlSingle.Controls.Add(this.lblMacAddress);
			this.PnlSingle.Location = new System.Drawing.Point(577, 428);
			this.PnlSingle.Name = "PnlSingle";
			this.PnlSingle.Size = new System.Drawing.Size(271, 59);
			this.PnlSingle.TabIndex = 59;
			this.PnlLocalServer.Controls.Add(this.txtPassword);
			this.PnlLocalServer.Controls.Add(this.lblNetworkID);
			this.PnlLocalServer.Controls.Add(this.lblPassword);
			this.PnlLocalServer.Controls.Add(this.txtNetworkID);
			this.PnlLocalServer.Location = new System.Drawing.Point(577, 493);
			this.PnlLocalServer.Name = "PnlLocalServer";
			this.PnlLocalServer.Size = new System.Drawing.Size(271, 83);
			this.PnlLocalServer.TabIndex = 60;
			this.buttonLoad.Location = new System.Drawing.Point(899, 279);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(60, 23);
			this.buttonLoad.TabIndex = 58;
			this.buttonLoad.Text = "加载";
			this.buttonLoad.UseVisualStyleBackColor = true;
			this.buttonLoad.Visible = false;
			this.buttonLoad.Click += new EventHandler(this.buttonLoad_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(555, 589);
			base.Controls.Add(this.PnlLocalServer);
			base.Controls.Add(this.buttonLoad);
			base.Controls.Add(this.PnlSingle);
			base.Controls.Add(this.PnlCloudServer);
			base.Controls.Add(this.panel_GPRS);
			base.Controls.Add(this.panel_Net);
			base.Controls.Add(this.panel_WIFI);
			base.Controls.Add(this.zhGroupBox3);
			base.Controls.Add(this.panel_USB);
			base.Controls.Add(this.zhGroupBox2);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formPanelEditForForeignTrade";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "显示屏设置";
			base.FormClosing += new FormClosingEventHandler(this.formPanelEditForForeignTrade_Load_FormClosing);
			base.Load += new EventHandler(this.formPanelEditForForeignTrade_Load);
			this.panel_Serial.ResumeLayout(false);
			((ISupportInitialize)this.numericUpDownPanelAddress).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.numericUpDownHeigth).EndInit();
			((ISupportInitialize)this.numericUpDownWidth).EndInit();
			this.zhGroupBox2.ResumeLayout(false);
			this.zhGroupBox3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.nudHeigthModule).EndInit();
			((ISupportInitialize)this.nudWidthModule).EndInit();
			this.panel_USB.ResumeLayout(false);
			this.panel_WIFI.ResumeLayout(false);
			this.panel_WIFI.PerformLayout();
			this.panel_Net.ResumeLayout(false);
			this.panel_Net.PerformLayout();
			this.PnlFixedIP.ResumeLayout(false);
			this.PnlFixedIP.PerformLayout();
			this.panel_GPRS.ResumeLayout(false);
			this.PnlCloudServer.ResumeLayout(false);
			this.PnlCloudServer.PerformLayout();
			this.PnlSingle.ResumeLayout(false);
			this.PnlSingle.PerformLayout();
			this.PnlLocalServer.ResumeLayout(false);
			this.PnlLocalServer.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
