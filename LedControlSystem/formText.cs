using LedModel;
using LedModel.Content;
using LedModel.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formText : Form
	{
		private static string formID = "formText";

		private IContainer components;

		private TextEditor textEditor1;

		public event LedGlobal.LedContentEvent UpdateEvent;

		public static string FormID
		{
			get
			{
				return formText.formID;
			}
			set
			{
				formText.formID = value;
			}
		}

		public formText()
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formText.formID;
			this.Text = formMain.ML.GetStr("formText_FormText");
			this.textEditor1.Controls["chkCoupletWord"].Text = formMain.ML.GetStr("formText_chkCoupletWord");
		}

		public void Edit(LedDText pText)
		{
			this.Text = formMain.ML.GetStr("formText_FormText");
			this.textEditor1.Controls["chkCoupletWord"].Text = formMain.ML.GetStr("formText_chkCoupletWord");
			this.textEditor1.Edit(pText);
			this.textEditor1.UpdateEvent += new LedGlobal.LedContentEvent(this.textEditor1_UpdateEvent);
			base.ShowDialog();
		}

		private void textEditor1_UpdateEvent(LedContentEventType type, object sender)
		{
			this.UpdateEvent(type, sender);
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
			this.textEditor1 = new TextEditor();
			base.SuspendLayout();
			this.textEditor1.BackColor = System.Drawing.Color.Transparent;
			this.textEditor1.Dock = DockStyle.Fill;
			this.textEditor1.Location = new System.Drawing.Point(0, 0);
			this.textEditor1.Name = "textEditor1";
			this.textEditor1.Size = new System.Drawing.Size(450, 169);
			this.textEditor1.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImageLayout = ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(450, 169);
			base.Controls.Add(this.textEditor1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "formText";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "字幕编辑";
			base.ResumeLayout(false);
		}
	}
}
