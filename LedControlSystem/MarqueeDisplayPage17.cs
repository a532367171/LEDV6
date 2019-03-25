using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage17 : MarqueeDisplayPageBase
	{
		private int y1;

		private int y2;

		private int y3;

		private System.Drawing.Bitmap all;

		public MarqueeDisplayPage17(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = (float)(30 / pSetting.EntrySpeed);
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
			this.nowPositionF = (float)(-(float)this.newBitmap.Height * 3);
			this.nowState = MarqueeDisplayState.First;
			this.all = new System.Drawing.Bitmap(this.newBitmap, new System.Drawing.Size(this.newBitmap.Width, this.newBitmap.Height * 3));
			System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(this.all);
			System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);
			for (int i = 0; i < this.all.Height; i += 2)
			{
				graphics2.DrawLine(pen, new System.Drawing.Point(0, i), new System.Drawing.Point(this.newBitmap.Width, i));
			}
			graphics2.Dispose();
			this.y1 = -this.newBitmap.Height;
			this.y3 = this.newBitmap.Height;
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
						int arg_56_0 = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.oldBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						this.y1 = (int)this.nowPositionF;
						this.y2 = -(this.newBitmap.Height * 3 + this.y1 - this.y3 * 3) / 2;
						graphics.DrawImage(this.all, new System.Drawing.Point(0, this.y1));
						graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(0, this.y2, this.newBitmap.Width, this.y3 - this.y2), new System.Drawing.Rectangle(0, this.y2, this.newBitmap.Width, this.y3 - this.y2), System.Drawing.GraphicsUnit.Pixel);
						graphics.Dispose();
						this.nowPositionF += this.step;
						if (this.y1 >= 0)
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
