using LedControlSystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formComTest : Form
	{
		private static string formID = "formComTest";

		private int readNum;

		private byte[] testData = new byte[]
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10
		};

		private RadioButton[] radioButtonList;

		private string NowPort;

		private int totalRead;

		private IList<byte> receiveList = new List<byte>();

		private IContainer components;

		private Panel panel1;

		private GroupBox groupBoxCom;

		private PictureBox pictureBox2;

		private TextBox textBox2;

		private Label labelMessage;

		private GroupBox groupBox2;

		private Label label_Operation4;

		private Label label_Operation3;

		private Label label_Operation2;

		private Label label_Operation1;

		private TextBox textBox1;

		private Panel panelstart;

		private System.Windows.Forms.Timer timer1;

		private SerialPort serialPort1;

		private Panel panel2;

		private Label label_Operation5;

		private Label label_Operation6;

		private Label label1;

		public static string FormID
		{
			get
			{
				return formComTest.formID;
			}
			set
			{
				formComTest.formID = value;
			}
		}

		public formComTest()
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formComTest.formID;
			this.Text = formMain.ML.GetStr("formComTest_FormText");
			this.label_Operation1.Text = formMain.ML.GetStr("formComTest_label_Operation_Introduction");
			this.label_Operation2.Text = formMain.ML.GetStr("formComTest_label_Operation_First");
			this.label_Operation3.Text = formMain.ML.GetStr("formComTest_label_Operation_Second");
			this.label_Operation4.Text = formMain.ML.GetStr("formComTest_label_Operation_third");
			this.groupBoxCom.Text = formMain.ML.GetStr("formComTest_groupBoxCom");
			this.label_Operation5.Text = formMain.ML.GetStr("formComTest_label_Operation_Method");
			this.label_Operation6.Text = formMain.ML.GetStr("formComTest_label_Operation_Connection");
			this.label1.Text = formMain.ML.GetStr("formComTest_label_JackDescription");
			this.textBox1.Text = formMain.ML.GetStr("formComTest_textBox_Send");
			this.textBox2.Text = formMain.ML.GetStr("formComTest_textBox_Rec");
		}

		public void rescan()
		{
			if (this.radioButtonList != null && this.radioButtonList.Length > 0)
			{
				for (int i = 0; i < this.radioButtonList.Length; i++)
				{
					this.radioButtonList[i].Dispose();
				}
			}
			string[] portNames = SerialPort.GetPortNames();
			this.radioButtonList = new RadioButton[portNames.Length];
			int num = 0;
			string[] array = portNames;
			for (int j = 0; j < array.Length; j++)
			{
				string text = array[j];
				this.radioButtonList[num] = new RadioButton();
				this.radioButtonList[num].Parent = this.groupBoxCom;
				this.radioButtonList[num].Location = new System.Drawing.Point(16, num * 48 + 48);
				this.radioButtonList[num].Name = text;
				this.radioButtonList[num].Text = text;
				this.radioButtonList[num].ForeColor = System.Drawing.Color.White;
				this.radioButtonList[num].Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Bold);
				this.radioButtonList[num].Tag = text;
				if (this.serialPort1.PortName == text)
				{
					this.radioButtonList[num].Checked = true;
					this.NowPort = text;
				}
				this.radioButtonList[num].CheckedChanged += new EventHandler(this.formComTest_CheckedChanged);
				num++;
			}
		}

		private void formComTest_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			this.NowPort = (string)radioButton.Tag;
			this.panelstart.Enabled = true;
		}

		private void formComTest_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.panel2.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
			this.testData = new byte[256];
			for (int i = 0; i < this.testData.Length; i++)
			{
				this.testData[i] = (byte)i;
			}
			this.rescan();
		}

		private void panelstart_Click(object sender, EventArgs e)
		{
			this.textBox1.Text = "";
			this.textBox2.Text = "";
			this.labelMessage.Text = "";
			this.Refresh();
			Thread.Sleep(200);
			this.totalRead = 0;
			this.receiveList.Clear();
			this.panelstart.Enabled = false;
			this.readNum = 0;
			this.textBox1.Text = "";
			this.textBox2.Text = "";
			try
			{
				if (this.serialPort1.IsOpen)
				{
					this.serialPort1.Close();
				}
				this.serialPort1.PortName = this.NowPort;
				this.serialPort1.BaudRate = 1200;
				this.serialPort1.Open();
				this.timer1.Start();
				DateTime now = DateTime.Now;
				this.serialPort1.Write(this.testData, 0, this.testData.Length);
				DateTime now2 = DateTime.Now;
				TimeSpan timeSpan = now2 - now;
				this.textBox1.Text = timeSpan.TotalMilliseconds.ToString() + formMain.ML.GetStr("Display_Send") + ":" + this.BytesToString(this.testData);
				TextBox expr_148 = this.textBox2;
				expr_148.Text = expr_148.Text + formMain.ML.GetStr("Display_Receive") + ":";
			}
			catch (Exception ex)
			{
				this.labelMessage.ForeColor = System.Drawing.Color.Red;
				this.labelMessage.Text = formMain.ML.GetStr("Prompt_ComTestFail") + "," + ex.Message;
			}
		}

		private void StopTest()
		{
		}

		private bool CheckResult()
		{
			for (int i = 0; i < this.receiveList.Count; i++)
			{
				if (this.receiveList[i] != this.testData[i])
				{
					return false;
				}
			}
			return true;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.readNum++;
			if (this.serialPort1.IsOpen)
			{
				if (this.readNum > 500)
				{
					this.timer1.Stop();
					this.panelstart.Enabled = true;
					this.labelMessage.ForeColor = System.Drawing.Color.Red;
					this.labelMessage.Text = formMain.ML.GetStr("Prompt_ComTestFail");
					return;
				}
				int bytesToRead = this.serialPort1.BytesToRead;
				if (bytesToRead > 0)
				{
					this.totalRead += bytesToRead;
					byte[] array = new byte[bytesToRead];
					this.serialPort1.Read(array, 0, bytesToRead);
					TextBox expr_AA = this.textBox2;
					expr_AA.Text += this.BytesToString(array);
					for (int i = 0; i < array.Length; i++)
					{
						this.receiveList.Add(array[i]);
					}
					if (this.totalRead == 256)
					{
						this.timer1.Stop();
						this.panelstart.Enabled = true;
						if (this.CheckResult())
						{
							this.labelMessage.ForeColor = System.Drawing.Color.FromArgb(0, 255, 0);
							this.labelMessage.Text = formMain.ML.GetStr("Prompt_ComTestSuccess");
							return;
						}
						this.labelMessage.ForeColor = System.Drawing.Color.Red;
						this.labelMessage.Text = formMain.ML.GetStr("Prompt_ComTestFail");
					}
				}
			}
		}

		private string BytesToString(byte[] pData)
		{
			string text = "";
			for (int i = 0; i < pData.Length; i++)
			{
				text = text + pData[i].ToString("X2") + " ";
			}
			return text;
		}

		private void formComTest_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				this.serialPort1.Close();
				this.timer1.Stop();
			}
			catch
			{
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formComTest));
			this.panel1 = new Panel();
			this.panel2 = new Panel();
			this.label1 = new Label();
			this.label_Operation6 = new Label();
			this.label_Operation5 = new Label();
			this.pictureBox2 = new PictureBox();
			this.groupBoxCom = new GroupBox();
			this.textBox2 = new TextBox();
			this.labelMessage = new Label();
			this.groupBox2 = new GroupBox();
			this.label_Operation4 = new Label();
			this.label_Operation3 = new Label();
			this.label_Operation2 = new Label();
			this.label_Operation1 = new Label();
			this.textBox1 = new TextBox();
			this.panelstart = new Panel();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.serialPort1 = new SerialPort(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.groupBoxCom);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.labelMessage);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.panelstart);
			this.panel1.Location = new System.Drawing.Point(-4, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(605, 636);
			this.panel1.TabIndex = 21;
			this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.label_Operation6);
			this.panel2.Controls.Add(this.label_Operation5);
			this.panel2.Controls.Add(this.pictureBox2);
			this.panel2.Location = new System.Drawing.Point(175, 159);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(419, 368);
			this.panel2.TabIndex = 16;
			this.label1.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(226, 144);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(138, 34);
			this.label1.TabIndex = 17;
			this.label1.Text = "插孔说明";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.label_Operation6.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label_Operation6.ForeColor = System.Drawing.Color.Red;
			this.label_Operation6.Location = new System.Drawing.Point(19, 130);
			this.label_Operation6.Name = "label_Operation6";
			this.label_Operation6.Size = new System.Drawing.Size(138, 34);
			this.label_Operation6.TabIndex = 16;
			this.label_Operation6.Text = "金属丝";
			this.label_Operation6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.label_Operation5.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label_Operation5.ForeColor = System.Drawing.Color.Red;
			this.label_Operation5.Location = new System.Drawing.Point(3, 0);
			this.label_Operation5.Name = "label_Operation5";
			this.label_Operation5.Size = new System.Drawing.Size(413, 109);
			this.label_Operation5.TabIndex = 15;
			this.label_Operation5.Text = "开始测试前,请先用金属丝讲串口通讯线9孔串口的2,3短接(注意:测试结束后将金属丝去除)";
			this.label_Operation5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.pictureBox2.Image = (System.Drawing.Image)componentResourceManager.GetObject("pictureBox2.Image");
			this.pictureBox2.ImeMode = ImeMode.NoControl;
			this.pictureBox2.Location = new System.Drawing.Point(3, 167);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(412, 200);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 14;
			this.pictureBox2.TabStop = false;
			this.groupBoxCom.ForeColor = System.Drawing.Color.White;
			this.groupBoxCom.Location = new System.Drawing.Point(5, 150);
			this.groupBoxCom.Name = "groupBoxCom";
			this.groupBoxCom.Size = new System.Drawing.Size(164, 377);
			this.groupBoxCom.TabIndex = 9;
			this.groupBoxCom.TabStop = false;
			this.groupBoxCom.Text = "当前可用串口";
			this.textBox2.Enabled = false;
			this.textBox2.Location = new System.Drawing.Point(102, 590);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(274, 31);
			this.textBox2.TabIndex = 10;
			this.textBox2.Text = "接收：";
			this.labelMessage.BorderStyle = BorderStyle.Fixed3D;
			this.labelMessage.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Bold);
			this.labelMessage.ForeColor = System.Drawing.Color.Lime;
			this.labelMessage.ImeMode = ImeMode.NoControl;
			this.labelMessage.Location = new System.Drawing.Point(379, 545);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(215, 76);
			this.labelMessage.TabIndex = 13;
			this.groupBox2.Controls.Add(this.label_Operation4);
			this.groupBox2.Controls.Add(this.label_Operation3);
			this.groupBox2.Controls.Add(this.label_Operation2);
			this.groupBox2.Controls.Add(this.label_Operation1);
			this.groupBox2.Location = new System.Drawing.Point(5, -9);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(597, 153);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.label_Operation4.AutoSize = true;
			this.label_Operation4.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label_Operation4.ForeColor = System.Drawing.Color.White;
			this.label_Operation4.ImeMode = ImeMode.NoControl;
			this.label_Operation4.Location = new System.Drawing.Point(8, 127);
			this.label_Operation4.Name = "label_Operation4";
			this.label_Operation4.Size = new System.Drawing.Size(315, 14);
			this.label_Operation4.TabIndex = 2;
			this.label_Operation4.Text = "第三步：单击“开始测试”，右下角显示测试结果";
			this.label_Operation3.AutoSize = true;
			this.label_Operation3.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label_Operation3.ForeColor = System.Drawing.Color.White;
			this.label_Operation3.ImeMode = ImeMode.NoControl;
			this.label_Operation3.Location = new System.Drawing.Point(8, 99);
			this.label_Operation3.Name = "label_Operation3";
			this.label_Operation3.Size = new System.Drawing.Size(301, 14);
			this.label_Operation3.TabIndex = 2;
			this.label_Operation3.Text = "第二步：在左边的串口列表中选择要测试的串口";
			this.label_Operation2.AutoSize = true;
			this.label_Operation2.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label_Operation2.ForeColor = System.Drawing.Color.White;
			this.label_Operation2.ImeMode = ImeMode.NoControl;
			this.label_Operation2.Location = new System.Drawing.Point(8, 71);
			this.label_Operation2.Name = "label_Operation2";
			this.label_Operation2.Size = new System.Drawing.Size(231, 14);
			this.label_Operation2.TabIndex = 1;
			this.label_Operation2.Text = "第一步：按照下图所示短接2，3引脚";
			this.label_Operation1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.label_Operation1.ForeColor = System.Drawing.Color.White;
			this.label_Operation1.ImeMode = ImeMode.NoControl;
			this.label_Operation1.Location = new System.Drawing.Point(4, 17);
			this.label_Operation1.Name = "label_Operation1";
			this.label_Operation1.Size = new System.Drawing.Size(578, 45);
			this.label_Operation1.TabIndex = 0;
			this.label_Operation1.Text = "说明：此工具旨在测试电脑串口是否可用，串口通信线是否正常，在测试前请确认通信线与电脑连接。";
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(102, 545);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(274, 31);
			this.textBox1.TabIndex = 11;
			this.textBox1.Text = "发送：";
			this.panelstart.BackColor = System.Drawing.Color.Transparent;
			this.panelstart.BackgroundImage = (System.Drawing.Image)componentResourceManager.GetObject("panelstart.BackgroundImage");
			this.panelstart.BackgroundImageLayout = ImageLayout.Zoom;
			this.panelstart.Location = new System.Drawing.Point(4, 533);
			this.panelstart.Name = "panelstart";
			this.panelstart.Size = new System.Drawing.Size(96, 96);
			this.panelstart.TabIndex = 15;
			this.panelstart.Click += new EventHandler(this.panelstart_Click);
			this.timer1.Enabled = true;
			this.timer1.Interval = 30;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.serialPort1.BaudRate = 38400;
			this.serialPort1.ReadBufferSize = 8192;
			this.serialPort1.WriteBufferSize = 8192;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(599, 635);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formComTest";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "串口测试";
			base.FormClosing += new FormClosingEventHandler(this.formComTest_FormClosing);
			base.Load += new EventHandler(this.formComTest_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox2).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
