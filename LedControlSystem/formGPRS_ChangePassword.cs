using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formGPRS_ChangePassword : Form
	{
		private string id = "";

		private bool isUser;

		private static string formID = "formGPRS_ChangePassword";

		private bool operationResult;

		private IContainer components;

		private Label label2;

		private TextBox textBox_NewPassword;

		private Label label3;

		private TextBox textBox_NewPasswordCon;

		private Button button1;

		private Button button_Cancel;

		private TextBox textBox_OldPassword;

		private Label label4;

		public static string FormID
		{
			get
			{
				return formGPRS_ChangePassword.formID;
			}
			set
			{
				formGPRS_ChangePassword.formID = value;
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

		public formGPRS_ChangePassword()
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formGPRS_ChangePassword_FormText");
			this.label4.Text = formMain.ML.GetStr("formGPRS_ChangePassword_label_oldPassword");
			this.label2.Text = formMain.ML.GetStr("formGPRS_ChangePassword_label_NewPassword");
			this.label3.Text = formMain.ML.GetStr("formGPRS_ChangePassword_label_Confirm_Password");
			this.button1.Text = formMain.ML.GetStr("formGPRS_ChangePassword_button_confirm");
			this.button_Cancel.Text = formMain.ML.GetStr("formGPRS_ChangePassword_button_Cancel");
		}

		private void formGPRS_ChangePassword_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
		}

		public bool ChangePassword(string pID, string pUsername, formGPRS_Send fmGPRSSend)
		{
			this.id = pID;
			base.ShowDialog(fmGPRSSend);
			return this.operationResult;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.textBox_OldPassword.Text.Length < 8)
			{
				MessageBox.Show(this, formMain.ML.GetStr("GPRS_InvalidOldPassword"));
				return;
			}
			if (this.textBox_NewPassword.Text.Length < 8)
			{
				MessageBox.Show(this, formMain.ML.GetStr("GPRS_InvalidNewPassword"));
				return;
			}
			if (this.textBox_NewPassword.Text != this.textBox_NewPasswordCon.Text)
			{
				MessageBox.Show(this, formMain.ML.GetStr("GPRS_NewPassrodDiff"));
				return;
			}
			if (this.isUser)
			{
				this.operationResult = GprsAdministrator.API_UserChangePassword(this.id, this.textBox_OldPassword.Text.Trim(), this.textBox_NewPassword.Text.Trim());
				if (this.operationResult)
				{
					MessageBox.Show(this, formMain.ML.GetStr("GPRS_Change_Success"));
					base.Dispose();
					return;
				}
				MessageBox.Show(formMain.ML.GetStr("GPRS_Change_Failed"));
				return;
			}
			else
			{
				this.operationResult = GprsAdministrator.API_ChangePassword(this.id, this.textBox_OldPassword.Text.Trim(), this.textBox_NewPassword.Text.Trim());
				if (this.operationResult)
				{
					MessageBox.Show(this, formMain.ML.GetStr("GPRS_Change_Success"));
					base.Dispose();
					return;
				}
				MessageBox.Show(formMain.ML.GetStr("GPRS_Change_Failed"));
				return;
			}
		}

		private void button_Cancel_Click(object sender, EventArgs e)
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
			this.label2 = new Label();
			this.textBox_NewPassword = new TextBox();
			this.label3 = new Label();
			this.textBox_NewPasswordCon = new TextBox();
			this.button1 = new Button();
			this.button_Cancel = new Button();
			this.textBox_OldPassword = new TextBox();
			this.label4 = new Label();
			base.SuspendLayout();
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "新密码";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_NewPassword.Location = new System.Drawing.Point(119, 42);
			this.textBox_NewPassword.Name = "textBox_NewPassword";
			this.textBox_NewPassword.PasswordChar = '*';
			this.textBox_NewPassword.Size = new System.Drawing.Size(194, 21);
			this.textBox_NewPassword.TabIndex = 2;
			this.label3.Location = new System.Drawing.Point(12, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 27);
			this.label3.TabIndex = 3;
			this.label3.Text = "确认新密码";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_NewPasswordCon.Location = new System.Drawing.Point(119, 79);
			this.textBox_NewPasswordCon.Name = "textBox_NewPasswordCon";
			this.textBox_NewPasswordCon.PasswordChar = '*';
			this.textBox_NewPasswordCon.Size = new System.Drawing.Size(194, 21);
			this.textBox_NewPasswordCon.TabIndex = 4;
			this.button1.Location = new System.Drawing.Point(56, 128);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button_Cancel.Location = new System.Drawing.Point(196, 128);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.button_Cancel.TabIndex = 6;
			this.button_Cancel.Text = "取消";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new EventHandler(this.button_Cancel_Click);
			this.textBox_OldPassword.Location = new System.Drawing.Point(119, 6);
			this.textBox_OldPassword.Name = "textBox_OldPassword";
			this.textBox_OldPassword.PasswordChar = '*';
			this.textBox_OldPassword.Size = new System.Drawing.Size(194, 21);
			this.textBox_OldPassword.TabIndex = 0;
			this.label4.Location = new System.Drawing.Point(12, 5);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "原密码";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(325, 163);
			base.Controls.Add(this.textBox_OldPassword);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.textBox_NewPasswordCon);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBox_NewPassword);
			base.Controls.Add(this.label2);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGPRS_ChangePassword";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "修改密码";
			base.Load += new EventHandler(this.formGPRS_ChangePassword_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
