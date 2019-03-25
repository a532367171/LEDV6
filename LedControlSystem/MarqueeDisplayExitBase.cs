using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public abstract class MarqueeDisplayExitBase
	{
		protected LedEffectsSetting effect;

		protected System.Drawing.Bitmap newBitmap;

		protected System.Drawing.Bitmap now;

		protected float step;

		protected float nowPositionF;

		protected int nowPosition;

		protected int StayNum;

		protected MarqueeDisplayState nowState;

		public abstract System.Drawing.Bitmap GetNext();
	}
}
