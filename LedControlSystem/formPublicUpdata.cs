using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formPublicUpdata : Form
	{
		private string codefile;

		private Form ownerForm;

		private static string formID = "formPublicUpdata";

		private IContainer components;

		private CheckBox checkBoxCode;

		private Button buttonCode;

		private TextBox textBoxCode;

		private GroupBox groupBox1;

		private Button buttonCodeSend;

		public static string FormID
		{
			get
			{
				return formPublicUpdata.formID;
			}
			set
			{
				formPublicUpdata.formID = value;
			}
		}

		public formPublicUpdata()
		{
			this.InitializeComponent();
			this.Diplay_lanuage_Text();
		}

		public void Diplay_lanuage_Text()
		{
			this.groupBox1.Text = formMain.ML.GetStr("formPublicUpdata_groupBox_Update");
			this.checkBoxCode.Text = formMain.ML.GetStr("formPublicUpdata_checkBox_CodeProgram");
			this.buttonCode.Text = formMain.ML.GetStr("formPublicUpdata_button_CodeBrowse");
			this.buttonCodeSend.Text = formMain.ML.GetStr("formPublicUpdata_button_CodeSend");
		}

		public formPublicUpdata(Form mf)
		{
			this.InitializeComponent();
			this.Diplay_lanuage_Text();
			this.ownerForm = mf;
		}

		public string getCodeFile()
		{
			base.ShowDialog(this.ownerForm);
			return this.codefile;
		}

		private void buttonCode_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "bin files (*.zhcode)|*.zhcode";
			try
			{
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
			catch
			{
				openFileDialog.InitialDirectory = formMain.DesktopPath;
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
			this.textBoxCode.Text = openFileDialog.FileName;
		}

		private void buttonCodeSend_Click(object sender, EventArgs e)
		{
			this.codefile = this.textBoxCode.Text;
			base.Close();
		}

		private void checkBoxCode_CheckedChanged(object sender, EventArgs e)
		{
			this.textBoxCode.Enabled = this.checkBoxCode.Checked;
			this.buttonCode.Enabled = this.checkBoxCode.Checked;
			this.buttonCodeSend.Enabled = this.checkBoxCode.Checked;
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
			this.checkBoxCode = new CheckBox();
			this.buttonCode = new Button();
			this.textBoxCode = new TextBox();
			this.groupBox1 = new GroupBox();
			this.buttonCodeSend = new Button();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.checkBoxCode.AutoSize = true;
			this.checkBoxCode.Location = new System.Drawing.Point(28, 32);
			this.checkBoxCode.Name = "checkBoxCode";
			this.checkBoxCode.Size = new System.Drawing.Size(72, 16);
			this.checkBoxCode.TabIndex = 0;
			this.checkBoxCode.Text = "升级程序";
			this.checkBoxCode.UseVisualStyleBackColor = true;
			this.checkBoxCode.CheckedChanged += new EventHandler(this.checkBoxCode_CheckedChanged);
			this.buttonCode.Enabled = false;
			this.buttonCode.Location = new System.Drawing.Point(433, 52);
			this.buttonCode.Name = "buttonCode";
			this.buttonCode.Size = new System.Drawing.Size(75, 23);
			this.buttonCode.TabIndex = 1;
			this.buttonCode.Text = "浏览";
			this.buttonCode.UseVisualStyleBackColor = true;
			this.buttonCode.Click += new EventHandler(this.buttonCode_Click);
			this.textBoxCode.Enabled = false;
			this.textBoxCode.Location = new System.Drawing.Point(28, 54);
			this.textBoxCode.Name = "textBoxCode";
			this.textBoxCode.Size = new System.Drawing.Size(399, 21);
			this.textBoxCode.TabIndex = 2;
			this.groupBox1.Controls.Add(this.buttonCodeSend);
			this.groupBox1.Controls.Add(this.buttonCode);
			this.groupBox1.Controls.Add(this.textBoxCode);
			this.groupBox1.Controls.Add(this.checkBoxCode);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(524, 150);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "程序升级";
			this.buttonCodeSend.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.buttonCodeSend.Enabled = false;
			this.buttonCodeSend.Location = new System.Drawing.Point(187, 110);
			this.buttonCodeSend.Name = "buttonCodeSend";
			this.buttonCodeSend.Size = new System.Drawing.Size(87, 24);
			this.buttonCodeSend.TabIndex = 3;
			this.buttonCodeSend.Text = "发送";
			this.buttonCodeSend.UseVisualStyleBackColor = true;
			this.buttonCodeSend.Click += new EventHandler(this.buttonCodeSend_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.ClientSize = new System.Drawing.Size(548, 184);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			this.MaximumSize = new System.Drawing.Size(564, 223);
			this.MinimumSize = new System.Drawing.Size(564, 223);
			base.Name = "formPublicUpdata";
			base.StartPosition = FormStartPosition.CenterParent;
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
