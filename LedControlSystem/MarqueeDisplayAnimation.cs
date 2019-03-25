using LedModel.Content;
using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public class MarqueeDisplayAnimation : MarqueeDisplay
	{
		public MarqueeDisplayAnimation(LedPictureText pContent)
		{
			this.content = pContent;
			if (this.content.Elements != null && this.content.Elements.Count > 0 && this.content.Elements[0].PageCount == 0)
			{
				return;
			}
			this.nowLoopNum = 0;
		}

		public override System.Drawing.Bitmap getNew()
		{
			if (this.animationNowIndex >= this.content.Elements[0].PageCount)
			{
				this.nowLoopNum++;
				this.animationNowIndex = 0;
			}
			if (this.nowLoopNum > this.content.EffectsSetting.LoopCount)
			{
				return null;
			}
			System.Drawing.Bitmap bmpFromFile = LedGraphics.GetBmpFromFile(this.content.GetSize(), this.animationNowIndex, this.content.GetFileFullPath());
			this.animationNowIndex++;
			return bmpFromFile;
		}
	}
}
