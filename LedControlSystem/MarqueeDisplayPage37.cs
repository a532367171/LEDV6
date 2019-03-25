using LedModel.Foundation;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace LedControlSystem
{
	public class MarqueeDisplayPage37 : MarqueeDisplayPageBase
	{
		private System.Drawing.Rectangle rect;

		public MarqueeDisplayPage37(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 0.03f;
			if (pLast == null)
			{
				this.oldBitmap = new System.Drawing.Bitmap(pNew.Width, pNew.Height);
			}
			else
			{
				this.oldBitmap = pLast;
			}
			this.newBitmap = pNew;
			this.nowPositionF = 0f;
			this.nowState = MarqueeDisplayState.First;
			this.Exit = MarqueeDisplay.getExtInstance(this.newBitmap, pSetting);
		}

		private void GetLeft(int pLength)
		{
			int x = 0;
			int y = 0;
			int num = 0;
			int num2 = 0;
			if (this.newBitmap != null && this.newBitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
			{
				num = (int)(this.nowPositionF * (float)this.newBitmap.Width);
				num2 = (int)(this.nowPositionF * (float)this.newBitmap.Height);
				x = (this.newBitmap.Width - num) / 2;
				y = (this.newBitmap.Height - num2) / 2;
			}
			this.rect = new System.Drawing.Rectangle(x, y, num, num2);
		}

		public override System.Drawing.Bitmap GetNext()
		{
			System.Drawing.Bitmap result;
			lock (this.newBitmap)
			{
				lock (this.oldBitmap)
				{
					if (this.nowState == MarqueeDisplayState.First)
					{
						this.nowPosition = (int)this.nowPositionF;
						int arg_50_0 = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.oldBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						this.GetLeft(this.nowPosition);
						graphics.DrawImage(this.newBitmap, this.rect, this.rect, System.Drawing.GraphicsUnit.Pixel);
						graphics.Dispose();
						this.nowPositionF += this.step;
						if (this.nowPosition >= 1)
						{
							this.nowState = MarqueeDisplayState.Stay;
						}
						result = bitmap;
					}
					else if (this.nowState == MarqueeDisplayState.Stay)
					{
						this.StayNum += 42;
						if (this.StayNum > this.effect.Stay)
						{
							if (this.effect.ExitMode == 0)
							{
								result = null;
								return result;
							}
							this.nowState = MarqueeDisplayState.Exit;
						}
						result = new System.Drawing.Bitmap(this.newBitmap);
					}
					else
					{
						result = this.Exit.GetNext();
					}
				}
			}
			return result;
		}
	}
}
