using LedModel.Content;
using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplayTotal : MarqueeDisplay
	{
		private MarqueeDisplayPageBase nowDisplay;

		private System.Drawing.Bitmap oldBitmap;

		private System.Drawing.Bitmap newBitmap;

		private int nowIndex;

		public MarqueeDisplayTotal(LedPictureText pContent)
		{
			this.content = pContent;
		}

		public override System.Drawing.Bitmap getNew()
		{
			LedElement ledElement = this.content.Elements[0];
			System.Drawing.Size size = this.content.GetSize();
			if (this.nowDisplay == null)
			{
				this.newBitmap = LedGraphics.GetBmpFromFile(size, this.nowIndex, this.content.GetFileFullPath());
				this.nowDisplay = base.GetMarqueeDisplayPageInstance(this.content.EffectsSetting, null, this.newBitmap);
				this.nowIndex++;
			}
			System.Drawing.Bitmap next = this.nowDisplay.GetNext();
			if (next != null)
			{
				return next;
			}
			if (this.nowIndex >= ledElement.PageCount)
			{
				this.nowIndex = 0;
				try
				{
					this.newBitmap.Dispose();
					this.oldBitmap.Dispose();
				}
				catch
				{
				}
				return null;
			}
			if (this.content.EffectsSetting.ExitMode == 0)
			{
				if (this.oldBitmap != null)
				{
					this.oldBitmap.Dispose();
				}
				this.oldBitmap = (System.Drawing.Bitmap)this.newBitmap.Clone();
				this.newBitmap = LedGraphics.GetBmpFromFile(size, this.nowIndex, this.content.GetFileFullPath());
				this.nowDisplay = base.GetMarqueeDisplayPageInstance(this.content.EffectsSetting, this.oldBitmap, this.newBitmap);
			}
			else
			{
				this.newBitmap = LedGraphics.GetBmpFromFile(size, this.nowIndex, this.content.GetFileFullPath());
				this.nowDisplay = base.GetMarqueeDisplayPageInstance(this.content.EffectsSetting, null, this.newBitmap);
			}
			this.nowIndex++;
			return this.nowDisplay.GetNext();
		}
	}
}
