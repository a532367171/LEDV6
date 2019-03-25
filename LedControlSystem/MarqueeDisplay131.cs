using LedModel.Content;
using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplay131 : MarqueeDisplay
	{
		private float positionMark;

		private int pageCount;

		private int pageNumber;

		public MarqueeDisplay131(LedPictureText pContent)
		{
			this.content = pContent;
			LedElement ledElement = this.content.Elements[0];
			System.Drawing.Size size = this.content.GetSize();
			this.pageCount = ledElement.PageCount;
			this.nowLoopNum = 1;
			int num;
			if (this.pageCount >= 3)
			{
				num = 3;
				this.pageNumber = 3;
			}
			else
			{
				num = this.pageCount;
				this.pageNumber = this.pageCount;
			}
			int num2 = size.Width * num;
			bool flag = false;
			if (num2 > pContent.EffectiveLength)
			{
				num2 = pContent.EffectiveLength;
			}
			if (pContent.EffectiveLength < size.Width)
			{
				num2 = (size.Width / pContent.EffectiveLength + 1) * num2;
				flag = true;
			}
			this.AllBit = new System.Drawing.Bitmap(num2, size.Height);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(this.AllBit);
			int num3 = size.Width * (num - 1);
			for (int i = 0; i < num; i++)
			{
				System.Drawing.Bitmap bmpFromFile = LedGraphics.GetBmpFromFile(size, this.pageCount - 1 - i, this.content.GetFileFullPath());
				if (i == 0)
				{
					if (flag)
					{
						for (int j = 0; j < num2; j += pContent.EffectiveLength)
						{
							graphics.DrawImage(bmpFromFile, new System.Drawing.Rectangle(j, 0, pContent.EffectiveLength, size.Height), new System.Drawing.Rectangle(0, 0, pContent.EffectiveLength, size.Height), System.Drawing.GraphicsUnit.Pixel);
						}
					}
					else
					{
						graphics.DrawImage(bmpFromFile, new System.Drawing.Point(num3, 0));
					}
				}
				else
				{
					graphics.DrawImage(bmpFromFile, new System.Drawing.Point(num3, 0));
				}
				num3 -= bmpFromFile.Width;
				bmpFromFile.Dispose();
			}
			this.nowPositionF = (float)this.AllBit.Width;
		}

		public override System.Drawing.Bitmap getNew()
		{
			LedElement arg_11_0 = this.content.Elements[0];
			System.Drawing.Size size = this.content.GetSize();
			int width = size.Width;
			int height = size.Height;
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
			this.nowPosition = (int)this.nowPositionF;
			try
			{
				if (this.nowPosition + width <= 0)
				{
					this.nowPosition = 0;
					this.nowPositionF = 0f;
					this.nowLoopNum++;
					this.nowPositionF = (float)this.AllBit.Width;
					if (this.nowLoopNum > this.content.EffectsSetting.LoopCount)
					{
						return null;
					}
				}
				if (this.nowPosition < 0 && this.nowLoopNum < this.content.EffectsSetting.LoopCount)
				{
					graphics.DrawImage(this.AllBit, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(this.AllBit.Width + this.nowPosition, 0, width, height), System.Drawing.GraphicsUnit.Pixel);
				}
				if (this.nowPosition < 0)
				{
					graphics.DrawImage(this.AllBit, new System.Drawing.Rectangle(0, 0, Math.Abs(this.nowPosition), bitmap.Height), new System.Drawing.Rectangle(this.AllBit.Width + this.nowPosition, 0, Math.Abs(this.nowPosition), height), System.Drawing.GraphicsUnit.Pixel);
				}
				graphics.DrawImage(this.AllBit, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), new System.Drawing.Rectangle(this.nowPosition, 0, width, height), System.Drawing.GraphicsUnit.Pixel);
				if (this.positionMark > (float)(size.Width * 2) && this.pageNumber < this.pageCount)
				{
					this.nowPositionF += (float)size.Width;
					this.positionMark -= (float)size.Width;
					System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(size.Width * 2, size.Height);
					System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap2);
					graphics2.DrawImage(this.AllBit, new System.Drawing.Point(0, 0));
					graphics2.Dispose();
					graphics2 = System.Drawing.Graphics.FromImage(this.AllBit);
					graphics2.DrawImage(bitmap2, new System.Drawing.Point(size.Width, 0));
					bitmap2.Dispose();
					bitmap2 = LedGraphics.GetBmpFromFile(size, this.pageCount - this.pageNumber - 1, this.content.GetFileFullPath());
					this.pageNumber++;
					graphics2.DrawImage(bitmap2, new System.Drawing.Point(0, 0));
					graphics2.Dispose();
				}
				this.nowPositionF -= MarqueeDisplay.speedEntryBase / (float)this.content.EffectsSetting.EntrySpeed;
				this.positionMark += MarqueeDisplay.speedEntryBase / (float)this.content.EffectsSetting.EntrySpeed;
			}
			catch
			{
			}
			return bitmap;
		}
	}
}
