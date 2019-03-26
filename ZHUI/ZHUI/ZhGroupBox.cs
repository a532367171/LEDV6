using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ZHUI
{
	public class ZhGroupBox : Panel
	{
		private Color textColor;

		private Font textFont;

		[Browsable(true), Category("N1"), Description("说明文本的颜色")]
		public Color TextColor
		{
			get
			{
				return this.textColor;
			}
			set
			{
				this.textColor = value;
			}
		}

		[Browsable(true), Category("N1"), Description("说明文本的字体")]
		public Font TextFont
		{
			get
			{
				return this.textFont;
			}
			set
			{
				this.textFont = value;
			}
		}

		[Browsable(true), Category("N1"), Description("文本")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			try
			{
				Pen pen = new Pen(Template.MainForm_BackColor);
				graphics.DrawRectangle(pen, new Rectangle(0, 0, Template.GroupBox_LeftTop.Width - 1, Template.GroupBox_LeftTop.Height - 1));
				graphics.DrawRectangle(pen, new Rectangle(base.Width - Template.GroupBox_LeftTop.Width, 0, Template.GroupBox_LeftTop.Width - 1, Template.GroupBox_LeftTop.Height - 1));
				graphics.DrawRectangle(pen, new Rectangle(0, base.Height - Template.GroupBox_LeftTop.Height, Template.GroupBox_LeftTop.Width - 1, Template.GroupBox_LeftTop.Height - 1));
				graphics.DrawRectangle(pen, new Rectangle(base.Width - Template.GroupBox_LeftTop.Width, base.Height - Template.GroupBox_LeftTop.Height, Template.GroupBox_LeftTop.Width - 1, Template.GroupBox_LeftTop.Height - 1));
				graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				graphics.DrawImage(Template.GroupBox_LeftTop, new PointF(0f, 0f));
				graphics.DrawImage(Template.GroupBox_LeftBottom, new Point(0, base.Height - Template.GroupBox_LeftBottom.Height));
				graphics.DrawImage(Template.GroupBox_RightTop, new Point(base.Width - Template.GroupBox_RightTop.Width, 0));
				graphics.DrawImage(Template.GroupBox_RightBottom, new Point(base.Width - Template.GroupBox_RightBottom.Width, base.Height - Template.GroupBox_RightBottom.Height));
				this.BackColor = Template.GroupBox_BackColor;
				Pen pen2 = new Pen(Template.GroupBox_BroderColor);
				graphics.DrawLine(pen2, new Point(Template.GroupBox_LeftTop.Width, 0), new Point(base.Width - Template.GroupBox_RightTop.Width, 0));
				graphics.DrawLine(pen2, new Point(0, Template.GroupBox_LeftTop.Height), new Point(0, base.Height - Template.GroupBox_LeftBottom.Height));
				graphics.DrawLine(pen2, new Point(base.Width - 1, Template.GroupBox_RightTop.Height - 3), new Point(base.Width - 1, base.Height - Template.GroupBox_RightBottom.Height));
				graphics.DrawLine(pen2, new Point(Template.GroupBox_LeftTop.Width, base.Height - 1), new Point(base.Width - Template.GroupBox_RightBottom.Width, base.Height - 1));
				if (this.Text.Length > 0)
				{
					Brush brush = new SolidBrush(this.textColor);
					graphics.DrawString(this.Text, this.TextFont, brush, new PointF(3f, 3f));
				}
			}
			catch
			{
				graphics.DrawString((string)base.Tag, this.textFont, new SolidBrush(this.textColor), new PointF(0f, 0f));
			}
		}

		public ZhGroupBox()
		{
			this.textFont = new Font("微软雅黑", 12f, FontStyle.Bold, GraphicsUnit.Pixel);
			this.textColor = Color.Black;
		}

		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			this.Refresh();
		}
	}
}
