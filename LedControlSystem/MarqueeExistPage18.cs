using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeExistPage18 : MarqueeDisplayExitBase
	{
		private System.Drawing.Point[] pointLeft;

		private System.Drawing.Point[] pointRight;

		public MarqueeExistPage18(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = (float)(20 / this.effect.EntrySpeed);
			this.newBitmap = pNew;
			this.nowPositionF = (float)(this.newBitmap.Width / 2 + this.newBitmap.Height);
			this.nowState = MarqueeDisplayState.First;
		}

		private void GetLeft(int pLength)
		{
			if (pLength < this.newBitmap.Width / 2)
			{
				this.pointLeft = new System.Drawing.Point[4];
				this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
				this.pointLeft[1] = new System.Drawing.Point(0, this.newBitmap.Height);
				this.pointLeft[2] = new System.Drawing.Point(0, 0);
				this.pointLeft[3] = new System.Drawing.Point(this.newBitmap.Width / 2 - pLength, 0);
				this.pointRight = new System.Drawing.Point[4];
				this.pointRight[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
				this.pointRight[1] = new System.Drawing.Point(this.newBitmap.Width, this.newBitmap.Height);
				this.pointRight[2] = new System.Drawing.Point(this.newBitmap.Width, 0);
				this.pointRight[3] = new System.Drawing.Point(this.newBitmap.Width / 2 + pLength);
				return;
			}
			this.pointLeft = new System.Drawing.Point[3];
			this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
			this.pointLeft[1] = new System.Drawing.Point(0, this.newBitmap.Height);
			this.pointLeft[2] = new System.Drawing.Point(0, pLength - this.newBitmap.Width / 2);
			this.pointRight = new System.Drawing.Point[3];
			this.pointRight[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
			this.pointRight[1] = new System.Drawing.Point(this.newBitmap.Width, this.newBitmap.Height);
			this.pointRight[2] = new System.Drawing.Point(this.newBitmap.Width, pLength - this.newBitmap.Width / 2);
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
				graphics.FillPolygon(brush, this.pointRight);
				brush.Dispose();
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
