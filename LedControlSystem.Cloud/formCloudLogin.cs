using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Foundation;
using LedService.Cloud.Account;
using LedService.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.Cloud
{
	public class formCloudLogin : Form
	{
		private int mode;

		private int loginTimes;

		private bool isCookie;

		private bool isCountdowning;

		private string sessionIDMobileLogin;

		private IContainer components;

		private Panel pnlMessage;

		private PictureBox picMessage;

		private Label lblMessage;

		private Panel pnlLoginInfo;

		private LinkLabel llbRegister;

		private Button btnCancel;

		private TextBox txtLoginName;

		private TextBox txtPassword;

		private Label lblLoginName;

		private Label lblPassword;

		private CheckBox chbCookie;

		private Button btnLogin;

		private LinkLabel llbForgotPassword;

		private LinkLabel llbMode;

		private Panel pnlAccount;

		private Panel pnlMobile;

		private Label lblSecurityCode;

		private TextBox txtSecurityCode;

		private Label lblMobile;

		private TextBox txtMobile;

		private LinkLabel llbSendSecurityCode;

		public formCloudLogin()
		{
			this.InitializeComponent();
			this.DisplayLanuageText();
		}

		private void DisplayLanuageText()
		{
			this.Text = formMain.ML.GetStr("formGroup_Form_CloudLogin");
			this.lblLoginName.Text = formMain.ML.GetStr("formGroup_Label_LoginName");
			this.lblPassword.Text = formMain.ML.GetStr("formGroup_Label_Password");
			this.llbRegister.Text = formMain.ML.GetStr("formGroup_LinkLabel_Register");
			this.chbCookie.Text = formMain.ML.GetStr("formGroup_CheckBox_Cookie");
			this.btnLogin.Text = formMain.ML.GetStr("formCloudLogin_Button_Login");
			this.btnCancel.Text = formMain.ML.GetStr("formCloudLogin_Button_Cancel");
			this.lblMobile.Text = formMain.ML.GetStr("formCloudLogin_Label_Mobile");
			this.lblSecurityCode.Text = formMain.ML.GetStr("formCloudLogin_Label_SecurityCode");
			this.llbSendSecurityCode.Text = formMain.ML.GetStr("formCloudLogin_LinkLabel_SendSecurityCode");
			this.llbForgotPassword.Text = formMain.ML.GetStr("formCloudLogin_LinkLabel_ForgotPassword");
			this.llbMode.Text = formMain.ML.GetStr("formCloudLogin_LinkLabel_Mode_SMS");
			this.btnCancel.Visible = false;
			this.mode = 1;
			this.sessionIDMobileLogin = string.Empty;
			this.pnlAccount.Visible = true;
			this.pnlMobile.Visible = false;
		}

		private void formCloudLogin_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.llbMode.Left = this.btnLogin.Left + this.btnLogin.Width - this.llbMode.Width;
			this.llbRegister.Left = this.btnLogin.Left + this.btnLogin.Width - this.llbRegister.Width;
			this.pnlMessage.Visible = false;
			base.Height -= this.pnlMessage.Height;
			this.isCookie = false;
			this.chbCookie.Checked = Settings.Default.CloudServerRememberUser;
			if (this.chbCookie.Checked)
			{
				this.isCookie = true;
				this.txtLoginName.Text = Settings.Default.CloudServerUserName;
				this.txtPassword.Text = Settings.Default.CloudServerPassword;
			}
			else
			{
				this.txtLoginName.Text = string.Empty;
				this.txtPassword.Text = string.Empty;
			}
			this.loginTimes = 0;
		}

		private void formCloudLogin_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.isCountdowning)
			{
				this.isCountdowning = false;
				Thread.Sleep(1000);
			}
			if (base.DialogResult == DialogResult.OK)
			{
				Settings.Default.CloudServerRememberUser = this.chbCookie.Checked;
				Settings.Default.CloudServerUserName = this.txtLoginName.Text;
				string text = this.txtPassword.Text;
				if (!this.isCookie)
				{
					text = SystemTool.MD5Encode(text);
				}
				Settings.Default.CloudServerPassword = text;
				Settings.Default.Save();
			}
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			AccountInfo accountInfo;
			if (this.mode == 1)
			{
				string text = this.txtLoginName.Text;
				if (string.IsNullOrEmpty(text))
				{
					this.txtLoginName.Focus();
					return;
				}
				string text2 = this.txtPassword.Text;
				if (string.IsNullOrEmpty(text2))
				{
					this.txtPassword.Focus();
					return;
				}
				if (!this.isCookie)
				{
					text2 = SystemTool.MD5Encode(text2);
				}
				accountInfo = new AccountService().Login(text, text2);
				if (this.loginTimes >= 2)
				{
					base.Close();
				}
				if (accountInfo == null)
				{
					this.ShowMessage(formMain.ML.GetStr("Message_LoginName_Or_Password_Error"));
					this.loginTimes++;
					return;
				}
			}
			else
			{
				string text3 = this.txtMobile.Text;
				if (string.IsNullOrEmpty(text3))
				{
					this.txtMobile.Focus();
					return;
				}
				string text4 = this.txtSecurityCode.Text;
				if (string.IsNullOrEmpty(text4))
				{
					this.txtSecurityCode.Focus();
					return;
				}
				if (string.IsNullOrEmpty(this.sessionIDMobileLogin))
				{
					this.txtSecurityCode.Text = string.Empty;
					this.txtSecurityCode.Focus();
					return;
				}
				accountInfo = new AccountService().MobileLogin(text3, text4, this.sessionIDMobileLogin);
				if (this.loginTimes >= 2)
				{
					base.Close();
				}
				if (accountInfo == null)
				{
					this.ShowMessage(formMain.ML.GetStr("Message_Mobile_Or_SecurityCode_Error"));
					this.loginTimes++;
					return;
				}
			}
			if (LedGlobal.CloudAccount == null)
			{
				LedGlobal.CloudAccount = new LedAccount();
			}
			LedGlobal.CloudAccount.SetAccount(accountInfo);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void llbMode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			bool flag;
			if (this.mode == 1)
			{
				this.mode = 2;
				flag = false;
				this.llbMode.Text = formMain.ML.GetStr("formCloudLogin_LinkLabel_Mode_AccountPassword");
			}
			else
			{
				this.mode = 1;
				flag = true;
				this.llbMode.Text = formMain.ML.GetStr("formCloudLogin_LinkLabel_Mode_SMS");
			}
			this.llbMode.Left = this.btnLogin.Left + this.btnLogin.Width - this.llbMode.Width;
			this.pnlAccount.Visible = flag;
			this.pnlMobile.Visible = !flag;
		}

		private void llbSendSecurityCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string text = this.txtMobile.Text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				this.txtMobile.Focus();
				return;
			}
			if (!Regex.IsMatch(text, "^1\\d{10}$"))
			{
				this.txtMobile.Focus();
				return;
			}
			this.llbSendSecurityCode.Enabled = false;
			this.isCountdowning = true;
			ParameterizedThreadStart start = new ParameterizedThreadStart(this.SendAndCountdown);
			Thread thread = new Thread(start);
			object parameter = text;
			thread.IsBackground = true;
			thread.Start(parameter);
		}

		private void llbRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			formCloudRegister formCloudRegister = new formCloudRegister(this.mode);
			formCloudRegister.ShowDialog(this);
		}

		private void llbForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			formCloudForgotPassword formCloudForgotPassword = new formCloudForgotPassword(this.mode);
			formCloudForgotPassword.ShowDialog(this);
		}

		private void SendAndCountdown(object obj)
		{
			string mobile = obj as string;
			int duration = 30;
			int time = 0;
			this.sessionIDMobileLogin = new AccountService().LoginSecurityCode(mobile);
			while (time < duration)
			{
				if (!this.isCountdowning)
				{
					return;
				}
				base.Invoke(new MethodInvoker(delegate
				{
					this.llbSendSecurityCode.Text = string.Format("{0}（{1}）", formMain.ML.GetStr("formCloudLogin_LinkLabel_ResendSecurityCode"), duration - time);
				}));
				if (!this.isCountdowning)
				{
					return;
				}
				Thread.Sleep(1000);
				time++;
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.llbSendSecurityCode.Text = formMain.ML.GetStr("formCloudLogin_LinkLabel_ResendSecurityCode");
				this.llbSendSecurityCode.Enabled = true;
			}));
			Thread.Sleep(200);
		}

		private void ShowMessage(string message)
		{
			this.lblMessage.Text = message;
			if (!this.pnlMessage.Visible)
			{
				this.lblMessage.ForeColor = System.Drawing.Color.Red;
				this.pnlMessage.Visible = true;
				base.Height += this.pnlMessage.Height;
			}
		}

		private void txtPassword_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			this.isCookie = false;
		}

		private void txtPassword_MouseClick(object sender, MouseEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox.TextLength > 0)
			{
				textBox.Select(0, textBox.TextLength);
			}
		}

		private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
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
			this.pnlMessage = new Panel();
			this.picMessage = new PictureBox();
			this.lblMessage = new Label();
			this.pnlLoginInfo = new Panel();
			this.pnlAccount = new Panel();
			this.lblLoginName = new Label();
			this.chbCookie = new CheckBox();
			this.lblPassword = new Label();
			this.txtPassword = new TextBox();
			this.txtLoginName = new TextBox();
			this.llbMode = new LinkLabel();
			this.llbRegister = new LinkLabel();
			this.btnCancel = new Button();
			this.llbForgotPassword = new LinkLabel();
			this.btnLogin = new Button();
			this.pnlMobile = new Panel();
			this.llbSendSecurityCode = new LinkLabel();
			this.lblSecurityCode = new Label();
			this.txtSecurityCode = new TextBox();
			this.lblMobile = new Label();
			this.txtMobile = new TextBox();
			this.pnlMessage.SuspendLayout();
			((ISupportInitialize)this.picMessage).BeginInit();
			this.pnlLoginInfo.SuspendLayout();
			this.pnlAccount.SuspendLayout();
			this.pnlMobile.SuspendLayout();
			base.SuspendLayout();
			this.pnlMessage.BackColor = System.Drawing.Color.FromArgb(255, 255, 230);
			this.pnlMessage.Controls.Add(this.picMessage);
			this.pnlMessage.Controls.Add(this.lblMessage);
			this.pnlMessage.Location = new System.Drawing.Point(0, 279);
			this.pnlMessage.Name = "pnlMessage";
			this.pnlMessage.Size = new System.Drawing.Size(345, 22);
			this.pnlMessage.TabIndex = 29;
			this.picMessage.Image = Resources.information;
			this.picMessage.Location = new System.Drawing.Point(6, 4);
			this.picMessage.Name = "picMessage";
			this.picMessage.Size = new System.Drawing.Size(16, 16);
			this.picMessage.TabIndex = 8;
			this.picMessage.TabStop = false;
			this.lblMessage.BackColor = System.Drawing.Color.FromArgb(255, 255, 230);
			this.lblMessage.Location = new System.Drawing.Point(28, 3);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(304, 19);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "Message";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.pnlLoginInfo.BackColor = System.Drawing.Color.White;
			this.pnlLoginInfo.Controls.Add(this.pnlAccount);
			this.pnlLoginInfo.Controls.Add(this.llbMode);
			this.pnlLoginInfo.Controls.Add(this.llbRegister);
			this.pnlLoginInfo.Controls.Add(this.btnCancel);
			this.pnlLoginInfo.Controls.Add(this.llbForgotPassword);
			this.pnlLoginInfo.Controls.Add(this.btnLogin);
			this.pnlLoginInfo.Controls.Add(this.pnlMobile);
			this.pnlLoginInfo.Location = new System.Drawing.Point(0, 0);
			this.pnlLoginInfo.Name = "pnlLoginInfo";
			this.pnlLoginInfo.Size = new System.Drawing.Size(345, 279);
			this.pnlLoginInfo.TabIndex = 28;
			this.pnlAccount.Controls.Add(this.lblLoginName);
			this.pnlAccount.Controls.Add(this.chbCookie);
			this.pnlAccount.Controls.Add(this.lblPassword);
			this.pnlAccount.Controls.Add(this.txtPassword);
			this.pnlAccount.Controls.Add(this.txtLoginName);
			this.pnlAccount.Location = new System.Drawing.Point(15, 40);
			this.pnlAccount.Name = "pnlAccount";
			this.pnlAccount.Size = new System.Drawing.Size(315, 138);
			this.pnlAccount.TabIndex = 16;
			this.lblLoginName.Image = Resources.user;
			this.lblLoginName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblLoginName.Location = new System.Drawing.Point(3, 8);
			this.lblLoginName.Name = "lblLoginName";
			this.lblLoginName.Size = new System.Drawing.Size(87, 23);
			this.lblLoginName.TabIndex = 9;
			this.lblLoginName.Text = "用户名：";
			this.lblLoginName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chbCookie.AutoSize = true;
			this.chbCookie.Location = new System.Drawing.Point(5, 111);
			this.chbCookie.Name = "chbCookie";
			this.chbCookie.Size = new System.Drawing.Size(96, 16);
			this.chbCookie.TabIndex = 13;
			this.chbCookie.Text = "记住登录信息";
			this.chbCookie.UseVisualStyleBackColor = true;
			this.lblPassword.Image = Resources.key;
			this.lblPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblPassword.Location = new System.Drawing.Point(3, 61);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(89, 23);
			this.lblPassword.TabIndex = 10;
			this.lblPassword.Text = "密  码：";
			this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtPassword.Location = new System.Drawing.Point(104, 63);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(208, 21);
			this.txtPassword.TabIndex = 8;
			this.txtPassword.MouseClick += new MouseEventHandler(this.txtPassword_MouseClick);
			this.txtPassword.TextChanged += new EventHandler(this.txtPassword_TextChanged);
			this.txtLoginName.Location = new System.Drawing.Point(104, 10);
			this.txtLoginName.Name = "txtLoginName";
			this.txtLoginName.Size = new System.Drawing.Size(208, 21);
			this.txtLoginName.TabIndex = 7;
			this.llbMode.AutoSize = true;
			this.llbMode.LinkBehavior = LinkBehavior.HoverUnderline;
			this.llbMode.LinkColor = System.Drawing.Color.FromArgb(30, 120, 213);
			this.llbMode.Location = new System.Drawing.Point(253, 15);
			this.llbMode.Name = "llbMode";
			this.llbMode.Size = new System.Drawing.Size(77, 12);
			this.llbMode.TabIndex = 15;
			this.llbMode.TabStop = true;
			this.llbMode.Text = "短信快捷登录";
			this.llbMode.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llbMode_LinkClicked);
			this.llbRegister.AutoSize = true;
			this.llbRegister.LinkBehavior = LinkBehavior.HoverUnderline;
			this.llbRegister.LinkColor = System.Drawing.Color.FromArgb(30, 120, 213);
			this.llbRegister.Location = new System.Drawing.Point(301, 257);
			this.llbRegister.Name = "llbRegister";
			this.llbRegister.Size = new System.Drawing.Size(29, 12);
			this.llbRegister.TabIndex = 14;
			this.llbRegister.TabStop = true;
			this.llbRegister.Text = "注册";
			this.llbRegister.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llbRegister_LinkClicked);
			this.btnCancel.Location = new System.Drawing.Point(133, 250);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.llbForgotPassword.AutoSize = true;
			this.llbForgotPassword.LinkBehavior = LinkBehavior.HoverUnderline;
			this.llbForgotPassword.LinkColor = System.Drawing.Color.FromArgb(30, 120, 213);
			this.llbForgotPassword.Location = new System.Drawing.Point(13, 257);
			this.llbForgotPassword.Name = "llbForgotPassword";
			this.llbForgotPassword.Size = new System.Drawing.Size(65, 12);
			this.llbForgotPassword.TabIndex = 14;
			this.llbForgotPassword.TabStop = true;
			this.llbForgotPassword.Text = "忘记密码？";
			this.llbForgotPassword.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llbForgotPassword_LinkClicked);
			this.btnLogin.BackColor = System.Drawing.Color.FromArgb(67, 122, 232);
			this.btnLogin.FlatStyle = FlatStyle.Flat;
			this.btnLogin.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.btnLogin.ForeColor = System.Drawing.Color.White;
			this.btnLogin.Location = new System.Drawing.Point(15, 193);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(315, 45);
			this.btnLogin.TabIndex = 11;
			this.btnLogin.Text = "登录";
			this.btnLogin.UseVisualStyleBackColor = false;
			this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
			this.pnlMobile.Controls.Add(this.llbSendSecurityCode);
			this.pnlMobile.Controls.Add(this.lblSecurityCode);
			this.pnlMobile.Controls.Add(this.txtSecurityCode);
			this.pnlMobile.Controls.Add(this.lblMobile);
			this.pnlMobile.Controls.Add(this.txtMobile);
			this.pnlMobile.Location = new System.Drawing.Point(15, 40);
			this.pnlMobile.Name = "pnlMobile";
			this.pnlMobile.Size = new System.Drawing.Size(315, 138);
			this.pnlMobile.TabIndex = 17;
			this.llbSendSecurityCode.AutoSize = true;
			this.llbSendSecurityCode.LinkBehavior = LinkBehavior.HoverUnderline;
			this.llbSendSecurityCode.LinkColor = System.Drawing.Color.FromArgb(30, 120, 213);
			this.llbSendSecurityCode.Location = new System.Drawing.Point(200, 66);
			this.llbSendSecurityCode.Name = "llbSendSecurityCode";
			this.llbSendSecurityCode.Size = new System.Drawing.Size(65, 12);
			this.llbSendSecurityCode.TabIndex = 14;
			this.llbSendSecurityCode.TabStop = true;
			this.llbSendSecurityCode.Text = "发送验证码";
			this.llbSendSecurityCode.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llbSendSecurityCode_LinkClicked);
			this.lblSecurityCode.Image = Resources.key;
			this.lblSecurityCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblSecurityCode.Location = new System.Drawing.Point(3, 61);
			this.lblSecurityCode.Name = "lblSecurityCode";
			this.lblSecurityCode.Size = new System.Drawing.Size(89, 23);
			this.lblSecurityCode.TabIndex = 13;
			this.lblSecurityCode.Text = "验证码：";
			this.lblSecurityCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtSecurityCode.Location = new System.Drawing.Point(104, 63);
			this.txtSecurityCode.Name = "txtSecurityCode";
			this.txtSecurityCode.PasswordChar = '*';
			this.txtSecurityCode.Size = new System.Drawing.Size(90, 21);
			this.txtSecurityCode.TabIndex = 12;
			this.lblMobile.Image = Resources.mobile;
			this.lblMobile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblMobile.Location = new System.Drawing.Point(3, 8);
			this.lblMobile.Name = "lblMobile";
			this.lblMobile.Size = new System.Drawing.Size(87, 23);
			this.lblMobile.TabIndex = 11;
			this.lblMobile.Text = "手机号：";
			this.lblMobile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.txtMobile.Location = new System.Drawing.Point(104, 10);
			this.txtMobile.Name = "txtMobile";
			this.txtMobile.Size = new System.Drawing.Size(208, 21);
			this.txtMobile.TabIndex = 10;
			this.txtMobile.KeyPress += new KeyPressEventHandler(this.txtMobile_KeyPress);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(344, 302);
			base.Controls.Add(this.pnlMessage);
			base.Controls.Add(this.pnlLoginInfo);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudLogin";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "云服务登录";
			base.FormClosing += new FormClosingEventHandler(this.formCloudLogin_FormClosing);
			base.Load += new EventHandler(this.formCloudLogin_Load);
			this.pnlMessage.ResumeLayout(false);
			((ISupportInitialize)this.picMessage).EndInit();
			this.pnlLoginInfo.ResumeLayout(false);
			this.pnlLoginInfo.PerformLayout();
			this.pnlAccount.ResumeLayout(false);
			this.pnlAccount.PerformLayout();
			this.pnlMobile.ResumeLayout(false);
			this.pnlMobile.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
