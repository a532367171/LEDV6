using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedService.Const;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.Cloud
{
	public class formCloudForgotPassword : Form
	{
		private int mode;

		private IContainer components;

		private WebBrowser wbsForgotPassword;

		public formCloudForgotPassword(int pMode = 0)
		{
			this.InitializeComponent();
			this.mode = pMode;
			this.Text = formMain.ML.GetStr("formCloudForgotPassword_Form_CloudForgotPassword");
		}

		private void formCloudForgotPassword_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			string format = "{0}/zcc/web/password/forgot";
			if (this.mode == 2)
			{
				format = "{0}/zcc/web/password/mobile-forgot";
			}
			this.wbsForgotPassword.Navigate(string.Format(format, CommonConst.CloudServer));
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
			this.wbsForgotPassword = new WebBrowser();
			base.SuspendLayout();
			this.wbsForgotPassword.Dock = DockStyle.Fill;
			this.wbsForgotPassword.Location = new System.Drawing.Point(0, 0);
			this.wbsForgotPassword.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbsForgotPassword.Name = "wbsForgotPassword";
			this.wbsForgotPassword.Size = new System.Drawing.Size(959, 687);
			this.wbsForgotPassword.TabIndex = 1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(959, 687);
			base.Controls.Add(this.wbsForgotPassword);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudForgotPassword";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "云服务忘记密码";
			base.Load += new EventHandler(this.formCloudForgotPassword_Load);
			base.ResumeLayout(false);
		}
	}
}
