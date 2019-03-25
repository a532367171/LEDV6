using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedService.Const;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.Cloud
{
	public class formCloudRegister : Form
	{
		private int mode;

		private IContainer components;

		private WebBrowser wbsRegister;

		public formCloudRegister(int pMode = 0)
		{
			this.InitializeComponent();
			this.mode = pMode;
			this.Text = formMain.ML.GetStr("formCloudRegister_Form_CloudRegister");
		}

		private void formCloudRegister_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			string format = "{0}/zcc/web/register";
			if (this.mode == 2)
			{
				format = "{0}/zcc/web/mobile-register";
			}
			this.wbsRegister.Navigate(string.Format(format, CommonConst.CloudServer));
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
			this.wbsRegister = new WebBrowser();
			base.SuspendLayout();
			this.wbsRegister.Dock = DockStyle.Fill;
			this.wbsRegister.Location = new System.Drawing.Point(0, 0);
			this.wbsRegister.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbsRegister.Name = "wbsRegister";
			this.wbsRegister.Size = new System.Drawing.Size(959, 687);
			this.wbsRegister.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(959, 687);
			base.Controls.Add(this.wbsRegister);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudRegister";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "云服务注册";
			base.Load += new EventHandler(this.formCloudRegister_Load);
			base.ResumeLayout(false);
		}
	}
}
