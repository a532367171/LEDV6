using LedControlSystem.Properties;
using LedService.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formCheckCode : Form
	{
		private bool result;

		private string oldPassword = "";

		private bool encrypted;

		private IContainer components;

		private TextBox textBox1;

		private Button button_OK;

		private Button button_Cancle;

		private Label LblPasswordIsCorrect;

		public formCheckCode()
		{
			this.InitializeComponent();
		}

		public bool Check(string pPassword, bool pEncrypted = false)
		{
			this.oldPassword = pPassword;
			this.encrypted = pEncrypted;
			base.ShowDialog();
			return this.result;
		}

		private void formCheckCode_Load(object sender, EventArgs e)
		{
			this.button_OK.Text = formMain.ML.GetStr("Button_Sure");
			this.button_Cancle.Text = formMain.ML.GetStr("Button_Cancel");
			this.Text = formMain.ML.GetStr("Prompt_PleaseEnterPassword");
			this.LblPasswordIsCorrect.Text = formMain.ML.GetStr("formCheckCode_PasswordWrong");
			this.LblPasswordIsCorrect.Visible = false;
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
		}

		private void button_OK_Click(object sender, EventArgs e)
		{
			string text = this.textBox1.Text;
			if (this.encrypted)
			{
				text = SystemTool.MD5Encode(text);
			}
			if (text == this.oldPassword)
			{
				this.result = true;
				base.Dispose();
				return;
			}
			this.LblPasswordIsCorrect.Visible = true;
			this.textBox1.Text = "";
			this.result = false;
		}

		private void button_Cancle_Click(object sender, EventArgs e)
		{
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
			this.textBox1 = new TextBox();
			this.button_OK = new Button();
			this.button_Cancle = new Button();
			this.LblPasswordIsCorrect = new Label();
			base.SuspendLayout();
			this.textBox1.Location = new System.Drawing.Point(61, 12);
			this.textBox1.Name = "textBox1";
			this.textBox1.PasswordChar = '*';
			this.textBox1.Size = new System.Drawing.Size(198, 21);
			this.textBox1.TabIndex = 0;
			this.button_OK.Location = new System.Drawing.Point(61, 63);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(75, 23);
			this.button_OK.TabIndex = 1;
			this.button_OK.Text = "确定";
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new EventHandler(this.button_OK_Click);
			this.button_Cancle.Location = new System.Drawing.Point(184, 63);
			this.button_Cancle.Name = "button_Cancle";
			this.button_Cancle.Size = new System.Drawing.Size(75, 23);
			this.button_Cancle.TabIndex = 2;
			this.button_Cancle.Text = "取消";
			this.button_Cancle.UseVisualStyleBackColor = true;
			this.button_Cancle.Click += new EventHandler(this.button_Cancle_Click);
			this.LblPasswordIsCorrect.AutoSize = true;
			this.LblPasswordIsCorrect.ForeColor = System.Drawing.Color.Red;
			this.LblPasswordIsCorrect.Location = new System.Drawing.Point(61, 41);
			this.LblPasswordIsCorrect.MinimumSize = new System.Drawing.Size(30, 12);
			this.LblPasswordIsCorrect.Name = "LblPasswordIsCorrect";
			this.LblPasswordIsCorrect.Size = new System.Drawing.Size(185, 12);
			this.LblPasswordIsCorrect.TabIndex = 3;
			this.LblPasswordIsCorrect.Text = "Enter the password incorrectly";
			base.AcceptButton = this.button_OK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(321, 91);
			base.Controls.Add(this.LblPasswordIsCorrect);
			base.Controls.Add(this.button_Cancle);
			base.Controls.Add(this.button_OK);
			base.Controls.Add(this.textBox1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "formCheckCode";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "请输入密码";
			base.Load += new EventHandler(this.formCheckCode_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
