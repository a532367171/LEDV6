using LedControlSystem.Properties;
using LedModel;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formGroupSelect : Form
	{
		private LedPanel panel;

		private IContainer components;

		private TreeViewEnhanced tvwGroup;

		private Button btnCancel;

		private Button btnOK;

		private ImageList imgGroup;

		public formGroupSelect(LedPanel pPanel)
		{
			this.InitializeComponent();
			this.DisplayLanuageText();
			this.panel = pPanel;
		}

		private void DisplayLanuageText()
		{
			this.Text = formMain.ML.GetStr("formGroupSelect_Form_Regroup");
			this.btnOK.Text = formMain.ML.GetStr("formGroupSelect_Button_OK");
			this.btnCancel.Text = formMain.ML.GetStr("formGroupSelect_Button_Cancel");
		}

		private void formGroupSelect_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.tvwGroup.Nodes.Clear();
			LedProject ledsys = formMain.ledsys;
			IList<LedGroup> groups = ledsys.Groups;
			for (int i = 0; i < groups.Count; i++)
			{
				LedGroup ledGroup = groups[i];
				if (!(this.panel.Group == ledGroup.ID))
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = ledGroup.Name;
					treeNode.ImageIndex = 0;
					treeNode.SelectedImageIndex = 0;
					treeNode.Tag = ledGroup;
					this.tvwGroup.Nodes.Add(treeNode);
				}
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwGroup.SelectedNode;
			if (selectedNode == null)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Please_Select_Group"), formMain.ML.GetStr("Display_Prompt"));
				this.tvwGroup.Focus();
				return;
			}
			if (selectedNode.Tag.GetType() != typeof(LedGroup))
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Selected_Group_Error_And_Try_Again"), formMain.ML.GetStr("Display_Prompt"));
				this.btnCancel.Focus();
				return;
			}
			LedGroup ledGroup = selectedNode.Tag as LedGroup;
			this.panel.Group = ledGroup.ID;
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formGroupSelect));
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.imgGroup = new ImageList(this.components);
			this.tvwGroup = new TreeViewEnhanced();
			base.SuspendLayout();
			this.btnCancel.Location = new System.Drawing.Point(191, 379);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(57, 23);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnOK.Location = new System.Drawing.Point(118, 379);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(57, 23);
			this.btnOK.TabIndex = 10;
			this.btnOK.Text = "确认";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.imgGroup.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imgGroup.ImageStream");
			this.imgGroup.TransparentColor = System.Drawing.Color.Transparent;
			this.imgGroup.Images.SetKeyName(0, "group.ico");
			this.imgGroup.Images.SetKeyName(1, "applications-stack.png");
			this.imgGroup.Images.SetKeyName(2, "applications-blue.png");
			this.tvwGroup.ImageIndex = 0;
			this.tvwGroup.ImageList = this.imgGroup;
			this.tvwGroup.Location = new System.Drawing.Point(0, 0);
			this.tvwGroup.Name = "tvwGroup";
			this.tvwGroup.SelectedImageIndex = 0;
			this.tvwGroup.Size = new System.Drawing.Size(258, 361);
			this.tvwGroup.TabIndex = 12;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(258, 420);
			base.Controls.Add(this.tvwGroup);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGroupSelect";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "移动分组";
			base.Load += new EventHandler(this.formGroupSelect_Load);
			base.ResumeLayout(false);
		}
	}
}
