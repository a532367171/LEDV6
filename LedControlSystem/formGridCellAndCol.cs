using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formGridCellAndCol : Form
	{
		public static int col;

		public static int row;

		public static bool autoRowNumber;

		private static string formID = "formGridCellAndCol";

		private IContainer components;

		private NumericUpDown numericUpDown_Col;

		private NumericUpDown numericUpDown_Rol;

		private Label label_grid_Col;

		private Label label_grid_Row;

		private Button button_OK;

		private Button button_Cancel;

		private CheckBox checkBox1;

		public static string FormID
		{
			get
			{
				return formGridCellAndCol.formID;
			}
			set
			{
				formGridCellAndCol.formID = value;
			}
		}

		public formGridCellAndCol()
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formGridCellAndCol_FormText");
			this.label_grid_Col.Text = formMain.ML.GetStr("formGridCellAndCol_label_grid_Cols");
			this.label_grid_Row.Text = formMain.ML.GetStr("formGridCellAndCol_label_grid_Rows");
			this.checkBox1.Text = formMain.ML.GetStr("formGridCellAndCol_checkBox_AddRowNumber");
			this.button_OK.Text = formMain.ML.GetStr("formGridCellAndCol_button_OK");
			this.button_Cancel.Text = formMain.ML.GetStr("formGridCellAndCol_button_Cancel");
		}

		public void Get()
		{
			base.ShowDialog();
		}

		private void button_Cancel_Click(object sender, EventArgs e)
		{
			formGridCellAndCol.col = 0;
			formGridCellAndCol.row = 0;
			formGridCellAndCol.autoRowNumber = this.checkBox1.Checked;
			base.Dispose();
		}

		private void button_OK_Click(object sender, EventArgs e)
		{
			formGridCellAndCol.col = (int)this.numericUpDown_Col.Value;
			formGridCellAndCol.row = (int)this.numericUpDown_Rol.Value;
			formGridCellAndCol.autoRowNumber = this.checkBox1.Checked;
			base.Dispose();
		}

		private void formGridCellAndCol_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
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
			this.numericUpDown_Col = new NumericUpDown();
			this.numericUpDown_Rol = new NumericUpDown();
			this.label_grid_Col = new Label();
			this.label_grid_Row = new Label();
			this.button_OK = new Button();
			this.button_Cancel = new Button();
			this.checkBox1 = new CheckBox();
			((ISupportInitialize)this.numericUpDown_Col).BeginInit();
			((ISupportInitialize)this.numericUpDown_Rol).BeginInit();
			base.SuspendLayout();
			this.numericUpDown_Col.Location = new System.Drawing.Point(177, 12);
			this.numericUpDown_Col.Name = "numericUpDown_Col";
			this.numericUpDown_Col.Size = new System.Drawing.Size(97, 21);
			this.numericUpDown_Col.TabIndex = 0;
			NumericUpDown arg_C7_0 = this.numericUpDown_Col;
			int[] array = new int[4];
			array[0] = 5;
			arg_C7_0.Value = new decimal(array);
			this.numericUpDown_Rol.Location = new System.Drawing.Point(177, 39);
			this.numericUpDown_Rol.Name = "numericUpDown_Rol";
			this.numericUpDown_Rol.Size = new System.Drawing.Size(97, 21);
			this.numericUpDown_Rol.TabIndex = 1;
			NumericUpDown arg_12B_0 = this.numericUpDown_Rol;
			int[] array2 = new int[4];
			array2[0] = 10;
			arg_12B_0.Value = new decimal(array2);
			this.label_grid_Col.Location = new System.Drawing.Point(12, 14);
			this.label_grid_Col.Name = "label_grid_Col";
			this.label_grid_Col.Size = new System.Drawing.Size(159, 19);
			this.label_grid_Col.TabIndex = 2;
			this.label_grid_Col.Text = "列数";
			this.label_grid_Col.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label_grid_Row.Location = new System.Drawing.Point(12, 39);
			this.label_grid_Row.Name = "label_grid_Row";
			this.label_grid_Row.Size = new System.Drawing.Size(159, 19);
			this.label_grid_Row.TabIndex = 3;
			this.label_grid_Row.Text = "行数";
			this.label_grid_Row.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.button_OK.Location = new System.Drawing.Point(82, 86);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(75, 23);
			this.button_OK.TabIndex = 4;
			this.button_OK.Text = "确定";
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new EventHandler(this.button_OK_Click);
			this.button_Cancel.Location = new System.Drawing.Point(177, 86);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.button_Cancel.TabIndex = 5;
			this.button_Cancel.Text = "取消";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new EventHandler(this.button_Cancel_Click);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(177, 64);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 16);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "添加行号";
			this.checkBox1.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(307, 113);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.button_OK);
			base.Controls.Add(this.label_grid_Row);
			base.Controls.Add(this.label_grid_Col);
			base.Controls.Add(this.numericUpDown_Rol);
			base.Controls.Add(this.numericUpDown_Col);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGridCellAndCol";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "新建表格";
			base.Load += new EventHandler(this.formGridCellAndCol_Load);
			((ISupportInitialize)this.numericUpDown_Col).EndInit();
			((ISupportInitialize)this.numericUpDown_Rol).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
