using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeExistPage3 : MarqueeDisplayExitBase
	{
		public MarqueeExistPage3(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 30f / (float)this.effect.ExitSpeed;
			this.newBitmap = pNew;
			this.nowPositionF = (float)this.newBitmap.Width;
			this.nowState = MarqueeDisplayState.First;
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.nowState == MarqueeDisplayState.First)
			{
				this.nowPosition = (int)this.nowPositionF;
				int arg_2C_0 = (this.newBitmap.Width - this.nowPosition) / 2;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap.Width, this.newBitmap.Height);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(0, 0, this.nowPosition, this.newBitmap.Height), new System.Drawing.Rectangle(this.newBitmap.Width - this.nowPosition, 0, this.nowPosition, this.newBitmap.Height), System.Drawing.GraphicsUnit.Pixel);
				this.nowPositionF -= this.step;
				graphics.Dispose();
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
