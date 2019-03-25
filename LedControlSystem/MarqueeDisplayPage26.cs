using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplayPage26 : MarqueeDisplayPageBase
	{
		private System.Drawing.Point[] pointLeft;

		public MarqueeDisplayPage26(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
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
			this.nowPositionF = (float)(this.newBitmap.Width / 2 + this.newBitmap.Height);
			this.nowState = MarqueeDisplayState.First;
			this.Exit = MarqueeDisplay.getExtInstance(this.newBitmap, pSetting);
		}

		private void GetLeft(int pLength)
		{
			if (pLength < this.newBitmap.Width / 2)
			{
				this.pointLeft = new System.Drawing.Point[3];
				this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
				this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width / 2 - pLength, 0);
				this.pointLeft[2] = new System.Drawing.Point(this.newBitmap.Width / 2 + pLength, 0);
				return;
			}
			this.pointLeft = new System.Drawing.Point[5];
			this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
			this.pointLeft[1] = new System.Drawing.Point(0, pLength - this.newBitmap.Width / 2);
			this.pointLeft[2] = new System.Drawing.Point(0, 0);
			this.pointLeft[3] = new System.Drawing.Point(this.newBitmap.Width, 0);
			this.pointLeft[4] = new System.Drawing.Point(this.newBitmap.Width, pLength - this.newBitmap.Width / 2);
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
						int arg_51_0 = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
						this.GetLeft(this.nowPosition);
						graphics.FillPolygon(brush, this.pointLeft);
						brush.Dispose();
						graphics.Dispose();
						this.nowPositionF -= this.step;
						if (this.nowPosition <= 0)
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
