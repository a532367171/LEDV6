using LedControlSystem.Properties;
using LedModel.Content;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formAnimation : Form
	{
		private static string formID = "formAnimation";

		private AnimationMaker animationMaker;

		private LedAnimation animation;

		private System.Drawing.Size panelSize;

		private IContainer components;

		private Panel panel1;

		private AnimationEditor animationEditor1;

		private AnimationEffect animationEffect1;

		private GroupBox zhGroupBox1;

		private GroupBox zhGroupBox2;

		public static string FormID
		{
			get
			{
				return formAnimation.formID;
			}
			set
			{
				formAnimation.formID = value;
			}
		}

		public formAnimation()
		{
			this.InitializeComponent();
			this.animationEditor1.Event += new AnimationEvent(this.animationEditor1_Event);
			this.animationEffect1.Event += new AnimationEvent(this.animationEditor1_Event);
			formMain.ML.NowFormID = formAnimation.formID;
			this.Text = formMain.ML.GetStr("formAnimation_FormText");
			this.zhGroupBox2.Text = formMain.ML.GetStr("formAnimation_GroupBox_AnimationEffects");
			Console.WriteLine(this.animationEffect1.Controls.Count);
			this.animationEffect1.Controls["Animation_label1"].Text = formMain.ML.GetStr("formAnimation_Animation_label_effect");
			this.animationEffect1.Controls["Animation_label_wordspace"].Text = formMain.ML.GetStr("formAnimation_Animation_label_wordspace");
			this.animationEffect1.Controls["Animation_label_Speed"].Text = formMain.ML.GetStr("formAnimation_Animation_label_Speed");
			this.animationEffect1.Controls["Animation_label6_DisplayTimes"].Text = formMain.ML.GetStr("formAnimation_Animation_label_DisplayTimes");
			this.animationEditor1.Controls["startPreviewButton"].Text = formMain.ML.GetStr("formAnimation_Button_startPreview");
			this.zhGroupBox1.Text = formMain.ML.GetStr("formAnimation_GroupBox_AnimationSettings");
		}

		public void Edit(System.Drawing.Size pSize, LedAnimation pAnimation)
		{
			this.animationMaker = new AnimationMaker(pSize, pAnimation);
			int x = (this.panel1.Width - this.animationMaker.Width) / 2;
			int y = (this.panel1.Height - this.animationMaker.Height) / 2;
			this.animationMaker.Location = new System.Drawing.Point(x, y);
			this.animationMaker.Parent = this.panel1;
			this.animation = pAnimation;
			this.panelSize = pSize;
			this.animationEditor1.Edit(pAnimation);
			this.animationEditor1.AllwaysPreview();
			this.animationEffect1.Edit(pAnimation);
			base.ShowDialog();
		}

		private void animationEditor1_Event(AnimationEventType pType)
		{
			switch (pType)
			{
			case AnimationEventType.ChangeEffect:
			{
				this.StopPreview();
				this.animationMaker = new AnimationMaker(this.panelSize, this.animation);
				int x = (this.panel1.Width - this.animationMaker.Width) / 2;
				int y = (this.panel1.Height - this.animationMaker.Height) / 2;
				this.animationMaker.Location = new System.Drawing.Point(x, y);
				this.animationMaker.Parent = this.panel1;
				this.animationMaker.ChangeEffect();
				return;
			}
			case AnimationEventType.Update:
				this.animationMaker.RefrshAnimation(false);
				return;
			case AnimationEventType.StartPreview:
			case AnimationEventType.StopPreview:
				break;
			case AnimationEventType.ReDraw:
				this.animationMaker.RefrshAnimation(false);
				break;
			default:
				return;
			}
		}

		private void StopPreview()
		{
			try
			{
				this.animationMaker.Dis();
				this.animationMaker.Dispose();
			}
			catch
			{
			}
		}

		private void formAnimation_SizeChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.animationMaker != null)
				{
					int x = (this.panel1.Width - this.animationMaker.Width) / 2;
					int y = (this.panel1.Height - this.animationMaker.Height) / 2;
					this.animationMaker.Location = new System.Drawing.Point(x, y);
				}
			}
			catch
			{
			}
		}

		private void formAnimation_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
		}

		private void formAnimation_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.StopPreview();
			this.animation = null;
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
			this.panel1 = new Panel();
			this.animationEffect1 = new AnimationEffect();
			this.animationEditor1 = new AnimationEditor();
			this.zhGroupBox1 = new GroupBox();
			this.zhGroupBox2 = new GroupBox();
			this.zhGroupBox1.SuspendLayout();
			this.zhGroupBox2.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel1.BackColor = System.Drawing.Color.Gray;
			this.panel1.Location = new System.Drawing.Point(1, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1013, 340);
			this.panel1.TabIndex = 21;
			this.animationEffect1.Location = new System.Drawing.Point(16, 20);
			this.animationEffect1.Name = "animationEffect1";
			this.animationEffect1.Size = new System.Drawing.Size(122, 175);
			this.animationEffect1.TabIndex = 0;
			this.animationEditor1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.animationEditor1.BackColor = System.Drawing.Color.Transparent;
			this.animationEditor1.Location = new System.Drawing.Point(8, 12);
			this.animationEditor1.Name = "animationEditor1";
			this.animationEditor1.Size = new System.Drawing.Size(835, 204);
			this.animationEditor1.TabIndex = 0;
			this.zhGroupBox1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.zhGroupBox1.Controls.Add(this.animationEditor1);
			this.zhGroupBox1.Location = new System.Drawing.Point(1, 346);
			this.zhGroupBox1.Name = "zhGroupBox1";
			this.zhGroupBox1.Size = new System.Drawing.Size(849, 223);
			this.zhGroupBox1.TabIndex = 24;
			this.zhGroupBox1.TabStop = false;
			this.zhGroupBox1.Text = "动画设置";
			this.zhGroupBox2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.zhGroupBox2.Controls.Add(this.animationEffect1);
			this.zhGroupBox2.Location = new System.Drawing.Point(856, 346);
			this.zhGroupBox2.Name = "zhGroupBox2";
			this.zhGroupBox2.Size = new System.Drawing.Size(158, 223);
			this.zhGroupBox2.TabIndex = 25;
			this.zhGroupBox2.TabStop = false;
			this.zhGroupBox2.Text = "动画效果";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1024, 578);
			base.Controls.Add(this.zhGroupBox2);
			base.Controls.Add(this.zhGroupBox1);
			base.Controls.Add(this.panel1);
			base.Name = "formAnimation";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "动画编辑";
			base.FormClosing += new FormClosingEventHandler(this.formAnimation_FormClosing);
			base.Load += new EventHandler(this.formAnimation_Load);
			base.SizeChanged += new EventHandler(this.formAnimation_SizeChanged);
			this.zhGroupBox1.ResumeLayout(false);
			this.zhGroupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
