using System;
using System.Windows.Forms;

namespace LedControlSystem
{
	public class TreeViewEnhanced : TreeView
	{
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 515)
			{
				m.Result = IntPtr.Zero;
				return;
			}
			base.WndProc(ref m);
		}
	}
}
