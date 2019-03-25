using LedControlSystem.Fonts;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Content;
using LedModel.DataSource;
using LedModel.Enum;
using LedModel.Foundation;
using LedModel.Public;
using OfficeTool.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formRichtext : Form
	{
		private struct PARAFORMAT2
		{
			public int cbSize;

			public uint dwMask;

			public short wNumbering;

			public short wReserved;

			public int dxStartIndent;

			public int dxRightIndent;

			public int dxOffset;

			public short wAlignment;

			public short cTabCount;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public int[] rgxTabs;

			public int dySpaceBefore;

			public int dySpaceAfter;

			public int dyLineSpacing;

			public short sStyle;

			public byte bLineSpacingRule;

			public byte bOutlineLevel;

			public short wShadingWeight;

			public short wShadingStyle;

			public short wNumberingStart;

			public short wNumberingStyle;

			public short wNumberingTab;

			public short wBorderSpace;

			public short wBorderWidth;

			public short wBorders;
		}

		public const int WM_USER = 1024;

		public const int EM_GETPARAFORMAT = 1085;

		public const int EM_SETPARAFORMAT = 1095;

		public const long MAX_TAB_STOPS = 32L;

		public const uint PFM_LINESPACING = 256u;

		private static string formID = "formRichtext";

		public static string TempFile;

		public bool fontFlag;

		public bool sizeFlag;

		private string ClipboardText;

		private string fileName;

		public LedMText MarContent;

		public LedPText PubTContent;

		private List<Thread> ThreadList = new List<Thread>();

		private bool isLoad;

		private bool isWaitingReDraw;

		private bool SaveSuccess = true;

		private bool IsPublicText;

		private string LastClipboardText = "";

		private static StreamReader sr;

		public bool IsNotUpdate_PublicText = true;

		private object thisLock;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton tsbtnOpen;

		private ToolStripButton tsbtnSave;

		private ToolStripLabel tslblFontName;

		private ToolStripComboBox tscmbFontName;

		private ToolStripLabel tslblFontSize;

		private ToolStripComboBox tscmbFontSize;

		private ToolStripButton tsbtnFontBold;

		private ToolStripButton tsbtnFontItalic;

		private ToolStripButton tsbtnFontUnderline;

		private ToolStripButton tsbtnAlignLeft;

		private ToolStripButton tsbtnAlignCenter;

		private ToolStripButton tsbtnAlignRight;

		private PrintableRtf rtxContent;

		private NumericUpDown nudLineSpace;

		private ComboBox cmbColor;

		private OpenFileDialog openFileDialog1;

		private SaveFileDialog saveFileDialog1;

		private System.Windows.Forms.Timer timer1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem Undo_ToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem Cut_ToolStripMenuItem;

		private ToolStripMenuItem Copy_ToolStripMenuItem;

		private ToolStripMenuItem Paste_ToolStripMenuItem;

		private ToolStripMenuItem Delete_ToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem Select_All_ToolStripMenuItem;

		private Label label1;

		private ComboBox cmbBackColor;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripLabel Public_Text_label;

		private ToolStripComboBox Public_Text_toolStripComboBox;

		private Button btnDataSource;

		private Button btnDataSourceRun;

		private Button btnDataSourceStop;

		public event LedGlobal.LedContentEvent ReDraw;

		public static string FormID
		{
			get
			{
				return formRichtext.formID;
			}
			set
			{
				formRichtext.formID = value;
			}
		}

		[DllImport("user32", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref formRichtext.PARAFORMAT2 lParam);

		public static void SetLineSpace(Control ctl, int dyLineSpacing)
		{
			formRichtext.PARAFORMAT2 pARAFORMAT = default(formRichtext.PARAFORMAT2);
			pARAFORMAT.cbSize = Marshal.SizeOf(pARAFORMAT);
			pARAFORMAT.bLineSpacingRule = 4;
			pARAFORMAT.dyLineSpacing = dyLineSpacing * 30;
			pARAFORMAT.dwMask = 256u;
			try
			{
				formRichtext.SendMessage(new HandleRef(ctl, ctl.Handle), 1095, 0, ref pARAFORMAT);
			}
			catch
			{
			}
		}

		public formRichtext()
		{
			this.toolStripSeparator3.Visible = false;
			this.Public_Text_label.Visible = false;
			this.Public_Text_toolStripComboBox.Visible = false;
			this.InitializeComponent();
		}

		public void Language_trans()
		{
			formMain.ML.NowFormID = formRichtext.formID;
			this.Text = formMain.ML.GetStr("formRichtext_FormText");
			this.Undo_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Undo");
			this.Cut_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Cut");
			this.Copy_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Copy");
			this.Paste_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Paste");
			this.Delete_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Delete");
			this.Select_All_ToolStripMenuItem.Text = formMain.ML.GetStr("formRichtext_ToolStripMenuItem_Select_All");
			this.tslblFontName.Text = formMain.ML.GetStr("formRichtext_toolStrip_toolStripLabel_Fonts");
			this.tslblFontSize.Text = formMain.ML.GetStr("formRichtext_toolStrip_toolStripLabel_Size");
			this.label1.Text = formMain.ML.GetStr("formRichtext_label_LineSpacing");
			this.toolStrip1.Items["tsbtnOpen"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tsbtnOpen");
			this.toolStrip1.Items["tsbtnSave"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tsbtnSave");
			this.toolStrip1.Items["tslblFontName"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tslblFontName");
			this.toolStrip1.Items["tslblFontSize"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tslblFontSize");
			this.toolStrip1.Items["tsbtnFontBold"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tsbtnFontBold");
			this.toolStrip1.Items["tsbtnFontItalic"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tsbtnFontItalic");
			this.toolStrip1.Items["tsbtnFontUnderline"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tsbtnFontUnderline");
			this.toolStrip1.Items["tsbtnAlignLeft"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tsbtnAlignLeft");
			this.toolStrip1.Items["tsbtnAlignCenter"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tsbtnAlignCenter");
			this.toolStrip1.Items["tsbtnAlignRight"].Text = formMain.ML.GetStr("formRichtext_ToolStrip_toolStrip1_tsbtnAlignRight");
			this.toolStrip1.Items["Public_Text_label"].Text = formMain.ML.GetStr("Mar_PublicText");
		}

		public formRichtext(int Width, bool Is_PublicText = false)
		{
			this.InitializeComponent();
			this.Setting(Width);
			this.Language_trans();
			this.btnDataSource.FlatAppearance.BorderSize = 0;
			this.btnDataSourceRun.FlatAppearance.BorderSize = 0;
			this.btnDataSourceStop.FlatAppearance.BorderSize = 0;
			if (!Is_PublicText)
			{
				this.toolStripSeparator3.Visible = false;
				this.Public_Text_label.Visible = false;
				this.Public_Text_toolStripComboBox.Visible = false;
				return;
			}
			this.toolStripSeparator3.Visible = true;
			this.Public_Text_label.Visible = true;
			this.Public_Text_toolStripComboBox.Visible = true;
			this.tsbtnOpen.Visible = false;
			this.tsbtnSave.Visible = false;
			base.Size = new System.Drawing.Size(this.Public_Text_label.Size.Width - 55 + 710 + 30, 249);
			this.cmbColor.Location = new System.Drawing.Point(this.Public_Text_label.Size.Width - 55 + 540, 1);
			this.label1.Location = new System.Drawing.Point(this.Public_Text_label.Size.Width - 55 + 585, 6);
			this.nudLineSpace.Location = new System.Drawing.Point(this.Public_Text_label.Size.Width - 55 + 619, 3);
			this.btnDataSource.Location = new System.Drawing.Point(this.nudLineSpace.Left + this.nudLineSpace.Width + 1, 1);
			this.btnDataSource.Location = new System.Drawing.Point(this.nudLineSpace.Left + this.nudLineSpace.Width, 1);
			this.Load_PublicTextList();
			this.rtxContent.ReadOnly = true;
		}

		public void Load_PublicTextList()
		{
			foreach (LedPublicText current in formMain.Ledsys.PublicTexts)
			{
				this.Public_Text_toolStripComboBox.Items.Add(current.Name);
			}
		}

		public void PublictextSelectedAll()
		{
			if (this.IsPublicText)
			{
				this.rtxContent.SelectAll();
			}
		}

		private void Public_Text_Bind_Changed(object sender, EventArgs e)
		{
			try
			{
				string name = this.Public_Text_toolStripComboBox.SelectedItem as string;
				RichTextBox richTextBox = new RichTextBox();
				richTextBox.Rtf = this.rtxContent.Rtf;
				this.rtxContent.Text = formMain.Ledsys.GetPublicText(name).Text;
				this.rtxContent.SelectAll();
				this.rtxContent.ReadOnly = true;
				this.rtxContent.SelectionAlignment = richTextBox.SelectionAlignment;
				if (richTextBox.SelectionColor == System.Drawing.Color.Black)
				{
					this.rtxContent.SelectionColor = System.Drawing.Color.Red;
				}
				else
				{
					this.rtxContent.SelectionColor = richTextBox.SelectionColor;
				}
				this.PubTContent.EPText.PublicText = formMain.Ledsys.GetPublicText(name);
				this.Set_HorizontalAlignment(this.rtxContent.SelectionAlignment);
			}
			catch
			{
			}
		}

		private void Setting(int pWidth)
		{
			this.rtxContent.RightMargin = pWidth + 1;
			if (pWidth > this.rtxContent.Width)
			{
				this.rtxContent.RightMargin = this.rtxContent.Width;
			}
		}

		public void New(string pFilename)
		{
			this.fileName = pFilename;
			this.rtxContent.SaveFile(this.fileName);
			base.ShowDialog();
		}

		public void Edit(string pFilename, LedMText pContent)
		{
			this.MarContent = pContent;
			this.IsPublicText = false;
			this.isLoad = true;
			this.fileName = pFilename;
			try
			{
				this.rtxContent.LoadFile(this.fileName);
			}
			catch
			{
			}
			this.isLoad = false;
			base.ShowDialog();
		}

		public void Edit(string pFilename, LedPText PubContent)
		{
			this.PubTContent = PubContent;
			this.IsPublicText = true;
			this.MarContent = PubContent;
			this.MarContent.EMText.Leading = this.PubTContent.EPText.Leading;
			this.isLoad = true;
			this.fileName = pFilename;
			try
			{
				this.rtxContent.LoadFile(this.fileName);
				if (this.PubTContent.EPText != null && this.PubTContent.EPText.PublicText.Name == string.Empty)
				{
					this.rtxContent.Text = "";
				}
				this.Set_HorizontalAlignment(this.rtxContent.SelectionAlignment);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.isLoad = false;
			base.ShowDialog();
		}

		private void formRichtext_Load(object sender, EventArgs e)
		{
			this.thisLock = new object();
			formRichtext.sr = new StreamReader(new MemoryStream());
			this.cmbBackColor.Visible = false;
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.BackgroundImageLayout = ImageLayout.Stretch;
			RichTextBox richTextBox = new RichTextBox();
			if (this.PubTContent == null)
			{
				this.rtxContent.Select(0, 0);
				this.rtxContent.SelectionColor = System.Drawing.Color.Red;
			}
			else
			{
				this.MarContent = this.PubTContent;
				this.MarContent.EMText.Leading = this.PubTContent.EPText.Leading;
				richTextBox.Rtf = this.rtxContent.Rtf;
				this.rtxContent.SelectAll();
				try
				{
					if (this.PubTContent.EPText.PublicText.Name != null && formMain.Ledsys.GetPublicText(this.PubTContent.EPText.PublicText.Name) != null)
					{
						this.Public_Text_toolStripComboBox.SelectedItem = this.PubTContent.EPText.PublicText.Name;
					}
					else
					{
						LedPublicText publicText = new LedPublicText();
						this.PubTContent.EPText.PublicText = publicText;
					}
				}
				catch
				{
				}
			}
			try
			{
				this.InitControl();
				this.Binding();
			}
			catch
			{
			}
		}

		private void formRichtext_FormClosing(object sender, FormClosingEventArgs e)
		{
			for (int i = 0; i < this.ThreadList.Count; i++)
			{
				if (this.ThreadList[i].ThreadState != ThreadState.Aborted)
				{
					try
					{
						this.ThreadList[i].Abort();
					}
					catch
					{
					}
				}
			}
			try
			{
				this.rtxContent.SaveFile(this.fileName);
				Thread.Sleep(500);
				this.ReDraw(LedContentEventType.Redraw, null);
				LedDataSourceSetting dataSourceSetting = this.GetDataSourceSetting();
				if (dataSourceSetting != null && dataSourceSetting.State == LedRunningState.Running)
				{
					this.ReDraw(LedContentEventType.DataSourceEnabled, null);
				}
			}
			catch
			{
			}
		}

		private void InitControl()
		{
			LedDataSourceSetting dataSourceSetting = this.GetDataSourceSetting();
			if (dataSourceSetting != null)
			{
				this.btnDataSourceRun.Visible = dataSourceSetting.Enabled;
				this.btnDataSourceStop.Visible = dataSourceSetting.Enabled;
				if (dataSourceSetting.Enabled)
				{
					if (dataSourceSetting.State == LedRunningState.Running)
					{
						this.btnDataSourceStop.Visible = true;
						this.btnDataSourceRun.Visible = false;
						this.btnDataSource.Enabled = false;
					}
					else
					{
						this.btnDataSourceStop.Visible = false;
						this.btnDataSourceRun.Visible = true;
					}
				}
			}
			this.tscmbFontName.Items.Clear();
			IList<string> fontFamilies = new FontFamiliesEx().GetFontFamilies();
			if (fontFamilies != null && fontFamilies.Count > 0)
			{
				foreach (string current in fontFamilies)
				{
					this.tscmbFontName.Items.Add(current);
				}
			}
			this.cmbColor.Items.Clear();
			LedColorMode colorMode = formMain.ledsys.SelectedPanel.ColorMode;
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
			if (this.tscmbFontName.Items.Count > 0)
			{
				int num = -1;
				LedFont ledFont = new LedFont();
				for (int i = 0; i < this.tscmbFontName.Items.Count; i++)
				{
					if (this.tscmbFontName.Items[i].ToString() == ledFont.FamilyName)
					{
						num = i;
						break;
					}
					if (this.tscmbFontName.Items[i].ToString() == formMain.DefalutForeignTradeFamilyName)
					{
						num = i;
					}
				}
				this.tscmbFontName.SelectedIndex = ((num < 0) ? 0 : num);
			}
			if (this.tscmbFontSize.Items.Count > 0)
			{
				if (this.rtxContent != null && this.rtxContent.SelectionFont != null)
				{
					this.tscmbFontSize.Text = Math.Floor((double)this.rtxContent.SelectionFont.Size).ToString();
				}
				else
				{
					this.tscmbFontSize.SelectedIndex = 0;
				}
			}
			if (this.cmbColor.Items.Count > 0)
			{
				int num2 = 0;
				if (this.rtxContent != null)
				{
					System.Drawing.Color arg_11F_0 = this.rtxContent.SelectionColor;
					num2 = formMain.FromColorToIndex(this.rtxContent.SelectionColor);
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
				if (this.rtxContent != null)
				{
					System.Drawing.Color arg_184_0 = this.rtxContent.SelectionBackColor;
					num3 = formMain.FromBackColorToIndex(this.rtxContent.SelectionBackColor);
					int count2 = this.cmbBackColor.Items.Count;
					if (num3 > count2 - 1)
					{
						num3 = count2 - 1;
					}
				}
				this.cmbBackColor.SelectedIndex = num3;
			}
			if (this.MarContent != null)
			{
				this.nudLineSpace.Value = this.MarContent.EMText.Leading;
				return;
			}
			this.nudLineSpace.Value = 1.0m;
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

		private void tsbtnOpen_Click(object sender, EventArgs e)
		{
			try
			{
				this.openFileDialog1.Filter = "(*.rtf;*.txt;*.doc)|*.rtf;*.txt;*.doc";
				DateTime now = DateTime.Now;
				try
				{
					if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
					{
						return;
					}
				}
				catch
				{
					this.openFileDialog1.InitialDirectory = formMain.DesktopPath;
					if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
					{
						return;
					}
				}
				DateTime now2 = DateTime.Now;
				string docFileName = this.openFileDialog1.FileName;
				DateTime now3 = DateTime.Now;
				string text = this.Doc2Text(docFileName);
				DateTime now4 = DateTime.Now;
    //            now2= now2 - now;
				//now3 - now2;
				//now4 - now3;
				this.rtxContent.Text = text;
				this.rtxContent.SelectAll();
				this.rtxContent.SelectionColor = System.Drawing.Color.Red;
				this.rtxContent.Select(0, 0);
			}
			catch
			{
			}
		}

		public string Doc2Text(string docFileName)
		{
			string result;
			try
			{
				if (docFileName.ToLower().EndsWith(".txt"))
				{
					StreamReader streamReader = new StreamReader(docFileName, Encoding.Default);
					string text = streamReader.ReadToEnd();
					streamReader.Dispose();
					result = text;
				}
				else
				{
					try
					{
						WordTool wordTool = new WordTool(docFileName);
						string text2 = wordTool.GetText();
						result = text2;
						return result;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
					result = this.rtxContent.Text;
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private void tsbtnSave_Click(object sender, EventArgs e)
		{
			this.saveFileDialog1.DefaultExt = "*.rtf";
			this.saveFileDialog1.Filter = ".rtf|*.rtf";
			try
			{
				if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
			catch
			{
				this.saveFileDialog1.InitialDirectory = formMain.DesktopPath;
				if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
			this.rtxContent.SaveFile(this.saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
		}

		private void tscmbFontName_SelectedIndexChanged(object sender, EventArgs e)
		{
			ToolStripComboBox toolStripComboBox = (ToolStripComboBox)sender;
			if (!toolStripComboBox.Focused)
			{
				return;
			}
			try
			{
				this.PublictextSelectedAll();
				int selectionStart = this.rtxContent.SelectionStart;
				int selectionLength = this.rtxContent.SelectionLength;
				System.Drawing.Font pFont;
				if (this.rtxContent.SelectionFont == null)
				{
					pFont = new System.Drawing.Font(toolStripComboBox.Text, this.rtxContent.Font.Size, this.rtxContent.Font.Style);
				}
				else
				{
					pFont = new System.Drawing.Font(toolStripComboBox.Text, this.rtxContent.SelectionFont.Size, this.rtxContent.SelectionFont.Style);
				}
				this.UpdateFont(pFont, selectionStart, selectionLength);
			}
			catch
			{
			}
		}

		private void UpdateFont(System.Drawing.Font pFont, int pStart, int Length)
		{
			if (pFont != null)
			{
				this.rtxContent.SelectionStart = pStart;
				this.rtxContent.SelectionLength = Length;
				this.rtxContent.SelectionFont = pFont;
			}
		}

		private void tscmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ToolStripComboBox toolStripComboBox = (ToolStripComboBox)sender;
				if (toolStripComboBox.Focused)
				{
					this.PublictextSelectedAll();
					int selectionStart = this.rtxContent.SelectionStart;
					int selectionLength = this.rtxContent.SelectionLength;
					System.Drawing.Font pFont;
					if (this.rtxContent.SelectionFont == null)
					{
						pFont = new System.Drawing.Font(this.rtxContent.Font.Name, Convert.ToSingle(toolStripComboBox.Text), this.rtxContent.Font.Style);
					}
					else
					{
						pFont = new System.Drawing.Font(this.rtxContent.SelectionFont.Name, Convert.ToSingle(toolStripComboBox.Text), this.rtxContent.SelectionFont.Style);
					}
					this.UpdateFont(pFont, selectionStart, selectionLength);
				}
			}
			catch
			{
			}
		}

		private void tscmbFontSize_TextChanged(object sender, EventArgs e)
		{
			ToolStripComboBox toolStripComboBox = (ToolStripComboBox)sender;
			if (!toolStripComboBox.Focused)
			{
				return;
			}
			int selectionStart = this.rtxContent.SelectionStart;
			int selectionLength = this.rtxContent.SelectionLength;
			float num = 8f;
			if (!string.IsNullOrEmpty(toolStripComboBox.Text))
			{
				try
				{
					num = float.Parse(toolStripComboBox.Text);
				}
				catch
				{
				}
				if (num > 800f || (double)num == 0.0)
				{
					num = 8f;
					toolStripComboBox.Text = ((int)num).ToString();
					return;
				}
			}
			try
			{
				System.Drawing.Font pFont;
				if (this.rtxContent.SelectionFont == null)
				{
					pFont = new System.Drawing.Font(this.rtxContent.Font.Name, num, this.rtxContent.Font.Style);
				}
				else
				{
					pFont = new System.Drawing.Font(this.rtxContent.SelectionFont.Name, num, this.rtxContent.SelectionFont.Style);
				}
				this.UpdateFont(pFont, selectionStart, selectionLength);
			}
			catch
			{
			}
		}

		private void tscmbFontSize_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void tsbtnFontBold_Click(object sender, EventArgs e)
		{
			try
			{
				this.PublictextSelectedAll();
				ToolStripButton tsbtn = (ToolStripButton)sender;
				this.SetFontStyle(tsbtn, System.Drawing.FontStyle.Bold);
			}
			catch
			{
			}
		}

		private void tsbtnFontItalic_Click(object sender, EventArgs e)
		{
			try
			{
				this.PublictextSelectedAll();
				ToolStripButton tsbtn = (ToolStripButton)sender;
				this.SetFontStyle(tsbtn, System.Drawing.FontStyle.Italic);
			}
			catch
			{
			}
		}

		private void tsbtnFontUnderline_Click(object sender, EventArgs e)
		{
			try
			{
				this.PublictextSelectedAll();
				ToolStripButton tsbtn = (ToolStripButton)sender;
				this.SetFontStyle(tsbtn, System.Drawing.FontStyle.Underline);
			}
			catch
			{
			}
		}

		private void SetFontStyle(ToolStripButton tsbtn, System.Drawing.FontStyle fs)
		{
			int selectionStart = this.rtxContent.SelectionStart;
			int selectionLength = this.rtxContent.SelectionLength;
			if (selectionLength > 0)
			{
				RichTextBox richTextBox = new RichTextBox();
				richTextBox.Rtf = this.rtxContent.SelectedRtf;
				bool flag = false;
				for (int i = 0; i < selectionLength; i++)
				{
					richTextBox.Select(i, 1);
					System.Drawing.FontStyle fontStyle = richTextBox.SelectionFont.Style;
					if (i == 0 && (fontStyle & fs) != fs)
					{
						flag = true;
					}
					if ((fontStyle & fs) == fs && !tsbtn.Checked)
					{
						fontStyle &= ~fs;
					}
					else if (flag)
					{
						fontStyle |= fs;
					}
					System.Drawing.Font selectionFont = new System.Drawing.Font(richTextBox.SelectionFont.OriginalFontName, richTextBox.SelectionFont.Size, fontStyle);
					richTextBox.SelectionFont = selectionFont;
				}
				richTextBox.Select(0, selectionLength);
				this.rtxContent.SelectedRtf = richTextBox.SelectedRtf;
			}
			else
			{
				System.Drawing.FontStyle fontStyle2 = this.rtxContent.SelectionFont.Style;
				if ((this.rtxContent.SelectionFont.Style & fs) == fs && !tsbtn.Checked)
				{
					fontStyle2 &= ~fs;
				}
				else
				{
					fontStyle2 |= fs;
				}
				System.Drawing.Font selectionFont2 = new System.Drawing.Font(this.rtxContent.SelectionFont.OriginalFontName, this.rtxContent.SelectionFont.Size, fontStyle2);
				this.rtxContent.SelectionFont = selectionFont2;
			}
			this.rtxContent.Select(selectionStart, selectionLength);
		}

		private void tsbtnAlignLeft_Click(object sender, EventArgs e)
		{
			this.rtxContent.SelectionAlignment = HorizontalAlignment.Left;
			ToolStripButton toolStripButton = (ToolStripButton)sender;
			if (!toolStripButton.Checked)
			{
				toolStripButton.Checked = true;
				this.tsbtnAlignCenter.Checked = false;
				this.tsbtnAlignRight.Checked = false;
			}
		}

		private void tsbtnAlignCenter_Click(object sender, EventArgs e)
		{
			this.rtxContent.SelectionAlignment = HorizontalAlignment.Center;
			ToolStripButton toolStripButton = (ToolStripButton)sender;
			if (!toolStripButton.Checked)
			{
				toolStripButton.Checked = true;
				this.tsbtnAlignLeft.Checked = false;
				this.tsbtnAlignRight.Checked = false;
			}
		}

		private void tsbtnAlignRight_Click(object sender, EventArgs e)
		{
			this.rtxContent.SelectionAlignment = HorizontalAlignment.Right;
			ToolStripButton toolStripButton = (ToolStripButton)sender;
			if (!toolStripButton.Checked)
			{
				toolStripButton.Checked = true;
				this.tsbtnAlignLeft.Checked = false;
				this.tsbtnAlignCenter.Checked = false;
			}
		}

		private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.PublictextSelectedAll();
			this.rtxContent.SelectionColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
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

		private void cmbBackColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			int selectionStart = this.rtxContent.SelectionStart;
			int selectionLength = this.rtxContent.SelectionLength;
			this.rtxContent.SelectAll();
			this.rtxContent.SelectionBackColor = formMain.FromIndexToBackColor(comboBox.SelectedIndex);
			comboBox.SelectedIndex = formMain.FromColorToIndex(this.rtxContent.SelectionBackColor);
			this.rtxContent.Select(selectionStart, selectionLength);
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

		private void nudLineSpace_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			try
			{
				base.CreateGraphics();
				formRichtext.PARAFORMAT2 pARAFORMAT = default(formRichtext.PARAFORMAT2);
				pARAFORMAT.cbSize = Marshal.SizeOf(pARAFORMAT);
				pARAFORMAT.bLineSpacingRule = 4;
				pARAFORMAT.dyLineSpacing = (int)(250m * this.nudLineSpace.Value);
				pARAFORMAT.dwMask = 256u;
				formRichtext.SendMessage(new HandleRef(this.rtxContent, this.rtxContent.Handle), 1095, 0, ref pARAFORMAT);
				this.rtxContent.SaveFile(this.fileName, RichTextBoxStreamType.RichText);
				this.MarContent.EMText.Leading = (int)numericUpDown.Value;
				this.PubTContent.EPText.Leading = (int)numericUpDown.Value;
			}
			catch
			{
			}
		}

		private void rtxContent_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.V)
			{
				try
				{
					if (!this.IsPublicText)
					{
						this.ClipboardText = Clipboard.GetText();
						if (this.ClipboardText.EndsWith("\r\n"))
						{
							this.ClipboardText = this.ClipboardText.Substring(0, this.ClipboardText.Length - 4);
						}
						else if (this.ClipboardText.EndsWith("\r"))
						{
							this.ClipboardText = this.ClipboardText.Substring(0, this.ClipboardText.Length - 2);
						}
						else if (this.ClipboardText.EndsWith("\n"))
						{
							this.ClipboardText = this.ClipboardText.Substring(0, this.ClipboardText.Length - 2);
						}
						int selectionStart = this.rtxContent.SelectionStart;
						int selectionLength = this.rtxContent.SelectionLength;
						this.rtxContent.SelectedText = this.ClipboardText;
						if (selectionLength > 0)
						{
							this.rtxContent.Select(selectionStart, this.ClipboardText.Length);
							this.rtxContent.SelectionColor = formMain.FromIndexToColor(this.cmbColor.SelectedIndex);
							this.rtxContent.Select(selectionStart + this.ClipboardText.Length, 0);
						}
					}
					e.Handled = true;
				}
				catch
				{
				}
			}
		}

		private void rtxContent_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (!this.isLoad)
				{
					if (this.IsNotUpdate_PublicText)
					{
						this.SaveAndDraw();
					}
				}
			}
			catch
			{
			}
		}

		public void Set_HorizontalAlignment(HorizontalAlignment align)
		{
			if (align == HorizontalAlignment.Left)
			{
				this.tsbtnAlignLeft.Checked = true;
				this.tsbtnAlignCenter.Checked = false;
				this.tsbtnAlignRight.Checked = false;
				return;
			}
			if (align == HorizontalAlignment.Center)
			{
				this.tsbtnAlignLeft.Checked = false;
				this.tsbtnAlignCenter.Checked = true;
				this.tsbtnAlignRight.Checked = false;
				return;
			}
			this.tsbtnAlignLeft.Checked = false;
			this.tsbtnAlignCenter.Checked = false;
			this.tsbtnAlignRight.Checked = true;
		}

		private void rtxContent_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				int selectionStart = this.rtxContent.SelectionStart;
				int selectionLength = this.rtxContent.SelectionLength;
				RichTextBox richTextBox = new RichTextBox();
				richTextBox.Rtf = this.rtxContent.Rtf;
				if (selectionLength > 0)
				{
					richTextBox.Select(selectionStart, 1);
				}
				else if (selectionStart > 0)
				{
					richTextBox.Select(selectionStart - 1, 1);
				}
				else if (richTextBox.TextLength > 0)
				{
					richTextBox.Select(0, 1);
				}
				if (richTextBox.TextLength > 0)
				{
					System.Drawing.FontStyle style = richTextBox.SelectionFont.Style;
					if ((style & System.Drawing.FontStyle.Bold) == System.Drawing.FontStyle.Bold)
					{
						this.tsbtnFontBold.Checked = true;
					}
					else
					{
						this.tsbtnFontBold.Checked = false;
					}
					if ((style & System.Drawing.FontStyle.Italic) == System.Drawing.FontStyle.Italic)
					{
						this.tsbtnFontItalic.Checked = true;
					}
					else
					{
						this.tsbtnFontItalic.Checked = false;
					}
					if ((style & System.Drawing.FontStyle.Underline) == System.Drawing.FontStyle.Underline)
					{
						this.tsbtnFontUnderline.Checked = true;
					}
					else
					{
						this.tsbtnFontUnderline.Checked = false;
					}
				}
				HorizontalAlignment selectionAlignment = richTextBox.SelectionAlignment;
				if (selectionLength > 0)
				{
					richTextBox.Select(selectionStart, 1);
					selectionAlignment = richTextBox.SelectionAlignment;
				}
				this.Set_HorizontalAlignment(selectionAlignment);
			}
			catch
			{
			}
		}

		private void SetClipboardText()
		{
			try
			{
				Thread.Sleep(200);
				this.ClipboardText = Clipboard.GetText();
				if (this.ClipboardText != this.LastClipboardText)
				{
					this.ClipboardText = this.ClipboardText.Replace(" ", "");
					this.ClipboardText = this.ClipboardText.Replace("\r", "");
					this.ClipboardText = this.ClipboardText.Replace("\n", "");
					Clipboard.Clear();
					Clipboard.SetText(this.ClipboardText, TextDataFormat.Text);
					this.LastClipboardText = this.ClipboardText;
				}
			}
			catch
			{
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.isWaitingReDraw)
			{
				try
				{
					this.isWaitingReDraw = false;
					this.SaveAndDraw();
				}
				catch
				{
				}
			}
		}

		private void Draw()
		{
			if (this.isLoad)
			{
				return;
			}
			this.isWaitingReDraw = true;
		}

		private void SaveAndDraw()
		{
			Thread thread = new Thread(new ThreadStart(this.StartSaveAndDraw));
			this.ThreadList.Add(thread);
			thread.Start();
		}

		private void StartSaveAndDraw()
		{
			if (this.SaveSuccess)
			{
				this.SaveSuccess = false;
				int num = 10;
				while (num-- > 0)
				{
					lock (this.thisLock)
					{
						try
						{
							this.rtxContent.SaveFile(this.fileName);
							this.ReDraw(LedContentEventType.Redraw, null);
							this.SaveSuccess = true;
							this.MarContent.DoNeedDrawingFull = true;
							break;
						}
						catch
						{
						}
					}
				}
				this.SaveSuccess = true;
			}
		}

		private void btnDataSource_Click(object sender, EventArgs e)
		{
			LedDataSourceSetting dataSourceSetting = this.GetDataSourceSetting();
			if (dataSourceSetting != null)
			{
				formDataSourceSetting formDataSourceSetting = new formDataSourceSetting(dataSourceSetting);
				formDataSourceSetting.ShowDialog(this);
				this.btnDataSourceRun.Visible = dataSourceSetting.Enabled;
			}
		}

		private void btnDataSourceRun_Click(object sender, EventArgs e)
		{
			LedDataSourceSetting dataSourceSetting = this.GetDataSourceSetting();
			if (dataSourceSetting != null)
			{
				this.btnDataSourceRun.Visible = false;
				this.btnDataSourceStop.Visible = true;
				this.btnDataSource.Enabled = false;
				dataSourceSetting.State = LedRunningState.Running;
			}
		}

		private void btnDataSourceStop_Click(object sender, EventArgs e)
		{
			LedDataSourceSetting dataSourceSetting = this.GetDataSourceSetting();
			if (dataSourceSetting != null)
			{
				this.btnDataSourceRun.Visible = true;
				this.btnDataSourceStop.Visible = false;
				this.btnDataSource.Enabled = true;
				dataSourceSetting.State = LedRunningState.Stopped;
			}
		}

		private void btnDataSource_MouseEnter(object sender, EventArgs e)
		{
			ToolTip toolTip = new ToolTip();
			toolTip.SetToolTip(this.btnDataSource, "数据源");
		}

		private void btnDataSourceRun_MouseEnter(object sender, EventArgs e)
		{
			ToolTip toolTip = new ToolTip();
			toolTip.SetToolTip(this.btnDataSourceRun, "启动");
		}

		private void btnDataSourceStop_MouseEnter(object sender, EventArgs e)
		{
			ToolTip toolTip = new ToolTip();
			toolTip.SetToolTip(this.btnDataSourceStop, "停止");
		}

		private LedDataSourceSetting GetDataSourceSetting()
		{
			LedDataSourceSetting result = null;
			if (this.IsPublicText)
			{
				if (this.PubTContent != null)
				{
					result = this.PubTContent.DataSourceSetting;
				}
			}
			else if (this.MarContent != null)
			{
				result = this.MarContent.DataSourceSetting;
			}
			return result;
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(formRichtext));
			this.toolStrip1 = new ToolStrip();
			this.tsbtnOpen = new ToolStripButton();
			this.tsbtnSave = new ToolStripButton();
			this.tslblFontName = new ToolStripLabel();
			this.tscmbFontName = new ToolStripComboBox();
			this.tslblFontSize = new ToolStripLabel();
			this.tscmbFontSize = new ToolStripComboBox();
			this.tsbtnFontBold = new ToolStripButton();
			this.tsbtnFontItalic = new ToolStripButton();
			this.tsbtnFontUnderline = new ToolStripButton();
			this.tsbtnAlignLeft = new ToolStripButton();
			this.tsbtnAlignCenter = new ToolStripButton();
			this.tsbtnAlignRight = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.Public_Text_label = new ToolStripLabel();
			this.Public_Text_toolStripComboBox = new ToolStripComboBox();
			this.nudLineSpace = new NumericUpDown();
			this.cmbColor = new ComboBox();
			this.openFileDialog1 = new OpenFileDialog();
			this.saveFileDialog1 = new SaveFileDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.Undo_ToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.Cut_ToolStripMenuItem = new ToolStripMenuItem();
			this.Copy_ToolStripMenuItem = new ToolStripMenuItem();
			this.Paste_ToolStripMenuItem = new ToolStripMenuItem();
			this.Delete_ToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.Select_All_ToolStripMenuItem = new ToolStripMenuItem();
			this.label1 = new Label();
			this.cmbBackColor = new ComboBox();
			this.btnDataSource = new Button();
			this.btnDataSourceRun = new Button();
			this.btnDataSourceStop = new Button();
			this.rtxContent = new PrintableRtf();
			this.toolStrip1.SuspendLayout();
			((ISupportInitialize)this.nudLineSpace).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnOpen,
				this.tsbtnSave,
				this.tslblFontName,
				this.tscmbFontName,
				this.tslblFontSize,
				this.tscmbFontSize,
				this.tsbtnFontBold,
				this.tsbtnFontItalic,
				this.tsbtnFontUnderline,
				this.tsbtnAlignLeft,
				this.tsbtnAlignCenter,
				this.tsbtnAlignRight,
				this.toolStripSeparator3,
				this.Public_Text_label,
				this.Public_Text_toolStripComboBox
			});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(614, 25);
			this.toolStrip1.TabIndex = 10;
			this.toolStrip1.Text = "左对齐";
			this.tsbtnOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnOpen.Image = (System.Drawing.Image)resources.GetObject("tsbtnOpen.Image");
			this.tsbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnOpen.Name = "tsbtnOpen";
			this.tsbtnOpen.Size = new System.Drawing.Size(23, 22);
			this.tsbtnOpen.Text = "打开";
			this.tsbtnOpen.Click += new EventHandler(this.tsbtnOpen_Click);
			this.tsbtnSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnSave.Image = (System.Drawing.Image)resources.GetObject("tsbtnSave.Image");
			this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSave.Name = "tsbtnSave";
			this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
			this.tsbtnSave.Text = "保存";
			this.tsbtnSave.Click += new EventHandler(this.tsbtnSave_Click);
			this.tslblFontName.Name = "tslblFontName";
			this.tslblFontName.Size = new System.Drawing.Size(32, 22);
			this.tslblFontName.Text = "字体";
			this.tscmbFontName.AutoSize = false;
			this.tscmbFontName.DropDownStyle = ComboBoxStyle.DropDownList;
			this.tscmbFontName.DropDownWidth = 100;
			this.tscmbFontName.Name = "tscmbFontName";
			this.tscmbFontName.Size = new System.Drawing.Size(110, 25);
			this.tscmbFontName.SelectedIndexChanged += new EventHandler(this.tscmbFontName_SelectedIndexChanged);
			this.tslblFontSize.Name = "tslblFontSize";
			this.tslblFontSize.Size = new System.Drawing.Size(32, 22);
			this.tslblFontSize.Text = "大小";
			this.tscmbFontSize.AutoSize = false;
			this.tscmbFontSize.DropDownWidth = 50;
			this.tscmbFontSize.Items.AddRange(new object[]
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
			this.tscmbFontSize.MaxLength = 5;
			this.tscmbFontSize.Name = "tscmbFontSize";
			this.tscmbFontSize.Size = new System.Drawing.Size(45, 25);
			this.tscmbFontSize.Text = "12";
			this.tscmbFontSize.SelectedIndexChanged += new EventHandler(this.tscmbFontSize_SelectedIndexChanged);
			this.tscmbFontSize.KeyPress += new KeyPressEventHandler(this.tscmbFontSize_KeyPress);
			this.tscmbFontSize.TextChanged += new EventHandler(this.tscmbFontSize_TextChanged);
			this.tsbtnFontBold.BackColor = System.Drawing.Color.Transparent;
			this.tsbtnFontBold.CheckOnClick = true;
			this.tsbtnFontBold.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnFontBold.Image = (System.Drawing.Image)resources.GetObject("tsbtnFontBold.Image");
			this.tsbtnFontBold.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFontBold.Name = "tsbtnFontBold";
			this.tsbtnFontBold.Size = new System.Drawing.Size(23, 22);
			this.tsbtnFontBold.Tag = "false";
			this.tsbtnFontBold.Text = "加粗";
			this.tsbtnFontBold.Click += new EventHandler(this.tsbtnFontBold_Click);
			this.tsbtnFontItalic.CheckOnClick = true;
			this.tsbtnFontItalic.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnFontItalic.Image = (System.Drawing.Image)resources.GetObject("tsbtnFontItalic.Image");
			this.tsbtnFontItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFontItalic.Name = "tsbtnFontItalic";
			this.tsbtnFontItalic.Size = new System.Drawing.Size(23, 22);
			this.tsbtnFontItalic.Tag = "false";
			this.tsbtnFontItalic.Text = "斜体";
			this.tsbtnFontItalic.Click += new EventHandler(this.tsbtnFontItalic_Click);
			this.tsbtnFontUnderline.CheckOnClick = true;
			this.tsbtnFontUnderline.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnFontUnderline.Image = (System.Drawing.Image)resources.GetObject("tsbtnFontUnderline.Image");
			this.tsbtnFontUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFontUnderline.Name = "tsbtnFontUnderline";
			this.tsbtnFontUnderline.Size = new System.Drawing.Size(23, 22);
			this.tsbtnFontUnderline.Tag = "false";
			this.tsbtnFontUnderline.Text = "下划线";
			this.tsbtnFontUnderline.Click += new EventHandler(this.tsbtnFontUnderline_Click);
			this.tsbtnAlignLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnAlignLeft.Image = (System.Drawing.Image)resources.GetObject("tsbtnAlignLeft.Image");
			this.tsbtnAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAlignLeft.Name = "tsbtnAlignLeft";
			this.tsbtnAlignLeft.Size = new System.Drawing.Size(23, 22);
			this.tsbtnAlignLeft.Text = "左对齐";
			this.tsbtnAlignLeft.Click += new EventHandler(this.tsbtnAlignLeft_Click);
			this.tsbtnAlignCenter.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnAlignCenter.Image = (System.Drawing.Image)resources.GetObject("tsbtnAlignCenter.Image");
			this.tsbtnAlignCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAlignCenter.Name = "tsbtnAlignCenter";
			this.tsbtnAlignCenter.Size = new System.Drawing.Size(23, 22);
			this.tsbtnAlignCenter.Text = "水平居中";
			this.tsbtnAlignCenter.Click += new EventHandler(this.tsbtnAlignCenter_Click);
			this.tsbtnAlignRight.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbtnAlignRight.Image = (System.Drawing.Image)resources.GetObject("tsbtnAlignRight.Image");
			this.tsbtnAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAlignRight.Name = "tsbtnAlignRight";
			this.tsbtnAlignRight.Size = new System.Drawing.Size(23, 22);
			this.tsbtnAlignRight.Text = "右对齐";
			this.tsbtnAlignRight.Click += new EventHandler(this.tsbtnAlignRight_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			this.Public_Text_label.Name = "Public_Text_label";
			this.Public_Text_label.Size = new System.Drawing.Size(56, 22);
			this.Public_Text_label.Text = "公共文本";
			this.Public_Text_toolStripComboBox.Name = "Public_Text_toolStripComboBox";
			this.Public_Text_toolStripComboBox.Size = new System.Drawing.Size(100, 25);
			this.Public_Text_toolStripComboBox.SelectedIndexChanged += new EventHandler(this.Public_Text_Bind_Changed);
			this.nudLineSpace.DecimalPlaces = 2;
			this.nudLineSpace.Increment = new decimal(new int[]
			{
				125,
				0,
				0,
				196608
			});
			this.nudLineSpace.Location = new System.Drawing.Point(498, 3);
			this.nudLineSpace.Minimum = new decimal(new int[]
			{
				125,
				0,
				0,
				196608
			});
			this.nudLineSpace.Name = "nudLineSpace";
			this.nudLineSpace.Size = new System.Drawing.Size(50, 21);
			this.nudLineSpace.TabIndex = 12;
			this.nudLineSpace.Value = new decimal(new int[]
			{
				10,
				0,
				0,
				65536
			});
			this.nudLineSpace.ValueChanged += new EventHandler(this.nudLineSpace_ValueChanged);
			this.cmbColor.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbColor.FlatStyle = FlatStyle.Flat;
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
			this.cmbColor.Location = new System.Drawing.Point(419, 1);
			this.cmbColor.Name = "cmbColor";
			this.cmbColor.Size = new System.Drawing.Size(44, 22);
			this.cmbColor.TabIndex = 14;
			this.cmbColor.DrawItem += new DrawItemEventHandler(this.cmbColor_DrawItem);
			this.cmbColor.SelectedIndexChanged += new EventHandler(this.cmbColor_SelectedIndexChanged);
			this.openFileDialog1.FileName = "openFileDialog1";
			this.timer1.Interval = 1500;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
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
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.ImeMode = ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(464, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 13;
			this.label1.Text = "行距";
			this.cmbBackColor.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmbBackColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbBackColor.FlatStyle = FlatStyle.Flat;
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
			this.cmbBackColor.Location = new System.Drawing.Point(459, 1);
			this.cmbBackColor.Name = "cmbBackColor";
			this.cmbBackColor.Size = new System.Drawing.Size(44, 22);
			this.cmbBackColor.TabIndex = 15;
			this.cmbBackColor.DrawItem += new DrawItemEventHandler(this.cmbBackColor_DrawItem);
			this.cmbBackColor.SelectedIndexChanged += new EventHandler(this.cmbBackColor_SelectedIndexChanged);
			this.btnDataSource.BackgroundImage = Resources.datasource;
			this.btnDataSource.BackgroundImageLayout = ImageLayout.Center;
			this.btnDataSource.FlatStyle = FlatStyle.Flat;
			this.btnDataSource.Location = new System.Drawing.Point(549, 1);
			this.btnDataSource.Name = "btnDataSource";
			this.btnDataSource.Size = new System.Drawing.Size(23, 23);
			this.btnDataSource.TabIndex = 16;
			this.btnDataSource.UseVisualStyleBackColor = true;
			this.btnDataSource.Click += new EventHandler(this.btnDataSource_Click);
			this.btnDataSource.MouseEnter += new EventHandler(this.btnDataSource_MouseEnter);
			this.btnDataSourceRun.BackgroundImage = Resources.play;
			this.btnDataSourceRun.BackgroundImageLayout = ImageLayout.Center;
			this.btnDataSourceRun.FlatStyle = FlatStyle.Flat;
			this.btnDataSourceRun.Location = new System.Drawing.Point(572, 1);
			this.btnDataSourceRun.Name = "btnDataSourceRun";
			this.btnDataSourceRun.Size = new System.Drawing.Size(23, 23);
			this.btnDataSourceRun.TabIndex = 17;
			this.btnDataSourceRun.UseVisualStyleBackColor = true;
			this.btnDataSourceRun.Click += new EventHandler(this.btnDataSourceRun_Click);
			this.btnDataSourceRun.MouseEnter += new EventHandler(this.btnDataSourceRun_MouseEnter);
			this.btnDataSourceStop.BackgroundImage = Resources.stop;
			this.btnDataSourceStop.BackgroundImageLayout = ImageLayout.Stretch;
			this.btnDataSourceStop.FlatStyle = FlatStyle.Flat;
			this.btnDataSourceStop.Location = new System.Drawing.Point(572, 1);
			this.btnDataSourceStop.Name = "btnDataSourceStop";
			this.btnDataSourceStop.Size = new System.Drawing.Size(23, 23);
			this.btnDataSourceStop.TabIndex = 18;
			this.btnDataSourceStop.UseVisualStyleBackColor = true;
			this.btnDataSourceStop.Click += new EventHandler(this.btnDataSourceStop_Click);
			this.btnDataSourceStop.MouseEnter += new EventHandler(this.btnDataSourceStop_MouseEnter);
			this.rtxContent.AcceptsTab = true;
			this.rtxContent.BackColor = System.Drawing.Color.Black;
			this.rtxContent.BorderStyle = BorderStyle.None;
			this.rtxContent.ContextMenuStrip = this.contextMenuStrip1;
			this.rtxContent.DetectUrls = false;
			this.rtxContent.Dock = DockStyle.Fill;
			this.rtxContent.EnableAutoDragDrop = true;
			this.rtxContent.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.rtxContent.HideSelection = false;
			this.rtxContent.ImeMode = ImeMode.NoControl;
			this.rtxContent.Location = new System.Drawing.Point(0, 25);
			this.rtxContent.Name = "rtxContent";
			this.rtxContent.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtxContent.Size = new System.Drawing.Size(614, 185);
			this.rtxContent.TabIndex = 11;
			this.rtxContent.Text = "";
			this.rtxContent.SelectionChanged += new EventHandler(this.rtxContent_SelectionChanged);
			this.rtxContent.TextChanged += new EventHandler(this.rtxContent_TextChanged);
			this.rtxContent.KeyDown += new KeyEventHandler(this.rtxContent_KeyDown);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(614, 210);
			base.Controls.Add(this.btnDataSourceStop);
			base.Controls.Add(this.btnDataSourceRun);
			base.Controls.Add(this.btnDataSource);
			base.Controls.Add(this.cmbBackColor);
			base.Controls.Add(this.cmbColor);
			base.Controls.Add(this.nudLineSpace);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.rtxContent);
			base.Controls.Add(this.toolStrip1);
			base.Name = "formRichtext";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "文本编辑";
			base.FormClosing += new FormClosingEventHandler(this.formRichtext_FormClosing);
			base.Load += new EventHandler(this.formRichtext_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((ISupportInitialize)this.nudLineSpace).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
