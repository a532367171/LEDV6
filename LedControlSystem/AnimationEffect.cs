using LedModel.Content;
using LedModel.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class AnimationEffect : UserControl
	{
		private LedAnimation animation;

		private IContainer components;

		private Label label2;

		private Label Animation_label1;

		private ComboBox effectNameComboBox;

		private NumericUpDown charSpaceNumericUpDown;

		private Label Animation_label_wordspace;

		private Label label4;

		private Label label5;

		private NumericUpDown ySpaceNumericUpDown;

		private NumericUpDown xSpaceNumericUpDown;

		private NumericUpDown alphaNumericUpDown1;

		private NumericUpDown loopNumericUpDown;

		private Label Animation_label6_DisplayTimes;

		private Label Animation_label_Speed;

		private ComboBox speedComboBox;

		public event AnimationEvent Event;

		public AnimationEffect()
		{
			this.InitializeComponent();
		}

		public void Edit(LedAnimation pAnimation)
		{
			this.animation = pAnimation;
			this.LoadParam();
		}

		private void LoadParam()
		{
			try
			{
				this.effectNameComboBox.Text = this.animation.AnimationEffectsSetting.Name;
				this.alphaNumericUpDown1.Value = this.animation.EAnimation.Alpha;
				this.charSpaceNumericUpDown.Value = this.animation.AnimationEffectsSetting.Kerning;
				this.xSpaceNumericUpDown.Value = this.animation.EAnimation.HorizontalOffset;
				this.ySpaceNumericUpDown.Value = this.animation.EAnimation.VerticalOffset;
				this.loopNumericUpDown.Value = this.animation.EffectsSetting.LoopCount;
				this.speedComboBox.SelectedIndex = this.animation.EffectsSetting.StayIndex;
			}
			catch
			{
			}
		}

		private void RefreshAnimation()
		{
			this.animation.Changed = true;
			this.Event(AnimationEventType.Update);
		}

		private void effectNameComboBox_Click(object sender, EventArgs e)
		{
			formEffectSelecter formEffectSelecter = new formEffectSelecter();
			formEffectSelecter.Size = new System.Drawing.Size(276, 658);
			string text = formEffectSelecter.SelectEffect(LedEffectsType.Text, this.animation.AnimationEffectsSetting.Name);
			formEffectSelecter.Dispose();
			this.effectNameComboBox.Text = text;
			this.animation.AnimationEffectsSetting.Name = text;
			this.Event(AnimationEventType.ChangeEffect);
			this.RefreshAnimation();
		}

		private void charSpaceNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (!numericUpDown.Focused)
			{
				return;
			}
			this.animation.AnimationEffectsSetting.Kerning = (int)numericUpDown.Value;
			this.RefreshAnimation();
		}

		private void ySpaceNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (!numericUpDown.Focused)
			{
				return;
			}
			this.animation.EAnimation.VerticalOffset = (int)numericUpDown.Value;
			this.RefreshAnimation();
		}

		private void xSpaceNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (!numericUpDown.Focused)
			{
				return;
			}
			this.animation.EAnimation.HorizontalOffset = (int)numericUpDown.Value;
			this.RefreshAnimation();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (!numericUpDown.Focused)
			{
				return;
			}
			this.animation.EAnimation.Alpha = (int)numericUpDown.Value;
			this.RefreshAnimation();
		}

		private void AnimationEffect_Load(object sender, EventArgs e)
		{
		}

		private void loopNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (!numericUpDown.Focused)
			{
				return;
			}
			this.animation.EffectsSetting.LoopCount = (int)numericUpDown.Value;
		}

		private void speedComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			int stay = (int)(Convert.ToSingle(comboBox.Text.Remove(comboBox.Text.Length - 1)) * 1000f);
			this.animation.EffectsSetting.Stay = stay;
			this.animation.EffectsSetting.StayIndex = comboBox.SelectedIndex;
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
            this.label2 = new System.Windows.Forms.Label();
            this.Animation_label1 = new System.Windows.Forms.Label();
            this.effectNameComboBox = new System.Windows.Forms.ComboBox();
            this.charSpaceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Animation_label_wordspace = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ySpaceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.xSpaceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.alphaNumericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.loopNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Animation_label6_DisplayTimes = new System.Windows.Forms.Label();
            this.Animation_label_Speed = new System.Windows.Forms.Label();
            this.speedComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.charSpaceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ySpaceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xSpaceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaNumericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "亮度";
            this.label2.Visible = false;
            // 
            // Animation_label1
            // 
            this.Animation_label1.Location = new System.Drawing.Point(4, 10);
            this.Animation_label1.Name = "Animation_label1";
            this.Animation_label1.Size = new System.Drawing.Size(41, 44);
            this.Animation_label1.TabIndex = 6;
            this.Animation_label1.Text = "效果";
            this.Animation_label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // effectNameComboBox
            // 
            this.effectNameComboBox.FormattingEnabled = true;
            this.effectNameComboBox.Location = new System.Drawing.Point(47, 21);
            this.effectNameComboBox.Name = "effectNameComboBox";
            this.effectNameComboBox.Size = new System.Drawing.Size(72, 20);
            this.effectNameComboBox.TabIndex = 5;
            this.effectNameComboBox.Click += new System.EventHandler(this.effectNameComboBox_Click);
            // 
            // charSpaceNumericUpDown
            // 
            this.charSpaceNumericUpDown.Location = new System.Drawing.Point(72, 65);
            this.charSpaceNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.charSpaceNumericUpDown.Name = "charSpaceNumericUpDown";
            this.charSpaceNumericUpDown.Size = new System.Drawing.Size(47, 21);
            this.charSpaceNumericUpDown.TabIndex = 9;
            this.charSpaceNumericUpDown.ValueChanged += new System.EventHandler(this.charSpaceNumericUpDown_ValueChanged);
            // 
            // Animation_label_wordspace
            // 
            this.Animation_label_wordspace.AutoSize = true;
            this.Animation_label_wordspace.Location = new System.Drawing.Point(4, 67);
            this.Animation_label_wordspace.Name = "Animation_label_wordspace";
            this.Animation_label_wordspace.Size = new System.Drawing.Size(41, 12);
            this.Animation_label_wordspace.TabIndex = 10;
            this.Animation_label_wordspace.Text = "字间距";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "上下偏移";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(125, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "左右偏移";
            this.label5.Visible = false;
            // 
            // ySpaceNumericUpDown
            // 
            this.ySpaceNumericUpDown.Location = new System.Drawing.Point(109, 89);
            this.ySpaceNumericUpDown.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.ySpaceNumericUpDown.Name = "ySpaceNumericUpDown";
            this.ySpaceNumericUpDown.Size = new System.Drawing.Size(10, 21);
            this.ySpaceNumericUpDown.TabIndex = 13;
            this.ySpaceNumericUpDown.Visible = false;
            this.ySpaceNumericUpDown.ValueChanged += new System.EventHandler(this.ySpaceNumericUpDown_ValueChanged);
            // 
            // xSpaceNumericUpDown
            // 
            this.xSpaceNumericUpDown.Location = new System.Drawing.Point(109, 135);
            this.xSpaceNumericUpDown.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.xSpaceNumericUpDown.Name = "xSpaceNumericUpDown";
            this.xSpaceNumericUpDown.Size = new System.Drawing.Size(10, 21);
            this.xSpaceNumericUpDown.TabIndex = 14;
            this.xSpaceNumericUpDown.Visible = false;
            this.xSpaceNumericUpDown.ValueChanged += new System.EventHandler(this.xSpaceNumericUpDown_ValueChanged);
            // 
            // alphaNumericUpDown1
            // 
            this.alphaNumericUpDown1.Location = new System.Drawing.Point(127, 21);
            this.alphaNumericUpDown1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.alphaNumericUpDown1.Name = "alphaNumericUpDown1";
            this.alphaNumericUpDown1.Size = new System.Drawing.Size(10, 21);
            this.alphaNumericUpDown1.TabIndex = 15;
            this.alphaNumericUpDown1.Visible = false;
            this.alphaNumericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // loopNumericUpDown
            // 
            this.loopNumericUpDown.Location = new System.Drawing.Point(72, 134);
            this.loopNumericUpDown.Name = "loopNumericUpDown";
            this.loopNumericUpDown.Size = new System.Drawing.Size(47, 21);
            this.loopNumericUpDown.TabIndex = 17;
            this.loopNumericUpDown.ValueChanged += new System.EventHandler(this.loopNumericUpDown_ValueChanged);
            // 
            // Animation_label6_DisplayTimes
            // 
            this.Animation_label6_DisplayTimes.AutoSize = true;
            this.Animation_label6_DisplayTimes.Location = new System.Drawing.Point(4, 138);
            this.Animation_label6_DisplayTimes.Name = "Animation_label6_DisplayTimes";
            this.Animation_label6_DisplayTimes.Size = new System.Drawing.Size(53, 12);
            this.Animation_label6_DisplayTimes.TabIndex = 16;
            this.Animation_label6_DisplayTimes.Text = "播放次数";
            // 
            // Animation_label_Speed
            // 
            this.Animation_label_Speed.AutoSize = true;
            this.Animation_label_Speed.Location = new System.Drawing.Point(4, 107);
            this.Animation_label_Speed.Name = "Animation_label_Speed";
            this.Animation_label_Speed.Size = new System.Drawing.Size(29, 12);
            this.Animation_label_Speed.TabIndex = 18;
            this.Animation_label_Speed.Text = "速度";
            // 
            // speedComboBox
            // 
            this.speedComboBox.FormattingEnabled = true;
            this.speedComboBox.Items.AddRange(new object[] {
            "0.000s",
            "0.010s",
            "0.015s",
            "0.020s",
            "0.030s",
            "0.040s",
            "0.050s",
            "0.080s",
            "0.1s",
            "0.2s",
            "0.5s",
            "0.8s",
            "1s",
            "2s",
            "3s",
            "5s",
            "6s",
            "7s",
            "8s",
            "9s",
            "10s",
            "11s",
            "13s",
            "14s",
            "15s",
            "16s",
            "17s",
            "18s",
            "19s",
            "20s",
            "21s",
            "22s",
            "23s",
            "25s",
            "30s",
            "35s",
            "40s",
            "45s",
            "50s",
            "60s",
            "70s",
            "80s",
            "90s",
            "100s",
            "120s",
            "150s",
            "200s",
            "500s"});
            this.speedComboBox.Location = new System.Drawing.Point(72, 90);
            this.speedComboBox.Name = "speedComboBox";
            this.speedComboBox.Size = new System.Drawing.Size(47, 20);
            this.speedComboBox.TabIndex = 19;
            this.speedComboBox.SelectedIndexChanged += new System.EventHandler(this.speedComboBox_SelectedIndexChanged);
            // 
            // AnimationEffect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.charSpaceNumericUpDown);
            this.Controls.Add(this.effectNameComboBox);
            this.Controls.Add(this.speedComboBox);
            this.Controls.Add(this.Animation_label_Speed);
            this.Controls.Add(this.loopNumericUpDown);
            this.Controls.Add(this.Animation_label6_DisplayTimes);
            this.Controls.Add(this.alphaNumericUpDown1);
            this.Controls.Add(this.xSpaceNumericUpDown);
            this.Controls.Add(this.ySpaceNumericUpDown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Animation_label_wordspace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Animation_label1);
            this.Name = "AnimationEffect";
            this.Size = new System.Drawing.Size(197, 178);
            this.Load += new System.EventHandler(this.AnimationEffect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.charSpaceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ySpaceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xSpaceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaNumericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loopNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
