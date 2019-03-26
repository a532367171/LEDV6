using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ZHUI
{
	public class ZhForm : Form
	{
		private IContainer components;

		private PictureBox pictureBox1;

		public ZhForm()
		{
			this.InitializeComponent();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
		}

		private void ZhForm_Paint(object sender, PaintEventArgs e)
		{
			RoundFormPainter.Paint(sender, e);
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
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.BackColor = SystemColors.Info;
			this.pictureBox1.Location = new Point(1, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(19, 29);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(813, 547);
			base.Controls.Add(this.pictureBox1);
			base.FormBorderStyle = FormBorderStyle.None;
			base.Name = "ZhForm";
			this.Text = "ZhForm";
			base.Paint += new PaintEventHandler(this.ZhForm_Paint);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
