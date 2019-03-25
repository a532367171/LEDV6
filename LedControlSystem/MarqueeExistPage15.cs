using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeExistPage15 : MarqueeDisplayExitBase
	{
		private int pennum;

		public MarqueeExistPage15(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 0.03f;
			this.newBitmap = pNew;
			this.nowPositionF = 1f;
			this.nowState = MarqueeDisplayState.First;
			this.pennum = pNew.Width / 20;
			if (this.pennum == 0)
			{
				this.pennum = 1;
			}
		}

		private void GetLeft(int pLength)
		{
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.nowState == MarqueeDisplayState.First)
			{
				this.nowPosition = (int)(8f * this.nowPositionF);
				int arg_32_0 = (this.newBitmap.Width - this.nowPosition) / 2;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, (float)(9 - this.nowPosition));
				for (int i = 0; i < this.newBitmap.Height; i += 8)
				{
					graphics.DrawLine(pen, new System.Drawing.Point(0, i + this.nowPosition), new System.Drawing.Point(this.newBitmap.Width, i + this.nowPosition));
				}
				graphics.Dispose();
				this.nowPositionF -= this.step;
				if (this.nowPositionF <= 0f)
				{
					this.nowState = MarqueeDisplayState.Stay;
				}
				return bitmap;
			}
			return null;
		}
	}
}
