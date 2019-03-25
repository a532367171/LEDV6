using LedModel;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace LedControlSystem
{
	internal class EdgeDisplay
	{
		private LedEdge ledEdge;

		private System.Drawing.Bitmap AlphaBitmap;

		private System.Drawing.Bitmap StaticBitmap;

		private System.Drawing.Bitmap EdgeBitmap;

		private System.Drawing.Size subSize;

		private int circular;

		private decimal circularCount;

		private LedSubarea ledSub;

		public static System.Drawing.Color alphaColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);

		private int flashCount;

		private LedItem ledItem;

		private System.Drawing.Bitmap itemEdgeTop;

		private System.Drawing.Bitmap itemEdgeLeft;

		private System.Drawing.Bitmap itemEdgeBottom;

		private System.Drawing.Bitmap itemEdgeRight;

		public bool itemFlashBlack;

		public System.Drawing.Bitmap ItemEdgeTop
		{
			get
			{
				if (this.itemFlashBlack)
				{
					return new System.Drawing.Bitmap(this.subSize.Width, this.ledEdge.Height);
				}
				return this.itemEdgeTop;
			}
			set
			{
				this.itemEdgeTop = value;
			}
		}

		public System.Drawing.Bitmap ItemEdgeLeft
		{
			get
			{
				if (this.itemFlashBlack)
				{
					return new System.Drawing.Bitmap(this.ledEdge.Height, this.subSize.Width);
				}
				return this.itemEdgeLeft;
			}
			set
			{
				this.itemEdgeLeft = value;
			}
		}

		public System.Drawing.Bitmap ItemEdgeBottom
		{
			get
			{
				if (this.itemFlashBlack)
				{
					return new System.Drawing.Bitmap(this.subSize.Width, this.ledEdge.Height);
				}
				return this.itemEdgeBottom;
			}
		}

		public System.Drawing.Bitmap ItemEdgeRight
		{
			get
			{
				if (this.itemFlashBlack)
				{
					return new System.Drawing.Bitmap(this.ledEdge.Height, this.subSize.Width);
				}
				return this.itemEdgeRight;
			}
		}

		public EdgeDisplay(LedEdge edge, LedSubarea pSub)
		{
			this.ledEdge = edge;
			this.ledSub = pSub;
			this.subSize = pSub.Size;
			this.circular = (this.subSize.Width + this.subSize.Height) * 2;
			this.circularCount = 0m;
			lock (LedGlobal.LedEdgeList[edge.Index])
			{
				if (this.ledEdge.Enabled)
				{
					this.AlphaBitmap = new System.Drawing.Bitmap((this.subSize.Width + this.subSize.Height) * 2, edge.Height);
					this.EdgeBitmap = new System.Drawing.Bitmap((this.subSize.Width + this.subSize.Height) * 2, edge.Height);
					System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(this.EdgeBitmap);
					System.Drawing.TextureBrush brush = new System.Drawing.TextureBrush(LedGlobal.LedEdgeList[edge.Index]);
					graphics.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, this.EdgeBitmap.Width, this.EdgeBitmap.Height));
					if (this.ledEdge.Mode != LedEdgeMode.Clockwise && this.ledEdge.Mode == LedEdgeMode.CounterClockwise)
					{
						this.EdgeBitmap = this.SetCornerAlpha(this.EdgeBitmap);
					}
					this.StaticBitmap = new System.Drawing.Bitmap(this.subSize.Width, this.subSize.Height);
					System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(this.StaticBitmap);
					graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Point(0, 0));
					graphics2.TranslateTransform(0f, 0f);
					graphics2.RotateTransform(90f);
					graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Rectangle(0, -this.subSize.Width, this.subSize.Height, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width, -1, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
					graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Rectangle(0, -this.subSize.Width, this.subSize.Height, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width, 0, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
					graphics2.RotateTransform(90f);
					graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Rectangle(-this.subSize.Width, -this.subSize.Height, this.subSize.Width, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width + this.subSize.Height, -1, this.subSize.Width, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
					graphics2.RotateTransform(90f);
					graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Rectangle(-this.subSize.Height, 0, this.subSize.Height, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width * 2 + this.subSize.Height, 0, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
				}
			}
		}

		private void SplitBitmapToItemEdge(System.Drawing.Bitmap pBit)
		{
			this.itemEdgeTop = new System.Drawing.Bitmap(this.subSize.Width, this.ledEdge.Height);
			this.itemEdgeLeft = new System.Drawing.Bitmap(this.ledEdge.Height, this.subSize.Height);
			this.itemEdgeBottom = new System.Drawing.Bitmap(this.subSize.Width, this.ledEdge.Height);
			this.itemEdgeRight = new System.Drawing.Bitmap(this.ledEdge.Height, this.subSize.Height);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(this.itemEdgeTop);
			graphics.DrawImage(pBit, new System.Drawing.Point(0, 0));
			System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(this.itemEdgeRight);
			graphics2.TranslateTransform(0f, 0f);
			graphics2.RotateTransform(90f);
			graphics2.DrawImage(pBit, new System.Drawing.Rectangle(0, -this.itemEdgeRight.Width, this.itemEdgeRight.Height, this.itemEdgeRight.Width), new System.Drawing.Rectangle(this.subSize.Width, -1, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
			graphics2.DrawImage(pBit, new System.Drawing.Rectangle(0, -this.itemEdgeRight.Width, this.itemEdgeRight.Height, this.itemEdgeRight.Width), new System.Drawing.Rectangle(this.subSize.Width, 0, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
			System.Drawing.Graphics graphics3 = System.Drawing.Graphics.FromImage(this.itemEdgeBottom);
			graphics3.TranslateTransform(0f, 0f);
			graphics3.RotateTransform(180f);
			graphics3.DrawImage(pBit, new System.Drawing.Rectangle(-this.itemEdgeBottom.Width, -this.itemEdgeBottom.Height, this.itemEdgeBottom.Width, this.itemEdgeBottom.Height), new System.Drawing.Rectangle(this.subSize.Width + this.subSize.Height, -1, this.subSize.Width, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
			System.Drawing.Graphics graphics4 = System.Drawing.Graphics.FromImage(this.itemEdgeLeft);
			graphics4.TranslateTransform(0f, 0f);
			graphics4.RotateTransform(270f);
			graphics4.DrawImage(pBit, new System.Drawing.Rectangle(-this.subSize.Height, 0, this.subSize.Width, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width * 2 + this.subSize.Height, 0, this.subSize.Width, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
		}

		public EdgeDisplay(LedEdge edge, LedItem pItem)
		{
			this.ledEdge = edge;
			this.ledItem = pItem;
			this.subSize = new System.Drawing.Size(pItem.ParentPanel.Width, pItem.ParentPanel.Height);
			this.circular = (this.subSize.Width + this.subSize.Height) * 2;
			this.circularCount = 0m;
			if (this.ledEdge.Enabled)
			{
				this.AlphaBitmap = new System.Drawing.Bitmap((this.subSize.Width + this.subSize.Height) * 2, edge.Height);
				this.EdgeBitmap = new System.Drawing.Bitmap((this.subSize.Width + this.subSize.Height) * 2, edge.Height);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(this.EdgeBitmap);
				System.Drawing.TextureBrush brush = new System.Drawing.TextureBrush(LedGlobal.LedEdgeList[edge.Index]);
				graphics.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, this.EdgeBitmap.Width, this.EdgeBitmap.Height));
				if (this.ledEdge.Mode == LedEdgeMode.Static | this.ledEdge.Mode == LedEdgeMode.Blink)
				{
					this.EdgeBitmap = this.SetCornerAlpha(this.EdgeBitmap);
					this.SplitBitmapToItemEdge(this.EdgeBitmap);
				}
			}
		}

		public void GetNewItemEdge()
		{
			if (!this.ledEdge.Enabled)
			{
				return;
			}
			switch (this.ledEdge.Mode)
			{
			case LedEdgeMode.Static:
				break;
			case LedEdgeMode.Clockwise:
			case LedEdgeMode.CounterClockwise:
			{
				if (this.circularCount >= this.circular)
				{
					this.circularCount = 0m;
				}
				if (this.circularCount <= -1m)
				{
					this.circularCount = this.circular;
				}
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.AlphaBitmap);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				if (this.circularCount == 0m)
				{
					graphics.DrawImage(this.EdgeBitmap, new System.Drawing.Point(0, 0));
				}
				else
				{
					graphics.DrawImage(this.EdgeBitmap, new System.Drawing.Point((int)this.circularCount, 0));
					graphics.DrawImage(this.EdgeBitmap, new System.Drawing.Rectangle(0, 0, (int)this.circularCount, this.ledEdge.Height), new System.Drawing.Rectangle(this.EdgeBitmap.Width - (int)this.circularCount, 0, (int)this.circularCount, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
				}
				bitmap = this.SetCornerAlpha(bitmap);
				this.SplitBitmapToItemEdge(bitmap);
				if (this.ledEdge.Mode == LedEdgeMode.Clockwise)
				{
					this.circularCount += this.ledEdge.Speed / 5m;
					return;
				}
				this.circularCount -= this.ledEdge.Speed / 5m;
				return;
			}
			case LedEdgeMode.Blink:
				this.flashCount += this.ledEdge.Speed;
				if (this.flashCount >= 200)
				{
					this.flashCount = 0;
				}
				if (this.flashCount >= 100)
				{
					this.itemFlashBlack = false;
					return;
				}
				this.itemFlashBlack = true;
				break;
			default:
				return;
			}
		}

		public System.Drawing.Bitmap GetNew()
		{
			System.Drawing.Bitmap result;
			lock (this.StaticBitmap)
			{
				lock (this.ledSub.LastDrawn)
				{
					try
					{
						System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(this.ledEdge.Height, this.ledEdge.Height, this.subSize.Width - this.ledEdge.Height * 2, this.subSize.Height - this.ledEdge.Height * 2);
						System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(this.ledEdge.Height, this.ledEdge.Height, this.subSize.Width - this.ledEdge.Height * 2, this.subSize.Height - this.ledEdge.Height * 2);
						switch (this.ledEdge.Mode)
						{
						case LedEdgeMode.Static:
						{
							System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.StaticBitmap);
							System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
							graphics.DrawImage(this.ledSub.LastDrawn, destRect, srcRect, System.Drawing.GraphicsUnit.Pixel);
							result = bitmap;
							return result;
						}
						case LedEdgeMode.Clockwise:
						case LedEdgeMode.CounterClockwise:
						{
							if (this.circularCount >= this.circular)
							{
								this.circularCount = 0m;
							}
							if (this.circularCount <= -1m)
							{
								this.circularCount = this.circular;
							}
							System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(this.AlphaBitmap);
							System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap2);
							graphics2.Clear(System.Drawing.Color.Green);
							if (this.circularCount == 0m)
							{
								graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Point(0, 0));
							}
							else
							{
								graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Point((int)this.circularCount, 0));
								graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Rectangle(0, 0, (int)this.circularCount, this.ledEdge.Height), new System.Drawing.Rectangle(this.EdgeBitmap.Width - (int)this.circularCount, 0, (int)this.circularCount, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
							}
							bitmap2 = this.SetCornerAlpha(bitmap2);
							System.Drawing.Bitmap bitmap3 = new System.Drawing.Bitmap(this.subSize.Width, this.subSize.Height);
							System.Drawing.Graphics graphics3 = System.Drawing.Graphics.FromImage(bitmap3);
							graphics3.DrawImage(bitmap2, new System.Drawing.Point(0, 0));
							graphics3.TranslateTransform(0f, 0f);
							graphics3.RotateTransform(90f);
							graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(0, -this.subSize.Width, this.subSize.Height, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width, -1, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
							graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(0, -this.subSize.Width, this.subSize.Height, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width, 0, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
							graphics3.RotateTransform(90f);
							graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(-this.subSize.Width, -this.subSize.Height, this.subSize.Width, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width + this.subSize.Height, -1, this.subSize.Width, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
							graphics3.RotateTransform(90f);
							graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(-this.subSize.Height, 0, this.subSize.Height, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width * 2 + this.subSize.Height, 0, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
							graphics3.RotateTransform(90f);
							graphics3.DrawImage(this.ledSub.LastDrawn, destRect, srcRect, System.Drawing.GraphicsUnit.Pixel);
							this.circularCount -= this.ledEdge.Speed / 5m;
							result = bitmap3;
							return result;
						}
						case LedEdgeMode.Blink:
						{
							this.flashCount += this.ledEdge.Speed;
							if (this.flashCount >= 200)
							{
								this.flashCount = 0;
							}
							if (this.flashCount >= 100)
							{
								System.Drawing.Bitmap bitmap4 = new System.Drawing.Bitmap(this.StaticBitmap);
								System.Drawing.Graphics graphics4 = System.Drawing.Graphics.FromImage(bitmap4);
								graphics4.DrawImage(this.ledSub.LastDrawn, destRect, srcRect, System.Drawing.GraphicsUnit.Pixel);
								result = bitmap4;
								return result;
							}
							System.Drawing.Bitmap bitmap5 = new System.Drawing.Bitmap(this.StaticBitmap.Width, this.StaticBitmap.Height);
							System.Drawing.Graphics graphics5 = System.Drawing.Graphics.FromImage(bitmap5);
							graphics5.DrawImage(this.ledSub.LastDrawn, destRect, srcRect, System.Drawing.GraphicsUnit.Pixel);
							result = bitmap5;
							return result;
						}
						}
					}
					catch
					{
					}
					result = null;
				}
			}
			return result;
		}

		public System.Drawing.Bitmap GetNew(System.Drawing.Bitmap pMarqueeData)
		{
			System.Drawing.Bitmap result;
			lock (this.StaticBitmap)
			{
				lock (this.ledSub.LastDrawn)
				{
					switch (this.ledEdge.Mode)
					{
					case LedEdgeMode.Static:
					{
						System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.StaticBitmap);
						System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
						graphics.DrawImage(pMarqueeData, new System.Drawing.Point(this.ledEdge.Height, this.ledEdge.Height));
						result = bitmap;
						break;
					}
					case LedEdgeMode.Clockwise:
					case LedEdgeMode.CounterClockwise:
					{
						if (this.circularCount >= this.circular)
						{
							this.circularCount = 0m;
						}
						if (this.circularCount <= -1m)
						{
							this.circularCount = this.circular;
						}
						System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(this.AlphaBitmap);
						System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap2);
						graphics2.Clear(System.Drawing.Color.Green);
						if (this.circularCount == 0m)
						{
							graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Point(0, 0));
						}
						else
						{
							graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Point((int)this.circularCount, 0));
							graphics2.DrawImage(this.EdgeBitmap, new System.Drawing.Rectangle(0, 0, (int)this.circularCount, this.ledEdge.Height), new System.Drawing.Rectangle(this.EdgeBitmap.Width - (int)this.circularCount, 0, (int)this.circularCount, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
						}
						bitmap2 = this.SetCornerAlpha(bitmap2);
						System.Drawing.Bitmap bitmap3 = new System.Drawing.Bitmap(this.subSize.Width, this.subSize.Height);
						System.Drawing.Graphics graphics3 = System.Drawing.Graphics.FromImage(bitmap3);
						graphics3.DrawImage(bitmap2, new System.Drawing.Point(0, 0));
						graphics3.TranslateTransform(0f, 0f);
						graphics3.RotateTransform(90f);
						graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(0, -this.subSize.Width, this.subSize.Height, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width, -1, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
						graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(0, -this.subSize.Width, this.subSize.Height, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width, 0, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
						graphics3.RotateTransform(90f);
						graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(-this.subSize.Width, -this.subSize.Height, this.subSize.Width, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width + this.subSize.Height, -1, this.subSize.Width, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
						graphics3.RotateTransform(90f);
						graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(-this.subSize.Height, 0, this.subSize.Height, this.ledEdge.Height), new System.Drawing.Rectangle(this.subSize.Width * 2 + this.subSize.Height, 0, this.subSize.Height, this.ledEdge.Height), System.Drawing.GraphicsUnit.Pixel);
						graphics3.RotateTransform(90f);
						lock (this.ledSub.LastDrawn)
						{
							lock (this.ledEdge)
							{
								graphics3.DrawImage(pMarqueeData, new System.Drawing.Point(this.ledEdge.Height, this.ledEdge.Height));
							}
						}
						this.circularCount -= this.ledEdge.Speed / 5m;
						result = bitmap3;
						break;
					}
					case LedEdgeMode.Blink:
						this.flashCount += this.ledEdge.Speed;
						if (this.flashCount >= 200)
						{
							this.flashCount = 0;
						}
						if (this.flashCount >= 100)
						{
							System.Drawing.Bitmap bitmap4 = new System.Drawing.Bitmap(this.StaticBitmap);
							System.Drawing.Graphics graphics4 = System.Drawing.Graphics.FromImage(bitmap4);
							graphics4.DrawImage(pMarqueeData, new System.Drawing.Point(this.ledEdge.Height, this.ledEdge.Height));
							result = bitmap4;
						}
						else
						{
							System.Drawing.Bitmap bitmap5 = new System.Drawing.Bitmap(this.StaticBitmap.Width, this.StaticBitmap.Height);
							System.Drawing.Graphics graphics5 = System.Drawing.Graphics.FromImage(bitmap5);
							graphics5.DrawImage(pMarqueeData, new System.Drawing.Point(this.ledEdge.Height, this.ledEdge.Height));
							result = bitmap5;
						}
						break;
					default:
						result = null;
						break;
					}
				}
			}
			return result;
		}

		public static System.Drawing.Bitmap SetAlaph(System.Drawing.Bitmap pBit)
		{
			System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, pBit.Width, pBit.Height);
			System.Drawing.Imaging.BitmapData bitmapData = pBit.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			IntPtr scan = bitmapData.Scan0;
			int num = pBit.Width * pBit.Height * 4;
			byte[] array = new byte[num];
			Marshal.Copy(scan, array, 0, num);
			for (int i = 0; i < num; i += 4)
			{
				array[i + 3] = 0;
			}
			Marshal.Copy(array, 0, scan, num);
			pBit.UnlockBits(bitmapData);
			return pBit;
		}

		public System.Drawing.Bitmap SetCornerAlpha(System.Drawing.Bitmap pBit)
		{
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(pBit);
			new System.Drawing.SolidBrush(EdgeDisplay.alphaColor);
			System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
			System.Drawing.PointF[] array = new System.Drawing.PointF[4];
			array[0] = new System.Drawing.PointF(0f, 0f);
			array[1] = new System.Drawing.PointF(0f, (float)pBit.Height);
			array[2] = new System.Drawing.PointF((float)pBit.Height, (float)pBit.Height);
			graphicsPath.ClearMarkers();
			graphicsPath.AddPolygon(array);
			graphics.SetClip(graphicsPath);
			graphics.Clear(System.Drawing.Color.Transparent);
			array[0] = new System.Drawing.PointF((float)this.subSize.Width, 0f);
			array[1] = new System.Drawing.PointF((float)(this.subSize.Width - pBit.Height), (float)pBit.Height);
			array[2] = new System.Drawing.PointF((float)(this.subSize.Width + pBit.Height + 1), (float)pBit.Height);
			array[3] = new System.Drawing.PointF((float)(this.subSize.Width + 1), 0f);
			graphicsPath.ClearMarkers();
			graphicsPath.AddPolygon(array);
			graphics.SetClip(graphicsPath);
			graphics.Clear(System.Drawing.Color.Transparent);
			array[0] = new System.Drawing.PointF((float)(this.subSize.Width + this.subSize.Height), 0f);
			array[1] = new System.Drawing.PointF((float)(this.subSize.Width + this.subSize.Height - pBit.Height), (float)pBit.Height);
			array[2] = new System.Drawing.PointF((float)(this.subSize.Width + this.subSize.Height + pBit.Height + 1), (float)pBit.Height);
			array[3] = new System.Drawing.PointF((float)(this.subSize.Width + this.subSize.Height + 1), 0f);
			graphicsPath.ClearMarkers();
			graphicsPath.AddPolygon(array);
			graphics.SetClip(graphicsPath);
			graphics.Clear(System.Drawing.Color.Transparent);
			array[0] = new System.Drawing.PointF((float)(this.subSize.Width * 2 + this.subSize.Height), 0f);
			array[1] = new System.Drawing.PointF((float)(this.subSize.Width * 2 + this.subSize.Height - pBit.Height), (float)pBit.Height);
			array[2] = new System.Drawing.PointF((float)(this.subSize.Width * 2 + this.subSize.Height + pBit.Height + 1), (float)pBit.Height);
			array[3] = new System.Drawing.PointF((float)(this.subSize.Width * 2 + this.subSize.Height + 1), 0f);
			graphicsPath.ClearMarkers();
			graphicsPath.AddPolygon(array);
			graphics.SetClip(graphicsPath);
			graphics.Clear(System.Drawing.Color.Transparent);
			array[0] = new System.Drawing.PointF((float)(this.subSize.Width * 2 + this.subSize.Height * 2), 0f);
			array[1] = new System.Drawing.PointF((float)(this.subSize.Width * 2 + this.subSize.Height * 2 - pBit.Height), (float)pBit.Height);
			array[2] = new System.Drawing.PointF((float)(this.subSize.Width * 2 + this.subSize.Height * 2 + pBit.Height + 1), (float)pBit.Height);
			array[3] = new System.Drawing.PointF((float)(this.subSize.Width * 2 + this.subSize.Height * 2 + 1), 0f);
			graphicsPath.ClearMarkers();
			graphicsPath.AddPolygon(array);
			graphics.SetClip(graphicsPath);
			graphics.Clear(System.Drawing.Color.Transparent);
			return pBit;
		}
	}
}
