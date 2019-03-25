using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LedControlSystem.Fonts
{
	public class FontFamiliesEx
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LogFont
		{
			public int lfHeight;

			public int lfWidth;

			public int lfEscapement;

			public int lfOrientation;

			public FontFamiliesEx.FontWeight lfWeight;

			[MarshalAs(UnmanagedType.U1)]
			public bool lfItalic;

			[MarshalAs(UnmanagedType.U1)]
			public bool lfUnderline;

			[MarshalAs(UnmanagedType.U1)]
			public bool lfStrikeOut;

			public FontFamiliesEx.FontCharSet lfCharSet;

			public FontFamiliesEx.FontPrecision lfOutPrecision;

			public FontFamiliesEx.FontClipPrecision lfClipPrecision;

			public FontFamiliesEx.FontQuality lfQuality;

			public FontFamiliesEx.FontPitchAndFamily lfPitchAndFamily;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}

		public enum FontWeight
		{
			FW_DONTCARE,
			FW_THIN = 100,
			FW_EXTRALIGHT = 200,
			FW_LIGHT = 300,
			FW_NORMAL = 400,
			FW_MEDIUM = 500,
			FW_SEMIBOLD = 600,
			FW_BOLD = 700,
			FW_EXTRABOLD = 800,
			FW_HEAVY = 900
		}

		public enum FontCharSet : byte
		{
			ANSI_CHARSET,
			DEFAULT_CHARSET,
			SYMBOL_CHARSET,
			SHIFTJIS_CHARSET = 128,
			HANGEUL_CHARSET,
			HANGUL_CHARSET = 129,
			GB2312_CHARSET = 134,
			CHINESEBIG5_CHARSET = 136,
			OEM_CHARSET = 255,
			JOHAB_CHARSET = 130,
			HEBREW_CHARSET = 177,
			ARABIC_CHARSET,
			GREEK_CHARSET = 161,
			TURKISH_CHARSET,
			VIETNAMESE_CHARSET,
			THAI_CHARSET = 222,
			EASTEUROPE_CHARSET = 238,
			RUSSIAN_CHARSET = 204,
			MAC_CHARSET = 77,
			BALTIC_CHARSET = 186
		}

		public enum FontPrecision : byte
		{
			OUT_DEFAULT_PRECIS,
			OUT_STRING_PRECIS,
			OUT_CHARACTER_PRECIS,
			OUT_STROKE_PRECIS,
			OUT_TT_PRECIS,
			OUT_DEVICE_PRECIS,
			OUT_RASTER_PRECIS,
			OUT_TT_ONLY_PRECIS,
			OUT_OUTLINE_PRECIS,
			OUT_SCREEN_OUTLINE_PRECIS,
			OUT_PS_ONLY_PRECIS
		}

		public enum FontClipPrecision : byte
		{
			CLIP_DEFAULT_PRECIS,
			CLIP_CHARACTER_PRECIS,
			CLIP_STROKE_PRECIS,
			CLIP_MASK = 15,
			CLIP_LH_ANGLES,
			CLIP_TT_ALWAYS = 32,
			CLIP_DFA_DISABLE = 64,
			CLIP_EMBEDDED = 128
		}

		public enum FontQuality : byte
		{
			DEFAULT_QUALITY,
			DRAFT_QUALITY,
			PROOF_QUALITY,
			NONANTIALIASED_QUALITY,
			ANTIALIASED_QUALITY,
			CLEARTYPE_QUALITY,
			CLEARTYPE_NATURAL_QUALITY
		}

		[Flags]
		public enum FontPitchAndFamily : byte
		{
			DEFAULT_PITCH = 0,
			FIXED_PITCH = 1,
			VARIABLE_PITCH = 2,
			FF_DONTCARE = 0,
			FF_ROMAN = 16,
			FF_SWISS = 32,
			FF_MODERN = 48,
			FF_SCRIPT = 64,
			FF_DECORATIVE = 80
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct EnumLogFontEx
		{
			public FontFamiliesEx.LogFont elfLogFont;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string elfFullName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string elfStyle;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string elfScript;
		}

		private delegate int EnumFontExDelegate(ref FontFamiliesEx.EnumLogFontEx lpelfe, IntPtr lpntme, int FontType, int lParam);

		public const int LF_FACESIZE = 32;

		public IList<string> FontNames;

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
		private static extern int EnumFontFamiliesEx(IntPtr hDC, [MarshalAs(UnmanagedType.LPStruct)] [In] FontFamiliesEx.LogFont logFont, FontFamiliesEx.EnumFontExDelegate enumFontExCallback, IntPtr lParam, uint dwFlags);

		public FontFamiliesEx()
		{
			this.FontNames = new List<string>();
		}

		public IList<string> GetFontFamilies()
		{
			this.FontNames.Clear();
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(10, 10);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
			IntPtr hdc = graphics.GetHdc();
			int num = FontFamiliesEx.EnumFontFamiliesEx(hdc, new FontFamiliesEx.LogFont
			{
				lfCharSet = FontFamiliesEx.FontCharSet.DEFAULT_CHARSET
			}, new FontFamiliesEx.EnumFontExDelegate(this.enumFontEx), IntPtr.Zero, 0u);
			graphics.ReleaseHdc(hdc);
			graphics.Dispose();
			bitmap.Dispose();
			if (num == 1)
			{
				((List<string>)this.FontNames).Sort();
				return this.FontNames;
			}
			return new List<string>();
		}

		private int enumFontEx(ref FontFamiliesEx.EnumLogFontEx lpelfe, IntPtr lpntme, int FontType, int lParam)
		{
			string lfFaceName = lpelfe.elfLogFont.lfFaceName;
			if (!this.FontNames.Contains(lfFaceName))
			{
				this.FontNames.Add(lfFaceName);
			}
			return 1;
		}
	}
}
