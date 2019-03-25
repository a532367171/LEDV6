using LedControlSystem.Properties;
using LedModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formDisplay : Form
	{
		private static string formID = "formDisplay";

		public static LedPanel panel;

		private List<LedSubareaDisplayHolder> holderList = new List<LedSubareaDisplayHolder>();

		public static decimal zoon = 1m;

		private LedItem nowItem;

		private EdgeDisplay nowEdgeDisplay;

		private IList<Thread> threadList = new List<Thread>();

		private IContainer components;

		private Panel panel_Border;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButton1;

		private Panel Panel_Item;

		private ToolStripButton toolStripButton2;

		private ToolStripButton toolStripButton3;

		private ToolStripButton close_ToolStripButton;

		private PictureBox edgeTop_pictureBox;

		private Panel panel_Panel;

		private PictureBox edgeRight_pictureBox;

		private PictureBox edgeBottom_pictureBox;

		private PictureBox edgeLeft_pictureBox;

		private System.Windows.Forms.Timer timer_ItemEdge;

		private PictureBox EdgeCorner1_pictureBox;

		private PictureBox EdgeCorner4_pictureBox;

		private PictureBox EdgeCorner3_pictureBox;

		private PictureBox EdgeCorner2_pictureBox;

		public static string FormID
		{
			get
			{
				return formDisplay.formID;
			}
			set
			{
				formDisplay.formID = value;
			}
		}

		private void Load_Parameters()
		{
			this.Text = formMain.ML.GetStr("formDisplay_FormText");
			this.toolStripButton1.ToolTipText = formMain.ML.GetStr("formDisplay_toolstrip1_toolstripbutton1");
			this.toolStripButton2.ToolTipText = formMain.ML.GetStr("formDisplay_toolstrip1_toolstripbutton2");
			this.toolStripButton3.ToolTipText = formMain.ML.GetStr("formDisplay_toolstrip1_toolstripbutton3");
			this.close_ToolStripButton.ToolTipText = formMain.ML.GetStr("formDisplay_toolstrip1_close_ToolstripButton");
		}

		public formDisplay()
		{
			this.InitializeComponent();
			this.Load_Parameters();
		}

		public formDisplay(LedPanel pPanel)
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formDisplay.formID;
			this.Load_Parameters();
			formDisplay.panel = pPanel;
			this.DisplayItem(formDisplay.panel.SelectedItem);
		}

		public void DisplayItem(LedItem pItem)
		{
			if (pItem == null)
			{
				return;
			}
			for (int i = 0; i < pItem.Subareas.Count; i++)
			{
				LedSubareaDisplayHolder ledSubareaDisplayHolder = new LedSubareaDisplayHolder(pItem.Subareas[i]);
				this.holderList.Add(ledSubareaDisplayHolder);
				this.Panel_Item.Controls.Add(ledSubareaDisplayHolder);
			}
			this.nowItem = pItem;
			this.nowEdgeDisplay = new EdgeDisplay(pItem.Edge, pItem);
			if (this.nowItem.Edge.Enabled)
			{
				this.timer_ItemEdge.Start();
			}
			formDisplay.zoon = 1m;
		}

		private void ChangeFormSize()
		{
			this.Panel_Item.VerticalScroll.Value = 0;
			this.Panel_Item.HorizontalScroll.Value = 0;
			while (this.threadList.Count > 0)
			{
				this.threadList[0].Abort();
				this.threadList.RemoveAt(0);
			}
			for (int i = 0; i < this.holderList.Count; i++)
			{
				Thread thread = new Thread(new ThreadStart(this.holderList[i].ChangeSize));
				this.threadList.Add(thread);
				thread.Start();
			}
			int num = (int)(formDisplay.panel.Width * formDisplay.zoon);
			int num2 = (int)(formDisplay.panel.Height * formDisplay.zoon);
			this.panel_Panel.Size = new System.Drawing.Size(num, num2);
			this.edgeTop_pictureBox.Size = new System.Drawing.Size(num, (int)(this.nowItem.Edge.Height * formDisplay.zoon));
			this.edgeTop_pictureBox.Location = new System.Drawing.Point(0, 0);
			this.edgeLeft_pictureBox.Size = new System.Drawing.Size((int)(this.nowItem.Edge.Height * formDisplay.zoon), num2);
			this.edgeLeft_pictureBox.Location = new System.Drawing.Point(0, 0);
			this.edgeBottom_pictureBox.Size = new System.Drawing.Size(num, (int)(this.nowItem.Edge.Height * formDisplay.zoon));
			this.edgeBottom_pictureBox.Location = new System.Drawing.Point(0, num2 - (int)(this.nowItem.Edge.Height * formDisplay.zoon));
			this.edgeRight_pictureBox.Size = new System.Drawing.Size((int)(this.nowItem.Edge.Height * formDisplay.zoon), num2);
			this.edgeRight_pictureBox.Location = new System.Drawing.Point(num - (int)(this.nowItem.Edge.Height * formDisplay.zoon), 0);
			if (this.nowItem.Edge.Enabled)
			{
				this.Panel_Item.Size = new System.Drawing.Size(num - (int)(this.nowItem.Edge.Height * formDisplay.zoon), num2 - (int)(this.nowItem.Edge.Height * formDisplay.zoon));
				this.Panel_Item.Location = new System.Drawing.Point((int)(this.nowItem.Edge.Height * formDisplay.zoon), (int)(this.nowItem.Edge.Height * formDisplay.zoon));
				this.EdgeCorner1_pictureBox.Visible = true;
				this.EdgeCorner2_pictureBox.Visible = true;
				this.EdgeCorner3_pictureBox.Visible = true;
				this.EdgeCorner4_pictureBox.Visible = true;
				this.EdgeCorner1_pictureBox.Visible = true;
				this.EdgeCorner2_pictureBox.Visible = true;
				this.EdgeCorner3_pictureBox.Visible = true;
				this.EdgeCorner4_pictureBox.Visible = true;
			}
			else
			{
				this.Panel_Item.Size = new System.Drawing.Size(num, num2);
				this.Panel_Item.Location = new System.Drawing.Point(0, 0);
				this.EdgeCorner1_pictureBox.Visible = false;
				this.EdgeCorner2_pictureBox.Visible = false;
				this.EdgeCorner3_pictureBox.Visible = false;
				this.EdgeCorner4_pictureBox.Visible = false;
				this.EdgeCorner1_pictureBox.Visible = false;
				this.EdgeCorner2_pictureBox.Visible = false;
				this.EdgeCorner3_pictureBox.Visible = false;
				this.EdgeCorner4_pictureBox.Visible = false;
			}
			this.EdgeCorner1_pictureBox.Size = new System.Drawing.Size((int)(this.nowItem.Edge.Height * formDisplay.zoon), (int)(this.nowItem.Edge.Height * formDisplay.zoon));
			this.EdgeCorner2_pictureBox.Size = this.EdgeCorner1_pictureBox.Size;
			this.EdgeCorner3_pictureBox.Size = this.EdgeCorner1_pictureBox.Size;
			this.EdgeCorner4_pictureBox.Size = this.EdgeCorner1_pictureBox.Size;
			this.EdgeCorner1_pictureBox.Location = this.edgeTop_pictureBox.Location;
			this.EdgeCorner2_pictureBox.Location = this.edgeRight_pictureBox.Location;
			this.EdgeCorner3_pictureBox.Location = this.edgeBottom_pictureBox.Location;
			this.EdgeCorner4_pictureBox.Location = new System.Drawing.Point(this.Panel_Item.Size.Width, this.Panel_Item.Size.Height);
			if (this.nowItem.Edge.Enabled)
			{
				this.EdgeCorner1_pictureBox.Visible = true;
				this.EdgeCorner2_pictureBox.Visible = true;
				this.EdgeCorner3_pictureBox.Visible = true;
				this.EdgeCorner4_pictureBox.Visible = true;
				this.edgeTop_pictureBox.Visible = true;
				this.edgeRight_pictureBox.Visible = true;
				this.edgeBottom_pictureBox.Visible = true;
				this.edgeLeft_pictureBox.Visible = true;
			}
			else
			{
				this.EdgeCorner1_pictureBox.Visible = false;
				this.EdgeCorner2_pictureBox.Visible = false;
				this.EdgeCorner3_pictureBox.Visible = false;
				this.EdgeCorner4_pictureBox.Visible = false;
				this.edgeTop_pictureBox.Visible = false;
				this.edgeRight_pictureBox.Visible = false;
				this.edgeBottom_pictureBox.Visible = false;
				this.edgeLeft_pictureBox.Visible = false;
			}
			num += 10;
			num2 += 41;
			if (num + 30 > Screen.PrimaryScreen.WorkingArea.Width)
			{
				num = Screen.PrimaryScreen.WorkingArea.Width;
			}
			if (num2 + 30 > Screen.PrimaryScreen.WorkingArea.Height)
			{
				num2 = Screen.PrimaryScreen.WorkingArea.Height;
			}
			base.Size = new System.Drawing.Size(num, num2);
			base.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - num) / 2, (Screen.PrimaryScreen.WorkingArea.Height - num2) / 2);
		}

		private void close_ToolStripButton_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.holderList.Count; i++)
			{
				this.holderList[i].StopDisplay();
			}
			Thread.Sleep(1000);
			for (int j = this.holderList.Count - 1; j >= 0; j--)
			{
				if (this.holderList[j].t != null)
				{
					this.holderList[j].t.Abort();
				}
				this.holderList[j].Dispose();
			}
			while (this.threadList.Count > 0)
			{
				this.threadList[0].Abort();
				this.threadList.RemoveAt(0);
			}
			base.Dispose();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (formDisplay.zoon == 8m)
			{
				return;
			}
			if (formDisplay.zoon < 1m)
			{
				formDisplay.zoon += 0.1m;
			}
			else
			{
				formDisplay.zoon = ++formDisplay.zoon;
			}
			this.ChangeFormSize();
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			if (formDisplay.zoon == 0.2m)
			{
				return;
			}
			if (formDisplay.zoon > 1m)
			{
				formDisplay.zoon = --formDisplay.zoon;
			}
			else
			{
				formDisplay.zoon -= 0.1m;
			}
			this.ChangeFormSize();
		}

		private void formDisplay_FormClosing(object sender, FormClosingEventArgs e)
		{
			for (int i = this.holderList.Count - 1; i >= 0; i--)
			{
				this.holderList[i].Dispose();
				this.holderList[i].t.Abort();
			}
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			formDisplay.zoon = 1m;
			this.ChangeFormSize();
		}

		private void Panel_Item_Paint(object sender, PaintEventArgs e)
		{
		}

		private void timer_ItemEdge_Tick(object sender, EventArgs e)
		{
			System.Drawing.Bitmap itemEdgeTop = this.nowEdgeDisplay.ItemEdgeTop;
			System.Drawing.Bitmap itemEdgeRight = this.nowEdgeDisplay.ItemEdgeRight;
			System.Drawing.Bitmap itemEdgeBottom = this.nowEdgeDisplay.ItemEdgeBottom;
			System.Drawing.Bitmap itemEdgeLeft = this.nowEdgeDisplay.ItemEdgeLeft;
			this.nowEdgeDisplay.GetNewItemEdge();
			this.edgeTop_pictureBox.Image = LedSubareaDisplayHolder.Griding(itemEdgeTop, formDisplay.zoon);
			this.edgeRight_pictureBox.Image = LedSubareaDisplayHolder.Griding(itemEdgeRight, formDisplay.zoon);
			this.edgeBottom_pictureBox.Image = LedSubareaDisplayHolder.Griding(itemEdgeBottom, formDisplay.zoon);
			this.edgeLeft_pictureBox.Image = LedSubareaDisplayHolder.Griding(itemEdgeLeft, formDisplay.zoon);
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.nowItem.Edge.Height, this.nowItem.Edge.Height);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
			graphics.Clear(System.Drawing.Color.Transparent);
			graphics.DrawImage(this.nowEdgeDisplay.ItemEdgeTop, new System.Drawing.Point(0, 0));
			graphics.DrawImage(this.nowEdgeDisplay.ItemEdgeLeft, new System.Drawing.Point(0, 0));
			this.EdgeCorner1_pictureBox.Image = LedSubareaDisplayHolder.Griding(bitmap, formDisplay.zoon);
			System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(this.nowItem.Edge.Height, this.nowItem.Edge.Height);
			graphics = System.Drawing.Graphics.FromImage(bitmap2);
			graphics.Clear(System.Drawing.Color.Transparent);
			graphics.DrawImage(this.nowEdgeDisplay.ItemEdgeRight, new System.Drawing.Point(0, 0));
			graphics.DrawImage(this.nowEdgeDisplay.ItemEdgeTop, new System.Drawing.Rectangle(0, 0, bitmap2.Width, bitmap2.Height), new System.Drawing.Rectangle(formDisplay.panel.Width - bitmap2.Width, 0, bitmap2.Width, bitmap2.Width), System.Drawing.GraphicsUnit.Pixel);
			this.EdgeCorner2_pictureBox.Image = LedSubareaDisplayHolder.Griding(bitmap2, formDisplay.zoon);
			System.Drawing.Bitmap bitmap3 = new System.Drawing.Bitmap(this.nowItem.Edge.Height, this.nowItem.Edge.Height);
			graphics = System.Drawing.Graphics.FromImage(bitmap3);
			graphics.Clear(System.Drawing.Color.Transparent);
			graphics.DrawImage(this.nowEdgeDisplay.ItemEdgeBottom, new System.Drawing.Point(0, 0));
			graphics.DrawImage(this.nowEdgeDisplay.ItemEdgeLeft, new System.Drawing.Rectangle(0, 0, bitmap3.Width, bitmap3.Height), new System.Drawing.Rectangle(0, formDisplay.panel.Height - bitmap3.Width, bitmap3.Width, bitmap3.Width), System.Drawing.GraphicsUnit.Pixel);
			this.EdgeCorner3_pictureBox.Image = LedSubareaDisplayHolder.Griding(bitmap3, formDisplay.zoon);
			System.Drawing.Bitmap bitmap4 = new System.Drawing.Bitmap(this.nowItem.Edge.Height, this.nowItem.Edge.Height);
			graphics = System.Drawing.Graphics.FromImage(bitmap4);
			graphics.Clear(System.Drawing.Color.Transparent);
			graphics.DrawImage(this.nowEdgeDisplay.ItemEdgeBottom, new System.Drawing.Rectangle(0, 0, bitmap4.Width, bitmap4.Width), new System.Drawing.Rectangle(formDisplay.panel.Width - bitmap4.Width, 0, bitmap4.Width, bitmap4.Width), System.Drawing.GraphicsUnit.Pixel);
			graphics.DrawImage(this.nowEdgeDisplay.ItemEdgeRight, new System.Drawing.Rectangle(0, 0, bitmap4.Width, bitmap4.Height), new System.Drawing.Rectangle(0, formDisplay.panel.Height - bitmap4.Width, bitmap4.Width, bitmap4.Width), System.Drawing.GraphicsUnit.Pixel);
			this.EdgeCorner4_pictureBox.Image = LedSubareaDisplayHolder.Griding(bitmap4, formDisplay.zoon);
		}

		private void formDisplay_Load(object sender, EventArgs e)
		{
			this.ChangeFormSize();
			base.Icon = Resources.AppIcon;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formDisplay));
			this.panel_Border = new Panel();
			this.panel_Panel = new Panel();
			this.EdgeCorner4_pictureBox = new PictureBox();
			this.EdgeCorner3_pictureBox = new PictureBox();
			this.EdgeCorner2_pictureBox = new PictureBox();
			this.EdgeCorner1_pictureBox = new PictureBox();
			this.edgeLeft_pictureBox = new PictureBox();
			this.edgeRight_pictureBox = new PictureBox();
			this.edgeBottom_pictureBox = new PictureBox();
			this.Panel_Item = new Panel();
			this.edgeTop_pictureBox = new PictureBox();
			this.toolStrip1 = new ToolStrip();
			this.toolStripButton1 = new ToolStripButton();
			this.toolStripButton2 = new ToolStripButton();
			this.toolStripButton3 = new ToolStripButton();
			this.close_ToolStripButton = new ToolStripButton();
			this.timer_ItemEdge = new System.Windows.Forms.Timer(this.components);
			this.panel_Border.SuspendLayout();
			this.panel_Panel.SuspendLayout();
			((ISupportInitialize)this.EdgeCorner4_pictureBox).BeginInit();
			((ISupportInitialize)this.EdgeCorner3_pictureBox).BeginInit();
			((ISupportInitialize)this.EdgeCorner2_pictureBox).BeginInit();
			((ISupportInitialize)this.EdgeCorner1_pictureBox).BeginInit();
			((ISupportInitialize)this.edgeLeft_pictureBox).BeginInit();
			((ISupportInitialize)this.edgeRight_pictureBox).BeginInit();
			((ISupportInitialize)this.edgeBottom_pictureBox).BeginInit();
			((ISupportInitialize)this.edgeTop_pictureBox).BeginInit();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.panel_Border.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel_Border.AutoScroll = true;
			this.panel_Border.BackColor = System.Drawing.Color.FromArgb(100, 30, 0);
			this.panel_Border.Controls.Add(this.panel_Panel);
			this.panel_Border.Location = new System.Drawing.Point(0, 0);
			this.panel_Border.Name = "panel_Border";
			this.panel_Border.Size = new System.Drawing.Size(510, 210);
			this.panel_Border.TabIndex = 0;
			this.panel_Panel.AutoScroll = true;
			this.panel_Panel.BackColor = System.Drawing.Color.Black;
			this.panel_Panel.Controls.Add(this.EdgeCorner4_pictureBox);
			this.panel_Panel.Controls.Add(this.EdgeCorner3_pictureBox);
			this.panel_Panel.Controls.Add(this.EdgeCorner2_pictureBox);
			this.panel_Panel.Controls.Add(this.EdgeCorner1_pictureBox);
			this.panel_Panel.Controls.Add(this.edgeLeft_pictureBox);
			this.panel_Panel.Controls.Add(this.edgeRight_pictureBox);
			this.panel_Panel.Controls.Add(this.edgeBottom_pictureBox);
			this.panel_Panel.Controls.Add(this.Panel_Item);
			this.panel_Panel.Controls.Add(this.edgeTop_pictureBox);
			this.panel_Panel.Location = new System.Drawing.Point(5, 5);
			this.panel_Panel.Name = "panel_Panel";
			this.panel_Panel.Size = new System.Drawing.Size(500, 199);
			this.panel_Panel.TabIndex = 2;
			this.EdgeCorner4_pictureBox.Location = new System.Drawing.Point(431, 147);
			this.EdgeCorner4_pictureBox.Name = "EdgeCorner4_pictureBox";
			this.EdgeCorner4_pictureBox.Size = new System.Drawing.Size(32, 26);
			this.EdgeCorner4_pictureBox.TabIndex = 8;
			this.EdgeCorner4_pictureBox.TabStop = false;
			this.EdgeCorner3_pictureBox.Location = new System.Drawing.Point(383, 147);
			this.EdgeCorner3_pictureBox.Name = "EdgeCorner3_pictureBox";
			this.EdgeCorner3_pictureBox.Size = new System.Drawing.Size(32, 26);
			this.EdgeCorner3_pictureBox.TabIndex = 7;
			this.EdgeCorner3_pictureBox.TabStop = false;
			this.EdgeCorner2_pictureBox.Location = new System.Drawing.Point(431, 98);
			this.EdgeCorner2_pictureBox.Name = "EdgeCorner2_pictureBox";
			this.EdgeCorner2_pictureBox.Size = new System.Drawing.Size(32, 26);
			this.EdgeCorner2_pictureBox.TabIndex = 6;
			this.EdgeCorner2_pictureBox.TabStop = false;
			this.EdgeCorner1_pictureBox.Location = new System.Drawing.Point(383, 98);
			this.EdgeCorner1_pictureBox.Name = "EdgeCorner1_pictureBox";
			this.EdgeCorner1_pictureBox.Size = new System.Drawing.Size(32, 26);
			this.EdgeCorner1_pictureBox.TabIndex = 5;
			this.EdgeCorner1_pictureBox.TabStop = false;
			this.edgeLeft_pictureBox.BackColor = System.Drawing.Color.Transparent;
			this.edgeLeft_pictureBox.Location = new System.Drawing.Point(7, 39);
			this.edgeLeft_pictureBox.Name = "edgeLeft_pictureBox";
			this.edgeLeft_pictureBox.Size = new System.Drawing.Size(10, 110);
			this.edgeLeft_pictureBox.TabIndex = 2;
			this.edgeLeft_pictureBox.TabStop = false;
			this.edgeRight_pictureBox.BackColor = System.Drawing.Color.Transparent;
			this.edgeRight_pictureBox.Location = new System.Drawing.Point(483, 27);
			this.edgeRight_pictureBox.Name = "edgeRight_pictureBox";
			this.edgeRight_pictureBox.Size = new System.Drawing.Size(10, 134);
			this.edgeRight_pictureBox.TabIndex = 4;
			this.edgeRight_pictureBox.TabStop = false;
			this.edgeBottom_pictureBox.BackColor = System.Drawing.Color.Transparent;
			this.edgeBottom_pictureBox.Location = new System.Drawing.Point(142, 170);
			this.edgeBottom_pictureBox.Name = "edgeBottom_pictureBox";
			this.edgeBottom_pictureBox.Size = new System.Drawing.Size(178, 10);
			this.edgeBottom_pictureBox.TabIndex = 3;
			this.edgeBottom_pictureBox.TabStop = false;
			this.Panel_Item.BackColor = System.Drawing.Color.DimGray;
			this.Panel_Item.Location = new System.Drawing.Point(142, 39);
			this.Panel_Item.Name = "Panel_Item";
			this.Panel_Item.Size = new System.Drawing.Size(208, 93);
			this.Panel_Item.TabIndex = 0;
			this.Panel_Item.Paint += new PaintEventHandler(this.Panel_Item_Paint);
			this.edgeTop_pictureBox.BackColor = System.Drawing.Color.Transparent;
			this.edgeTop_pictureBox.Location = new System.Drawing.Point(160, 7);
			this.edgeTop_pictureBox.Name = "edgeTop_pictureBox";
			this.edgeTop_pictureBox.Size = new System.Drawing.Size(178, 10);
			this.edgeTop_pictureBox.TabIndex = 1;
			this.edgeTop_pictureBox.TabStop = false;
			this.toolStrip1.Dock = DockStyle.Bottom;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripButton1,
				this.toolStripButton2,
				this.toolStripButton3,
				this.close_ToolStripButton
			});
			this.toolStrip1.Location = new System.Drawing.Point(0, 207);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(510, 31);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = (System.Drawing.Image)componentResourceManager.GetObject("toolStripButton1.Image");
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
			this.toolStripButton1.Text = "放大";
			this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
			this.toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = (System.Drawing.Image)componentResourceManager.GetObject("toolStripButton2.Image");
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(28, 28);
			this.toolStripButton2.Text = "复原";
			this.toolStripButton2.Click += new EventHandler(this.toolStripButton2_Click);
			this.toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = (System.Drawing.Image)componentResourceManager.GetObject("toolStripButton3.Image");
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(28, 28);
			this.toolStripButton3.Text = "缩小";
			this.toolStripButton3.Click += new EventHandler(this.toolStripButton3_Click);
			this.close_ToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.close_ToolStripButton.Image = (System.Drawing.Image)componentResourceManager.GetObject("close_ToolStripButton.Image");
			this.close_ToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.close_ToolStripButton.Name = "close_ToolStripButton";
			this.close_ToolStripButton.Size = new System.Drawing.Size(28, 28);
			this.close_ToolStripButton.Text = "关闭";
			this.close_ToolStripButton.Click += new EventHandler(this.close_ToolStripButton_Click);
			this.timer_ItemEdge.Interval = 42;
			this.timer_ItemEdge.Tick += new EventHandler(this.timer_ItemEdge_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(510, 238);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.panel_Border);
			base.FormBorderStyle = FormBorderStyle.None;
			base.Name = "formDisplay";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "预览";
			base.Load += new EventHandler(this.formDisplay_Load);
			base.FormClosing += new FormClosingEventHandler(this.formDisplay_FormClosing);
			this.panel_Border.ResumeLayout(false);
			this.panel_Panel.ResumeLayout(false);
			((ISupportInitialize)this.EdgeCorner4_pictureBox).EndInit();
			((ISupportInitialize)this.EdgeCorner3_pictureBox).EndInit();
			((ISupportInitialize)this.EdgeCorner2_pictureBox).EndInit();
			((ISupportInitialize)this.EdgeCorner1_pictureBox).EndInit();
			((ISupportInitialize)this.edgeLeft_pictureBox).EndInit();
			((ISupportInitialize)this.edgeRight_pictureBox).EndInit();
			((ISupportInitialize)this.edgeBottom_pictureBox).EndInit();
			((ISupportInitialize)this.edgeTop_pictureBox).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
