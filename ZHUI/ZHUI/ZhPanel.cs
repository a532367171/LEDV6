using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZHUI
{
	public class ZhPanel : Panel
	{
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			Pen pen;
			try
			{
				pen = new Pen(Template.Panel_BroderColor);
			}
			catch
			{
				pen = new Pen(Color.Gray);
			}
			graphics.DrawLine(pen, 0, 0, base.Width, 0);
			graphics.DrawLine(pen, 0, 0, 0, base.Height);
			graphics.DrawLine(pen, 0, base.Height - 1, base.Width, base.Height - 1);
			graphics.DrawLine(pen, base.Width - 1, 0, base.Width - 1, base.Height);
		}

		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			this.Refresh();
		}
	}
}
