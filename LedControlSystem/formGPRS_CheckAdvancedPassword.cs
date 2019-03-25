using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formGPRS_CheckAdvancedPassword : Form
	{
		private bool checkResult;

		private static string formID = "formGPRS_CheckAdvancedPassword";

		private IContainer components;

		private Button button1;

		private Button button2;

		private Label label1;

		private TextBox textBox1;

		private Label LblPasswordMessage;

		public static string FormID
		{
			get
			{
				return formGPRS_CheckAdvancedPassword.formID;
			}
			set
			{
				formGPRS_CheckAdvancedPassword.formID = value;
			}
		}

		public formGPRS_CheckAdvancedPassword()
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formGPRS_CheckAdvancedPassword_FormText");
			this.label1.Text = formMain.ML.GetStr("formGPRS_CheckAdvancedPassword_label_Warning");
			this.button1.Text = formMain.ML.GetStr("formGPRS_CheckAdvancedPassword_button_confirm");
			this.button2.Text = formMain.ML.GetStr("formGPRS_CheckAdvancedPassword_button_Cancel");
		}

		public bool Check(formGPRS_Send fmGPRSSend)
		{
			base.ShowDialog(fmGPRSSend);
			return this.checkResult;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.checkResult = false;
			base.Dispose();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.textBox1.Text == GprsAdministrator.Entity_Password)
			{
				this.checkResult = true;
				base.Dispose();
				return;
			}
			this.LblPasswordMessage.Visible = true;
			this.LblPasswordMessage.Text = formMain.ML.GetStr("formCheckCode_PasswordWrong");
		}

		private void formGPRS_CheckAdvancedPassword_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.LblPasswordMessage.Visible = false;
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
			this.button1 = new Button();
			this.button2 = new Button();
			this.label1 = new Label();
			this.textBox1 = new TextBox();
			this.LblPasswordMessage = new Label();
			base.SuspendLayout();
			this.button1.Location = new System.Drawing.Point(98, 121);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new System.Drawing.Point(249, 121);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label1.Location = new System.Drawing.Point(3, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(418, 62);
			this.label1.TabIndex = 2;
			this.label1.Text = "确认屏参设置和实际屏幕参数符合后，请再次输入登陆密码，并点击【确定】，或点击【取消】返回。";
			this.textBox1.Location = new System.Drawing.Point(87, 73);
			this.textBox1.Name = "textBox1";
			this.textBox1.PasswordChar = '*';
			this.textBox1.Size = new System.Drawing.Size(251, 21);
			this.textBox1.TabIndex = 4;
			this.LblPasswordMessage.ForeColor = System.Drawing.Color.Red;
			this.LblPasswordMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LblPasswordMessage.Location = new System.Drawing.Point(87, 96);
			this.LblPasswordMessage.Name = "LblPasswordMessage";
			this.LblPasswordMessage.Size = new System.Drawing.Size(100, 18);
			this.LblPasswordMessage.TabIndex = 5;
			this.LblPasswordMessage.Text = "message";
			this.LblPasswordMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(421, 155);
			base.Controls.Add(this.LblPasswordMessage);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGPRS_CheckAdvancedPassword";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "验证登录密码";
			base.Load += new EventHandler(this.formGPRS_CheckAdvancedPassword_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
