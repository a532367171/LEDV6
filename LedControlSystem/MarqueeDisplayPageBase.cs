using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public abstract class MarqueeDisplayPageBase
	{
		protected LedEffectsSetting effect;

		protected System.Drawing.Bitmap oldBitmap;

		protected System.Drawing.Bitmap newBitmap;

		protected System.Drawing.Bitmap now;

		protected float step;

		protected float nowPositionF;

		protected int nowPosition;

		protected int StayNum;

		protected MarqueeDisplayState nowState;

		protected MarqueeDisplayExitBase Exit;

		public abstract System.Drawing.Bitmap GetNext();
	}
}
