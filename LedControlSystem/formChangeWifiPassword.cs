using LedControlSystem.Properties;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formChangeWifiPassword : Form
	{
		private formMain fm;

		private formConnectToWIFI fw;

		public static bool isFinish = false;

		public static bool isSuccess = false;

		private static string formID = "formGPRS_ChangeWifiPassword";

		public static string LastWIFIPassword = "";

		public static bool LastResult = false;

		private IContainer components;

		private Label label1;

		private TextBox textBox_OldPassword;

		private TextBox textBox_NewPassword;

		private Label label2;

		private TextBox textBox_ConfirmPassword;

		private Label label3;

		private GroupBox groupBox1;

		private Button button_OK;

		private Button button_Cancel;

		private Label label4;

		private Timer timer1;

		private Label label5;

		private Timer timer_CheckIP;

		public static string FormID
		{
			get
			{
				return formChangeWifiPassword.formID;
			}
			set
			{
				formChangeWifiPassword.formID = value;
			}
		}

		public formChangeWifiPassword(formMain pMain, formConnectToWIFI pWifi)
		{
			this.InitializeComponent();
			this.fm = pMain;
			this.fw = pWifi;
			formChangeWifiPassword.isFinish = false;
			formChangeWifiPassword.isSuccess = false;
			formMain.ML.NowFormID = formChangeWifiPassword.formID;
			this.Text = formMain.ML.GetStr("formGPRS_ChangeWifiPassword_FormText");
			this.label1.Text = formMain.ML.GetStr("formGPRS_ChangeWifiPassword_label_OldPassword");
			this.label2.Text = formMain.ML.GetStr("formGPRS_ChangeWifiPassword_label_NewPassword");
			this.label3.Text = formMain.ML.GetStr("formGPRS_ChangeWifiPassword_label_confirm_NewPassword");
			this.label5.Text = formMain.ML.GetStr("formGPRS_ChangeWifiPassword_label_prompt");
			this.label4.Text = formMain.ML.GetStr("formGPRS_ChangeWifiPassword_label_SpecificMethod");
			this.groupBox1.Text = formMain.ML.GetStr("formGPRS_ChangeWifiPassword_groupBox_ModifyTheSteps");
			this.button_OK.Text = formMain.ML.GetStr("formGPRS_ChangeWifiPassword_button_OK");
			this.button_Cancel.Text = formMain.ML.GetStr("formGPRS_ChangeWifiPassword_button_Cancel");
		}

		public formChangeWifiPassword()
		{
			this.InitializeComponent();
		}

		private void formChangeWifiPassword_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
		}

		private void button_OK_Click(object sender, EventArgs e)
		{
			if (this.textBox_OldPassword.Text.Trim().Length < 8 || this.textBox_OldPassword.Text.Trim().Length > 31)
			{
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_Invalid_Oldpassword"));
				return;
			}
			if (this.textBox_NewPassword.Text.Length < 8 || this.textBox_NewPassword.Text.Length > 31)
			{
				MessageBox.Show(this, formMain.ML.GetStr("WIFI_Invalid_Newpassword"));
				return;
			}
			if (this.textBox_NewPassword.Text != this.textBox_ConfirmPassword.Text)
			{
				MessageBox.Show(this, formMain.ML.GetStr("GPRS_NewPassrodDiff"));
				return;
			}
			LedWiFiPassword ledWiFiPassword = new LedWiFiPassword();
			ledWiFiPassword.OldPassword = this.textBox_OldPassword.Text;
			ledWiFiPassword.NewPassword = this.textBox_NewPassword.Text;
			string currentWIfiSSID = formConnectToWIFI.GetCurrentWIfiSSID();
			if (currentWIfiSSID.StartsWith("ZH-W") || currentWIfiSSID.StartsWith("ZH-5W"))
			{
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_WiFi_Password, ledWiFiPassword, formMain.ML.GetStr("WIFI_ChangePassworOperation"), formMain.ledsys.SelectedPanel, false, this);
				if (formSendSingle.LastSendResult)
				{
					formChangeWifiPassword.LastWIFIPassword = this.textBox_NewPassword.Text.Trim();
					MessageBox.Show(this, formMain.ML.GetStr("WIFI_ChangePassworOperation") + formMain.ML.GetStr("Display_Successed"));
					formChangeWifiPassword.isSuccess = true;
					formChangeWifiPassword.LastResult = true;
					formChangeWifiPassword.isFinish = true;
					return;
				}
			}
			else
			{
				MessageBox.Show(this, formMain.ML.GetStr("formGPRS_ChangeWifiPassword_message_Change_Failed"));
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
		}

		private void button_Cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void timer_CheckIP_Tick(object sender, EventArgs e)
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
			this.textBox_OldPassword = new TextBox();
			this.textBox_NewPassword = new TextBox();
			this.label2 = new Label();
			this.textBox_ConfirmPassword = new TextBox();
			this.label3 = new Label();
			this.groupBox1 = new GroupBox();
			this.label4 = new Label();
			this.button_OK = new Button();
			this.button_Cancel = new Button();
			this.timer1 = new Timer(this.components);
			this.label5 = new Label();
			this.timer_CheckIP = new Timer(this.components);
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.label1.Location = new System.Drawing.Point(12, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "原密码";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_OldPassword.Location = new System.Drawing.Point(108, 24);
			this.textBox_OldPassword.Name = "textBox_OldPassword";
			this.textBox_OldPassword.PasswordChar = '*';
			this.textBox_OldPassword.Size = new System.Drawing.Size(192, 21);
			this.textBox_OldPassword.TabIndex = 1;
			this.textBox_NewPassword.Location = new System.Drawing.Point(108, 51);
			this.textBox_NewPassword.Name = "textBox_NewPassword";
			this.textBox_NewPassword.PasswordChar = '*';
			this.textBox_NewPassword.Size = new System.Drawing.Size(192, 21);
			this.textBox_NewPassword.TabIndex = 3;
			this.label2.Location = new System.Drawing.Point(12, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "新密码";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_ConfirmPassword.Location = new System.Drawing.Point(108, 78);
			this.textBox_ConfirmPassword.Name = "textBox_ConfirmPassword";
			this.textBox_ConfirmPassword.PasswordChar = '*';
			this.textBox_ConfirmPassword.Size = new System.Drawing.Size(192, 21);
			this.textBox_ConfirmPassword.TabIndex = 5;
			this.label3.Location = new System.Drawing.Point(12, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "确认新密码";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Location = new System.Drawing.Point(321, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(10, 10);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "修改WIFI密码步骤";
			this.groupBox1.Visible = false;
			this.label4.Dock = DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(4, 0);
			this.label4.TabIndex = 0;
			this.label4.Text = "1.输入正确的原密码\r\n2.输入合法的新密码\r\n3.输入确认密码\r\n4.点【确定】按钮,如果输入的信息正确会提示成功\r\n5.如果是手机重新连接网络即可,会提示重新输入密码\r\n6.如果是电脑XP系统,重新连接网络并输入密码即可\r\n7.如果是Win7系统,在网络模块重启后,在网络列表中右击WIFI网络名称,点属性,在弹出的电话卡中选择【安全】选项卡,输入新密码按【确定】按钮,重新连接网络即可.\r\n";
			this.button_OK.Location = new System.Drawing.Point(89, 201);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(75, 23);
			this.button_OK.TabIndex = 7;
			this.button_OK.Text = "确定";
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new EventHandler(this.button_OK_Click);
			this.button_Cancel.Location = new System.Drawing.Point(170, 201);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.button_Cancel.TabIndex = 8;
			this.button_Cancel.Text = "取消";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new EventHandler(this.button_Cancel_Click);
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.label5.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label5.ForeColor = System.Drawing.Color.Red;
			this.label5.Location = new System.Drawing.Point(39, 102);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(290, 96);
			this.label5.TabIndex = 9;
			this.label5.Text = "修改成功后需要1-2分钟时间重启,请耐心等待";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.timer_CheckIP.Tick += new EventHandler(this.timer_CheckIP_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(337, 246);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_OK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.textBox_ConfirmPassword);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBox_NewPassword);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBox_OldPassword);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formChangeWifiPassword";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "修改WIFI密码";
			base.Load += new EventHandler(this.formChangeWifiPassword_Load);
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
