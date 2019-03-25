using System;
using System.Drawing;
using System.Windows.Forms;

namespace IpInputExt.Ctrls
{
	public class NumberTextBoxExt : TextBox
	{
		public delegate void PressBackspaceHandle(int index);

		public delegate void PressLeftHandle(int index);

		public delegate void PressRightHandle(int index);

		private int index;

		public event NumberTextBoxExt.PressBackspaceHandle OnPressBackspace;

		public event NumberTextBoxExt.PressRightHandle OnPressRight;

		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		public NumberTextBoxExt()
		{
			this.ShortcutsEnabled = false;
			this.MaxLength = 3;
			base.BorderStyle = BorderStyle.Fixed3D;
			this.BackColor = System.Drawing.SystemColors.Window;
			base.TextAlign = HorizontalAlignment.Left;
			base.Size = new System.Drawing.Size(30, 14);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			int ipNum = this.GetIpNum();
			if (ipNum >= 0 && ipNum > 255)
			{
				this.Text = "255";
				base.SelectionStart = 3;
			}
			base.OnTextChanged(e);
		}

		private int GetIpNum()
		{
			int result;
			if (this.Text.Length > 0 && int.TryParse(this.Text, out result))
			{
				return result;
			}
			return -1;
		}

		public override bool PreProcessMessage(ref Message m)
		{
			if (m.WParam.ToInt32() >= 48 && m.WParam.ToInt32() <= 57)
			{
				return base.PreProcessMessage(ref m);
			}
			if (m.WParam.ToInt32() >= 96 && m.WParam.ToInt32() <= 105)
			{
				return base.PreProcessMessage(ref m);
			}
			if (m.WParam.ToInt32() == 38 && m.WParam.ToInt32() == 40)
			{
				return base.PreProcessMessage(ref m);
			}
			if (m.WParam.ToInt32() == 37)
			{
				return base.PreProcessMessage(ref m);
			}
			if (m.WParam.ToInt32() == 39)
			{
				if (this.Text.Length == 0 && this.OnPressRight != null)
				{
					this.OnPressRight(this.index);
				}
				int arg_E0_0 = base.SelectionStart;
				return base.PreProcessMessage(ref m);
			}
			if (m.WParam.ToInt32() == 8)
			{
				if (this.Text.Length == 0)
				{
					if (this.OnPressBackspace != null)
					{
						this.OnPressBackspace(this.index);
					}
				}
				else if (this.OnPressBackspace != null)
				{
					this.OnPressBackspace(99);
				}
				return base.PreProcessMessage(ref m);
			}
			if (m.WParam.ToInt32() == 9)
			{
				return base.ProcessKeyMessage(ref m);
			}
			if (m.WParam.ToInt32() == 110)
			{
				m.WParam = new IntPtr(9);
				return base.ProcessKeyMessage(ref m);
			}
			return m.WParam.ToInt32() != 46 || base.PreProcessMessage(ref m);
		}
	}
}
