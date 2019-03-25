//using FlexCell;
//using LedControl;
//using LedControlSystem.LedControlSystem;
//using LedControlSystem.Properties;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Data.OleDb;
//using System.Drawing;
//using System.Drawing.Text;
//using System.Reflection;
//using System.Runtime.InteropServices;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Windows.Forms;
//using ZHUI;

//namespace LedControlSystem
//{
//    public class formGrid2 : System.Windows.Forms.Form
//    {
//        private System.ComponentModel.IContainer components;

//        private System.Windows.Forms.MenuStrip menuStrip1;

//        private System.Windows.Forms.Panel panel1;

//        private System.Windows.Forms.ToolStripMenuItem fileExcelToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem import_ExcelToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem export_XmlToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem save_ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem exit_ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem CutToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem propertyToolStripMenuItem;

//        private System.Windows.Forms.OpenFileDialog openFileDialog1;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

//        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem clearContentToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem clearFormatToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

//        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

//        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem noMergeToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

//        private System.Windows.Forms.ToolStripMenuItem insertRowToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem insertColumnToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

//        private System.Windows.Forms.ToolStripMenuItem removeRowToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem removeColumnToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem borderToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem borderNoneToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderLeftToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderRightToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderTopToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderBottomToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderLineUpToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderLineDownToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderInsideToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderOutsideThinToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderOutsideStickToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem forecolorToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem backcolor_RedToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem gridcolorToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem bordercolorToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem wrapTextToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem nowrapTextToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem readonlyToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem noreadonlyToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem borderAllToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;

//        private System.Windows.Forms.ToolStripMenuItem alignNormalToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignTopToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignMiddleToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignBottomToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;

//        private System.Windows.Forms.ToolStripMenuItem alignLeftToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignTopLeftToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignMiddleLeftToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignBottomLeftToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;

//        private System.Windows.Forms.ToolStripMenuItem alignCenterToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignTopCenterToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignMiddleCenterToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignBottomCenterToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;

//        private System.Windows.Forms.ToolStripMenuItem alignRightToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignTopRightToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignMiddleRightToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem alignBottomRightToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem foreColor_RedToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem foreColor_GreenToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem foreColor_YellowToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem backColorRedToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem backcolor_GreenToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem backcolor_YellowToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem backcolor_BlackToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem gridcolor_RedToolStripMenuItem2;

//        private System.Windows.Forms.ToolStripMenuItem gridcolor_GreenToolStripMenuItem2;

//        private System.Windows.Forms.ToolStripMenuItem gridcolor_YellowToolStripMenuItem2;

//        private System.Windows.Forms.ToolStripMenuItem gridcolor_BlackToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem bordercolor_RedToolStripMenuItem3;

//        private System.Windows.Forms.ToolStripMenuItem bordercolor_GreenToolStripMenuItem3;

//        private System.Windows.Forms.ToolStripMenuItem bordercolor_YellowToolStripMenuItem3;

//        private System.Windows.Forms.ToolStripMenuItem bordercolor_BlackToolStripMenuItem2;

//        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

//        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;

//        private System.Windows.Forms.ToolStripMenuItem splitGridToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 取消合并单元格ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;

//        private System.Windows.Forms.ToolStripMenuItem 清除ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 清除内容ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 清除格式ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 清除格式ToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;

//        private System.Windows.Forms.ToolStripMenuItem 插入行ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 插入列ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;

//        private System.Windows.Forms.ToolStripMenuItem 删除行ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 删除列ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 属性ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 边框ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 无边框ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 左边框ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 右边框ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 上边框ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 下边框ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;

//        private System.Windows.Forms.ToolStripMenuItem 正斜线ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 反斜线ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;

//        private System.Windows.Forms.ToolStripMenuItem 内部框线ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 外部框线细ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 外部框线粗ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 全部框线ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 对齐方式ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 常规ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 顶部对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 中间对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 底部对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;

//        private System.Windows.Forms.ToolStripMenuItem 左对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 顶部左对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 中间左对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 底部左对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;

//        private System.Windows.Forms.ToolStripMenuItem 中对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 顶部中对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 中心对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 底部中对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;

//        private System.Windows.Forms.ToolStripMenuItem 右对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 顶部右对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 中间右对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 底部右对齐ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem 红ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 绿ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 黄ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 背景颜色ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 红ToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem 绿ToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem 黄ToolStripMenuItem1;

//        private System.Windows.Forms.ToolStripMenuItem 黑ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

//        private System.Windows.Forms.ToolStripMenuItem 红ToolStripMenuItem3;

//        private System.Windows.Forms.ToolStripMenuItem 绿ToolStripMenuItem4;

//        private System.Windows.Forms.ToolStripMenuItem 黄ToolStripMenuItem4;

//        private System.Windows.Forms.ToolStripMenuItem 黑ToolStripMenuItem2;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;

//        private System.Windows.Forms.ToolStripMenuItem 红ToolStripMenuItem5;

//        private System.Windows.Forms.ToolStripMenuItem 绿ToolStripMenuItem6;

//        private System.Windows.Forms.ToolStripMenuItem 黄ToolStripMenuItem6;

//        private System.Windows.Forms.ToolStripMenuItem 黑ToolStripMenuItem4;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;

//        private System.Windows.Forms.ToolStripMenuItem 设置自动换行ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 取消自动换行ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;

//        private System.Windows.Forms.ToolStripMenuItem 设置单元格只读ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripMenuItem 取消单元格只读ToolStripMenuItem;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;

//        private System.Windows.Forms.Timer timer1;

//        private System.Windows.Forms.ToolStrip toolStrip1;

//        private System.Windows.Forms.ToolStripButton button_Save;

//        private System.Windows.Forms.ToolStripButton button_Open;

//        private System.Windows.Forms.ToolStripButton button_New;

//        private System.Windows.Forms.ToolStripButton button_boardStyle;

//        private System.Windows.Forms.ToolStripButton button_Insert_Row;

//        private System.Windows.Forms.ToolStripButton button_insert_Col;

//        private System.Windows.Forms.ToolStripButton button_delete_Row;

//        private System.Windows.Forms.ToolStripButton button_delete_Col;

//        private System.Windows.Forms.ToolStripButton button_combin_Cell;

//        private System.Windows.Forms.ToolStripButton button_split_Cell;

//        private System.Windows.Forms.ToolStrip toolStrip2;

//        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_Font;

//        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;

//        private System.Windows.Forms.ToolStripLabel toolStripLabel_Blod;

//        private System.Windows.Forms.ToolStripLabel toolStripLabel_Italic;

//        private System.Windows.Forms.ToolStripLabel toolStripLabel_Underline;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;

//        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;

//        private System.Windows.Forms.Panel panel_Alignt;

//        private System.Windows.Forms.PictureBox pictureBox_Aligent_LeftTop;

//        private System.Windows.Forms.PictureBox pictureBox_Aligent_RightButtom;

//        private System.Windows.Forms.PictureBox pictureBox_Aligent_MiddleButtom;

//        private System.Windows.Forms.PictureBox pictureBox_Aligent_RightMiddle;

//        private System.Windows.Forms.PictureBox pictureBox_Aligent_MiddleMiddle;

//        private System.Windows.Forms.PictureBox pictureBox_Aligent_RightTop;

//        private System.Windows.Forms.PictureBox pictureBox_Aligent_MiddleTop;

//        private System.Windows.Forms.PictureBox pictureBox_Aligent_LeftButtom;

//        private System.Windows.Forms.PictureBox pictureBox_Aligent_LeftMiddle;

//        private System.Windows.Forms.ToolStripButton toolStripButton1;

//        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_fontColor_Red;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_fontColor_Green;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_fontColor_Yellow;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_fontColor_black;

//        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Backcolor_Red;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Backcolor_Green;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Backcolor_Yellow;

//        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Backcolor_Black;

//        private System.Windows.Forms.PictureBox pictureBox_max;

//        private System.Windows.Forms.PictureBox pictureBox2;

//        private System.Windows.Forms.ToolStripMenuItem saveas_ToolStripMenuItem;

//        private System.Windows.Forms.Panel panel2;

//        private System.Windows.Forms.ToolStripButton button_wraptext;

//        private System.Windows.Forms.Panel panel3;

//        private System.Windows.Forms.Label label_Percent;

//        private System.Windows.Forms.Button button_Cancel;

//        private System.Windows.Forms.Timer timer2;

//        private System.Windows.Forms.ToolStripMenuItem zishiToolStripMenuItem;

//        private System.Windows.Forms.ProgressBar progressBar1;

//        private static string formID = "formGrid";

//        private System.DateTime lastKeyUp;

//        private string fileName = "";

//        private formMain fm;

//        private Grid grid1;

//        private int maxWidth;

//        private int MaxHeight;

//        public static string ImportFileName = "";

//        public static int ImportSheetIndex = 0;

//        public static bool NeedImport = false;

//        private bool isMaxSize;

//        private System.Drawing.Size normalSize;

//        private System.Drawing.Point normalLocation;

//        private int nowRowNum;

//        private int totalRowNum;

//        private System.Windows.Forms.Panel panel4;

//        private System.Windows.Forms.Panel panel5;

//        private System.Windows.Forms.TextBox textBox大气区湿度;

//        private System.Windows.Forms.TextBox textBox大气区温度;

//        private System.Windows.Forms.Label label36;

//        private System.Windows.Forms.Label label37;

//        private System.Windows.Forms.TextBox textBox14;

//        private System.Windows.Forms.TextBox textBox15;

//        private System.Windows.Forms.Label label38;

//        private System.Windows.Forms.Label label39;

//        private System.Windows.Forms.TextBox textBox16;

//        private System.Windows.Forms.Label label40;

//        private System.Windows.Forms.Label label41;

//        private System.Windows.Forms.TextBox textBox17;

//        private System.Windows.Forms.Label label44;

//        private System.Windows.Forms.Label label45;

//        private System.Windows.Forms.Label label35;

//        private System.Windows.Forms.Panel panel6;

//        private System.Windows.Forms.TextBox textBox降温区湿度;

//        private System.Windows.Forms.TextBox textBox降温区温度;

//        private System.Windows.Forms.Label label25;

//        private System.Windows.Forms.Label label26;

//        private System.Windows.Forms.TextBox textBox10;

//        private System.Windows.Forms.TextBox textBox11;

//        private System.Windows.Forms.Label label27;

//        private System.Windows.Forms.Label label28;

//        private System.Windows.Forms.TextBox textBox12;

//        private System.Windows.Forms.Label label29;

//        private System.Windows.Forms.Label label30;

//        private System.Windows.Forms.TextBox textBox13;

//        private System.Windows.Forms.Label label33;

//        private System.Windows.Forms.Label label34;

//        private System.Windows.Forms.Label label24;

//        private System.Windows.Forms.Panel panel7;

//        private System.Windows.Forms.TextBox textBox恒温区湿度;

//        private System.Windows.Forms.TextBox textBox恒温区温度;

//        private System.Windows.Forms.Label label4;

//        private System.Windows.Forms.Label label5;

//        private System.Windows.Forms.TextBox textBox6;

//        private System.Windows.Forms.TextBox textBox7;

//        private System.Windows.Forms.Label label16;

//        private System.Windows.Forms.Label label17;

//        private System.Windows.Forms.TextBox textBox8;

//        private System.Windows.Forms.Label label18;

//        private System.Windows.Forms.Label label19;

//        private System.Windows.Forms.TextBox textBox9;

//        private System.Windows.Forms.Label label22;

//        private System.Windows.Forms.Label label23;

//        private System.Windows.Forms.Label label3;

//        private System.Windows.Forms.Panel panel8;

//        private System.Windows.Forms.TextBox textBox升温区湿度;

//        private System.Windows.Forms.TextBox textBox升温区温度;

//        private System.Windows.Forms.Label label14;

//        private System.Windows.Forms.Label label15;

//        private System.Windows.Forms.TextBox textBox4;

//        private System.Windows.Forms.TextBox textBox5;

//        private System.Windows.Forms.Label label13;

//        private System.Windows.Forms.Label label12;

//        private System.Windows.Forms.TextBox textBox3;

//        private System.Windows.Forms.Label label11;

//        private System.Windows.Forms.Label label10;

//        private System.Windows.Forms.TextBox textBox2;

//        private System.Windows.Forms.Label label7;

//        private System.Windows.Forms.Label label6;

//        private System.Windows.Forms.Label label2;

//        private System.Windows.Forms.Button button3;

//        private System.Windows.Forms.Button button2;

//        private System.Windows.Forms.TextBox textBox1;

//        private System.Windows.Forms.Button button1;

//        private System.Windows.Forms.Button button4;

//        private System.Windows.Forms.Label label1;

//        private System.Windows.Forms.Button button5;

//        private System.Windows.Forms.Timer timer3;

//        private System.Threading.Thread setRowHeight;

//        public event LedContentEvent ReDraw;

//        public static string FormID
//        {
//            get
//            {
//                return formGrid2.formID;
//            }
//            set
//            {
//                formGrid2.formID = value;
//            }
//        }

//        public formMain FormMain
//        {
//            set
//            {
//                this.fm = value;
//            }
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && this.components != null)
//            {
//                this.components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            this.components = new System.ComponentModel.Container();
//            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
//            this.fileExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.import_ExcelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.export_XmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.saveas_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.save_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.exit_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.CutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
//            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.clearContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.clearFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
//            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
//            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.noMergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
//            this.insertRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.insertColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
//            this.removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.removeColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.propertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.fontToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
//            this.borderLineUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderLineDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
//            this.borderInsideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderOutsideThinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderOutsideStickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.borderAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignMiddleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
//            this.alignLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignTopLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignMiddleLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignBottomLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
//            this.alignCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignTopCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignMiddleCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignBottomCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
//            this.alignRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignTopRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignMiddleRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.alignBottomRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.forecolorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.foreColor_RedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.foreColor_GreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.foreColor_YellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.backcolor_RedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.backColorRedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.backcolor_GreenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.backcolor_YellowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.backcolor_BlackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.gridcolorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.gridcolor_RedToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
//            this.gridcolor_GreenToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
//            this.gridcolor_YellowToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
//            this.gridcolor_BlackToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.bordercolorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.bordercolor_RedToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
//            this.bordercolor_GreenToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
//            this.bordercolor_YellowToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
//            this.bordercolor_BlackToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
//            this.wrapTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.nowrapTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.readonlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.noreadonlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.zishiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.panel1 = new System.Windows.Forms.Panel();
//            this.panel3 = new System.Windows.Forms.Panel();
//            this.progressBar1 = new System.Windows.Forms.ProgressBar();
//            this.label_Percent = new System.Windows.Forms.Label();
//            this.button_Cancel = new System.Windows.Forms.Button();
//            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
//            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
//            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
//            this.splitGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.取消合并单元格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
//            this.清除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.清除内容ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.清除格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.清除格式ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
//            this.插入行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.插入列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
//            this.删除行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.删除列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.边框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.无边框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.左边框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.右边框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.上边框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.下边框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
//            this.正斜线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.反斜线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
//            this.内部框线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.外部框线细ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.外部框线粗ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.全部框线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.对齐方式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.常规ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.顶部对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.中间对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.底部对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
//            this.左对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.顶部左对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.中间左对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.底部左对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
//            this.中对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.顶部中对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.中心对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.底部中对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
//            this.右对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.顶部右对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.中间右对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.底部右对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
//            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.红ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.绿ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.黄ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.背景颜色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.红ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.绿ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.黄ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
//            this.黑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
//            this.红ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
//            this.绿ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
//            this.黄ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
//            this.黑ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
//            this.红ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
//            this.绿ToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
//            this.黄ToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
//            this.黑ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
//            this.设置自动换行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.取消自动换行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
//            this.设置单元格只读ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.取消单元格只读ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
//            this.timer1 = new System.Windows.Forms.Timer(this.components);
//            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
//            this.button_New = new System.Windows.Forms.ToolStripButton();
//            this.button_Open = new System.Windows.Forms.ToolStripButton();
//            this.button_Save = new System.Windows.Forms.ToolStripButton();
//            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
//            this.button_boardStyle = new System.Windows.Forms.ToolStripButton();
//            this.button_Insert_Row = new System.Windows.Forms.ToolStripButton();
//            this.button_insert_Col = new System.Windows.Forms.ToolStripButton();
//            this.button_delete_Row = new System.Windows.Forms.ToolStripButton();
//            this.button_delete_Col = new System.Windows.Forms.ToolStripButton();
//            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
//            this.button_combin_Cell = new System.Windows.Forms.ToolStripButton();
//            this.button_split_Cell = new System.Windows.Forms.ToolStripButton();
//            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
//            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
//            this.toolStripComboBox_Font = new System.Windows.Forms.ToolStripComboBox();
//            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
//            this.toolStripLabel_Blod = new System.Windows.Forms.ToolStripLabel();
//            this.toolStripLabel_Italic = new System.Windows.Forms.ToolStripLabel();
//            this.toolStripLabel_Underline = new System.Windows.Forms.ToolStripLabel();
//            this.button_wraptext = new System.Windows.Forms.ToolStripButton();
//            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
//            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
//            this.toolStripMenuItem_fontColor_Red = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripMenuItem_fontColor_Green = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripMenuItem_fontColor_Yellow = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripMenuItem_fontColor_black = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
//            this.toolStripMenuItem_Backcolor_Red = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripMenuItem_Backcolor_Green = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripMenuItem_Backcolor_Yellow = new System.Windows.Forms.ToolStripMenuItem();
//            this.toolStripMenuItem_Backcolor_Black = new System.Windows.Forms.ToolStripMenuItem();
//            this.panel_Alignt = new System.Windows.Forms.Panel();
//            this.pictureBox_Aligent_RightButtom = new System.Windows.Forms.PictureBox();
//            this.pictureBox_Aligent_MiddleButtom = new System.Windows.Forms.PictureBox();
//            this.pictureBox_Aligent_RightMiddle = new System.Windows.Forms.PictureBox();
//            this.pictureBox_Aligent_MiddleMiddle = new System.Windows.Forms.PictureBox();
//            this.pictureBox_Aligent_RightTop = new System.Windows.Forms.PictureBox();
//            this.pictureBox_Aligent_MiddleTop = new System.Windows.Forms.PictureBox();
//            this.pictureBox_Aligent_LeftButtom = new System.Windows.Forms.PictureBox();
//            this.pictureBox_Aligent_LeftMiddle = new System.Windows.Forms.PictureBox();
//            this.pictureBox_Aligent_LeftTop = new System.Windows.Forms.PictureBox();
//            this.pictureBox_max = new System.Windows.Forms.PictureBox();
//            this.pictureBox2 = new System.Windows.Forms.PictureBox();
//            this.panel2 = new System.Windows.Forms.Panel();
//            this.panel4 = new System.Windows.Forms.Panel();
//            this.button5 = new System.Windows.Forms.Button();
//            this.label1 = new System.Windows.Forms.Label();
//            this.button4 = new System.Windows.Forms.Button();
//            this.button3 = new System.Windows.Forms.Button();
//            this.button2 = new System.Windows.Forms.Button();
//            this.textBox1 = new System.Windows.Forms.TextBox();
//            this.button1 = new System.Windows.Forms.Button();
//            this.panel5 = new System.Windows.Forms.Panel();
//            this.textBox大气区湿度 = new System.Windows.Forms.TextBox();
//            this.textBox大气区温度 = new System.Windows.Forms.TextBox();
//            this.label36 = new System.Windows.Forms.Label();
//            this.label37 = new System.Windows.Forms.Label();
//            this.textBox14 = new System.Windows.Forms.TextBox();
//            this.textBox15 = new System.Windows.Forms.TextBox();
//            this.label38 = new System.Windows.Forms.Label();
//            this.label39 = new System.Windows.Forms.Label();
//            this.textBox16 = new System.Windows.Forms.TextBox();
//            this.label40 = new System.Windows.Forms.Label();
//            this.label41 = new System.Windows.Forms.Label();
//            this.textBox17 = new System.Windows.Forms.TextBox();
//            this.label44 = new System.Windows.Forms.Label();
//            this.label45 = new System.Windows.Forms.Label();
//            this.label35 = new System.Windows.Forms.Label();
//            this.panel6 = new System.Windows.Forms.Panel();
//            this.textBox降温区湿度 = new System.Windows.Forms.TextBox();
//            this.textBox降温区温度 = new System.Windows.Forms.TextBox();
//            this.label25 = new System.Windows.Forms.Label();
//            this.label26 = new System.Windows.Forms.Label();
//            this.textBox10 = new System.Windows.Forms.TextBox();
//            this.textBox11 = new System.Windows.Forms.TextBox();
//            this.label27 = new System.Windows.Forms.Label();
//            this.label28 = new System.Windows.Forms.Label();
//            this.textBox12 = new System.Windows.Forms.TextBox();
//            this.label29 = new System.Windows.Forms.Label();
//            this.label30 = new System.Windows.Forms.Label();
//            this.textBox13 = new System.Windows.Forms.TextBox();
//            this.label33 = new System.Windows.Forms.Label();
//            this.label34 = new System.Windows.Forms.Label();
//            this.label24 = new System.Windows.Forms.Label();
//            this.panel7 = new System.Windows.Forms.Panel();
//            this.textBox恒温区湿度 = new System.Windows.Forms.TextBox();
//            this.textBox恒温区温度 = new System.Windows.Forms.TextBox();
//            this.label4 = new System.Windows.Forms.Label();
//            this.label5 = new System.Windows.Forms.Label();
//            this.textBox6 = new System.Windows.Forms.TextBox();
//            this.textBox7 = new System.Windows.Forms.TextBox();
//            this.label16 = new System.Windows.Forms.Label();
//            this.label17 = new System.Windows.Forms.Label();
//            this.textBox8 = new System.Windows.Forms.TextBox();
//            this.label18 = new System.Windows.Forms.Label();
//            this.label19 = new System.Windows.Forms.Label();
//            this.textBox9 = new System.Windows.Forms.TextBox();
//            this.label22 = new System.Windows.Forms.Label();
//            this.label23 = new System.Windows.Forms.Label();
//            this.label3 = new System.Windows.Forms.Label();
//            this.panel8 = new System.Windows.Forms.Panel();
//            this.textBox升温区湿度 = new System.Windows.Forms.TextBox();
//            this.textBox升温区温度 = new System.Windows.Forms.TextBox();
//            this.label14 = new System.Windows.Forms.Label();
//            this.label15 = new System.Windows.Forms.Label();
//            this.textBox4 = new System.Windows.Forms.TextBox();
//            this.textBox5 = new System.Windows.Forms.TextBox();
//            this.label13 = new System.Windows.Forms.Label();
//            this.label12 = new System.Windows.Forms.Label();
//            this.textBox3 = new System.Windows.Forms.TextBox();
//            this.label11 = new System.Windows.Forms.Label();
//            this.label10 = new System.Windows.Forms.Label();
//            this.textBox2 = new System.Windows.Forms.TextBox();
//            this.label7 = new System.Windows.Forms.Label();
//            this.label6 = new System.Windows.Forms.Label();
//            this.label2 = new System.Windows.Forms.Label();
//            this.timer2 = new System.Windows.Forms.Timer(this.components);
//            this.timer3 = new System.Windows.Forms.Timer(this.components);
//            this.menuStrip1.SuspendLayout();
//            this.panel1.SuspendLayout();
//            this.panel3.SuspendLayout();
//            this.contextMenuStrip1.SuspendLayout();
//            this.toolStrip1.SuspendLayout();
//            this.toolStrip2.SuspendLayout();
//            this.panel_Alignt.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_RightButtom).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_MiddleButtom).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_RightMiddle).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_MiddleMiddle).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_RightTop).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_MiddleTop).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_LeftButtom).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_LeftMiddle).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_LeftTop).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_max).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
//            this.panel2.SuspendLayout();
//            this.panel4.SuspendLayout();
//            this.panel5.SuspendLayout();
//            this.panel6.SuspendLayout();
//            this.panel7.SuspendLayout();
//            this.panel8.SuspendLayout();
//            base.SuspendLayout();
//            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
//            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
//            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.fileExcelToolStripMenuItem,
//                this.EditToolStripMenuItem,
//                this.propertyToolStripMenuItem,
//                this.zishiToolStripMenuItem
//            });
//            this.menuStrip1.Location = new System.Drawing.Point(4, 1);
//            this.menuStrip1.Name = "menuStrip1";
//            this.menuStrip1.Size = new System.Drawing.Size(244, 25);
//            this.menuStrip1.TabIndex = 0;
//            this.menuStrip1.Text = "menuStrip1";
//            this.fileExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.import_ExcelToolStripMenuItem1,
//                this.export_XmlToolStripMenuItem,
//                this.saveas_ToolStripMenuItem,
//                this.save_ToolStripMenuItem,
//                this.exit_ToolStripMenuItem
//            });
//            this.fileExcelToolStripMenuItem.Name = "fileExcelToolStripMenuItem";
//            this.fileExcelToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
//            this.fileExcelToolStripMenuItem.Text = "文件";
//            this.fileExcelToolStripMenuItem.Click += new System.EventHandler(this.fileExcelToolStripMenuItem_Click);
//            this.import_ExcelToolStripMenuItem1.Name = "import_ExcelToolStripMenuItem1";
//            this.import_ExcelToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
//            this.import_ExcelToolStripMenuItem1.Text = "导入Excel";
//            this.import_ExcelToolStripMenuItem1.Click += new System.EventHandler(this.import_ExcelToolStripMenuItem1_Click);
//            this.export_XmlToolStripMenuItem.Name = "export_XmlToolStripMenuItem";
//            this.export_XmlToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
//            this.export_XmlToolStripMenuItem.Text = "导出Xml";
//            this.export_XmlToolStripMenuItem.Click += new System.EventHandler(this.export_XmlToolStripMenuItem_Click);
//            this.saveas_ToolStripMenuItem.Name = "saveas_ToolStripMenuItem";
//            this.saveas_ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
//            this.saveas_ToolStripMenuItem.Text = "另存为";
//            this.saveas_ToolStripMenuItem.Click += new System.EventHandler(this.saveas_ToolStripMenuItem_Click);
//            this.save_ToolStripMenuItem.Name = "save_ToolStripMenuItem";
//            this.save_ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
//            this.save_ToolStripMenuItem.Text = "保存";
//            this.save_ToolStripMenuItem.Click += new System.EventHandler(this.save_ToolStripMenuItem_Click);
//            this.exit_ToolStripMenuItem.Name = "exit_ToolStripMenuItem";
//            this.exit_ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
//            this.exit_ToolStripMenuItem.Text = "退出";
//            this.exit_ToolStripMenuItem.Click += new System.EventHandler(this.exit_ToolStripMenuItem_Click);
//            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.CopyToolStripMenuItem,
//                this.CutToolStripMenuItem,
//                this.pasteToolStripMenuItem,
//                this.toolStripSeparator2,
//                this.clearToolStripMenuItem,
//                this.toolStripSeparator3,
//                this.mergeToolStripMenuItem,
//                this.noMergeToolStripMenuItem,
//                this.toolStripSeparator4,
//                this.insertRowToolStripMenuItem,
//                this.insertColumnToolStripMenuItem,
//                this.toolStripSeparator5,
//                this.removeRowToolStripMenuItem,
//                this.removeColumnToolStripMenuItem
//            });
//            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
//            this.EditToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
//            this.EditToolStripMenuItem.Text = "编辑";
//            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
//            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.CopyToolStripMenuItem.Text = "复制";
//            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
//            this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
//            this.CutToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.CutToolStripMenuItem.Text = "剪切";
//            this.CutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
//            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
//            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.pasteToolStripMenuItem.Text = "粘贴";
//            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
//            this.toolStripSeparator2.Name = "toolStripSeparator2";
//            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
//            this.clearToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.clearContentToolStripMenuItem,
//                this.clearFormatToolStripMenuItem,
//                this.toolStripSeparator1,
//                this.clearAllToolStripMenuItem
//            });
//            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
//            this.clearToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.clearToolStripMenuItem.Text = "清除";
//            this.clearContentToolStripMenuItem.Name = "clearContentToolStripMenuItem";
//            this.clearContentToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
//            this.clearContentToolStripMenuItem.Text = "清除内容";
//            this.clearContentToolStripMenuItem.Click += new System.EventHandler(this.clearContentToolStripMenuItem_Click);
//            this.clearFormatToolStripMenuItem.Name = "clearFormatToolStripMenuItem";
//            this.clearFormatToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
//            this.clearFormatToolStripMenuItem.Text = "清除格式";
//            this.clearFormatToolStripMenuItem.Click += new System.EventHandler(this.clearFormatToolStripMenuItem_Click);
//            this.toolStripSeparator1.Name = "toolStripSeparator1";
//            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
//            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
//            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
//            this.clearAllToolStripMenuItem.Text = "清除全部";
//            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
//            this.toolStripSeparator3.Name = "toolStripSeparator3";
//            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
//            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
//            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.mergeToolStripMenuItem.Text = "合并单元格";
//            this.mergeToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
//            this.noMergeToolStripMenuItem.Name = "noMergeToolStripMenuItem";
//            this.noMergeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.noMergeToolStripMenuItem.Text = "取消合并单元格";
//            this.noMergeToolStripMenuItem.Click += new System.EventHandler(this.noMergeToolStripMenuItem_Click);
//            this.toolStripSeparator4.Name = "toolStripSeparator4";
//            this.toolStripSeparator4.Size = new System.Drawing.Size(157, 6);
//            this.insertRowToolStripMenuItem.Name = "insertRowToolStripMenuItem";
//            this.insertRowToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.insertRowToolStripMenuItem.Text = "插入行";
//            this.insertRowToolStripMenuItem.Click += new System.EventHandler(this.insertRowToolStripMenuItem_Click);
//            this.insertColumnToolStripMenuItem.Name = "insertColumnToolStripMenuItem";
//            this.insertColumnToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.insertColumnToolStripMenuItem.Text = "插入列";
//            this.insertColumnToolStripMenuItem.Click += new System.EventHandler(this.insertColumnToolStripMenuItem_Click);
//            this.toolStripSeparator5.Name = "toolStripSeparator5";
//            this.toolStripSeparator5.Size = new System.Drawing.Size(157, 6);
//            this.removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
//            this.removeRowToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.removeRowToolStripMenuItem.Text = "删除行";
//            this.removeRowToolStripMenuItem.Click += new System.EventHandler(this.removeRowToolStripMenuItem_Click);
//            this.removeColumnToolStripMenuItem.Name = "removeColumnToolStripMenuItem";
//            this.removeColumnToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.removeColumnToolStripMenuItem.Text = "删除列";
//            this.removeColumnToolStripMenuItem.Click += new System.EventHandler(this.removeColumnToolStripMenuItem_Click);
//            this.propertyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.fontToolStripMenuItem1,
//                this.borderToolStripMenuItem1,
//                this.alignToolStripMenuItem1,
//                this.forecolorToolStripMenuItem,
//                this.backcolor_RedToolStripMenuItem,
//                this.gridcolorToolStripMenuItem,
//                this.bordercolorToolStripMenuItem,
//                this.wrapTextToolStripMenuItem,
//                this.nowrapTextToolStripMenuItem,
//                this.readonlyToolStripMenuItem,
//                this.noreadonlyToolStripMenuItem
//            });
//            this.propertyToolStripMenuItem.Name = "propertyToolStripMenuItem";
//            this.propertyToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
//            this.propertyToolStripMenuItem.Text = "属性";
//            this.fontToolStripMenuItem1.Name = "fontToolStripMenuItem1";
//            this.fontToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
//            this.fontToolStripMenuItem1.Text = "字体";
//            this.fontToolStripMenuItem1.Click += new System.EventHandler(this.fontToolStripMenuItem1_Click);
//            this.borderToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.borderNoneToolStripMenuItem,
//                this.borderLeftToolStripMenuItem,
//                this.borderRightToolStripMenuItem,
//                this.borderTopToolStripMenuItem,
//                this.borderBottomToolStripMenuItem,
//                this.toolStripSeparator6,
//                this.borderLineUpToolStripMenuItem,
//                this.borderLineDownToolStripMenuItem,
//                this.toolStripSeparator7,
//                this.borderInsideToolStripMenuItem,
//                this.borderOutsideThinToolStripMenuItem,
//                this.borderOutsideStickToolStripMenuItem,
//                this.borderAllToolStripMenuItem1
//            });
//            this.borderToolStripMenuItem1.Name = "borderToolStripMenuItem1";
//            this.borderToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
//            this.borderToolStripMenuItem1.Text = "边框";
//            this.borderNoneToolStripMenuItem.Name = "borderNoneToolStripMenuItem";
//            this.borderNoneToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderNoneToolStripMenuItem.Text = "无边框";
//            this.borderNoneToolStripMenuItem.Click += new System.EventHandler(this.borderNoneToolStripMenuItem_Click);
//            this.borderLeftToolStripMenuItem.Name = "borderLeftToolStripMenuItem";
//            this.borderLeftToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderLeftToolStripMenuItem.Text = "左边框";
//            this.borderLeftToolStripMenuItem.Click += new System.EventHandler(this.borderLeftToolStripMenuItem_Click);
//            this.borderRightToolStripMenuItem.Name = "borderRightToolStripMenuItem";
//            this.borderRightToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderRightToolStripMenuItem.Text = "右边框";
//            this.borderRightToolStripMenuItem.Click += new System.EventHandler(this.borderRightToolStripMenuItem_Click);
//            this.borderTopToolStripMenuItem.Name = "borderTopToolStripMenuItem";
//            this.borderTopToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderTopToolStripMenuItem.Text = "上边框";
//            this.borderTopToolStripMenuItem.Click += new System.EventHandler(this.borderTopToolStripMenuItem_Click);
//            this.borderBottomToolStripMenuItem.Name = "borderBottomToolStripMenuItem";
//            this.borderBottomToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderBottomToolStripMenuItem.Text = "下边框";
//            this.borderBottomToolStripMenuItem.Click += new System.EventHandler(this.borderBottomToolStripMenuItem_Click);
//            this.toolStripSeparator6.Name = "toolStripSeparator6";
//            this.toolStripSeparator6.Size = new System.Drawing.Size(141, 6);
//            this.borderLineUpToolStripMenuItem.Name = "borderLineUpToolStripMenuItem";
//            this.borderLineUpToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderLineUpToolStripMenuItem.Text = "正斜线";
//            this.borderLineUpToolStripMenuItem.Click += new System.EventHandler(this.borderLineUpToolStripMenuItem_Click);
//            this.borderLineDownToolStripMenuItem.Name = "borderLineDownToolStripMenuItem";
//            this.borderLineDownToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderLineDownToolStripMenuItem.Text = "反斜线";
//            this.borderLineDownToolStripMenuItem.Click += new System.EventHandler(this.borderLineDownToolStripMenuItem_Click);
//            this.toolStripSeparator7.Name = "toolStripSeparator7";
//            this.toolStripSeparator7.Size = new System.Drawing.Size(141, 6);
//            this.borderInsideToolStripMenuItem.Name = "borderInsideToolStripMenuItem";
//            this.borderInsideToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderInsideToolStripMenuItem.Text = "内部边框";
//            this.borderInsideToolStripMenuItem.Click += new System.EventHandler(this.borderInsideToolStripMenuItem_Click);
//            this.borderOutsideThinToolStripMenuItem.Name = "borderOutsideThinToolStripMenuItem";
//            this.borderOutsideThinToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderOutsideThinToolStripMenuItem.Text = "外部边框(细)";
//            this.borderOutsideThinToolStripMenuItem.Click += new System.EventHandler(this.borderOutsideThinToolStripMenuItem_Click);
//            this.borderOutsideStickToolStripMenuItem.Name = "borderOutsideStickToolStripMenuItem";
//            this.borderOutsideStickToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
//            this.borderOutsideStickToolStripMenuItem.Text = "外部边框(粗)";
//            this.borderOutsideStickToolStripMenuItem.Click += new System.EventHandler(this.borderOutsideStickToolStripMenuItem_Click);
//            this.borderAllToolStripMenuItem1.Name = "borderAllToolStripMenuItem1";
//            this.borderAllToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
//            this.borderAllToolStripMenuItem1.Text = "全部边框";
//            this.borderAllToolStripMenuItem1.Click += new System.EventHandler(this.borderAllToolStripMenuItem1_Click);
//            this.alignToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.alignNormalToolStripMenuItem,
//                this.alignTopToolStripMenuItem,
//                this.alignMiddleToolStripMenuItem,
//                this.alignBottomToolStripMenuItem,
//                this.toolStripSeparator10,
//                this.alignLeftToolStripMenuItem,
//                this.alignTopLeftToolStripMenuItem,
//                this.alignMiddleLeftToolStripMenuItem,
//                this.alignBottomLeftToolStripMenuItem,
//                this.toolStripSeparator9,
//                this.alignCenterToolStripMenuItem,
//                this.alignTopCenterToolStripMenuItem,
//                this.alignMiddleCenterToolStripMenuItem,
//                this.alignBottomCenterToolStripMenuItem,
//                this.toolStripSeparator8,
//                this.alignRightToolStripMenuItem,
//                this.alignTopRightToolStripMenuItem,
//                this.alignMiddleRightToolStripMenuItem,
//                this.alignBottomRightToolStripMenuItem
//            });
//            this.alignToolStripMenuItem1.Name = "alignToolStripMenuItem1";
//            this.alignToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
//            this.alignToolStripMenuItem1.Text = "对齐方式";
//            this.alignNormalToolStripMenuItem.Name = "alignNormalToolStripMenuItem";
//            this.alignNormalToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignNormalToolStripMenuItem.Text = "常规";
//            this.alignNormalToolStripMenuItem.Click += new System.EventHandler(this.alignNormalToolStripMenuItem_Click);
//            this.alignTopToolStripMenuItem.Name = "alignTopToolStripMenuItem";
//            this.alignTopToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignTopToolStripMenuItem.Text = "顶部对齐";
//            this.alignTopToolStripMenuItem.Click += new System.EventHandler(this.alignTopToolStripMenuItem_Click);
//            this.alignMiddleToolStripMenuItem.Name = "alignMiddleToolStripMenuItem";
//            this.alignMiddleToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignMiddleToolStripMenuItem.Text = "中间对齐";
//            this.alignMiddleToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleToolStripMenuItem_Click);
//            this.alignBottomToolStripMenuItem.Name = "alignBottomToolStripMenuItem";
//            this.alignBottomToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignBottomToolStripMenuItem.Text = "底部对齐";
//            this.alignBottomToolStripMenuItem.Click += new System.EventHandler(this.alignBottomToolStripMenuItem_Click);
//            this.toolStripSeparator10.Name = "toolStripSeparator10";
//            this.toolStripSeparator10.Size = new System.Drawing.Size(133, 6);
//            this.alignLeftToolStripMenuItem.Name = "alignLeftToolStripMenuItem";
//            this.alignLeftToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignLeftToolStripMenuItem.Text = "左对齐";
//            this.alignLeftToolStripMenuItem.Click += new System.EventHandler(this.alignLeftToolStripMenuItem_Click);
//            this.alignTopLeftToolStripMenuItem.Name = "alignTopLeftToolStripMenuItem";
//            this.alignTopLeftToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignTopLeftToolStripMenuItem.Text = "顶部左对齐";
//            this.alignTopLeftToolStripMenuItem.Click += new System.EventHandler(this.alignTopLeftToolStripMenuItem_Click);
//            this.alignMiddleLeftToolStripMenuItem.Name = "alignMiddleLeftToolStripMenuItem";
//            this.alignMiddleLeftToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignMiddleLeftToolStripMenuItem.Text = "中间左对齐";
//            this.alignMiddleLeftToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleLeftToolStripMenuItem_Click);
//            this.alignBottomLeftToolStripMenuItem.Name = "alignBottomLeftToolStripMenuItem";
//            this.alignBottomLeftToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignBottomLeftToolStripMenuItem.Text = "底部左对齐";
//            this.alignBottomLeftToolStripMenuItem.Click += new System.EventHandler(this.alignBottomLeftToolStripMenuItem_Click);
//            this.toolStripSeparator9.Name = "toolStripSeparator9";
//            this.toolStripSeparator9.Size = new System.Drawing.Size(133, 6);
//            this.alignCenterToolStripMenuItem.Name = "alignCenterToolStripMenuItem";
//            this.alignCenterToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignCenterToolStripMenuItem.Text = "中对齐";
//            this.alignCenterToolStripMenuItem.Click += new System.EventHandler(this.alignCenterToolStripMenuItem_Click);
//            this.alignTopCenterToolStripMenuItem.Name = "alignTopCenterToolStripMenuItem";
//            this.alignTopCenterToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignTopCenterToolStripMenuItem.Text = "顶部中对齐";
//            this.alignTopCenterToolStripMenuItem.Click += new System.EventHandler(this.alignTopCenterToolStripMenuItem_Click);
//            this.alignMiddleCenterToolStripMenuItem.Name = "alignMiddleCenterToolStripMenuItem";
//            this.alignMiddleCenterToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignMiddleCenterToolStripMenuItem.Text = "中心对齐";
//            this.alignMiddleCenterToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleCenterToolStripMenuItem_Click);
//            this.alignBottomCenterToolStripMenuItem.Name = "alignBottomCenterToolStripMenuItem";
//            this.alignBottomCenterToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignBottomCenterToolStripMenuItem.Text = "底部中对齐";
//            this.alignBottomCenterToolStripMenuItem.Click += new System.EventHandler(this.alignBottomCenterToolStripMenuItem_Click);
//            this.toolStripSeparator8.Name = "toolStripSeparator8";
//            this.toolStripSeparator8.Size = new System.Drawing.Size(133, 6);
//            this.alignRightToolStripMenuItem.Name = "alignRightToolStripMenuItem";
//            this.alignRightToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignRightToolStripMenuItem.Text = "右对齐";
//            this.alignRightToolStripMenuItem.Click += new System.EventHandler(this.alignRightToolStripMenuItem_Click);
//            this.alignTopRightToolStripMenuItem.Name = "alignTopRightToolStripMenuItem";
//            this.alignTopRightToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignTopRightToolStripMenuItem.Text = "顶部右对齐";
//            this.alignTopRightToolStripMenuItem.Click += new System.EventHandler(this.alignTopRightToolStripMenuItem_Click);
//            this.alignMiddleRightToolStripMenuItem.Name = "alignMiddleRightToolStripMenuItem";
//            this.alignMiddleRightToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignMiddleRightToolStripMenuItem.Text = "中间右对齐";
//            this.alignMiddleRightToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleRightToolStripMenuItem_Click);
//            this.alignBottomRightToolStripMenuItem.Name = "alignBottomRightToolStripMenuItem";
//            this.alignBottomRightToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.alignBottomRightToolStripMenuItem.Text = "底部右对齐";
//            this.alignBottomRightToolStripMenuItem.Click += new System.EventHandler(this.alignBottomRightToolStripMenuItem_Click);
//            this.forecolorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.foreColor_RedToolStripMenuItem,
//                this.foreColor_GreenToolStripMenuItem,
//                this.foreColor_YellowToolStripMenuItem
//            });
//            this.forecolorToolStripMenuItem.Name = "forecolorToolStripMenuItem";
//            this.forecolorToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.forecolorToolStripMenuItem.Text = "文字颜色";
//            this.foreColor_RedToolStripMenuItem.Name = "foreColor_RedToolStripMenuItem";
//            this.foreColor_RedToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
//            this.foreColor_RedToolStripMenuItem.Text = "红";
//            this.foreColor_RedToolStripMenuItem.Click += new System.EventHandler(this.foreColor_RedToolStripMenuItem_Click);
//            this.foreColor_GreenToolStripMenuItem.Name = "foreColor_GreenToolStripMenuItem";
//            this.foreColor_GreenToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
//            this.foreColor_GreenToolStripMenuItem.Text = "绿";
//            this.foreColor_GreenToolStripMenuItem.Click += new System.EventHandler(this.foreColor_GreenToolStripMenuItem_Click);
//            this.foreColor_YellowToolStripMenuItem.Name = "foreColor_YellowToolStripMenuItem";
//            this.foreColor_YellowToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
//            this.foreColor_YellowToolStripMenuItem.Text = "黄";
//            this.foreColor_YellowToolStripMenuItem.Click += new System.EventHandler(this.foreColor_YellowToolStripMenuItem_Click);
//            this.backcolor_RedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.backColorRedToolStripMenuItem1,
//                this.backcolor_GreenToolStripMenuItem1,
//                this.backcolor_YellowToolStripMenuItem1,
//                this.backcolor_BlackToolStripMenuItem
//            });
//            this.backcolor_RedToolStripMenuItem.Name = "backcolor_RedToolStripMenuItem";
//            this.backcolor_RedToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.backcolor_RedToolStripMenuItem.Text = "背景颜色";
//            this.backColorRedToolStripMenuItem1.Name = "backColorRedToolStripMenuItem1";
//            this.backColorRedToolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
//            this.backColorRedToolStripMenuItem1.Text = "红";
//            this.backColorRedToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_RedToolStripMenuItem1_Click);
//            this.backcolor_GreenToolStripMenuItem1.Name = "backcolor_GreenToolStripMenuItem1";
//            this.backcolor_GreenToolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
//            this.backcolor_GreenToolStripMenuItem1.Text = "绿";
//            this.backcolor_GreenToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_GreenToolStripMenuItem1_Click);
//            this.backcolor_YellowToolStripMenuItem1.Name = "backcolor_YellowToolStripMenuItem1";
//            this.backcolor_YellowToolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
//            this.backcolor_YellowToolStripMenuItem1.Text = "黄";
//            this.backcolor_YellowToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_YellowToolStripMenuItem1_Click);
//            this.backcolor_BlackToolStripMenuItem.Name = "backcolor_BlackToolStripMenuItem";
//            this.backcolor_BlackToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
//            this.backcolor_BlackToolStripMenuItem.Text = "黑";
//            this.backcolor_BlackToolStripMenuItem.Click += new System.EventHandler(this.backcolor_BlackToolStripMenuItem_Click);
//            this.gridcolorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.gridcolor_RedToolStripMenuItem2,
//                this.gridcolor_GreenToolStripMenuItem2,
//                this.gridcolor_YellowToolStripMenuItem2,
//                this.gridcolor_BlackToolStripMenuItem1
//            });
//            this.gridcolorToolStripMenuItem.Name = "gridcolorToolStripMenuItem";
//            this.gridcolorToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.gridcolorToolStripMenuItem.Text = "表格颜色";
//            this.gridcolor_RedToolStripMenuItem2.Name = "gridcolor_RedToolStripMenuItem2";
//            this.gridcolor_RedToolStripMenuItem2.Size = new System.Drawing.Size(88, 22);
//            this.gridcolor_RedToolStripMenuItem2.Text = "红";
//            this.gridcolor_RedToolStripMenuItem2.Click += new System.EventHandler(this.gridcolor_RedToolStripMenuItem2_Click);
//            this.gridcolor_GreenToolStripMenuItem2.Name = "gridcolor_GreenToolStripMenuItem2";
//            this.gridcolor_GreenToolStripMenuItem2.Size = new System.Drawing.Size(88, 22);
//            this.gridcolor_GreenToolStripMenuItem2.Text = "绿";
//            this.gridcolor_GreenToolStripMenuItem2.Click += new System.EventHandler(this.gridcolor_GreenToolStripMenuItem2_Click);
//            this.gridcolor_YellowToolStripMenuItem2.Name = "gridcolor_YellowToolStripMenuItem2";
//            this.gridcolor_YellowToolStripMenuItem2.Size = new System.Drawing.Size(88, 22);
//            this.gridcolor_YellowToolStripMenuItem2.Text = "黄";
//            this.gridcolor_YellowToolStripMenuItem2.Click += new System.EventHandler(this.gridcolor_YellowToolStripMenuItem2_Click);
//            this.gridcolor_BlackToolStripMenuItem1.Name = "gridcolor_BlackToolStripMenuItem1";
//            this.gridcolor_BlackToolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
//            this.gridcolor_BlackToolStripMenuItem1.Text = "黑";
//            this.gridcolor_BlackToolStripMenuItem1.Click += new System.EventHandler(this.gridcolor_BlackToolStripMenuItem1_Click);
//            this.bordercolorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.bordercolor_RedToolStripMenuItem3,
//                this.bordercolor_GreenToolStripMenuItem3,
//                this.bordercolor_YellowToolStripMenuItem3,
//                this.bordercolor_BlackToolStripMenuItem2
//            });
//            this.bordercolorToolStripMenuItem.Name = "bordercolorToolStripMenuItem";
//            this.bordercolorToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.bordercolorToolStripMenuItem.Text = "边框颜色";
//            this.bordercolor_RedToolStripMenuItem3.Name = "bordercolor_RedToolStripMenuItem3";
//            this.bordercolor_RedToolStripMenuItem3.Size = new System.Drawing.Size(88, 22);
//            this.bordercolor_RedToolStripMenuItem3.Text = "红";
//            this.bordercolor_RedToolStripMenuItem3.Click += new System.EventHandler(this.bordercolor_RedToolStripMenuItem3_Click);
//            this.bordercolor_GreenToolStripMenuItem3.Name = "bordercolor_GreenToolStripMenuItem3";
//            this.bordercolor_GreenToolStripMenuItem3.Size = new System.Drawing.Size(88, 22);
//            this.bordercolor_GreenToolStripMenuItem3.Text = "绿";
//            this.bordercolor_GreenToolStripMenuItem3.Click += new System.EventHandler(this.bordercolor_GreenToolStripMenuItem3_Click);
//            this.bordercolor_YellowToolStripMenuItem3.Name = "bordercolor_YellowToolStripMenuItem3";
//            this.bordercolor_YellowToolStripMenuItem3.Size = new System.Drawing.Size(88, 22);
//            this.bordercolor_YellowToolStripMenuItem3.Text = "黄";
//            this.bordercolor_YellowToolStripMenuItem3.Click += new System.EventHandler(this.bordercolor_YellowToolStripMenuItem3_Click);
//            this.bordercolor_BlackToolStripMenuItem2.Name = "bordercolor_BlackToolStripMenuItem2";
//            this.bordercolor_BlackToolStripMenuItem2.Size = new System.Drawing.Size(88, 22);
//            this.bordercolor_BlackToolStripMenuItem2.Text = "黑";
//            this.bordercolor_BlackToolStripMenuItem2.Click += new System.EventHandler(this.bordercolor_BlackToolStripMenuItem2_Click);
//            this.wrapTextToolStripMenuItem.Name = "wrapTextToolStripMenuItem";
//            this.wrapTextToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.wrapTextToolStripMenuItem.Text = "设置自动换行";
//            this.wrapTextToolStripMenuItem.Click += new System.EventHandler(this.wrapTextToolStripMenuItem_Click);
//            this.nowrapTextToolStripMenuItem.Name = "nowrapTextToolStripMenuItem";
//            this.nowrapTextToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.nowrapTextToolStripMenuItem.Text = "取消自动换行";
//            this.nowrapTextToolStripMenuItem.Click += new System.EventHandler(this.nowrapTextToolStripMenuItem_Click);
//            this.readonlyToolStripMenuItem.Name = "readonlyToolStripMenuItem";
//            this.readonlyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.readonlyToolStripMenuItem.Text = "设置单元格只读";
//            this.readonlyToolStripMenuItem.Click += new System.EventHandler(this.readonlyToolStripMenuItem_Click);
//            this.noreadonlyToolStripMenuItem.Name = "noreadonlyToolStripMenuItem";
//            this.noreadonlyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.noreadonlyToolStripMenuItem.Text = "取消单元格只读";
//            this.noreadonlyToolStripMenuItem.Click += new System.EventHandler(this.noreadonlyToolStripMenuItem_Click);
//            this.zishiToolStripMenuItem.Name = "zishiToolStripMenuItem";
//            this.zishiToolStripMenuItem.Size = new System.Drawing.Size(104, 21);
//            this.zishiToolStripMenuItem.Text = "自适应内容高度";
//            this.zishiToolStripMenuItem.Click += new System.EventHandler(this.zishiToolStripMenuItem_Click);
//            this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
//            this.panel1.Controls.Add(this.panel3);
//            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
//            this.panel1.Location = new System.Drawing.Point(0, 0);
//            this.panel1.Name = "panel1";
//            this.panel1.Size = new System.Drawing.Size(1007, 317);
//            this.panel1.TabIndex = 1;
//            this.panel3.BackColor = System.Drawing.SystemColors.ScrollBar;
//            this.panel3.Controls.Add(this.progressBar1);
//            this.panel3.Controls.Add(this.label_Percent);
//            this.panel3.Controls.Add(this.button_Cancel);
//            this.panel3.Location = new System.Drawing.Point(254, 11);
//            this.panel3.Name = "panel3";
//            this.panel3.Size = new System.Drawing.Size(313, 92);
//            this.panel3.TabIndex = 0;
//            this.panel3.Visible = false;
//            this.progressBar1.Location = new System.Drawing.Point(3, 3);
//            this.progressBar1.Name = "progressBar1";
//            this.progressBar1.Size = new System.Drawing.Size(307, 23);
//            this.progressBar1.TabIndex = 2;
//            this.label_Percent.AutoSize = true;
//            this.label_Percent.BackColor = System.Drawing.Color.Transparent;
//            this.label_Percent.Location = new System.Drawing.Point(121, 33);
//            this.label_Percent.Name = "label_Percent";
//            this.label_Percent.Size = new System.Drawing.Size(53, 12);
//            this.label_Percent.TabIndex = 1;
//            this.label_Percent.Text = "        ";
//            this.button_Cancel.Location = new System.Drawing.Point(113, 56);
//            this.button_Cancel.Name = "button_Cancel";
//            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
//            this.button_Cancel.TabIndex = 0;
//            this.button_Cancel.Text = "取消";
//            this.button_Cancel.UseVisualStyleBackColor = true;
//            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
//            this.openFileDialog1.FileName = "openFileDialog1";
//            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.编辑ToolStripMenuItem,
//                this.属性ToolStripMenuItem,
//                this.边框ToolStripMenuItem,
//                this.对齐方式ToolStripMenuItem,
//                this.toolStripSeparator28,
//                this.toolStripMenuItem1,
//                this.背景颜色ToolStripMenuItem,
//                this.toolStripMenuItem2,
//                this.toolStripMenuItem5,
//                this.toolStripSeparator15,
//                this.设置自动换行ToolStripMenuItem,
//                this.取消自动换行ToolStripMenuItem,
//                this.toolStripSeparator16,
//                this.设置单元格只读ToolStripMenuItem,
//                this.取消单元格只读ToolStripMenuItem,
//                this.toolStripSeparator17
//            });
//            this.contextMenuStrip1.Name = "contextMenuStrip1";
//            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 292);
//            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.剪切ToolStripMenuItem,
//                this.复制ToolStripMenuItem,
//                this.粘贴ToolStripMenuItem,
//                this.toolStripSeparator11,
//                this.splitGridToolStripMenuItem,
//                this.取消合并单元格ToolStripMenuItem,
//                this.toolStripSeparator12,
//                this.清除ToolStripMenuItem,
//                this.toolStripSeparator13,
//                this.插入行ToolStripMenuItem,
//                this.插入列ToolStripMenuItem,
//                this.toolStripSeparator14,
//                this.删除行ToolStripMenuItem,
//                this.删除列ToolStripMenuItem
//            });
//            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
//            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.编辑ToolStripMenuItem.Text = "编辑";
//            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
//            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.剪切ToolStripMenuItem.Text = "剪切";
//            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
//            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
//            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.复制ToolStripMenuItem.Text = "复制";
//            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
//            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
//            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.粘贴ToolStripMenuItem.Text = "粘贴";
//            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
//            this.toolStripSeparator11.Name = "toolStripSeparator11";
//            this.toolStripSeparator11.Size = new System.Drawing.Size(157, 6);
//            this.splitGridToolStripMenuItem.Name = "splitGridToolStripMenuItem";
//            this.splitGridToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.splitGridToolStripMenuItem.Text = "合并单元格";
//            this.splitGridToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
//            this.取消合并单元格ToolStripMenuItem.Name = "取消合并单元格ToolStripMenuItem";
//            this.取消合并单元格ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.取消合并单元格ToolStripMenuItem.Text = "取消合并单元格";
//            this.取消合并单元格ToolStripMenuItem.Click += new System.EventHandler(this.noMergeToolStripMenuItem_Click);
//            this.toolStripSeparator12.Name = "toolStripSeparator12";
//            this.toolStripSeparator12.Size = new System.Drawing.Size(157, 6);
//            this.清除ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.清除内容ToolStripMenuItem,
//                this.清除格式ToolStripMenuItem,
//                this.清除格式ToolStripMenuItem1
//            });
//            this.清除ToolStripMenuItem.Name = "清除ToolStripMenuItem";
//            this.清除ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.清除ToolStripMenuItem.Text = "清除";
//            this.清除ToolStripMenuItem.Click += new System.EventHandler(this.clearContentToolStripMenuItem_Click);
//            this.清除内容ToolStripMenuItem.Name = "清除内容ToolStripMenuItem";
//            this.清除内容ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
//            this.清除内容ToolStripMenuItem.Text = "清除内容";
//            this.清除格式ToolStripMenuItem.Name = "清除格式ToolStripMenuItem";
//            this.清除格式ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
//            this.清除格式ToolStripMenuItem.Text = "清除格式";
//            this.清除格式ToolStripMenuItem.Click += new System.EventHandler(this.clearFormatToolStripMenuItem_Click);
//            this.清除格式ToolStripMenuItem1.Name = "清除格式ToolStripMenuItem1";
//            this.清除格式ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
//            this.清除格式ToolStripMenuItem1.Text = "清除全部";
//            this.清除格式ToolStripMenuItem1.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
//            this.toolStripSeparator13.Name = "toolStripSeparator13";
//            this.toolStripSeparator13.Size = new System.Drawing.Size(157, 6);
//            this.插入行ToolStripMenuItem.Name = "插入行ToolStripMenuItem";
//            this.插入行ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.插入行ToolStripMenuItem.Text = "插入行";
//            this.插入行ToolStripMenuItem.Click += new System.EventHandler(this.insertRowToolStripMenuItem_Click);
//            this.插入列ToolStripMenuItem.Name = "插入列ToolStripMenuItem";
//            this.插入列ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.插入列ToolStripMenuItem.Text = "插入列";
//            this.插入列ToolStripMenuItem.Click += new System.EventHandler(this.insertColumnToolStripMenuItem_Click);
//            this.toolStripSeparator14.Name = "toolStripSeparator14";
//            this.toolStripSeparator14.Size = new System.Drawing.Size(157, 6);
//            this.删除行ToolStripMenuItem.Name = "删除行ToolStripMenuItem";
//            this.删除行ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.删除行ToolStripMenuItem.Text = "删除行";
//            this.删除行ToolStripMenuItem.Click += new System.EventHandler(this.removeRowToolStripMenuItem_Click);
//            this.删除列ToolStripMenuItem.Name = "删除列ToolStripMenuItem";
//            this.删除列ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.删除列ToolStripMenuItem.Text = "删除列";
//            this.删除列ToolStripMenuItem.Click += new System.EventHandler(this.removeColumnToolStripMenuItem_Click);
//            this.属性ToolStripMenuItem.Name = "属性ToolStripMenuItem";
//            this.属性ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.属性ToolStripMenuItem.Text = "字体";
//            this.属性ToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem1_Click);
//            this.边框ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.无边框ToolStripMenuItem,
//                this.左边框ToolStripMenuItem,
//                this.右边框ToolStripMenuItem,
//                this.上边框ToolStripMenuItem,
//                this.下边框ToolStripMenuItem,
//                this.toolStripSeparator26,
//                this.正斜线ToolStripMenuItem,
//                this.反斜线ToolStripMenuItem,
//                this.toolStripSeparator27,
//                this.内部框线ToolStripMenuItem,
//                this.外部框线细ToolStripMenuItem,
//                this.外部框线粗ToolStripMenuItem,
//                this.全部框线ToolStripMenuItem
//            });
//            this.边框ToolStripMenuItem.Name = "边框ToolStripMenuItem";
//            this.边框ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.边框ToolStripMenuItem.Text = "边框";
//            this.无边框ToolStripMenuItem.Name = "无边框ToolStripMenuItem";
//            this.无边框ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.无边框ToolStripMenuItem.Text = "无边框";
//            this.无边框ToolStripMenuItem.Click += new System.EventHandler(this.borderNoneToolStripMenuItem_Click);
//            this.左边框ToolStripMenuItem.Name = "左边框ToolStripMenuItem";
//            this.左边框ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.左边框ToolStripMenuItem.Text = "左边框";
//            this.左边框ToolStripMenuItem.Click += new System.EventHandler(this.borderLeftToolStripMenuItem_Click);
//            this.右边框ToolStripMenuItem.Name = "右边框ToolStripMenuItem";
//            this.右边框ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.右边框ToolStripMenuItem.Text = "右边框";
//            this.右边框ToolStripMenuItem.Click += new System.EventHandler(this.borderRightToolStripMenuItem_Click);
//            this.上边框ToolStripMenuItem.Name = "上边框ToolStripMenuItem";
//            this.上边框ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.上边框ToolStripMenuItem.Text = "上边框";
//            this.上边框ToolStripMenuItem.Click += new System.EventHandler(this.borderTopToolStripMenuItem_Click);
//            this.下边框ToolStripMenuItem.Name = "下边框ToolStripMenuItem";
//            this.下边框ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.下边框ToolStripMenuItem.Text = "下边框";
//            this.下边框ToolStripMenuItem.Click += new System.EventHandler(this.borderBottomToolStripMenuItem_Click);
//            this.toolStripSeparator26.Name = "toolStripSeparator26";
//            this.toolStripSeparator26.Size = new System.Drawing.Size(157, 6);
//            this.正斜线ToolStripMenuItem.Name = "正斜线ToolStripMenuItem";
//            this.正斜线ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.正斜线ToolStripMenuItem.Text = "正斜线";
//            this.正斜线ToolStripMenuItem.Click += new System.EventHandler(this.borderLineUpToolStripMenuItem_Click);
//            this.反斜线ToolStripMenuItem.Name = "反斜线ToolStripMenuItem";
//            this.反斜线ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.反斜线ToolStripMenuItem.Text = "反斜线";
//            this.反斜线ToolStripMenuItem.Click += new System.EventHandler(this.borderLineDownToolStripMenuItem_Click);
//            this.toolStripSeparator27.Name = "toolStripSeparator27";
//            this.toolStripSeparator27.Size = new System.Drawing.Size(157, 6);
//            this.内部框线ToolStripMenuItem.Name = "内部框线ToolStripMenuItem";
//            this.内部框线ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.内部框线ToolStripMenuItem.Text = "内部框线";
//            this.内部框线ToolStripMenuItem.Click += new System.EventHandler(this.borderInsideToolStripMenuItem_Click);
//            this.外部框线细ToolStripMenuItem.Name = "外部框线细ToolStripMenuItem";
//            this.外部框线细ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.外部框线细ToolStripMenuItem.Text = "外部框线（细）";
//            this.外部框线细ToolStripMenuItem.Click += new System.EventHandler(this.borderOutsideThinToolStripMenuItem_Click);
//            this.外部框线粗ToolStripMenuItem.Name = "外部框线粗ToolStripMenuItem";
//            this.外部框线粗ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.外部框线粗ToolStripMenuItem.Text = "外部框线（粗）";
//            this.外部框线粗ToolStripMenuItem.Click += new System.EventHandler(this.borderOutsideStickToolStripMenuItem_Click);
//            this.全部框线ToolStripMenuItem.Name = "全部框线ToolStripMenuItem";
//            this.全部框线ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.全部框线ToolStripMenuItem.Text = "全部框线";
//            this.全部框线ToolStripMenuItem.Click += new System.EventHandler(this.borderAllToolStripMenuItem1_Click);
//            this.对齐方式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.常规ToolStripMenuItem,
//                this.顶部对齐ToolStripMenuItem,
//                this.中间对齐ToolStripMenuItem,
//                this.底部对齐ToolStripMenuItem,
//                this.toolStripSeparator23,
//                this.左对齐ToolStripMenuItem,
//                this.顶部左对齐ToolStripMenuItem,
//                this.中间左对齐ToolStripMenuItem,
//                this.底部左对齐ToolStripMenuItem,
//                this.toolStripSeparator24,
//                this.中对齐ToolStripMenuItem,
//                this.顶部中对齐ToolStripMenuItem,
//                this.中心对齐ToolStripMenuItem,
//                this.底部中对齐ToolStripMenuItem,
//                this.toolStripSeparator25,
//                this.右对齐ToolStripMenuItem,
//                this.顶部右对齐ToolStripMenuItem,
//                this.中间右对齐ToolStripMenuItem,
//                this.底部右对齐ToolStripMenuItem
//            });
//            this.对齐方式ToolStripMenuItem.Name = "对齐方式ToolStripMenuItem";
//            this.对齐方式ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.对齐方式ToolStripMenuItem.Text = "对齐方式";
//            this.常规ToolStripMenuItem.Name = "常规ToolStripMenuItem";
//            this.常规ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.常规ToolStripMenuItem.Text = "常规";
//            this.常规ToolStripMenuItem.Click += new System.EventHandler(this.alignNormalToolStripMenuItem_Click);
//            this.顶部对齐ToolStripMenuItem.Name = "顶部对齐ToolStripMenuItem";
//            this.顶部对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.顶部对齐ToolStripMenuItem.Text = "顶部对齐";
//            this.顶部对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignTopToolStripMenuItem_Click);
//            this.中间对齐ToolStripMenuItem.Name = "中间对齐ToolStripMenuItem";
//            this.中间对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.中间对齐ToolStripMenuItem.Text = "中间对齐";
//            this.中间对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleToolStripMenuItem_Click);
//            this.底部对齐ToolStripMenuItem.Name = "底部对齐ToolStripMenuItem";
//            this.底部对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.底部对齐ToolStripMenuItem.Text = "底部对齐";
//            this.底部对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignBottomToolStripMenuItem_Click);
//            this.toolStripSeparator23.Name = "toolStripSeparator23";
//            this.toolStripSeparator23.Size = new System.Drawing.Size(133, 6);
//            this.左对齐ToolStripMenuItem.Name = "左对齐ToolStripMenuItem";
//            this.左对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.左对齐ToolStripMenuItem.Text = "左对齐";
//            this.左对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignLeftToolStripMenuItem_Click);
//            this.顶部左对齐ToolStripMenuItem.Name = "顶部左对齐ToolStripMenuItem";
//            this.顶部左对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.顶部左对齐ToolStripMenuItem.Text = "顶部左对齐";
//            this.顶部左对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignTopLeftToolStripMenuItem_Click);
//            this.中间左对齐ToolStripMenuItem.Name = "中间左对齐ToolStripMenuItem";
//            this.中间左对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.中间左对齐ToolStripMenuItem.Text = "中间左对齐";
//            this.中间左对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleLeftToolStripMenuItem_Click);
//            this.底部左对齐ToolStripMenuItem.Name = "底部左对齐ToolStripMenuItem";
//            this.底部左对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.底部左对齐ToolStripMenuItem.Text = "底部左对齐";
//            this.底部左对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignBottomLeftToolStripMenuItem_Click);
//            this.toolStripSeparator24.Name = "toolStripSeparator24";
//            this.toolStripSeparator24.Size = new System.Drawing.Size(133, 6);
//            this.中对齐ToolStripMenuItem.Name = "中对齐ToolStripMenuItem";
//            this.中对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.中对齐ToolStripMenuItem.Text = "中对齐";
//            this.中对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignCenterToolStripMenuItem_Click);
//            this.顶部中对齐ToolStripMenuItem.Name = "顶部中对齐ToolStripMenuItem";
//            this.顶部中对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.顶部中对齐ToolStripMenuItem.Text = "顶部中对齐";
//            this.顶部中对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignTopCenterToolStripMenuItem_Click);
//            this.中心对齐ToolStripMenuItem.Name = "中心对齐ToolStripMenuItem";
//            this.中心对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.中心对齐ToolStripMenuItem.Text = "中心对齐";
//            this.中心对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleCenterToolStripMenuItem_Click);
//            this.底部中对齐ToolStripMenuItem.Name = "底部中对齐ToolStripMenuItem";
//            this.底部中对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.底部中对齐ToolStripMenuItem.Text = "底部中对齐";
//            this.底部中对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignBottomCenterToolStripMenuItem_Click);
//            this.toolStripSeparator25.Name = "toolStripSeparator25";
//            this.toolStripSeparator25.Size = new System.Drawing.Size(133, 6);
//            this.右对齐ToolStripMenuItem.Name = "右对齐ToolStripMenuItem";
//            this.右对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.右对齐ToolStripMenuItem.Text = "右对齐";
//            this.右对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignRightToolStripMenuItem_Click);
//            this.顶部右对齐ToolStripMenuItem.Name = "顶部右对齐ToolStripMenuItem";
//            this.顶部右对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.顶部右对齐ToolStripMenuItem.Text = "顶部右对齐";
//            this.顶部右对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignTopRightToolStripMenuItem_Click);
//            this.中间右对齐ToolStripMenuItem.Name = "中间右对齐ToolStripMenuItem";
//            this.中间右对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.中间右对齐ToolStripMenuItem.Text = "中间右对齐";
//            this.中间右对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignMiddleRightToolStripMenuItem_Click);
//            this.底部右对齐ToolStripMenuItem.Name = "底部右对齐ToolStripMenuItem";
//            this.底部右对齐ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
//            this.底部右对齐ToolStripMenuItem.Text = "底部右对齐";
//            this.底部右对齐ToolStripMenuItem.Click += new System.EventHandler(this.alignBottomRightToolStripMenuItem_Click);
//            this.toolStripSeparator28.Name = "toolStripSeparator28";
//            this.toolStripSeparator28.Size = new System.Drawing.Size(157, 6);
//            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.红ToolStripMenuItem,
//                this.绿ToolStripMenuItem,
//                this.黄ToolStripMenuItem
//            });
//            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
//            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
//            this.toolStripMenuItem1.Text = "文字颜色";
//            this.红ToolStripMenuItem.Name = "红ToolStripMenuItem";
//            this.红ToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
//            this.红ToolStripMenuItem.Text = "红";
//            this.红ToolStripMenuItem.Click += new System.EventHandler(this.foreColor_RedToolStripMenuItem_Click);
//            this.绿ToolStripMenuItem.Name = "绿ToolStripMenuItem";
//            this.绿ToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
//            this.绿ToolStripMenuItem.Text = "绿";
//            this.绿ToolStripMenuItem.Click += new System.EventHandler(this.foreColor_GreenToolStripMenuItem_Click);
//            this.黄ToolStripMenuItem.Name = "黄ToolStripMenuItem";
//            this.黄ToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
//            this.黄ToolStripMenuItem.Text = "黄";
//            this.黄ToolStripMenuItem.Click += new System.EventHandler(this.foreColor_YellowToolStripMenuItem_Click);
//            this.背景颜色ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.红ToolStripMenuItem1,
//                this.绿ToolStripMenuItem1,
//                this.黄ToolStripMenuItem1,
//                this.黑ToolStripMenuItem
//            });
//            this.背景颜色ToolStripMenuItem.Name = "背景颜色ToolStripMenuItem";
//            this.背景颜色ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.背景颜色ToolStripMenuItem.Text = "背景颜色";
//            this.红ToolStripMenuItem1.Name = "红ToolStripMenuItem1";
//            this.红ToolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
//            this.红ToolStripMenuItem1.Text = "红";
//            this.红ToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_RedToolStripMenuItem1_Click);
//            this.绿ToolStripMenuItem1.Name = "绿ToolStripMenuItem1";
//            this.绿ToolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
//            this.绿ToolStripMenuItem1.Text = "绿";
//            this.绿ToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_GreenToolStripMenuItem1_Click);
//            this.黄ToolStripMenuItem1.Name = "黄ToolStripMenuItem1";
//            this.黄ToolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
//            this.黄ToolStripMenuItem1.Text = "黄";
//            this.黄ToolStripMenuItem1.Click += new System.EventHandler(this.backcolor_YellowToolStripMenuItem1_Click);
//            this.黑ToolStripMenuItem.Name = "黑ToolStripMenuItem";
//            this.黑ToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
//            this.黑ToolStripMenuItem.Text = "黑";
//            this.黑ToolStripMenuItem.Click += new System.EventHandler(this.backcolor_BlackToolStripMenuItem_Click);
//            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.红ToolStripMenuItem3,
//                this.绿ToolStripMenuItem4,
//                this.黄ToolStripMenuItem4,
//                this.黑ToolStripMenuItem2
//            });
//            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
//            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 22);
//            this.toolStripMenuItem2.Text = "表格颜色";
//            this.红ToolStripMenuItem3.Name = "红ToolStripMenuItem3";
//            this.红ToolStripMenuItem3.Size = new System.Drawing.Size(88, 22);
//            this.红ToolStripMenuItem3.Text = "红";
//            this.红ToolStripMenuItem3.Click += new System.EventHandler(this.gridcolor_RedToolStripMenuItem2_Click);
//            this.绿ToolStripMenuItem4.Name = "绿ToolStripMenuItem4";
//            this.绿ToolStripMenuItem4.Size = new System.Drawing.Size(88, 22);
//            this.绿ToolStripMenuItem4.Text = "绿";
//            this.绿ToolStripMenuItem4.Click += new System.EventHandler(this.gridcolor_GreenToolStripMenuItem2_Click);
//            this.黄ToolStripMenuItem4.Name = "黄ToolStripMenuItem4";
//            this.黄ToolStripMenuItem4.Size = new System.Drawing.Size(88, 22);
//            this.黄ToolStripMenuItem4.Text = "黄";
//            this.黄ToolStripMenuItem4.Click += new System.EventHandler(this.gridcolor_YellowToolStripMenuItem2_Click);
//            this.黑ToolStripMenuItem2.Name = "黑ToolStripMenuItem2";
//            this.黑ToolStripMenuItem2.Size = new System.Drawing.Size(88, 22);
//            this.黑ToolStripMenuItem2.Text = "黑";
//            this.黑ToolStripMenuItem2.Click += new System.EventHandler(this.gridcolor_BlackToolStripMenuItem1_Click);
//            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.红ToolStripMenuItem5,
//                this.绿ToolStripMenuItem6,
//                this.黄ToolStripMenuItem6,
//                this.黑ToolStripMenuItem4
//            });
//            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
//            this.toolStripMenuItem5.Size = new System.Drawing.Size(160, 22);
//            this.toolStripMenuItem5.Text = "边框颜色";
//            this.红ToolStripMenuItem5.Name = "红ToolStripMenuItem5";
//            this.红ToolStripMenuItem5.Size = new System.Drawing.Size(88, 22);
//            this.红ToolStripMenuItem5.Text = "红";
//            this.红ToolStripMenuItem5.Click += new System.EventHandler(this.bordercolor_RedToolStripMenuItem3_Click);
//            this.绿ToolStripMenuItem6.Name = "绿ToolStripMenuItem6";
//            this.绿ToolStripMenuItem6.Size = new System.Drawing.Size(88, 22);
//            this.绿ToolStripMenuItem6.Text = "绿";
//            this.绿ToolStripMenuItem6.Click += new System.EventHandler(this.bordercolor_GreenToolStripMenuItem3_Click);
//            this.黄ToolStripMenuItem6.Name = "黄ToolStripMenuItem6";
//            this.黄ToolStripMenuItem6.Size = new System.Drawing.Size(88, 22);
//            this.黄ToolStripMenuItem6.Text = "黄";
//            this.黄ToolStripMenuItem6.Click += new System.EventHandler(this.bordercolor_YellowToolStripMenuItem3_Click);
//            this.黑ToolStripMenuItem4.Name = "黑ToolStripMenuItem4";
//            this.黑ToolStripMenuItem4.Size = new System.Drawing.Size(88, 22);
//            this.黑ToolStripMenuItem4.Text = "黑";
//            this.黑ToolStripMenuItem4.Click += new System.EventHandler(this.bordercolor_BlackToolStripMenuItem2_Click);
//            this.toolStripSeparator15.Name = "toolStripSeparator15";
//            this.toolStripSeparator15.Size = new System.Drawing.Size(157, 6);
//            this.设置自动换行ToolStripMenuItem.Name = "设置自动换行ToolStripMenuItem";
//            this.设置自动换行ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.设置自动换行ToolStripMenuItem.Text = "设置自动换行";
//            this.设置自动换行ToolStripMenuItem.Click += new System.EventHandler(this.wrapTextToolStripMenuItem_Click);
//            this.取消自动换行ToolStripMenuItem.Name = "取消自动换行ToolStripMenuItem";
//            this.取消自动换行ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.取消自动换行ToolStripMenuItem.Text = "取消自动换行";
//            this.取消自动换行ToolStripMenuItem.Click += new System.EventHandler(this.nowrapTextToolStripMenuItem_Click);
//            this.toolStripSeparator16.Name = "toolStripSeparator16";
//            this.toolStripSeparator16.Size = new System.Drawing.Size(157, 6);
//            this.设置单元格只读ToolStripMenuItem.Name = "设置单元格只读ToolStripMenuItem";
//            this.设置单元格只读ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.设置单元格只读ToolStripMenuItem.Text = "设置单元格只读";
//            this.设置单元格只读ToolStripMenuItem.Click += new System.EventHandler(this.readonlyToolStripMenuItem_Click);
//            this.取消单元格只读ToolStripMenuItem.Name = "取消单元格只读ToolStripMenuItem";
//            this.取消单元格只读ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
//            this.取消单元格只读ToolStripMenuItem.Text = "取消单元格只读";
//            this.取消单元格只读ToolStripMenuItem.Click += new System.EventHandler(this.noreadonlyToolStripMenuItem_Click);
//            this.toolStripSeparator17.Name = "toolStripSeparator17";
//            this.toolStripSeparator17.Size = new System.Drawing.Size(157, 6);
//            this.timer1.Interval = 50000;
//            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
//            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
//            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.button_New,
//                this.button_Open,
//                this.button_Save,
//                this.toolStripSeparator18,
//                this.button_boardStyle,
//                this.button_Insert_Row,
//                this.button_insert_Col,
//                this.button_delete_Row,
//                this.button_delete_Col,
//                this.toolStripSeparator19,
//                this.button_combin_Cell,
//                this.button_split_Cell,
//                this.toolStripSeparator20
//            });
//            this.toolStrip1.Location = new System.Drawing.Point(3, 26);
//            this.toolStrip1.Name = "toolStrip1";
//            this.toolStrip1.Size = new System.Drawing.Size(237, 25);
//            this.toolStrip1.TabIndex = 22;
//            this.toolStrip1.Text = "toolStrip1";
//            this.button_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_New.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_New.Name = "button_New";
//            this.button_New.Size = new System.Drawing.Size(23, 22);
//            this.button_New.Text = "toolStripButton3";
//            this.button_New.ToolTipText = "新建";
//            this.button_New.Click += new System.EventHandler(this.button_New_Click);
//            this.button_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_Open.Name = "button_Open";
//            this.button_Open.Size = new System.Drawing.Size(23, 22);
//            this.button_Open.Text = "toolStripButton2";
//            this.button_Open.ToolTipText = "打开";
//            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
//            this.button_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_Save.Name = "button_Save";
//            this.button_Save.Size = new System.Drawing.Size(23, 22);
//            this.button_Save.Text = "toolStripButton1";
//            this.button_Save.ToolTipText = "保存";
//            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
//            this.toolStripSeparator18.Name = "toolStripSeparator18";
//            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
//            this.button_boardStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_boardStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_boardStyle.Name = "button_boardStyle";
//            this.button_boardStyle.Size = new System.Drawing.Size(23, 22);
//            this.button_boardStyle.Text = "toolStripButton4";
//            this.button_boardStyle.Visible = false;
//            this.button_Insert_Row.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_Insert_Row.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_Insert_Row.Name = "button_Insert_Row";
//            this.button_Insert_Row.Size = new System.Drawing.Size(23, 22);
//            this.button_Insert_Row.Text = "toolStripButton5";
//            this.button_Insert_Row.ToolTipText = "插入行";
//            this.button_Insert_Row.Click += new System.EventHandler(this.button_Insert_Row_Click);
//            this.button_insert_Col.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_insert_Col.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_insert_Col.Name = "button_insert_Col";
//            this.button_insert_Col.Size = new System.Drawing.Size(23, 22);
//            this.button_insert_Col.Text = "toolStripButton6";
//            this.button_insert_Col.ToolTipText = "插入列";
//            this.button_insert_Col.Click += new System.EventHandler(this.button_insert_Col_Click);
//            this.button_delete_Row.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_delete_Row.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_delete_Row.Name = "button_delete_Row";
//            this.button_delete_Row.Size = new System.Drawing.Size(23, 22);
//            this.button_delete_Row.Text = "toolStripButton7";
//            this.button_delete_Row.ToolTipText = "删除行";
//            this.button_delete_Row.Click += new System.EventHandler(this.button_delete_Row_Click);
//            this.button_delete_Col.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_delete_Col.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_delete_Col.Name = "button_delete_Col";
//            this.button_delete_Col.Size = new System.Drawing.Size(23, 22);
//            this.button_delete_Col.Text = "toolStripButton8";
//            this.button_delete_Col.ToolTipText = "删除列";
//            this.button_delete_Col.Click += new System.EventHandler(this.button_delete_Col_Click);
//            this.toolStripSeparator19.Name = "toolStripSeparator19";
//            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 25);
//            this.button_combin_Cell.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_combin_Cell.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_combin_Cell.Name = "button_combin_Cell";
//            this.button_combin_Cell.Size = new System.Drawing.Size(23, 22);
//            this.button_combin_Cell.Text = "toolStripButton9";
//            this.button_combin_Cell.ToolTipText = "合并单元格";
//            this.button_combin_Cell.Click += new System.EventHandler(this.button_combin_Cell_Click);
//            this.button_split_Cell.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_split_Cell.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_split_Cell.Name = "button_split_Cell";
//            this.button_split_Cell.Size = new System.Drawing.Size(23, 22);
//            this.button_split_Cell.Text = "toolStripButton10";
//            this.button_split_Cell.ToolTipText = "拆分单元格";
//            this.button_split_Cell.Click += new System.EventHandler(this.button_split_Cell_Click);
//            this.toolStripSeparator20.Name = "toolStripSeparator20";
//            this.toolStripSeparator20.Size = new System.Drawing.Size(6, 25);
//            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
//            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.toolStripComboBox_Font,
//                this.toolStripComboBox2,
//                this.toolStripLabel_Blod,
//                this.toolStripLabel_Italic,
//                this.toolStripLabel_Underline,
//                this.button_wraptext,
//                this.toolStripButton1,
//                this.toolStripSplitButton1,
//                this.toolStripSplitButton2
//            });
//            this.toolStrip2.Location = new System.Drawing.Point(3, 51);
//            this.toolStrip2.Name = "toolStrip2";
//            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
//            this.toolStrip2.Size = new System.Drawing.Size(362, 25);
//            this.toolStrip2.TabIndex = 23;
//            this.toolStrip2.Text = "toolStrip2";
//            this.toolStripComboBox_Font.Name = "toolStripComboBox_Font";
//            this.toolStripComboBox_Font.Size = new System.Drawing.Size(121, 25);
//            this.toolStripComboBox_Font.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_Font_SelectedIndexChanged);
//            this.toolStripComboBox2.Items.AddRange(new object[]
//            {
//                "8",
//                "9",
//                "10",
//                "11",
//                "12",
//                "14",
//                "16",
//                "18",
//                "20",
//                "22",
//                "24",
//                "26",
//                "28",
//                "30",
//                "32",
//                "34",
//                "36",
//                "38",
//                "40",
//                "42",
//                "45",
//                "46",
//                "48",
//                "50",
//                "52",
//                "54",
//                "56",
//                "58",
//                "60",
//                "62",
//                "64",
//                "66",
//                "68",
//                "70",
//                "72",
//                "80",
//                "90",
//                "100",
//                "110",
//                "120",
//                "130",
//                "140",
//                "150",
//                "160",
//                "170",
//                "180",
//                "190",
//                "200"
//            });
//            this.toolStripComboBox2.Name = "toolStripComboBox2";
//            this.toolStripComboBox2.Size = new System.Drawing.Size(75, 25);
//            this.toolStripComboBox2.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox2_SelectedIndexChanged);
//            this.toolStripLabel_Blod.BackColor = System.Drawing.SystemColors.ActiveCaption;
//            this.toolStripLabel_Blod.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold);
//            this.toolStripLabel_Blod.Name = "toolStripLabel_Blod";
//            this.toolStripLabel_Blod.Size = new System.Drawing.Size(26, 22);
//            this.toolStripLabel_Blod.Text = " B ";
//            this.toolStripLabel_Blod.ToolTipText = "粗体";
//            this.toolStripLabel_Blod.Click += new System.EventHandler(this.toolStripLabel_Blod_Click);
//            this.toolStripLabel_Italic.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Italic);
//            this.toolStripLabel_Italic.Name = "toolStripLabel_Italic";
//            this.toolStripLabel_Italic.Size = new System.Drawing.Size(23, 22);
//            this.toolStripLabel_Italic.Text = " I ";
//            this.toolStripLabel_Italic.ToolTipText = "斜体";
//            this.toolStripLabel_Italic.Click += new System.EventHandler(this.toolStripLabel_Italic_Click);
//            this.toolStripLabel_Underline.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Underline);
//            this.toolStripLabel_Underline.Name = "toolStripLabel_Underline";
//            this.toolStripLabel_Underline.Size = new System.Drawing.Size(23, 22);
//            this.toolStripLabel_Underline.Text = " U ";
//            this.toolStripLabel_Underline.ToolTipText = "下划线";
//            this.toolStripLabel_Underline.Click += new System.EventHandler(this.toolStripLabel_Underline_Click);
//            this.button_wraptext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.button_wraptext.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.button_wraptext.Name = "button_wraptext";
//            this.button_wraptext.Size = new System.Drawing.Size(23, 22);
//            this.button_wraptext.Text = "toolStripButton2";
//            this.button_wraptext.ToolTipText = "自动换行";
//            this.button_wraptext.Click += new System.EventHandler(this.button_wraptext_Click);
//            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.toolStripButton1.Name = "toolStripButton1";
//            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
//            this.toolStripButton1.Text = "toolStripButton1";
//            this.toolStripButton1.ToolTipText = "对齐方式";
//            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
//            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.toolStripMenuItem_fontColor_Red,
//                this.toolStripMenuItem_fontColor_Green,
//                this.toolStripMenuItem_fontColor_Yellow,
//                this.toolStripMenuItem_fontColor_black
//            });
//            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
//            this.toolStripSplitButton1.Size = new System.Drawing.Size(16, 22);
//            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
//            this.toolStripSplitButton1.ToolTipText = "字体颜色";
//            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
//            this.toolStripMenuItem_fontColor_Red.BackColor = System.Drawing.Color.Red;
//            this.toolStripMenuItem_fontColor_Red.Name = "toolStripMenuItem_fontColor_Red";
//            this.toolStripMenuItem_fontColor_Red.Size = new System.Drawing.Size(108, 22);
//            this.toolStripMenuItem_fontColor_Red.Text = "    ";
//            this.toolStripMenuItem_fontColor_Red.Click += new System.EventHandler(this.toolStripMenuItem_fontColor_Red_Click);
//            this.toolStripMenuItem_fontColor_Green.BackColor = System.Drawing.Color.Green;
//            this.toolStripMenuItem_fontColor_Green.Name = "toolStripMenuItem_fontColor_Green";
//            this.toolStripMenuItem_fontColor_Green.Size = new System.Drawing.Size(108, 22);
//            this.toolStripMenuItem_fontColor_Green.Text = "    ";
//            this.toolStripMenuItem_fontColor_Green.Click += new System.EventHandler(this.toolStripMenuItem_fontColor_Green_Click);
//            this.toolStripMenuItem_fontColor_Yellow.BackColor = System.Drawing.Color.Yellow;
//            this.toolStripMenuItem_fontColor_Yellow.Name = "toolStripMenuItem_fontColor_Yellow";
//            this.toolStripMenuItem_fontColor_Yellow.Size = new System.Drawing.Size(108, 22);
//            this.toolStripMenuItem_fontColor_Yellow.Text = "      ";
//            this.toolStripMenuItem_fontColor_Yellow.Click += new System.EventHandler(this.toolStripMenuItem_fontColor_Yellow_Click);
//            this.toolStripMenuItem_fontColor_black.BackColor = System.Drawing.Color.Black;
//            this.toolStripMenuItem_fontColor_black.Name = "toolStripMenuItem_fontColor_black";
//            this.toolStripMenuItem_fontColor_black.Size = new System.Drawing.Size(108, 22);
//            this.toolStripMenuItem_fontColor_black.Text = "        ";
//            this.toolStripMenuItem_fontColor_black.Click += new System.EventHandler(this.toolStripMenuItem_fontColor_black_Click);
//            this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
//            this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
//            {
//                this.toolStripMenuItem_Backcolor_Red,
//                this.toolStripMenuItem_Backcolor_Green,
//                this.toolStripMenuItem_Backcolor_Yellow,
//                this.toolStripMenuItem_Backcolor_Black
//            });
//            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
//            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
//            this.toolStripSplitButton2.Size = new System.Drawing.Size(16, 22);
//            this.toolStripSplitButton2.Text = "toolStripSplitButton2";
//            this.toolStripSplitButton2.ToolTipText = "背景颜色";
//            this.toolStripMenuItem_Backcolor_Red.BackColor = System.Drawing.Color.Red;
//            this.toolStripMenuItem_Backcolor_Red.Name = "toolStripMenuItem_Backcolor_Red";
//            this.toolStripMenuItem_Backcolor_Red.Size = new System.Drawing.Size(92, 22);
//            this.toolStripMenuItem_Backcolor_Red.Text = "    ";
//            this.toolStripMenuItem_Backcolor_Red.Click += new System.EventHandler(this.toolStripMenuItem_Backcolor_Red_Click);
//            this.toolStripMenuItem_Backcolor_Green.BackColor = System.Drawing.Color.Green;
//            this.toolStripMenuItem_Backcolor_Green.Name = "toolStripMenuItem_Backcolor_Green";
//            this.toolStripMenuItem_Backcolor_Green.Size = new System.Drawing.Size(92, 22);
//            this.toolStripMenuItem_Backcolor_Green.Text = "    ";
//            this.toolStripMenuItem_Backcolor_Green.Click += new System.EventHandler(this.toolStripMenuItem_Backcolor_Green_Click);
//            this.toolStripMenuItem_Backcolor_Yellow.BackColor = System.Drawing.Color.Yellow;
//            this.toolStripMenuItem_Backcolor_Yellow.Name = "toolStripMenuItem_Backcolor_Yellow";
//            this.toolStripMenuItem_Backcolor_Yellow.Size = new System.Drawing.Size(92, 22);
//            this.toolStripMenuItem_Backcolor_Yellow.Text = "    ";
//            this.toolStripMenuItem_Backcolor_Yellow.Click += new System.EventHandler(this.toolStripMenuItem_Backcolor_Yellow_Click);
//            this.toolStripMenuItem_Backcolor_Black.BackColor = System.Drawing.Color.Black;
//            this.toolStripMenuItem_Backcolor_Black.Name = "toolStripMenuItem_Backcolor_Black";
//            this.toolStripMenuItem_Backcolor_Black.Size = new System.Drawing.Size(92, 22);
//            this.toolStripMenuItem_Backcolor_Black.Text = "    ";
//            this.toolStripMenuItem_Backcolor_Black.Click += new System.EventHandler(this.toolStripMenuItem_Backcolor_Black_Click);
//            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_RightButtom);
//            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_MiddleButtom);
//            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_RightMiddle);
//            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_MiddleMiddle);
//            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_RightTop);
//            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_MiddleTop);
//            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_LeftButtom);
//            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_LeftMiddle);
//            this.panel_Alignt.Controls.Add(this.pictureBox_Aligent_LeftTop);
//            this.panel_Alignt.Location = new System.Drawing.Point(254, 77);
//            this.panel_Alignt.Name = "panel_Alignt";
//            this.panel_Alignt.Size = new System.Drawing.Size(106, 106);
//            this.panel_Alignt.TabIndex = 24;
//            this.panel_Alignt.Visible = false;
//            this.pictureBox_Aligent_RightButtom.BackColor = System.Drawing.SystemColors.AppWorkspace;
//            this.pictureBox_Aligent_RightButtom.Location = new System.Drawing.Point(71, 70);
//            this.pictureBox_Aligent_RightButtom.Name = "pictureBox_Aligent_RightButtom";
//            this.pictureBox_Aligent_RightButtom.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox_Aligent_RightButtom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox_Aligent_RightButtom.TabIndex = 8;
//            this.pictureBox_Aligent_RightButtom.TabStop = false;
//            this.pictureBox_Aligent_RightButtom.Click += new System.EventHandler(this.pictureBox_Aligent_RightButtom_Click);
//            this.pictureBox_Aligent_MiddleButtom.BackColor = System.Drawing.SystemColors.AppWorkspace;
//            this.pictureBox_Aligent_MiddleButtom.Location = new System.Drawing.Point(37, 70);
//            this.pictureBox_Aligent_MiddleButtom.Name = "pictureBox_Aligent_MiddleButtom";
//            this.pictureBox_Aligent_MiddleButtom.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox_Aligent_MiddleButtom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox_Aligent_MiddleButtom.TabIndex = 7;
//            this.pictureBox_Aligent_MiddleButtom.TabStop = false;
//            this.pictureBox_Aligent_MiddleButtom.Click += new System.EventHandler(this.pictureBox_Aligent_MiddleButtom_Click);
//            this.pictureBox_Aligent_RightMiddle.BackColor = System.Drawing.SystemColors.AppWorkspace;
//            this.pictureBox_Aligent_RightMiddle.Location = new System.Drawing.Point(71, 36);
//            this.pictureBox_Aligent_RightMiddle.Name = "pictureBox_Aligent_RightMiddle";
//            this.pictureBox_Aligent_RightMiddle.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox_Aligent_RightMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox_Aligent_RightMiddle.TabIndex = 6;
//            this.pictureBox_Aligent_RightMiddle.TabStop = false;
//            this.pictureBox_Aligent_RightMiddle.Click += new System.EventHandler(this.pictureBox8_Click);
//            this.pictureBox_Aligent_MiddleMiddle.BackColor = System.Drawing.SystemColors.AppWorkspace;
//            this.pictureBox_Aligent_MiddleMiddle.Location = new System.Drawing.Point(37, 36);
//            this.pictureBox_Aligent_MiddleMiddle.Name = "pictureBox_Aligent_MiddleMiddle";
//            this.pictureBox_Aligent_MiddleMiddle.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox_Aligent_MiddleMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox_Aligent_MiddleMiddle.TabIndex = 5;
//            this.pictureBox_Aligent_MiddleMiddle.TabStop = false;
//            this.pictureBox_Aligent_MiddleMiddle.Click += new System.EventHandler(this.pictureBox_Aligent_MiddleMiddle_Click);
//            this.pictureBox_Aligent_RightTop.BackColor = System.Drawing.SystemColors.AppWorkspace;
//            this.pictureBox_Aligent_RightTop.Location = new System.Drawing.Point(71, 2);
//            this.pictureBox_Aligent_RightTop.Name = "pictureBox_Aligent_RightTop";
//            this.pictureBox_Aligent_RightTop.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox_Aligent_RightTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox_Aligent_RightTop.TabIndex = 4;
//            this.pictureBox_Aligent_RightTop.TabStop = false;
//            this.pictureBox_Aligent_RightTop.Click += new System.EventHandler(this.pictureBox_Aligent_RightTop_Click);
//            this.pictureBox_Aligent_MiddleTop.BackColor = System.Drawing.SystemColors.AppWorkspace;
//            this.pictureBox_Aligent_MiddleTop.Location = new System.Drawing.Point(37, 2);
//            this.pictureBox_Aligent_MiddleTop.Name = "pictureBox_Aligent_MiddleTop";
//            this.pictureBox_Aligent_MiddleTop.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox_Aligent_MiddleTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox_Aligent_MiddleTop.TabIndex = 3;
//            this.pictureBox_Aligent_MiddleTop.TabStop = false;
//            this.pictureBox_Aligent_MiddleTop.Click += new System.EventHandler(this.pictureBox_Aligent_MiddleTop_Click);
//            this.pictureBox_Aligent_LeftButtom.BackColor = System.Drawing.SystemColors.AppWorkspace;
//            this.pictureBox_Aligent_LeftButtom.Location = new System.Drawing.Point(3, 70);
//            this.pictureBox_Aligent_LeftButtom.Name = "pictureBox_Aligent_LeftButtom";
//            this.pictureBox_Aligent_LeftButtom.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox_Aligent_LeftButtom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox_Aligent_LeftButtom.TabIndex = 2;
//            this.pictureBox_Aligent_LeftButtom.TabStop = false;
//            this.pictureBox_Aligent_LeftButtom.Click += new System.EventHandler(this.pictureBox_Aligent_LeftButtom_Click);
//            this.pictureBox_Aligent_LeftMiddle.BackColor = System.Drawing.SystemColors.AppWorkspace;
//            this.pictureBox_Aligent_LeftMiddle.Location = new System.Drawing.Point(3, 36);
//            this.pictureBox_Aligent_LeftMiddle.Name = "pictureBox_Aligent_LeftMiddle";
//            this.pictureBox_Aligent_LeftMiddle.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox_Aligent_LeftMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox_Aligent_LeftMiddle.TabIndex = 1;
//            this.pictureBox_Aligent_LeftMiddle.TabStop = false;
//            this.pictureBox_Aligent_LeftMiddle.Click += new System.EventHandler(this.pictureBox_Aligent_LeftMiddle_Click);
//            this.pictureBox_Aligent_LeftTop.BackColor = System.Drawing.SystemColors.AppWorkspace;
//            this.pictureBox_Aligent_LeftTop.Location = new System.Drawing.Point(3, 2);
//            this.pictureBox_Aligent_LeftTop.Name = "pictureBox_Aligent_LeftTop";
//            this.pictureBox_Aligent_LeftTop.Size = new System.Drawing.Size(32, 32);
//            this.pictureBox_Aligent_LeftTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox_Aligent_LeftTop.TabIndex = 0;
//            this.pictureBox_Aligent_LeftTop.TabStop = false;
//            this.pictureBox_Aligent_LeftTop.Click += new System.EventHandler(this.pictureBox_Aligent_LeftTop_Click);
//            this.pictureBox_max.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
//            this.pictureBox_max.BackColor = System.Drawing.Color.Maroon;
//            this.pictureBox_max.Location = new System.Drawing.Point(958, 7);
//            this.pictureBox_max.Name = "pictureBox_max";
//            this.pictureBox_max.Size = new System.Drawing.Size(200, 200);
//            this.pictureBox_max.TabIndex = 33;
//            this.pictureBox_max.TabStop = false;
//            this.pictureBox_max.Click += new System.EventHandler(this.pictureBox_max_Click);
//            this.pictureBox2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
//            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
//            this.pictureBox2.Location = new System.Drawing.Point(952, 6);
//            this.pictureBox2.Name = "pictureBox2";
//            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
//            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox2.TabIndex = 25;
//            this.pictureBox2.TabStop = false;
//            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
//            this.panel2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
//            this.panel2.BackColor = System.Drawing.SystemColors.WindowFrame;
//            this.panel2.Controls.Add(this.panel4);
//            this.panel2.Controls.Add(this.panel1);
//            this.panel2.Location = new System.Drawing.Point(0, 80);
//            this.panel2.Name = "panel2";
//            this.panel2.Size = new System.Drawing.Size(1007, 544);
//            this.panel2.TabIndex = 2;
//            this.panel4.BackColor = System.Drawing.SystemColors.WindowFrame;
//            this.panel4.Controls.Add(this.button5);
//            this.panel4.Controls.Add(this.label1);
//            this.panel4.Controls.Add(this.button4);
//            this.panel4.Controls.Add(this.button3);
//            this.panel4.Controls.Add(this.button2);
//            this.panel4.Controls.Add(this.textBox1);
//            this.panel4.Controls.Add(this.button1);
//            this.panel4.Controls.Add(this.panel5);
//            this.panel4.Controls.Add(this.panel6);
//            this.panel4.Controls.Add(this.panel7);
//            this.panel4.Controls.Add(this.panel8);
//            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.panel4.Location = new System.Drawing.Point(0, 317);
//            this.panel4.Name = "panel4";
//            this.panel4.Size = new System.Drawing.Size(1007, 227);
//            this.panel4.TabIndex = 2;
//            this.button5.Location = new System.Drawing.Point(803, 196);
//            this.button5.Name = "button5";
//            this.button5.Size = new System.Drawing.Size(93, 23);
//            this.button5.TabIndex = 26;
//            this.button5.Text = "停止";
//            this.button5.UseVisualStyleBackColor = true;
//            this.button5.Visible = false;
//            this.button5.Click += new System.EventHandler(this.button5_Click);
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(798, 127);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(125, 12);
//            this.label1.TabIndex = 25;
//            this.label1.Text = "自动更新时间（分钟）";
//            this.button4.Location = new System.Drawing.Point(800, 14);
//            this.button4.Name = "button4";
//            this.button4.Size = new System.Drawing.Size(95, 30);
//            this.button4.TabIndex = 24;
//            this.button4.Text = "获取数据";
//            this.button4.UseVisualStyleBackColor = true;
//            this.button4.Click += new System.EventHandler(this.button4_Click);
//            this.button3.Location = new System.Drawing.Point(803, 169);
//            this.button3.Name = "button3";
//            this.button3.Size = new System.Drawing.Size(93, 24);
//            this.button3.TabIndex = 23;
//            this.button3.Text = "开始";
//            this.button3.UseVisualStyleBackColor = true;
//            this.button3.Click += new System.EventHandler(this.button3_Click);
//            this.button2.Location = new System.Drawing.Point(801, 87);
//            this.button2.Name = "button2";
//            this.button2.Size = new System.Drawing.Size(95, 28);
//            this.button2.TabIndex = 22;
//            this.button2.Text = "发送数据到LED屏";
//            this.button2.UseVisualStyleBackColor = true;
//            this.button2.Click += new System.EventHandler(this.button2_Click);
//            this.textBox1.Location = new System.Drawing.Point(803, 145);
//            this.textBox1.Name = "textBox1";
//            this.textBox1.Size = new System.Drawing.Size(92, 21);
//            this.textBox1.TabIndex = 21;
//            this.textBox1.Text = "5";
//            this.button1.Location = new System.Drawing.Point(800, 51);
//            this.button1.Name = "button1";
//            this.button1.Size = new System.Drawing.Size(95, 30);
//            this.button1.TabIndex = 20;
//            this.button1.Text = "更新数据";
//            this.button1.UseVisualStyleBackColor = true;
//            this.button1.Click += new System.EventHandler(this.button1_Click);
//            this.panel5.BackColor = System.Drawing.SystemColors.WindowFrame;
//            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
//            this.panel5.Controls.Add(this.textBox大气区湿度);
//            this.panel5.Controls.Add(this.textBox大气区温度);
//            this.panel5.Controls.Add(this.label36);
//            this.panel5.Controls.Add(this.label37);
//            this.panel5.Controls.Add(this.textBox14);
//            this.panel5.Controls.Add(this.textBox15);
//            this.panel5.Controls.Add(this.label38);
//            this.panel5.Controls.Add(this.label39);
//            this.panel5.Controls.Add(this.textBox16);
//            this.panel5.Controls.Add(this.label40);
//            this.panel5.Controls.Add(this.label41);
//            this.panel5.Controls.Add(this.textBox17);
//            this.panel5.Controls.Add(this.label44);
//            this.panel5.Controls.Add(this.label45);
//            this.panel5.Controls.Add(this.label35);
//            this.panel5.Location = new System.Drawing.Point(571, 0);
//            this.panel5.Name = "panel5";
//            this.panel5.Size = new System.Drawing.Size(184, 219);
//            this.panel5.TabIndex = 17;
//            this.textBox大气区湿度.Location = new System.Drawing.Point(78, 76);
//            this.textBox大气区湿度.Name = "textBox大气区湿度";
//            this.textBox大气区湿度.Size = new System.Drawing.Size(43, 21);
//            this.textBox大气区湿度.TabIndex = 18;
//            this.textBox大气区温度.Location = new System.Drawing.Point(78, 49);
//            this.textBox大气区温度.Name = "textBox大气区温度";
//            this.textBox大气区温度.Size = new System.Drawing.Size(43, 21);
//            this.textBox大气区温度.TabIndex = 17;
//            this.label36.AutoSize = true;
//            this.label36.Location = new System.Drawing.Point(117, 164);
//            this.label36.Name = "label36";
//            this.label36.Size = new System.Drawing.Size(17, 12);
//            this.label36.TabIndex = 14;
//            this.label36.Text = "列";
//            this.label37.AutoSize = true;
//            this.label37.Location = new System.Drawing.Point(76, 164);
//            this.label37.Name = "label37";
//            this.label37.Size = new System.Drawing.Size(17, 12);
//            this.label37.TabIndex = 13;
//            this.label37.Text = "行";
//            this.textBox14.Location = new System.Drawing.Point(119, 179);
//            this.textBox14.Name = "textBox14";
//            this.textBox14.Size = new System.Drawing.Size(39, 21);
//            this.textBox14.TabIndex = 12;
//            this.textBox14.Text = "5";
//            this.textBox15.Location = new System.Drawing.Point(74, 179);
//            this.textBox15.Name = "textBox15";
//            this.textBox15.Size = new System.Drawing.Size(39, 21);
//            this.textBox15.TabIndex = 11;
//            this.textBox15.Text = "4";
//            this.label38.AutoSize = true;
//            this.label38.Location = new System.Drawing.Point(117, 125);
//            this.label38.Name = "label38";
//            this.label38.Size = new System.Drawing.Size(17, 12);
//            this.label38.TabIndex = 10;
//            this.label38.Text = "列";
//            this.label39.AutoSize = true;
//            this.label39.Location = new System.Drawing.Point(76, 125);
//            this.label39.Name = "label39";
//            this.label39.Size = new System.Drawing.Size(17, 12);
//            this.label39.TabIndex = 9;
//            this.label39.Text = "行";
//            this.textBox16.Location = new System.Drawing.Point(119, 140);
//            this.textBox16.Name = "textBox16";
//            this.textBox16.Size = new System.Drawing.Size(39, 21);
//            this.textBox16.TabIndex = 8;
//            this.textBox16.Text = "5";
//            this.label40.AutoSize = true;
//            this.label40.Location = new System.Drawing.Point(15, 179);
//            this.label40.Name = "label40";
//            this.label40.Size = new System.Drawing.Size(59, 12);
//            this.label40.TabIndex = 7;
//            this.label40.Text = "湿度坐标:";
//            this.label41.AutoSize = true;
//            this.label41.Location = new System.Drawing.Point(15, 143);
//            this.label41.Name = "label41";
//            this.label41.Size = new System.Drawing.Size(59, 12);
//            this.label41.TabIndex = 6;
//            this.label41.Text = "温度坐标:";
//            this.textBox17.Location = new System.Drawing.Point(74, 140);
//            this.textBox17.Name = "textBox17";
//            this.textBox17.Size = new System.Drawing.Size(39, 21);
//            this.textBox17.TabIndex = 5;
//            this.textBox17.Text = "3";
//            this.label44.AutoSize = true;
//            this.label44.Location = new System.Drawing.Point(15, 80);
//            this.label44.Name = "label44";
//            this.label44.Size = new System.Drawing.Size(65, 12);
//            this.label44.TabIndex = 2;
//            this.label44.Text = "湿度(%RH):";
//            this.label45.AutoSize = true;
//            this.label45.Location = new System.Drawing.Point(15, 52);
//            this.label45.Name = "label45";
//            this.label45.Size = new System.Drawing.Size(65, 12);
//            this.label45.TabIndex = 1;
//            this.label45.Text = "温度:(℃):";
//            this.label35.AutoSize = true;
//            this.label35.Location = new System.Drawing.Point(52, 21);
//            this.label35.Name = "label35";
//            this.label35.Size = new System.Drawing.Size(41, 12);
//            this.label35.TabIndex = 0;
//            this.label35.Text = "大气区";
//            this.panel6.BackColor = System.Drawing.SystemColors.WindowFrame;
//            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
//            this.panel6.Controls.Add(this.textBox降温区湿度);
//            this.panel6.Controls.Add(this.textBox降温区温度);
//            this.panel6.Controls.Add(this.label25);
//            this.panel6.Controls.Add(this.label26);
//            this.panel6.Controls.Add(this.textBox10);
//            this.panel6.Controls.Add(this.textBox11);
//            this.panel6.Controls.Add(this.label27);
//            this.panel6.Controls.Add(this.label28);
//            this.panel6.Controls.Add(this.textBox12);
//            this.panel6.Controls.Add(this.label29);
//            this.panel6.Controls.Add(this.label30);
//            this.panel6.Controls.Add(this.textBox13);
//            this.panel6.Controls.Add(this.label33);
//            this.panel6.Controls.Add(this.label34);
//            this.panel6.Controls.Add(this.label24);
//            this.panel6.Location = new System.Drawing.Point(381, 0);
//            this.panel6.Name = "panel6";
//            this.panel6.Size = new System.Drawing.Size(184, 219);
//            this.panel6.TabIndex = 18;
//            this.textBox降温区湿度.Location = new System.Drawing.Point(78, 76);
//            this.textBox降温区湿度.Name = "textBox降温区湿度";
//            this.textBox降温区湿度.Size = new System.Drawing.Size(43, 21);
//            this.textBox降温区湿度.TabIndex = 18;
//            this.textBox降温区温度.Location = new System.Drawing.Point(78, 49);
//            this.textBox降温区温度.Name = "textBox降温区温度";
//            this.textBox降温区温度.Size = new System.Drawing.Size(43, 21);
//            this.textBox降温区温度.TabIndex = 17;
//            this.label25.AutoSize = true;
//            this.label25.Location = new System.Drawing.Point(117, 164);
//            this.label25.Name = "label25";
//            this.label25.Size = new System.Drawing.Size(17, 12);
//            this.label25.TabIndex = 14;
//            this.label25.Text = "列";
//            this.label26.AutoSize = true;
//            this.label26.Location = new System.Drawing.Point(76, 164);
//            this.label26.Name = "label26";
//            this.label26.Size = new System.Drawing.Size(17, 12);
//            this.label26.TabIndex = 13;
//            this.label26.Text = "行";
//            this.textBox10.Location = new System.Drawing.Point(119, 179);
//            this.textBox10.Name = "textBox10";
//            this.textBox10.Size = new System.Drawing.Size(39, 21);
//            this.textBox10.TabIndex = 12;
//            this.textBox10.Text = "4";
//            this.textBox11.Location = new System.Drawing.Point(74, 179);
//            this.textBox11.Name = "textBox11";
//            this.textBox11.Size = new System.Drawing.Size(39, 21);
//            this.textBox11.TabIndex = 11;
//            this.textBox11.Text = "4";
//            this.label27.AutoSize = true;
//            this.label27.Location = new System.Drawing.Point(117, 125);
//            this.label27.Name = "label27";
//            this.label27.Size = new System.Drawing.Size(17, 12);
//            this.label27.TabIndex = 10;
//            this.label27.Text = "列";
//            this.label28.AutoSize = true;
//            this.label28.Location = new System.Drawing.Point(76, 125);
//            this.label28.Name = "label28";
//            this.label28.Size = new System.Drawing.Size(17, 12);
//            this.label28.TabIndex = 9;
//            this.label28.Text = "行";
//            this.textBox12.Location = new System.Drawing.Point(119, 140);
//            this.textBox12.Name = "textBox12";
//            this.textBox12.Size = new System.Drawing.Size(39, 21);
//            this.textBox12.TabIndex = 8;
//            this.textBox12.Text = "4";
//            this.label29.AutoSize = true;
//            this.label29.Location = new System.Drawing.Point(15, 179);
//            this.label29.Name = "label29";
//            this.label29.Size = new System.Drawing.Size(59, 12);
//            this.label29.TabIndex = 7;
//            this.label29.Text = "湿度坐标:";
//            this.label30.AutoSize = true;
//            this.label30.Location = new System.Drawing.Point(15, 143);
//            this.label30.Name = "label30";
//            this.label30.Size = new System.Drawing.Size(59, 12);
//            this.label30.TabIndex = 6;
//            this.label30.Text = "温度坐标:";
//            this.textBox13.Location = new System.Drawing.Point(74, 140);
//            this.textBox13.Name = "textBox13";
//            this.textBox13.Size = new System.Drawing.Size(39, 21);
//            this.textBox13.TabIndex = 5;
//            this.textBox13.Text = "3";
//            this.label33.AutoSize = true;
//            this.label33.Location = new System.Drawing.Point(15, 80);
//            this.label33.Name = "label33";
//            this.label33.Size = new System.Drawing.Size(65, 12);
//            this.label33.TabIndex = 2;
//            this.label33.Text = "湿度(%RH):";
//            this.label34.AutoSize = true;
//            this.label34.Location = new System.Drawing.Point(15, 52);
//            this.label34.Name = "label34";
//            this.label34.Size = new System.Drawing.Size(65, 12);
//            this.label34.TabIndex = 1;
//            this.label34.Text = "温度:(℃):";
//            this.label24.AutoSize = true;
//            this.label24.Location = new System.Drawing.Point(52, 21);
//            this.label24.Name = "label24";
//            this.label24.Size = new System.Drawing.Size(41, 12);
//            this.label24.TabIndex = 0;
//            this.label24.Text = "降温区";
//            this.panel7.BackColor = System.Drawing.SystemColors.WindowFrame;
//            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
//            this.panel7.Controls.Add(this.textBox恒温区湿度);
//            this.panel7.Controls.Add(this.textBox恒温区温度);
//            this.panel7.Controls.Add(this.label4);
//            this.panel7.Controls.Add(this.label5);
//            this.panel7.Controls.Add(this.textBox6);
//            this.panel7.Controls.Add(this.textBox7);
//            this.panel7.Controls.Add(this.label16);
//            this.panel7.Controls.Add(this.label17);
//            this.panel7.Controls.Add(this.textBox8);
//            this.panel7.Controls.Add(this.label18);
//            this.panel7.Controls.Add(this.label19);
//            this.panel7.Controls.Add(this.textBox9);
//            this.panel7.Controls.Add(this.label22);
//            this.panel7.Controls.Add(this.label23);
//            this.panel7.Controls.Add(this.label3);
//            this.panel7.Location = new System.Drawing.Point(191, 0);
//            this.panel7.Name = "panel7";
//            this.panel7.Size = new System.Drawing.Size(184, 219);
//            this.panel7.TabIndex = 19;
//            this.textBox恒温区湿度.Location = new System.Drawing.Point(78, 76);
//            this.textBox恒温区湿度.Name = "textBox恒温区湿度";
//            this.textBox恒温区湿度.Size = new System.Drawing.Size(43, 21);
//            this.textBox恒温区湿度.TabIndex = 18;
//            this.textBox恒温区温度.Location = new System.Drawing.Point(78, 49);
//            this.textBox恒温区温度.Name = "textBox恒温区温度";
//            this.textBox恒温区温度.Size = new System.Drawing.Size(43, 21);
//            this.textBox恒温区温度.TabIndex = 17;
//            this.label4.AutoSize = true;
//            this.label4.Location = new System.Drawing.Point(117, 164);
//            this.label4.Name = "label4";
//            this.label4.Size = new System.Drawing.Size(17, 12);
//            this.label4.TabIndex = 14;
//            this.label4.Text = "列";
//            this.label5.AutoSize = true;
//            this.label5.Location = new System.Drawing.Point(76, 164);
//            this.label5.Name = "label5";
//            this.label5.Size = new System.Drawing.Size(17, 12);
//            this.label5.TabIndex = 13;
//            this.label5.Text = "行";
//            this.textBox6.Location = new System.Drawing.Point(119, 179);
//            this.textBox6.Name = "textBox6";
//            this.textBox6.Size = new System.Drawing.Size(39, 21);
//            this.textBox6.TabIndex = 12;
//            this.textBox6.Text = "3";
//            this.textBox7.Location = new System.Drawing.Point(74, 179);
//            this.textBox7.Name = "textBox7";
//            this.textBox7.Size = new System.Drawing.Size(39, 21);
//            this.textBox7.TabIndex = 11;
//            this.textBox7.Text = "4";
//            this.label16.AutoSize = true;
//            this.label16.Location = new System.Drawing.Point(117, 125);
//            this.label16.Name = "label16";
//            this.label16.Size = new System.Drawing.Size(17, 12);
//            this.label16.TabIndex = 10;
//            this.label16.Text = "列";
//            this.label17.AutoSize = true;
//            this.label17.Location = new System.Drawing.Point(76, 125);
//            this.label17.Name = "label17";
//            this.label17.Size = new System.Drawing.Size(17, 12);
//            this.label17.TabIndex = 9;
//            this.label17.Text = "行";
//            this.textBox8.Location = new System.Drawing.Point(119, 140);
//            this.textBox8.Name = "textBox8";
//            this.textBox8.Size = new System.Drawing.Size(39, 21);
//            this.textBox8.TabIndex = 8;
//            this.textBox8.Text = "3";
//            this.label18.AutoSize = true;
//            this.label18.Location = new System.Drawing.Point(15, 179);
//            this.label18.Name = "label18";
//            this.label18.Size = new System.Drawing.Size(59, 12);
//            this.label18.TabIndex = 7;
//            this.label18.Text = "湿度坐标:";
//            this.label19.AutoSize = true;
//            this.label19.Location = new System.Drawing.Point(15, 143);
//            this.label19.Name = "label19";
//            this.label19.Size = new System.Drawing.Size(59, 12);
//            this.label19.TabIndex = 6;
//            this.label19.Text = "温度坐标:";
//            this.textBox9.Location = new System.Drawing.Point(74, 140);
//            this.textBox9.Name = "textBox9";
//            this.textBox9.Size = new System.Drawing.Size(39, 21);
//            this.textBox9.TabIndex = 5;
//            this.textBox9.Text = "3";
//            this.label22.AutoSize = true;
//            this.label22.Location = new System.Drawing.Point(15, 80);
//            this.label22.Name = "label22";
//            this.label22.Size = new System.Drawing.Size(65, 12);
//            this.label22.TabIndex = 2;
//            this.label22.Text = "湿度(%RH):";
//            this.label23.AutoSize = true;
//            this.label23.Location = new System.Drawing.Point(15, 52);
//            this.label23.Name = "label23";
//            this.label23.Size = new System.Drawing.Size(65, 12);
//            this.label23.TabIndex = 1;
//            this.label23.Text = "温度:(℃):";
//            this.label3.AutoSize = true;
//            this.label3.Location = new System.Drawing.Point(52, 21);
//            this.label3.Name = "label3";
//            this.label3.Size = new System.Drawing.Size(41, 12);
//            this.label3.TabIndex = 0;
//            this.label3.Text = "恒温区";
//            this.panel8.BackColor = System.Drawing.SystemColors.WindowFrame;
//            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
//            this.panel8.Controls.Add(this.textBox升温区湿度);
//            this.panel8.Controls.Add(this.textBox升温区温度);
//            this.panel8.Controls.Add(this.label14);
//            this.panel8.Controls.Add(this.label15);
//            this.panel8.Controls.Add(this.textBox4);
//            this.panel8.Controls.Add(this.textBox5);
//            this.panel8.Controls.Add(this.label13);
//            this.panel8.Controls.Add(this.label12);
//            this.panel8.Controls.Add(this.textBox3);
//            this.panel8.Controls.Add(this.label11);
//            this.panel8.Controls.Add(this.label10);
//            this.panel8.Controls.Add(this.textBox2);
//            this.panel8.Controls.Add(this.label7);
//            this.panel8.Controls.Add(this.label6);
//            this.panel8.Controls.Add(this.label2);
//            this.panel8.Location = new System.Drawing.Point(1, 0);
//            this.panel8.Name = "panel8";
//            this.panel8.Size = new System.Drawing.Size(184, 219);
//            this.panel8.TabIndex = 16;
//            this.textBox升温区湿度.Location = new System.Drawing.Point(78, 76);
//            this.textBox升温区湿度.Name = "textBox升温区湿度";
//            this.textBox升温区湿度.Size = new System.Drawing.Size(43, 21);
//            this.textBox升温区湿度.TabIndex = 16;
//            this.textBox升温区温度.Location = new System.Drawing.Point(78, 49);
//            this.textBox升温区温度.Name = "textBox升温区温度";
//            this.textBox升温区温度.Size = new System.Drawing.Size(43, 21);
//            this.textBox升温区温度.TabIndex = 15;
//            this.label14.AutoSize = true;
//            this.label14.Location = new System.Drawing.Point(117, 164);
//            this.label14.Name = "label14";
//            this.label14.Size = new System.Drawing.Size(17, 12);
//            this.label14.TabIndex = 14;
//            this.label14.Text = "列";
//            this.label15.AutoSize = true;
//            this.label15.Location = new System.Drawing.Point(76, 164);
//            this.label15.Name = "label15";
//            this.label15.Size = new System.Drawing.Size(17, 12);
//            this.label15.TabIndex = 13;
//            this.label15.Text = "行";
//            this.textBox4.Location = new System.Drawing.Point(119, 179);
//            this.textBox4.Name = "textBox4";
//            this.textBox4.Size = new System.Drawing.Size(39, 21);
//            this.textBox4.TabIndex = 12;
//            this.textBox4.Text = "2";
//            this.textBox5.Location = new System.Drawing.Point(74, 179);
//            this.textBox5.Name = "textBox5";
//            this.textBox5.Size = new System.Drawing.Size(39, 21);
//            this.textBox5.TabIndex = 11;
//            this.textBox5.Text = "4";
//            this.label13.AutoSize = true;
//            this.label13.Location = new System.Drawing.Point(117, 125);
//            this.label13.Name = "label13";
//            this.label13.Size = new System.Drawing.Size(17, 12);
//            this.label13.TabIndex = 10;
//            this.label13.Text = "列";
//            this.label12.AutoSize = true;
//            this.label12.Location = new System.Drawing.Point(76, 125);
//            this.label12.Name = "label12";
//            this.label12.Size = new System.Drawing.Size(17, 12);
//            this.label12.TabIndex = 9;
//            this.label12.Text = "行";
//            this.textBox3.Location = new System.Drawing.Point(119, 140);
//            this.textBox3.Name = "textBox3";
//            this.textBox3.Size = new System.Drawing.Size(39, 21);
//            this.textBox3.TabIndex = 8;
//            this.textBox3.Text = "2";
//            this.label11.AutoSize = true;
//            this.label11.Location = new System.Drawing.Point(15, 179);
//            this.label11.Name = "label11";
//            this.label11.Size = new System.Drawing.Size(59, 12);
//            this.label11.TabIndex = 7;
//            this.label11.Text = "湿度坐标:";
//            this.label10.AutoSize = true;
//            this.label10.Location = new System.Drawing.Point(15, 143);
//            this.label10.Name = "label10";
//            this.label10.Size = new System.Drawing.Size(59, 12);
//            this.label10.TabIndex = 6;
//            this.label10.Text = "温度坐标:";
//            this.textBox2.Location = new System.Drawing.Point(74, 140);
//            this.textBox2.Name = "textBox2";
//            this.textBox2.Size = new System.Drawing.Size(39, 21);
//            this.textBox2.TabIndex = 5;
//            this.textBox2.Text = "3";
//            this.label7.AutoSize = true;
//            this.label7.Location = new System.Drawing.Point(15, 80);
//            this.label7.Name = "label7";
//            this.label7.Size = new System.Drawing.Size(65, 12);
//            this.label7.TabIndex = 2;
//            this.label7.Text = "湿度(%RH):";
//            this.label6.AutoSize = true;
//            this.label6.Location = new System.Drawing.Point(15, 52);
//            this.label6.Name = "label6";
//            this.label6.Size = new System.Drawing.Size(65, 12);
//            this.label6.TabIndex = 1;
//            this.label6.Text = "温度:(℃):";
//            this.label2.AutoSize = true;
//            this.label2.Location = new System.Drawing.Point(52, 21);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(41, 12);
//            this.label2.TabIndex = 0;
//            this.label2.Text = "升温区";
//            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
//            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
//            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
//            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            base.ClientSize = new System.Drawing.Size(1007, 625);
//            base.Controls.Add(this.panel2);
//            base.Controls.Add(this.pictureBox2);
//            base.Controls.Add(this.panel_Alignt);
//            base.Controls.Add(this.toolStrip2);
//            base.Controls.Add(this.toolStrip1);
//            base.Controls.Add(this.menuStrip1);
//            base.MainMenuStrip = this.menuStrip1;
//            base.Name = "formGrid";
//            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
//            this.Text = "表格编辑";
//            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formGrid_FormClosing);
//            base.Load += new System.EventHandler(this.formGrid_Load);
//            this.menuStrip1.ResumeLayout(false);
//            this.menuStrip1.PerformLayout();
//            this.panel1.ResumeLayout(false);
//            this.panel3.ResumeLayout(false);
//            this.panel3.PerformLayout();
//            this.contextMenuStrip1.ResumeLayout(false);
//            this.toolStrip1.ResumeLayout(false);
//            this.toolStrip1.PerformLayout();
//            this.toolStrip2.ResumeLayout(false);
//            this.toolStrip2.PerformLayout();
//            this.panel_Alignt.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_RightButtom).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_MiddleButtom).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_RightMiddle).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_MiddleMiddle).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_RightTop).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_MiddleTop).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_LeftButtom).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_LeftMiddle).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_Aligent_LeftTop).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox_max).EndInit();
//            ((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
//            this.panel2.ResumeLayout(false);
//            this.panel4.ResumeLayout(false);
//            this.panel4.PerformLayout();
//            this.panel5.ResumeLayout(false);
//            this.panel5.PerformLayout();
//            this.panel6.ResumeLayout(false);
//            this.panel6.PerformLayout();
//            this.panel7.ResumeLayout(false);
//            this.panel7.PerformLayout();
//            this.panel8.ResumeLayout(false);
//            this.panel8.PerformLayout();
//            base.ResumeLayout(false);
//            base.PerformLayout();
//        }

//        public formGrid2(int pWidth, int pHeight)
//        {
//            this.InitializeComponent();
//            formMain.ML.NowFormID = formGrid2.formID;
//            //formMain.ML.Refresh(this);
//            this.maxWidth = pWidth;
//        }

//        public void ResetColumWidth(int pNowColum)
//        {
//        }

//        private void InitGrid()
//        {
//            if (this.grid1 != null)
//            {
//                this.grid1.Dispose();
//            }
//            this.grid1 = new Grid();
//            this.grid1.BackColor1 = System.Drawing.Color.Black;
//            this.grid1.BackColor2 = System.Drawing.Color.Black;
//            this.grid1.BorderStyle = BorderStyleEnum.None;
//            this.grid1.CellBorderColor = System.Drawing.Color.Red;
//            this.grid1.DefaultFont = new System.Drawing.Font("宋体", 9f);
//            this.grid1.DisplayRowArrow = true;
//            this.grid1.DisplayRowNumber = true;
//            this.grid1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
//            this.grid1.GridColor = System.Drawing.Color.Red;
//            this.grid1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
//            this.grid1.Dock = System.Windows.Forms.DockStyle.None;
//            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.grid1.Name = "grid1";
//            this.grid1.Size = new System.Drawing.Size(232, 173);
//            this.grid1.TabIndex = 1;
//            this.grid1.ForeColor = System.Drawing.Color.Red;
//            Range r = this.grid1.Range(0, 0, this.grid1.Rows - 1, this.grid1.Cols - 1);
//            r.SelectCells();
//            this.grid1.Selection.set_Borders(EdgeEnum.Outside, LineStyleEnum.Thin);
//            this.grid1.Selection.ForeColor = System.Drawing.Color.Red;
//            this.grid1.DisplayClientBorder = false;
//            this.grid1.FixedRowColStyle = FixedRowColStyleEnum.Flat;
//            r = this.grid1.Range(1, 1, 1, 1);
//            this.grid1.BackColorBkg = System.Drawing.Color.Black;
//            r.SelectCells();
//            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.grid1.DisplayRowNumber = true;
//            System.Drawing.Point OldLocal = this.grid1.Location;
//            this.grid1.Location = new System.Drawing.Point(OldLocal.X, OldLocal.Y + 5);
//            this.grid1.Parent = this.panel1;
//            this.grid1.ContextMenuStrip = this.contextMenuStrip1;
//            this.grid1.KeyDown += new Grid.KeyDownEventHandler(this.grid1_KeyDown);
//            this.grid1.KeyUp += new Grid.KeyUpEventHandler(this.grid1_KeyUp);
//            this.grid1.RowColChange += new Grid.RowColChangeEventHandler(this.grid1_RowColChange);
//            this.grid1.RowHeightChange += new Grid.RowHeightChangeEventHandler(this.grid1_RowHeightChange);
//            this.grid1.ColWidthChange += new Grid.ColWidthChangeEventHandler(this.grid1_ColWidthChange);
//            this.grid1.MouseUp += new Grid.MouseUpEventHandler(this.grid1_MouseUp);
//            this.grid1.MouseEnter += new System.EventHandler(this.grid1_MouseEnter);
//            this.grid1.MouseDown += new Grid.MouseDownEventHandler(this.grid1_MouseDown);
//            this.grid1.CellChange += new Grid.CellChangeEventHandler(this.grid1_CellChange);
//            this.grid1.LostFocus += new System.EventHandler(this.grid1_LostFocus);
//            this.timer1.Start();
//        }

//        private void grid1_LostFocus(object sender, System.EventArgs e)
//        {
//        }

//        private void grid1_KeyDown(object Sender, System.Windows.Forms.KeyEventArgs e)
//        {
//            if (e.KeyCode == System.Windows.Forms.Keys.Return)
//            {
//                Cell cell = this.grid1.Cell(this.grid1.Selection.FirstRow, this.grid1.Selection.FirstCol);
//                string tempValue = cell.Text;
//                cell.WrapText = true;
//                cell.Text = tempValue + "\r\n";
//                e.Handled = true;
//            }
//        }

//        private void grid1_CellChange(object Sender, Grid.CellChangeEventArgs e)
//        {
//        }

//        private string getFormatString(string pValue)
//        {
//            return pValue;
//        }

//        private void grid1_MouseDown(object Sender, System.Windows.Forms.MouseEventArgs e)
//        {
//            if (this.panel_Alignt.Visible)
//            {
//                this.panel_Alignt.Visible = false;
//                this.panel_Alignt.SendToBack();
//            }
//        }

//        private void grid1_MouseEnter(object sender, System.EventArgs e)
//        {
//        }

//        private void grid1_MouseUp(object Sender, System.Windows.Forms.MouseEventArgs e)
//        {
//            try
//            {
//                if (this.grid1.Selection.FirstCol != this.grid1.Selection.LastCol || this.grid1.Selection.FirstRow != this.grid1.Selection.LastRow)
//                {
//                    this.toolStripComboBox_Font.Text = "";
//                    this.toolStripComboBox2.Text = "";
//                    this.toolStripLabel_Blod.BackColor = System.Drawing.Color.Transparent;
//                    this.toolStripLabel_Italic.BackColor = System.Drawing.Color.Transparent;
//                    this.toolStripLabel_Underline.BackColor = System.Drawing.Color.Transparent;
//                    this.button_wraptext.Tag = false;
//                }
//                else
//                {
//                    Cell cell = this.grid1.Cell(this.grid1.MouseRow, this.grid1.MouseCol);
//                    this.toolStripComboBox_Font.Text = cell.FontName;
//                    this.toolStripComboBox2.Text = cell.FontSize.ToString();
//                    if (cell.FontBold)
//                    {
//                        this.toolStripLabel_Blod.BackColor = System.Drawing.Color.LightBlue;
//                    }
//                    else
//                    {
//                        this.toolStripLabel_Blod.BackColor = System.Drawing.Color.Transparent;
//                    }
//                    if (cell.FontItalic)
//                    {
//                        this.toolStripLabel_Italic.BackColor = System.Drawing.Color.LightBlue;
//                    }
//                    else
//                    {
//                        this.toolStripLabel_Italic.BackColor = System.Drawing.Color.Transparent;
//                    }
//                    if (cell.FontUnderline)
//                    {
//                        this.toolStripLabel_Underline.BackColor = System.Drawing.Color.LightBlue;
//                    }
//                    else
//                    {
//                        this.toolStripLabel_Underline.BackColor = System.Drawing.Color.Transparent;
//                    }
//                    this.button_wraptext.Tag = cell.WrapText;
//                }
//            }
//            catch
//            {
//            }
//        }

//        private void grid1_ColWidthChange(object Sender, Grid.ColWidthChangeEventArgs e)
//        {
//            this.Save();
//        }

//        private void grid1_RowHeightChange(object Sender, Grid.RowHeightChangeEventArgs e)
//        {
//            this.Save();
//        }

//        private void grid1_RowColChange(object Sender, Grid.RowColChangeEventArgs e)
//        {
//            this.Save();
//        }

//        private void grid1_KeyUp(object Sender, System.Windows.Forms.KeyEventArgs e)
//        {
//        }

//        private void timer1_Tick_1(object sender, System.EventArgs e)
//        {
//            if (this.StartSave())
//            {
//                this.fm.RedrawSubarea();
//            }
//        }

//        private void formGrid_Load(object sender, System.EventArgs e)
//        {
//            base.Icon = Resources.Icon1;
//            base.Size = new System.Drawing.Size(this.fm.Width, 387);
//            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - base.Width;
//            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - base.Height;
//            base.Location = new System.Drawing.Point(x / 2, y);
//            this.pictureBox2.Image = Template.Main_Max;
//            System.Drawing.Text.InstalledFontCollection objFont = new System.Drawing.Text.InstalledFontCollection();
//            System.Drawing.FontFamily[] families = objFont.Families;
//            for (int i = 0; i < families.Length; i++)
//            {
//                System.Drawing.FontFamily j = families[i];
//                this.toolStripComboBox_Font.Items.Add(j.Name.ToString());
//                if (System.Text.RegularExpressions.Regex.IsMatch(string.Concat(j.Name[0]), "[\\u4e00-\\u9fa5]+$") && j.Name.ToString() != "宋体-PUA")
//                {
//                    this.toolStripComboBox_Font.Items.Add("@" + j.Name.ToString());
//                }
//            }
//        }

//        public void NewGrid(string pFileName)
//        {
//            this.fileName = pFileName;
//            this.InitGrid();
//            this.grid1.SaveFile(pFileName);
//            if (formGrid2.NeedImport)
//            {
//                formGrid2.NeedImport = false;
//                this.ImportNew(formGrid2.ImportFileName, formGrid2.ImportSheetIndex);
//            }
//            this.fm.Hide();
//            this.fm.notifyIcon1.Visible = true;
//            base.Show();
//        }

//        public void Edit(string pFileName)
//        {
//            try
//            {
//                this.fileName = pFileName;
//                this.InitGrid();
//                new System.Drawing.Size(2, 2);
//                System.Reflection.Missing arg_1A_0 = System.Reflection.Missing.Value;
//                this.grid1.OpenFile(pFileName);
//                if (this.grid1.Cols == 2 && this.grid1.Rows == 2 && this.grid1.Column(1).Width < 15)
//                {
//                    this.newgrid(5, 10, this.maxWidth);
//                }
//                this.grid1.Refresh();
//                if (formGrid2.NeedImport)
//                {
//                    formGrid2.NeedImport = false;
//                    this.ImportNew(formGrid2.ImportFileName, formGrid2.ImportSheetIndex);
//                }
//                this.fm.Hide();
//                this.fm.notifyIcon1.Visible = true;
//                base.ShowDialog();
//            }
//            catch
//            {
//            }
//        }

//        private void SetCellAutoWrap(Grid grid)
//        {
//        }

//        public void Edit_New(string pFileName, string pExcelName, int pSheet)
//        {
//            this.fileName = pFileName;
//            this.InitGrid();
//            new System.Drawing.Size(2, 2);
//            System.Reflection.Missing arg_1A_0 = System.Reflection.Missing.Value;
//            this.grid1.OpenFile(pFileName);
//            this.grid1.Refresh();
//            string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + pExcelName + ";Extended Properties=Excel 8.0";
//            OleDbConnection odcConnection = new OleDbConnection(strCon);
//            odcConnection.Open();
//            DataTable _Table = odcConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
//            System.Collections.Generic.List<string> sheetNames = new System.Collections.Generic.List<string>();
//            for (int i = 0; i < _Table.Rows.Count; i++)
//            {
//                sheetNames.Add(_Table.Rows[i]["Table_Name"].ToString());
//            }
//            OleDbCommand odCommand = odcConnection.CreateCommand();
//            odCommand.CommandText = "SELECT * FROM [" + sheetNames[pSheet] + "]";
//            OleDbDataReader odrReader = odCommand.ExecuteReader();
//            int size = odrReader.FieldCount;
//            this.InitGrid();
//            Range ra = this.grid1.Range(2, 1, this.grid1.Rows - 1, this.grid1.Cols - 1);
//            ra.DeleteByRow();
//            Range ra2 = this.grid1.Range(1, 2, this.grid1.Rows - 1, this.grid1.Cols - 1);
//            ra2.DeleteByCol();
//            for (int j = 0; j < size; j++)
//            {
//                this.grid1.InsertCol(j, 1);
//            }
//            int k = 1;
//            odrReader.Read();
//            for (int l = 0; l < size; l++)
//            {
//                string str = odrReader.GetName(l);
//                if (str == "F" + (l + 1).ToString())
//                {
//                    this.grid1.Cell(k, l + 1).Text = "";
//                }
//                else
//                {
//                    this.grid1.Cell(k, l + 1).Text = str;
//                }
//                this.grid1.Cell(k, l + 1).ForeColor = System.Drawing.Color.Red;
//            }
//            k++;
//            do
//            {
//                this.grid1.AddItem(k.ToString(), false);
//                for (int m = 0; m < size; m++)
//                {
//                    string str2 = odrReader[m].ToString();
//                    this.grid1.Cell(k, m + 1).Text = str2;
//                    this.grid1.Cell(k, m + 1).ForeColor = System.Drawing.Color.Red;
//                }
//                k++;
//            }
//            while (odrReader.Read());
//            this.grid1.Refresh();
//            odrReader.Close();
//            odrReader.Dispose();
//            odcConnection.Close();
//            odcConnection.Dispose();
//            this.grid1.ContextMenuStrip = this.contextMenuStrip1;
//            if (this.maxWidth == 0)
//            {
//                base.ShowDialog();
//            }
//            else
//            {
//                int col = this.grid1.Cols - 1;
//                int width = this.maxWidth / col;
//                for (int n = 0; n < col; n++)
//                {
//                    this.grid1.Column(n + 1).Width = short.Parse(width.ToString());
//                    if (n + 1 == col && col * width == this.maxWidth)
//                    {
//                        this.grid1.Column(n + 1).Width = short.Parse((width - 1).ToString());
//                    }
//                }
//                this.SetCellAutoWrap(this.grid1);
//                this.Save();
//                base.ShowDialog();
//            }
//        }

//        private void timer1_Tick(object sender, System.EventArgs e)
//        {
//            this.grid1.SaveFile(this.fileName);
//            this.fm.RedrawSubarea();
//        }

//        private bool Save()
//        {
//            return this.StartSave();
//        }

//        private bool StartSave()
//        {
//            bool result;
//            try
//            {
//                this.grid1.SaveFile(this.fileName);
//                result = true;
//            }
//            catch
//            {
//                result = false;
//            }
//            return result;
//        }

//        private void import_ExcelToolStripMenuItem1_Click(object sender, System.EventArgs e)
//        {
//            this.openFileDialog1.Filter = "Excel 97-2003 files(*.xls)|*.xls";
//            try
//            {
//                if (this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
//                {
//                    return;
//                }
//            }
//            catch
//            {
//                this.openFileDialog1.InitialDirectory = formMain.DesktopPath;
//                if (this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
//                {
//                    return;
//                }
//            }
//            string filename = this.openFileDialog1.FileName;
//            string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + filename + ";Extended Properties=Excel 8.0";
//            OleDbConnection odcConnection = new OleDbConnection(strCon);
//            odcConnection.Open();
//            DataTable _Table = odcConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
//            System.Collections.Generic.List<string> sheetNames = new System.Collections.Generic.List<string>();
//            for (int i = 0; i < _Table.Rows.Count; i++)
//            {
//                sheetNames.Add(_Table.Rows[i]["Table_Name"].ToString());
//            }
//            int SheetIndex;
//            if (sheetNames.Count > 1)
//            {
//                formSelectIndex SII = new formSelectIndex(formMain.ML.GetStr("PleaseSelect"), formMain.ML.GetStr("PleaseChooseTheSheetyouwantimport"), sheetNames);
//                SheetIndex = SII.GetIndex();
//                if (SheetIndex == -1)
//                {
//                    return;
//                }
//            }
//            else
//            {
//                SheetIndex = 0;
//            }
//            OleDbCommand odCommand = odcConnection.CreateCommand();
//            odCommand.CommandText = "SELECT * FROM [" + sheetNames[SheetIndex] + "]";
//            OleDbDataReader odrReader = odCommand.ExecuteReader();
//            int size = odrReader.FieldCount;
//            if (size > 10)
//            {
//                size = 10;
//            }
//            this.InitGrid();
//            Range ra = this.grid1.Range(2, 1, this.grid1.Rows - 1, this.grid1.Cols - 1);
//            ra.DeleteByRow();
//            Range ra2 = this.grid1.Range(1, 2, this.grid1.Rows - 1, this.grid1.Cols - 1);
//            ra2.DeleteByCol();
//            for (int j = 0; j < size; j++)
//            {
//                this.grid1.InsertCol(j, 1);
//            }
//            int k = 1;
//            odrReader.Read();
//            for (int l = 0; l < size; l++)
//            {
//                string str = odrReader.GetName(l);
//                if (str == "F" + (l + 1).ToString())
//                {
//                    this.grid1.Cell(k, l + 1).Text = "";
//                }
//                else
//                {
//                    this.grid1.Cell(k, l + 1).Text = str;
//                }
//                this.grid1.Cell(k, l + 1).ForeColor = System.Drawing.Color.Red;
//            }
//            k++;
//            do
//            {
//                this.grid1.AddItem(k.ToString(), false);
//                for (int m = 0; m < size; m++)
//                {
//                    string str2 = odrReader[m].ToString();
//                    if (str2.StartsWith("'"))
//                    {
//                        str2 = str2.Replace("'", "");
//                    }
//                    this.grid1.Cell(k, m + 1).Text = str2;
//                    this.grid1.Cell(k, m + 1).ForeColor = System.Drawing.Color.Red;
//                    this.grid1.Cell(k, m + 1).WrapText = true;
//                }
//                k++;
//            }
//            while (odrReader.Read());
//            this.grid1.Refresh();
//            odrReader.Close();
//            odrReader.Dispose();
//            odcConnection.Close();
//            odcConnection.Dispose();
//            this.grid1.ContextMenuStrip = this.contextMenuStrip1;
//            if (this.maxWidth == 0)
//            {
//                base.ShowDialog();
//            }
//            else
//            {
//                int col = this.grid1.Cols - 1;
//                int width = this.maxWidth / col;
//                for (int n = 0; n < col; n++)
//                {
//                    this.grid1.Column(n + 1).Width = short.Parse(width.ToString());
//                    if (n + 1 == col && col * width == this.maxWidth)
//                    {
//                        this.grid1.Column(n + 1).Width = short.Parse((width - 1).ToString());
//                    }
//                }
//                this.SetCellAutoWrap(this.grid1);
//                this.Save();
//            }
//        }

//        private void ImportNew(string filename, int SheetIndex)
//        {
//            string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + filename + ";Extended Properties=Excel 8.0";
//            OleDbConnection odcConnection = new OleDbConnection(strCon);
//            odcConnection.Open();
//            DataTable _Table = odcConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
//            System.Collections.Generic.List<string> sheetNames = new System.Collections.Generic.List<string>();
//            for (int i = 0; i < _Table.Rows.Count; i++)
//            {
//                sheetNames.Add(_Table.Rows[i]["Table_Name"].ToString());
//            }
//            OleDbCommand odCommand = odcConnection.CreateCommand();
//            odCommand.CommandText = "SELECT * FROM [" + sheetNames[SheetIndex] + "]";
//            OleDbDataReader odrReader = odCommand.ExecuteReader();
//            int size = odrReader.FieldCount;
//            this.InitGrid();
//            Range ra = this.grid1.Range(2, 1, this.grid1.Rows - 1, this.grid1.Cols - 1);
//            ra.DeleteByRow();
//            Range ra2 = this.grid1.Range(1, 2, this.grid1.Rows - 1, this.grid1.Cols - 1);
//            ra2.DeleteByCol();
//            for (int j = 0; j < size; j++)
//            {
//                this.grid1.InsertCol(j, 1);
//            }
//            int k = 1;
//            odrReader.Read();
//            for (int l = 0; l < size; l++)
//            {
//                string str = odrReader.GetName(l);
//                if (str == "F" + (l + 1).ToString())
//                {
//                    this.grid1.Cell(k, l + 1).Text = "";
//                }
//                else
//                {
//                    this.grid1.Cell(k, l + 1).Text = str;
//                }
//                this.grid1.Cell(k, l + 1).ForeColor = System.Drawing.Color.Red;
//            }
//            k++;
//            do
//            {
//                this.grid1.AddItem(k.ToString(), false);
//                for (int m = 0; m < size; m++)
//                {
//                    string str2 = odrReader[m].ToString();
//                    if (str2.StartsWith("'"))
//                    {
//                        str2 = str2.Replace("'", "");
//                    }
//                    this.grid1.Cell(k, m + 1).Text = str2;
//                    this.grid1.Cell(k, m + 1).ForeColor = System.Drawing.Color.Red;
//                    this.grid1.Cell(k, m + 1).WrapText = true;
//                }
//                k++;
//            }
//            while (odrReader.Read());
//            this.grid1.Refresh();
//            odrReader.Close();
//            odrReader.Dispose();
//            odcConnection.Close();
//            odcConnection.Dispose();
//            this.grid1.ContextMenuStrip = this.contextMenuStrip1;
//            if (this.maxWidth == 0)
//            {
//                base.ShowDialog();
//            }
//            else
//            {
//                int col = this.grid1.Cols - 1;
//                int width = this.maxWidth / col;
//                for (int n = 0; n < col; n++)
//                {
//                    this.grid1.Column(n + 1).Width = short.Parse(width.ToString());
//                    if (n + 1 == col && col * width == this.maxWidth)
//                    {
//                        this.grid1.Column(n + 1).Width = short.Parse((width - 1).ToString());
//                    }
//                }
//                this.SetCellAutoWrap(this.grid1);
//                this.Save();
//            }
//        }

//        private void export_XmlToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//        }

//        private void exit_ToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            base.Dispose();
//        }

//        private void save_ToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.SaveFile(this.fileName);
//        }

//        private void CopyToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.CopyData();
//            this.Save();
//        }

//        private void CutToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.CutData();
//            this.Save();
//        }

//        private void pasteToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.PasteData();
//            this.Save();
//        }

//        private void clearContentToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.ClearText();
//            this.Save();
//        }

//        private void clearFormatToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.ClearFormat();
//            this.Save();
//        }

//        private void clearAllToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.ClearAll();
//            this.Save();
//        }

//        private void mergeToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            try
//            {
//                this.grid1.Selection.MergeCells = true;
//                this.Save();
//            }
//            catch
//            {
//                System.Windows.Forms.MessageBox.Show(formMain.ML.GetStr("CanMergeReadonlyCells"));
//            }
//        }

//        private void noMergeToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.MergeCells = false;
//            this.Save();
//        }

//        private void insertRowToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.InsertRows();
//            this.Save();
//        }

//        private void insertColumnToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.InsertCols();
//            this.Save();
//        }

//        private void removeRowToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            if (this.IsAlowDeleteRow())
//            {
//                this.grid1.Selection.DeleteByRow();
//                this.Save();
//            }
//        }

//        private void removeColumnToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            if (this.IsAlowDelteCol())
//            {
//                this.grid1.Selection.DeleteByCol();
//                this.Save();
//            }
//        }

//        private void fontToolStripMenuItem1_Click(object sender, System.EventArgs e)
//        {
//            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
//            fontDialog.Font = this.grid1.ActiveCell.Font;
//            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
//            {
//                this.grid1.Selection.Font = fontDialog.Font;
//            }
//            fontDialog.Dispose();
//            this.Save();
//        }

//        private void borderNoneToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders((EdgeEnum)255, LineStyleEnum.None);
//            this.Save();
//        }

//        private void borderLeftToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders(EdgeEnum.Left, LineStyleEnum.Thin);
//            this.Save();
//        }

//        private void borderRightToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders(EdgeEnum.Right, LineStyleEnum.Thin);
//            this.Save();
//        }

//        private void borderTopToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders(EdgeEnum.Top, LineStyleEnum.Thin);
//            this.Save();
//        }

//        private void borderBottomToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders(EdgeEnum.Bottom, LineStyleEnum.Thin);
//            this.Save();
//        }

//        private void borderLineUpToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders(EdgeEnum.DiagonalUp, LineStyleEnum.Thin);
//            this.Save();
//        }

//        private void borderLineDownToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders(EdgeEnum.DiagonalDown, LineStyleEnum.Thin);
//            this.Save();
//        }

//        private void borderInsideToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders(EdgeEnum.Inside, LineStyleEnum.Thin);
//            this.Save();
//        }

//        private void borderOutsideThinToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders(EdgeEnum.Outside, LineStyleEnum.Thin);
//            this.Save();
//        }

//        private void borderOutsideStickToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders(EdgeEnum.Outside, LineStyleEnum.Thick);
//            this.Save();
//        }

//        private void borderAllToolStripMenuItem1_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.set_Borders((EdgeEnum)207, LineStyleEnum.Thin);
//            this.Save();
//        }

//        private void alignNormalToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.GeneralGeneral;
//            this.Save();
//        }

//        private void alignTopToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.GeneralTop;
//            this.Save();
//        }

//        private void alignMiddleToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.GeneralCenter;
//            this.Save();
//        }

//        private void alignBottomToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.GeneralBottom;
//            this.Save();
//        }

//        private void alignLeftToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.LeftGeneral;
//            this.Save();
//        }

//        private void alignTopLeftToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.LeftTop;
//            this.Save();
//        }

//        private void alignMiddleLeftToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.LeftCenter;
//            this.Save();
//        }

//        private void alignBottomLeftToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.LeftBottom;
//            this.Save();
//        }

//        private void alignCenterToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.CenterGeneral;
//            this.Save();
//        }

//        private void alignTopCenterToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.CenterTop;
//            this.Save();
//        }

//        private void alignMiddleCenterToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.CenterCenter;
//            this.Save();
//        }

//        private void alignBottomCenterToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.CenterBottom;
//            this.Save();
//        }

//        private void alignRightToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.RightGeneral;
//            this.Save();
//        }

//        private void alignTopRightToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.RightTop;
//            this.Save();
//        }

//        private void alignMiddleRightToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.RightCenter;
//            this.Save();
//        }

//        private void alignBottomRightToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.RightBottom;
//            this.Save();
//        }

//        private void foreColor_RedToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.ForeColor = System.Drawing.Color.Red;
//            this.Save();
//        }

//        private void foreColor_GreenToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count != 1)
//            {
//                this.grid1.Selection.ForeColor = System.Drawing.Color.Green;
//                this.Save();
//            }
//        }

//        private void foreColor_YellowToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count != 1)
//            {
//                this.grid1.Selection.ForeColor = System.Drawing.Color.Yellow;
//                this.Save();
//            }
//        }

//        private void backcolor_RedToolStripMenuItem1_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.BackColor = System.Drawing.Color.Red;
//            this.Save();
//        }

//        private void backcolor_GreenToolStripMenuItem1_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count != 1)
//            {
//                this.grid1.Selection.BackColor = System.Drawing.Color.Green;
//                this.Save();
//            }
//        }

//        private void backcolor_YellowToolStripMenuItem1_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count != 1)
//            {
//                this.grid1.Selection.BackColor = System.Drawing.Color.Yellow;
//                this.Save();
//            }
//        }

//        private void backcolor_BlackToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.BackColor = System.Drawing.Color.Black;
//            this.Save();
//        }

//        private void gridcolor_RedToolStripMenuItem2_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count == 1)
//            {
//                this.grid1.GridColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                this.grid1.GridColor = System.Drawing.Color.Red;
//                this.Save();
//            }
//        }

//        private void gridcolor_GreenToolStripMenuItem2_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count == 1)
//            {
//                this.grid1.GridColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                this.grid1.GridColor = System.Drawing.Color.Green;
//                this.Save();
//            }
//        }

//        private void gridcolor_YellowToolStripMenuItem2_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count == 1)
//            {
//                this.grid1.GridColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                this.grid1.GridColor = System.Drawing.Color.Yellow;
//                this.Save();
//            }
//        }

//        private void gridcolor_BlackToolStripMenuItem1_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.GridColor = System.Drawing.Color.Black;
//            this.Save();
//        }

//        private void bordercolor_RedToolStripMenuItem3_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.BorderColor = System.Drawing.Color.Red;
//            this.Save();
//        }

//        private void bordercolor_GreenToolStripMenuItem3_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count == 1)
//            {
//                this.grid1.BorderColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                this.grid1.BorderColor = System.Drawing.Color.Green;
//                this.Save();
//            }
//        }

//        private void bordercolor_YellowToolStripMenuItem3_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count == 1)
//            {
//                this.grid1.BorderColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                this.grid1.BorderColor = System.Drawing.Color.Yellow;
//                this.Save();
//            }
//        }

//        private void bordercolor_BlackToolStripMenuItem2_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.BorderColor = System.Drawing.Color.Black;
//            this.Save();
//        }

//        private void wrapTextToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.WrapText = true;
//            this.Save();
//        }

//        private void nowrapTextToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.WrapText = false;
//            this.Save();
//        }

//        private void readonlyToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Locked = true;
//            this.Save();
//        }

//        private void noreadonlyToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Locked = false;
//            this.Save();
//        }

//        private void formGrid_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
//        {
//            this.timer1.Stop();
//            this.Save();
//        }

//        private void toolStripComboBox_Font_SelectedIndexChanged(object sender, System.EventArgs e)
//        {
//            try
//            {
//                System.Windows.Forms.ToolStripComboBox tscb = (System.Windows.Forms.ToolStripComboBox)sender;
//                if (tscb.Focused)
//                {
//                    this.grid1.Selection.FontName = tscb.Text;
//                }
//            }
//            catch
//            {
//            }
//        }

//        private void toolStripComboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
//        {
//            try
//            {
//                System.Windows.Forms.ToolStripComboBox tscb = (System.Windows.Forms.ToolStripComboBox)sender;
//                if (tscb.Focused)
//                {
//                    this.grid1.Selection.FontSize = float.Parse(tscb.Text);
//                }
//            }
//            catch
//            {
//            }
//        }

//        private void toolStripLabel_Blod_Click(object sender, System.EventArgs e)
//        {
//            System.Windows.Forms.ToolStripLabel tsl = (System.Windows.Forms.ToolStripLabel)sender;
//            if (tsl.BackColor == System.Drawing.Color.LightBlue)
//            {
//                this.grid1.Selection.FontBold = false;
//                tsl.BackColor = System.Drawing.Color.Transparent;
//            }
//            else
//            {
//                this.grid1.Selection.FontBold = true;
//                tsl.BackColor = System.Drawing.Color.LightBlue;
//            }
//        }

//        private void toolStripLabel_Italic_Click(object sender, System.EventArgs e)
//        {
//            System.Windows.Forms.ToolStripLabel tsl = (System.Windows.Forms.ToolStripLabel)sender;
//            if (tsl.BackColor == System.Drawing.Color.LightBlue)
//            {
//                this.grid1.Selection.FontItalic = false;
//                tsl.BackColor = System.Drawing.Color.Transparent;
//            }
//            else
//            {
//                this.grid1.Selection.FontItalic = true;
//                tsl.BackColor = System.Drawing.Color.LightBlue;
//            }
//        }

//        private void toolStripLabel_Underline_Click(object sender, System.EventArgs e)
//        {
//            System.Windows.Forms.ToolStripLabel tsl = (System.Windows.Forms.ToolStripLabel)sender;
//            if (tsl.BackColor == System.Drawing.Color.LightBlue)
//            {
//                this.grid1.Selection.FontUnderline = false;
//                tsl.BackColor = System.Drawing.Color.Transparent;
//            }
//            else
//            {
//                this.grid1.Selection.FontUnderline = true;
//                tsl.BackColor = System.Drawing.Color.LightBlue;
//            }
//        }

//        private void button_combin_Cell_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Merge();
//        }

//        private void button_split_Cell_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.MergeCells = false;
//        }

//        private void button_Save_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.SaveFile(this.fileName);
//        }

//        private void button_Open_Click(object sender, System.EventArgs e)
//        {
//            this.import_ExcelToolStripMenuItem1_Click(null, null);
//        }

//        private void button_New_Click(object sender, System.EventArgs e)
//        {
//            this.panel1.Visible = false;
//            this.grid1.Visible = false;
//            this.Refresh();
//            formGridCellAndCol fgc = new formGridCellAndCol();
//            fgc.Get();
//            int col = formGridCellAndCol.col;
//            int row = formGridCellAndCol.row;
//            if (col == 0 | row == 0)
//            {
//                this.panel1.Visible = true;
//                this.grid1.Visible = true;
//                base.BringToFront();
//            }
//            else
//            {
//                this.newgrid(col, row, this.maxWidth);
//                this.panel1.Visible = true;
//                this.grid1.Visible = true;
//                base.BringToFront();
//            }
//        }

//        private void newgrid(int col, int row, int allwidth)
//        {
//            this.InitGrid();
//            while (this.grid1.Cols < col + 1)
//            {
//                this.grid1.Selection.InsertCols();
//            }
//            while (this.grid1.Rows < row + 1)
//            {
//                this.grid1.Selection.InsertRows();
//            }
//            if (this.grid1.Cols - 1 != col)
//            {
//                if (this.grid1.Cols - 1 > col)
//                {
//                    int needDelete = this.grid1.Cols - 1 - col;
//                    for (int i = 0; i < needDelete; i++)
//                    {
//                        this.grid1.Column(1).Delete();
//                    }
//                }
//                else
//                {
//                    int needDelete2 = col - (this.grid1.Cols - 1);
//                    for (int j = 0; j < needDelete2; j++)
//                    {
//                        this.grid1.InsertCol(0, 0);
//                    }
//                }
//            }
//            if (this.grid1.Rows - 1 != row)
//            {
//                if (this.grid1.Rows - 1 > row)
//                {
//                    int needDelete3 = this.grid1.Rows - 1 - row;
//                    for (int k = 0; k < needDelete3; k++)
//                    {
//                        this.grid1.Row(1).Delete();
//                    }
//                }
//                else
//                {
//                    int needDelete4 = row - (this.grid1.Rows - 1);
//                    this.grid1.InsertRow(this.grid1.Rows - 1, needDelete4);
//                }
//            }
//            if (formGridCellAndCol.autoRowNumber)
//            {
//                for (int l = 0; l < this.grid1.Rows; l++)
//                {
//                    this.grid1.Cell(l, 1).Text = l.ToString();
//                }
//            }
//            if (this.maxWidth != 0)
//            {
//                int width = this.maxWidth / col;
//                for (int m = 0; m < col; m++)
//                {
//                    this.grid1.Column(m + 1).Width = short.Parse(width.ToString());
//                    if (m + 1 == col && col * width == allwidth)
//                    {
//                        this.grid1.Column(m + 1).Width = short.Parse((width - 1).ToString());
//                    }
//                }
//                for (int n = 0; n < this.grid1.Rows; n++)
//                {
//                    this.grid1.Row(n).Height = 15;
//                }
//                this.SetCellAutoWrap(this.grid1);
//            }
//        }

//        private void button_Insert_Row_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.InsertRows();
//        }

//        private void button_insert_Col_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.InsertCols();
//        }

//        private void button_delete_Row_Click(object sender, System.EventArgs e)
//        {
//            if (this.IsAlowDeleteRow())
//            {
//                this.grid1.Selection.DeleteByRow();
//            }
//        }

//        private void button_delete_Col_Click(object sender, System.EventArgs e)
//        {
//            if (this.IsAlowDelteCol())
//            {
//                this.grid1.Selection.DeleteByCol();
//            }
//        }

//        private bool IsAlowDeleteRow()
//        {
//            return this.grid1.Rows > 2;
//        }

//        private bool IsAlowDelteCol()
//        {
//            return this.grid1.Cols > 2;
//        }

//        private void toolStripLabel_Aligment_Click(object sender, System.EventArgs e)
//        {
//        }

//        private void toolStripButton1_Click(object sender, System.EventArgs e)
//        {
//            this.panel_Alignt.Visible = true;
//            this.panel_Alignt.BringToFront();
//        }

//        private void pictureBox8_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.RightCenter;
//            this.panel_Alignt.Visible = false;
//            this.panel_Alignt.SendToBack();
//        }

//        private void pictureBox_Aligent_LeftTop_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.LeftTop;
//            this.panel_Alignt.Visible = false;
//            this.panel_Alignt.SendToBack();
//        }

//        private void pictureBox_Aligent_LeftMiddle_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.LeftCenter;
//            this.panel_Alignt.Visible = false;
//            this.panel_Alignt.SendToBack();
//        }

//        private void pictureBox_Aligent_LeftButtom_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.LeftBottom;
//            this.panel_Alignt.Visible = false;
//            this.panel_Alignt.SendToBack();
//        }

//        private void pictureBox_Aligent_MiddleTop_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.CenterTop;
//            this.panel_Alignt.Visible = false;
//            this.panel_Alignt.SendToBack();
//        }

//        private void pictureBox_Aligent_MiddleMiddle_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.CenterCenter;
//            this.panel_Alignt.Visible = false;
//            this.panel_Alignt.SendToBack();
//        }

//        private void pictureBox_Aligent_MiddleButtom_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.CenterBottom;
//            this.panel_Alignt.Visible = false;
//            this.panel_Alignt.SendToBack();
//        }

//        private void pictureBox_Aligent_RightTop_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.RightTop;
//            this.panel_Alignt.Visible = false;
//            this.panel_Alignt.SendToBack();
//        }

//        private void pictureBox_Aligent_RightButtom_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.Alignment = AlignmentEnum.RightBottom;
//            this.panel_Alignt.Visible = false;
//            this.panel_Alignt.SendToBack();
//        }

//        private void toolStripMenuItem_Backcolor_Red_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.BackColor = System.Drawing.Color.Red;
//        }

//        private void toolStripMenuItem_Backcolor_Green_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count == 1)
//            {
//                this.grid1.Selection.BackColor = System.Drawing.Color.Black;
//            }
//            else
//            {
//                this.grid1.Selection.BackColor = System.Drawing.Color.Green;
//            }
//        }

//        private void toolStripMenuItem_Backcolor_Yellow_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count == 1)
//            {
//                this.grid1.Selection.BackColor = System.Drawing.Color.Black;
//            }
//            else
//            {
//                this.grid1.Selection.BackColor = System.Drawing.Color.Yellow;
//            }
//        }

//        private void toolStripMenuItem_Backcolor_Black_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.BackColor = System.Drawing.Color.Black;
//        }

//        private void toolStripMenuItem_fontColor_Red_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.ForeColor = System.Drawing.Color.Red;
//        }

//        private void toolStripMenuItem_fontColor_Green_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count == 1)
//            {
//                this.grid1.Selection.ForeColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                this.grid1.Selection.ForeColor = System.Drawing.Color.Green;
//            }
//        }

//        private void toolStripMenuItem_fontColor_Yellow_Click(object sender, System.EventArgs e)
//        {
//            if (formMain.ledsys.Panel.ColorList.Count == 1)
//            {
//                this.grid1.Selection.ForeColor = System.Drawing.Color.Red;
//            }
//            else
//            {
//                this.grid1.Selection.ForeColor = System.Drawing.Color.Yellow;
//            }
//        }

//        private void toolStripMenuItem_fontColor_black_Click(object sender, System.EventArgs e)
//        {
//            this.grid1.Selection.ForeColor = System.Drawing.Color.Black;
//        }

//        private void MaxWind()
//        {
//            if (this.isMaxSize)
//            {
//                this.isMaxSize = false;
//                base.Size = this.normalSize;
//                base.Location = this.normalLocation;
//            }
//            else
//            {
//                this.isMaxSize = true;
//                this.normalSize = base.Size;
//                this.normalLocation = base.Location;
//                base.Size = (base.Size = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size);
//                base.Location = new System.Drawing.Point(0, 0);
//            }
//        }

//        private void pictureBox_max_Click(object sender, System.EventArgs e)
//        {
//            this.MaxWind();
//        }

//        private void pictureBox2_Click(object sender, System.EventArgs e)
//        {
//            this.MaxWind();
//        }

//        private void saveas_ToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            System.Windows.Forms.SaveFileDialog save = new System.Windows.Forms.SaveFileDialog();
//            save.Filter = "Excel 97-2003 files(*.xls)|*.xls";
//            try
//            {
//                if (save.ShowDialog() != System.Windows.Forms.DialogResult.OK)
//                {
//                    return;
//                }
//            }
//            catch
//            {
//                save.InitialDirectory = (this.openFileDialog1.InitialDirectory = formMain.DesktopPath);
//                if (save.ShowDialog() != System.Windows.Forms.DialogResult.OK)
//                {
//                    return;
//                }
//            }
//            this.grid1.SaveFile(save.FileName);
//        }

//        private void fileExcelToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//        }

//        private void button_wraptext_Click(object sender, System.EventArgs e)
//        {
//            System.Windows.Forms.ToolStripButton tsb = (System.Windows.Forms.ToolStripButton)sender;
//            bool isWrap = (bool)tsb.Tag;
//            if (isWrap)
//            {
//                this.grid1.Selection.WrapText = false;
//                tsb.Tag = false;
//            }
//            else
//            {
//                this.grid1.Selection.WrapText = true;
//                tsb.Tag = true;
//            }
//        }

//        private void toolStripSplitButton1_ButtonClick(object sender, System.EventArgs e)
//        {
//        }

//        private void autoHeightToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//        }

//        private void SetRowAutoFit()
//        {
//            try
//            {
//                for (int i = 0; i < this.totalRowNum; i++)
//                {
//                    this.grid1.Row(i).AutoFit();
//                    this.nowRowNum++;
//                }
//                this.nowRowNum = this.totalRowNum;
//            }
//            catch
//            {
//                this.nowRowNum = this.totalRowNum;
//            }
//        }

//        private void timer2_Tick(object sender, System.EventArgs e)
//        {
//            string percentText = ((double)this.nowRowNum / (double)this.totalRowNum).ToString("0.0%");
//            this.label_Percent.Text = percentText;
//            if (this.nowRowNum == this.totalRowNum)
//            {
//                this.panel3.Visible = false;
//                this.timer1.Start();
//                this.timer2.Stop();
//            }
//        }

//        private void button_Cancel_Click(object sender, System.EventArgs e)
//        {
//            this.panel3.SendToBack();
//            this.panel3.Visible = false;
//            this.timer2.Stop();
//            this.timer1.Start();
//        }

//        private void zishiToolStripMenuItem_Click(object sender, System.EventArgs e)
//        {
//            this.timer2.Start();
//            this.timer1.Stop();
//            this.panel3.Visible = true;
//            this.panel3.BringToFront();
//            this.totalRowNum = this.grid1.Rows;
//            this.nowRowNum = 0;
//            this.setRowHeight = new System.Threading.Thread(new System.Threading.ThreadStart(this.SetRowAutoFit));
//            this.setRowHeight.Start();
//        }

//        private void toolStripButton2_Click(object sender, System.EventArgs e)
//        {
//        }

//        public static System.Drawing.Bitmap GetWindowCapture(System.IntPtr hWnd)
//        {
//            System.IntPtr hscrdc = formGrid2.GetWindowDC(hWnd);
//            System.Drawing.Rectangle windowRect = default(System.Drawing.Rectangle);
//            formGrid2.GetWindowRect(hWnd, ref windowRect);
//            int width = windowRect.Right - windowRect.Left;
//            int height = windowRect.Bottom - windowRect.Top;
//            System.IntPtr hbitmap = formGrid2.CreateCompatibleBitmap(hscrdc, width, height);
//            System.IntPtr hmemdc = formGrid2.CreateCompatibleDC(hscrdc);
//            formGrid2.SelectObject(hmemdc, hbitmap);
//            formGrid2.PrintWindow(hWnd, hmemdc, 0u);
//            System.Drawing.Bitmap bmp = System.Drawing.Image.FromHbitmap(hbitmap);
//            formGrid2.DeleteDC(hscrdc);
//            formGrid2.DeleteDC(hmemdc);
//            return bmp;
//        }

//        private void Update_the_data()
//        {
//            string s_SQL_升温区 = "SELECT Top 1 Tem,Hum FROM rsmonitor.dbo.tbhistory WHERE DeviceID='10008862' order by RecordTime desc";
//            DataTable DT_升温区 = DbUtils.ExecuteDataTable(s_SQL_升温区);
//            this.textBox升温区温度.Text = DT_升温区.Rows[0]["Tem"].ToString();
//            this.textBox升温区湿度.Text = DT_升温区.Rows[0]["Hum"].ToString();
//            string s_SQL_恒温区 = "SELECT Top 1 Tem,Hum FROM rsmonitor.dbo.tbhistory WHERE DeviceID='10008836' order by RecordTime desc";
//            DataTable DT_恒温区 = DbUtils.ExecuteDataTable(s_SQL_恒温区);
//            this.textBox恒温区温度.Text = DT_恒温区.Rows[0]["Tem"].ToString();
//            this.textBox恒温区湿度.Text = DT_恒温区.Rows[0]["Hum"].ToString();
//            string s_SQL_降温区 = "SELECT Top 1 Tem,Hum FROM rsmonitor.dbo.tbhistory WHERE DeviceID='10008834' order by RecordTime desc";
//            DataTable DT_降温区 = DbUtils.ExecuteDataTable(s_SQL_降温区);
//            this.textBox降温区温度.Text = DT_降温区.Rows[0]["Tem"].ToString();
//            this.textBox降温区湿度.Text = DT_降温区.Rows[0]["Hum"].ToString();
//            string s_SQL_大气区 = "SELECT Top 1 Tem,Hum FROM rsmonitor.dbo.tbhistory WHERE DeviceID='10008841' order by RecordTime desc";
//            DataTable DT_大气区 = DbUtils.ExecuteDataTable(s_SQL_大气区);
//            this.textBox大气区温度.Text = DT_大气区.Rows[0]["Tem"].ToString();
//            this.textBox大气区湿度.Text = DT_大气区.Rows[0]["Hum"].ToString();
//        }

//        [System.Runtime.InteropServices.DllImport("user32.dll")]
//        public static extern System.IntPtr GetWindowRect(System.IntPtr hWnd, ref System.Drawing.Rectangle rect);

//        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
//        public static extern System.IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, System.IntPtr lpInitData);

//        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
//        public static extern int BitBlt(System.IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, System.IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

//        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
//        public static extern System.IntPtr CreateCompatibleDC(System.IntPtr hdc);

//        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
//        public static extern System.IntPtr CreateCompatibleBitmap(System.IntPtr hdc, int nWidth, int nHeight);

//        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
//        public static extern System.IntPtr SelectObject(System.IntPtr hdc, System.IntPtr hgdiobj);

//        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
//        public static extern int DeleteDC(System.IntPtr hdc);

//        [System.Runtime.InteropServices.DllImport("user32.dll")]
//        public static extern bool PrintWindow(System.IntPtr hwnd, System.IntPtr hdcBlt, uint nFlags);

//        [System.Runtime.InteropServices.DllImport("user32.dll")]
//        public static extern System.IntPtr GetWindowDC(System.IntPtr hwnd);

//        private void button1_Click(object sender, System.EventArgs e)
//        {
//            this.Update_the_data_to_LED();
//        }

//        private void Update_the_data_to_LED()
//        {
//            Cell cell_升温区温度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox2.Text.Trim()), System.Convert.ToInt32(this.textBox3.Text.Trim()));
//            cell_升温区温度.Text = this.textBox升温区温度.Text.Trim();
//            Cell cell_升温区湿度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox5.Text.Trim()), System.Convert.ToInt32(this.textBox4.Text.Trim()));
//            cell_升温区湿度.Text = this.textBox升温区湿度.Text.Trim();
//            Cell cell_恒温区温度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox9.Text.Trim()), System.Convert.ToInt32(this.textBox9.Text.Trim()));
//            cell_恒温区温度.Text = this.textBox恒温区温度.Text.Trim();
//            Cell cell_恒温区湿度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox7.Text.Trim()), System.Convert.ToInt32(this.textBox6.Text.Trim()));
//            cell_恒温区湿度.Text = this.textBox恒温区湿度.Text.Trim();
//            Cell cell_降温区温度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox13.Text.Trim()), System.Convert.ToInt32(this.textBox12.Text.Trim()));
//            cell_降温区温度.Text = this.textBox降温区温度.Text.Trim();
//            Cell cell_降温区湿度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox11.Text.Trim()), System.Convert.ToInt32(this.textBox10.Text.Trim()));
//            cell_降温区湿度.Text = this.textBox降温区湿度.Text.Trim();
//            Cell cell_大气区温度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox17.Text.Trim()), System.Convert.ToInt32(this.textBox16.Text.Trim()));
//            cell_大气区温度.Text = this.textBox大气区温度.Text.Trim();
//            Cell cell_大气区湿度 = this.grid1.Cell(System.Convert.ToInt32(this.textBox15.Text.Trim()), System.Convert.ToInt32(this.textBox14.Text.Trim()));
//            cell_大气区湿度.Text = this.textBox大气区湿度.Text.Trim();
//        }

//        private void button2_Click(object sender, System.EventArgs e)
//        {
//            this.fm.SendDatatoolStripButton_Click(sender, e);
//        }

//        private void button4_Click(object sender, System.EventArgs e)
//        {
//            this.Update_the_data();
//        }

//        private void button3_Click(object sender, System.EventArgs e)
//        {
//            this.timer3.Interval = System.Convert.ToInt32(this.textBox1.Text.Trim()) * 1000 * 60;
//            this.button5.Visible = true;
//            this.button3.Visible = false;
//            this.Update_data();
//            this.timer3.Start();
//        }

//        private void timer3_Tick(object sender, System.EventArgs e)
//        {
//            this.Update_data();
//        }

//        private void Update_data()
//        {
//            this.Update_the_data();
//            this.Update_the_data_to_LED();
//            this.fm.RedrawSubarea();
//            object sender = null;
//            System.EventArgs e = null;
//            this.fm.SendDatatoolStripButton_Click(sender, e);
//        }

//        private void button5_Click(object sender, System.EventArgs e)
//        {
//            this.button3.Visible = true;
//            this.button5.Visible = false;
//        }
//    }
//}
