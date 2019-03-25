using LedControlSystem.Properties;
using LedModel.DataSource;
using LedModel.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formDataSource : Form
	{
		private LedDataSource dataSource;

		private IContainer components;

		private DataGridView dgvList;

		private TextBox txtServer;

		private TextBox txtDatabase;

		private TextBox txtPort;

		private TextBox txtUserName;

		private TextBox txtPassword;

		private Label lblServer;

		private Label lblPort;

		private Label lblDatabase;

		private Label lblUserName;

		private Label lblPassword;

		private Label lblDataSourceType;

		private RadioButton rdoOracle;

		private RadioButton rdoSQLServer;

		private RadioButton rdoMySQL;

		private ToolStrip tsMenu;

		private ToolStripButton tsbtnAdd;

		private ToolStripButton tsbtnDelete;

		private Button btnOK;

		private Button btnCancel;

		private Panel pnlSetting;

		private DataGridViewTextBoxColumn colID;

		private DataGridViewTextBoxColumn colNo;

		private DataGridViewTextBoxColumn colName;

		private DataGridViewTextBoxColumn colConnectionString;

		private DataGridViewButtonColumn colEdit;

		public formDataSource()
		{
			this.InitializeComponent();
		}

		private void formDataSource_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.dgvList.Rows.Clear();
			this.InitControl();
			int num = 0;
			foreach (LedDataSource current in formMain.Ledsys.DataSources)
			{
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.dgvList);
				dataGridViewRow.Cells[2].Value = current.Name;
				dataGridViewRow.Cells[3].Value = current.GetConnectionString();
				this.dgvList.Rows.Add(dataGridViewRow);
				if (num == 0)
				{
					this.dataSource = current;
					this.Binding(current);
				}
				num++;
			}
		}

		private void tsbtnAdd_Click(object sender, EventArgs e)
		{
			this.Save(this.dataSource);
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			dataGridViewRow.CreateCells(this.dgvList);
			for (int i = 1; i < 9999; i++)
			{
				string text = "数据源" + i.ToString();
				LedDataSource ledDataSource = formMain.Ledsys.AddDataSource(text);
				if (ledDataSource != null)
				{
					dataGridViewRow.Cells[2].Value = text;
					this.dataSource = ledDataSource;
					this.Binding(ledDataSource);
					break;
				}
			}
			this.dgvList.Rows.Add(dataGridViewRow);
			dataGridViewRow.Selected = true;
		}

		private void tsbtnDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "是否删除所选数据源？", formMain.ML.GetStr("UpdateButton_Delete"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK && this.dgvList.Rows.Count > 0)
			{
				try
				{
					foreach (DataGridViewRow dataGridViewRow in this.dgvList.SelectedRows)
					{
						formMain.Ledsys.RemoveDataSource(dataGridViewRow.Cells[2].Value.ToString());
						this.dataSource = null;
						this.dgvList.Rows.Remove(dataGridViewRow);
					}
					if (this.dgvList.Rows.Count > 0)
					{
						DataGridViewRow dataGridViewRow2 = this.dgvList.Rows[this.dgvList.Rows.Count - 1];
						dataGridViewRow2.Selected = true;
						this.dataSource = formMain.ledsys.GetDataSource(dataGridViewRow2.Cells[2].Value.ToString());
						this.Binding(this.dataSource);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Save(this.dataSource);
			this.btnCancel.PerformClick();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void dgvList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, this.dgvList.RowHeadersWidth, e.RowBounds.Height);
			TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), this.dgvList.RowHeadersDefaultCellStyle.Font, bounds, this.dgvList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
		}

		private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			DataGridViewRow dataGridViewRow = this.dgvList.Rows[e.RowIndex];
			string text = dataGridViewRow.Cells[2].Value.ToString();
			this.dataSource = formMain.ledsys.GetDataSource(text);
			this.Binding(this.dataSource);
			dataGridViewRow.Cells[3].Value = this.dataSource.GetConnectionString();
			if (e.ColumnIndex == 4)
			{
				formReName formReName = new formReName();
				formReName.name_text_str = text;
				formReName.Set_Text_Name();
				formReName.ShowDialog();
				if (formReName.rename_result)
				{
					string name_text_str = formReName.name_text_str;
					if (text == name_text_str)
					{
						return;
					}
					formMain.Ledsys.RenameDataSource(text, name_text_str);
					dataGridViewRow.Cells[2].Value = name_text_str;
				}
			}
		}

		private void dgvList_SelectionChanged(object sender, EventArgs e)
		{
			if (this.dgvList.Rows.Count == 0)
			{
				this.InitControl();
				return;
			}
			this.Save(this.dataSource);
		}

		private void rdoMySQL_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked && this.dataSource != null)
			{
				this.dataSource.DataSourceType = LedDataSourceType.MySQL;
				this.txtPort.Text = this.dataSource.Port;
			}
		}

		private void rdoSQLServer_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked && this.dataSource != null)
			{
				this.dataSource.DataSourceType = LedDataSourceType.SQLServer;
				this.txtPort.Text = this.dataSource.Port;
			}
		}

		private void rdoOracle_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked && this.dataSource != null)
			{
				this.dataSource.DataSourceType = LedDataSourceType.Oracle;
				this.txtPort.Text = this.dataSource.Port;
			}
		}

		private void InitControl()
		{
			this.dataSource = null;
			this.pnlSetting.Enabled = false;
			this.rdoMySQL.Checked = false;
			this.rdoSQLServer.Checked = false;
			this.rdoOracle.Checked = false;
			this.txtServer.Text = string.Empty;
			this.txtPort.Text = string.Empty;
			this.txtDatabase.Text = string.Empty;
			this.txtUserName.Text = string.Empty;
			this.txtPassword.Text = string.Empty;
		}

		private void Binding(LedDataSource dataSource)
		{
			this.pnlSetting.Enabled = true;
			switch (dataSource.DataSourceType)
			{
			case LedDataSourceType.MySQL:
				this.rdoMySQL.Checked = true;
				this.rdoSQLServer.Checked = false;
				this.rdoOracle.Checked = false;
				break;
			case LedDataSourceType.SQLServer:
				this.rdoMySQL.Checked = false;
				this.rdoSQLServer.Checked = true;
				this.rdoOracle.Checked = false;
				break;
			case LedDataSourceType.Oracle:
				this.rdoMySQL.Checked = false;
				this.rdoSQLServer.Checked = false;
				this.rdoOracle.Checked = true;
				break;
			default:
				this.rdoMySQL.Checked = false;
				this.rdoSQLServer.Checked = false;
				this.rdoOracle.Checked = false;
				break;
			}
			this.txtServer.Text = dataSource.Server;
			this.txtPort.Text = dataSource.Port;
			this.txtDatabase.Text = dataSource.Database;
			this.txtUserName.Text = dataSource.Username;
			this.txtPassword.Text = dataSource.Password;
			this.txtServer.Focus();
		}

		private void Save(LedDataSource dataSource)
		{
			if (dataSource != null)
			{
				dataSource.Server = this.txtServer.Text;
				dataSource.Port = this.txtPort.Text;
				dataSource.Database = this.txtDatabase.Text;
				dataSource.Username = this.txtUserName.Text;
				dataSource.Password = this.txtPassword.Text;
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.dgvList = new DataGridView();
			this.txtServer = new TextBox();
			this.txtDatabase = new TextBox();
			this.txtPort = new TextBox();
			this.txtUserName = new TextBox();
			this.txtPassword = new TextBox();
			this.lblServer = new Label();
			this.lblPort = new Label();
			this.lblDatabase = new Label();
			this.lblUserName = new Label();
			this.lblPassword = new Label();
			this.lblDataSourceType = new Label();
			this.rdoOracle = new RadioButton();
			this.rdoSQLServer = new RadioButton();
			this.rdoMySQL = new RadioButton();
			this.tsMenu = new ToolStrip();
			this.tsbtnAdd = new ToolStripButton();
			this.tsbtnDelete = new ToolStripButton();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.pnlSetting = new Panel();
			this.colID = new DataGridViewTextBoxColumn();
			this.colNo = new DataGridViewTextBoxColumn();
			this.colName = new DataGridViewTextBoxColumn();
			this.colConnectionString = new DataGridViewTextBoxColumn();
			this.colEdit = new DataGridViewButtonColumn();
			((ISupportInitialize)this.dgvList).BeginInit();
			this.tsMenu.SuspendLayout();
			this.pnlSetting.SuspendLayout();
			base.SuspendLayout();
			this.dgvList.AllowUserToAddRows = false;
			this.dgvList.AllowUserToResizeRows = false;
			this.dgvList.BackgroundColor = System.Drawing.SystemColors.Menu;
			this.dgvList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvList.Columns.AddRange(new DataGridViewColumn[]
			{
				this.colID,
				this.colNo,
				this.colName,
				this.colConnectionString,
				this.colEdit
			});
			this.dgvList.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.dgvList.Location = new System.Drawing.Point(12, 52);
			this.dgvList.MultiSelect = false;
			this.dgvList.Name = "dgvList";
			this.dgvList.ReadOnly = true;
			this.dgvList.RowHeadersVisible = false;
			this.dgvList.RowTemplate.Height = 23;
			this.dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvList.Size = new System.Drawing.Size(664, 168);
			this.dgvList.TabIndex = 0;
			this.dgvList.CellClick += new DataGridViewCellEventHandler(this.dgvList_CellClick);
			this.dgvList.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dgvList_RowPostPaint);
			this.dgvList.SelectionChanged += new EventHandler(this.dgvList_SelectionChanged);
			this.txtServer.Location = new System.Drawing.Point(112, 53);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(100, 21);
			this.txtServer.TabIndex = 1;
			this.txtDatabase.Location = new System.Drawing.Point(562, 53);
			this.txtDatabase.Name = "txtDatabase";
			this.txtDatabase.Size = new System.Drawing.Size(100, 21);
			this.txtDatabase.TabIndex = 2;
			this.txtPort.Location = new System.Drawing.Point(334, 53);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(100, 21);
			this.txtPort.TabIndex = 3;
			this.txtUserName.Location = new System.Drawing.Point(112, 104);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(100, 21);
			this.txtUserName.TabIndex = 4;
			this.txtPassword.Location = new System.Drawing.Point(335, 104);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(100, 21);
			this.txtPassword.TabIndex = 5;
			this.lblServer.AutoSize = true;
			this.lblServer.Location = new System.Drawing.Point(39, 56);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(41, 12);
			this.lblServer.TabIndex = 6;
			this.lblServer.Text = "地址：";
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new System.Drawing.Point(262, 56);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(41, 12);
			this.lblPort.TabIndex = 6;
			this.lblPort.Text = "端口：";
			this.lblDatabase.AutoSize = true;
			this.lblDatabase.Location = new System.Drawing.Point(461, 56);
			this.lblDatabase.Name = "lblDatabase";
			this.lblDatabase.Size = new System.Drawing.Size(65, 12);
			this.lblDatabase.TabIndex = 6;
			this.lblDatabase.Text = "数据库名：";
			this.lblUserName.AutoSize = true;
			this.lblUserName.Location = new System.Drawing.Point(27, 107);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(53, 12);
			this.lblUserName.TabIndex = 6;
			this.lblUserName.Text = "用户名：";
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(262, 107);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(41, 12);
			this.lblPassword.TabIndex = 6;
			this.lblPassword.Text = "密码：";
			this.lblDataSourceType.AutoSize = true;
			this.lblDataSourceType.Location = new System.Drawing.Point(3, 17);
			this.lblDataSourceType.Name = "lblDataSourceType";
			this.lblDataSourceType.Size = new System.Drawing.Size(77, 12);
			this.lblDataSourceType.TabIndex = 7;
			this.lblDataSourceType.Text = "数据源类型：";
			this.rdoOracle.AutoSize = true;
			this.rdoOracle.Location = new System.Drawing.Point(339, 15);
			this.rdoOracle.Name = "rdoOracle";
			this.rdoOracle.Size = new System.Drawing.Size(59, 16);
			this.rdoOracle.TabIndex = 8;
			this.rdoOracle.TabStop = true;
			this.rdoOracle.Text = "Oracle";
			this.rdoOracle.UseVisualStyleBackColor = true;
			this.rdoOracle.CheckedChanged += new EventHandler(this.rdoOracle_CheckedChanged);
			this.rdoSQLServer.AutoSize = true;
			this.rdoSQLServer.Location = new System.Drawing.Point(212, 15);
			this.rdoSQLServer.Name = "rdoSQLServer";
			this.rdoSQLServer.Size = new System.Drawing.Size(77, 16);
			this.rdoSQLServer.TabIndex = 8;
			this.rdoSQLServer.TabStop = true;
			this.rdoSQLServer.Text = "SQLServer";
			this.rdoSQLServer.UseVisualStyleBackColor = true;
			this.rdoSQLServer.CheckedChanged += new EventHandler(this.rdoSQLServer_CheckedChanged);
			this.rdoMySQL.AutoSize = true;
			this.rdoMySQL.Location = new System.Drawing.Point(112, 15);
			this.rdoMySQL.Name = "rdoMySQL";
			this.rdoMySQL.Size = new System.Drawing.Size(53, 16);
			this.rdoMySQL.TabIndex = 8;
			this.rdoMySQL.TabStop = true;
			this.rdoMySQL.Text = "MySQL";
			this.rdoMySQL.UseVisualStyleBackColor = true;
			this.rdoMySQL.CheckedChanged += new EventHandler(this.rdoMySQL_CheckedChanged);
			this.tsMenu.AutoSize = false;
			this.tsMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tsMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnAdd,
				this.tsbtnDelete
			});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(688, 49);
			this.tsMenu.TabIndex = 17;
			this.tsMenu.Text = "菜单";
			this.tsbtnAdd.Image = Resources.PublicText_Add;
			this.tsbtnAdd.ImageScaling = ToolStripItemImageScaling.None;
			this.tsbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAdd.Margin = new Padding(10, 1, 0, 2);
			this.tsbtnAdd.Name = "tsbtnAdd";
			this.tsbtnAdd.Size = new System.Drawing.Size(36, 46);
			this.tsbtnAdd.Text = "添加";
			this.tsbtnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.tsbtnAdd.TextImageRelation = TextImageRelation.ImageAboveText;
			this.tsbtnAdd.Click += new EventHandler(this.tsbtnAdd_Click);
			this.tsbtnDelete.Image = Resources.PublicText_Del;
			this.tsbtnDelete.ImageScaling = ToolStripItemImageScaling.None;
			this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnDelete.Margin = new Padding(10, 1, 0, 2);
			this.tsbtnDelete.Name = "tsbtnDelete";
			this.tsbtnDelete.Size = new System.Drawing.Size(36, 46);
			this.tsbtnDelete.Text = "删除";
			this.tsbtnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.tsbtnDelete.TextImageRelation = TextImageRelation.ImageAboveText;
			this.tsbtnDelete.Click += new EventHandler(this.tsbtnDelete_Click);
			this.btnOK.Location = new System.Drawing.Point(501, 385);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 18;
			this.btnOK.Text = "确认";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Location = new System.Drawing.Point(601, 385);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.pnlSetting.Controls.Add(this.lblDataSourceType);
			this.pnlSetting.Controls.Add(this.txtServer);
			this.pnlSetting.Controls.Add(this.txtDatabase);
			this.pnlSetting.Controls.Add(this.txtPort);
			this.pnlSetting.Controls.Add(this.rdoMySQL);
			this.pnlSetting.Controls.Add(this.txtUserName);
			this.pnlSetting.Controls.Add(this.rdoSQLServer);
			this.pnlSetting.Controls.Add(this.txtPassword);
			this.pnlSetting.Controls.Add(this.rdoOracle);
			this.pnlSetting.Controls.Add(this.lblServer);
			this.pnlSetting.Controls.Add(this.lblPort);
			this.pnlSetting.Controls.Add(this.lblPassword);
			this.pnlSetting.Controls.Add(this.lblDatabase);
			this.pnlSetting.Controls.Add(this.lblUserName);
			this.pnlSetting.Location = new System.Drawing.Point(12, 226);
			this.pnlSetting.Name = "pnlSetting";
			this.pnlSetting.Size = new System.Drawing.Size(664, 153);
			this.pnlSetting.TabIndex = 19;
			this.colID.HeaderText = "ID";
			this.colID.Name = "colID";
			this.colID.ReadOnly = true;
			this.colID.Visible = false;
			this.colNo.HeaderText = "序号";
			this.colNo.Name = "colNo";
			this.colNo.ReadOnly = true;
			this.colNo.Width = 60;
			this.colName.HeaderText = "名称";
			this.colName.Name = "colName";
			this.colName.ReadOnly = true;
			this.colName.Width = 140;
			this.colConnectionString.HeaderText = "连接字符串";
			this.colConnectionString.Name = "colConnectionString";
			this.colConnectionString.ReadOnly = true;
			this.colConnectionString.Width = 400;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.NullValue = "重命名";
			this.colEdit.DefaultCellStyle = dataGridViewCellStyle;
			this.colEdit.HeaderText = "编辑";
			this.colEdit.Name = "colEdit";
			this.colEdit.ReadOnly = true;
			this.colEdit.Resizable = DataGridViewTriState.True;
			this.colEdit.Width = 60;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(688, 425);
			base.Controls.Add(this.pnlSetting);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.tsMenu);
			base.Controls.Add(this.dgvList);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formDataSource";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "数据源";
			base.Load += new EventHandler(this.formDataSource_Load);
			((ISupportInitialize)this.dgvList).EndInit();
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.pnlSetting.ResumeLayout(false);
			this.pnlSetting.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
