using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formSharedItemRename : Form
	{
		public string itemName;

		private IContainer components;

		private Label lblItemName;

		private TextBox txtItemName;

		private Button btnOK;

		private Button btnCancel;

		private Label lblHint;

		private PictureBox picInformation;

		public formSharedItemRename()
		{
			this.InitializeComponent();
			this.picInformation.Image = Resources.information_share_program;
			this.picInformation.SizeMode = PictureBoxSizeMode.StretchImage;
			this.DisplayLanuageText();
		}

		private void DisplayLanuageText()
		{
			this.Text = formMain.ML.GetStr("formSharedItemRename_FormText");
			this.lblItemName.Text = formMain.ML.GetStr("formSharedItemRename_Label_ItemName");
			this.lblHint.Text = formMain.ML.GetStr("formSharedItemRename_Label_Hint");
			this.btnOK.Text = formMain.ML.GetStr("formSharedItemRename_Button_OK");
			this.btnCancel.Text = formMain.ML.GetStr("formSharedItemRename_Button_Cancel");
		}

		private void formSharedItemRename_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.itemName = string.Empty;
			this.txtItemName.Text = string.Format("{0}-{1}", formMain.ML.GetStr("formSharedItemRename_ItemName_SharedItem"), formMain.ledsys.SharedItemNoCounter);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtItemName.Text))
			{
				MessageBox.Show(this, formMain.ML.GetStr("formSharedItemRename_Message_ItemNameCanNotBeEmpty"));
				this.txtItemName.Focus();
				return;
			}
			this.itemName = this.txtItemName.Text;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
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
			this.lblItemName = new Label();
			this.txtItemName = new TextBox();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.lblHint = new Label();
			this.picInformation = new PictureBox();
			((ISupportInitialize)this.picInformation).BeginInit();
			base.SuspendLayout();
			this.lblItemName.AutoSize = true;
			this.lblItemName.Location = new System.Drawing.Point(12, 73);
			this.lblItemName.Name = "lblItemName";
			this.lblItemName.Size = new System.Drawing.Size(53, 12);
			this.lblItemName.TabIndex = 0;
			this.lblItemName.Text = "节目名称";
			this.txtItemName.Location = new System.Drawing.Point(104, 70);
			this.txtItemName.Name = "txtItemName";
			this.txtItemName.Size = new System.Drawing.Size(245, 21);
			this.txtItemName.TabIndex = 1;
			this.btnOK.Location = new System.Drawing.Point(180, 107);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Location = new System.Drawing.Point(274, 107);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.lblHint.Location = new System.Drawing.Point(52, 15);
			this.lblHint.Name = "lblHint";
			this.lblHint.Size = new System.Drawing.Size(297, 49);
			this.lblHint.TabIndex = 4;
			this.lblHint.Text = "共享节目之前，建议您将该节目名称修改为醒目的名称。";
			this.picInformation.Location = new System.Drawing.Point(14, 15);
			this.picInformation.Name = "picInformation";
			this.picInformation.Size = new System.Drawing.Size(32, 32);
			this.picInformation.TabIndex = 5;
			this.picInformation.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(370, 142);
			base.Controls.Add(this.picInformation);
			base.Controls.Add(this.lblHint);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.txtItemName);
			base.Controls.Add(this.lblItemName);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formSharedItemRename";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "提示";
			base.Load += new EventHandler(this.formSharedItemRename_Load);
			((ISupportInitialize)this.picInformation).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
