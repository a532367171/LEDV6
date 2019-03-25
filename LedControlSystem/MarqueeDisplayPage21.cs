using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage21 : MarqueeDisplayPageBase
	{
		private int y2;

		private int y3;

		private System.Drawing.Bitmap all;

		public MarqueeDisplayPage21(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
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
			this.nowPositionF = (float)(this.newBitmap.Height * 4);
			this.nowState = MarqueeDisplayState.First;
			this.all = new System.Drawing.Bitmap(this.newBitmap, new System.Drawing.Size(this.newBitmap.Width, this.newBitmap.Height * 3));
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
						this.y3 = (int)this.nowPositionF;
						graphics.DrawImage(this.all, new System.Drawing.Point(0, this.y3 - this.newBitmap.Height * 3));
						this.y2 = (this.newBitmap.Height * 3 - this.y3) / 2;
						graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(0, 0, this.newBitmap.Width, this.y2), new System.Drawing.Rectangle(0, 0, this.newBitmap.Width, this.y2), System.Drawing.GraphicsUnit.Pixel);
						graphics.Dispose();
						this.nowPositionF -= this.step;
						if (this.y3 <= this.newBitmap.Height)
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
