using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeExistPage25 : MarqueeDisplayExitBase
	{
		private System.Drawing.Point[] pointLeft;

		public MarqueeExistPage25(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 0.1f / (float)this.effect.ExitSpeed;
			this.newBitmap = pNew;
			this.nowPositionF = 0f;
			this.nowState = MarqueeDisplayState.First;
		}

		private void GetLeft(int pLength)
		{
			int x = (int)((float)this.newBitmap.Width * this.nowPositionF);
			int y = (int)((float)this.newBitmap.Height * this.nowPositionF);
			this.pointLeft = new System.Drawing.Point[4];
			this.pointLeft[0] = new System.Drawing.Point(0, 0);
			this.pointLeft[1] = new System.Drawing.Point(x, 0);
			this.pointLeft[2] = new System.Drawing.Point(x, y);
			this.pointLeft[3] = new System.Drawing.Point(0, y);
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.nowState == MarqueeDisplayState.First)
			{
				int arg_1F_0 = (this.newBitmap.Width - this.nowPosition) / 2;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				this.GetLeft(this.nowPosition);
				System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
				graphics.FillPolygon(solidBrush, this.pointLeft);
				solidBrush.Dispose();
				graphics.Dispose();
				this.nowPositionF += this.step;
				if (this.nowPositionF >= 1f)
				{
					this.nowState = MarqueeDisplayState.Stay;
				}
				return bitmap;
			}
			return null;
		}
	}
}
