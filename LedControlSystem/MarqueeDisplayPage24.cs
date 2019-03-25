using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeDisplayPage24 : MarqueeDisplayPageBase
	{
		private bool returnNew;

		private int num;

		private int reNum;

		public MarqueeDisplayPage24(System.Drawing.Bitmap pLast, System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
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
			this.nowState = MarqueeDisplayState.First;
			this.reNum = (int)(this.step * 3f);
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
						System.Drawing.Bitmap bitmap;
						if (this.returnNew)
						{
							bitmap = new System.Drawing.Bitmap(this.newBitmap);
							this.returnNew = false;
						}
						else
						{
							bitmap = new System.Drawing.Bitmap(this.oldBitmap);
							this.returnNew = true;
						}
						this.num++;
						if (this.num >= 100)
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
