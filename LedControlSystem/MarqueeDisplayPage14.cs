using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage14 : MarqueeDisplayPageBase
	{
		private int pennum;

		public MarqueeDisplayPage14(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 0.03f;
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
						this.nowPosition = (int)(8f * this.nowPositionF);
						int arg_58_0 = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.oldBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Silver, (float)(9 - this.nowPosition));
						System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(this.newBitmap);
						System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap2);
						for (int i = 0; i < this.newBitmap.Height; i += 8)
						{
							graphics2.DrawLine(pen, new System.Drawing.Point(0, i + this.nowPosition), new System.Drawing.Point(this.newBitmap.Width, i + this.nowPosition));
						}
						graphics2.Dispose();
						bitmap2.MakeTransparent(System.Drawing.Color.Silver);
						graphics.DrawImage(bitmap2, new System.Drawing.Point(0, 0));
						bitmap2.Dispose();
						graphics.Dispose();
						this.nowPositionF += this.step;
						if (this.nowPositionF >= 1f)
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
