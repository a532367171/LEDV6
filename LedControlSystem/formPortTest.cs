using LedControlSystem.Properties;
using LedModel.Foundation;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formPortTest : Form
	{
		private formMain main;

		private LedRoutingSetting routingSetting;

		private byte PanelOEPolarity;

		private byte PanelDataPolarity;

		private IContainer components;

		private GroupBox groupBox4;

		private Button COMButton;

		private Button EthernetButton;

		private Label label7;

		private ComboBox PortComboBox;

		private GroupBox groupBox3;

		private ComboBox DATA16;

		private ComboBox DATA8;

		private ComboBox OE16;

		private ComboBox DATA4;

		private ComboBox OE8;

		private ComboBox OE4;

		private Label label4;

		private Label label3;

		private Label label6;

		private Label label5;

		private Label label2;

		private GroupBox groupBox2;

		private Button button4;

		private Button button8;

		private Button button16;

		public formPortTest()
		{
			this.InitializeComponent();
		}

		public void Edig(formMain fm)
		{
			this.main = fm;
			base.TopMost = true;
			base.ShowDialog();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			base.Visible = false;
			LedRoutingSetting ledRoutingSetting = new LedRoutingSetting();
			ledRoutingSetting.SettingFileName = formMain.Ledsys.SelectedPanel.RoutingSetting.SettingFileName;
			ledRoutingSetting.ScanTypeIndex = 4;
			ledRoutingSetting.RoutingIndex = 0;
			ledRoutingSetting.LoadScanFromFile();
			formMain.Ledsys.SelectedPanel.RoutingSetting = ledRoutingSetting;
			formMain.ledsys.SelectedPanel.OEPolarity = (byte)this.OE4.SelectedIndex;
			formMain.ledsys.SelectedPanel.DataPolarity = (byte)this.DATA4.SelectedIndex;
			this.LoadParam();
			this.main.PortTestSendAndTiming();
			base.Visible = true;
		}

		private void LoadParam()
		{
			if (formMain.ledsys.SelectedPanel.RoutingSetting.ScanTypeIndex == 4)
			{
				this.OE4.SelectedIndex = (int)formMain.ledsys.SelectedPanel.OEPolarity;
				this.DATA4.SelectedIndex = (int)formMain.ledsys.SelectedPanel.DataPolarity;
				return;
			}
			this.OE16.SelectedIndex = (int)formMain.ledsys.SelectedPanel.OEPolarity;
			this.DATA16.SelectedIndex = (int)formMain.ledsys.SelectedPanel.DataPolarity;
		}

		private void button16_Click(object sender, EventArgs e)
		{
			base.Visible = false;
			LedRoutingSetting ledRoutingSetting = new LedRoutingSetting();
			ledRoutingSetting.SettingFileName = formMain.Ledsys.SelectedPanel.RoutingSetting.SettingFileName;
			ledRoutingSetting.ScanTypeIndex = 16;
			ledRoutingSetting.RoutingIndex = 0;
			ledRoutingSetting.LoadScanFromFile();
			formMain.Ledsys.SelectedPanel.RoutingSetting = ledRoutingSetting;
			formMain.ledsys.SelectedPanel.OEPolarity = (byte)this.OE16.SelectedIndex;
			formMain.ledsys.SelectedPanel.DataPolarity = (byte)this.DATA16.SelectedIndex;
			this.LoadParam();
			this.main.PortTestSendAndTiming();
			base.Visible = true;
		}

		private void formPortTest_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Alt && e.KeyCode == Keys.X)
			{
				this.button16_Click(null, null);
				return;
			}
			if (e.Alt && e.KeyCode == Keys.C)
			{
				this.button4_Click(null, null);
				return;
			}
			if (e.Alt && e.KeyCode == Keys.Z)
			{
				this.button4_Click(null, null);
			}
		}

		private void formPortTest_Load(object sender, EventArgs e)
		{
			this.PanelOEPolarity = formMain.Ledsys.SelectedPanel.OEPolarity;
			this.PanelDataPolarity = formMain.Ledsys.SelectedPanel.DataPolarity;
			this.routingSetting = formMain.Ledsys.SelectedPanel.RoutingSetting;
			this.OE4.SelectedIndex = 1;
			this.DATA4.SelectedIndex = 1;
			this.OE16.SelectedIndex = 0;
			this.DATA16.SelectedIndex = 1;
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
		}

		private void button8_Click(object sender, EventArgs e)
		{
		}

		private void formPortTest_Close(object sender, FormClosedEventArgs e)
		{
			formMain.Ledsys.SelectedPanel.RoutingSetting = this.routingSetting;
			formMain.Ledsys.SelectedPanel.OEPolarity = this.PanelOEPolarity;
			formMain.Ledsys.SelectedPanel.DataPolarity = this.PanelDataPolarity;
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
			this.groupBox4 = new GroupBox();
			this.COMButton = new Button();
			this.EthernetButton = new Button();
			this.label7 = new Label();
			this.PortComboBox = new ComboBox();
			this.groupBox3 = new GroupBox();
			this.DATA16 = new ComboBox();
			this.DATA8 = new ComboBox();
			this.OE16 = new ComboBox();
			this.DATA4 = new ComboBox();
			this.OE8 = new ComboBox();
			this.OE4 = new ComboBox();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.label2 = new Label();
			this.groupBox2 = new GroupBox();
			this.button4 = new Button();
			this.button8 = new Button();
			this.button16 = new Button();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.groupBox4.Controls.Add(this.COMButton);
			this.groupBox4.Controls.Add(this.EthernetButton);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.PortComboBox);
			this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.groupBox4.ForeColor = System.Drawing.Color.White;
			this.groupBox4.Location = new System.Drawing.Point(47, 265);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(459, 120);
			this.groupBox4.TabIndex = 12;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "通信设置";
			this.groupBox4.Visible = false;
			this.COMButton.ForeColor = System.Drawing.Color.Black;
			this.COMButton.Location = new System.Drawing.Point(256, 25);
			this.COMButton.Name = "COMButton";
			this.COMButton.Size = new System.Drawing.Size(166, 42);
			this.COMButton.TabIndex = 10;
			this.COMButton.Text = "串口发送";
			this.COMButton.UseVisualStyleBackColor = true;
			this.EthernetButton.ForeColor = System.Drawing.Color.Black;
			this.EthernetButton.Location = new System.Drawing.Point(256, 72);
			this.EthernetButton.Name = "EthernetButton";
			this.EthernetButton.Size = new System.Drawing.Size(166, 42);
			this.EthernetButton.TabIndex = 10;
			this.EthernetButton.Text = "网络发送";
			this.EthernetButton.UseVisualStyleBackColor = true;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(20, 43);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(77, 20);
			this.label7.TabIndex = 9;
			this.label7.Text = "默认接口";
			this.PortComboBox.FormattingEnabled = true;
			this.PortComboBox.Items.AddRange(new object[]
			{
				"串口",
				"网络"
			});
			this.PortComboBox.Location = new System.Drawing.Point(23, 71);
			this.PortComboBox.Name = "PortComboBox";
			this.PortComboBox.Size = new System.Drawing.Size(121, 28);
			this.PortComboBox.TabIndex = 8;
			this.groupBox3.Controls.Add(this.DATA16);
			this.groupBox3.Controls.Add(this.DATA8);
			this.groupBox3.Controls.Add(this.OE16);
			this.groupBox3.Controls.Add(this.DATA4);
			this.groupBox3.Controls.Add(this.OE8);
			this.groupBox3.Controls.Add(this.OE4);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.groupBox3.ForeColor = System.Drawing.Color.White;
			this.groupBox3.Location = new System.Drawing.Point(47, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(213, 237);
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "设置";
			this.DATA16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.DATA16.FormattingEnabled = true;
			this.DATA16.ItemHeight = 20;
			this.DATA16.Items.AddRange(new object[]
			{
				"低",
				"高"
			});
			this.DATA16.Location = new System.Drawing.Point(136, 172);
			this.DATA16.Name = "DATA16";
			this.DATA16.Size = new System.Drawing.Size(53, 28);
			this.DATA16.TabIndex = 1;
			this.DATA8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.DATA8.FormattingEnabled = true;
			this.DATA8.ItemHeight = 20;
			this.DATA8.Items.AddRange(new object[]
			{
				"低",
				"高"
			});
			this.DATA8.Location = new System.Drawing.Point(136, 116);
			this.DATA8.Name = "DATA8";
			this.DATA8.Size = new System.Drawing.Size(53, 28);
			this.DATA8.TabIndex = 1;
			this.DATA8.Visible = false;
			this.OE16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.OE16.FormattingEnabled = true;
			this.OE16.ItemHeight = 20;
			this.OE16.Items.AddRange(new object[]
			{
				"低",
				"高"
			});
			this.OE16.Location = new System.Drawing.Point(63, 172);
			this.OE16.Name = "OE16";
			this.OE16.Size = new System.Drawing.Size(53, 28);
			this.OE16.TabIndex = 1;
			this.DATA4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.DATA4.FormattingEnabled = true;
			this.DATA4.ItemHeight = 20;
			this.DATA4.Items.AddRange(new object[]
			{
				"低",
				"高"
			});
			this.DATA4.Location = new System.Drawing.Point(136, 60);
			this.DATA4.Name = "DATA4";
			this.DATA4.Size = new System.Drawing.Size(53, 28);
			this.DATA4.TabIndex = 1;
			this.OE8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.OE8.FormattingEnabled = true;
			this.OE8.ItemHeight = 20;
			this.OE8.Items.AddRange(new object[]
			{
				"低",
				"高"
			});
			this.OE8.Location = new System.Drawing.Point(63, 116);
			this.OE8.Name = "OE8";
			this.OE8.Size = new System.Drawing.Size(53, 28);
			this.OE8.TabIndex = 1;
			this.OE8.Visible = false;
			this.OE4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.OE4.FormattingEnabled = true;
			this.OE4.ItemHeight = 20;
			this.OE4.Items.AddRange(new object[]
			{
				"低",
				"高"
			});
			this.OE4.Location = new System.Drawing.Point(63, 60);
			this.OE4.Name = "OE4";
			this.OE4.Size = new System.Drawing.Size(53, 28);
			this.OE4.TabIndex = 1;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 25);
			this.label4.TabIndex = 0;
			this.label4.Text = "1/16";
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 25);
			this.label3.TabIndex = 0;
			this.label3.Text = "1/8";
			this.label3.Visible = false;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(138, 23);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(54, 25);
			this.label6.TabIndex = 0;
			this.label6.Text = "数据";
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(77, 23);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(43, 25);
			this.label5.TabIndex = 0;
			this.label5.Text = "OE";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 25);
			this.label2.TabIndex = 0;
			this.label2.Text = "1/4";
			this.groupBox2.Controls.Add(this.button4);
			this.groupBox2.Controls.Add(this.button8);
			this.groupBox2.Controls.Add(this.button16);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.groupBox2.ForeColor = System.Drawing.Color.White;
			this.groupBox2.Location = new System.Drawing.Point(266, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(260, 237);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "单一测试";
			this.button4.BackColor = System.Drawing.Color.White;
			this.button4.ForeColor = System.Drawing.Color.Black;
			this.button4.Location = new System.Drawing.Point(25, 44);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(197, 40);
			this.button4.TabIndex = 4;
			this.button4.Text = "1/4串口(Alt+Z)";
			this.button4.UseVisualStyleBackColor = false;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.button8.BackColor = System.Drawing.Color.White;
			this.button8.ForeColor = System.Drawing.Color.Black;
			this.button8.Location = new System.Drawing.Point(25, 176);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(197, 40);
			this.button8.TabIndex = 4;
			this.button8.Text = "1/16网络(Alt+A)";
			this.button8.UseVisualStyleBackColor = false;
			this.button8.Click += new EventHandler(this.button8_Click);
			this.button16.BackColor = System.Drawing.Color.White;
			this.button16.ForeColor = System.Drawing.Color.Black;
			this.button16.Location = new System.Drawing.Point(25, 115);
			this.button16.Name = "button16";
			this.button16.Size = new System.Drawing.Size(197, 40);
			this.button16.TabIndex = 4;
			this.button16.Text = "1/16串口(Alt+X)";
			this.button16.UseVisualStyleBackColor = false;
			this.button16.Click += new EventHandler(this.button16_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			base.ClientSize = new System.Drawing.Size(550, 394);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formPortTest";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "测试界面";
			base.FormClosed += new FormClosedEventHandler(this.formPortTest_Close);
			base.Load += new EventHandler(this.formPortTest_Load);
			base.KeyDown += new KeyEventHandler(this.formPortTest_KeyDown);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
