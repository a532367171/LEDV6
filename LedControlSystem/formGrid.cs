using FlexCell;
using LedControlSystem.Fonts;
using LedControlSystem.Properties;
using LedModel.Enum;
using LedModel.Foundation;
using OfficeTool.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem.LedControlSystem
{
	public class formGrid : Form
	{
		private delegate void ShowAutoFitMsgDelegate();

		private delegate void StartTimerDelegate();

		private static string formID = "formGrid";

		private string fileName = "";

		private formMain fm;

		private Grid grid1;

		private int maxWidth;

		public static string ImportFileName = "";

		public static int ImportSheetIndex = 0;

		public static bool NeedImport = false;

		private Thread thrSaveRedraw;

		private bool isThreadSaveRedraw;

		private bool isThreadSaveRedrawSuspended;

		private int intervalSaveRedraw = 10;

		private object thisLockObject = new object();

		private bool isMaxSize;

		private System.Drawing.Size normalSize;

		private System.Drawing.Point normalLocation;

		private int nowRowNum;

		private int totalRowNum;

		private bool isCancelled;

		private Thread thrRowHeightAutoFit;

		private formGrid.ShowAutoFitMsgDelegate showAutoFitMsg;

		private object this_LockObject = new object();

		private IContainer components;

		private MenuStrip menuStrip1;

		private Panel panel1;

		private ToolStripMenuItem fileExcelToolStripMenuItem;

		private ToolStripMenuItem importExcel_ToolStripMenuItem;

		private ToolStripMenuItem export_XmlToolStripMenuItem;

		private ToolStripMenuItem save_ToolStripMenuItem;

		private ToolStripMenuItem exit_ToolStripMenuItem;

		private ToolStripMenuItem EditToolStripMenuItem;

		private ToolStripMenuItem CopyToolStripMenuItem;

		private ToolStripMenuItem CutToolStripMenuItem;

		private ToolStripMenuItem pasteToolStripMenuItem;

		private ToolStripMenuItem propertyToolStripMenuItem;

		private OpenFileDialog openFileDialog1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem clearToolStripMenuItem;

		private ToolStripMenuItem clearContentToolStripMenuItem;

		private ToolStripMenuItem clearFormatToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem clearAllToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem mergeToolStripMenuItem;

		private ToolStripMenuItem noMergeToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem insertRowToolStripMenuItem;

		private ToolStripMenuItem insertColumnToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ToolStripMenuItem removeColumnToolStripMenuItem;

		private ToolStripMenuItem fontToolStripMenuItem1;

		private ToolStripMenuItem borderToolStripMenuItem1;

		private ToolStripMenuItem borderNoneToolStripMenuItem;

		private ToolStripMenuItem borderLeftToolStripMenuItem;

		private ToolStripMenuItem borderRightToolStripMenuItem;

		private ToolStripMenuItem borderTopToolStripMenuItem;

		private ToolStripMenuItem borderBottomToolStripMenuItem;

		private ToolStripMenuItem borderLineUpToolStripMenuItem;

		private ToolStripMenuItem borderLineDownToolStripMenuItem;

		private ToolStripMenuItem borderInsideToolStripMenuItem;

		private ToolStripMenuItem borderOutsideThinToolStripMenuItem;

		private ToolStripMenuItem borderOutsideStickToolStripMenuItem;

		private ToolStripMenuItem alignToolStripMenuItem1;

		private ToolStripMenuItem msmiColor;

		private ToolStripMenuItem backcolor_RedToolStripMenuItem;

		private ToolStripMenuItem msmiGridColor;

		private ToolStripMenuItem msmiBorderColor;

		private ToolStripMenuItem wrapTextToolStripMenuItem;

		private ToolStripMenuItem nowrapTextToolStripMenuItem;

		private ToolStripMenuItem readonlyToolStripMenuItem;

		private ToolStripMenuItem noreadonlyToolStripMenuItem;

		private ToolStripMenuItem borderAllToolStripMenuItem1;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem alignNormalToolStripMenuItem;

		private ToolStripMenuItem alignTopToolStripMenuItem;

		private ToolStripMenuItem alignMiddleToolStripMenuItem;

		private ToolStripMenuItem alignBottomToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator10;

		private ToolStripMenuItem alignLeftToolStripMenuItem;

		private ToolStripMenuItem alignTopLeftToolStripMenuItem;

		private ToolStripMenuItem alignMiddleLeftToolStripMenuItem;

		private ToolStripMenuItem alignBottomLeftToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripMenuItem alignCenterToolStripMenuItem;

		private ToolStripMenuItem alignTopCenterToolStripMenuItem;

		private ToolStripMenuItem alignMiddleCenterToolStripMenuItem;

		private ToolStripMenuItem alignBottomCenterToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem alignRightToolStripMenuItem;

		private ToolStripMenuItem alignTopRightToolStripMenuItem;

		private ToolStripMenuItem alignMiddleRightToolStripMenuItem;

		private ToolStripMenuItem alignBottomRightToolStripMenuItem;

		private ToolStripMenuItem msmiColorRed;

		private ToolStripMenuItem msmiColorGreen;

		private ToolStripMenuItem msmiColorYellow;

		private ToolStripMenuItem backColorRedToolStripMenuItem1;

		private ToolStripMenuItem backcolor_GreenToolStripMenuItem1;

		private ToolStripMenuItem backcolor_YellowToolStripMenuItem1;

		private ToolStripMenuItem backcolor_BlackToolStripMenuItem;

		private ToolStripMenuItem msmiGridColorRed;

		private ToolStripMenuItem msmiGridColorGreen;

		private ToolStripMenuItem msmiGridColorYellow;

		private ToolStripMenuItem msmiGridColorBlack;

		private ToolStripMenuItem msmiBorderColorRed;

		private ToolStripMenuItem msmiBorderColorGreen;

		private ToolStripMenuItem msmiBorderColorYellow;

		private ToolStripMenuItem msmiBorderColorBlack;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem EditContentToolStripMenuItem;

		private ToolStripMenuItem ToolStripMenuItemCut;

		private ToolStripMenuItem ToolStripMenuItemCopy;

		private ToolStripMenuItem ToolStripMenuItemPaste;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripMenuItem splitGridToolStripMenuItem;

		private ToolStripMenuItem CancelMergeToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripMenuItem ToolStripMenuItemClear;

		private ToolStripMenuItem ToolStripMenuItemClearContent;

		private ToolStripMenuItem ClearStyleToolStripMenuItem;

		private ToolStripMenuItem ClearStyleToolStripMenuItem1;

		private ToolStripSeparator toolStripSeparator13;

		private ToolStripMenuItem InsertRowsToolStripMenuItem;

		private ToolStripMenuItem InsertColumnsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator14;

		private ToolStripMenuItem DeleteRowToolStripMenuItem;

		private ToolStripMenuItem DeleteColumnToolStripMenuItem;

		private ToolStripMenuItem AttributesToolStripMenuItem;

		private ToolStripMenuItem FrameToolStripMenuItem;

		private ToolStripMenuItem NoFrameToolStripMenuItem;

		private ToolStripMenuItem ToolStripMenuItemLeftFrame;

		private ToolStripMenuItem RightFrameToolStripMenuItem;

		private ToolStripMenuItem UpperBorderToolStripMenuItem;

		private ToolStripMenuItem LowerBorderToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator26;

		private ToolStripMenuItem ToolStripMenuItemForwardSlash;

		private ToolStripMenuItem BackslashToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator27;

		private ToolStripMenuItem InnerBorderToolStripMenuItem;

		private ToolStripMenuItem FineBorderToolStripMenuItem;

		private ToolStripMenuItem ThickBorderToolStripMenuItem;

		private ToolStripMenuItem AllBorderToolStripMenuItem;

		private ToolStripMenuItem AlignmentToolStripMenuItem;

		private ToolStripMenuItem ConventionalToolStripMenuItem;

		private ToolStripMenuItem TopToolStripMenuItem;

		private ToolStripMenuItem ToolStripMenuItemCenter;

		private ToolStripMenuItem BottomToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator23;

		private ToolStripMenuItem ToolStripMenuItemLeft;

		private ToolStripMenuItem TopLeftToolStripMenuItem;

		private ToolStripMenuItem ToolStripMenuItemCenterLeft;

		private ToolStripMenuItem BottomLeftToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator24;

		private ToolStripMenuItem ToolStripMenuItemCen;

		private ToolStripMenuItem TopCenterToolStripMenuItem;

		private ToolStripMenuItem ToolStripMenuItemAtCenter;

		private ToolStripMenuItem BottomCenterToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator25;

		private ToolStripMenuItem RightToolStripMenuItem;

		private ToolStripMenuItem TopRightToolStripMenuItem;

		private ToolStripMenuItem ToolStripMenuItemCenterRight;

		private ToolStripMenuItem BottomRightToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator28;

		private ToolStripMenuItem cmsmiColor;

		private ToolStripMenuItem cmsmiColorRed;

		private ToolStripMenuItem cmsmiColorGreen;

		private ToolStripMenuItem cmsmiColorYellow;

		private ToolStripMenuItem BGColorToolStripMenuItem;

		private ToolStripMenuItem RedToolStripMenuItem1;

		private ToolStripMenuItem GreenToolStripMenuItem1;

		private ToolStripMenuItem YellowToolStripMenuItem1;

		private ToolStripMenuItem BlackToolStripMenuItem;

		private ToolStripMenuItem cmsmiGridColor;

		private ToolStripMenuItem cmsmiGridColorRed;

		private ToolStripMenuItem cmsmiGridColorGreen;

		private ToolStripMenuItem cmsmiGridColorYellow;

		private ToolStripMenuItem cmsmiGridColorBlack;

		private ToolStripMenuItem cmsmiBorderColor;

		private ToolStripMenuItem cmsmiBorderColorRed;

		private ToolStripMenuItem cmsmiBorderColorGreen;

		private ToolStripMenuItem cmsmiBorderColorYellow;

		private ToolStripMenuItem cmsmiBorderColorBlack;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripMenuItem SetEnterToolStripMenuItem;

		private ToolStripMenuItem CancelEnterToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripMenuItem SetReadOnlyToolStripMenuItem;

		private ToolStripMenuItem CancelReadonlyToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator17;

		private ToolStrip toolStrip1;

		private ToolStripButton button_Save;

		private ToolStripButton button_Open;

		private ToolStripButton button_New;

		private ToolStripButton button_boardStyle;

		private ToolStripButton button_Insert_Row;

		private ToolStripButton button_insert_Col;

		private ToolStripButton button_delete_Row;

		private ToolStripButton button_delete_Col;

		private ToolStripButton button_combin_Cell;

		private ToolStripButton button_split_Cell;

		private ToolStrip toolStrip2;

		private ToolStripComboBox toolStripComboBox_Font;

		private ToolStripComboBox tscmbFontSize;

		private ToolStripLabel toolStripLabel_Blod;

		private ToolStripLabel toolStripLabel_Italic;

		private ToolStripLabel toolStripLabel_Underline;

		private ToolStripSeparator toolStripSeparator18;

		private ToolStripSeparator toolStripSeparator19;

		private ToolStripSeparator toolStripSeparator20;

		private Panel panel_Alignt;

		private PictureBox pictureBox_Aligent_LeftTop;

		private PictureBox pictureBox_Aligent_RightButtom;

		private PictureBox pictureBox_Aligent_MiddleButtom;

		private PictureBox pictureBox_Aligent_RightMiddle;

		private PictureBox pictureBox_Aligent_MiddleMiddle;

		private PictureBox pictureBox_Aligent_RightTop;

		private PictureBox pictureBox_Aligent_MiddleTop;

		private PictureBox pictureBox_Aligent_LeftButtom;

		private PictureBox pictureBox_Aligent_LeftMiddle;

		private ToolStripButton toolStripButton1;

		private ToolStripSplitButton tssbtnColor;

		private ToolStripMenuItem tsmiColorRed;

		private ToolStripMenuItem tsmiColorGreen;

		private ToolStripMenuItem tsmiColorYellow;

		private ToolStripMenuItem tsmiColorBlack;

		private ToolStripSplitButton toolStripSplitButton2;

		private ToolStripMenuItem toolStripMenuItem_Backcolor_Red;

		private ToolStripMenuItem toolStripMenuItem_Backcolor_Green;

		private ToolStripMenuItem toolStripMenuItem_Backcolor_Yellow;

		private ToolStripMenuItem toolStripMenuItem_Backcolor_Black;

		private PictureBox pictureBox_max;

		private PictureBox pictureBox2;

		private ToolStripMenuItem saveas_ToolStripMenuItem;

		private Panel panel2;

		private ToolStripButton button_wraptext;

		private Panel pnlAutoFit;

		private Label lblAutoFitPercent;

		private Button btnAutoFitCancel;

		private ToolStripMenuItem formatToolStripMenuItem;

		private ProgressBar prgAutoFit;

		private ToolStripMenuItem zishiToolStripMenuItem;

		private ToolStripMenuItem tsmiColorBlue;

		private ToolStripMenuItem tsmiColorPurple;

		private ToolStripMenuItem tsmiColorCyan;

		private ToolStripMenuItem tsmiColorWhite;

		private ToolStripMenuItem msmiColorBlue;

		private ToolStripMenuItem msmiColorPurple;

		private ToolStripMenuItem msmiColorCyan;

		private ToolStripMenuItem msmiColorWhite;

		private ToolStripMenuItem msmiGridColorBlue;

		private ToolStripMenuItem msmiGridColorPurple;

		private ToolStripMenuItem msmiGridColorCyan;

		private ToolStripMenuItem msmiGridColorWhite;

		private ToolStripMenuItem msmiBorderColorBlue;

		private ToolStripMenuItem msmiBorderColorPurple;

		private ToolStripMenuItem msmiBorderColorCyan;

		private ToolStripMenuItem msmiBorderColorWhite;

		private ToolStripMenuItem cmsmiColorBlue;

		private ToolStripMenuItem cmsmiColorPurple;

		private ToolStripMenuItem cmsmiColorCyan;

		private ToolStripMenuItem cmsmiColorWhite;

		private ToolStripMenuItem cmsmiGridColorBlue;

		private ToolStripMenuItem cmsmiGridColorPurple;

		private ToolStripMenuItem cmsmiGridColorCyan;

		private ToolStripMenuItem cmsmiGridColorWhite;

		private ToolStripMenuItem cmsmiBorderColorBlue;

		private ToolStripMenuItem cmsmiBorderColorPurple;

		private ToolStripMenuItem cmsmiBorderColorCyan;

		private ToolStripMenuItem cmsmiBorderColorWhite;

		private ToolStripMenuItem StripMenuItem_ImportProjectFile;

		private ToolStripMenuItem StripMenuItem_ProjectFileSave;
        private Panel panel4;
        private Button button5;
        private Label label1;
        private Button button4;
        private Button button3;
        private Button button2;
        private TextBox textBox1;
        private Button button1;
        private Panel panel5;
        private TextBox textBox大气区湿度;
        private TextBox textBox大气区温度;
        private Label label36;
        private Label label37;
        private TextBox textBox14;
        private TextBox textBox15;
        private Label label38;
        private Label label39;
        private TextBox textBox16;
        private Label label40;
        private Label label41;
        private TextBox textBox17;
        private Label label44;
        private Label label45;
        private Label label35;
        private Panel panel6;
        private TextBox textBox降温区湿度;
        private TextBox textBox降温区温度;
        private Label label25;
        private Label label26;
        private TextBox textBox10;
        private TextBox textBox11;
        private Label label27;
        private Label label28;
        private TextBox textBox12;
        private Label label29;
        private Label label30;
        private TextBox textBox13;
        private Label label33;
        private Label label34;
        private Label label24;
        private Panel panel7;
        private TextBox textBox恒温区湿度;
        private TextBox textBox恒温区温度;
        private Label label4;
        private Label label5;
        private TextBox textBox6;
        private TextBox textBox7;
        private Label label16;
        private Label label17;
        private TextBox textBox8;
        private Label label18;
        private Label label19;
        private TextBox textBox9;
        private Label label22;
        private Label label23;
        private Label label3;
        private Panel panel8;
        private TextBox textBox升温区湿度;
        private TextBox textBox升温区温度;
        private Label label14;
        private Label label15;
        private TextBox textBox4;
        private TextBox textBox5;
        private Label label13;
        private Label label12;
        private TextBox textBox3;
        private Label label11;
        private Label label10;
        private TextBox textBox2;
        private Label label7;
        private Label label6;
        private Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private SaveFileDialog saveFileDialog1;

		public static string FormID
		{
			get
			{
				return formGrid.formID;
			}
			set
			{
				formGrid.formID = value;
			}
		}

		public formMain FormMain
		{
			set
			{
				this.fm = value;
			}
		}

		public formGrid(int pWidth, int pHeight)
		{
			this.InitializeComponent();
			this.InitColor();
			formMain.ML.NowFormID = formGrid.formID;
			this.Text = formMain.ML.GetStr("formGrid_FormText");
			this.btnAutoFitCancel.Text = formMain.ML.GetStr("formGrid_btnAutoFitCancel");
			this.fileExcelToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_fileExcelToolStripMenuItem");
			this.StripMenuItem_ImportProjectFile.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_file_importProjectFile");
			this.importExcel_ToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_file_importExcel");
			this.export_XmlToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_file_exportXml");
			this.saveas_ToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_file_exportExcel");
			this.save_ToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_file_save");
			this.StripMenuItem_ProjectFileSave.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_file_SaveAs");
			this.exit_ToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_file_exit");
			this.EditToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit");
			this.CopyToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_Copy");
			this.CutToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_Cut");
			this.pasteToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_paste");
			this.clearToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_clear");
			this.clearContentToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_clearContent");
			this.clearFormatToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_clearFormat");
			this.clearAllToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_clearAll");
			this.mergeToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_merge");
			this.noMergeToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_noMerge");
			this.insertRowToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_insertRow");
			this.insertColumnToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_insert");
			this.removeRowToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_removeRow");
			this.removeColumnToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_removeColumn");
			this.propertyToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property");
			this.fontToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_font");
			this.borderToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border");
			this.borderNoneToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_None");
			this.borderLeftToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Left");
			this.borderRightToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Right");
			this.borderTopToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Top");
			this.borderBottomToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Bottom");
			this.borderLineUpToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_LineUp");
			this.borderLineDownToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_LineDown");
			this.borderInsideToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Inside");
			this.borderOutsideThinToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_OutsideThin");
			this.borderOutsideStickToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_OutsideStick");
			this.borderAllToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_All");
			this.alignToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_align");
			this.alignNormalToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_align_Normal");
			this.alignTopToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_align_Top");
			this.alignMiddleToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignMiddle");
			this.alignBottomToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignBottom");
			this.alignLeftToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignLeft");
			this.alignTopLeftToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignTopLeft");
			this.alignMiddleLeftToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignMiddleLeft");
			this.alignBottomLeftToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignBottomLeft");
			this.alignCenterToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignCenter");
			this.alignTopCenterToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignTopCenter");
			this.alignMiddleCenterToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignMiddleCenter");
			this.alignBottomCenterToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignBottomCenter");
			this.alignRightToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignRight");
			this.alignTopRightToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignTopRight");
			this.alignMiddleRightToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignMiddleRight");
			this.alignBottomRightToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignBottomRight");
			this.msmiColor.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_TextColor");
			this.msmiColorRed.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_RED");
			this.msmiColorGreen.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Green");
			this.msmiColorYellow.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_yellow");
			this.msmiColorBlue.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Blue");
			this.msmiColorPurple.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_purple");
			this.msmiColorCyan.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_cyan_blue");
			this.msmiColorWhite.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_White");
			this.backcolor_RedToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_backcolor");
			this.backColorRedToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_RED");
			this.backcolor_GreenToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Green");
			this.backcolor_YellowToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_yellow");
			this.backcolor_BlackToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Black");
			this.msmiGridColor.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_GridColor");
			this.msmiGridColorRed.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_RED");
			this.msmiGridColorGreen.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Green");
			this.msmiGridColorYellow.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_yellow");
			this.msmiGridColorBlack.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Black");
			this.msmiGridColorBlue.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Blue");
			this.msmiGridColorPurple.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_purple");
			this.msmiGridColorCyan.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_cyan_blue");
			this.msmiGridColorWhite.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_White");
			this.msmiBorderColor.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_BorderColor");
			this.msmiBorderColorRed.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_RED");
			this.msmiBorderColorGreen.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Green");
			this.msmiBorderColorYellow.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_yellow");
			this.msmiBorderColorBlack.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Black");
			this.msmiBorderColorBlue.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Blue");
			this.msmiBorderColorPurple.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_purple");
			this.msmiBorderColorCyan.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_cyan_blue");
			this.msmiBorderColorWhite.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_White");
			this.wrapTextToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_wrapText");
			this.nowrapTextToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_nowrapText");
			this.readonlyToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_readonly");
			this.noreadonlyToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_noreadonly");
			this.formatToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_format");
			this.zishiToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_AdaptiveHeight");
			this.EditContentToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_Edit");
			this.AttributesToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_font");
			this.FrameToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_border");
			this.AlignmentToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_align");
			this.cmsmiColor.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_TextColor");
			this.BGColorToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_backColor");
			this.cmsmiGridColor.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_GridColor");
			this.cmsmiBorderColor.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_BorderColor");
			this.SetEnterToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_wrapText");
			this.CancelEnterToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_nowrapText");
			this.SetReadOnlyToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_readonly");
			this.CancelReadonlyToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ContextMenuStrip_noreadonly");
			this.ToolStripMenuItemCut.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_Cut");
			this.ToolStripMenuItemCopy.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_Copy");
			this.ToolStripMenuItemPaste.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_paste");
			this.splitGridToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_merge");
			this.CancelMergeToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_noMerge");
			this.ToolStripMenuItemClear.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_clear");
			this.InsertRowsToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_insertRow");
			this.InsertColumnsToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_insert");
			this.DeleteRowToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_removeRow");
			this.DeleteColumnToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_removeColumn");
			this.ToolStripMenuItemClearContent.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_clearContent");
			this.ClearStyleToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_clearFormat");
			this.ClearStyleToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Edit_clearAll");
			this.NoFrameToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_None");
			this.ToolStripMenuItemLeftFrame.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Left");
			this.RightFrameToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Right");
			this.UpperBorderToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Top");
			this.LowerBorderToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Bottom");
			this.ToolStripMenuItemForwardSlash.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_LineUp");
			this.BackslashToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_LineDown");
			this.InnerBorderToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_Inside");
			this.FineBorderToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_OutsideThin");
			this.ThickBorderToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_OutsideStick");
			this.AllBorderToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_property_border_All");
			this.ConventionalToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_align_Normal");
			this.TopToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_align_Top");
			this.ToolStripMenuItemCenter.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignMiddle");
			this.BottomToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignBottom");
			this.ToolStripMenuItemLeft.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignLeft");
			this.TopLeftToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignTopLeft");
			this.ToolStripMenuItemCenterLeft.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignMiddleLeft");
			this.BottomLeftToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignBottomLeft");
			this.ToolStripMenuItemCen.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignCenter");
			this.TopCenterToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignTopCenter");
			this.ToolStripMenuItemAtCenter.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignMiddleCenter");
			this.BottomCenterToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignBottomCenter");
			this.RightToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignRight");
			this.TopRightToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignTopRight");
			this.ToolStripMenuItemCenterRight.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignMiddleRight");
			this.BottomRightToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_alignBottomRight");
			this.cmsmiColorRed.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_RED");
			this.cmsmiColorGreen.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Green");
			this.cmsmiColorYellow.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_yellow");
			this.cmsmiColorBlue.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Blue");
			this.cmsmiColorPurple.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_purple");
			this.cmsmiColorCyan.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_cyan_blue");
			this.cmsmiColorWhite.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_White");
			this.RedToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_RED");
			this.GreenToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Green");
			this.YellowToolStripMenuItem1.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_yellow");
			this.BlackToolStripMenuItem.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Black");
			this.cmsmiGridColorRed.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_RED");
			this.cmsmiGridColorGreen.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Green");
			this.cmsmiGridColorYellow.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_yellow");
			this.cmsmiGridColorBlack.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Black");
			this.cmsmiGridColorBlue.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Blue");
			this.cmsmiGridColorPurple.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_purple");
			this.cmsmiGridColorCyan.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_cyan_blue");
			this.cmsmiGridColorWhite.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_White");
			this.cmsmiBorderColorRed.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_RED");
			this.cmsmiBorderColorGreen.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Green");
			this.cmsmiBorderColorYellow.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_yellow");
			this.cmsmiBorderColorBlack.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Black");
			this.cmsmiBorderColorBlue.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_Blue");
			this.cmsmiBorderColorPurple.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_purple");
			this.cmsmiBorderColorCyan.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_cyan_blue");
			this.cmsmiBorderColorWhite.Text = formMain.ML.GetStr("formGrid_ToolStripMenuItem_Color_White");
			this.toolStrip2.Items["toolStripLabel_Blod"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip2_toolStripLabel_Blod");
			this.toolStrip2.Items["toolStripLabel_Italic"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip2_toolStripLabel_Italic");
			this.toolStrip2.Items["toolStripLabel_Underline"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip2_toolStripLabel_Underline");
			this.toolStrip2.Items["button_wraptext"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip2_button_wraptext");
			this.toolStrip2.Items["toolStripButton1"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip2_toolStripButton1");
			this.toolStrip2.Items["tssbtnColor"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip2_tssbtnColor");
			this.toolStrip2.Items["toolStripSplitButton2"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip2_toolStripSplitButton2");
			this.toolStrip1.Items["button_New"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip1_button_New");
			this.toolStrip1.Items["button_Open"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip1_button_Open");
			this.toolStrip1.Items["button_Save"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip1_button_Save");
			this.toolStrip1.Items["button_Insert_Row"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip1_button_Insert_Row");
			this.toolStrip1.Items["button_insert_Col"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip1_button_insert_Col");
			this.toolStrip1.Items["button_delete_Row"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip1_button_delete_Row");
			this.toolStrip1.Items["button_delete_Col"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip1_button_delete_Col");
			this.toolStrip1.Items["button_combin_Cell"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip1_button_combin_Cell");
			this.toolStrip1.Items["button_split_Cell"].ToolTipText = formMain.ML.GetStr("formGrid_toolStrip_toolStrip1_button_split_Cell");
			this.maxWidth = pWidth;
		}

		private void InitColor()
		{
			try
			{
				this.tssbtnColor.DropDownItems.Clear();
				this.msmiColor.DropDownItems.Clear();
				this.msmiGridColor.DropDownItems.Clear();
				this.msmiBorderColor.DropDownItems.Clear();
				this.cmsmiColor.DropDownItems.Clear();
				this.cmsmiGridColor.DropDownItems.Clear();
				this.cmsmiBorderColor.DropDownItems.Clear();
				if (formMain.Ledsys != null && formMain.Ledsys.SelectedPanel != null)
				{
					LedColorMode colorMode = formMain.Ledsys.SelectedPanel.ColorMode;
					if (colorMode >= LedColorMode.R)
					{
						this.tssbtnColor.DropDownItems.Add(this.tsmiColorRed);
						this.msmiColor.DropDownItems.Add(this.msmiColorRed);
						this.msmiGridColor.DropDownItems.Add(this.msmiGridColorRed);
						this.msmiBorderColor.DropDownItems.Add(this.msmiBorderColorRed);
						this.cmsmiColor.DropDownItems.Add(this.cmsmiColorRed);
						this.cmsmiGridColor.DropDownItems.Add(this.cmsmiGridColorRed);
						this.cmsmiBorderColor.DropDownItems.Add(this.cmsmiBorderColorRed);
					}
					if (colorMode >= LedColorMode.RG)
					{
						this.tssbtnColor.DropDownItems.Add(this.tsmiColorGreen);
						this.tssbtnColor.DropDownItems.Add(this.tsmiColorYellow);
						this.msmiColor.DropDownItems.Add(this.msmiColorGreen);
						this.msmiColor.DropDownItems.Add(this.msmiColorYellow);
						this.msmiGridColor.DropDownItems.Add(this.msmiGridColorGreen);
						this.msmiGridColor.DropDownItems.Add(this.msmiGridColorYellow);
						this.msmiBorderColor.DropDownItems.Add(this.msmiBorderColorGreen);
						this.msmiBorderColor.DropDownItems.Add(this.msmiBorderColorYellow);
						this.cmsmiColor.DropDownItems.Add(this.cmsmiColorGreen);
						this.cmsmiColor.DropDownItems.Add(this.cmsmiColorYellow);
						this.cmsmiGridColor.DropDownItems.Add(this.cmsmiGridColorGreen);
						this.cmsmiGridColor.DropDownItems.Add(this.cmsmiGridColorYellow);
						this.cmsmiBorderColor.DropDownItems.Add(this.cmsmiBorderColorGreen);
						this.cmsmiBorderColor.DropDownItems.Add(this.cmsmiBorderColorYellow);
					}
					if (colorMode == LedColorMode.RGB)
					{
						this.tssbtnColor.DropDownItems.Add(this.tsmiColorBlue);
						this.tssbtnColor.DropDownItems.Add(this.tsmiColorPurple);
						this.tssbtnColor.DropDownItems.Add(this.tsmiColorCyan);
						this.tssbtnColor.DropDownItems.Add(this.tsmiColorWhite);
						this.msmiColor.DropDownItems.Add(this.msmiColorBlue);
						this.msmiColor.DropDownItems.Add(this.msmiColorPurple);
						this.msmiColor.DropDownItems.Add(this.msmiColorCyan);
						this.msmiColor.DropDownItems.Add(this.msmiColorWhite);
						this.msmiGridColor.DropDownItems.Add(this.msmiGridColorBlue);
						this.msmiGridColor.DropDownItems.Add(this.msmiGridColorPurple);
						this.msmiGridColor.DropDownItems.Add(this.msmiGridColorCyan);
						this.msmiGridColor.DropDownItems.Add(this.msmiGridColorWhite);
						this.msmiBorderColor.DropDownItems.Add(this.msmiBorderColorBlue);
						this.msmiBorderColor.DropDownItems.Add(this.msmiBorderColorPurple);
						this.msmiBorderColor.DropDownItems.Add(this.msmiBorderColorCyan);
						this.msmiBorderColor.DropDownItems.Add(this.msmiBorderColorWhite);
						this.cmsmiColor.DropDownItems.Add(this.cmsmiColorBlue);
						this.cmsmiColor.DropDownItems.Add(this.cmsmiColorPurple);
						this.cmsmiColor.DropDownItems.Add(this.cmsmiColorCyan);
						this.cmsmiColor.DropDownItems.Add(this.cmsmiColorWhite);
						this.cmsmiGridColor.DropDownItems.Add(this.cmsmiGridColorBlue);
						this.cmsmiGridColor.DropDownItems.Add(this.cmsmiGridColorPurple);
						this.cmsmiGridColor.DropDownItems.Add(this.cmsmiGridColorCyan);
						this.cmsmiGridColor.DropDownItems.Add(this.cmsmiGridColorWhite);
						this.cmsmiBorderColor.DropDownItems.Add(this.cmsmiBorderColorBlue);
						this.cmsmiBorderColor.DropDownItems.Add(this.cmsmiBorderColorPurple);
						this.cmsmiBorderColor.DropDownItems.Add(this.cmsmiBorderColorCyan);
						this.cmsmiBorderColor.DropDownItems.Add(this.cmsmiBorderColorWhite);
					}
				}
			}
			catch
			{
			}
		}

		public void ResetColumWidth(int pNowColum)
		{
		}

		private void InitGrid()
		{
			if (this.grid1 != null)
			{
				this.grid1.Dispose();
			}
			this.grid1 = new Grid();
			this.grid1.BackColor1 = System.Drawing.Color.Black;
			this.grid1.BackColor2 = System.Drawing.Color.Black;
			this.grid1.BorderStyle = BorderStyleEnum.None;
			this.grid1.CellBorderColor = System.Drawing.Color.Red;
			this.grid1.DefaultFont = new System.Drawing.Font("宋体", 9f);
			this.grid1.DisplayRowArrow = true;
			this.grid1.DisplayRowNumber = true;
			this.grid1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.grid1.GridColor = System.Drawing.Color.Red;
			this.grid1.ImeMode = ImeMode.NoControl;
			this.grid1.Dock = DockStyle.None;
			this.grid1.Dock = DockStyle.Fill;
			this.grid1.Name = "grid1";
			this.grid1.Size = new System.Drawing.Size(232, 173);
			this.grid1.TabIndex = 1;
			this.grid1.ForeColor = System.Drawing.Color.Red;
			Range range = this.grid1.Range(0, 0, this.grid1.Rows - 1, this.grid1.Cols - 1);
			range.SelectCells();
			this.grid1.Selection.set_Borders(EdgeEnum.Outside, LineStyleEnum.Thin);
			this.grid1.Selection.ForeColor = System.Drawing.Color.Red;
			this.grid1.DisplayClientBorder = false;
			this.grid1.FixedRowColStyle = FixedRowColStyleEnum.Flat;
			range = this.grid1.Range(1, 1, 1, 1);
			this.grid1.BackColorBkg = System.Drawing.Color.Black;
			range.SelectCells();
			this.grid1.Dock = DockStyle.Fill;
			this.grid1.DisplayRowNumber = true;
			System.Drawing.Point location = this.grid1.Location;
			this.grid1.Location = new System.Drawing.Point(location.X, location.Y + 5);
			this.grid1.Parent = this.panel1;
			this.grid1.ContextMenuStrip = this.contextMenuStrip1;
			this.grid1.KeyDown += new Grid.KeyDownEventHandler(this.grid1_KeyDown);
			this.grid1.SelChange += new Grid.SelChangeEventHandler(this.grid1_SelChange);
			this.grid1.MouseDown += new Grid.MouseDownEventHandler(this.grid1_MouseDown);
		}

		private void grid1_LostFocus(object sender, EventArgs e)
		{
		}

		private void grid1_KeyDown(object Sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				FlexCell.Cell cell = this.grid1.Cell(this.grid1.Selection.FirstRow, this.grid1.Selection.FirstCol);
				cell.WrapText = true;
				e.Handled = true;
			}
		}

		private void grid1_CellChange(object Sender, Grid.CellChangeEventArgs e)
		{
		}

		private string getFormatString(string pValue)
		{
			return pValue;
		}

		private void grid1_MouseDown(object Sender, MouseEventArgs e)
		{
			if (this.panel_Alignt.Visible)
			{
				this.panel_Alignt.Visible = false;
				this.panel_Alignt.SendToBack();
			}
		}

		private void grid1_MouseEnter(object sender, EventArgs e)
		{
		}

		private void grid1_SelChange(object Sender, Grid.SelChangeEventArgs e)
		{
			int firstRow = e.FirstRow;
			int firstCol = e.FirstCol;
			FlexCell.Cell cell = this.grid1.Cell(firstRow, firstCol);
			this.toolStripComboBox_Font.Text = cell.FontName;
			this.tscmbFontSize.Text = cell.FontSize.ToString();
			if (cell.FontBold)
			{
				this.toolStripLabel_Blod.BackColor = System.Drawing.SystemColors.Highlight;
			}
			else
			{
				this.toolStripLabel_Blod.BackColor = System.Drawing.SystemColors.Control;
			}
			if (cell.FontItalic)
			{
				this.toolStripLabel_Italic.BackColor = System.Drawing.SystemColors.Highlight;
			}
			else
			{
				this.toolStripLabel_Italic.BackColor = System.Drawing.SystemColors.Control;
			}
			if (cell.FontUnderline)
			{
				this.toolStripLabel_Underline.BackColor = System.Drawing.SystemColors.Highlight;
			}
			else
			{
				this.toolStripLabel_Underline.BackColor = System.Drawing.SystemColors.Control;
			}
			if (cell.WrapText)
			{
				this.button_wraptext.Checked = true;
				return;
			}
			this.button_wraptext.Checked = false;
		}

		private void grid1_MouseUp(object Sender, MouseEventArgs e)
		{
			try
			{
				if (this.grid1.Selection.FirstCol != this.grid1.Selection.LastCol || this.grid1.Selection.FirstRow != this.grid1.Selection.LastRow)
				{
					this.toolStripComboBox_Font.Text = "";
					this.tscmbFontSize.Text = "";
					this.toolStripLabel_Blod.BackColor = System.Drawing.Color.Transparent;
					this.toolStripLabel_Italic.BackColor = System.Drawing.Color.Transparent;
					this.toolStripLabel_Underline.BackColor = System.Drawing.Color.Transparent;
					this.button_wraptext.Tag = false;
				}
				else
				{
					FlexCell.Cell cell = this.grid1.Cell(this.grid1.MouseRow, this.grid1.MouseCol);
					this.toolStripComboBox_Font.Text = cell.FontName;
					this.tscmbFontSize.Text = cell.FontSize.ToString();
					if (cell.FontBold)
					{
						this.toolStripLabel_Blod.BackColor = System.Drawing.Color.LightBlue;
					}
					else
					{
						this.toolStripLabel_Blod.BackColor = System.Drawing.Color.Transparent;
					}
					if (cell.FontItalic)
					{
						this.toolStripLabel_Italic.BackColor = System.Drawing.Color.LightBlue;
					}
					else
					{
						this.toolStripLabel_Italic.BackColor = System.Drawing.Color.Transparent;
					}
					if (cell.FontUnderline)
					{
						this.toolStripLabel_Underline.BackColor = System.Drawing.Color.LightBlue;
					}
					else
					{
						this.toolStripLabel_Underline.BackColor = System.Drawing.Color.Transparent;
					}
					this.button_wraptext.Tag = cell.WrapText;
				}
			}
			catch
			{
			}
		}

		private void grid1_KeyUp(object Sender, KeyEventArgs e)
		{
		}

		private void SaveRedraw()
		{
			try
			{
				while (this.isThreadSaveRedraw)
				{
					if (!this.isThreadSaveRedrawSuspended && this.StartSave())
					{
						this.fm.RedrawSubarea();
					}
					Thread.Sleep(this.intervalSaveRedraw * 1000);
				}
			}
			catch
			{
			}
		}

		private void formGrid_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			base.Size = new System.Drawing.Size(this.fm.Width, 387);
			int num = Screen.PrimaryScreen.WorkingArea.Width - base.Width;
			int y = Screen.PrimaryScreen.WorkingArea.Height - base.Height;
			base.Location = new System.Drawing.Point(num / 2, y);
			this.pictureBox2.Image = Template.Main_Max;
			this.toolStripSplitButton2.Visible = false;
			this.backcolor_RedToolStripMenuItem.Visible = false;
			this.BGColorToolStripMenuItem.Visible = false;
			this.pictureBox2.Visible = false;
			try
			{
				IList<string> fontFamilies = new FontFamiliesEx().GetFontFamilies();
				if (fontFamilies != null && fontFamilies.Count > 0)
				{
					foreach (string current in fontFamilies)
					{
						this.toolStripComboBox_Font.Items.Add(current);
					}
				}
			}
			catch
			{
			}
			if (this.toolStripComboBox_Font.Items.Count > 0)
			{
				int num2 = -1;
				LedFont ledFont = new LedFont();
				for (int i = 0; i < this.toolStripComboBox_Font.Items.Count; i++)
				{
					if (this.toolStripComboBox_Font.Items[i].ToString() == ledFont.FamilyName)
					{
						num2 = i;
						break;
					}
					if (this.toolStripComboBox_Font.Items[i].ToString() == formMain.DefalutForeignTradeFamilyName)
					{
						num2 = i;
					}
				}
				this.toolStripComboBox_Font.SelectedIndex = ((num2 < 0) ? 0 : num2);
			}
			if (this.tscmbFontSize.Items.Count > 0)
			{
				this.tscmbFontSize.SelectedIndex = 1;
			}
			if (this.thrSaveRedraw == null || this.thrSaveRedraw.ThreadState != ThreadState.Running)
			{
				this.isThreadSaveRedrawSuspended = false;
				this.isThreadSaveRedraw = true;
				this.thrSaveRedraw = new Thread(new ThreadStart(this.SaveRedraw));
				this.thrSaveRedraw.IsBackground = true;
				this.thrSaveRedraw.Priority = ThreadPriority.Lowest;
				this.thrSaveRedraw.Start();
			}
		}

		private void formGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.panel_Alignt.Visible)
			{
				this.panel_Alignt.Visible = false;
				this.panel_Alignt.SendToBack();
			}
		}

		public void NewGrid(string pFileName)
		{
			this.fileName = pFileName;
			this.InitGrid();
			if (formGrid.NeedImport)
			{
				formGrid.NeedImport = false;
				this.ImportNew(formGrid.ImportFileName, formGrid.ImportSheetIndex);
			}
		}

		public void Edit(string pFileName)
		{
			try
			{
				this.fileName = pFileName;
				this.InitGrid();
				if (!formGrid.NeedImport)
				{
					if (this.grid1.OpenFile(pFileName))
					{
						if (this.grid1.Cols == 2 && this.grid1.Rows == 2 && this.grid1.Column(1).Width < 15)
						{
							this.NewGrid(5, 10, this.maxWidth);
						}
					}
					else
					{
						this.NewGrid(5, 10, this.maxWidth);
					}
				}
				else
				{
					formGrid.NeedImport = false;
					this.ImportNew(formGrid.ImportFileName, formGrid.ImportSheetIndex);
				}
				this.grid1.Refresh();
			}
			catch
			{
			}
		}

		private void SetCellAutoWrap(Grid grid)
		{
		}

		private bool Save()
		{
			return this.StartSave();
		}

		private bool StartSave()
		{
			bool result = false;
			try
			{
				lock (this.thisLockObject)
				{
					result = this.grid1.SaveFile(this.fileName);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void importExcel_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.isThreadSaveRedrawSuspended = true;
				this.openFileDialog1.Filter = "Excel 97-2007 files|*.xls;*.xlsx";
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
				string path = this.openFileDialog1.FileName;
				Path.GetExtension(path);
				List<OfficeTool.Excel.Cell[]> list = new List<OfficeTool.Excel.Cell[]>();
				ExcelTool excelTool = new ExcelTool(path);
				List<string> sheetNameList = excelTool.GetSheetNameList();
				if (sheetNameList.Count != 0)
				{
					formSelectIndex formSelectIndex = new formSelectIndex(formMain.ML.GetStr("Display_PleaseSelect"), formMain.ML.GetStr("Prompt_PleaseChooseTheSheetyouwantimport"), sheetNameList);
					int index = formSelectIndex.GetIndex();
					if (index != -1)
					{
						list = excelTool.GetExcelSheetData(sheetNameList[index]);
						int num = (list.Count > 0) ? list[0].Length : 0;
						this.InitGrid();
						Range range = this.grid1.Range(2, 1, this.grid1.Rows - 1, this.grid1.Cols - 1);
						range.DeleteByRow();
						Range range2 = this.grid1.Range(1, 2, this.grid1.Rows - 1, this.grid1.Cols - 1);
						range2.DeleteByCol();
						for (int i = 0; i < num; i++)
						{
							this.grid1.InsertCol(i, 1);
						}
						int num2 = 1;
						for (int j = 0; j < list.Count; j++)
						{
							this.grid1.AddItem(num2.ToString(), false);
							for (int k = 0; k < num; k++)
							{
								this.grid1.Cell(num2, k + 1).ForeColor = System.Drawing.Color.Red;
								this.grid1.Cell(num2, k + 1).WrapText = true;
								OfficeTool.Excel.Cell cell = list[j][k];
								if (cell != null && !string.IsNullOrEmpty(cell.CellValue))
								{
									string text = cell.CellValue;
									if (text.StartsWith("'"))
									{
										text = text.Replace("'", "");
									}
									this.grid1.Cell(num2, k + 1).Text = text;
								}
							}
							num2++;
						}
						int num3 = this.grid1.Rows - 1;
						if (num3 > list.Count)
						{
							Range range3 = this.grid1.Range(list.Count + 1, 1, num3, this.grid1.Cols - 1);
							range3.DeleteByRow();
						}
						List<CellRange> excelSheetMergedRegions = excelTool.GetExcelSheetMergedRegions(sheetNameList[index]);
						for (int l = 0; l < excelSheetMergedRegions.Count; l++)
						{
							CellRange cellRange = excelSheetMergedRegions[l];
							if (cellRange != null)
							{
								Range range4 = this.grid1.Range(cellRange.FirstRow + 1, cellRange.FirstColumn + 1, cellRange.LastRow + 1, cellRange.LastColumn + 1);
								if (range4 != null)
								{
									range4.Merge();
								}
							}
						}
						this.grid1.Refresh();
						this.grid1.ContextMenuStrip = this.contextMenuStrip1;
						if (this.maxWidth != 0)
						{
							int num4 = this.grid1.Cols - 1;
							int num5 = this.maxWidth / num4;
							for (int m = 0; m < num4; m++)
							{
								int num6 = num5;
								if (m == num4 - 1)
								{
									if (this.maxWidth % num4 != 0)
									{
										num6 = this.maxWidth - num5 * (num4 - 1) - 1;
									}
									else
									{
										num6 = num5 - 1;
									}
								}
								this.grid1.Column(m + 1).Width = short.Parse(num6.ToString());
							}
							this.SetCellAutoWrap(this.grid1);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message);
			}
			finally
			{
				this.isThreadSaveRedrawSuspended = false;
			}
		}

		private void Load2007(string pFilePath)
		{
		}

		public DataTable GetExcelTableByOleDB(string strExcelPath, string tableName)
		{
			DataTable dataTable = new DataTable();
			DataSet dataSet = new DataSet();
			string extension = Path.GetExtension(strExcelPath);
			Path.GetFileName(strExcelPath);
			string a;
			OleDbConnection oleDbConnection;
			if ((a = extension) != null)
			{
				if (a == ".xls")
				{
					oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strExcelPath + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"");
					goto IL_6D;
				}
				if (a == ".xlsx")
				{
					oleDbConnection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=False;IMEX=1;'", strExcelPath));
					goto IL_6D;
				}
			}
			oleDbConnection = null;
			IL_6D:
			if (oleDbConnection == null)
			{
				return null;
			}
			oleDbConnection.Open();
			DataTable oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			List<string> list = new List<string>();
			for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
			{
				string text = (string)oleDbSchemaTable.Rows[i]["TABLE_NAME"];
				if ((!text.Contains("$") || text.Replace("'", "").EndsWith("$")) && list != null && !list.Contains(text))
				{
					list.Add(text);
				}
			}
			string text2 = "select * from [" + list[0] + "]";
			new OleDbCommand(text2, oleDbConnection);
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(text2, oleDbConnection);
			oleDbDataAdapter.Fill(dataSet, tableName);
			oleDbConnection.Close();
			return dataSet.Tables[tableName];
		}

		private void ImportNew(string filename, int SheetIndex)
		{
			List<OfficeTool.Excel.Cell[]> list = new List<OfficeTool.Excel.Cell[]>();
			ExcelTool excelTool = new ExcelTool(filename);
			if (SheetIndex == -1)
			{
				return;
			}
			list = excelTool.GetExcelSheetData(SheetIndex);
			int num = (list.Count > 0) ? list[0].Length : 0;
			Range range = this.grid1.Range(2, 1, this.grid1.Rows - 1, this.grid1.Cols - 1);
			range.DeleteByRow();
			Range range2 = this.grid1.Range(1, 2, this.grid1.Rows - 1, this.grid1.Cols - 1);
			range2.DeleteByCol();
			for (int i = 0; i < num; i++)
			{
				this.grid1.InsertCol(i, 1);
			}
			int num2 = 1;
			for (int j = 0; j < list.Count; j++)
			{
				this.grid1.AddItem(num2.ToString(), false);
				for (int k = 0; k < num; k++)
				{
					this.grid1.Cell(num2, k + 1).ForeColor = System.Drawing.Color.Red;
					this.grid1.Cell(num2, k + 1).WrapText = true;
					OfficeTool.Excel.Cell cell = list[j][k];
					if (cell != null && !string.IsNullOrEmpty(cell.CellValue))
					{
						string text = cell.CellValue;
						if (text.StartsWith("'"))
						{
							text = text.Replace("'", "");
						}
						this.grid1.Cell(num2, k + 1).Text = text;
					}
				}
				num2++;
			}
			int num3 = this.grid1.Rows - 1;
			if (num3 > list.Count)
			{
				Range range3 = this.grid1.Range(list.Count + 1, 1, num3, this.grid1.Cols - 1);
				range3.DeleteByRow();
			}
			List<CellRange> excelSheetMergedRegions = excelTool.GetExcelSheetMergedRegions(SheetIndex);
			for (int l = 0; l < excelSheetMergedRegions.Count; l++)
			{
				CellRange cellRange = excelSheetMergedRegions[l];
				if (cellRange != null)
				{
					Range range4 = this.grid1.Range(cellRange.FirstRow + 1, cellRange.FirstColumn + 1, cellRange.LastRow + 1, cellRange.LastColumn + 1);
					if (range4 != null)
					{
						range4.Merge();
					}
				}
			}
			this.grid1.Refresh();
			this.grid1.ContextMenuStrip = this.contextMenuStrip1;
			if (this.maxWidth == 0)
			{
				return;
			}
			int num4 = this.grid1.Cols - 1;
			int num5 = this.maxWidth / num4;
			for (int m = 0; m < num4; m++)
			{
				int num6 = num5;
				if (m == num4 - 1)
				{
					if (this.maxWidth % num4 != 0)
					{
						num6 = this.maxWidth - num5 * (num4 - 1) - 1;
					}
					else
					{
						num6 = num5 - 1;
					}
				}
				this.grid1.Column(m + 1).Width = short.Parse(num6.ToString());
			}
			this.SetCellAutoWrap(this.grid1);
		}

		private void export_XmlToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void exit_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void save_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.SaveFile(this.fileName);
		}

		private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.CopyData();
		}

		private void CutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.CutData();
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.PasteData();
		}

		private void clearContentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ClearText();
		}

		private void clearFormatToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ClearFormat();
		}

		private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ClearAll();
		}

		private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.grid1.Selection.MergeCells = true;
			}
			catch
			{
				MessageBox.Show(formMain.ML.GetStr("Prompt_CanMergeReadonlyCells"));
			}
		}

		private void noMergeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.MergeCells = false;
		}

		private void insertRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.InsertRows();
			if (this.grid1.ActiveCell.Row == 1)
			{
				for (int i = 1; i < this.grid1.Cols; i++)
				{
					this.grid1.Cell(1, i).ForeColor = System.Drawing.Color.Red;
				}
			}
		}

		private void insertColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.InsertCols();
			if (this.grid1.ActiveCell.Col == 1)
			{
				for (int i = 1; i < this.grid1.Rows; i++)
				{
					this.grid1.Cell(i, 1).ForeColor = System.Drawing.Color.Red;
				}
			}
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.IsAlowDeleteRow())
			{
				this.grid1.Range(2, this.grid1.Selection.FirstCol, this.grid1.Selection.LastRow, this.grid1.Selection.LastCol).SelectCells();
				this.grid1.Selection.DeleteByRow();
			}
		}

		private void removeColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.IsAlowDelteCol())
			{
				this.grid1.Range(this.grid1.Selection.FirstRow, 2, this.grid1.Selection.LastRow, this.grid1.Selection.LastCol).SelectCells();
				this.grid1.Selection.DeleteByCol();
			}
		}

		private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			FontDialog fontDialog = new FontDialog();
			fontDialog.Font = this.grid1.ActiveCell.Font;
			if (fontDialog.ShowDialog() == DialogResult.OK)
			{
				this.grid1.Selection.Font = fontDialog.Font;
			}
			fontDialog.Dispose();
		}

		private void borderNoneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders((EdgeEnum)255, LineStyleEnum.None);
		}

		private void borderLeftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders(EdgeEnum.Left, LineStyleEnum.Thin);
		}

		private void borderRightToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders(EdgeEnum.Right, LineStyleEnum.Thin);
		}

		private void borderTopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders(EdgeEnum.Top, LineStyleEnum.Thin);
		}

		private void borderBottomToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders(EdgeEnum.Bottom, LineStyleEnum.Thin);
		}

		private void borderLineUpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders(EdgeEnum.DiagonalUp, LineStyleEnum.Thin);
		}

		private void borderLineDownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders(EdgeEnum.DiagonalDown, LineStyleEnum.Thin);
		}

		private void borderInsideToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders(EdgeEnum.Inside, LineStyleEnum.Thin);
		}

		private void borderOutsideThinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders(EdgeEnum.Outside, LineStyleEnum.Thin);
		}

		private void borderOutsideStickToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders(EdgeEnum.Outside, LineStyleEnum.Thick);
		}

		private void borderAllToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.set_Borders((EdgeEnum)207, LineStyleEnum.Thin);
		}

		private void alignNormalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.GeneralGeneral;
		}

		private void alignTopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.GeneralTop;
		}

		private void alignMiddleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.GeneralCenter;
		}

		private void alignBottomToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.GeneralBottom;
		}

		private void alignLeftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.LeftGeneral;
		}

		private void alignTopLeftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.LeftTop;
		}

		private void alignMiddleLeftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.LeftCenter;
		}

		private void alignBottomLeftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.LeftBottom;
		}

		private void alignCenterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.CenterGeneral;
		}

		private void alignTopCenterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.CenterTop;
		}

		private void alignMiddleCenterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.CenterCenter;
		}

		private void alignBottomCenterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.CenterBottom;
		}

		private void alignRightToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.RightGeneral;
		}

		private void alignTopRightToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.RightTop;
		}

		private void alignMiddleRightToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.RightCenter;
		}

		private void alignBottomRightToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.RightBottom;
		}

		private void msmiColorRed_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Red;
		}

		private void msmiColorGreen_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Green;
		}

		private void msmiColorYellow_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Yellow;
		}

		private void msmiColorBlue_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Blue;
		}

		private void msmiColorPurple_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Purple;
		}

		private void msmiColorCyan_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Cyan;
		}

		private void msmiColorWhite_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.White;
		}

		private void backcolor_RedToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.BackColor = System.Drawing.Color.Red;
		}

		private void backcolor_GreenToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (formMain.ledsys.SelectedPanel.ColorMode == LedColorMode.R)
			{
				return;
			}
			this.grid1.Selection.BackColor = System.Drawing.Color.Green;
		}

		private void backcolor_YellowToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (formMain.ledsys.SelectedPanel.ColorMode == LedColorMode.R)
			{
				return;
			}
			this.grid1.Selection.BackColor = System.Drawing.Color.Yellow;
		}

		private void backcolor_BlackToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.BackColor = System.Drawing.Color.Black;
		}

		private void msmiGridColorRed_Click(object sender, EventArgs e)
		{
			this.grid1.GridColor = System.Drawing.Color.Red;
		}

		private void msmiGridColorGreen_Click(object sender, EventArgs e)
		{
			this.grid1.GridColor = System.Drawing.Color.Green;
		}

		private void msmiGridColorYellow_Click(object sender, EventArgs e)
		{
			this.grid1.GridColor = System.Drawing.Color.Yellow;
		}

		private void msmiGridColorBlack_Click(object sender, EventArgs e)
		{
			this.grid1.GridColor = System.Drawing.Color.Black;
		}

		private void msmiGridColorBlue_Click(object sender, EventArgs e)
		{
			this.grid1.GridColor = System.Drawing.Color.Blue;
		}

		private void msmiGridColorPurple_Click(object sender, EventArgs e)
		{
			this.grid1.GridColor = System.Drawing.Color.Purple;
		}

		private void msmiGridColorCyan_Click(object sender, EventArgs e)
		{
			this.grid1.GridColor = System.Drawing.Color.Cyan;
		}

		private void msmiGridColorWhite_Click(object sender, EventArgs e)
		{
			this.grid1.GridColor = System.Drawing.Color.White;
		}

		private void msmiBorderColorRed_Click(object sender, EventArgs e)
		{
			this.grid1.BorderColor = System.Drawing.Color.Red;
		}

		private void msmiBorderColorGreen_Click(object sender, EventArgs e)
		{
			this.grid1.BorderColor = System.Drawing.Color.Green;
		}

		private void msmiBorderColorYellow_Click(object sender, EventArgs e)
		{
			this.grid1.BorderColor = System.Drawing.Color.Yellow;
		}

		private void msmiBorderColorBlack_Click(object sender, EventArgs e)
		{
			this.grid1.BorderColor = System.Drawing.Color.Black;
		}

		private void msmiBorderColorBlue_Click(object sender, EventArgs e)
		{
			this.grid1.BorderColor = System.Drawing.Color.Blue;
		}

		private void msmiBorderColorPurple_Click(object sender, EventArgs e)
		{
			this.grid1.BorderColor = System.Drawing.Color.Purple;
		}

		private void msmiBorderColorCyan_Click(object sender, EventArgs e)
		{
			this.grid1.BorderColor = System.Drawing.Color.Cyan;
		}

		private void msmiBorderColorWhite_Click(object sender, EventArgs e)
		{
			this.grid1.BorderColor = System.Drawing.Color.White;
		}

		private void wrapTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.WrapText = true;
		}

		private void nowrapTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.WrapText = false;
		}

		private void readonlyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Locked = true;
		}

		private void noreadonlyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Locked = false;
		}

		private void formGrid_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.isThreadSaveRedraw = false;
			if (this.Save())
			{
				this.fm.RedrawSubarea();
			}
		}

		private void toolStripComboBox_Font_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ToolStripComboBox toolStripComboBox = (ToolStripComboBox)sender;
				if (toolStripComboBox.Focused)
				{
					this.grid1.Selection.FontName = toolStripComboBox.Text;
				}
			}
			catch
			{
			}
		}

		private void tscmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ToolStripComboBox toolStripComboBox = (ToolStripComboBox)sender;
				if (toolStripComboBox.Focused)
				{
					this.grid1.Selection.FontSize = float.Parse(toolStripComboBox.Text);
				}
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

		private void tscmbFontSize_TextChanged(object sender, EventArgs e)
		{
			ToolStripComboBox toolStripComboBox = (ToolStripComboBox)sender;
			if (!toolStripComboBox.Focused)
			{
				return;
			}
			float num = 9f;
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
			this.grid1.Selection.FontSize = num;
		}

		private void toolStripLabel_Blod_Click(object sender, EventArgs e)
		{
			ToolStripLabel toolStripLabel = (ToolStripLabel)sender;
			if (toolStripLabel.BackColor == System.Drawing.SystemColors.Highlight)
			{
				this.grid1.Selection.FontBold = false;
				toolStripLabel.BackColor = System.Drawing.SystemColors.Control;
				return;
			}
			this.grid1.Selection.FontBold = true;
			toolStripLabel.BackColor = System.Drawing.SystemColors.Highlight;
		}

		private void toolStripLabel_Italic_Click(object sender, EventArgs e)
		{
			ToolStripLabel toolStripLabel = (ToolStripLabel)sender;
			if (toolStripLabel.BackColor == System.Drawing.SystemColors.Highlight)
			{
				this.grid1.Selection.FontItalic = false;
				toolStripLabel.BackColor = System.Drawing.SystemColors.Control;
				return;
			}
			this.grid1.Selection.FontItalic = true;
			toolStripLabel.BackColor = System.Drawing.SystemColors.Highlight;
		}

		private void toolStripLabel_Underline_Click(object sender, EventArgs e)
		{
			ToolStripLabel toolStripLabel = (ToolStripLabel)sender;
			if (toolStripLabel.BackColor == System.Drawing.SystemColors.Highlight)
			{
				this.grid1.Selection.FontUnderline = false;
				toolStripLabel.BackColor = System.Drawing.SystemColors.Control;
				return;
			}
			this.grid1.Selection.FontUnderline = true;
			toolStripLabel.BackColor = System.Drawing.SystemColors.Highlight;
		}

		private void button_combin_Cell_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Merge();
		}

		private void button_split_Cell_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.MergeCells = false;
			this.grid1.Selection.ForeColor = System.Drawing.Color.Red;
		}

		private void button_Save_Click(object sender, EventArgs e)
		{
			this.grid1.SaveFile(this.fileName);
		}

		private void button_Open_Click(object sender, EventArgs e)
		{
			this.importExcel_ToolStripMenuItem.PerformClick();
		}

		private void button_New_Click(object sender, EventArgs e)
		{
			this.panel1.Visible = false;
			this.grid1.Visible = false;
			this.Refresh();
			formGridCellAndCol formGridCellAndCol = new formGridCellAndCol();
			formGridCellAndCol.Get();
			int col = formGridCellAndCol.col;
			int row = formGridCellAndCol.row;
			if (col == 0 | row == 0)
			{
				this.panel1.Visible = true;
				this.grid1.Visible = true;
				base.BringToFront();
				return;
			}
			this.NewGrid(col, row, this.maxWidth);
			this.panel1.Visible = true;
			this.grid1.Visible = true;
			base.BringToFront();
		}

		private void NewGrid(int col, int row, int allwidth)
		{
			while (this.grid1.Cols < col + 1)
			{
				this.grid1.Selection.InsertCols();
			}
			while (this.grid1.Rows < row + 1)
			{
				this.grid1.Selection.InsertRows();
			}
			if (this.grid1.Cols - 1 != col)
			{
				if (this.grid1.Cols - 1 > col)
				{
					int num = this.grid1.Cols - 1 - col;
					for (int i = 0; i < num; i++)
					{
						this.grid1.Column(1).Delete();
					}
				}
				else
				{
					int num2 = col - (this.grid1.Cols - 1);
					for (int j = 0; j < num2; j++)
					{
						this.grid1.InsertCol(0, 0);
					}
				}
			}
			if (this.grid1.Rows - 1 != row)
			{
				if (this.grid1.Rows - 1 > row)
				{
					int num3 = this.grid1.Rows - 1 - row;
					for (int k = 0; k < num3; k++)
					{
						this.grid1.Row(1).Delete();
					}
				}
				else
				{
					int count = row - (this.grid1.Rows - 1);
					this.grid1.InsertRow(this.grid1.Rows - 1, count);
				}
			}
			if (formGridCellAndCol.autoRowNumber)
			{
				for (int l = 0; l < this.grid1.Rows; l++)
				{
					this.grid1.Cell(l, 1).Text = l.ToString();
				}
			}
			if (this.maxWidth == 0)
			{
				return;
			}
			int num4 = this.maxWidth / col;
			for (int m = 0; m < col; m++)
			{
				int num5 = num4;
				if (m == col - 1)
				{
					if (this.maxWidth % col != 0)
					{
						num5 = this.maxWidth - num4 * (col - 1) - 1;
					}
					else
					{
						num5 = num4 - 1;
					}
				}
				this.grid1.Column(m + 1).Width = short.Parse(num5.ToString());
			}
			for (int n = 0; n < this.grid1.Rows; n++)
			{
				this.grid1.Row(n).Height = 15;
				if (n > 0)
				{
					for (int num6 = 1; num6 < this.grid1.Cols; num6++)
					{
						this.grid1.Cell(n, num6).ForeColor = System.Drawing.Color.Red;
					}
				}
			}
			this.SetCellAutoWrap(this.grid1);
		}

		private void button_Insert_Row_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.InsertRows();
		}

		private void button_insert_Col_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.InsertCols();
		}

		private void button_delete_Row_Click(object sender, EventArgs e)
		{
			if (this.IsAlowDeleteRow())
			{
				this.grid1.Selection.DeleteByRow();
			}
		}

		private void button_delete_Col_Click(object sender, EventArgs e)
		{
			if (this.IsAlowDelteCol())
			{
				this.grid1.Selection.DeleteByCol();
			}
		}

		private bool IsAlowDeleteRow()
		{
			return this.grid1.Rows > 2;
		}

		private bool IsAlowDelteCol()
		{
			return this.grid1.Cols > 2;
		}

		private void toolStripLabel_Aligment_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (this.panel_Alignt.Visible)
			{
				this.panel_Alignt.Visible = false;
				this.panel_Alignt.SendToBack();
				return;
			}
			this.panel_Alignt.Visible = true;
			this.panel_Alignt.BringToFront();
		}

		private void pictureBox8_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.RightCenter;
			this.panel_Alignt.Visible = false;
			this.panel_Alignt.SendToBack();
		}

		private void pictureBox_Aligent_LeftTop_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.LeftTop;
			this.panel_Alignt.Visible = false;
			this.panel_Alignt.SendToBack();
		}

		private void pictureBox_Aligent_LeftMiddle_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.LeftCenter;
			this.panel_Alignt.Visible = false;
			this.panel_Alignt.SendToBack();
		}

		private void pictureBox_Aligent_LeftButtom_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.LeftBottom;
			this.panel_Alignt.Visible = false;
			this.panel_Alignt.SendToBack();
		}

		private void pictureBox_Aligent_MiddleTop_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.CenterTop;
			this.panel_Alignt.Visible = false;
			this.panel_Alignt.SendToBack();
		}

		private void pictureBox_Aligent_MiddleMiddle_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.CenterCenter;
			this.panel_Alignt.Visible = false;
			this.panel_Alignt.SendToBack();
		}

		private void pictureBox_Aligent_MiddleButtom_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.CenterBottom;
			this.panel_Alignt.Visible = false;
			this.panel_Alignt.SendToBack();
		}

		private void pictureBox_Aligent_RightTop_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.RightTop;
			this.panel_Alignt.Visible = false;
			this.panel_Alignt.SendToBack();
		}

		private void pictureBox_Aligent_RightButtom_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.Alignment = AlignmentEnum.RightBottom;
			this.panel_Alignt.Visible = false;
			this.panel_Alignt.SendToBack();
		}

		private void toolStripMenuItem_Backcolor_Red_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.BackColor = System.Drawing.Color.Red;
		}

		private void toolStripMenuItem_Backcolor_Green_Click(object sender, EventArgs e)
		{
			if (formMain.ledsys.SelectedPanel.ColorMode == LedColorMode.R)
			{
				this.grid1.Selection.BackColor = System.Drawing.Color.Black;
				return;
			}
			this.grid1.Selection.BackColor = System.Drawing.Color.Green;
		}

		private void toolStripMenuItem_Backcolor_Yellow_Click(object sender, EventArgs e)
		{
			if (formMain.ledsys.SelectedPanel.ColorMode == LedColorMode.R)
			{
				this.grid1.Selection.BackColor = System.Drawing.Color.Black;
				return;
			}
			this.grid1.Selection.BackColor = System.Drawing.Color.Yellow;
		}

		private void toolStripMenuItem_Backcolor_Black_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.BackColor = System.Drawing.Color.Black;
		}

		private void tsmiColorRed_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Red;
		}

		private void tsmiColorGreen_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Green;
		}

		private void tsmiColorYellow_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Yellow;
		}

		private void tsmiColorBlack_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Black;
		}

		private void tsmiColorBlue_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Blue;
		}

		private void tsmiColorPurple_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Purple;
		}

		private void tsmiColorCyan_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.Cyan;
		}

		private void tsmiColorWhite_Click(object sender, EventArgs e)
		{
			this.grid1.Selection.ForeColor = System.Drawing.Color.White;
		}

		private void MaxWind()
		{
			if (this.isMaxSize)
			{
				this.isMaxSize = false;
				base.Size = this.normalSize;
				base.Location = this.normalLocation;
				return;
			}
			this.isMaxSize = true;
			this.normalSize = base.Size;
			this.normalLocation = base.Location;
			base.Size = (base.Size = Screen.PrimaryScreen.WorkingArea.Size);
			base.Location = new System.Drawing.Point(0, 0);
		}

		private void pictureBox_max_Click(object sender, EventArgs e)
		{
			this.MaxWind();
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			this.MaxWind();
		}

		private void saveas_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Excel 97-2003 files(*.xls)|*.xls";
			saveFileDialog.FileName = "表格1";
			saveFileDialog.Title = "导出Excel";
			try
			{
				if (saveFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
			catch
			{
				saveFileDialog.InitialDirectory = (this.openFileDialog1.InitialDirectory = formMain.DesktopPath);
				if (saveFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
			ParameterizedThreadStart start = new ParameterizedThreadStart(this.ExportToExcel);
			new Thread(start)
			{
				IsBackground = true,
				Priority = ThreadPriority.Lowest
			}.Start(saveFileDialog.FileName);
		}

		private void ExportToExcel(object filename)
		{
			try
			{
				this.grid1.ExportToExcel(filename.ToString(), false, false);
			}
			catch
			{
			}
		}

		private void fileExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void button_wraptext_Click(object sender, EventArgs e)
		{
			ToolStripButton toolStripButton = (ToolStripButton)sender;
			this.grid1.Selection.WrapText = toolStripButton.Checked;
		}

		private void autoHeightToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void RowHeightAutoFit()
		{
			this.showAutoFitMsg = new formGrid.ShowAutoFitMsgDelegate(this.AutoFitMsg);
			this.grid1.AutoRedraw = false;
			try
			{
				int num = 0;
				while (num < this.totalRowNum && !this.isCancelled)
				{
					this.grid1.Row(num).AutoFit();
					this.nowRowNum++;
					base.BeginInvoke(this.showAutoFitMsg);
					Thread.Sleep(5);
					num++;
				}
				this.nowRowNum = this.totalRowNum;
			}
			catch
			{
				this.nowRowNum = this.totalRowNum;
			}
			finally
			{
				this.grid1.AutoRedraw = true;
				this.grid1.Refresh();
				if (!this.isCancelled)
				{
					Thread.Sleep(1000);
					this.btnAutoFitCancel.PerformClick();
				}
			}
		}

		private void AutoFitMsg()
		{
			string text = ((double)this.nowRowNum / (double)this.totalRowNum).ToString("0.0%");
			this.lblAutoFitPercent.Text = text;
			this.prgAutoFit.Value = this.nowRowNum;
			this.lblAutoFitPercent.Refresh();
			this.prgAutoFit.Refresh();
		}

		private void btnAutoFitCancel_Click(object sender, EventArgs e)
		{
			foreach (Control control in base.Controls)
			{
				control.Enabled = true;
			}
			this.grid1.Enabled = true;
			this.isCancelled = true;
			this.pnlAutoFit.SendToBack();
			this.pnlAutoFit.Visible = false;
			this.isThreadSaveRedrawSuspended = false;
		}

		private void zishiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.isThreadSaveRedrawSuspended = true;
			foreach (Control control in base.Controls)
			{
				control.Enabled = false;
			}
			this.totalRowNum = this.grid1.Rows;
			this.nowRowNum = 0;
			this.isCancelled = false;
			this.panel2.Enabled = true;
			this.grid1.Enabled = false;
			this.pnlAutoFit.Visible = true;
			this.pnlAutoFit.Enabled = true;
			this.lblAutoFitPercent.Enabled = true;
			this.prgAutoFit.Enabled = true;
			this.btnAutoFitCancel.Enabled = true;
			this.prgAutoFit.Minimum = 0;
			this.prgAutoFit.Maximum = this.totalRowNum;
			this.lblAutoFitPercent.Text = "0.0%";
			this.pnlAutoFit.Left = (this.panel1.Width - this.pnlAutoFit.Width) / 2;
			this.pnlAutoFit.Top = (this.panel1.Height - this.pnlAutoFit.Height) / 2;
			this.pnlAutoFit.BringToFront();
			this.pnlAutoFit.Refresh();
			this.thrRowHeightAutoFit = new Thread(new ThreadStart(this.RowHeightAutoFit));
			this.thrRowHeightAutoFit.IsBackground = true;
			this.thrRowHeightAutoFit.Start();
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
		}

		public static System.Drawing.Bitmap GetWindowCapture(IntPtr hWnd)
		{
			IntPtr windowDC = formGrid.GetWindowDC(hWnd);
			System.Drawing.Rectangle rectangle = default(System.Drawing.Rectangle);
			formGrid.GetWindowRect(hWnd, ref rectangle);
			int nWidth = rectangle.Right - rectangle.Left;
			int nHeight = rectangle.Bottom - rectangle.Top;
			IntPtr intPtr = formGrid.CreateCompatibleBitmap(windowDC, nWidth, nHeight);
			IntPtr intPtr2 = formGrid.CreateCompatibleDC(windowDC);
			formGrid.SelectObject(intPtr2, intPtr);
			formGrid.PrintWindow(hWnd, intPtr2, 0u);
			System.Drawing.Bitmap result = System.Drawing.Image.FromHbitmap(intPtr);
			formGrid.DeleteDC(windowDC);
			formGrid.DeleteDC(intPtr2);
			return result;
		}

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowRect(IntPtr hWnd, ref System.Drawing.Rectangle rect);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

		[DllImport("gdi32.dll")]
		public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		[DllImport("gdi32.dll")]
		public static extern int DeleteDC(IntPtr hdc);

		[DllImport("user32.dll")]
		public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hwnd);

		private void StripMenuItem_ImportProjectFile_Click(object sender, EventArgs e)
		{
			this.openFileDialog1.Filter = "Grid|*.zhl";
			try
			{
				if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					lock (this.this_LockObject)
					{
						File.Delete(this.fileName);
						File.Copy(this.openFileDialog1.FileName, this.fileName, true);
						this.Edit(this.fileName);
					}
				}
			}
			catch
			{
			}
		}

		private void StripMenuItem_ProjectFileSave_Click(object sender, EventArgs e)
		{
			this.saveFileDialog1.Filter = "Grid|*.zhl";
			try
			{
				if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					File.Copy(this.fileName, this.saveFileDialog1.FileName, true);
				}
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuItem_ImportProjectFile = new System.Windows.Forms.ToolStripMenuItem();
            this.importExcel_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.export_XmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveas_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.save_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuItem_ProjectFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.exit_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noMergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.insertRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.borderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.borderNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.borderLineUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderLineDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.borderInsideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderOutsideThinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderOutsideStickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.alignToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.alignNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignMiddleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.alignLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignTopLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignMiddleLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignBottomLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.alignCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignTopCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignMiddleCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignBottomCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.alignRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignTopRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignMiddleRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignBottomRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiColor = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiColorRed = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiColorGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiColorYellow = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiColorBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiColorPurple = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiColorCyan = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiColorWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.backcolor_RedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backColorRedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.backcolor_GreenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.backcolor_YellowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.backcolor_BlackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiGridColor = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiGridColorRed = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiGridColorGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiGridColorYellow = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiGridColorBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiGridColorBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiGridColorPurple = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiGridColorCyan = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiGridColorWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiBorderColor = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiBorderColorRed = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiBorderColorGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiBorderColorYellow = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiBorderColorBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiBorderColorBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiBorderColorPurple = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiBorderColorCyan = new System.Windows.Forms.ToolStripMenuItem();
            this.msmiBorderColorWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.wrapTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nowrapTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readonlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noreadonlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zishiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlAutoFit = new System.Windows.Forms.Panel();
            this.prgAutoFit = new System.Windows.Forms.ProgressBar();
            this.lblAutoFitPercent = new System.Windows.Forms.Label();
            this.btnAutoFitCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.splitGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelMergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemClearContent = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearStyleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.InsertRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AttributesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemLeftFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.RightFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpperBorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LowerBorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemForwardSlash = new System.Windows.Forms.ToolStripMenuItem();
            this.BackslashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.InnerBorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FineBorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ThickBorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AllBorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AlignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConventionalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.TopLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCenterLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemCen = new System.Windows.Forms.ToolStripMenuItem();
            this.TopCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAtCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.RightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCenterRight = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsmiColor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiColorRed = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiColorGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiColorYellow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiColorBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiColorPurple = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiColorCyan = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiColorWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.BGColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.GreenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.YellowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.BlackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiGridColor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiGridColorRed = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiGridColorGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiGridColorYellow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiGridColorBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiGridColorBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiGridColorPurple = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiGridColorCyan = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiGridColorWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiBorderColor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiBorderColorRed = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiBorderColorGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiBorderColorYellow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiBorderColorBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiBorderColorBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiBorderColorPurple = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiBorderColorCyan = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmiBorderColorWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.SetEnterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelEnterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.SetReadOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelReadonlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.button_New = new System.Windows.Forms.ToolStripButton();
            this.button_Open = new System.Windows.Forms.ToolStripButton();
            this.button_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.button_boardStyle = new System.Windows.Forms.ToolStripButton();
            this.button_Insert_Row = new System.Windows.Forms.ToolStripButton();
            this.button_insert_Col = new System.Windows.Forms.ToolStripButton();
            this.button_delete_Row = new System.Windows.Forms.ToolStripButton();
            this.button_delete_Col = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.button_combin_Cell = new System.Windows.Forms.ToolStripButton();
            this.button_split_Cell = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBox_Font = new System.Windows.Forms.ToolStripComboBox();
            this.tscmbFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel_Blod = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel_Italic = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel_Underline = new System.Windows.Forms.ToolStripLabel();
            this.button_wraptext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tssbtnColor = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiColorRed = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorYellow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorPurple = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorCyan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiColorWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem_Backcolor_Red = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Backcolor_Green = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Backcolor_Yellow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Backcolor_Black = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_Alignt = new System.Windows.Forms.Panel();
            this.pictureBox_Aligent_RightButtom = new System.Windows.Forms.PictureBox();
            this.pictureBox_Aligent_MiddleButtom = new System.Windows.Forms.PictureBox();
            this.pictureBox_Aligent_RightMiddle = new System.Windows.Forms.PictureBox();
            this.pictureBox_Aligent_MiddleMiddle = new System.Windows.Forms.PictureBox();
            this.pictureBox_Aligent_RightTop = new System.Windows.Forms.PictureBox();
            this.pictureBox_Aligent_MiddleTop = new System.Windows.Forms.PictureBox();
            this.pictureBox_Aligent_LeftButtom = new System.Windows.Forms.PictureBox();
            this.pictureBox_Aligent_LeftMiddle = new System.Windows.Forms.PictureBox();
            this.pictureBox_Aligent_LeftTop = new System.Windows.Forms.PictureBox();
            this.pictureBox_max = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBox大气区湿度 = new System.Windows.Forms.TextBox();
            this.textBox大气区温度 = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.textBox降温区湿度 = new System.Windows.Forms.TextBox();
            this.textBox降温区温度 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.textBox恒温区湿度 = new System.Windows.Forms.TextBox();
            this.textBox恒温区温度 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.textBox升温区湿度 = new System.Windows.Forms.TextBox();
            this.textBox升温区温度 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlAutoFit.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel_Alignt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_RightButtom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_MiddleButtom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_RightMiddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_MiddleMiddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_RightTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_MiddleTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_LeftButtom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_LeftMiddle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_LeftTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileExcelToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.propertyToolStripMenuItem,
            this.formatToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(4, 1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(172, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileExcelToolStripMenuItem
            // 
            this.fileExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenuItem_ImportProjectFile,
            this.importExcel_ToolStripMenuItem,
            this.export_XmlToolStripMenuItem,
            this.saveas_ToolStripMenuItem,
            this.save_ToolStripMenuItem,
            this.StripMenuItem_ProjectFileSave,
            this.exit_ToolStripMenuItem});
            this.fileExcelToolStripMenuItem.Name = "fileExcelToolStripMenuItem";
            this.fileExcelToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.fileExcelToolStripMenuItem.Text = "文件";
            this.fileExcelToolStripMenuItem.Click += new System.EventHandler(this.fileExcelToolStripMenuItem_Click);
            // 
            // StripMenuItem_ImportProjectFile
            // 
            this.StripMenuItem_ImportProjectFile.Name = "StripMenuItem_ImportProjectFile";
            this.StripMenuItem_ImportProjectFile.Size = new System.Drawing.Size(124, 22);
            this.StripMenuItem_ImportProjectFile.Text = "打开";
            this.StripMenuItem_ImportProjectFile.Click += new System.EventHandler(this.StripMenuItem_ImportProjectFile_Click);
            // 
            // importExcel_ToolStripMenuItem
            // 
            this.importExcel_ToolStripMenuItem.Name = "importExcel_ToolStripMenuItem";
            this.importExcel_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.importExcel_ToolStripMenuItem.Text = "导入Excel";
            this.importExcel_ToolStripMenuItem.Click += new System.EventHandler(this.importExcel_ToolStripMenuItem_Click);
            // 
            // export_XmlToolStripMenuItem
            // 
            this.export_XmlToolStripMenuItem.Name = "export_XmlToolStripMenuItem";
            this.export_XmlToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.export_XmlToolStripMenuItem.Text = "导出Xml";
            this.export_XmlToolStripMenuItem.Visible = false;
            this.export_XmlToolStripMenuItem.Click += new System.EventHandler(this.export_XmlToolStripMenuItem_Click);
            // 
            // saveas_ToolStripMenuItem
            // 
            this.saveas_ToolStripMenuItem.Name = "saveas_ToolStripMenuItem";
            this.saveas_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveas_ToolStripMenuItem.Text = "导出Excel";
            this.saveas_ToolStripMenuItem.Click += new System.EventHandler(this.saveas_ToolStripMenuItem_Click);
            // 
            // save_ToolStripMenuItem
            // 
            this.save_ToolStripMenuItem.Name = "save_ToolStripMenuItem";
            this.save_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.save_ToolStripMenuItem.Text = "保存";
            this.save_ToolStripMenuItem.Click += new System.EventHandler(this.save_ToolStripMenuItem_Click);
            // 
            // StripMenuItem_ProjectFileSave
            // 
            this.StripMenuItem_ProjectFileSave.Name = "StripMenuItem_ProjectFileSave";
            this.StripMenuItem_ProjectFileSave.Size = new System.Drawing.Size(124, 22);
            this.StripMenuItem_ProjectFileSave.Text = "另存为";
            this.StripMenuItem_ProjectFileSave.Click += new System.EventHandler(this.StripMenuItem_ProjectFileSave_Click);
            // 
            // exit_ToolStripMenuItem
            // 
            this.exit_ToolStripMenuItem.Name = "exit_ToolStripMenuItem";
            this.exit_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.exit_ToolStripMenuItem.Text = "退出";
            this.exit_ToolStripMenuItem.Click += new System.EventHandler(this.exit_ToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToolStripMenuItem,
            this.CutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearToolStripMenuItem,
            this.toolStripSeparator3,
            this.mergeToolStripMenuItem,
            this.noMergeToolStripMenuItem,
            this.toolStripSeparator4,
            this.insertRowToolStripMenuItem,
            this.insertColumnToolStripMenuItem,
            this.toolStripSeparator5,
            this.removeRowToolStripMenuItem,
            this.removeColumnToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.EditToolStripMenuItem.Text = "编辑";
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.CopyToolStripMenuItem.Text = "复制";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // CutToolStripMenuItem
            // 
            this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
            this.CutToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.CutToolStripMenuItem.Text = "剪切";
            this.CutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.pasteToolStripMenuItem.Text = "粘贴";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearContentToolStripMenuItem,
            this.clearFormatToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearAllToolStripMenuItem});
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.clearToolStripMenuItem.Text = "清除";
            // 
            // clearContentToolStripMenuItem
            // 
            this.clearContentToolStripMenuItem.Name = "clearContentToolStripMenuItem";
            this.clearContentToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.clearContentToolStripMenuItem.Text = "清除内容";
            this.clearContentToolStripMenuItem.Click += new System.EventHandler(this.clearContentToolStripMenuItem_Click);
            // 
            // clearFormatToolStripMenuItem
            // 
            this.clearFormatToolStripMenuItem.Name = "clearFormatToolStripMenuItem";
            this.clearFormatToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.clearFormatToolStripMenuItem.Text = "清除格式";
            this.clearFormatToolStripMenuItem.Click += new System.EventHandler(this.clearFormatToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.clearAllToolStripMenuItem.Text = "清除全部";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(151, 6);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.mergeToolStripMenuItem.Text = "合并单元格";
            this.mergeToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
            // 
            // noMergeToolStripMenuItem
            // 
            this.noMergeToolStripMenuItem.Name = "noMergeToolStripMenuItem";
            this.noMergeToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.noMergeToolStripMenuItem.Text = "取消合并单元格";
            this.noMergeToolStripMenuItem.Click += new System.EventHandler(this.noMergeToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(151, 6);
            // 
            // insertRowToolStripMenuItem
            // 
            this.insertRowToolStripMenuItem.Name = "insertRowToolStripMenuItem";
            this.insertRowToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.insertRowToolStripMenuItem.Text = "插入行";
            this.insertRowToolStripMenuItem.Click += new System.EventHandler(this.insertRowToolStripMenuItem_Click);
            // 
            // insertColumnToolStripMenuItem
            // 
            this.insertColumnToolStripMenuItem.Name = "insertColumnToolStripMenuItem";
            this.insertColumnToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.insertColumnToolStripMenuItem.Text = "插入列";
            this.insertColumnToolStripMenuItem.Click += new System.EventHandler(this.insertColumnToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(151, 6);
            // 
            // removeRowToolStripMenuItem
            // 
            this.removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
            this.removeRowToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.removeRowToolStripMenuItem.Text = "删除行";
            this.removeRowToolStripMenuItem.Click += new System.EventHandler(this.removeRowToolStripMenuItem_Click);
            // 
            // removeColumnToolStripMenuItem
            // 
            this.removeColumnToolStripMenuItem.Name = "removeColumnToolStripMenuItem";
            this.removeColumnToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.removeColumnToolStripMenuItem.Text = "删除列";
            this.removeColumnToolStripMenuItem.Click += new System.EventHandler(this.removeColumnToolStripMenuItem_Click);
            // 
            // propertyToolStripMenuItem
            // 
            this.propertyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontToolStripMenuItem1,
            this.borderToolStripMenuItem1,
            this.alignToolStripMenuItem1,
            this.msmiColor,
            this.backcolor_RedToolStripMenuItem,
            this.msmiGridColor,
            this.msmiBorderColor,
            this.wrapTextToolStripMenuItem,
            this.nowrapTextToolStripMenuItem,
            this.readonlyToolStripMenuItem,
            this.noreadonlyToolStripMenuItem});
            this.propertyToolStripMenuItem.Name = "propertyToolStripMenuItem";
            this.propertyToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.propertyToolStripMenuItem.Text = "属性";
            // 
            // fontToolStripMenuItem1
            // 
            this.fontToolStripMenuItem1.Name = "fontToolStripMenuItem1";
            this.fontToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.fontToolStripMenuItem1.Text = "字体";
            this.fontToolStripMenuItem1.Click += new System.EventHandler(this.fontToolStripMenuItem1_Click);
            // 
            // borderToolStripMenuItem1
            // 
            this.borderToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.borderNoneToolStripMenuItem,
            this.borderLeftToolStripMenuItem,
            this.borderRightToolStripMenuItem,
            this.borderTopToolStripMenuItem,
            this.borderBottomToolStripMenuItem,
            this.toolStripSeparator6,
            this.borderLineUpToolStripMenuItem,
            this.borderLineDownToolStripMenuItem,
            this.toolStripSeparator7,
            this.borderInsideToolStripMenuItem,
            this.borderOutsideThinToolStripMenuItem,
            this.borderOutsideStickToolStripMenuItem,
            this.borderAllToolStripMenuItem1});
            this.borderToolStripMenuItem1.Name = "borderToolStripMenuItem1";
            this.borderToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.borderToolStripMenuItem1.Text = "边框";
            // 
            // borderNoneToolStripMenuItem
            // 
            this.borderNoneToolStripMenuItem.Name = "borderNoneToolStripMenuItem";
            this.borderNoneToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderNoneToolStripMenuItem.Text = "无边框";
            this.borderNoneToolStripMenuItem.Click += new System.EventHandler(this.borderNoneToolStripMenuItem_Click);
            // 
            // borderLeftToolStripMenuItem
            // 
            this.borderLeftToolStripMenuItem.Name = "borderLeftToolStripMenuItem";
            this.borderLeftToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderLeftToolStripMenuItem.Text = "左边框";
            this.borderLeftToolStripMenuItem.Click += new System.EventHandler(this.borderLeftToolStripMenuItem_Click);
            // 
            // borderRightToolStripMenuItem
            // 
            this.borderRightToolStripMenuItem.Name = "borderRightToolStripMenuItem";
            this.borderRightToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderRightToolStripMenuItem.Text = "右边框";
            this.borderRightToolStripMenuItem.Click += new System.EventHandler(this.borderRightToolStripMenuItem_Click);
            // 
            // borderTopToolStripMenuItem
            // 
            this.borderTopToolStripMenuItem.Name = "borderTopToolStripMenuItem";
            this.borderTopToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderTopToolStripMenuItem.Text = "上边框";
            this.borderTopToolStripMenuItem.Click += new System.EventHandler(this.borderTopToolStripMenuItem_Click);
            // 
            // borderBottomToolStripMenuItem
            // 
            this.borderBottomToolStripMenuItem.Name = "borderBottomToolStripMenuItem";
            this.borderBottomToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderBottomToolStripMenuItem.Text = "下边框";
            this.borderBottomToolStripMenuItem.Click += new System.EventHandler(this.borderBottomToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(139, 6);
            // 
            // borderLineUpToolStripMenuItem
            // 
            this.borderLineUpToolStripMenuItem.Name = "borderLineUpToolStripMenuItem";
            this.borderLineUpToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderLineUpToolStripMenuItem.Text = "正斜线";
            this.borderLineUpToolStripMenuItem.Click += new System.EventHandler(this.borderLineUpToolStripMenuItem_Click);
            // 
            // borderLineDownToolStripMenuItem
            // 
            this.borderLineDownToolStripMenuItem.Name = "borderLineDownToolStripMenuItem";
            this.borderLineDownToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderLineDownToolStripMenuItem.Text = "反斜线";
            this.borderLineDownToolStripMenuItem.Click += new System.EventHandler(this.borderLineDownToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(139, 6);
            // 
            // borderInsideToolStripMenuItem
            // 
            this.borderInsideToolStripMenuItem.Name = "borderInsideToolStripMenuItem";
            this.borderInsideToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderInsideToolStripMenuItem.Text = "内部边框";
            this.borderInsideToolStripMenuItem.Click += new System.EventHandler(this.borderInsideToolStripMenuItem_Click);
            // 
            // borderOutsideThinToolStripMenuItem
            // 
            this.borderOutsideThinToolStripMenuItem.Name = "borderOutsideThinToolStripMenuItem";
            this.borderOutsideThinToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderOutsideThinToolStripMenuItem.Text = "外部边框(细)";
            this.borderOutsideThinToolStripMenuItem.Click += new System.EventHandler(this.borderOutsideThinToolStripMenuItem_Click);
            // 
            // borderOutsideStickToolStripMenuItem
            // 
            this.borderOutsideStickToolStripMenuItem.Name = "borderOutsideStickToolStripMenuItem";
            this.borderOutsideStickToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.borderOutsideStickToolStripMenuItem.Text = "外部边框(粗)";
            this.borderOutsideStickToolStripMenuItem.Click += new System.EventHandler(this.borderOutsideStickToolStripMenuItem_Click);
            // 
            // borderAllToolStripMenuItem1
            // 
            this.borderAllToolStripMenuItem1.Name = "borderAllToolStripMenuItem1";
            this.borderAllToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.borderAllToolStripMenuItem1.Text = "全部边框";
            this.borderAllToolStripMenuItem1.Click += new System.EventHandler(this.borderAllToolStripMenuItem1_Click);
            // 
            // alignToolStripMenuItem1
            // 
            this.alignToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alignNormalToolStripMenuItem,
            this.alignTopToolStripMenuItem,
            this.alignMiddleToolStripMenuItem,
            this.alignBottomToolStripMenuItem,
            this.toolStripSeparator10,
            this.alignLeftToolStripMenuItem,
            this.alignTopLeftToolStripMenuItem,
            this.alignMiddleLeftToolStripMenuItem,
            this.alignBottomLeftToolStripMenuItem,
            this.toolStripSeparator9,
            this.alignCenterToolStripMenuItem,
            this.alignTopCenterToolStripMenuItem,
            this.alignMiddleCenterToolStripMenuItem,
            this.alignBottomCenterToolStripMenuItem,
            this.toolStripSeparator8,
            this.alignRightToolStripMenuItem,
            this.alignTopRightToolStripMenuItem,
            this.alignMiddleRightToolStripMenuItem,
            this.alignBottomRightToolStripMenuItem});
            this.alignToolStripMenuItem1.Name = "alignToolStripMenuItem1";
            this.alignToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.alignToolStripMenuItem1.Text = "对齐方式";
            // 
            // alignNormalToolStripMenuItem
            // 
            this.alignNormalToolStripMenuItem.Name = "alignNormalToolStripMenuItem";
            this.alignNormalToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignNormalToolStripMenuItem.Text = "常规";
            this.alignNormalToolStripMenuItem.Click += new System.EventHandler(this.alignNormalToolStripMenuItem_Click);
            // 
            // alignTopToolStripMenuItem
            // 
            this.alignTopToolStripMenuItem.Name = "alignTopToolStripMenuItem";
            this.alignTopToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignTopToolStripMenuItem.Text = "顶部对齐";
            this.alignTopToolStripMenuItem.Click += new System.EventHandler(this.alignTopToolStripMenuItem_Click);
            // 
            // alignMiddleToolStripMenuItem
            // 
            this.alignMiddleToolStripMenuItem.Name = "alignMiddleToolStripMenuItem";
            this.alignMiddleToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignMiddleToolStripMenuItem.Text = "中间对齐";
            this.alignMiddleToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleToolStripMenuItem_Click);
            // 
            // alignBottomToolStripMenuItem
            // 
            this.alignBottomToolStripMenuItem.Name = "alignBottomToolStripMenuItem";
            this.alignBottomToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignBottomToolStripMenuItem.Text = "底部对齐";
            this.alignBottomToolStripMenuItem.Click += new System.EventHandler(this.alignBottomToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(127, 6);
            // 
            // alignLeftToolStripMenuItem
            // 
            this.alignLeftToolStripMenuItem.Name = "alignLeftToolStripMenuItem";
            this.alignLeftToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignLeftToolStripMenuItem.Text = "左对齐";
            this.alignLeftToolStripMenuItem.Click += new System.EventHandler(this.alignLeftToolStripMenuItem_Click);
            // 
            // alignTopLeftToolStripMenuItem
            // 
            this.alignTopLeftToolStripMenuItem.Name = "alignTopLeftToolStripMenuItem";
            this.alignTopLeftToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignTopLeftToolStripMenuItem.Text = "顶部左对齐";
            this.alignTopLeftToolStripMenuItem.Click += new System.EventHandler(this.alignTopLeftToolStripMenuItem_Click);
            // 
            // alignMiddleLeftToolStripMenuItem
            // 
            this.alignMiddleLeftToolStripMenuItem.Name = "alignMiddleLeftToolStripMenuItem";
            this.alignMiddleLeftToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignMiddleLeftToolStripMenuItem.Text = "中间左对齐";
            this.alignMiddleLeftToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleLeftToolStripMenuItem_Click);
            // 
            // alignBottomLeftToolStripMenuItem
            // 
            this.alignBottomLeftToolStripMenuItem.Name = "alignBottomLeftToolStripMenuItem";
            this.alignBottomLeftToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignBottomLeftToolStripMenuItem.Text = "底部左对齐";
            this.alignBottomLeftToolStripMenuItem.Click += new System.EventHandler(this.alignBottomLeftToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(127, 6);
            // 
            // alignCenterToolStripMenuItem
            // 
            this.alignCenterToolStripMenuItem.Name = "alignCenterToolStripMenuItem";
            this.alignCenterToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignCenterToolStripMenuItem.Text = "中对齐";
            this.alignCenterToolStripMenuItem.Click += new System.EventHandler(this.alignCenterToolStripMenuItem_Click);
            // 
            // alignTopCenterToolStripMenuItem
            // 
            this.alignTopCenterToolStripMenuItem.Name = "alignTopCenterToolStripMenuItem";
            this.alignTopCenterToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignTopCenterToolStripMenuItem.Text = "顶部中对齐";
            this.alignTopCenterToolStripMenuItem.Click += new System.EventHandler(this.alignTopCenterToolStripMenuItem_Click);
            // 
            // alignMiddleCenterToolStripMenuItem
            // 
            this.alignMiddleCenterToolStripMenuItem.Name = "alignMiddleCenterToolStripMenuItem";
            this.alignMiddleCenterToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignMiddleCenterToolStripMenuItem.Text = "中心对齐";
            this.alignMiddleCenterToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleCenterToolStripMenuItem_Click);
            // 
            // alignBottomCenterToolStripMenuItem
            // 
            this.alignBottomCenterToolStripMenuItem.Name = "alignBottomCenterToolStripMenuItem";
            this.alignBottomCenterToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignBottomCenterToolStripMenuItem.Text = "底部中对齐";
            this.alignBottomCenterToolStripMenuItem.Click += new System.EventHandler(this.alignBottomCenterToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(127, 6);
            // 
            // alignRightToolStripMenuItem
            // 
            this.alignRightToolStripMenuItem.Name = "alignRightToolStripMenuItem";
            this.alignRightToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignRightToolStripMenuItem.Text = "右对齐";
            this.alignRightToolStripMenuItem.Click += new System.EventHandler(this.alignRightToolStripMenuItem_Click);
            // 
            // alignTopRightToolStripMenuItem
            // 
            this.alignTopRightToolStripMenuItem.Name = "alignTopRightToolStripMenuItem";
            this.alignTopRightToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignTopRightToolStripMenuItem.Text = "顶部右对齐";
            this.alignTopRightToolStripMenuItem.Click += new System.EventHandler(this.alignTopRightToolStripMenuItem_Click);
            // 
            // alignMiddleRightToolStripMenuItem
            // 
            this.alignMiddleRightToolStripMenuItem.Name = "alignMiddleRightToolStripMenuItem";
            this.alignMiddleRightToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignMiddleRightToolStripMenuItem.Text = "中间右对齐";
            this.alignMiddleRightToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleRightToolStripMenuItem_Click);
            // 
            // alignBottomRightToolStripMenuItem
            // 
            this.alignBottomRightToolStripMenuItem.Name = "alignBottomRightToolStripMenuItem";
            this.alignBottomRightToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.alignBottomRightToolStripMenuItem.Text = "底部右对齐";
            this.alignBottomRightToolStripMenuItem.Click += new System.EventHandler(this.alignBottomRightToolStripMenuItem_Click);
            // 
            // msmiColor
            // 
            this.msmiColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msmiColorRed,
            this.msmiColorGreen,
            this.msmiColorYellow,
            this.msmiColorBlue,
            this.msmiColorPurple,
            this.msmiColorCyan,
            this.msmiColorWhite});
            this.msmiColor.Name = "msmiColor";
            this.msmiColor.Size = new System.Drawing.Size(154, 22);
            this.msmiColor.Text = "文字颜色";
            // 
            // msmiColorRed
            // 
            this.msmiColorRed.Name = "msmiColorRed";
            this.msmiColorRed.Size = new System.Drawing.Size(82, 22);
            this.msmiColorRed.Text = "红";
            this.msmiColorRed.Click += new System.EventHandler(this.msmiColorRed_Click);
            // 
            // msmiColorGreen
            // 
            this.msmiColorGreen.Name = "msmiColorGreen";
            this.msmiColorGreen.Size = new System.Drawing.Size(82, 22);
            this.msmiColorGreen.Text = "绿";
            this.msmiColorGreen.Click += new System.EventHandler(this.msmiColorGreen_Click);
            // 
            // msmiColorYellow
            // 
            this.msmiColorYellow.Name = "msmiColorYellow";
            this.msmiColorYellow.Size = new System.Drawing.Size(82, 22);
            this.msmiColorYellow.Text = "黄";
            this.msmiColorYellow.Click += new System.EventHandler(this.msmiColorYellow_Click);
            // 
            // msmiColorBlue
            // 
            this.msmiColorBlue.Name = "msmiColorBlue";
            this.msmiColorBlue.Size = new System.Drawing.Size(82, 22);
            this.msmiColorBlue.Text = "蓝";
            this.msmiColorBlue.Click += new System.EventHandler(this.msmiColorBlue_Click);
            // 
            // msmiColorPurple
            // 
            this.msmiColorPurple.Name = "msmiColorPurple";
            this.msmiColorPurple.Size = new System.Drawing.Size(82, 22);
            this.msmiColorPurple.Text = "紫";
            this.msmiColorPurple.Click += new System.EventHandler(this.msmiColorPurple_Click);
            // 
            // msmiColorCyan
            // 
            this.msmiColorCyan.Name = "msmiColorCyan";
            this.msmiColorCyan.Size = new System.Drawing.Size(82, 22);
            this.msmiColorCyan.Text = "青";
            this.msmiColorCyan.Click += new System.EventHandler(this.msmiColorCyan_Click);
            // 
            // msmiColorWhite
            // 
            this.msmiColorWhite.Name = "msmiColorWhite";
            this.msmiColorWhite.Size = new System.Drawing.Size(82, 22);
            this.msmiColorWhite.Text = "白";
            this.msmiColorWhite.Click += new System.EventHandler(this.msmiColorWhite_Click);
            // 
            // backcolor_RedToolStripMenuItem
            // 
            this.backcolor_RedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backColorRedToolStripMenuItem1,
            this.backcolor_GreenToolStripMenuItem1,
            this.backcolor_YellowToolStripMenuItem1,
            this.backcolor_BlackToolStripMenuItem});
            this.backcolor_RedToolStripMenuItem.Name = "backcolor_RedToolStripMenuItem";
            this.backcolor_RedToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.backcolor_RedToolStripMenuItem.Text = "背景颜色";
            // 
            // backColorRedToolStripMenuItem1
            // 
            this.backColorRedToolStripMenuItem1.Name = "backColorRedToolStripMenuItem1";
            this.backColorRedToolStripMenuItem1.Size = new System.Drawing.Size(82, 22);
            this.backColorRedToolStripMenuItem1.Text = "红";
            this.backColorRedToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_RedToolStripMenuItem1_Click);
            // 
            // backcolor_GreenToolStripMenuItem1
            // 
            this.backcolor_GreenToolStripMenuItem1.Name = "backcolor_GreenToolStripMenuItem1";
            this.backcolor_GreenToolStripMenuItem1.Size = new System.Drawing.Size(82, 22);
            this.backcolor_GreenToolStripMenuItem1.Text = "绿";
            this.backcolor_GreenToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_GreenToolStripMenuItem1_Click);
            // 
            // backcolor_YellowToolStripMenuItem1
            // 
            this.backcolor_YellowToolStripMenuItem1.Name = "backcolor_YellowToolStripMenuItem1";
            this.backcolor_YellowToolStripMenuItem1.Size = new System.Drawing.Size(82, 22);
            this.backcolor_YellowToolStripMenuItem1.Text = "黄";
            this.backcolor_YellowToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_YellowToolStripMenuItem1_Click);
            // 
            // backcolor_BlackToolStripMenuItem
            // 
            this.backcolor_BlackToolStripMenuItem.Name = "backcolor_BlackToolStripMenuItem";
            this.backcolor_BlackToolStripMenuItem.Size = new System.Drawing.Size(82, 22);
            this.backcolor_BlackToolStripMenuItem.Text = "黑";
            this.backcolor_BlackToolStripMenuItem.Click += new System.EventHandler(this.backcolor_BlackToolStripMenuItem_Click);
            // 
            // msmiGridColor
            // 
            this.msmiGridColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msmiGridColorRed,
            this.msmiGridColorGreen,
            this.msmiGridColorYellow,
            this.msmiGridColorBlack,
            this.msmiGridColorBlue,
            this.msmiGridColorPurple,
            this.msmiGridColorCyan,
            this.msmiGridColorWhite});
            this.msmiGridColor.Name = "msmiGridColor";
            this.msmiGridColor.Size = new System.Drawing.Size(154, 22);
            this.msmiGridColor.Text = "表格颜色";
            // 
            // msmiGridColorRed
            // 
            this.msmiGridColorRed.Name = "msmiGridColorRed";
            this.msmiGridColorRed.Size = new System.Drawing.Size(82, 22);
            this.msmiGridColorRed.Text = "红";
            this.msmiGridColorRed.Click += new System.EventHandler(this.msmiGridColorRed_Click);
            // 
            // msmiGridColorGreen
            // 
            this.msmiGridColorGreen.Name = "msmiGridColorGreen";
            this.msmiGridColorGreen.Size = new System.Drawing.Size(82, 22);
            this.msmiGridColorGreen.Text = "绿";
            this.msmiGridColorGreen.Click += new System.EventHandler(this.msmiGridColorGreen_Click);
            // 
            // msmiGridColorYellow
            // 
            this.msmiGridColorYellow.Name = "msmiGridColorYellow";
            this.msmiGridColorYellow.Size = new System.Drawing.Size(82, 22);
            this.msmiGridColorYellow.Text = "黄";
            this.msmiGridColorYellow.Click += new System.EventHandler(this.msmiGridColorYellow_Click);
            // 
            // msmiGridColorBlack
            // 
            this.msmiGridColorBlack.Name = "msmiGridColorBlack";
            this.msmiGridColorBlack.Size = new System.Drawing.Size(82, 22);
            this.msmiGridColorBlack.Text = "黑";
            this.msmiGridColorBlack.Click += new System.EventHandler(this.msmiGridColorBlack_Click);
            // 
            // msmiGridColorBlue
            // 
            this.msmiGridColorBlue.Name = "msmiGridColorBlue";
            this.msmiGridColorBlue.Size = new System.Drawing.Size(82, 22);
            this.msmiGridColorBlue.Text = "蓝";
            this.msmiGridColorBlue.Click += new System.EventHandler(this.msmiGridColorBlue_Click);
            // 
            // msmiGridColorPurple
            // 
            this.msmiGridColorPurple.Name = "msmiGridColorPurple";
            this.msmiGridColorPurple.Size = new System.Drawing.Size(82, 22);
            this.msmiGridColorPurple.Text = "紫";
            this.msmiGridColorPurple.Click += new System.EventHandler(this.msmiGridColorPurple_Click);
            // 
            // msmiGridColorCyan
            // 
            this.msmiGridColorCyan.Name = "msmiGridColorCyan";
            this.msmiGridColorCyan.Size = new System.Drawing.Size(82, 22);
            this.msmiGridColorCyan.Text = "青";
            this.msmiGridColorCyan.Click += new System.EventHandler(this.msmiGridColorCyan_Click);
            // 
            // msmiGridColorWhite
            // 
            this.msmiGridColorWhite.Name = "msmiGridColorWhite";
            this.msmiGridColorWhite.Size = new System.Drawing.Size(82, 22);
            this.msmiGridColorWhite.Text = "白";
            this.msmiGridColorWhite.Click += new System.EventHandler(this.msmiGridColorWhite_Click);
            // 
            // msmiBorderColor
            // 
            this.msmiBorderColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msmiBorderColorRed,
            this.msmiBorderColorGreen,
            this.msmiBorderColorYellow,
            this.msmiBorderColorBlack,
            this.msmiBorderColorBlue,
            this.msmiBorderColorPurple,
            this.msmiBorderColorCyan,
            this.msmiBorderColorWhite});
            this.msmiBorderColor.Name = "msmiBorderColor";
            this.msmiBorderColor.Size = new System.Drawing.Size(154, 22);
            this.msmiBorderColor.Text = "边框颜色";
            // 
            // msmiBorderColorRed
            // 
            this.msmiBorderColorRed.Name = "msmiBorderColorRed";
            this.msmiBorderColorRed.Size = new System.Drawing.Size(82, 22);
            this.msmiBorderColorRed.Text = "红";
            this.msmiBorderColorRed.Click += new System.EventHandler(this.msmiBorderColorRed_Click);
            // 
            // msmiBorderColorGreen
            // 
            this.msmiBorderColorGreen.Name = "msmiBorderColorGreen";
            this.msmiBorderColorGreen.Size = new System.Drawing.Size(82, 22);
            this.msmiBorderColorGreen.Text = "绿";
            this.msmiBorderColorGreen.Click += new System.EventHandler(this.msmiBorderColorGreen_Click);
            // 
            // msmiBorderColorYellow
            // 
            this.msmiBorderColorYellow.Name = "msmiBorderColorYellow";
            this.msmiBorderColorYellow.Size = new System.Drawing.Size(82, 22);
            this.msmiBorderColorYellow.Text = "黄";
            this.msmiBorderColorYellow.Click += new System.EventHandler(this.msmiBorderColorYellow_Click);
            // 
            // msmiBorderColorBlack
            // 
            this.msmiBorderColorBlack.Name = "msmiBorderColorBlack";
            this.msmiBorderColorBlack.Size = new System.Drawing.Size(82, 22);
            this.msmiBorderColorBlack.Text = "黑";
            this.msmiBorderColorBlack.Click += new System.EventHandler(this.msmiBorderColorBlack_Click);
            // 
            // msmiBorderColorBlue
            // 
            this.msmiBorderColorBlue.Name = "msmiBorderColorBlue";
            this.msmiBorderColorBlue.Size = new System.Drawing.Size(82, 22);
            this.msmiBorderColorBlue.Text = "蓝";
            this.msmiBorderColorBlue.Click += new System.EventHandler(this.msmiBorderColorBlue_Click);
            // 
            // msmiBorderColorPurple
            // 
            this.msmiBorderColorPurple.Name = "msmiBorderColorPurple";
            this.msmiBorderColorPurple.Size = new System.Drawing.Size(82, 22);
            this.msmiBorderColorPurple.Text = "紫";
            this.msmiBorderColorPurple.Click += new System.EventHandler(this.msmiBorderColorPurple_Click);
            // 
            // msmiBorderColorCyan
            // 
            this.msmiBorderColorCyan.Name = "msmiBorderColorCyan";
            this.msmiBorderColorCyan.Size = new System.Drawing.Size(82, 22);
            this.msmiBorderColorCyan.Text = "青";
            this.msmiBorderColorCyan.Click += new System.EventHandler(this.msmiBorderColorCyan_Click);
            // 
            // msmiBorderColorWhite
            // 
            this.msmiBorderColorWhite.Name = "msmiBorderColorWhite";
            this.msmiBorderColorWhite.Size = new System.Drawing.Size(82, 22);
            this.msmiBorderColorWhite.Text = "白";
            this.msmiBorderColorWhite.Click += new System.EventHandler(this.msmiBorderColorWhite_Click);
            // 
            // wrapTextToolStripMenuItem
            // 
            this.wrapTextToolStripMenuItem.Name = "wrapTextToolStripMenuItem";
            this.wrapTextToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.wrapTextToolStripMenuItem.Text = "设置自动换行";
            this.wrapTextToolStripMenuItem.Click += new System.EventHandler(this.wrapTextToolStripMenuItem_Click);
            // 
            // nowrapTextToolStripMenuItem
            // 
            this.nowrapTextToolStripMenuItem.Name = "nowrapTextToolStripMenuItem";
            this.nowrapTextToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.nowrapTextToolStripMenuItem.Text = "取消自动换行";
            this.nowrapTextToolStripMenuItem.Click += new System.EventHandler(this.nowrapTextToolStripMenuItem_Click);
            // 
            // readonlyToolStripMenuItem
            // 
            this.readonlyToolStripMenuItem.Name = "readonlyToolStripMenuItem";
            this.readonlyToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.readonlyToolStripMenuItem.Text = "设置单元格只读";
            this.readonlyToolStripMenuItem.Click += new System.EventHandler(this.readonlyToolStripMenuItem_Click);
            // 
            // noreadonlyToolStripMenuItem
            // 
            this.noreadonlyToolStripMenuItem.Name = "noreadonlyToolStripMenuItem";
            this.noreadonlyToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.noreadonlyToolStripMenuItem.Text = "取消单元格只读";
            this.noreadonlyToolStripMenuItem.Click += new System.EventHandler(this.noreadonlyToolStripMenuItem_Click);
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zishiToolStripMenuItem});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.formatToolStripMenuItem.Text = "格式";
            // 
            // zishiToolStripMenuItem
            // 
            this.zishiToolStripMenuItem.Name = "zishiToolStripMenuItem";
            this.zishiToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.zishiToolStripMenuItem.Text = "自适应内容高度";
            this.zishiToolStripMenuItem.Click += new System.EventHandler(this.zishiToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel1.Controls.Add(this.pnlAutoFit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 350);
            this.panel1.TabIndex = 1;
            // 
            // pnlAutoFit
            // 
            this.pnlAutoFit.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnlAutoFit.Controls.Add(this.prgAutoFit);
            this.pnlAutoFit.Controls.Add(this.lblAutoFitPercent);
            this.pnlAutoFit.Controls.Add(this.btnAutoFitCancel);
            this.pnlAutoFit.Location = new System.Drawing.Point(313, 93);
            this.pnlAutoFit.Name = "pnlAutoFit";
            this.pnlAutoFit.Size = new System.Drawing.Size(310, 110);
            this.pnlAutoFit.TabIndex = 0;
            this.pnlAutoFit.Visible = false;
            // 
            // prgAutoFit
            // 
            this.prgAutoFit.Location = new System.Drawing.Point(0, 40);
            this.prgAutoFit.Name = "prgAutoFit";
            this.prgAutoFit.Size = new System.Drawing.Size(310, 25);
            this.prgAutoFit.TabIndex = 2;
            // 
            // lblAutoFitPercent
            // 
            this.lblAutoFitPercent.BackColor = System.Drawing.Color.Transparent;
            this.lblAutoFitPercent.Location = new System.Drawing.Point(120, 14);
            this.lblAutoFitPercent.Name = "lblAutoFitPercent";
            this.lblAutoFitPercent.Size = new System.Drawing.Size(75, 12);
            this.lblAutoFitPercent.TabIndex = 1;
            this.lblAutoFitPercent.Text = "        ";
            this.lblAutoFitPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAutoFitCancel
            // 
            this.btnAutoFitCancel.Location = new System.Drawing.Point(120, 75);
            this.btnAutoFitCancel.Name = "btnAutoFitCancel";
            this.btnAutoFitCancel.Size = new System.Drawing.Size(75, 25);
            this.btnAutoFitCancel.TabIndex = 0;
            this.btnAutoFitCancel.Text = "取消";
            this.btnAutoFitCancel.UseVisualStyleBackColor = true;
            this.btnAutoFitCancel.Click += new System.EventHandler(this.btnAutoFitCancel_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditContentToolStripMenuItem,
            this.AttributesToolStripMenuItem,
            this.FrameToolStripMenuItem,
            this.AlignmentToolStripMenuItem,
            this.toolStripSeparator28,
            this.cmsmiColor,
            this.BGColorToolStripMenuItem,
            this.cmsmiGridColor,
            this.cmsmiBorderColor,
            this.toolStripSeparator15,
            this.SetEnterToolStripMenuItem,
            this.CancelEnterToolStripMenuItem,
            this.toolStripSeparator16,
            this.SetReadOnlyToolStripMenuItem,
            this.CancelReadonlyToolStripMenuItem,
            this.toolStripSeparator17});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 292);
            // 
            // EditContentToolStripMenuItem
            // 
            this.EditContentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCut,
            this.ToolStripMenuItemCopy,
            this.ToolStripMenuItemPaste,
            this.toolStripSeparator11,
            this.splitGridToolStripMenuItem,
            this.CancelMergeToolStripMenuItem,
            this.toolStripSeparator12,
            this.ToolStripMenuItemClear,
            this.toolStripSeparator13,
            this.InsertRowsToolStripMenuItem,
            this.InsertColumnsToolStripMenuItem,
            this.toolStripSeparator14,
            this.DeleteRowToolStripMenuItem,
            this.DeleteColumnToolStripMenuItem});
            this.EditContentToolStripMenuItem.Name = "EditContentToolStripMenuItem";
            this.EditContentToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.EditContentToolStripMenuItem.Text = "编辑";
            // 
            // ToolStripMenuItemCut
            // 
            this.ToolStripMenuItemCut.Name = "ToolStripMenuItemCut";
            this.ToolStripMenuItemCut.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItemCut.Text = "剪切";
            this.ToolStripMenuItemCut.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemCopy
            // 
            this.ToolStripMenuItemCopy.Name = "ToolStripMenuItemCopy";
            this.ToolStripMenuItemCopy.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItemCopy.Text = "复制";
            this.ToolStripMenuItemCopy.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemPaste
            // 
            this.ToolStripMenuItemPaste.Name = "ToolStripMenuItemPaste";
            this.ToolStripMenuItemPaste.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItemPaste.Text = "粘贴";
            this.ToolStripMenuItemPaste.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(151, 6);
            // 
            // splitGridToolStripMenuItem
            // 
            this.splitGridToolStripMenuItem.Name = "splitGridToolStripMenuItem";
            this.splitGridToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.splitGridToolStripMenuItem.Text = "合并单元格";
            this.splitGridToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
            // 
            // CancelMergeToolStripMenuItem
            // 
            this.CancelMergeToolStripMenuItem.Name = "CancelMergeToolStripMenuItem";
            this.CancelMergeToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.CancelMergeToolStripMenuItem.Text = "取消合并单元格";
            this.CancelMergeToolStripMenuItem.Click += new System.EventHandler(this.noMergeToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(151, 6);
            // 
            // ToolStripMenuItemClear
            // 
            this.ToolStripMenuItemClear.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemClearContent,
            this.ClearStyleToolStripMenuItem,
            this.ClearStyleToolStripMenuItem1});
            this.ToolStripMenuItemClear.Name = "ToolStripMenuItemClear";
            this.ToolStripMenuItemClear.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItemClear.Text = "清除";
            this.ToolStripMenuItemClear.Click += new System.EventHandler(this.clearContentToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemClearContent
            // 
            this.ToolStripMenuItemClearContent.Name = "ToolStripMenuItemClearContent";
            this.ToolStripMenuItemClearContent.Size = new System.Drawing.Size(118, 22);
            this.ToolStripMenuItemClearContent.Text = "清除内容";
            // 
            // ClearStyleToolStripMenuItem
            // 
            this.ClearStyleToolStripMenuItem.Name = "ClearStyleToolStripMenuItem";
            this.ClearStyleToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.ClearStyleToolStripMenuItem.Text = "清除格式";
            this.ClearStyleToolStripMenuItem.Click += new System.EventHandler(this.clearFormatToolStripMenuItem_Click);
            // 
            // ClearStyleToolStripMenuItem1
            // 
            this.ClearStyleToolStripMenuItem1.Name = "ClearStyleToolStripMenuItem1";
            this.ClearStyleToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.ClearStyleToolStripMenuItem1.Text = "清除全部";
            this.ClearStyleToolStripMenuItem1.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(151, 6);
            // 
            // InsertRowsToolStripMenuItem
            // 
            this.InsertRowsToolStripMenuItem.Name = "InsertRowsToolStripMenuItem";
            this.InsertRowsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.InsertRowsToolStripMenuItem.Text = "插入行";
            this.InsertRowsToolStripMenuItem.Click += new System.EventHandler(this.insertRowToolStripMenuItem_Click);
            // 
            // InsertColumnsToolStripMenuItem
            // 
            this.InsertColumnsToolStripMenuItem.Name = "InsertColumnsToolStripMenuItem";
            this.InsertColumnsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.InsertColumnsToolStripMenuItem.Text = "插入列";
            this.InsertColumnsToolStripMenuItem.Click += new System.EventHandler(this.insertColumnToolStripMenuItem_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(151, 6);
            // 
            // DeleteRowToolStripMenuItem
            // 
            this.DeleteRowToolStripMenuItem.Name = "DeleteRowToolStripMenuItem";
            this.DeleteRowToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.DeleteRowToolStripMenuItem.Text = "删除行";
            this.DeleteRowToolStripMenuItem.Click += new System.EventHandler(this.removeRowToolStripMenuItem_Click);
            // 
            // DeleteColumnToolStripMenuItem
            // 
            this.DeleteColumnToolStripMenuItem.Name = "DeleteColumnToolStripMenuItem";
            this.DeleteColumnToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.DeleteColumnToolStripMenuItem.Text = "删除列";
            this.DeleteColumnToolStripMenuItem.Click += new System.EventHandler(this.removeColumnToolStripMenuItem_Click);
            // 
            // AttributesToolStripMenuItem
            // 
            this.AttributesToolStripMenuItem.Name = "AttributesToolStripMenuItem";
            this.AttributesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.AttributesToolStripMenuItem.Text = "字体";
            this.AttributesToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem1_Click);
            // 
            // FrameToolStripMenuItem
            // 
            this.FrameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NoFrameToolStripMenuItem,
            this.ToolStripMenuItemLeftFrame,
            this.RightFrameToolStripMenuItem,
            this.UpperBorderToolStripMenuItem,
            this.LowerBorderToolStripMenuItem,
            this.toolStripSeparator26,
            this.ToolStripMenuItemForwardSlash,
            this.BackslashToolStripMenuItem,
            this.toolStripSeparator27,
            this.InnerBorderToolStripMenuItem,
            this.FineBorderToolStripMenuItem,
            this.ThickBorderToolStripMenuItem,
            this.AllBorderToolStripMenuItem});
            this.FrameToolStripMenuItem.Name = "FrameToolStripMenuItem";
            this.FrameToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.FrameToolStripMenuItem.Text = "边框";
            // 
            // NoFrameToolStripMenuItem
            // 
            this.NoFrameToolStripMenuItem.Name = "NoFrameToolStripMenuItem";
            this.NoFrameToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.NoFrameToolStripMenuItem.Text = "无边框";
            this.NoFrameToolStripMenuItem.Click += new System.EventHandler(this.borderNoneToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemLeftFrame
            // 
            this.ToolStripMenuItemLeftFrame.Name = "ToolStripMenuItemLeftFrame";
            this.ToolStripMenuItemLeftFrame.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItemLeftFrame.Text = "左边框";
            this.ToolStripMenuItemLeftFrame.Click += new System.EventHandler(this.borderLeftToolStripMenuItem_Click);
            // 
            // RightFrameToolStripMenuItem
            // 
            this.RightFrameToolStripMenuItem.Name = "RightFrameToolStripMenuItem";
            this.RightFrameToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.RightFrameToolStripMenuItem.Text = "右边框";
            this.RightFrameToolStripMenuItem.Click += new System.EventHandler(this.borderRightToolStripMenuItem_Click);
            // 
            // UpperBorderToolStripMenuItem
            // 
            this.UpperBorderToolStripMenuItem.Name = "UpperBorderToolStripMenuItem";
            this.UpperBorderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.UpperBorderToolStripMenuItem.Text = "上边框";
            this.UpperBorderToolStripMenuItem.Click += new System.EventHandler(this.borderTopToolStripMenuItem_Click);
            // 
            // LowerBorderToolStripMenuItem
            // 
            this.LowerBorderToolStripMenuItem.Name = "LowerBorderToolStripMenuItem";
            this.LowerBorderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.LowerBorderToolStripMenuItem.Text = "下边框";
            this.LowerBorderToolStripMenuItem.Click += new System.EventHandler(this.borderBottomToolStripMenuItem_Click);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(151, 6);
            // 
            // ToolStripMenuItemForwardSlash
            // 
            this.ToolStripMenuItemForwardSlash.Name = "ToolStripMenuItemForwardSlash";
            this.ToolStripMenuItemForwardSlash.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItemForwardSlash.Text = "正斜线";
            this.ToolStripMenuItemForwardSlash.Click += new System.EventHandler(this.borderLineUpToolStripMenuItem_Click);
            // 
            // BackslashToolStripMenuItem
            // 
            this.BackslashToolStripMenuItem.Name = "BackslashToolStripMenuItem";
            this.BackslashToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.BackslashToolStripMenuItem.Text = "反斜线";
            this.BackslashToolStripMenuItem.Click += new System.EventHandler(this.borderLineDownToolStripMenuItem_Click);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(151, 6);
            // 
            // InnerBorderToolStripMenuItem
            // 
            this.InnerBorderToolStripMenuItem.Name = "InnerBorderToolStripMenuItem";
            this.InnerBorderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.InnerBorderToolStripMenuItem.Text = "内部框线";
            this.InnerBorderToolStripMenuItem.Click += new System.EventHandler(this.borderInsideToolStripMenuItem_Click);
            // 
            // FineBorderToolStripMenuItem
            // 
            this.FineBorderToolStripMenuItem.Name = "FineBorderToolStripMenuItem";
            this.FineBorderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.FineBorderToolStripMenuItem.Text = "外部框线（细）";
            this.FineBorderToolStripMenuItem.Click += new System.EventHandler(this.borderOutsideThinToolStripMenuItem_Click);
            // 
            // ThickBorderToolStripMenuItem
            // 
            this.ThickBorderToolStripMenuItem.Name = "ThickBorderToolStripMenuItem";
            this.ThickBorderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.ThickBorderToolStripMenuItem.Text = "外部框线（粗）";
            this.ThickBorderToolStripMenuItem.Click += new System.EventHandler(this.borderOutsideStickToolStripMenuItem_Click);
            // 
            // AllBorderToolStripMenuItem
            // 
            this.AllBorderToolStripMenuItem.Name = "AllBorderToolStripMenuItem";
            this.AllBorderToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.AllBorderToolStripMenuItem.Text = "全部框线";
            this.AllBorderToolStripMenuItem.Click += new System.EventHandler(this.borderAllToolStripMenuItem1_Click);
            // 
            // AlignmentToolStripMenuItem
            // 
            this.AlignmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConventionalToolStripMenuItem,
            this.TopToolStripMenuItem,
            this.ToolStripMenuItemCenter,
            this.BottomToolStripMenuItem,
            this.toolStripSeparator23,
            this.ToolStripMenuItemLeft,
            this.TopLeftToolStripMenuItem,
            this.ToolStripMenuItemCenterLeft,
            this.BottomLeftToolStripMenuItem,
            this.toolStripSeparator24,
            this.ToolStripMenuItemCen,
            this.TopCenterToolStripMenuItem,
            this.ToolStripMenuItemAtCenter,
            this.BottomCenterToolStripMenuItem,
            this.toolStripSeparator25,
            this.RightToolStripMenuItem,
            this.TopRightToolStripMenuItem,
            this.ToolStripMenuItemCenterRight,
            this.BottomRightToolStripMenuItem});
            this.AlignmentToolStripMenuItem.Name = "AlignmentToolStripMenuItem";
            this.AlignmentToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.AlignmentToolStripMenuItem.Text = "对齐方式";
            // 
            // ConventionalToolStripMenuItem
            // 
            this.ConventionalToolStripMenuItem.Name = "ConventionalToolStripMenuItem";
            this.ConventionalToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.ConventionalToolStripMenuItem.Text = "常规";
            this.ConventionalToolStripMenuItem.Click += new System.EventHandler(this.alignNormalToolStripMenuItem_Click);
            // 
            // TopToolStripMenuItem
            // 
            this.TopToolStripMenuItem.Name = "TopToolStripMenuItem";
            this.TopToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.TopToolStripMenuItem.Text = "顶部对齐";
            this.TopToolStripMenuItem.Click += new System.EventHandler(this.alignTopToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemCenter
            // 
            this.ToolStripMenuItemCenter.Name = "ToolStripMenuItemCenter";
            this.ToolStripMenuItemCenter.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemCenter.Text = "中间对齐";
            this.ToolStripMenuItemCenter.Click += new System.EventHandler(this.alignMiddleToolStripMenuItem_Click);
            // 
            // BottomToolStripMenuItem
            // 
            this.BottomToolStripMenuItem.Name = "BottomToolStripMenuItem";
            this.BottomToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.BottomToolStripMenuItem.Text = "底部对齐";
            this.BottomToolStripMenuItem.Click += new System.EventHandler(this.alignBottomToolStripMenuItem_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(127, 6);
            // 
            // ToolStripMenuItemLeft
            // 
            this.ToolStripMenuItemLeft.Name = "ToolStripMenuItemLeft";
            this.ToolStripMenuItemLeft.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemLeft.Text = "左对齐";
            this.ToolStripMenuItemLeft.Click += new System.EventHandler(this.alignLeftToolStripMenuItem_Click);
            // 
            // TopLeftToolStripMenuItem
            // 
            this.TopLeftToolStripMenuItem.Name = "TopLeftToolStripMenuItem";
            this.TopLeftToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.TopLeftToolStripMenuItem.Text = "顶部左对齐";
            this.TopLeftToolStripMenuItem.Click += new System.EventHandler(this.alignTopLeftToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemCenterLeft
            // 
            this.ToolStripMenuItemCenterLeft.Name = "ToolStripMenuItemCenterLeft";
            this.ToolStripMenuItemCenterLeft.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemCenterLeft.Text = "中间左对齐";
            this.ToolStripMenuItemCenterLeft.Click += new System.EventHandler(this.alignMiddleLeftToolStripMenuItem_Click);
            // 
            // BottomLeftToolStripMenuItem
            // 
            this.BottomLeftToolStripMenuItem.Name = "BottomLeftToolStripMenuItem";
            this.BottomLeftToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.BottomLeftToolStripMenuItem.Text = "底部左对齐";
            this.BottomLeftToolStripMenuItem.Click += new System.EventHandler(this.alignBottomLeftToolStripMenuItem_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(127, 6);
            // 
            // ToolStripMenuItemCen
            // 
            this.ToolStripMenuItemCen.Name = "ToolStripMenuItemCen";
            this.ToolStripMenuItemCen.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemCen.Text = "中对齐";
            this.ToolStripMenuItemCen.Click += new System.EventHandler(this.alignCenterToolStripMenuItem_Click);
            // 
            // TopCenterToolStripMenuItem
            // 
            this.TopCenterToolStripMenuItem.Name = "TopCenterToolStripMenuItem";
            this.TopCenterToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.TopCenterToolStripMenuItem.Text = "顶部中对齐";
            this.TopCenterToolStripMenuItem.Click += new System.EventHandler(this.alignTopCenterToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemAtCenter
            // 
            this.ToolStripMenuItemAtCenter.Name = "ToolStripMenuItemAtCenter";
            this.ToolStripMenuItemAtCenter.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemAtCenter.Text = "中心对齐";
            this.ToolStripMenuItemAtCenter.Click += new System.EventHandler(this.alignMiddleCenterToolStripMenuItem_Click);
            // 
            // BottomCenterToolStripMenuItem
            // 
            this.BottomCenterToolStripMenuItem.Name = "BottomCenterToolStripMenuItem";
            this.BottomCenterToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.BottomCenterToolStripMenuItem.Text = "底部中对齐";
            this.BottomCenterToolStripMenuItem.Click += new System.EventHandler(this.alignBottomCenterToolStripMenuItem_Click);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(127, 6);
            // 
            // RightToolStripMenuItem
            // 
            this.RightToolStripMenuItem.Name = "RightToolStripMenuItem";
            this.RightToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.RightToolStripMenuItem.Text = "右对齐";
            this.RightToolStripMenuItem.Click += new System.EventHandler(this.alignRightToolStripMenuItem_Click);
            // 
            // TopRightToolStripMenuItem
            // 
            this.TopRightToolStripMenuItem.Name = "TopRightToolStripMenuItem";
            this.TopRightToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.TopRightToolStripMenuItem.Text = "顶部右对齐";
            this.TopRightToolStripMenuItem.Click += new System.EventHandler(this.alignTopRightToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemCenterRight
            // 
            this.ToolStripMenuItemCenterRight.Name = "ToolStripMenuItemCenterRight";
            this.ToolStripMenuItemCenterRight.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemCenterRight.Text = "中间右对齐";
            this.ToolStripMenuItemCenterRight.Click += new System.EventHandler(this.alignMiddleRightToolStripMenuItem_Click);
            // 
            // BottomRightToolStripMenuItem
            // 
            this.BottomRightToolStripMenuItem.Name = "BottomRightToolStripMenuItem";
            this.BottomRightToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.BottomRightToolStripMenuItem.Text = "底部右对齐";
            this.BottomRightToolStripMenuItem.Click += new System.EventHandler(this.alignBottomRightToolStripMenuItem_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(151, 6);
            // 
            // cmsmiColor
            // 
            this.cmsmiColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsmiColorRed,
            this.cmsmiColorGreen,
            this.cmsmiColorYellow,
            this.cmsmiColorBlue,
            this.cmsmiColorPurple,
            this.cmsmiColorCyan,
            this.cmsmiColorWhite});
            this.cmsmiColor.Name = "cmsmiColor";
            this.cmsmiColor.Size = new System.Drawing.Size(154, 22);
            this.cmsmiColor.Text = "文字颜色";
            // 
            // cmsmiColorRed
            // 
            this.cmsmiColorRed.Name = "cmsmiColorRed";
            this.cmsmiColorRed.Size = new System.Drawing.Size(82, 22);
            this.cmsmiColorRed.Text = "红";
            this.cmsmiColorRed.Click += new System.EventHandler(this.msmiColorRed_Click);
            // 
            // cmsmiColorGreen
            // 
            this.cmsmiColorGreen.Name = "cmsmiColorGreen";
            this.cmsmiColorGreen.Size = new System.Drawing.Size(82, 22);
            this.cmsmiColorGreen.Text = "绿";
            this.cmsmiColorGreen.Click += new System.EventHandler(this.msmiColorGreen_Click);
            // 
            // cmsmiColorYellow
            // 
            this.cmsmiColorYellow.Name = "cmsmiColorYellow";
            this.cmsmiColorYellow.Size = new System.Drawing.Size(82, 22);
            this.cmsmiColorYellow.Text = "黄";
            this.cmsmiColorYellow.Click += new System.EventHandler(this.msmiColorYellow_Click);
            // 
            // cmsmiColorBlue
            // 
            this.cmsmiColorBlue.Name = "cmsmiColorBlue";
            this.cmsmiColorBlue.Size = new System.Drawing.Size(82, 22);
            this.cmsmiColorBlue.Text = "蓝";
            this.cmsmiColorBlue.Click += new System.EventHandler(this.msmiColorBlue_Click);
            // 
            // cmsmiColorPurple
            // 
            this.cmsmiColorPurple.Name = "cmsmiColorPurple";
            this.cmsmiColorPurple.Size = new System.Drawing.Size(82, 22);
            this.cmsmiColorPurple.Text = "紫";
            this.cmsmiColorPurple.Click += new System.EventHandler(this.msmiColorPurple_Click);
            // 
            // cmsmiColorCyan
            // 
            this.cmsmiColorCyan.Name = "cmsmiColorCyan";
            this.cmsmiColorCyan.Size = new System.Drawing.Size(82, 22);
            this.cmsmiColorCyan.Text = "青";
            this.cmsmiColorCyan.Click += new System.EventHandler(this.msmiColorCyan_Click);
            // 
            // cmsmiColorWhite
            // 
            this.cmsmiColorWhite.Name = "cmsmiColorWhite";
            this.cmsmiColorWhite.Size = new System.Drawing.Size(82, 22);
            this.cmsmiColorWhite.Text = "白";
            this.cmsmiColorWhite.Click += new System.EventHandler(this.msmiColorWhite_Click);
            // 
            // BGColorToolStripMenuItem
            // 
            this.BGColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RedToolStripMenuItem1,
            this.GreenToolStripMenuItem1,
            this.YellowToolStripMenuItem1,
            this.BlackToolStripMenuItem});
            this.BGColorToolStripMenuItem.Name = "BGColorToolStripMenuItem";
            this.BGColorToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.BGColorToolStripMenuItem.Text = "背景颜色";
            // 
            // RedToolStripMenuItem1
            // 
            this.RedToolStripMenuItem1.Name = "RedToolStripMenuItem1";
            this.RedToolStripMenuItem1.Size = new System.Drawing.Size(82, 22);
            this.RedToolStripMenuItem1.Text = "红";
            this.RedToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_RedToolStripMenuItem1_Click);
            // 
            // GreenToolStripMenuItem1
            // 
            this.GreenToolStripMenuItem1.Name = "GreenToolStripMenuItem1";
            this.GreenToolStripMenuItem1.Size = new System.Drawing.Size(82, 22);
            this.GreenToolStripMenuItem1.Text = "绿";
            this.GreenToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_GreenToolStripMenuItem1_Click);
            // 
            // YellowToolStripMenuItem1
            // 
            this.YellowToolStripMenuItem1.Name = "YellowToolStripMenuItem1";
            this.YellowToolStripMenuItem1.Size = new System.Drawing.Size(82, 22);
            this.YellowToolStripMenuItem1.Text = "黄";
            this.YellowToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_YellowToolStripMenuItem1_Click);
            // 
            // BlackToolStripMenuItem
            // 
            this.BlackToolStripMenuItem.Name = "BlackToolStripMenuItem";
            this.BlackToolStripMenuItem.Size = new System.Drawing.Size(82, 22);
            this.BlackToolStripMenuItem.Text = "黑";
            this.BlackToolStripMenuItem.Click += new System.EventHandler(this.backcolor_BlackToolStripMenuItem_Click);
            // 
            // cmsmiGridColor
            // 
            this.cmsmiGridColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsmiGridColorRed,
            this.cmsmiGridColorGreen,
            this.cmsmiGridColorYellow,
            this.cmsmiGridColorBlack,
            this.cmsmiGridColorBlue,
            this.cmsmiGridColorPurple,
            this.cmsmiGridColorCyan,
            this.cmsmiGridColorWhite});
            this.cmsmiGridColor.Name = "cmsmiGridColor";
            this.cmsmiGridColor.Size = new System.Drawing.Size(154, 22);
            this.cmsmiGridColor.Text = "表格颜色";
            // 
            // cmsmiGridColorRed
            // 
            this.cmsmiGridColorRed.Name = "cmsmiGridColorRed";
            this.cmsmiGridColorRed.Size = new System.Drawing.Size(82, 22);
            this.cmsmiGridColorRed.Text = "红";
            this.cmsmiGridColorRed.Click += new System.EventHandler(this.msmiGridColorRed_Click);
            // 
            // cmsmiGridColorGreen
            // 
            this.cmsmiGridColorGreen.Name = "cmsmiGridColorGreen";
            this.cmsmiGridColorGreen.Size = new System.Drawing.Size(82, 22);
            this.cmsmiGridColorGreen.Text = "绿";
            this.cmsmiGridColorGreen.Click += new System.EventHandler(this.msmiGridColorGreen_Click);
            // 
            // cmsmiGridColorYellow
            // 
            this.cmsmiGridColorYellow.Name = "cmsmiGridColorYellow";
            this.cmsmiGridColorYellow.Size = new System.Drawing.Size(82, 22);
            this.cmsmiGridColorYellow.Text = "黄";
            this.cmsmiGridColorYellow.Click += new System.EventHandler(this.msmiGridColorYellow_Click);
            // 
            // cmsmiGridColorBlack
            // 
            this.cmsmiGridColorBlack.Name = "cmsmiGridColorBlack";
            this.cmsmiGridColorBlack.Size = new System.Drawing.Size(82, 22);
            this.cmsmiGridColorBlack.Text = "黑";
            this.cmsmiGridColorBlack.Click += new System.EventHandler(this.msmiGridColorBlack_Click);
            // 
            // cmsmiGridColorBlue
            // 
            this.cmsmiGridColorBlue.Name = "cmsmiGridColorBlue";
            this.cmsmiGridColorBlue.Size = new System.Drawing.Size(82, 22);
            this.cmsmiGridColorBlue.Text = "蓝";
            this.cmsmiGridColorBlue.Click += new System.EventHandler(this.msmiGridColorBlue_Click);
            // 
            // cmsmiGridColorPurple
            // 
            this.cmsmiGridColorPurple.Name = "cmsmiGridColorPurple";
            this.cmsmiGridColorPurple.Size = new System.Drawing.Size(82, 22);
            this.cmsmiGridColorPurple.Text = "紫";
            this.cmsmiGridColorPurple.Click += new System.EventHandler(this.msmiGridColorPurple_Click);
            // 
            // cmsmiGridColorCyan
            // 
            this.cmsmiGridColorCyan.Name = "cmsmiGridColorCyan";
            this.cmsmiGridColorCyan.Size = new System.Drawing.Size(82, 22);
            this.cmsmiGridColorCyan.Text = "青";
            this.cmsmiGridColorCyan.Click += new System.EventHandler(this.msmiGridColorCyan_Click);
            // 
            // cmsmiGridColorWhite
            // 
            this.cmsmiGridColorWhite.Name = "cmsmiGridColorWhite";
            this.cmsmiGridColorWhite.Size = new System.Drawing.Size(82, 22);
            this.cmsmiGridColorWhite.Text = "白";
            this.cmsmiGridColorWhite.Click += new System.EventHandler(this.msmiGridColorWhite_Click);
            // 
            // cmsmiBorderColor
            // 
            this.cmsmiBorderColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsmiBorderColorRed,
            this.cmsmiBorderColorGreen,
            this.cmsmiBorderColorYellow,
            this.cmsmiBorderColorBlack,
            this.cmsmiBorderColorBlue,
            this.cmsmiBorderColorPurple,
            this.cmsmiBorderColorCyan,
            this.cmsmiBorderColorWhite});
            this.cmsmiBorderColor.Name = "cmsmiBorderColor";
            this.cmsmiBorderColor.Size = new System.Drawing.Size(154, 22);
            this.cmsmiBorderColor.Text = "边框颜色";
            // 
            // cmsmiBorderColorRed
            // 
            this.cmsmiBorderColorRed.Name = "cmsmiBorderColorRed";
            this.cmsmiBorderColorRed.Size = new System.Drawing.Size(82, 22);
            this.cmsmiBorderColorRed.Text = "红";
            this.cmsmiBorderColorRed.Click += new System.EventHandler(this.msmiBorderColorRed_Click);
            // 
            // cmsmiBorderColorGreen
            // 
            this.cmsmiBorderColorGreen.Name = "cmsmiBorderColorGreen";
            this.cmsmiBorderColorGreen.Size = new System.Drawing.Size(82, 22);
            this.cmsmiBorderColorGreen.Text = "绿";
            this.cmsmiBorderColorGreen.Click += new System.EventHandler(this.msmiBorderColorGreen_Click);
            // 
            // cmsmiBorderColorYellow
            // 
            this.cmsmiBorderColorYellow.Name = "cmsmiBorderColorYellow";
            this.cmsmiBorderColorYellow.Size = new System.Drawing.Size(82, 22);
            this.cmsmiBorderColorYellow.Text = "黄";
            this.cmsmiBorderColorYellow.Click += new System.EventHandler(this.msmiBorderColorYellow_Click);
            // 
            // cmsmiBorderColorBlack
            // 
            this.cmsmiBorderColorBlack.Name = "cmsmiBorderColorBlack";
            this.cmsmiBorderColorBlack.Size = new System.Drawing.Size(82, 22);
            this.cmsmiBorderColorBlack.Text = "黑";
            this.cmsmiBorderColorBlack.Click += new System.EventHandler(this.msmiBorderColorBlack_Click);
            // 
            // cmsmiBorderColorBlue
            // 
            this.cmsmiBorderColorBlue.Name = "cmsmiBorderColorBlue";
            this.cmsmiBorderColorBlue.Size = new System.Drawing.Size(82, 22);
            this.cmsmiBorderColorBlue.Text = "蓝";
            this.cmsmiBorderColorBlue.Click += new System.EventHandler(this.msmiBorderColorBlue_Click);
            // 
            // cmsmiBorderColorPurple
            // 
            this.cmsmiBorderColorPurple.Name = "cmsmiBorderColorPurple";
            this.cmsmiBorderColorPurple.Size = new System.Drawing.Size(82, 22);
            this.cmsmiBorderColorPurple.Text = "紫";
            this.cmsmiBorderColorPurple.Click += new System.EventHandler(this.msmiBorderColorPurple_Click);
            // 
            // cmsmiBorderColorCyan
            // 
            this.cmsmiBorderColorCyan.Name = "cmsmiBorderColorCyan";
            this.cmsmiBorderColorCyan.Size = new System.Drawing.Size(82, 22);
            this.cmsmiBorderColorCyan.Text = "青";
            this.cmsmiBorderColorCyan.Click += new System.EventHandler(this.msmiBorderColorCyan_Click);
            // 
            // cmsmiBorderColorWhite
            // 
            this.cmsmiBorderColorWhite.Name = "cmsmiBorderColorWhite";
            this.cmsmiBorderColorWhite.Size = new System.Drawing.Size(82, 22);
            this.cmsmiBorderColorWhite.Text = "白";
            this.cmsmiBorderColorWhite.Click += new System.EventHandler(this.msmiBorderColorWhite_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(151, 6);
            // 
            // SetEnterToolStripMenuItem
            // 
            this.SetEnterToolStripMenuItem.Name = "SetEnterToolStripMenuItem";
            this.SetEnterToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.SetEnterToolStripMenuItem.Text = "设置自动换行";
            this.SetEnterToolStripMenuItem.Click += new System.EventHandler(this.wrapTextToolStripMenuItem_Click);
            // 
            // CancelEnterToolStripMenuItem
            // 
            this.CancelEnterToolStripMenuItem.Name = "CancelEnterToolStripMenuItem";
            this.CancelEnterToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.CancelEnterToolStripMenuItem.Text = "取消自动换行";
            this.CancelEnterToolStripMenuItem.Click += new System.EventHandler(this.nowrapTextToolStripMenuItem_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(151, 6);
            // 
            // SetReadOnlyToolStripMenuItem
            // 
            this.SetReadOnlyToolStripMenuItem.Name = "SetReadOnlyToolStripMenuItem";
            this.SetReadOnlyToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.SetReadOnlyToolStripMenuItem.Text = "设置单元格只读";
            this.SetReadOnlyToolStripMenuItem.Click += new System.EventHandler(this.readonlyToolStripMenuItem_Click);
            // 
            // CancelReadonlyToolStripMenuItem
            // 
            this.CancelReadonlyToolStripMenuItem.Name = "CancelReadonlyToolStripMenuItem";
            this.CancelReadonlyToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.CancelReadonlyToolStripMenuItem.Text = "取消单元格只读";
            this.CancelReadonlyToolStripMenuItem.Click += new System.EventHandler(this.noreadonlyToolStripMenuItem_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(151, 6);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.button_New,
            this.button_Open,
            this.button_Save,
            this.toolStripSeparator18,
            this.button_boardStyle,
            this.button_Insert_Row,
            this.button_insert_Col,
            this.button_delete_Row,
            this.button_delete_Col,
            this.toolStripSeparator19,
            this.button_combin_Cell,
            this.button_split_Cell,
            this.toolStripSeparator20});
            this.toolStrip1.Location = new System.Drawing.Point(3, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(235, 25);
            this.toolStrip1.TabIndex = 22;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // button_New
            // 
            this.button_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_New.Name = "button_New";
            this.button_New.Size = new System.Drawing.Size(23, 22);
            this.button_New.Text = "toolStripButton3";
            this.button_New.ToolTipText = "新建";
            this.button_New.Click += new System.EventHandler(this.button_New_Click);
            // 
            // button_Open
            // 
            this.button_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(23, 22);
            this.button_Open.Text = "toolStripButton2";
            this.button_Open.ToolTipText = "打开";
            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
            // 
            // button_Save
            // 
            this.button_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(23, 22);
            this.button_Save.Text = "toolStripButton1";
            this.button_Save.ToolTipText = "保存";
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
            // 
            // button_boardStyle
            // 
            this.button_boardStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_boardStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_boardStyle.Name = "button_boardStyle";
            this.button_boardStyle.Size = new System.Drawing.Size(23, 22);
            this.button_boardStyle.Text = "toolStripButton4";
            this.button_boardStyle.Visible = false;
            // 
            // button_Insert_Row
            // 
            this.button_Insert_Row.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_Insert_Row.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_Insert_Row.Name = "button_Insert_Row";
            this.button_Insert_Row.Size = new System.Drawing.Size(23, 22);
            this.button_Insert_Row.Text = "toolStripButton5";
            this.button_Insert_Row.ToolTipText = "插入行";
            this.button_Insert_Row.Click += new System.EventHandler(this.button_Insert_Row_Click);
            // 
            // button_insert_Col
            // 
            this.button_insert_Col.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_insert_Col.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_insert_Col.Name = "button_insert_Col";
            this.button_insert_Col.Size = new System.Drawing.Size(23, 22);
            this.button_insert_Col.Text = "toolStripButton6";
            this.button_insert_Col.ToolTipText = "插入列";
            this.button_insert_Col.Click += new System.EventHandler(this.button_insert_Col_Click);
            // 
            // button_delete_Row
            // 
            this.button_delete_Row.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_delete_Row.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_delete_Row.Name = "button_delete_Row";
            this.button_delete_Row.Size = new System.Drawing.Size(23, 22);
            this.button_delete_Row.Text = "toolStripButton7";
            this.button_delete_Row.ToolTipText = "删除行";
            this.button_delete_Row.Click += new System.EventHandler(this.button_delete_Row_Click);
            // 
            // button_delete_Col
            // 
            this.button_delete_Col.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_delete_Col.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_delete_Col.Name = "button_delete_Col";
            this.button_delete_Col.Size = new System.Drawing.Size(23, 22);
            this.button_delete_Col.Text = "toolStripButton8";
            this.button_delete_Col.ToolTipText = "删除列";
            this.button_delete_Col.Click += new System.EventHandler(this.button_delete_Col_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 25);
            // 
            // button_combin_Cell
            // 
            this.button_combin_Cell.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_combin_Cell.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_combin_Cell.Name = "button_combin_Cell";
            this.button_combin_Cell.Size = new System.Drawing.Size(23, 22);
            this.button_combin_Cell.Text = "toolStripButton9";
            this.button_combin_Cell.ToolTipText = "合并单元格";
            this.button_combin_Cell.Click += new System.EventHandler(this.button_combin_Cell_Click);
            // 
            // button_split_Cell
            // 
            this.button_split_Cell.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_split_Cell.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_split_Cell.Name = "button_split_Cell";
            this.button_split_Cell.Size = new System.Drawing.Size(23, 22);
            this.button_split_Cell.Text = "toolStripButton10";
            this.button_split_Cell.ToolTipText = "拆分单元格";
            this.button_split_Cell.Click += new System.EventHandler(this.button_split_Cell_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox_Font,
            this.tscmbFontSize,
            this.toolStripLabel_Blod,
            this.toolStripLabel_Italic,
            this.toolStripLabel_Underline,
            this.button_wraptext,
            this.toolStripButton1,
            this.tssbtnColor,
            this.toolStripSplitButton2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 51);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(360, 25);
            this.toolStrip2.TabIndex = 23;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripComboBox_Font
            // 
            this.toolStripComboBox_Font.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox_Font.Name = "toolStripComboBox_Font";
            this.toolStripComboBox_Font.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox_Font.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_Font_SelectedIndexChanged);
            // 
            // tscmbFontSize
            // 
            this.tscmbFontSize.Items.AddRange(new object[] {
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
            "200"});
            this.tscmbFontSize.Name = "tscmbFontSize";
            this.tscmbFontSize.Size = new System.Drawing.Size(75, 25);
            this.tscmbFontSize.SelectedIndexChanged += new System.EventHandler(this.tscmbFontSize_SelectedIndexChanged);
            this.tscmbFontSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tscmbFontSize_KeyPress);
            this.tscmbFontSize.TextChanged += new System.EventHandler(this.tscmbFontSize_TextChanged);
            // 
            // toolStripLabel_Blod
            // 
            this.toolStripLabel_Blod.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripLabel_Blod.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel_Blod.Name = "toolStripLabel_Blod";
            this.toolStripLabel_Blod.Size = new System.Drawing.Size(26, 22);
            this.toolStripLabel_Blod.Text = " B ";
            this.toolStripLabel_Blod.ToolTipText = "粗体";
            this.toolStripLabel_Blod.Click += new System.EventHandler(this.toolStripLabel_Blod_Click);
            // 
            // toolStripLabel_Italic
            // 
            this.toolStripLabel_Italic.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic);
            this.toolStripLabel_Italic.Name = "toolStripLabel_Italic";
            this.toolStripLabel_Italic.Size = new System.Drawing.Size(23, 22);
            this.toolStripLabel_Italic.Text = " I ";
            this.toolStripLabel_Italic.ToolTipText = "斜体";
            this.toolStripLabel_Italic.Click += new System.EventHandler(this.toolStripLabel_Italic_Click);
            // 
            // toolStripLabel_Underline
            // 
            this.toolStripLabel_Underline.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline);
            this.toolStripLabel_Underline.Name = "toolStripLabel_Underline";
            this.toolStripLabel_Underline.Size = new System.Drawing.Size(23, 22);
            this.toolStripLabel_Underline.Text = " U ";
            this.toolStripLabel_Underline.ToolTipText = "下划线";
            this.toolStripLabel_Underline.Click += new System.EventHandler(this.toolStripLabel_Underline_Click);
            // 
            // button_wraptext
            // 
            this.button_wraptext.CheckOnClick = true;
            this.button_wraptext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button_wraptext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_wraptext.Name = "button_wraptext";
            this.button_wraptext.Size = new System.Drawing.Size(23, 22);
            this.button_wraptext.Text = "toolStripButton2";
            this.button_wraptext.ToolTipText = "自动换行";
            this.button_wraptext.Click += new System.EventHandler(this.button_wraptext_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "对齐方式";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tssbtnColor
            // 
            this.tssbtnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbtnColor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiColorRed,
            this.tsmiColorGreen,
            this.tsmiColorYellow,
            this.tsmiColorBlack,
            this.tsmiColorBlue,
            this.tsmiColorPurple,
            this.tsmiColorCyan,
            this.tsmiColorWhite});
            this.tssbtnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbtnColor.Name = "tssbtnColor";
            this.tssbtnColor.Size = new System.Drawing.Size(16, 22);
            this.tssbtnColor.Text = "Color";
            this.tssbtnColor.ToolTipText = "字体颜色";
            // 
            // tsmiColorRed
            // 
            this.tsmiColorRed.BackColor = System.Drawing.Color.Red;
            this.tsmiColorRed.Name = "tsmiColorRed";
            this.tsmiColorRed.Size = new System.Drawing.Size(118, 22);
            this.tsmiColorRed.Text = "    ";
            this.tsmiColorRed.Click += new System.EventHandler(this.tsmiColorRed_Click);
            // 
            // tsmiColorGreen
            // 
            this.tsmiColorGreen.BackColor = System.Drawing.Color.Green;
            this.tsmiColorGreen.Name = "tsmiColorGreen";
            this.tsmiColorGreen.Size = new System.Drawing.Size(118, 22);
            this.tsmiColorGreen.Text = "    ";
            this.tsmiColorGreen.Click += new System.EventHandler(this.tsmiColorGreen_Click);
            // 
            // tsmiColorYellow
            // 
            this.tsmiColorYellow.BackColor = System.Drawing.Color.Yellow;
            this.tsmiColorYellow.Name = "tsmiColorYellow";
            this.tsmiColorYellow.Size = new System.Drawing.Size(118, 22);
            this.tsmiColorYellow.Text = "      ";
            this.tsmiColorYellow.Click += new System.EventHandler(this.tsmiColorYellow_Click);
            // 
            // tsmiColorBlack
            // 
            this.tsmiColorBlack.BackColor = System.Drawing.Color.Black;
            this.tsmiColorBlack.Name = "tsmiColorBlack";
            this.tsmiColorBlack.Size = new System.Drawing.Size(118, 22);
            this.tsmiColorBlack.Text = "        ";
            this.tsmiColorBlack.Click += new System.EventHandler(this.tsmiColorBlack_Click);
            // 
            // tsmiColorBlue
            // 
            this.tsmiColorBlue.BackColor = System.Drawing.Color.Blue;
            this.tsmiColorBlue.Name = "tsmiColorBlue";
            this.tsmiColorBlue.Size = new System.Drawing.Size(118, 22);
            this.tsmiColorBlue.Text = "        ";
            this.tsmiColorBlue.Click += new System.EventHandler(this.tsmiColorBlue_Click);
            // 
            // tsmiColorPurple
            // 
            this.tsmiColorPurple.BackColor = System.Drawing.Color.Purple;
            this.tsmiColorPurple.Name = "tsmiColorPurple";
            this.tsmiColorPurple.Size = new System.Drawing.Size(118, 22);
            this.tsmiColorPurple.Text = "        ";
            this.tsmiColorPurple.Click += new System.EventHandler(this.tsmiColorPurple_Click);
            // 
            // tsmiColorCyan
            // 
            this.tsmiColorCyan.BackColor = System.Drawing.Color.Cyan;
            this.tsmiColorCyan.Name = "tsmiColorCyan";
            this.tsmiColorCyan.Size = new System.Drawing.Size(118, 22);
            this.tsmiColorCyan.Text = "        ";
            this.tsmiColorCyan.Click += new System.EventHandler(this.tsmiColorCyan_Click);
            // 
            // tsmiColorWhite
            // 
            this.tsmiColorWhite.BackColor = System.Drawing.Color.White;
            this.tsmiColorWhite.Name = "tsmiColorWhite";
            this.tsmiColorWhite.Size = new System.Drawing.Size(118, 22);
            this.tsmiColorWhite.Text = "        ";
            this.tsmiColorWhite.Click += new System.EventHandler(this.tsmiColorWhite_Click);
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Backcolor_Red,
            this.toolStripMenuItem_Backcolor_Green,
            this.toolStripMenuItem_Backcolor_Yellow,
            this.toolStripMenuItem_Backcolor_Black});
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(16, 22);
            this.toolStripSplitButton2.Text = "toolStripSplitButton2";
            this.toolStripSplitButton2.ToolTipText = "背景颜色";
            // 
            // toolStripMenuItem_Backcolor_Red
            // 
            this.toolStripMenuItem_Backcolor_Red.BackColor = System.Drawing.Color.Red;
            this.toolStripMenuItem_Backcolor_Red.Name = "toolStripMenuItem_Backcolor_Red";
            this.toolStripMenuItem_Backcolor_Red.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem_Backcolor_Red.Text = "    ";
            this.toolStripMenuItem_Backcolor_Red.Click += new System.EventHandler(this.toolStripMenuItem_Backcolor_Red_Click);
            // 
            // toolStripMenuItem_Backcolor_Green
            // 
            this.toolStripMenuItem_Backcolor_Green.BackColor = System.Drawing.Color.Green;
            this.toolStripMenuItem_Backcolor_Green.Name = "toolStripMenuItem_Backcolor_Green";
            this.toolStripMenuItem_Backcolor_Green.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem_Backcolor_Green.Text = "    ";
            this.toolStripMenuItem_Backcolor_Green.Click += new System.EventHandler(this.toolStripMenuItem_Backcolor_Green_Click);
            // 
            // toolStripMenuItem_Backcolor_Yellow
            // 
            this.toolStripMenuItem_Backcolor_Yellow.BackColor = System.Drawing.Color.Yellow;
            this.toolStripMenuItem_Backcolor_Yellow.Name = "toolStripMenuItem_Backcolor_Yellow";
            this.toolStripMenuItem_Backcolor_Yellow.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem_Backcolor_Yellow.Text = "    ";
            this.toolStripMenuItem_Backcolor_Yellow.Click += new System.EventHandler(this.toolStripMenuItem_Backcolor_Yellow_Click);
            // 
            // toolStripMenuItem_Backcolor_Black
            // 
            this.toolStripMenuItem_Backcolor_Black.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem_Backcolor_Black.Name = "toolStripMenuItem_Backcolor_Black";
            this.toolStripMenuItem_Backcolor_Black.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem_Backcolor_Black.Text = "    ";
            this.toolStripMenuItem_Backcolor_Black.Click += new System.EventHandler(this.toolStripMenuItem_Backcolor_Black_Click);
            // 
            // panel_Alignt
            // 
            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_RightButtom);
            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_MiddleButtom);
            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_RightMiddle);
            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_MiddleMiddle);
            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_RightTop);
            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_MiddleTop);
            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_LeftButtom);
            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_LeftMiddle);
            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_LeftTop);
            this.panel_Alignt.Location = new System.Drawing.Point(254, 77);
            this.panel_Alignt.Name = "panel_Alignt";
            this.panel_Alignt.Size = new System.Drawing.Size(106, 106);
            this.panel_Alignt.TabIndex = 24;
            this.panel_Alignt.Visible = false;
            // 
            // pictureBox_Aligent_RightButtom
            // 
            this.pictureBox_Aligent_RightButtom.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_Aligent_RightButtom.Location = new System.Drawing.Point(71, 70);
            this.pictureBox_Aligent_RightButtom.Name = "pictureBox_Aligent_RightButtom";
            this.pictureBox_Aligent_RightButtom.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Aligent_RightButtom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Aligent_RightButtom.TabIndex = 8;
            this.pictureBox_Aligent_RightButtom.TabStop = false;
            this.pictureBox_Aligent_RightButtom.Click += new System.EventHandler(this.pictureBox_Aligent_RightButtom_Click);
            // 
            // pictureBox_Aligent_MiddleButtom
            // 
            this.pictureBox_Aligent_MiddleButtom.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_Aligent_MiddleButtom.Location = new System.Drawing.Point(37, 70);
            this.pictureBox_Aligent_MiddleButtom.Name = "pictureBox_Aligent_MiddleButtom";
            this.pictureBox_Aligent_MiddleButtom.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Aligent_MiddleButtom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Aligent_MiddleButtom.TabIndex = 7;
            this.pictureBox_Aligent_MiddleButtom.TabStop = false;
            this.pictureBox_Aligent_MiddleButtom.Click += new System.EventHandler(this.pictureBox_Aligent_MiddleButtom_Click);
            // 
            // pictureBox_Aligent_RightMiddle
            // 
            this.pictureBox_Aligent_RightMiddle.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_Aligent_RightMiddle.Location = new System.Drawing.Point(71, 36);
            this.pictureBox_Aligent_RightMiddle.Name = "pictureBox_Aligent_RightMiddle";
            this.pictureBox_Aligent_RightMiddle.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Aligent_RightMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Aligent_RightMiddle.TabIndex = 6;
            this.pictureBox_Aligent_RightMiddle.TabStop = false;
            this.pictureBox_Aligent_RightMiddle.Click += new System.EventHandler(this.pictureBox8_Click);
            // 
            // pictureBox_Aligent_MiddleMiddle
            // 
            this.pictureBox_Aligent_MiddleMiddle.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_Aligent_MiddleMiddle.Location = new System.Drawing.Point(37, 36);
            this.pictureBox_Aligent_MiddleMiddle.Name = "pictureBox_Aligent_MiddleMiddle";
            this.pictureBox_Aligent_MiddleMiddle.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Aligent_MiddleMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Aligent_MiddleMiddle.TabIndex = 5;
            this.pictureBox_Aligent_MiddleMiddle.TabStop = false;
            this.pictureBox_Aligent_MiddleMiddle.Click += new System.EventHandler(this.pictureBox_Aligent_MiddleMiddle_Click);
            // 
            // pictureBox_Aligent_RightTop
            // 
            this.pictureBox_Aligent_RightTop.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_Aligent_RightTop.Location = new System.Drawing.Point(71, 2);
            this.pictureBox_Aligent_RightTop.Name = "pictureBox_Aligent_RightTop";
            this.pictureBox_Aligent_RightTop.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Aligent_RightTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Aligent_RightTop.TabIndex = 4;
            this.pictureBox_Aligent_RightTop.TabStop = false;
            this.pictureBox_Aligent_RightTop.Click += new System.EventHandler(this.pictureBox_Aligent_RightTop_Click);
            // 
            // pictureBox_Aligent_MiddleTop
            // 
            this.pictureBox_Aligent_MiddleTop.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_Aligent_MiddleTop.Location = new System.Drawing.Point(37, 2);
            this.pictureBox_Aligent_MiddleTop.Name = "pictureBox_Aligent_MiddleTop";
            this.pictureBox_Aligent_MiddleTop.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Aligent_MiddleTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Aligent_MiddleTop.TabIndex = 3;
            this.pictureBox_Aligent_MiddleTop.TabStop = false;
            this.pictureBox_Aligent_MiddleTop.Click += new System.EventHandler(this.pictureBox_Aligent_MiddleTop_Click);
            // 
            // pictureBox_Aligent_LeftButtom
            // 
            this.pictureBox_Aligent_LeftButtom.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_Aligent_LeftButtom.Location = new System.Drawing.Point(3, 70);
            this.pictureBox_Aligent_LeftButtom.Name = "pictureBox_Aligent_LeftButtom";
            this.pictureBox_Aligent_LeftButtom.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Aligent_LeftButtom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Aligent_LeftButtom.TabIndex = 2;
            this.pictureBox_Aligent_LeftButtom.TabStop = false;
            this.pictureBox_Aligent_LeftButtom.Click += new System.EventHandler(this.pictureBox_Aligent_LeftButtom_Click);
            // 
            // pictureBox_Aligent_LeftMiddle
            // 
            this.pictureBox_Aligent_LeftMiddle.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_Aligent_LeftMiddle.Location = new System.Drawing.Point(3, 36);
            this.pictureBox_Aligent_LeftMiddle.Name = "pictureBox_Aligent_LeftMiddle";
            this.pictureBox_Aligent_LeftMiddle.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Aligent_LeftMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Aligent_LeftMiddle.TabIndex = 1;
            this.pictureBox_Aligent_LeftMiddle.TabStop = false;
            this.pictureBox_Aligent_LeftMiddle.Click += new System.EventHandler(this.pictureBox_Aligent_LeftMiddle_Click);
            // 
            // pictureBox_Aligent_LeftTop
            // 
            this.pictureBox_Aligent_LeftTop.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox_Aligent_LeftTop.Location = new System.Drawing.Point(3, 2);
            this.pictureBox_Aligent_LeftTop.Name = "pictureBox_Aligent_LeftTop";
            this.pictureBox_Aligent_LeftTop.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Aligent_LeftTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Aligent_LeftTop.TabIndex = 0;
            this.pictureBox_Aligent_LeftTop.TabStop = false;
            this.pictureBox_Aligent_LeftTop.Click += new System.EventHandler(this.pictureBox_Aligent_LeftTop_Click);
            // 
            // pictureBox_max
            // 
            this.pictureBox_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_max.BackColor = System.Drawing.Color.Maroon;
            this.pictureBox_max.Location = new System.Drawing.Point(958, 7);
            this.pictureBox_max.Name = "pictureBox_max";
            this.pictureBox_max.Size = new System.Drawing.Size(200, 200);
            this.pictureBox_max.TabIndex = 33;
            this.pictureBox_max.TabStop = false;
            this.pictureBox_max.Click += new System.EventHandler(this.pictureBox_max_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(952, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1007, 827);
            this.panel2.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Controls.Add(this.button5);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 350);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1007, 477);
            this.panel4.TabIndex = 3;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(803, 196);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(93, 23);
            this.button5.TabIndex = 26;
            this.button5.Text = "停止";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(798, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "自动更新时间（分钟）";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(800, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 30);
            this.button4.TabIndex = 24;
            this.button4.Text = "获取数据";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(803, 169);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 24);
            this.button3.TabIndex = 23;
            this.button3.Text = "开始";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(801, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 28);
            this.button2.TabIndex = 22;
            this.button2.Text = "发送数据到LED屏";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(803, 145);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(92, 21);
            this.textBox1.TabIndex = 21;
            this.textBox1.Text = "5";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(800, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 30);
            this.button1.TabIndex = 20;
            this.button1.Text = "更新数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.textBox大气区湿度);
            this.panel5.Controls.Add(this.textBox大气区温度);
            this.panel5.Controls.Add(this.label36);
            this.panel5.Controls.Add(this.label37);
            this.panel5.Controls.Add(this.textBox14);
            this.panel5.Controls.Add(this.textBox15);
            this.panel5.Controls.Add(this.label38);
            this.panel5.Controls.Add(this.label39);
            this.panel5.Controls.Add(this.textBox16);
            this.panel5.Controls.Add(this.label40);
            this.panel5.Controls.Add(this.label41);
            this.panel5.Controls.Add(this.textBox17);
            this.panel5.Controls.Add(this.label44);
            this.panel5.Controls.Add(this.label45);
            this.panel5.Controls.Add(this.label35);
            this.panel5.Location = new System.Drawing.Point(571, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(184, 219);
            this.panel5.TabIndex = 17;
            // 
            // textBox大气区湿度
            // 
            this.textBox大气区湿度.Location = new System.Drawing.Point(78, 76);
            this.textBox大气区湿度.Name = "textBox大气区湿度";
            this.textBox大气区湿度.Size = new System.Drawing.Size(43, 21);
            this.textBox大气区湿度.TabIndex = 18;
            // 
            // textBox大气区温度
            // 
            this.textBox大气区温度.Location = new System.Drawing.Point(78, 49);
            this.textBox大气区温度.Name = "textBox大气区温度";
            this.textBox大气区温度.Size = new System.Drawing.Size(43, 21);
            this.textBox大气区温度.TabIndex = 17;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(117, 164);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(17, 12);
            this.label36.TabIndex = 14;
            this.label36.Text = "列";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(76, 164);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(17, 12);
            this.label37.TabIndex = 13;
            this.label37.Text = "行";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(119, 179);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(39, 21);
            this.textBox14.TabIndex = 12;
            this.textBox14.Text = "5";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(74, 179);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(39, 21);
            this.textBox15.TabIndex = 11;
            this.textBox15.Text = "4";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(117, 125);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(17, 12);
            this.label38.TabIndex = 10;
            this.label38.Text = "列";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(76, 125);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(17, 12);
            this.label39.TabIndex = 9;
            this.label39.Text = "行";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(119, 140);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(39, 21);
            this.textBox16.TabIndex = 8;
            this.textBox16.Text = "5";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(15, 179);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(59, 12);
            this.label40.TabIndex = 7;
            this.label40.Text = "湿度坐标:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(15, 143);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(59, 12);
            this.label41.TabIndex = 6;
            this.label41.Text = "温度坐标:";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(74, 140);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(39, 21);
            this.textBox17.TabIndex = 5;
            this.textBox17.Text = "3";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(15, 80);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(65, 12);
            this.label44.TabIndex = 2;
            this.label44.Text = "湿度(%RH):";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(15, 52);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(65, 12);
            this.label45.TabIndex = 1;
            this.label45.Text = "温度:(℃):";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(52, 21);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(41, 12);
            this.label35.TabIndex = 0;
            this.label35.Text = "静停区";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Window;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.textBox降温区湿度);
            this.panel6.Controls.Add(this.textBox降温区温度);
            this.panel6.Controls.Add(this.label25);
            this.panel6.Controls.Add(this.label26);
            this.panel6.Controls.Add(this.textBox10);
            this.panel6.Controls.Add(this.textBox11);
            this.panel6.Controls.Add(this.label27);
            this.panel6.Controls.Add(this.label28);
            this.panel6.Controls.Add(this.textBox12);
            this.panel6.Controls.Add(this.label29);
            this.panel6.Controls.Add(this.label30);
            this.panel6.Controls.Add(this.textBox13);
            this.panel6.Controls.Add(this.label33);
            this.panel6.Controls.Add(this.label34);
            this.panel6.Controls.Add(this.label24);
            this.panel6.Location = new System.Drawing.Point(381, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(184, 219);
            this.panel6.TabIndex = 18;
            // 
            // textBox降温区湿度
            // 
            this.textBox降温区湿度.Location = new System.Drawing.Point(78, 76);
            this.textBox降温区湿度.Name = "textBox降温区湿度";
            this.textBox降温区湿度.Size = new System.Drawing.Size(43, 21);
            this.textBox降温区湿度.TabIndex = 18;
            // 
            // textBox降温区温度
            // 
            this.textBox降温区温度.Location = new System.Drawing.Point(78, 49);
            this.textBox降温区温度.Name = "textBox降温区温度";
            this.textBox降温区温度.Size = new System.Drawing.Size(43, 21);
            this.textBox降温区温度.TabIndex = 17;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(117, 164);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(17, 12);
            this.label25.TabIndex = 14;
            this.label25.Text = "列";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(76, 164);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(17, 12);
            this.label26.TabIndex = 13;
            this.label26.Text = "行";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(119, 179);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(39, 21);
            this.textBox10.TabIndex = 12;
            this.textBox10.Text = "4";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(74, 179);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(39, 21);
            this.textBox11.TabIndex = 11;
            this.textBox11.Text = "4";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(117, 125);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(17, 12);
            this.label27.TabIndex = 10;
            this.label27.Text = "列";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(76, 125);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(17, 12);
            this.label28.TabIndex = 9;
            this.label28.Text = "行";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(119, 140);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(39, 21);
            this.textBox12.TabIndex = 8;
            this.textBox12.Text = "4";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(15, 179);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(59, 12);
            this.label29.TabIndex = 7;
            this.label29.Text = "湿度坐标:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(15, 143);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(59, 12);
            this.label30.TabIndex = 6;
            this.label30.Text = "温度坐标:";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(74, 140);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(39, 21);
            this.textBox13.TabIndex = 5;
            this.textBox13.Text = "3";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(15, 80);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(65, 12);
            this.label33.TabIndex = 2;
            this.label33.Text = "湿度(%RH):";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(15, 52);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(65, 12);
            this.label34.TabIndex = 1;
            this.label34.Text = "温度:(℃):";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(52, 21);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 12);
            this.label24.TabIndex = 0;
            this.label24.Text = "降温区";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Window;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.textBox恒温区湿度);
            this.panel7.Controls.Add(this.textBox恒温区温度);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.textBox6);
            this.panel7.Controls.Add(this.textBox7);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.label17);
            this.panel7.Controls.Add(this.textBox8);
            this.panel7.Controls.Add(this.label18);
            this.panel7.Controls.Add(this.label19);
            this.panel7.Controls.Add(this.textBox9);
            this.panel7.Controls.Add(this.label22);
            this.panel7.Controls.Add(this.label23);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Location = new System.Drawing.Point(191, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(184, 219);
            this.panel7.TabIndex = 19;
            // 
            // textBox恒温区湿度
            // 
            this.textBox恒温区湿度.Location = new System.Drawing.Point(78, 76);
            this.textBox恒温区湿度.Name = "textBox恒温区湿度";
            this.textBox恒温区湿度.Size = new System.Drawing.Size(43, 21);
            this.textBox恒温区湿度.TabIndex = 18;
            // 
            // textBox恒温区温度
            // 
            this.textBox恒温区温度.Location = new System.Drawing.Point(78, 49);
            this.textBox恒温区温度.Name = "textBox恒温区温度";
            this.textBox恒温区温度.Size = new System.Drawing.Size(43, 21);
            this.textBox恒温区温度.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(117, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "列";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "行";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(119, 179);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(39, 21);
            this.textBox6.TabIndex = 12;
            this.textBox6.Text = "3";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(74, 179);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(39, 21);
            this.textBox7.TabIndex = 11;
            this.textBox7.Text = "4";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(117, 125);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 12);
            this.label16.TabIndex = 10;
            this.label16.Text = "列";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(76, 125);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 12);
            this.label17.TabIndex = 9;
            this.label17.Text = "行";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(119, 140);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(39, 21);
            this.textBox8.TabIndex = 8;
            this.textBox8.Text = "3";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(15, 179);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 12);
            this.label18.TabIndex = 7;
            this.label18.Text = "湿度坐标:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 143);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 6;
            this.label19.Text = "温度坐标:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(74, 140);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(39, 21);
            this.textBox9.TabIndex = 5;
            this.textBox9.Text = "3";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(15, 80);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(65, 12);
            this.label22.TabIndex = 2;
            this.label22.Text = "湿度(%RH):";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(15, 52);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(65, 12);
            this.label23.TabIndex = 1;
            this.label23.Text = "温度:(℃):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "恒温区";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.Window;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.textBox升温区湿度);
            this.panel8.Controls.Add(this.textBox升温区温度);
            this.panel8.Controls.Add(this.label14);
            this.panel8.Controls.Add(this.label15);
            this.panel8.Controls.Add(this.textBox4);
            this.panel8.Controls.Add(this.textBox5);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Controls.Add(this.label12);
            this.panel8.Controls.Add(this.textBox3);
            this.panel8.Controls.Add(this.label11);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.textBox2);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.label2);
            this.panel8.Location = new System.Drawing.Point(1, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(184, 219);
            this.panel8.TabIndex = 16;
            // 
            // textBox升温区湿度
            // 
            this.textBox升温区湿度.Location = new System.Drawing.Point(78, 76);
            this.textBox升温区湿度.Name = "textBox升温区湿度";
            this.textBox升温区湿度.Size = new System.Drawing.Size(43, 21);
            this.textBox升温区湿度.TabIndex = 16;
            // 
            // textBox升温区温度
            // 
            this.textBox升温区温度.Location = new System.Drawing.Point(78, 49);
            this.textBox升温区温度.Name = "textBox升温区温度";
            this.textBox升温区温度.Size = new System.Drawing.Size(43, 21);
            this.textBox升温区温度.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(117, 164);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 14;
            this.label14.Text = "列";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(76, 164);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 13;
            this.label15.Text = "行";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(119, 179);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(39, 21);
            this.textBox4.TabIndex = 12;
            this.textBox4.Text = "2";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(74, 179);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(39, 21);
            this.textBox5.TabIndex = 11;
            this.textBox5.Text = "4";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(117, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 10;
            this.label13.Text = "列";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(76, 125);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 9;
            this.label12.Text = "行";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(119, 140);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(39, 21);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 179);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 7;
            this.label11.Text = "湿度坐标:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 143);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "温度坐标:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(74, 140);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(39, 21);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "湿度(%RH):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "温度:(℃):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "升温区";
            // 
            // timer1
            // 
            this.timer1.Interval = 50000;
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // formGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 743);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel_Alignt);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "formGrid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "表格编辑";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formGrid_FormClosing);
            this.Load += new System.EventHandler(this.formGrid_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formGrid_MouseDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlAutoFit.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel_Alignt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_RightButtom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_MiddleButtom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_RightMiddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_MiddleMiddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_RightTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_MiddleTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_LeftButtom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_LeftMiddle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Aligent_LeftTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        //private void button4_Click(object sender, EventArgs e)
        //{

        //}

        //private void button1_Click(object sender, EventArgs e)
        //{

        //}

        //private void button2_Click(object sender, EventArgs e)
        //{

        //}

        //private void button3_Click(object sender, EventArgs e)
        //{

        //}

        //private void button5_Click(object sender, EventArgs e)
        //{

        //}
        private void Update_the_data()
        {


            //string s_SQL_升温区 = "SELECT Top 1 Tem,Hum FROM rsmonitor.dbo.tbhistory WHERE DeviceID='10008862' order by RecordTime desc";
            //DataTable DT_升温区 = DbUtils.ExecuteDataTable(s_SQL_升温区);
            //this.textBox升温区温度.Text = DT_升温区.Rows[0]["Tem"].ToString();
            //this.textBox升温区湿度.Text = DT_升温区.Rows[0]["Hum"].ToString();
            //string s_SQL_恒温区 = "SELECT Top 1 Tem,Hum FROM rsmonitor.dbo.tbhistory WHERE DeviceID='10008836' order by RecordTime desc";
            //DataTable DT_恒温区 = DbUtils.ExecuteDataTable(s_SQL_恒温区);
            //this.textBox恒温区温度.Text = DT_恒温区.Rows[0]["Tem"].ToString();
            //this.textBox恒温区湿度.Text = DT_恒温区.Rows[0]["Hum"].ToString();
            //string s_SQL_降温区 = "SELECT Top 1 Tem,Hum FROM rsmonitor.dbo.tbhistory WHERE DeviceID='10008834' order by RecordTime desc";
            //DataTable DT_降温区 = DbUtils.ExecuteDataTable(s_SQL_降温区);
            //this.textBox降温区温度.Text = DT_降温区.Rows[0]["Tem"].ToString();
            //this.textBox降温区湿度.Text = DT_降温区.Rows[0]["Hum"].ToString();
            //string s_SQL_大气区 = "SELECT Top 1 Tem,Hum FROM rsmonitor.dbo.tbhistory WHERE DeviceID='10008841' order by RecordTime desc";
            //DataTable DT_大气区 = DbUtils.ExecuteDataTable(s_SQL_大气区);
            //this.textBox大气区温度.Text = DT_大气区.Rows[0]["Tem"].ToString();
            //this.textBox大气区湿度.Text = DT_大气区.Rows[0]["Hum"].ToString();

            string s_SQL_升温区 = "SELECT Top 1 nTongDao1,nTongDao9 FROM SThree.dbo.pr_WenDu WHERE GongWeiType='10' and   cManufacture = '福建省榕圣市政工程股份有限公司连江建材分公司' order by dCaiJI desc";
            DataTable DT_升温区 = DbUtils.ExecuteDataTable(s_SQL_升温区);
            this.textBox升温区温度.Text = DT_升温区.Rows[0]["nTongDao1"].ToString();
            this.textBox升温区湿度.Text = DT_升温区.Rows[0]["nTongDao9"].ToString();
            string s_SQL_恒温区 = "SELECT Top 1 nTongDao1,nTongDao9 FROM SThree.dbo.pr_WenDu WHERE GongWeiType='11' and   cManufacture = '福建省榕圣市政工程股份有限公司连江建材分公司' order by dCaiJI desc";
            DataTable DT_恒温区 = DbUtils.ExecuteDataTable(s_SQL_恒温区);
            this.textBox恒温区温度.Text = DT_恒温区.Rows[0]["nTongDao1"].ToString();
            this.textBox恒温区湿度.Text = DT_恒温区.Rows[0]["nTongDao9"].ToString();
            string s_SQL_降温区 = "SELECT Top 1 nTongDao1,nTongDao9 FROM SThree.dbo.pr_WenDu WHERE GongWeiType='12' and   cManufacture = '福建省榕圣市政工程股份有限公司连江建材分公司' order by dCaiJI desc";
            DataTable DT_降温区 = DbUtils.ExecuteDataTable(s_SQL_降温区);
            this.textBox降温区温度.Text = DT_降温区.Rows[0]["nTongDao1"].ToString();
            this.textBox降温区湿度.Text = DT_降温区.Rows[0]["nTongDao9"].ToString();
            string s_SQL_大气区 = "SELECT Top 1 nTongDao1,nTongDao9 FROM SThree.dbo.pr_WenDu WHERE GongWeiType='09' and   cManufacture = '福建省榕圣市政工程股份有限公司连江建材分公司' order by dCaiJI desc";
            DataTable DT_大气区 = DbUtils.ExecuteDataTable(s_SQL_大气区);
            this.textBox大气区温度.Text = DT_大气区.Rows[0]["nTongDao1"].ToString();
            this.textBox大气区湿度.Text = DT_大气区.Rows[0]["nTongDao9"].ToString();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Update_the_data_to_LED();
        }

        private void Update_the_data_to_LED()
        {
            FlexCell.Cell cell_升温区温度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox2.Text.Trim()), System.Convert.ToInt32(this.textBox3.Text.Trim()));
            cell_升温区温度.Text = this.textBox升温区温度.Text.Trim();
            FlexCell.Cell  cell_升温区湿度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox5.Text.Trim()), System.Convert.ToInt32(this.textBox4.Text.Trim()));
            cell_升温区湿度.Text = this.textBox升温区湿度.Text.Trim();
            FlexCell.Cell  cell_恒温区温度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox9.Text.Trim()), System.Convert.ToInt32(this.textBox9.Text.Trim()));
            cell_恒温区温度.Text = this.textBox恒温区温度.Text.Trim();
            FlexCell.Cell  cell_恒温区湿度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox7.Text.Trim()), System.Convert.ToInt32(this.textBox6.Text.Trim()));
            cell_恒温区湿度.Text = this.textBox恒温区湿度.Text.Trim();
            FlexCell.Cell  cell_降温区温度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox13.Text.Trim()), System.Convert.ToInt32(this.textBox12.Text.Trim()));
            cell_降温区温度.Text = this.textBox降温区温度.Text.Trim();
            FlexCell.Cell  cell_降温区湿度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox11.Text.Trim()), System.Convert.ToInt32(this.textBox10.Text.Trim()));
            cell_降温区湿度.Text = this.textBox降温区湿度.Text.Trim();
            FlexCell.Cell  cell_大气区温度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox17.Text.Trim()), System.Convert.ToInt32(this.textBox16.Text.Trim()));
            cell_大气区温度.Text = this.textBox大气区温度.Text.Trim();
            FlexCell.Cell  cell_大气区湿度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox15.Text.Trim()), System.Convert.ToInt32(this.textBox14.Text.Trim()));
            cell_大气区湿度.Text = this.textBox大气区湿度.Text.Trim();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.fm.SendDatatoolStripButton_Click(sender, e);
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            this.Update_the_data();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            this.timer3.Interval = System.Convert.ToInt32(this.textBox1.Text.Trim()) * 1000 * 60;
            this.button5.Visible = true;
            this.button3.Visible = false;
            this.Update_data();
            this.timer3.Start();
        }

        private void timer3_Tick(object sender, System.EventArgs e)
        {
            this.Update_data();
        }

        private void Update_data()
        {
            this.Update_the_data();
            this.Update_the_data_to_LED();
            this.fm.RedrawSubarea();
            object sender = null;
            System.EventArgs e = null;
            this.fm.SendDatatoolStripButton_Click(sender, e);
            //this.fm.zhToolButton_Send_Click(sender, e);
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            this.button3.Visible = true;
            this.button5.Visible = false;
        }
    }
}
