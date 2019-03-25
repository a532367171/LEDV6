using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage54 : MarqueeDisplayPageBase
	{
		private int pennum;

		private int nowx;

		private int nowy;

		public MarqueeDisplayPage54(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
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
						int arg_52_0 = (this.newBitmap.Width - this.nowPosition) / 2;
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.oldBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red);
						while (this.nowy > 8)
						{
							this.nowx += 8;
							this.nowy -= 8;
						}
						if (this.nowy > 0)
						{
							for (int i = 0; i < this.newBitmap.Height; i += 8)
							{
								graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(this.nowx, 0, 8, this.nowy), new System.Drawing.Rectangle(this.nowx, 0, 8, this.nowy), System.Drawing.GraphicsUnit.Pixel);
								for (int j = 0; j < 8; j++)
								{
									try
									{
										if (this.newBitmap.GetPixel(this.nowx + j, i + this.nowy).R > 10)
										{
											graphics.DrawLine(pen, new System.Drawing.Point(this.nowx, i + this.nowy), new System.Drawing.Point(this.newBitmap.Width, i + this.nowy));
											break;
										}
									}
									catch
									{
									}
								}
							}
						}
						if (this.nowx > 0)
						{
							graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(0, 0, this.nowx, this.newBitmap.Height), new System.Drawing.Rectangle(0, 0, this.nowx, this.newBitmap.Height), System.Drawing.GraphicsUnit.Pixel);
						}
						this.nowy += (int)this.step;
						if (this.nowx >= this.newBitmap.Width)
						{
							this.nowState = MarqueeDisplayState.Stay;
						}
						pen.Dispose();
						graphics.Dispose();
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
