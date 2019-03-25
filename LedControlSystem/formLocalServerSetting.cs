using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formLocalServerSetting : Form
	{
		private static string formID = "formLocalServerSetting";

		private IContainer components;

		private Label lblLocalServerIPAddress;

		private Label lblLocalServerIPPort;

		private NumericUpDown nudLocalServerIPPort;

		private Button btnOK;

		private Button btnClose;

		private TextBox txtLocalServerIPAddress;

		public static string FormID
		{
			get
			{
				return formLocalServerSetting.formID;
			}
			set
			{
				formLocalServerSetting.formID = value;
			}
		}

		public formLocalServerSetting()
		{
			this.InitializeComponent();
			this.Diplay_lanuage_Text();
		}

		public void Diplay_lanuage_Text()
		{
			this.Text = formMain.ML.GetStr("formLocalServerSetting_FormText");
			this.lblLocalServerIPAddress.Text = formMain.ML.GetStr("formLocalServerSetting_labelLocalServerIPAddress");
			this.lblLocalServerIPPort.Text = formMain.ML.GetStr("formLocalServerSetting_labelLocalServerIPPort");
			this.btnOK.Text = formMain.ML.GetStr("formLocalServerSetting_button_OK");
			this.btnClose.Text = formMain.ML.GetStr("formLocalServerSetting_button_Close");
		}

		private void formLocalServerSetting_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.btnOK.Enabled = false;
			this.txtLocalServerIPAddress.Text = string.Empty;
			string hostName = Dns.GetHostName();
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
			IPAddress[] array = hostAddresses;
			for (int i = 0; i < array.Length; i++)
			{
				IPAddress iPAddress = array[i];
				if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
				{
					TextBox expr_63 = this.txtLocalServerIPAddress;
					expr_63.Text = expr_63.Text + iPAddress.ToString() + "\r\n";
				}
			}
			if (formMain.ledsys != null)
			{
				this.nudLocalServerIPPort.Value = formMain.ledsys.LocalServerIPPort;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			int num = (int)this.nudLocalServerIPPort.Value;
			formMain.IServer.restart_tcp_listen((ushort)num);
			if (formMain.ledsys != null)
			{
				formMain.ledsys.LocalServerIPPort = num;
			}
			this.btnOK.Enabled = false;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void nudLocalServerIPPort_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (!numericUpDown.Focused)
			{
				return;
			}
			if (!this.btnOK.Enabled)
			{
				this.btnOK.Enabled = true;
			}
		}

		private void nudLocalServerIPPort_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
				return;
			}
			if (!this.btnOK.Enabled)
			{
				this.btnOK.Enabled = true;
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
			this.lblLocalServerIPAddress = new Label();
			this.lblLocalServerIPPort = new Label();
			this.nudLocalServerIPPort = new NumericUpDown();
			this.btnOK = new Button();
			this.btnClose = new Button();
			this.txtLocalServerIPAddress = new TextBox();
			((ISupportInitialize)this.nudLocalServerIPPort).BeginInit();
			base.SuspendLayout();
			this.lblLocalServerIPAddress.AutoSize = true;
			this.lblLocalServerIPAddress.Location = new System.Drawing.Point(22, 18);
			this.lblLocalServerIPAddress.Name = "lblLocalServerIPAddress";
			this.lblLocalServerIPAddress.Size = new System.Drawing.Size(53, 12);
			this.lblLocalServerIPAddress.TabIndex = 0;
			this.lblLocalServerIPAddress.Text = "服务器IP";
			this.lblLocalServerIPPort.AutoSize = true;
			this.lblLocalServerIPPort.Location = new System.Drawing.Point(331, 18);
			this.lblLocalServerIPPort.Name = "lblLocalServerIPPort";
			this.lblLocalServerIPPort.Size = new System.Drawing.Size(29, 12);
			this.lblLocalServerIPPort.TabIndex = 1;
			this.lblLocalServerIPPort.Text = "端口";
			this.nudLocalServerIPPort.Location = new System.Drawing.Point(333, 39);
			NumericUpDown arg_148_0 = this.nudLocalServerIPPort;
			int[] array = new int[4];
			array[0] = 65535;
			arg_148_0.Maximum = new decimal(array);
			NumericUpDown arg_168_0 = this.nudLocalServerIPPort;
			int[] array2 = new int[4];
			array2[0] = 1024;
			arg_168_0.Minimum = new decimal(array2);
			this.nudLocalServerIPPort.Name = "nudLocalServerIPPort";
			this.nudLocalServerIPPort.Size = new System.Drawing.Size(120, 21);
			this.nudLocalServerIPPort.TabIndex = 2;
			NumericUpDown arg_1B8_0 = this.nudLocalServerIPPort;
			int[] array3 = new int[4];
			array3[0] = 1024;
			arg_1B8_0.Value = new decimal(array3);
			this.nudLocalServerIPPort.ValueChanged += new EventHandler(this.nudLocalServerIPPort_ValueChanged);
			this.nudLocalServerIPPort.KeyPress += new KeyPressEventHandler(this.nudLocalServerIPPort_KeyPress);
			this.btnOK.Location = new System.Drawing.Point(333, 181);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 30);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "应用";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnClose.Location = new System.Drawing.Point(422, 181);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 30);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.txtLocalServerIPAddress.Location = new System.Drawing.Point(24, 39);
			this.txtLocalServerIPAddress.Multiline = true;
			this.txtLocalServerIPAddress.Name = "txtLocalServerIPAddress";
			this.txtLocalServerIPAddress.ReadOnly = true;
			this.txtLocalServerIPAddress.ScrollBars = ScrollBars.Vertical;
			this.txtLocalServerIPAddress.Size = new System.Drawing.Size(212, 118);
			this.txtLocalServerIPAddress.TabIndex = 5;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(518, 220);
			base.Controls.Add(this.txtLocalServerIPAddress);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.nudLocalServerIPPort);
			base.Controls.Add(this.lblLocalServerIPPort);
			base.Controls.Add(this.lblLocalServerIPAddress);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formLocalServerSetting";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "本地服务器设置";
			base.Load += new EventHandler(this.formLocalServerSetting_Load);
			((ISupportInitialize)this.nudLocalServerIPPort).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
