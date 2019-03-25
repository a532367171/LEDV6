using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Content;
using LedModel.Enum;
using LedModel.Foundation;
using LedResources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formStringTest : Form
	{
		public formMain fm;

		private LedPanel panel;

		private LedString stringContent;

		private LedStringUpdate stringUpdate;

		private IContainer components;

		private GroupBox gbUpdate;

		private Label lblContentHint;

		private Label lblIndexHint;

		private Button btnSend;

		private TextBox txtContent;

		private ComboBox cmbColor;

		private TextBox txtIndex;

		private ComboBox cmbDisplayWay;

		private ComboBox cmbEncoding;

		private Label lblContent;

		private Label lblColor;

		private Label lblIndex;

		private Label lblDisplayWay;

		private Label lblEncoding;

		private GroupBox gbDelete;

		private TextBox txtDeleteIndex;

		private ComboBox cmbDeleteType;

		private Label lblDeleteIndex;

		private Label lblDeleteType;

		private Button btnDelete;

		private Label lblDeleteIndexHint;

		public formStringTest(LedString ps, formMain pfm)
		{
			this.InitializeComponent();
			this.stringContent = ps;
			this.fm = pfm;
			this.panel = formMain.Ledsys.SelectedPanel;
			this.stringUpdate = new LedStringUpdate();
			this.Text = formMain.ML.GetStr("formStringTest_Form_StringTest");
			this.gbUpdate.Text = formMain.ML.GetStr("formStringTest_GroupBox_Update");
			this.gbDelete.Text = formMain.ML.GetStr("formStringTest_GroupBox_Delete");
			this.lblEncoding.Text = formMain.ML.GetStr("formStringTest_Label_Encoding");
			this.lblIndex.Text = formMain.ML.GetStr("formStringTest_Label_Index");
			this.lblDisplayWay.Text = formMain.ML.GetStr("formStringTest_Label_DisplayWay");
			this.lblColor.Text = formMain.ML.GetStr("formStringTest_Label_Color");
			this.lblContent.Text = formMain.ML.GetStr("formStringTest_Label_Content");
			this.lblDeleteType.Text = formMain.ML.GetStr("formStringTest_Label_DeleteType");
			this.lblDeleteIndex.Text = formMain.ML.GetStr("formStringTest_Label_DeleteIndex");
			this.lblContentHint.Text = formMain.ML.GetStr("formStringTest_Label_ContentHint");
			this.btnSend.Text = formMain.ML.GetStr("formStringTest_Button_Send");
			this.btnDelete.Text = formMain.ML.GetStr("formStringTest_Button_Delete");
		}

		private void formStringTest_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.cmbEncoding.Enabled = false;
			this.cmbEncoding.Items.Clear();
			foreach (int num in Enum.GetValues(typeof(LedStringEncoding)))
			{
				string name = Enum.GetName(typeof(LedStringEncoding), num);
				this.cmbEncoding.Items.Add(name);
			}
			this.cmbDisplayWay.Items.Clear();
			List<string> list = MulitLanguageFresher.Language_Control_List["formStringTest_ComboBox_DisplayWay"];
			DataTable dataTable = new DataTable();
			DataColumn column = new DataColumn("ID");
			dataTable.Columns.Add(column);
			column = new DataColumn("Name");
			dataTable.Columns.Add(column);
			foreach (int num2 in Enum.GetValues(typeof(LedStringDisplayWay)))
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = num2;
				if (num2 == 0)
				{
					dataRow[1] = list[0];
				}
				else
				{
					dataRow[1] = list[1];
				}
				dataTable.Rows.Add(dataRow);
			}
			this.cmbDisplayWay.DisplayMember = "Name";
			this.cmbDisplayWay.ValueMember = "ID";
			this.cmbDisplayWay.DataSource = dataTable;
			this.cmbColor.Items.Clear();
			if (this.panel != null)
			{
				IList<System.Drawing.Color> colorList = LedColorConst.GetColorList(this.panel.ColorMode);
				foreach (System.Drawing.Color current in colorList)
				{
					this.cmbColor.Items.Add(current);
				}
			}
			this.cmbDeleteType.Items.Clear();
			List<string> list2 = MulitLanguageFresher.Language_Control_List["formStringTest_ComboBox_DeleteType"];
			dataTable = new DataTable();
			column = new DataColumn("ID");
			dataTable.Columns.Add(column);
			column = new DataColumn("Name");
			dataTable.Columns.Add(column);
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2[0] = 0;
			dataRow2[1] = list2[0];
			dataTable.Rows.Add(dataRow2);
			dataRow2 = dataTable.NewRow();
			dataRow2[0] = 2;
			dataRow2[1] = list2[1];
			dataTable.Rows.Add(dataRow2);
			this.cmbDeleteType.DisplayMember = "Name";
			this.cmbDeleteType.ValueMember = "ID";
			this.cmbDeleteType.DataSource = dataTable;
			if (this.stringContent != null && this.stringUpdate != null)
			{
				this.stringUpdate.ID = this.stringContent.ID;
			}
			if (this.cmbEncoding.Items.Count > 0)
			{
				if (this.stringContent != null)
				{
					this.cmbEncoding.Text = this.stringContent.StringEncoding.ToString();
					if (this.stringUpdate != null)
					{
						this.stringUpdate.StringEncoding = this.stringContent.StringEncoding;
					}
				}
				else
				{
					this.cmbEncoding.SelectedIndex = 1;
				}
			}
			if (this.cmbDisplayWay.Items.Count > 0)
			{
				this.cmbDisplayWay.SelectedIndex = 0;
			}
			if (this.stringContent != null)
			{
				this.txtIndex.Text = "0";
				this.lblIndexHint.Text = "（0-" + (this.stringContent.NumberCount - 1) + "）";
			}
			if (this.cmbColor.Items.Count > 0)
			{
				this.cmbColor.SelectedIndex = 0;
			}
			this.txtContent.Text = string.Empty;
			if (this.stringContent != null)
			{
				this.txtContent.MaxLength = this.stringContent.EachNumber;
				this.lblContentHint.Text = string.Format("{0}/{1}", this.txtContent.TextLength, this.stringContent.EachNumber);
			}
			if (this.cmbDeleteType.Items.Count > 0)
			{
				this.cmbDeleteType.SelectedIndex = 0;
			}
			if (this.stringContent != null)
			{
				this.txtDeleteIndex.Text = "0";
				this.lblDeleteIndexHint.Text = string.Format(formMain.ML.GetStr("formStringTest_Label_DeleteIndexHint"), this.stringContent.NumberCount - 1);
			}
		}

		private void cmbEncoding_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.stringUpdate != null)
			{
				this.stringUpdate.StringEncoding = (LedStringEncoding)comboBox.SelectedIndex;
			}
		}

		private void cmbDisplayWay_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.stringUpdate != null)
			{
				this.stringUpdate.StringDisplayWay = (LedStringDisplayWay)int.Parse(comboBox.SelectedValue.ToString());
			}
		}

		private void txtIndex_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.stringContent != null)
			{
				int num;
				bool flag = int.TryParse(textBox.Text, out num);
				if (flag && this.stringUpdate != null)
				{
					this.stringUpdate.Index = byte.Parse(textBox.Text);
				}
			}
		}

		private void txtIndex_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void cmbColor_DrawItem(object sender, DrawItemEventArgs e)
		{
			System.Drawing.Graphics arg_06_0 = e.Graphics;
			System.Drawing.Rectangle bounds = e.Bounds;
			ComboBox comboBox = (ComboBox)sender;
			if (e.Index >= 0 && e.Index < comboBox.Items.Count)
			{
				System.Drawing.Color color = (System.Drawing.Color)comboBox.Items[e.Index];
				using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(color))
				{
					e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
					e.Graphics.FillRectangle(brush, new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height - 2));
					e.DrawFocusRectangle();
				}
			}
		}

		private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.stringUpdate != null)
			{
				this.stringUpdate.ForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
			}
		}

		private void txtContent_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.stringContent != null)
			{
				if (textBox.Text.Length > this.stringContent.EachNumber)
				{
					textBox.Text = textBox.Text.Substring(0, this.stringContent.EachNumber);
				}
				if (this.stringUpdate != null)
				{
					this.stringUpdate.Length = textBox.TextLength;
					this.stringUpdate.Text = textBox.Text;
				}
				this.lblContentHint.Text = string.Format("{0}/{1}", textBox.TextLength, this.stringContent.EachNumber);
			}
		}

		private void txtDeleteIndex_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			bool arg_0D_0 = textBox.Focused;
		}

		private void txtDeleteIndex_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			if (this.txtIndex.Text.Length == 0)
			{
				MessageBox.Show(formMain.ML.GetStr("Message_Please_Input_Correct_Index"));
				this.txtIndex.Focus();
				return;
			}
			if (this.txtContent.Text.Length == 0)
			{
				MessageBox.Show(formMain.ML.GetStr("Message_Please_Input_Content"));
				this.txtContent.Focus();
				return;
			}
			this.fm.SendSingleCmdStart(LedCmdType.Ctrl_String_Update, this.stringUpdate, formMain.ML.GetStr("formStringTest_Operation_Update"), formMain.ledsys.SelectedPanel, true, this);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.txtDeleteIndex.Text.Length == 0)
			{
				MessageBox.Show(formMain.ML.GetStr("Message_Please_Input_Correct_Index"));
				this.txtDeleteIndex.Focus();
				return;
			}
			this.fm.SendSingleCmdStart(LedCmdType.Ctrl_String_Delete, string.Format("{0}|{1}|{2}", this.stringContent.ID, this.cmbDeleteType.SelectedValue, this.txtDeleteIndex.Text), formMain.ML.GetStr("formStringTest_Operation_Delete"), formMain.ledsys.SelectedPanel, true, this);
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
			this.gbUpdate = new GroupBox();
			this.lblContentHint = new Label();
			this.lblIndexHint = new Label();
			this.btnSend = new Button();
			this.txtContent = new TextBox();
			this.cmbColor = new ComboBox();
			this.txtIndex = new TextBox();
			this.cmbDisplayWay = new ComboBox();
			this.cmbEncoding = new ComboBox();
			this.lblContent = new Label();
			this.lblColor = new Label();
			this.lblIndex = new Label();
			this.lblDisplayWay = new Label();
			this.lblEncoding = new Label();
			this.gbDelete = new GroupBox();
			this.lblDeleteIndexHint = new Label();
			this.txtDeleteIndex = new TextBox();
			this.cmbDeleteType = new ComboBox();
			this.lblDeleteIndex = new Label();
			this.lblDeleteType = new Label();
			this.btnDelete = new Button();
			this.gbUpdate.SuspendLayout();
			this.gbDelete.SuspendLayout();
			base.SuspendLayout();
			this.gbUpdate.Controls.Add(this.lblContentHint);
			this.gbUpdate.Controls.Add(this.lblIndexHint);
			this.gbUpdate.Controls.Add(this.btnSend);
			this.gbUpdate.Controls.Add(this.txtContent);
			this.gbUpdate.Controls.Add(this.cmbColor);
			this.gbUpdate.Controls.Add(this.txtIndex);
			this.gbUpdate.Controls.Add(this.cmbDisplayWay);
			this.gbUpdate.Controls.Add(this.cmbEncoding);
			this.gbUpdate.Controls.Add(this.lblContent);
			this.gbUpdate.Controls.Add(this.lblColor);
			this.gbUpdate.Controls.Add(this.lblIndex);
			this.gbUpdate.Controls.Add(this.lblDisplayWay);
			this.gbUpdate.Controls.Add(this.lblEncoding);
			this.gbUpdate.Location = new System.Drawing.Point(12, 12);
			this.gbUpdate.Name = "gbUpdate";
			this.gbUpdate.Size = new System.Drawing.Size(485, 322);
			this.gbUpdate.TabIndex = 13;
			this.gbUpdate.TabStop = false;
			this.gbUpdate.Text = "更新";
			this.lblContentHint.Location = new System.Drawing.Point(117, 248);
			this.lblContentHint.Name = "lblContentHint";
			this.lblContentHint.Size = new System.Drawing.Size(350, 18);
			this.lblContentHint.TabIndex = 25;
			this.lblContentHint.Text = "最多50字";
			this.lblContentHint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblIndexHint.AutoSize = true;
			this.lblIndexHint.Location = new System.Drawing.Point(423, 30);
			this.lblIndexHint.Name = "lblIndexHint";
			this.lblIndexHint.Size = new System.Drawing.Size(41, 12);
			this.lblIndexHint.TabIndex = 24;
			this.lblIndexHint.Text = "(范围)";
			this.btnSend.Location = new System.Drawing.Point(392, 283);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 23);
			this.btnSend.TabIndex = 23;
			this.btnSend.Text = "发送";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new EventHandler(this.btnSend_Click);
			this.txtContent.Location = new System.Drawing.Point(117, 100);
			this.txtContent.Multiline = true;
			this.txtContent.Name = "txtContent";
			this.txtContent.ScrollBars = ScrollBars.Vertical;
			this.txtContent.Size = new System.Drawing.Size(350, 145);
			this.txtContent.TabIndex = 22;
			this.txtContent.TextChanged += new EventHandler(this.txtContent_TextChanged);
			this.cmbColor.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbColor.FormattingEnabled = true;
			this.cmbColor.Location = new System.Drawing.Point(374, 64);
			this.cmbColor.Name = "cmbColor";
			this.cmbColor.Size = new System.Drawing.Size(95, 22);
			this.cmbColor.TabIndex = 21;
			this.cmbColor.DrawItem += new DrawItemEventHandler(this.cmbColor_DrawItem);
			this.cmbColor.SelectedIndexChanged += new EventHandler(this.cmbColor_SelectedIndexChanged);
			this.txtIndex.Location = new System.Drawing.Point(374, 26);
			this.txtIndex.Name = "txtIndex";
			this.txtIndex.Size = new System.Drawing.Size(46, 21);
			this.txtIndex.TabIndex = 20;
			this.txtIndex.TextChanged += new EventHandler(this.txtIndex_TextChanged);
			this.txtIndex.KeyPress += new KeyPressEventHandler(this.txtIndex_KeyPress);
			this.cmbDisplayWay.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDisplayWay.FormattingEnabled = true;
			this.cmbDisplayWay.Location = new System.Drawing.Point(117, 63);
			this.cmbDisplayWay.Name = "cmbDisplayWay";
			this.cmbDisplayWay.Size = new System.Drawing.Size(95, 20);
			this.cmbDisplayWay.TabIndex = 19;
			this.cmbDisplayWay.SelectedIndexChanged += new EventHandler(this.cmbDisplayWay_SelectedIndexChanged);
			this.cmbEncoding.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbEncoding.FormattingEnabled = true;
			this.cmbEncoding.Location = new System.Drawing.Point(117, 26);
			this.cmbEncoding.Name = "cmbEncoding";
			this.cmbEncoding.Size = new System.Drawing.Size(95, 20);
			this.cmbEncoding.TabIndex = 18;
			this.cmbEncoding.SelectedIndexChanged += new EventHandler(this.cmbEncoding_SelectedIndexChanged);
			this.lblContent.AutoSize = true;
			this.lblContent.Location = new System.Drawing.Point(44, 100);
			this.lblContent.Name = "lblContent";
			this.lblContent.Size = new System.Drawing.Size(29, 12);
			this.lblContent.TabIndex = 17;
			this.lblContent.Text = "内容";
			this.lblColor.AutoSize = true;
			this.lblColor.Location = new System.Drawing.Point(301, 67);
			this.lblColor.Name = "lblColor";
			this.lblColor.Size = new System.Drawing.Size(29, 12);
			this.lblColor.TabIndex = 16;
			this.lblColor.Text = "颜色";
			this.lblIndex.AutoSize = true;
			this.lblIndex.Location = new System.Drawing.Point(301, 26);
			this.lblIndex.Name = "lblIndex";
			this.lblIndex.Size = new System.Drawing.Size(29, 12);
			this.lblIndex.TabIndex = 15;
			this.lblIndex.Text = "索引";
			this.lblDisplayWay.AutoSize = true;
			this.lblDisplayWay.Location = new System.Drawing.Point(20, 63);
			this.lblDisplayWay.Name = "lblDisplayWay";
			this.lblDisplayWay.Size = new System.Drawing.Size(53, 12);
			this.lblDisplayWay.TabIndex = 14;
			this.lblDisplayWay.Text = "显示方式";
			this.lblEncoding.AutoSize = true;
			this.lblEncoding.Location = new System.Drawing.Point(44, 26);
			this.lblEncoding.Name = "lblEncoding";
			this.lblEncoding.Size = new System.Drawing.Size(29, 12);
			this.lblEncoding.TabIndex = 13;
			this.lblEncoding.Text = "编码";
			this.gbDelete.Controls.Add(this.lblDeleteIndexHint);
			this.gbDelete.Controls.Add(this.txtDeleteIndex);
			this.gbDelete.Controls.Add(this.cmbDeleteType);
			this.gbDelete.Controls.Add(this.lblDeleteIndex);
			this.gbDelete.Controls.Add(this.lblDeleteType);
			this.gbDelete.Controls.Add(this.btnDelete);
			this.gbDelete.Location = new System.Drawing.Point(12, 340);
			this.gbDelete.Name = "gbDelete";
			this.gbDelete.Size = new System.Drawing.Size(485, 117);
			this.gbDelete.TabIndex = 14;
			this.gbDelete.TabStop = false;
			this.gbDelete.Text = "删除";
			this.lblDeleteIndexHint.AutoSize = true;
			this.lblDeleteIndexHint.Location = new System.Drawing.Point(164, 78);
			this.lblDeleteIndexHint.Name = "lblDeleteIndexHint";
			this.lblDeleteIndexHint.Size = new System.Drawing.Size(41, 12);
			this.lblDeleteIndexHint.TabIndex = 5;
			this.lblDeleteIndexHint.Text = "(范围)";
			this.txtDeleteIndex.Location = new System.Drawing.Point(115, 74);
			this.txtDeleteIndex.Name = "txtDeleteIndex";
			this.txtDeleteIndex.Size = new System.Drawing.Size(46, 21);
			this.txtDeleteIndex.TabIndex = 4;
			this.txtDeleteIndex.TextChanged += new EventHandler(this.txtDeleteIndex_TextChanged);
			this.txtDeleteIndex.KeyPress += new KeyPressEventHandler(this.txtDeleteIndex_KeyPress);
			this.cmbDeleteType.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDeleteType.FormattingEnabled = true;
			this.cmbDeleteType.Location = new System.Drawing.Point(117, 33);
			this.cmbDeleteType.Name = "cmbDeleteType";
			this.cmbDeleteType.Size = new System.Drawing.Size(95, 20);
			this.cmbDeleteType.TabIndex = 3;
			this.lblDeleteIndex.AutoSize = true;
			this.lblDeleteIndex.Location = new System.Drawing.Point(44, 78);
			this.lblDeleteIndex.Name = "lblDeleteIndex";
			this.lblDeleteIndex.Size = new System.Drawing.Size(29, 12);
			this.lblDeleteIndex.TabIndex = 2;
			this.lblDeleteIndex.Text = "索引";
			this.lblDeleteType.AutoSize = true;
			this.lblDeleteType.Location = new System.Drawing.Point(44, 37);
			this.lblDeleteType.Name = "lblDeleteType";
			this.lblDeleteType.Size = new System.Drawing.Size(29, 12);
			this.lblDeleteType.TabIndex = 1;
			this.lblDeleteType.Text = "类型";
			this.btnDelete.Location = new System.Drawing.Point(392, 72);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 0;
			this.btnDelete.Text = "发送";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(510, 469);
			base.Controls.Add(this.gbDelete);
			base.Controls.Add(this.gbUpdate);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formStringTest";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "字符分区测试";
			base.Load += new EventHandler(this.formStringTest_Load);
			this.gbUpdate.ResumeLayout(false);
			this.gbUpdate.PerformLayout();
			this.gbDelete.ResumeLayout(false);
			this.gbDelete.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
