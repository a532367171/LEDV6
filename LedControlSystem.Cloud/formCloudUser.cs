using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedModel;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.Cloud
{
	public class formCloudUser : Form
	{
		private IContainer components;

		private Button btnClose;

		private Label lblCreateTimeValue;

		private Label lblUpdateTimeValue;

		private Label lblCreateTime;

		private Label lblUpdateTime;

		private TextBox txtAddress;

		private Label lblAddress;

		private TextBox txtCompanyName;

		private Label lblCompanyName;

		private TextBox txtPhoneNumber;

		private Label lblPhoneNumber;

		private TextBox txtEmail;

		private Label lblEmail;

		private TextBox txtNickName;

		private Label lblNickName;

		private TextBox txtUserName;

		private Label lblUserName;

		public formCloudUser()
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formCloudUser_Form_VisitingCard");
			this.lblUserName.Text = formMain.ML.GetStr("formCloudUser_Label_UserName");
			this.lblNickName.Text = formMain.ML.GetStr("formCloudUser_Label_NickName");
			this.lblPhoneNumber.Text = formMain.ML.GetStr("formCloudUser_Label_PhoneNumber");
			this.lblEmail.Text = formMain.ML.GetStr("formCloudUser_Label_Email");
			this.lblCompanyName.Text = formMain.ML.GetStr("formCloudUser_Label_CompanyName");
			this.lblAddress.Text = formMain.ML.GetStr("formCloudUser_Label_Address");
			this.lblCreateTime.Text = formMain.ML.GetStr("formCloudUser_Label_CreateTime");
			this.lblUpdateTime.Text = formMain.ML.GetStr("formCloudUser_Label_UpdateTime");
			this.btnClose.Text = formMain.ML.GetStr("formCloudUser_Button_Close");
			this.txtUserName.ReadOnly = true;
			this.txtNickName.ReadOnly = true;
			this.txtEmail.ReadOnly = true;
			this.txtPhoneNumber.ReadOnly = true;
			this.txtCompanyName.ReadOnly = true;
			this.txtAddress.ReadOnly = true;
		}

		private void formCloudUser_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.txtUserName.Text = LedGlobal.CloudAccount.UserName;
			this.txtNickName.Text = LedGlobal.CloudAccount.NickName;
			this.txtEmail.Text = LedGlobal.CloudAccount.Email;
			this.txtPhoneNumber.Text = LedGlobal.CloudAccount.PhoneNumber;
			this.txtCompanyName.Text = LedGlobal.CloudAccount.CompanyName;
			this.txtAddress.Text = LedGlobal.CloudAccount.Address;
			this.lblCreateTimeValue.Text = LedGlobal.CloudAccount.CreateTime;
			this.lblUpdateTimeValue.Text = LedGlobal.CloudAccount.UpdateTime;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
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
			this.btnClose = new Button();
			this.lblCreateTimeValue = new Label();
			this.lblUpdateTimeValue = new Label();
			this.lblCreateTime = new Label();
			this.lblUpdateTime = new Label();
			this.txtAddress = new TextBox();
			this.lblAddress = new Label();
			this.txtCompanyName = new TextBox();
			this.lblCompanyName = new Label();
			this.txtPhoneNumber = new TextBox();
			this.lblPhoneNumber = new Label();
			this.txtEmail = new TextBox();
			this.lblEmail = new Label();
			this.txtNickName = new TextBox();
			this.lblNickName = new Label();
			this.txtUserName = new TextBox();
			this.lblUserName = new Label();
			base.SuspendLayout();
			this.btnClose.Location = new System.Drawing.Point(216, 345);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(69, 23);
			this.btnClose.TabIndex = 75;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.lblCreateTimeValue.Location = new System.Drawing.Point(116, 279);
			this.lblCreateTimeValue.Name = "lblCreateTimeValue";
			this.lblCreateTimeValue.Size = new System.Drawing.Size(157, 12);
			this.lblCreateTimeValue.TabIndex = 74;
			this.lblUpdateTimeValue.Location = new System.Drawing.Point(116, 302);
			this.lblUpdateTimeValue.Name = "lblUpdateTimeValue";
			this.lblUpdateTimeValue.Size = new System.Drawing.Size(157, 12);
			this.lblUpdateTimeValue.TabIndex = 73;
			this.lblCreateTime.AutoSize = true;
			this.lblCreateTime.Location = new System.Drawing.Point(24, 279);
			this.lblCreateTime.Name = "lblCreateTime";
			this.lblCreateTime.Size = new System.Drawing.Size(53, 12);
			this.lblCreateTime.TabIndex = 72;
			this.lblCreateTime.Text = "创建时间";
			this.lblUpdateTime.AutoSize = true;
			this.lblUpdateTime.Location = new System.Drawing.Point(24, 302);
			this.lblUpdateTime.Name = "lblUpdateTime";
			this.lblUpdateTime.Size = new System.Drawing.Size(53, 12);
			this.lblUpdateTime.TabIndex = 71;
			this.lblUpdateTime.Text = "更新时间";
			this.txtAddress.Location = new System.Drawing.Point(128, 211);
			this.txtAddress.Multiline = true;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(157, 51);
			this.txtAddress.TabIndex = 70;
			this.lblAddress.AutoSize = true;
			this.lblAddress.Location = new System.Drawing.Point(24, 211);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(29, 12);
			this.lblAddress.TabIndex = 69;
			this.lblAddress.Text = "地址";
			this.txtCompanyName.Location = new System.Drawing.Point(128, 170);
			this.txtCompanyName.Name = "txtCompanyName";
			this.txtCompanyName.Size = new System.Drawing.Size(157, 21);
			this.txtCompanyName.TabIndex = 68;
			this.lblCompanyName.AutoSize = true;
			this.lblCompanyName.Location = new System.Drawing.Point(24, 173);
			this.lblCompanyName.Name = "lblCompanyName";
			this.lblCompanyName.Size = new System.Drawing.Size(53, 12);
			this.lblCompanyName.TabIndex = 67;
			this.lblCompanyName.Text = "公司名称";
			this.txtPhoneNumber.Location = new System.Drawing.Point(128, 94);
			this.txtPhoneNumber.Name = "txtPhoneNumber";
			this.txtPhoneNumber.Size = new System.Drawing.Size(157, 21);
			this.txtPhoneNumber.TabIndex = 66;
			this.lblPhoneNumber.AutoSize = true;
			this.lblPhoneNumber.Location = new System.Drawing.Point(24, 97);
			this.lblPhoneNumber.Name = "lblPhoneNumber";
			this.lblPhoneNumber.Size = new System.Drawing.Size(53, 12);
			this.lblPhoneNumber.TabIndex = 65;
			this.lblPhoneNumber.Text = "电话号码";
			this.txtEmail.Location = new System.Drawing.Point(128, 132);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(157, 21);
			this.txtEmail.TabIndex = 64;
			this.lblEmail.AutoSize = true;
			this.lblEmail.Location = new System.Drawing.Point(24, 135);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(53, 12);
			this.lblEmail.TabIndex = 63;
			this.lblEmail.Text = "邮箱地址";
			this.txtNickName.Location = new System.Drawing.Point(128, 56);
			this.txtNickName.Name = "txtNickName";
			this.txtNickName.Size = new System.Drawing.Size(157, 21);
			this.txtNickName.TabIndex = 62;
			this.lblNickName.AutoSize = true;
			this.lblNickName.Location = new System.Drawing.Point(24, 59);
			this.lblNickName.Name = "lblNickName";
			this.lblNickName.Size = new System.Drawing.Size(41, 12);
			this.lblNickName.TabIndex = 61;
			this.lblNickName.Text = "用户名";
			this.txtUserName.Location = new System.Drawing.Point(128, 18);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(157, 21);
			this.txtUserName.TabIndex = 60;
			this.lblUserName.AutoSize = true;
			this.lblUserName.Location = new System.Drawing.Point(24, 21);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(41, 12);
			this.lblUserName.TabIndex = 59;
			this.lblUserName.Text = "登录名";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(324, 380);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.lblCreateTimeValue);
			base.Controls.Add(this.lblUpdateTimeValue);
			base.Controls.Add(this.lblCreateTime);
			base.Controls.Add(this.lblUpdateTime);
			base.Controls.Add(this.txtAddress);
			base.Controls.Add(this.lblAddress);
			base.Controls.Add(this.txtCompanyName);
			base.Controls.Add(this.lblCompanyName);
			base.Controls.Add(this.txtPhoneNumber);
			base.Controls.Add(this.lblPhoneNumber);
			base.Controls.Add(this.txtEmail);
			base.Controls.Add(this.lblEmail);
			base.Controls.Add(this.txtNickName);
			base.Controls.Add(this.lblNickName);
			base.Controls.Add(this.txtUserName);
			base.Controls.Add(this.lblUserName);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudUser";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "名片";
			base.Load += new EventHandler(this.formCloudUser_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
