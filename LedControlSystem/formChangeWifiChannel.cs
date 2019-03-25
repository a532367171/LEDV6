using HelloRemoting;
using LedControlSystem.Properties;
using LedModel.Enum;
using server_interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formChangeWifiChannel : Form
	{
		private formMain fm;

		private formConnectToWIFI fw;

		private static string formID = "formChangeWifiChannel";

		public static bool LastSendResult = false;

		public static bool LastResult = false;

		private IContainer components;

		private ComboBox comboBox1;

		private Button button_Cancel;

		private Button button_OK;

		private Timer timer1;

		private Label label1;

		private Label label2;

		private Label label3;

		public static string FormID
		{
			get
			{
				return formChangeWifiChannel.formID;
			}
			set
			{
				formChangeWifiChannel.formID = value;
			}
		}

		public formChangeWifiChannel()
		{
			this.InitializeComponent();
			this.Load_Parameters();
		}

		public void Load_Parameters()
		{
			this.Text = formMain.ML.GetStr("formChangeWifiChannel_FormText");
			this.label2.Text = formMain.ML.GetStr("formChangeWifiChannel_label_NowChannel");
			this.label1.Text = formMain.ML.GetStr("formChangeWifiChannel_label_NewChannel");
			this.button_OK.Text = formMain.ML.GetStr("formChangeWifiChannel_button_OK");
			this.button_Cancel.Text = formMain.ML.GetStr("formChangeWifiChannel_button_Cancel");
		}

		public formChangeWifiChannel(formMain pMain, formConnectToWIFI pWifi)
		{
			this.InitializeComponent();
			this.Load_Parameters();
			this.fm = pMain;
			this.fw = pWifi;
		}

		private void formChangeWifiChannel_Load(object sender, EventArgs e)
		{
			try
			{
				if (Program.IsforeignTradeMode)
				{
					base.Icon = Resources.AppIconV5;
				}
				else
				{
					base.Icon = Resources.AppIcon;
				}
				call.OnDeviceCmdReturnResult += new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
				this.ReadVersion();
			}
			catch
			{
			}
		}

		private void OnDeviceCmdReturn(object sender, DeviceCmdEventArgs arg)
		{
			IPC_SIMPLE_ANSWER isa = arg.isa;
			if (isa == null)
			{
				return;
			}
			if ((byte)isa.cmd_id == 85 && !isa.is_cmd_failed_flag && isa.is_cmd_over_flag && isa.return_object != null)
			{
				this.label3.Text = isa.return_object.ToString();
				this.comboBox1.Text = this.label3.Text;
			}
		}

		private void ReadVersion()
		{
			if (formMain.IServer != null)
			{
				formMain.IServer.send_cmd_to_device_async(85, null, formMain.ledsys.SelectedPanel.ProductID);
			}
		}

		private void UpdateVersionText(string pVersion)
		{
			this.label3.Text = pVersion;
		}

		private void button_OK_Click(object sender, EventArgs e)
		{
			string currentWIfiSSID = formConnectToWIFI.GetCurrentWIfiSSID();
			if (currentWIfiSSID.StartsWith("ZH-W") || currentWIfiSSID.StartsWith("ZH-5W"))
			{
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_WiFi_Channel, this.comboBox1.Text, this.Text, formMain.ledsys.SelectedPanel, false, this);
				if (formSendSingle.LastSendResult)
				{
					MessageBox.Show(this, this.Text + formMain.ML.GetStr("Display_Successed"));
					formChangeWifiChannel.LastResult = true;
					return;
				}
			}
			else
			{
				MessageBox.Show(this, this.Text + formMain.ML.GetStr("Display_Failed"));
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
		}

		private void button_Cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void formChangeWifiChannel_FormClosing(object sender, FormClosingEventArgs e)
		{
			call.OnDeviceCmdReturnResult -= new EventHandler<DeviceCmdEventArgs>(this.OnDeviceCmdReturn);
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
			this.comboBox1 = new ComboBox();
			this.button_Cancel = new Button();
			this.button_OK = new Button();
			this.timer1 = new Timer(this.components);
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			base.SuspendLayout();
			this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11"
			});
			this.comboBox1.Location = new System.Drawing.Point(147, 44);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 20);
			this.comboBox1.TabIndex = 0;
			this.button_Cancel.Location = new System.Drawing.Point(182, 90);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.button_Cancel.TabIndex = 10;
			this.button_Cancel.Text = "取消";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new EventHandler(this.button_Cancel_Click);
			this.button_OK.Location = new System.Drawing.Point(101, 90);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(75, 23);
			this.button_OK.TabIndex = 9;
			this.button_OK.Text = "确定";
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new EventHandler(this.button_OK_Click);
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(81, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 11;
			this.label1.Text = "新信道：";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(81, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "当前信道：";
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(145, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(23, 12);
			this.label3.TabIndex = 13;
			this.label3.Text = "- -";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(361, 125);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_OK);
			base.Controls.Add(this.comboBox1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formChangeWifiChannel";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "修改WIFI信道";
			base.FormClosing += new FormClosingEventHandler(this.formChangeWifiChannel_FormClosing);
			base.Load += new EventHandler(this.formChangeWifiChannel_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
