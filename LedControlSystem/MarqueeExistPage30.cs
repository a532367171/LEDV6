using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeExistPage30 : MarqueeDisplayExitBase
	{
		public MarqueeExistPage30(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 0.1f / (float)this.effect.ExitSpeed;
			this.newBitmap = pNew;
			this.nowPositionF = 1f;
			this.nowState = MarqueeDisplayState.First;
		}

		private void GetLeft(int pLength)
		{
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.nowState == MarqueeDisplayState.First)
			{
				int arg_1F_0 = (this.newBitmap.Width - this.nowPosition) / 2;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap.Width, this.newBitmap.Height);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				this.GetLeft(this.nowPosition);
				System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
				int num = (int)((float)this.newBitmap.Width * this.nowPositionF);
				int num2 = (int)((float)this.newBitmap.Height * this.nowPositionF);
				graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle((this.newBitmap.Width - num) / 2, (this.newBitmap.Height - num2) / 2, num, num2), new System.Drawing.Rectangle((this.newBitmap.Width - num) / 2, (this.newBitmap.Height - num2) / 2, num, num2), System.Drawing.GraphicsUnit.Pixel);
				brush.Dispose();
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
