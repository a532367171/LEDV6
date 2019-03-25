using LedModel;
using LedModel.Content;
using LedModel.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class LedSubareaDisplayHolder : UserControl
	{
		private delegate void sizeHandeler(System.Drawing.Size pSize, System.Drawing.Point pLocation);

		private delegate void changeImage(System.Drawing.Bitmap obj);

		private bool isMarquee;

		private LedSubarea ledSub;

		private EdgeDisplay edgeDis;

		public Thread t;

		private LedAnimation ledAnimation;

		private bool isAnimation;

		private int nowMarqueeIndex;

		private MarqueeDisplay dis;

		private LedPictureText content;

		private IContainer components;

		private PictureBox pictureBox1;

		private System.Windows.Forms.Timer timer_Draw;

		private System.Windows.Forms.Timer timer_edge;

		private System.Windows.Forms.Timer timer_Text;

		private System.Windows.Forms.Timer timer_Marquee;

		public LedSubareaDisplayHolder(LedSubarea pSubarea)
		{
			this.InitializeComponent();
			this.ledSub = pSubarea;
			this.ledSub.LoadFromFile();
			if (pSubarea.Type == LedSubareaType.Subtitle)
			{
				this.isMarquee = true;
				LedDText arg_39_0 = (LedDText)pSubarea.SelectedContent;
				this.timer_Text.Start();
			}
			else if (pSubarea.Type == LedSubareaType.PictureText)
			{
				this.isMarquee = true;
				this.timer_Marquee.Start();
			}
			else if (pSubarea.Type == LedSubareaType.Animation)
			{
				this.isMarquee = true;
				this.isAnimation = true;
				this.ledAnimation = (LedAnimation)pSubarea.SelectedContent;
				this.timer_Marquee.Start();
			}
			else
			{
				pSubarea.SelectedContent.LastDrawn = null;
				this.isMarquee = false;
				this.timer_Draw.Interval = 1000;
				this.timer_Draw.Start();
			}
			if (pSubarea.SelectedContent != null && pSubarea.SelectedContent.Edge.Enabled && !this.isMarquee)
			{
				this.edgeDis = new EdgeDisplay(pSubarea.SelectedContent.Edge, pSubarea);
				this.timer_edge.Start();
			}
		}

		public void ReDraw()
		{
		}

		public void ChangeSize()
		{
			int x = (int)(formDisplay.zoon * this.ledSub.X);
			int y = (int)(formDisplay.zoon * this.ledSub.Y);
			int width = (int)(formDisplay.zoon * this.ledSub.Size.Width);
			int height = (int)(formDisplay.zoon * this.ledSub.Size.Height);
			LedSubareaDisplayHolder.sizeHandeler method = new LedSubareaDisplayHolder.sizeHandeler(this.ChangeSizeLocation);
			base.Invoke(method, new object[]
			{
				new System.Drawing.Size(width, height),
				new System.Drawing.Point(x, y)
			});
		}

		public void ChangeSizeLocation(System.Drawing.Size pSize, System.Drawing.Point pLocation)
		{
			base.Size = pSize;
			base.Location = pLocation;
		}

		private void updateImage(System.Drawing.Bitmap pImg)
		{
		}

		public void StartUpdate()
		{
			try
			{
				this.ledSub.PreviewDraw();
				this.ledSub.LastScaleDrawn = LedSubareaDisplayHolder.Griding(this.ledSub.LastDrawn, formDisplay.zoon);
				if (!this.ledSub.SelectedContent.Edge.Enabled)
				{
					formMain.ReleasePicture(this.pictureBox1, this.ledSub.LastScaleDrawn);
				}
			}
			catch
			{
			}
		}

		public void StopDisplay()
		{
			try
			{
				this.ledSub.SaveToFile();
				this.timer_Draw.Stop();
				this.timer_edge.Stop();
				this.timer_Marquee.Stop();
				this.timer_Text.Stop();
				if (!this.isMarquee)
				{
					this.ledSub.LastScaleDrawn.Dispose();
				}
			}
			catch
			{
			}
		}

		public static System.Drawing.Bitmap Griding(System.Drawing.Bitmap pBit, decimal pZoon)
		{
			if (pBit == null)
			{
				return pBit;
			}
			if ((int)(pBit.Width * pZoon) == 0 | (int)(pBit.Height * pZoon) == 0)
			{
				return pBit;
			}
			new object();
			if (pZoon == 1m)
			{
				return new System.Drawing.Bitmap(pBit);
			}
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap((int)(pBit.Width * pZoon), (int)(pBit.Height * pZoon));
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
			graphics.Clear(System.Drawing.Color.Transparent);
			graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
			graphics.DrawImage(pBit, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), new System.Drawing.Rectangle(0, 0, pBit.Width, pBit.Height), System.Drawing.GraphicsUnit.Pixel);
			pBit.Dispose();
			System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Gray);
			if (pZoon < 3m)
			{
				return bitmap;
			}
			int num = (int)pZoon;
			for (int i = 0; i < bitmap.Width; i += num)
			{
				graphics.DrawLine(pen, i, 0, i, bitmap.Height);
			}
			for (int i = 0; i < bitmap.Height; i += num)
			{
				graphics.DrawLine(pen, 0, i, bitmap.Width, i);
			}
			graphics.DrawLine(pen, bitmap.Width - 1, 0, bitmap.Width, bitmap.Height);
			graphics.DrawLine(pen, 0, bitmap.Height, bitmap.Width, bitmap.Height);
			graphics.Dispose();
			pen.Dispose();
			return bitmap;
		}

		private void LedSubareaDisplayHolder_Paint(object sender, PaintEventArgs e)
		{
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(this.StartUpdate));
			thread.Start();
		}

		private void timer_edge_Tick(object sender, EventArgs e)
		{
			if (this.isMarquee)
			{
				this.timer_edge.Stop();
				return;
			}
			this.t = new Thread(new ThreadStart(this.UpdateEdge));
			this.t.IsBackground = true;
			this.t.Start();
		}

		private void UpdateEdge()
		{
			System.Drawing.Bitmap @new = this.edgeDis.GetNew();
			formMain.ReleasePicture(this.pictureBox1, LedSubareaDisplayHolder.Griding(@new, formDisplay.zoon));
		}

		private void timer_Marquee_Tick(object sender, EventArgs e)
		{
			if (!this.isAnimation && this.ledSub.Contents.Count == 0)
			{
				return;
			}
			if (this.dis == null)
			{
				this.nowMarqueeIndex = 0;
				if (this.isAnimation)
				{
					this.content = this.ledAnimation;
				}
				else
				{
					this.content = (LedPictureText)this.ledSub.Contents[0];
				}
				this.dis = LedSubareaDisplayHolder.getMarqueeDisplayByContent(this.content);
				if (this.ledSub != null && this.ledSub.Contents.Count > 1)
				{
					this.dis.NeedChangeContent = true;
				}
				this.edgeDis = new EdgeDisplay(this.content.Edge, this.ledSub);
			}
			System.Drawing.Bitmap @new = this.dis.getNew();
			if (@new == null)
			{
				if (this.isAnimation)
				{
					this.content = this.ledAnimation;
				}
				else
				{
					this.nowMarqueeIndex++;
					if (this.nowMarqueeIndex == this.ledSub.Contents.Count)
					{
						this.nowMarqueeIndex = 0;
					}
					this.content = (LedPictureText)this.ledSub.Contents[this.nowMarqueeIndex];
				}
				this.dis = LedSubareaDisplayHolder.getMarqueeDisplayByContent(this.content);
				if (this.ledSub != null && this.ledSub.Contents.Count > 1)
				{
					this.dis.NeedChangeContent = true;
				}
				this.edgeDis = new EdgeDisplay(this.content.Edge, this.ledSub);
				@new = this.dis.getNew();
			}
			if (@new != null)
			{
				lock (@new)
				{
					if (this.content.Edge.Enabled)
					{
						System.Drawing.Bitmap new2 = this.edgeDis.GetNew(@new);
						formMain.ReleasePicture(this.pictureBox1, LedSubareaDisplayHolder.Griding(new2, formDisplay.zoon));
					}
					else
					{
						System.Drawing.Bitmap pBit = LedSubareaDisplayHolder.Griding(@new, formDisplay.zoon);
						formMain.ReleasePicture(this.pictureBox1, pBit);
					}
				}
			}
		}

		private void timer_Text_Tick(object sender, EventArgs e)
		{
			this.t = new Thread(new ThreadStart(this.UpdateText));
			this.t.Start();
		}

		private void UpdateText()
		{
			if (this.dis == null)
			{
				this.content = (LedDText)this.ledSub.SelectedContent;
				this.dis = LedSubareaDisplayHolder.getMarqueeDisplayByContent(this.content);
				this.edgeDis = new EdgeDisplay(this.ledSub.SelectedContent.Edge, this.ledSub);
			}
			System.Drawing.Bitmap @new = this.dis.getNew();
			if (@new == null)
			{
				this.dis = LedSubareaDisplayHolder.getMarqueeDisplayByContent(this.content);
				this.edgeDis = new EdgeDisplay(this.ledSub.SelectedContent.Edge, this.ledSub);
				@new = this.dis.getNew();
			}
			lock (@new)
			{
				if (this.content.Edge.Enabled)
				{
					System.Drawing.Bitmap new2 = this.edgeDis.GetNew(@new);
					formMain.ReleasePicture(this.pictureBox1, LedSubareaDisplayHolder.Griding(new2, formDisplay.zoon));
				}
				else
				{
					formMain.ReleasePicture(this.pictureBox1, LedSubareaDisplayHolder.Griding(@new, formDisplay.zoon));
				}
			}
		}

		private static MarqueeDisplay getMarqueeDisplayByContent(LedPictureText pContent)
		{
			int num = (int)LedGlobal.EntryModeList[(int)pContent.EffectsSetting.EntryMode];
			if (pContent.PictureTextType == LedPictureTextType.Animation | pContent.PictureTextType == LedPictureTextType.GIF)
			{
				return new MarqueeDisplayAnimation(pContent);
			}
			if (pContent.PictureTextType == LedPictureTextType.Text && num == 130)
			{
				return new MarqueeDisplay1300(pContent);
			}
			if (num == 130)
			{
				return new MarqueeDisplay130(pContent);
			}
			if (num == 131)
			{
				return new MarqueeDisplay131(pContent);
			}
			if (num == 132)
			{
				return new MarqueeDisplay132(pContent);
			}
			if (num == 133)
			{
				return new MarqueeDisplay133(pContent);
			}
			return new MarqueeDisplayTotal(pContent);
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
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
			this.components = new Container();
			this.pictureBox1 = new PictureBox();
			this.timer_Draw = new System.Windows.Forms.Timer(this.components);
			this.timer_edge = new System.Windows.Forms.Timer(this.components);
			this.timer_Text = new System.Windows.Forms.Timer(this.components);
			this.timer_Marquee = new System.Windows.Forms.Timer(this.components);
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.BackColor = System.Drawing.Color.Black;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(204, 78);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_Paint);
			this.timer_Draw.Tick += new EventHandler(this.timer1_Tick);
			this.timer_edge.Interval = 42;
			this.timer_edge.Tick += new EventHandler(this.timer_edge_Tick);
			this.timer_Text.Interval = 42;
			this.timer_Text.Tick += new EventHandler(this.timer_Text_Tick);
			this.timer_Marquee.Interval = 42;
			this.timer_Marquee.Tick += new EventHandler(this.timer_Marquee_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			base.Controls.Add(this.pictureBox1);
			base.Name = "LedSubareaDisplayHolder";
			base.Size = new System.Drawing.Size(204, 78);
			base.Paint += new PaintEventHandler(this.LedSubareaDisplayHolder_Paint);
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
