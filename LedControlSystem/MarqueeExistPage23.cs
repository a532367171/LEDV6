using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeExistPage23 : MarqueeDisplayExitBase
	{
		private System.Drawing.Point[] pointLeft;

		public MarqueeExistPage23(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 30f / (float)this.effect.ExitSpeed;
			this.newBitmap = pNew;
			this.nowPositionF = (float)(this.newBitmap.Height + this.newBitmap.Width);
			this.nowState = MarqueeDisplayState.First;
		}

		private void GetLeft(int pLength)
		{
			this.pointLeft = new System.Drawing.Point[4];
			this.pointLeft[0] = new System.Drawing.Point(pLength, 0);
			this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width + this.newBitmap.Height, 0);
			this.pointLeft[2] = new System.Drawing.Point(this.newBitmap.Width + this.newBitmap.Height, this.newBitmap.Height);
			this.pointLeft[3] = new System.Drawing.Point(pLength - this.newBitmap.Height, this.newBitmap.Height);
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.nowState == MarqueeDisplayState.First)
			{
				this.nowPosition = (int)this.nowPositionF;
				int arg_2C_0 = (this.newBitmap.Width - this.nowPosition) / 2;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				this.GetLeft(this.nowPosition);
				System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
				graphics.FillPolygon(solidBrush, this.pointLeft);
				solidBrush.Dispose();
				graphics.Dispose();
				this.nowPositionF -= this.step;
				if (this.nowPositionF <= 0f)
				{
					this.nowState = MarqueeDisplayState.Stay;
				}
				return bitmap;
			}
			return null;
		}
	}
}
