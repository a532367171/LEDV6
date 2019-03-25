using LedControlSystem.Properties;
using LedModel;
using LedModel.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formEffectSelecter : Form
	{
		private string SelectedEffect;

		private static string formID = "formEffectSelecter";

		private string[] cancelEffect = new string[]
		{
			"DownEgg",
			"DownGongxi"
		};

		private IContainer components;

		private Button button1;

		private Panel panel1;

		public static string FormID
		{
			get
			{
				return formEffectSelecter.formID;
			}
			set
			{
				formEffectSelecter.formID = value;
			}
		}

		public formEffectSelecter()
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formEffectSelecter.formID;
		}

		public string SelectEffect(LedEffectsType pType, string pNowEffect)
		{
			string str = "";
			if (pType == LedEffectsType.Text)
			{
				str = "TextEffect_";
			}
			else if (pType == LedEffectsType.Background)
			{
				str = "BackEffect_";
			}
			bool flag = true;
			int num = 0;
			int num2 = 0;
			int num3 = 110;
			for (int i = 0; i < LedGlobal.LedEffectsList.Count; i++)
			{
				bool flag2 = false;
				for (int j = 0; j < this.cancelEffect.Length; j++)
				{
					if (this.cancelEffect[j] == LedGlobal.LedEffectsList[i].Name)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2 && LedGlobal.LedEffectsList[i].Type == pType)
				{
					PictureBox pictureBox = new PictureBox();
					pictureBox.Size = new System.Drawing.Size(108, 32);
					pictureBox.Image = LedGlobal.LedEffectsList[i].Preview;
					RadioButton radioButton = new RadioButton();
					radioButton.Text = formMain.ML.GetStr(str + LedGlobal.LedEffectsList[i].Name);
					if (radioButton.Text == "")
					{
						radioButton.Text = LedGlobal.LedEffectsList[i].Name;
					}
					if (LedGlobal.LedEffectsList[i].Name == pNowEffect)
					{
						radioButton.Checked = true;
						this.SelectedEffect = LedGlobal.LedEffectsList[i].Name;
					}
					radioButton.Tag = LedGlobal.LedEffectsList[i].Name;
					pictureBox.Tag = radioButton;
					radioButton.Click += new EventHandler(this.rb_Click);
					pictureBox.Click += new EventHandler(this.pic_Click);
					pictureBox.DoubleClick += new EventHandler(this.pic_DoubleClick);
					pictureBox.MouseEnter += new EventHandler(this.pic_MouseEnter);
					pictureBox.MouseLeave += new EventHandler(this.pic_MouseLeave);
					base.Controls.Add(pictureBox);
					base.Controls.Add(radioButton);
					if (flag)
					{
						pictureBox.Location = new System.Drawing.Point(num2, num);
						radioButton.Location = new System.Drawing.Point(num2 + 20, num + 32);
						flag = false;
					}
					else
					{
						pictureBox.Location = new System.Drawing.Point(num3, num);
						radioButton.Location = new System.Drawing.Point(num3 + 20, num + 32);
						num += 58;
						flag = true;
					}
				}
			}
			base.ShowDialog();
			return this.SelectedEffect;
		}

		private void pic_MouseLeave(object sender, EventArgs e)
		{
			PictureBox pictureBox = (PictureBox)sender;
			RadioButton radioButton = (RadioButton)pictureBox.Tag;
			for (int i = 0; i < LedGlobal.LedEffectsList.Count; i++)
			{
				if (LedGlobal.LedEffectsList[i].Name == radioButton.Tag.ToString())
				{
					pictureBox.Image = LedGlobal.LedEffectsList[i].Preview;
				}
			}
		}

		private void pic_MouseEnter(object sender, EventArgs e)
		{
			PictureBox pictureBox = (PictureBox)sender;
			RadioButton radioButton = (RadioButton)pictureBox.Tag;
			for (int i = 0; i < LedGlobal.LedEffectsList.Count; i++)
			{
				if (LedGlobal.LedEffectsList[i].Name == radioButton.Tag.ToString())
				{
					pictureBox.Image = LedGlobal.LedEffectsList[i].GIF;
				}
			}
		}

		private void pic_DoubleClick(object sender, EventArgs e)
		{
			base.Close();
		}

		private void pic_Click(object sender, EventArgs e)
		{
			PictureBox pictureBox = (PictureBox)sender;
			RadioButton radioButton = (RadioButton)pictureBox.Tag;
			radioButton.Checked = true;
			this.SelectedEffect = (string)radioButton.Tag;
		}

		private void rb_Click(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			this.SelectedEffect = (string)radioButton.Tag;
		}

		public void AddEffect(string pEffectName)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void formEffectSelecter_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
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
			this.button1 = new Button();
			this.panel1 = new Panel();
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
			this.AutoScroll = true;
			base.ClientSize = new System.Drawing.Size(284, 262);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "formEffectSelecter";
			base.StartPosition = FormStartPosition.CenterParent;
			base.Load += new EventHandler(this.formEffectSelecter_Load);
			base.ResumeLayout(false);
		}
	}
}
