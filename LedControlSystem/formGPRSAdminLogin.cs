using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formGPRSAdminLogin : Form
	{
		private static string formID = "formGPRSAdminLogin";

		private int LoginTimes;

		private bool isUser;

		private bool isForBind;

		public static string bindUserName = "";

		public static string bindPassword = "";

		private IContainer components;

		private TextBox textBox_AdminName;

		private TextBox textBox_AdminPassword;

		private Label label_AdminName;

		private Label label_AdminPassword;

		private Button button_AdminOK;

		private Button button_AdminCancel;

		private CheckBox checkBox_RememberInfo;

		private Panel PnlMessage;

		private PictureBox pictureBox1;

		private Label LblMessage;

		public static string FormID
		{
			get
			{
				return formGPRSAdminLogin.formID;
			}
			set
			{
				formGPRSAdminLogin.formID = value;
			}
		}

		public bool IsForBind
		{
			get
			{
				return this.isForBind;
			}
			set
			{
				this.isForBind = value;
			}
		}

		public bool IsUser
		{
			get
			{
				return this.isUser;
			}
			set
			{
				this.isUser = value;
			}
		}

		public formGPRSAdminLogin()
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formGPRSAdminLogin_FormText");
			this.label_AdminName.Text = formMain.ML.GetStr("formGPRSAdminLogin_label_AdminName");
			this.label_AdminPassword.Text = formMain.ML.GetStr("formGPRSAdminLogin_label_AdminPassword");
			this.checkBox_RememberInfo.Text = formMain.ML.GetStr("formGPRSAdminLogin_checkBox_RememberInfo");
			this.button_AdminOK.Text = formMain.ML.GetStr("formGPRSAdminLogin_button_AdminOK");
			this.button_AdminCancel.Text = formMain.ML.GetStr("formGPRSAdminLogin_button_AdminCancel");
		}

		private void button_AdminOK_Click(object sender, EventArgs e)
		{
			if (this.textBox_AdminPassword.Text == "")
			{
				this.textBox_AdminPassword.Focus();
				return;
			}
			if (this.isUser)
			{
				if (this.checkBox_RememberInfo.Checked)
				{
					Settings.Default.GprsRememberUser = true;
					Settings.Default.GPRSUsername = this.textBox_AdminName.Text;
					Settings.Default.GPRSPassword = this.textBox_AdminPassword.Text;
					Settings.Default.Save();
				}
				else
				{
					Settings.Default.GprsRememberUser = false;
					Settings.Default.GPRSUsername = "";
					Settings.Default.GPRSPassword = "";
					Settings.Default.Save();
				}
				if (GprsAdministrator.API_UserLogin(this.textBox_AdminName.Text.Trim(), this.textBox_AdminPassword.Text.Trim()))
				{
					GprsAdministrator.LoginSuccess = true;
					base.Dispose();
					return;
				}
				this.VisiblePnlMessage(true, "登录名或密码错误");
				this.LoginTimes++;
				GprsAdministrator.LoginSuccess = false;
				if (this.LoginTimes >= 3)
				{
					base.Dispose();
					return;
				}
			}
			else
			{
				if (this.IsForBind)
				{
					formGPRSAdminLogin.bindUserName = this.textBox_AdminName.Text;
					formGPRSAdminLogin.bindPassword = this.textBox_AdminPassword.Text;
					base.Dispose();
					return;
				}
				if (GprsAdministrator.API_AdminLogin(this.textBox_AdminName.Text.Trim(), this.textBox_AdminPassword.Text.Trim()))
				{
					GprsAdministrator.LoginSuccess = true;
					base.Dispose();
					return;
				}
				this.VisiblePnlMessage(true, "登录名或密码错误");
				this.LoginTimes++;
				GprsAdministrator.LoginSuccess = false;
				if (this.LoginTimes >= 3)
				{
					base.Dispose();
				}
			}
		}

		private void button_AdminCancel_Click(object sender, EventArgs e)
		{
			GprsAdministrator.LoginSuccess = false;
			base.Dispose();
		}

		private void formGPRSAdminLogin_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			if (this.IsUser)
			{
				if (Settings.Default.GprsRememberUser)
				{
					this.checkBox_RememberInfo.Checked = true;
					this.textBox_AdminName.Text = Settings.Default.GPRSUsername;
					this.textBox_AdminPassword.Text = Settings.Default.GPRSPassword;
				}
			}
			else if (!this.IsForBind)
			{
				this.checkBox_RememberInfo.Visible = false;
			}
			else
			{
				this.checkBox_RememberInfo.Visible = false;
			}
			this.VisiblePnlMessage(false, string.Empty);
		}

		private void VisiblePnlMessage(bool IsDisplay, string Message)
		{
			if (IsDisplay)
			{
				this.LblMessage.Text = Message;
				this.LblMessage.ForeColor = System.Drawing.Color.Red;
				this.PnlMessage.Visible = true;
				base.Size = new System.Drawing.Size(341, 226);
				return;
			}
			this.LblMessage.Text = string.Empty;
			this.LblMessage.ForeColor = System.Drawing.Color.Black;
			this.PnlMessage.Visible = false;
			base.Size = new System.Drawing.Size(341, 199);
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
			this.textBox_AdminName = new TextBox();
			this.textBox_AdminPassword = new TextBox();
			this.label_AdminName = new Label();
			this.label_AdminPassword = new Label();
			this.button_AdminOK = new Button();
			this.button_AdminCancel = new Button();
			this.checkBox_RememberInfo = new CheckBox();
			this.PnlMessage = new Panel();
			this.pictureBox1 = new PictureBox();
			this.LblMessage = new Label();
			this.PnlMessage.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.textBox_AdminName.Location = new System.Drawing.Point(147, 12);
			this.textBox_AdminName.Name = "textBox_AdminName";
			this.textBox_AdminName.Size = new System.Drawing.Size(162, 21);
			this.textBox_AdminName.TabIndex = 0;
			this.textBox_AdminPassword.Location = new System.Drawing.Point(147, 48);
			this.textBox_AdminPassword.Name = "textBox_AdminPassword";
			this.textBox_AdminPassword.PasswordChar = '*';
			this.textBox_AdminPassword.Size = new System.Drawing.Size(162, 21);
			this.textBox_AdminPassword.TabIndex = 1;
			this.label_AdminName.Location = new System.Drawing.Point(12, 10);
			this.label_AdminName.Name = "label_AdminName";
			this.label_AdminName.Size = new System.Drawing.Size(125, 23);
			this.label_AdminName.TabIndex = 2;
			this.label_AdminName.Text = "用户名";
			this.label_AdminName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label_AdminPassword.Location = new System.Drawing.Point(12, 42);
			this.label_AdminPassword.Name = "label_AdminPassword";
			this.label_AdminPassword.Size = new System.Drawing.Size(129, 30);
			this.label_AdminPassword.TabIndex = 3;
			this.label_AdminPassword.Text = "密码";
			this.label_AdminPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.button_AdminOK.Location = new System.Drawing.Point(70, 117);
			this.button_AdminOK.Name = "button_AdminOK";
			this.button_AdminOK.Size = new System.Drawing.Size(75, 23);
			this.button_AdminOK.TabIndex = 4;
			this.button_AdminOK.Text = "确定";
			this.button_AdminOK.UseVisualStyleBackColor = true;
			this.button_AdminOK.Click += new EventHandler(this.button_AdminOK_Click);
			this.button_AdminCancel.Location = new System.Drawing.Point(194, 117);
			this.button_AdminCancel.Name = "button_AdminCancel";
			this.button_AdminCancel.Size = new System.Drawing.Size(75, 23);
			this.button_AdminCancel.TabIndex = 5;
			this.button_AdminCancel.Text = "取消";
			this.button_AdminCancel.UseVisualStyleBackColor = true;
			this.button_AdminCancel.Click += new EventHandler(this.button_AdminCancel_Click);
			this.checkBox_RememberInfo.AutoSize = true;
			this.checkBox_RememberInfo.Location = new System.Drawing.Point(147, 86);
			this.checkBox_RememberInfo.Name = "checkBox_RememberInfo";
			this.checkBox_RememberInfo.Size = new System.Drawing.Size(96, 16);
			this.checkBox_RememberInfo.TabIndex = 6;
			this.checkBox_RememberInfo.Text = "记住登录信息";
			this.checkBox_RememberInfo.UseVisualStyleBackColor = true;
			this.PnlMessage.BackColor = System.Drawing.Color.FromArgb(255, 255, 230);
			this.PnlMessage.Controls.Add(this.pictureBox1);
			this.PnlMessage.Controls.Add(this.LblMessage);
			this.PnlMessage.Location = new System.Drawing.Point(0, 161);
			this.PnlMessage.Name = "PnlMessage";
			this.PnlMessage.Size = new System.Drawing.Size(325, 26);
			this.PnlMessage.TabIndex = 7;
			this.pictureBox1.Image = Resources.information;
			this.pictureBox1.Location = new System.Drawing.Point(6, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			this.LblMessage.BackColor = System.Drawing.Color.FromArgb(255, 255, 230);
			this.LblMessage.Location = new System.Drawing.Point(28, 4);
			this.LblMessage.Name = "LblMessage";
			this.LblMessage.Size = new System.Drawing.Size(294, 18);
			this.LblMessage.TabIndex = 0;
			this.LblMessage.Text = "Message";
			this.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			base.AcceptButton = this.button_AdminOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(325, 187);
			base.Controls.Add(this.PnlMessage);
			base.Controls.Add(this.checkBox_RememberInfo);
			base.Controls.Add(this.button_AdminCancel);
			base.Controls.Add(this.button_AdminOK);
			base.Controls.Add(this.label_AdminPassword);
			base.Controls.Add(this.label_AdminName);
			base.Controls.Add(this.textBox_AdminPassword);
			base.Controls.Add(this.textBox_AdminName);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGPRSAdminLogin";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "GPRS登陆";
			base.Load += new EventHandler(this.formGPRSAdminLogin_Load);
			this.PnlMessage.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
