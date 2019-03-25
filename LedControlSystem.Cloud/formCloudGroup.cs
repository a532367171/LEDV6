using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedModel;
using LedService.Cloud.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.Cloud
{
	public class formCloudGroup : Form
	{
		private IList<GroupInfo> groups;

		private bool needtoClose;

		private IContainer components;

		private DataGridView dgvGroupTerminal;

		private DataGridViewTextBoxColumn TerminalName;

		private DataGridViewTextBoxColumn No;

		private Label lblCustomerName;

		private Label lblUpdatedAt;

		private Label lblCreatedAt;

		private Label lblGroup;

		private ComboBox cmbGroup;

		private Label lblUpdatedAtValue;

		private Label lblCreatedAtValue;

		private Label lblCustomerNameValue;

		private PictureBox picLoading;

		public formCloudGroup()
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formCloudGroup_Form_GroupInfo");
			this.lblGroup.Text = formMain.ML.GetStr("formCloudGroup_Label_Groups");
			this.lblCustomerName.Text = formMain.ML.GetStr("formCloudGroup_Label_CustomerName");
			this.lblCreatedAt.Text = formMain.ML.GetStr("formCloudGroup_Label_CreateTime");
			this.lblUpdatedAt.Text = formMain.ML.GetStr("formCloudGroup_Label_UpdateTime");
			this.dgvGroupTerminal.Columns[0].HeaderText = formMain.ML.GetStr("formCloudGroup_DataGridView_TerminalName");
			this.dgvGroupTerminal.Columns[1].HeaderText = formMain.ML.GetStr("formCloudGroup_DataGridView_No");
		}

		private void formCloudGroup_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.needtoClose = false;
			Thread thread = new Thread(new ThreadStart(this.Group));
			thread.Start();
		}

		private void formCloudGroup_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
			}
		}

		private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.LoadGroup(comboBox.SelectedIndex);
		}

		private void Group()
		{
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(500);
			int i = 0;
			while (i < 5)
			{
				i++;
				this.groups = new GroupService().GetList(LedGlobal.CloudAccount.SessionID);
				if (this.groups != null)
				{
					break;
				}
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadGroup();
				this.LoadingVisible(false);
			}));
			Thread.Sleep(500);
			this.needtoClose = true;
		}

		private void LoadGroup()
		{
			this.cmbGroup.Items.Clear();
			this.lblCustomerNameValue.Text = string.Empty;
			this.lblCreatedAtValue.Text = string.Empty;
			this.lblUpdatedAtValue.Text = string.Empty;
			this.dgvGroupTerminal.Rows.Clear();
			if (this.groups == null || this.groups.Count == 0)
			{
				return;
			}
			for (int i = this.groups.Count - 1; i > -1; i--)
			{
				GroupInfo groupInfo = this.groups[i];
				if (groupInfo.Terminals != null && groupInfo.Terminals.Count > 0)
				{
					for (int j = groupInfo.Terminals.Count - 1; j > -1; j--)
					{
						string[] array = groupInfo.Terminals[j];
						bool flag = false;
						if (array == null || array.Length == 0)
						{
							flag = true;
						}
						else if (!array[3].Contains(formMain.CloudModelDescription))
						{
							flag = true;
						}
						if (flag)
						{
							groupInfo.Terminals.RemoveAt(j);
						}
					}
				}
				if (groupInfo.Terminals == null || groupInfo.Terminals.Count == 0)
				{
					this.groups.RemoveAt(i);
				}
			}
			for (int k = 0; k < this.groups.Count; k++)
			{
				GroupInfo groupInfo2 = this.groups[k];
				string text = groupInfo2.Name;
				if (!string.IsNullOrEmpty(groupInfo2.CreatorID))
				{
					text = text + "->" + groupInfo2.CreatorName;
				}
				this.cmbGroup.Items.Add(text);
			}
			this.LoadGroup(0);
			this.cmbGroup.SelectedIndex = 0;
		}

		private void LoadGroup(int index)
		{
			if (this.groups != null && index >= this.groups.Count)
			{
				return;
			}
			GroupInfo groupInfo = this.groups[index];
			this.lblCustomerNameValue.Text = groupInfo.CustomerName;
			this.lblCreatedAtValue.Text = groupInfo.CreateTime;
			this.lblUpdatedAtValue.Text = groupInfo.UpdateTime;
			this.dgvGroupTerminal.Rows.Clear();
			if (groupInfo.Terminals != null && groupInfo.Terminals.Count > 0)
			{
				for (int i = 0; i < groupInfo.Terminals.Count; i++)
				{
					string[] array = groupInfo.Terminals[i];
					if (array.Length - 2 == this.dgvGroupTerminal.ColumnCount)
					{
						DataGridViewRow dataGridViewRow = new DataGridViewRow();
						dataGridViewRow.CreateCells(this.dgvGroupTerminal);
						for (int j = 1; j < array.Length - 1; j++)
						{
							dataGridViewRow.Cells[j - 1].Value = array[j];
						}
						this.dgvGroupTerminal.Rows.Add(dataGridViewRow);
					}
				}
			}
		}

		public void LoadingVisible(bool pBool)
		{
			if (base.Controls != null && base.Controls.Count > 0)
			{
				foreach (Control control in base.Controls)
				{
					if (control != null && control.Name != "picLoading")
					{
						control.Enabled = !pBool;
					}
				}
			}
			if (pBool)
			{
				this.picLoading.Visible = true;
				this.picLoading.BringToFront();
				return;
			}
			this.picLoading.Visible = false;
			this.picLoading.SendToBack();
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
			this.dgvGroupTerminal = new DataGridView();
			this.TerminalName = new DataGridViewTextBoxColumn();
			this.No = new DataGridViewTextBoxColumn();
			this.lblCustomerName = new Label();
			this.lblUpdatedAt = new Label();
			this.lblCreatedAt = new Label();
			this.lblGroup = new Label();
			this.cmbGroup = new ComboBox();
			this.lblUpdatedAtValue = new Label();
			this.lblCreatedAtValue = new Label();
			this.lblCustomerNameValue = new Label();
			this.picLoading = new PictureBox();
			((ISupportInitialize)this.dgvGroupTerminal).BeginInit();
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.dgvGroupTerminal.AllowUserToAddRows = false;
			this.dgvGroupTerminal.AllowUserToResizeRows = false;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvGroupTerminal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvGroupTerminal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvGroupTerminal.Columns.AddRange(new DataGridViewColumn[]
			{
				this.TerminalName,
				this.No
			});
			this.dgvGroupTerminal.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.dgvGroupTerminal.Location = new System.Drawing.Point(9, 184);
			this.dgvGroupTerminal.MultiSelect = false;
			this.dgvGroupTerminal.Name = "dgvGroupTerminal";
			this.dgvGroupTerminal.ReadOnly = true;
			this.dgvGroupTerminal.RowHeadersVisible = false;
			this.dgvGroupTerminal.RowTemplate.Height = 23;
			this.dgvGroupTerminal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvGroupTerminal.Size = new System.Drawing.Size(367, 263);
			this.dgvGroupTerminal.TabIndex = 18;
			this.TerminalName.HeaderText = "终端名称";
			this.TerminalName.Name = "TerminalName";
			this.TerminalName.ReadOnly = true;
			this.TerminalName.Width = 220;
			this.No.HeaderText = "编号";
			this.No.Name = "No";
			this.No.ReadOnly = true;
			this.No.Width = 144;
			this.lblCustomerName.AutoSize = true;
			this.lblCustomerName.Location = new System.Drawing.Point(24, 62);
			this.lblCustomerName.Name = "lblCustomerName";
			this.lblCustomerName.Size = new System.Drawing.Size(65, 12);
			this.lblCustomerName.TabIndex = 17;
			this.lblCustomerName.Text = "客户名称：";
			this.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblUpdatedAt.AutoSize = true;
			this.lblUpdatedAt.Location = new System.Drawing.Point(24, 146);
			this.lblUpdatedAt.Name = "lblUpdatedAt";
			this.lblUpdatedAt.Size = new System.Drawing.Size(65, 12);
			this.lblUpdatedAt.TabIndex = 16;
			this.lblUpdatedAt.Text = "更新时间：";
			this.lblUpdatedAt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblCreatedAt.AutoSize = true;
			this.lblCreatedAt.Location = new System.Drawing.Point(24, 104);
			this.lblCreatedAt.Name = "lblCreatedAt";
			this.lblCreatedAt.Size = new System.Drawing.Size(65, 12);
			this.lblCreatedAt.TabIndex = 15;
			this.lblCreatedAt.Text = "创建时间：";
			this.lblCreatedAt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblGroup.AutoSize = true;
			this.lblGroup.Location = new System.Drawing.Point(24, 20);
			this.lblGroup.Name = "lblGroup";
			this.lblGroup.Size = new System.Drawing.Size(41, 12);
			this.lblGroup.TabIndex = 14;
			this.lblGroup.Text = "分组：";
			this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbGroup.FormattingEnabled = true;
			this.cmbGroup.Location = new System.Drawing.Point(136, 17);
			this.cmbGroup.Name = "cmbGroup";
			this.cmbGroup.Size = new System.Drawing.Size(185, 20);
			this.cmbGroup.TabIndex = 13;
			this.cmbGroup.SelectedIndexChanged += new EventHandler(this.cmbGroup_SelectedIndexChanged);
			this.lblUpdatedAtValue.AutoSize = true;
			this.lblUpdatedAtValue.Location = new System.Drawing.Point(134, 146);
			this.lblUpdatedAtValue.MinimumSize = new System.Drawing.Size(20, 12);
			this.lblUpdatedAtValue.Name = "lblUpdatedAtValue";
			this.lblUpdatedAtValue.Size = new System.Drawing.Size(20, 12);
			this.lblUpdatedAtValue.TabIndex = 21;
			this.lblUpdatedAtValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblCreatedAtValue.AutoSize = true;
			this.lblCreatedAtValue.Location = new System.Drawing.Point(134, 104);
			this.lblCreatedAtValue.MinimumSize = new System.Drawing.Size(20, 12);
			this.lblCreatedAtValue.Name = "lblCreatedAtValue";
			this.lblCreatedAtValue.Size = new System.Drawing.Size(20, 12);
			this.lblCreatedAtValue.TabIndex = 20;
			this.lblCreatedAtValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblCustomerNameValue.AutoSize = true;
			this.lblCustomerNameValue.Location = new System.Drawing.Point(134, 62);
			this.lblCustomerNameValue.MinimumSize = new System.Drawing.Size(20, 12);
			this.lblCustomerNameValue.Name = "lblCustomerNameValue";
			this.lblCustomerNameValue.Size = new System.Drawing.Size(20, 12);
			this.lblCustomerNameValue.TabIndex = 19;
			this.lblCustomerNameValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.picLoading.Image = Resources.loading;
			this.picLoading.Location = new System.Drawing.Point(161, 202);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(63, 58);
			this.picLoading.SizeMode = PictureBoxSizeMode.StretchImage;
			this.picLoading.TabIndex = 0;
			this.picLoading.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(384, 462);
			base.Controls.Add(this.picLoading);
			base.Controls.Add(this.dgvGroupTerminal);
			base.Controls.Add(this.lblCustomerName);
			base.Controls.Add(this.lblUpdatedAt);
			base.Controls.Add(this.lblCreatedAt);
			base.Controls.Add(this.lblGroup);
			base.Controls.Add(this.cmbGroup);
			base.Controls.Add(this.lblUpdatedAtValue);
			base.Controls.Add(this.lblCreatedAtValue);
			base.Controls.Add(this.lblCustomerNameValue);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudGroup";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "分组信息";
			base.FormClosing += new FormClosingEventHandler(this.formCloudGroup_FormClosing);
			base.Load += new EventHandler(this.formCloudGroup_Load);
			((ISupportInitialize)this.dgvGroupTerminal).EndInit();
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
