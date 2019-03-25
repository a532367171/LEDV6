using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage28 : MarqueeDisplayPageBase
	{
		private System.Drawing.Point[] pointLeft;

		private System.Drawing.Point[] pointRight;

		public MarqueeDisplayPage28(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
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
			this.nowPositionF = (float)(this.newBitmap.Height + this.newBitmap.Width / 2);
			this.nowState = MarqueeDisplayState.First;
			this.Exit = MarqueeDisplay.getExtInstance(this.newBitmap, pSetting);
		}

		private void GetLeft(int pLength)
		{
			if (pLength <= this.newBitmap.Width / 2)
			{
				this.pointLeft = new System.Drawing.Point[3];
				this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2);
				this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
				this.pointLeft[2] = new System.Drawing.Point(this.newBitmap.Width / 2 - pLength, this.newBitmap.Height);
				this.pointRight = new System.Drawing.Point[3];
				this.pointRight[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2);
				this.pointRight[1] = new System.Drawing.Point(this.newBitmap.Width / 2, 0);
				this.pointRight[2] = new System.Drawing.Point(this.newBitmap.Width / 2 + pLength, 0);
				return;
			}
			if (pLength < this.newBitmap.Height + this.newBitmap.Width / 2)
			{
				this.pointLeft = new System.Drawing.Point[4];
				this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2);
				this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
				this.pointLeft[2] = new System.Drawing.Point(0, this.newBitmap.Height);
				this.pointLeft[3] = new System.Drawing.Point(0, this.newBitmap.Height - (pLength - this.newBitmap.Width / 2));
				this.pointRight = new System.Drawing.Point[4];
				this.pointRight[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2);
				this.pointRight[1] = new System.Drawing.Point(this.newBitmap.Width / 2, 0);
				this.pointRight[2] = new System.Drawing.Point(this.newBitmap.Width, 0);
				this.pointRight[3] = new System.Drawing.Point(this.newBitmap.Width, pLength - this.newBitmap.Width / 2);
				return;
			}
			this.pointLeft = new System.Drawing.Point[5];
			this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2);
			this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height);
			this.pointLeft[2] = new System.Drawing.Point(0, this.newBitmap.Height);
			this.pointLeft[3] = new System.Drawing.Point(0, 0);
			this.pointLeft[4] = new System.Drawing.Point(pLength - this.newBitmap.Height - this.newBitmap.Width / 2, 0);
			this.pointRight = new System.Drawing.Point[5];
			this.pointRight[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2);
			this.pointRight[1] = new System.Drawing.Point(this.newBitmap.Width / 2, 0);
			this.pointRight[2] = new System.Drawing.Point(this.newBitmap.Width, 0);
			this.pointRight[3] = new System.Drawing.Point(this.newBitmap.Width, this.newBitmap.Height);
			this.pointRight[4] = new System.Drawing.Point(this.newBitmap.Width - (pLength - this.newBitmap.Width / 2 - this.newBitmap.Height));
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
						int arg_52_0 = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.oldBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						this.GetLeft(this.nowPosition);
						System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Purple);
						System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(this.newBitmap);
						System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap2);
						graphics2.FillPolygon(solidBrush, this.pointLeft);
						graphics2.FillPolygon(solidBrush, this.pointRight);
						bitmap2.MakeTransparent(System.Drawing.Color.Purple);
						graphics.DrawImage(bitmap2, new System.Drawing.Point(0, 0));
						graphics2.Dispose();
						solidBrush.Dispose();
						bitmap2.Dispose();
						graphics.Dispose();
						this.nowPositionF -= this.step;
						if (this.nowPositionF <= 0f)
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
