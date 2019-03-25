using LedModel.Foundation;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LedControlSystem
{
	public class MarqueeExistPage40 : MarqueeDisplayExitBase
	{
		private System.Drawing.Point pTop;

		private System.Drawing.Point pLeft;

		private System.Drawing.Point pRight;

		private System.Drawing.Point pBottom;

		private System.Drawing.Point pCenter;

		private int mx;

		private int my;

		private System.Drawing.Point[] pa1;

		private System.Drawing.Point[] pa2;

		private System.Drawing.Point[] pa3;

		private System.Drawing.Point[] pa4;

		public System.Drawing.Point getPoint1(int nowP)
		{
			if (nowP < this.my)
			{
				this.pa1 = new System.Drawing.Point[4];
				this.pa1[0] = this.pCenter;
				this.pa1[1] = this.pTop;
				this.pa1[2] = new System.Drawing.Point(0, 0);
				this.pa1[3] = new System.Drawing.Point(0, this.my - nowP);
				return new System.Drawing.Point(0, this.my - nowP);
			}
			this.pa1 = new System.Drawing.Point[3];
			this.pa1[0] = this.pCenter;
			this.pa1[1] = this.pTop;
			this.pa1[2] = new System.Drawing.Point(Math.Abs(this.my - nowP), 0);
			return new System.Drawing.Point(Math.Abs(this.my - nowP), 0);
		}

		public System.Drawing.Point getPoint2(int nowP)
		{
			if (nowP < this.mx)
			{
				this.pa2 = new System.Drawing.Point[4];
				this.pa2[0] = this.pCenter;
				this.pa2[1] = this.pRight;
				this.pa2[2] = new System.Drawing.Point(this.newBitmap.Width - 1, 0);
				this.pa2[3] = new System.Drawing.Point(this.mx + nowP, 0);
				return new System.Drawing.Point(this.mx + nowP, 0);
			}
			this.pa2 = new System.Drawing.Point[3];
			this.pa2[0] = this.pCenter;
			this.pa2[1] = this.pRight;
			this.pa2[2] = new System.Drawing.Point(this.newBitmap.Width - 1, nowP - this.mx);
			return new System.Drawing.Point(this.newBitmap.Width - 1, nowP - this.mx);
		}

		public System.Drawing.Point getPoint3(int nowP)
		{
			if (nowP < this.mx)
			{
				this.pa3 = new System.Drawing.Point[4];
				this.pa3[0] = this.pCenter;
				this.pa3[1] = this.pLeft;
				this.pa3[2] = new System.Drawing.Point(0, this.newBitmap.Height - 1);
				this.pa3[3] = new System.Drawing.Point(this.mx - nowP, this.newBitmap.Height - 1);
				return new System.Drawing.Point(this.mx - nowP, this.newBitmap.Height - 1);
			}
			this.pa3 = new System.Drawing.Point[3];
			this.pa3[0] = this.pCenter;
			this.pa3[1] = this.pLeft;
			this.pa3[2] = new System.Drawing.Point(0, this.newBitmap.Height - (nowP - this.mx));
			return new System.Drawing.Point(0, this.newBitmap.Height - (nowP - this.mx));
		}

		public System.Drawing.Point getPoint4(int nowP)
		{
			if (nowP < this.my)
			{
				this.pa4 = new System.Drawing.Point[4];
				this.pa4[0] = this.pCenter;
				this.pa4[1] = this.pBottom;
				this.pa4[2] = new System.Drawing.Point(this.newBitmap.Width - 1, this.newBitmap.Height - 1);
				this.pa4[3] = new System.Drawing.Point(this.newBitmap.Width - 1, this.my + nowP);
				return new System.Drawing.Point(this.newBitmap.Width - 1, this.my + nowP);
			}
			this.pa4 = new System.Drawing.Point[3];
			this.pa4[0] = this.pCenter;
			this.pa4[1] = this.pBottom;
			this.pa4[2] = new System.Drawing.Point(this.newBitmap.Width - (nowP - this.my), this.newBitmap.Height - 1);
			return new System.Drawing.Point(this.newBitmap.Width - (nowP - this.my), this.newBitmap.Height - 1);
		}

		public MarqueeExistPage40(System.Drawing.Bitmap pNew, LedEffectsSetting pSetting)
		{
			this.effect = pSetting;
			this.step = (float)(20 / this.effect.EntrySpeed);
			this.newBitmap = pNew;
			if (pNew.Width % 2 == 1)
			{
				this.mx = pNew.Width / 2 + 1;
			}
			else
			{
				this.mx = pNew.Width / 2;
			}
			if (pNew.Height % 2 == 1)
			{
				this.my = pNew.Height / 2 + 1;
			}
			else
			{
				this.my = pNew.Height / 2;
			}
			this.pTop = new System.Drawing.Point(this.mx, 0);
			this.pLeft = new System.Drawing.Point(0, this.my);
			this.pRight = new System.Drawing.Point(pNew.Width - 1, this.my);
			this.pBottom = new System.Drawing.Point(this.mx, pNew.Height - 1);
			this.pCenter = new System.Drawing.Point(this.mx, this.my);
			this.nowPositionF = (float)(this.newBitmap.Height / 2 + this.newBitmap.Width / 2);
		}

		public override System.Drawing.Bitmap GetNext()
		{
			if (this.nowState == MarqueeDisplayState.First)
			{
				this.nowPositionF -= this.step;
				if (this.nowPositionF > (float)((this.newBitmap.Height + this.newBitmap.Width) / 2))
				{
					this.nowState = MarqueeDisplayState.Stay;
				}
				this.nowPosition = (int)this.nowPositionF;
				this.getPoint1(this.nowPosition);
				this.getPoint2(this.nowPosition);
				this.getPoint3(this.nowPosition);
				this.getPoint4(this.nowPosition);
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.newBitmap);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
				graphicsPath.AddPolygon(this.pa1);
				graphics.SetClip(graphicsPath);
				graphics.Clear(System.Drawing.Color.Black);
				graphicsPath.ClearMarkers();
				graphicsPath.AddPolygon(this.pa2);
				graphics.SetClip(graphicsPath);
				graphics.Clear(System.Drawing.Color.Black);
				graphicsPath.ClearMarkers();
				graphicsPath.AddPolygon(this.pa3);
				graphics.SetClip(graphicsPath);
				graphics.Clear(System.Drawing.Color.Black);
				graphicsPath.ClearMarkers();
				graphicsPath.AddPolygon(this.pa4);
				graphics.SetClip(graphicsPath);
				graphics.Clear(System.Drawing.Color.Black);
				if (this.nowPositionF <= 0f)
				{
					this.nowState = MarqueeDisplayState.Second;
				}
				return bitmap;
			}
			return null;
		}
	}
}
