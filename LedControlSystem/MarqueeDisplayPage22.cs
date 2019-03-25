using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage22 : MarqueeDisplayPageBase
	{
		private int x1;

		private int x2;

		private int x3;

		private System.Drawing.Bitmap all;

		public MarqueeDisplayPage22(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 30f / (float)pSetting.EntrySpeed;
			if (pLast == null)
			{
				this.oldBitmap = new System.Drawing.Bitmap(pNew.Width, pNew.Height);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(this.oldBitmap);
				graphics.Clear(System.Drawing.Color.Black);
				graphics.Dispose();
			}
			else
			{
				this.oldBitmap = pLast;
			}
			this.newBitmap = pNew;
			this.nowPositionF = (float)(-(float)this.newBitmap.Width * 3);
			this.nowState = MarqueeDisplayState.First;
			this.all = new System.Drawing.Bitmap(this.newBitmap, new System.Drawing.Size(this.newBitmap.Width * 3, this.newBitmap.Height));
			this.x3 = this.newBitmap.Width;
			this.Exit = MarqueeDisplay.getExtInstance(this.newBitmap, pSetting);
		}

		private void GetLeft(int pLength)
		{
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
						int arg_43_0 = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.oldBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						this.x1 = (int)this.nowPositionF;
						this.x2 = -this.x1 / 2;
						graphics.DrawImage(this.all, new System.Drawing.Point(this.x1, 0));
						graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(this.x2, 0, this.newBitmap.Width - this.x2, this.newBitmap.Height), new System.Drawing.Rectangle(this.x2, 0, this.newBitmap.Width - this.x2, this.newBitmap.Height), System.Drawing.GraphicsUnit.Pixel);
						graphics.Dispose();
						this.nowPositionF += this.step;
						if (this.x1 >= 0)
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
