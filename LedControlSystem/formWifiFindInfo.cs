using LedControlSystem.Properties;
using LedModel;
using LedModel.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formWifiFindInfo : Form
	{
		private LedPanel panel;

		private bool returnflag;

		private static string formID = "formWifiFindInfo";

		private IContainer components;

		private Button button1;

		private Button button2;

		private Label label1;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn LedType;

		private DataGridViewTextBoxColumn MaxArea;

		private DataGridViewTextBoxColumn OEType;

		private DataGridViewTextBoxColumn DataType;

		private DataGridViewTextBoxColumn ScanType;

		private DataGridViewTextBoxColumn IpAddress;

		public static string FormID
		{
			get
			{
				return formWifiFindInfo.formID;
			}
			set
			{
				formWifiFindInfo.formID = value;
			}
		}

		public formWifiFindInfo()
		{
			this.InitializeComponent();
		}

		public formWifiFindInfo(LedPanel lp)
		{
			this.returnflag = false;
			this.panel = lp;
			this.InitializeComponent();
			formMain.ML.NowFormID = formWifiFindInfo.formID;
			this.Text = formMain.ML.GetStr("formWifiFindInfo_FormText");
			this.button1.Text = formMain.ML.GetStr("formWifiFindInfo_button_Loading");
			this.button2.Text = formMain.ML.GetStr("formWifiFindInfo_button_cancel");
			this.label1.Text = formMain.ML.GetStr("formWifiFindInfo_label_Description");
			this.dataGridView1.Columns[0].HeaderText = formMain.ML.GetStr("FD_Model");
			this.dataGridView1.Columns[1].HeaderText = formMain.ML.GetStr("Display_Version");
			this.dataGridView1.Columns[2].HeaderText = formMain.ML.GetStr("Display_Width");
			this.dataGridView1.Columns[3].HeaderText = formMain.ML.GetStr("Display_Height");
			this.dataGridView1.Columns[4].HeaderText = formMain.ML.GetStr("FD_Scan");
			this.dataGridView1.Columns[5].HeaderText = formMain.ML.GetStr("Display_Colours");
		}

		private void formWifiFindInfo_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			dataGridViewRow.CreateCells(this.dataGridView1);
			dataGridViewRow.Cells[0].Value = this.panel.CardType.ToString().Replace("_", "-");
			dataGridViewRow.Cells[1].Value = string.Concat(new string[]
			{
				"V",
				this.panel.MainVersion.ToString(),
				".",
				this.panel.HardwareVersion.ToString("d2"),
				".",
				this.panel.ProgramVersion.ToString("d2")
			});
			dataGridViewRow.Cells[2].Value = this.panel.Width.ToString();
			dataGridViewRow.Cells[3].Value = this.panel.Height.ToString();
			LedScanType scanType = (LedScanType)this.panel.RoutingSetting.ScanType;
			if (scanType != LedScanType.Scan4)
			{
				if (scanType != LedScanType.Scan8)
				{
					if (scanType == LedScanType.Scan16)
					{
						dataGridViewRow.Cells[4].Value = "1/16";
					}
				}
				else
				{
					dataGridViewRow.Cells[4].Value = "1/8";
				}
			}
			else
			{
				dataGridViewRow.Cells[4].Value = "1/4";
			}
			switch (this.panel.ColorMode)
			{
			case LedColorMode.R:
				dataGridViewRow.Cells[5].Value = formMain.ML.GetStr("formWifiFindInfo_Grid_color_single");
				break;
			case LedColorMode.RG:
				dataGridViewRow.Cells[5].Value = formMain.ML.GetStr("formWifiFindInfo_Grid_color_double");
				break;
			case LedColorMode.GR:
				dataGridViewRow.Cells[5].Value = formMain.ML.GetStr("formWifiFindInfo_Grid_color_double");
				break;
			case LedColorMode.RGB:
				dataGridViewRow.Cells[5].Value = formMain.ML.GetStr("formWifiFindInfo_Grid_color_three");
				break;
			case LedColorMode.RBG:
				dataGridViewRow.Cells[5].Value = formMain.ML.GetStr("formWifiFindInfo_Grid_color_three");
				break;
			case LedColorMode.GRB:
				dataGridViewRow.Cells[5].Value = formMain.ML.GetStr("formWifiFindInfo_Grid_color_three");
				break;
			case LedColorMode.GBR:
				dataGridViewRow.Cells[5].Value = formMain.ML.GetStr("formWifiFindInfo_Grid_color_three");
				break;
			case LedColorMode.BRG:
				dataGridViewRow.Cells[5].Value = formMain.ML.GetStr("formWifiFindInfo_Grid_color_three");
				break;
			case LedColorMode.BGR:
				dataGridViewRow.Cells[5].Value = formMain.ML.GetStr("formWifiFindInfo_Grid_color_three");
				break;
			}
			this.dataGridView1.Rows.Add(dataGridViewRow);
		}

		public bool wifiFindDisplay()
		{
			base.ShowDialog();
			return this.returnflag;
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.returnflag = true;
			base.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.returnflag = false;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formWifiFindInfo));
			this.button1 = new Button();
			this.button2 = new Button();
			this.label1 = new Label();
			this.dataGridView1 = new DataGridView();
			this.LedType = new DataGridViewTextBoxColumn();
			this.MaxArea = new DataGridViewTextBoxColumn();
			this.OEType = new DataGridViewTextBoxColumn();
			this.DataType = new DataGridViewTextBoxColumn();
			this.ScanType = new DataGridViewTextBoxColumn();
			this.IpAddress = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.button1.Location = new System.Drawing.Point(70, 93);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 34);
			this.button1.TabIndex = 1;
			this.button1.Text = "载入参数";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new System.Drawing.Point(276, 93);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(93, 34);
			this.button2.TabIndex = 1;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label1.Location = new System.Drawing.Point(0, 130);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(483, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "单击“载入参数”可将参数更新至电脑工程数据。";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.LedType,
				this.MaxArea,
				this.OEType,
				this.DataType,
				this.ScanType,
				this.IpAddress
			});
			this.dataGridView1.Dock = DockStyle.Top;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(483, 87);
			this.dataGridView1.TabIndex = 15;
			this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			this.LedType.HeaderText = "型号";
			this.LedType.Name = "LedType";
			this.LedType.Width = 60;
			this.MaxArea.HeaderText = "版本";
			this.MaxArea.Name = "MaxArea";
			this.MaxArea.Width = 80;
			this.OEType.HeaderText = "宽度";
			this.OEType.Name = "OEType";
			this.OEType.Width = 80;
			this.DataType.HeaderText = "高度";
			this.DataType.Name = "DataType";
			this.DataType.Width = 80;
			this.ScanType.HeaderText = "扫描";
			this.ScanType.Name = "ScanType";
			this.ScanType.Width = 80;
			this.IpAddress.HeaderText = "颜色";
			this.IpAddress.Name = "IpAddress";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(483, 148);
			base.Controls.Add(this.dataGridView1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(499, 187);
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(499, 187);
			base.Name = "formWifiFindInfo";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "已检测到WIFI控制系统";
			base.Load += new EventHandler(this.formWifiFindInfo_Load);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
