using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ZHUI
{
	public static class RoundFormPainter
	{
		public static void Paint(object sender, PaintEventArgs e)
		{
			Form form = (Form)sender;
			List<Point> list = new List<Point>();
			int width = form.Width;
			int height = form.Height;
			list.Add(new Point(0, 5));
			list.Add(new Point(1, 5));
			list.Add(new Point(1, 3));
			list.Add(new Point(2, 3));
			list.Add(new Point(2, 2));
			list.Add(new Point(3, 2));
			list.Add(new Point(3, 1));
			list.Add(new Point(5, 1));
			list.Add(new Point(5, 0));
			list.Add(new Point(width - 5, 0));
			list.Add(new Point(width - 5, 1));
			list.Add(new Point(width - 3, 1));
			list.Add(new Point(width - 3, 2));
			list.Add(new Point(width - 2, 2));
			list.Add(new Point(width - 2, 3));
			list.Add(new Point(width - 1, 3));
			list.Add(new Point(width - 1, 5));
			list.Add(new Point(width, 5));
			list.Add(new Point(width, height - 5));
			list.Add(new Point(width - 1, height - 5));
			list.Add(new Point(width - 1, height - 3));
			list.Add(new Point(width - 2, height - 3));
			list.Add(new Point(width - 2, height - 2));
			list.Add(new Point(width - 3, height - 2));
			list.Add(new Point(width - 3, height - 1));
			list.Add(new Point(width - 5, height - 1));
			list.Add(new Point(width - 5, height));
			list.Add(new Point(5, height));
			list.Add(new Point(5, height - 1));
			list.Add(new Point(3, height - 1));
			list.Add(new Point(3, height - 2));
			list.Add(new Point(2, height - 2));
			list.Add(new Point(2, height - 3));
			list.Add(new Point(1, height - 3));
			list.Add(new Point(1, height - 5));
			list.Add(new Point(0, height - 5));
			Point[] points = list.ToArray();
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddPolygon(points);
			form.Region = new Region(graphicsPath);
		}
	}
}
