using LedCommunication;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Data;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formTimeSequence : Form
	{
		private formMain fm;

		private LedTimeSequence timeSequence;

		private LedPanel panel;

		private string LastMessage = "";

		private IList<string> UdiskDirList;

		private IContainer components;

		private Label lblCKCPHA;

		private Label lblCKCDC;

		private Label lblLTDelay;

		private NumericUpDown nudCKCPHA;

		private NumericUpDown nudCKCDC;

		private NumericUpDown nudLTDelay;

		private Button btnRead;

		private Button btnSetting;

		private CheckBox checkBox_Udisk_Update;

		private Panel panel_Comm_NOW;

		private Panel panel_Udisk_Save;

		private Label label_Remind;

		private Button UsbSaveButton;

		private ComboBox UsbListComboBox;

		private Label SelUdiskLabel;

		private System.Windows.Forms.Timer timer1;

		public formTimeSequence(LedTimeSequence pTimeSequence, formMain pForm)
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formTimeSequence_FormText");
			this.lblCKCDC.Text = formMain.ML.GetStr("formTimeSequence_label_lblCKCDC");
			this.lblCKCPHA.Text = formMain.ML.GetStr("formTimeSequence_label_lblCKCPHA");
			this.lblLTDelay.Text = formMain.ML.GetStr("formTimeSequence_label_lblLTDelay");
			this.btnRead.Text = formMain.ML.GetStr("formTimeSequence_Button_btnRead");
			this.btnSetting.Text = formMain.ML.GetStr("formTimeSequence_Button_btnSetting");
			this.SelUdiskLabel.Text = formMain.ML.GetStr("formUSBWrite_label_SelUdisk");
			this.checkBox_Udisk_Update.Text = formMain.ML.GetStr("formTimeSequence_checkBox_UdiskUpdate");
			this.UsbSaveButton.Text = formMain.ML.GetStr("UpdateButton_Save");
			this.panel = formMain.Ledsys.SelectedPanel;
			this.timeSequence = pTimeSequence;
			this.fm = pForm;
			this.panel_Udisk_Save.Visible = false;
			base.Size = new System.Drawing.Size(356, 365);
		}

		private void formTimeSequence_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			if (this.timeSequence != null)
			{
				this.nudCKCPHA.Value = this.timeSequence.CKCPHA;
				this.nudCKCDC.Value = this.timeSequence.CKCDC;
				this.nudLTDelay.Value = this.timeSequence.LTDelay;
			}
		}

		private void btnRead_Click(object sender, EventArgs e)
		{
			this.fm.SendSingleCmdStart(LedCmdType.Read_Time_Sequence, null, formMain.ML.GetStr("formTimeSequence_Button_btnRead"), formMain.ledsys.SelectedPanel, false, this);
			if (formSendSingle.LastSendResult && formSendSingle.LastSendResultObject != null && formSendSingle.LastSendResultObject.GetType() == typeof(LedTimeSequence))
			{
				LedTimeSequence ledTimeSequence = (LedTimeSequence)formSendSingle.LastSendResultObject;
				this.nudCKCPHA.Value = ledTimeSequence.CKCPHA;
				this.nudCKCDC.Value = ledTimeSequence.CKCDC;
				this.nudLTDelay.Value = ledTimeSequence.LTDelay;
			}
		}

		private void btnSetting_Click(object sender, EventArgs e)
		{
			LedTimeSequence ledTimeSequence = new LedTimeSequence();
			ledTimeSequence.CKCPHA = (byte)this.nudCKCPHA.Value;
			ledTimeSequence.CKCDC = (byte)this.nudCKCDC.Value;
			ledTimeSequence.LTDelay = (int)this.nudLTDelay.Value;
			this.fm.SendSingleCmdStart(LedCmdType.Ctrl_Time_Sequence, ledTimeSequence, formMain.ML.GetStr("formTimeSequence_FormText"), formMain.ledsys.SelectedPanel, true, this);
			if (formSendSingle.LastSendResult)
			{
				this.timeSequence.CKCPHA = ledTimeSequence.CKCPHA;
				this.timeSequence.CKCDC = ledTimeSequence.CKCDC;
				this.timeSequence.LTDelay = ledTimeSequence.LTDelay;
				this.timeSequence.Changed = true;
			}
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
				if (this.UsbListComboBox.SelectedIndex == -1)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_NoFlashDisk"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				}
				else
				{
					LedTimeSequence ledTimeSequence = new LedTimeSequence();
					ledTimeSequence.CKCPHA = (byte)this.nudCKCPHA.Value;
					ledTimeSequence.CKCDC = (byte)this.nudCKCDC.Value;
					ledTimeSequence.LTDelay = (int)this.nudLTDelay.Value;
					this.panel.TimeSequence.CKCPHA = ledTimeSequence.CKCPHA;
					this.panel.TimeSequence.CKCDC = ledTimeSequence.CKCDC;
					this.panel.TimeSequence.LTDelay = ledTimeSequence.LTDelay;
					this.panel.TimeSequence.Changed = true;
					base.Enabled = false;
					ProcessUSB processUSB = new ProcessUSB();
					processUSB.PanelBytes = this.panel.ToLBytes();
					processUSB.TimeSequenceBytes = this.panel.TimeSequence.ToBytes();
					processUSB.ProtocolVersion = this.panel.ProtocolVersion;
					protocol_data_integration protocol_data_integration = new protocol_data_integration();
					byte[] array = protocol_data_integration.WritingData_USB_Pack(processUSB, true, false);
					if (array == null)
					{
						base.Enabled = true;
						this.label_Remind.ForeColor = System.Drawing.Color.Red;
						this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Failed");
						this.UsbSaveButton.Enabled = true;
						Thread.Sleep(1000);
						this.timer1.Start();
						this.LastMessage = "save";
					}
					else if (array.Length > this.panel.GetFlashCapacity())
					{
						MessageBox.Show(this, formMain.ML.GetStr("Prompt_MemeoryOverSize"));
						base.Enabled = true;
						this.UsbSaveButton.Enabled = true;
						Thread.Sleep(1000);
						this.timer1.Start();
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
									this.UsbSaveButton.Enabled = true;
									this.label_Remind.ForeColor = System.Drawing.Color.Red;
									this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Failed") + "(" + ex.Message + ")";
									base.Enabled = true;
									Thread.Sleep(1000);
									this.timer1.Start();
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
						this.UsbSaveButton.Enabled = true;
						base.Enabled = true;
						Thread.Sleep(1000);
						this.timer1.Start();
						this.LastMessage = "save";
					}
				}
			}
			catch (Exception ex2)
			{
				this.UsbSaveButton.Enabled = true;
				this.label_Remind.ForeColor = System.Drawing.Color.Red;
				this.label_Remind.Text = formMain.ML.GetStr("USB_Save_Failed") + "(" + ex2.Message + ")";
				base.Enabled = true;
				Thread.Sleep(1000);
				this.timer1.Start();
				this.LastMessage = "save";
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.updataUdiskList();
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

		private void checkBox_Udisk_Update_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBox_Udisk_Update.Checked)
			{
				this.timer1.Start();
				this.panel_Comm_NOW.Visible = false;
				this.panel_Udisk_Save.Visible = true;
				this.panel_Udisk_Save.Location = new System.Drawing.Point(7, 266);
				base.Size = new System.Drawing.Size(356, 400);
				return;
			}
			this.timer1.Stop();
			this.panel_Comm_NOW.Visible = true;
			this.panel_Udisk_Save.Visible = false;
			this.panel_Comm_NOW.Location = new System.Drawing.Point(7, 266);
			base.Size = new System.Drawing.Size(356, 365);
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
			this.lblCKCPHA = new Label();
			this.lblCKCDC = new Label();
			this.lblLTDelay = new Label();
			this.nudCKCPHA = new NumericUpDown();
			this.nudCKCDC = new NumericUpDown();
			this.nudLTDelay = new NumericUpDown();
			this.btnRead = new Button();
			this.btnSetting = new Button();
			this.checkBox_Udisk_Update = new CheckBox();
			this.panel_Comm_NOW = new Panel();
			this.panel_Udisk_Save = new Panel();
			this.label_Remind = new Label();
			this.UsbSaveButton = new Button();
			this.UsbListComboBox = new ComboBox();
			this.SelUdiskLabel = new Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((ISupportInitialize)this.nudCKCPHA).BeginInit();
			((ISupportInitialize)this.nudCKCDC).BeginInit();
			((ISupportInitialize)this.nudLTDelay).BeginInit();
			this.panel_Comm_NOW.SuspendLayout();
			this.panel_Udisk_Save.SuspendLayout();
			base.SuspendLayout();
			this.lblCKCPHA.AutoSize = true;
			this.lblCKCPHA.Location = new System.Drawing.Point(45, 44);
			this.lblCKCPHA.Name = "lblCKCPHA";
			this.lblCKCPHA.Size = new System.Drawing.Size(77, 12);
			this.lblCKCPHA.TabIndex = 0;
			this.lblCKCPHA.Text = "CK时钟相位：";
			this.lblCKCDC.AutoSize = true;
			this.lblCKCDC.Location = new System.Drawing.Point(33, 117);
			this.lblCKCDC.Name = "lblCKCDC";
			this.lblCKCDC.Size = new System.Drawing.Size(89, 12);
			this.lblCKCDC.TabIndex = 1;
			this.lblCKCDC.Text = "CK时钟占空比：";
			this.lblLTDelay.AutoSize = true;
			this.lblLTDelay.Location = new System.Drawing.Point(69, 191);
			this.lblLTDelay.Name = "lblLTDelay";
			this.lblLTDelay.Size = new System.Drawing.Size(53, 12);
			this.lblLTDelay.TabIndex = 2;
			this.lblLTDelay.Text = "LT延时：";
			this.nudCKCPHA.Location = new System.Drawing.Point(176, 42);
			NumericUpDown arg_250_0 = this.nudCKCPHA;
			int[] array = new int[4];
			array[0] = 99;
			arg_250_0.Maximum = new decimal(array);
			NumericUpDown arg_26C_0 = this.nudCKCPHA;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_26C_0.Minimum = new decimal(array2);
			this.nudCKCPHA.Name = "nudCKCPHA";
			this.nudCKCPHA.Size = new System.Drawing.Size(120, 21);
			this.nudCKCPHA.TabIndex = 3;
			NumericUpDown arg_2B8_0 = this.nudCKCPHA;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_2B8_0.Value = new decimal(array3);
			this.nudCKCDC.Location = new System.Drawing.Point(176, 115);
			NumericUpDown arg_2EC_0 = this.nudCKCDC;
			int[] array4 = new int[4];
			array4[0] = 99;
			arg_2EC_0.Maximum = new decimal(array4);
			NumericUpDown arg_30B_0 = this.nudCKCDC;
			int[] array5 = new int[4];
			array5[0] = 1;
			arg_30B_0.Minimum = new decimal(array5);
			this.nudCKCDC.Name = "nudCKCDC";
			this.nudCKCDC.Size = new System.Drawing.Size(120, 21);
			this.nudCKCDC.TabIndex = 4;
			NumericUpDown arg_35A_0 = this.nudCKCDC;
			int[] array6 = new int[4];
			array6[0] = 1;
			arg_35A_0.Value = new decimal(array6);
			this.nudLTDelay.Location = new System.Drawing.Point(176, 189);
			NumericUpDown arg_397_0 = this.nudLTDelay;
			int[] array7 = new int[4];
			array7[0] = 65535;
			arg_397_0.Maximum = new decimal(array7);
			this.nudLTDelay.Name = "nudLTDelay";
			this.nudLTDelay.Size = new System.Drawing.Size(120, 21);
			this.nudLTDelay.TabIndex = 5;
			this.btnRead.Location = new System.Drawing.Point(172, 11);
			this.btnRead.Name = "btnRead";
			this.btnRead.Size = new System.Drawing.Size(54, 23);
			this.btnRead.TabIndex = 6;
			this.btnRead.Text = "回读";
			this.btnRead.UseVisualStyleBackColor = true;
			this.btnRead.Click += new EventHandler(this.btnRead_Click);
			this.btnSetting.Location = new System.Drawing.Point(242, 11);
			this.btnSetting.Name = "btnSetting";
			this.btnSetting.Size = new System.Drawing.Size(54, 23);
			this.btnSetting.TabIndex = 7;
			this.btnSetting.Text = "设置";
			this.btnSetting.UseVisualStyleBackColor = true;
			this.btnSetting.Click += new EventHandler(this.btnSetting_Click);
			this.checkBox_Udisk_Update.AutoSize = true;
			this.checkBox_Udisk_Update.Location = new System.Drawing.Point(12, 242);
			this.checkBox_Udisk_Update.Name = "checkBox_Udisk_Update";
			this.checkBox_Udisk_Update.Size = new System.Drawing.Size(90, 16);
			this.checkBox_Udisk_Update.TabIndex = 10;
			this.checkBox_Udisk_Update.Text = "U盘时序更新";
			this.checkBox_Udisk_Update.UseVisualStyleBackColor = true;
			this.checkBox_Udisk_Update.CheckedChanged += new EventHandler(this.checkBox_Udisk_Update_CheckedChanged);
			this.panel_Comm_NOW.Controls.Add(this.btnRead);
			this.panel_Comm_NOW.Controls.Add(this.btnSetting);
			this.panel_Comm_NOW.Location = new System.Drawing.Point(7, 266);
			this.panel_Comm_NOW.Name = "panel_Comm_NOW";
			this.panel_Comm_NOW.Size = new System.Drawing.Size(312, 44);
			this.panel_Comm_NOW.TabIndex = 11;
			this.panel_Udisk_Save.Controls.Add(this.label_Remind);
			this.panel_Udisk_Save.Controls.Add(this.UsbSaveButton);
			this.panel_Udisk_Save.Controls.Add(this.UsbListComboBox);
			this.panel_Udisk_Save.Controls.Add(this.SelUdiskLabel);
			this.panel_Udisk_Save.Location = new System.Drawing.Point(7, 340);
			this.panel_Udisk_Save.Name = "panel_Udisk_Save";
			this.panel_Udisk_Save.Size = new System.Drawing.Size(312, 115);
			this.panel_Udisk_Save.TabIndex = 12;
			this.label_Remind.Location = new System.Drawing.Point(17, 48);
			this.label_Remind.Name = "label_Remind";
			this.label_Remind.Size = new System.Drawing.Size(279, 45);
			this.label_Remind.TabIndex = 22;
			this.label_Remind.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.UsbSaveButton.Location = new System.Drawing.Point(242, 16);
			this.UsbSaveButton.Name = "UsbSaveButton";
			this.UsbSaveButton.Size = new System.Drawing.Size(54, 23);
			this.UsbSaveButton.TabIndex = 20;
			this.UsbSaveButton.Text = "保存";
			this.UsbSaveButton.UseVisualStyleBackColor = true;
			this.UsbSaveButton.Click += new EventHandler(this.UsbSaveButton_Click);
			this.UsbListComboBox.Cursor = Cursors.Default;
			this.UsbListComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.UsbListComboBox.FormattingEnabled = true;
			this.UsbListComboBox.ImeMode = ImeMode.On;
			this.UsbListComboBox.Location = new System.Drawing.Point(72, 18);
			this.UsbListComboBox.Name = "UsbListComboBox";
			this.UsbListComboBox.Size = new System.Drawing.Size(154, 20);
			this.UsbListComboBox.TabIndex = 18;
			this.SelUdiskLabel.Location = new System.Drawing.Point(15, 15);
			this.SelUdiskLabel.Name = "SelUdiskLabel";
			this.SelUdiskLabel.Size = new System.Drawing.Size(47, 25);
			this.SelUdiskLabel.TabIndex = 19;
			this.SelUdiskLabel.Text = "选择U盘";
			this.SelUdiskLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(340, 464);
			base.Controls.Add(this.panel_Udisk_Save);
			base.Controls.Add(this.panel_Comm_NOW);
			base.Controls.Add(this.checkBox_Udisk_Update);
			base.Controls.Add(this.nudLTDelay);
			base.Controls.Add(this.nudCKCDC);
			base.Controls.Add(this.nudCKCPHA);
			base.Controls.Add(this.lblLTDelay);
			base.Controls.Add(this.lblCKCDC);
			base.Controls.Add(this.lblCKCPHA);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formTimeSequence";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "时序设置";
			base.Load += new EventHandler(this.formTimeSequence_Load);
			((ISupportInitialize)this.nudCKCPHA).EndInit();
			((ISupportInitialize)this.nudCKCDC).EndInit();
			((ISupportInitialize)this.nudLTDelay).EndInit();
			this.panel_Comm_NOW.ResumeLayout(false);
			this.panel_Udisk_Save.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
