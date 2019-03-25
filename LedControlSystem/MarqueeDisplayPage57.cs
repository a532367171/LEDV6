using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage57 : MarqueeDisplayPageBase
	{
		private int pennum;

		private int nowx;

		private int nowy;

		public MarqueeDisplayPage57(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
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
			this.nowx = this.newBitmap.Height;
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
							this.nowx -= 8;
							this.nowy -= 8;
						}
						if (this.nowy > 0)
						{
							int i = 0;
							int width = this.newBitmap.Width;
							lock (this.newBitmap)
							{
								while (i < width)
								{
									graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(i + this.nowy, this.nowx - 8, this.nowy, 8), new System.Drawing.Rectangle(i + this.nowy, this.nowx, this.nowy - 8, 8), System.Drawing.GraphicsUnit.Pixel);
									for (int j = 0; j < 8; j++)
									{
										try
										{
											if (this.newBitmap.GetPixel(i + this.nowy, this.nowx + j).R > 10)
											{
												graphics.DrawLine(pen, new System.Drawing.Point(i + this.nowy, this.nowx - 8), new System.Drawing.Point(i + this.nowy, this.newBitmap.Height));
												break;
											}
										}
										catch
										{
										}
									}
									i += 8;
								}
							}
						}
						if (this.nowx > 0)
						{
							graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(0, 0, this.newBitmap.Width, this.newBitmap.Height - this.nowx), new System.Drawing.Rectangle(0, 0, this.newBitmap.Width, this.newBitmap.Height - this.nowx), System.Drawing.GraphicsUnit.Pixel);
						}
						this.nowy += (int)this.step;
						if (this.nowx <= 0)
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
