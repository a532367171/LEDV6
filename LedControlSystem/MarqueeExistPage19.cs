using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	internal class MarqueeExistPage19 : MarqueeDisplayExitBase
	{
		private System.Drawing.Point[] pointLeft;

		private System.Drawing.Point[] pointRight;

		public MarqueeExistPage19(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = 30f / (float)this.effect.ExitSpeed;
			this.newBitmap = pNew;
			this.nowPositionF = 0f;
			this.nowState = MarqueeDisplayState.First;
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
			this.pointRight[4] = new System.Drawing.Point(this.newBitmap.Width - (pLength - this.newBitmap.Width / 2 - this.newBitmap.Height), this.newBitmap.Height);
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.nowState == MarqueeDisplayState.First)
			{
				this.nowPosition = (int)this.nowPositionF;
				int arg_2C_0 = (this.newBitmap.Width - this.nowPosition) / 2;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				this.GetLeft(this.nowPosition);
				System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
				graphics.FillPolygon(solidBrush, this.pointLeft);
				graphics.FillPolygon(solidBrush, this.pointRight);
				solidBrush.Dispose();
				graphics.Dispose();
				this.nowPositionF += this.step;
				if (this.nowPositionF >= (float)(this.newBitmap.Height + this.newBitmap.Width))
				{
					this.nowState = MarqueeDisplayState.Stay;
				}
				return bitmap;
			}
			return null;
		}
	}
}
