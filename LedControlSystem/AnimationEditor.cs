using LedControlSystem.Fonts;
using LedModel.Const;
using LedModel.Content;
using LedModel.Element.PictureText;
using LedModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class AnimationEditor : UserControl
	{
		private LedAnimation animation;

		public bool isAlways;

		private bool isPreview;

		private bool isFontDone;

		private bool isLoadingParam;

		private IContainer components;

		private Button btnFontUnderline;

		private Button btnFontItalic;

		private Button btnFontBold;

		private ComboBox cmbFontSize;

		private ComboBox cmbFontName;

		private Button textColor;

		private Button backColor;

		private ColorDialog colorDialog1;

		private Button startPreviewButton;

		private RichTextBox rtxContent;

		private ComboBox cmbColor;

		private ComboBox cmbBackColor;

		public event AnimationEvent Event;

		public RichTextBox Content
		{
			get
			{
				return this.rtxContent;
			}
			set
			{
				this.rtxContent = value;
			}
		}

		public AnimationEditor()
		{
			this.InitializeComponent();
		}

		public void Edit(LedAnimation pAnimation)
		{
			this.textColor.Enabled = formMain.GetColorComboBoxEnable(true);
			this.backColor.Enabled = formMain.GetColorComboBoxEnable(true);
			this.animation = pAnimation;
			this.LoadParam();
		}

		public void AllwaysPreview()
		{
			this.isAlways = true;
			this.isPreview = true;
			this.startPreviewButton.Visible = false;
		}

		private void LoadParam()
		{
			this.isLoadingParam = true;
			this.cmbColor.Enabled = formMain.GetColorComboBoxEnable(true);
			try
			{
				this.InitControl();
				this.Binding();
			}
			catch
			{
			}
			this.isLoadingParam = false;
		}

		private void AnimationEditor_Load(object sender, EventArgs e)
		{
			try
			{
				this.InitControl();
				this.Binding();
			}
			catch
			{
			}
		}

		private void InitControl()
		{
			if (!this.isFontDone)
			{
				this.cmbFontName.Items.Clear();
				IList<string> fontFamilies = new FontFamiliesEx().GetFontFamilies();
				if (fontFamilies != null && fontFamilies.Count > 0)
				{
					foreach (string current in fontFamilies)
					{
						this.cmbFontName.Items.Add(current);
					}
				}
				this.isFontDone = true;
			}
			this.cmbColor.Items.Clear();
			LedColorMode colorMode = LedColorMode.R;
			if (formMain.ledsys != null && formMain.ledsys.SelectedPanel != null)
			{
				colorMode = formMain.ledsys.SelectedPanel.ColorMode;
			}
			IList<System.Drawing.Color> colorList = LedColorConst.GetColorList(colorMode);
			foreach (System.Drawing.Color current2 in colorList)
			{
				this.cmbColor.Items.Add(current2);
			}
			this.cmbBackColor.Items.Clear();
			IList<System.Drawing.Color> backColorList = LedColorConst.GetBackColorList(colorMode);
			foreach (System.Drawing.Color current3 in backColorList)
			{
				this.cmbBackColor.Items.Add(current3);
			}
		}

		private void Binding()
		{
			LedEAnimation ledEAnimation = (this.animation != null) ? this.animation.EAnimation : null;
			if (this.cmbFontName.Items.Count > 0)
			{
				int num = -1;
				if (this.animation != null && !string.IsNullOrEmpty(ledEAnimation.Font.FamilyName))
				{
					for (int i = 0; i < this.cmbFontName.Items.Count; i++)
					{
						if (this.cmbFontName.Items[i].ToString() == ledEAnimation.Font.FamilyName)
						{
							num = i;
							break;
						}
					}
				}
				if (num < 0)
				{
					for (int j = 0; j < this.cmbFontName.Items.Count; j++)
					{
						if (this.cmbFontName.Items[j].ToString() == formMain.DefalutForeignTradeFamilyName)
						{
							num = j;
							break;
						}
					}
				}
				this.cmbFontName.SelectedIndex = ((num < 0) ? 0 : num);
			}
			if (this.cmbFontSize.Items.Count > 0)
			{
				if (this.animation != null)
				{
					this.cmbFontSize.Text = ledEAnimation.Font.Size.ToString();
				}
				else
				{
					this.cmbFontSize.SelectedIndex = 0;
				}
			}
			if (this.animation != null && this.animation.EAnimation.Font.Bold)
			{
				this.btnFontBold.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontBold.BackColor = this.BackColor;
			}
			if (this.animation != null && this.animation.EAnimation.Font.Italic)
			{
				this.btnFontItalic.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontItalic.BackColor = this.BackColor;
			}
			if (this.animation != null && ledEAnimation.Font.Underline)
			{
				this.btnFontUnderline.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontUnderline.BackColor = this.BackColor;
			}
			if (this.cmbColor.Items.Count > 0)
			{
				int num2 = 0;
				if (this.animation != null)
				{
					num2 = formMain.FromColorToIndex(ledEAnimation.ForeColor);
					int count = this.cmbColor.Items.Count;
					if (num2 > count - 1)
					{
						num2 = count - 1;
					}
				}
				this.cmbColor.SelectedIndex = num2;
			}
			if (this.animation != null)
			{
				this.rtxContent.Text = ledEAnimation.Text;
				this.rtxContent.SelectAll();
				this.rtxContent.SelectionColor = System.Drawing.Color.Red;
			}
		}

		private void RefreshAnimation()
		{
			this.animation.Changed = true;
			this.Event(AnimationEventType.ReDraw);
		}

		private void cmbFontName_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.animation.EAnimation.Font.FamilyName = comboBox.Text;
			this.RefreshAnimation();
		}

		private void btnFontBold_Click(object sender, EventArgs e)
		{
			if (!this.animation.EAnimation.Font.Bold)
			{
				this.btnFontBold.BackColor = System.Drawing.Color.LightBlue;
				this.animation.EAnimation.Font.Bold = true;
			}
			else
			{
				this.btnFontBold.BackColor = this.BackColor;
				this.animation.EAnimation.Font.Bold = false;
			}
			this.RefreshAnimation();
		}

		private void btnFontItalic_Click(object sender, EventArgs e)
		{
			if (!this.animation.EAnimation.Font.Italic)
			{
				this.btnFontItalic.BackColor = System.Drawing.Color.LightBlue;
				this.animation.EAnimation.Font.Italic = true;
			}
			else
			{
				this.btnFontItalic.BackColor = this.BackColor;
				this.animation.EAnimation.Font.Italic = false;
			}
			this.RefreshAnimation();
		}

		private void btnFontUnderline_Click(object sender, EventArgs e)
		{
			if (!this.animation.EAnimation.Font.Underline)
			{
				this.btnFontUnderline.BackColor = System.Drawing.Color.LightBlue;
				this.animation.EAnimation.Font.Underline = true;
			}
			else
			{
				this.btnFontUnderline.BackColor = this.BackColor;
				this.animation.EAnimation.Font.Underline = false;
			}
			this.RefreshAnimation();
		}

		private void textColor_Click(object sender, EventArgs e)
		{
			if (this.colorDialog1.ShowDialog() == DialogResult.OK)
			{
				this.textColor.BackColor = this.colorDialog1.Color;
				this.animation.EAnimation.ForeColor = this.colorDialog1.Color;
				this.rtxContent.ForeColor = this.colorDialog1.Color;
				this.Refresh();
			}
			this.RefreshAnimation();
		}

		private void backColor_Click(object sender, EventArgs e)
		{
			if (this.colorDialog1.ShowDialog() == DialogResult.OK)
			{
				this.backColor.BackColor = this.colorDialog1.Color;
				this.animation.EAnimation.BackColor = this.colorDialog1.Color;
				this.rtxContent.BackColor = this.colorDialog1.Color;
				this.Refresh();
			}
			this.RefreshAnimation();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			this.rtxContent.Text = this.rtxContent.Text.Replace("\r\n", "");
			this.animation.EAnimation.Text = this.rtxContent.Text;
			this.RefreshAnimation();
		}

		private void isPreviewCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isPreview)
			{
				this.Event(AnimationEventType.StartPreview);
				return;
			}
			this.Event(AnimationEventType.StopPreview);
		}

		private void startPreviewButton_Click(object sender, EventArgs e)
		{
			if (this.isPreview)
			{
				this.isPreview = false;
				this.startPreviewButton.Text = formMain.ML.GetStr("Display_StartPreview");
				this.Event(AnimationEventType.StopPreview);
				return;
			}
			this.isPreview = true;
			this.startPreviewButton.Text = formMain.ML.GetStr("Display_StopPreview");
			this.Event(AnimationEventType.StartPreview);
		}

		public void StopPreview()
		{
			this.isPreview = false;
			this.startPreviewButton.Text = formMain.ML.GetStr("Display_StartPreview");
		}

		private void rtxContent_TextChanged(object sender, EventArgs e)
		{
			RichTextBox richTextBox = (RichTextBox)sender;
			if (!richTextBox.Focused)
			{
				return;
			}
			if (richTextBox.TextLength == 0)
			{
				this.animation.EAnimation.Text = " ";
			}
			else
			{
				this.animation.EAnimation.Text = richTextBox.Text;
			}
			this.RefreshAnimation();
		}

		private void rtxContent_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.Handled = true;
				return;
			}
			if (e.Control && e.KeyCode == Keys.V)
			{
				string text = Clipboard.GetText();
				text = text.Replace("\r\n", "");
				this.rtxContent.SelectedText = text;
				e.Handled = true;
			}
		}

		private void cmbColor_DrawItem(object sender, DrawItemEventArgs e)
		{
			System.Drawing.Graphics arg_06_0 = e.Graphics;
			System.Drawing.Rectangle bounds = e.Bounds;
			ComboBox comboBox = (ComboBox)sender;
			if (e.Index >= 0 && e.Index < comboBox.Items.Count)
			{
				System.Drawing.Color color = (System.Drawing.Color)comboBox.Items[e.Index];
				using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(color))
				{
					e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
					e.Graphics.FillRectangle(brush, new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height - 2));
					e.DrawFocusRectangle();
				}
			}
		}

		private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.animation.EAnimation.ForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
			this.RefreshAnimation();
		}

		private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.animation.EAnimation.Font.Size = float.Parse(comboBox.Text);
			this.RefreshAnimation();
		}

		private void cmbFontSize_TextChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			float num = 8f;
			if (!string.IsNullOrEmpty(comboBox.Text))
			{
				try
				{
					num = float.Parse(comboBox.Text);
				}
				catch
				{
				}
				if (num > 800f || (double)num == 0.0)
				{
					num = 8f;
					comboBox.Text = ((int)num).ToString();
					return;
				}
			}
			this.animation.EAnimation.Font.Size = num;
			this.RefreshAnimation();
		}

		private void cmbFontSize_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
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
			this.btnFontUnderline = new Button();
			this.btnFontItalic = new Button();
			this.btnFontBold = new Button();
			this.cmbFontSize = new ComboBox();
			this.cmbFontName = new ComboBox();
			this.textColor = new Button();
			this.backColor = new Button();
			this.colorDialog1 = new ColorDialog();
			this.startPreviewButton = new Button();
			this.rtxContent = new RichTextBox();
			this.cmbColor = new ComboBox();
			this.cmbBackColor = new ComboBox();
			base.SuspendLayout();
			this.btnFontUnderline.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 134);
			this.btnFontUnderline.Location = new System.Drawing.Point(181, 1);
			this.btnFontUnderline.Name = "btnFontUnderline";
			this.btnFontUnderline.Size = new System.Drawing.Size(22, 23);
			this.btnFontUnderline.TabIndex = 36;
			this.btnFontUnderline.Text = "U";
			this.btnFontUnderline.UseVisualStyleBackColor = true;
			this.btnFontUnderline.Click += new EventHandler(this.btnFontUnderline_Click);
			this.btnFontItalic.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 134);
			this.btnFontItalic.Location = new System.Drawing.Point(159, 1);
			this.btnFontItalic.Name = "btnFontItalic";
			this.btnFontItalic.Size = new System.Drawing.Size(22, 23);
			this.btnFontItalic.TabIndex = 35;
			this.btnFontItalic.Text = "I";
			this.btnFontItalic.UseVisualStyleBackColor = true;
			this.btnFontItalic.Click += new EventHandler(this.btnFontItalic_Click);
			this.btnFontBold.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.btnFontBold.Location = new System.Drawing.Point(137, 1);
			this.btnFontBold.Name = "btnFontBold";
			this.btnFontBold.Size = new System.Drawing.Size(22, 23);
			this.btnFontBold.TabIndex = 34;
			this.btnFontBold.Text = "B";
			this.btnFontBold.UseVisualStyleBackColor = true;
			this.btnFontBold.Click += new EventHandler(this.btnFontBold_Click);
			this.cmbFontSize.FormattingEnabled = true;
			this.cmbFontSize.Items.AddRange(new object[]
			{
				"8",
				"9",
				"10",
				"11",
				"12",
				"14",
				"16",
				"18",
				"20",
				"22",
				"24",
				"26",
				"28",
				"36",
				"48",
				"72",
				"80",
				"90",
				"100",
				"110",
				"120",
				"130",
				"140",
				"150",
				"160",
				"170",
				"180",
				"190",
				"200"
			});
			this.cmbFontSize.Location = new System.Drawing.Point(74, 1);
			this.cmbFontSize.Name = "cmbFontSize";
			this.cmbFontSize.Size = new System.Drawing.Size(58, 20);
			this.cmbFontSize.TabIndex = 33;
			this.cmbFontSize.SelectedIndexChanged += new EventHandler(this.cmbFontSize_SelectedIndexChanged);
			this.cmbFontSize.TextChanged += new EventHandler(this.cmbFontSize_TextChanged);
			this.cmbFontSize.KeyPress += new KeyPressEventHandler(this.cmbFontSize_KeyPress);
			this.cmbFontName.FormattingEnabled = true;
			this.cmbFontName.Location = new System.Drawing.Point(0, 1);
			this.cmbFontName.Name = "cmbFontName";
			this.cmbFontName.Size = new System.Drawing.Size(68, 20);
			this.cmbFontName.TabIndex = 32;
			this.cmbFontName.SelectedIndexChanged += new EventHandler(this.cmbFontName_SelectedIndexChanged);
			this.textColor.BackColor = System.Drawing.Color.Red;
			this.textColor.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 134);
			this.textColor.Location = new System.Drawing.Point(839, 3);
			this.textColor.Name = "textColor";
			this.textColor.Size = new System.Drawing.Size(30, 23);
			this.textColor.TabIndex = 37;
			this.textColor.UseVisualStyleBackColor = false;
			this.textColor.Visible = false;
			this.textColor.Click += new EventHandler(this.textColor_Click);
			this.backColor.BackColor = System.Drawing.Color.Red;
			this.backColor.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 134);
			this.backColor.Location = new System.Drawing.Point(869, 3);
			this.backColor.Name = "backColor";
			this.backColor.Size = new System.Drawing.Size(30, 23);
			this.backColor.TabIndex = 38;
			this.backColor.UseVisualStyleBackColor = false;
			this.backColor.Visible = false;
			this.backColor.Click += new EventHandler(this.backColor_Click);
			this.startPreviewButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.startPreviewButton.Location = new System.Drawing.Point(918, 1);
			this.startPreviewButton.Name = "startPreviewButton";
			this.startPreviewButton.Size = new System.Drawing.Size(95, 23);
			this.startPreviewButton.TabIndex = 40;
			this.startPreviewButton.Text = "开启预览";
			this.startPreviewButton.UseVisualStyleBackColor = true;
			this.startPreviewButton.Click += new EventHandler(this.startPreviewButton_Click);
			this.rtxContent.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.rtxContent.BackColor = System.Drawing.Color.Black;
			this.rtxContent.Location = new System.Drawing.Point(1, 27);
			this.rtxContent.Name = "rtxContent";
			this.rtxContent.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtxContent.Size = new System.Drawing.Size(1012, 47);
			this.rtxContent.TabIndex = 41;
			this.rtxContent.Text = "";
			this.rtxContent.TextChanged += new EventHandler(this.rtxContent_TextChanged);
			this.rtxContent.KeyDown += new KeyEventHandler(this.rtxContent_KeyDown);
			this.cmbColor.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbColor.FormattingEnabled = true;
			this.cmbColor.Items.AddRange(new object[]
			{
				"--",
				"--",
				"--",
				"--",
				"--",
				"--",
				"--",
				"--"
			});
			this.cmbColor.Location = new System.Drawing.Point(203, 2);
			this.cmbColor.Name = "cmbColor";
			this.cmbColor.Size = new System.Drawing.Size(45, 22);
			this.cmbColor.TabIndex = 42;
			this.cmbColor.DrawItem += new DrawItemEventHandler(this.cmbColor_DrawItem);
			this.cmbColor.SelectedIndexChanged += new EventHandler(this.cmbColor_SelectedIndexChanged);
			this.cmbBackColor.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbBackColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbBackColor.FormattingEnabled = true;
			this.cmbBackColor.Items.AddRange(new object[]
			{
				"--",
				"--",
				"--",
				"--",
				"--",
				"--",
				"--",
				"--"
			});
			this.cmbBackColor.Location = new System.Drawing.Point(262, 2);
			this.cmbBackColor.Name = "cmbBackColor";
			this.cmbBackColor.Size = new System.Drawing.Size(45, 22);
			this.cmbBackColor.TabIndex = 43;
			this.cmbBackColor.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gray;
			base.Controls.Add(this.cmbBackColor);
			base.Controls.Add(this.cmbColor);
			base.Controls.Add(this.rtxContent);
			base.Controls.Add(this.startPreviewButton);
			base.Controls.Add(this.backColor);
			base.Controls.Add(this.textColor);
			base.Controls.Add(this.btnFontUnderline);
			base.Controls.Add(this.btnFontItalic);
			base.Controls.Add(this.btnFontBold);
			base.Controls.Add(this.cmbFontSize);
			base.Controls.Add(this.cmbFontName);
			base.Name = "AnimationEditor";
			base.Size = new System.Drawing.Size(1016, 75);
			base.Load += new EventHandler(this.AnimationEditor_Load);
			base.ResumeLayout(false);
		}
	}
}
