using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZHUI
{
	public class ZhToolStripSeparator : ToolStripSeparator
	{
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			graphics.Clear(this.BackColor);
			Pen pen = new Pen(Color.Gray);
			graphics.DrawLine(pen, 30, 3, base.Width - 1, 3);
		}
	}
}
