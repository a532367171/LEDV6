using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeExistPage10 : MarqueeDisplayExitBase
	{
		public MarqueeExistPage10(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 30f / (float)this.effect.ExitSpeed;
			this.newBitmap = pNew;
			this.nowPositionF = 0f;
			this.nowState = MarqueeDisplayState.First;
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
				graphics.FillRectangle(brush, new System.Drawing.Rectangle((this.newBitmap.Width - this.nowPosition) / 2, 0, this.nowPosition, this.newBitmap.Height));
				this.nowPositionF += this.step;
				graphics.Dispose();
				if (this.nowPositionF >= (float)this.newBitmap.Width)
				{
					this.nowState = MarqueeDisplayState.Stay;
				}
				return bitmap;
			}
			return null;
		}
	}
}
