using LedControlSystem.Properties;
using LedModel.DataSource;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formDataSourceSetting : Form
	{
		private LedDataSourceSetting dataSourceSetting;

		private IContainer components;

		private Panel pnlDataSourceSetting;

		private TextBox txtInterval;

		private Label lblInterval;

		private TextBox txtCommandText;

		private Label lblCommandText;

		private ComboBox cmbDataSource;

		private Label lblDataSource;

		private Button btnCancel;

		private Button btnOK;

		private CheckBox chkEnabled;

		private Label lblIntervalUnit;

		public formDataSourceSetting(LedDataSourceSetting pDataSourceSetting)
		{
			this.dataSourceSetting = pDataSourceSetting;
			this.InitializeComponent();
		}

		private void formDataSourceSetting_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.cmbDataSource.Items.Clear();
			this.cmbDataSource.DataSource = formMain.ledsys.DataSources;
			this.cmbDataSource.DisplayMember = "Name";
			this.cmbDataSource.ValueMember = "Name";
			this.chkEnabled.Checked = this.dataSourceSetting.Enabled;
			this.pnlDataSourceSetting.Enabled = this.dataSourceSetting.Enabled;
			if (this.dataSourceSetting.DataSource != null)
			{
				this.cmbDataSource.SelectedValue = this.dataSourceSetting.DataSource.Name;
			}
			this.txtCommandText.Text = this.dataSourceSetting.CommandText;
			this.txtInterval.Text = this.dataSourceSetting.Interval.ToString();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.dataSourceSetting.Enabled = this.chkEnabled.Checked;
			LedDataSource dataSource = formMain.ledsys.GetDataSource(this.cmbDataSource.SelectedValue.ToString());
			if (dataSource == null)
			{
				if (this.dataSourceSetting.DataSource != null)
				{
					this.dataSourceSetting.DataSource.Dispose();
					this.dataSourceSetting.DataSource = null;
				}
			}
			else
			{
				this.dataSourceSetting.DataSource = dataSource;
			}
			if (this.dataSourceSetting.Enabled)
			{
				if (dataSource == null)
				{
					MessageBox.Show(this, "请选择数据源！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.cmbDataSource.Focus();
					return;
				}
				if (string.IsNullOrEmpty(this.txtCommandText.Text))
				{
					MessageBox.Show(this, "请输入SQL命令行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.txtCommandText.Focus();
					return;
				}
				if (string.IsNullOrEmpty(this.txtInterval.Text))
				{
					MessageBox.Show(this, "请输入间隔时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.txtInterval.Focus();
					return;
				}
			}
			this.dataSourceSetting.CommandText = this.txtCommandText.Text;
			int interval;
			bool flag = int.TryParse(this.txtInterval.Text, out interval);
			if (flag)
			{
				this.dataSourceSetting.Interval = interval;
			}
			this.btnCancel.PerformClick();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void chkEnabled_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if (!checkBox.Focused)
			{
				return;
			}
			this.pnlDataSourceSetting.Enabled = this.chkEnabled.Checked;
		}

		private void txtInterval_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
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
			this.pnlDataSourceSetting = new Panel();
			this.lblIntervalUnit = new Label();
			this.txtInterval = new TextBox();
			this.lblInterval = new Label();
			this.txtCommandText = new TextBox();
			this.lblCommandText = new Label();
			this.cmbDataSource = new ComboBox();
			this.lblDataSource = new Label();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.chkEnabled = new CheckBox();
			this.pnlDataSourceSetting.SuspendLayout();
			base.SuspendLayout();
			this.pnlDataSourceSetting.BorderStyle = BorderStyle.FixedSingle;
			this.pnlDataSourceSetting.Controls.Add(this.lblIntervalUnit);
			this.pnlDataSourceSetting.Controls.Add(this.txtInterval);
			this.pnlDataSourceSetting.Controls.Add(this.lblInterval);
			this.pnlDataSourceSetting.Controls.Add(this.txtCommandText);
			this.pnlDataSourceSetting.Controls.Add(this.lblCommandText);
			this.pnlDataSourceSetting.Controls.Add(this.cmbDataSource);
			this.pnlDataSourceSetting.Controls.Add(this.lblDataSource);
			this.pnlDataSourceSetting.Location = new System.Drawing.Point(12, 18);
			this.pnlDataSourceSetting.Name = "pnlDataSourceSetting";
			this.pnlDataSourceSetting.Size = new System.Drawing.Size(442, 233);
			this.pnlDataSourceSetting.TabIndex = 6;
			this.lblIntervalUnit.Location = new System.Drawing.Point(228, 188);
			this.lblIntervalUnit.Name = "lblIntervalUnit";
			this.lblIntervalUnit.Size = new System.Drawing.Size(41, 20);
			this.lblIntervalUnit.TabIndex = 18;
			this.lblIntervalUnit.Text = "(秒)";
			this.lblIntervalUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.txtInterval.Location = new System.Drawing.Point(122, 187);
			this.txtInterval.Name = "txtInterval";
			this.txtInterval.Size = new System.Drawing.Size(100, 21);
			this.txtInterval.TabIndex = 11;
			this.txtInterval.KeyPress += new KeyPressEventHandler(this.txtInterval_KeyPress);
			this.lblInterval.AutoSize = true;
			this.lblInterval.Location = new System.Drawing.Point(15, 192);
			this.lblInterval.Name = "lblInterval";
			this.lblInterval.Size = new System.Drawing.Size(65, 12);
			this.lblInterval.TabIndex = 10;
			this.lblInterval.Text = "间隔时间：";
			this.txtCommandText.Location = new System.Drawing.Point(122, 68);
			this.txtCommandText.Multiline = true;
			this.txtCommandText.Name = "txtCommandText";
			this.txtCommandText.ScrollBars = ScrollBars.Vertical;
			this.txtCommandText.Size = new System.Drawing.Size(285, 98);
			this.txtCommandText.TabIndex = 9;
			this.lblCommandText.AutoSize = true;
			this.lblCommandText.Location = new System.Drawing.Point(15, 106);
			this.lblCommandText.Name = "lblCommandText";
			this.lblCommandText.Size = new System.Drawing.Size(71, 12);
			this.lblCommandText.TabIndex = 8;
			this.lblCommandText.Text = "SQL命令行：";
			this.cmbDataSource.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDataSource.FormattingEnabled = true;
			this.cmbDataSource.Location = new System.Drawing.Point(122, 24);
			this.cmbDataSource.Name = "cmbDataSource";
			this.cmbDataSource.Size = new System.Drawing.Size(285, 20);
			this.cmbDataSource.TabIndex = 7;
			this.lblDataSource.AutoSize = true;
			this.lblDataSource.Location = new System.Drawing.Point(15, 27);
			this.lblDataSource.Name = "lblDataSource";
			this.lblDataSource.Size = new System.Drawing.Size(53, 12);
			this.lblDataSource.TabIndex = 6;
			this.lblDataSource.Text = "数据源：";
			this.btnCancel.Location = new System.Drawing.Point(379, 269);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnOK.Location = new System.Drawing.Point(287, 269);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 7;
			this.btnOK.Text = "确认";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.chkEnabled.AutoSize = true;
			this.chkEnabled.Location = new System.Drawing.Point(28, 12);
			this.chkEnabled.Name = "chkEnabled";
			this.chkEnabled.Size = new System.Drawing.Size(15, 14);
			this.chkEnabled.TabIndex = 14;
			this.chkEnabled.UseVisualStyleBackColor = true;
			this.chkEnabled.CheckedChanged += new EventHandler(this.chkEnabled_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(466, 304);
			base.Controls.Add(this.chkEnabled);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.pnlDataSourceSetting);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formDataSourceSetting";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "数据源设置";
			base.Load += new EventHandler(this.formDataSourceSetting_Load);
			this.pnlDataSourceSetting.ResumeLayout(false);
			this.pnlDataSourceSetting.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
