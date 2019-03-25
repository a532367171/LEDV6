using AxShockwaveFlashObjects;
using LedControlSystem.Properties;
using LedModel.Const;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formBackgroundEffectSelecter : Form
	{
		private struct MouseEvents
		{
			private EventHandler RadioButton_Click_;

			private EventHandler Picture_Click_;

			private EventHandler MouseEnter_;

			private EventHandler MouseLeave_;

			public EventHandler RadioButton_Click
			{
				get
				{
					return this.RadioButton_Click_;
				}
				set
				{
					this.RadioButton_Click_ = value;
				}
			}

			public EventHandler Picture_Click
			{
				get
				{
					return this.Picture_Click_;
				}
				set
				{
					this.Picture_Click_ = value;
				}
			}

			public EventHandler MouseEnter
			{
				get
				{
					return this.MouseEnter_;
				}
				set
				{
					this.MouseEnter_ = value;
				}
			}

			public EventHandler MouseLeave
			{
				get
				{
					return this.MouseLeave_;
				}
				set
				{
					this.MouseLeave_ = value;
				}
			}

			public MouseEvents(EventHandler rb_clk, EventHandler pic_clk, EventHandler mos_ent, EventHandler mos_lea)
			{
				this.RadioButton_Click_ = rb_clk;
				this.Picture_Click_ = pic_clk;
				this.MouseEnter_ = mos_ent;
				this.MouseLeave_ = mos_lea;
			}
		}

		private const string G_GIF = "gif";

		private const string G_SWF = "swf";

		private LedBackground background;

		public static Dictionary<string, LedMaterial> LoadedMaterials = new Dictionary<string, LedMaterial>();

		private string backOriginalName;

		private LedMaterialType backOriginalType;

		private LedImageFillMode backOriginalLayout;

		private formBackgroundEffectSelecter.MouseEvents events_mouse;

		private string[] cancelEffect = new string[]
		{
			"DownEgg",
			"DownGongxi"
		};

		private static int materialno = 0;

		private IContainer components;

		private Button button1;

		private Panel panel1;

		private FlowLayoutPanel panel_Picture;

		private AxShockwaveFlash axShockwaveFlash1;

		private Button buttonOk;

		private Button buttonAdd;

		private PictureBox pictureBox_View;

		private Panel panel2;

		private Panel panel3;

		private RadioButton RdoZoom;

		private RadioButton RdoOriginal;

		private RadioButton RdoStretch;

		private RadioButton RdoTiling;

		private Button buttonCancel;

		private Label LblBackgroundFillType;

		public formBackgroundEffectSelecter()
		{
			this.InitializeComponent();
			this.events_mouse = new formBackgroundEffectSelecter.MouseEvents(new EventHandler(this.rb_Click), new EventHandler(this.pic_Click), new EventHandler(this.pic_MouseEnter), new EventHandler(this.pic_MouseLeave));
		}

		private string PlayPreView(string fname)
		{
			if (fname == "")
			{
				this.axShockwaveFlash1.Hide();
				this.pictureBox_View.Hide();
				return "";
			}
			if (fname.EndsWith(".png") || fname.EndsWith(".jpg") || fname.EndsWith(".bmp"))
			{
				this.axShockwaveFlash1.Hide();
				this.axShockwaveFlash1.Stop();
				this.pictureBox_View.Show();
				this.pictureBox_View.ImageLocation = fname;
				this.pictureBox_View.SizeMode = PictureBoxSizeMode.StretchImage;
				this.pictureBox_View.BringToFront();
			}
			if (fname.EndsWith("gif"))
			{
				this.axShockwaveFlash1.Hide();
				this.axShockwaveFlash1.Stop();
				this.pictureBox_View.Show();
				this.pictureBox_View.ImageLocation = fname;
				this.pictureBox_View.SizeMode = PictureBoxSizeMode.StretchImage;
				this.pictureBox_View.BringToFront();
			}
			else if (fname.EndsWith("swf"))
			{
				this.pictureBox_View.Hide();
				this.axShockwaveFlash1.Show();
				this.axShockwaveFlash1.Stop();
				this.axShockwaveFlash1.Movie = fname;
				this.axShockwaveFlash1.Play();
				this.axShockwaveFlash1.Visible = true;
				this.axShockwaveFlash1.BringToFront();
			}
			return fname;
		}

		private void LoadMaterial(LedMaterialType type)
		{
			if (type != LedMaterialType.Text)
			{
				this.LoadBackgroundMaterial_Pic();
			}
		}

		private void LoadBackgroundMaterial_Pic()
		{
			this.panel_Picture.BringToFront();
			this.panel_Picture.Show();
			this.pictureBox_View.Show();
			this.axShockwaveFlash1.Hide();
			this.panel_Picture.Controls.Clear();
			formBackgroundEffectSelecter.materialno = 0;
			foreach (string current in formBackgroundEffectSelecter.LoadedMaterials.Keys)
			{
				if (formBackgroundEffectSelecter.LoadedMaterials[current].Type == LedMaterialType.Flash)
				{
					this.ListAddFile((System.Drawing.Bitmap)formBackgroundEffectSelecter.LoadedMaterials[current].MaterialPreview, LedCommonConst.MaterialSwfPath + current);
				}
				else if (formBackgroundEffectSelecter.LoadedMaterials[current].Type == LedMaterialType.Gif)
				{
					this.ListAddFile((System.Drawing.Bitmap)formBackgroundEffectSelecter.LoadedMaterials[current].MaterialPreview, LedCommonConst.MaterialGifPath + current);
				}
				else if (formBackgroundEffectSelecter.LoadedMaterials[current].Type == LedMaterialType.image)
				{
					this.ListAddFile((System.Drawing.Bitmap)formBackgroundEffectSelecter.LoadedMaterials[current].MaterialPreview, LedCommonConst.MaterialImagePath + current);
				}
			}
			this.panel_Picture.Focus();
		}

		private void AddFile(string file)
		{
			if (file == "" || file == null)
			{
				return;
			}
			FileInfo fileInfo = new FileInfo(file);
			if (fileInfo != null)
			{
				this.ListAddFile(fileInfo.FullName);
			}
		}

		private void ListAddFile(string f)
		{
			if (!f.EndsWith(".bmp") && !f.EndsWith(".jpg") && !f.EndsWith(".png") && !f.EndsWith(".swf") && !f.EndsWith(".gif"))
			{
				return;
			}
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(LedCommonConst.MaterialPreviewPath + Path.GetFileName(f).Replace(".", "") + ".bmp");
			this.Add_BackGround(bitmap, f);
		}

		private void ListAddFile(System.Drawing.Bitmap bitmap, string f)
		{
			if (!f.EndsWith(".bmp") && !f.EndsWith(".jpg") && !f.EndsWith(".png") && !f.EndsWith(".swf") && !f.EndsWith(".gif"))
			{
				return;
			}
			this.Add_BackGround(bitmap, f);
		}

		private void Add_BackGround(System.Drawing.Bitmap bitmap, string fname)
		{
			PictureBox pictureBox = new PictureBox();
			pictureBox.Image = bitmap;
			pictureBox.Location = new System.Drawing.Point(0, 0);
			pictureBox.Size = new System.Drawing.Size(88, 32);
			pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox.Click += this.events_mouse.Picture_Click;
			pictureBox.MouseEnter += this.events_mouse.MouseEnter;
			pictureBox.MouseLeave += this.events_mouse.MouseLeave;
			RadioButton radioButton = new RadioButton();
			radioButton.Tag = fname;
			radioButton.Location = new System.Drawing.Point(pictureBox.Width + 5, 0);
			radioButton.Size = new System.Drawing.Size(45, 32);
			radioButton.Click += this.events_mouse.RadioButton_Click;
			radioButton.Text = formBackgroundEffectSelecter.materialno.ToString("D3");
			formBackgroundEffectSelecter.materialno++;
			pictureBox.Tag = radioButton;
			this.panel_Picture.Controls.Add(pictureBox);
			this.panel_Picture.Controls.Add(radioButton);
			this.SetCurSelect(radioButton);
		}

		private void SetCurSelect(RadioButton rb)
		{
			bool flag = false;
			string text = rb.Tag.ToString();
			if (!this.background.Enabled)
			{
				if (text == "")
				{
					flag = true;
				}
			}
			else if (this.background.MaterialType == LedMaterialType.Gif && this.background.MaterialName == Path.GetFileName(text))
			{
				flag = true;
			}
			else if (this.background.MaterialType == LedMaterialType.Flash && this.background.MaterialName == Path.GetFileName(text))
			{
				flag = true;
			}
			else if (this.background.MaterialType == LedMaterialType.image && this.background.MaterialName == Path.GetFileName(text))
			{
				flag = true;
			}
			if (flag)
			{
				this.panel_Picture.ScrollControlIntoView(rb);
				rb.Checked = true;
				this.rb_Click(rb, null);
			}
		}

		public void SelectEffect(LedBackground pBackground)
		{
			this.background = pBackground;
			this.backOriginalName = pBackground.MaterialName;
			this.backOriginalType = pBackground.MaterialType;
			this.backOriginalLayout = pBackground.MaterialLayout;
			base.Size = new System.Drawing.Size(648, 349);
			this.Load_formEffectSelecter();
			base.ShowDialog();
		}

		private void pic_MouseLeave(object sender, EventArgs e)
		{
			PictureBox pictureBox = (PictureBox)sender;
			RadioButton arg_12_0 = (RadioButton)pictureBox.Tag;
			this.panel_Picture.Focus();
			if (this.background.MaterialLayout != LedImageFillMode.Stretch)
			{
				pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				return;
			}
			pictureBox.SizeMode = (PictureBoxSizeMode)this.background.MaterialLayout;
		}

		private void pic_MouseEnter(object sender, EventArgs e)
		{
			try
			{
				PictureBox pictureBox = (PictureBox)sender;
				RadioButton radioButton = (RadioButton)pictureBox.Tag;
				this.panel_Picture.Focus();
				this.PlayPreView(radioButton.Tag.ToString());
				if (this.background.MaterialLayout != LedImageFillMode.Stretch)
				{
					pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				}
				else
				{
					pictureBox.SizeMode = (PictureBoxSizeMode)this.background.MaterialLayout;
				}
			}
			catch
			{
			}
		}

		private void pic_Click(object sender, EventArgs e)
		{
			PictureBox pictureBox = (PictureBox)sender;
			RadioButton radioButton = (RadioButton)pictureBox.Tag;
			this.rb_Click(radioButton, null);
			radioButton.Checked = true;
			if (this.background.MaterialLayout != LedImageFillMode.Stretch)
			{
				pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				return;
			}
			pictureBox.SizeMode = (PictureBoxSizeMode)this.background.MaterialLayout;
		}

		private void rb_Click(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			this.panel_Picture.Focus();
			string text = this.PlayPreView(radioButton.Tag.ToString());
			this.Text = Path.GetFileNameWithoutExtension(radioButton.Tag.ToString());
			this.background.Enabled = true;
			if (text == "")
			{
				this.background.Enabled = false;
				this.background.MaterialType = LedMaterialType.Null;
			}
			else
			{
				this.background.Enabled = true;
				string fileName = Path.GetFileName(text);
				if (Path.GetExtension(fileName) == ".gif")
				{
					this.background.MaterialType = LedMaterialType.Gif;
					this.background.MaterialName = fileName;
				}
				else if (Path.GetExtension(fileName) == ".swf")
				{
					this.background.MaterialType = LedMaterialType.Flash;
					this.background.MaterialName = fileName;
				}
				else if (Path.GetExtension(fileName) == ".jpg" || Path.GetExtension(fileName) == ".bmp" || Path.GetExtension(fileName) == ".png")
				{
					this.background.MaterialType = LedMaterialType.image;
					this.background.MaterialName = fileName;
				}
			}
			if (this.background.MaterialLayout == LedImageFillMode.Zoom)
			{
				this.RdoZoom.Checked = true;
				return;
			}
			if (this.background.MaterialLayout == LedImageFillMode.Stretch)
			{
				this.RdoStretch.Checked = true;
				return;
			}
			if (this.background.MaterialLayout == LedImageFillMode.Tile)
			{
				this.RdoTiling.Checked = true;
				return;
			}
			this.RdoOriginal.Checked = true;
		}

		private void Load_formEffectSelecter()
		{
			this.LoadMaterial(this.background.MaterialType);
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void panel_Picture_Click(object sender, EventArgs e)
		{
			this.panel_Picture.Focus();
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			string text = formMaterialEdit.EditMaterial(this.background.MaterialType);
			if (text != "")
			{
				this.background.Changed = true;
				this.AddFile(text);
			}
		}

		private void RdoTiling_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.background.MaterialLayout = LedImageFillMode.Tile;
			}
		}

		private void RdoZoom_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.background.MaterialLayout = LedImageFillMode.Zoom;
			}
		}

		private void RdoStretch_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.background.MaterialLayout = LedImageFillMode.Stretch;
			}
		}

		private void RdoOriginal_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			if (radioButton.Checked)
			{
				this.background.MaterialLayout = LedImageFillMode.Original;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.background.MaterialName = this.backOriginalName;
			this.background.MaterialType = this.backOriginalType;
			this.background.MaterialLayout = this.backOriginalLayout;
			base.Close();
		}

		private void formBackgroundEffectSelecter_Load(object sender, EventArgs e)
		{
			this.RdoTiling.Text = formMain.ML.GetStr("formBackgroundEffectSelecter_Effect_Tiling");
			this.RdoStretch.Text = formMain.ML.GetStr("formBackgroundEffectSelecter_Effect_Stretch");
			this.RdoOriginal.Text = formMain.ML.GetStr("formBackgroundEffectSelecter_Effect_Original");
			this.buttonAdd.Text = formMain.ML.GetStr("formBackgroundEffectSelecter_Btn_AddEffect");
			this.buttonOk.Text = formMain.ML.GetStr("Global_Messagebox_OK");
			this.buttonCancel.Text = formMain.ML.GetStr("Global_Messagebox_Cancel");
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formBackgroundEffectSelecter));
			this.button1 = new Button();
			this.panel1 = new Panel();
			this.panel_Picture = new FlowLayoutPanel();
			this.axShockwaveFlash1 = new AxShockwaveFlash();
			this.buttonOk = new Button();
			this.buttonAdd = new Button();
			this.pictureBox_View = new PictureBox();
			this.panel2 = new Panel();
			this.panel3 = new Panel();
			this.LblBackgroundFillType = new Label();
			this.RdoOriginal = new RadioButton();
			this.RdoStretch = new RadioButton();
			this.RdoTiling = new RadioButton();
			this.RdoZoom = new RadioButton();
			this.buttonCancel = new Button();
			((ISupportInitialize)this.axShockwaveFlash1).BeginInit();
			((ISupportInitialize)this.pictureBox_View).BeginInit();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.button1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.button1.Location = new System.Drawing.Point(87, 543);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 21;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.panel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel1.AutoScroll = true;
			this.panel1.Location = new System.Drawing.Point(2, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(300, 529);
			this.panel1.TabIndex = 0;
			this.panel_Picture.AutoScroll = true;
			this.panel_Picture.BorderStyle = BorderStyle.FixedSingle;
			this.panel_Picture.Location = new System.Drawing.Point(12, 17);
			this.panel_Picture.Name = "panel_Picture";
			this.panel_Picture.Size = new System.Drawing.Size(470, 190);
			this.panel_Picture.TabIndex = 0;
			this.panel_Picture.Click += new EventHandler(this.panel_Picture_Click);
			this.axShockwaveFlash1.Dock = DockStyle.Fill;
			this.axShockwaveFlash1.Enabled = true;
			this.axShockwaveFlash1.Location = new System.Drawing.Point(0, 0);
			this.axShockwaveFlash1.Name = "axShockwaveFlash1";
			this.axShockwaveFlash1.OcxState = (AxHost.State)componentResourceManager.GetObject("axShockwaveFlash1.OcxState");
			this.axShockwaveFlash1.Size = new System.Drawing.Size(116, 66);
			this.axShockwaveFlash1.TabIndex = 25;
			this.axShockwaveFlash1.Visible = false;
			this.buttonOk.Location = new System.Drawing.Point(498, 141);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(120, 30);
			this.buttonOk.TabIndex = 23;
			this.buttonOk.Text = "确认";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new EventHandler(this.buttonOk_Click);
			this.buttonAdd.BackgroundImageLayout = ImageLayout.Zoom;
			this.buttonAdd.Image = Resources.image_add;
			this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAdd.Location = new System.Drawing.Point(12, 268);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(119, 30);
			this.buttonAdd.TabIndex = 22;
			this.buttonAdd.Text = "添加素材";
			this.buttonAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new EventHandler(this.buttonAdd_Click);
			this.pictureBox_View.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.pictureBox_View.Dock = DockStyle.Fill;
			this.pictureBox_View.Location = new System.Drawing.Point(0, 0);
			this.pictureBox_View.Name = "pictureBox_View";
			this.pictureBox_View.Size = new System.Drawing.Size(116, 66);
			this.pictureBox_View.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox_View.TabIndex = 24;
			this.pictureBox_View.TabStop = false;
			this.panel2.BorderStyle = BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.pictureBox_View);
			this.panel2.Controls.Add(this.axShockwaveFlash1);
			this.panel2.Location = new System.Drawing.Point(500, 17);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(120, 70);
			this.panel2.TabIndex = 26;
			this.panel3.BackColor = System.Drawing.SystemColors.HighlightText;
			this.panel3.Controls.Add(this.LblBackgroundFillType);
			this.panel3.Controls.Add(this.RdoOriginal);
			this.panel3.Controls.Add(this.RdoStretch);
			this.panel3.Controls.Add(this.RdoTiling);
			this.panel3.Location = new System.Drawing.Point(12, 224);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(470, 29);
			this.panel3.TabIndex = 27;
			this.LblBackgroundFillType.AutoSize = true;
			this.LblBackgroundFillType.Location = new System.Drawing.Point(3, 9);
			this.LblBackgroundFillType.Name = "LblBackgroundFillType";
			this.LblBackgroundFillType.Size = new System.Drawing.Size(65, 12);
			this.LblBackgroundFillType.TabIndex = 29;
			this.LblBackgroundFillType.Text = "背景填充：";
			this.RdoOriginal.AutoSize = true;
			this.RdoOriginal.Location = new System.Drawing.Point(373, 7);
			this.RdoOriginal.Name = "RdoOriginal";
			this.RdoOriginal.Size = new System.Drawing.Size(47, 16);
			this.RdoOriginal.TabIndex = 2;
			this.RdoOriginal.TabStop = true;
			this.RdoOriginal.Text = "原始";
			this.RdoOriginal.UseVisualStyleBackColor = true;
			this.RdoOriginal.CheckedChanged += new EventHandler(this.RdoOriginal_CheckedChanged);
			this.RdoStretch.AutoSize = true;
			this.RdoStretch.Location = new System.Drawing.Point(247, 7);
			this.RdoStretch.Name = "RdoStretch";
			this.RdoStretch.Size = new System.Drawing.Size(47, 16);
			this.RdoStretch.TabIndex = 1;
			this.RdoStretch.TabStop = true;
			this.RdoStretch.Text = "拉伸";
			this.RdoStretch.UseVisualStyleBackColor = true;
			this.RdoStretch.CheckedChanged += new EventHandler(this.RdoStretch_CheckedChanged);
			this.RdoTiling.AutoSize = true;
			this.RdoTiling.Location = new System.Drawing.Point(124, 7);
			this.RdoTiling.Name = "RdoTiling";
			this.RdoTiling.Size = new System.Drawing.Size(47, 16);
			this.RdoTiling.TabIndex = 0;
			this.RdoTiling.TabStop = true;
			this.RdoTiling.Text = "平铺";
			this.RdoTiling.UseVisualStyleBackColor = true;
			this.RdoTiling.CheckedChanged += new EventHandler(this.RdoTiling_CheckedChanged);
			this.RdoZoom.AutoSize = true;
			this.RdoZoom.Location = new System.Drawing.Point(502, 231);
			this.RdoZoom.Name = "RdoZoom";
			this.RdoZoom.Size = new System.Drawing.Size(47, 16);
			this.RdoZoom.TabIndex = 3;
			this.RdoZoom.TabStop = true;
			this.RdoZoom.Text = "缩放";
			this.RdoZoom.UseVisualStyleBackColor = true;
			this.RdoZoom.Visible = false;
			this.RdoZoom.CheckedChanged += new EventHandler(this.RdoZoom_CheckedChanged);
			this.buttonCancel.Location = new System.Drawing.Point(498, 177);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(120, 30);
			this.buttonCancel.TabIndex = 28;
			this.buttonCancel.Text = "取消";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			base.ClientSize = new System.Drawing.Size(632, 310);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.RdoZoom);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.buttonOk);
			base.Controls.Add(this.buttonAdd);
			base.Controls.Add(this.panel_Picture);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formBackgroundEffectSelecter";
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += new EventHandler(this.formBackgroundEffectSelecter_Load);
			((ISupportInitialize)this.axShockwaveFlash1).EndInit();
			((ISupportInitialize)this.pictureBox_View).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
