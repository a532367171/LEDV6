using HelloRemoting;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Enum;
using LedModel.Foundation;
using server_interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class Form_find_device : Form
	{
		private UI_Interface IServer;

		private formMain fm;

		private Thread thrSearch;

		private bool isRestarting;

		private bool isContainsNetworkCard;

		private static string formID = "Form_find_device";

		private IList<DEVICE_INFO> deviceList;

		private IList<DEVICE_INFO> deviceServerList;

		private IList<LedPanel> panelList;

		private Dictionary<string, int> deviceResendDict;

		private bool isThread;

		private int findCount;

		private int failureCount;

		private string summaryMessage;

		private int processBar_hold_jsq;

		private IContainer components;

		private Button button_start_find;

		private Button button_END_find;

		private Button button_OK;

		private Button button_Cancel;

		private StatusStrip STBAR;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private ToolStripStatusLabel label_msg;

		private ToolStripProgressBar processBar;

		private ToolStripStatusLabel toolStripStatusLabel2;

		private Label label_ip_tip;

		private Label lblIPPort;

		private TextBox textBox_port;

		private ImageList imgDeviceState;

		private System.Windows.Forms.Timer timer_processBar_draw;

		private MaskedTextBox mtxIPAddress;

		private DataGridView dgvDevice;

		private DataGridViewImageColumn dataGridViewImageColumn1;

		private DataGridViewImageColumn ColumnState;

		private DataGridViewCheckBoxColumn ColumnSelect;

		private DataGridViewTextBoxColumn ColumnSN;

		private DataGridViewComboBoxColumn ColumnGroup;

		private DataGridViewTextBoxColumn ColumnCardType;

		private DataGridViewTextBoxColumn ColumnArea;

		private DataGridViewTextBoxColumn ColumnOE;

		private DataGridViewTextBoxColumn ColumnData;

		private DataGridViewTextBoxColumn ColumnScan;

		private DataGridViewTextBoxColumn ColumnCommMode;

		private DataGridViewTextBoxColumn ColumnCardAddress;

		private DataGridViewTextBoxColumn ColumnIPAddress;

		private DataGridViewTextBoxColumn ColumnMACAddress;

		private DataGridViewTextBoxColumn ColumnNetID;

		private DataGridViewTextBoxColumn ColumnID;

		private Label LblWarning;

		public static string FormID
		{
			get
			{
				return Form_find_device.formID;
			}
			set
			{
				Form_find_device.formID = value;
			}
		}

		public Form_find_device()
		{
			this.InitializeComponent();
			this.Diplay_lanuage_Text();
		}

		public void Diplay_lanuage_Text()
		{
			this.Text = formMain.ML.GetStr("Form_find_device_FormText");
			this.button_start_find.Text = formMain.ML.GetStr("Form_find_device_button_start_find");
			this.lblIPPort.Text = formMain.ML.GetStr("Form_find_device_label_IPPort");
			this.button_END_find.Text = formMain.ML.GetStr("Form_find_device_button_END_find");
			this.button_OK.Text = formMain.ML.GetStr("Form_find_device_button_OK");
			this.button_Cancel.Text = formMain.ML.GetStr("Form_find_device_button_Cancel");
			this.toolStripStatusLabel1.Text = formMain.ML.GetStr("Form_find_device_toolStripStatusLabel_PromptMessage");
			this.label_msg.Text = formMain.ML.GetStr("Form_find_device_label_message");
			this.LblWarning.Text = formMain.ML.GetStr("Form_find_device_Message_Prompt_NotSupport");
			this.dgvDevice.Columns[0].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_Status");
			this.dgvDevice.Columns[1].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_Select");
			this.dgvDevice.Columns[2].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_SerialNumber");
			this.dgvDevice.Columns[3].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_Group");
			this.dgvDevice.Columns[4].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_Model");
			this.dgvDevice.Columns[5].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_Area");
			this.dgvDevice.Columns[6].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_OE");
			this.dgvDevice.Columns[7].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_Data");
			this.dgvDevice.Columns[8].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_Scanning");
			this.dgvDevice.Columns[9].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_CommunicationMethod");
			this.dgvDevice.Columns[10].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_Address");
			this.dgvDevice.Columns[11].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_IPAddress");
			this.dgvDevice.Columns[12].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_MACAddress");
			this.dgvDevice.Columns[13].HeaderText = formMain.ML.GetStr("Form_find_device_DataGridView_dgvDevice_NetworkID");
		}

		public Form_find_device(UI_Interface _IServer, formMain fmain)
		{
			this.InitializeComponent();
			this.Diplay_lanuage_Text();
			this.IServer = _IServer;
			this.fm = fmain;
			call.OnDeviceCmdReturnResult += new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
			this.imgDeviceState.Images.Add(Resources.Led_Off);
			this.imgDeviceState.Images.Add(Resources.Led_On_Difference);
			this.imgDeviceState.Images.Add(Resources.Led_On);
			this.imgDeviceState.Images.Add(Resources.Led_On_New);
			if (this.dgvDevice.Columns[3].GetType() == typeof(DataGridViewComboBoxColumn))
			{
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)this.dgvDevice.Columns[3];
				dataGridViewComboBoxColumn.Items.Clear();
				LedProject ledsys = formMain.ledsys;
				if (ledsys != null && ledsys.Groups != null)
				{
					foreach (LedGroup current in ledsys.Groups)
					{
						dataGridViewComboBoxColumn.Items.Add(current.Name);
					}
				}
			}
			this.isContainsNetworkCard = false;
		}

		private void Form_find_device_Load(object sender, EventArgs e)
		{
			base.Icon = Resources.AppIcon;
			this.label_ip_tip.Visible = this.isContainsNetworkCard;
			this.mtxIPAddress.Visible = this.isContainsNetworkCard;
			this.lblIPPort.Visible = this.isContainsNetworkCard;
			this.textBox_port.Visible = this.isContainsNetworkCard;
			if (this.isContainsNetworkCard)
			{
				this.load_di_to_list();
			}
			this.load_panel_to_datagridview();
			this.button_start_find_Click(null, null);
		}

		private void Form_find_device_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.button_start_find.Enabled)
			{
				this.IServer.Cancel_prev_long_cmd();
			}
			this.isThread = false;
			call.OnDeviceCmdReturnResult -= new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
			this.timer_processBar_draw.Stop();
		}

		private void load_di_to_list()
		{
			this.deviceServerList = new List<DEVICE_INFO>();
			LedProject ledsys = formMain.ledsys;
			if (ledsys == null || ledsys.Panels == null)
			{
				return;
			}
			if (call.m_devList == null || call.m_devList.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<string, DEVICE_INFO> current in call.m_devList)
			{
				if (current.Value.cur_use_net_mode == 2 && (byte)(current.Value.state & DEVICE_STATE.ST_ONLINE) == 1)
				{
					this.deviceServerList.Add(current.Value);
				}
			}
		}

		private void load_panel_to_datagridview()
		{
			this.dgvDevice.Rows.Clear();
			LedProject ledsys = formMain.ledsys;
			if (ledsys == null || ledsys.Panels == null)
			{
				return;
			}
			int num = 0;
			foreach (LedPanel current in ledsys.Panels)
			{
				if (current.PortType != LedPortType.GPRS && (current.PortType != LedPortType.Ethernet || this.isContainsNetworkCard) && current.PortType != LedPortType.USB)
				{
					num++;
					this.dgvDevice.Rows.Add(this.pl_to_dv(num, current, false));
				}
			}
		}

		private void load_panel_to_datagridview(LedPanel panel)
		{
			LedProject ledsys = formMain.ledsys;
			if (ledsys == null || ledsys.Panels == null)
			{
				return;
			}
			if (panel == null)
			{
				return;
			}
			int num = 0;
			int num2 = 3;
			bool flag = false;
			foreach (LedPanel current in ledsys.Panels)
			{
				if (current.PortType == LedPortType.SerialPort)
				{
					flag = true;
				}
				if (current.PortType == LedPortType.Ethernet || current.PortType == LedPortType.SerialPort)
				{
					num2 = current.CompareTo(panel);
					if (num2 < 3)
					{
						panel.ID = current.ID;
						if (panel.PortType != LedPortType.Ethernet)
						{
							panel.ProductID = current.ProductID;
							break;
						}
						break;
					}
					else
					{
						num++;
					}
				}
			}
			bool flag2 = false;
			if (num2 == 3)
			{
				DataGridViewRow dataGridViewRow = this.pl_to_dv(this.dgvDevice.Rows.Count + 1, panel, true);
				foreach (DataGridViewRow dataGridViewRow2 in ((IEnumerable)this.dgvDevice.Rows))
				{
					if ((int)dataGridViewRow2.Cells[0].Tag == 3)
					{
						bool flag3 = true;
						for (int i = 4; i < dataGridViewRow2.Cells.Count - 1; i++)
						{
							if (dataGridViewRow2.Cells[i].Value.ToString() != dataGridViewRow.Cells[i].Value.ToString())
							{
								flag3 = false;
							}
						}
						if (flag3)
						{
							dataGridViewRow2.Cells[14].Value = dataGridViewRow.Cells[14].Value;
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					this.dgvDevice.Rows.Add(dataGridViewRow);
					num = this.dgvDevice.Rows.Count - 1;
				}
			}
			if (num < this.dgvDevice.Rows.Count && !flag2)
			{
				DataGridViewCell dataGridViewCell = this.dgvDevice.Rows[num].Cells[0];
				dataGridViewCell.Value = this.imgDeviceState.Images[num2];
				dataGridViewCell.Tag = num2;
				if (num2 == 1)
				{
					dataGridViewCell.ToolTipText = formMain.ML.GetStr("Form_find_device_message_result_foundButDifferent");
					if (flag)
					{
						DataGridViewCell dataGridViewCell2 = this.dgvDevice.Rows[num].Cells[9];
						dataGridViewCell2.Value = panel.SerialPortName;
					}
				}
				else if (num2 == 2)
				{
					dataGridViewCell.ToolTipText = formMain.ML.GetStr("Form_find_device_message_result_found");
					if (flag)
					{
						DataGridViewCell dataGridViewCell3 = this.dgvDevice.Rows[num].Cells[9];
						dataGridViewCell3.Value = panel.SerialPortName;
					}
				}
				else
				{
					dataGridViewCell.ToolTipText = formMain.ML.GetStr("Form_find_device_message_result_New");
				}
				dataGridViewCell = this.dgvDevice.Rows[num].Cells[13];
				dataGridViewCell.ToolTipText = this.GetDetailCommInfo(panel);
			}
			this.dgvDevice.Refresh();
		}

		private DataGridViewRow pl_to_dv(int num, LedPanel panel, bool isNew = false)
		{
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			dataGridViewRow.CreateCells(this.dgvDevice);
			dataGridViewRow.Cells[0].Value = this.imgDeviceState.Images[0];
			dataGridViewRow.Cells[0].Tag = 0;
			dataGridViewRow.Cells[0].ToolTipText = formMain.ML.GetStr("Form_find_device_message_result_NotFound");
			dataGridViewRow.Cells[1].Value = true;
			dataGridViewRow.Cells[2].Value = num.ToString();
			((DataGridViewComboBoxCell)dataGridViewRow.Cells[3]).Value = panel.Group;
			((DataGridViewComboBoxCell)dataGridViewRow.Cells[3]).ReadOnly = !isNew;
			dataGridViewRow.Cells[4].Value = panel.CardType.ToString().Replace("_", "-");
			dataGridViewRow.Cells[5].Value = panel.Width.ToString() + " x " + panel.Height.ToString();
			dataGridViewRow.Cells[6].Value = panel.OEPolarity.ToString();
			dataGridViewRow.Cells[7].Value = panel.DataPolarity.ToString();
			string text = string.Empty;
			LedScanType scanType = (LedScanType)panel.RoutingSetting.ScanType;
			if (scanType != LedScanType.Scan4)
			{
				if (scanType != LedScanType.Scan8)
				{
					if (scanType == LedScanType.Scan16)
					{
						text = "1/16";
					}
				}
				else
				{
					text = "1/8";
				}
			}
			else
			{
				text = "1/4";
			}
			text += this.GetColorModeString(panel.ColorMode);
			dataGridViewRow.Cells[8].Value = text;
			string value = string.Empty;
			if (panel.PortType == LedPortType.WiFi)
			{
				value = panel.PortType.ToString();
			}
			else if (panel.PortType == LedPortType.SerialPort)
			{
				value = panel.SerialPortName;
			}
			else if (panel.PortType == LedPortType.Ethernet)
			{
				if (panel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.Directly)
				{
					value = formMain.ML.GetStr("Form_find_device_message_commMode_aloneDirect");
				}
				else if (panel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.FixedIP)
				{
					value = formMain.ML.GetStr("Form_find_device_message_commMode_fixedIP");
				}
				else
				{
					value = formMain.ML.GetStr("Form_find_device_message_commMode_localServer");
				}
			}
			dataGridViewRow.Cells[9].Value = value;
			dataGridViewRow.Cells[10].Value = panel.CardAddress.ToString();
			dataGridViewRow.Cells[11].Value = panel.IPAddress;
			dataGridViewRow.Cells[12].Value = panel.MACAddress;
			dataGridViewRow.Cells[13].Value = panel.NetworkID;
			dataGridViewRow.Cells[13].ToolTipText = this.GetDetailCommInfo(panel);
			dataGridViewRow.Cells[14].Value = panel.ID;
			return dataGridViewRow;
		}

		private string message_code_ToString(int code_num, ushort cmd_code)
		{
			if (code_num == 11521 || code_num == 11522 || code_num == 11765 || code_num == 62209 || code_num == 62211)
			{
				return formMain.ML.GetStr("Communication_message_ercode_message_" + code_num.ToString());
			}
			if (((int)cmd_code << 8) + 1 == code_num)
			{
				return formMain.ML.GetStr("Communication_message_ercode_message_cmd01");
			}
			if (((int)cmd_code << 8) + 3 == code_num)
			{
				return formMain.ML.GetStr("Communication_message_ercode_message_cmd03");
			}
			if (((int)cmd_code << 8) + 4 == code_num)
			{
				return formMain.ML.GetStr("Communication_message_ercode_message_cmd05");
			}
			return formMain.ML.GetStr("Communication_message_ercode_message_unknown");
		}

		private string GetDetailCommInfo(LedPanel panel)
		{
			string text = string.Empty;
			string str = formMain.ML.GetStr("Form_find_device_ToolTipText_passwordMode_open");
			if (panel.AuthorityPasswordMode == LedAuthorityPasswordMode.Enabled)
			{
				str = formMain.ML.GetStr("Form_find_device_ToolTipText_passwordMode_close");
			}
			string str2 = formMain.ML.GetStr("Form_find_device_ToolTipText_localServerMode_open");
			if (panel.LocalServerMode == LedLocalServerMode.Enabled)
			{
				str2 = formMain.ML.GetStr("Form_find_device_ToolTipText_localServerMode_close");
			}
			text = text + formMain.ML.GetStr("Form_find_device_ToolTipText_PasswordMode") + str + "\r\n";
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				formMain.ML.GetStr("Form_find_device_ToolTipText_FixedIPPort"),
				panel.IPPort,
				"\r\n"
			});
			text = text + formMain.ML.GetStr("Form_find_device_ToolTipText_FixedIPSubnetMask") + panel.IPNetMask + "\r\n";
			text = text + formMain.ML.GetStr("Form_find_device_ToolTipText_FixedIPDefaultGateway") + panel.IPGateway + "\r\n";
			text = text + formMain.ML.GetStr("Form_find_device_ToolTipText_LocalServerMode") + str2 + "\r\n";
			text = text + formMain.ML.GetStr("Form_find_device_ToolTipText_LocalServerIPAddress") + panel.LocalServerIPAddress + "\r\n";
			object obj2 = text;
			text = string.Concat(new object[]
			{
				obj2,
				formMain.ML.GetStr("Form_find_device_ToolTipText_LocalServerIPPort"),
				panel.LocalServerIPPort,
				"\r\n"
			});
			object obj3 = text;
			return string.Concat(new object[]
			{
				obj3,
				formMain.ML.GetStr("Form_find_device_ToolTipText_LocalServerHeartbeatTime"),
				panel.LocalServerHeartbeatTime,
				"\r\n"
			});
		}

		private void OnDeviceCmdReturn(object sender, DeviceCmdEventArgs arg)
		{
			IPC_SIMPLE_ANSWER isa = arg.isa;
			if (isa.cmd_id == 45 || isa.cmd_id == 4339)
			{
				if (!isa.is_cmd_over_flag)
				{
					if (!isa.is_cmd_failed_flag)
					{
						if (isa.return_object != null && isa.return_object.GetType() == typeof(DEVICE_INFO))
						{
							this.add_di_to_lst((DEVICE_INFO)isa.return_object);
						}
					}
					else
					{
						string text = this.message_code_ToString(isa.er_code, isa.cmd_id);
						this.summaryMessage = this.summaryMessage + "●  " + text + "\r\n";
						this.label_msg.Text = text;
					}
				}
				else if (!isa.is_cmd_failed_flag)
				{
					if (isa.return_object != null && isa.return_object.GetType() == typeof(DEVICE_INFO))
					{
						this.add_di_to_lst((DEVICE_INFO)isa.return_object);
					}
					if (this.deviceList.Count > 0)
					{
						Thread thread = new Thread(new ThreadStart(this.ReadParm));
						thread.Start();
					}
					else
					{
						this.button_END_find.Enabled = false;
						this.button_start_find.Enabled = true;
						this.button_OK.Enabled = true;
						this.label_msg.Text = formMain.ML.GetStr("Form_find_device_message_label_msg_findDevice_complete");
						this.processBar.Value = this.processBar.Maximum;
						this.summaryMessage = formMain.ML.GetStr("Form_find_device_message_findDevice_number_0") + this.summaryMessage;
						MessageBox.Show(this.summaryMessage);
					}
				}
				else if (isa.cmd_id == 4339)
				{
					this.button_END_find.Enabled = false;
					this.button_start_find.Enabled = true;
					this.button_OK.Enabled = true;
					this.label_msg.Text = formMain.ML.GetStr("Form_find_device_message_label_msg_findDevice_complete");
					this.processBar.Value = this.processBar.Maximum;
					this.summaryMessage = this.summaryMessage + "●  " + this.message_code_ToString(isa.er_code, isa.cmd_id);
					this.summaryMessage = formMain.ML.GetStr("Form_find_device_message_findDevice_number_0") + this.summaryMessage;
					MessageBox.Show(this.summaryMessage);
				}
				else
				{
					this.summaryMessage = this.summaryMessage + "●  " + this.message_code_ToString(isa.er_code, isa.cmd_id);
					this.label_msg.Text = this.message_code_ToString(isa.er_code, isa.cmd_id);
				}
			}
			if (isa.cmd_id == 18 && isa.is_cmd_over_flag)
			{
				if (!isa.is_cmd_failed_flag)
				{
					if (isa.return_object != null && isa.return_object.GetType() == typeof(LedPanel))
					{
						LedPanel ledPanel = (LedPanel)isa.return_object;
						ledPanel.ID = LedCommon.GetDateTimeNow();
						ledPanel.ProductID = ledPanel.ID;
						foreach (LedPanel current in this.panelList)
						{
							if (current.ID == ledPanel.ID)
							{
								Thread.Sleep(10);
								ledPanel.ID = LedCommon.GetDateTimeNow();
								ledPanel.ProductID = ledPanel.ID;
								break;
							}
						}
						this.di_comm_to_pl(ledPanel, isa.product_id);
						this.panelList.Add(ledPanel);
						if (this.deviceResendDict != null && this.deviceResendDict.Count > 0 && this.deviceResendDict.ContainsKey(isa.product_id))
						{
							this.deviceResendDict[isa.product_id] = 0;
						}
						if (ledPanel.PortType != LedPortType.Ethernet)
						{
							this.load_panel_to_datagridview(ledPanel);
							this.findCount++;
						}
						else if (ledPanel.ProtocolVersion != 49)
						{
							this.ReadDeviceParm(isa.product_id);
						}
					}
				}
				else if (this.deviceResendDict != null && this.deviceResendDict.Count > 0)
				{
					int num;
					bool flag = this.deviceResendDict.TryGetValue(isa.product_id, out num);
					if (flag && num < 3)
					{
						Dictionary<string, int> dictionary;
						string product_id;
						(dictionary = this.deviceResendDict)[product_id = isa.product_id] = dictionary[product_id] + 1;
						this.ReadPanelParm(isa.product_id);
					}
					else
					{
						this.failureCount++;
					}
				}
				else
				{
					this.failureCount++;
				}
			}
			if (isa.cmd_id == 23 && isa.is_cmd_over_flag)
			{
				if (!isa.is_cmd_failed_flag)
				{
					if (isa.return_object != null && isa.return_object.GetType() == typeof(LedCardCommunication))
					{
						LedCardCommunication cardCommunication = (LedCardCommunication)isa.return_object;
						LedPanel panel = null;
						foreach (LedPanel current2 in this.panelList)
						{
							if (isa.product_id == current2.ProductID)
							{
								current2.Copy(cardCommunication, true);
								panel = current2;
								break;
							}
						}
						this.di_comm_to_pl(panel, isa.product_id);
						this.load_panel_to_datagridview(panel);
						this.findCount++;
					}
				}
				else if (this.deviceResendDict != null && this.deviceResendDict.Count > 0)
				{
					int num2;
					bool flag2 = this.deviceResendDict.TryGetValue(isa.product_id, out num2);
					if (flag2 && num2 < 4)
					{
						Dictionary<string, int> dictionary2;
						string product_id2;
						(dictionary2 = this.deviceResendDict)[product_id2 = isa.product_id] = dictionary2[product_id2] + 1;
						this.ReadDeviceParm(isa.product_id);
					}
					else
					{
						this.failureCount++;
					}
				}
				else
				{
					this.failureCount++;
				}
			}
			if (this.deviceList.Count > 0 && this.deviceList.Count == this.findCount + this.failureCount)
			{
				this.button_END_find.Enabled = false;
				this.button_start_find.Enabled = true;
				this.button_OK.Enabled = true;
				this.processBar.Value = this.processBar.Maximum;
				this.label_msg.Text = formMain.ML.GetStr("Form_find_device_message_label_msg_findDevice_complete");
				this.summaryMessage = string.Concat(new object[]
				{
					formMain.ML.GetStr("Form_find_device_message_findDevice_number"),
					this.findCount,
					"\r\n",
					this.summaryMessage
				});
				MessageBox.Show(this, this.summaryMessage);
			}
		}

		private void add_di_to_lst(DEVICE_INFO di)
		{
			bool flag = false;
			foreach (DEVICE_INFO current in this.deviceList)
			{
				if (current.product_id == di.product_id)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.deviceList.Add(di);
			}
		}

		private void di_comm_to_pl(LedPanel panel, string pid)
		{
			if (panel == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(pid))
			{
				return;
			}
			foreach (DEVICE_INFO current in this.deviceList)
			{
				if (current.product_id == pid)
				{
					if (current.cur_use_net_mode == 2)
					{
						panel.PortType = LedPortType.Ethernet;
						panel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.LocalServer;
						panel.ProductID = pid;
					}
					else if (current.cur_use_net_mode == 1)
					{
						if ((byte)(current.dev_net_mode & DEV_NET_MODE.MODE_TCP_LOCALE_SERVER) == 2)
						{
							panel.PortType = LedPortType.Ethernet;
							panel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.LocalServer;
							panel.ProductID = pid;
							panel.State = LedPanelState.Offline;
						}
						else
						{
							if (current.device_sflag == DEVICE_SPECIFY_FLAG.wifi_card)
							{
								panel.PortType = LedPortType.WiFi;
							}
							else
							{
								panel.PortType = LedPortType.Ethernet;
								panel.ProductID = pid;
							}
							panel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.FixedIP;
						}
					}
					else if (current.cur_use_net_mode == 4)
					{
						panel.PortType = LedPortType.Ethernet;
						panel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.Directly;
						panel.ProductID = pid;
					}
					else if (current.cur_use_net_mode == 8)
					{
						panel.PortType = LedPortType.SerialPort;
						panel.SerialPortName = current.ComPort_name;
						panel.BaudRate = current.BaudRate;
					}
					panel.ProtocolVersion = current.firmware_comm_ver;
					break;
				}
			}
		}

		private void ReadParm()
		{
			int num = 0;
			this.deviceResendDict = new Dictionary<string, int>();
			foreach (DEVICE_INFO current in this.deviceList)
			{
				if (!this.isThread)
				{
					break;
				}
				if (this.isContainsNetworkCard || ((current.cur_use_net_mode != 1 || current.device_sflag != DEVICE_SPECIFY_FLAG.network_card) && current.cur_use_net_mode != 2 && current.cur_use_net_mode != 4))
				{
					this.deviceResendDict.Add(current.product_id, 0);
					bool flag = false;
					for (int i = 0; i < LedCommunicationConst.ProtocolSendVersionList.Length; i++)
					{
						if (current.firmware_comm_ver == LedCommunicationConst.ProtocolSendVersionList[i])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						string text = this.summaryMessage;
						this.summaryMessage = string.Concat(new string[]
						{
							text,
							"●  ",
							formMain.ML.GetStr("Form_find_device_message_ProductID"),
							current.product_id,
							"，",
							formMain.ML.GetStr("Form_find_device_message_VersionNumber_Inconsistent"),
							"\r\n"
						});
					}
					else
					{
						num++;
						this.ReadPanelParm(current.product_id);
						Thread.Sleep(100);
					}
				}
			}
			if (num == 0)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.button_END_find.Enabled = false;
					this.button_start_find.Enabled = true;
					this.button_OK.Enabled = true;
					this.processBar.Value = this.processBar.Maximum;
					this.label_msg.Text = formMain.ML.GetStr("Form_find_device_message_label_msg_findDevice_complete");
					this.summaryMessage = string.Concat(new object[]
					{
						formMain.ML.GetStr("Form_find_device_message_findDevice_number"),
						this.findCount,
						" \r\n",
						this.summaryMessage
					});
					MessageBox.Show(this, this.summaryMessage);
				}));
			}
		}

		private void ReadPanelParm(string pid)
		{
			int i = 0;
			while (i < 5)
			{
				i++;
				SEND_CMD_RET_VALUE sEND_CMD_RET_VALUE = SEND_CMD_RET_VALUE.ER_NONE_INIT;
				if (this.IServer != null)
				{
					sEND_CMD_RET_VALUE = formMain.IServer.send_cmd_to_device_async(18, null, pid);
				}
				if (sEND_CMD_RET_VALUE != SEND_CMD_RET_VALUE.IPC_communication_FAILED)
				{
					break;
				}
				if (!this.isRestarting)
				{
					this.RestartIPCServer();
				}
				while (this.isRestarting)
				{
					Thread.Sleep(100);
				}
			}
		}

		private void ReadDeviceParm(string pid)
		{
			int i = 0;
			while (i < 5)
			{
				i++;
				SEND_CMD_RET_VALUE sEND_CMD_RET_VALUE = SEND_CMD_RET_VALUE.ER_NONE_INIT;
				if (this.IServer != null)
				{
					formMain.IServer.send_cmd_to_device_async(23, null, pid);
				}
				if (sEND_CMD_RET_VALUE != SEND_CMD_RET_VALUE.IPC_communication_FAILED)
				{
					break;
				}
				if (!this.isRestarting)
				{
					this.RestartIPCServer();
				}
				while (this.isRestarting)
				{
					Thread.Sleep(100);
				}
			}
		}

		private void RestartIPCServer()
		{
			this.isRestarting = true;
			string originalText = this.label_msg.Text;
			base.Invoke(new MethodInvoker(delegate
			{
				this.timer_processBar_draw.Stop();
				this.processBar.Visible = false;
				this.label_msg.Text = formMain.ML.GetStr("Form_find_device_label_msg_restart");
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
				this.IServer = formMain.IServer;
				base.Invoke(new MethodInvoker(delegate
				{
					this.label_msg.Text = formMain.ML.GetStr("Form_find_device_label_msg_Started");
				}));
			}
			else
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.label_msg.Text = formMain.ML.GetStr("Form_find_device_label_msg_Start_failure");
				}));
			}
			Thread.Sleep(1000);
			base.Invoke(new MethodInvoker(delegate
			{
				this.label_msg.Text = originalText;
				this.timer_processBar_draw.Start();
				this.processBar.Visible = true;
			}));
			Thread.Sleep(500);
			this.isRestarting = false;
		}

		private void button_start_find_Click(object sender, EventArgs e)
		{
			this.load_panel_to_datagridview();
			this.deviceList = new List<DEVICE_INFO>();
			this.panelList = new List<LedPanel>();
			this.findCount = 0;
			this.failureCount = 0;
			this.summaryMessage = string.Empty;
			if (this.deviceServerList != null && this.deviceServerList.Count > 0)
			{
				((List<DEVICE_INFO>)this.deviceList).AddRange(this.deviceServerList);
			}
			this.button_END_find.Enabled = true;
			this.button_start_find.Enabled = false;
			this.button_OK.Enabled = false;
			string a = this.mtxIPAddress.Text.Replace(" ", "");
			if (a != "...")
			{
				this.label_msg.Text = formMain.ML.GetStr("Form_find_device_label_msg_fixedIP");
				this.processBar.Visible = true;
				this.processBar.Value = 1;
				this.processBar.Minimum = 1;
				this.processBar.Maximum = 1501;
				this.timer_processBar_draw.Start();
			}
			else
			{
				this.label_msg.Text = formMain.ML.GetStr("Form_find_device_label_msg_boardcast");
				this.processBar.Visible = true;
				this.processBar.Value = 1;
				this.processBar.Minimum = 1;
				this.processBar.Maximum = 2000;
				this.timer_processBar_draw.Start();
			}
			this.isRestarting = false;
			this.isThread = true;
			this.thrSearch = new Thread(new ThreadStart(this.SearhDevice));
			this.thrSearch.IsBackground = true;
			this.thrSearch.Start();
		}

		private void SearhDevice()
		{
			int i = 0;
			while (i < 5)
			{
				i++;
				string text = this.mtxIPAddress.Text.Replace(" ", "");
				SEND_CMD_RET_VALUE sEND_CMD_RET_VALUE;
				if (text != "...")
				{
					sEND_CMD_RET_VALUE = this.IServer.add_device_to_list_async(text, ushort.Parse(this.textBox_port.Text));
				}
				else
				{
					sEND_CMD_RET_VALUE = this.IServer.find_device_async();
				}
				if (sEND_CMD_RET_VALUE != SEND_CMD_RET_VALUE.IPC_communication_FAILED)
				{
					break;
				}
				if (!this.isRestarting)
				{
					this.RestartIPCServer();
				}
				while (this.isRestarting)
				{
					Thread.Sleep(100);
				}
			}
		}

		private void button_END_find_Click(object sender, EventArgs e)
		{
			this.isThread = false;
			this.button_END_find.Enabled = false;
			this.button_start_find.Enabled = true;
			this.button_OK.Enabled = true;
			this.IServer.Cancel_prev_long_cmd();
			this.timer_processBar_draw.Stop();
			this.processBar.Visible = false;
			this.label_msg.Text = formMain.ML.GetStr("Form_find_device_label_message");
		}

		private void button_OK_Click(object sender, EventArgs e)
		{
			LedProject ledsys = formMain.ledsys;
			if (ledsys != null)
			{
				string str = formMain.ML.GetStr("Display_PanelAbbr");
				for (int i = 0; i < this.dgvDevice.Rows.Count; i++)
				{
					DataGridViewRow dataGridViewRow = this.dgvDevice.Rows[i];
					if ((bool)dataGridViewRow.Cells[1].Value)
					{
						int num = (int)dataGridViewRow.Cells[0].Tag;
						if (num > 0)
						{
							int j = 0;
							while (j < this.panelList.Count)
							{
								if (this.panelList[j].ID == dataGridViewRow.Cells[14].Value.ToString())
								{
									if (num == 1)
									{
										for (int k = 0; k < ledsys.Panels.Count; k++)
										{
											if (this.panelList[j].ID == ledsys.Panels[k].ID)
											{
												ledsys.Panels[k].Merge(this.panelList[j]);
												if (this.panelList[j].PortType == ledsys.Panels[k].PortType && ledsys.Panels[k].PortType == LedPortType.SerialPort)
												{
													ledsys.Panels[k].SerialPortName = this.panelList[j].SerialPortName;
													ledsys.Panels[k].BaudRate = this.panelList[j].BaudRate;
												}
												formMain.ModifyPanelFromIPCServer(ledsys.Panels[k]);
												break;
											}
										}
										break;
									}
									if (num == 2)
									{
										for (int l = 0; l < ledsys.Panels.Count; l++)
										{
											if (this.panelList[j].PortType == ledsys.Panels[l].PortType && ledsys.Panels[l].PortType == LedPortType.SerialPort)
											{
												ledsys.Panels[l].SerialPortName = this.panelList[j].SerialPortName;
												ledsys.Panels[l].BaudRate = this.panelList[j].BaudRate;
												formMain.ModifyPanelFromIPCServer(ledsys.Panels[l]);
												break;
											}
										}
										break;
									}
									if (num == 3)
									{
										int count = ledsys.Panels.Count;
										for (int m = 1; m < 99999; m++)
										{
											bool flag = false;
											string text = str + m.ToString();
											for (int n = 0; n < count; n++)
											{
												if (ledsys.Panels[n].ValueName == text)
												{
													flag = true;
													break;
												}
											}
											if (!flag)
											{
												this.panelList[j].TextName = text;
												this.panelList[j].ValueName = text;
												break;
											}
										}
										this.panelList[j].Group = dataGridViewRow.Cells[3].Value.ToString();
										ledsys.AddPanel(this.panelList[j]);
										break;
									}
									break;
								}
								else
								{
									j++;
								}
							}
						}
					}
				}
				base.DialogResult = DialogResult.OK;
			}
			base.Close();
		}

		private void button_Cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void timer_processBar_draw_Tick(object sender, EventArgs e)
		{
			if (this.processBar.Value < this.processBar.Maximum)
			{
				this.processBar.Value++;
				this.processBar_hold_jsq = 0;
				return;
			}
			this.processBar_hold_jsq++;
			if (this.processBar_hold_jsq > 10)
			{
				this.timer_processBar_draw.Stop();
				this.processBar.Visible = false;
			}
		}

		private void textBox_port_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		public string GetColorModeString(LedColorMode colorMode)
		{
			if (colorMode == LedColorMode.R)
			{
				return formMain.ML.GetStr("Form_find_device_colorMode_single");
			}
			if (colorMode == LedColorMode.RG || colorMode == LedColorMode.GR)
			{
				return formMain.ML.GetStr("Form_find_device_colorMode_double");
			}
			return formMain.ML.GetStr("Form_find_device_colorMode_three");
		}

		private void mtxIPAddress_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Decimal)
			{
				MaskedTextBox maskedTextBox = (MaskedTextBox)sender;
				int selectionStart = maskedTextBox.SelectionStart;
				if (selectionStart < 12)
				{
					int num = maskedTextBox.MaskedTextProvider.Length - maskedTextBox.MaskedTextProvider.EditPositionCount;
					int num2 = 0;
					for (int i = 0; i < maskedTextBox.MaskedTextProvider.Length; i++)
					{
						if (!maskedTextBox.MaskedTextProvider.IsEditPosition(i) && selectionStart + num >= i)
						{
							num2 = i;
						}
					}
					num2++;
					maskedTextBox.SelectionStart = num2;
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
			this.button_start_find = new Button();
			this.button_END_find = new Button();
			this.button_OK = new Button();
			this.button_Cancel = new Button();
			this.STBAR = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.label_msg = new ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new ToolStripStatusLabel();
			this.processBar = new ToolStripProgressBar();
			this.imgDeviceState = new ImageList(this.components);
			this.label_ip_tip = new Label();
			this.lblIPPort = new Label();
			this.textBox_port = new TextBox();
			this.timer_processBar_draw = new System.Windows.Forms.Timer(this.components);
			this.mtxIPAddress = new MaskedTextBox();
			this.dgvDevice = new DataGridView();
			this.ColumnState = new DataGridViewImageColumn();
			this.ColumnSelect = new DataGridViewCheckBoxColumn();
			this.ColumnSN = new DataGridViewTextBoxColumn();
			this.ColumnGroup = new DataGridViewComboBoxColumn();
			this.ColumnCardType = new DataGridViewTextBoxColumn();
			this.ColumnArea = new DataGridViewTextBoxColumn();
			this.ColumnOE = new DataGridViewTextBoxColumn();
			this.ColumnData = new DataGridViewTextBoxColumn();
			this.ColumnScan = new DataGridViewTextBoxColumn();
			this.ColumnCommMode = new DataGridViewTextBoxColumn();
			this.ColumnCardAddress = new DataGridViewTextBoxColumn();
			this.ColumnIPAddress = new DataGridViewTextBoxColumn();
			this.ColumnMACAddress = new DataGridViewTextBoxColumn();
			this.ColumnNetID = new DataGridViewTextBoxColumn();
			this.ColumnID = new DataGridViewTextBoxColumn();
			this.dataGridViewImageColumn1 = new DataGridViewImageColumn();
			this.LblWarning = new Label();
			this.STBAR.SuspendLayout();
			((ISupportInitialize)this.dgvDevice).BeginInit();
			base.SuspendLayout();
			this.button_start_find.AutoSize = true;
			this.button_start_find.Cursor = Cursors.Hand;
			this.button_start_find.Image = Resources.Find_Device;
			this.button_start_find.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button_start_find.Location = new System.Drawing.Point(11, 384);
			this.button_start_find.Name = "button_start_find";
			this.button_start_find.Size = new System.Drawing.Size(103, 31);
			this.button_start_find.TabIndex = 0;
			this.button_start_find.Text = "开始寻机(&S)";
			this.button_start_find.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.button_start_find.UseVisualStyleBackColor = true;
			this.button_start_find.Click += new EventHandler(this.button_start_find_Click);
			this.button_END_find.AutoSize = true;
			this.button_END_find.Cursor = Cursors.Hand;
			this.button_END_find.Enabled = false;
			this.button_END_find.Image = Resources.Find_Device_Stop;
			this.button_END_find.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button_END_find.Location = new System.Drawing.Point(671, 384);
			this.button_END_find.Name = "button_END_find";
			this.button_END_find.Size = new System.Drawing.Size(77, 31);
			this.button_END_find.TabIndex = 0;
			this.button_END_find.Text = "停止(&E)";
			this.button_END_find.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.button_END_find.UseVisualStyleBackColor = true;
			this.button_END_find.Click += new EventHandler(this.button_END_find_Click);
			this.button_OK.AutoSize = true;
			this.button_OK.Cursor = Cursors.Hand;
			this.button_OK.DialogResult = DialogResult.OK;
			this.button_OK.Enabled = false;
			this.button_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button_OK.Location = new System.Drawing.Point(774, 384);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(61, 31);
			this.button_OK.TabIndex = 0;
			this.button_OK.Text = "应用";
			this.button_OK.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new EventHandler(this.button_OK_Click);
			this.button_Cancel.AutoSize = true;
			this.button_Cancel.Cursor = Cursors.Hand;
			this.button_Cancel.DialogResult = DialogResult.Cancel;
			this.button_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button_Cancel.Location = new System.Drawing.Point(860, 384);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(61, 31);
			this.button_Cancel.TabIndex = 0;
			this.button_Cancel.Text = "取消";
			this.button_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new EventHandler(this.button_Cancel_Click);
			this.STBAR.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripStatusLabel1,
				this.label_msg,
				this.toolStripStatusLabel2,
				this.processBar
			});
			this.STBAR.Location = new System.Drawing.Point(0, 422);
			this.STBAR.Name = "STBAR";
			this.STBAR.Size = new System.Drawing.Size(936, 22);
			this.STBAR.SizingGrip = false;
			this.STBAR.TabIndex = 0;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(58, 17);
			this.toolStripStatusLabel1.Text = "提示信息:";
			this.label_msg.Name = "label_msg";
			this.label_msg.Size = new System.Drawing.Size(31, 17);
			this.label_msg.Text = "就绪";
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(832, 17);
			this.toolStripStatusLabel2.Spring = true;
			this.processBar.Alignment = ToolStripItemAlignment.Right;
			this.processBar.Name = "processBar";
			this.processBar.Size = new System.Drawing.Size(468, 16);
			this.processBar.Style = ProgressBarStyle.Continuous;
			this.processBar.Visible = false;
			this.imgDeviceState.ColorDepth = ColorDepth.Depth24Bit;
			this.imgDeviceState.ImageSize = new System.Drawing.Size(32, 24);
			this.imgDeviceState.TransparentColor = System.Drawing.Color.Transparent;
			this.label_ip_tip.AutoSize = true;
			this.label_ip_tip.Location = new System.Drawing.Point(143, 393);
			this.label_ip_tip.Name = "label_ip_tip";
			this.label_ip_tip.Size = new System.Drawing.Size(23, 12);
			this.label_ip_tip.TabIndex = 0;
			this.label_ip_tip.Text = "IP:";
			this.lblIPPort.AutoSize = true;
			this.lblIPPort.Location = new System.Drawing.Point(302, 393);
			this.lblIPPort.Name = "lblIPPort";
			this.lblIPPort.Size = new System.Drawing.Size(35, 12);
			this.lblIPPort.TabIndex = 0;
			this.lblIPPort.Text = "端口:";
			this.textBox_port.Location = new System.Drawing.Point(338, 390);
			this.textBox_port.MaxLength = 5;
			this.textBox_port.Name = "textBox_port";
			this.textBox_port.Size = new System.Drawing.Size(45, 21);
			this.textBox_port.TabIndex = 2;
			this.textBox_port.Text = "58258";
			this.textBox_port.KeyPress += new KeyPressEventHandler(this.textBox_port_KeyPress);
			this.timer_processBar_draw.Tick += new EventHandler(this.timer_processBar_draw_Tick);
			this.mtxIPAddress.Location = new System.Drawing.Point(172, 390);
			this.mtxIPAddress.Mask = "###.###.###.###";
			this.mtxIPAddress.Name = "mtxIPAddress";
			this.mtxIPAddress.PromptChar = ' ';
			this.mtxIPAddress.Size = new System.Drawing.Size(112, 21);
			this.mtxIPAddress.TabIndex = 3;
			this.mtxIPAddress.KeyDown += new KeyEventHandler(this.mtxIPAddress_KeyDown);
			this.dgvDevice.AllowUserToAddRows = false;
			this.dgvDevice.AllowUserToResizeColumns = false;
			this.dgvDevice.AllowUserToResizeRows = false;
			this.dgvDevice.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvDevice.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ColumnState,
				this.ColumnSelect,
				this.ColumnSN,
				this.ColumnGroup,
				this.ColumnCardType,
				this.ColumnArea,
				this.ColumnOE,
				this.ColumnData,
				this.ColumnScan,
				this.ColumnCommMode,
				this.ColumnCardAddress,
				this.ColumnIPAddress,
				this.ColumnMACAddress,
				this.ColumnNetID,
				this.ColumnID
			});
			this.dgvDevice.Location = new System.Drawing.Point(11, 44);
			this.dgvDevice.Name = "dgvDevice";
			this.dgvDevice.RowHeadersVisible = false;
			this.dgvDevice.RowTemplate.Height = 23;
			this.dgvDevice.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvDevice.Size = new System.Drawing.Size(910, 327);
			this.dgvDevice.TabIndex = 4;
			this.ColumnState.HeaderText = "状态";
			this.ColumnState.Name = "ColumnState";
			this.ColumnState.Width = 40;
			this.ColumnSelect.HeaderText = "选择";
			this.ColumnSelect.Name = "ColumnSelect";
			this.ColumnSelect.Width = 36;
			this.ColumnSN.HeaderText = "序号";
			this.ColumnSN.Name = "ColumnSN";
			this.ColumnSN.Width = 36;
			this.ColumnGroup.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
			this.ColumnGroup.HeaderText = "分组";
			this.ColumnGroup.Name = "ColumnGroup";
			this.ColumnGroup.Width = 65;
			this.ColumnCardType.HeaderText = "型号";
			this.ColumnCardType.Name = "ColumnCardType";
			this.ColumnCardType.Width = 55;
			this.ColumnArea.HeaderText = "面积";
			this.ColumnArea.Name = "ColumnArea";
			this.ColumnArea.Width = 70;
			this.ColumnOE.HeaderText = "OE";
			this.ColumnOE.Name = "ColumnOE";
			this.ColumnOE.Width = 30;
			this.ColumnData.HeaderText = "数据";
			this.ColumnData.Name = "ColumnData";
			this.ColumnData.Width = 40;
			this.ColumnScan.HeaderText = "扫描";
			this.ColumnScan.Name = "ColumnScan";
			this.ColumnScan.Width = 80;
			this.ColumnCommMode.HeaderText = "通讯方式";
			this.ColumnCommMode.Name = "ColumnCommMode";
			this.ColumnCommMode.Width = 80;
			this.ColumnCardAddress.HeaderText = "地址";
			this.ColumnCardAddress.Name = "ColumnCardAddress";
			this.ColumnCardAddress.Width = 55;
			this.ColumnIPAddress.HeaderText = "IP地址";
			this.ColumnIPAddress.Name = "ColumnIPAddress";
			this.ColumnIPAddress.Width = 110;
			this.ColumnMACAddress.HeaderText = "MAC地址";
			this.ColumnMACAddress.Name = "ColumnMACAddress";
			this.ColumnMACAddress.Width = 90;
			this.ColumnNetID.HeaderText = "网络ID";
			this.ColumnNetID.Name = "ColumnNetID";
			this.ColumnNetID.Width = 103;
			this.ColumnID.HeaderText = "ID";
			this.ColumnID.Name = "ColumnID";
			this.ColumnID.Visible = false;
			this.dataGridViewImageColumn1.HeaderText = "状态";
			this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
			this.dataGridViewImageColumn1.Width = 40;
			this.LblWarning.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.LblWarning.ForeColor = System.Drawing.Color.Red;
			this.LblWarning.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LblWarning.Location = new System.Drawing.Point(9, 11);
			this.LblWarning.Name = "LblWarning";
			this.LblWarning.Size = new System.Drawing.Size(912, 25);
			this.LblWarning.TabIndex = 5;
			this.LblWarning.Text = "提示：不支持网口寻机";
			this.LblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			base.AcceptButton = this.button_OK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.button_Cancel;
			base.ClientSize = new System.Drawing.Size(936, 444);
			base.Controls.Add(this.LblWarning);
			base.Controls.Add(this.dgvDevice);
			base.Controls.Add(this.mtxIPAddress);
			base.Controls.Add(this.lblIPPort);
			base.Controls.Add(this.label_ip_tip);
			base.Controls.Add(this.textBox_port);
			base.Controls.Add(this.STBAR);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_OK);
			base.Controls.Add(this.button_END_find);
			base.Controls.Add(this.button_start_find);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Form_find_device";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "寻机";
			base.FormClosing += new FormClosingEventHandler(this.Form_find_device_FormClosing);
			base.Load += new EventHandler(this.Form_find_device_Load);
			this.STBAR.ResumeLayout(false);
			this.STBAR.PerformLayout();
			((ISupportInitialize)this.dgvDevice).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
