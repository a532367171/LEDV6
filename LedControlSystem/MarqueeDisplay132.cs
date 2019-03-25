using LedModel.Content;
using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplay132 : MarqueeDisplay
	{
		private System.Drawing.Bitmap previousBitmap;

		private System.Drawing.Bitmap thisBitmap;

		public MarqueeDisplay132(LedPictureText pContent)
		{
			this.content = pContent;
			LedElement ledElement = this.content.Elements[0];
			System.Drawing.Size size = this.content.GetSize();
			this.nowLoopNum = 1;
			this.nowPositionF = 0f;
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
				ledElement.PageNumber = ledElement.PageCount;
			}
			this.AllBit = new System.Drawing.Bitmap(size.Width, size.Height * num2);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(this.AllBit);
			for (int i = 0; i < num2; i++)
			{
				System.Drawing.Bitmap bmpFromFile = LedGraphics.GetBmpFromFile(size, i, this.content.GetFileFullPath());
				graphics.DrawImage(bmpFromFile, new System.Drawing.Point(0, num));
				num += bmpFromFile.Height;
				bmpFromFile.Dispose();
			}
			this.thisBitmap = LedGraphics.GetBmpFromFile(size, 0, this.content.GetFileFullPath());
			ledElement.PageNumber = 1;
		}

		public override System.Drawing.Bitmap getNew()
		{
			LedElement ledElement = this.content.Elements[0];
			System.Drawing.Size size = this.content.GetSize();
			int width = size.Width;
			int height = size.Height;
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
			int num = (int)this.nowPositionF;
			graphics.DrawImage(this.thisBitmap, new System.Drawing.Point(0, height - num));
			if (num < height && this.previousBitmap != null)
			{
				graphics.DrawImage(this.previousBitmap, new System.Drawing.Point(0, -(height - (height - num))));
			}
			this.nowPositionF += MarqueeDisplay.speedEntryBase / (float)this.content.EffectsSetting.EntrySpeed;
			if (this.nowPositionF >= (float)height)
			{
				this.nowPositionF -= (float)height;
				if (this.previousBitmap != null)
				{
					this.previousBitmap.Dispose();
				}
				this.previousBitmap = this.thisBitmap;
				this.thisBitmap = LedGraphics.GetBmpFromFile(size, ledElement.PageNumber, this.content.GetFileFullPath());
				ledElement.PageNumber++;
				if (ledElement.PageNumber >= ledElement.PageCount)
				{
					ledElement.PageNumber = 0;
					System.Drawing.Bitmap image = new System.Drawing.Bitmap(width, height);
					System.Drawing.Bitmap image2 = new System.Drawing.Bitmap(width, height);
					int num2 = this.content.EffectiveLength % height;
					System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(image2);
					graphics2.DrawImage(this.thisBitmap, new System.Drawing.Point(0, height - num2));
					graphics2.DrawImage(this.previousBitmap, new System.Drawing.Point(0, -num2));
					graphics2 = System.Drawing.Graphics.FromImage(image);
					graphics2.DrawImage(this.previousBitmap, new System.Drawing.Point(0, height - num2));
					this.previousBitmap.Dispose();
					this.thisBitmap.Dispose();
					this.previousBitmap = image;
					this.thisBitmap = image2;
					this.nowPositionF = this.nowPositionF + (float)height - (float)num2;
				}
			}
			return bitmap;
		}
	}
}
