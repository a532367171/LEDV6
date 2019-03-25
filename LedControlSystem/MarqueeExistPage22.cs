using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeExistPage22 : MarqueeDisplayExitBase
	{
		private System.Drawing.Point[] pointLeft;

		public MarqueeExistPage22(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 30f / (float)this.effect.ExitSpeed;
			this.newBitmap = pNew;
			this.nowPositionF = 0f;
			this.nowState = MarqueeDisplayState.First;
		}

		private void GetLeft(int pLength)
		{
			if (pLength <= this.newBitmap.Height)
			{
				this.pointLeft = new System.Drawing.Point[3];
				this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width, 0);
				this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width, pLength);
				this.pointLeft[2] = new System.Drawing.Point(this.newBitmap.Width - pLength, 0);
				return;
			}
			this.pointLeft = new System.Drawing.Point[4];
			this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width, 0);
			this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width, pLength);
			this.pointLeft[2] = new System.Drawing.Point(this.newBitmap.Width - pLength + this.newBitmap.Height, this.newBitmap.Height);
			this.pointLeft[3] = new System.Drawing.Point(this.newBitmap.Width - pLength, 0);
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
				this.nowPositionF += this.step;
				if (this.nowPositionF >= (float)(this.newBitmap.Width + this.newBitmap.Height))
				{
					this.nowState = MarqueeDisplayState.Stay;
				}
				return bitmap;
			}
			return null;
		}
	}
}
