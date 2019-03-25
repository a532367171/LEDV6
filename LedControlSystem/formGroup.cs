using LedControlSystem.Properties;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formGroup : Form
	{
		public LedGroup group;

		private IContainer components;

		private Label lblGroupName;

		private Button btnCancel;

		private TextBox txtGroupName;

		private Button btnOK;

		public formGroup()
		{
			this.InitializeComponent();
			this.DisplayLanuageText();
		}

		private void DisplayLanuageText()
		{
			this.Text = formMain.ML.GetStr("formGroup_Form_GroupName");
			this.lblGroupName.Text = formMain.ML.GetStr("formGroup_Label_Input_GroupName");
			this.btnOK.Text = formMain.ML.GetStr("formGroup_Button_OK");
			this.btnCancel.Text = formMain.ML.GetStr("formGroup_Button_Cancel");
		}

		private void formGroup_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			string text = string.Empty;
			for (int i = 1; i < 9999; i++)
			{
				bool flag = false;
				text = formMain.ML.GetStr("Display_group_add") + i.ToString();
				foreach (LedGroup current in formMain.ledsys.Groups)
				{
					if (current.Name == text)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
			}
			this.txtGroupName.Text = text;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			string text = this.txtGroupName.Text;
			bool flag = false;
			foreach (LedGroup current in formMain.ledsys.Groups)
			{
				if (current.Name == text)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Group_Name_Cannot_Be_Repeated"), formMain.ML.GetStr("Display_Prompt"));
				this.txtGroupName.Focus();
				return;
			}
			this.group = new LedGroup();
			this.group.Name = text;
			this.group.Description = text;
			this.group.CreationMethod = LedCreationMethod.Manual;
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
			this.lblGroupName = new Label();
			this.btnCancel = new Button();
			this.txtGroupName = new TextBox();
			this.btnOK = new Button();
			base.SuspendLayout();
			this.lblGroupName.AutoSize = true;
			this.lblGroupName.Location = new System.Drawing.Point(32, 22);
			this.lblGroupName.Name = "lblGroupName";
			this.lblGroupName.Size = new System.Drawing.Size(53, 12);
			this.lblGroupName.TabIndex = 8;
			this.lblGroupName.Text = "输入分组";
			this.btnCancel.Location = new System.Drawing.Point(154, 79);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(70, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.txtGroupName.Location = new System.Drawing.Point(31, 41);
			this.txtGroupName.Name = "txtGroupName";
			this.txtGroupName.Size = new System.Drawing.Size(193, 21);
			this.txtGroupName.TabIndex = 6;
			this.btnOK.Location = new System.Drawing.Point(31, 79);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(70, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "确认";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(252, 114);
			base.Controls.Add(this.lblGroupName);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.txtGroupName);
			base.Controls.Add(this.btnOK);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGroup";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "分组名称";
			base.Load += new EventHandler(this.formGroup_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
