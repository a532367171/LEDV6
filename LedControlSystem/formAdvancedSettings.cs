using LedControlSystem.Properties;
using LedModel;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formAdvancedSettings : Form
	{
		private static string formID = "formAdvancedSettings";

		private formMain fm;

		private LedPanel panel;

		private bool isModified;

		private LedEthernetCommunicationMode eMode;

		private IContainer components;

		private Panel panel_LocalServerSet;

		private GroupBox gbLocalServer;

		private Button button_SetlocalServer;

		private Label label3;

		private CheckBox checkBoxLocalServerEnabled;

		private TextBox textBox_NetID_localServer;

		private TextBox textBox_heartbeat_localServer;

		private Label lblNetID;

		private Label lblHeartbeat;

		private TextBox textBox_port_localServer;

		private Label lblLocalServerIPPort;

		private Label lblLocalServerIPAddress;

		private Panel panel_FixedIPSet;

		private GroupBox gbFixedIP;

		private Button button_Set_Client;

		private Label lblGateway;

		private Label lblIPMask;

		private TextBox textBox_port_Client;

		private Label lblIPPort;

		private Label lblIPAddress;

		private Panel panel_Advanced;

		private GroupBox gbMAC;

		private Button button_Set_MAC;

		private Label label8;

		private Button button_SetPassword;

		private CheckBox checkBox_PasswordEnabled;

		private TextBox textBox_Password;

		private Label lblPassword;

		private GroupBox gbPassword;

		private TextBox textBoxMAC;

		private Panel PnlCloudServer;

		private GroupBox GrpCloudServer;

		private Button BtnSetCloudServer;

		private Label label1;

		private TextBox TxtCloudHeartBeatPort;

		private TextBox TxtCloudHeartBeatTime;

		private Label LblCloudHeartBeatPort;

		private Label LblCloudHeartBeatTime;

		private TextBox TxtCloudServerPort;

		private Label LblCloudServerPort;

		private Label LblCloudServerDomainName;

		private CheckBox ChkCloudServer;

		private TextBox TxtCloudServerDomainName;

		private RadioButton RdoCloudDomain;

		private RadioButton RdoCloudIP;

		private Label LblCloudServerIP;

		private RadioButton RdoCloudUdp;

		private RadioButton RdoCloudTcp;

		private Label LblCloudServerCommMode;

		private Label LblCloudServerAccessMode;

		private Panel panel2;

		private Panel panel1;

		private TextBox txtIPAddress;

		private TextBox txtGateway;

		private TextBox txtIPMask;

		private TextBox txtLocalServerIPAddress;

		private TextBox txtCloudServerIPAddress;

		private CheckBox chkMac;

		private Label lblMacHint;

		private TextBox txtCloudServerUserName;

		private Label lblCloudServerUserName;

		private CheckBox chkDHCPEnabled;

		private TextBox txtDNS;

		private Label lblDNS;

		private CheckBox chkDNS;

		public static string FormID
		{
			get
			{
				return formAdvancedSettings.formID;
			}
			set
			{
				formAdvancedSettings.formID = value;
			}
		}

		public formAdvancedSettings()
		{
			this.InitializeComponent();
			this.Diplay_lanuage_Text();
		}

		public void Diplay_lanuage_Text()
		{
			this.Text = formMain.ML.GetStr("formAdvancedSettings_FormText");
			this.chkMac.Text = formMain.ML.GetStr("formAdvancedSettings_groupBox_MAC");
			this.lblMacHint.Text = formMain.ML.GetStr("formAdvancedSettings_label_Mac_Hint");
			this.label8.Text = formMain.ML.GetStr("formAdvancedSettings_label_Mac_Address");
			this.button_Set_MAC.Text = formMain.ML.GetStr("formAdvancedSettings_button_Set_MAC");
			this.checkBox_PasswordEnabled.Text = formMain.ML.GetStr("formAdvancedSettings_checkBox_PasswordEnabled");
			this.gbPassword.Text = formMain.ML.GetStr("formAdvancedSettings_groupBox_PasswordSet");
			this.lblPassword.Text = formMain.ML.GetStr("formAdvancedSettings_label_Password");
			this.button_SetPassword.Text = formMain.ML.GetStr("formAdvancedSettings_button_SetPassword");
			this.gbFixedIP.Text = formMain.ML.GetStr("formAdvancedSettings_groupBox_FixedIP");
			this.lblIPAddress.Text = formMain.ML.GetStr("formAdvancedSettings_label_IPAdrress");
			this.lblIPPort.Text = formMain.ML.GetStr("formAdvancedSettings_label_Port");
			this.lblIPMask.Text = formMain.ML.GetStr("formAdvancedSettings_label_SubnetMask");
			this.lblGateway.Text = formMain.ML.GetStr("formAdvancedSettings_label_DefaultGateway");
			this.lblDNS.Text = formMain.ML.GetStr("formAdvancedSettings_label_DNS");
			this.chkDHCPEnabled.Text = formMain.ML.GetStr("formAdvancedSettings_CheckBox_DHCPMode");
			this.button_Set_Client.Text = formMain.ML.GetStr("formAdvancedSettings_button_Set_Client");
			this.checkBoxLocalServerEnabled.Text = formMain.ML.GetStr("formAdvancedSettings_checkBoxLocalServerEnabled");
			this.lblLocalServerIPAddress.Text = formMain.ML.GetStr("formAdvancedSettings_label_LocalServerIPAddress");
			this.lblLocalServerIPPort.Text = formMain.ML.GetStr("formAdvancedSettings_label_LocalServerIPPort");
			this.lblHeartbeat.Text = formMain.ML.GetStr("formAdvancedSettings_label_Heartbeat");
			this.label3.Text = formMain.ML.GetStr("formAdvancedSettings_label_MS");
			this.lblNetID.Text = formMain.ML.GetStr("formAdvancedSettings_label_NetID");
			this.button_SetlocalServer.Text = formMain.ML.GetStr("formAdvancedSettings_button_SetlocalServer");
			this.ChkCloudServer.Text = formMain.ML.GetStr("formAdvancedSettings_label_CloudServerMode");
			this.RdoCloudDomain.Text = formMain.ML.GetStr("formAdvancedSettings_label_CloudServerDomainName");
			this.RdoCloudIP.Text = formMain.ML.GetStr("formAdvancedSettings_label_CloudServerIP");
			this.LblCloudServerIP.Text = formMain.ML.GetStr("formAdvancedSettings_label_CloudServerIP");
			this.LblCloudServerDomainName.Text = formMain.ML.GetStr("formAdvancedSettings_label_CloudServerDomainName");
			this.lblCloudServerUserName.Text = formMain.ML.GetStr("formAdvancedSettings_label_CloudSeverUserName");
			this.LblCloudServerPort.Text = formMain.ML.GetStr("formAdvancedSettings_label_LocalServerIPPort");
			this.LblCloudHeartBeatTime.Text = formMain.ML.GetStr("formAdvancedSettings_label_Heartbeat");
			this.label1.Text = formMain.ML.GetStr("formMain_Panel_Playback_second");
			this.LblCloudHeartBeatPort.Text = formMain.ML.GetStr("formAdvancedSettings_label_HeartBeatPort");
			this.BtnSetCloudServer.Text = formMain.ML.GetStr("formAdvancedSettings_button_Set_Client");
			this.LblCloudServerCommMode.Text = formMain.ML.GetStr("formAdvancedSettings_label_CommMode");
			this.RdoCloudUdp.Text = formMain.ML.GetStr("formAdvancedSettings_checkBoxUDP");
			this.RdoCloudTcp.Text = formMain.ML.GetStr("formAdvancedSettings_checkBoxTCP");
			this.LblCloudServerAccessMode.Text = formMain.ML.GetStr("formAdvancedSettings_label_AccessMode");
		}

		public formAdvancedSettings(LedPanel pPanel, formMain pMain)
		{
			this.InitializeComponent();
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.checkBox_PasswordEnabled.Enabled = false;
			this.gbPassword.Enabled = false;
			this.panel = pPanel;
			this.fm = pMain;
			this.Diplay_lanuage_Text();
			this.lblMacHint.Left = this.chkMac.Left + this.chkMac.Width;
			if (!this.panel.IsLSeries())
			{
				this.checkBox_PasswordEnabled.Enabled = false;
				this.gbPassword.Enabled = false;
				this.textBox_port_Client.Enabled = false;
				this.txtIPMask.Enabled = false;
				this.txtGateway.Enabled = false;
				this.checkBoxLocalServerEnabled.Enabled = false;
				this.gbLocalServer.Enabled = false;
				this.button_SetlocalServer.Enabled = false;
			}
		}

		public void SetPanel_Sizeandcontent(LedEthernetCommunicationMode mode, bool IsMode_L = true)
		{
			this.eMode = mode;
			if (mode == LedEthernetCommunicationMode.Directly)
			{
				this.panel_Advanced.Visible = true;
				this.panel_FixedIPSet.Visible = false;
				this.panel_LocalServerSet.Visible = false;
				this.PnlCloudServer.Visible = false;
				this.panel_Advanced.Top = 12;
			}
			else if (mode == LedEthernetCommunicationMode.FixedIP)
			{
				this.panel_Advanced.Visible = true;
				this.panel_FixedIPSet.Visible = true;
				this.panel_LocalServerSet.Visible = false;
				this.PnlCloudServer.Visible = false;
				this.panel_Advanced.Top = this.panel_LocalServerSet.Top;
			}
			else if (mode == LedEthernetCommunicationMode.LocalServer)
			{
				this.panel_Advanced.Visible = true;
				this.panel_FixedIPSet.Visible = true;
				this.panel_LocalServerSet.Visible = true;
				this.PnlCloudServer.Visible = false;
				this.panel_Advanced.Top = this.PnlCloudServer.Top;
			}
			else if (mode == LedEthernetCommunicationMode.CloudServer)
			{
				this.panel_Advanced.Visible = true;
				this.panel_FixedIPSet.Visible = true;
				this.panel_LocalServerSet.Visible = false;
				this.PnlCloudServer.Visible = true;
				this.PnlCloudServer.Top = this.panel_LocalServerSet.Top;
				this.panel_Advanced.Top = this.PnlCloudServer.Top + this.PnlCloudServer.Height + 6;
			}
			base.Height = this.panel_Advanced.Top + this.panel_Advanced.Height + 45;
		}

		private void formAdvancedSettings_Load(object sender, EventArgs e)
		{
			this.textBoxMAC.Text = this.panel.MACAddress;
			this.chkMac.Checked = false;
			this.label8.Enabled = false;
			this.button_Set_MAC.Enabled = false;
			this.textBoxMAC.Enabled = false;
			bool flag = Convert.ToBoolean((int)this.panel.AuthorityPasswordMode);
			this.checkBox_PasswordEnabled.Checked = flag;
			this.lblPassword.Enabled = flag;
			this.textBox_Password.Enabled = flag;
			this.textBox_Password.Text = this.panel.AuthorityPassword;
			this.txtIPAddress.Text = this.panel.IPAddress;
			this.textBox_port_Client.Text = this.panel.IPPort.ToString();
			this.txtIPMask.Text = this.panel.IPNetMask;
			this.txtGateway.Text = this.panel.IPGateway;
			this.chkDHCPEnabled.Checked = Convert.ToBoolean((int)this.panel.DHCPMode);
			this.chkDNS.Checked = !string.IsNullOrEmpty(this.panel.DNS);
			this.txtDNS.Enabled = this.chkDNS.Checked;
			this.txtDNS.Text = this.panel.DNS;
			bool flag2 = Convert.ToBoolean((int)this.panel.LocalServerMode);
			this.checkBoxLocalServerEnabled.Checked = flag2;
			this.txtLocalServerIPAddress.Enabled = flag2;
			this.textBox_port_localServer.Enabled = flag2;
			this.textBox_heartbeat_localServer.Enabled = flag2;
			this.textBox_NetID_localServer.Enabled = flag2;
			this.lblLocalServerIPAddress.Enabled = flag2;
			this.lblLocalServerIPPort.Enabled = flag2;
			this.lblHeartbeat.Enabled = flag2;
			this.lblNetID.Enabled = flag2;
			this.label3.Enabled = flag2;
			this.button_SetlocalServer.Enabled = flag2;
			this.txtLocalServerIPAddress.Text = this.panel.LocalServerIPAddress;
			this.textBox_port_localServer.Text = this.panel.LocalServerIPPort.ToString();
			this.textBox_heartbeat_localServer.Text = this.panel.LocalServerHeartbeatTime.ToString();
			this.textBox_NetID_localServer.Text = this.panel.NetworkID;
			bool flag3 = Convert.ToBoolean((int)this.panel.CloudServerMode);
			this.ChkCloudServer.Checked = flag3;
			this.lblCloudServerUserName.Enabled = flag3;
			this.txtCloudServerUserName.Enabled = flag3;
			this.txtCloudServerUserName.Text = this.panel.CloudServerUserName;
			this.isModified = false;
		}

		private void formAdvancedSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.isModified && MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Confirm_DiscardChanges"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK)
			{
				e.Cancel = true;
			}
			if (this.eMode != this.panel.EthernetCommunicaitonMode)
			{
				this.panel.EthernetCommunicaitonMode = this.eMode;
			}
			formMain.ModifyPanelFromIPCServer(this.panel);
		}

		private void button_Set_MAC_Click(object sender, EventArgs e)
		{
			string text = formPanelEdit.FromStringToMac(this.textBoxMAC.Text.Trim());
			if (string.IsNullOrEmpty(text) || text == "FFFFFFFFFFFF" || text == "000000000000")
			{
				MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_SetMac_Error"));
				this.textBoxMAC.Focus();
				return;
			}
			if (MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Prompt_MAC_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
			{
				this.ChangeEModeToDirectly();
				LedCardCommunication ledCardCommunication = new LedCardCommunication();
				ledCardCommunication.MACAddress = text;
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_MAC, ledCardCommunication, formMain.ML.GetStr("NETCARD_message_SetMAC"), this.panel, false, this);
				if (formSendSingle.LastSendResult)
				{
					this.isModified = false;
					this.panel.MACAddress = text;
				}
			}
		}

		private void button_SetPassword_Click(object sender, EventArgs e)
		{
			bool @checked = this.checkBox_PasswordEnabled.Checked;
			LedCardCommunication ledCardCommunication = new LedCardCommunication();
			if (@checked)
			{
				if (string.IsNullOrEmpty(this.textBox_Password.Text))
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Password_NULL"));
					this.textBox_Password.Focus();
					return;
				}
				ledCardCommunication.AuthorityPassword = this.textBox_Password.Text;
			}
			else
			{
				ledCardCommunication.AuthorityPassword = string.Empty;
			}
			ledCardCommunication.AuthorityPasswordMode = (LedAuthorityPasswordMode)Convert.ToInt32(@checked);
			if (MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_prompt_Password_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
			{
				this.ChangeEModeToDirectly();
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_Authority, ledCardCommunication, formMain.ML.GetStr("NETCARD_message_SetPassword"), this.panel, false, this);
				if (formSendSingle.LastSendResult)
				{
					this.fm.SendSingleCmdStart(LedCmdType.Ctrl_Authority_Confirm, null, formMain.ML.GetStr("NETCARD_message_Confirm_SetPassword"), this.panel, false, this);
					if (formSendSingle.LastSendResult && formSendSingle.LastSendResultObject != null && formSendSingle.LastSendResultObject.GetType() == typeof(byte) && (byte)formSendSingle.LastSendResultObject == 0)
					{
						this.isModified = false;
						this.panel.AuthorityPassword = ledCardCommunication.AuthorityPassword;
						this.panel.AuthorityPasswordMode = ledCardCommunication.AuthorityPasswordMode;
					}
				}
			}
		}

		private void button_Set_Client_Click(object sender, EventArgs e)
		{
			string text = this.txtIPAddress.Text.Trim();
			string text2 = this.txtGateway.Text.Trim();
			string text3 = this.textBox_port_Client.Text;
			string text4 = this.txtIPMask.Text.Trim();
			bool @checked = this.chkDHCPEnabled.Checked;
			string text5 = this.txtDNS.Text.Trim();
			bool checked2 = this.chkDNS.Checked;
			if (!@checked)
			{
				if (!this.IPAddressIScorrect(text))
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_IPAddress_SetFault"));
					this.txtIPAddress.Focus();
					return;
				}
				if (!this.panel.IsLSeries())
				{
					this.ChangeEModeToDirectly();
					this.fm.SendSingleCmdStart(LedCmdType.Ctrl_IP_Address, text, formMain.ML.GetStr("NETCARD_message_SetIP"), this.panel, false, this);
					if (formSendSingle.LastSendResult)
					{
						this.isModified = false;
						this.panel.IPAddress = text;
					}
					return;
				}
				if (!this.IPAddressIScorrect(text2))
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_default_gateway_SetFault"));
					this.txtGateway.Focus();
					return;
				}
				if (!this.ISInteger(this.textBox_port_Client.Text))
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Port_SetFault"));
					this.textBox_port_Client.Focus();
					return;
				}
				int num = int.Parse(this.textBox_port_Client.Text);
				if (num > 65535 || num < 5000)
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Port_SetFault_range"));
					this.textBox_port_Client.Focus();
					return;
				}
				if (!this.IPAddressIScorrect(text4))
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Mask_SetFault"));
					this.textBox_port_Client.Focus();
					return;
				}
			}
			if (checked2 && !this.IPAddressIScorrect(text5))
			{
				MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_DNS_SetFault"));
				this.txtDNS.Focus();
				return;
			}
			if (MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Prompt_IP_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
			{
				this.ChangeEModeToDirectly();
				LedCardCommunication ledCardCommunication = new LedCardCommunication();
				ledCardCommunication.IPAddress = text;
				ledCardCommunication.IPPort = int.Parse(text3);
				ledCardCommunication.IPNetMask = text4;
				ledCardCommunication.IPGateway = text2;
				ledCardCommunication.DHCPMode = (LedDHCPMode)Convert.ToInt32(@checked);
				ledCardCommunication.DNS = (checked2 ? text5 : string.Empty);
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_DHCP_Mode, ledCardCommunication, formMain.ML.GetStr(@checked ? "NETCARD_message_Enable_DHCP" : "NETCARD_message_Disable_DHCP"), this.panel, false, this);
				if (formSendSingle.LastSendResult)
				{
					this.panel.DHCPMode = ledCardCommunication.DHCPMode;
					this.fm.SendSingleCmdStart(LedCmdType.Ctrl_DNS, ledCardCommunication, formMain.ML.GetStr("NETCARD_message_SetDNS"), this.panel, false, this);
					if (formSendSingle.LastSendResult)
					{
						this.panel.DNS = ledCardCommunication.DNS;
						if (!@checked)
						{
							this.fm.SendSingleCmdStart(LedCmdType.Ctrl_IP_Address, ledCardCommunication, formMain.ML.GetStr("NETCARD_message_SetIP"), this.panel, false, this);
							if (formSendSingle.LastSendResult)
							{
								this.isModified = false;
								this.panel.IPAddress = ledCardCommunication.IPAddress;
								this.panel.IPPort = ledCardCommunication.IPPort;
								this.panel.IPNetMask = ledCardCommunication.IPNetMask;
								this.panel.IPGateway = ledCardCommunication.IPGateway;
								return;
							}
						}
						else
						{
							this.isModified = false;
						}
					}
				}
			}
		}

		private void button_SetlocalServer_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Prompt_parameter_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			bool @checked = this.checkBoxLocalServerEnabled.Checked;
			if (@checked)
			{
				string text = this.txtLocalServerIPAddress.Text;
				if (!this.IPAddressIScorrect(text))
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_IPAddress_Fault"));
					this.txtLocalServerIPAddress.Focus();
					return;
				}
				string text2 = this.textBox_port_localServer.Text;
				if (!this.ISInteger(text2))
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_PortFault"));
					this.textBox_port_localServer.Focus();
					return;
				}
				int num = int.Parse(text2);
				if (num > 65535 || num < 5000)
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Port_SetFault_range"));
					this.textBox_port_localServer.Focus();
					return;
				}
				string text3 = this.textBox_heartbeat_localServer.Text;
				if (!this.ISInteger(text3))
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_HeartTimeSetFault"));
					this.textBox_heartbeat_localServer.Focus();
					return;
				}
				int num2 = int.Parse(text3);
				if (num2 < 3000)
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_HeartSetFault"));
					this.textBox_heartbeat_localServer.Focus();
					return;
				}
				string text4 = this.textBox_NetID_localServer.Text;
				if (string.IsNullOrEmpty(text4))
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_NetWorkID_null"));
					this.textBox_NetID_localServer.Focus();
					return;
				}
				if (Encoding.UTF8.GetBytes(text4).Length > 32)
				{
					MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_NetWorkID_Length_Over"));
					this.textBox_NetID_localServer.Focus();
					return;
				}
				this.ChangeEModeToDirectly();
				LedCardCommunication ledCardCommunication = new LedCardCommunication();
				ledCardCommunication.LocalServerIPAddress = text;
				ledCardCommunication.LocalServerIPPort = int.Parse(text2);
				ledCardCommunication.LocalServerHeartbeatTime = int.Parse(text3);
				ledCardCommunication.NetworkID = text4;
				ledCardCommunication.LocalServerMode = LedLocalServerMode.Enabled;
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_Server_Parameter, ledCardCommunication, formMain.ML.GetStr("NETCARD_message_Setup_local_server"), this.panel, false, this);
				if (formSendSingle.LastSendResult)
				{
					this.fm.SendSingleCmdStart(LedCmdType.Ctrl_Server_Mode, ledCardCommunication, formMain.ML.GetStr("NETCARD_message_Enable_local_server"), this.panel, false, this);
					if (formSendSingle.LastSendResult)
					{
						this.isModified = false;
						this.panel.LocalServerIPAddress = ledCardCommunication.LocalServerIPAddress;
						this.panel.LocalServerIPPort = ledCardCommunication.LocalServerIPPort;
						this.panel.LocalServerHeartbeatTime = ledCardCommunication.LocalServerHeartbeatTime;
						this.panel.NetworkID = ledCardCommunication.NetworkID;
						this.panel.LocalServerMode = ledCardCommunication.LocalServerMode;
						return;
					}
				}
			}
			else
			{
				this.ChangeEModeToDirectly();
				LedCardCommunication ledCardCommunication2 = new LedCardCommunication();
				ledCardCommunication2.LocalServerMode = LedLocalServerMode.Disabled;
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_Server_Mode, ledCardCommunication2, formMain.ML.GetStr("NETCARD_message_Shutdown_local_server"), this.panel, false, this);
				if (formSendSingle.LastSendResult)
				{
					this.isModified = false;
					this.panel.LocalServerMode = ledCardCommunication2.LocalServerMode;
				}
			}
		}

		private void textBoxMAC_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			this.isModified = true;
			string text = formPanelEdit.FromStringToMac(textBox.Text.Trim());
			if (string.IsNullOrEmpty(text) || text.ToUpper() == "FFFFFFFFFFFF" || text == "000000000000")
			{
				textBox.ForeColor = System.Drawing.Color.Red;
				return;
			}
			textBox.ForeColor = System.Drawing.Color.Black;
		}

		private void textBox_Password_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			this.isModified = true;
		}

		private void checkBox_PasswordEnable_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (!checkBox.Focused)
			{
				return;
			}
			this.isModified = true;
			bool @checked = checkBox.Checked;
			this.lblPassword.Enabled = @checked;
			this.textBox_Password.Enabled = @checked;
		}

		private void checkBoxLocalServerEnabled_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (!checkBox.Focused)
			{
				return;
			}
			this.isModified = true;
			bool @checked = checkBox.Checked;
			this.txtLocalServerIPAddress.Enabled = @checked;
			this.textBox_port_localServer.Enabled = @checked;
			this.textBox_heartbeat_localServer.Enabled = @checked;
			this.textBox_NetID_localServer.Enabled = @checked;
			this.lblLocalServerIPAddress.Enabled = @checked;
			this.lblLocalServerIPPort.Enabled = @checked;
			this.lblHeartbeat.Enabled = @checked;
			this.lblNetID.Enabled = @checked;
			this.label3.Enabled = @checked;
			this.button_SetlocalServer.Enabled = true;
		}

		private void textBox_port_Client_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private bool IPAddressIScorrect(string ip_addreess_srtr)
		{
			IPAddress iPAddress;
			return IPAddress.TryParse(ip_addreess_srtr, out iPAddress);
		}

		private bool ISInteger(string integer_str)
		{
			bool result;
			try
			{
				int.Parse(integer_str);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void chkDNS_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (checkBox.Focused)
			{
				this.isModified = true;
			}
			bool @checked = checkBox.Checked;
			this.txtDNS.Enabled = @checked;
		}

		private void chkDHCPEnabled_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (checkBox.Focused)
			{
				this.isModified = true;
			}
			bool @checked = checkBox.Checked;
			this.txtIPAddress.Enabled = !@checked;
			this.textBox_port_Client.Enabled = !@checked;
			this.txtIPMask.Enabled = !@checked;
			this.txtGateway.Enabled = !@checked;
		}

		private void ChangeEModeToDirectly()
		{
			if (this.panel.EthernetCommunicaitonMode != LedEthernetCommunicationMode.Directly)
			{
				this.panel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.Directly;
				formMain.ModifyPanelFromIPCServer(this.panel);
			}
		}

		private void ChkCloudServer_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (!checkBox.Focused)
			{
				return;
			}
			this.isModified = true;
			bool @checked = checkBox.Checked;
			this.lblCloudServerUserName.Enabled = @checked;
			this.txtCloudServerUserName.Enabled = @checked;
			this.BtnSetCloudServer.Enabled = true;
		}

		private void chkMac_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (!checkBox.Focused)
			{
				return;
			}
			bool @checked = checkBox.Checked;
			this.label8.Enabled = @checked;
			this.button_Set_MAC.Enabled = @checked;
			this.textBoxMAC.Enabled = @checked;
		}

		private void textBox_Data_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			this.isModified = true;
		}

		private void textBox_NumberText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void BtnSetCloudServer_Click(object sender, EventArgs e)
		{
			if (this.ChkCloudServer.Checked)
			{
				string text = this.txtCloudServerUserName.Text;
				if (string.IsNullOrEmpty(text))
				{
					MessageBox.Show(this, formMain.ML.GetStr("Message_CloudServer_UserName_Cannot_Be_Empty"));
					this.txtCloudServerUserName.Focus();
					return;
				}
				if (Encoding.Default.GetByteCount(text) > 30)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Message_CloudServer_UserName_Cannot_Be_More_Than_30_Characters"));
					this.txtCloudServerUserName.Focus();
					return;
				}
				bool flag = true;
				for (int i = 0; i < text.Length; i++)
				{
					int num = (int)text[i];
					if (num > 127 || num < 8 || (num > 8 && num < 21))
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Message_CloudServer_UserName_Can_Only_Support_Alphabet_Numbers_And_Special_Characters"));
					this.txtCloudServerUserName.Focus();
					return;
				}
				if (MessageBox.Show(this, "1." + formMain.ML.GetStr("NETCARD_message_Prompt_parameter_IPSetting") + "\r\n2." + formMain.ML.GetStr("NETCARD_message_Prompt_parameter_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
				{
					return;
				}
				this.ChangeEModeToDirectly();
				LedCardCommunication ledCardCommunication = new LedCardCommunication();
				ledCardCommunication.CloudServerMode = LedCloudServerMode.Enabled;
				ledCardCommunication.CloudServerUserName = text;
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_CloudServer_UserName, ledCardCommunication, formMain.ML.GetStr("NETCARD_message_Setup_CloudServer"), this.panel, false, this);
				if (formSendSingle.LastSendResult)
				{
					this.fm.SendSingleCmdStart(LedCmdType.Ctrl_CloudServer_Parameter, ledCardCommunication, formMain.ML.GetStr("NETCARD_message_Setup_CloudServer"), this.panel, false, this);
					if (formSendSingle.LastSendResult)
					{
						this.fm.SendSingleCmdStart(LedCmdType.Ctrl_CloudServer_Mode, ledCardCommunication, formMain.ML.GetStr("NETCARD_message_Enable_CloudServer"), this.panel, false, this);
						if (formSendSingle.LastSendResult)
						{
							this.isModified = false;
							this.panel.CloudServerMode = ledCardCommunication.CloudServerMode;
							this.panel.CloudServerUserName = ledCardCommunication.CloudServerUserName;
							return;
						}
					}
				}
			}
			else
			{
				if (MessageBox.Show(this, "1." + formMain.ML.GetStr("NETCARD_message_Prompt_parameter_IPSetting") + "\r\n2." + formMain.ML.GetStr("NETCARD_message_Prompt_parameter_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
				{
					return;
				}
				this.ChangeEModeToDirectly();
				LedCardCommunication ledCardCommunication2 = new LedCardCommunication();
				ledCardCommunication2.CloudServerMode = LedCloudServerMode.Disabled;
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_CloudServer_Mode, ledCardCommunication2, formMain.ML.GetStr("NETCARD_message_Shutdown_CloudServer"), this.panel, false, this);
				if (formSendSingle.LastSendResult)
				{
					this.isModified = false;
					this.panel.CloudServerMode = ledCardCommunication2.CloudServerMode;
				}
			}
		}

		private void RdoCloudIP_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.TxtCloudServerDomainName.Visible = false;
				this.LblCloudServerDomainName.Visible = false;
				this.txtCloudServerIPAddress.Visible = true;
				this.LblCloudServerIP.Visible = true;
			}
		}

		private void RdoCloudDomain_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.TxtCloudServerDomainName.Visible = true;
				this.LblCloudServerDomainName.Visible = true;
				this.txtCloudServerIPAddress.Visible = false;
				this.LblCloudServerIP.Visible = false;
			}
		}

		public int BitData(int Data, int PositionBit)
		{
			if ((double)(Data & (int)Math.Pow(2.0, (double)PositionBit)) != Math.Pow(2.0, (double)PositionBit))
			{
				return 0;
			}
			return 1;
		}

		private void txtIPAddress_TextChanged(object sender, EventArgs e)
		{
			this.IPAddressValidate(sender);
		}

		private void txtIPAddress_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.IPAddressInputValidate(e);
		}

		private void txtIPAddress_Leave(object sender, EventArgs e)
		{
			this.IPAddressInvalidMessage(sender, formMain.ML.GetStr("NETCARD_message_IPAddress_SetFault"));
		}

		private void txtIPMask_TextChanged(object sender, EventArgs e)
		{
			this.IPMaskValidate(sender);
		}

		private void txtIPMask_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.IPAddressInputValidate(e);
		}

		private void txtIPMask_Leave(object sender, EventArgs e)
		{
			this.IPAddressInvalidMessage(sender, formMain.ML.GetStr("NETCARD_message_Mask_SetFault"));
		}

		private void txtGateway_TextChanged(object sender, EventArgs e)
		{
			this.IPAddressValidate(sender);
		}

		private void txtGateway_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.IPAddressInputValidate(e);
		}

		private void txtGateway_Leave(object sender, EventArgs e)
		{
			this.IPAddressInvalidMessage(sender, formMain.ML.GetStr("NETCARD_message_default_gateway_SetFault"));
		}

		private void txtDNS_TextChanged(object sender, EventArgs e)
		{
			this.DNSValidate(sender);
		}

		private void txtDNS_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.IPAddressInputValidate(e);
		}

		private void txtDNS_Leave(object sender, EventArgs e)
		{
			this.IPAddressInvalidMessage(sender, formMain.ML.GetStr("NETCARD_message_DNS_SetFault"));
		}

		private void txtLocalServerIPAddress_TextChanged(object sender, EventArgs e)
		{
			this.IPAddressValidate(sender);
		}

		private void txtLocalServerIPAddress_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.IPAddressInputValidate(e);
		}

		private void txtLocalServerIPAddress_Leave(object sender, EventArgs e)
		{
			this.IPAddressInvalidMessage(sender, formMain.ML.GetStr("NETCARD_message_IPAddress_SetFault"));
		}

		private void txtCloudServerIPAddress_TextChanged(object sender, EventArgs e)
		{
			this.IPAddressValidate(sender);
		}

		private void txtCloudServerIPAddress_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.IPAddressInputValidate(e);
		}

		private void txtCloudServerIPAddress_Leave(object sender, EventArgs e)
		{
			this.IPAddressInvalidMessage(sender, formMain.ML.GetStr("NETCARD_message_IPAddress_SetFault"));
		}

		private void IPAddressValidate(object sender)
		{
			this.IPMaskValidate(sender);
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (textBox.ForeColor == System.Drawing.Color.Red)
			{
				return;
			}
			string text = textBox.Text.Trim();
			string[] array = text.Split(new char[]
			{
				'.'
			}, StringSplitOptions.RemoveEmptyEntries);
			int num = int.Parse(array[0]);
			if (num < 1 || num > 223)
			{
				textBox.ForeColor = System.Drawing.Color.Red;
			}
		}

		private void DNSValidate(object sender)
		{
			this.IPMaskValidate(sender);
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (string.IsNullOrEmpty(textBox.Text))
			{
				textBox.ForeColor = System.Drawing.Color.Black;
				return;
			}
			if (textBox.ForeColor == System.Drawing.Color.Red)
			{
				return;
			}
			string text = textBox.Text.Trim();
			string[] array = text.Split(new char[]
			{
				'.'
			}, StringSplitOptions.RemoveEmptyEntries);
			int num = int.Parse(array[0]);
			if (num < 1 || num > 223)
			{
				textBox.ForeColor = System.Drawing.Color.Red;
			}
		}

		private void IPMaskValidate(object sender)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			this.isModified = true;
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
				return;
			}
			textBox.ForeColor = System.Drawing.Color.Red;
		}

		private void IPAddressInputValidate(KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b' && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		private void IPAddressInvalidMessage(object sender, string msgtext)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.ForeColor == System.Drawing.Color.Red)
			{
				MessageBox.Show(this, msgtext);
				textBox.SelectAll();
				textBox.Focus();
			}
		}

		private void txtCloudServerUserName_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar > '\u007f' || e.KeyChar < '\b' || (e.KeyChar > '\b' && e.KeyChar < '\u0015'))
			{
				e.Handled = true;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formAdvancedSettings));
			this.panel_LocalServerSet = new Panel();
			this.gbLocalServer = new GroupBox();
			this.txtLocalServerIPAddress = new TextBox();
			this.checkBoxLocalServerEnabled = new CheckBox();
			this.button_SetlocalServer = new Button();
			this.label3 = new Label();
			this.textBox_NetID_localServer = new TextBox();
			this.textBox_heartbeat_localServer = new TextBox();
			this.lblNetID = new Label();
			this.lblHeartbeat = new Label();
			this.textBox_port_localServer = new TextBox();
			this.lblLocalServerIPPort = new Label();
			this.lblLocalServerIPAddress = new Label();
			this.gbPassword = new GroupBox();
			this.button_SetPassword = new Button();
			this.lblPassword = new Label();
			this.textBox_Password = new TextBox();
			this.checkBox_PasswordEnabled = new CheckBox();
			this.panel_FixedIPSet = new Panel();
			this.gbFixedIP = new GroupBox();
			this.chkDNS = new CheckBox();
			this.txtDNS = new TextBox();
			this.lblDNS = new Label();
			this.chkDHCPEnabled = new CheckBox();
			this.txtGateway = new TextBox();
			this.txtIPMask = new TextBox();
			this.txtIPAddress = new TextBox();
			this.button_Set_Client = new Button();
			this.lblGateway = new Label();
			this.lblIPMask = new Label();
			this.textBox_port_Client = new TextBox();
			this.lblIPPort = new Label();
			this.lblIPAddress = new Label();
			this.panel_Advanced = new Panel();
			this.gbMAC = new GroupBox();
			this.lblMacHint = new Label();
			this.chkMac = new CheckBox();
			this.textBoxMAC = new TextBox();
			this.button_Set_MAC = new Button();
			this.label8 = new Label();
			this.PnlCloudServer = new Panel();
			this.GrpCloudServer = new GroupBox();
			this.txtCloudServerUserName = new TextBox();
			this.lblCloudServerUserName = new Label();
			this.txtCloudServerIPAddress = new TextBox();
			this.panel2 = new Panel();
			this.RdoCloudUdp = new RadioButton();
			this.RdoCloudTcp = new RadioButton();
			this.LblCloudServerCommMode = new Label();
			this.ChkCloudServer = new CheckBox();
			this.panel1 = new Panel();
			this.RdoCloudDomain = new RadioButton();
			this.RdoCloudIP = new RadioButton();
			this.LblCloudServerAccessMode = new Label();
			this.LblCloudServerIP = new Label();
			this.TxtCloudServerDomainName = new TextBox();
			this.BtnSetCloudServer = new Button();
			this.label1 = new Label();
			this.TxtCloudHeartBeatPort = new TextBox();
			this.TxtCloudHeartBeatTime = new TextBox();
			this.LblCloudHeartBeatPort = new Label();
			this.LblCloudHeartBeatTime = new Label();
			this.TxtCloudServerPort = new TextBox();
			this.LblCloudServerPort = new Label();
			this.LblCloudServerDomainName = new Label();
			this.panel_LocalServerSet.SuspendLayout();
			this.gbLocalServer.SuspendLayout();
			this.gbPassword.SuspendLayout();
			this.panel_FixedIPSet.SuspendLayout();
			this.gbFixedIP.SuspendLayout();
			this.panel_Advanced.SuspendLayout();
			this.gbMAC.SuspendLayout();
			this.PnlCloudServer.SuspendLayout();
			this.GrpCloudServer.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panel_LocalServerSet.Controls.Add(this.gbLocalServer);
			this.panel_LocalServerSet.Location = new System.Drawing.Point(10, 210);
			this.panel_LocalServerSet.Name = "panel_LocalServerSet";
			this.panel_LocalServerSet.Size = new System.Drawing.Size(528, 174);
			this.panel_LocalServerSet.TabIndex = 22;
			this.gbLocalServer.Controls.Add(this.txtLocalServerIPAddress);
			this.gbLocalServer.Controls.Add(this.checkBoxLocalServerEnabled);
			this.gbLocalServer.Controls.Add(this.button_SetlocalServer);
			this.gbLocalServer.Controls.Add(this.label3);
			this.gbLocalServer.Controls.Add(this.textBox_NetID_localServer);
			this.gbLocalServer.Controls.Add(this.textBox_heartbeat_localServer);
			this.gbLocalServer.Controls.Add(this.lblNetID);
			this.gbLocalServer.Controls.Add(this.lblHeartbeat);
			this.gbLocalServer.Controls.Add(this.textBox_port_localServer);
			this.gbLocalServer.Controls.Add(this.lblLocalServerIPPort);
			this.gbLocalServer.Controls.Add(this.lblLocalServerIPAddress);
			this.gbLocalServer.Location = new System.Drawing.Point(7, 10);
			this.gbLocalServer.Name = "gbLocalServer";
			this.gbLocalServer.Size = new System.Drawing.Size(513, 157);
			this.gbLocalServer.TabIndex = 2;
			this.gbLocalServer.TabStop = false;
			this.txtLocalServerIPAddress.Location = new System.Drawing.Point(113, 36);
			this.txtLocalServerIPAddress.Name = "txtLocalServerIPAddress";
			this.txtLocalServerIPAddress.Size = new System.Drawing.Size(116, 21);
			this.txtLocalServerIPAddress.TabIndex = 21;
			this.txtLocalServerIPAddress.TextChanged += new EventHandler(this.txtLocalServerIPAddress_TextChanged);
			this.txtLocalServerIPAddress.KeyPress += new KeyPressEventHandler(this.txtLocalServerIPAddress_KeyPress);
			this.txtLocalServerIPAddress.Leave += new EventHandler(this.txtLocalServerIPAddress_Leave);
			this.checkBoxLocalServerEnabled.AutoSize = true;
			this.checkBoxLocalServerEnabled.Location = new System.Drawing.Point(9, 0);
			this.checkBoxLocalServerEnabled.Name = "checkBoxLocalServerEnabled";
			this.checkBoxLocalServerEnabled.Size = new System.Drawing.Size(108, 16);
			this.checkBoxLocalServerEnabled.TabIndex = 18;
			this.checkBoxLocalServerEnabled.Text = "本地服务器配置";
			this.checkBoxLocalServerEnabled.UseVisualStyleBackColor = true;
			this.checkBoxLocalServerEnabled.CheckedChanged += new EventHandler(this.checkBoxLocalServerEnabled_CheckedChanged);
			this.button_SetlocalServer.Location = new System.Drawing.Point(401, 119);
			this.button_SetlocalServer.Name = "button_SetlocalServer";
			this.button_SetlocalServer.Size = new System.Drawing.Size(75, 23);
			this.button_SetlocalServer.TabIndex = 8;
			this.button_SetlocalServer.Text = "设置发送";
			this.button_SetlocalServer.UseVisualStyleBackColor = true;
			this.button_SetlocalServer.Click += new EventHandler(this.button_SetlocalServer_Click);
			this.label3.Location = new System.Drawing.Point(233, 85);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 20);
			this.label3.TabIndex = 17;
			this.label3.Text = "(毫秒)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_NetID_localServer.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.textBox_NetID_localServer.Location = new System.Drawing.Point(354, 80);
			this.textBox_NetID_localServer.Name = "textBox_NetID_localServer";
			this.textBox_NetID_localServer.Size = new System.Drawing.Size(121, 23);
			this.textBox_NetID_localServer.TabIndex = 16;
			this.textBox_NetID_localServer.TextChanged += new EventHandler(this.textBox_Data_TextChanged);
			this.textBox_heartbeat_localServer.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.textBox_heartbeat_localServer.Location = new System.Drawing.Point(112, 84);
			this.textBox_heartbeat_localServer.Name = "textBox_heartbeat_localServer";
			this.textBox_heartbeat_localServer.Size = new System.Drawing.Size(116, 23);
			this.textBox_heartbeat_localServer.TabIndex = 15;
			this.textBox_heartbeat_localServer.TextChanged += new EventHandler(this.textBox_Data_TextChanged);
			this.textBox_heartbeat_localServer.KeyPress += new KeyPressEventHandler(this.textBox_NumberText_KeyPress);
			this.lblNetID.Location = new System.Drawing.Point(260, 83);
			this.lblNetID.Name = "lblNetID";
			this.lblNetID.Size = new System.Drawing.Size(90, 20);
			this.lblNetID.TabIndex = 14;
			this.lblNetID.Text = "网络ID";
			this.lblNetID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblHeartbeat.Location = new System.Drawing.Point(13, 84);
			this.lblHeartbeat.Name = "lblHeartbeat";
			this.lblHeartbeat.Size = new System.Drawing.Size(89, 21);
			this.lblHeartbeat.TabIndex = 13;
			this.lblHeartbeat.Text = "心跳时间";
			this.lblHeartbeat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_port_localServer.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.textBox_port_localServer.Location = new System.Drawing.Point(354, 34);
			this.textBox_port_localServer.Name = "textBox_port_localServer";
			this.textBox_port_localServer.Size = new System.Drawing.Size(121, 23);
			this.textBox_port_localServer.TabIndex = 12;
			this.textBox_port_localServer.TextChanged += new EventHandler(this.textBox_Data_TextChanged);
			this.textBox_port_localServer.KeyPress += new KeyPressEventHandler(this.textBox_NumberText_KeyPress);
			this.lblLocalServerIPPort.Location = new System.Drawing.Point(258, 34);
			this.lblLocalServerIPPort.Name = "lblLocalServerIPPort";
			this.lblLocalServerIPPort.Size = new System.Drawing.Size(92, 20);
			this.lblLocalServerIPPort.TabIndex = 6;
			this.lblLocalServerIPPort.Text = "服务器端口";
			this.lblLocalServerIPPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblLocalServerIPAddress.Location = new System.Drawing.Point(11, 35);
			this.lblLocalServerIPAddress.Name = "lblLocalServerIPAddress";
			this.lblLocalServerIPAddress.Size = new System.Drawing.Size(91, 21);
			this.lblLocalServerIPAddress.TabIndex = 5;
			this.lblLocalServerIPAddress.Text = "服务器IP地址";
			this.lblLocalServerIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.gbPassword.Controls.Add(this.button_SetPassword);
			this.gbPassword.Controls.Add(this.lblPassword);
			this.gbPassword.Controls.Add(this.textBox_Password);
			this.gbPassword.Location = new System.Drawing.Point(16, 120);
			this.gbPassword.Name = "gbPassword";
			this.gbPassword.Size = new System.Drawing.Size(513, 53);
			this.gbPassword.TabIndex = 23;
			this.gbPassword.TabStop = false;
			this.gbPassword.Text = "密码设置";
			this.button_SetPassword.Location = new System.Drawing.Point(400, 19);
			this.button_SetPassword.Name = "button_SetPassword";
			this.button_SetPassword.Size = new System.Drawing.Size(75, 23);
			this.button_SetPassword.TabIndex = 8;
			this.button_SetPassword.Text = "设置发送";
			this.button_SetPassword.UseVisualStyleBackColor = true;
			this.button_SetPassword.Click += new EventHandler(this.button_SetPassword_Click);
			this.lblPassword.Location = new System.Drawing.Point(8, 21);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(92, 21);
			this.lblPassword.TabIndex = 9;
			this.lblPassword.Text = "密码";
			this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_Password.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.textBox_Password.Location = new System.Drawing.Point(113, 21);
			this.textBox_Password.Name = "textBox_Password";
			this.textBox_Password.PasswordChar = '*';
			this.textBox_Password.Size = new System.Drawing.Size(116, 23);
			this.textBox_Password.TabIndex = 11;
			this.textBox_Password.TextChanged += new EventHandler(this.textBox_Password_TextChanged);
			this.checkBox_PasswordEnabled.AutoSize = true;
			this.checkBox_PasswordEnabled.Location = new System.Drawing.Point(10, 753);
			this.checkBox_PasswordEnabled.Name = "checkBox_PasswordEnabled";
			this.checkBox_PasswordEnabled.Size = new System.Drawing.Size(72, 16);
			this.checkBox_PasswordEnabled.TabIndex = 20;
			this.checkBox_PasswordEnabled.Text = "启用密码";
			this.checkBox_PasswordEnabled.UseVisualStyleBackColor = true;
			this.checkBox_PasswordEnabled.CheckedChanged += new EventHandler(this.checkBox_PasswordEnable_CheckedChanged);
			this.panel_FixedIPSet.Controls.Add(this.gbFixedIP);
			this.panel_FixedIPSet.Location = new System.Drawing.Point(10, 12);
			this.panel_FixedIPSet.Name = "panel_FixedIPSet";
			this.panel_FixedIPSet.Size = new System.Drawing.Size(528, 192);
			this.panel_FixedIPSet.TabIndex = 23;
			this.gbFixedIP.Controls.Add(this.chkDNS);
			this.gbFixedIP.Controls.Add(this.txtDNS);
			this.gbFixedIP.Controls.Add(this.lblDNS);
			this.gbFixedIP.Controls.Add(this.chkDHCPEnabled);
			this.gbFixedIP.Controls.Add(this.txtGateway);
			this.gbFixedIP.Controls.Add(this.txtIPMask);
			this.gbFixedIP.Controls.Add(this.txtIPAddress);
			this.gbFixedIP.Controls.Add(this.button_Set_Client);
			this.gbFixedIP.Controls.Add(this.lblGateway);
			this.gbFixedIP.Controls.Add(this.lblIPMask);
			this.gbFixedIP.Controls.Add(this.textBox_port_Client);
			this.gbFixedIP.Controls.Add(this.lblIPPort);
			this.gbFixedIP.Controls.Add(this.lblIPAddress);
			this.gbFixedIP.Location = new System.Drawing.Point(7, 12);
			this.gbFixedIP.Name = "gbFixedIP";
			this.gbFixedIP.Size = new System.Drawing.Size(513, 170);
			this.gbFixedIP.TabIndex = 20;
			this.gbFixedIP.TabStop = false;
			this.gbFixedIP.Text = "控制卡IP设置";
			this.chkDNS.AutoSize = true;
			this.chkDNS.Location = new System.Drawing.Point(232, 134);
			this.chkDNS.Name = "chkDNS";
			this.chkDNS.Size = new System.Drawing.Size(15, 14);
			this.chkDNS.TabIndex = 37;
			this.chkDNS.UseVisualStyleBackColor = true;
			this.chkDNS.CheckedChanged += new EventHandler(this.chkDNS_CheckedChanged);
			this.txtDNS.Location = new System.Drawing.Point(113, 130);
			this.txtDNS.Name = "txtDNS";
			this.txtDNS.Size = new System.Drawing.Size(116, 21);
			this.txtDNS.TabIndex = 36;
			this.txtDNS.TextChanged += new EventHandler(this.txtDNS_TextChanged);
			this.txtDNS.KeyPress += new KeyPressEventHandler(this.txtDNS_KeyPress);
			this.txtDNS.Leave += new EventHandler(this.txtDNS_Leave);
			this.lblDNS.Location = new System.Drawing.Point(12, 128);
			this.lblDNS.Name = "lblDNS";
			this.lblDNS.Size = new System.Drawing.Size(93, 23);
			this.lblDNS.TabIndex = 35;
			this.lblDNS.Text = "DNS服务器";
			this.lblDNS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkDHCPEnabled.AutoSize = true;
			this.chkDHCPEnabled.Location = new System.Drawing.Point(297, 133);
			this.chkDHCPEnabled.Name = "chkDHCPEnabled";
			this.chkDHCPEnabled.Size = new System.Drawing.Size(72, 16);
			this.chkDHCPEnabled.TabIndex = 34;
			this.chkDHCPEnabled.Text = "DHCP模式";
			this.chkDHCPEnabled.UseVisualStyleBackColor = true;
			this.chkDHCPEnabled.CheckedChanged += new EventHandler(this.chkDHCPEnabled_CheckedChanged);
			this.txtGateway.Location = new System.Drawing.Point(354, 78);
			this.txtGateway.Name = "txtGateway";
			this.txtGateway.Size = new System.Drawing.Size(121, 21);
			this.txtGateway.TabIndex = 33;
			this.txtGateway.TextChanged += new EventHandler(this.txtGateway_TextChanged);
			this.txtGateway.KeyPress += new KeyPressEventHandler(this.txtGateway_KeyPress);
			this.txtGateway.Leave += new EventHandler(this.txtGateway_Leave);
			this.txtIPMask.Location = new System.Drawing.Point(113, 81);
			this.txtIPMask.Name = "txtIPMask";
			this.txtIPMask.Size = new System.Drawing.Size(116, 21);
			this.txtIPMask.TabIndex = 32;
			this.txtIPMask.TextChanged += new EventHandler(this.txtIPMask_TextChanged);
			this.txtIPMask.KeyPress += new KeyPressEventHandler(this.txtIPMask_KeyPress);
			this.txtIPMask.Leave += new EventHandler(this.txtIPMask_Leave);
			this.txtIPAddress.Location = new System.Drawing.Point(112, 35);
			this.txtIPAddress.Name = "txtIPAddress";
			this.txtIPAddress.Size = new System.Drawing.Size(117, 21);
			this.txtIPAddress.TabIndex = 31;
			this.txtIPAddress.TextChanged += new EventHandler(this.txtIPAddress_TextChanged);
			this.txtIPAddress.KeyPress += new KeyPressEventHandler(this.txtIPAddress_KeyPress);
			this.txtIPAddress.Leave += new EventHandler(this.txtIPAddress_Leave);
			this.button_Set_Client.Location = new System.Drawing.Point(401, 128);
			this.button_Set_Client.Name = "button_Set_Client";
			this.button_Set_Client.Size = new System.Drawing.Size(75, 23);
			this.button_Set_Client.TabIndex = 8;
			this.button_Set_Client.Text = "设置发送";
			this.button_Set_Client.UseVisualStyleBackColor = true;
			this.button_Set_Client.Click += new EventHandler(this.button_Set_Client_Click);
			this.lblGateway.Location = new System.Drawing.Point(258, 77);
			this.lblGateway.Name = "lblGateway";
			this.lblGateway.Size = new System.Drawing.Size(92, 20);
			this.lblGateway.TabIndex = 14;
			this.lblGateway.Text = "默认网关";
			this.lblGateway.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblIPMask.Location = new System.Drawing.Point(9, 80);
			this.lblIPMask.Name = "lblIPMask";
			this.lblIPMask.Size = new System.Drawing.Size(93, 21);
			this.lblIPMask.TabIndex = 13;
			this.lblIPMask.Text = "子网掩码";
			this.lblIPMask.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_port_Client.BackColor = System.Drawing.SystemColors.Window;
			this.textBox_port_Client.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.textBox_port_Client.Location = new System.Drawing.Point(353, 35);
			this.textBox_port_Client.Name = "textBox_port_Client";
			this.textBox_port_Client.Size = new System.Drawing.Size(121, 23);
			this.textBox_port_Client.TabIndex = 12;
			this.textBox_port_Client.TextChanged += new EventHandler(this.textBox_Data_TextChanged);
			this.textBox_port_Client.KeyPress += new KeyPressEventHandler(this.textBox_port_Client_KeyPress);
			this.lblIPPort.Location = new System.Drawing.Point(256, 35);
			this.lblIPPort.Name = "lblIPPort";
			this.lblIPPort.Size = new System.Drawing.Size(91, 20);
			this.lblIPPort.TabIndex = 6;
			this.lblIPPort.Text = "端口";
			this.lblIPPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblIPAddress.Location = new System.Drawing.Point(9, 34);
			this.lblIPAddress.Name = "lblIPAddress";
			this.lblIPAddress.Size = new System.Drawing.Size(93, 21);
			this.lblIPAddress.TabIndex = 5;
			this.lblIPAddress.Text = "IP地址";
			this.lblIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.panel_Advanced.Controls.Add(this.gbPassword);
			this.panel_Advanced.Controls.Add(this.gbMAC);
			this.panel_Advanced.Location = new System.Drawing.Point(10, 494);
			this.panel_Advanced.Name = "panel_Advanced";
			this.panel_Advanced.Size = new System.Drawing.Size(528, 88);
			this.panel_Advanced.TabIndex = 23;
			this.gbMAC.Controls.Add(this.lblMacHint);
			this.gbMAC.Controls.Add(this.chkMac);
			this.gbMAC.Controls.Add(this.textBoxMAC);
			this.gbMAC.Controls.Add(this.button_Set_MAC);
			this.gbMAC.Controls.Add(this.label8);
			this.gbMAC.Location = new System.Drawing.Point(7, 10);
			this.gbMAC.Name = "gbMAC";
			this.gbMAC.Size = new System.Drawing.Size(513, 69);
			this.gbMAC.TabIndex = 21;
			this.gbMAC.TabStop = false;
			this.lblMacHint.AutoSize = true;
			this.lblMacHint.ForeColor = System.Drawing.Color.Red;
			this.lblMacHint.Location = new System.Drawing.Point(135, 1);
			this.lblMacHint.Name = "lblMacHint";
			this.lblMacHint.Size = new System.Drawing.Size(137, 12);
			this.lblMacHint.TabIndex = 15;
			this.lblMacHint.Text = "（非专业人士请勿改动）";
			this.chkMac.AutoSize = true;
			this.chkMac.Location = new System.Drawing.Point(9, 0);
			this.chkMac.Name = "chkMac";
			this.chkMac.Size = new System.Drawing.Size(126, 16);
			this.chkMac.TabIndex = 14;
			this.chkMac.Text = "控制卡MAC地址设置";
			this.chkMac.UseVisualStyleBackColor = true;
			this.chkMac.CheckedChanged += new EventHandler(this.chkMac_CheckedChanged);
			this.textBoxMAC.Location = new System.Drawing.Point(112, 34);
			this.textBoxMAC.MaxLength = 12;
			this.textBoxMAC.Name = "textBoxMAC";
			this.textBoxMAC.Size = new System.Drawing.Size(116, 21);
			this.textBoxMAC.TabIndex = 13;
			this.textBoxMAC.TextChanged += new EventHandler(this.textBoxMAC_TextChanged);
			this.button_Set_MAC.Location = new System.Drawing.Point(401, 32);
			this.button_Set_MAC.Name = "button_Set_MAC";
			this.button_Set_MAC.Size = new System.Drawing.Size(75, 23);
			this.button_Set_MAC.TabIndex = 8;
			this.button_Set_MAC.Text = "设置发送";
			this.button_Set_MAC.UseVisualStyleBackColor = true;
			this.button_Set_MAC.Click += new EventHandler(this.button_Set_MAC_Click);
			this.label8.Location = new System.Drawing.Point(7, 33);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(92, 21);
			this.label8.TabIndex = 9;
			this.label8.Text = "MAC地址";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.PnlCloudServer.Controls.Add(this.GrpCloudServer);
			this.PnlCloudServer.Location = new System.Drawing.Point(10, 390);
			this.PnlCloudServer.Name = "PnlCloudServer";
			this.PnlCloudServer.Size = new System.Drawing.Size(528, 98);
			this.PnlCloudServer.TabIndex = 23;
			this.GrpCloudServer.Controls.Add(this.txtCloudServerUserName);
			this.GrpCloudServer.Controls.Add(this.lblCloudServerUserName);
			this.GrpCloudServer.Controls.Add(this.txtCloudServerIPAddress);
			this.GrpCloudServer.Controls.Add(this.panel2);
			this.GrpCloudServer.Controls.Add(this.ChkCloudServer);
			this.GrpCloudServer.Controls.Add(this.panel1);
			this.GrpCloudServer.Controls.Add(this.LblCloudServerIP);
			this.GrpCloudServer.Controls.Add(this.TxtCloudServerDomainName);
			this.GrpCloudServer.Controls.Add(this.BtnSetCloudServer);
			this.GrpCloudServer.Controls.Add(this.label1);
			this.GrpCloudServer.Controls.Add(this.TxtCloudHeartBeatPort);
			this.GrpCloudServer.Controls.Add(this.TxtCloudHeartBeatTime);
			this.GrpCloudServer.Controls.Add(this.LblCloudHeartBeatPort);
			this.GrpCloudServer.Controls.Add(this.LblCloudHeartBeatTime);
			this.GrpCloudServer.Controls.Add(this.TxtCloudServerPort);
			this.GrpCloudServer.Controls.Add(this.LblCloudServerPort);
			this.GrpCloudServer.Controls.Add(this.LblCloudServerDomainName);
			this.GrpCloudServer.Location = new System.Drawing.Point(7, 10);
			this.GrpCloudServer.Name = "GrpCloudServer";
			this.GrpCloudServer.Size = new System.Drawing.Size(513, 81);
			this.GrpCloudServer.TabIndex = 2;
			this.GrpCloudServer.TabStop = false;
			this.txtCloudServerUserName.Location = new System.Drawing.Point(113, 35);
			this.txtCloudServerUserName.Name = "txtCloudServerUserName";
			this.txtCloudServerUserName.Size = new System.Drawing.Size(116, 21);
			this.txtCloudServerUserName.TabIndex = 31;
			this.txtCloudServerUserName.KeyPress += new KeyPressEventHandler(this.txtCloudServerUserName_KeyPress);
			this.lblCloudServerUserName.Location = new System.Drawing.Point(10, 34);
			this.lblCloudServerUserName.Name = "lblCloudServerUserName";
			this.lblCloudServerUserName.Size = new System.Drawing.Size(89, 21);
			this.lblCloudServerUserName.TabIndex = 30;
			this.lblCloudServerUserName.Text = "用户名";
			this.lblCloudServerUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtCloudServerIPAddress.Location = new System.Drawing.Point(112, 108);
			this.txtCloudServerIPAddress.Name = "txtCloudServerIPAddress";
			this.txtCloudServerIPAddress.Size = new System.Drawing.Size(117, 21);
			this.txtCloudServerIPAddress.TabIndex = 29;
			this.txtCloudServerIPAddress.TextChanged += new EventHandler(this.txtCloudServerIPAddress_TextChanged);
			this.txtCloudServerIPAddress.KeyPress += new KeyPressEventHandler(this.txtCloudServerIPAddress_KeyPress);
			this.txtCloudServerIPAddress.Leave += new EventHandler(this.txtCloudServerIPAddress_Leave);
			this.panel2.Controls.Add(this.RdoCloudUdp);
			this.panel2.Controls.Add(this.RdoCloudTcp);
			this.panel2.Controls.Add(this.LblCloudServerCommMode);
			this.panel2.Location = new System.Drawing.Point(9, 108);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(498, 25);
			this.panel2.TabIndex = 28;
			this.RdoCloudUdp.AutoSize = true;
			this.RdoCloudUdp.Location = new System.Drawing.Point(158, 5);
			this.RdoCloudUdp.Name = "RdoCloudUdp";
			this.RdoCloudUdp.Size = new System.Drawing.Size(65, 16);
			this.RdoCloudUdp.TabIndex = 24;
			this.RdoCloudUdp.TabStop = true;
			this.RdoCloudUdp.Text = "UDP通信";
			this.RdoCloudUdp.UseVisualStyleBackColor = true;
			this.RdoCloudTcp.AutoSize = true;
			this.RdoCloudTcp.Location = new System.Drawing.Point(348, 5);
			this.RdoCloudTcp.Name = "RdoCloudTcp";
			this.RdoCloudTcp.Size = new System.Drawing.Size(65, 16);
			this.RdoCloudTcp.TabIndex = 23;
			this.RdoCloudTcp.TabStop = true;
			this.RdoCloudTcp.Text = "TCP通信";
			this.RdoCloudTcp.UseVisualStyleBackColor = true;
			this.LblCloudServerCommMode.Location = new System.Drawing.Point(5, 3);
			this.LblCloudServerCommMode.Name = "LblCloudServerCommMode";
			this.LblCloudServerCommMode.Size = new System.Drawing.Size(91, 21);
			this.LblCloudServerCommMode.TabIndex = 26;
			this.LblCloudServerCommMode.Text = "通信模式";
			this.LblCloudServerCommMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ChkCloudServer.AutoSize = true;
			this.ChkCloudServer.Location = new System.Drawing.Point(9, 0);
			this.ChkCloudServer.Name = "ChkCloudServer";
			this.ChkCloudServer.Size = new System.Drawing.Size(96, 16);
			this.ChkCloudServer.TabIndex = 18;
			this.ChkCloudServer.Text = "云服务器设置";
			this.ChkCloudServer.UseVisualStyleBackColor = true;
			this.ChkCloudServer.CheckedChanged += new EventHandler(this.ChkCloudServer_CheckedChanged);
			this.panel1.Controls.Add(this.RdoCloudDomain);
			this.panel1.Controls.Add(this.RdoCloudIP);
			this.panel1.Controls.Add(this.LblCloudServerAccessMode);
			this.panel1.Location = new System.Drawing.Point(9, 146);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(498, 25);
			this.panel1.TabIndex = 27;
			this.RdoCloudDomain.AutoSize = true;
			this.RdoCloudDomain.Location = new System.Drawing.Point(347, 5);
			this.RdoCloudDomain.Name = "RdoCloudDomain";
			this.RdoCloudDomain.Size = new System.Drawing.Size(83, 16);
			this.RdoCloudDomain.TabIndex = 20;
			this.RdoCloudDomain.TabStop = true;
			this.RdoCloudDomain.Text = "服务器域名";
			this.RdoCloudDomain.UseVisualStyleBackColor = true;
			this.RdoCloudDomain.CheckedChanged += new EventHandler(this.RdoCloudDomain_CheckedChanged);
			this.RdoCloudIP.AutoSize = true;
			this.RdoCloudIP.Location = new System.Drawing.Point(158, 5);
			this.RdoCloudIP.Name = "RdoCloudIP";
			this.RdoCloudIP.Size = new System.Drawing.Size(71, 16);
			this.RdoCloudIP.TabIndex = 19;
			this.RdoCloudIP.TabStop = true;
			this.RdoCloudIP.Text = "服务器IP";
			this.RdoCloudIP.UseVisualStyleBackColor = true;
			this.RdoCloudIP.CheckedChanged += new EventHandler(this.RdoCloudIP_CheckedChanged);
			this.LblCloudServerAccessMode.Location = new System.Drawing.Point(5, 3);
			this.LblCloudServerAccessMode.Name = "LblCloudServerAccessMode";
			this.LblCloudServerAccessMode.Size = new System.Drawing.Size(91, 21);
			this.LblCloudServerAccessMode.TabIndex = 25;
			this.LblCloudServerAccessMode.Text = "访问方式";
			this.LblCloudServerAccessMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblCloudServerIP.Location = new System.Drawing.Point(11, 108);
			this.LblCloudServerIP.Name = "LblCloudServerIP";
			this.LblCloudServerIP.Size = new System.Drawing.Size(91, 21);
			this.LblCloudServerIP.TabIndex = 22;
			this.LblCloudServerIP.Text = "服务器IP";
			this.LblCloudServerIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblCloudServerIP.Visible = false;
			this.TxtCloudServerDomainName.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.TxtCloudServerDomainName.Location = new System.Drawing.Point(113, 107);
			this.TxtCloudServerDomainName.Name = "TxtCloudServerDomainName";
			this.TxtCloudServerDomainName.Size = new System.Drawing.Size(116, 23);
			this.TxtCloudServerDomainName.TabIndex = 18;
			this.TxtCloudServerDomainName.Visible = false;
			this.TxtCloudServerDomainName.TextChanged += new EventHandler(this.textBox_Data_TextChanged);
			this.BtnSetCloudServer.Location = new System.Drawing.Point(401, 34);
			this.BtnSetCloudServer.Name = "BtnSetCloudServer";
			this.BtnSetCloudServer.Size = new System.Drawing.Size(75, 23);
			this.BtnSetCloudServer.TabIndex = 8;
			this.BtnSetCloudServer.Text = "设置发送";
			this.BtnSetCloudServer.UseVisualStyleBackColor = true;
			this.BtnSetCloudServer.Click += new EventHandler(this.BtnSetCloudServer_Click);
			this.label1.Location = new System.Drawing.Point(233, 85);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 20);
			this.label1.TabIndex = 17;
			this.label1.Text = "秒";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TxtCloudHeartBeatPort.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.TxtCloudHeartBeatPort.Location = new System.Drawing.Point(354, 150);
			this.TxtCloudHeartBeatPort.Name = "TxtCloudHeartBeatPort";
			this.TxtCloudHeartBeatPort.Size = new System.Drawing.Size(121, 23);
			this.TxtCloudHeartBeatPort.TabIndex = 16;
			this.TxtCloudHeartBeatPort.TextChanged += new EventHandler(this.textBox_Data_TextChanged);
			this.TxtCloudHeartBeatPort.KeyPress += new KeyPressEventHandler(this.textBox_NumberText_KeyPress);
			this.TxtCloudHeartBeatTime.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.TxtCloudHeartBeatTime.Location = new System.Drawing.Point(113, 84);
			this.TxtCloudHeartBeatTime.Name = "TxtCloudHeartBeatTime";
			this.TxtCloudHeartBeatTime.Size = new System.Drawing.Size(116, 21);
			this.TxtCloudHeartBeatTime.TabIndex = 15;
			this.TxtCloudHeartBeatTime.TextChanged += new EventHandler(this.textBox_Data_TextChanged);
			this.TxtCloudHeartBeatTime.KeyPress += new KeyPressEventHandler(this.textBox_NumberText_KeyPress);
			this.LblCloudHeartBeatPort.Location = new System.Drawing.Point(260, 150);
			this.LblCloudHeartBeatPort.Name = "LblCloudHeartBeatPort";
			this.LblCloudHeartBeatPort.Size = new System.Drawing.Size(90, 20);
			this.LblCloudHeartBeatPort.TabIndex = 14;
			this.LblCloudHeartBeatPort.Text = "心跳端口";
			this.LblCloudHeartBeatPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblCloudHeartBeatTime.Location = new System.Drawing.Point(13, 84);
			this.LblCloudHeartBeatTime.Name = "LblCloudHeartBeatTime";
			this.LblCloudHeartBeatTime.Size = new System.Drawing.Size(89, 21);
			this.LblCloudHeartBeatTime.TabIndex = 13;
			this.LblCloudHeartBeatTime.Text = "心跳时间";
			this.LblCloudHeartBeatTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.TxtCloudServerPort.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.TxtCloudServerPort.Location = new System.Drawing.Point(354, 108);
			this.TxtCloudServerPort.Name = "TxtCloudServerPort";
			this.TxtCloudServerPort.Size = new System.Drawing.Size(121, 23);
			this.TxtCloudServerPort.TabIndex = 12;
			this.TxtCloudServerPort.TextChanged += new EventHandler(this.textBox_Data_TextChanged);
			this.TxtCloudServerPort.KeyPress += new KeyPressEventHandler(this.textBox_NumberText_KeyPress);
			this.LblCloudServerPort.Location = new System.Drawing.Point(254, 108);
			this.LblCloudServerPort.Name = "LblCloudServerPort";
			this.LblCloudServerPort.Size = new System.Drawing.Size(92, 20);
			this.LblCloudServerPort.TabIndex = 6;
			this.LblCloudServerPort.Text = "服务器端口";
			this.LblCloudServerPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblCloudServerDomainName.Location = new System.Drawing.Point(7, 109);
			this.LblCloudServerDomainName.Name = "LblCloudServerDomainName";
			this.LblCloudServerDomainName.Size = new System.Drawing.Size(91, 21);
			this.LblCloudServerDomainName.TabIndex = 5;
			this.LblCloudServerDomainName.Text = "服务器域名";
			this.LblCloudServerDomainName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblCloudServerDomainName.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(548, 594);
			base.Controls.Add(this.PnlCloudServer);
			base.Controls.Add(this.panel_Advanced);
			base.Controls.Add(this.checkBox_PasswordEnabled);
			base.Controls.Add(this.panel_FixedIPSet);
			base.Controls.Add(this.panel_LocalServerSet);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "formAdvancedSettings";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "控制卡网络设置";
			base.FormClosing += new FormClosingEventHandler(this.formAdvancedSettings_FormClosing);
			base.Load += new EventHandler(this.formAdvancedSettings_Load);
			this.panel_LocalServerSet.ResumeLayout(false);
			this.gbLocalServer.ResumeLayout(false);
			this.gbLocalServer.PerformLayout();
			this.gbPassword.ResumeLayout(false);
			this.gbPassword.PerformLayout();
			this.panel_FixedIPSet.ResumeLayout(false);
			this.gbFixedIP.ResumeLayout(false);
			this.gbFixedIP.PerformLayout();
			this.panel_Advanced.ResumeLayout(false);
			this.gbMAC.ResumeLayout(false);
			this.gbMAC.PerformLayout();
			this.PnlCloudServer.ResumeLayout(false);
			this.GrpCloudServer.ResumeLayout(false);
			this.GrpCloudServer.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
