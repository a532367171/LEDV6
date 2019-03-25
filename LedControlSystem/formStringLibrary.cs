using LedCommunication;
using LedControlSystem.Fonts;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Data;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formStringLibrary : Form
	{
		public formMain fm;

		private LedPanel panel;

		private LedStringLibrary stringLibrary;

		private ProcessStringLibrary processStringLibrary;

		private bool isProcessString;

		private string LastMessage = "";

		private IList<string> USBDirList;

		private bool isLoading;

		private bool needtoClose;

		private IContainer components;

		private Label lblFontName;

		private Label lblFontSize;

		private Label lblEncoding;

		private CheckBox chkUSBUpdate;

		private ComboBox cmbFontName;

		private ComboBox cmbFontSize;

		private ComboBox cmbEncoding;

		private Panel pnlSetting;

		private Button btnSetting;

		private Panel pnlUSB;

		private Label lblHint;

		private Button btnUSBSave;

		private ComboBox cmbUSB;

		private Label lblUSB;

		private System.Windows.Forms.Timer tmrUSB;

		private Label lblPreview;

		private Panel pnlPreview;

		private PictureBox picPreview;

		private Label lblWidth;

		private NumericUpDown nudWidth;

		private Label lblHeight;

		private NumericUpDown nudHeight;

		private Label lblVerticalStretch;

		private Label lblVerticalStretchOffset;

		private Button btnVerticalStretchUp;

		private Button btnVerticalStretchDown;

		private Button btnZoomOut;

		private Button btnZoomIn;

		private Label lblZoom;

		private Button btnZoomOriginal;

		public formStringLibrary()
		{
			this.InitializeComponent();
		}

		public formStringLibrary(LedStringLibrary psl, formMain pfm)
		{
			this.InitializeComponent();
			this.Text = formMain.ML.GetStr("formStringLibrary_Form_String_Library");
			this.lblWidth.Text = formMain.ML.GetStr("formStringLibrary_label_Width");
			this.lblHeight.Text = formMain.ML.GetStr("formStringLibrary_label_Height");
			this.lblVerticalStretch.Text = formMain.ML.GetStr("formStringLibrary_label_VerticalStretch");
			this.lblFontName.Text = formMain.ML.GetStr("formStringLibrary_label_FontName");
			this.lblFontSize.Text = formMain.ML.GetStr("formStringLibrary_label_FontSize");
			this.lblEncoding.Text = formMain.ML.GetStr("formStringLibrary_label_Encoding");
			this.lblPreview.Text = formMain.ML.GetStr("formStringLibrary_label_Preview");
			this.btnSetting.Text = formMain.ML.GetStr("formStringLibrary_Button_Setting");
			this.chkUSBUpdate.Text = formMain.ML.GetStr("formStringLibrary_checkBox_USBUpdate");
			this.lblUSB.Text = formMain.ML.GetStr("formUSBWrite_label_SelUdisk");
			this.btnUSBSave.Text = formMain.ML.GetStr("UpdateButton_Save");
			this.stringLibrary = psl;
			this.fm = pfm;
			this.panel = formMain.Ledsys.SelectedPanel;
		}

		private void formStringLibrary_Load(object sender, EventArgs e)
		{
			this.isLoading = true;
			this.needtoClose = true;
			base.Size = new System.Drawing.Size(480, 390);
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.pnlUSB.Visible = false;
			this.cmbFontName.Items.Clear();
			IList<string> fontFamilies = new FontFamiliesEx().GetFontFamilies();
			if (fontFamilies != null && fontFamilies.Count > 0)
			{
				using (IEnumerator<string> enumerator = fontFamilies.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						this.cmbFontName.Items.Add(current);
					}
					goto IL_F6;
				}
			}
			string[] fontNames = LedCommonConst.FontNames;
			for (int i = 0; i < fontNames.Length; i++)
			{
				this.cmbFontName.Items.Add(fontNames[i]);
				this.cmbFontName.Items.Add("@" + fontNames[i]);
			}
			IL_F6:
			this.cmbEncoding.Items.Clear();
			foreach (int num in Enum.GetValues(typeof(LedStringEncoding)))
			{
				string name = Enum.GetName(typeof(LedStringEncoding), num);
				this.cmbEncoding.Items.Add(name);
			}
			if (this.stringLibrary != null)
			{
				this.nudWidth.Value = this.stringLibrary.Width;
			}
			else
			{
				this.nudWidth.Value = 16m;
			}
			if (this.stringLibrary != null)
			{
				this.nudHeight.Value = this.stringLibrary.Height;
			}
			else
			{
				this.nudHeight.Value = 16m;
			}
			if (this.stringLibrary != null)
			{
				this.lblVerticalStretchOffset.Text = this.stringLibrary.VerticalStretch.ToString("D3");
			}
			else
			{
				this.lblVerticalStretchOffset.Text = "000";
			}
			if (this.cmbFontName.Items.Count > 0)
			{
				if (this.stringLibrary != null && !string.IsNullOrEmpty(this.stringLibrary.Font.FamilyName))
				{
					int selectedIndex = 0;
					for (int j = 0; j < this.cmbFontName.Items.Count; j++)
					{
						if (this.cmbFontName.Items[j].ToString() == this.stringLibrary.Font.FamilyName)
						{
							selectedIndex = j;
							break;
						}
					}
					this.cmbFontName.SelectedIndex = selectedIndex;
				}
				else
				{
					this.cmbFontName.SelectedIndex = 0;
				}
			}
			if (this.cmbFontSize.Items.Count > 0)
			{
				if (this.stringLibrary != null)
				{
					this.cmbFontSize.Text = this.stringLibrary.Font.Size.ToString();
				}
				else
				{
					this.cmbFontSize.SelectedIndex = 0;
				}
			}
			if (this.cmbEncoding.Items.Count > 0)
			{
				if (this.stringLibrary != null)
				{
					this.cmbEncoding.Text = this.stringLibrary.StringEncoding.ToString();
				}
				else
				{
					this.cmbEncoding.SelectedIndex = 0;
				}
			}
			if (this.stringLibrary != null)
			{
				this.lblZoom.Text = this.stringLibrary.Zoom.ToString();
			}
			else
			{
				this.lblZoom.Text = "1.0";
			}
			this.PreviewDraw();
			this.isLoading = false;
		}

		private void formStringLibrary_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
				return;
			}
			if (this.processStringLibrary != null)
			{
				this.processStringLibrary.Dispose();
				this.processStringLibrary = null;
			}
			if (this.USBDirList != null)
			{
				this.USBDirList.Clear();
				this.USBDirList = null;
			}
			this.tmrUSB.Stop();
			this.panel.SetStringLibrary();
			this.fm.RedrawSubarea();
		}

		private void btnSetting_Click(object sender, EventArgs e)
		{
			this.panel.SetStringLibrary();
			this.fm.isUpdataCode = false;
			this.fm.isDownloadStringLibrary = true;
			this.fm.StartSend(true, this);
		}

		private void btnUSBSave_Click(object sender, EventArgs e)
		{
			this.needtoClose = false;
			this.panel.SetStringLibrary();
			this.tmrUSB.Stop();
			this.cmbFontName.Enabled = false;
			this.cmbFontSize.Enabled = false;
			this.cmbEncoding.Enabled = false;
			this.chkUSBUpdate.Enabled = false;
			this.cmbUSB.Enabled = false;
			this.btnUSBSave.Enabled = false;
			this.nudWidth.Enabled = false;
			this.nudHeight.Enabled = false;
			this.btnVerticalStretchUp.Enabled = false;
			this.btnVerticalStretchDown.Enabled = false;
			this.btnZoomIn.Enabled = false;
			this.btnZoomOut.Enabled = false;
			this.btnZoomOriginal.Enabled = false;
			try
			{
				this.lblHint.Text = formMain.ML.GetStr("USB_SavingData");
				this.lblHint.Refresh();
				Thread.Sleep(500);
				this.isProcessString = true;
				Thread thread = new Thread(new ThreadStart(this.ProcessString));
				thread.Start();
				Thread.Sleep(100);
				while (this.isProcessString)
				{
					Application.DoEvents();
					Thread.Sleep(100);
				}
				protocol_data_integration protocol_data_integration = new protocol_data_integration();
				byte[] array = protocol_data_integration.String_Library_USB(this.processStringLibrary);
				if (array != null)
				{
					string path = string.Empty;
					if (formMain.IsforeignTradeMode)
					{
						string text = this.panel.CardType.ToString();
						if (text.IndexOf("ZH_5W") > -1)
						{
							text = "ZH-5WX";
						}
						else
						{
							text = "ZH-XXL";
						}
						path = this.USBDirList[this.cmbUSB.SelectedIndex] + "ZH_LED\\" + text + "\\led_data.zh";
						Directory.CreateDirectory(this.USBDirList[this.cmbUSB.SelectedIndex] + "ZH_LED\\" + text);
					}
					else
					{
						path = this.USBDirList[this.cmbUSB.SelectedIndex] + "ledV3.zh3";
					}
					FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
					DriveInfo driveInfo = new DriveInfo(this.USBDirList[this.cmbUSB.SelectedIndex]);
					if ((long)array.Length > driveInfo.TotalFreeSpace)
					{
						this.lblHint.Text = string.Empty;
						this.lblHint.Refresh();
						Thread.Sleep(500);
						fileStream.Close();
						MessageBox.Show(this, formMain.ML.GetStr("formUSBWrite_Message_FileOverUsbSize"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
					}
					else
					{
						fileStream.Write(array, 0, array.Length);
						fileStream.Close();
						Thread.Sleep(500);
						this.lblHint.Text = string.Empty;
						this.lblHint.Refresh();
						Thread.Sleep(500);
						if (File.Exists(path))
						{
							MessageBox.Show(this, formMain.ML.GetStr("Prompt_DataDownloadSuccessed"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
							base.Close();
						}
						else
						{
							this.lblHint.Text = formMain.ML.GetStr("Prompt_DataDownloadFailed");
						}
					}
				}
				else
				{
					this.lblHint.Text = formMain.ML.GetStr("Prompt_DataDownloadFailed");
				}
			}
			catch
			{
				this.lblHint.Text = formMain.ML.GetStr("Prompt_DataDownloadFailed");
			}
			finally
			{
				this.cmbFontName.Enabled = true;
				this.cmbFontSize.Enabled = true;
				this.cmbEncoding.Enabled = true;
				this.chkUSBUpdate.Enabled = true;
				this.cmbUSB.Enabled = true;
				this.btnUSBSave.Enabled = true;
				this.nudWidth.Enabled = true;
				this.nudHeight.Enabled = true;
				this.btnVerticalStretchUp.Enabled = true;
				this.btnVerticalStretchDown.Enabled = true;
				this.btnZoomIn.Enabled = true;
				this.btnZoomOut.Enabled = true;
				this.btnZoomOriginal.Enabled = true;
				this.tmrUSB.Start();
				this.needtoClose = true;
			}
		}

		private void ProcessString()
		{
			this.processStringLibrary = new ProcessStringLibrary();
			this.processStringLibrary.BmpDataBytes = this.panel.StringLibrary.ToBmpDataBytes();
			this.processStringLibrary.StartBytes = this.panel.StringLibrary.ToBytes();
			this.processStringLibrary.PanelBytes = this.panel.ToLBytes();
			this.isProcessString = false;
		}

		private void chkUSBUpdate_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkUSBUpdate.Checked)
			{
				this.tmrUSB.Start();
				this.pnlSetting.Visible = false;
				this.pnlUSB.Visible = true;
				this.pnlUSB.Location = new System.Drawing.Point(14, 304);
				base.Size = new System.Drawing.Size(480, 465);
				return;
			}
			this.tmrUSB.Stop();
			this.pnlSetting.Visible = true;
			this.pnlUSB.Visible = false;
			this.pnlSetting.Location = new System.Drawing.Point(14, 304);
			base.Size = new System.Drawing.Size(480, 390);
		}

		private void nudWidth_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (this.isLoading)
			{
				return;
			}
			this.stringLibrary.Width = (byte)numericUpDown.Value;
			this.PreviewDraw();
		}

		private void nudHeight_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown numericUpDown = (NumericUpDown)sender;
			if (this.isLoading)
			{
				return;
			}
			this.stringLibrary.Height = (byte)numericUpDown.Value;
			this.PreviewDraw();
		}

		private void btnVerticalStretchUp_Click(object sender, EventArgs e)
		{
			Control arg_29_0 = this.lblVerticalStretchOffset;
			int num = ++this.stringLibrary.VerticalStretch;
			arg_29_0.Text = num.ToString("D3");
			this.PreviewDraw();
		}

		private void btnVerticalStretchDown_Click(object sender, EventArgs e)
		{
			Control arg_29_0 = this.lblVerticalStretchOffset;
			int num = --this.stringLibrary.VerticalStretch;
			arg_29_0.Text = num.ToString("D3");
			this.PreviewDraw();
		}

		private void cmbFontName_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.stringLibrary.Font.FamilyName = comboBox.Text;
			this.PreviewDraw();
		}

		private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.stringLibrary.Font.Size = float.Parse(comboBox.Text);
			this.PreviewDraw();
		}

		private void cmbEncoding_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.stringLibrary.StringEncoding = (LedStringEncoding)comboBox.SelectedIndex;
		}

		private void tmrUSB_Tick(object sender, EventArgs e)
		{
			this.updataUSB();
		}

		public void updataUSB()
		{
			int selectedIndex = this.cmbUSB.SelectedIndex;
			DriveInfo[] drives = DriveInfo.GetDrives();
			new List<string>();
			this.USBDirList = new List<string>();
			IList<string> list = new List<string>();
			int num = 0;
			for (int i = 0; i < drives.Length; i++)
			{
				if (drives[i].DriveType == DriveType.Removable)
				{
					try
					{
						string text = drives[i].VolumeLabel;
						if (text == "" || text == null)
						{
							text = formMain.ML.GetStr("Prompt_RemovableDisk");
						}
						list.Add(text + "(" + drives[i].Name.Remove(1) + ")");
						this.USBDirList.Add(drives[i].Name);
						num++;
					}
					catch
					{
					}
				}
			}
			if (list.Count != this.cmbUSB.Items.Count)
			{
				this.cmbUSB.Items.Clear();
				foreach (string current in list)
				{
					this.cmbUSB.Items.Add(current);
				}
			}
			if (this.USBDirList.Count >= selectedIndex + 1)
			{
				this.cmbUSB.SelectedIndex = selectedIndex;
			}
			else if (this.USBDirList.Count > 0)
			{
				this.cmbUSB.SelectedIndex = 0;
			}
			bool flag = false;
			if (this.USBDirList.Count == 0)
			{
				this.btnUSBSave.Enabled = false;
				this.lblHint.Text = "";
			}
			else
			{
				flag = true;
			}
			if (this.USBDirList.Count == 1)
			{
				this.cmbUSB.SelectedIndex = 0;
			}
			if (flag)
			{
				if (this.cmbUSB.SelectedIndex == -1)
				{
					this.cmbUSB.SelectedIndex = 0;
				}
				this.btnUSBSave.Enabled = this.IsNTFS(this.USBDirList[this.cmbUSB.SelectedIndex]);
			}
		}

		private bool IsNTFS(string pDisk)
		{
			bool result;
			try
			{
				DriveInfo driveInfo = new DriveInfo(pDisk);
				if (driveInfo.DriveFormat.ToUpper().IndexOf("FAT") >= 0)
				{
					if (driveInfo.DriveFormat.ToUpper() == "FAT32")
					{
						if (this.LastMessage == "notntfs")
						{
							this.lblHint.Text = "";
						}
					}
					else
					{
						this.lblHint.Text = formMain.ML.GetStr("USB_NotFAT32");
						this.lblHint.ForeColor = System.Drawing.Color.Orange;
					}
					result = true;
				}
				else
				{
					this.lblHint.Text = formMain.ML.GetStr("USB_NotSupportFormat");
					this.lblHint.ForeColor = System.Drawing.Color.Red;
					this.LastMessage = "notntfs";
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public void PreviewDraw()
		{
			if (this.stringLibrary == null)
			{
				return;
			}
			this.stringLibrary.PreviewDraw();
			if (this.stringLibrary.LastDrawn == null)
			{
				return;
			}
			System.Drawing.Bitmap bitmap = LedGraphics.ScaleAndGrid(this.stringLibrary.LastDrawn, this.stringLibrary.Zoom);
			if (bitmap != null)
			{
				this.picPreview.Width = bitmap.Width;
				this.picPreview.Height = bitmap.Height;
				this.pnlPreview.AutoScroll = true;
				int x;
				if (this.pnlPreview.Width < bitmap.Width)
				{
					x = 0;
				}
				else
				{
					this.pnlPreview.HorizontalScroll.Value = this.pnlPreview.HorizontalScroll.Minimum;
					this.pnlPreview.HorizontalScroll.Visible = false;
					x = 5;
				}
				int y;
				if (this.pnlPreview.Height < bitmap.Height)
				{
					y = 0;
				}
				else
				{
					this.pnlPreview.VerticalScroll.Value = this.pnlPreview.VerticalScroll.Minimum;
					this.pnlPreview.VerticalScroll.Visible = false;
					y = (this.pnlPreview.Height - bitmap.Height) / 2;
				}
				this.picPreview.Location = new System.Drawing.Point(x, y);
				formMain.ReleasePicture(this.picPreview, bitmap);
			}
		}

		private void btnZoomIn_Click(object sender, EventArgs e)
		{
			if (this.stringLibrary.Zoom == 8.0m)
			{
				return;
			}
			if (this.stringLibrary.Zoom > 0.9m)
			{
				LedStringLibrary expr_41 = this.stringLibrary;
				expr_41.Zoom = ++expr_41.Zoom;
			}
			else
			{
				this.stringLibrary.Zoom += 0.1m;
			}
			this.lblZoom.Text = this.stringLibrary.Zoom.ToString();
			this.PreviewDraw();
		}

		private void btnZoomOut_Click(object sender, EventArgs e)
		{
			if (this.stringLibrary.Zoom < 0.3m)
			{
				return;
			}
			if (this.stringLibrary.Zoom > 1.0m)
			{
				LedStringLibrary expr_40 = this.stringLibrary;
				expr_40.Zoom = --expr_40.Zoom;
			}
			else
			{
				this.stringLibrary.Zoom -= 0.1m;
			}
			this.lblZoom.Text = this.stringLibrary.Zoom.ToString();
			this.PreviewDraw();
		}

		private void btnZoomOriginal_Click(object sender, EventArgs e)
		{
			this.stringLibrary.Zoom = 1.0m;
			this.lblZoom.Text = "1.0";
			this.PreviewDraw();
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
			this.lblFontName = new Label();
			this.lblFontSize = new Label();
			this.lblEncoding = new Label();
			this.chkUSBUpdate = new CheckBox();
			this.cmbFontName = new ComboBox();
			this.cmbFontSize = new ComboBox();
			this.cmbEncoding = new ComboBox();
			this.pnlSetting = new Panel();
			this.btnSetting = new Button();
			this.pnlUSB = new Panel();
			this.lblHint = new Label();
			this.btnUSBSave = new Button();
			this.cmbUSB = new ComboBox();
			this.lblUSB = new Label();
			this.tmrUSB = new System.Windows.Forms.Timer(this.components);
			this.lblPreview = new Label();
			this.pnlPreview = new Panel();
			this.picPreview = new PictureBox();
			this.lblWidth = new Label();
			this.nudWidth = new NumericUpDown();
			this.lblHeight = new Label();
			this.nudHeight = new NumericUpDown();
			this.lblVerticalStretch = new Label();
			this.lblVerticalStretchOffset = new Label();
			this.btnVerticalStretchUp = new Button();
			this.btnVerticalStretchDown = new Button();
			this.btnZoomOut = new Button();
			this.btnZoomIn = new Button();
			this.lblZoom = new Label();
			this.btnZoomOriginal = new Button();
			this.pnlSetting.SuspendLayout();
			this.pnlUSB.SuspendLayout();
			this.pnlPreview.SuspendLayout();
			((ISupportInitialize)this.picPreview).BeginInit();
			((ISupportInitialize)this.nudWidth).BeginInit();
			((ISupportInitialize)this.nudHeight).BeginInit();
			base.SuspendLayout();
			this.lblFontName.AutoSize = true;
			this.lblFontName.Location = new System.Drawing.Point(251, 102);
			this.lblFontName.Name = "lblFontName";
			this.lblFontName.Size = new System.Drawing.Size(41, 12);
			this.lblFontName.TabIndex = 0;
			this.lblFontName.Text = "字体：";
			this.lblFontSize.AutoSize = true;
			this.lblFontSize.Location = new System.Drawing.Point(251, 57);
			this.lblFontSize.Name = "lblFontSize";
			this.lblFontSize.Size = new System.Drawing.Size(41, 12);
			this.lblFontSize.TabIndex = 1;
			this.lblFontSize.Text = "字号：";
			this.lblEncoding.AutoSize = true;
			this.lblEncoding.Location = new System.Drawing.Point(251, 18);
			this.lblEncoding.Name = "lblEncoding";
			this.lblEncoding.Size = new System.Drawing.Size(41, 12);
			this.lblEncoding.TabIndex = 2;
			this.lblEncoding.Text = "编码：";
			this.chkUSBUpdate.AutoSize = true;
			this.chkUSBUpdate.Location = new System.Drawing.Point(14, 282);
			this.chkUSBUpdate.Name = "chkUSBUpdate";
			this.chkUSBUpdate.Size = new System.Drawing.Size(90, 16);
			this.chkUSBUpdate.TabIndex = 3;
			this.chkUSBUpdate.Text = "U盘字库更新";
			this.chkUSBUpdate.UseVisualStyleBackColor = true;
			this.chkUSBUpdate.CheckedChanged += new EventHandler(this.chkUSBUpdate_CheckedChanged);
			this.cmbFontName.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbFontName.FormattingEnabled = true;
			this.cmbFontName.Location = new System.Drawing.Point(322, 99);
			this.cmbFontName.Name = "cmbFontName";
			this.cmbFontName.Size = new System.Drawing.Size(128, 20);
			this.cmbFontName.TabIndex = 4;
			this.cmbFontName.SelectedIndexChanged += new EventHandler(this.cmbFontName_SelectedIndexChanged);
			this.cmbFontSize.DropDownStyle = ComboBoxStyle.DropDownList;
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
			this.cmbFontSize.Location = new System.Drawing.Point(322, 54);
			this.cmbFontSize.Name = "cmbFontSize";
			this.cmbFontSize.Size = new System.Drawing.Size(128, 20);
			this.cmbFontSize.TabIndex = 5;
			this.cmbFontSize.SelectedIndexChanged += new EventHandler(this.cmbFontSize_SelectedIndexChanged);
			this.cmbEncoding.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbEncoding.FormattingEnabled = true;
			this.cmbEncoding.Location = new System.Drawing.Point(322, 15);
			this.cmbEncoding.Name = "cmbEncoding";
			this.cmbEncoding.Size = new System.Drawing.Size(128, 20);
			this.cmbEncoding.TabIndex = 6;
			this.cmbEncoding.SelectedIndexChanged += new EventHandler(this.cmbEncoding_SelectedIndexChanged);
			this.pnlSetting.Controls.Add(this.btnSetting);
			this.pnlSetting.Location = new System.Drawing.Point(14, 304);
			this.pnlSetting.Name = "pnlSetting";
			this.pnlSetting.Size = new System.Drawing.Size(436, 44);
			this.pnlSetting.TabIndex = 7;
			this.btnSetting.Location = new System.Drawing.Point(347, 12);
			this.btnSetting.Name = "btnSetting";
			this.btnSetting.Size = new System.Drawing.Size(75, 23);
			this.btnSetting.TabIndex = 0;
			this.btnSetting.Text = "设置";
			this.btnSetting.UseVisualStyleBackColor = true;
			this.btnSetting.Click += new EventHandler(this.btnSetting_Click);
			this.pnlUSB.Controls.Add(this.lblHint);
			this.pnlUSB.Controls.Add(this.btnUSBSave);
			this.pnlUSB.Controls.Add(this.cmbUSB);
			this.pnlUSB.Controls.Add(this.lblUSB);
			this.pnlUSB.Location = new System.Drawing.Point(14, 368);
			this.pnlUSB.Name = "pnlUSB";
			this.pnlUSB.Size = new System.Drawing.Size(436, 115);
			this.pnlUSB.TabIndex = 13;
			this.lblHint.Location = new System.Drawing.Point(17, 48);
			this.lblHint.Name = "lblHint";
			this.lblHint.Size = new System.Drawing.Size(405, 45);
			this.lblHint.TabIndex = 22;
			this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnUSBSave.Location = new System.Drawing.Point(368, 16);
			this.btnUSBSave.Name = "btnUSBSave";
			this.btnUSBSave.Size = new System.Drawing.Size(54, 23);
			this.btnUSBSave.TabIndex = 20;
			this.btnUSBSave.Text = "保存";
			this.btnUSBSave.UseVisualStyleBackColor = true;
			this.btnUSBSave.Click += new EventHandler(this.btnUSBSave_Click);
			this.cmbUSB.Cursor = Cursors.Default;
			this.cmbUSB.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbUSB.FormattingEnabled = true;
			this.cmbUSB.ImeMode = ImeMode.On;
			this.cmbUSB.Location = new System.Drawing.Point(78, 18);
			this.cmbUSB.Name = "cmbUSB";
			this.cmbUSB.Size = new System.Drawing.Size(266, 20);
			this.cmbUSB.TabIndex = 18;
			this.lblUSB.Location = new System.Drawing.Point(15, 15);
			this.lblUSB.Name = "lblUSB";
			this.lblUSB.Size = new System.Drawing.Size(47, 25);
			this.lblUSB.TabIndex = 19;
			this.lblUSB.Text = "选择U盘";
			this.lblUSB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.tmrUSB.Interval = 1000;
			this.tmrUSB.Tick += new EventHandler(this.tmrUSB_Tick);
			this.lblPreview.AutoSize = true;
			this.lblPreview.Location = new System.Drawing.Point(12, 196);
			this.lblPreview.Name = "lblPreview";
			this.lblPreview.Size = new System.Drawing.Size(41, 12);
			this.lblPreview.TabIndex = 17;
			this.lblPreview.Text = "预览：";
			this.pnlPreview.BorderStyle = BorderStyle.FixedSingle;
			this.pnlPreview.Controls.Add(this.picPreview);
			this.pnlPreview.Location = new System.Drawing.Point(80, 133);
			this.pnlPreview.Name = "pnlPreview";
			this.pnlPreview.Size = new System.Drawing.Size(370, 138);
			this.pnlPreview.TabIndex = 18;
			this.picPreview.Location = new System.Drawing.Point(5, 37);
			this.picPreview.Name = "picPreview";
			this.picPreview.Size = new System.Drawing.Size(64, 64);
			this.picPreview.TabIndex = 17;
			this.picPreview.TabStop = false;
			this.lblWidth.AutoSize = true;
			this.lblWidth.Location = new System.Drawing.Point(12, 18);
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.Size = new System.Drawing.Size(41, 12);
			this.lblWidth.TabIndex = 19;
			this.lblWidth.Text = "宽度：";
			NumericUpDown arg_B6B_0 = this.nudWidth;
			int[] array = new int[4];
			array[0] = 2;
			arg_B6B_0.Increment = new decimal(array);
			this.nudWidth.Location = new System.Drawing.Point(80, 14);
			NumericUpDown arg_B9F_0 = this.nudWidth;
			int[] array2 = new int[4];
			array2[0] = 128;
			arg_B9F_0.Maximum = new decimal(array2);
			NumericUpDown arg_BBB_0 = this.nudWidth;
			int[] array3 = new int[4];
			array3[0] = 8;
			arg_BBB_0.Minimum = new decimal(array3);
			this.nudWidth.Name = "nudWidth";
			this.nudWidth.Size = new System.Drawing.Size(128, 21);
			this.nudWidth.TabIndex = 20;
			NumericUpDown arg_C0E_0 = this.nudWidth;
			int[] array4 = new int[4];
			array4[0] = 8;
			arg_C0E_0.Value = new decimal(array4);
			this.nudWidth.ValueChanged += new EventHandler(this.nudWidth_ValueChanged);
			this.lblHeight.AutoSize = true;
			this.lblHeight.Location = new System.Drawing.Point(12, 57);
			this.lblHeight.Name = "lblHeight";
			this.lblHeight.Size = new System.Drawing.Size(41, 12);
			this.lblHeight.TabIndex = 21;
			this.lblHeight.Text = "高度：";
			NumericUpDown arg_CA5_0 = this.nudHeight;
			int[] array5 = new int[4];
			array5[0] = 2;
			arg_CA5_0.Increment = new decimal(array5);
			this.nudHeight.Location = new System.Drawing.Point(80, 53);
			NumericUpDown arg_CDC_0 = this.nudHeight;
			int[] array6 = new int[4];
			array6[0] = 128;
			arg_CDC_0.Maximum = new decimal(array6);
			NumericUpDown arg_CFB_0 = this.nudHeight;
			int[] array7 = new int[4];
			array7[0] = 8;
			arg_CFB_0.Minimum = new decimal(array7);
			this.nudHeight.Name = "nudHeight";
			this.nudHeight.Size = new System.Drawing.Size(128, 21);
			this.nudHeight.TabIndex = 22;
			NumericUpDown arg_D4E_0 = this.nudHeight;
			int[] array8 = new int[4];
			array8[0] = 8;
			arg_D4E_0.Value = new decimal(array8);
			this.nudHeight.ValueChanged += new EventHandler(this.nudHeight_ValueChanged);
			this.lblVerticalStretch.AutoSize = true;
			this.lblVerticalStretch.Location = new System.Drawing.Point(12, 102);
			this.lblVerticalStretch.Name = "lblVerticalStretch";
			this.lblVerticalStretch.Size = new System.Drawing.Size(41, 12);
			this.lblVerticalStretch.TabIndex = 23;
			this.lblVerticalStretch.Text = "拉伸：";
			this.lblVerticalStretchOffset.AutoSize = true;
			this.lblVerticalStretchOffset.Location = new System.Drawing.Point(103, 102);
			this.lblVerticalStretchOffset.Name = "lblVerticalStretchOffset";
			this.lblVerticalStretchOffset.Size = new System.Drawing.Size(23, 12);
			this.lblVerticalStretchOffset.TabIndex = 24;
			this.lblVerticalStretchOffset.Text = "000";
			this.btnVerticalStretchUp.FlatStyle = FlatStyle.Flat;
			this.btnVerticalStretchUp.Location = new System.Drawing.Point(80, 98);
			this.btnVerticalStretchUp.Name = "btnVerticalStretchUp";
			this.btnVerticalStretchUp.Size = new System.Drawing.Size(20, 20);
			this.btnVerticalStretchUp.TabIndex = 25;
			this.btnVerticalStretchUp.Text = "↑";
			this.btnVerticalStretchUp.UseVisualStyleBackColor = true;
			this.btnVerticalStretchUp.Click += new EventHandler(this.btnVerticalStretchUp_Click);
			this.btnVerticalStretchDown.FlatStyle = FlatStyle.Flat;
			this.btnVerticalStretchDown.Location = new System.Drawing.Point(130, 98);
			this.btnVerticalStretchDown.Name = "btnVerticalStretchDown";
			this.btnVerticalStretchDown.Size = new System.Drawing.Size(20, 20);
			this.btnVerticalStretchDown.TabIndex = 26;
			this.btnVerticalStretchDown.Text = "↓";
			this.btnVerticalStretchDown.UseVisualStyleBackColor = true;
			this.btnVerticalStretchDown.Click += new EventHandler(this.btnVerticalStretchDown_Click);
			this.btnZoomOut.FlatStyle = FlatStyle.Flat;
			this.btnZoomOut.Location = new System.Drawing.Point(410, 279);
			this.btnZoomOut.Name = "btnZoomOut";
			this.btnZoomOut.Size = new System.Drawing.Size(20, 20);
			this.btnZoomOut.TabIndex = 27;
			this.btnZoomOut.Text = "－";
			this.btnZoomOut.UseVisualStyleBackColor = true;
			this.btnZoomOut.Click += new EventHandler(this.btnZoomOut_Click);
			this.btnZoomIn.FlatStyle = FlatStyle.Flat;
			this.btnZoomIn.Location = new System.Drawing.Point(353, 279);
			this.btnZoomIn.Name = "btnZoomIn";
			this.btnZoomIn.Size = new System.Drawing.Size(20, 20);
			this.btnZoomIn.TabIndex = 28;
			this.btnZoomIn.Text = "＋";
			this.btnZoomIn.UseVisualStyleBackColor = true;
			this.btnZoomIn.Click += new EventHandler(this.btnZoomIn_Click);
			this.lblZoom.Location = new System.Drawing.Point(376, 279);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Size = new System.Drawing.Size(32, 20);
			this.lblZoom.TabIndex = 29;
			this.lblZoom.Text = "0.0";
			this.lblZoom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnZoomOriginal.FlatStyle = FlatStyle.Flat;
			this.btnZoomOriginal.Location = new System.Drawing.Point(430, 279);
			this.btnZoomOriginal.Name = "btnZoomOriginal";
			this.btnZoomOriginal.Size = new System.Drawing.Size(20, 20);
			this.btnZoomOriginal.TabIndex = 30;
			this.btnZoomOriginal.Text = "□";
			this.btnZoomOriginal.UseVisualStyleBackColor = true;
			this.btnZoomOriginal.Click += new EventHandler(this.btnZoomOriginal_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(464, 492);
			base.Controls.Add(this.btnZoomOriginal);
			base.Controls.Add(this.lblZoom);
			base.Controls.Add(this.btnZoomIn);
			base.Controls.Add(this.btnZoomOut);
			base.Controls.Add(this.btnVerticalStretchDown);
			base.Controls.Add(this.btnVerticalStretchUp);
			base.Controls.Add(this.lblVerticalStretchOffset);
			base.Controls.Add(this.lblVerticalStretch);
			base.Controls.Add(this.nudHeight);
			base.Controls.Add(this.lblHeight);
			base.Controls.Add(this.nudWidth);
			base.Controls.Add(this.lblWidth);
			base.Controls.Add(this.pnlPreview);
			base.Controls.Add(this.lblPreview);
			base.Controls.Add(this.pnlUSB);
			base.Controls.Add(this.pnlSetting);
			base.Controls.Add(this.cmbEncoding);
			base.Controls.Add(this.cmbFontSize);
			base.Controls.Add(this.cmbFontName);
			base.Controls.Add(this.chkUSBUpdate);
			base.Controls.Add(this.lblEncoding);
			base.Controls.Add(this.lblFontSize);
			base.Controls.Add(this.lblFontName);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formStringLibrary";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "字符字库";
			base.FormClosing += new FormClosingEventHandler(this.formStringLibrary_FormClosing);
			base.Load += new EventHandler(this.formStringLibrary_Load);
			this.pnlSetting.ResumeLayout(false);
			this.pnlUSB.ResumeLayout(false);
			this.pnlPreview.ResumeLayout(false);
			((ISupportInitialize)this.picPreview).EndInit();
			((ISupportInitialize)this.nudWidth).EndInit();
			((ISupportInitialize)this.nudHeight).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
