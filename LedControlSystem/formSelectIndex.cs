using LedControlSystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formSelectIndex : Form
	{
		private int Selected = -1;

		private bool isOK;

		private static string formID = "formSelectIndex";

		private IContainer components;

		private ComboBox comboBox1;

		private Label label1;

		private Button buttonOK;

		private Button buttonCancel;

		public static string FormID
		{
			get
			{
				return formSelectIndex.formID;
			}
			set
			{
				formSelectIndex.formID = value;
			}
		}

		public int GetIndex()
		{
			base.ShowDialog();
			return this.Selected;
		}

		public formSelectIndex()
		{
			this.InitializeComponent();
			this.buttonOK.Text = formMain.ML.GetStr("Button_Sure");
			this.buttonCancel.Text = formMain.ML.GetStr("Button_Cancel");
		}

		public formSelectIndex(string pTitle, string pDescription, List<string> pTerms)
		{
			this.InitializeComponent();
			this.Text = pTitle;
			this.label1.Text = pDescription;
			foreach (string current in pTerms)
			{
				this.comboBox1.Items.Add(current);
			}
			this.comboBox1.SelectedIndex = 0;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.isOK = true;
			base.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.Selected = this.comboBox1.SelectedIndex;
		}

		private void formSelectIndex_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
				return;
			}
			base.Icon = Resources.AppIcon;
		}

		private void formSelectIndex_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.isOK)
			{
				this.Selected = -1;
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
			this.comboBox1 = new ComboBox();
			this.label1 = new Label();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			base.SuspendLayout();
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(24, 38);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(274, 20);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "请选择";
			this.buttonOK.Location = new System.Drawing.Point(52, 80);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "确定";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new System.Drawing.Point(197, 80);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "取消";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(321, 115);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBox1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formSelectIndex";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "选择";
			base.FormClosing += new FormClosingEventHandler(this.formSelectIndex_FormClosing);
			base.Load += new EventHandler(this.formSelectIndex_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
