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

namespace LedControlSystem.LedControlSystem
{
	public class formButtonFunctionConfiguration : Form
	{
		private int WarningLevel;

		private string LastMessage = "";

		private IList<string> UsbDeviceList;

		private LedPanel panel;

		private IContainer components;

		private Label lblOption;

		private ComboBox cboOption;

		private Label lblUsbHint;

		private ComboBox cboUsb;

		private Label lblUsb;

		private Button btnSave;

		private System.Windows.Forms.Timer tmrUsb;

		public formButtonFunctionConfiguration()
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formButtonFunctionConfiguration_FormText");
			this.lblOption.Text = formMain.ML.GetStr("formButtonFunctionConfiguration_Label_Option");
			this.lblUsb.Text = formMain.ML.GetStr("formUSBWrite_label_SelUdisk");
			this.btnSave.Text = formMain.ML.GetStr("UpdateButton_Save");
			this.cboOption.Items.Clear();
			this.cboOption.Items.Add("测试功能");
			this.cboOption.Items.Add("按钮控制循环切换节目");
			this.cboOption.Items.Add("电平控制切换节目");
			formMain.str_item_comboBox(this.cboOption, "formButtonFunctionConfiguration_ComboBox_Option", null);
			this.panel = formMain.Ledsys.SelectedPanel;
		}

		private void formButtonFunctionConfiguration_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.cboOption.SelectedIndex = (int)this.panel.ButtonFunctionType;
			this.tmrUsb.Start();
		}

		private void formButtonFunctionConfiguration_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.tmrUsb.Stop();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			this.tmrUsb.Stop();
			try
			{
				this.btnSave.Enabled = false;
				this.lblUsbHint.Text = formMain.ML.GetStr("USB_SavingData");
				Thread.Sleep(500);
				this.lblUsbHint.ForeColor = System.Drawing.Color.Black;
				if (this.cboUsb.SelectedIndex == -1)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_NoFlashDisk"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				}
				else
				{
					this.panel.ButtonFunctionType = (LedButtonFunctionType)this.cboOption.SelectedIndex;
					base.Enabled = false;
					ProcessUSB processUSB = new ProcessUSB();
					processUSB.PanelBytes = this.panel.ToLBytes();
					processUSB.ButtonFunctionBytes = this.panel.ToButtonFunctionBytes();
					processUSB.ProtocolVersion = (byte)this.panel.GetProtocolType();
					protocol_data_integration protocol_data_integration = new protocol_data_integration();
					byte[] array = protocol_data_integration.WritingData_USB_Pack(processUSB, false, true);
					if (array == null)
					{
						base.Enabled = true;
						this.lblUsbHint.ForeColor = System.Drawing.Color.Red;
						this.lblUsbHint.Text = formMain.ML.GetStr("USB_Save_Failed");
						this.btnSave.Enabled = true;
						Thread.Sleep(1000);
						this.tmrUsb.Start();
						this.LastMessage = "save";
					}
					else if (array.Length > this.panel.GetFlashCapacity())
					{
						MessageBox.Show(this, formMain.ML.GetStr("Prompt_MemeoryOverSize"));
						base.Enabled = true;
						this.btnSave.Enabled = true;
						Thread.Sleep(1000);
						this.tmrUsb.Start();
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
								FileStream fileStream = new FileStream(this.UsbDeviceList[this.cboUsb.SelectedIndex] + "ledV3.zh3", FileMode.Create, FileAccess.Write);
								fileStream.Write(array, 0, array.Length);
								fileStream.Close();
								break;
							}
							catch (Exception ex)
							{
								if (i == 0)
								{
									this.btnSave.Enabled = true;
									this.lblUsbHint.ForeColor = System.Drawing.Color.Red;
									this.lblUsbHint.Text = formMain.ML.GetStr("USB_Save_Failed") + "(" + ex.Message + ")";
									base.Enabled = true;
									Thread.Sleep(1000);
									this.tmrUsb.Start();
									this.LastMessage = "save";
									return;
								}
								Thread.Sleep(200);
							}
						}
						Thread.Sleep(500);
						if (!File.Exists(this.UsbDeviceList[this.cboUsb.SelectedIndex] + "\\ledV3.zh3"))
						{
							MessageBox.Show(this, formMain.ML.GetStr("Prompt_DataDownloadFailed"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
						}
						this.lblUsbHint.ForeColor = System.Drawing.Color.Blue;
						this.lblUsbHint.Text = formMain.ML.GetStr("USB_Save_Success");
						this.btnSave.Enabled = true;
						base.Enabled = true;
						Thread.Sleep(1000);
						this.tmrUsb.Start();
						this.LastMessage = "save";
					}
				}
			}
			catch (Exception ex2)
			{
				this.btnSave.Enabled = true;
				this.lblUsbHint.ForeColor = System.Drawing.Color.Red;
				this.lblUsbHint.Text = formMain.ML.GetStr("USB_Save_Failed") + "(" + ex2.Message + ")";
				base.Enabled = true;
				Thread.Sleep(1000);
				this.tmrUsb.Start();
				this.LastMessage = "save";
			}
		}

		private void tmrUsb_Tick(object sender, EventArgs e)
		{
			this.UpdataUsbList();
		}

		public void UpdataUsbList()
		{
			int selectedIndex = this.cboUsb.SelectedIndex;
			DriveInfo[] drives = DriveInfo.GetDrives();
			new List<string>();
			this.UsbDeviceList = new List<string>();
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
						this.UsbDeviceList.Add(drives[i].Name);
						num++;
					}
					catch
					{
					}
				}
			}
			if (list.Count != this.cboUsb.Items.Count)
			{
				this.cboUsb.Items.Clear();
				foreach (string current in list)
				{
					this.cboUsb.Items.Add(current);
				}
			}
			if (this.UsbDeviceList.Count >= selectedIndex + 1)
			{
				this.cboUsb.SelectedIndex = selectedIndex;
			}
			else if (this.UsbDeviceList.Count > 0)
			{
				this.cboUsb.SelectedIndex = 0;
			}
			bool flag = false;
			if (this.UsbDeviceList.Count == 0)
			{
				this.btnSave.Enabled = false;
				this.lblUsbHint.Text = "";
			}
			else
			{
				flag = true;
			}
			if (this.UsbDeviceList.Count == 1)
			{
				this.cboUsb.SelectedIndex = 0;
			}
			if (flag)
			{
				if (this.cboUsb.SelectedIndex == -1)
				{
					this.cboUsb.SelectedIndex = 0;
				}
				this.btnSave.Enabled = this.IsNTFS(this.UsbDeviceList[this.cboUsb.SelectedIndex]);
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
							this.lblUsbHint.Text = "";
						}
					}
					else if (this.WarningLevel < 2)
					{
						this.lblUsbHint.Text = formMain.ML.GetStr("USB_NotFAT32");
						this.lblUsbHint.ForeColor = System.Drawing.Color.Orange;
						this.WarningLevel = 1;
					}
					result = true;
				}
				else
				{
					this.lblUsbHint.Text = formMain.ML.GetStr("USB_NotSupportFormat");
					this.lblUsbHint.ForeColor = System.Drawing.Color.Red;
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
			this.lblOption = new Label();
			this.cboOption = new ComboBox();
			this.lblUsbHint = new Label();
			this.cboUsb = new ComboBox();
			this.lblUsb = new Label();
			this.btnSave = new Button();
			this.tmrUsb = new System.Windows.Forms.Timer(this.components);
			base.SuspendLayout();
			this.lblOption.Location = new System.Drawing.Point(14, 22);
			this.lblOption.Name = "lblOption";
			this.lblOption.Size = new System.Drawing.Size(45, 20);
			this.lblOption.TabIndex = 0;
			this.lblOption.Text = "选项";
			this.lblOption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cboOption.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboOption.FormattingEnabled = true;
			this.cboOption.Location = new System.Drawing.Point(69, 23);
			this.cboOption.Name = "cboOption";
			this.cboOption.Size = new System.Drawing.Size(274, 20);
			this.cboOption.TabIndex = 1;
			this.lblUsbHint.Location = new System.Drawing.Point(12, 101);
			this.lblUsbHint.Name = "lblUsbHint";
			this.lblUsbHint.Size = new System.Drawing.Size(331, 45);
			this.lblUsbHint.TabIndex = 22;
			this.lblUsbHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cboUsb.Cursor = Cursors.Default;
			this.cboUsb.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboUsb.FormattingEnabled = true;
			this.cboUsb.ImeMode = ImeMode.On;
			this.cboUsb.Location = new System.Drawing.Point(69, 69);
			this.cboUsb.Name = "cboUsb";
			this.cboUsb.Size = new System.Drawing.Size(193, 20);
			this.cboUsb.TabIndex = 18;
			this.lblUsb.Location = new System.Drawing.Point(12, 66);
			this.lblUsb.Name = "lblUsb";
			this.lblUsb.Size = new System.Drawing.Size(47, 25);
			this.lblUsb.TabIndex = 19;
			this.lblUsb.Text = "选择U盘";
			this.lblUsb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnSave.Location = new System.Drawing.Point(268, 67);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 20;
			this.btnSave.Text = "保存";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new EventHandler(this.btnSave_Click);
			this.tmrUsb.Interval = 1000;
			this.tmrUsb.Tick += new EventHandler(this.tmrUsb_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(360, 164);
			base.Controls.Add(this.lblUsbHint);
			base.Controls.Add(this.cboUsb);
			base.Controls.Add(this.lblUsb);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.cboOption);
			base.Controls.Add(this.lblOption);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formButtonFunctionConfiguration";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "按钮功能配置";
			base.FormClosing += new FormClosingEventHandler(this.formButtonFunctionConfiguration_FormClosing);
			base.Load += new EventHandler(this.formButtonFunctionConfiguration_Load);
			base.ResumeLayout(false);
		}
	}
}
