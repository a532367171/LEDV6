using LedCommunication;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Data;
using LedModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem.LedControlSystem
{
	public class formUSBWrite : Form
	{
		private LedPanel panel;

		private string codefile;

		private static string formID = "formUSBWrite";

		private IList<string> UdiskDirList;

		private string LastMessage = "";

		private int WarningLevel;

		private IContainer components;

		private DateTimePicker TimingDateTimePicker;

		private CheckBox TimingCheckBox;

		private CheckBox DataCheckBox;

		private Button UsbSaveButton;

		private Label SelUdiskLabel;

		private ComboBox UsbListComboBox;

		private Label label1_PanelParam;

		private Label label4_OffOn;

		private Label label3_Lumiance;

		private Label label2_Scantype;

		private Label label_Info_OffOn;

		private Label label_Info_Luminance;

		private Label label_Info_Scantype;

		private Label label_info_Panel;

		private System.Windows.Forms.Timer timer1;

		private Label label_SelectFlash;

		private Label label_Remind;

		private Button button_AddOneMinute;

		private GroupBox zhGroupBox_USBOption;

		public static string FormID
		{
			get
			{
				return formUSBWrite.formID;
			}
			set
			{
				formUSBWrite.formID = value;
			}
		}

		public formUSBWrite()
		{
			this.InitializeComponent();
		}

		public formUSBWrite(LedPanel pPanel)
		{
			this.InitializeComponent();
			base.Size = new System.Drawing.Size(470, 400);
			formMain.ML.NowFormID = formUSBWrite.formID;
			this.Text = formMain.ML.GetStr("formUSBWrite_FormText");
			this.zhGroupBox_USBOption.Text = formMain.ML.GetStr("formUSBWrite_GroupBox_USBOption");
			this.label1_PanelParam.Text = formMain.ML.GetStr("formUSBWrite_label_PanelParam");
			this.label2_Scantype.Text = formMain.ML.GetStr("formUSBWrite_label_Scantype");
			this.label3_Lumiance.Text = formMain.ML.GetStr("formUSBWrite_label_Lumiance");
			this.label4_OffOn.Text = formMain.ML.GetStr("formUSBWrite_label_OFFandON");
			this.DataCheckBox.Text = formMain.ML.GetStr("formUSBWrite_CheckBox_DisplayData");
			this.TimingCheckBox.Text = formMain.ML.GetStr("formUSBWrite_CheckBox_Timing");
			this.button_AddOneMinute.Text = formMain.ML.GetStr("formUSBWrite_Button_AddOneMinute");
			this.label_SelectFlash.Text = formMain.ML.GetStr("formUSBWrite_label_SelectFlash");
			this.SelUdiskLabel.Text = formMain.ML.GetStr("formUSBWrite_label_SelUdisk");
			this.UsbSaveButton.Text = formMain.ML.GetStr("formUSBWrite_ButtonUsbSave");
			this.panel = pPanel;
			this.updataUdiskList();
			if (this.UsbListComboBox.Items.Count > 0)
			{
				this.UsbListComboBox.SelectedIndex = 0;
			}
			else
			{
				this.UsbListComboBox.Text = "无";
			}
			this.DataCheckBox.Checked = true;
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

		private void formUSBWrite_Load(object sender, EventArgs e)
		{
			this.zhGroupBox_USBOption.BackColor = Template.GroupBox_BackColor;
			this.timer1.Start();
			base.BringToFront();
			this.Refresh();
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.label_info_Panel.Text = formUSBWrite.GetPanelParamString(this.panel);
			this.label_Info_Scantype.Text = "1/" + this.panel.RoutingSetting.ScanTypeIndex.ToString() + "," + formRouting.GetRoutingString(this.panel.RoutingSetting.ScanTypeIndex, this.panel.RoutingSetting.RoutingIndex);
			this.label_Info_Luminance.Text = formUSBWrite.GetLuminanceString(this.panel);
			if (this.panel.TimerSwitch.Enabled)
			{
				this.label_Info_OffOn.Text = formMain.ML.GetStr("Display_Enable");
				return;
			}
			this.label_Info_OffOn.Text = formMain.ML.GetStr("Display_Disable");
		}

		private void formUSBWrite_Event(int type)
		{
			base.Close();
		}

		public static string GetLuminanceString(LedPanel pPanel)
		{
			string result;
			if (pPanel.Luminance.RegulateMode == 0)
			{
				result = formMain.ML.GetStr("USB_Luminance_Manual") + pPanel.Luminance.ManualValue.ToString();
			}
			else if (pPanel.Luminance.RegulateMode == 1)
			{
				result = formMain.ML.GetStr("USB_Luminance_Auto");
			}
			else
			{
				result = formMain.ML.GetStr("USB_Luminance_Sensor");
			}
			return result;
		}

		private void TimingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			this.TimingDateTimePicker.Enabled = checkBox.Checked;
			this.button_AddOneMinute.Enabled = checkBox.Checked;
		}

		private void CodeButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "bin files (*.zhcode)|*.zhcode";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.codefile = openFileDialog.FileName;
			}
		}

		private void CodeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				this.DataCheckBox.Checked = false;
				this.TimingCheckBox.Checked = false;
				this.TimingDateTimePicker.Enabled = false;
				return;
			}
			this.DataCheckBox.Checked = true;
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
						this.WarningLevel = 1;
					}
					result = true;
				}
				else
				{
					this.label_Remind.Text = formMain.ML.GetStr("USB_NotSupportFormat");
					this.label_Remind.ForeColor = System.Drawing.Color.Red;
					this.WarningLevel = 2;
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

		private void UsbSaveButton_Click(object sender, EventArgs e)
		{
			this.timer1.Stop();
			try
			{
				this.UsbSaveButton.Enabled = false;
				this.label_Remind.Text = formMain.ML.GetStr("USB_SavingData");
				Thread.Sleep(500);
				this.label_Remind.ForeColor = System.Drawing.Color.Black;
				this.WarningLevel = 0;
				if (this.UsbListComboBox.SelectedIndex == -1)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_NoFlashDisk"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				}
				else
				{
					base.Enabled = false;
					ProcessUSB processUSB = new ProcessUSB();
					if (this.panel.IsLSeries())
					{
						processUSB.PanelBytes = this.panel.ToLBytes();
						processUSB.ItemStartLBytes = this.panel.ToItemStartLBytes();
						processUSB.ItemLBytes = this.panel.ToItemLBytes();
						processUSB.ItemTimerLBytes = this.panel.ToItemTimerLByte();
						processUSB.TimerSwitchBytes = this.panel.TimerSwitch.ToLBytes();
						processUSB.LuminanceBytes = this.panel.Luminance.ToLBytes();
					}
					else
					{
						processUSB.PanelBytes = this.panel.ToBytes();
						processUSB.BmpDataBytes = this.panel.ToItemBmpDataBytes();
						processUSB.ItemBytes = this.panel.ToItemBytes();
						processUSB.TimerSwitchBytes = this.panel.TimerSwitch.ToBytes();
						processUSB.LuminanceBytes = this.panel.Luminance.ToBytes();
					}
					processUSB.ProtocolVersion = (byte)this.panel.GetProtocolType();
					if (this.TimingCheckBox.Checked)
					{
						processUSB.SetTimingBytes(this.TimingDateTimePicker.Value);
					}
					protocol_data_integration protocol_data_integration = new protocol_data_integration();
					byte[] array = protocol_data_integration.WritingData_USB_Pack(processUSB, false, false);
					if (array == null)
					{
						this.UsbDataMessagePrompt("USB_Save_Failed", 2, System.Drawing.Color.Red);
					}
					else if (array.Length > this.panel.GetFlashCapacity())
					{
						this.UsbDataMessagePrompt("Prompt_MemeoryOverSize", 2, System.Drawing.Color.Red);
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
						if (!File.Exists(this.UdiskDirList[this.UsbListComboBox.SelectedIndex] + "\\ledV3.zh3"))
						{
							MessageBox.Show(this, formMain.ML.GetStr("Prompt_DataDownloadFailed"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
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
			this.UsbSaveButton.Enabled = true;
			base.Enabled = true;
			Thread.Sleep(1000);
			this.timer1.Start();
			this.LastMessage = "save";
		}

		public static string GetPanelParamString(LedPanel pPanel)
		{
			string text = "";
			text = text + formMain.ML.GetStr("USB_LedModel") + pPanel.CardType.ToString().Replace("_", "-") + ",";
			text += formMain.ML.GetStr("USB_ColorModel");
			if (pPanel.ColorMode == LedColorMode.R)
			{
				text += formMain.ML.GetStr("USB_ColorModel_Single");
			}
			else if (pPanel.ColorMode == LedColorMode.RG | pPanel.ColorMode == LedColorMode.GR)
			{
				text += formMain.ML.GetStr("USB_ColorModel_Double");
			}
			else
			{
				text += formMain.ML.GetStr("USB_ColorModel_Three");
			}
			text = text + formMain.ML.GetStr("USB_Height") + pPanel.Height.ToString() + ",";
			text = text + formMain.ML.GetStr("USB_Width") + pPanel.Width.ToString() + ",";
			text += formMain.ML.GetStr("USB_OE");
			if (pPanel.OEPolarity == 1)
			{
				text += formMain.ML.GetStr("USB_HightLevel");
			}
			else
			{
				text += formMain.ML.GetStr("USB_LowLevel");
			}
			text += formMain.ML.GetStr("USB_Data");
			if (pPanel.DataPolarity == 1)
			{
				text += formMain.ML.GetStr("USB_HightLevel");
			}
			else
			{
				text += formMain.ML.GetStr("USB_LowLevel");
			}
			return text;
		}

		private void DataCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			bool arg_0D_0 = checkBox.Focused;
		}

		private void zhGroupBox_USBOption_MouseEnter(object sender, EventArgs e)
		{
			this.Cursor = Cursors.Arrow;
		}

		private void zhGroupBox_USBOption_MouseMove(object sender, MouseEventArgs e)
		{
			this.Cursor = Cursors.Arrow;
		}

		private void UsbListComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void UsbListComboBox_Click(object sender, EventArgs e)
		{
		}

		private void UsbListComboBox_MouseDown(object sender, MouseEventArgs e)
		{
		}

		private void label2_Click(object sender, EventArgs e)
		{
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.updataUdiskList();
		}

		private void button_AddOneMinute_Click(object sender, EventArgs e)
		{
			DateTime value = DateTime.Now.AddMinutes(1.0);
			this.TimingDateTimePicker.Value = value;
		}

		public bool SaveForeignTradeModeData()
		{
			bool result;
			try
			{
				ProcessUSB processUSB = new ProcessUSB();
				processUSB.ItemStartLBytes = this.panel.ToItemStartLBytes();
				processUSB.ItemLBytes = this.panel.ToItemLBytes();
				processUSB.ItemTimerLBytes = this.panel.ToItemTimerLByte();
				processUSB.TimerSwitchBytes = this.panel.TimerSwitch.ToLBytes();
				processUSB.LuminanceBytes = this.panel.Luminance.ToLBytes();
				processUSB.ProtocolVersion = 52;
				if (this.TimingCheckBox.Checked)
				{
					processUSB.SetTimingBytes(this.TimingDateTimePicker.Value);
				}
				protocol_data_integration protocol_data_integration = new protocol_data_integration();
				byte[] array = protocol_data_integration.WritingData_USB_Pack(processUSB, false, true);
				processUSB.TimerSwitchBytes = null;
				processUSB.LuminanceBytes = null;
				processUSB.PanelBytes = this.panel.ToBytes();
				processUSB.BmpDataBytes = this.panel.ToItemBmpDataBytes();
				processUSB.ItemBytes = this.panel.ToItemBytes();
				processUSB.TimerSwitchBytes = this.panel.TimerSwitch.ToBytes();
				processUSB.LuminanceBytes = this.panel.Luminance.ToBytes();
				processUSB.ProtocolVersion = 49;
				byte[] array2 = protocol_data_integration.WritingData_USB_Pack(processUSB, false, false);
				if (array2 == null || array == null)
				{
					this.UsbDataMessagePrompt("USB_Save_Failed", 2, System.Drawing.Color.Red);
					result = false;
				}
				else if (array2.Length > this.panel.GetFlashCapacity() || array.Length > this.panel.GetFlashCapacity())
				{
					this.UsbDataMessagePrompt("Prompt_MemeoryOverSize", 2, System.Drawing.Color.Red);
					result = false;
				}
				else
				{
					string str = this.UdiskDirList[this.UsbListComboBox.SelectedIndex];
					Dictionary<string, byte[]> dictionary = new Dictionary<string, byte[]>();
					Directory.CreateDirectory(str + "ZH_LED\\ZH-5WX");
					Directory.CreateDirectory(str + "ZH_LED\\ZH-XXL");
					string key = str + "ZH_LED\\ZH-5WX\\led_data.zh";
					string key2 = str + "ZH_LED\\ZH-XXL\\led_data.zh";
					dictionary.Add(key, array2);
					dictionary.Add(key2, array);
					foreach (string current in dictionary.Keys)
					{
						int i = 10;
						while (i > 0)
						{
							try
							{
								i--;
								FileStream fileStream = new FileStream(current, FileMode.Create, FileAccess.Write);
								DriveInfo driveInfo = new DriveInfo(this.UdiskDirList[this.UsbListComboBox.SelectedIndex]);
								if ((long)dictionary[current].Length > driveInfo.TotalFreeSpace)
								{
									fileStream.Close();
									this.UsbDataMessagePrompt("formUSBWrite_Message_FileOverUsbSize", 2, System.Drawing.Color.Red);
									result = false;
									return result;
								}
								fileStream.Write(dictionary[current], 0, dictionary[current].Length);
								fileStream.Close();
								break;
							}
							catch
							{
								if (i == 0)
								{
									this.UsbDataMessagePrompt("USB_Save_Failed", 2, System.Drawing.Color.Red);
									result = false;
									return result;
								}
								Thread.Sleep(200);
							}
						}
					}
					Thread.Sleep(500);
					foreach (string current2 in dictionary.Keys)
					{
						if (!File.Exists(current2))
						{
							MessageBox.Show(this, formMain.ML.GetStr("Prompt_DataDownloadFailed"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
							result = false;
							return result;
						}
					}
					this.UsbDataMessagePrompt("USB_Save_Success", 3, System.Drawing.Color.Blue);
					result = true;
				}
			}
			catch
			{
				this.UsbDataMessagePrompt("USB_Save_Failed", 2, System.Drawing.Color.Red);
				result = false;
			}
			return result;
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
			this.label_Remind = new Label();
			this.button_AddOneMinute = new Button();
			this.label_SelectFlash = new Label();
			this.label_Info_OffOn = new Label();
			this.label_Info_Luminance = new Label();
			this.label_Info_Scantype = new Label();
			this.label_info_Panel = new Label();
			this.label4_OffOn = new Label();
			this.label3_Lumiance = new Label();
			this.label2_Scantype = new Label();
			this.label1_PanelParam = new Label();
			this.UsbSaveButton = new Button();
			this.SelUdiskLabel = new Label();
			this.UsbListComboBox = new ComboBox();
			this.TimingDateTimePicker = new DateTimePicker();
			this.TimingCheckBox = new CheckBox();
			this.DataCheckBox = new CheckBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.zhGroupBox_USBOption = new GroupBox();
			this.zhGroupBox_USBOption.SuspendLayout();
			base.SuspendLayout();
			this.label_Remind.Location = new System.Drawing.Point(20, 298);
			this.label_Remind.Name = "label_Remind";
			this.label_Remind.Size = new System.Drawing.Size(378, 45);
			this.label_Remind.TabIndex = 17;
			this.label_Remind.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button_AddOneMinute.Enabled = false;
			this.button_AddOneMinute.Location = new System.Drawing.Point(301, 218);
			this.button_AddOneMinute.Name = "button_AddOneMinute";
			this.button_AddOneMinute.Size = new System.Drawing.Size(75, 23);
			this.button_AddOneMinute.TabIndex = 16;
			this.button_AddOneMinute.Text = "+1分钟";
			this.button_AddOneMinute.UseVisualStyleBackColor = true;
			this.button_AddOneMinute.Click += new EventHandler(this.button_AddOneMinute_Click);
			this.button_AddOneMinute.MouseEnter += new EventHandler(this.zhGroupBox_USBOption_MouseEnter);
			this.button_AddOneMinute.MouseMove += new MouseEventHandler(this.zhGroupBox_USBOption_MouseMove);
			this.label_SelectFlash.AutoSize = true;
			this.label_SelectFlash.Location = new System.Drawing.Point(77, 249);
			this.label_SelectFlash.Name = "label_SelectFlash";
			this.label_SelectFlash.Size = new System.Drawing.Size(95, 12);
			this.label_SelectFlash.TabIndex = 15;
			this.label_SelectFlash.Text = "请正确的选择U盘";
			this.label_Info_OffOn.Cursor = Cursors.Arrow;
			this.label_Info_OffOn.Location = new System.Drawing.Point(16, 194);
			this.label_Info_OffOn.Name = "label_Info_OffOn";
			this.label_Info_OffOn.Size = new System.Drawing.Size(307, 12);
			this.label_Info_OffOn.TabIndex = 14;
			this.label_Info_Luminance.Cursor = Cursors.Arrow;
			this.label_Info_Luminance.Location = new System.Drawing.Point(16, 150);
			this.label_Info_Luminance.Name = "label_Info_Luminance";
			this.label_Info_Luminance.Size = new System.Drawing.Size(344, 12);
			this.label_Info_Luminance.TabIndex = 13;
			this.label_Info_Scantype.Cursor = Cursors.Arrow;
			this.label_Info_Scantype.Location = new System.Drawing.Point(16, 105);
			this.label_Info_Scantype.Name = "label_Info_Scantype";
			this.label_Info_Scantype.Size = new System.Drawing.Size(344, 12);
			this.label_Info_Scantype.TabIndex = 12;
			this.label_Info_Scantype.Click += new EventHandler(this.label2_Click);
			this.label_info_Panel.Cursor = Cursors.Arrow;
			this.label_info_Panel.Location = new System.Drawing.Point(16, 44);
			this.label_info_Panel.Name = "label_info_Panel";
			this.label_info_Panel.Size = new System.Drawing.Size(344, 31);
			this.label_info_Panel.TabIndex = 11;
			this.label4_OffOn.AutoSize = true;
			this.label4_OffOn.Cursor = Cursors.Arrow;
			this.label4_OffOn.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label4_OffOn.Location = new System.Drawing.Point(16, 172);
			this.label4_OffOn.Name = "label4_OffOn";
			this.label4_OffOn.Size = new System.Drawing.Size(70, 12);
			this.label4_OffOn.TabIndex = 10;
			this.label4_OffOn.Text = "自动开关机";
			this.label3_Lumiance.AutoSize = true;
			this.label3_Lumiance.Cursor = Cursors.Arrow;
			this.label3_Lumiance.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label3_Lumiance.Location = new System.Drawing.Point(16, 128);
			this.label3_Lumiance.Name = "label3_Lumiance";
			this.label3_Lumiance.Size = new System.Drawing.Size(57, 12);
			this.label3_Lumiance.TabIndex = 9;
			this.label3_Lumiance.Text = "亮度调节";
			this.label2_Scantype.AutoSize = true;
			this.label2_Scantype.Cursor = Cursors.Arrow;
			this.label2_Scantype.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label2_Scantype.Location = new System.Drawing.Point(16, 82);
			this.label2_Scantype.Name = "label2_Scantype";
			this.label2_Scantype.Size = new System.Drawing.Size(57, 12);
			this.label2_Scantype.TabIndex = 8;
			this.label2_Scantype.Text = "扫描方式";
			this.label1_PanelParam.AutoSize = true;
			this.label1_PanelParam.Cursor = Cursors.Arrow;
			this.label1_PanelParam.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label1_PanelParam.Location = new System.Drawing.Point(16, 24);
			this.label1_PanelParam.Name = "label1_PanelParam";
			this.label1_PanelParam.Size = new System.Drawing.Size(57, 12);
			this.label1_PanelParam.TabIndex = 7;
			this.label1_PanelParam.Text = "基本屏参";
			this.UsbSaveButton.Location = new System.Drawing.Point(301, 264);
			this.UsbSaveButton.Name = "UsbSaveButton";
			this.UsbSaveButton.Size = new System.Drawing.Size(75, 23);
			this.UsbSaveButton.TabIndex = 6;
			this.UsbSaveButton.Text = "保存";
			this.UsbSaveButton.UseVisualStyleBackColor = true;
			this.UsbSaveButton.Click += new EventHandler(this.UsbSaveButton_Click);
			this.UsbSaveButton.MouseEnter += new EventHandler(this.zhGroupBox_USBOption_MouseEnter);
			this.UsbSaveButton.MouseMove += new MouseEventHandler(this.zhGroupBox_USBOption_MouseMove);
			this.SelUdiskLabel.Location = new System.Drawing.Point(20, 263);
			this.SelUdiskLabel.Name = "SelUdiskLabel";
			this.SelUdiskLabel.Size = new System.Drawing.Size(47, 25);
			this.SelUdiskLabel.TabIndex = 5;
			this.SelUdiskLabel.Text = "选择U盘";
			this.SelUdiskLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SelUdiskLabel.Click += new EventHandler(this.zhGroupBox_USBOption_MouseEnter);
			this.SelUdiskLabel.MouseEnter += new EventHandler(this.zhGroupBox_USBOption_MouseEnter);
			this.SelUdiskLabel.MouseMove += new MouseEventHandler(this.zhGroupBox_USBOption_MouseMove);
			this.UsbListComboBox.Cursor = Cursors.Default;
			this.UsbListComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.UsbListComboBox.FormattingEnabled = true;
			this.UsbListComboBox.ImeMode = ImeMode.On;
			this.UsbListComboBox.Location = new System.Drawing.Point(77, 266);
			this.UsbListComboBox.Name = "UsbListComboBox";
			this.UsbListComboBox.Size = new System.Drawing.Size(218, 20);
			this.UsbListComboBox.TabIndex = 4;
			this.UsbListComboBox.SelectedIndexChanged += new EventHandler(this.UsbListComboBox_SelectedIndexChanged);
			this.UsbListComboBox.Click += new EventHandler(this.UsbListComboBox_Click);
			this.UsbListComboBox.MouseDown += new MouseEventHandler(this.UsbListComboBox_MouseDown);
			this.TimingDateTimePicker.CustomFormat = "yyyy-MM-dd  HH:mm:ss";
			this.TimingDateTimePicker.Enabled = false;
			this.TimingDateTimePicker.Format = DateTimePickerFormat.Custom;
			this.TimingDateTimePicker.Location = new System.Drawing.Point(139, 219);
			this.TimingDateTimePicker.Name = "TimingDateTimePicker";
			this.TimingDateTimePicker.ShowUpDown = true;
			this.TimingDateTimePicker.Size = new System.Drawing.Size(156, 21);
			this.TimingDateTimePicker.TabIndex = 1;
			this.TimingDateTimePicker.MouseEnter += new EventHandler(this.zhGroupBox_USBOption_MouseEnter);
			this.TimingDateTimePicker.MouseMove += new MouseEventHandler(this.zhGroupBox_USBOption_MouseMove);
			this.TimingCheckBox.Location = new System.Drawing.Point(22, 221);
			this.TimingCheckBox.Name = "TimingCheckBox";
			this.TimingCheckBox.Size = new System.Drawing.Size(95, 19);
			this.TimingCheckBox.TabIndex = 0;
			this.TimingCheckBox.Text = "校准时间到:";
			this.TimingCheckBox.UseVisualStyleBackColor = true;
			this.TimingCheckBox.CheckedChanged += new EventHandler(this.TimingCheckBox_CheckedChanged);
			this.TimingCheckBox.MouseEnter += new EventHandler(this.zhGroupBox_USBOption_MouseEnter);
			this.TimingCheckBox.MouseMove += new MouseEventHandler(this.zhGroupBox_USBOption_MouseMove);
			this.DataCheckBox.AutoSize = true;
			this.DataCheckBox.Location = new System.Drawing.Point(21, 221);
			this.DataCheckBox.Name = "DataCheckBox";
			this.DataCheckBox.Size = new System.Drawing.Size(72, 16);
			this.DataCheckBox.TabIndex = 0;
			this.DataCheckBox.Text = "显示数据";
			this.DataCheckBox.UseVisualStyleBackColor = true;
			this.DataCheckBox.Visible = false;
			this.DataCheckBox.CheckedChanged += new EventHandler(this.DataCheckBox_CheckedChanged);
			this.DataCheckBox.MouseEnter += new EventHandler(this.zhGroupBox_USBOption_MouseEnter);
			this.DataCheckBox.MouseMove += new MouseEventHandler(this.zhGroupBox_USBOption_MouseMove);
			this.timer1.Interval = 1000;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.zhGroupBox_USBOption.Controls.Add(this.label_Remind);
			this.zhGroupBox_USBOption.Controls.Add(this.label_info_Panel);
			this.zhGroupBox_USBOption.Controls.Add(this.button_AddOneMinute);
			this.zhGroupBox_USBOption.Controls.Add(this.DataCheckBox);
			this.zhGroupBox_USBOption.Controls.Add(this.label_SelectFlash);
			this.zhGroupBox_USBOption.Controls.Add(this.TimingCheckBox);
			this.zhGroupBox_USBOption.Controls.Add(this.label_Info_OffOn);
			this.zhGroupBox_USBOption.Controls.Add(this.TimingDateTimePicker);
			this.zhGroupBox_USBOption.Controls.Add(this.label_Info_Luminance);
			this.zhGroupBox_USBOption.Controls.Add(this.UsbListComboBox);
			this.zhGroupBox_USBOption.Controls.Add(this.label_Info_Scantype);
			this.zhGroupBox_USBOption.Controls.Add(this.SelUdiskLabel);
			this.zhGroupBox_USBOption.Controls.Add(this.UsbSaveButton);
			this.zhGroupBox_USBOption.Controls.Add(this.label4_OffOn);
			this.zhGroupBox_USBOption.Controls.Add(this.label1_PanelParam);
			this.zhGroupBox_USBOption.Controls.Add(this.label3_Lumiance);
			this.zhGroupBox_USBOption.Controls.Add(this.label2_Scantype);
			this.zhGroupBox_USBOption.Location = new System.Drawing.Point(3, 3);
			this.zhGroupBox_USBOption.Name = "zhGroupBox_USBOption";
			this.zhGroupBox_USBOption.Size = new System.Drawing.Size(447, 358);
			this.zhGroupBox_USBOption.TabIndex = 22;
			this.zhGroupBox_USBOption.TabStop = false;
			this.zhGroupBox_USBOption.Text = "USB写入选项";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(452, 368);
			base.Controls.Add(this.zhGroupBox_USBOption);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formUSBWrite";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "写入U盘数据";
			base.Load += new EventHandler(this.formUSBWrite_Load);
			this.zhGroupBox_USBOption.ResumeLayout(false);
			this.zhGroupBox_USBOption.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
