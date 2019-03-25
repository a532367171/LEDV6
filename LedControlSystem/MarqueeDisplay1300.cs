using LedModel.Content;
using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplay1300 : MarqueeDisplay
	{
		private bool drawHead;

		public MarqueeDisplay1300(LedPictureText pContent)
		{
			this.content = pContent;
			LedElement ledElement = this.content.Elements[0];
			System.Drawing.Size size = this.content.GetSize();
			this.nowLoopNum = 1;
			int num = 0;
			int num2;
			if (ledElement.PageCount >= 3)
			{
				num2 = 3;
			}
			else
			{
				num2 = ledElement.PageCount;
			}
			int num3 = size.Width * num2;
			bool flag = false;
			int num4 = pContent.EffectiveLength;
			if (num3 > pContent.EffectiveLength)
			{
				num3 = pContent.EffectiveLength;
			}
			if (pContent.EffectiveLength < size.Width)
			{
				if (pContent.EffectiveLength == 0)
				{
					num3 = size.Width;
					num4 = size.Width;
				}
				else
				{
					num3 = (size.Width / pContent.EffectiveLength + 1) * num3;
					num4 = pContent.EffectiveLength;
				}
				flag = true;
			}
			this.AllBit = new System.Drawing.Bitmap(num3, size.Height);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(this.AllBit);
			for (int i = 0; i < num2; i++)
			{
				System.Drawing.Bitmap bmpFromFile = LedGraphics.GetBmpFromFile(size, i, this.content.GetFileFullPath());
				if (flag)
				{
					for (int j = 0; j < num3; j += num4)
					{
						graphics.DrawImage(bmpFromFile, new System.Drawing.Rectangle(j, 0, num4, size.Height), new System.Drawing.Rectangle(0, 0, num4, size.Height), System.Drawing.GraphicsUnit.Pixel);
					}
				}
				else
				{
					graphics.DrawImage(bmpFromFile, new System.Drawing.Point(num, 0));
					num += bmpFromFile.Width;
					bmpFromFile.Dispose();
				}
			}
		}

		public override System.Drawing.Bitmap getNew()
		{
			System.Drawing.Size size = this.content.GetSize();
			int width = size.Width;
			int height = size.Height;
			if (this.content.EffectiveLength <= width)
			{
				System.Drawing.Bitmap bmpFromFile = LedGraphics.GetBmpFromFile(size, 0, this.content.GetFileFullPath());
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.content.EffectiveLength, height);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				graphics.DrawImage(bmpFromFile, new System.Drawing.Point(0, 0));
				int i;
				for (i = 0; i <= width; i += bitmap.Width)
				{
				}
				System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(i, height);
				System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap2);
				for (int j = 0; j < i; j += bitmap.Width)
				{
					graphics2.DrawImage(bitmap, new System.Drawing.Point(j, 0));
				}
				System.Drawing.Bitmap bitmap3 = new System.Drawing.Bitmap(width, height);
				System.Drawing.Graphics graphics3 = System.Drawing.Graphics.FromImage(bitmap3);
				int num = (int)this.nowPositionF;
				int x = 0;
				if (num > width)
				{
					x = num - width;
					num = width;
				}
				int x2 = width - num;
				graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(x2, 0, num, height), new System.Drawing.Rectangle(x, 0, num, height), System.Drawing.GraphicsUnit.Pixel);
				if (this.drawHead)
				{
					graphics3.DrawImage(bitmap2, new System.Drawing.Rectangle(0, 0, width - num, height), new System.Drawing.Rectangle(bitmap2.Width - (width - num), 0, width - num, height), System.Drawing.GraphicsUnit.Pixel);
				}
				if (this.nowPositionF > (float)bitmap2.Width)
				{
					this.nowPositionF -= (float)bitmap2.Width;
					this.drawHead = true;
				}
				this.nowPositionF += MarqueeDisplay.speedEntryBase / (float)this.content.EffectsSetting.EntrySpeed;
				bmpFromFile.Dispose();
				bitmap.Dispose();
				bitmap2.Dispose();
				return bitmap3;
			}
			int num2 = (int)this.nowPositionF;
			if (num2 == 0)
			{
				num2 = 1;
			}
			if (num2 > width)
			{
				num2 = width;
			}
			int num3 = (int)this.nowPositionF;
			if (num3 < width)
			{
				num3 = 0;
			}
			else
			{
				num3 -= width;
			}
			if (this.nowPositionF > (float)this.content.EffectiveLength)
			{
				this.nowPositionF -= (float)this.content.EffectiveLength;
				this.drawHead = true;
			}
			this.nowPositionF += MarqueeDisplay.speedEntryBase / (float)this.content.EffectsSetting.EntrySpeed;
			System.Drawing.Bitmap bmpFromFile2 = LedGraphics.GetBmpFromFile(num2, height, num3, this.content.GetFileFullPath());
			if (num2 == width)
			{
				return bmpFromFile2;
			}
			System.Drawing.Bitmap bitmap4 = new System.Drawing.Bitmap(width, height);
			System.Drawing.Graphics graphics4 = System.Drawing.Graphics.FromImage(bitmap4);
			graphics4.DrawImage(bmpFromFile2, new System.Drawing.Point(width - num2, 0));
			bmpFromFile2.Dispose();
			if (this.drawHead)
			{
				int num4 = width - num2;
				int offset = this.content.EffectiveLength - num4;
				System.Drawing.Bitmap bmpFromFile3 = LedGraphics.GetBmpFromFile(num4, height, offset, this.content.GetFileFullPath());
				graphics4.DrawImage(bmpFromFile3, new System.Drawing.Point(0, 0));
				bmpFromFile3.Dispose();
			}
			return bitmap4;
		}
	}
}
