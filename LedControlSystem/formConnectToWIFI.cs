using HelloRemoting;
using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Enum;
using NativeWifi;
using server_interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace LedControlSystem.LedControlSystem
{
	public class formConnectToWIFI : Form
	{
		public delegate void SetButtonEnable(bool able_set);

		private delegate void ProxyClient();

		private string[] signalDescription = new string[6];

		private formMain fm;

		private bool isFindDis;

		private bool isWiFiProductionTest = LedGlobal.IsWiFiProductionTest;

		private static string formID = "formConnectToWIFI";

		private XmlDocument wifiXML = new XmlDocument();

		private XmlNode wifiInfoModel;

		private bool isSingleModel;

		private bool ConnectResult;

		private bool isWiFiConnect;

		private bool isLoad;

		private LedPortType pType;

		private byte protocolVersion;

		private int ResendCount;

		private DateTime dtStart;

		public bool IsOnlyConntectToWifi;

		private string targetSSID = "";

		private string currentSSID = "";

		private List<Wlan.WlanAvailableNetwork> zhWIFIList = new List<Wlan.WlanAvailableNetwork>();

		private List<string> zhWIFIListAdded = new List<string>();

		private WlanClient.WlanInterface wlanIfacecc;

		private bool passwordChanged;

		private static string SSID_state_now = "";

		private int timer_new_count;

		public static WlanClient client = null;

		private bool isShowAdvancedOption;

		private List<Thread> threadList = new List<Thread>();

		private formConnectToWIFI.SetButtonEnable SetAble;

		private System.Threading.Timer thread_time_finddevice;

		private string profileName_Default;

		private bool notprofile_defaultPassword;

		private bool IsFindOrSend;

		private bool IsConnectOneTime;

		private Wlan.WlanAvailableNetwork state_link_currentWifi;

		private int connect_time;

		private bool defaultPassword_successed;

		private Thread refreash;

		private IContainer components;

		private Label label1;

		private Button button_Cancel;

		private TextBox textBox1;

		private Label label2;

		private Button button_Refresh;

		private DataGridView dataGridView_DeviceList;

		private Button button_Advance;

		private Button button_Send;

		private CheckBox checkBox1;

		private CheckBox checkBox_Showpassword;

		private Button button_changePassword;

		private Button button_changeSSID;

		private PictureBox pictureBox1;

		private Label label3;

		private System.Windows.Forms.Timer timer2;

		private DataGridViewTextBoxColumn Col_SSID;

		private DataGridViewTextBoxColumn Col_Signal;

		private DataGridViewTextBoxColumn Col_Status;

		private System.Windows.Forms.Timer timer3;

		private Button button_updataCode;

		private Button buttonConnect;

		private Button button_changeChannel;

		private System.Windows.Forms.Timer timer_new;

		private System.Windows.Forms.Timer timer1;

		private Label label4;

		public static string FormID
		{
			get
			{
				return formConnectToWIFI.formID;
			}
			set
			{
				formConnectToWIFI.formID = value;
			}
		}

		public void Language_Text()
		{
			this.signalDescription[0] = formMain.ML.GetStr("WIFI_SignalDescription_verystrong");
			this.signalDescription[1] = formMain.ML.GetStr("WIFI_SignalDescription_strong");
			this.signalDescription[2] = formMain.ML.GetStr("WIFI_SignalDescription_medium");
			this.signalDescription[3] = formMain.ML.GetStr("WIFI_SignalDescription_weak");
			this.signalDescription[4] = formMain.ML.GetStr("WIFI_SignalDescription_veryweak");
			this.signalDescription[5] = formMain.ML.GetStr("WIFI_SignalDescription_nosignal");
			this.Text = formMain.ML.GetStr("formConnectToWIFI_FormText");
			this.label1.Text = formMain.ML.GetStr("formConnectToWIFI_label_Select_Send_Device");
			this.button_Advance.Text = formMain.ML.GetStr("formConnectToWIFI_button_Advance");
			this.button_Refresh.Text = formMain.ML.GetStr("formConnectToWIFI_button_Refresh");
			this.buttonConnect.Text = formMain.ML.GetStr("formConnectToWIFI_button_Connect");
			this.button_Cancel.Text = formMain.ML.GetStr("formConnectToWIFI_button_Cancel");
			this.button_Send.Text = formMain.ML.GetStr("formConnectToWIFI_button_Send");
			this.label2.Text = formMain.ML.GetStr("formConnectToWIFI_label_Password");
			this.checkBox_Showpassword.Text = formMain.ML.GetStr("formConnectToWIFI_checkBox_Showpassword");
			this.button_changePassword.Text = formMain.ML.GetStr("formConnectToWIFI_button_changePassword");
			this.button_changeSSID.Text = formMain.ML.GetStr("formConnectToWIFI_button_changeSSID");
			this.button_changeChannel.Text = formMain.ML.GetStr("formConnectToWIFI_button_changeChannel");
			this.button_updataCode.Text = formMain.ML.GetStr("formConnectToWIFI_button_updataCode");
			this.checkBox1.Text = formMain.ML.GetStr("formConnectToWIFI_checkBox_DisplayAllNet");
			this.label3.Text = formMain.ML.GetStr("formConnectToWIFI_label_systemhint");
			this.label4.Text = formMain.ML.GetStr("formConnectToWIFI_Prompt_PasswordNotSpaces");
			this.Text = formMain.ML.GetStr("WIFI_Connect");
			this.dataGridView_DeviceList.Columns[0].HeaderText = formMain.ML.GetStr("WIFI_SSID");
			this.dataGridView_DeviceList.Columns[1].HeaderText = formMain.ML.GetStr("WIFI_Signal");
			this.dataGridView_DeviceList.Columns[2].HeaderText = formMain.ML.GetStr("WIFI_ConnectStatus");
		}

		public formConnectToWIFI(formMain pManform)
		{
			this.InitializeComponent();
			base.Height = this.button_Advance.Top + this.button_Advance.Height + (this.button_Advance.Top - (this.dataGridView_DeviceList.Top + this.dataGridView_DeviceList.Height)) + 30;
			this.fm = pManform;
			this.isFindDis = false;
			formMain.ML.NowFormID = formConnectToWIFI.formID;
			this.Language_Text();
		}

		public formConnectToWIFI()
		{
			this.InitializeComponent();
			base.Height = this.button_Advance.Top + this.button_Advance.Height + (this.button_Advance.Top - (this.dataGridView_DeviceList.Top + this.dataGridView_DeviceList.Height)) + 30;
			formMain.ML.NowFormID = formConnectToWIFI.formID;
			this.Language_Text();
		}

		private void EnabledOrDisableAll(bool flag)
		{
			this.dataGridView_DeviceList.Enabled = flag;
			this.button_Advance.Enabled = flag;
			this.button_Cancel.Enabled = flag;
			this.buttonConnect.Enabled = flag;
			if (!this.isFindDis)
			{
				this.button_changePassword.Enabled = flag;
				this.button_changeSSID.Enabled = flag;
				this.button_Send.Enabled = flag;
				this.checkBox_Showpassword.Enabled = flag;
				this.checkBox1.Enabled = flag;
				this.button_changeChannel.Enabled = flag;
				this.button_updataCode.Enabled = flag;
			}
			this.button_Refresh.Enabled = flag;
			if (!flag)
			{
				this.textBox1.Enabled = false;
				return;
			}
			if (this.checkBox_Showpassword.Checked)
			{
				this.textBox1.Enabled = true;
				return;
			}
			this.textBox1.Enabled = false;
		}

		public bool ConnectWifi()
		{
			this.isWiFiConnect = false;
			this.isLoad = false;
			this.pType = LedPortType.WiFi;
			this.protocolVersion = 49;
			base.ShowDialog();
			return this.ConnectResult;
		}

		public bool FindConnectWifi(formMain pManform)
		{
			this.isWiFiConnect = true;
			this.isLoad = false;
			this.pType = LedPortType.WiFi;
			this.protocolVersion = 49;
			this.button_Send.Enabled = false;
			this.button_changePassword.Enabled = false;
			this.button_changeSSID.Enabled = false;
			this.buttonConnect.Location = this.button_Send.Location;
			this.buttonConnect.Enabled = true;
			this.buttonConnect.Visible = true;
			this.button_updataCode.Enabled = false;
			this.button_changeChannel.Enabled = false;
			this.fm = pManform;
			this.isFindDis = true;
			base.ShowDialog();
			return this.ConnectResult;
		}

		public bool ConnectToWifi(formMain pManform)
		{
			this.isWiFiConnect = true;
			this.isLoad = false;
			this.pType = LedPortType.WiFi;
			this.protocolVersion = 49;
			this.button_Send.Visible = false;
			this.textBox1.Visible = false;
			this.checkBox_Showpassword.Visible = false;
			this.label2.Visible = false;
			this.button_changePassword.Visible = false;
			this.button_changeSSID.Visible = false;
			this.button_changeChannel.Visible = false;
			this.button_updataCode.Visible = false;
			this.button_Advance.Visible = false;
			this.checkBox1.Visible = false;
			this.buttonConnect.Location = this.button_Send.Location;
			this.buttonConnect.Enabled = true;
			this.buttonConnect.Visible = true;
			this.fm = pManform;
			this.isFindDis = true;
			base.ShowDialog();
			return this.ConnectResult;
		}

		private bool CreateConfig(string pXmlPath)
		{
			bool result;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement("WIFIINFO");
				xmlDocument.AppendChild(xmlElement);
				XmlElement xmlElement2 = xmlDocument.CreateElement("WIFI");
				xmlElement2.SetAttribute("SSID", "");
				xmlElement2.SetAttribute("RSSID", "");
				xmlElement2.SetAttribute("PASSWORD", "");
				xmlElement.AppendChild(xmlElement2);
				xmlDocument.Save(pXmlPath);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void formConnectToWIFI_Load(object sender, EventArgs e)
		{
			try
			{
				if (formMain.IsXP())
				{
					this.checkBox_Showpassword.Enabled = false;
				}
				this.timer3.Start();
				if (Program.IsforeignTradeMode)
				{
					base.Icon = Resources.AppIconV5;
				}
				else
				{
					base.Icon = Resources.AppIcon;
				}
				this.getWIfiList();
				if (formMain.IsXP())
				{
					this.label3.Visible = true;
				}
				string text = Application.StartupPath + "\\WifiInfo.xml";
				if (!File.Exists(text))
				{
					this.CreateConfig(text);
				}
				this.wifiXML.Load(text);
				this.wifiInfoModel = this.wifiXML.SelectSingleNode("/WIFIINFO/WIFI");
				this.showPassword();
				this.SetDefaultSSID();
				if (this.isWiFiConnect)
				{
					LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
					if (selectedPanel != null && selectedPanel.PortType != LedPortType.WiFi)
					{
						this.pType = selectedPanel.PortType;
						this.protocolVersion = selectedPanel.ProtocolVersion;
						selectedPanel.PortType = LedPortType.WiFi;
						selectedPanel.ProtocolVersion = 49;
						formMain.ModifyPanelFromIPCServer(selectedPanel);
					}
				}
				call.OnDeviceCmdReturnResult += new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void OnDeviceCmdReturn(object sender, DeviceCmdEventArgs arg)
		{
			IPC_SIMPLE_ANSWER isa = arg.isa;
			if ((byte)isa.cmd_id == 18)
			{
				if (!isa.is_cmd_failed_flag)
				{
					if (this.IsOnlyConntectToWifi)
					{
						this.ConnectResult = true;
						base.Close();
						return;
					}
					if (isa.is_cmd_over_flag && isa.return_object.GetType() == typeof(LedPanel))
					{
						LedPanel ledPanel = isa.return_object as LedPanel;
						LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
						formWifiFindInfo formWifiFindInfo = new formWifiFindInfo(ledPanel);
						if (formWifiFindInfo.wifiFindDisplay())
						{
							if (!formConnectToWIFI.ComparePanelWifi(selectedPanel, ledPanel))
							{
								ledPanel.TextName = selectedPanel.TextName;
								ledPanel.ValueName = selectedPanel.ValueName;
								ledPanel.Group = selectedPanel.Group;
								ledPanel.PortType = LedPortType.WiFi;
								ledPanel.LTPolarity = formMain.ledsys.SelectedPanel.LTPolarity;
								formMain.ReplacePanelFromIPCServer(selectedPanel, ledPanel);
								formMain.ledsys.ReplacePanel(ledPanel);
							}
							this.isLoad = true;
							this.ConnectResult = true;
						}
						base.Close();
						return;
					}
				}
				else if (isa.is_cmd_over_flag)
				{
					if (this.ResendCount < 4)
					{
						this.ResendCount++;
						this.wifiFindDeviceSend();
						return;
					}
					this.timer1.Stop();
					MessageBox.Show(this, formMain.GetStr("formConnectToWIFI_message_Connectionfailed"));
					this.pictureBox1.Visible = false;
					this.EnabledOrDisableAll(true);
				}
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.getWIfiList();
			this.showPassword();
			this.SetDefaultSSID();
		}

		private void getWIfiList()
		{
			try
			{
				this.dataGridView_DeviceList.Rows.Clear();
				this.zhWIFIListAdded.Clear();
				if (formConnectToWIFI.client == null)
				{
					formConnectToWIFI.client = new WlanClient();
				}
				WlanClient.WlanInterface[] interfaces = formConnectToWIFI.client.Interfaces;
				for (int i = 0; i < interfaces.Length; i++)
				{
					WlanClient.WlanInterface wlanInterface = interfaces[i];
					this.wlanIfacecc = wlanInterface;
					Wlan.WlanAvailableNetwork[] availableNetworkList = wlanInterface.GetAvailableNetworkList((Wlan.WlanGetAvailableNetworkFlags)0);
					Wlan.WlanAvailableNetwork[] array = availableNetworkList;
					for (int j = 0; j < array.Length; j++)
					{
						Wlan.WlanAvailableNetwork network = array[j];
						string stringForSSID = formConnectToWIFI.GetStringForSSID(network.dot11Ssid);
						if (this.checkBox1.Checked)
						{
							this.AddSSIDToGrid(network);
						}
						else if (stringForSSID.StartsWith("ZH-"))
						{
							this.AddSSIDToGrid(network);
						}
						else if (stringForSSID.StartsWith("ZH_PP"))
						{
							this.AddSSIDToGrid(network);
						}
					}
				}
			}
			catch
			{
			}
		}

		private void AddSSIDToGrid(Wlan.WlanAvailableNetwork network)
		{
			string text = network.profileName;
			string text2 = formMain.ML.GetStr("WIFI_Send_Saved");
			if (text.Trim() == "")
			{
				text = formConnectToWIFI.GetStringForSSID(network.dot11Ssid);
				text2 = "";
			}
			if ((network.flags & Wlan.WlanAvailableNetworkFlags.Connected) == Wlan.WlanAvailableNetworkFlags.Connected)
			{
				text2 = text2 + "(" + formMain.ML.GetStr("WIFI_Send_Connected") + ")";
				this.currentSSID = formConnectToWIFI.GetStringForSSID(network.dot11Ssid);
			}
			for (int i = 0; i < this.zhWIFIListAdded.Count; i++)
			{
				if (this.zhWIFIListAdded[i] == text)
				{
					return;
				}
			}
			this.zhWIFIList.Add(network);
			this.zhWIFIListAdded.Add(text);
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			dataGridViewRow.CreateCells(this.dataGridView_DeviceList);
			dataGridViewRow.Cells[0].Value = text;
			dataGridViewRow.Cells[1].Value = this.getSignal(network.wlanSignalQuality);
			dataGridViewRow.Cells[2].Value = text2;
			this.dataGridView_DeviceList.Rows.Add(dataGridViewRow);
			if ((network.flags & Wlan.WlanAvailableNetworkFlags.Connected) == Wlan.WlanAvailableNetworkFlags.Connected)
			{
				this.dataGridView_DeviceList.CurrentCell = this.dataGridView_DeviceList.Rows[dataGridViewRow.Index].Cells[0];
				dataGridViewRow.Selected = true;
			}
			else
			{
				dataGridViewRow.Selected = false;
			}
			dataGridViewRow.Height = 20;
		}

		private string GetNetWorkStatus(Wlan.WlanAvailableNetwork network)
		{
			string text = network.profileName;
			string text2 = formMain.GetStr("WIFI_Save");
			if (text.Trim() == "")
			{
				text = formConnectToWIFI.GetStringForSSID(network.dot11Ssid);
				text2 = "";
			}
			if ((network.flags & Wlan.WlanAvailableNetworkFlags.Connected) == Wlan.WlanAvailableNetworkFlags.Connected)
			{
				text2 = text2 + "(" + formMain.GetStr("WIFI_Connected") + ")";
				this.currentSSID = formConnectToWIFI.GetStringForSSID(network.dot11Ssid);
			}
			return text2;
		}

		private string getSignal(uint pSignal)
		{
			if (pSignal > 80u)
			{
				return this.signalDescription[0];
			}
			if (pSignal > 60u)
			{
				return this.signalDescription[1];
			}
			if (pSignal > 40u)
			{
				return this.signalDescription[2];
			}
			if (pSignal > 20u)
			{
				return this.signalDescription[3];
			}
			if (pSignal > 0u)
			{
				return this.signalDescription[4];
			}
			return this.signalDescription[5];
		}

		public static string GetStringForSSID(Wlan.Dot11Ssid ssid)
		{
			return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
		}

		private void StartSend()
		{
			if (this.isSingleModel)
			{
				base.Dispose();
				return;
			}
			if (this.isFindDis)
			{
				this.ResendCount = 0;
				this.timer1.Start();
				this.wifiFindDeviceSend();
				return;
			}
			this.StartSend("");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.thread_time_finddevice != null)
				{
					this.thread_time_finddevice.Dispose();
				}
				base.Dispose();
			}
			catch
			{
			}
		}

		private void ConnectBySSIDAndPassword(string pPassword, bool isConnectNow)
		{
			if (formMain.IsXP())
			{
				return;
			}
			this.EnabledOrDisableAll(false);
			if (this.dataGridView_DeviceList.Rows.Count == 0)
			{
				return;
			}
			int index = 0;
			if (this.dataGridView_DeviceList.CurrentRow != null)
			{
				index = this.dataGridView_DeviceList.CurrentRow.Index;
			}
			string text = this.dataGridView_DeviceList.Rows[index].Cells[0].Value.ToString();
			string text2 = "<?xml version=\"1.0\"?>\r\n<WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\">\r\n\t<name>SSIDHere</name>\r\n\t<SSIDConfig>\r\n\t\t<SSID>\r\n\t\t\t<name>SSIDHere</name>\r\n\t\t</SSID>\r\n\t</SSIDConfig>\r\n\t<connectionType>ESS</connectionType>\r\n\t<connectionMode>auto</connectionMode>\r\n\t<MSM>\r\n\t\t<security>\r\n\t\t\t<authEncryption>\r\n\t\t\t\t<authentication>WPA2PSK</authentication>\r\n\t\t\t\t<encryption>AES</encryption>\r\n\t\t\t\t<useOneX>false</useOneX>\r\n\t\t\t</authEncryption>\r\n\t\t\t<sharedKey>\r\n\t\t\t\t<keyType>passPhrase</keyType>\r\n\t\t\t\t<protected>false</protected>\r\n\t\t\t\t<keyMaterial>WIFIPasswordIsHere</keyMaterial>\r\n\t\t\t</sharedKey>\r\n\t\t</security>\r\n\t</MSM>\r\n</WLANProfile>\r\n";
			text2 = text2.Replace("WIFIPasswordIsHere", pPassword);
			text2 = text2.Replace("SSIDHere", text);
			try
			{
				this.wlanIfacecc.SetProfile(Wlan.WlanProfileFlags.User, text2, true);
			}
			catch
			{
			}
			if (isConnectNow)
			{
				this.wlanIfacecc.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, text);
			}
		}

		private bool ConnectBySSIDAndPassword(string pSSID, string pPassword, bool isConnectNow)
		{
			bool result;
			try
			{
				string text = "<?xml version=\"1.0\"?>\r\n<WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\">\r\n\t<name>SSIDHere</name>\r\n\t<SSIDConfig>\r\n\t\t<SSID>\r\n\t\t\t<name>SSIDHere</name>\r\n\t\t</SSID>\r\n\t</SSIDConfig>\r\n\t<connectionType>ESS</connectionType>\r\n\t<connectionMode>auto</connectionMode>\r\n\t<MSM>\r\n\t\t<security>\r\n\t\t\t<authEncryption>\r\n\t\t\t\t<authentication>WPA2PSK</authentication>\r\n\t\t\t\t<encryption>AES</encryption>\r\n\t\t\t\t<useOneX>false</useOneX>\r\n\t\t\t</authEncryption>\r\n\t\t\t<sharedKey>\r\n\t\t\t\t<keyType>passPhrase</keyType>\r\n\t\t\t\t<protected>false</protected>\r\n\t\t\t\t<keyMaterial>WIFIPasswordIsHere</keyMaterial>\r\n\t\t\t</sharedKey>\r\n\t\t</security>\r\n\t</MSM>\r\n</WLANProfile>\r\n";
				text = text.Replace("WIFIPasswordIsHere", pPassword);
				text = text.Replace("SSIDHere", pSSID);
				try
				{
					this.wlanIfacecc.SetProfile(Wlan.WlanProfileFlags.User, text, true);
				}
				catch
				{
					this.wlanIfacecc.DeleteProfile(pSSID);
					this.wlanIfacecc.SetProfile(Wlan.WlanProfileFlags.User, text, true);
				}
				if (isConnectNow)
				{
					this.wlanIfacecc.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, pSSID);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public bool IsTargetSSID()
		{
			int index = 0;
			if (this.dataGridView_DeviceList.CurrentRow != null)
			{
				index = this.dataGridView_DeviceList.CurrentRow.Index;
			}
			string currentWIfiSSID = formConnectToWIFI.GetCurrentWIfiSSID();
			string stringForSSID = formConnectToWIFI.GetStringForSSID(this.zhWIFIList[index].dot11Ssid);
			return currentWIfiSSID == stringForSSID;
		}

		public void SetDefaultSSID()
		{
			if (this.dataGridView_DeviceList.Rows.Count == 0)
			{
				return;
			}
			if (this.dataGridView_DeviceList.Rows.Count == 1)
			{
				this.dataGridView_DeviceList.Rows[0].Selected = true;
				return;
			}
			for (int i = 0; i < this.dataGridView_DeviceList.Rows.Count; i++)
			{
				string text = this.dataGridView_DeviceList.Rows[i].Cells[2].Value.ToString();
				if (text.IndexOf(formMain.ML.GetStr("WIFI_Send_Connected")) > -1)
				{
					this.dataGridView_DeviceList.Rows[i].Selected = true;
					return;
				}
			}
		}

		private void button_Send_Click(object sender, EventArgs e)
		{
			this.fm.isUpdataCode = false;
			this.fm.isDownloadStringLibrary = false;
			Thread thread = new Thread(new ThreadStart(this.startSendCheck));
			this.threadList.Add(thread);
			thread.Start();
		}

		private void startSendCheck()
		{
			if (this.dataGridView_DeviceList.SelectedRows.Count == 0)
			{
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_Send_PleaseSelect"));
				return;
			}
			this.targetSSID = this.getTargetSSID();
			string currentWIfiSSID = formConnectToWIFI.GetCurrentWIfiSSID();
			if (formMain.IsXP())
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.button_Advance.Enabled = false;
					this.button_Send.Enabled = false;
					this.button_Refresh.Enabled = false;
				}));
				Thread.Sleep(100);
				this.StartSend("");
				base.Invoke(new MethodInvoker(delegate
				{
					this.button_Advance.Enabled = true;
					this.button_Send.Enabled = true;
					this.button_Refresh.Enabled = true;
				}));
				Thread.Sleep(100);
				return;
			}
			if (currentWIfiSSID == this.targetSSID)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.button_Advance.Enabled = false;
					this.button_Send.Enabled = false;
					this.button_Refresh.Enabled = false;
				}));
				Thread.Sleep(100);
				this.StartSend("");
				base.Invoke(new MethodInvoker(delegate
				{
					this.button_Advance.Enabled = true;
					this.button_Send.Enabled = true;
					this.button_Refresh.Enabled = true;
				}));
				Thread.Sleep(100);
				return;
			}
			this.IsConnectOneTime = false;
			this.connect_time = 0;
			this.IsFindOrSend = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.EnabledOrDisableAll(false);
			}));
			Thread.Sleep(100);
			this.notprofile_defaultPassword = false;
			this.profileName_Default = null;
			this.defaultPassword_successed = false;
			this.Connecting_WIfi();
		}

		private void startConnectToWifiBySSID(string pSSID)
		{
			this.getWIfiList();
			if (this.zhWIFIList.Count == 0)
			{
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_Send_NoWIFI"));
				return;
			}
			int num = -1;
			string pProfile = "";
			for (int i = 0; i < this.zhWIFIList.Count; i++)
			{
				if (formConnectToWIFI.GetStringForSSID(this.zhWIFIList[i].dot11Ssid) == pSSID)
				{
					num = i;
					pProfile = this.zhWIFIList[i].profileName;
				}
			}
			if (num == -1)
			{
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_Send_WIFIInvalid"));
				return;
			}
			this.startConnect(pSSID, pProfile);
		}

		private void startConnect(string pSSID, string pProfile)
		{
			if (this.passwordChanged)
			{
				this.ConnectBySSIDAndPassword(pSSID, this.textBox1.Text, true);
			}
			else if (pProfile == "")
			{
				this.ConnectBySSIDAndPassword(pSSID, "12345678", true);
			}
			else
			{
				this.wlanIfacecc.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, pProfile);
			}
			this.timer_new_count = 0;
			Thread.Sleep(500);
			this.timer_new.Start();
		}

		private string getTargetSSID()
		{
			string result;
			try
			{
				string text = this.dataGridView_DeviceList.SelectedRows[0].Cells[0].Value.ToString();
				result = text;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private void StartSend(string pVersion)
		{
			this.fm.StartSend(false, this);
			if (this.isWiFiProductionTest)
			{
				Thread.Sleep(600);
				this.fm.Timing(false, this);
			}
		}

		public static string GetCurrentWIfiSSID()
		{
			string result;
			try
			{
				if (formConnectToWIFI.client == null)
				{
					formConnectToWIFI.client = new WlanClient();
				}
				WlanClient.WlanInterface[] interfaces = formConnectToWIFI.client.Interfaces;
				for (int i = 0; i < interfaces.Length; i++)
				{
					WlanClient.WlanInterface wlanInterface = interfaces[i];
					Wlan.WlanAvailableNetwork[] availableNetworkList = wlanInterface.GetAvailableNetworkList((Wlan.WlanGetAvailableNetworkFlags)0);
					Wlan.WlanAvailableNetwork[] array = availableNetworkList;
					for (int j = 0; j < array.Length; j++)
					{
						Wlan.WlanAvailableNetwork wlanAvailableNetwork = array[j];
						if ((wlanAvailableNetwork.flags & Wlan.WlanAvailableNetworkFlags.Connected) == Wlan.WlanAvailableNetworkFlags.Connected)
						{
							result = Encoding.ASCII.GetString(wlanAvailableNetwork.dot11Ssid.SSID, 0, (int)wlanAvailableNetwork.dot11Ssid.SSIDLength);
							return result;
						}
					}
				}
				result = "";
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private bool CheckToNetwork()
		{
			this.EnabledOrDisableAll(false);
			if (this.dataGridView_DeviceList.Rows.Count == 0)
			{
				return false;
			}
			if (this.IsTargetSSID())
			{
				return true;
			}
			int index = 0;
			if (this.dataGridView_DeviceList.CurrentRow != null)
			{
				index = this.dataGridView_DeviceList.CurrentRow.Index;
			}
			if (this.passwordChanged)
			{
				if (!this.passwordChanged)
				{
					MessageBox.Show(this, formMain.GetStr("WIFI_Passwordmust8"));
					return false;
				}
				this.ConnectBySSIDAndPassword(this.textBox1.Text, true);
			}
			this.targetSSID = formConnectToWIFI.GetStringForSSID(this.zhWIFIList[index].dot11Ssid);
			string profileName = this.zhWIFIList[index].profileName;
			if (profileName == "")
			{
				this.ConnectBySSIDAndPassword("12345678", true);
			}
			else
			{
				this.wlanIfacecc.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
			}
			int i = 0;
			while (i < 30)
			{
				Thread.Sleep(400);
				i++;
				if (this.IsTargetSSID())
				{
					return true;
				}
			}
			this.EnabledOrDisableAll(true);
			return false;
		}

		private void button_Advance_Click(object sender, EventArgs e)
		{
			if (!this.isShowAdvancedOption)
			{
				if (this.isFindDis)
				{
					base.Height = this.label4.Top + this.label4.Height + 2 + 30;
				}
				else
				{
					base.Height = this.button_updataCode.Top + this.button_updataCode.Height + (this.button_updataCode.Top - (this.button_changeChannel.Top + this.button_changeChannel.Height)) + 30;
				}
				this.isShowAdvancedOption = true;
				return;
			}
			base.Height = this.button_Advance.Top + this.button_Advance.Height + (this.button_Advance.Top - (this.dataGridView_DeviceList.Top + this.dataGridView_DeviceList.Height)) + 30;
			this.isShowAdvancedOption = false;
		}

		private void checkBox_Showpassword_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.checkBox_Showpassword.Checked)
			{
				this.textBox1.Enabled = false;
				return;
			}
			if (formMain.IsXP())
			{
				return;
			}
			this.textBox1.Enabled = true;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (!this.textBox1.Focused)
			{
				return;
			}
			if (this.textBox1.Text.Trim().Length >= 8)
			{
				this.passwordChanged = true;
				int index = 0;
				if (this.dataGridView_DeviceList.CurrentRow != null)
				{
					index = this.dataGridView_DeviceList.CurrentRow.Index;
				}
				string stringForSSID = formConnectToWIFI.GetStringForSSID(this.zhWIFIList[index].dot11Ssid);
				try
				{
					this.setPassword(stringForSSID, this.textBox1.Text.Trim());
				}
				catch
				{
				}
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.getWIfiList();
		}

		private void button_changePassword_Click(object sender, EventArgs e)
		{
			this.fm.isUpdataCode = false;
			this.fm.isDownloadStringLibrary = false;
			formChangeWifiPassword.LastResult = false;
			formSendSingle.LastSendResult = false;
			formChangeWIFISSID.LastResult = false;
			if (this.CheckToNetwork())
			{
				this.EnabledOrDisableAll(true);
				formChangeWifiPassword formChangeWifiPassword = new formChangeWifiPassword(this.fm, this);
				this.timer2.Start();
				formChangeWifiPassword.ShowDialog();
				return;
			}
			this.EnabledOrDisableAll(true);
		}

		private string getPassword(string pSSID)
		{
			string result;
			try
			{
				XmlNode xmlNode = this.wifiXML.SelectSingleNode("/WIFIINFO/WIFI[@SSID='" + pSSID + "']");
				if (xmlNode == null)
				{
					result = "12345678";
				}
				else
				{
					result = xmlNode.Attributes["PASSWORD"].Value;
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private void setPassword(string pSSID, string pPassword)
		{
			XmlNode xmlNode = this.wifiXML.SelectSingleNode("/WIFIINFO/WIFI[@SSID='" + pSSID + "']");
			XmlNode xmlNode2 = this.wifiXML.SelectSingleNode("/WIFIINFO");
			XmlElement xmlElement = (XmlElement)xmlNode;
			XmlElement xmlElement2 = (XmlElement)xmlNode2;
			if (xmlNode2 == null)
			{
				xmlElement2 = this.wifiXML.CreateElement("WIFIINFO");
				this.wifiXML.AppendChild(xmlElement2);
			}
			if (xmlNode == null)
			{
				xmlElement = this.wifiXML.CreateElement("WIFI");
				xmlElement2.AppendChild(xmlElement);
			}
			xmlElement.SetAttribute("SSID", pSSID);
			xmlElement.SetAttribute("PASSWORD", pPassword);
			this.wifiXML.Save(Application.StartupPath + "\\WifiInfo.xml");
		}

		private void startChangePassword()
		{
		}

		private void button_changeSSID_Click(object sender, EventArgs e)
		{
			this.fm.isUpdataCode = false;
			this.fm.isDownloadStringLibrary = false;
			formChangeWIFISSID.LastResult = false;
			formSendSingle.LastSendResult = false;
			formChangeWifiPassword.LastResult = false;
			formSendSingle.LastSendResult = false;
			formChangeWIFISSID.LastResult = false;
			if (this.CheckToNetwork())
			{
				this.EnabledOrDisableAll(true);
				string currentWIfiSSID = formConnectToWIFI.GetCurrentWIfiSSID();
				if (currentWIfiSSID == "")
				{
					currentWIfiSSID = this.targetSSID;
				}
				if (currentWIfiSSID.Length > 6 && (currentWIfiSSID.StartsWith("ZH-W") || currentWIfiSSID.StartsWith("ZH-PP") || currentWIfiSSID.StartsWith("ZH-5W")))
				{
					formChangeWIFISSID formChangeWIFISSID = new formChangeWIFISSID(this.fm, currentWIfiSSID, this);
					this.timer2.Start();
					formChangeWIFISSID.ShowDialog();
					return;
				}
			}
			else
			{
				this.EnabledOrDisableAll(true);
			}
		}

		private void startChangeSSID()
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void dataGridView_DeviceList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				this.passwordChanged = false;
				this.dataGridView_DeviceList.CurrentRow.Selected = true;
			}
			catch
			{
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			if (formChangeWifiPassword.LastResult)
			{
				this.timer2.Stop();
				this.EnabledOrDisableAll(true);
				this.textBox1.Text = formChangeWifiPassword.LastWIFIPassword;
				this.ConnectBySSIDAndPassword(formChangeWifiPassword.LastWIFIPassword, false);
				int index = 0;
				if (this.dataGridView_DeviceList.CurrentRow != null)
				{
					index = this.dataGridView_DeviceList.CurrentRow.Index;
				}
				string stringForSSID = formConnectToWIFI.GetStringForSSID(this.zhWIFIList[index].dot11Ssid);
				this.setPassword(stringForSSID, formChangeWifiPassword.LastWIFIPassword);
				formChangeWifiPassword.LastResult = false;
				base.Dispose();
			}
			if (formChangeWIFISSID.LastResult)
			{
				formChangeWifiPassword.LastResult = false;
				this.timer2.Stop();
				base.Dispose();
			}
		}

		private void showPassword()
		{
			if (this.dataGridView_DeviceList.Rows.Count == 0)
			{
				return;
			}
			try
			{
				int index = 0;
				if (this.dataGridView_DeviceList.CurrentRow != null)
				{
					index = this.dataGridView_DeviceList.CurrentRow.Index;
				}
				string stringForSSID = formConnectToWIFI.GetStringForSSID(this.zhWIFIList[index].dot11Ssid);
				this.textBox1.Text = this.getPassword(stringForSSID);
			}
			catch
			{
			}
		}

		private void formConnectToWIFI_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				this.timer1.Stop();
				call.OnDeviceCmdReturnResult -= new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
				if ((this.notprofile_defaultPassword || this.passwordChanged) && !this.defaultPassword_successed)
				{
					this.wlanIfacecc.DeleteProfile(this.profileName_Default);
				}
				if (this.thread_time_finddevice != null)
				{
					this.thread_time_finddevice.Dispose();
				}
				if (this.isWiFiConnect && !this.isLoad)
				{
					LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
					if (selectedPanel != null && this.pType != LedPortType.WiFi)
					{
						selectedPanel.PortType = this.pType;
						selectedPanel.ProtocolVersion = this.protocolVersion;
						formMain.ModifyPanelFromIPCServer(selectedPanel);
					}
				}
			}
			catch
			{
			}
		}

		private void SetEnableNow()
		{
			this.SetEnableNow(true);
		}

		private void SetEnableNow(bool able_set)
		{
			if (base.InvokeRequired)
			{
				this.SetAble = new formConnectToWIFI.SetButtonEnable(this.SetEnableNow);
				base.Invoke(this.SetAble, new object[]
				{
					able_set
				});
				return;
			}
			this.button_Advance.Enabled = able_set;
			this.button_Send.Enabled = able_set;
			this.button_Refresh.Enabled = able_set;
		}

		private void UpdateWIFIState(bool isSelect)
		{
			string text = "";
			try
			{
				if (formConnectToWIFI.client == null)
				{
					formConnectToWIFI.client = new WlanClient();
				}
				WlanClient.WlanInterface[] interfaces = formConnectToWIFI.client.Interfaces;
				for (int i = 0; i < interfaces.Length; i++)
				{
					WlanClient.WlanInterface wlanInterface = interfaces[i];
					Wlan.WlanAvailableNetwork[] availableNetworkList = wlanInterface.GetAvailableNetworkList((Wlan.WlanGetAvailableNetworkFlags)0);
					Wlan.WlanAvailableNetwork[] array = availableNetworkList;
					for (int j = 0; j < array.Length; j++)
					{
						Wlan.WlanAvailableNetwork network = array[j];
						string profileName = network.profileName;
						if (text.IndexOf(profileName + ",") <= 0)
						{
							text = text + profileName + ",";
							for (int k = 0; k < this.dataGridView_DeviceList.Rows.Count; k++)
							{
								if (profileName == this.dataGridView_DeviceList.Rows[k].Cells[0].Value.ToString())
								{
									formConnectToWIFI.SSID_state_now = this.GetNetWorkStatus(network);
									this.dataGridView_DeviceList.Rows[k].Cells[2].Value = formConnectToWIFI.SSID_state_now;
									break;
								}
							}
						}
					}
					for (int l = 0; l < this.dataGridView_DeviceList.Rows.Count; l++)
					{
						if (text.IndexOf(this.dataGridView_DeviceList.Rows[l].Cells[0].Value.ToString()) == -1)
						{
							this.dataGridView_DeviceList.Rows[l].Cells[2].Value = "";
						}
					}
				}
			}
			catch
			{
			}
		}

		private void timer3_Tick_1(object sender, EventArgs e)
		{
			this.UpdateWIFIState(false);
		}

		private void dataGridView_DeviceList_DoubleClick(object sender, EventArgs e)
		{
		}

		private void dataGridView_DeviceList_CurrentCellChanged(object sender, EventArgs e)
		{
			if (this.dataGridView_DeviceList.CurrentRow != null)
			{
				this.dataGridView_DeviceList.CurrentRow.Selected = true;
				this.textBox1.Enabled = false;
				this.passwordChanged = false;
				this.checkBox_Showpassword.Checked = false;
				this.showPassword();
			}
		}

		private void pictureBox1_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void dataGridView_DeviceList_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				this.dataGridView_DeviceList.CurrentRow.Selected = true;
			}
			catch
			{
			}
		}

		private void dataGridView_DeviceList_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				this.dataGridView_DeviceList.CurrentRow.Selected = true;
				if (this.isWiFiProductionTest)
				{
					this.button_Send_Click(null, null);
				}
			}
			catch
			{
			}
		}

		private void button_updataCode_Click(object sender, EventArgs e)
		{
			this.fm.isUpdataCode = true;
			this.fm.isDownloadStringLibrary = false;
			this.IsConnectOneTime = false;
			this.connect_time = 0;
			this.IsFindOrSend = false;
			this.notprofile_defaultPassword = false;
			this.profileName_Default = null;
			this.defaultPassword_successed = false;
			this.Connecting_WIfi();
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			this.fm.isUpdataCode = false;
			this.fm.isDownloadStringLibrary = false;
			formChangeWifiPassword.LastResult = false;
			formSendSingle.LastSendResult = false;
			this.IsConnectOneTime = false;
			this.connect_time = 0;
			this.IsFindOrSend = true;
			this.notprofile_defaultPassword = false;
			this.profileName_Default = null;
			this.defaultPassword_successed = false;
			this.EnabledOrDisableAll(false);
			this.pictureBox1.Visible = true;
			this.Connecting_WIfi();
		}

		private void Connecting_WIfi()
		{
			try
			{
				if (this.dataGridView_DeviceList.Rows.Count != 0)
				{
					string text = null;
					string text2 = null;
					WlanClient.WlanInterface[] interfaces = formConnectToWIFI.client.Interfaces;
					for (int i = 0; i < interfaces.Length; i++)
					{
						WlanClient.WlanInterface wlanInterface = interfaces[i];
						Wlan.WlanAvailableNetwork[] availableNetworkList = wlanInterface.GetAvailableNetworkList((Wlan.WlanGetAvailableNetworkFlags)0);
						Wlan.WlanAvailableNetwork[] array = availableNetworkList;
						for (int j = 0; j < array.Length; j++)
						{
							Wlan.WlanAvailableNetwork wlanAvailableNetwork = array[j];
							string stringForSSID = formConnectToWIFI.GetStringForSSID(wlanAvailableNetwork.dot11Ssid);
							if (stringForSSID.StartsWith("ZH-") || stringForSSID.StartsWith("ZH_PP"))
							{
								text2 = text2 + stringForSSID + ",";
							}
							string profileName = wlanAvailableNetwork.profileName;
							if (profileName != "")
							{
								text = text + profileName + ",";
							}
						}
					}
					if (formMain.IsXP())
					{
						int index = 0;
						if (this.dataGridView_DeviceList.CurrentRow != null)
						{
							index = this.dataGridView_DeviceList.CurrentRow.Index;
						}
						this.targetSSID = formConnectToWIFI.GetStringForSSID(this.zhWIFIList[index].dot11Ssid);
						this.currentSSID = formConnectToWIFI.GetCurrentWIfiSSID();
						if (this.isFindDis)
						{
							this.ResendCount = 0;
							this.timer1.Start();
							this.wifiFindDeviceSend();
						}
						else
						{
							this.StartSend("");
						}
						string profileName2 = this.zhWIFIList[index].profileName;
						if (profileName2 == "")
						{
							MessageBox.Show(this, formMain.ML.GetStr("WIFI_NoProfile"));
						}
						else
						{
							this.EnabledOrDisableAll(false);
							this.wlanIfacecc.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName2);
							this.thread_time_finddevice = new System.Threading.Timer(new TimerCallback(this.Thread_ConnectAndFindDevice), this.zhWIFIList[index], 0, 1000);
						}
					}
					else if (this.dataGridView_DeviceList.Rows.Count != 0)
					{
						if (this.IsTargetSSID())
						{
							this.StartSend();
							this.EnabledOrDisableAll(true);
							this.pictureBox1.Visible = false;
						}
						else
						{
							int index2 = 0;
							if (this.dataGridView_DeviceList.CurrentRow != null)
							{
								index2 = this.dataGridView_DeviceList.CurrentRow.Index;
							}
							this.profileName_Default = this.dataGridView_DeviceList.Rows[index2].Cells[0].Value.ToString();
							if (text2.IndexOf(this.profileName_Default) == -1)
							{
								MessageBox.Show(this, formMain.ML.GetStr("formConnectToWIFI_message_not_exist"));
								this.EnabledOrDisableAll(true);
								this.pictureBox1.Visible = false;
							}
							else if (this.passwordChanged)
							{
								this.ConnectBySSIDAndPassword(this.textBox1.Text, true);
								this.state_link_currentWifi = this.zhWIFIList[index2];
								this.thread_time_finddevice = new System.Threading.Timer(new TimerCallback(this.Thread_ConnectAndFindDevice), null, 0, 1000);
							}
							else
							{
								this.targetSSID = formConnectToWIFI.GetStringForSSID(this.zhWIFIList[index2].dot11Ssid);
								string profileName3 = this.zhWIFIList[index2].profileName;
								if (text != null && text.IndexOf(profileName3) != -1 && profileName3 != "")
								{
									this.wlanIfacecc.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName3);
									this.state_link_currentWifi = this.zhWIFIList[index2];
									this.thread_time_finddevice = new System.Threading.Timer(new TimerCallback(this.Thread_ConnectAndFindDevice), null, 0, 1000);
								}
								else
								{
									this.notprofile_defaultPassword = true;
									this.ConnectBySSIDAndPassword("12345678", true);
									this.state_link_currentWifi = this.zhWIFIList[index2];
									this.thread_time_finddevice = new System.Threading.Timer(new TimerCallback(this.Thread_ConnectAndFindDevice), null, 0, 1000);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message);
				this.EnabledOrDisableAll(true);
				this.pictureBox1.Visible = false;
			}
		}

		private void Thread_ConnectAndFindDevice(object state)
		{
			if (this.connect_time <= 20)
			{
				this.connect_time++;
				try
				{
					if (!this.IsConnectOneTime)
					{
						bool flag = false;
						if (formConnectToWIFI.client == null)
						{
							formConnectToWIFI.client = new WlanClient();
						}
						WlanClient.WlanInterface[] interfaces = formConnectToWIFI.client.Interfaces;
						for (int i = 0; i < interfaces.Length; i++)
						{
							WlanClient.WlanInterface wlanInterface = interfaces[i];
							Wlan.WlanAvailableNetwork[] availableNetworkList = wlanInterface.GetAvailableNetworkList((Wlan.WlanGetAvailableNetworkFlags)0);
							Wlan.WlanAvailableNetwork[] array = availableNetworkList;
							int j = 0;
							while (j < array.Length)
							{
								Wlan.WlanAvailableNetwork wlanAvailableNetwork = array[j];
								if ((wlanAvailableNetwork.flags & Wlan.WlanAvailableNetworkFlags.Connected) == Wlan.WlanAvailableNetworkFlags.Connected && wlanAvailableNetwork.profileName == this.profileName_Default)
								{
									this.IsConnectOneTime = true;
									this.thread_time_finddevice.Dispose();
									flag = true;
									if (this.IsFindOrSend)
									{
										Thread thread = new Thread(new ThreadStart(this.FindingAndSendParam));
										thread.Start();
										this.defaultPassword_successed = true;
										break;
									}
									Thread thread2 = new Thread(new ThreadStart(this.SendData_NotLink));
									thread2.Start();
									this.defaultPassword_successed = true;
									break;
								}
								else
								{
									j++;
								}
							}
							if (flag)
							{
								break;
							}
						}
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.thread_time_finddevice.Dispose();
			Thread thread3 = new Thread(new ThreadStart(this.FailureMessage));
			thread3.Start();
		}

		private void FailureMessage()
		{
			try
			{
				if (this.notprofile_defaultPassword || this.passwordChanged)
				{
					this.wlanIfacecc.DeleteProfile(this.profileName_Default);
				}
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_ErrorPassword"));
				this.pictureBox1.Visible = false;
				this.EnabledOrDisableAll(true);
				this.refreash = new Thread(new ThreadStart(this.refreash_Grid));
				this.refreash.Start();
			}
			catch
			{
			}
		}

		private void refreash_Grid()
		{
			this.ThreadFunction();
		}

		private void ThreadFunction()
		{
			if (this.dataGridView_DeviceList.InvokeRequired)
			{
				formConnectToWIFI.ProxyClient method = new formConnectToWIFI.ProxyClient(this.ThreadFunction);
				base.Invoke(method);
				return;
			}
			this.getWIfiList();
			this.showPassword();
			this.SetDefaultSSID();
			this.refreash.Abort();
		}

		private void SendData_NotLink()
		{
			base.Invoke(new MethodInvoker(delegate
			{
				this.StartSend("");
				this.EnabledOrDisableAll(true);
			}));
		}

		private void FindingAndSendParam()
		{
			this.wifiFindDeviceSend();
			this.pictureBox1.Visible = false;
			this.EnabledOrDisableAll(true);
		}

		private static bool ComparePanelWifi(LedPanel pOldPanel, LedPanel pNewPanel)
		{
			return false;
		}

		private void wifiFindDeviceSend()
		{
			this.dtStart = DateTime.Now;
			formMain.IServer.send_cmd_to_device_async(18, null, formMain.ledsys.SelectedPanel.ProductID);
		}

		private void button_changeChannel_Click(object sender, EventArgs e)
		{
			this.fm.isUpdataCode = false;
			this.fm.isDownloadStringLibrary = false;
			formChangeWIFISSID.LastResult = false;
			formSendSingle.LastSendResult = false;
			formChangeWifiPassword.LastResult = false;
			formSendSingle.LastSendResult = false;
			formChangeWIFISSID.LastResult = false;
			formChangeWifiChannel.LastResult = false;
			formChangeWifiChannel.LastSendResult = false;
			if (this.CheckToNetwork())
			{
				this.EnabledOrDisableAll(true);
				string currentWIfiSSID = formConnectToWIFI.GetCurrentWIfiSSID();
				if (currentWIfiSSID == "")
				{
					currentWIfiSSID = this.targetSSID;
				}
				if (currentWIfiSSID.Length > 10 && (currentWIfiSSID.StartsWith("ZH-W") || currentWIfiSSID.StartsWith("ZH-PP") || currentWIfiSSID.StartsWith("ZH-5W")))
				{
					formChangeWifiChannel formChangeWifiChannel = new formChangeWifiChannel(this.fm, this);
					this.timer2.Start();
					formChangeWifiChannel.ShowDialog();
					formChangeWifiChannel.Dispose();
					return;
				}
			}
			else
			{
				this.EnabledOrDisableAll(true);
			}
		}

		private void timer_new_Tick(object sender, EventArgs e)
		{
			this.timer_new_count++;
			if (this.timer_new_count > 21)
			{
				this.timer_new.Stop();
				this.EnabledOrDisableAll(true);
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_Send_WifiConnectFailed"));
			}
			string currentWIfiSSID = formConnectToWIFI.GetCurrentWIfiSSID();
			if (currentWIfiSSID != "")
			{
				this.timer_new.Stop();
				this.EnabledOrDisableAll(true);
				if (currentWIfiSSID == this.targetSSID)
				{
					this.StartSend("");
					return;
				}
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_Send_WifiConnectFailed"));
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.pictureBox1.Visible)
			{
				DateTime now = DateTime.Now;
				if ((now - this.dtStart).TotalSeconds > 30.0)
				{
					this.timer1.Stop();
					MessageBox.Show(this, formMain.ML.GetStr("formConnectToWIFI_message_Connectionfailed"));
					this.pictureBox1.Visible = false;
					this.EnabledOrDisableAll(true);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formConnectToWIFI));
			this.label1 = new Label();
			this.button_Cancel = new Button();
			this.textBox1 = new TextBox();
			this.label2 = new Label();
			this.button_Refresh = new Button();
			this.dataGridView_DeviceList = new DataGridView();
			this.Col_SSID = new DataGridViewTextBoxColumn();
			this.Col_Signal = new DataGridViewTextBoxColumn();
			this.Col_Status = new DataGridViewTextBoxColumn();
			this.button_Advance = new Button();
			this.button_Send = new Button();
			this.checkBox1 = new CheckBox();
			this.checkBox_Showpassword = new CheckBox();
			this.button_changePassword = new Button();
			this.button_changeSSID = new Button();
			this.pictureBox1 = new PictureBox();
			this.label3 = new Label();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.timer3 = new System.Windows.Forms.Timer(this.components);
			this.button_updataCode = new Button();
			this.buttonConnect = new Button();
			this.button_changeChannel = new Button();
			this.timer_new = new System.Windows.Forms.Timer(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label4 = new Label();
			((ISupportInitialize)this.dataGridView_DeviceList).BeginInit();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "请选择要发送的设备:";
			this.button_Cancel.Location = new System.Drawing.Point(284, 236);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.button_Cancel.TabIndex = 3;
			this.button_Cancel.Text = "关闭";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new EventHandler(this.button2_Click);
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(69, 267);
			this.textBox1.Name = "textBox1";
			this.textBox1.PasswordChar = '*';
			this.textBox1.Size = new System.Drawing.Size(181, 21);
			this.textBox1.TabIndex = 4;
			this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
			this.label2.Location = new System.Drawing.Point(3, 271);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "密码:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.button_Refresh.Location = new System.Drawing.Point(118, 236);
			this.button_Refresh.Name = "button_Refresh";
			this.button_Refresh.Size = new System.Drawing.Size(75, 23);
			this.button_Refresh.TabIndex = 6;
			this.button_Refresh.Text = "刷新";
			this.button_Refresh.UseVisualStyleBackColor = true;
			this.button_Refresh.Click += new EventHandler(this.button3_Click);
			this.dataGridView_DeviceList.AllowUserToAddRows = false;
			this.dataGridView_DeviceList.AllowUserToDeleteRows = false;
			this.dataGridView_DeviceList.AllowUserToResizeColumns = false;
			this.dataGridView_DeviceList.AllowUserToResizeRows = false;
			this.dataGridView_DeviceList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_DeviceList.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Col_SSID,
				this.Col_Signal,
				this.Col_Status
			});
			this.dataGridView_DeviceList.Location = new System.Drawing.Point(-1, 24);
			this.dataGridView_DeviceList.MultiSelect = false;
			this.dataGridView_DeviceList.Name = "dataGridView_DeviceList";
			this.dataGridView_DeviceList.RowHeadersVisible = false;
			this.dataGridView_DeviceList.RowTemplate.Height = 23;
			this.dataGridView_DeviceList.ScrollBars = ScrollBars.Vertical;
			this.dataGridView_DeviceList.Size = new System.Drawing.Size(374, 210);
			this.dataGridView_DeviceList.TabIndex = 7;
			this.dataGridView_DeviceList.CellClick += new DataGridViewCellEventHandler(this.dataGridView_DeviceList_CellClick);
			this.dataGridView_DeviceList.CurrentCellChanged += new EventHandler(this.dataGridView_DeviceList_CurrentCellChanged);
			this.dataGridView_DeviceList.DoubleClick += new EventHandler(this.dataGridView_DeviceList_DoubleClick);
			this.dataGridView_DeviceList.MouseClick += new MouseEventHandler(this.dataGridView_DeviceList_MouseClick);
			this.dataGridView_DeviceList.MouseDoubleClick += new MouseEventHandler(this.dataGridView_DeviceList_MouseDoubleClick);
			this.Col_SSID.HeaderText = "网络名称";
			this.Col_SSID.Name = "Col_SSID";
			this.Col_SSID.ReadOnly = true;
			this.Col_SSID.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Col_SSID.Width = 170;
			this.Col_Signal.HeaderText = "信号强度";
			this.Col_Signal.Name = "Col_Signal";
			this.Col_Signal.ReadOnly = true;
			this.Col_Signal.Resizable = DataGridViewTriState.True;
			this.Col_Signal.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Col_Signal.Width = 80;
			this.Col_Status.HeaderText = "状态";
			this.Col_Status.Name = "Col_Status";
			this.Col_Status.ReadOnly = true;
			this.Col_Status.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Col_Status.Width = 120;
			this.button_Advance.Location = new System.Drawing.Point(10, 236);
			this.button_Advance.Name = "button_Advance";
			this.button_Advance.Size = new System.Drawing.Size(100, 23);
			this.button_Advance.TabIndex = 9;
			this.button_Advance.Text = "高级功能";
			this.button_Advance.UseVisualStyleBackColor = true;
			this.button_Advance.Click += new EventHandler(this.button_Advance_Click);
			this.button_Send.Location = new System.Drawing.Point(201, 236);
			this.button_Send.Name = "button_Send";
			this.button_Send.Size = new System.Drawing.Size(75, 23);
			this.button_Send.TabIndex = 10;
			this.button_Send.Text = "发送";
			this.button_Send.UseVisualStyleBackColor = true;
			this.button_Send.Click += new EventHandler(this.button_Send_Click);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(3, 240);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(96, 16);
			this.checkBox1.TabIndex = 11;
			this.checkBox1.Text = "显示所有网络";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.Visible = false;
			this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
			this.checkBox_Showpassword.AutoSize = true;
			this.checkBox_Showpassword.Location = new System.Drawing.Point(256, 270);
			this.checkBox_Showpassword.Name = "checkBox_Showpassword";
			this.checkBox_Showpassword.Size = new System.Drawing.Size(72, 16);
			this.checkBox_Showpassword.TabIndex = 12;
			this.checkBox_Showpassword.Text = "输入密码";
			this.checkBox_Showpassword.UseVisualStyleBackColor = true;
			this.checkBox_Showpassword.CheckedChanged += new EventHandler(this.checkBox_Showpassword_CheckedChanged);
			this.button_changePassword.Location = new System.Drawing.Point(30, 319);
			this.button_changePassword.Name = "button_changePassword";
			this.button_changePassword.Size = new System.Drawing.Size(314, 23);
			this.button_changePassword.TabIndex = 13;
			this.button_changePassword.Text = "修改网络密码";
			this.button_changePassword.UseVisualStyleBackColor = true;
			this.button_changePassword.Click += new EventHandler(this.button_changePassword_Click);
			this.button_changeSSID.Location = new System.Drawing.Point(30, 348);
			this.button_changeSSID.Name = "button_changeSSID";
			this.button_changeSSID.Size = new System.Drawing.Size(314, 23);
			this.button_changeSSID.TabIndex = 14;
			this.button_changeSSID.Text = "修改网络名称";
			this.button_changeSSID.UseVisualStyleBackColor = true;
			this.button_changeSSID.Click += new EventHandler(this.button_changeSSID_Click);
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = (System.Drawing.Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new System.Drawing.Point(-1, 189);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(374, 41);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 15;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			this.pictureBox1.VisibleChanged += new EventHandler(this.pictureBox1_VisibleChanged);
			this.label3.Location = new System.Drawing.Point(1, 189);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(369, 44);
			this.label3.TabIndex = 16;
			this.label3.Text = "XP以及更旧的操作系统不支持自动连接WIFI,请手工连接至WIFI网络后再点发送按钮";
			this.label3.Visible = false;
			this.timer2.Tick += new EventHandler(this.timer2_Tick);
			this.timer3.Interval = 1000;
			this.timer3.Tick += new EventHandler(this.timer3_Tick_1);
			this.button_updataCode.Location = new System.Drawing.Point(30, 407);
			this.button_updataCode.Name = "button_updataCode";
			this.button_updataCode.Size = new System.Drawing.Size(314, 23);
			this.button_updataCode.TabIndex = 17;
			this.button_updataCode.Text = "升级程序";
			this.button_updataCode.UseVisualStyleBackColor = true;
			this.button_updataCode.Click += new EventHandler(this.button_updataCode_Click);
			this.buttonConnect.Enabled = false;
			this.buttonConnect.Location = new System.Drawing.Point(201, 236);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(75, 23);
			this.buttonConnect.TabIndex = 18;
			this.buttonConnect.Text = "连接";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Visible = false;
			this.buttonConnect.Click += new EventHandler(this.buttonConnect_Click);
			this.button_changeChannel.Location = new System.Drawing.Point(30, 377);
			this.button_changeChannel.Name = "button_changeChannel";
			this.button_changeChannel.Size = new System.Drawing.Size(314, 23);
			this.button_changeChannel.TabIndex = 19;
			this.button_changeChannel.Text = "修改网络信道";
			this.button_changeChannel.UseVisualStyleBackColor = true;
			this.button_changeChannel.Click += new EventHandler(this.button_changeChannel_Click);
			this.timer_new.Interval = 500;
			this.timer_new.Tick += new EventHandler(this.timer_new_Tick);
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(30, 295);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(314, 18);
			this.label4.TabIndex = 20;
			this.label4.Text = "Prompt: There should not be spaces in the password";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(372, 440);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.button_changeChannel);
			base.Controls.Add(this.buttonConnect);
			base.Controls.Add(this.button_updataCode);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.button_changeSSID);
			base.Controls.Add(this.button_changePassword);
			base.Controls.Add(this.checkBox_Showpassword);
			base.Controls.Add(this.button_Send);
			base.Controls.Add(this.button_Advance);
			base.Controls.Add(this.dataGridView_DeviceList);
			base.Controls.Add(this.button_Refresh);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.checkBox1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formConnectToWIFI";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "WIFI发送";
			base.FormClosing += new FormClosingEventHandler(this.formConnectToWIFI_FormClosing);
			base.Load += new EventHandler(this.formConnectToWIFI_Load);
			((ISupportInitialize)this.dataGridView_DeviceList).EndInit();
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
