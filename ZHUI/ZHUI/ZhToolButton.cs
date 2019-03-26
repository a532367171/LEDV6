using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ZHUI
{
	public class ZhToolButton : UserControl
	{
		private Bitmap bitmap;

		private IContainer components;

		private PictureBox pictureBox1;

		private Label label1_ButtonContent;

		public Bitmap Bitmap
		{
			get
			{
				return this.bitmap;
			}
			set
			{
				this.bitmap = value;
				this.pictureBox1.Image = value;
			}
		}

		[Browsable(true), Category("N1"), Description("文本")]
		public new string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.label1_ButtonContent.Text = value;
			}
		}

		[Browsable(false), Category("N2"), Description("控件尺寸")]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
				this.Refresh();
			}
		}

		public ZhToolButton()
		{
			this.InitializeComponent();
			base.Size = new Size(52, 58);
			this.BackColor = Color.Transparent;
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			this.OnClick(e);
		}

		private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{
			this.pictureBox1.Size = new Size(64, 64);
			this.pictureBox1.Location = new Point(-6, -4);
		}

		private void pictureBox1_MouseLeave(object sender, EventArgs e)
		{
			this.pictureBox1.Size = new Size(48, 48);
			this.pictureBox1.Location = new Point(2, 4);
		}

		private void label1_Paint(object sender, PaintEventArgs e)
		{
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
			this.pictureBox1 = new PictureBox();
			this.label1_ButtonContent = new Label();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.Location = new Point(2, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(48, 48);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseLeave += new EventHandler(this.pictureBox1_MouseLeave);
			this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
			this.pictureBox1.MouseEnter += new EventHandler(this.pictureBox1_MouseEnter);
			this.label1_ButtonContent.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 134);
			this.label1_ButtonContent.ForeColor = Color.White;
			this.label1_ButtonContent.Location = new Point(-25, 53);
			this.label1_ButtonContent.Name = "label1_ButtonContent";
			this.label1_ButtonContent.Size = new Size(100, 16);
			this.label1_ButtonContent.TabIndex = 1;
			this.label1_ButtonContent.Text = "label1";
			this.label1_ButtonContent.TextAlign = ContentAlignment.MiddleCenter;
			this.label1_ButtonContent.MouseLeave += new EventHandler(this.pictureBox1_MouseLeave);
			this.label1_ButtonContent.Paint += new PaintEventHandler(this.label1_Paint);
			this.label1_ButtonContent.MouseEnter += new EventHandler(this.pictureBox1_MouseEnter);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.label1_ButtonContent);
			base.Controls.Add(this.pictureBox1);
			base.Name = "ZhToolButton";
			this.Size = new Size(52, 70);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
