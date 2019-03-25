using LedModel;
using LedModel.Const;
using LedModel.Content;
using LedModel.Foundation;
using System;
using System.Drawing;

namespace LedControlSystem
{
	public abstract class MarqueeDisplay
	{
		protected static float speedEntryBase = 10f;

		protected static float speedExitBase = 5f;

		protected LedPictureText content;

		protected int nowLoopNum = 1;

		public bool isEnd;

		protected System.Drawing.Bitmap AllBit;

		protected int nowPosition;

		protected float nowPositionF;

		public bool NeedChangeContent;

		protected int animationNowIndex;

		public abstract System.Drawing.Bitmap getNew();

		public static MarqueeDisplayExitBase getExtInstance(System.Drawing.Bitmap pExitBitmap, LedEffectsSetting pSetting)
		{
			if (pSetting.EntryMode == 0)
			{
				return new MarqueeExistPage1(pExitBitmap, pSetting);
			}
			int num = 100;
			if (pSetting.ExitMode == 1)
			{
				bool flag = true;
				Random random = new Random();
				while (flag)
				{
					try
					{
						num = (int)LedGlobal.ExitModeList[random.Next(0, LedGlobal.ExitModeList.Count)];
					}
					catch
					{
					}
					if (num != 0)
					{
						flag = false;
					}
				}
			}
			else
			{
				num = (int)LedCommonConst.ExitModeList[(int)pSetting.ExitMode];
			}
			MarqueeDisplayExitBase marqueeDisplayExitBase = null;
			switch (num)
			{
			case 2:
				marqueeDisplayExitBase = new MarqueeExistPage2(pExitBitmap, pSetting);
				break;
			case 3:
				marqueeDisplayExitBase = new MarqueeExistPage3(pExitBitmap, pSetting);
				break;
			case 4:
				marqueeDisplayExitBase = new MarqueeExistPage4(pExitBitmap, pSetting);
				break;
			case 5:
				marqueeDisplayExitBase = new MarqueeExistPage5(pExitBitmap, pSetting);
				break;
			case 6:
				marqueeDisplayExitBase = new MarqueeExistPage6(pExitBitmap, pSetting);
				break;
			case 7:
				marqueeDisplayExitBase = new MarqueeExistPage7(pExitBitmap, pSetting);
				break;
			case 8:
				marqueeDisplayExitBase = new MarqueeExistPage8(pExitBitmap, pSetting);
				break;
			case 9:
				marqueeDisplayExitBase = new MarqueeExistPage9(pExitBitmap, pSetting);
				break;
			case 10:
				marqueeDisplayExitBase = new MarqueeExistPage10(pExitBitmap, pSetting);
				break;
			case 11:
				marqueeDisplayExitBase = new MarqueeExistPage11(pExitBitmap, pSetting);
				break;
			case 12:
				marqueeDisplayExitBase = new MarqueeExistPage12(pExitBitmap, pSetting);
				break;
			case 13:
				marqueeDisplayExitBase = new MarqueeExistPage13(pExitBitmap, pSetting);
				break;
			case 14:
				marqueeDisplayExitBase = new MarqueeExistPage14(pExitBitmap, pSetting);
				break;
			case 15:
				marqueeDisplayExitBase = new MarqueeExistPage15(pExitBitmap, pSetting);
				break;
			case 16:
				marqueeDisplayExitBase = new MarqueeExistPage16(pExitBitmap, pSetting);
				break;
			case 17:
				marqueeDisplayExitBase = new MarqueeExistPage17(pExitBitmap, pSetting);
				break;
			case 18:
				marqueeDisplayExitBase = new MarqueeExistPage18(pExitBitmap, pSetting);
				break;
			case 19:
				marqueeDisplayExitBase = new MarqueeExistPage19(pExitBitmap, pSetting);
				break;
			case 20:
				marqueeDisplayExitBase = new MarqueeExistPage20(pExitBitmap, pSetting);
				break;
			case 21:
				marqueeDisplayExitBase = new MarqueeExistPage21(pExitBitmap, pSetting);
				break;
			case 22:
				marqueeDisplayExitBase = new MarqueeExistPage22(pExitBitmap, pSetting);
				break;
			case 23:
				marqueeDisplayExitBase = new MarqueeExistPage23(pExitBitmap, pSetting);
				break;
			case 24:
				marqueeDisplayExitBase = new MarqueeExistPage24(pExitBitmap, pSetting);
				break;
			case 25:
				marqueeDisplayExitBase = new MarqueeExistPage25(pExitBitmap, pSetting);
				break;
			case 26:
				marqueeDisplayExitBase = new MarqueeExistPage26(pExitBitmap, pSetting);
				break;
			case 27:
				marqueeDisplayExitBase = new MarqueeExistPage27(pExitBitmap, pSetting);
				break;
			case 28:
				marqueeDisplayExitBase = new MarqueeExistPage28(pExitBitmap, pSetting);
				break;
			case 29:
				marqueeDisplayExitBase = new MarqueeExistPage29(pExitBitmap, pSetting);
				break;
			case 30:
				marqueeDisplayExitBase = new MarqueeExistPage30(pExitBitmap, pSetting);
				break;
			case 31:
				marqueeDisplayExitBase = new MarqueeExistPage31(pExitBitmap, pSetting);
				break;
			case 32:
				marqueeDisplayExitBase = new MarqueeExistPage32(pExitBitmap, pSetting);
				break;
			case 33:
				marqueeDisplayExitBase = new MarqueeExistPage33(pExitBitmap, pSetting);
				break;
			case 34:
				marqueeDisplayExitBase = new MarqueeExistPage34(pExitBitmap, pSetting);
				break;
			case 35:
				marqueeDisplayExitBase = new MarqueeExistPage35(pExitBitmap, pSetting);
				break;
			case 36:
				marqueeDisplayExitBase = new MarqueeExistPage36(pExitBitmap, pSetting);
				break;
			case 37:
				marqueeDisplayExitBase = new MarqueeExistPage37(pExitBitmap, pSetting);
				break;
			case 38:
				marqueeDisplayExitBase = new MarqueeExistPage38(pExitBitmap, pSetting);
				break;
			case 39:
				marqueeDisplayExitBase = new MarqueeExistPage39(pExitBitmap, pSetting);
				break;
			case 40:
				marqueeDisplayExitBase = new MarqueeExistPage40(pExitBitmap, pSetting);
				break;
			case 41:
				marqueeDisplayExitBase = new MarqueeExistPage41(pExitBitmap, pSetting);
				break;
			}
			if (marqueeDisplayExitBase == null)
			{
				marqueeDisplayExitBase = new MarqueeExistPage40(pExitBitmap, pSetting);
			}
			return marqueeDisplayExitBase;
		}

		protected MarqueeDisplayPageBase GetMarqueeDisplayPageInstance(LedEffectsSetting pEffect, System.Drawing.Bitmap pOldBit, System.Drawing.Bitmap pNewBit)
		{
			int num = 100;
			if (pEffect.EntryMode == 1)
			{
				bool flag = true;
				Random random = new Random();
				while (flag)
				{
					try
					{
						num = (int)LedGlobal.EntryModeList[random.Next(0, LedGlobal.EntryModeList.Count)];
					}
					catch
					{
					}
					if (num < 100 && num != 1)
					{
						flag = false;
					}
				}
			}
			else
			{
				num = (int)LedGlobal.EntryModeList[(int)pEffect.EntryMode];
			}
			MarqueeDisplayPageBase marqueeDisplayPageBase = null;
			switch (num)
			{
			case 1:
				marqueeDisplayPageBase = new MarqueeDisplayPage1(pOldBit, pNewBit, pEffect);
				break;
			case 2:
				marqueeDisplayPageBase = new MarqueeDisplayPage2(pOldBit, pNewBit, pEffect);
				break;
			case 3:
				marqueeDisplayPageBase = new MarqueeDisplayPage3(pOldBit, pNewBit, pEffect);
				break;
			case 4:
				marqueeDisplayPageBase = new MarqueeDisplayPage4(pOldBit, pNewBit, pEffect);
				break;
			case 5:
				marqueeDisplayPageBase = new MarqueeDisplayPage5(pOldBit, pNewBit, pEffect);
				break;
			case 6:
				marqueeDisplayPageBase = new MarqueeDisplayPage6(pOldBit, pNewBit, pEffect);
				break;
			case 7:
				marqueeDisplayPageBase = new MarqueeDisplayPage7(pOldBit, pNewBit, pEffect);
				break;
			case 8:
				marqueeDisplayPageBase = new MarqueeDisplayPage8(pOldBit, pNewBit, pEffect);
				break;
			case 9:
				marqueeDisplayPageBase = new MarqueeDisplayPage9(pOldBit, pNewBit, pEffect);
				break;
			case 10:
				marqueeDisplayPageBase = new MarqueeDisplayPage10(pOldBit, pNewBit, pEffect);
				break;
			case 11:
				marqueeDisplayPageBase = new MarqueeDisplayPage11(pOldBit, pNewBit, pEffect);
				break;
			case 12:
				marqueeDisplayPageBase = new MarqueeDisplayPage12(pOldBit, pNewBit, pEffect);
				break;
			case 13:
				marqueeDisplayPageBase = new MarqueeDisplayPage13(pOldBit, pNewBit, pEffect);
				break;
			case 14:
				marqueeDisplayPageBase = new MarqueeDisplayPage14(pOldBit, pNewBit, pEffect);
				break;
			case 15:
				marqueeDisplayPageBase = new MarqueeDisplayPage15(pOldBit, pNewBit, pEffect);
				break;
			case 16:
				marqueeDisplayPageBase = new MarqueeDisplayPage16(pOldBit, pNewBit, pEffect);
				break;
			case 17:
				marqueeDisplayPageBase = new MarqueeDisplayPage17(pOldBit, pNewBit, pEffect);
				break;
			case 18:
				marqueeDisplayPageBase = new MarqueeDisplayPage18(pOldBit, pNewBit, pEffect);
				break;
			case 19:
				marqueeDisplayPageBase = new MarqueeDisplayPage19(pOldBit, pNewBit, pEffect);
				break;
			case 20:
				marqueeDisplayPageBase = new MarqueeDisplayPage20(pOldBit, pNewBit, pEffect);
				break;
			case 21:
				marqueeDisplayPageBase = new MarqueeDisplayPage21(pOldBit, pNewBit, pEffect);
				break;
			case 22:
				marqueeDisplayPageBase = new MarqueeDisplayPage22(pOldBit, pNewBit, pEffect);
				break;
			case 23:
				marqueeDisplayPageBase = new MarqueeDisplayPage23(pOldBit, pNewBit, pEffect);
				break;
			case 24:
				marqueeDisplayPageBase = new MarqueeDisplayPage24(pOldBit, pNewBit, pEffect);
				break;
			case 25:
				marqueeDisplayPageBase = new MarqueeDisplayPage25(pOldBit, pNewBit, pEffect);
				break;
			case 26:
				marqueeDisplayPageBase = new MarqueeDisplayPage26(pOldBit, pNewBit, pEffect);
				break;
			case 27:
				marqueeDisplayPageBase = new MarqueeDisplayPage27(pOldBit, pNewBit, pEffect);
				break;
			case 28:
				marqueeDisplayPageBase = new MarqueeDisplayPage28(pOldBit, pNewBit, pEffect);
				break;
			case 29:
				marqueeDisplayPageBase = new MarqueeDisplayPage29(pOldBit, pNewBit, pEffect);
				break;
			case 30:
				marqueeDisplayPageBase = new MarqueeDisplayPage30(pOldBit, pNewBit, pEffect);
				break;
			case 31:
				marqueeDisplayPageBase = new MarqueeDisplayPage31(pOldBit, pNewBit, pEffect);
				break;
			case 32:
				marqueeDisplayPageBase = new MarqueeDisplayPage32(pOldBit, pNewBit, pEffect);
				break;
			case 33:
				marqueeDisplayPageBase = new MarqueeDisplayPage33(pOldBit, pNewBit, pEffect);
				break;
			case 34:
				marqueeDisplayPageBase = new MarqueeDisplayPage34(pOldBit, pNewBit, pEffect);
				break;
			case 35:
				marqueeDisplayPageBase = new MarqueeDisplayPage35(pOldBit, pNewBit, pEffect);
				break;
			case 36:
				marqueeDisplayPageBase = new MarqueeDisplayPage36(pOldBit, pNewBit, pEffect);
				break;
			case 37:
				marqueeDisplayPageBase = new MarqueeDisplayPage37(pOldBit, pNewBit, pEffect);
				break;
			case 38:
				marqueeDisplayPageBase = new MarqueeDisplayPage38(pOldBit, pNewBit, pEffect);
				break;
			case 39:
				marqueeDisplayPageBase = new MarqueeDisplayPage39(pOldBit, pNewBit, pEffect);
				break;
			case 40:
				marqueeDisplayPageBase = new MarqueeDisplayPage40(pOldBit, pNewBit, pEffect);
				break;
			case 41:
				marqueeDisplayPageBase = new MarqueeDisplayPage41(pOldBit, pNewBit, pEffect);
				break;
			case 42:
				marqueeDisplayPageBase = new MarqueeDisplayPage42(pOldBit, pNewBit, pEffect);
				break;
			case 43:
				marqueeDisplayPageBase = new MarqueeDisplayPage43(pOldBit, pNewBit, pEffect);
				break;
			case 44:
				marqueeDisplayPageBase = new MarqueeDisplayPage44(pOldBit, pNewBit, pEffect);
				break;
			case 45:
				marqueeDisplayPageBase = new MarqueeDisplayPage45(pOldBit, pNewBit, pEffect);
				break;
			case 46:
				marqueeDisplayPageBase = new MarqueeDisplayPage46(pOldBit, pNewBit, pEffect);
				break;
			case 47:
				marqueeDisplayPageBase = new MarqueeDisplayPage47(pOldBit, pNewBit, pEffect);
				break;
			case 48:
				marqueeDisplayPageBase = new MarqueeDisplayPage48(pOldBit, pNewBit, pEffect);
				break;
			case 49:
				marqueeDisplayPageBase = new MarqueeDisplayPage49(pOldBit, pNewBit, pEffect);
				break;
			case 50:
				marqueeDisplayPageBase = new MarqueeDisplayPage50(pOldBit, pNewBit, pEffect);
				break;
			case 51:
				marqueeDisplayPageBase = new MarqueeDisplayPage51(pOldBit, pNewBit, pEffect);
				break;
			case 52:
				marqueeDisplayPageBase = new MarqueeDisplayPage52(pOldBit, pNewBit, pEffect);
				break;
			case 53:
				marqueeDisplayPageBase = new MarqueeDisplayPage53(pOldBit, pNewBit, pEffect);
				break;
			case 54:
				marqueeDisplayPageBase = new MarqueeDisplayPage54(pOldBit, pNewBit, pEffect);
				break;
			case 55:
				marqueeDisplayPageBase = new MarqueeDisplayPage55(pOldBit, pNewBit, pEffect);
				break;
			case 56:
				marqueeDisplayPageBase = new MarqueeDisplayPage57(pOldBit, pNewBit, pEffect);
				break;
			case 57:
				marqueeDisplayPageBase = new MarqueeDisplayPage56(pOldBit, pNewBit, pEffect);
				break;
			}
			if (marqueeDisplayPageBase == null)
			{
				marqueeDisplayPageBase = new MarqueeDisplayPage47(pOldBit, pNewBit, pEffect);
			}
			return marqueeDisplayPageBase;
		}
	}
}
