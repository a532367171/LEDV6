using LedModel.Content;
using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplay133 : MarqueeDisplay
	{
		private float positionMark;

		public MarqueeDisplay133(LedPictureText pContent)
		{
			this.content = pContent;
			LedElement ledElement = this.content.Elements[0];
			System.Drawing.Size size = this.content.GetSize();
			this.nowLoopNum = 1;
			this.nowPositionF = (float)(-(float)size.Height);
			int num;
			if (ledElement.PageCount >= 3)
			{
				num = 3;
				ledElement.PageNumber = 3;
			}
			else
			{
				num = ledElement.PageCount;
				ledElement.PageNumber = ledElement.PageCount;
			}
			this.AllBit = new System.Drawing.Bitmap(size.Width, size.Height * num);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(this.AllBit);
			int num2 = size.Height * (num - 1);
			for (int i = 0; i < num; i++)
			{
				System.Drawing.Bitmap bmpFromFile = LedGraphics.GetBmpFromFile(size, ledElement.PageCount - i - 1, this.content.GetFileFullPath());
				graphics.DrawImage(bmpFromFile, new System.Drawing.Point(0, num2));
				num2 -= bmpFromFile.Height;
				bmpFromFile.Dispose();
			}
			this.nowPositionF = (float)this.AllBit.Height;
		}

		public override System.Drawing.Bitmap getNew()
		{
			System.Drawing.Bitmap result;
			lock (this.AllBit)
			{
				LedElement ledElement = this.content.Elements[0];
				System.Drawing.Size size = this.content.GetSize();
				int width = size.Width;
				int height = size.Height;
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height);
				System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
				this.nowPosition = (int)this.nowPositionF;
				if (this.nowPositionF < (float)(-(float)size.Height))
				{
					this.nowPositionF = (float)this.AllBit.Height;
				}
				if (this.nowPosition <= 0)
				{
					this.nowPosition = 0;
					this.nowPositionF = 0f;
					this.nowLoopNum++;
					if (this.nowLoopNum > this.content.EffectsSetting.LoopCount)
					{
						result = null;
						return result;
					}
				}
				if (this.AllBit.Height - this.nowPosition < height)
				{
					graphics.DrawImage(this.AllBit, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(0, this.nowPosition, width, height), System.Drawing.GraphicsUnit.Pixel);
					if (this.nowLoopNum < this.content.EffectsSetting.LoopCount)
					{
						graphics.DrawImage(this.AllBit, new System.Drawing.Point(0, this.AllBit.Height - this.nowPosition));
					}
				}
				else
				{
					graphics.DrawImage(this.AllBit, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), new System.Drawing.Rectangle(0, this.nowPosition, width, height), System.Drawing.GraphicsUnit.Pixel);
				}
				if (this.positionMark >= (float)(size.Height * 2) && ledElement.PageNumber < ledElement.PageCount)
				{
					this.nowPositionF += (float)size.Height;
					this.positionMark -= (float)size.Height;
					System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(this.AllBit);
					System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(size.Width, size.Height * 2);
					System.Drawing.Graphics graphics3 = System.Drawing.Graphics.FromImage(bitmap2);
					graphics3.DrawImage(this.AllBit, new System.Drawing.Rectangle(0, 0, bitmap2.Width, bitmap2.Height), new System.Drawing.Rectangle(0, 0, bitmap2.Width, bitmap2.Height), System.Drawing.GraphicsUnit.Pixel);
					graphics3.Dispose();
					System.Drawing.Bitmap bmpFromFile = LedGraphics.GetBmpFromFile(size, ledElement.PageCount - ledElement.PageNumber - 1, this.content.GetFileFullPath());
					graphics2.DrawImage(bitmap2, new System.Drawing.Point(0, size.Height));
					graphics2.DrawImage(bmpFromFile, new System.Drawing.Point(0, 0));
					bitmap2.Dispose();
					bmpFromFile.Dispose();
					ledElement.PageNumber++;
				}
				this.nowPositionF -= MarqueeDisplay.speedEntryBase / (float)this.content.EffectsSetting.EntrySpeed;
				this.positionMark += MarqueeDisplay.speedEntryBase / (float)this.content.EffectsSetting.EntrySpeed;
				result = (System.Drawing.Bitmap)bitmap.Clone();
			}
			return result;
		}
	}
}
