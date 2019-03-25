using LedControlSystem.Fonts;
using LedControlSystem.Properties;
using LedModel;
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
	public class TextEditor : UserControl
	{
		private LedDText mar_Text;

		private LedPanel panel;

		private bool isFontDone;

		private bool isWaitingToReDraw;

		private IContainer components;

		private ComboBox cmbColor;

		private Button btnFontStrikeout;

		private Button btnFontUnderline;

		private Button btnFontItalic;

		private Button btnFontBold;

		private ComboBox cmbFontSize;

		private ComboBox cmbFontName;

		private ComboBox cmbLineSpace;

		private RichTextBox rtxContent;

		private Timer timer1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem Undo_ToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem Cut_ToolStripMenuItem;

		private ToolStripMenuItem Copy_ToolStripMenuItem;

		private ToolStripMenuItem Paste_ToolStripMenuItem;

		private ToolStripMenuItem Delete_ToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem Select_All_ToolStripMenuItem;

		private CheckBox chkCoupletWord;

		private NumericUpDown nudWordSpace;

		private ComboBox cmbBackColor;

		private Button btnOneWordOneColor;

		public event LedGlobal.LedContentEvent UpdateEvent;

		public TextEditor()
		{
			this.InitializeComponent();
		}

		public void Edit(LedDText pText)
		{
			this.ContextMenuStripDisplayLan();
			this.panel = formMain.ledsys.SelectedPanel;
			this.mar_Text = pText;
			this.cmbBackColor.Visible = false;
			try
			{
				this.InitControl();
				this.Binding();
			}
			catch
			{
			}
			this.timer1.Start();
		}

		public void Edit(LedDText pText, LedPanel pPanel)
		{
			this.panel = pPanel;
			this.mar_Text = pText;
			this.cmbBackColor.Visible = false;
			this.chkCoupletWord.Visible = false;
			this.cmbFontSize.Visible = false;
			this.btnOneWordOneColor.Visible = false;
			int num = this.btnFontBold.Location.X - this.cmbFontSize.Location.X;
			int num2 = num + (this.btnOneWordOneColor.Location.X + this.btnOneWordOneColor.Width - (this.btnFontStrikeout.Location.X + this.btnFontStrikeout.Width));
			this.btnFontBold.Location = new System.Drawing.Point(this.btnFontBold.Location.X - num, this.btnFontBold.Location.Y);
			this.btnFontItalic.Location = new System.Drawing.Point(this.btnFontItalic.Location.X - num, this.btnFontItalic.Location.Y);
			this.btnFontUnderline.Location = new System.Drawing.Point(this.btnFontUnderline.Location.X - num, this.btnFontUnderline.Location.Y);
			this.btnFontStrikeout.Location = new System.Drawing.Point(this.btnFontStrikeout.Location.X - num, this.btnFontStrikeout.Location.Y);
			this.cmbColor.Location = new System.Drawing.Point(this.cmbColor.Location.X - num2, this.cmbColor.Location.Y);
			this.nudWordSpace.Location = new System.Drawing.Point(this.nudWordSpace.Location.X - num2, this.nudWordSpace.Location.Y);
			try
			{
				this.InitControl();
				this.Binding();
			}
			catch
			{
			}
		}

		public void ContextMenuStripDisplayLan()
		{
			this.Undo_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Undo");
			this.Cut_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Cut");
			this.Copy_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Copy");
			this.Paste_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Paste");
			this.Delete_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Delete");
			this.Select_All_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Select_All");
		}

		private void TextEditor_Load(object sender, EventArgs e)
		{
			try
			{
				this.InitControl();
				this.Binding();
				this.ContextMenuStripDisplayLan();
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
			if (this.mar_Text != null && this.panel != null)
			{
				IList<System.Drawing.Color> colorList = LedColorConst.GetColorList(this.panel.ColorMode);
				foreach (System.Drawing.Color current2 in colorList)
				{
					this.cmbColor.Items.Add(current2);
				}
			}
			this.cmbBackColor.Items.Clear();
			if (this.panel != null)
			{
				IList<System.Drawing.Color> backColorList = LedColorConst.GetBackColorList(this.panel.ColorMode);
				foreach (System.Drawing.Color current3 in backColorList)
				{
					this.cmbBackColor.Items.Add(current3);
				}
			}
		}

		private void Binding()
		{
			LedEDText ledEDText = (this.mar_Text != null) ? this.mar_Text.EDText : null;
			if (this.cmbFontName.Items.Count > 0)
			{
				int num = -1;
				if (this.mar_Text != null && !string.IsNullOrEmpty(ledEDText.Font.FamilyName))
				{
					for (int i = 0; i < this.cmbFontName.Items.Count; i++)
					{
						if (this.cmbFontName.Items[i].ToString() == ledEDText.Font.FamilyName)
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
				if (this.mar_Text != null)
				{
					this.cmbFontSize.Text = ledEDText.Font.Size.ToString();
				}
				else
				{
					this.cmbFontSize.SelectedIndex = 0;
				}
			}
			if (this.mar_Text != null && ledEDText.Font.Bold)
			{
				this.btnFontBold.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontBold.BackColor = this.BackColor;
			}
			if (this.mar_Text != null && ledEDText.Font.Italic)
			{
				this.btnFontItalic.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontItalic.BackColor = this.BackColor;
			}
			if (this.mar_Text != null && ledEDText.Font.Underline)
			{
				this.btnFontUnderline.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontUnderline.BackColor = this.BackColor;
			}
			if (this.mar_Text != null && ledEDText.Font.Strikeout)
			{
				this.btnFontStrikeout.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontStrikeout.BackColor = this.BackColor;
			}
			if (this.mar_Text != null && ledEDText.WordColor)
			{
				this.btnOneWordOneColor.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnOneWordOneColor.BackColor = this.BackColor;
			}
			if (this.cmbColor.Items.Count > 0)
			{
				int num2 = 0;
				if (this.mar_Text != null)
				{
					num2 = formMain.FromColorToIndex(ledEDText.ForeColor);
					int count = this.cmbColor.Items.Count;
					if (num2 > count - 1)
					{
						num2 = count - 1;
					}
				}
				this.cmbColor.SelectedIndex = num2;
			}
			if (this.cmbBackColor.Items.Count > 0)
			{
				int num3 = 0;
				if (this.mar_Text != null)
				{
					num3 = formMain.FromBackColorToIndex(ledEDText.BackColor);
					int count2 = this.cmbBackColor.Items.Count;
					if (num3 > count2 - 1)
					{
						num3 = count2 - 1;
					}
				}
				this.cmbBackColor.SelectedIndex = num3;
			}
			if (this.mar_Text != null)
			{
				this.chkCoupletWord.Checked = ledEDText.Couplet;
			}
			else
			{
				this.chkCoupletWord.Checked = false;
			}
			if (this.mar_Text != null)
			{
				this.nudWordSpace.Value = ledEDText.Kerning;
			}
			else
			{
				this.nudWordSpace.Value = 0m;
			}
			if (this.cmbLineSpace.Items.Count > 0)
			{
				if (this.mar_Text != null)
				{
					this.cmbLineSpace.Text = "0";
				}
				else
				{
					this.cmbLineSpace.SelectedIndex = 0;
				}
			}
			if (this.mar_Text != null)
			{
				this.rtxContent.Text = ledEDText.Text;
				return;
			}
			this.rtxContent.Text = string.Empty;
		}

		private void Copy_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.contextMenuStrip1.SourceControl.Select();
			RichTextBox richTextBox = (RichTextBox)this.contextMenuStrip1.SourceControl;
			richTextBox.Copy();
		}

		private void Paste_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.contextMenuStrip1.SourceControl.Select();
			RichTextBox richTextBox = (RichTextBox)this.contextMenuStrip1.SourceControl;
			string text = Clipboard.GetText();
			text = text.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
			richTextBox.SelectedText = text;
		}

		private void Cut_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.contextMenuStrip1.SourceControl.Select();
			RichTextBox richTextBox = (RichTextBox)this.contextMenuStrip1.SourceControl;
			richTextBox.Cut();
		}

		private void Delete_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.contextMenuStrip1.SourceControl.Select();
			RichTextBox richTextBox = (RichTextBox)this.contextMenuStrip1.SourceControl;
			richTextBox.SelectedText = "";
		}

		private void SelectAll_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.contextMenuStrip1.SourceControl.Select();
			RichTextBox richTextBox = (RichTextBox)this.contextMenuStrip1.SourceControl;
			richTextBox.SelectAll();
		}

		private void Undo_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.contextMenuStrip1.SourceControl.Select();
			RichTextBox richTextBox = (RichTextBox)this.contextMenuStrip1.SourceControl;
			richTextBox.Undo();
		}

		private void cmbFontName_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.mar_Text.EDText.Font.FamilyName = comboBox.Text;
			this.RefreshContent();
		}

		private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.mar_Text.EDText.Font.Size = float.Parse(comboBox.Text);
			this.RefreshContent();
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
					this.cmbFontSize.Text = ((int)num).ToString();
					return;
				}
			}
			this.mar_Text.EDText.Font.Size = num;
			this.RefreshContent();
		}

		private void cmbFontSize_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void btnFontBold_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.mar_Text.EDText.Font.Bold)
			{
				this.mar_Text.EDText.Font.Bold = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.mar_Text.EDText.Font.Bold = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.RefreshContent();
		}

		private void btnFontItalic_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.mar_Text.EDText.Font.Italic)
			{
				this.mar_Text.EDText.Font.Italic = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.mar_Text.EDText.Font.Italic = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.RefreshContent();
		}

		private void btnFontUnderline_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.mar_Text.EDText.Font.Underline)
			{
				this.mar_Text.EDText.Font.Underline = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.mar_Text.EDText.Font.Underline = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.RefreshContent();
		}

		private void btnFontStrikeout_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.mar_Text.EDText.Font.Strikeout)
			{
				this.mar_Text.EDText.Font.Strikeout = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.mar_Text.EDText.Font.Strikeout = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.RefreshContent();
		}

		private void btnOneWordOneColor_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.mar_Text.EDText.WordColor)
			{
				this.mar_Text.EDText.WordColor = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.mar_Text.EDText.WordColor = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.RefreshContent();
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
			this.mar_Text.EDText.ForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
			if (this.mar_Text.EDText.WordColor)
			{
				this.mar_Text.EDText.WordColor = false;
				this.btnOneWordOneColor.BackColor = this.BackColor;
			}
			this.RefreshContent();
		}

		private void cmbBackColor_DrawItem(object sender, DrawItemEventArgs e)
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

		private void cmbBackColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.mar_Text.EDText.BackColor = formMain.FromIndexToBackColor(comboBox.SelectedIndex);
			this.RefreshContent();
		}

		private void cmbLineSpace_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			bool arg_0D_0 = comboBox.Focused;
		}

		private void nudWordSpace_ValueChanged(object sender, EventArgs e)
		{
			this.mar_Text.EDText.Kerning = (int)this.nudWordSpace.Value;
			this.RefreshContent();
		}

		private void chkCoupletWord_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			this.mar_Text.EDText.Couplet = checkBox.Checked;
			this.RefreshContent();
		}

		private void rtxContent_TextChanged(object sender, EventArgs e)
		{
			RichTextBox richTextBox = (RichTextBox)sender;
			if (!richTextBox.Focused)
			{
				return;
			}
			if (richTextBox.TextLength > 0)
			{
				this.mar_Text.EDText.Text = richTextBox.Text;
			}
			else
			{
				this.mar_Text.EDText.Text = " ";
			}
			this.RefreshContent();
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

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.isWaitingToReDraw)
			{
				this.isWaitingToReDraw = false;
				if (this.UpdateEvent != null)
				{
					this.UpdateEvent(LedContentEventType.Text, this);
				}
			}
		}

		private void RefreshContent()
		{
			try
			{
				this.isWaitingToReDraw = true;
			}
			catch
			{
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
			this.cmbColor = new ComboBox();
			this.btnFontStrikeout = new Button();
			this.btnFontUnderline = new Button();
			this.btnFontItalic = new Button();
			this.btnFontBold = new Button();
			this.cmbFontSize = new ComboBox();
			this.cmbFontName = new ComboBox();
			this.cmbLineSpace = new ComboBox();
			this.rtxContent = new RichTextBox();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.Undo_ToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.Cut_ToolStripMenuItem = new ToolStripMenuItem();
			this.Copy_ToolStripMenuItem = new ToolStripMenuItem();
			this.Paste_ToolStripMenuItem = new ToolStripMenuItem();
			this.Delete_ToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.Select_All_ToolStripMenuItem = new ToolStripMenuItem();
			this.timer1 = new Timer(this.components);
			this.chkCoupletWord = new CheckBox();
			this.nudWordSpace = new NumericUpDown();
			this.cmbBackColor = new ComboBox();
			this.btnOneWordOneColor = new Button();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.nudWordSpace).BeginInit();
			base.SuspendLayout();
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
				"--",
				"--",
				"--"
			});
			this.cmbColor.Location = new System.Drawing.Point(274, 2);
			this.cmbColor.Name = "cmbColor";
			this.cmbColor.Size = new System.Drawing.Size(45, 22);
			this.cmbColor.TabIndex = 30;
			this.cmbColor.DrawItem += new DrawItemEventHandler(this.cmbColor_DrawItem);
			this.cmbColor.SelectedIndexChanged += new EventHandler(this.cmbColor_SelectedIndexChanged);
			this.btnFontStrikeout.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, 134);
			this.btnFontStrikeout.Location = new System.Drawing.Point(228, 1);
			this.btnFontStrikeout.Name = "btnFontStrikeout";
			this.btnFontStrikeout.Size = new System.Drawing.Size(22, 23);
			this.btnFontStrikeout.TabIndex = 29;
			this.btnFontStrikeout.Text = "S";
			this.btnFontStrikeout.UseVisualStyleBackColor = true;
			this.btnFontStrikeout.Click += new EventHandler(this.btnFontStrikeout_Click);
			this.btnFontUnderline.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 134);
			this.btnFontUnderline.Location = new System.Drawing.Point(206, 1);
			this.btnFontUnderline.Name = "btnFontUnderline";
			this.btnFontUnderline.Size = new System.Drawing.Size(22, 23);
			this.btnFontUnderline.TabIndex = 28;
			this.btnFontUnderline.Text = "U";
			this.btnFontUnderline.UseVisualStyleBackColor = true;
			this.btnFontUnderline.Click += new EventHandler(this.btnFontUnderline_Click);
			this.btnFontItalic.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 134);
			this.btnFontItalic.Location = new System.Drawing.Point(184, 1);
			this.btnFontItalic.Name = "btnFontItalic";
			this.btnFontItalic.Size = new System.Drawing.Size(22, 23);
			this.btnFontItalic.TabIndex = 27;
			this.btnFontItalic.Text = "I";
			this.btnFontItalic.UseVisualStyleBackColor = true;
			this.btnFontItalic.Click += new EventHandler(this.btnFontItalic_Click);
			this.btnFontBold.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.btnFontBold.Location = new System.Drawing.Point(162, 1);
			this.btnFontBold.Name = "btnFontBold";
			this.btnFontBold.Size = new System.Drawing.Size(22, 23);
			this.btnFontBold.TabIndex = 26;
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
				"30",
				"32",
				"34",
				"36",
				"38",
				"40",
				"42",
				"45",
				"46",
				"48",
				"50",
				"52",
				"54",
				"56",
				"58",
				"60",
				"62",
				"64",
				"66",
				"68",
				"70",
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
			this.cmbFontSize.Location = new System.Drawing.Point(110, 3);
			this.cmbFontSize.Name = "cmbFontSize";
			this.cmbFontSize.Size = new System.Drawing.Size(50, 20);
			this.cmbFontSize.TabIndex = 25;
			this.cmbFontSize.SelectedIndexChanged += new EventHandler(this.cmbFontSize_SelectedIndexChanged);
			this.cmbFontSize.TextChanged += new EventHandler(this.cmbFontSize_TextChanged);
			this.cmbFontSize.KeyPress += new KeyPressEventHandler(this.cmbFontSize_KeyPress);
			this.cmbFontName.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbFontName.FormattingEnabled = true;
			this.cmbFontName.Location = new System.Drawing.Point(0, 3);
			this.cmbFontName.Name = "cmbFontName";
			this.cmbFontName.Size = new System.Drawing.Size(107, 20);
			this.cmbFontName.TabIndex = 24;
			this.cmbFontName.SelectedIndexChanged += new EventHandler(this.cmbFontName_SelectedIndexChanged);
			this.cmbLineSpace.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbLineSpace.FormattingEnabled = true;
			this.cmbLineSpace.Items.AddRange(new object[]
			{
				"+50%",
				"+40%",
				"+30%",
				"+20%",
				"+10%",
				"+00%",
				"-10%",
				"-20%",
				"-30%",
				"-40%",
				"-50%"
			});
			this.cmbLineSpace.Location = new System.Drawing.Point(396, 4);
			this.cmbLineSpace.Name = "cmbLineSpace";
			this.cmbLineSpace.Size = new System.Drawing.Size(74, 20);
			this.cmbLineSpace.TabIndex = 31;
			this.cmbLineSpace.Visible = false;
			this.cmbLineSpace.SelectedIndexChanged += new EventHandler(this.cmbLineSpace_SelectedIndexChanged);
			this.rtxContent.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.rtxContent.BackColor = System.Drawing.Color.White;
			this.rtxContent.ContextMenuStrip = this.contextMenuStrip1;
			this.rtxContent.Location = new System.Drawing.Point(0, 26);
			this.rtxContent.Name = "rtxContent";
			this.rtxContent.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtxContent.Size = new System.Drawing.Size(442, 145);
			this.rtxContent.TabIndex = 32;
			this.rtxContent.Text = "";
			this.rtxContent.TextChanged += new EventHandler(this.rtxContent_TextChanged);
			this.rtxContent.KeyDown += new KeyEventHandler(this.rtxContent_KeyDown);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.Undo_ToolStripMenuItem,
				this.toolStripSeparator1,
				this.Cut_ToolStripMenuItem,
				this.Copy_ToolStripMenuItem,
				this.Paste_ToolStripMenuItem,
				this.Delete_ToolStripMenuItem,
				this.toolStripSeparator2,
				this.Select_All_ToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(101, 148);
			this.Undo_ToolStripMenuItem.Name = "Undo_ToolStripMenuItem";
			this.Undo_ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.Undo_ToolStripMenuItem.Text = "撤销";
			this.Undo_ToolStripMenuItem.Click += new EventHandler(this.Undo_ToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
			this.Cut_ToolStripMenuItem.Name = "Cut_ToolStripMenuItem";
			this.Cut_ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.Cut_ToolStripMenuItem.Text = "剪切";
			this.Cut_ToolStripMenuItem.Click += new EventHandler(this.Cut_ToolStripMenuItem_Click);
			this.Copy_ToolStripMenuItem.Name = "Copy_ToolStripMenuItem";
			this.Copy_ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.Copy_ToolStripMenuItem.Text = "复制";
			this.Copy_ToolStripMenuItem.Click += new EventHandler(this.Copy_ToolStripMenuItem_Click);
			this.Paste_ToolStripMenuItem.Name = "Paste_ToolStripMenuItem";
			this.Paste_ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.Paste_ToolStripMenuItem.Text = "粘贴";
			this.Paste_ToolStripMenuItem.Click += new EventHandler(this.Paste_ToolStripMenuItem_Click);
			this.Delete_ToolStripMenuItem.Name = "Delete_ToolStripMenuItem";
			this.Delete_ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.Delete_ToolStripMenuItem.Text = "删除";
			this.Delete_ToolStripMenuItem.Click += new EventHandler(this.Delete_ToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
			this.Select_All_ToolStripMenuItem.Name = "Select_All_ToolStripMenuItem";
			this.Select_All_ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.Select_All_ToolStripMenuItem.Text = "全选";
			this.Select_All_ToolStripMenuItem.Click += new EventHandler(this.SelectAll_ToolStripMenuItem_Click);
			this.timer1.Interval = 1500;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.chkCoupletWord.AutoSize = true;
			this.chkCoupletWord.Location = new System.Drawing.Point(364, 5);
			this.chkCoupletWord.Name = "chkCoupletWord";
			this.chkCoupletWord.Size = new System.Drawing.Size(60, 16);
			this.chkCoupletWord.TabIndex = 33;
			this.chkCoupletWord.Text = "对联字";
			this.chkCoupletWord.UseVisualStyleBackColor = true;
			this.chkCoupletWord.CheckedChanged += new EventHandler(this.chkCoupletWord_CheckedChanged);
			this.nudWordSpace.Location = new System.Drawing.Point(322, 2);
			this.nudWordSpace.Name = "nudWordSpace";
			this.nudWordSpace.Size = new System.Drawing.Size(40, 21);
			this.nudWordSpace.TabIndex = 34;
			this.nudWordSpace.ValueChanged += new EventHandler(this.nudWordSpace_ValueChanged);
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
				"--",
				"--",
				"--"
			});
			this.cmbBackColor.Location = new System.Drawing.Point(281, 1);
			this.cmbBackColor.Name = "cmbBackColor";
			this.cmbBackColor.Size = new System.Drawing.Size(45, 22);
			this.cmbBackColor.TabIndex = 35;
			this.cmbBackColor.DrawItem += new DrawItemEventHandler(this.cmbBackColor_DrawItem);
			this.cmbBackColor.SelectedIndexChanged += new EventHandler(this.cmbBackColor_SelectedIndexChanged);
			this.btnOneWordOneColor.BackgroundImage = Resources.WordColor;
			this.btnOneWordOneColor.BackgroundImageLayout = ImageLayout.Zoom;
			this.btnOneWordOneColor.Location = new System.Drawing.Point(250, 1);
			this.btnOneWordOneColor.Name = "btnOneWordOneColor";
			this.btnOneWordOneColor.Size = new System.Drawing.Size(22, 23);
			this.btnOneWordOneColor.TabIndex = 36;
			this.btnOneWordOneColor.UseVisualStyleBackColor = true;
			this.btnOneWordOneColor.Click += new EventHandler(this.btnOneWordOneColor_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(this.cmbColor);
			base.Controls.Add(this.btnOneWordOneColor);
			base.Controls.Add(this.cmbBackColor);
			base.Controls.Add(this.nudWordSpace);
			base.Controls.Add(this.chkCoupletWord);
			base.Controls.Add(this.rtxContent);
			base.Controls.Add(this.cmbLineSpace);
			base.Controls.Add(this.btnFontStrikeout);
			base.Controls.Add(this.btnFontUnderline);
			base.Controls.Add(this.btnFontItalic);
			base.Controls.Add(this.btnFontBold);
			base.Controls.Add(this.cmbFontSize);
			base.Controls.Add(this.cmbFontName);
			base.Name = "TextEditor";
			base.Size = new System.Drawing.Size(442, 171);
			base.Load += new EventHandler(this.TextEditor_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			((ISupportInitialize)this.nudWordSpace).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
