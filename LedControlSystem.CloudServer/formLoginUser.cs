using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.CloudServer
{
	public class formLoginUser : Form
	{
		public CloudServerUser LoggedInUser = new CloudServerUser();

		private IContainer components;

		private Label LblCreateAtDisplay;

		private Label LblUpdateDisplay;

		private Label LblCreateAt;

		private Label LblUpdateAt;

		private TextBox TxtCompanyAddress;

		private Label LblCompanyAddress;

		private TextBox TxtCompanyName;

		private Label LblCompanyName;

		private TextBox TxtPhoneNumber;

		private Label LblPhoneNumber;

		private TextBox TxtEmail;

		private Label LblEmail;

		private TextBox TxtNickName;

		private Label LblNickName;

		private TextBox TxtUserName;

		private Label LblUserName;

		private Button Btnconfirm;

		private Button BtnCancel;

		public formLoginUser()
		{
			this.InitializeComponent();
		}

		private void formLoginUser_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.TxtUserName.Text = this.LoggedInUser.UserName;
			this.TxtNickName.Text = this.LoggedInUser.NickName;
			this.TxtEmail.Text = this.LoggedInUser.Email;
			this.TxtPhoneNumber.Text = this.LoggedInUser.PhoneNumber;
			this.TxtCompanyName.Text = this.LoggedInUser.CompanyName;
			this.TxtCompanyAddress.Text = this.LoggedInUser.Address;
			this.LblCreateAtDisplay.Text = this.LoggedInUser.CreatedAt;
			this.LblUpdateDisplay.Text = this.LoggedInUser.UpdateAt;
			this.Text = formMain.ML.GetStr("formLoginUser_FormText");
			this.LblUserName.Text = formMain.ML.GetStr("formLoginUser_Lbl_UserName");
			this.LblNickName.Text = formMain.ML.GetStr("formLoginUser_Lbl_LoginName");
			this.LblPhoneNumber.Text = formMain.ML.GetStr("formLoginUser_Lbl_PhoneNumber");
			this.LblEmail.Text = formMain.ML.GetStr("formLoginUser_Lbl_Mail");
			this.LblCompanyName.Text = formMain.ML.GetStr("formLoginUser_Lbl_CompanyName");
			this.LblCompanyAddress.Text = formMain.ML.GetStr("formLoginUser_Lbl_CompanyAddress");
			this.LblCreateAt.Text = formMain.ML.GetStr("formLoginUser_Lbl_CreateAt");
			this.LblUpdateAt.Text = formMain.ML.GetStr("formLoginUser_Lbl_UpdateAt");
			this.Btnconfirm.Text = formMain.ML.GetStr("Global_Messagebox_OK");
			this.BtnCancel.Text = formMain.ML.GetStr("Global_Messagebox_Cancel");
		}

		private void Btnconfirm_Click(object sender, EventArgs e)
		{
			base.Dispose();
		}

		private void BtnCancel_Click(object sender, EventArgs e)
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
			this.LblCreateAtDisplay = new Label();
			this.LblUpdateDisplay = new Label();
			this.LblCreateAt = new Label();
			this.LblUpdateAt = new Label();
			this.TxtCompanyAddress = new TextBox();
			this.LblCompanyAddress = new Label();
			this.TxtCompanyName = new TextBox();
			this.LblCompanyName = new Label();
			this.TxtPhoneNumber = new TextBox();
			this.LblPhoneNumber = new Label();
			this.TxtEmail = new TextBox();
			this.LblEmail = new Label();
			this.TxtNickName = new TextBox();
			this.LblNickName = new Label();
			this.TxtUserName = new TextBox();
			this.LblUserName = new Label();
			this.Btnconfirm = new Button();
			this.BtnCancel = new Button();
			base.SuspendLayout();
			this.LblCreateAtDisplay.Location = new System.Drawing.Point(116, 279);
			this.LblCreateAtDisplay.Name = "LblCreateAtDisplay";
			this.LblCreateAtDisplay.Size = new System.Drawing.Size(157, 12);
			this.LblCreateAtDisplay.TabIndex = 56;
			this.LblUpdateDisplay.Location = new System.Drawing.Point(116, 302);
			this.LblUpdateDisplay.Name = "LblUpdateDisplay";
			this.LblUpdateDisplay.Size = new System.Drawing.Size(157, 12);
			this.LblUpdateDisplay.TabIndex = 55;
			this.LblCreateAt.AutoSize = true;
			this.LblCreateAt.Location = new System.Drawing.Point(24, 279);
			this.LblCreateAt.Name = "LblCreateAt";
			this.LblCreateAt.Size = new System.Drawing.Size(65, 12);
			this.LblCreateAt.TabIndex = 54;
			this.LblCreateAt.Text = "创建时间：";
			this.LblUpdateAt.AutoSize = true;
			this.LblUpdateAt.Location = new System.Drawing.Point(24, 302);
			this.LblUpdateAt.Name = "LblUpdateAt";
			this.LblUpdateAt.Size = new System.Drawing.Size(65, 12);
			this.LblUpdateAt.TabIndex = 53;
			this.LblUpdateAt.Text = "更新时间：";
			this.TxtCompanyAddress.Location = new System.Drawing.Point(128, 208);
			this.TxtCompanyAddress.Multiline = true;
			this.TxtCompanyAddress.Name = "TxtCompanyAddress";
			this.TxtCompanyAddress.Size = new System.Drawing.Size(157, 51);
			this.TxtCompanyAddress.TabIndex = 52;
			this.LblCompanyAddress.AutoSize = true;
			this.LblCompanyAddress.Location = new System.Drawing.Point(24, 211);
			this.LblCompanyAddress.Name = "LblCompanyAddress";
			this.LblCompanyAddress.Size = new System.Drawing.Size(53, 12);
			this.LblCompanyAddress.TabIndex = 51;
			this.LblCompanyAddress.Text = "公司地址";
			this.TxtCompanyName.Location = new System.Drawing.Point(128, 170);
			this.TxtCompanyName.Name = "TxtCompanyName";
			this.TxtCompanyName.Size = new System.Drawing.Size(157, 21);
			this.TxtCompanyName.TabIndex = 50;
			this.LblCompanyName.AutoSize = true;
			this.LblCompanyName.Location = new System.Drawing.Point(24, 173);
			this.LblCompanyName.Name = "LblCompanyName";
			this.LblCompanyName.Size = new System.Drawing.Size(53, 12);
			this.LblCompanyName.TabIndex = 49;
			this.LblCompanyName.Text = "公司名称";
			this.TxtPhoneNumber.Location = new System.Drawing.Point(128, 97);
			this.TxtPhoneNumber.Name = "TxtPhoneNumber";
			this.TxtPhoneNumber.Size = new System.Drawing.Size(157, 21);
			this.TxtPhoneNumber.TabIndex = 48;
			this.LblPhoneNumber.AutoSize = true;
			this.LblPhoneNumber.Location = new System.Drawing.Point(24, 100);
			this.LblPhoneNumber.Name = "LblPhoneNumber";
			this.LblPhoneNumber.Size = new System.Drawing.Size(53, 12);
			this.LblPhoneNumber.TabIndex = 47;
			this.LblPhoneNumber.Text = "电话号码";
			this.TxtEmail.Location = new System.Drawing.Point(128, 133);
			this.TxtEmail.Name = "TxtEmail";
			this.TxtEmail.Size = new System.Drawing.Size(157, 21);
			this.TxtEmail.TabIndex = 46;
			this.LblEmail.AutoSize = true;
			this.LblEmail.Location = new System.Drawing.Point(24, 136);
			this.LblEmail.Name = "LblEmail";
			this.LblEmail.Size = new System.Drawing.Size(53, 12);
			this.LblEmail.TabIndex = 45;
			this.LblEmail.Text = "邮箱地址";
			this.TxtNickName.Location = new System.Drawing.Point(128, 58);
			this.TxtNickName.Name = "TxtNickName";
			this.TxtNickName.Size = new System.Drawing.Size(157, 21);
			this.TxtNickName.TabIndex = 44;
			this.LblNickName.AutoSize = true;
			this.LblNickName.Location = new System.Drawing.Point(24, 61);
			this.LblNickName.Name = "LblNickName";
			this.LblNickName.Size = new System.Drawing.Size(41, 12);
			this.LblNickName.TabIndex = 43;
			this.LblNickName.Text = "登录名";
			this.TxtUserName.Location = new System.Drawing.Point(128, 18);
			this.TxtUserName.Name = "TxtUserName";
			this.TxtUserName.Size = new System.Drawing.Size(157, 21);
			this.TxtUserName.TabIndex = 42;
			this.LblUserName.AutoSize = true;
			this.LblUserName.Location = new System.Drawing.Point(24, 21);
			this.LblUserName.Name = "LblUserName";
			this.LblUserName.Size = new System.Drawing.Size(41, 12);
			this.LblUserName.TabIndex = 41;
			this.LblUserName.Text = "用户名";
			this.Btnconfirm.Location = new System.Drawing.Point(116, 343);
			this.Btnconfirm.Name = "Btnconfirm";
			this.Btnconfirm.Size = new System.Drawing.Size(69, 23);
			this.Btnconfirm.TabIndex = 57;
			this.Btnconfirm.Text = "确认";
			this.Btnconfirm.UseVisualStyleBackColor = true;
			this.Btnconfirm.Click += new EventHandler(this.Btnconfirm_Click);
			this.BtnCancel.Location = new System.Drawing.Point(204, 343);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(69, 23);
			this.BtnCancel.TabIndex = 58;
			this.BtnCancel.Text = "取消";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new EventHandler(this.BtnCancel_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(324, 380);
			base.Controls.Add(this.BtnCancel);
			base.Controls.Add(this.Btnconfirm);
			base.Controls.Add(this.LblCreateAtDisplay);
			base.Controls.Add(this.LblUpdateDisplay);
			base.Controls.Add(this.LblCreateAt);
			base.Controls.Add(this.LblUpdateAt);
			base.Controls.Add(this.TxtCompanyAddress);
			base.Controls.Add(this.LblCompanyAddress);
			base.Controls.Add(this.TxtCompanyName);
			base.Controls.Add(this.LblCompanyName);
			base.Controls.Add(this.TxtPhoneNumber);
			base.Controls.Add(this.LblPhoneNumber);
			base.Controls.Add(this.TxtEmail);
			base.Controls.Add(this.LblEmail);
			base.Controls.Add(this.TxtNickName);
			base.Controls.Add(this.LblNickName);
			base.Controls.Add(this.TxtUserName);
			base.Controls.Add(this.LblUserName);
			base.Icon = Resources.AppIcon;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formLoginUser";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "名片";
			base.Load += new EventHandler(this.formLoginUser_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
