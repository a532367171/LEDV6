using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formGetString : Form
	{
		private string value;

		private IContainer components;

		private TextBox valueTextBox;

		private Button button1;

		private Button button2;

		public formGetString()
		{
			this.InitializeComponent();
			this.button1.Text = formMain.ML.GetStr("Button_Sure");
			this.button2.Text = formMain.ML.GetStr("Button_Cancel");
		}

		private void valueTextBox_TextChanged(object sender, EventArgs e)
		{
			this.value = this.valueTextBox.Text;
		}

		public string GetString(string PTitle)
		{
			this.Text = PTitle;
			base.ShowDialog();
			return this.value.Trim();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.value))
			{
				MessageBox.Show(this, formMain.ML.GetStr("Prompt_ProjectNameNotEmpty"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			if (Regex.IsMatch(this.value, "[\\/*:?\"<>|]"))
			{
				MessageBox.Show(this, string.Format(formMain.ML.GetStr("Prompt_ProjectNameNotContainsSpecialCharacters"), "\\ / * : ? \" < > |"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			base.Dispose();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.value = "";
			base.Dispose();
		}

		private void formGetString_Load(object sender, EventArgs e)
		{
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
			this.valueTextBox = new TextBox();
			this.button1 = new Button();
			this.button2 = new Button();
			base.SuspendLayout();
			this.valueTextBox.Location = new System.Drawing.Point(38, 23);
			this.valueTextBox.Name = "valueTextBox";
			this.valueTextBox.Size = new System.Drawing.Size(302, 21);
			this.valueTextBox.TabIndex = 0;
			this.valueTextBox.TextChanged += new EventHandler(this.valueTextBox_TextChanged);
			this.button1.Location = new System.Drawing.Point(98, 80);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new System.Drawing.Point(210, 80);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(382, 129);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.valueTextBox);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGetString";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "formGetString";
			base.Load += new EventHandler(this.formGetString_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
