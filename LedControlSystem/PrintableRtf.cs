using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LedControlSystem
{
	internal class PrintableRtf : RichTextBox
	{
		private struct RECT
		{
			public int Left;

			public int Top;

			public int Right;

			public int Bottom;
		}

		private struct CHARRANGE
		{
			public int cpMin;

			public int cpMax;
		}

		private struct FORMATRANGE
		{
			public IntPtr hdc;

			public IntPtr hdcTarget;

			public PrintableRtf.RECT rc;

			public PrintableRtf.RECT rcPage;

			public PrintableRtf.CHARRANGE chrg;
		}

		private const int WM_USER = 1024;

		private const int EM_FORMATRANGE = 1081;

		private double anInch = 14.4;

		private static IntPtr moduleHandle;

		protected override CreateParams CreateParams
		{
			get
			{
				if (PrintableRtf.moduleHandle == IntPtr.Zero)
				{
					PrintableRtf.moduleHandle = PrintableRtf.LoadLibrary("msftedit.dll");
					if ((long)PrintableRtf.moduleHandle < 32L)
					{
						throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not load Msftedit.dll");
					}
				}
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "RichEdit50W";
				if (this.Multiline)
				{
					if ((base.ScrollBars & RichTextBoxScrollBars.Horizontal) != RichTextBoxScrollBars.None && !base.WordWrap)
					{
						createParams.Style |= 1048576;
						if ((base.ScrollBars & (RichTextBoxScrollBars)16) != RichTextBoxScrollBars.None)
						{
							createParams.Style |= 8192;
						}
					}
					if ((base.ScrollBars & RichTextBoxScrollBars.Vertical) != RichTextBoxScrollBars.None)
					{
						createParams.Style |= 2097152;
						if ((base.ScrollBars & (RichTextBoxScrollBars)16) != RichTextBoxScrollBars.None)
						{
							createParams.Style |= 8192;
						}
					}
				}
				if (BorderStyle.FixedSingle == base.BorderStyle && (createParams.Style & 8388608) != 0)
				{
					createParams.Style &= -8388609;
					createParams.ExStyle |= 512;
				}
				return createParams;
			}
		}

		[DllImport("USER32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string path);

		public int Print(int charFrom, int charTo, System.Drawing.RectangleF marginBounds, System.Drawing.RectangleF pageBounds, System.Drawing.Graphics g)
		{
			this.anInch = 1440.0 / (double)g.DpiX;
			PrintableRtf.RECT rc;
			rc.Top = (int)((double)marginBounds.Top * this.anInch);
			rc.Bottom = (int)((double)marginBounds.Bottom * this.anInch);
			rc.Left = (int)((double)marginBounds.Left * this.anInch);
			rc.Right = (int)((double)marginBounds.Right * this.anInch);
			PrintableRtf.RECT rcPage;
			rcPage.Top = (int)((double)pageBounds.Top * this.anInch);
			rcPage.Bottom = (int)((double)pageBounds.Bottom * this.anInch);
			rcPage.Left = (int)((double)pageBounds.Left * this.anInch);
			rcPage.Right = (int)((double)pageBounds.Right * this.anInch);
			IntPtr hdc = g.GetHdc();
			PrintableRtf.FORMATRANGE fORMATRANGE;
			fORMATRANGE.chrg.cpMax = charTo;
			fORMATRANGE.chrg.cpMin = charFrom;
			fORMATRANGE.hdc = hdc;
			fORMATRANGE.hdcTarget = hdc;
			fORMATRANGE.rc = rc;
			fORMATRANGE.rcPage = rcPage;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			zero = new IntPtr(1);
			IntPtr intPtr2 = IntPtr.Zero;
			intPtr2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(fORMATRANGE));
			Marshal.StructureToPtr(fORMATRANGE, intPtr2, false);
			intPtr = PrintableRtf.SendMessage(base.Handle, 1081, zero, intPtr2);
			Marshal.FreeCoTaskMem(intPtr2);
			g.ReleaseHdc(hdc);
			return intPtr.ToInt32();
		}
	}
}
