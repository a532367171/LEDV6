using LedControlSystem.Properties;
using LedResources;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LedControlSystem
{
	public class formMultiLanguage : Form
	{
		private MultiLanguageGenerator miuLanguate = new MultiLanguageGenerator();

		private string xmlPath;

		private IContainer components;

		private Button button1;

		private Button button2;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private Label label2;

		private Label label1;

		private Button button3;

		private Button button4;

		public formMultiLanguage()
		{
			this.InitializeComponent();
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath);
			this.xmlPath = directoryInfo.Parent.Parent.FullName + "\\Language\\";
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void button2_Click(object sender, EventArgs e)
		{
		}

		private void button3_Click(object sender, EventArgs e)
		{
		}

		private void formMultiLanguage_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			MultiLanguageGenerator.IsCreateTempalate = true;
		}

		private void formMultiLanguage_FormClosing(object sender, FormClosingEventArgs e)
		{
			MultiLanguageGenerator.IsCreateTempalate = false;
		}

		private void button4_Click(object sender, EventArgs e)
		{
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
			this.button2 = new Button();
			this.groupBox1 = new GroupBox();
			this.button3 = new Button();
			this.label2 = new Label();
			this.groupBox2 = new GroupBox();
			this.button4 = new Button();
			this.label1 = new Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.button1.Location = new System.Drawing.Point(24, 142);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(98, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "重新生成全新";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new System.Drawing.Point(24, 142);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(126, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "同步资源";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Location = new System.Drawing.Point(311, 30);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(176, 218);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "多语言模板同步";
			this.button3.Location = new System.Drawing.Point(24, 171);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(126, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "同步界面";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.label2.Location = new System.Drawing.Point(22, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(148, 89);
			this.label2.TabIndex = 5;
			this.label2.Text = "当多语言模板已经生成,并且使用,添加了新的窗体或控件,需要补足缺失项时,请使用此项";
			this.groupBox2.Controls.Add(this.button4);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Location = new System.Drawing.Point(12, 30);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(176, 218);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "多语言模板生成";
			this.button4.Location = new System.Drawing.Point(24, 171);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(98, 23);
			this.button4.TabIndex = 5;
			this.button4.Text = "补足新控件";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.label1.Location = new System.Drawing.Point(22, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148, 113);
			this.label1.TabIndex = 4;
			this.label1.Text = "当大部分的窗体控件都完成以后,请使用此项生成多语言模板";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(505, 256);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formMultiLanguage";
			this.Text = "fromMultiLanguage";
			base.FormClosing += new FormClosingEventHandler(this.formMultiLanguage_FormClosing);
			base.Load += new EventHandler(this.formMultiLanguage_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
