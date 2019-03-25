using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeExistPage32 : MarqueeDisplayExitBase
	{
		private System.Drawing.Point[] pointLeft;

		public MarqueeExistPage32(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 0.03f;
			this.newBitmap = pNew;
			this.nowPositionF = 1f;
			this.nowState = MarqueeDisplayState.First;
		}

		private void GetLeft(int pLength)
		{
			int num = (int)(this.nowPositionF * (float)this.newBitmap.Height);
			int num2 = (int)(this.nowPositionF * (float)this.newBitmap.Width);
			this.pointLeft = new System.Drawing.Point[4];
			this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2 - num);
			this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width / 2 - num2, this.newBitmap.Height / 2);
			this.pointLeft[2] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2 + num);
			this.pointLeft[3] = new System.Drawing.Point(this.newBitmap.Width / 2 + num2, this.newBitmap.Height / 2);
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
				System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Silver);
				System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(this.newBitmap.Width, this.newBitmap.Height);
				System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap2);
				graphics2.Clear(System.Drawing.Color.Black);
				graphics2.FillPolygon(solidBrush, this.pointLeft);
				graphics2.Dispose();
				bitmap2.MakeTransparent(System.Drawing.Color.Silver);
				graphics.DrawImage(bitmap2, new System.Drawing.Point(0, 0));
				bitmap2.Dispose();
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
