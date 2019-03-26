using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ZHUI
{
	public class ZhLabel : UserControl
	{
		private string text = "text1";

		private Graphics g;

		private TextAlaginType textAlign;

		private IContainer components;

		[Browsable(true), Category("文本1"), DefaultValue("文本3"), Description("要显示的文本")]
		public new string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		[Browsable(true), Category("文本1"), DefaultValue("文本3"), Description("对齐方式")]
		public TextAlaginType TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				this.textAlign = value;
				this.Refresh();
			}
		}

		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				this.Refresh();
			}
		}

		public ZhLabel()
		{
			this.InitializeComponent();
			this.BackColor = Color.Transparent;
			this.textAlign = TextAlaginType.Right;
		}

		private void Label_Paint(object sender, PaintEventArgs e)
		{
			this.g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			string s = (string)base.Tag;
			this.g.DrawString(s, this.Font, new SolidBrush(this.ForeColor), new PointF(0f, 0f));
		}

		private void Label_Load(object sender, EventArgs e)
		{
			this.g = base.CreateGraphics();
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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "Label";
			base.Size = new Size(77, 18);
			base.Load += new EventHandler(this.Label_Load);
			base.Paint += new PaintEventHandler(this.Label_Paint);
			base.ResumeLayout(false);
		}
	}
}
