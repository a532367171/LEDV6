using LedControlSystem.Properties;
using LedModel.Public;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formReName : Form
	{
		public string name_text_str = "";

		public bool rename_result;

		private IContainer components;

		private Button button_ReName_confirm;

		private TextBox textBox_ReName_Text;

		private Button button_ReName_cancel;

		private Label label_NewName;

		public formReName()
		{
			this.InitializeComponent();
			foreach (LedPublicText arg_2F_0 in formMain.Ledsys.PublicTexts)
			{
			}
		}

		public void Set_Text_Name()
		{
			this.textBox_ReName_Text.Text = this.name_text_str;
		}

		public bool PubTexts_Is_Exist(string pub_name)
		{
			foreach (LedPublicText current in formMain.Ledsys.PublicTexts)
			{
				if (pub_name == current.Name)
				{
					return true;
				}
			}
			return false;
		}

		private void button_ReName_confirm_Click(object sender, EventArgs e)
		{
			if (this.textBox_ReName_Text.Text == "" || this.textBox_ReName_Text.Text == string.Empty)
			{
				MessageBox.Show(formMain.ML.GetStr("formReName_Message_NULL_Name"));
				this.textBox_ReName_Text.Text = string.Empty;
				return;
			}
			if (this.PubTexts_Is_Exist(this.textBox_ReName_Text.Text))
			{
				MessageBox.Show(formMain.ML.GetStr("formReName_Message_Name_IsExist"));
				return;
			}
			this.rename_result = true;
			this.name_text_str = this.textBox_ReName_Text.Text;
			base.Close();
		}

		private void button_ReName_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void formReName_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.Text = formMain.ML.GetStr("formReName_FormText");
			this.button_ReName_confirm.Text = formMain.ML.GetStr("Button_Sure");
			this.button_ReName_cancel.Text = formMain.ML.GetStr("Button_Cancel");
			this.label_NewName.Text = formMain.ML.GetStr("formReName_Label_ReName");
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
			this.button_ReName_confirm = new Button();
			this.textBox_ReName_Text = new TextBox();
			this.button_ReName_cancel = new Button();
			this.label_NewName = new Label();
			base.SuspendLayout();
			this.button_ReName_confirm.Location = new System.Drawing.Point(31, 79);
			this.button_ReName_confirm.Name = "button_ReName_confirm";
			this.button_ReName_confirm.Size = new System.Drawing.Size(58, 23);
			this.button_ReName_confirm.TabIndex = 0;
			this.button_ReName_confirm.Text = "确认";
			this.button_ReName_confirm.UseVisualStyleBackColor = true;
			this.button_ReName_confirm.Click += new EventHandler(this.button_ReName_confirm_Click);
			this.textBox_ReName_Text.Location = new System.Drawing.Point(31, 41);
			this.textBox_ReName_Text.Name = "textBox_ReName_Text";
			this.textBox_ReName_Text.Size = new System.Drawing.Size(193, 21);
			this.textBox_ReName_Text.TabIndex = 2;
			this.button_ReName_cancel.Location = new System.Drawing.Point(166, 79);
			this.button_ReName_cancel.Name = "button_ReName_cancel";
			this.button_ReName_cancel.Size = new System.Drawing.Size(58, 23);
			this.button_ReName_cancel.TabIndex = 3;
			this.button_ReName_cancel.Text = "取消";
			this.button_ReName_cancel.UseVisualStyleBackColor = true;
			this.button_ReName_cancel.Click += new EventHandler(this.button_ReName_cancel_Click);
			this.label_NewName.AutoSize = true;
			this.label_NewName.Location = new System.Drawing.Point(32, 22);
			this.label_NewName.Name = "label_NewName";
			this.label_NewName.Size = new System.Drawing.Size(29, 12);
			this.label_NewName.TabIndex = 4;
			this.label_NewName.Text = "名称";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(252, 114);
			base.Icon = Resources.AppIcon;
			base.Controls.Add(this.label_NewName);
			base.Controls.Add(this.button_ReName_cancel);
			base.Controls.Add(this.textBox_ReName_Text);
			base.Controls.Add(this.button_ReName_confirm);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formReName";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "formReName";
			base.Load += new EventHandler(this.formReName_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
