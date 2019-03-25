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
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formGPRSAdvancedSettings : Form
	{
		private bool isModified;

		private int WarningLevel;

		private string LastMessage = "";

		private IList<string> UsbDeviceList;

		private LedPanel panel;

		private IContainer components;

		private Panel pnlCloudServer;

		private GroupBox grpCloudServer;

		private TextBox txtCloudServerUserName;

		private Label lblCloudServerUserName;

		private CheckBox chkCloudServer;

		private Button btnCloudServerSave;

		private Label lblUsbHint;

		private ComboBox cboUsb;

		private Label lblUsb;

		private System.Windows.Forms.Timer tmrUsb;

		private CheckBox chkAPN;

		private TextBox txtAPN;

		private Label lblAPN;

		public formGPRSAdvancedSettings(LedPanel pPanel)
		{
			this.InitializeComponent();
			this.panel = pPanel;
			this.Text = formMain.ML.GetStr("formGPRSAdvancedSettings_FormText");
			this.chkCloudServer.Text = formMain.ML.GetStr("formGPRSAdvancedSettings_CheckBox_CloudServerMode");
			this.lblCloudServerUserName.Text = formMain.ML.GetStr("formGPRSAdvancedSettings_Label_CloudSeverUserName");
			this.lblAPN.Text = formMain.ML.GetStr("formGPRSAdvancedSettings_Label_APN");
			this.lblUsb.Text = formMain.ML.GetStr("formGPRSAdvancedSettings_Label_USB");
			this.btnCloudServerSave.Text = formMain.ML.GetStr("formGPRSAdvancedSettings_Button_CloudServerSave");
		}

		private void formGPRSAdvancedSettings_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			bool flag = Convert.ToBoolean((int)this.panel.CloudServerMode);
			this.chkCloudServer.Checked = flag;
			this.lblCloudServerUserName.Enabled = flag;
			this.txtCloudServerUserName.Enabled = flag;
			this.lblUsb.Enabled = flag;
			this.cboUsb.Enabled = flag;
			this.lblUsbHint.Enabled = flag;
			this.btnCloudServerSave.Enabled = flag;
			this.chkAPN.Checked = !string.IsNullOrEmpty(this.panel.GPRSAPN);
			this.chkAPN.Enabled = flag;
			this.lblAPN.Enabled = flag;
			this.txtAPN.Enabled = (flag && this.chkAPN.Checked);
			this.txtCloudServerUserName.Text = this.panel.CloudServerUserName;
			this.txtAPN.Text = this.panel.GPRSAPN;
			if (flag)
			{
				this.tmrUsb.Start();
			}
		}

		private void formGPRSAdvancedSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.isModified && MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Confirm_DiscardChanges"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK)
			{
				e.Cancel = true;
			}
			this.tmrUsb.Stop();
		}

		private void btnCloudServerSave_Click(object sender, EventArgs e)
		{
			this.tmrUsb.Stop();
			string text = this.txtCloudServerUserName.Text;
			if (string.IsNullOrEmpty(text))
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_CloudServer_UserName_Cannot_Be_Empty"));
				this.txtCloudServerUserName.Focus();
				this.tmrUsb.Start();
				return;
			}
			if (Encoding.Default.GetByteCount(text) > 30)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_CloudServer_UserName_Cannot_Be_More_Than_30_Characters"));
				this.txtCloudServerUserName.Focus();
				this.tmrUsb.Start();
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
				this.tmrUsb.Start();
				return;
			}
			string text2 = this.txtAPN.Text;
			if (this.chkAPN.Checked)
			{
				if (string.IsNullOrEmpty(text2))
				{
					MessageBox.Show(this, formMain.ML.GetStr("Message_APN_Cannot_Be_Empty"));
					this.txtAPN.Focus();
					this.tmrUsb.Start();
					return;
				}
				if (Encoding.Default.GetByteCount(text2) > 31)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Message_APN_Cannot_Be_More_Than_31_Characters"));
					this.txtAPN.Focus();
					this.tmrUsb.Start();
					return;
				}
				bool flag2 = true;
				for (int j = 0; j < text2.Length; j++)
				{
					int num2 = (int)text2[j];
					if (num2 > 122 || num2 < 46 || (num2 > 46 && num2 < 48) || (num2 > 57 && num2 < 65) || (num2 > 90 && num2 < 97))
					{
						break;
					}
				}
				if (!flag2)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Message_APN_Can_Only_Support_Alphabet_And_Decimal"));
					this.txtAPN.Focus();
					this.tmrUsb.Start();
					return;
				}
			}
			try
			{
				this.btnCloudServerSave.Enabled = false;
				this.lblUsbHint.Text = formMain.ML.GetStr("USB_SavingData");
				Thread.Sleep(500);
				this.lblUsbHint.ForeColor = System.Drawing.Color.Black;
				if (this.cboUsb.SelectedIndex == -1)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Prompt_NoFlashDisk"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
					this.tmrUsb.Start();
				}
				else
				{
					LedCardCommunication ledCardCommunication = new LedCardCommunication();
					ledCardCommunication.CloudServerMode = LedCloudServerMode.Enabled;
					ledCardCommunication.CloudServerUserName = text;
					ledCardCommunication.CloudServerHeartbeatTime = 0;
					if (this.chkAPN.Checked)
					{
						ledCardCommunication.GPRSAPN = text2;
					}
					base.Enabled = false;
					LedCommunicationSetting ledCommunicationSetting = LedCommunicationSetting.CloudServer;
					if (this.chkAPN.Checked)
					{
						ledCommunicationSetting |= LedCommunicationSetting.GPRSAPN;
					}
					ProcessUSB processUSB = new ProcessUSB();
					processUSB.PanelBytes = this.panel.ToLBytes();
					processUSB.CloudServerUserNameBytes = ledCardCommunication.ToCloudServerUserNameBytes();
					processUSB.CommunicationBytes = ledCardCommunication.ToCommunicationBytes(ledCommunicationSetting);
					processUSB.ProtocolVersion = (byte)this.panel.GetProtocolType();
					protocol_data_integration protocol_data_integration = new protocol_data_integration();
					byte[] array = protocol_data_integration.WritingData_USB_Pack(processUSB, false, false);
					if (array == null)
					{
						base.Enabled = true;
						this.lblUsbHint.ForeColor = System.Drawing.Color.Red;
						this.lblUsbHint.Text = formMain.ML.GetStr("USB_Save_Failed");
						this.btnCloudServerSave.Enabled = true;
						Thread.Sleep(1000);
						this.tmrUsb.Start();
						this.LastMessage = "save";
					}
					else if (array.Length > this.panel.GetFlashCapacity())
					{
						MessageBox.Show(this, formMain.ML.GetStr("Prompt_MemeoryOverSize"));
						base.Enabled = true;
						this.btnCloudServerSave.Enabled = true;
						Thread.Sleep(1000);
						this.tmrUsb.Start();
						this.LastMessage = "save";
					}
					else
					{
						int k = 10;
						while (k > 0)
						{
							try
							{
								k--;
								FileStream fileStream = new FileStream(this.UsbDeviceList[this.cboUsb.SelectedIndex] + "ledV3.zh3", FileMode.Create, FileAccess.Write);
								fileStream.Write(array, 0, array.Length);
								fileStream.Close();
								break;
							}
							catch (Exception ex)
							{
								if (k == 0)
								{
									this.btnCloudServerSave.Enabled = true;
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
						this.btnCloudServerSave.Enabled = true;
						this.panel.CloudServerMode = LedCloudServerMode.Enabled;
						this.panel.CloudServerUserName = ledCardCommunication.CloudServerUserName;
						if (this.chkAPN.Checked)
						{
							this.panel.GPRSAPN = ledCardCommunication.GPRSAPN;
						}
						this.isModified = false;
						base.Enabled = true;
						Thread.Sleep(1000);
						this.tmrUsb.Start();
						this.LastMessage = "save";
					}
				}
			}
			catch (Exception ex2)
			{
				this.btnCloudServerSave.Enabled = true;
				this.lblUsbHint.ForeColor = System.Drawing.Color.Red;
				this.lblUsbHint.Text = formMain.ML.GetStr("USB_Save_Failed") + "(" + ex2.Message + ")";
				base.Enabled = true;
				Thread.Sleep(1000);
				this.tmrUsb.Start();
				this.LastMessage = "save";
			}
		}

		private void chkCloudServer_CheckedChanged(object sender, EventArgs e)
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
			this.lblUsb.Enabled = @checked;
			this.cboUsb.Enabled = @checked;
			this.lblUsbHint.Enabled = @checked;
			this.lblAPN.Enabled = @checked;
			this.chkAPN.Enabled = @checked;
			this.txtAPN.Enabled = (@checked && this.chkAPN.Checked);
			this.btnCloudServerSave.Enabled = @checked;
			if (@checked)
			{
				this.tmrUsb.Start();
				return;
			}
			this.tmrUsb.Stop();
		}

		private void chkAPN_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (!checkBox.Focused)
			{
				return;
			}
			this.txtAPN.Enabled = checkBox.Checked;
		}

		private void txtAPN_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || (e.KeyChar > '9' && e.KeyChar < 'A') || (e.KeyChar > 'Z' && e.KeyChar < 'a') || e.KeyChar > 'z') && e.KeyChar != '\b' && e.KeyChar != '.')
			{
				e.Handled = true;
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
				this.btnCloudServerSave.Enabled = false;
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
				this.btnCloudServerSave.Enabled = this.IsNTFS(this.UsbDeviceList[this.cboUsb.SelectedIndex]);
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
			this.pnlCloudServer = new Panel();
			this.grpCloudServer = new GroupBox();
			this.lblUsbHint = new Label();
			this.cboUsb = new ComboBox();
			this.lblUsb = new Label();
			this.txtCloudServerUserName = new TextBox();
			this.lblCloudServerUserName = new Label();
			this.chkCloudServer = new CheckBox();
			this.btnCloudServerSave = new Button();
			this.tmrUsb = new System.Windows.Forms.Timer(this.components);
			this.lblAPN = new Label();
			this.txtAPN = new TextBox();
			this.chkAPN = new CheckBox();
			this.pnlCloudServer.SuspendLayout();
			this.grpCloudServer.SuspendLayout();
			base.SuspendLayout();
			this.pnlCloudServer.Controls.Add(this.grpCloudServer);
			this.pnlCloudServer.Location = new System.Drawing.Point(8, 12);
			this.pnlCloudServer.Name = "pnlCloudServer";
			this.pnlCloudServer.Size = new System.Drawing.Size(528, 239);
			this.pnlCloudServer.TabIndex = 24;
			this.grpCloudServer.Controls.Add(this.chkAPN);
			this.grpCloudServer.Controls.Add(this.txtAPN);
			this.grpCloudServer.Controls.Add(this.lblAPN);
			this.grpCloudServer.Controls.Add(this.lblUsbHint);
			this.grpCloudServer.Controls.Add(this.cboUsb);
			this.grpCloudServer.Controls.Add(this.lblUsb);
			this.grpCloudServer.Controls.Add(this.txtCloudServerUserName);
			this.grpCloudServer.Controls.Add(this.lblCloudServerUserName);
			this.grpCloudServer.Controls.Add(this.chkCloudServer);
			this.grpCloudServer.Controls.Add(this.btnCloudServerSave);
			this.grpCloudServer.Location = new System.Drawing.Point(7, 10);
			this.grpCloudServer.Name = "grpCloudServer";
			this.grpCloudServer.Size = new System.Drawing.Size(513, 222);
			this.grpCloudServer.TabIndex = 2;
			this.grpCloudServer.TabStop = false;
			this.lblUsbHint.Location = new System.Drawing.Point(56, 166);
			this.lblUsbHint.Name = "lblUsbHint";
			this.lblUsbHint.Size = new System.Drawing.Size(370, 45);
			this.lblUsbHint.TabIndex = 34;
			this.lblUsbHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.cboUsb.Cursor = Cursors.Default;
			this.cboUsb.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboUsb.FormattingEnabled = true;
			this.cboUsb.ImeMode = ImeMode.On;
			this.cboUsb.Location = new System.Drawing.Point(113, 133);
			this.cboUsb.Name = "cboUsb";
			this.cboUsb.Size = new System.Drawing.Size(209, 20);
			this.cboUsb.TabIndex = 32;
			this.lblUsb.Location = new System.Drawing.Point(10, 130);
			this.lblUsb.Name = "lblUsb";
			this.lblUsb.Size = new System.Drawing.Size(89, 25);
			this.lblUsb.TabIndex = 33;
			this.lblUsb.Text = "选择U盘";
			this.lblUsb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtCloudServerUserName.Location = new System.Drawing.Point(113, 35);
			this.txtCloudServerUserName.Name = "txtCloudServerUserName";
			this.txtCloudServerUserName.Size = new System.Drawing.Size(313, 21);
			this.txtCloudServerUserName.TabIndex = 31;
			this.lblCloudServerUserName.Location = new System.Drawing.Point(10, 34);
			this.lblCloudServerUserName.Name = "lblCloudServerUserName";
			this.lblCloudServerUserName.Size = new System.Drawing.Size(89, 21);
			this.lblCloudServerUserName.TabIndex = 30;
			this.lblCloudServerUserName.Text = "用户名";
			this.lblCloudServerUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkCloudServer.AutoSize = true;
			this.chkCloudServer.Location = new System.Drawing.Point(9, 0);
			this.chkCloudServer.Name = "chkCloudServer";
			this.chkCloudServer.Size = new System.Drawing.Size(96, 16);
			this.chkCloudServer.TabIndex = 18;
			this.chkCloudServer.Text = "云服务器设置";
			this.chkCloudServer.UseVisualStyleBackColor = true;
			this.chkCloudServer.CheckedChanged += new EventHandler(this.chkCloudServer_CheckedChanged);
			this.btnCloudServerSave.Location = new System.Drawing.Point(351, 131);
			this.btnCloudServerSave.Name = "btnCloudServerSave";
			this.btnCloudServerSave.Size = new System.Drawing.Size(75, 23);
			this.btnCloudServerSave.TabIndex = 8;
			this.btnCloudServerSave.Text = "保存";
			this.btnCloudServerSave.UseVisualStyleBackColor = true;
			this.btnCloudServerSave.Click += new EventHandler(this.btnCloudServerSave_Click);
			this.tmrUsb.Interval = 1000;
			this.tmrUsb.Tick += new EventHandler(this.tmrUsb_Tick);
			this.lblAPN.Location = new System.Drawing.Point(10, 84);
			this.lblAPN.Name = "lblAPN";
			this.lblAPN.Size = new System.Drawing.Size(89, 21);
			this.lblAPN.TabIndex = 35;
			this.lblAPN.Text = "APN";
			this.lblAPN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtAPN.Location = new System.Drawing.Point(113, 84);
			this.txtAPN.Name = "txtAPN";
			this.txtAPN.Size = new System.Drawing.Size(292, 21);
			this.txtAPN.TabIndex = 36;
			this.txtAPN.KeyPress += new KeyPressEventHandler(this.txtAPN_KeyPress);
			this.chkAPN.AutoSize = true;
			this.chkAPN.Location = new System.Drawing.Point(411, 87);
			this.chkAPN.Name = "chkAPN";
			this.chkAPN.Size = new System.Drawing.Size(15, 14);
			this.chkAPN.TabIndex = 37;
			this.chkAPN.UseVisualStyleBackColor = true;
			this.chkAPN.CheckedChanged += new EventHandler(this.chkAPN_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(548, 259);
			base.Controls.Add(this.pnlCloudServer);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGPRSAdvancedSettings";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "控制卡网络设置";
			base.FormClosing += new FormClosingEventHandler(this.formGPRSAdvancedSettings_FormClosing);
			base.Load += new EventHandler(this.formGPRSAdvancedSettings_Load);
			this.pnlCloudServer.ResumeLayout(false);
			this.grpCloudServer.ResumeLayout(false);
			this.grpCloudServer.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
