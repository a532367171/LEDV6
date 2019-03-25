using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeExistPage17 : MarqueeDisplayExitBase
	{
		private System.Drawing.Point[] pointLeft;

		public MarqueeExistPage17(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = (float)(20 / this.effect.EntrySpeed);
			this.newBitmap = pNew;
			this.nowPositionF = 0f;
			this.nowState = MarqueeDisplayState.First;
		}

		private void GetLeft(int pLength)
		{
			if (pLength < this.newBitmap.Width / 2)
			{
				this.pointLeft = new System.Drawing.Point[3];
				this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
				this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width / 2 - pLength, 0);
				this.pointLeft[2] = new System.Drawing.Point(this.newBitmap.Width / 2 + pLength, 0);
				return;
			}
			this.pointLeft = new System.Drawing.Point[5];
			this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
			this.pointLeft[1] = new System.Drawing.Point(0, this.newBitmap.Height);
			this.pointLeft[2] = new System.Drawing.Point(0, 0);
			this.pointLeft[3] = new System.Drawing.Point(this.newBitmap.Width, 0);
			this.pointLeft[4] = new System.Drawing.Point(this.newBitmap.Width, pLength - this.newBitmap.Width / 2);
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.nowState == MarqueeDisplayState.First)
			{
				this.nowPosition = (int)this.nowPositionF;
				int arg_2C_0 = (this.newBitmap.Width - this.nowPosition) / 2;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
				this.GetLeft(this.nowPosition);
				graphics.FillPolygon(brush, this.pointLeft);
				brush.Dispose();
				graphics.Dispose();
				this.nowPositionF += this.step;
				if (this.nowPosition >= this.newBitmap.Width / 2 + this.newBitmap.Height)
				{
					this.nowState = MarqueeDisplayState.Stay;
				}
				return bitmap;
			}
			return null;
		}
	}
}
