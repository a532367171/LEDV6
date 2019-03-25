using LedModel;
using LedModel.Content;
using LedModel.Element.String;
using LedModel.Enum;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class StringEditor : UserControl
	{
		private LedString stringContent;

		private LedPanel panel;

		private IContainer components;

		private RichTextBox rtxContent;

		private Label lblNumberCount;

		private Label lblEachNumber;

		private TextBox txtNumberCount;

		private TextBox txtEachNumber;

		private Label lblNumberCountHint;

		private Label lblEachNumberHint;

		private Label lblID;

		private TextBox txtID;

		private Label lblIDHint;

		public event LedGlobal.LedContentEvent UpdateEvent;

		public StringEditor()
		{
			this.InitializeComponent();
		}

		public void Edit(LedString pString)
		{
			this.panel = formMain.ledsys.SelectedPanel;
			this.stringContent = pString;
			try
			{
				this.Binding();
			}
			catch
			{
			}
		}

		private void Binding()
		{
			LedStringBackground ledStringBackground = (this.stringContent != null) ? this.stringContent.StringBackground : null;
			if (this.stringContent != null)
			{
				this.txtID.Text = this.stringContent.ID;
			}
			else
			{
				this.txtID.Text = "0";
			}
			if (this.stringContent != null)
			{
				this.txtNumberCount.Text = this.stringContent.NumberCount.ToString();
			}
			else
			{
				this.txtNumberCount.Text = "1";
			}
			if (this.stringContent != null)
			{
				this.txtEachNumber.Text = this.stringContent.EachNumber.ToString();
			}
			else
			{
				this.txtEachNumber.Text = "1";
			}
			if (ledStringBackground != null)
			{
				this.rtxContent.Text = ledStringBackground.Text;
			}
			else
			{
				this.rtxContent.Text = string.Empty;
			}
			this.rtxContent.Visible = false;
		}

		private void rtxContent_TextChanged(object sender, EventArgs e)
		{
			RichTextBox richTextBox = (RichTextBox)sender;
			if (!richTextBox.Focused)
			{
				return;
			}
			if (this.stringContent != null && this.stringContent.StringBackground != null)
			{
				if (richTextBox.TextLength > 0)
				{
					this.stringContent.StringBackground.Text = richTextBox.Text;
				}
				else
				{
					this.stringContent.StringBackground.Text = " ";
				}
			}
			this.Redraw();
		}

		private void txtID_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.stringContent != null)
			{
				int num;
				bool flag = int.TryParse(textBox.Text, out num);
				if (flag && num >= 0 && num <= 65535)
				{
					this.stringContent.ID = num.ToString();
					this.Redraw();
					return;
				}
				textBox.Text = this.stringContent.ID;
			}
		}

		private void txtID_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void txtNumberCount_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.stringContent != null)
			{
				int numberCount;
				bool flag = int.TryParse(textBox.Text, out numberCount);
				if (flag)
				{
					this.stringContent.NumberCount = numberCount;
					textBox.Text = this.stringContent.NumberCount.ToString();
				}
			}
		}

		private void txtNumberCount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void txtEachNumber_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			if (!textBox.Focused)
			{
				return;
			}
			if (this.stringContent != null)
			{
				int eachNumber;
				bool flag = int.TryParse(textBox.Text, out eachNumber);
				if (flag)
				{
					this.stringContent.EachNumber = eachNumber;
					textBox.Text = this.stringContent.EachNumber.ToString();
				}
			}
		}

		private void txtEachNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void Redraw()
		{
			if (this.UpdateEvent != null)
			{
				this.UpdateEvent(LedContentEventType.Text, this);
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
			this.rtxContent = new RichTextBox();
			this.lblNumberCount = new Label();
			this.lblEachNumber = new Label();
			this.txtNumberCount = new TextBox();
			this.txtEachNumber = new TextBox();
			this.lblNumberCountHint = new Label();
			this.lblEachNumberHint = new Label();
			this.lblID = new Label();
			this.txtID = new TextBox();
			this.lblIDHint = new Label();
			base.SuspendLayout();
			this.rtxContent.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.rtxContent.BackColor = System.Drawing.Color.White;
			this.rtxContent.Location = new System.Drawing.Point(0, 95);
			this.rtxContent.Name = "rtxContent";
			this.rtxContent.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtxContent.Size = new System.Drawing.Size(442, 74);
			this.rtxContent.TabIndex = 33;
			this.rtxContent.Text = "";
			this.rtxContent.TextChanged += new EventHandler(this.rtxContent_TextChanged);
			this.lblNumberCount.AutoSize = true;
			this.lblNumberCount.Location = new System.Drawing.Point(2, 42);
			this.lblNumberCount.Name = "lblNumberCount";
			this.lblNumberCount.Size = new System.Drawing.Size(29, 12);
			this.lblNumberCount.TabIndex = 34;
			this.lblNumberCount.Text = "数量";
			this.lblEachNumber.AutoSize = true;
			this.lblEachNumber.Location = new System.Drawing.Point(2, 71);
			this.lblEachNumber.Name = "lblEachNumber";
			this.lblEachNumber.Size = new System.Drawing.Size(29, 12);
			this.lblEachNumber.TabIndex = 35;
			this.lblEachNumber.Text = "字数";
			this.txtNumberCount.Location = new System.Drawing.Point(62, 39);
			this.txtNumberCount.Name = "txtNumberCount";
			this.txtNumberCount.Size = new System.Drawing.Size(40, 21);
			this.txtNumberCount.TabIndex = 36;
			this.txtNumberCount.TextChanged += new EventHandler(this.txtNumberCount_TextChanged);
			this.txtNumberCount.KeyPress += new KeyPressEventHandler(this.txtNumberCount_KeyPress);
			this.txtEachNumber.Location = new System.Drawing.Point(62, 68);
			this.txtEachNumber.Name = "txtEachNumber";
			this.txtEachNumber.Size = new System.Drawing.Size(40, 21);
			this.txtEachNumber.TabIndex = 37;
			this.txtEachNumber.TextChanged += new EventHandler(this.txtEachNumber_TextChanged);
			this.txtEachNumber.KeyPress += new KeyPressEventHandler(this.txtEachNumber_KeyPress);
			this.lblNumberCountHint.AutoSize = true;
			this.lblNumberCountHint.Location = new System.Drawing.Point(108, 42);
			this.lblNumberCountHint.Name = "lblNumberCountHint";
			this.lblNumberCountHint.Size = new System.Drawing.Size(71, 12);
			this.lblNumberCountHint.TabIndex = 38;
			this.lblNumberCountHint.Text = "(最多250个)";
			this.lblEachNumberHint.AutoSize = true;
			this.lblEachNumberHint.Location = new System.Drawing.Point(108, 71);
			this.lblEachNumberHint.Name = "lblEachNumberHint";
			this.lblEachNumberHint.Size = new System.Drawing.Size(221, 12);
			this.lblEachNumberHint.TabIndex = 39;
			this.lblEachNumberHint.Text = "(最多500个字，数量x字数不能超过8000)";
			this.lblID.AutoSize = true;
			this.lblID.Location = new System.Drawing.Point(2, 11);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(17, 12);
			this.lblID.TabIndex = 34;
			this.lblID.Text = "ID";
			this.txtID.Location = new System.Drawing.Point(62, 8);
			this.txtID.MaxLength = 5;
			this.txtID.Name = "txtID";
			this.txtID.Size = new System.Drawing.Size(40, 21);
			this.txtID.TabIndex = 36;
			this.txtID.TextChanged += new EventHandler(this.txtID_TextChanged);
			this.txtID.KeyPress += new KeyPressEventHandler(this.txtID_KeyPress);
			this.lblIDHint.AutoSize = true;
			this.lblIDHint.Location = new System.Drawing.Point(108, 11);
			this.lblIDHint.Name = "lblIDHint";
			this.lblIDHint.Size = new System.Drawing.Size(59, 12);
			this.lblIDHint.TabIndex = 38;
			this.lblIDHint.Text = "(0~65535)";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.lblEachNumberHint);
			base.Controls.Add(this.lblIDHint);
			base.Controls.Add(this.lblNumberCountHint);
			base.Controls.Add(this.txtEachNumber);
			base.Controls.Add(this.txtID);
			base.Controls.Add(this.txtNumberCount);
			base.Controls.Add(this.lblEachNumber);
			base.Controls.Add(this.lblID);
			base.Controls.Add(this.lblNumberCount);
			base.Controls.Add(this.rtxContent);
			base.Name = "StringEditor";
			base.Size = new System.Drawing.Size(437, 180);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
