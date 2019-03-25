using LedControlSystem.Properties;
using LedModel.Content;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formAnimationMaker : Form
	{
		private AnimationMaker am;

		private int countDown = 5;

		private LedBackground ledbackground;

		private System.Drawing.Size backGroundSize;

		private static string formID = "formAnimationMaker";

		public static int FrameRate = Settings.Default.FrameRate;

		private bool isBackGround;

		private LedAnimation man;

		private bool needtoClose;

		private bool statared;

		private IContainer components;

		private Timer timer1;

		private Panel panel1;

		public static string FormID
		{
			get
			{
				return formAnimationMaker.formID;
			}
			set
			{
				formAnimationMaker.formID = value;
			}
		}

		public formAnimationMaker()
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formAnimationMaker_FormText");
		}

		public void Make(System.Drawing.Size pSize, LedAnimation pAnimation)
		{
			int num = base.Size.Width - this.panel1.Width;
			int num2 = base.Size.Height - this.panel1.Height;
			this.man = pAnimation;
			this.timer1.Interval = 1000 / formAnimationMaker.FrameRate;
			this.am = new AnimationMaker(pSize, pAnimation);
			this.am.ChangeEffect();
			this.am.Size = pSize;
			this.am.Dock = DockStyle.Fill;
			this.am.Event += new AnimationEvent(this.am_Event);
			this.panel1.Controls.Add(this.am);
			this.am.Location = new System.Drawing.Point(0, 0);
			this.timer1.Start();
			base.Size = new System.Drawing.Size(pSize.Width + num, pSize.Height + num2);
			this.am.Location = new System.Drawing.Point(0, 0);
			base.ShowDialog();
			this.CantClose();
		}

		private void CantClose()
		{
		}

		private void am_Event(AnimationEventType pType)
		{
			if (pType == AnimationEventType.Stop)
			{
				this.needtoClose = true;
			}
		}

		public void Make(System.Drawing.Size pSize, LedBackground pBackground)
		{
			int num = base.Size.Width - this.panel1.Width;
			int num2 = base.Size.Height - this.panel1.Height;
			this.backGroundSize = pSize;
			this.ledbackground = pBackground;
			this.isBackGround = true;
			this.timer1.Interval = 1000 / formAnimationMaker.FrameRate;
			this.am = new AnimationMaker(pSize, pBackground);
			if (pSize.Width > 1000)
			{
				pSize.Width = 1000;
			}
			if (pBackground.MaterialType == LedMaterialType.Flash)
			{
				this.am.changeMaterial();
			}
			else
			{
				this.am.ChangeEffect();
			}
			this.am.Size = pSize;
			this.am.Event += new AnimationEvent(this.am_Event);
			this.panel1.Controls.Add(this.am);
			this.am.Location = new System.Drawing.Point(0, 0);
			this.timer1.Start();
			base.Size = new System.Drawing.Size(pSize.Width + num, pSize.Height + num2);
			this.am.Location = new System.Drawing.Point(0, 0);
			this.CantClose();
			this.countDown = formAnimationMaker.FrameRate * 3;
			base.ShowDialog();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.needtoClose)
			{
				this.timer1.Stop();
				this.am.Dis();
				base.Dispose();
				return;
			}
			if (!this.isBackGround)
			{
				if (!this.statared)
				{
					this.am.Clear();
					this.am.RefrshAnimation(true);
					this.statared = true;
				}
				this.am.Make();
				return;
			}
			if (this.countDown > 0)
			{
				this.countDown--;
				return;
			}
			if (this.am.GetCount > 6 * formAnimationMaker.FrameRate)
			{
				this.timer1.Stop();
				this.am.Dis();
				base.Dispose();
				return;
			}
			this.am.Make();
		}

		private void formAnimationMaker_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.Text = formMain.ML.GetStr("Prompt_MakingAnimation");
			this.CantClose();
		}

		private void formAnimationMaker_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
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
			this.timer1 = new Timer(this.components);
			this.panel1 = new Panel();
			base.SuspendLayout();
			this.timer1.Interval = 42;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(613, 253);
			this.panel1.TabIndex = 23;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(613, 253);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formAnimationMaker";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "正在生成动画";
			base.TopMost = true;
			base.FormClosing += new FormClosingEventHandler(this.formAnimationMaker_FormClosing);
			base.Load += new EventHandler(this.formAnimationMaker_Load);
			base.ResumeLayout(false);
		}
	}
}
