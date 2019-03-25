using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplayPage51 : MarqueeDisplayPageBase
	{
		public MarqueeDisplayPage51(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = (float)(20 / this.effect.EntrySpeed);
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
						int num = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
						graphics.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, num, this.newBitmap.Height));
						graphics.FillRectangle(brush, new System.Drawing.Rectangle(num + this.nowPosition, 0, num, this.newBitmap.Height));
						brush.Dispose();
						System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red);
						graphics.DrawLine(pen, new System.Drawing.Point(num, 0), new System.Drawing.Point(num, this.newBitmap.Height));
						graphics.DrawLine(pen, new System.Drawing.Point(num - 1, 0), new System.Drawing.Point(num - 1, this.newBitmap.Height));
						graphics.DrawLine(pen, new System.Drawing.Point(num - 2, 0), new System.Drawing.Point(num - 2, this.newBitmap.Height));
						graphics.DrawLine(pen, new System.Drawing.Point(num + this.nowPosition, 0), new System.Drawing.Point(num + this.nowPosition, this.newBitmap.Height));
						graphics.DrawLine(pen, new System.Drawing.Point(num + this.nowPosition + 1, 0), new System.Drawing.Point(num + this.nowPosition + 1, this.newBitmap.Height));
						graphics.DrawLine(pen, new System.Drawing.Point(num + this.nowPosition + 2, 0), new System.Drawing.Point(num + this.nowPosition + 2, this.newBitmap.Height));
						pen.Dispose();
						graphics.Dispose();
						this.nowPositionF += this.step;
						if (this.nowPosition >= this.newBitmap.Width)
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
