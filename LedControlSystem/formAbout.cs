using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formAbout : Form
	{
		private static string formID = "formAbout";

		private IContainer components;

		private Label lblVersion;

		private Button button1;

		private Label lblTitle;

		public static string FormID
		{
			get
			{
				return formAbout.formID;
			}
			set
			{
				formAbout.formID = value;
			}
		}

		public formAbout()
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formAbout.formID;
			this.Text = formMain.ML.GetStr("formAbout_FormText");
			this.lblTitle.Text = formMain.ML.GetStr("formAbout_label_Title");
			this.button1.Text = formMain.ML.GetStr("formAbout_button_OK");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Dispose();
		}

		private void formAbout_Load(object sender, EventArgs e)
		{
			this.lblTitle.Text = formMain.ledTitle;
			this.lblVersion.Text = formMain.ledVersion;
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
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
			this.lblVersion = new Label();
			this.button1 = new Button();
			this.lblTitle = new Label();
			base.SuspendLayout();
			this.lblVersion.AutoSize = true;
			this.lblVersion.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.lblVersion.Location = new System.Drawing.Point(49, 109);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(94, 24);
			this.lblVersion.TabIndex = 0;
			this.lblVersion.Text = "Version";
			this.button1.Location = new System.Drawing.Point(439, 178);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("宋体", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.lblTitle.Location = new System.Drawing.Point(47, 44);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(105, 35);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "Title";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(558, 231);
			base.Controls.Add(this.lblTitle);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.lblVersion);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formAbout";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "关于";
			base.Load += new EventHandler(this.formAbout_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
