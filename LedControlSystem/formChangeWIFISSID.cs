using LedControlSystem.Properties;
using LedModel.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formChangeWIFISSID : Form
	{
		private formMain fm;

		private string oldSSID;

		private formConnectToWIFI fw;

		public static bool isFinish = false;

		public static bool isSuccess = false;

		private static string formID = "formGPRS_ChangeWifiSSID";

		public static bool LastResult = false;

		private IContainer components;

		private Label label1;

		private TextBox textBox1;

		private TextBox textBox_NewSSID;

		private Label label2;

		private TextBox textBox_NewSSID_Confrim;

		private Label label3;

		private TextBox textBox4;

		private TextBox textBox5;

		private Button button_OK;

		private Button button_Cancel;

		private Label label5;

		private Timer timer1;

		public static string FormID
		{
			get
			{
				return formChangeWIFISSID.formID;
			}
			set
			{
				formChangeWIFISSID.formID = value;
			}
		}

		public formChangeWIFISSID(formMain pFormmain, string pOldWIFISSID, formConnectToWIFI pFW)
		{
			this.InitializeComponent();
			this.fm = pFormmain;
			this.fw = pFW;
			this.oldSSID = pOldWIFISSID;
			formMain.ML.NowFormID = formChangeWIFISSID.formID;
			this.Text = formMain.ML.GetStr("formGPRS_ChangeWifiSSID_FormText");
			this.label1.Text = formMain.ML.GetStr("formGPRS_ChangeWifiSSID_label_NowSSID");
			this.label2.Text = formMain.ML.GetStr("formGPRS_ChangeWifiSSID_label_NewSSID");
			this.label3.Text = formMain.ML.GetStr("formGPRS_ChangeWifiSSID_label_confirm_NewSSID");
			this.label5.Text = formMain.ML.GetStr("formGPRS_ChangeWifiSSID_label_prompt");
			this.button_OK.Text = formMain.ML.GetStr("formGPRS_ChangeWifiSSID_button_OK");
			this.button_Cancel.Text = formMain.ML.GetStr("formGPRS_ChangeWifiSSID_button_Cancel");
		}

		public formChangeWIFISSID()
		{
		}

		private void formChangeWIFISSID_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.textBox1.Text = this.oldSSID;
			int num = this.oldSSID.LastIndexOf('_');
			if (num > 0)
			{
				this.textBox5.Text = this.oldSSID.Substring(0, num + 1);
				this.textBox4.Text = this.oldSSID.Substring(0, num + 1);
			}
		}

		private void button_Cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void button_OK_Click(object sender, EventArgs e)
		{
			if (this.textBox_NewSSID.Text.Length == 0 || this.textBox_NewSSID.Text.Length > 32 - this.textBox5.Text.Length - 1)
			{
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_ErrorSSIDLength"));
				return;
			}
			if (this.textBox_NewSSID.Text != this.textBox_NewSSID_Confrim.Text)
			{
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_ErrorConfirmPassword"));
				return;
			}
			string objData = this.textBox4.Text + this.textBox_NewSSID.Text;
			if (this.textBox4.Text.StartsWith("ZH-W") || this.textBox4.Text.StartsWith("ZH-5W"))
			{
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_WiFi_SSID, objData, formMain.ML.GetStr("WIFI_ChangeSSIDOperation"), formMain.ledsys.SelectedPanel, false, this);
				if (formSendSingle.LastSendResult)
				{
					MessageBox.Show(this, formMain.ML.GetStr("WIFI_ChangeSSIDOperation") + formMain.ML.GetStr("Display_Successed"));
					formChangeWIFISSID.isSuccess = true;
					formChangeWIFISSID.LastResult = true;
					formChangeWIFISSID.isFinish = true;
					return;
				}
			}
			else
			{
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_ChangeSSIDOperation") + formMain.ML.GetStr("Display_Failed"));
			}
		}

		private void textBox_NewSSID_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '_' | e.KeyChar == '\b')
			{
				return;
			}
			if (e.KeyChar > '/' && e.KeyChar < ':')
			{
				return;
			}
			if (e.KeyChar > '`' && e.KeyChar < '{')
			{
				return;
			}
			if (e.KeyChar > '@' && e.KeyChar < '[')
			{
				return;
			}
			e.Handled = true;
		}

		private void textBox_NewSSID_Confrim_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '_' | e.KeyChar == '\b')
			{
				return;
			}
			if (e.KeyChar > '/' && e.KeyChar < ':')
			{
				return;
			}
			if (e.KeyChar > '`' && e.KeyChar < '{')
			{
				return;
			}
			if (e.KeyChar > '@' && e.KeyChar < '[')
			{
				return;
			}
			e.Handled = true;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
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
			this.label1 = new Label();
			this.textBox1 = new TextBox();
			this.textBox_NewSSID = new TextBox();
			this.label2 = new Label();
			this.textBox_NewSSID_Confrim = new TextBox();
			this.label3 = new Label();
			this.textBox4 = new TextBox();
			this.textBox5 = new TextBox();
			this.button_OK = new Button();
			this.button_Cancel = new Button();
			this.label5 = new Label();
			this.timer1 = new Timer(this.components);
			base.SuspendLayout();
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "当前SSID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox1.BackColor = System.Drawing.Color.White;
			this.textBox1.Location = new System.Drawing.Point(116, 12);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(196, 21);
			this.textBox1.TabIndex = 1;
			this.textBox_NewSSID.Location = new System.Drawing.Point(170, 39);
			this.textBox_NewSSID.Name = "textBox_NewSSID";
			this.textBox_NewSSID.Size = new System.Drawing.Size(142, 21);
			this.textBox_NewSSID.TabIndex = 3;
			this.textBox_NewSSID.KeyPress += new KeyPressEventHandler(this.textBox_NewSSID_KeyPress);
			this.label2.Location = new System.Drawing.Point(10, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "新SSID";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_NewSSID_Confrim.Location = new System.Drawing.Point(170, 66);
			this.textBox_NewSSID_Confrim.Name = "textBox_NewSSID_Confrim";
			this.textBox_NewSSID_Confrim.Size = new System.Drawing.Size(142, 21);
			this.textBox_NewSSID_Confrim.TabIndex = 5;
			this.textBox_NewSSID_Confrim.KeyPress += new KeyPressEventHandler(this.textBox_NewSSID_Confrim_KeyPress);
			this.label3.Location = new System.Drawing.Point(10, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "确认SSID";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox4.BackColor = System.Drawing.Color.White;
			this.textBox4.Location = new System.Drawing.Point(116, 66);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(55, 21);
			this.textBox4.TabIndex = 7;
			this.textBox5.BackColor = System.Drawing.Color.White;
			this.textBox5.Location = new System.Drawing.Point(116, 39);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(55, 21);
			this.textBox5.TabIndex = 6;
			this.button_OK.Location = new System.Drawing.Point(68, 189);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(75, 23);
			this.button_OK.TabIndex = 8;
			this.button_OK.Text = "确定";
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new EventHandler(this.button_OK_Click);
			this.button_Cancel.Location = new System.Drawing.Point(192, 189);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.button_Cancel.TabIndex = 9;
			this.button_Cancel.Text = "取消";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new EventHandler(this.button_Cancel_Click);
			this.label5.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label5.ForeColor = System.Drawing.Color.Red;
			this.label5.Location = new System.Drawing.Point(22, 90);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(290, 96);
			this.label5.TabIndex = 10;
			this.label5.Text = "修改成功后需要1-2分钟时间重启,请耐心等待";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(321, 218);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_OK);
			base.Controls.Add(this.textBox4);
			base.Controls.Add(this.textBox5);
			base.Controls.Add(this.textBox_NewSSID_Confrim);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBox_NewSSID);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formChangeWIFISSID";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "修改网络SSID";
			base.Load += new EventHandler(this.formChangeWIFISSID_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
