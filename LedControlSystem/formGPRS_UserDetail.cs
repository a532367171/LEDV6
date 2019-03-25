using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formGPRS_UserDetail : Form
	{
		private static string formID = "formGPRS_UserDetail";

		private DataGridViewRow dr;

		private IContainer components;

		private Label label1;

		private TextBox textBox1;

		private TextBox textBox2;

		private Label label2;

		private TextBox textBox3;

		private Label label3;

		private TextBox textBox4;

		private Label label4;

		private TextBox textBox5;

		private Label label5;

		private TextBox textBox6;

		private Label label6;

		private Button button_OK;

		private Button button_Unbind;

		private Label label7;

		private ComboBox comboBox_GroupList;

		private Button button1;

		public static string FormID
		{
			get
			{
				return formGPRS_UserDetail.formID;
			}
			set
			{
				formGPRS_UserDetail.formID = value;
			}
		}

		public DataGridViewRow Dr
		{
			get
			{
				return this.dr;
			}
			set
			{
				this.dr = value;
			}
		}

		public formGPRS_UserDetail()
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formGPRS_UserDetail.formID;
			this.Text = formMain.ML.GetStr("formGPRS_UserDetail_FormText");
			this.label1.Text = formMain.ML.GetStr("formGPRS_UserDetail_label_DeviceNo");
			this.label2.Text = formMain.ML.GetStr("formGPRS_UserDetail_label_mode");
			this.label3.Text = formMain.ML.GetStr("formGPRS_UserDetail_label_width");
			this.label4.Text = formMain.ML.GetStr("formGPRS_UserDetail_label_height");
			this.label5.Text = formMain.ML.GetStr("formGPRS_UserDetail_label_Traces");
			this.label6.Text = formMain.ML.GetStr("formGPRS_UserDetail_label_DeviceDescription");
			this.label7.Text = formMain.ML.GetStr("formGPRS_UserDetail_label_DeviceGrouping");
			this.button_Unbind.Text = formMain.ML.GetStr("formGPRS_UserDetail_button_Unbind");
			this.button_OK.Text = formMain.ML.GetStr("formGPRS_UserDetail_button_OK");
		}

		public formGPRS_UserDetail(DataGridViewRow pValue)
		{
			this.InitializeComponent();
		}

		private void button_OK_Click(object sender, EventArgs e)
		{
			formGPRS_Send.UpdateGPRSDescInfo(formMain.ledsys.SelectedPanel, this.dr.Cells[9].Value.ToString(), this.textBox6.Text, this.comboBox_GroupList.Text);
			base.Dispose();
		}

		private void button_Unbind_Click(object sender, EventArgs e)
		{
			if (GprsAdministrator.API_UserUnBindNewDevice(this.dr.Cells[9].Value.ToString()))
			{
				MessageBox.Show(this, formMain.ML.GetStr("GPRS_UnbindSuccess"));
				base.Dispose();
				return;
			}
			MessageBox.Show(this, formMain.ML.GetStr("GPRS_UnbindFailed"));
		}

		private void formGPRS_UserDetail_Load(object sender, EventArgs e)
		{
			foreach (string current in formGPRS_Send.GroupList)
			{
				this.comboBox_GroupList.Items.Add(current);
			}
			try
			{
				if (Program.IsforeignTradeMode)
				{
					base.Icon = Resources.AppIconV5;
				}
				else
				{
					base.Icon = Resources.AppIcon;
				}
				if (this.dr != null)
				{
					this.textBox1.Text = this.dr.Cells[10].Value.ToString();
					this.textBox3.Text = this.dr.Cells[7].Value.ToString();
					this.textBox4.Text = this.dr.Cells[8].Value.ToString();
					this.textBox5.Text = this.dr.Cells[13].Value.ToString();
					this.textBox2.Text = this.dr.Cells[6].Value.ToString();
					string text = this.dr.Cells[1].Value.ToString();
					int num = text.IndexOf("_");
					if (num > 0)
					{
						this.textBox6.Text = text.Substring(num + 1);
						this.comboBox_GroupList.Text = text.Substring(0, num);
					}
					else
					{
						this.textBox6.Text = text;
					}
				}
			}
			catch
			{
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			formInputString formInputString = new formInputString();
			string text = formInputString.Get(formMain.ML.GetStr("GPRS_InputGroupName"));
			if (text == null || text.Length == 0)
			{
				return;
			}
			if (formGPRS_Send.AddGroup(text))
			{
				this.comboBox_GroupList.Items.Add(text);
				this.comboBox_GroupList.SelectedIndex = this.comboBox_GroupList.Items.Count - 1;
				return;
			}
			for (int i = 0; i < this.comboBox_GroupList.Items.Count; i++)
			{
				if (this.comboBox_GroupList.Items[i].ToString() == text)
				{
					this.comboBox_GroupList.SelectedIndex = i;
					return;
				}
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
			this.label1 = new Label();
			this.textBox1 = new TextBox();
			this.textBox2 = new TextBox();
			this.label2 = new Label();
			this.textBox3 = new TextBox();
			this.label3 = new Label();
			this.textBox4 = new TextBox();
			this.label4 = new Label();
			this.textBox5 = new TextBox();
			this.label5 = new Label();
			this.textBox6 = new TextBox();
			this.label6 = new Label();
			this.button_OK = new Button();
			this.button_Unbind = new Button();
			this.label7 = new Label();
			this.comboBox_GroupList = new ComboBox();
			this.button1 = new Button();
			base.SuspendLayout();
			this.label1.Location = new System.Drawing.Point(24, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(147, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "设备号";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox1.Location = new System.Drawing.Point(177, 10);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(183, 21);
			this.textBox1.TabIndex = 1;
			this.textBox2.Location = new System.Drawing.Point(177, 37);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(183, 21);
			this.textBox2.TabIndex = 3;
			this.label2.Location = new System.Drawing.Point(24, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(147, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "型号";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox3.Location = new System.Drawing.Point(177, 64);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(183, 21);
			this.textBox3.TabIndex = 5;
			this.label3.Location = new System.Drawing.Point(24, 63);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(147, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "宽度";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox4.Location = new System.Drawing.Point(177, 91);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(183, 21);
			this.textBox4.TabIndex = 7;
			this.label4.Location = new System.Drawing.Point(24, 90);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(147, 23);
			this.label4.TabIndex = 6;
			this.label4.Text = "高度";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox5.Location = new System.Drawing.Point(177, 118);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(183, 21);
			this.textBox5.TabIndex = 9;
			this.label5.Location = new System.Drawing.Point(24, 117);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(147, 23);
			this.label5.TabIndex = 8;
			this.label5.Text = "走线方式";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox6.Location = new System.Drawing.Point(177, 145);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(183, 81);
			this.textBox6.TabIndex = 11;
			this.label6.Location = new System.Drawing.Point(24, 144);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(147, 23);
			this.label6.TabIndex = 10;
			this.label6.Text = "设备描述";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button_OK.Location = new System.Drawing.Point(234, 264);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(116, 23);
			this.button_OK.TabIndex = 12;
			this.button_OK.Text = "确定";
			this.button_OK.UseVisualStyleBackColor = true;
			this.button_OK.Click += new EventHandler(this.button_OK_Click);
			this.button_Unbind.Location = new System.Drawing.Point(101, 264);
			this.button_Unbind.Name = "button_Unbind";
			this.button_Unbind.Size = new System.Drawing.Size(110, 23);
			this.button_Unbind.TabIndex = 13;
			this.button_Unbind.Text = "解绑此设备";
			this.button_Unbind.UseVisualStyleBackColor = true;
			this.button_Unbind.Click += new EventHandler(this.button_Unbind_Click);
			this.label7.Location = new System.Drawing.Point(24, 228);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(147, 23);
			this.label7.TabIndex = 14;
			this.label7.Text = "设备分组";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.comboBox_GroupList.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_GroupList.FormattingEnabled = true;
			this.comboBox_GroupList.Location = new System.Drawing.Point(177, 230);
			this.comboBox_GroupList.Name = "comboBox_GroupList";
			this.comboBox_GroupList.Size = new System.Drawing.Size(121, 20);
			this.comboBox_GroupList.TabIndex = 15;
			this.button1.Location = new System.Drawing.Point(304, 228);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(44, 23);
			this.button1.TabIndex = 16;
			this.button1.Text = "+";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(392, 299);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.comboBox_GroupList);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.button_Unbind);
			base.Controls.Add(this.button_OK);
			base.Controls.Add(this.textBox6);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.textBox5);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.textBox4);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.textBox3);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGPRS_UserDetail";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "设备详细信息";
			base.Load += new EventHandler(this.formGPRS_UserDetail_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
