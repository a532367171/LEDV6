using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	public class MessageBoxManager
	{
		private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

		public struct CWPRETSTRUCT
		{
			public IntPtr lResult;

			public IntPtr lParam;

			public IntPtr wParam;

			public uint message;

			public IntPtr hwnd;
		}

		private const int WH_CALLWNDPROCRET = 12;

		private const int WM_DESTROY = 2;

		private const int WM_INITDIALOG = 272;

		private const int WM_TIMER = 275;

		private const int WM_USER = 1024;

		private const int DM_GETDEFID = 1024;

		private const int MBOK = 1;

		private const int MBCancel = 2;

		private const int MBAbort = 3;

		private const int MBRetry = 4;

		private const int MBIgnore = 5;

		private const int MBYes = 6;

		private const int MBNo = 7;

		private static MessageBoxManager.HookProc hookProc;

		private static MessageBoxManager.EnumChildProc enumProc;

		[ThreadStatic]
		private static IntPtr hHook;

		[ThreadStatic]
		private static int nButton;

		public static string OK;

		public static string Cancel;

		public static string Abort;

		public static string Retry;

		public static string Ignore;

		public static string Yes;

		public static string No;

		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowsHookEx(int idHook, MessageBoxManager.HookProc lpfn, IntPtr hInstance, int threadId);

		[DllImport("user32.dll")]
		private static extern int UnhookWindowsHookEx(IntPtr idHook);

		[DllImport("user32.dll")]
		private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetWindowTextLengthW")]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetWindowTextW")]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

		[DllImport("user32.dll")]
		private static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

		[DllImport("user32.dll")]
		private static extern bool EnumChildWindows(IntPtr hWndParent, MessageBoxManager.EnumChildProc lpEnumFunc, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetClassNameW")]
		private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		[DllImport("user32.dll")]
		private static extern int GetDlgCtrlID(IntPtr hwndCtl);

		[DllImport("user32.dll")]
		private static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetWindowTextW")]
		private static extern bool SetWindowText(IntPtr hWnd, string lpString);

		static MessageBoxManager()
		{
			MessageBoxManager.OK = "&OK";
			MessageBoxManager.Cancel = "&Cancel";
			MessageBoxManager.Abort = "&Abort";
			MessageBoxManager.Retry = "&Retry";
			MessageBoxManager.Ignore = "&Ignore";
			MessageBoxManager.Yes = "&Yes";
			MessageBoxManager.No = "&No";
			MessageBoxManager.hookProc = new MessageBoxManager.HookProc(MessageBoxManager.MessageBoxHookProc);
			MessageBoxManager.enumProc = new MessageBoxManager.EnumChildProc(MessageBoxManager.MessageBoxEnumProc);
			MessageBoxManager.hHook = IntPtr.Zero;
		}

		public static void Register()
		{
			if (MessageBoxManager.hHook != IntPtr.Zero)
			{
				throw new NotSupportedException("One hook per thread allowed.");
			}
			MessageBoxManager.hHook = MessageBoxManager.SetWindowsHookEx(12, MessageBoxManager.hookProc, IntPtr.Zero, AppDomain.GetCurrentThreadId());
		}

		public static void Unregister()
		{
			if (MessageBoxManager.hHook != IntPtr.Zero)
			{
				MessageBoxManager.UnhookWindowsHookEx(MessageBoxManager.hHook);
				MessageBoxManager.hHook = IntPtr.Zero;
			}
		}

		private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode < 0)
			{
				return MessageBoxManager.CallNextHookEx(MessageBoxManager.hHook, nCode, wParam, lParam);
			}
			MessageBoxManager.CWPRETSTRUCT cWPRETSTRUCT = (MessageBoxManager.CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(MessageBoxManager.CWPRETSTRUCT));
			IntPtr idHook = MessageBoxManager.hHook;
			if (cWPRETSTRUCT.message == 272u)
			{
				MessageBoxManager.GetWindowTextLength(cWPRETSTRUCT.hwnd);
				StringBuilder stringBuilder = new StringBuilder(10);
				MessageBoxManager.GetClassName(cWPRETSTRUCT.hwnd, stringBuilder, stringBuilder.Capacity);
				if (stringBuilder.ToString() == "#32770")
				{
					MessageBoxManager.nButton = 0;
					MessageBoxManager.EnumChildWindows(cWPRETSTRUCT.hwnd, MessageBoxManager.enumProc, IntPtr.Zero);
					if (MessageBoxManager.nButton == 1)
					{
						IntPtr dlgItem = MessageBoxManager.GetDlgItem(cWPRETSTRUCT.hwnd, 2);
						if (dlgItem != IntPtr.Zero)
						{
							MessageBoxManager.SetWindowText(dlgItem, MessageBoxManager.OK);
						}
					}
				}
			}
			return MessageBoxManager.CallNextHookEx(idHook, nCode, wParam, lParam);
		}

		private static bool MessageBoxEnumProc(IntPtr hWnd, IntPtr lParam)
		{
			StringBuilder stringBuilder = new StringBuilder(10);
			MessageBoxManager.GetClassName(hWnd, stringBuilder, stringBuilder.Capacity);
			if (stringBuilder.ToString() == "Button")
			{
				switch (MessageBoxManager.GetDlgCtrlID(hWnd))
				{
				case 1:
					MessageBoxManager.SetWindowText(hWnd, MessageBoxManager.OK);
					break;
				case 2:
					MessageBoxManager.SetWindowText(hWnd, MessageBoxManager.Cancel);
					break;
				case 3:
					MessageBoxManager.SetWindowText(hWnd, MessageBoxManager.Abort);
					break;
				case 4:
					MessageBoxManager.SetWindowText(hWnd, MessageBoxManager.Retry);
					break;
				case 5:
					MessageBoxManager.SetWindowText(hWnd, MessageBoxManager.Ignore);
					break;
				case 6:
					MessageBoxManager.SetWindowText(hWnd, MessageBoxManager.Yes);
					break;
				case 7:
					MessageBoxManager.SetWindowText(hWnd, MessageBoxManager.No);
					break;
				}
				MessageBoxManager.nButton++;
			}
			return true;
		}
	}
}
