using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplayPage15 : MarqueeDisplayPageBase
	{
		public MarqueeDisplayPage15(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = (float)(20 / this.effect.EntrySpeed);
			pNew.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
			if (pLast == null)
			{
				this.oldBitmap = new System.Drawing.Bitmap(pNew.Width, pNew.Height);
			}
			else
			{
				this.oldBitmap = pLast;
			}
			this.newBitmap = pNew;
			this.Exit = MarqueeDisplay.getExtInstance(pNew, pSetting);
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
						this.nowPositionF += this.step;
						if (this.nowPositionF > (float)this.newBitmap.Height)
						{
							this.nowState = MarqueeDisplayState.Second;
						}
					}
					else if (this.nowState == MarqueeDisplayState.Second)
					{
						this.nowPositionF -= this.step;
						if (this.nowPositionF < 0f)
						{
							this.nowState = MarqueeDisplayState.Third;
							this.newBitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
						}
					}
					else if (this.nowState == MarqueeDisplayState.Third)
					{
						this.nowPositionF += this.step;
						if (this.nowPositionF > (float)this.newBitmap.Height)
						{
							this.nowState = MarqueeDisplayState.Stay;
						}
					}
					else
					{
						if (this.nowState == MarqueeDisplayState.Stay)
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
							return result;
						}
						if (this.nowState == MarqueeDisplayState.Exit)
						{
							result = this.Exit.GetNext();
							return result;
						}
					}
					this.nowPosition = (int)this.nowPositionF;
					System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.oldBitmap);
					System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
					graphics.DrawImage(this.newBitmap, new System.Drawing.Rectangle(0, (this.newBitmap.Height - this.nowPosition) / 2, this.newBitmap.Width, this.nowPosition), new System.Drawing.Rectangle(0, 0, this.newBitmap.Width, this.newBitmap.Height), System.Drawing.GraphicsUnit.Pixel);
					result = bitmap;
				}
			}
			return result;
		}
	}
}
