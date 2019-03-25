using LedModel.Content;
using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplay130 : MarqueeDisplay
	{
		public MarqueeDisplay130(LedPictureText pContent)
		{
			System.Drawing.Size size = pContent.GetSize();
			this.content = pContent;
			this.nowLoopNum = 1;
			this.nowPositionF = (float)(-(float)size.Width);
			LedElement ledElement = pContent.Elements[0];
			int num = 0;
			int num2;
			if (ledElement.PageCount >= 3)
			{
				num2 = 3;
				ledElement.PageNumber = 3;
			}
			else
			{
				num2 = ledElement.PageCount;
				ledElement.PageNumber = ledElement.PageCount - 1;
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
			LedElement ledElement = this.content.Elements[0];
			System.Drawing.Bitmap result;
			lock (this.AllBit)
			{
				if (this.nowPosition >= this.AllBit.Width)
				{
					this.nowPositionF = (float)(-(float)size.Width);
				}
				int width = size.Width;
				int height = size.Height;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				this.nowPosition = (int)this.nowPositionF;
				if (this.nowPosition >= this.AllBit.Width)
				{
					this.nowPosition = 0;
					this.nowPositionF = 0f;
					this.nowLoopNum++;
					if (this.NeedChangeContent)
					{
						result = null;
						return result;
					}
				}
				if (this.AllBit.Width - this.nowPosition < width)
				{
					graphics.DrawImage(this.AllBit, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(this.nowPosition, 0, width, height), System.Drawing.GraphicsUnit.Pixel);
					graphics.DrawImage(this.AllBit, new System.Drawing.Rectangle(width - (width - (this.AllBit.Width - this.nowPosition)), 0, width, height), new System.Drawing.Rectangle(0, 0, width, height), System.Drawing.GraphicsUnit.Pixel);
					if (this.nowLoopNum < this.content.EffectsSetting.LoopCount)
					{
						graphics.DrawImage(this.AllBit, new System.Drawing.Point(this.AllBit.Width - this.nowPosition, 0));
					}
				}
				else
				{
					graphics.DrawImage(this.AllBit, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), new System.Drawing.Rectangle(this.nowPosition, 0, width, height), System.Drawing.GraphicsUnit.Pixel);
				}
				this.nowPositionF += MarqueeDisplay.speedEntryBase / (float)this.content.EffectsSetting.EntrySpeed;
				if (this.nowPosition >= size.Width && ledElement.PageNumber < ledElement.PageCount)
				{
					this.nowPositionF -= (float)size.Width;
					System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(this.AllBit);
					System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(size.Width * 2, size.Height);
					System.Drawing.Graphics graphics3 = System.Drawing.Graphics.FromImage(bitmap2);
					graphics3.DrawImage(this.AllBit, new System.Drawing.Rectangle(0, 0, bitmap2.Width, bitmap2.Height), new System.Drawing.Rectangle(size.Width, 0, size.Width * 2, size.Height), System.Drawing.GraphicsUnit.Pixel);
					graphics3.Dispose();
					System.Drawing.Bitmap bmpFromFile = LedGraphics.GetBmpFromFile(size, ledElement.PageNumber, this.content.GetFileFullPath());
					graphics2.DrawImage(bitmap2, new System.Drawing.Point(0, 0));
					graphics2.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.Rectangle(bitmap2.Width, 0, bmpFromFile.Width, bmpFromFile.Height));
					graphics2.DrawImage(bmpFromFile, new System.Drawing.Point(bitmap2.Width, 0));
					bitmap2.Dispose();
					bmpFromFile.Dispose();
					ledElement.PageNumber++;
				}
				result = (System.Drawing.Bitmap)bitmap.Clone();
			}
			return result;
		}
	}
}
