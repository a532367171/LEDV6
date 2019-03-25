using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class ColorfulBackground : UserControl
	{
		public delegate void BackgroundEventHandler(object sender, EventArgs e);

		private MyAxShockwaveFlash axShockwaveFlashBack1;

		private MyAxShockwaveFlash axShockwaveFlashBack2;

		private MyAxShockwaveFlash axShockwaveFlashBack3;

		private LedContent Content;

		private LedItem Item;

		private bool IsContent = true;

		private LedBackground backgroundNo1;

		private LedBackground backgroundNo2;

		private LedBackground backgroundNo3;

		public static int material_no;

		private IContainer components;

		private Panel panelBack2;

		private PictureBox pictureBoxBack2;

		private Panel panelBack1;

		private PictureBox pictureBoxBack1;

		private PictureBox pictureBoxBack3;

		private Button Back3;

		private Button Back2;

		private Button Back1;

		private Panel panelBack3;

		private CheckBox ChkBack1;

		private CheckBox ChkBack2;

		private CheckBox ChkBack3;

		public ColorfulBackground()
		{
			this.InitializeComponent();
			this.axShockwaveFlashBack1 = new MyAxShockwaveFlash();
			this.axShockwaveFlashBack2 = new MyAxShockwaveFlash();
			this.axShockwaveFlashBack3 = new MyAxShockwaveFlash();
			this.panelBack1.Controls.Add(this.axShockwaveFlashBack1);
			this.panelBack2.Controls.Add(this.axShockwaveFlashBack2);
			this.panelBack3.Controls.Add(this.axShockwaveFlashBack3);
			this.axShockwaveFlashBack1.Location = new System.Drawing.Point(0, 0);
			this.axShockwaveFlashBack1.Size = this.panelBack1.Size;
			MyAxShockwaveFlash expr_A4 = this.axShockwaveFlashBack1;
			expr_A4.MyMouseClick = (EventHandler)Delegate.Combine(expr_A4.MyMouseClick, new EventHandler(this.Back1_Click));
			this.axShockwaveFlashBack2.Location = new System.Drawing.Point(0, 0);
			this.axShockwaveFlashBack2.Size = this.panelBack2.Size;
			MyAxShockwaveFlash expr_F3 = this.axShockwaveFlashBack2;
			expr_F3.MyMouseClick = (EventHandler)Delegate.Combine(expr_F3.MyMouseClick, new EventHandler(this.Back2_Click));
			this.axShockwaveFlashBack3.Location = new System.Drawing.Point(0, 0);
			this.axShockwaveFlashBack3.Size = this.panelBack3.Size;
			MyAxShockwaveFlash expr_142 = this.axShockwaveFlashBack3;
			expr_142.MyMouseClick = (EventHandler)Delegate.Combine(expr_142.MyMouseClick, new EventHandler(this.Back3_Click));
		}

		public void LoadBackground(LedContent pContent)
		{
			this.Content = pContent;
			this.IsContent = true;
			this.backgroundNo1 = pContent.GetBackgroud(1);
			this.backgroundNo2 = pContent.GetBackgroud(2);
			this.backgroundNo3 = pContent.GetBackgroud(3);
			if (this.backgroundNo2 == null)
			{
				this.backgroundNo2 = new LedBackground();
				this.backgroundNo2.No = 2;
				LedBackground expr_5A = this.backgroundNo2;
				expr_5A.ID += ColorfulBackground.material_no.ToString();
				ColorfulBackground.material_no++;
				pContent.AddBackground(this.backgroundNo2);
			}
			if (this.backgroundNo3 == null)
			{
				this.backgroundNo3 = new LedBackground();
				this.backgroundNo3.No = 3;
				LedBackground expr_B1 = this.backgroundNo3;
				expr_B1.ID += ColorfulBackground.material_no.ToString();
				ColorfulBackground.material_no++;
				pContent.AddBackground(this.backgroundNo3);
			}
			if (this.backgroundNo1.MaterialName == string.Empty)
			{
				this.backgroundNo1.MaterialName = "BM0000.swf";
				this.backgroundNo1.MaterialType = LedMaterialType.Flash;
			}
			else if (!formBackgroundEffectSelecter.LoadedMaterials.ContainsKey(this.backgroundNo1.MaterialName))
			{
				this.backgroundNo1.MaterialName = "BM0000.swf";
				this.backgroundNo1.MaterialType = LedMaterialType.Flash;
			}
			if (this.backgroundNo2.MaterialName == string.Empty)
			{
				this.backgroundNo2.MaterialName = "BM0000.swf";
				this.backgroundNo2.MaterialType = LedMaterialType.Flash;
			}
			else if (!formBackgroundEffectSelecter.LoadedMaterials.ContainsKey(this.backgroundNo2.MaterialName))
			{
				this.backgroundNo2.MaterialName = "BM0000.swf";
				this.backgroundNo2.MaterialType = LedMaterialType.Flash;
			}
			if (this.backgroundNo3.MaterialName == string.Empty)
			{
				this.backgroundNo3.MaterialName = "BM0000.swf";
				this.backgroundNo3.MaterialType = LedMaterialType.Flash;
			}
			else if (!formBackgroundEffectSelecter.LoadedMaterials.ContainsKey(this.backgroundNo3.MaterialName))
			{
				this.backgroundNo3.MaterialName = "BM0000.swf";
				this.backgroundNo3.MaterialType = LedMaterialType.Flash;
			}
			string saveDirectory = pContent.ParentSubarea.ParentItem.ParentPanel.GetSaveDirectory();
			this.backgroundNo1.SaveDirectory = (this.backgroundNo2.SaveDirectory = (this.backgroundNo3.SaveDirectory = saveDirectory));
			this.ChkBackState();
			this.DisplayBackground(3);
		}

		public void LoadBackground(LedItem pItem)
		{
			this.Item = pItem;
			this.IsContent = false;
			this.backgroundNo1 = pItem.GetBackgroud(1);
			this.backgroundNo2 = pItem.GetBackgroud(2);
			this.backgroundNo3 = pItem.GetBackgroud(3);
			if (this.backgroundNo2 == null)
			{
				this.backgroundNo2 = new LedBackground();
				this.backgroundNo2.No = 2;
				LedBackground expr_5A = this.backgroundNo2;
				expr_5A.ID += ColorfulBackground.material_no.ToString();
				ColorfulBackground.material_no++;
				pItem.AddBackground(this.backgroundNo2);
				Thread.Sleep(100);
			}
			if (this.backgroundNo3 == null)
			{
				this.backgroundNo3 = new LedBackground();
				this.backgroundNo3.No = 3;
				LedBackground expr_B8 = this.backgroundNo3;
				expr_B8.ID += ColorfulBackground.material_no.ToString();
				ColorfulBackground.material_no++;
				pItem.AddBackground(this.backgroundNo3);
			}
			if (this.backgroundNo1.MaterialName == string.Empty)
			{
				this.backgroundNo1.MaterialName = "BM0000.swf";
				this.backgroundNo1.MaterialType = LedMaterialType.Flash;
			}
			else if (!formBackgroundEffectSelecter.LoadedMaterials.ContainsKey(this.backgroundNo1.MaterialName))
			{
				this.backgroundNo1.MaterialName = "BM0000.swf";
				this.backgroundNo1.MaterialType = LedMaterialType.Flash;
			}
			if (this.backgroundNo2.MaterialName == string.Empty)
			{
				this.backgroundNo2.MaterialName = "BM0000.swf";
				this.backgroundNo2.MaterialType = LedMaterialType.Flash;
			}
			else if (!formBackgroundEffectSelecter.LoadedMaterials.ContainsKey(this.backgroundNo2.MaterialName))
			{
				this.backgroundNo2.MaterialName = "BM0000.swf";
				this.backgroundNo2.MaterialType = LedMaterialType.Flash;
			}
			if (this.backgroundNo3.MaterialName == string.Empty)
			{
				this.backgroundNo3.MaterialName = "BM0000.swf";
				this.backgroundNo3.MaterialType = LedMaterialType.Flash;
			}
			else if (!formBackgroundEffectSelecter.LoadedMaterials.ContainsKey(this.backgroundNo3.MaterialName))
			{
				this.backgroundNo3.MaterialName = "BM0000.swf";
				this.backgroundNo3.MaterialType = LedMaterialType.Flash;
			}
			string saveDirectory = pItem.ParentPanel.GetSaveDirectory();
			this.backgroundNo1.SaveDirectory = (this.backgroundNo2.SaveDirectory = (this.backgroundNo3.SaveDirectory = saveDirectory));
			this.ChkBackState();
			this.DisplayBackground(3);
		}

		private void ChkBackState()
		{
			if (this.backgroundNo1.Enabled)
			{
				this.ChkBack1.Checked = true;
				this.Back1.Enabled = true;
			}
			else
			{
				this.ChkBack1.Checked = false;
				this.Back1.Enabled = false;
			}
			if (this.backgroundNo2.Enabled)
			{
				this.ChkBack2.Checked = true;
				this.Back2.Enabled = true;
			}
			else
			{
				this.ChkBack2.Checked = false;
				this.Back2.Enabled = false;
			}
			if (this.backgroundNo3.Enabled)
			{
				this.ChkBack3.Checked = true;
				this.Back3.Enabled = true;
				return;
			}
			this.ChkBack3.Checked = false;
			this.Back3.Enabled = false;
		}

		public void SetBackNotShown()
		{
			this.ChkBack1.Checked = false;
			this.ChkBack2.Checked = false;
			this.ChkBack3.Checked = false;
			this.axShockwaveFlashBack1.SendToBack();
			this.axShockwaveFlashBack1.Visible = false;
			this.axShockwaveFlashBack1.Stop();
			this.axShockwaveFlashBack1.StopPlay();
			this.axShockwaveFlashBack1.Hide();
			this.pictureBoxBack1.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxBack1.Size = this.panelBack1.Size;
			this.pictureBoxBack1.Image = null;
			this.pictureBoxBack1.Show();
			this.pictureBoxBack1.BringToFront();
			this.axShockwaveFlashBack2.SendToBack();
			this.axShockwaveFlashBack2.Visible = false;
			this.axShockwaveFlashBack2.Stop();
			this.axShockwaveFlashBack2.StopPlay();
			this.axShockwaveFlashBack2.Hide();
			this.pictureBoxBack2.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxBack2.Size = this.panelBack2.Size;
			this.pictureBoxBack2.Image = null;
			this.pictureBoxBack2.Show();
			this.pictureBoxBack2.BringToFront();
			this.axShockwaveFlashBack3.SendToBack();
			this.axShockwaveFlashBack3.Visible = false;
			this.axShockwaveFlashBack3.Stop();
			this.axShockwaveFlashBack3.StopPlay();
			this.axShockwaveFlashBack3.Hide();
			this.pictureBoxBack3.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxBack3.Size = this.panelBack3.Size;
			this.pictureBoxBack3.Image = null;
			this.pictureBoxBack3.Show();
			this.pictureBoxBack3.BringToFront();
		}

		private void DisplayBackground(int RefreshRule)
		{
			if ((RefreshRule == 3 || RefreshRule == 0) && this.backgroundNo1 != null)
			{
				if (!this.backgroundNo1.Enabled)
				{
					this.axShockwaveFlashBack1.SendToBack();
					this.axShockwaveFlashBack1.Visible = false;
					this.axShockwaveFlashBack1.Stop();
					this.axShockwaveFlashBack1.StopPlay();
					this.axShockwaveFlashBack1.Hide();
					this.pictureBoxBack1.Location = new System.Drawing.Point(0, 0);
					this.pictureBoxBack1.Size = this.panelBack1.Size;
					this.pictureBoxBack1.Image = null;
					this.pictureBoxBack1.Show();
					this.pictureBoxBack1.BringToFront();
				}
				else if (this.backgroundNo1.MaterialType == LedMaterialType.Flash)
				{
					this.axShockwaveFlashBack1.Show();
					this.axShockwaveFlashBack1.Movie = LedCommonConst.MaterialSwfPath + this.backgroundNo1.MaterialName;
					this.axShockwaveFlashBack1.Play();
					this.axShockwaveFlashBack1.Visible = true;
					this.axShockwaveFlashBack1.BringToFront();
					this.axShockwaveFlashBack1.Size = this.panelBack1.Size;
				}
				else if (this.backgroundNo1.MaterialType == LedMaterialType.Gif)
				{
					this.pictureBoxBack1.Show();
					this.pictureBoxBack1.Location = new System.Drawing.Point(0, 0);
					this.pictureBoxBack1.Size = this.panelBack1.Size;
					this.pictureBoxBack1.ImageLocation = LedCommonConst.MaterialGifPath + this.backgroundNo1.MaterialName;
					this.pictureBoxBack1.Visible = true;
					this.pictureBoxBack1.BringToFront();
					this.pictureBoxBack1.SizeMode = PictureBoxSizeMode.StretchImage;
				}
				else if (this.backgroundNo1.MaterialType == LedMaterialType.image)
				{
					this.pictureBoxBack1.Show();
					this.pictureBoxBack1.Location = new System.Drawing.Point(0, 0);
					this.pictureBoxBack1.Size = this.panelBack1.Size;
					this.pictureBoxBack1.ImageLocation = LedCommonConst.MaterialImagePath + this.backgroundNo1.MaterialName;
					this.pictureBoxBack1.Visible = true;
					this.pictureBoxBack1.BringToFront();
					this.pictureBoxBack1.SizeMode = PictureBoxSizeMode.StretchImage;
				}
				else
				{
					this.axShockwaveFlashBack1.Stop();
					this.axShockwaveFlashBack1.Hide();
					this.pictureBoxBack1.Hide();
				}
			}
			if ((RefreshRule == 3 || RefreshRule == 1) && this.backgroundNo2 != null)
			{
				if (!this.backgroundNo2.Enabled)
				{
					this.axShockwaveFlashBack2.SendToBack();
					this.axShockwaveFlashBack2.Visible = false;
					this.axShockwaveFlashBack2.Stop();
					this.axShockwaveFlashBack2.StopPlay();
					this.axShockwaveFlashBack2.Hide();
					this.pictureBoxBack2.Location = new System.Drawing.Point(0, 0);
					this.pictureBoxBack2.Size = this.panelBack2.Size;
					this.pictureBoxBack2.Image = null;
					this.pictureBoxBack2.Show();
					this.pictureBoxBack2.BringToFront();
				}
				else if (this.backgroundNo2.MaterialType == LedMaterialType.Flash)
				{
					this.axShockwaveFlashBack2.Show();
					this.axShockwaveFlashBack2.Movie = LedCommonConst.MaterialSwfPath + this.backgroundNo2.MaterialName;
					this.axShockwaveFlashBack2.Play();
					this.axShockwaveFlashBack2.Visible = true;
					this.axShockwaveFlashBack2.BringToFront();
					this.axShockwaveFlashBack2.Size = this.panelBack2.Size;
				}
				else if (this.backgroundNo2.MaterialType == LedMaterialType.Gif)
				{
					this.pictureBoxBack2.Show();
					this.pictureBoxBack2.Location = new System.Drawing.Point(0, 0);
					this.pictureBoxBack2.Size = this.panelBack2.Size;
					this.pictureBoxBack2.ImageLocation = LedCommonConst.MaterialGifPath + this.backgroundNo2.MaterialName;
					this.pictureBoxBack2.Visible = true;
					this.pictureBoxBack2.BringToFront();
					this.pictureBoxBack2.SizeMode = PictureBoxSizeMode.StretchImage;
				}
				else if (this.backgroundNo2.MaterialType == LedMaterialType.image)
				{
					this.pictureBoxBack2.Show();
					this.pictureBoxBack2.Location = new System.Drawing.Point(0, 0);
					this.pictureBoxBack2.Size = this.panelBack2.Size;
					this.pictureBoxBack2.ImageLocation = LedCommonConst.MaterialImagePath + this.backgroundNo2.MaterialName;
					this.pictureBoxBack2.Visible = true;
					this.pictureBoxBack2.BringToFront();
					this.pictureBoxBack2.SizeMode = PictureBoxSizeMode.StretchImage;
				}
				else
				{
					this.axShockwaveFlashBack2.Stop();
					this.axShockwaveFlashBack2.Hide();
					this.pictureBoxBack2.Hide();
				}
			}
			if ((RefreshRule == 3 || RefreshRule == 2) && this.backgroundNo3 != null)
			{
				if (!this.backgroundNo3.Enabled)
				{
					this.axShockwaveFlashBack3.SendToBack();
					this.axShockwaveFlashBack3.Visible = false;
					this.axShockwaveFlashBack3.Stop();
					this.axShockwaveFlashBack3.StopPlay();
					this.axShockwaveFlashBack3.Hide();
					this.pictureBoxBack3.Location = new System.Drawing.Point(0, 0);
					this.pictureBoxBack3.Size = this.panelBack3.Size;
					this.pictureBoxBack3.Image = null;
					this.pictureBoxBack3.Show();
					this.pictureBoxBack3.BringToFront();
					return;
				}
				if (this.backgroundNo3.MaterialType == LedMaterialType.Flash)
				{
					this.axShockwaveFlashBack3.Show();
					this.axShockwaveFlashBack3.Movie = LedCommonConst.MaterialSwfPath + this.backgroundNo3.MaterialName;
					this.axShockwaveFlashBack3.Play();
					this.axShockwaveFlashBack3.Visible = true;
					this.axShockwaveFlashBack3.BringToFront();
					this.axShockwaveFlashBack3.Size = this.panelBack3.Size;
					return;
				}
				if (this.backgroundNo3.MaterialType == LedMaterialType.Gif)
				{
					this.pictureBoxBack3.Show();
					this.pictureBoxBack3.Location = new System.Drawing.Point(0, 0);
					this.pictureBoxBack3.Size = this.panelBack3.Size;
					this.pictureBoxBack3.ImageLocation = LedCommonConst.MaterialGifPath + this.backgroundNo3.MaterialName;
					this.pictureBoxBack3.Visible = true;
					this.pictureBoxBack3.BringToFront();
					this.pictureBoxBack3.SizeMode = PictureBoxSizeMode.StretchImage;
					return;
				}
				if (this.backgroundNo3.MaterialType == LedMaterialType.image)
				{
					this.pictureBoxBack3.Show();
					this.pictureBoxBack3.Location = new System.Drawing.Point(0, 0);
					this.pictureBoxBack3.Size = this.panelBack3.Size;
					this.pictureBoxBack3.ImageLocation = LedCommonConst.MaterialImagePath + this.backgroundNo3.MaterialName;
					this.pictureBoxBack3.Visible = true;
					this.pictureBoxBack3.BringToFront();
					this.pictureBoxBack3.SizeMode = PictureBoxSizeMode.StretchImage;
					return;
				}
				this.axShockwaveFlashBack3.Stop();
				this.axShockwaveFlashBack3.Hide();
				this.pictureBoxBack3.Hide();
			}
		}

		private void changeBackgroundMaterial(LedBackground BackgroundMaterial, int Material)
		{
			formBackgroundEffectSelecter formBackgroundEffectSelecter = new formBackgroundEffectSelecter();
			formBackgroundEffectSelecter.Size = new System.Drawing.Size(276, 658);
			formBackgroundEffectSelecter.SelectEffect(BackgroundMaterial);
			formBackgroundEffectSelecter.Dispose();
			BackgroundMaterial.Changed = true;
			this.DisplayBackground(Material);
		}

		private void Back1_Click(object sender, EventArgs e)
		{
			if (this.IsContent)
			{
				this.changeBackgroundMaterial(this.Content.GetBackgroud(1), 0);
				return;
			}
			this.changeBackgroundMaterial(this.Item.GetBackgroud(1), 0);
		}

		private void Back2_Click(object sender, EventArgs e)
		{
			if (this.IsContent)
			{
				this.changeBackgroundMaterial(this.Content.GetBackgroud(2), 1);
				return;
			}
			this.changeBackgroundMaterial(this.Item.GetBackgroud(2), 1);
		}

		private void Back3_Click(object sender, EventArgs e)
		{
			if (this.IsContent)
			{
				this.changeBackgroundMaterial(this.Content.GetBackgroud(3), 2);
				return;
			}
			this.changeBackgroundMaterial(this.Item.GetBackgroud(3), 2);
		}

		private void ChkBack1_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				if (this.IsContent)
				{
					using (IEnumerator<LedBackground> enumerator = this.Content.ParentSubarea.ParentItem.Backgrounds.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							LedBackground current = enumerator.Current;
							if (current.Enabled)
							{
								MessageBox.Show(formMain.ML.GetStr("formMain_message_Reuse_Background"));
								this.ChkBack1.Checked = false;
								return;
							}
						}
						goto IL_156;
					}
				}
				foreach (LedSubarea current2 in this.Item.Subareas)
				{
					foreach (LedContent current3 in current2.Contents)
					{
						foreach (LedBackground current4 in current3.Backgrounds)
						{
							if (current4.Enabled)
							{
								MessageBox.Show(formMain.ML.GetStr("formMain_message_Reuse_Background"));
								this.ChkBack1.Checked = false;
								return;
							}
						}
					}
				}
				IL_156:
				this.ChkBack1.Checked = true;
				if (this.backgroundNo1 == null)
				{
					this.backgroundNo1 = new LedBackground();
				}
				this.backgroundNo1.Enabled = true;
				this.Back1.Enabled = true;
			}
			else
			{
				this.backgroundNo1.Enabled = false;
				this.Back1.Enabled = false;
			}
			this.DisplayBackground(0);
		}

		private void ChkBack2_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				if (this.IsContent)
				{
					using (IEnumerator<LedBackground> enumerator = this.Content.ParentSubarea.ParentItem.Backgrounds.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							LedBackground current = enumerator.Current;
							if (current.Enabled)
							{
								MessageBox.Show(formMain.ML.GetStr("formMain_message_Reuse_Background"));
								this.ChkBack2.Checked = false;
								return;
							}
						}
						goto IL_156;
					}
				}
				foreach (LedSubarea current2 in this.Item.Subareas)
				{
					foreach (LedContent current3 in current2.Contents)
					{
						foreach (LedBackground current4 in current3.Backgrounds)
						{
							if (current4.Enabled)
							{
								MessageBox.Show(formMain.ML.GetStr("formMain_message_Reuse_Background"));
								this.ChkBack2.Checked = false;
								return;
							}
						}
					}
				}
				IL_156:
				this.ChkBack2.Checked = true;
				if (this.backgroundNo2 == null)
				{
					this.backgroundNo2 = new LedBackground();
				}
				this.backgroundNo2.Enabled = true;
				this.Back2.Enabled = true;
			}
			else
			{
				this.backgroundNo2.Enabled = false;
				this.Back2.Enabled = false;
			}
			this.DisplayBackground(1);
		}

		private void ChkBack3_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (checkBox.Checked)
			{
				if (this.IsContent)
				{
					using (IEnumerator<LedBackground> enumerator = this.Content.ParentSubarea.ParentItem.Backgrounds.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							LedBackground current = enumerator.Current;
							if (current.Enabled)
							{
								MessageBox.Show(formMain.ML.GetStr("formMain_message_Reuse_Background"));
								this.ChkBack3.Checked = false;
								return;
							}
						}
						goto IL_156;
					}
				}
				foreach (LedSubarea current2 in this.Item.Subareas)
				{
					foreach (LedContent current3 in current2.Contents)
					{
						foreach (LedBackground current4 in current3.Backgrounds)
						{
							if (current4.Enabled)
							{
								MessageBox.Show(formMain.ML.GetStr("formMain_message_Reuse_Background"));
								this.ChkBack3.Checked = false;
								return;
							}
						}
					}
				}
				IL_156:
				this.ChkBack3.Checked = true;
				if (this.backgroundNo3 == null)
				{
					this.backgroundNo3 = new LedBackground();
				}
				this.backgroundNo3.Enabled = true;
				this.Back3.Enabled = true;
			}
			else
			{
				this.backgroundNo3.Enabled = false;
				this.Back3.Enabled = false;
			}
			this.DisplayBackground(2);
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
			this.panelBack2 = new Panel();
			this.pictureBoxBack2 = new PictureBox();
			this.panelBack1 = new Panel();
			this.pictureBoxBack1 = new PictureBox();
			this.pictureBoxBack3 = new PictureBox();
			this.Back3 = new Button();
			this.Back2 = new Button();
			this.Back1 = new Button();
			this.panelBack3 = new Panel();
			this.ChkBack1 = new CheckBox();
			this.ChkBack2 = new CheckBox();
			this.ChkBack3 = new CheckBox();
			this.panelBack2.SuspendLayout();
			((ISupportInitialize)this.pictureBoxBack2).BeginInit();
			this.panelBack1.SuspendLayout();
			((ISupportInitialize)this.pictureBoxBack1).BeginInit();
			((ISupportInitialize)this.pictureBoxBack3).BeginInit();
			this.panelBack3.SuspendLayout();
			base.SuspendLayout();
			this.panelBack2.BorderStyle = BorderStyle.FixedSingle;
			this.panelBack2.Controls.Add(this.pictureBoxBack2);
			this.panelBack2.Location = new System.Drawing.Point(17, 60);
			this.panelBack2.Name = "panelBack2";
			this.panelBack2.Size = new System.Drawing.Size(75, 30);
			this.panelBack2.TabIndex = 23;
			this.pictureBoxBack2.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxBack2.Name = "pictureBoxBack2";
			this.pictureBoxBack2.Size = new System.Drawing.Size(75, 30);
			this.pictureBoxBack2.TabIndex = 0;
			this.pictureBoxBack2.TabStop = false;
			this.panelBack1.BackgroundImageLayout = ImageLayout.Zoom;
			this.panelBack1.BorderStyle = BorderStyle.FixedSingle;
			this.panelBack1.Controls.Add(this.pictureBoxBack1);
			this.panelBack1.Location = new System.Drawing.Point(17, 7);
			this.panelBack1.Name = "panelBack1";
			this.panelBack1.Size = new System.Drawing.Size(75, 30);
			this.panelBack1.TabIndex = 24;
			this.pictureBoxBack1.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxBack1.Name = "pictureBoxBack1";
			this.pictureBoxBack1.Size = new System.Drawing.Size(75, 30);
			this.pictureBoxBack1.TabIndex = 0;
			this.pictureBoxBack1.TabStop = false;
			this.pictureBoxBack3.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxBack3.Name = "pictureBoxBack3";
			this.pictureBoxBack3.Size = new System.Drawing.Size(75, 30);
			this.pictureBoxBack3.TabIndex = 0;
			this.pictureBoxBack3.TabStop = false;
			this.Back3.BackColor = System.Drawing.Color.Transparent;
			this.Back3.BackgroundImage = Resources.BackSelect;
			this.Back3.BackgroundImageLayout = ImageLayout.Stretch;
			this.Back3.FlatAppearance.BorderSize = 0;
			this.Back3.FlatStyle = FlatStyle.Flat;
			this.Back3.ForeColor = System.Drawing.Color.Transparent;
			this.Back3.Location = new System.Drawing.Point(94, 114);
			this.Back3.Name = "Back3";
			this.Back3.Size = new System.Drawing.Size(27, 27);
			this.Back3.TabIndex = 28;
			this.Back3.UseVisualStyleBackColor = false;
			this.Back3.Click += new EventHandler(this.Back3_Click);
			this.Back2.BackColor = System.Drawing.Color.Transparent;
			this.Back2.BackgroundImage = Resources.BackSelect;
			this.Back2.BackgroundImageLayout = ImageLayout.Stretch;
			this.Back2.FlatAppearance.BorderSize = 0;
			this.Back2.FlatStyle = FlatStyle.Flat;
			this.Back2.ForeColor = System.Drawing.Color.Transparent;
			this.Back2.Location = new System.Drawing.Point(94, 62);
			this.Back2.Name = "Back2";
			this.Back2.Size = new System.Drawing.Size(27, 27);
			this.Back2.TabIndex = 27;
			this.Back2.UseVisualStyleBackColor = false;
			this.Back2.Click += new EventHandler(this.Back2_Click);
			this.Back1.BackColor = System.Drawing.Color.Transparent;
			this.Back1.BackgroundImage = Resources.BackSelect;
			this.Back1.BackgroundImageLayout = ImageLayout.Stretch;
			this.Back1.FlatAppearance.BorderSize = 0;
			this.Back1.FlatStyle = FlatStyle.Flat;
			this.Back1.ForeColor = System.Drawing.Color.Transparent;
			this.Back1.Location = new System.Drawing.Point(94, 9);
			this.Back1.Name = "Back1";
			this.Back1.Size = new System.Drawing.Size(27, 28);
			this.Back1.TabIndex = 26;
			this.Back1.UseVisualStyleBackColor = false;
			this.Back1.Click += new EventHandler(this.Back1_Click);
			this.panelBack3.BorderStyle = BorderStyle.FixedSingle;
			this.panelBack3.Controls.Add(this.pictureBoxBack3);
			this.panelBack3.Location = new System.Drawing.Point(17, 112);
			this.panelBack3.Name = "panelBack3";
			this.panelBack3.Size = new System.Drawing.Size(75, 30);
			this.panelBack3.TabIndex = 25;
			this.ChkBack1.AutoSize = true;
			this.ChkBack1.Location = new System.Drawing.Point(1, 14);
			this.ChkBack1.Name = "ChkBack1";
			this.ChkBack1.Size = new System.Drawing.Size(15, 14);
			this.ChkBack1.TabIndex = 33;
			this.ChkBack1.UseVisualStyleBackColor = true;
			this.ChkBack1.CheckedChanged += new EventHandler(this.ChkBack1_CheckedChanged);
			this.ChkBack2.AutoSize = true;
			this.ChkBack2.Location = new System.Drawing.Point(1, 67);
			this.ChkBack2.Name = "ChkBack2";
			this.ChkBack2.Size = new System.Drawing.Size(15, 14);
			this.ChkBack2.TabIndex = 34;
			this.ChkBack2.UseVisualStyleBackColor = true;
			this.ChkBack2.CheckedChanged += new EventHandler(this.ChkBack2_CheckedChanged);
			this.ChkBack3.AutoSize = true;
			this.ChkBack3.Location = new System.Drawing.Point(1, 119);
			this.ChkBack3.Name = "ChkBack3";
			this.ChkBack3.Size = new System.Drawing.Size(15, 14);
			this.ChkBack3.TabIndex = 35;
			this.ChkBack3.UseVisualStyleBackColor = true;
			this.ChkBack3.CheckedChanged += new EventHandler(this.ChkBack3_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			base.Controls.Add(this.ChkBack3);
			base.Controls.Add(this.ChkBack2);
			base.Controls.Add(this.ChkBack1);
			base.Controls.Add(this.Back1);
			base.Controls.Add(this.Back2);
			base.Controls.Add(this.panelBack1);
			base.Controls.Add(this.Back3);
			base.Controls.Add(this.panelBack2);
			base.Controls.Add(this.panelBack3);
			base.Name = "ColorfulBackground";
			base.Size = new System.Drawing.Size(123, 155);
			this.panelBack2.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxBack2).EndInit();
			this.panelBack1.ResumeLayout(false);
			((ISupportInitialize)this.pictureBoxBack1).EndInit();
			((ISupportInitialize)this.pictureBoxBack3).EndInit();
			this.panelBack3.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
