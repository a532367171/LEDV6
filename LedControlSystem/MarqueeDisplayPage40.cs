using LedModel.Foundation;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage40 : MarqueeDisplayPageBase
	{
		private System.Drawing.Point[] pointLeft;

		public MarqueeDisplayPage40(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
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
			this.nowPositionF = 1f;
			this.nowState = MarqueeDisplayState.First;
			this.Exit = MarqueeDisplay.getExtInstance(this.newBitmap, pSetting);
		}

		private void GetLeft(int pLength)
		{
			int num = (int)(this.nowPositionF * (float)this.newBitmap.Height);
			int num2 = (int)(this.nowPositionF * (float)this.newBitmap.Width);
			this.pointLeft = new System.Drawing.Point[4];
			this.pointLeft[0] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2 - num);
			this.pointLeft[1] = new System.Drawing.Point(this.newBitmap.Width / 2 - num2, this.newBitmap.Height / 2);
			this.pointLeft[2] = new System.Drawing.Point(this.newBitmap.Width / 2, this.newBitmap.Height / 2 + num);
			this.pointLeft[3] = new System.Drawing.Point(this.newBitmap.Width / 2 + num2, this.newBitmap.Height / 2);
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.newBitmap == null || this.newBitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined || this.oldBitmap == null || this.oldBitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined)
			{
				return new System.Drawing.Bitmap(8, 8);
			}
			System.Drawing.Bitmap result;
			lock (this.newBitmap)
			{
				lock (this.oldBitmap)
				{
					if (this.nowState == MarqueeDisplayState.First)
					{
						this.nowPosition = (int)this.nowPositionF;
						int arg_84_0 = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.oldBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						this.GetLeft(this.nowPosition);
						System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Purple);
						System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(this.newBitmap);
						System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap2);
						graphics2.FillPolygon(solidBrush, this.pointLeft);
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
