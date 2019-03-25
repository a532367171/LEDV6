using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeExistPage41 : MarqueeDisplayExitBase
	{
		public MarqueeExistPage41(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 20f / (float)this.effect.EntrySpeed;
			this.newBitmap = pNew;
			this.nowPositionF = (float)this.newBitmap.Width;
			this.nowState = MarqueeDisplayState.First;
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.nowState == MarqueeDisplayState.First)
			{
				this.nowPosition = (int)this.nowPositionF;
				int num = (this.newBitmap.Width - this.nowPosition) / 2;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
				graphics.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, num, this.newBitmap.Height));
				graphics.FillRectangle(brush, new System.Drawing.Rectangle(num + this.nowPosition, 0, num, this.newBitmap.Height));
				brush.Dispose();
				System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red);
				graphics.DrawLine(pen, new System.Drawing.Point(num, 0), new System.Drawing.Point(num, this.newBitmap.Height));
				graphics.DrawLine(pen, new System.Drawing.Point(num - 1, 0), new System.Drawing.Point(num - 1, this.newBitmap.Height));
				graphics.DrawLine(pen, new System.Drawing.Point(num - 2, 0), new System.Drawing.Point(num - 2, this.newBitmap.Height));
				graphics.DrawLine(pen, new System.Drawing.Point(num + this.nowPosition, 0), new System.Drawing.Point(num + this.nowPosition, this.newBitmap.Height));
				graphics.DrawLine(pen, new System.Drawing.Point(num + this.nowPosition + 1, 0), new System.Drawing.Point(num + this.nowPosition + 1, this.newBitmap.Height));
				graphics.DrawLine(pen, new System.Drawing.Point(num + this.nowPosition + 2, 0), new System.Drawing.Point(num + this.nowPosition + 2, this.newBitmap.Height));
				pen.Dispose();
				graphics.Dispose();
				this.nowPositionF -= this.step;
				if (this.nowPosition <= 0)
				{
					this.nowState = MarqueeDisplayState.Stay;
				}
				return bitmap;
			}
			return null;
		}
	}
}
