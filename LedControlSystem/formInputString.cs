using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formInputString : Form
	{
		private string result;

		private IContainer components;

		private Button button_Ok;

		private Button button_Cancel;

		private Label label1;

		private TextBox textBox1;

		public formInputString()
		{
			this.InitializeComponent();
		}

		private void formInputString_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.button_Cancel.Text = formMain.ML.GetStr("Button_Cancel");
			this.button_Ok.Text = formMain.ML.GetStr("Button_Sure");
		}

		public string Get(string pText)
		{
			this.label1.Text = pText;
			base.ShowDialog();
			return this.result;
		}

		private void button_Ok_Click(object sender, EventArgs e)
		{
			this.result = this.textBox1.Text;
			base.Dispose();
		}

		private void button_Cancel_Click(object sender, EventArgs e)
		{
			this.result = null;
			base.Dispose();
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
			this.button_Ok = new Button();
			this.button_Cancel = new Button();
			this.label1 = new Label();
			this.textBox1 = new TextBox();
			base.SuspendLayout();
			this.button_Ok.Location = new System.Drawing.Point(144, 90);
			this.button_Ok.Name = "button_Ok";
			this.button_Ok.Size = new System.Drawing.Size(75, 23);
			this.button_Ok.TabIndex = 0;
			this.button_Ok.Text = "确定";
			this.button_Ok.UseVisualStyleBackColor = true;
			this.button_Ok.Click += new EventHandler(this.button_Ok_Click);
			this.button_Cancel.Location = new System.Drawing.Point(46, 90);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.button_Cancel.TabIndex = 1;
			this.button_Cancel.Text = "取消";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new EventHandler(this.button_Cancel_Click);
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(245, 41);
			this.label1.TabIndex = 2;
			this.label1.Text = "label1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.textBox1.Location = new System.Drawing.Point(14, 53);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(243, 21);
			this.textBox1.TabIndex = 3;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(277, 121);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_Ok);
			base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			base.Name = "formInputString";
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += new EventHandler(this.formInputString_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
