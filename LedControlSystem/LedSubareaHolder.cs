using LedModel;
using LedModel.Content;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class LedSubareaHolder : UserControl
	{
		private LedSubarea subarea;

		public bool isActivated;

		private bool isChangingSize;

		private bool isMoving;

		private bool isMoved;

		private LedSubareaMoveType moveType;

		private int maxWidth;

		private int maxHeight;

		public static IList<Thread> threadList = new List<Thread>();

		private decimal nowZoom;

		private System.Drawing.Size oldSize;

		private System.Drawing.Point oldMousePoint;

		private System.Drawing.Point oldHolderPoint;

		private int oldBottom;

		private int oldRight;

		private int oldTop;

		private int oldLeft;

		private int oldHeight;

		private int oldWidth;

		private int minWidth;

		private int minHeight;

		private int maxxWidth;

		private int maxxHeight;

		private System.Drawing.Size oldHolderSize;

		public bool DrawFinished = true;

		private int DrawNum;

		private IContainer components;

		private ContextMenuStrip cmsSubarea;

		private ToolStripMenuItem tsmiDeleteSubarea;

		private ToolStripMenuItem tsmiRefresh;

		private PictureBox picSubarea;

		private Label lblLocation;

		private Label lblSize;

		public event LedGlobal.LedContentEvent HolderEvent;

		public LedSubarea Subarea
		{
			get
			{
				return this.subarea;
			}
		}

		public int MaxWidth
		{
			get
			{
				return this.maxWidth;
			}
			set
			{
				this.maxWidth = value;
			}
		}

		public int MaxHeight
		{
			get
			{
				return this.maxHeight;
			}
			set
			{
				this.maxHeight = value;
			}
		}

		public LedSubareaHolder(LedSubarea pSubarea)
		{
			this.InitializeComponent();
			this.subarea = pSubarea;
			this.lblLocation.Text = string.Empty;
			this.lblLocation.Visible = false;
			this.lblSize.Text = string.Empty;
			this.lblSize.Visible = false;
			this.moveType = LedSubareaMoveType.All;
			this.nowZoom = formMain.Ledsys.SelectedPanel.Zoom;
			this.maxWidth = (int)(formMain.Ledsys.SelectedPanel.Width * this.nowZoom);
			this.maxHeight = (int)(formMain.Ledsys.SelectedPanel.Height * this.nowZoom);
			this.ChangeZoom();
		}

		private void picSubarea_MouseEnter(object sender, EventArgs e)
		{
			this.DrawControlPoint();
		}

		private void picSubarea_MouseLeave(object sender, EventArgs e)
		{
			if (!this.isActivated)
			{
				this.Refresh();
			}
		}

		private void picSubarea_MouseMove(object sender, MouseEventArgs e)
		{
			if (!this.isActivated)
			{
				this.Cursor = Cursors.Default;
				return;
			}
			if (e.Button == MouseButtons.Left)
			{
				this.picSubarea.Visible = false;
				this.lblLocation.Visible = true;
				this.lblSize.Visible = true;
				if (this.isMoving)
				{
					this.isMoved = true;
					base.Location = this.GetNewLocation(Cursor.Position);
					this.GetRealSizeAndLocation();
				}
				else
				{
					this.GetNewSizeAndLocation(Cursor.Position);
					this.GetRealSizeAndLocation();
					this.DrawBlackBackground();
				}
				this.DrawLocation();
				return;
			}
			if (e.X < 10 && e.Y < 10)
			{
				this.Cursor = Cursors.SizeNWSE;
				this.moveType = LedSubareaMoveType.LeftTop;
				return;
			}
			if (e.X < 10 && e.Y > base.Height - 10)
			{
				this.Cursor = Cursors.SizeNESW;
				this.moveType = LedSubareaMoveType.LeftBottom;
				return;
			}
			if (e.X < 10 && e.Y > base.Height / 2 - 5 && e.Y < base.Height / 2 + 5)
			{
				this.Cursor = Cursors.SizeWE;
				this.moveType = LedSubareaMoveType.LeftMiddle;
				return;
			}
			if (e.X > base.Width - 10 && e.Y < 10)
			{
				this.Cursor = Cursors.SizeNESW;
				this.moveType = LedSubareaMoveType.RightTop;
				return;
			}
			if (e.X > base.Width - 10 && e.Y > base.Height / 2 - 5 && e.Y < base.Height / 2 + 5)
			{
				this.Cursor = Cursors.SizeWE;
				this.moveType = LedSubareaMoveType.RightMiddle;
				return;
			}
			if (e.X > base.Width - 10 && e.Y > base.Height - 10)
			{
				this.Cursor = Cursors.SizeNWSE;
				this.moveType = LedSubareaMoveType.RightBottom;
				return;
			}
			if (e.X > base.Width / 2 - 5 && e.X < base.Width / 2 + 5 && e.Y < 10)
			{
				this.Cursor = Cursors.SizeNS;
				this.moveType = LedSubareaMoveType.MiddleTop;
				return;
			}
			if (e.X > base.Width / 2 - 5 && e.X < base.Width / 2 + 5 && e.Y > base.Height - 10)
			{
				this.Cursor = Cursors.SizeNS;
				this.moveType = LedSubareaMoveType.MiddleBottom;
				return;
			}
			this.Cursor = Cursors.SizeAll;
			this.moveType = LedSubareaMoveType.All;
		}

		private void picSubarea_MouseDown(object sender, MouseEventArgs e)
		{
			formMain.LastSelectedContentType = 1;
			this.BackColor = System.Drawing.Color.Black;
			if (!this.isActivated)
			{
				this.HolderEvent(LedContentEventType.Active, this);
			}
			if (this.isMoving || this.isChangingSize)
			{
				return;
			}
			this.oldMousePoint = Cursor.Position;
			this.oldBottom = base.Location.Y + base.Size.Height;
			this.oldRight = base.Location.X + base.Size.Width;
			this.oldLeft = base.Location.X;
			this.oldTop = base.Location.Y;
			this.oldWidth = base.Size.Width;
			this.oldHeight = base.Size.Height;
			this.minHeight = (int)(8m * formMain.ledsys.SelectedPanel.Zoom);
			this.minWidth = (int)(8m * formMain.ledsys.SelectedPanel.Zoom);
			if (this.subarea.ParentItem != null)
			{
				this.maxxHeight = (int)(this.subarea.ParentItem.GetHeight() * formMain.ledsys.SelectedPanel.Zoom);
				this.maxxWidth = (int)(this.subarea.ParentItem.GetWidth() * formMain.ledsys.SelectedPanel.Zoom);
			}
			this.oldHolderPoint = base.Location;
			this.oldHolderSize = base.Size;
			this.oldSize = base.Size;
			if (this.moveType == LedSubareaMoveType.All)
			{
				this.isMoving = true;
				this.isChangingSize = false;
				return;
			}
			this.isMoving = false;
			this.isChangingSize = true;
		}

		private void picSubarea_MouseUp(object sender, MouseEventArgs e)
		{
			this.tsmiDeleteSubarea.Text = formMain.ML.GetStr("UserControl_LedSubareaHolder_DeleteSubarea");
			this.tsmiRefresh.Text = formMain.ML.GetStr("UserControl_LedSubareaHolder_Refresh");
			this.picSubarea.Visible = true;
			this.lblLocation.Text = string.Empty;
			this.lblLocation.Visible = false;
			this.lblSize.Text = string.Empty;
			this.lblSize.Visible = false;
			if (this.isChangingSize)
			{
				this.HolderEvent(LedContentEventType.ChangeSize, this);
				this.Redraw();
			}
			else if (this.isMoving && this.isMoved)
			{
				this.isMoved = false;
				this.HolderEvent(LedContentEventType.Moving, this);
			}
			this.isChangingSize = false;
			this.isMoving = false;
			this.moveType = LedSubareaMoveType.All;
			this.ResetHolderPosition();
		}

		private void tsmiDeleteSubarea_Click(object sender, EventArgs e)
		{
			this.Delete();
		}

		private void tsmiRefresh_Click(object sender, EventArgs e)
		{
			this.Redraw();
		}

		public void GetRealSizeAndLocation()
		{
			decimal zoom = formMain.Ledsys.SelectedPanel.Zoom;
			if (zoom == 1m)
			{
				this.subarea.Location = base.Location;
				this.subarea.Size = base.Size;
				return;
			}
			int num = (int)(base.Location.X / zoom);
			if (base.Location.X % zoom > zoom / 2m)
			{
				num++;
			}
			int num2 = (int)(base.Location.Y / zoom);
			if (base.Location.Y % zoom > zoom / 2m)
			{
				num2++;
			}
			int num3 = (int)(base.Size.Width / zoom);
			if (base.Size.Width % zoom > zoom / 2m)
			{
				num3++;
			}
			int num4 = (int)(base.Size.Height / zoom);
			if (base.Size.Height % zoom > zoom / 2m)
			{
				num4++;
			}
			if (num3 < 8)
			{
				num3 = 8;
			}
			base.Width = (int)(num3 * this.nowZoom);
			if (num4 < 8)
			{
				num4 = 8;
			}
			base.Height = (int)(num4 * this.nowZoom);
			if (num < 0)
			{
				num = 0;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			this.subarea.Location = new System.Drawing.Point(num, num2);
			this.subarea.Size = new System.Drawing.Size(num3, num4);
		}

		public void Activate()
		{
			this.isActivated = true;
			base.BringToFront();
			this.DrawBackground();
		}

		public void Deactivate()
		{
			this.isActivated = false;
			base.SendToBack();
			this.DrawBackground();
		}

		private System.Drawing.Point GetNewLocation(System.Drawing.Point pNowMouseLocation)
		{
			int num = pNowMouseLocation.X - this.oldMousePoint.X;
			int num2 = pNowMouseLocation.Y - this.oldMousePoint.Y;
			System.Drawing.Point result = new System.Drawing.Point(this.oldHolderPoint.X + num, this.oldHolderPoint.Y + num2);
			if (result.X < 0)
			{
				result.X = 0;
			}
			if (result.Y < 0)
			{
				result.Y = 0;
			}
			if (result.X + base.Width > this.maxWidth)
			{
				result.X = this.maxWidth - base.Width;
			}
			if (result.Y + base.Height > this.maxHeight)
			{
				result.Y = this.maxHeight - base.Height;
			}
			if (result.X < 0)
			{
				result.X = 0;
			}
			if (result.Y < 0)
			{
				result.Y = 0;
			}
			return result;
		}

		private void GetNewSizeAndLocation(System.Drawing.Point pNewLocation)
		{
			int num = pNewLocation.X - this.oldMousePoint.X;
			int num2 = pNewLocation.Y - this.oldMousePoint.Y;
			bool flag = false;
			bool flag2 = false;
			int num3;
			int num4;
			int num5;
			int num6;
			switch (this.moveType)
			{
			case LedSubareaMoveType.LeftTop:
				num3 = this.oldTop + num2;
				num4 = this.oldLeft + num;
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 < 0)
				{
					num4 = 0;
				}
				num5 = this.oldRight - num4;
				num6 = this.oldBottom - num3;
				flag = true;
				flag2 = true;
				break;
			case LedSubareaMoveType.LeftMiddle:
				num4 = this.oldLeft + num;
				if (num4 < 0)
				{
					num4 = 0;
				}
				num5 = this.oldRight - num4;
				num6 = this.oldHeight;
				num3 = this.oldTop;
				flag2 = true;
				break;
			case LedSubareaMoveType.LeftBottom:
				num4 = this.oldLeft + num;
				if (num4 < 0)
				{
					num4 = 0;
				}
				num5 = this.oldRight - num4;
				num3 = this.oldTop;
				num6 = this.oldHeight + num2;
				if (num3 + num6 > this.maxxHeight)
				{
					num6 = this.maxxHeight - this.oldTop;
				}
				flag2 = true;
				break;
			case LedSubareaMoveType.MiddleTop:
				num4 = this.oldLeft;
				num5 = this.oldWidth;
				num3 = this.oldTop + num2;
				if (num3 < 0)
				{
					num3 = 0;
				}
				num6 = this.oldBottom - num3;
				flag = true;
				break;
			case LedSubareaMoveType.MiddleBottom:
				num3 = this.oldTop;
				num4 = this.oldLeft;
				num5 = this.oldWidth;
				num6 = this.oldHeight + num2;
				if (num6 + num3 > this.maxxHeight)
				{
					num6 = this.maxxHeight - num3;
				}
				break;
			case LedSubareaMoveType.RightTop:
				num4 = this.oldLeft;
				num5 = this.oldWidth + num;
				num3 = this.oldTop + num2;
				if (num3 < 0)
				{
					num3 = 0;
				}
				num6 = this.oldBottom - num3;
				flag = true;
				break;
			case LedSubareaMoveType.RightMiddle:
				num3 = this.oldTop;
				num4 = this.oldLeft;
				num5 = this.oldWidth + num;
				num6 = this.oldHeight;
				if (num5 + num4 > this.maxxWidth)
				{
					num5 = this.maxxWidth - num4;
				}
				break;
			case LedSubareaMoveType.RightBottom:
				num3 = this.oldTop;
				num4 = this.oldLeft;
				num5 = this.oldWidth + num;
				num6 = this.oldHeight + num2;
				if (num6 + num3 > this.maxxHeight)
				{
					num6 = this.maxxHeight - num3;
				}
				if (num5 + num4 > this.maxxWidth)
				{
					num5 = this.maxxWidth - num4;
				}
				break;
			default:
				return;
			}
			if (num5 < this.minWidth)
			{
				num5 = this.minWidth;
			}
			if (num6 < this.minHeight)
			{
				num6 = this.minHeight;
			}
			if (flag && num3 + num6 > this.oldBottom)
			{
				num3 = this.oldBottom - num6;
			}
			if (flag2 && num4 + num5 > this.oldRight)
			{
				num4 = this.oldRight - num5;
			}
			if (num6 > this.maxxHeight)
			{
				num6 = this.maxxHeight;
			}
			base.Size = new System.Drawing.Size(num5, num6);
			base.Location = new System.Drawing.Point(num4, num3);
		}

		public void ChangeZoom()
		{
			this.nowZoom = formMain.Ledsys.SelectedPanel.Zoom;
			this.maxWidth = (int)(formMain.Ledsys.SelectedPanel.SelectedItem.GetWidth() * this.nowZoom);
			this.maxHeight = (int)(formMain.Ledsys.SelectedPanel.SelectedItem.GetHeight() * this.nowZoom);
			base.Location = new System.Drawing.Point((int)(this.subarea.X * this.nowZoom), (int)(this.subarea.Y * this.nowZoom));
			base.Size = new System.Drawing.Size((int)(this.subarea.Size.Width * this.nowZoom), (int)(this.subarea.Size.Height * this.nowZoom));
			this.DrawBackground();
		}

		public void ResetHolderPosition()
		{
			base.Location = new System.Drawing.Point((int)(this.subarea.X * this.nowZoom), (int)(this.subarea.Y * this.nowZoom));
			base.Size = new System.Drawing.Size((int)(this.subarea.Size.Width * this.nowZoom), (int)(this.subarea.Size.Height * this.nowZoom));
		}

		private void DrawControlPoint()
		{
			if (this.isChangingSize)
			{
				return;
			}
			System.Drawing.Graphics graphics = this.picSubarea.CreateGraphics();
			graphics.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.White, 1f)
			{
				DashStyle = System.Drawing.Drawing2D.DashStyle.Custom,
				DashPattern = new float[]
				{
					3f,
					2f
				}
			}, 0, 0, base.Width - 1, base.Height - 1);
			System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.White);
			int num = 5;
			graphics.DrawRectangle(pen, 0, 0, num, num);
			graphics.DrawRectangle(pen, 0, base.Height - 6, num, num);
			graphics.DrawRectangle(pen, base.Width - 6, 0, num, num);
			graphics.DrawRectangle(pen, base.Width - 6, base.Height - 6, num, num);
			graphics.DrawRectangle(pen, base.Width / 2 - 3, 0, num, num);
			graphics.DrawRectangle(pen, base.Width / 2 - 3, base.Height - 6, num, num);
			graphics.DrawRectangle(pen, 0, base.Height / 2 - 3, num, num);
			graphics.DrawRectangle(pen, base.Width - 6, base.Height / 2 - 3, num, num);
			graphics.Dispose();
		}

		private void DrawControlPoint(System.Drawing.Bitmap bmp)
		{
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bmp);
			graphics.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.White, 1f)
			{
				DashStyle = System.Drawing.Drawing2D.DashStyle.Custom,
				DashPattern = new float[]
				{
					3f,
					2f
				}
			}, 0, 0, base.Width - 1, base.Height - 1);
			System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.White);
			int num = 5;
			graphics.DrawRectangle(pen, 0, 0, num, num);
			graphics.DrawRectangle(pen, 0, base.Height - 6, num, num);
			graphics.DrawRectangle(pen, base.Width - 6, 0, num, num);
			graphics.DrawRectangle(pen, base.Width - 6, base.Height - 6, num, num);
			graphics.DrawRectangle(pen, base.Width / 2 - 3, 0, num, num);
			graphics.DrawRectangle(pen, base.Width / 2 - 3, base.Height - 6, num, num);
			graphics.DrawRectangle(pen, 0, base.Height / 2 - 3, num, num);
			graphics.DrawRectangle(pen, base.Width - 6, base.Height / 2 - 3, num, num);
			graphics.Dispose();
		}

		private void DrawLocation()
		{
			try
			{
				System.Drawing.Font font = new System.Drawing.Font(formMain.ML.GetStr("Display_DefaultFont"), (float)(8m * this.nowZoom), System.Drawing.FontStyle.Bold);
				this.lblLocation.Font = font;
				this.lblSize.Font = font;
				string text = "X:" + this.subarea.X.ToString() + ",Y:" + this.subarea.Y.ToString();
				if (this.subarea.IsOverlapping())
				{
					text = formMain.ML.GetStr("Display_Overlapping") + text;
					this.lblLocation.ForeColor = System.Drawing.Color.Red;
					this.lblLocation.Text = text;
					this.lblSize.ForeColor = System.Drawing.Color.Red;
				}
				else
				{
					this.lblLocation.ForeColor = System.Drawing.Color.Yellow;
					this.lblLocation.Text = "  " + text;
					this.lblSize.ForeColor = System.Drawing.Color.Yellow;
				}
				this.lblSize.Text = this.subarea.Size.Width.ToString() + " x " + this.subarea.Size.Height.ToString();
				this.lblLocation.Location = new System.Drawing.Point(0, 0);
				this.lblSize.Location = new System.Drawing.Point((base.Width - this.lblSize.Width) / 2, (base.Height - this.lblSize.Height) / 2);
				font.Dispose();
				this.lblLocation.Refresh();
				this.lblSize.Refresh();
			}
			catch
			{
			}
		}

		public void DrawBackground()
		{
			if (this.subarea.LastDrawn != null && this.subarea.LastDrawn.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
			{
				System.Drawing.Bitmap bitmap = LedGraphics.ScaleAndGrid(this.subarea.LastDrawn, formMain.ledsys.SelectedPanel.Zoom);
				if (this.isActivated)
				{
					this.DrawControlPoint(bitmap);
				}
				formMain.ReleasePicture(this.picSubarea, bitmap);
				bitmap.Dispose();
				return;
			}
			System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(base.Width, base.Height);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap2);
			graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, bitmap2.Width, bitmap2.Height);
			if (this.nowZoom < 3m)
			{
				formMain.ReleasePicture(this.picSubarea, bitmap2);
				return;
			}
			int num = (int)this.nowZoom;
			System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Gray);
			for (int i = 0; i < base.Width; i += num)
			{
				graphics.DrawLine(pen, i, 0, i, base.Size.Height);
			}
			for (int i = 0; i < base.Height; i += num)
			{
				graphics.DrawLine(pen, 0, i, base.Size.Width, i);
			}
			formMain.ReleasePicture(this.picSubarea, bitmap2);
		}

		private void DrawBlackBackground()
		{
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.subarea.Width, this.subarea.Height);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
			graphics.Clear(System.Drawing.Color.Black);
			formMain.ReleasePicture(this.picSubarea, bitmap);
			graphics.Dispose();
			bitmap.Dispose();
		}

		public void Redraw()
		{
			if (this.DrawFinished)
			{
				this.DrawFinished = false;
				Thread thread = new Thread(new ThreadStart(this.StartRedraw));
				LedSubareaHolder.threadList.Add(thread);
				thread.Start();
				return;
			}
			this.DrawNum++;
		}

		private void StartRedraw()
		{
			do
			{
				this.DrawNum = 0;
				int num = 0;
				if (this.subarea.SelectedContent != null)
				{
					num = this.subarea.SelectedContent.GetActualWidth();
				}
				if (num > 0 && this.subarea.Width < num)
				{
					this.subarea.Width = num;
				}
				int num2 = 0;
				if (this.subarea.ParentItem != null && this.subarea.ParentItem.Edge != null)
				{
					num2 = (this.subarea.ParentItem.Edge.Enabled ? this.subarea.ParentItem.Edge.Height : 0);
				}
				if (this.subarea.SelectedContent != null && this.subarea.SelectedContent.Edge != null && this.subarea.SelectedContent.Edge.Enabled)
				{
					int arg_E1_0 = this.subarea.SelectedContent.Edge.Height;
				}
				if (this.subarea.Width > this.subarea.ParentItem.ParentPanel.Width - num2 * 2)
				{
					this.subarea.Width = this.subarea.ParentItem.ParentPanel.Width - num2 * 2;
				}
				if (this.subarea.Width + this.subarea.X > this.subarea.ParentItem.ParentPanel.Width - num2 * 2)
				{
					this.subarea.X = this.subarea.ParentItem.ParentPanel.Width - num2 * 2 - this.subarea.Width;
				}
				this.ResetHolderPosition();
				try
				{
					this.subarea.Draw();
				}
				catch
				{
				}
			}
			while (this.DrawNum > 0);
			this.DrawFinished = true;
			this.subarea.LastScaleDrawn = LedGraphics.ScaleAndGrid(this.subarea.LastDrawn, formMain.ledsys.SelectedPanel.Zoom);
			if (this.subarea.LastScaleDrawn != null && this.subarea.LastScaleDrawn.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
			{
				System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)this.subarea.LastScaleDrawn.Clone();
				if (this.isActivated)
				{
					this.DrawControlPoint(bitmap);
				}
				formMain.ReleasePicture(this.picSubarea, bitmap);
				bitmap.Dispose();
			}
			if (this.subarea.Type == LedSubareaType.Animation)
			{
				LedAnimation ledAnimation = (LedAnimation)this.subarea.SelectedContent;
				ledAnimation.Changed = true;
				return;
			}
			if (this.subarea.Type == LedSubareaType.PictureText && this.subarea.SelectedContent != null)
			{
				LedPictureText ledPictureText = (LedPictureText)this.subarea.SelectedContent;
				if (ledPictureText.PictureTextType == LedPictureTextType.Animation)
				{
					((LedAnimation)ledPictureText).Changed = true;
				}
			}
		}

		public void Delete()
		{
			string str = formMain.ML.GetStr("Prompt_DeleteWarning");
			string text = string.Format(formMain.ML.GetStr("Prompt_Areyousuretodelete"), this.subarea.Name);
			if (MessageBox.Show(text, str, MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				this.HolderEvent(LedContentEventType.Delete, this);
			}
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
			this.cmsSubarea = new ContextMenuStrip(this.components);
			this.tsmiDeleteSubarea = new ToolStripMenuItem();
			this.tsmiRefresh = new ToolStripMenuItem();
			this.picSubarea = new PictureBox();
			this.lblLocation = new Label();
			this.lblSize = new Label();
			this.cmsSubarea.SuspendLayout();
			((ISupportInitialize)this.picSubarea).BeginInit();
			base.SuspendLayout();
			this.cmsSubarea.Items.AddRange(new ToolStripItem[]
			{
				this.tsmiDeleteSubarea,
				this.tsmiRefresh
			});
			this.cmsSubarea.Name = "contextMenuStrip1";
			this.cmsSubarea.Size = new System.Drawing.Size(125, 48);
			this.tsmiDeleteSubarea.Name = "tsmiDeleteSubarea";
			this.tsmiDeleteSubarea.Size = new System.Drawing.Size(124, 22);
			this.tsmiDeleteSubarea.Text = "删除分区";
			this.tsmiDeleteSubarea.Click += new EventHandler(this.tsmiDeleteSubarea_Click);
			this.tsmiRefresh.Name = "tsmiRefresh";
			this.tsmiRefresh.Size = new System.Drawing.Size(124, 22);
			this.tsmiRefresh.Text = "刷新";
			this.tsmiRefresh.Click += new EventHandler(this.tsmiRefresh_Click);
			this.picSubarea.Dock = DockStyle.Fill;
			this.picSubarea.Location = new System.Drawing.Point(0, 0);
			this.picSubarea.Name = "picSubarea";
			this.picSubarea.Size = new System.Drawing.Size(352, 94);
			this.picSubarea.SizeMode = PictureBoxSizeMode.StretchImage;
			this.picSubarea.TabIndex = 1;
			this.picSubarea.TabStop = false;
			this.picSubarea.MouseDown += new MouseEventHandler(this.picSubarea_MouseDown);
			this.picSubarea.MouseEnter += new EventHandler(this.picSubarea_MouseEnter);
			this.picSubarea.MouseLeave += new EventHandler(this.picSubarea_MouseLeave);
			this.picSubarea.MouseMove += new MouseEventHandler(this.picSubarea_MouseMove);
			this.picSubarea.MouseUp += new MouseEventHandler(this.picSubarea_MouseUp);
			this.lblLocation.AutoSize = true;
			this.lblLocation.ForeColor = System.Drawing.Color.Yellow;
			this.lblLocation.Location = new System.Drawing.Point(0, 0);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(47, 12);
			this.lblLocation.TabIndex = 2;
			this.lblLocation.Text = "X:0,Y:0";
			this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblSize.AutoSize = true;
			this.lblSize.ForeColor = System.Drawing.Color.Yellow;
			this.lblSize.Location = new System.Drawing.Point(0, 38);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(35, 12);
			this.lblSize.TabIndex = 3;
			this.lblSize.Text = "0 x 0";
			this.lblSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ContextMenuStrip = this.cmsSubarea;
			base.Controls.Add(this.lblSize);
			base.Controls.Add(this.lblLocation);
			base.Controls.Add(this.picSubarea);
			base.Name = "LedSubareaHolder";
			base.Size = new System.Drawing.Size(352, 94);
			this.cmsSubarea.ResumeLayout(false);
			((ISupportInitialize)this.picSubarea).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
