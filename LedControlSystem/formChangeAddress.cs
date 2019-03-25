using LedModel;
using LedModel.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formChangeAddress : Form
	{
		private LedPanel panel;

		private formMain fm;

		private bool isModified;

		private static string formID = "formChangeAddress";

		private int the_baudrate = 38400;

		private int the_cardaddress = 1;

		private IContainer components;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private Label label1;

		private NumericUpDown numericUpDown_Add_New;

		private TextBox textBox_Add_Old;

		private ComboBox comboBox_Baude_New;

		private Button button_Add_Save;

		private Button button_Add_Send;

		private Label label2;

		private Label label3;

		private Label label4;

		private Button button_Close;

		private TextBox textBox_IP_Old;

		private Label label5;

		private Label label6;

		private TextBox textBox_MAC_Old;

		private Label label7;

		private Label label8;

		private TextBox textBox_MAC_New;

		private TextBox textBox_IP_New;

		public static string FormID
		{
			get
			{
				return formChangeAddress.formID;
			}
			set
			{
				formChangeAddress.formID = value;
			}
		}

		public formChangeAddress(LedPanel pPanel, formMain pMain)
		{
			this.InitializeComponent();
			this.panel = pPanel;
			this.fm = pMain;
			this.Text = formMain.ML.GetStr("formChangeAddress_FormText");
			this.groupBox1.Text = formMain.ML.GetStr("formChangeAddress_groupBox_CardAddressAndBaudRate");
			this.label1.Text = formMain.ML.GetStr("formChangeAddress_label_CardAddress");
			this.label4.Text = formMain.ML.GetStr("formChangeAddress_label_BaudRate");
			this.button_Add_Save.Text = formMain.ML.GetStr("formChangeAddress_button_SaveSetting");
			this.button_Add_Send.Text = formMain.ML.GetStr("formChangeAddress_button_SetTheSend");
			this.button_Close.Text = formMain.ML.GetStr("formChangeAddress_button_Close");
		}

		public formChangeAddress()
		{
			this.InitializeComponent();
		}

		private void formChangeAddress_Load(object sender, EventArgs e)
		{
			this.textBox_Add_Old.Text = this.panel.CardAddress.ToString();
			this.numericUpDown_Add_New.Value = this.panel.CardAddress;
			this.comboBox_Baude_New.Text = this.panel.BaudRate.ToString();
			this.the_baudrate = this.panel.BaudRate;
			this.the_cardaddress = this.panel.CardAddress;
			this.isModified = false;
		}

		private void formChangeAddress_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.isModified && MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Confirm_DiscardChanges"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK)
			{
				e.Cancel = true;
			}
			formMain.ModifyPanelFromIPCServer(this.panel);
		}

		private void button_Close_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void numericUpDown_Add_New_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (!numericUpDown.Focused)
			{
				return;
			}
			this.isModified = true;
		}

		private void comboBox_Baude_New_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.isModified = true;
		}

		private void button_Add_Send_Click(object sender, EventArgs e)
		{
			string text = ((int)this.numericUpDown_Add_New.Value).ToString();
			string text2 = this.comboBox_Baude_New.Text;
			if (this.panel.IsLSeries())
			{
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_BaudRate, text2, this.Text, this.panel, false, this);
			}
			else
			{
				this.panel.CardAddress = int.Parse(text);
				this.panel.BaudRate = int.Parse(text2);
				this.fm.SendSingleCmdStart(LedCmdType.Send_Panel_Parameter, this.panel, this.Text, this.panel, false, this);
			}
			if (formSendSingle.LastSendResult)
			{
				if (!this.panel.IsLSeries())
				{
					MessageBox.Show(this.Text + formMain.ML.GetStr("Display_Successed"));
					formMain.ModifyPanelFromIPCServer(this.panel);
					this.isModified = false;
					this.the_cardaddress = int.Parse(text);
					this.the_baudrate = int.Parse(text2);
					return;
				}
				this.panel.BaudRate = int.Parse(text2);
				formMain.ModifyPanelFromIPCServer(this.panel);
				this.fm.SendSingleCmdStart(LedCmdType.Ctrl_Card_Address, text, this.Text, this.panel, false, this);
				if (formSendSingle.LastSendResult)
				{
					MessageBox.Show(this.Text + formMain.ML.GetStr("Display_Successed"));
					this.isModified = false;
					this.panel.CardAddress = int.Parse(text);
					formMain.ModifyPanelFromIPCServer(this.panel);
					return;
				}
			}
			else if (!this.panel.IsLSeries())
			{
				this.panel.CardAddress = this.the_cardaddress;
				this.panel.BaudRate = this.the_baudrate;
			}
		}

		private void button_Add_Save_Click(object sender, EventArgs e)
		{
			this.panel.CardAddress = (int)this.numericUpDown_Add_New.Value;
			this.panel.BaudRate = int.Parse(this.comboBox_Baude_New.Text);
		}

		private void textBox_IP_New_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBox_MAC_New_TextChanged(object sender, EventArgs e)
		{
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formChangeAddress));
			this.groupBox1 = new GroupBox();
			this.label3 = new Label();
			this.comboBox_Baude_New = new ComboBox();
			this.label2 = new Label();
			this.label4 = new Label();
			this.label1 = new Label();
			this.numericUpDown_Add_New = new NumericUpDown();
			this.textBox_Add_Old = new TextBox();
			this.button_Add_Save = new Button();
			this.button_Add_Send = new Button();
			this.groupBox2 = new GroupBox();
			this.textBox_MAC_New = new TextBox();
			this.textBox_IP_New = new TextBox();
			this.textBox_MAC_Old = new TextBox();
			this.label7 = new Label();
			this.label8 = new Label();
			this.textBox_IP_Old = new TextBox();
			this.label5 = new Label();
			this.label6 = new Label();
			this.button_Close = new Button();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_Add_New).BeginInit();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.numericUpDown_Add_New);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.comboBox_Baude_New);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBox_Add_Old);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(513, 51);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "卡地址及波特率";
			this.label3.Location = new System.Drawing.Point(246, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 20);
			this.label3.TabIndex = 6;
			this.label3.Text = "新波特率";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.comboBox_Baude_New.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_Baude_New.FormattingEnabled = true;
			this.comboBox_Baude_New.Items.AddRange(new object[]
			{
				"9600",
				"38400",
				"115200"
			});
			this.comboBox_Baude_New.Location = new System.Drawing.Point(368, 20);
			this.comboBox_Baude_New.Name = "comboBox_Baude_New";
			this.comboBox_Baude_New.Size = new System.Drawing.Size(121, 20);
			this.comboBox_Baude_New.TabIndex = 0;
			this.comboBox_Baude_New.SelectedIndexChanged += new EventHandler(this.comboBox_Baude_New_SelectedIndexChanged);
			this.label2.Location = new System.Drawing.Point(3, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "新地址";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label4.Location = new System.Drawing.Point(246, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(116, 21);
			this.label4.TabIndex = 5;
			this.label4.Text = "波特率";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label1.Location = new System.Drawing.Point(6, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 21);
			this.label1.TabIndex = 3;
			this.label1.Text = "卡地址";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.numericUpDown_Add_New.Location = new System.Drawing.Point(128, 19);
			NumericUpDown arg_470_0 = this.numericUpDown_Add_New;
			int[] array = new int[4];
			array[0] = 65535;
			arg_470_0.Maximum = new decimal(array);
			NumericUpDown arg_48C_0 = this.numericUpDown_Add_New;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_48C_0.Minimum = new decimal(array2);
			this.numericUpDown_Add_New.Name = "numericUpDown_Add_New";
			this.numericUpDown_Add_New.Size = new System.Drawing.Size(121, 21);
			this.numericUpDown_Add_New.TabIndex = 2;
			NumericUpDown arg_4DB_0 = this.numericUpDown_Add_New;
			int[] array3 = new int[4];
			array3[0] = 1;
			arg_4DB_0.Value = new decimal(array3);
			this.numericUpDown_Add_New.ValueChanged += new EventHandler(this.numericUpDown_Add_New_ValueChanged);
			this.textBox_Add_Old.Enabled = false;
			this.textBox_Add_Old.Location = new System.Drawing.Point(128, 55);
			this.textBox_Add_Old.Name = "textBox_Add_Old";
			this.textBox_Add_Old.Size = new System.Drawing.Size(116, 21);
			this.textBox_Add_Old.TabIndex = 1;
			this.textBox_Add_Old.Visible = false;
			this.button_Add_Save.Location = new System.Drawing.Point(262, 79);
			this.button_Add_Save.Name = "button_Add_Save";
			this.button_Add_Save.Size = new System.Drawing.Size(75, 23);
			this.button_Add_Save.TabIndex = 6;
			this.button_Add_Save.Text = "保存";
			this.button_Add_Save.UseVisualStyleBackColor = true;
			this.button_Add_Save.Visible = false;
			this.button_Add_Save.Click += new EventHandler(this.button_Add_Save_Click);
			this.button_Add_Send.Location = new System.Drawing.Point(358, 79);
			this.button_Add_Send.Name = "button_Add_Send";
			this.button_Add_Send.Size = new System.Drawing.Size(75, 23);
			this.button_Add_Send.TabIndex = 5;
			this.button_Add_Send.Text = "设置发送";
			this.button_Add_Send.UseVisualStyleBackColor = true;
			this.button_Add_Send.Click += new EventHandler(this.button_Add_Send_Click);
			this.groupBox2.Controls.Add(this.textBox_MAC_New);
			this.groupBox2.Controls.Add(this.textBox_IP_New);
			this.groupBox2.Controls.Add(this.textBox_MAC_Old);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.textBox_IP_Old);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Location = new System.Drawing.Point(12, 378);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(513, 88);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "IP地址";
			this.textBox_MAC_New.Location = new System.Drawing.Point(364, 52);
			this.textBox_MAC_New.Name = "textBox_MAC_New";
			this.textBox_MAC_New.Size = new System.Drawing.Size(121, 21);
			this.textBox_MAC_New.TabIndex = 13;
			this.textBox_MAC_New.TextChanged += new EventHandler(this.textBox_MAC_New_TextChanged);
			this.textBox_IP_New.Location = new System.Drawing.Point(364, 19);
			this.textBox_IP_New.Name = "textBox_IP_New";
			this.textBox_IP_New.Size = new System.Drawing.Size(121, 21);
			this.textBox_IP_New.TabIndex = 12;
			this.textBox_IP_New.TextChanged += new EventHandler(this.textBox_IP_New_TextChanged);
			this.textBox_MAC_Old.Enabled = false;
			this.textBox_MAC_Old.Location = new System.Drawing.Point(125, 53);
			this.textBox_MAC_Old.Name = "textBox_MAC_Old";
			this.textBox_MAC_Old.Size = new System.Drawing.Size(116, 21);
			this.textBox_MAC_Old.TabIndex = 11;
			this.label7.Location = new System.Drawing.Point(246, 53);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(116, 20);
			this.label7.TabIndex = 10;
			this.label7.Text = "新MAC地址";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label8.Location = new System.Drawing.Point(7, 53);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(116, 21);
			this.label8.TabIndex = 9;
			this.label8.Text = "原MAC地址";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.textBox_IP_Old.Enabled = false;
			this.textBox_IP_Old.Location = new System.Drawing.Point(125, 20);
			this.textBox_IP_Old.Name = "textBox_IP_Old";
			this.textBox_IP_Old.Size = new System.Drawing.Size(116, 21);
			this.textBox_IP_Old.TabIndex = 7;
			this.label5.Location = new System.Drawing.Point(246, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(116, 20);
			this.label5.TabIndex = 6;
			this.label5.Text = "新IP地址";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label6.Location = new System.Drawing.Point(7, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(116, 21);
			this.label6.TabIndex = 5;
			this.label6.Text = "原IP地址";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button_Close.Location = new System.Drawing.Point(450, 79);
			this.button_Close.Name = "button_Close";
			this.button_Close.Size = new System.Drawing.Size(75, 23);
			this.button_Close.TabIndex = 2;
			this.button_Close.Text = "关闭";
			this.button_Close.UseVisualStyleBackColor = true;
			this.button_Close.Click += new EventHandler(this.button_Close_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(539, 114);
			base.Controls.Add(this.button_Add_Save);
			base.Controls.Add(this.button_Close);
			base.Controls.Add(this.button_Add_Send);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "formChangeAddress";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "编辑地址及波特率";
			base.FormClosing += new FormClosingEventHandler(this.formChangeAddress_FormClosing);
			base.Load += new EventHandler(this.formChangeAddress_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.numericUpDown_Add_New).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
