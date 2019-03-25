using AxShockwaveFlashObjects;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace LedControlSystem.LedControlSystem
{
	public class formMaterialEdit : Form
	{
		private const int SRCCOPY = 13369376;

		private string destPath;

		private string sourPath;

		private string previewPath;

		private string basisPath;

		private string filename;

		private string extend;

		private string Suffix;

		private System.Drawing.Image curbitmap;

		private IList<string> materialNameNum = new List<string>();

		private LedMaterial MaterialParam = new LedMaterial();

		private IContainer components;

		private PictureBox pictureBoxMaterial;

		private AxShockwaveFlash axShockwaveFlashMaterial;

		private GroupBox groupBox1;

		private FlowLayoutPanel swfPreviews;

		private Timer timer1;

		private Button BtnOriginal;

		private Button BtnStretch;

		private Button BtnZoom;

		private Button BtnTile;

		private Button buttonOK;

		public string previewFile
		{
			get
			{
				return this.previewPath + this.filename + this.Suffix.Replace(".", "") + this.extend;
			}
		}

		[DllImport("gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, long dwRop);

		private void reset()
		{
			this.destPath = "";
			this.sourPath = "";
			this.previewPath = "";
			this.filename = "";
			this.extend = "";
			this.Suffix = "";
		}

		public static string EditMaterial(LedMaterialType ftype)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "(*.jpg;*.bmp;*.png;*.gif;*.swf;)|*.jpg;*.bmp;*.png;*.gif;*.swf";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (string.Equals(Path.GetExtension(openFileDialog.FileName), ".swf", StringComparison.CurrentCultureIgnoreCase))
				{
					ftype = LedMaterialType.Flash;
				}
				else if (string.Equals(Path.GetExtension(openFileDialog.FileName), ".gif", StringComparison.CurrentCultureIgnoreCase))
				{
					ftype = LedMaterialType.Gif;
				}
				else if (string.Equals(Path.GetExtension(openFileDialog.FileName), ".png", StringComparison.CurrentCultureIgnoreCase) || string.Equals(Path.GetExtension(openFileDialog.FileName), ".jpg", StringComparison.CurrentCultureIgnoreCase) || string.Equals(Path.GetExtension(openFileDialog.FileName), ".bmp", StringComparison.CurrentCultureIgnoreCase))
				{
					ftype = LedMaterialType.image;
				}
				formMaterialEdit formMaterialEdit = new formMaterialEdit(openFileDialog.FileName, ftype);
				DialogResult dialogResult = formMaterialEdit.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					return formMaterialEdit.destPath;
				}
			}
			return "";
		}

		public formMaterialEdit(string path, LedMaterialType ftype)
		{
			this.InitializeComponent();
			this.Suffix = Path.GetExtension(path);
			foreach (LedMaterial current in LedGlobal.LedMaterialList)
			{
				this.materialNameNum.Add(Path.GetFileNameWithoutExtension(current.Name));
			}
			this.previewPath = LedCommonConst.MaterialPreviewPath;
			this.basisPath = LedCommonConst.MaterialPath;
			this.MaterialParam.Type = ftype;
			string extension = Path.GetExtension(path);
			this.SetMaterialDestFileName(extension);
			this.sourPath = path;
			if (this.MaterialParam.Type == LedMaterialType.Gif)
			{
				this.pictureBoxMaterial.ImageLocation = path;
				return;
			}
			if (this.MaterialParam.Type == LedMaterialType.image)
			{
				this.pictureBoxMaterial.ImageLocation = path;
				return;
			}
			if (this.MaterialParam.Type == LedMaterialType.Flash)
			{
				this.pictureBoxMaterial.Hide();
				this.axShockwaveFlashMaterial.Show();
				this.axShockwaveFlashMaterial.Visible = true;
				this.axShockwaveFlashMaterial.BringToFront();
				this.axShockwaveFlashMaterial.Movie = this.sourPath;
				this.axShockwaveFlashMaterial.Play();
			}
		}

		private void formMaterialEdit_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.BtnTile.Text = formMain.ML.GetStr("formBackgroundEffectSelecter_Effect_Tiling");
			this.BtnStretch.Text = formMain.ML.GetStr("formBackgroundEffectSelecter_Effect_Stretch");
			this.BtnOriginal.Text = formMain.ML.GetStr("formBackgroundEffectSelecter_Effect_Original");
			this.buttonOK.Text = formMain.ML.GetStr("Global_Messagebox_OK");
			if (this.MaterialParam.Type == LedMaterialType.Flash)
			{
				this.swfPreviews.Location = this.pictureBoxMaterial.Location;
				this.swfPreviews.Size = new System.Drawing.Size(this.pictureBoxMaterial.Size.Width / 2, this.pictureBoxMaterial.Size.Height);
				this.pictureBoxMaterial.Location = new System.Drawing.Point(this.pictureBoxMaterial.Location.X + this.swfPreviews.Size.Width, this.pictureBoxMaterial.Location.Y);
				this.pictureBoxMaterial.Size = this.swfPreviews.Size;
				this.timer1.Interval = 1000;
				this.timer1.Start();
			}
			this.axShockwaveFlashMaterial.Location = this.pictureBoxMaterial.Location;
			this.axShockwaveFlashMaterial.Size = this.pictureBoxMaterial.Size;
		}

		private void SetMaterialDestFileName(string destExt)
		{
			string a = destExt.Substring(1);
			string str = this.basisPath;
			if (a == "swf")
			{
				str = LedCommonConst.MaterialSwfPath;
			}
			else if (a == "gif")
			{
				str = LedCommonConst.MaterialGifPath;
			}
			else if (a == "jpg" || a == "bmp" || a == "png")
			{
				str = LedCommonConst.MaterialImagePath;
			}
			this.filename = "BM";
			int num = 0;
			string text;
			do
			{
				text = this.filename + num.ToString("D4");
				num++;
			}
			while (this.materialNameNum.Contains(text));
            this.previewPath=this.previewPath + text + this.Suffix.Replace(".", "") + ".bmp";
			this.filename = text;
			this.destPath = str + text + destExt;
		}

		private static System.Drawing.Bitmap GetImageOfControl(Control control)
		{
			int width = control.Size.Width;
			int height = control.Size.Height;
			System.Drawing.Graphics graphics = control.CreateGraphics();
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height, graphics);
			System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap);
			IntPtr hdc = graphics.GetHdc();
			IntPtr hdc2 = graphics2.GetHdc();
			formMaterialEdit.BitBlt(hdc2, 0, 0, width, height, hdc, 0, 0, 13369376L);
			graphics.ReleaseHdc(hdc);
			graphics2.ReleaseHdc(hdc2);
			graphics.Dispose();
			graphics2.Dispose();
			return bitmap;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				File.Copy(this.sourPath, this.destPath);
			}
			catch
			{
				MessageBox.Show("\"" + Path.GetFileName(this.destPath) + "\"" + formMain.ML.GetStr("NETCARD_message_Save_Failed"));
				base.DialogResult = DialogResult.Cancel;
				base.Close();
				return;
			}
			if (this.SavePreview())
			{
				this.MaterialParam.Name = this.filename + this.Suffix;
				this.MaterialParam.MaterialPreview = System.Drawing.Image.FromFile(LedCommonConst.MaterialPreviewPath + this.filename + this.Suffix.Replace(".", "") + this.extend);
				this.MaterialSaveToXml(this.MaterialParam);
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		public bool MaterialSaveToXml(LedMaterial material)
		{
			XmlDocument xmlDocument = new XmlDocument();
			bool result;
			try
			{
				xmlDocument.Load(LedCommonConst.MaterialInfoPath);
				XmlElement xmlElement = xmlDocument.CreateElement(material.Name);
				xmlElement.SetAttribute("SourceWidth", "32");
				xmlElement.SetAttribute("SourceHeight", "32");
				xmlElement.SetAttribute("Type", ((byte)material.Type).ToString());
				xmlDocument.DocumentElement.AppendChild(xmlElement);
				xmlDocument.Save(LedCommonConst.MaterialInfoPath);
				LedGlobal.LedMaterialList.Add(material);
				formBackgroundEffectSelecter.LoadedMaterials.Add(material.Name, material);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private bool SavePreview()
		{
			this.extend = ".bmp";
			string text = this.previewPath + this.filename + this.Suffix.Replace(".", "") + this.extend;
			if (this.MaterialParam.Type == LedMaterialType.Gif && this.destPath.EndsWith(".gif"))
			{
				System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.pictureBoxMaterial.Size.Width, this.pictureBoxMaterial.Size.Height);
				if (this.pictureBoxMaterial.SizeMode != PictureBoxSizeMode.StretchImage)
				{
					this.pictureBoxMaterial.SizeMode = PictureBoxSizeMode.Zoom;
				}
				this.pictureBoxMaterial.DrawToBitmap(bitmap, new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), this.pictureBoxMaterial.Size));
				bitmap.Save(text);
			}
			else if (this.MaterialParam.Type == LedMaterialType.Flash)
			{
				if (this.curbitmap == null)
				{
					this.reset();
					return false;
				}
				this.curbitmap.Save(text);
			}
			else if (this.MaterialParam.Type == LedMaterialType.image && (this.destPath.EndsWith(".png") || this.destPath.EndsWith(".jpg") || this.destPath.EndsWith(".bmp")))
			{
				System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(this.pictureBoxMaterial.Size.Width, this.pictureBoxMaterial.Size.Height);
				if (this.pictureBoxMaterial.SizeMode != PictureBoxSizeMode.StretchImage)
				{
					this.pictureBoxMaterial.SizeMode = PictureBoxSizeMode.Zoom;
				}
				this.pictureBoxMaterial.DrawToBitmap(bitmap2, new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), this.pictureBoxMaterial.Size));
				bitmap2.Save(text);
			}
			else
			{
				text = this.destPath;
				this.extend = Path.GetExtension(this.destPath);
			}
			return true;
		}

		private int addPreview(System.Drawing.Bitmap bmap)
		{
			Button button = new Button();
			button.Image = bmap;
			button.Size = bmap.Size;
			this.swfPreviews.Controls.Add(button);
			button.Click += new EventHandler(this.bitmap_Click);
			if (this.curbitmap == null)
			{
				this.bitmap_Click(button, null);
			}
			return this.swfPreviews.Controls.Count;
		}

		private void bitmap_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			this.curbitmap = button.Image;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			int num = this.addPreview(formMaterialEdit.GetImageOfControl(this.axShockwaveFlashMaterial));
			if ((double)this.axShockwaveFlashMaterial.FrameNum > (double)this.axShockwaveFlashMaterial.TotalFrames * 0.8 || num > 10)
			{
				this.timer1.Stop();
			}
		}

		private void BtnTile_Click(object sender, EventArgs e)
		{
			this.pictureBoxMaterial.SizeMode = PictureBoxSizeMode.Normal;
		}

		private void BtnZoom_Click(object sender, EventArgs e)
		{
			this.pictureBoxMaterial.SizeMode = PictureBoxSizeMode.Zoom;
		}

		private void BtnStretch_Click(object sender, EventArgs e)
		{
			this.pictureBoxMaterial.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		private void BtnOriginal_Click(object sender, EventArgs e)
		{
			this.pictureBoxMaterial.SizeMode = PictureBoxSizeMode.CenterImage;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formMaterialEdit));
			this.pictureBoxMaterial = new PictureBox();
			this.axShockwaveFlashMaterial = new AxShockwaveFlash();
			this.groupBox1 = new GroupBox();
			this.BtnOriginal = new Button();
			this.BtnStretch = new Button();
			this.BtnTile = new Button();
			this.BtnZoom = new Button();
			this.swfPreviews = new FlowLayoutPanel();
			this.timer1 = new Timer(this.components);
			this.buttonOK = new Button();
			((ISupportInitialize)this.pictureBoxMaterial).BeginInit();
			((ISupportInitialize)this.axShockwaveFlashMaterial).BeginInit();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.pictureBoxMaterial.BackColor = System.Drawing.Color.Transparent;
			this.pictureBoxMaterial.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBoxMaterial.Location = new System.Drawing.Point(12, 12);
			this.pictureBoxMaterial.Name = "pictureBoxMaterial";
			this.pictureBoxMaterial.Size = new System.Drawing.Size(447, 142);
			this.pictureBoxMaterial.TabIndex = 0;
			this.pictureBoxMaterial.TabStop = false;
			this.axShockwaveFlashMaterial.Enabled = true;
			this.axShockwaveFlashMaterial.Location = new System.Drawing.Point(515, 166);
			this.axShockwaveFlashMaterial.Name = "axShockwaveFlashMaterial";
			this.axShockwaveFlashMaterial.OcxState = (AxHost.State)componentResourceManager.GetObject("axShockwaveFlashMaterial.OcxState");
			this.axShockwaveFlashMaterial.Size = new System.Drawing.Size(192, 192);
			this.axShockwaveFlashMaterial.TabIndex = 26;
			this.axShockwaveFlashMaterial.Visible = false;
			this.groupBox1.Controls.Add(this.BtnOriginal);
			this.groupBox1.Controls.Add(this.BtnStretch);
			this.groupBox1.Controls.Add(this.BtnTile);
			this.groupBox1.Location = new System.Drawing.Point(12, 162);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(447, 60);
			this.groupBox1.TabIndex = 27;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "效果展示";
			this.BtnOriginal.Location = new System.Drawing.Point(346, 24);
			this.BtnOriginal.Name = "BtnOriginal";
			this.BtnOriginal.Size = new System.Drawing.Size(55, 23);
			this.BtnOriginal.TabIndex = 4;
			this.BtnOriginal.Text = "原始";
			this.BtnOriginal.UseVisualStyleBackColor = true;
			this.BtnOriginal.Click += new EventHandler(this.BtnOriginal_Click);
			this.BtnStretch.Location = new System.Drawing.Point(196, 24);
			this.BtnStretch.Name = "BtnStretch";
			this.BtnStretch.Size = new System.Drawing.Size(55, 23);
			this.BtnStretch.TabIndex = 3;
			this.BtnStretch.Text = "拉伸";
			this.BtnStretch.UseVisualStyleBackColor = true;
			this.BtnStretch.Click += new EventHandler(this.BtnStretch_Click);
			this.BtnTile.Location = new System.Drawing.Point(46, 24);
			this.BtnTile.Name = "BtnTile";
			this.BtnTile.Size = new System.Drawing.Size(55, 23);
			this.BtnTile.TabIndex = 1;
			this.BtnTile.Text = "平铺";
			this.BtnTile.UseVisualStyleBackColor = true;
			this.BtnTile.Click += new EventHandler(this.BtnTile_Click);
			this.BtnZoom.Location = new System.Drawing.Point(727, 131);
			this.BtnZoom.Name = "BtnZoom";
			this.BtnZoom.Size = new System.Drawing.Size(55, 23);
			this.BtnZoom.TabIndex = 2;
			this.BtnZoom.Text = "缩放";
			this.BtnZoom.UseVisualStyleBackColor = true;
			this.BtnZoom.Visible = false;
			this.BtnZoom.Click += new EventHandler(this.BtnZoom_Click);
			this.swfPreviews.AutoScroll = true;
			this.swfPreviews.BorderStyle = BorderStyle.FixedSingle;
			this.swfPreviews.Location = new System.Drawing.Point(515, 44);
			this.swfPreviews.Name = "swfPreviews";
			this.swfPreviews.Size = new System.Drawing.Size(192, 110);
			this.swfPreviews.TabIndex = 29;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.buttonOK.Location = new System.Drawing.Point(189, 236);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(94, 27);
			this.buttonOK.TabIndex = 30;
			this.buttonOK.Text = "确认";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(471, 277);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.swfPreviews);
			base.Controls.Add(this.BtnZoom);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.axShockwaveFlashMaterial);
			base.Controls.Add(this.pictureBoxMaterial);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = Resources.AppIcon;
			base.Name = "formMaterialEdit";
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += new EventHandler(this.formMaterialEdit_Load);
			((ISupportInitialize)this.pictureBoxMaterial).EndInit();
			((ISupportInitialize)this.axShockwaveFlashMaterial).EndInit();
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
