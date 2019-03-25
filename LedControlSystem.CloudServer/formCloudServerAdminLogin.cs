using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.CloudServer
{
	public class formCloudServerAdminLogin : Form
	{
		private int LoginTimes;

		public bool IsRoot;

		private CloudServerComm APIUserLogin = new CloudServerComm();

		public bool IsLoginSuccess;

		private static int UserLevel = 2;

		private bool IsChildLoginSelected;

		private bool IsFatherLoginSelected;

		private string ChildUser = "";

		private string ChildUserName = "";

		private string ChildPassword = "";

		private string FatherUserName = "";

		private string FatherPassword = "";

		private IContainer components;

		private CheckBox checkBox_RememberInfo;

		private Button button_AdminCancel;

		private Button button_AdminOK;

		private Label label_AdminPassword;

		private Label label_AdminName;

		private TextBox textBox_AdminPassword;

		private TextBox textBox_AdminName;

		private Button BtnLevelChangeUserLogin;

		private Panel panelChildUser;

		private Label LblFatherUser;

		private TextBox textBox_AdminManager;

		private Panel panelAccount;

		private Panel PnlMessage;

		private PictureBox pictureBox1;

		private Label LblMessage;

		public formCloudServerAdminLogin()
		{
			this.InitializeComponent();
		}

		private void button_AdminOK_Click(object sender, EventArgs e)
		{
			if (this.textBox_AdminPassword.Text == "")
			{
				this.textBox_AdminPassword.Focus();
				return;
			}
			if (formCloudServerAdminLogin.UserLevel == 1)
			{
				bool flag = this.APIUserLogin.API_Login_Administrator(this.textBox_AdminName.Text, this.textBox_AdminPassword.Text);
				if (this.LoginTimes >= 2)
				{
					base.Close();
				}
				if (!flag)
				{
					this.VisiblePnlMessage(true, "登录名或密码错误");
					this.LoginTimes++;
					return;
				}
			}
			else
			{
				bool flag2 = this.APIUserLogin.API_UserLogin(this.textBox_AdminManager.Text, this.textBox_AdminName.Text, this.textBox_AdminPassword.Text);
				if (this.LoginTimes >= 2)
				{
					base.Close();
				}
				if (!flag2)
				{
					this.VisiblePnlMessage(true, "登录名或密码错误");
					this.LoginTimes++;
					return;
				}
			}
			this.IsLoginSuccess = true;
			this.remember_CloudServerLogin_login_info();
			CloudRecordToXml.LoginedUser = this.textBox_AdminName.Text;
			base.Close();
		}

		public void remember_CloudServerLogin_login_info()
		{
			if (this.IsChildLoginSelected)
			{
				Settings.Default.CloudServerRememberChild = true;
			}
			else
			{
				Settings.Default.CloudServerRememberChild = false;
			}
			if (this.IsFatherLoginSelected)
			{
				Settings.Default.CloudServerRememberUser = true;
			}
			else
			{
				Settings.Default.CloudServerRememberUser = false;
			}
			if (formCloudServerAdminLogin.UserLevel == 1)
			{
				Settings.Default.CloudServerIsAccount = false;
			}
			else
			{
				Settings.Default.CloudServerIsAccount = true;
			}
			Settings.Default.CloudServerUserName = this.FatherUserName;
			Settings.Default.CloudServerPassword = this.FatherPassword;
			Settings.Default.CloudServerFather = this.ChildUser;
			Settings.Default.CloudServerChildUserName = this.ChildUserName;
			Settings.Default.CloudServerChildPassword = this.ChildPassword;
			Settings.Default.Save();
		}

		public bool IsAdvancedUser(string UserName, string Password)
		{
			return UserName == "zoehoo" && Password == "zoehoo123";
		}

		private void button_AdminCancel_Click(object sender, EventArgs e)
		{
			this.remember_CloudServerLogin_login_info();
			base.Dispose();
		}

		private void formCloudServerAdminLogin_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.Text = formMain.ML.GetStr("formCloudServerAdminLogin_FormText");
			this.LblFatherUser.Text = formMain.ML.GetStr("formCloudServerAdminLogin_AdvancedUser");
			this.label_AdminName.Text = formMain.ML.GetStr("formCloudServerAdminLogin_UserName");
			this.label_AdminPassword.Text = formMain.ML.GetStr("formCloudServerAdminLogin_Password");
			this.checkBox_RememberInfo.Text = formMain.ML.GetStr("formCloudServerAdminLogin_RememberInfo");
			this.button_AdminOK.Text = formMain.ML.GetStr("Global_Messagebox_OK");
			this.button_AdminCancel.Text = formMain.ML.GetStr("Global_Messagebox_Cancel");
			this.PnlMessage.Visible = false;
			if (Settings.Default.CloudServerRememberChild)
			{
				this.ChildUser = Settings.Default.CloudServerFather;
				this.ChildUserName = Settings.Default.CloudServerChildUserName;
				this.ChildPassword = Settings.Default.CloudServerChildPassword;
				this.IsChildLoginSelected = true;
			}
			if (Settings.Default.CloudServerRememberUser)
			{
				this.FatherUserName = Settings.Default.CloudServerUserName;
				this.FatherPassword = Settings.Default.CloudServerPassword;
				this.IsFatherLoginSelected = true;
			}
			if (!Settings.Default.CloudServerIsAccount)
			{
				formCloudServerAdminLogin.UserLevel = 1;
				this.panelAccount.Visible = false;
				this.panelChildUser.Location = new System.Drawing.Point(0, 12);
				base.Height = 250;
				this.textBox_AdminManager.Text = string.Empty;
				this.textBox_AdminName.Text = this.FatherUserName;
				this.textBox_AdminPassword.Text = this.FatherPassword;
				this.BtnLevelChangeUserLogin.Text = formMain.ML.GetStr("formCloudServerAdminLogin_BtnOrdinary");
			}
			else
			{
				formCloudServerAdminLogin.UserLevel = 2;
				this.panelAccount.Visible = true;
				this.panelAccount.Location = new System.Drawing.Point(0, 12);
				this.panelChildUser.Location = new System.Drawing.Point(0, 44);
				base.Height = 282;
				this.textBox_AdminManager.Text = this.ChildUser;
				this.textBox_AdminName.Text = this.ChildUserName;
				this.textBox_AdminPassword.Text = this.ChildPassword;
				this.BtnLevelChangeUserLogin.Text = formMain.ML.GetStr("formCloudServerAdminLogin_BtnAdvanced");
			}
			if (formCloudServerAdminLogin.UserLevel == 1)
			{
				if (this.IsFatherLoginSelected)
				{
					this.checkBox_RememberInfo.Checked = true;
					return;
				}
				this.checkBox_RememberInfo.Checked = false;
				return;
			}
			else
			{
				if (this.IsChildLoginSelected)
				{
					this.checkBox_RememberInfo.Checked = true;
					return;
				}
				this.checkBox_RememberInfo.Checked = false;
				return;
			}
		}

		private void BtnLowLevelUserLogin_Click(object sender, EventArgs e)
		{
			this.VisiblePnlMessage(false, string.Empty);
			if (formCloudServerAdminLogin.UserLevel == 2)
			{
				formCloudServerAdminLogin.UserLevel = 1;
				this.BtnLevelChangeUserLogin.Text = formMain.ML.GetStr("formCloudServerAdminLogin_BtnOrdinary");
				Settings.Default.CloudServerIsAccount = false;
				this.panelAccount.Visible = false;
				this.panelChildUser.Location = new System.Drawing.Point(0, 12);
				base.Height = 250;
				this.checkBox_RememberInfo.Checked = this.IsFatherLoginSelected;
				this.textBox_AdminName.Text = this.FatherUserName;
				this.textBox_AdminPassword.Text = this.FatherPassword;
				return;
			}
			formCloudServerAdminLogin.UserLevel = 2;
			this.BtnLevelChangeUserLogin.Text = formMain.ML.GetStr("formCloudServerAdminLogin_BtnAdvanced");
			Settings.Default.CloudServerIsAccount = true;
			this.panelAccount.Visible = true;
			this.panelAccount.Location = new System.Drawing.Point(0, 12);
			this.panelChildUser.Location = new System.Drawing.Point(0, 44);
			base.Height = 282;
			this.checkBox_RememberInfo.Checked = this.IsChildLoginSelected;
			this.textBox_AdminManager.Text = this.ChildUser;
			this.textBox_AdminName.Text = this.ChildUserName;
			this.textBox_AdminPassword.Text = this.ChildPassword;
		}

		private void checkBox_RememberInfo_CheckedChanged(object sender, EventArgs e)
		{
			if (formCloudServerAdminLogin.UserLevel == 1)
			{
				if (this.checkBox_RememberInfo.Checked)
				{
					this.IsFatherLoginSelected = true;
					return;
				}
				this.IsFatherLoginSelected = false;
				return;
			}
			else
			{
				if (this.checkBox_RememberInfo.Checked)
				{
					this.IsChildLoginSelected = true;
					return;
				}
				this.IsChildLoginSelected = false;
				return;
			}
		}

		private void textBox_AdminManager_TextChanged(object sender, EventArgs e)
		{
			this.ChildUser = this.textBox_AdminManager.Text;
		}

		private void textBox_AdminName_TextChanged(object sender, EventArgs e)
		{
			if (formCloudServerAdminLogin.UserLevel == 1)
			{
				this.FatherUserName = this.textBox_AdminName.Text;
				return;
			}
			this.ChildUserName = this.textBox_AdminName.Text;
		}

		private void textBox_AdminPassword_TextChanged(object sender, EventArgs e)
		{
			if (formCloudServerAdminLogin.UserLevel == 1)
			{
				this.FatherPassword = this.textBox_AdminPassword.Text;
				return;
			}
			this.ChildPassword = this.textBox_AdminPassword.Text;
		}

		private void VisiblePnlMessage(bool IsDisplay, string Message)
		{
			if (!IsDisplay)
			{
				this.LblMessage.Text = string.Empty;
				this.LblMessage.ForeColor = System.Drawing.Color.Black;
				this.PnlMessage.Visible = false;
				return;
			}
			if (formCloudServerAdminLogin.UserLevel == 1)
			{
				this.LblMessage.Text = Message;
				this.LblMessage.ForeColor = System.Drawing.Color.Red;
				this.PnlMessage.Visible = true;
				base.Height = 266;
				this.PnlMessage.Location = new System.Drawing.Point(0, 203);
				return;
			}
			this.LblMessage.Text = Message;
			this.LblMessage.ForeColor = System.Drawing.Color.Red;
			this.PnlMessage.Visible = true;
			base.Height = 298;
			this.PnlMessage.Location = new System.Drawing.Point(0, 235);
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
			this.checkBox_RememberInfo = new CheckBox();
			this.button_AdminCancel = new Button();
			this.button_AdminOK = new Button();
			this.label_AdminPassword = new Label();
			this.label_AdminName = new Label();
			this.textBox_AdminPassword = new TextBox();
			this.textBox_AdminName = new TextBox();
			this.BtnLevelChangeUserLogin = new Button();
			this.panelChildUser = new Panel();
			this.LblFatherUser = new Label();
			this.textBox_AdminManager = new TextBox();
			this.panelAccount = new Panel();
			this.PnlMessage = new Panel();
			this.pictureBox1 = new PictureBox();
			this.LblMessage = new Label();
			this.panelChildUser.SuspendLayout();
			this.panelAccount.SuspendLayout();
			this.PnlMessage.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.checkBox_RememberInfo.AutoSize = true;
			this.checkBox_RememberInfo.Location = new System.Drawing.Point(138, 87);
			this.checkBox_RememberInfo.Name = "checkBox_RememberInfo";
			this.checkBox_RememberInfo.Size = new System.Drawing.Size(96, 16);
			this.checkBox_RememberInfo.TabIndex = 13;
			this.checkBox_RememberInfo.Text = "记住登录信息";
			this.checkBox_RememberInfo.UseVisualStyleBackColor = true;
			this.checkBox_RememberInfo.CheckedChanged += new EventHandler(this.checkBox_RememberInfo_CheckedChanged);
			this.button_AdminCancel.Location = new System.Drawing.Point(239, 156);
			this.button_AdminCancel.Name = "button_AdminCancel";
			this.button_AdminCancel.Size = new System.Drawing.Size(75, 23);
			this.button_AdminCancel.TabIndex = 12;
			this.button_AdminCancel.Text = "取消";
			this.button_AdminCancel.UseVisualStyleBackColor = true;
			this.button_AdminCancel.Click += new EventHandler(this.button_AdminCancel_Click);
			this.button_AdminOK.Location = new System.Drawing.Point(53, 156);
			this.button_AdminOK.Name = "button_AdminOK";
			this.button_AdminOK.Size = new System.Drawing.Size(75, 23);
			this.button_AdminOK.TabIndex = 11;
			this.button_AdminOK.Text = "确定";
			this.button_AdminOK.UseVisualStyleBackColor = true;
			this.button_AdminOK.Click += new EventHandler(this.button_AdminOK_Click);
			this.label_AdminPassword.Image = Resources.key;
			this.label_AdminPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label_AdminPassword.Location = new System.Drawing.Point(39, 48);
			this.label_AdminPassword.Name = "label_AdminPassword";
			this.label_AdminPassword.Size = new System.Drawing.Size(89, 23);
			this.label_AdminPassword.TabIndex = 10;
			this.label_AdminPassword.Text = "密  码：";
			this.label_AdminPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label_AdminName.Image = Resources.user;
			this.label_AdminName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label_AdminName.Location = new System.Drawing.Point(41, 11);
			this.label_AdminName.Name = "label_AdminName";
			this.label_AdminName.Size = new System.Drawing.Size(87, 23);
			this.label_AdminName.TabIndex = 9;
			this.label_AdminName.Text = "用户名：";
			this.label_AdminName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_AdminPassword.Location = new System.Drawing.Point(138, 49);
			this.textBox_AdminPassword.Name = "textBox_AdminPassword";
			this.textBox_AdminPassword.PasswordChar = '*';
			this.textBox_AdminPassword.Size = new System.Drawing.Size(162, 21);
			this.textBox_AdminPassword.TabIndex = 8;
			this.textBox_AdminPassword.TextChanged += new EventHandler(this.textBox_AdminPassword_TextChanged);
			this.textBox_AdminName.Location = new System.Drawing.Point(138, 13);
			this.textBox_AdminName.Name = "textBox_AdminName";
			this.textBox_AdminName.Size = new System.Drawing.Size(162, 21);
			this.textBox_AdminName.TabIndex = 7;
			this.textBox_AdminName.TextChanged += new EventHandler(this.textBox_AdminName_TextChanged);
			this.BtnLevelChangeUserLogin.Location = new System.Drawing.Point(138, 119);
			this.BtnLevelChangeUserLogin.Name = "BtnLevelChangeUserLogin";
			this.BtnLevelChangeUserLogin.Size = new System.Drawing.Size(116, 23);
			this.BtnLevelChangeUserLogin.TabIndex = 14;
			this.BtnLevelChangeUserLogin.Text = "子帐户登录";
			this.BtnLevelChangeUserLogin.UseVisualStyleBackColor = true;
			this.BtnLevelChangeUserLogin.Click += new EventHandler(this.BtnLowLevelUserLogin_Click);
			this.panelChildUser.Controls.Add(this.textBox_AdminName);
			this.panelChildUser.Controls.Add(this.textBox_AdminPassword);
			this.panelChildUser.Controls.Add(this.label_AdminName);
			this.panelChildUser.Controls.Add(this.BtnLevelChangeUserLogin);
			this.panelChildUser.Controls.Add(this.label_AdminPassword);
			this.panelChildUser.Controls.Add(this.checkBox_RememberInfo);
			this.panelChildUser.Controls.Add(this.button_AdminOK);
			this.panelChildUser.Controls.Add(this.button_AdminCancel);
			this.panelChildUser.Location = new System.Drawing.Point(0, 44);
			this.panelChildUser.Name = "panelChildUser";
			this.panelChildUser.Size = new System.Drawing.Size(369, 189);
			this.panelChildUser.TabIndex = 15;
			this.LblFatherUser.Image = Resources.user_levelTop;
			this.LblFatherUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LblFatherUser.Location = new System.Drawing.Point(41, 3);
			this.LblFatherUser.Name = "LblFatherUser";
			this.LblFatherUser.Size = new System.Drawing.Size(87, 23);
			this.LblFatherUser.TabIndex = 23;
			this.LblFatherUser.Text = "父帐户：";
			this.LblFatherUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_AdminManager.Location = new System.Drawing.Point(138, 5);
			this.textBox_AdminManager.Name = "textBox_AdminManager";
			this.textBox_AdminManager.Size = new System.Drawing.Size(162, 21);
			this.textBox_AdminManager.TabIndex = 24;
			this.textBox_AdminManager.TextChanged += new EventHandler(this.textBox_AdminManager_TextChanged);
			this.panelAccount.Controls.Add(this.LblFatherUser);
			this.panelAccount.Controls.Add(this.textBox_AdminManager);
			this.panelAccount.Location = new System.Drawing.Point(0, 12);
			this.panelAccount.Name = "panelAccount";
			this.panelAccount.Size = new System.Drawing.Size(369, 30);
			this.panelAccount.TabIndex = 25;
			this.PnlMessage.BackColor = System.Drawing.Color.FromArgb(255, 255, 230);
			this.PnlMessage.Controls.Add(this.pictureBox1);
			this.PnlMessage.Controls.Add(this.LblMessage);
			this.PnlMessage.Location = new System.Drawing.Point(0, 235);
			this.PnlMessage.Name = "PnlMessage";
			this.PnlMessage.Size = new System.Drawing.Size(369, 22);
			this.PnlMessage.TabIndex = 26;
			this.pictureBox1.Image = Resources.information;
			this.pictureBox1.Location = new System.Drawing.Point(6, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			this.LblMessage.BackColor = System.Drawing.Color.FromArgb(255, 255, 230);
			this.LblMessage.Location = new System.Drawing.Point(28, 3);
			this.LblMessage.Name = "LblMessage";
			this.LblMessage.Size = new System.Drawing.Size(338, 16);
			this.LblMessage.TabIndex = 0;
			this.LblMessage.Text = "Message";
			this.LblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(369, 259);
			base.Controls.Add(this.PnlMessage);
			base.Controls.Add(this.panelAccount);
			base.Controls.Add(this.panelChildUser);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = Resources.AppIcon;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudServerAdminLogin";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "远程服务器登录";
			base.Load += new EventHandler(this.formCloudServerAdminLogin_Load);
			this.panelChildUser.ResumeLayout(false);
			this.panelChildUser.PerformLayout();
			this.panelAccount.ResumeLayout(false);
			this.panelAccount.PerformLayout();
			this.PnlMessage.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
