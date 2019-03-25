using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedService.Const;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.Cloud
{
	public class formCloudManagement : Form
	{
		private IContainer components;

		private WebBrowser wbsManagement;

		public formCloudManagement()
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formCloudManagement_Form_CloudManagement");
		}

		private void formCloudManagement_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.wbsManagement.Navigate(string.Format("{0}/zcc/web/login", CommonConst.CloudServer));
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
			this.wbsManagement = new WebBrowser();
			base.SuspendLayout();
			this.wbsManagement.Dock = DockStyle.Fill;
			this.wbsManagement.Location = new System.Drawing.Point(0, 0);
			this.wbsManagement.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbsManagement.Name = "wbsManagement";
			this.wbsManagement.Size = new System.Drawing.Size(959, 687);
			this.wbsManagement.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(959, 687);
			base.Controls.Add(this.wbsManagement);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudManagement";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "云服务管理";
			base.Load += new EventHandler(this.formCloudManagement_Load);
			base.ResumeLayout(false);
		}
	}
}
