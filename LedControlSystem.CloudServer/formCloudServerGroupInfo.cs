using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.CloudServer
{
	public class formCloudServerGroupInfo : Form
	{
		public IList<GroupOfCloudServer> Groups = new List<GroupOfCloudServer>();

		public Dictionary<int, GroupOfCloudServer> DicGroups = new Dictionary<int, GroupOfCloudServer>();

		public static GroupOfCloudServer selectedGroup;

		private IContainer components;

		private ComboBox CmbGroups;

		private Label LblGroupsText;

		private Label LblCreatedAtText;

		private Label LblUpdatedText;

		private Label LblCustomerText;

		private DataGridView DgvGroups;

		private DataGridViewTextBoxColumn TerminalName;

		private DataGridViewTextBoxColumn No;

		private Label LblCustomer;

		private Label LblCreatedAt;

		private Label LblUpdatedAt;

		public formCloudServerGroupInfo()
		{
			this.InitializeComponent();
		}

		private void formCloudServerGroupInfo_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.Text = formMain.ML.GetStr("formCloudServerGroupInfo_FormText");
			this.LblGroupsText.Text = formMain.ML.GetStr("formCloudServerGroupInfo_Lbl_Groups");
			this.LblCustomerText.Text = formMain.ML.GetStr("formCloudServerGroupInfo_Lbl_Customer");
			this.LblCreatedAtText.Text = formMain.ML.GetStr("formCloudServerGroupInfo_Lbl_CreateAt");
			this.LblUpdatedText.Text = formMain.ML.GetStr("formCloudServerGroupInfo_Lbl_UpdateAt");
			this.DgvGroups.Columns[0].HeaderText = formMain.ML.GetStr("formCloudServerGroupInfo_Dgv_TerminalName");
			this.DgvGroups.Columns[1].HeaderText = formMain.ML.GetStr("formCloudServerGroupInfo_Dgv_Number");
			if (this.Groups.Count > 0)
			{
				for (int i = 0; i < this.Groups.Count; i++)
				{
					this.CmbGroups.Items.Add(this.Groups[i].GroupName);
					this.DicGroups.Add(i, this.Groups[i]);
				}
				this.CmbGroups.SelectedIndex = 0;
				formCloudServerGroupInfo.selectedGroup = this.DicGroups[0];
				this.LblCustomer.Text = this.DicGroups[0].CustomerName;
				this.LblCreatedAt.Text = this.DicGroups[0].CreatedAt;
				this.LblUpdatedAt.Text = this.DicGroups[0].UpdatedAt;
			}
		}

		private void CmbGroups_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.DgvGroups.Rows.Clear();
			foreach (GroupTerminal current in this.DicGroups[this.CmbGroups.SelectedIndex].GroupTerminals)
			{
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.DgvGroups);
				dataGridViewRow.Cells[0].Value = current.terminalName;
				dataGridViewRow.Cells[1].Value = current.terminalCode;
				this.DgvGroups.Rows.Add(dataGridViewRow);
			}
			this.LblCustomer.Text = this.DicGroups[this.CmbGroups.SelectedIndex].CustomerName;
			this.LblCreatedAt.Text = this.DicGroups[this.CmbGroups.SelectedIndex].CreatedAt;
			this.LblUpdatedAt.Text = this.DicGroups[this.CmbGroups.SelectedIndex].UpdatedAt;
			formCloudServerGroupInfo.selectedGroup = this.DicGroups[this.CmbGroups.SelectedIndex];
		}

		private void formCloudServerGroupInfo_FormClosed(object sender, FormClosedEventArgs e)
		{
			formCloudServerGroupInfo.selectedGroup = null;
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.CmbGroups = new ComboBox();
			this.LblGroupsText = new Label();
			this.LblCreatedAtText = new Label();
			this.LblUpdatedText = new Label();
			this.LblCustomerText = new Label();
			this.DgvGroups = new DataGridView();
			this.TerminalName = new DataGridViewTextBoxColumn();
			this.No = new DataGridViewTextBoxColumn();
			this.LblCustomer = new Label();
			this.LblCreatedAt = new Label();
			this.LblUpdatedAt = new Label();
			((ISupportInitialize)this.DgvGroups).BeginInit();
			base.SuspendLayout();
			this.CmbGroups.FormattingEnabled = true;
			this.CmbGroups.Location = new System.Drawing.Point(137, 28);
			this.CmbGroups.Name = "CmbGroups";
			this.CmbGroups.Size = new System.Drawing.Size(185, 20);
			this.CmbGroups.TabIndex = 0;
			this.CmbGroups.SelectedIndexChanged += new EventHandler(this.CmbGroups_SelectedIndexChanged);
			this.LblGroupsText.AutoSize = true;
			this.LblGroupsText.Location = new System.Drawing.Point(27, 31);
			this.LblGroupsText.Name = "LblGroupsText";
			this.LblGroupsText.Size = new System.Drawing.Size(41, 12);
			this.LblGroupsText.TabIndex = 1;
			this.LblGroupsText.Text = "分组：";
			this.LblGroupsText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblCreatedAtText.AutoSize = true;
			this.LblCreatedAtText.Location = new System.Drawing.Point(27, 115);
			this.LblCreatedAtText.Name = "LblCreatedAtText";
			this.LblCreatedAtText.Size = new System.Drawing.Size(65, 12);
			this.LblCreatedAtText.TabIndex = 2;
			this.LblCreatedAtText.Text = "创建时间：";
			this.LblCreatedAtText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblUpdatedText.AutoSize = true;
			this.LblUpdatedText.Location = new System.Drawing.Point(27, 157);
			this.LblUpdatedText.Name = "LblUpdatedText";
			this.LblUpdatedText.Size = new System.Drawing.Size(65, 12);
			this.LblUpdatedText.TabIndex = 3;
			this.LblUpdatedText.Text = "更新时间：";
			this.LblUpdatedText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblCustomerText.AutoSize = true;
			this.LblCustomerText.Location = new System.Drawing.Point(27, 76);
			this.LblCustomerText.Name = "LblCustomerText";
			this.LblCustomerText.Size = new System.Drawing.Size(53, 12);
			this.LblCustomerText.TabIndex = 4;
			this.LblCustomerText.Text = "创建人：";
			this.LblCustomerText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DgvGroups.AllowUserToAddRows = false;
			this.DgvGroups.AllowUserToResizeRows = false;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.DgvGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.DgvGroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvGroups.Columns.AddRange(new DataGridViewColumn[]
			{
				this.TerminalName,
				this.No
			});
			this.DgvGroups.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.DgvGroups.Location = new System.Drawing.Point(12, 195);
			this.DgvGroups.Name = "DgvGroups";
			this.DgvGroups.RowHeadersVisible = false;
			this.DgvGroups.RowTemplate.Height = 23;
			this.DgvGroups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.DgvGroups.Size = new System.Drawing.Size(367, 263);
			this.DgvGroups.TabIndex = 9;
			this.TerminalName.HeaderText = "终端名称";
			this.TerminalName.Name = "TerminalName";
			this.TerminalName.Width = 220;
			this.No.HeaderText = "编号";
			this.No.Name = "No";
			this.No.Width = 144;
			this.LblCustomer.AutoSize = true;
			this.LblCustomer.Location = new System.Drawing.Point(137, 76);
			this.LblCustomer.MinimumSize = new System.Drawing.Size(20, 12);
			this.LblCustomer.Name = "LblCustomer";
			this.LblCustomer.Size = new System.Drawing.Size(20, 12);
			this.LblCustomer.TabIndex = 10;
			this.LblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblCreatedAt.AutoSize = true;
			this.LblCreatedAt.Location = new System.Drawing.Point(137, 115);
			this.LblCreatedAt.MinimumSize = new System.Drawing.Size(20, 12);
			this.LblCreatedAt.Name = "LblCreatedAt";
			this.LblCreatedAt.Size = new System.Drawing.Size(20, 12);
			this.LblCreatedAt.TabIndex = 11;
			this.LblCreatedAt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LblUpdatedAt.AutoSize = true;
			this.LblUpdatedAt.Location = new System.Drawing.Point(137, 157);
			this.LblUpdatedAt.MinimumSize = new System.Drawing.Size(20, 12);
			this.LblUpdatedAt.Name = "LblUpdatedAt";
			this.LblUpdatedAt.Size = new System.Drawing.Size(20, 12);
			this.LblUpdatedAt.TabIndex = 12;
			this.LblUpdatedAt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(391, 470);
			base.Controls.Add(this.LblUpdatedAt);
			base.Controls.Add(this.LblCreatedAt);
			base.Controls.Add(this.LblCustomer);
			base.Controls.Add(this.DgvGroups);
			base.Controls.Add(this.LblCustomerText);
			base.Controls.Add(this.LblUpdatedText);
			base.Controls.Add(this.LblCreatedAtText);
			base.Controls.Add(this.LblGroupsText);
			base.Controls.Add(this.CmbGroups);
			base.Icon = Resources.AppIcon;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudServerGroupInfo";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "组信息";
			base.FormClosed += new FormClosedEventHandler(this.formCloudServerGroupInfo_FormClosed);
			base.Load += new EventHandler(this.formCloudServerGroupInfo_Load);
			((ISupportInitialize)this.DgvGroups).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
