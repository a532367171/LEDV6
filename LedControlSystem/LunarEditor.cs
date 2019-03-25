using LedControlSystem.Fonts;
using LedModel;
using LedModel.Const;
using LedModel.Content;
using LedModel.Element.Lunar;
using LedModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class LunarEditor : UserControl
	{
		private LedLunar lunar;

		private LedPanel panel;

		private bool isFontDone;

		private IContainer components;

		private TextBox txtText;

		private CheckBox chkDate;

		private CheckBox chkZodiac;

		private CheckBox chkText;

		private CheckBox chkChineseEra;

		private Button btnFontUnderline;

		private Button btnFontItalic;

		private Label lblText;

		private Label lblFontFamily;

		private Button btnFontBold;

		private ComboBox cmbLineStyle;

		private ComboBox cmbFontSize;

		private Label lblFontSize;

		private ComboBox cmbFontFamily;

		private ComboBox cmbDateForeColor;

		private ComboBox cmbZodiacForeColor;

		private ComboBox cmbTextForeColor;

		private ComboBox cmbChineseEraForeColor;

		private Label lblChineseEra;

		private Label lblZodiac;

		private Label lblDate;

		private Label lblSolarTerm;

		private CheckBox chkSolarTerm;

		private ComboBox cmbSolarTermForeColor;

		private RadioButton rdoAlignLeft;

		private RadioButton rdoAlignCenter;

		private RadioButton rdoAlignRight;

		public event LedGlobal.LedContentEvent UpdateEvent;

		public LunarEditor()
		{
			this.InitializeComponent();
		}

		public void Edit(LedLunar pLunar)
		{
			this.panel = formMain.ledsys.SelectedPanel;
			this.lunar = pLunar;
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
				this.cmbFontFamily.Items.Clear();
				IList<string> fontFamilies = new FontFamiliesEx().GetFontFamilies();
				if (fontFamilies != null && fontFamilies.Count > 0)
				{
					foreach (string current in fontFamilies)
					{
						this.cmbFontFamily.Items.Add(current);
					}
				}
				this.isFontDone = true;
			}
			this.cmbTextForeColor.Items.Clear();
			if (this.lunar != null && this.panel != null)
			{
				IList<System.Drawing.Color> colorList = LedColorConst.GetColorList(this.panel.ColorMode);
				foreach (System.Drawing.Color current2 in colorList)
				{
					this.cmbTextForeColor.Items.Add(current2);
				}
			}
			this.cmbChineseEraForeColor.Items.Clear();
			if (this.panel != null)
			{
				IList<System.Drawing.Color> colorList2 = LedColorConst.GetColorList(this.panel.ColorMode);
				foreach (System.Drawing.Color current3 in colorList2)
				{
					this.cmbChineseEraForeColor.Items.Add(current3);
				}
			}
			this.cmbDateForeColor.Items.Clear();
			if (this.panel != null)
			{
				IList<System.Drawing.Color> colorList3 = LedColorConst.GetColorList(this.panel.ColorMode);
				foreach (System.Drawing.Color current4 in colorList3)
				{
					this.cmbDateForeColor.Items.Add(current4);
				}
			}
			this.cmbZodiacForeColor.Items.Clear();
			if (this.panel != null)
			{
				IList<System.Drawing.Color> colorList4 = LedColorConst.GetColorList(this.panel.ColorMode);
				foreach (System.Drawing.Color current5 in colorList4)
				{
					this.cmbZodiacForeColor.Items.Add(current5);
				}
			}
			this.cmbSolarTermForeColor.Items.Clear();
			if (this.panel != null)
			{
				IList<System.Drawing.Color> colorList5 = LedColorConst.GetColorList(this.panel.ColorMode);
				foreach (System.Drawing.Color current6 in colorList5)
				{
					this.cmbSolarTermForeColor.Items.Add(current6);
				}
			}
		}

		private void Binding()
		{
			LedLunarBackground ledLunarBackground = (this.lunar != null) ? this.lunar.LunarBackground : null;
			if (this.cmbFontFamily.Items.Count > 0)
			{
				int num = -1;
				if (this.lunar != null && !string.IsNullOrEmpty(this.lunar.Font.FamilyName))
				{
					for (int i = 0; i < this.cmbFontFamily.Items.Count; i++)
					{
						if (this.cmbFontFamily.Items[i].ToString() == this.lunar.Font.FamilyName)
						{
							num = i;
							break;
						}
					}
				}
				if (num < 0)
				{
					for (int j = 0; j < this.cmbFontFamily.Items.Count; j++)
					{
						if (this.cmbFontFamily.Items[j].ToString() == formMain.DefalutForeignTradeFamilyName)
						{
							num = j;
							break;
						}
					}
				}
				this.cmbFontFamily.SelectedIndex = ((num < 0) ? 0 : num);
			}
			if (this.cmbFontSize.Items.Count > 0)
			{
				if (this.lunar != null)
				{
					this.cmbFontSize.Text = this.lunar.Font.Size.ToString();
				}
				else
				{
					this.cmbFontSize.SelectedIndex = 0;
				}
			}
			if (this.lunar != null && this.lunar.Font.Bold)
			{
				this.btnFontBold.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontBold.BackColor = this.BackColor;
			}
			if (this.lunar != null && this.lunar.Font.Italic)
			{
				this.btnFontItalic.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontItalic.BackColor = this.BackColor;
			}
			if (this.lunar != null && this.lunar.Font.Underline)
			{
				this.btnFontUnderline.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontUnderline.BackColor = this.BackColor;
			}
			if (this.lunar != null && this.lunar.TextAlign == LedTextAlign.Left)
			{
				this.rdoAlignLeft.Checked = true;
			}
			else
			{
				this.rdoAlignLeft.Checked = false;
			}
			if (this.lunar != null && this.lunar.TextAlign == LedTextAlign.Center)
			{
				this.rdoAlignCenter.Checked = true;
			}
			else
			{
				this.rdoAlignCenter.Checked = false;
			}
			if (this.lunar != null && this.lunar.TextAlign == LedTextAlign.Right)
			{
				this.rdoAlignRight.Checked = true;
			}
			else
			{
				this.rdoAlignRight.Checked = false;
			}
			if (!this.rdoAlignLeft.Checked && !this.rdoAlignCenter.Checked && !this.rdoAlignRight.Checked)
			{
				this.rdoAlignCenter.Checked = true;
			}
			if (this.cmbLineStyle.Items.Count > 0)
			{
				int num2 = 0;
				if (this.lunar != null)
				{
					num2 = (int)this.lunar.LineStyle;
					int count = this.cmbLineStyle.Items.Count;
					if (num2 > count - 1)
					{
						num2 = count - 1;
					}
				}
				this.cmbLineStyle.SelectedIndex = num2;
			}
			if (this.cmbTextForeColor.Items.Count > 0)
			{
				int num3 = 0;
				if (ledLunarBackground != null)
				{
					num3 = formMain.FromColorToIndex(ledLunarBackground.ForeColor);
					int count2 = this.cmbTextForeColor.Items.Count;
					if (num3 > count2 - 1)
					{
						num3 = count2 - 1;
					}
				}
				this.cmbTextForeColor.SelectedIndex = num3;
			}
			if (ledLunarBackground != null)
			{
				this.txtText.Text = ledLunarBackground.Text;
			}
			else
			{
				this.txtText.Text = string.Empty;
			}
			if (ledLunarBackground != null)
			{
				this.chkText.Checked = ledLunarBackground.TextEnabled;
			}
			else
			{
				this.chkText.Checked = false;
			}
			this.txtText.Enabled = this.chkText.Checked;
			this.cmbTextForeColor.Enabled = this.chkText.Checked;
			if (this.cmbChineseEraForeColor.Items.Count > 0)
			{
				int num4 = 0;
				if (this.lunar != null)
				{
					num4 = formMain.FromColorToIndex(this.lunar.ChineseEraForeColor);
					int count3 = this.cmbChineseEraForeColor.Items.Count;
					if (num4 > count3 - 1)
					{
						num4 = count3 - 1;
					}
				}
				this.cmbChineseEraForeColor.SelectedIndex = num4;
			}
			if (this.lunar != null)
			{
				this.chkChineseEra.Checked = this.lunar.ChineseEraEnabled;
			}
			else
			{
				this.chkChineseEra.Checked = false;
			}
			this.cmbChineseEraForeColor.Enabled = this.chkChineseEra.Checked;
			if (this.cmbDateForeColor.Items.Count > 0)
			{
				int num5 = 0;
				if (this.lunar != null)
				{
					num5 = formMain.FromColorToIndex(this.lunar.DayForeColor);
					int count4 = this.cmbDateForeColor.Items.Count;
					if (num5 > count4 - 1)
					{
						num5 = count4 - 1;
					}
				}
				this.cmbDateForeColor.SelectedIndex = num5;
			}
			if (this.lunar != null)
			{
				this.chkDate.Checked = this.lunar.DayEnabled;
			}
			else
			{
				this.chkDate.Checked = false;
			}
			this.cmbDateForeColor.Enabled = this.chkDate.Checked;
			if (this.cmbZodiacForeColor.Items.Count > 0)
			{
				int num6 = 0;
				if (this.lunar != null)
				{
					num6 = formMain.FromColorToIndex(this.lunar.ZodiacForeColor);
					int count5 = this.cmbZodiacForeColor.Items.Count;
					if (num6 > count5 - 1)
					{
						num6 = count5 - 1;
					}
				}
				this.cmbZodiacForeColor.SelectedIndex = num6;
			}
			if (this.lunar != null)
			{
				this.chkZodiac.Checked = this.lunar.ZodiacEnabled;
			}
			else
			{
				this.chkZodiac.Checked = false;
			}
			this.cmbZodiacForeColor.Enabled = this.chkZodiac.Checked;
			if (this.cmbSolarTermForeColor.Items.Count > 0)
			{
				int num7 = 0;
				if (this.lunar != null)
				{
					num7 = formMain.FromColorToIndex(this.lunar.SolarTermForeColor);
					int count6 = this.cmbSolarTermForeColor.Items.Count;
					if (num7 > count6 - 1)
					{
						num7 = count6 - 1;
					}
				}
				this.cmbSolarTermForeColor.SelectedIndex = num7;
			}
			if (this.lunar != null)
			{
				this.chkSolarTerm.Checked = this.lunar.SolarTermEnabled;
			}
			else
			{
				this.chkSolarTerm.Checked = false;
			}
			this.cmbSolarTermForeColor.Enabled = this.chkSolarTerm.Checked;
		}

		private void cmbFontFamily_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.lunar.Font.FamilyName = comboBox.Text;
			this.Redraw();
		}

		private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.lunar.Font.Size = float.Parse(comboBox.Text);
			this.Redraw();
		}

		private void cmbFontSize_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
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
			this.lunar.Font.Size = num;
			this.Redraw();
		}

		private void btnFontBold_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.lunar.Font.Bold)
			{
				this.lunar.Font.Bold = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.lunar.Font.Bold = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.Redraw();
		}

		private void btnFontItalic_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.lunar.Font.Italic)
			{
				this.lunar.Font.Italic = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.lunar.Font.Italic = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.Redraw();
		}

		private void btnFontUnderline_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.lunar.Font.Underline)
			{
				this.lunar.Font.Underline = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.lunar.Font.Underline = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.Redraw();
		}

		private void rdoAlignLeft_Click(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.TextAlign = LedTextAlign.Left;
				this.Redraw();
			}
		}

		private void rdoAlignCenter_Click(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.TextAlign = LedTextAlign.Center;
				this.Redraw();
			}
		}

		private void rdoAlignRight_Click(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.TextAlign = LedTextAlign.Right;
				this.Redraw();
			}
		}

		private void cmbLineStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.LineStyle = (LedLunarLineStyle)comboBox.SelectedIndex;
				this.Redraw();
			}
		}

		private void txtText_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.LunarBackground.Text = textBox.Text;
				this.Redraw();
			}
		}

		private void cmbTextForeColor_DrawItem(object sender, DrawItemEventArgs e)
		{
			this.DrawItem(sender, e);
		}

		private void cmbTextForeColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.lunar != null && this.lunar.LunarBackground != null)
			{
				this.lunar.LunarBackground.ForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
				this.Redraw();
			}
		}

		private void chkText_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.lunar != null && this.lunar.LunarBackground != null)
			{
				this.lunar.LunarBackground.TextEnabled = checkBox.Checked;
				this.txtText.Enabled = checkBox.Checked;
				this.cmbTextForeColor.Enabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void cmbChineseEraForeColor_DrawItem(object sender, DrawItemEventArgs e)
		{
			this.DrawItem(sender, e);
		}

		private void cmbChineseEraForeColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.ChineseEraForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
				this.Redraw();
			}
		}

		private void chkChineseEra_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.ChineseEraEnabled = checkBox.Checked;
				this.cmbChineseEraForeColor.Enabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void cmbDateForeColor_DrawItem(object sender, DrawItemEventArgs e)
		{
			this.DrawItem(sender, e);
		}

		private void cmbDateForeColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.DayForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
				this.lunar.MonthForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
				this.Redraw();
			}
		}

		private void chkDate_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.DayEnabled = checkBox.Checked;
				this.lunar.MonthEnabled = checkBox.Checked;
				this.cmbDateForeColor.Enabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void cmbZodiacForeColor_DrawItem(object sender, DrawItemEventArgs e)
		{
			this.DrawItem(sender, e);
		}

		private void cmbZodiacForeColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.ZodiacForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
				this.Redraw();
			}
		}

		private void chkZodiac_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.ZodiacEnabled = checkBox.Checked;
				this.cmbZodiacForeColor.Enabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void cmbSolarTermForeColor_DrawItem(object sender, DrawItemEventArgs e)
		{
			this.DrawItem(sender, e);
		}

		private void cmbSolarTermForeColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.SolarTermForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
				this.Redraw();
			}
		}

		private void cmbSolarTerm_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.lunar != null)
			{
				this.lunar.SolarTermEnabled = checkBox.Checked;
				this.cmbSolarTermForeColor.Enabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void DrawItem(object sender, DrawItemEventArgs e)
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

		private void Redraw()
		{
			if (this.UpdateEvent != null)
			{
				this.UpdateEvent(LedContentEventType.Text, this);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(LunarEditor));
			this.txtText = new TextBox();
			this.chkDate = new CheckBox();
			this.chkZodiac = new CheckBox();
			this.chkText = new CheckBox();
			this.chkChineseEra = new CheckBox();
			this.btnFontUnderline = new Button();
			this.btnFontItalic = new Button();
			this.lblText = new Label();
			this.lblFontFamily = new Label();
			this.btnFontBold = new Button();
			this.cmbLineStyle = new ComboBox();
			this.cmbFontSize = new ComboBox();
			this.lblFontSize = new Label();
			this.cmbFontFamily = new ComboBox();
			this.cmbDateForeColor = new ComboBox();
			this.cmbZodiacForeColor = new ComboBox();
			this.cmbTextForeColor = new ComboBox();
			this.cmbChineseEraForeColor = new ComboBox();
			this.lblChineseEra = new Label();
			this.lblZodiac = new Label();
			this.lblDate = new Label();
			this.lblSolarTerm = new Label();
			this.chkSolarTerm = new CheckBox();
			this.cmbSolarTermForeColor = new ComboBox();
			this.rdoAlignLeft = new RadioButton();
			this.rdoAlignCenter = new RadioButton();
			this.rdoAlignRight = new RadioButton();
			base.SuspendLayout();
			this.txtText.Location = new System.Drawing.Point(37, 27);
			this.txtText.Name = "txtText";
			this.txtText.Size = new System.Drawing.Size(373, 21);
			this.txtText.TabIndex = 71;
			this.txtText.TextChanged += new EventHandler(this.txtText_TextChanged);
			this.chkDate.AutoSize = true;
			this.chkDate.ForeColor = System.Drawing.Color.White;
			this.chkDate.ImeMode = ImeMode.NoControl;
			this.chkDate.Location = new System.Drawing.Point(416, 58);
			this.chkDate.Name = "chkDate";
			this.chkDate.Size = new System.Drawing.Size(15, 14);
			this.chkDate.TabIndex = 68;
			this.chkDate.UseVisualStyleBackColor = true;
			this.chkDate.CheckedChanged += new EventHandler(this.chkDate_CheckedChanged);
			this.chkZodiac.AutoSize = true;
			this.chkZodiac.ForeColor = System.Drawing.Color.White;
			this.chkZodiac.ImeMode = ImeMode.NoControl;
			this.chkZodiac.Location = new System.Drawing.Point(156, 89);
			this.chkZodiac.Name = "chkZodiac";
			this.chkZodiac.Size = new System.Drawing.Size(15, 14);
			this.chkZodiac.TabIndex = 67;
			this.chkZodiac.UseVisualStyleBackColor = true;
			this.chkZodiac.CheckedChanged += new EventHandler(this.chkZodiac_CheckedChanged);
			this.chkText.AutoSize = true;
			this.chkText.ForeColor = System.Drawing.Color.White;
			this.chkText.ImeMode = ImeMode.NoControl;
			this.chkText.Location = new System.Drawing.Point(473, 30);
			this.chkText.Name = "chkText";
			this.chkText.Size = new System.Drawing.Size(15, 14);
			this.chkText.TabIndex = 70;
			this.chkText.UseVisualStyleBackColor = true;
			this.chkText.CheckedChanged += new EventHandler(this.chkText_CheckedChanged);
			this.chkChineseEra.AutoSize = true;
			this.chkChineseEra.ForeColor = System.Drawing.Color.White;
			this.chkChineseEra.ImeMode = ImeMode.NoControl;
			this.chkChineseEra.Location = new System.Drawing.Point(157, 58);
			this.chkChineseEra.Name = "chkChineseEra";
			this.chkChineseEra.Size = new System.Drawing.Size(15, 14);
			this.chkChineseEra.TabIndex = 69;
			this.chkChineseEra.UseVisualStyleBackColor = true;
			this.chkChineseEra.CheckedChanged += new EventHandler(this.chkChineseEra_CheckedChanged);
			this.btnFontUnderline.BackColor = System.Drawing.Color.White;
			this.btnFontUnderline.Cursor = Cursors.Default;
			this.btnFontUnderline.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
			this.btnFontUnderline.ForeColor = System.Drawing.Color.Black;
			this.btnFontUnderline.ImeMode = ImeMode.NoControl;
			this.btnFontUnderline.Location = new System.Drawing.Point(268, 2);
			this.btnFontUnderline.Name = "btnFontUnderline";
			this.btnFontUnderline.Size = new System.Drawing.Size(21, 21);
			this.btnFontUnderline.TabIndex = 64;
			this.btnFontUnderline.Text = "U";
			this.btnFontUnderline.UseVisualStyleBackColor = false;
			this.btnFontUnderline.Click += new EventHandler(this.btnFontUnderline_Click);
			this.btnFontItalic.BackColor = System.Drawing.Color.White;
			this.btnFontItalic.Cursor = Cursors.Default;
			this.btnFontItalic.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
			this.btnFontItalic.ForeColor = System.Drawing.Color.Black;
			this.btnFontItalic.ImeMode = ImeMode.NoControl;
			this.btnFontItalic.Location = new System.Drawing.Point(247, 2);
			this.btnFontItalic.Name = "btnFontItalic";
			this.btnFontItalic.Size = new System.Drawing.Size(21, 21);
			this.btnFontItalic.TabIndex = 65;
			this.btnFontItalic.Text = "I";
			this.btnFontItalic.UseVisualStyleBackColor = false;
			this.btnFontItalic.Click += new EventHandler(this.btnFontItalic_Click);
			this.lblText.BackColor = System.Drawing.Color.Transparent;
			this.lblText.Cursor = Cursors.Default;
			this.lblText.ForeColor = System.Drawing.Color.Black;
			this.lblText.ImeMode = ImeMode.NoControl;
			this.lblText.Location = new System.Drawing.Point(-2, 28);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(37, 21);
			this.lblText.TabIndex = 59;
			this.lblText.Text = "文本";
			this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblFontFamily.BackColor = System.Drawing.Color.Transparent;
			this.lblFontFamily.Cursor = Cursors.Default;
			this.lblFontFamily.ForeColor = System.Drawing.Color.Black;
			this.lblFontFamily.ImeMode = ImeMode.NoControl;
			this.lblFontFamily.Location = new System.Drawing.Point(2, 5);
			this.lblFontFamily.Name = "lblFontFamily";
			this.lblFontFamily.Size = new System.Drawing.Size(30, 15);
			this.lblFontFamily.TabIndex = 58;
			this.lblFontFamily.Text = "字体";
			this.lblFontFamily.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnFontBold.BackColor = System.Drawing.Color.White;
			this.btnFontBold.Cursor = Cursors.Default;
			this.btnFontBold.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.btnFontBold.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Bold);
			this.btnFontBold.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnFontBold.ImeMode = ImeMode.NoControl;
			this.btnFontBold.Location = new System.Drawing.Point(226, 2);
			this.btnFontBold.Name = "btnFontBold";
			this.btnFontBold.Size = new System.Drawing.Size(21, 21);
			this.btnFontBold.TabIndex = 66;
			this.btnFontBold.Text = "B";
			this.btnFontBold.UseVisualStyleBackColor = false;
			this.btnFontBold.Click += new EventHandler(this.btnFontBold_Click);
			this.cmbLineStyle.Cursor = Cursors.Default;
			this.cmbLineStyle.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbLineStyle.FormattingEnabled = true;
			this.cmbLineStyle.ImeMode = ImeMode.On;
			this.cmbLineStyle.Items.AddRange(new object[]
			{
				"单行显示",
				"多行显示"
			});
			this.cmbLineStyle.Location = new System.Drawing.Point(368, 1);
			this.cmbLineStyle.MaxLength = 5;
			this.cmbLineStyle.Name = "cmbLineStyle";
			this.cmbLineStyle.Size = new System.Drawing.Size(120, 20);
			this.cmbLineStyle.TabIndex = 62;
			this.cmbLineStyle.SelectedIndexChanged += new EventHandler(this.cmbLineStyle_SelectedIndexChanged);
			this.cmbFontSize.Cursor = Cursors.Default;
			this.cmbFontSize.FormattingEnabled = true;
			this.cmbFontSize.ImeMode = ImeMode.On;
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
			this.cmbFontSize.Location = new System.Drawing.Point(175, 3);
			this.cmbFontSize.MaxLength = 5;
			this.cmbFontSize.Name = "cmbFontSize";
			this.cmbFontSize.Size = new System.Drawing.Size(47, 20);
			this.cmbFontSize.TabIndex = 61;
			this.cmbFontSize.Text = "14";
			this.cmbFontSize.SelectedIndexChanged += new EventHandler(this.cmbFontSize_SelectedIndexChanged);
			this.cmbFontSize.TextChanged += new EventHandler(this.cmbFontSize_TextChanged);
			this.cmbFontSize.KeyPress += new KeyPressEventHandler(this.cmbFontSize_KeyPress);
			this.lblFontSize.BackColor = System.Drawing.Color.Transparent;
			this.lblFontSize.Cursor = Cursors.Default;
			this.lblFontSize.ForeColor = System.Drawing.Color.Black;
			this.lblFontSize.ImeMode = ImeMode.NoControl;
			this.lblFontSize.Location = new System.Drawing.Point(142, 6);
			this.lblFontSize.Name = "lblFontSize";
			this.lblFontSize.Size = new System.Drawing.Size(35, 15);
			this.lblFontSize.TabIndex = 60;
			this.lblFontSize.Text = "字号";
			this.lblFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmbFontFamily.Cursor = Cursors.Default;
			this.cmbFontFamily.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbFontFamily.FormattingEnabled = true;
			this.cmbFontFamily.ImeMode = ImeMode.On;
			this.cmbFontFamily.Location = new System.Drawing.Point(37, 3);
			this.cmbFontFamily.Name = "cmbFontFamily";
			this.cmbFontFamily.Size = new System.Drawing.Size(98, 20);
			this.cmbFontFamily.TabIndex = 63;
			this.cmbFontFamily.SelectedIndexChanged += new EventHandler(this.cmbFontFamily_SelectedIndexChanged);
			this.cmbDateForeColor.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmbDateForeColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDateForeColor.FormattingEnabled = true;
			this.cmbDateForeColor.ImeMode = ImeMode.On;
			this.cmbDateForeColor.Items.AddRange(new object[]
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
			this.cmbDateForeColor.Location = new System.Drawing.Point(359, 55);
			this.cmbDateForeColor.Name = "cmbDateForeColor";
			this.cmbDateForeColor.Size = new System.Drawing.Size(51, 22);
			this.cmbDateForeColor.TabIndex = 53;
			this.cmbDateForeColor.DrawItem += new DrawItemEventHandler(this.cmbDateForeColor_DrawItem);
			this.cmbDateForeColor.SelectedIndexChanged += new EventHandler(this.cmbDateForeColor_SelectedIndexChanged);
			this.cmbZodiacForeColor.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmbZodiacForeColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbZodiacForeColor.FormattingEnabled = true;
			this.cmbZodiacForeColor.ImeMode = ImeMode.On;
			this.cmbZodiacForeColor.Items.AddRange(new object[]
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
			this.cmbZodiacForeColor.Location = new System.Drawing.Point(94, 84);
			this.cmbZodiacForeColor.Name = "cmbZodiacForeColor";
			this.cmbZodiacForeColor.Size = new System.Drawing.Size(51, 22);
			this.cmbZodiacForeColor.TabIndex = 51;
			this.cmbZodiacForeColor.DrawItem += new DrawItemEventHandler(this.cmbZodiacForeColor_DrawItem);
			this.cmbZodiacForeColor.SelectedIndexChanged += new EventHandler(this.cmbZodiacForeColor_SelectedIndexChanged);
			this.cmbTextForeColor.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmbTextForeColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbTextForeColor.FormattingEnabled = true;
			this.cmbTextForeColor.ImeMode = ImeMode.On;
			this.cmbTextForeColor.Items.AddRange(new object[]
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
			this.cmbTextForeColor.Location = new System.Drawing.Point(416, 27);
			this.cmbTextForeColor.Name = "cmbTextForeColor";
			this.cmbTextForeColor.Size = new System.Drawing.Size(51, 22);
			this.cmbTextForeColor.TabIndex = 57;
			this.cmbTextForeColor.DrawItem += new DrawItemEventHandler(this.cmbTextForeColor_DrawItem);
			this.cmbTextForeColor.SelectedIndexChanged += new EventHandler(this.cmbTextForeColor_SelectedIndexChanged);
			this.cmbChineseEraForeColor.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmbChineseEraForeColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbChineseEraForeColor.FormattingEnabled = true;
			this.cmbChineseEraForeColor.ImeMode = ImeMode.On;
			this.cmbChineseEraForeColor.Items.AddRange(new object[]
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
			this.cmbChineseEraForeColor.Location = new System.Drawing.Point(94, 55);
			this.cmbChineseEraForeColor.Name = "cmbChineseEraForeColor";
			this.cmbChineseEraForeColor.Size = new System.Drawing.Size(51, 22);
			this.cmbChineseEraForeColor.TabIndex = 56;
			this.cmbChineseEraForeColor.DrawItem += new DrawItemEventHandler(this.cmbChineseEraForeColor_DrawItem);
			this.cmbChineseEraForeColor.SelectedIndexChanged += new EventHandler(this.cmbChineseEraForeColor_SelectedIndexChanged);
			this.lblChineseEra.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblChineseEra.Location = new System.Drawing.Point(8, 58);
			this.lblChineseEra.Name = "lblChineseEra";
			this.lblChineseEra.Size = new System.Drawing.Size(80, 12);
			this.lblChineseEra.TabIndex = 72;
			this.lblChineseEra.Text = "显示干支";
			this.lblChineseEra.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblZodiac.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblZodiac.Location = new System.Drawing.Point(8, 88);
			this.lblZodiac.Name = "lblZodiac";
			this.lblZodiac.Size = new System.Drawing.Size(80, 12);
			this.lblZodiac.TabIndex = 73;
			this.lblZodiac.Text = "显示属相";
			this.lblZodiac.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblDate.Location = new System.Drawing.Point(247, 58);
			this.lblDate.MinimumSize = new System.Drawing.Size(53, 12);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(107, 12);
			this.lblDate.TabIndex = 74;
			this.lblDate.Text = "显示农历";
			this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblSolarTerm.Location = new System.Drawing.Point(249, 88);
			this.lblSolarTerm.Name = "lblSolarTerm";
			this.lblSolarTerm.Size = new System.Drawing.Size(105, 12);
			this.lblSolarTerm.TabIndex = 77;
			this.lblSolarTerm.Text = "显示节气";
			this.lblSolarTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkSolarTerm.AutoSize = true;
			this.chkSolarTerm.ForeColor = System.Drawing.Color.White;
			this.chkSolarTerm.ImeMode = ImeMode.NoControl;
			this.chkSolarTerm.Location = new System.Drawing.Point(416, 88);
			this.chkSolarTerm.Name = "chkSolarTerm";
			this.chkSolarTerm.Size = new System.Drawing.Size(15, 14);
			this.chkSolarTerm.TabIndex = 76;
			this.chkSolarTerm.UseVisualStyleBackColor = true;
			this.chkSolarTerm.CheckedChanged += new EventHandler(this.cmbSolarTerm_CheckedChanged);
			this.cmbSolarTermForeColor.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmbSolarTermForeColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbSolarTermForeColor.FormattingEnabled = true;
			this.cmbSolarTermForeColor.ImeMode = ImeMode.On;
			this.cmbSolarTermForeColor.Items.AddRange(new object[]
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
			this.cmbSolarTermForeColor.Location = new System.Drawing.Point(359, 84);
			this.cmbSolarTermForeColor.Name = "cmbSolarTermForeColor";
			this.cmbSolarTermForeColor.Size = new System.Drawing.Size(51, 22);
			this.cmbSolarTermForeColor.TabIndex = 75;
			this.cmbSolarTermForeColor.DrawItem += new DrawItemEventHandler(this.cmbSolarTermForeColor_DrawItem);
			this.cmbSolarTermForeColor.SelectedIndexChanged += new EventHandler(this.cmbSolarTermForeColor_SelectedIndexChanged);
			this.rdoAlignLeft.Appearance = Appearance.Button;
			this.rdoAlignLeft.FlatStyle = FlatStyle.Popup;
			this.rdoAlignLeft.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rdoAlignLeft.Image = (System.Drawing.Image)componentResourceManager.GetObject("rdoAlignLeft.Image");
			this.rdoAlignLeft.Location = new System.Drawing.Point(295, 1);
			this.rdoAlignLeft.Name = "rdoAlignLeft";
			this.rdoAlignLeft.Size = new System.Drawing.Size(23, 22);
			this.rdoAlignLeft.TabIndex = 81;
			this.rdoAlignLeft.TabStop = true;
			this.rdoAlignLeft.UseVisualStyleBackColor = true;
			this.rdoAlignLeft.Click += new EventHandler(this.rdoAlignLeft_Click);
			this.rdoAlignCenter.Appearance = Appearance.Button;
			this.rdoAlignCenter.FlatStyle = FlatStyle.Popup;
			this.rdoAlignCenter.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rdoAlignCenter.Image = (System.Drawing.Image)componentResourceManager.GetObject("rdoAlignCenter.Image");
			this.rdoAlignCenter.Location = new System.Drawing.Point(317, 1);
			this.rdoAlignCenter.Name = "rdoAlignCenter";
			this.rdoAlignCenter.Size = new System.Drawing.Size(23, 22);
			this.rdoAlignCenter.TabIndex = 82;
			this.rdoAlignCenter.TabStop = true;
			this.rdoAlignCenter.UseVisualStyleBackColor = true;
			this.rdoAlignCenter.Click += new EventHandler(this.rdoAlignCenter_Click);
			this.rdoAlignRight.Appearance = Appearance.Button;
			this.rdoAlignRight.BackColor = System.Drawing.SystemColors.Control;
			this.rdoAlignRight.FlatStyle = FlatStyle.Popup;
			this.rdoAlignRight.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rdoAlignRight.Image = (System.Drawing.Image)componentResourceManager.GetObject("rdoAlignRight.Image");
			this.rdoAlignRight.Location = new System.Drawing.Point(339, 1);
			this.rdoAlignRight.Name = "rdoAlignRight";
			this.rdoAlignRight.Size = new System.Drawing.Size(23, 22);
			this.rdoAlignRight.TabIndex = 83;
			this.rdoAlignRight.TabStop = true;
			this.rdoAlignRight.UseVisualStyleBackColor = false;
			this.rdoAlignRight.Click += new EventHandler(this.rdoAlignRight_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			base.Controls.Add(this.rdoAlignRight);
			base.Controls.Add(this.rdoAlignCenter);
			base.Controls.Add(this.rdoAlignLeft);
			base.Controls.Add(this.lblSolarTerm);
			base.Controls.Add(this.chkSolarTerm);
			base.Controls.Add(this.cmbSolarTermForeColor);
			base.Controls.Add(this.lblDate);
			base.Controls.Add(this.lblZodiac);
			base.Controls.Add(this.lblChineseEra);
			base.Controls.Add(this.txtText);
			base.Controls.Add(this.chkDate);
			base.Controls.Add(this.chkZodiac);
			base.Controls.Add(this.chkText);
			base.Controls.Add(this.chkChineseEra);
			base.Controls.Add(this.btnFontUnderline);
			base.Controls.Add(this.btnFontItalic);
			base.Controls.Add(this.lblText);
			base.Controls.Add(this.lblFontFamily);
			base.Controls.Add(this.btnFontBold);
			base.Controls.Add(this.cmbLineStyle);
			base.Controls.Add(this.cmbFontSize);
			base.Controls.Add(this.lblFontSize);
			base.Controls.Add(this.cmbFontFamily);
			base.Controls.Add(this.cmbDateForeColor);
			base.Controls.Add(this.cmbZodiacForeColor);
			base.Controls.Add(this.cmbTextForeColor);
			base.Controls.Add(this.cmbChineseEraForeColor);
			base.Name = "LunarEditor";
			base.Size = new System.Drawing.Size(493, 110);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
