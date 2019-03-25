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
	public class formUSBUpdate : Form
	{
		private static string formID = "FormUSBUpdate";

		private IList<string> UdiskDirList;

		private string codefile;

		private LedPanel panel;

		private bool isUDiskReady;

		private bool isFileReady;

		private int WarningLevel;

		private IContainer components;

		private GroupBox groupBox4;

		private CheckBox CodeCheckBox;

		private Button UsbSaveButton;

		private TextBox CodeTextBox;

		private Label SelUdiskLabel;

		private Button CodeButton;

		private ComboBox UsbListComboBox;

		private System.Windows.Forms.Timer timer1;

		private Label label_Remind;

		private Label label_SelectFlash;

		public static string FormID
		{
			get
			{
				return formUSBUpdate.formID;
			}
			set
			{
				formUSBUpdate.formID = value;
			}
		}

		public formUSBUpdate()
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formUSBUpdate.formID;
			this.panel = new LedPanel();
			this.Text = formMain.ML.GetStr("FormUSBUpdate_FormText");
			this.groupBox4.Text = formMain.ML.GetStr("FormUSBUpdate_groupBox_ProgramUpgrade");
			this.CodeCheckBox.Text = formMain.ML.GetStr("FormUSBUpdate_CodeCheckBox_UpgradeProgram");
			this.CodeButton.Text = formMain.ML.GetStr("FormUSBUpdate_Button_Browse");
			this.SelUdiskLabel.Text = formMain.ML.GetStr("FormUSBUpdate_Label_Select_Udisk");
			this.UsbSaveButton.Text = formMain.ML.GetStr("FormUSBUpdate_Button_UsbSave");
		}

		private void CodeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			this.CodeTextBox.Enabled = checkBox.Checked;
			this.CodeButton.Enabled = checkBox.Checked;
			this.UsbListComboBox.Enabled = checkBox.Checked;
			if (!checkBox.Checked)
			{
				this.CodeTextBox.Text = "";
				this.isFileReady = false;
				this.UsbSaveButton.Enabled = false;
			}
		}

		private void CodeButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "bin files (*.zhcode)|*.zhcode";
			try
			{
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
			catch
			{
				openFileDialog.InitialDirectory = formMain.DesktopPath;
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
			this.codefile = openFileDialog.FileName;
			this.CodeTextBox.Text = this.codefile;
			this.isFileReady = true;
			this.UsbSaveButton.Enabled = (this.isFileReady && this.isUDiskReady);
		}

		private void UsbSaveButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.CodeTextBox.Text))
				{
					this.label_Remind.Text = formMain.ML.GetStr("Prompt_DataDownloadFailed");
				}
				else
				{
					this.panel.FirmwareFilePath = this.CodeTextBox.Text;
					ProcessFirmware processFirmware = new ProcessFirmware();
					processFirmware.FirmwareBytes = this.panel.ToFirmwareBytes(true);
					if (processFirmware.FirmwareBytes == null || processFirmware.FirmwareBytes.Length == 0 || this.panel.CardType == LedCardType.Null)
					{
						this.label_Remind.Text = formMain.ML.GetStr("Prompt_DataDownloadFailed");
					}
					else
					{
						processFirmware.ProtocolVersion = (byte)this.panel.GetProtocolType();
						protocol_data_integration protocol_data_integration = new protocol_data_integration();
						byte[] array = protocol_data_integration.Upgrade_Program_USB(processFirmware);
						if (array != null)
						{
							string path = string.Empty;
							path = this.UdiskDirList[this.UsbListComboBox.SelectedIndex] + "ledV3.zh3";
							FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
							DriveInfo driveInfo = new DriveInfo(this.UdiskDirList[this.UsbListComboBox.SelectedIndex]);
							if ((long)array.Length > driveInfo.TotalFreeSpace)
							{
								fileStream.Close();
								MessageBox.Show(this, formMain.ML.GetStr("formUSBWrite_Message_FileOverUsbSize"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
							}
							else
							{
								fileStream.Write(array, 0, array.Length);
								fileStream.Close();
								Thread.Sleep(500);
								if (File.Exists(path))
								{
									MessageBox.Show(this, formMain.ML.GetStr("Prompt_DataDownloadSuccessed"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
									base.Close();
								}
								else
								{
									this.label_Remind.Text = formMain.ML.GetStr("Prompt_DataDownloadFailed");
								}
							}
						}
						else
						{
							this.label_Remind.Text = formMain.ML.GetStr("Prompt_DataDownloadFailed");
						}
					}
				}
			}
			catch
			{
				this.label_Remind.Text = formMain.ML.GetStr("Prompt_DataDownloadFailed");
			}
		}

		public void updataUdiskList()
		{
			this.label_Remind.Text = "";
			int num = this.UsbListComboBox.SelectedIndex;
			DriveInfo[] drives = DriveInfo.GetDrives();
			new List<string>();
			this.UdiskDirList = new List<string>();
			IList<string> list = new List<string>();
			int num2 = 0;
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
						num2++;
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
			if (this.UdiskDirList.Count >= num + 1)
			{
				if (num < 0)
				{
					num = 0;
				}
				if (this.UdiskDirList.Count > 0)
				{
					this.UsbListComboBox.SelectedIndex = num;
				}
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
				this.isUDiskReady = this.IsNTFS(this.UdiskDirList[this.UsbListComboBox.SelectedIndex]);
				this.UsbSaveButton.Enabled = (this.isUDiskReady && this.isFileReady);
			}
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
				else if (this.WarningLevel < 2)
				{
					this.label_Remind.Text = formMain.ML.GetStr("USB_NotFAT32");
					this.WarningLevel = 1;
				}
				return true;
			}
			this.label_Remind.Text = formMain.ML.GetStr("USB_NotSupportFormat");
			this.label_Remind.ForeColor = System.Drawing.Color.Red;
			this.WarningLevel = 2;
			return false;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.updataUdiskList();
		}

		private void formUSBUpdate_Load(object sender, EventArgs e)
		{
			this.timer1.Start();
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.BackColor = Template.GroupBox_BackColor;
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
			this.groupBox4 = new GroupBox();
			this.label_Remind = new Label();
			this.label_SelectFlash = new Label();
			this.CodeCheckBox = new CheckBox();
			this.UsbSaveButton = new Button();
			this.CodeTextBox = new TextBox();
			this.SelUdiskLabel = new Label();
			this.CodeButton = new Button();
			this.UsbListComboBox = new ComboBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.groupBox4.SuspendLayout();
			base.SuspendLayout();
			this.groupBox4.Controls.Add(this.label_Remind);
			this.groupBox4.Controls.Add(this.label_SelectFlash);
			this.groupBox4.Controls.Add(this.CodeCheckBox);
			this.groupBox4.Controls.Add(this.UsbSaveButton);
			this.groupBox4.Controls.Add(this.CodeTextBox);
			this.groupBox4.Controls.Add(this.SelUdiskLabel);
			this.groupBox4.Controls.Add(this.CodeButton);
			this.groupBox4.Controls.Add(this.UsbListComboBox);
			this.groupBox4.Location = new System.Drawing.Point(12, 12);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(446, 151);
			this.groupBox4.TabIndex = 14;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "U盘程序升级";
			this.label_Remind.Location = new System.Drawing.Point(21, 99);
			this.label_Remind.Name = "label_Remind";
			this.label_Remind.Size = new System.Drawing.Size(397, 43);
			this.label_Remind.TabIndex = 19;
			this.label_SelectFlash.AutoSize = true;
			this.label_SelectFlash.Location = new System.Drawing.Point(89, 17);
			this.label_SelectFlash.Name = "label_SelectFlash";
			this.label_SelectFlash.Size = new System.Drawing.Size(0, 12);
			this.label_SelectFlash.TabIndex = 18;
			this.CodeCheckBox.AutoSize = true;
			this.CodeCheckBox.Location = new System.Drawing.Point(13, 35);
			this.CodeCheckBox.Name = "CodeCheckBox";
			this.CodeCheckBox.Size = new System.Drawing.Size(72, 16);
			this.CodeCheckBox.TabIndex = 7;
			this.CodeCheckBox.Text = "升级程序";
			this.CodeCheckBox.UseVisualStyleBackColor = true;
			this.CodeCheckBox.CheckedChanged += new EventHandler(this.CodeCheckBox_CheckedChanged);
			this.UsbSaveButton.Enabled = false;
			this.UsbSaveButton.Location = new System.Drawing.Point(347, 68);
			this.UsbSaveButton.Name = "UsbSaveButton";
			this.UsbSaveButton.Size = new System.Drawing.Size(57, 23);
			this.UsbSaveButton.TabIndex = 12;
			this.UsbSaveButton.Text = "保存";
			this.UsbSaveButton.UseVisualStyleBackColor = true;
			this.UsbSaveButton.Click += new EventHandler(this.UsbSaveButton_Click);
			this.CodeTextBox.Cursor = Cursors.PanWest;
			this.CodeTextBox.Enabled = false;
			this.CodeTextBox.Location = new System.Drawing.Point(91, 33);
			this.CodeTextBox.Name = "CodeTextBox";
			this.CodeTextBox.ReadOnly = true;
			this.CodeTextBox.Size = new System.Drawing.Size(248, 21);
			this.CodeTextBox.TabIndex = 8;
			this.SelUdiskLabel.AutoSize = true;
			this.SelUdiskLabel.Location = new System.Drawing.Point(15, 76);
			this.SelUdiskLabel.Name = "SelUdiskLabel";
			this.SelUdiskLabel.Size = new System.Drawing.Size(47, 12);
			this.SelUdiskLabel.TabIndex = 11;
			this.SelUdiskLabel.Text = "选择U盘";
			this.CodeButton.Enabled = false;
			this.CodeButton.Location = new System.Drawing.Point(349, 31);
			this.CodeButton.Name = "CodeButton";
			this.CodeButton.Size = new System.Drawing.Size(55, 23);
			this.CodeButton.TabIndex = 9;
			this.CodeButton.Text = "浏览";
			this.CodeButton.UseVisualStyleBackColor = true;
			this.CodeButton.Click += new EventHandler(this.CodeButton_Click);
			this.UsbListComboBox.Cursor = Cursors.Default;
			this.UsbListComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.UsbListComboBox.Enabled = false;
			this.UsbListComboBox.FormattingEnabled = true;
			this.UsbListComboBox.ImeMode = ImeMode.On;
			this.UsbListComboBox.Location = new System.Drawing.Point(91, 71);
			this.UsbListComboBox.Name = "UsbListComboBox";
			this.UsbListComboBox.Size = new System.Drawing.Size(248, 20);
			this.UsbListComboBox.TabIndex = 10;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(469, 175);
			base.Controls.Add(this.groupBox4);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formUSBUpdate";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "U盘程序升级";
			base.Load += new EventHandler(this.formUSBUpdate_Load);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
