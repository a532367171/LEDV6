using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ZHUI
{
	public class ZhFontbar : UserControl
	{
		private bool isBold;

		private bool isItalic;

		private bool isUnderline;

		private AlignType alignType;

		private IContainer components;

		public bool IsBold
		{
			get
			{
				return this.isBold;
			}
			set
			{
				this.isBold = value;
			}
		}

		public bool IsItalic
		{
			get
			{
				return this.isItalic;
			}
			set
			{
				this.isItalic = value;
			}
		}

		public bool IsUnderline
		{
			get
			{
				return this.isUnderline;
			}
			set
			{
				this.isUnderline = value;
			}
		}

		public AlignType AlignType
		{
			get
			{
				return this.alignType;
			}
			set
			{
				this.alignType = value;
			}
		}

		public ZhFontbar()
		{
			this.InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "ZhFontbar";
			base.Size = new Size(154, 20);
			base.ResumeLayout(false);
		}
	}
}
