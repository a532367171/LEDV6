using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem
{
	public static class RoundFormPainter
	{
		public static void Paint(object sender, PaintEventArgs e)
		{
			if (Template.SmoothCorner)
			{
				Form form = (Form)sender;
				List<System.Drawing.Point> list = new List<System.Drawing.Point>();
				int width = form.Width;
				int height = form.Height;
				list.Add(new System.Drawing.Point(0, 7));
				list.Add(new System.Drawing.Point(1, 6));
				list.Add(new System.Drawing.Point(1, 5));
				list.Add(new System.Drawing.Point(2, 4));
				list.Add(new System.Drawing.Point(3, 3));
				list.Add(new System.Drawing.Point(4, 2));
				list.Add(new System.Drawing.Point(5, 1));
				list.Add(new System.Drawing.Point(6, 1));
				list.Add(new System.Drawing.Point(7, 1));
				list.Add(new System.Drawing.Point(8, 0));
				list.Add(new System.Drawing.Point(width - 7, 0));
				list.Add(new System.Drawing.Point(width - 6, 1));
				list.Add(new System.Drawing.Point(width - 5, 1));
				list.Add(new System.Drawing.Point(width - 4, 2));
				list.Add(new System.Drawing.Point(width - 3, 2));
				list.Add(new System.Drawing.Point(width - 3, 3));
				list.Add(new System.Drawing.Point(width - 2, 3));
				list.Add(new System.Drawing.Point(width - 2, 5));
				list.Add(new System.Drawing.Point(width - 1, 5));
				list.Add(new System.Drawing.Point(width - 1, 6));
				list.Add(new System.Drawing.Point(width, height));
				list.Add(new System.Drawing.Point(0, height));
				System.Drawing.Point[] points = list.ToArray();
				System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
				graphicsPath.AddPolygon(points);
				form.Region = new System.Drawing.Region(graphicsPath);
				return;
			}
			Form form2 = (Form)sender;
			form2.Region = new System.Drawing.Region(new System.Drawing.Rectangle(0, 0, form2.Width, form2.Height));
		}
	}
}
