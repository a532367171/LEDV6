using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace IpInputExt.Ctrls
{
	public class IpInputTextbox : UserControl
	{
		private string ip;

		private static int text4;

		private static int text3;

		private static int text2;

		private IContainer components;

		private Panel pnlMain;

		private Label lbldot1;

		private Label lbldot3;

		private Label lbldot2;

		private NumberTextBoxExt Ip1;

		private NumberTextBoxExt Ip4;

		private NumberTextBoxExt Ip3;

		private NumberTextBoxExt Ip2;

		public string IP
		{
			get
			{
				return this.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Ip1.Text = "";
					this.Ip2.Text = "";
					this.Ip3.Text = "";
					this.Ip4.Text = "";
					this.ip = "";
					return;
				}
				try
				{
					IPAddress.Parse(value);
					string[] array = value.Split(new char[]
					{
						'.'
					});
					this.Ip1.Text = array[0];
					this.Ip2.Text = array[1];
					this.Ip3.Text = array[2];
					this.Ip4.Text = array[3];
					this.ip = value;
				}
				catch
				{
					this.Ip1.Text = "";
					this.Ip2.Text = "";
					this.Ip3.Text = "";
					this.Ip4.Text = "";
					this.ip = "";
				}
			}
		}

		public string[] IP_Array
		{
			get
			{
				return new string[]
				{
					this.Ip1.Text,
					this.Ip2.Text,
					this.Ip3.Text,
					this.Ip4.Text
				};
			}
		}

		public IpInputTextbox()
		{
			this.InitializeComponent();
			this.Ip1.Index = 1;
			this.Ip2.Index = 2;
			this.Ip3.Index = 3;
			this.Ip4.Index = 4;
			this.Ip1.OnPressBackspace += new NumberTextBoxExt.PressBackspaceHandle(this.Ip_OnPressBackspace);
			this.Ip2.OnPressBackspace += new NumberTextBoxExt.PressBackspaceHandle(this.Ip_OnPressBackspace);
			this.Ip3.OnPressBackspace += new NumberTextBoxExt.PressBackspaceHandle(this.Ip_OnPressBackspace);
			this.Ip4.OnPressBackspace += new NumberTextBoxExt.PressBackspaceHandle(this.Ip_OnPressBackspace);
			this.Ip1.OnPressRight += new NumberTextBoxExt.PressRightHandle(this.Ip_OnPressRight);
			this.Ip2.OnPressRight += new NumberTextBoxExt.PressRightHandle(this.Ip_OnPressRight);
			this.Ip3.OnPressRight += new NumberTextBoxExt.PressRightHandle(this.Ip_OnPressRight);
			this.Ip4.OnPressRight += new NumberTextBoxExt.PressRightHandle(this.Ip_OnPressRight);
		}

		private void Ip_OnPressRight(int index)
		{
			switch (index)
			{
			case 1:
			case 2:
			case 3:
				return;
			}
		}

		private void Ip_OnPressBackspace(int index)
		{
			switch (index)
			{
			case 2:
				if (IpInputTextbox.text2 == 1)
				{
					IpInputTextbox.text2 = 0;
					this.Ip1.Focus();
					this.Ip1.SelectionStart = this.Ip1.Text.Length;
					return;
				}
				IpInputTextbox.text2++;
				return;
			case 3:
				if (IpInputTextbox.text3 == 1)
				{
					IpInputTextbox.text3 = 0;
					this.Ip2.Focus();
					this.Ip2.SelectionStart = this.Ip2.Text.Length;
					return;
				}
				IpInputTextbox.text3++;
				return;
			case 4:
				if (IpInputTextbox.text4 == 1)
				{
					IpInputTextbox.text4 = 0;
					this.Ip3.Focus();
					this.Ip3.SelectionStart = this.Ip3.Text.Length;
					return;
				}
				IpInputTextbox.text4++;
				return;
			default:
				if (index != 99)
				{
					return;
				}
				if (this.Ip4.Text.Length != 0)
				{
					IpInputTextbox.text4 = 0;
				}
				if (this.Ip3.Text.Length != 0)
				{
					IpInputTextbox.text3 = 0;
				}
				if (this.Ip2.Text.Length != 0)
				{
					IpInputTextbox.text2 = 0;
				}
				return;
			}
		}

		private void Ip1_TextChanged(object sender, EventArgs e)
		{
			if (this.Ip1.Text.Length == 3 && this.Ip1.Text.Length > 0 && this.Ip1.SelectionLength == 0)
			{
				this.Ip2.Focus();
				this.Ip2.Select(0, this.Ip2.Text.Length);
			}
		}

		private void Ip2_TextChanged(object sender, EventArgs e)
		{
			if (this.Ip2.Text.Length == 3 && this.Ip2.Text.Length > 0 && this.Ip2.SelectionLength == 0)
			{
				this.Ip3.Focus();
				this.Ip3.Select(0, this.Ip3.Text.Length);
			}
		}

		private void Ip3_TextChanged(object sender, EventArgs e)
		{
			if (this.Ip3.Text.Length == 3 && this.Ip3.Text.Length > 0 && this.Ip3.SelectionLength == 0)
			{
				this.Ip4.Focus();
				this.Ip4.Select(0, this.Ip4.Text.Length);
			}
		}

		public override string ToString()
		{
			string ipString = string.Concat(new string[]
			{
				this.Ip1.Text,
				".",
				this.Ip2.Text,
				".",
				this.Ip3.Text,
				".",
				this.Ip4.Text
			});
			try
			{
				IPAddress.Parse(ipString);
			}
			catch
			{
				return "IP地址格式不正确";
			}
			this.ip = ipString;
			return this.ip;
		}

		private void Ip1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\t')
			{
				if (this.Ip1.Text.Length == 0 && e.KeyChar == '\'')
				{
					this.Ip2.Focus();
					this.Ip2.Select(0, this.Ip2.Text.Length);
				}
				this.Ip2.Focus();
				this.Ip2.Select(0, this.Ip2.Text.Length);
			}
		}

		private void Ip2_KeyPress(object sender, KeyPressEventArgs e)
		{
			int arg_0B_0 = this.Ip2.SelectionStart;
			if (e.KeyChar == '\t')
			{
				this.Ip3.Focus();
				this.Ip3.Select(0, this.Ip3.Text.Length);
			}
		}

		private void Ip3_KeyPress(object sender, KeyPressEventArgs e)
		{
			int arg_0B_0 = this.Ip3.SelectionStart;
			if (e.KeyChar == '\t')
			{
				this.Ip4.Focus();
				this.Ip4.Select(0, this.Ip4.Text.Length);
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
			this.pnlMain = new Panel();
			this.lbldot1 = new Label();
			this.lbldot2 = new Label();
			this.lbldot3 = new Label();
			this.Ip4 = new NumberTextBoxExt();
			this.Ip3 = new NumberTextBoxExt();
			this.Ip2 = new NumberTextBoxExt();
			this.Ip1 = new NumberTextBoxExt();
			this.pnlMain.SuspendLayout();
			base.SuspendLayout();
			this.pnlMain.BackColor = System.Drawing.Color.White;
			this.pnlMain.Paint += new PaintEventHandler(this.pnlMain_Paint);
			this.pnlMain.Cursor = Cursors.Default;
			this.pnlMain.BorderStyle = BorderStyle.None;
			this.pnlMain.Controls.Add(this.Ip4);
			this.pnlMain.Controls.Add(this.Ip3);
			this.pnlMain.Controls.Add(this.Ip2);
			this.pnlMain.Controls.Add(this.Ip1);
			this.pnlMain.Controls.Add(this.lbldot3);
			this.pnlMain.Controls.Add(this.lbldot2);
			this.pnlMain.Controls.Add(this.lbldot1);
			this.pnlMain.Dock = DockStyle.Top;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(160, 21);
			this.pnlMain.TabIndex = 0;
			this.lbldot1.AutoSize = true;
			this.lbldot1.Location = new System.Drawing.Point(18, 4);
			this.lbldot1.Name = "lbldot1";
			this.lbldot1.Size = new System.Drawing.Size(5, 12);
			this.lbldot1.TabIndex = 1;
			this.lbldot1.Text = ".";
			this.lbldot2.AutoSize = true;
			this.lbldot2.Location = new System.Drawing.Point(44, 4);
			this.lbldot2.Name = "lbldot2";
			this.lbldot2.Size = new System.Drawing.Size(5, 12);
			this.lbldot2.TabIndex = 5;
			this.lbldot2.Text = ".";
			this.lbldot3.AutoSize = true;
			this.lbldot3.Location = new System.Drawing.Point(70, 4);
			this.lbldot3.Name = "lbldot3";
			this.lbldot3.Size = new System.Drawing.Size(5, 12);
			this.lbldot3.TabIndex = 6;
			this.lbldot3.Text = ".";
			this.Ip4.BorderStyle = BorderStyle.None;
			this.Ip4.Location = new System.Drawing.Point(79, 3);
			this.Ip4.MaxLength = 3;
			this.Ip4.Name = "Ip4";
			this.Ip4.ShortcutsEnabled = false;
			this.Ip4.Size = new System.Drawing.Size(20, 14);
			this.Ip4.TabIndex = 4;
			this.Ip4.TextAlign = HorizontalAlignment.Center;
			this.Ip3.BorderStyle = BorderStyle.None;
			this.Ip3.Location = new System.Drawing.Point(53, 3);
			this.Ip3.MaxLength = 3;
			this.Ip3.Name = "Ip3";
			this.Ip3.ShortcutsEnabled = false;
			this.Ip3.Size = new System.Drawing.Size(20, 14);
			this.Ip3.TabIndex = 3;
			this.Ip3.TextAlign = HorizontalAlignment.Center;
			this.Ip3.TextChanged += new EventHandler(this.Ip3_TextChanged);
			this.Ip3.KeyPress += new KeyPressEventHandler(this.Ip3_KeyPress);
			this.Ip2.BorderStyle = BorderStyle.None;
			this.Ip2.Location = new System.Drawing.Point(27, 3);
			this.Ip2.MaxLength = 3;
			this.Ip2.Name = "Ip2";
			this.Ip2.ShortcutsEnabled = false;
			this.Ip2.Size = new System.Drawing.Size(20, 14);
			this.Ip2.TabIndex = 2;
			this.Ip2.TextAlign = HorizontalAlignment.Center;
			this.Ip2.TextChanged += new EventHandler(this.Ip2_TextChanged);
			this.Ip2.KeyPress += new KeyPressEventHandler(this.Ip2_KeyPress);
			this.Ip1.BorderStyle = BorderStyle.None;
			this.Ip1.Location = new System.Drawing.Point(1, 3);
			this.Ip1.MaxLength = 3;
			this.Ip1.Name = "Ip1";
			this.Ip1.ShortcutsEnabled = false;
			this.Ip1.Size = new System.Drawing.Size(20, 14);
			this.Ip1.TabIndex = 0;
			this.Ip1.TextAlign = HorizontalAlignment.Center;
			this.Ip1.TextChanged += new EventHandler(this.Ip1_TextChanged);
			this.Ip1.KeyPress += new KeyPressEventHandler(this.Ip1_KeyPress);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.pnlMain);
			base.Name = "IpInputTextbox";
			base.Size = new System.Drawing.Size(160, 21);
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			base.ResumeLayout(false);
		}

		private void pnlMain_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, this.pnlMain.ClientRectangle, System.Drawing.Color.Silver, 1, ButtonBorderStyle.Solid, System.Drawing.Color.Silver, 1, ButtonBorderStyle.Solid, System.Drawing.Color.Silver, 1, ButtonBorderStyle.Solid, System.Drawing.Color.Silver, 1, ButtonBorderStyle.Solid);
		}
	}
}
