using AxShockwaveFlashObjects;
using System;
using System.ComponentModel;

namespace LedControlSystem
{
	public class MyAxShockwaveFlash : AxShockwaveFlash
	{
		private const int WM_LBUTTONDOWN = 513;

		public EventHandler MyMouseClick;

		public EventArgs e = new EventArgs();

		private IContainer components;

		public MyAxShockwaveFlash()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
		}
	}
}
