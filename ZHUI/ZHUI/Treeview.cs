using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ZHUI
{
	public class Treeview : TreeView
	{
		private IContainer components;

		public Treeview()
		{
			this.InitializeComponent();
			this.BackColor = Color.Transparent;
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
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
