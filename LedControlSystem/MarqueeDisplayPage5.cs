using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage5 : MarqueeDisplayPageBase
	{
		private int pennum;

		public MarqueeDisplayPage5(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = (float)(30 / this.effect.EntrySpeed);
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
			this.nowPositionF = 0f;
			this.nowState = MarqueeDisplayState.First;
			this.pennum = pNew.Width / 20;
			if (this.pennum == 0)
			{
				this.pennum = 1;
			}
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
						this.nowPosition = (int)this.nowPositionF;
						int arg_50_0 = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.oldBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(0, 0, this.newBitmap.Width, this.nowPosition), new System.Drawing.Rectangle(0, this.newBitmap.Height - this.nowPosition, this.newBitmap.Width, this.nowPosition), System.Drawing.GraphicsUnit.Pixel);
						this.nowPositionF += (float)((int)this.step);
						if (this.nowPosition >= this.newBitmap.Height)
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
