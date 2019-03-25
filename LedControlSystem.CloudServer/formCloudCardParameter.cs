using LedCommunication;
using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LedControlSystem.CloudServer
{
	public class formCloudCardParameter : Form
	{
		public LedPanel thispanel;

		public string terminalId = string.Empty;

		private CloudServerComm login_zoehoo = new CloudServerComm();

		public string sendData = string.Empty;

		public string mark = string.Empty;

		private IContainer components;

		private Label label7;

		private Label label6;

		private Label label5;

		private Label label4;

		private Label label3;

		private Label label2;

		private TextBox TxtModel;

		private TextBox TxtHeight;

		private TextBox TxtWidth;

		private TextBox TxtOEPolarity;

		private TextBox TxtDataPolarity;

		private ComboBox CboColorModel;

		private GroupBox groupBox1;

		private Label label1;

		private Label LblRouting;

		private Button BtnLoadParam;

		private Button BtnClose;

		private Timer timer1;

		private PictureBox pictureBox1;

		public formCloudCardParameter()
		{
			this.InitializeComponent();
		}

		private void formCloudCardParameter_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.Text = formMain.ML.GetStr("formCloudCardParameter_FormText");
			this.groupBox1.Text = formMain.ML.GetStr("formCloudCardParameter_GroupBoxParameter");
			this.label2.Text = formMain.ML.GetStr("formpaneledit_label_Type");
			this.label3.Text = formMain.ML.GetStr("formpaneledit_label_width");
			this.label4.Text = formMain.ML.GetStr("formpaneledit_label_Height");
			this.label5.Text = formMain.ML.GetStr("formpaneledit_label_OE_polarity");
			this.label6.Text = formMain.ML.GetStr("formpaneledit_label_Data");
			this.label7.Text = formMain.ML.GetStr("formpaneledit_label_ColorMode");
			this.label1.Text = formMain.ML.GetStr("formpaneledit_label_Traces");
			this.BtnLoadParam.Text = formMain.ML.GetStr("formRouting_Scannings_BtnLoad");
			this.BtnClose.Text = formMain.ML.GetStr("formServer_button_Close");
			this.groupBox1.Enabled = false;
			this.pictureBox1.Location = new System.Drawing.Point(125, 205);
			this.pictureBox1.Visible = true;
			this.BtnLoadParam.Visible = false;
			if (this.login_zoehoo.API_SendSingleCmd(this.terminalId, this.sendData))
			{
				this.timer1.Start();
			}
		}

		private void CboColorModel_DrawItem(object sender, DrawItemEventArgs e)
		{
			System.Drawing.Graphics arg_06_0 = e.Graphics;
			System.Drawing.Rectangle bounds = e.Bounds;
			ComboBox comboBox = (ComboBox)sender;
			if (e.Index < comboBox.Items.Count)
			{
				int arg_31_0 = e.Index;
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				switch (e.Index)
				{
				case 0:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height - 2));
					break;
				case 1:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 2, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width / 2, bounds.Y + 1, bounds.Width / 2, bounds.Height - 2));
					break;
				case 2:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 2, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width / 2, bounds.Y + 1, bounds.Width / 2, bounds.Height - 2));
					break;
				case 3:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 4:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 5:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 6:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 7:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				case 8:
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(bounds.X + bounds.Width / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(bounds.X + bounds.Width * 2 / 3, bounds.Y + 1, bounds.Width / 3, bounds.Height - 2));
					break;
				}
				e.DrawFocusRectangle();
			}
		}

		private void BtnLoadParam_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, formMain.ML.GetStr("formCloudCardParameter_LoadParam"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
			{
				formMain.ledsys.SelectedPanel.Copy(this.thispanel);
				string text = this.thispanel.CardType.ToString();
				if (text.IndexOf("E") > -1 && text.IndexOf("L") > -1)
				{
					formMain.ledsys.SelectedPanel.PortType = LedPortType.Ethernet;
					formMain.ledsys.SelectedPanel.EthernetCommunicaitonMode = LedEthernetCommunicationMode.CloudServer;
				}
				else if (text.IndexOf("G") > -1 && text.IndexOf("L") > -1)
				{
					formMain.ledsys.SelectedPanel.PortType = LedPortType.GPRS;
				}
				formCloudServerSend.IsLoadedPanelParam = true;
				base.Close();
			}
		}

		private void BtnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void formCloudCardParameter_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.timer1.Stop();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			IList<SingleCmdRecord> list = this.login_zoehoo.API_GetTerminalCmd(this.terminalId);
			if (list != null)
			{
				foreach (SingleCmdRecord current in list)
				{
					if (current.Description == this.mark && current.Response != null)
					{
						IList<Unpack_Results> list2 = protocol_single_cmd.Rec_Unpack(Convert.FromBase64String(current.Response), Convert.FromBase64String(current.Request));
						this.thispanel = (list2[0].UnpackedData as LedPanel);
						this.pictureBox1.Visible = false;
						this.groupBox1.Enabled = true;
						if (this.thispanel != null)
						{
							this.TxtModel.Text = this.thispanel.CardType.ToString();
							this.TxtHeight.Text = this.thispanel.Height.ToString();
							this.TxtWidth.Text = this.thispanel.Width.ToString();
							int scanTypeIndex = this.thispanel.RoutingSetting.ScanTypeIndex;
							int routingIndex = this.thispanel.RoutingSetting.RoutingIndex;
							this.LblRouting.Text = "\r\n" + formRouting.GetRoutingString(scanTypeIndex, routingIndex);
							this.thispanel.RoutingSetting.SettingFileName = LedCommonConst.RoutingSettingLFileName;
							byte[] array = this.thispanel.RoutingSetting.LoadScanFromFile();
							if (array[0] == 1)
							{
								this.TxtOEPolarity.Text = formMain.ML.GetStr("formCloudCardParameter_HighLevel");
							}
							else
							{
								this.TxtOEPolarity.Text = formMain.ML.GetStr("formCloudCardParameter_LowLevel");
							}
							if (array[1] == 1)
							{
								this.TxtDataPolarity.Text = formMain.ML.GetStr("formCloudCardParameter_HighLevel");
							}
							else
							{
								this.TxtDataPolarity.Text = formMain.ML.GetStr("formCloudCardParameter_LowLevel");
							}
							this.CboColorModel.SelectedIndex = this.thispanel.ColorMode - LedColorMode.R;
						}
						this.timer1.Stop();
					}
				}
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formCloudCardParameter));
			this.TxtModel = new TextBox();
			this.TxtHeight = new TextBox();
			this.TxtWidth = new TextBox();
			this.TxtOEPolarity = new TextBox();
			this.TxtDataPolarity = new TextBox();
			this.CboColorModel = new ComboBox();
			this.label7 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.groupBox1 = new GroupBox();
			this.BtnLoadParam = new Button();
			this.label1 = new Label();
			this.LblRouting = new Label();
			this.BtnClose = new Button();
			this.timer1 = new Timer(this.components);
			this.pictureBox1 = new PictureBox();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.TxtModel.Location = new System.Drawing.Point(119, 40);
			this.TxtModel.Name = "TxtModel";
			this.TxtModel.Size = new System.Drawing.Size(129, 21);
			this.TxtModel.TabIndex = 14;
			this.TxtHeight.Location = new System.Drawing.Point(119, 124);
			this.TxtHeight.Name = "TxtHeight";
			this.TxtHeight.Size = new System.Drawing.Size(129, 21);
			this.TxtHeight.TabIndex = 13;
			this.TxtWidth.Location = new System.Drawing.Point(119, 82);
			this.TxtWidth.Name = "TxtWidth";
			this.TxtWidth.Size = new System.Drawing.Size(129, 21);
			this.TxtWidth.TabIndex = 12;
			this.TxtOEPolarity.Location = new System.Drawing.Point(119, 166);
			this.TxtOEPolarity.Name = "TxtOEPolarity";
			this.TxtOEPolarity.Size = new System.Drawing.Size(129, 21);
			this.TxtOEPolarity.TabIndex = 11;
			this.TxtDataPolarity.Location = new System.Drawing.Point(119, 208);
			this.TxtDataPolarity.Name = "TxtDataPolarity";
			this.TxtDataPolarity.Size = new System.Drawing.Size(129, 21);
			this.TxtDataPolarity.TabIndex = 10;
			this.CboColorModel.DrawMode = DrawMode.OwnerDrawFixed;
			this.CboColorModel.DropDownStyle = ComboBoxStyle.DropDownList;
			this.CboColorModel.FormattingEnabled = true;
			this.CboColorModel.ImeMode = ImeMode.On;
			this.CboColorModel.Items.AddRange(new object[]
			{
				"1",
				"1",
				"1",
				"1"
			});
			this.CboColorModel.Location = new System.Drawing.Point(119, 250);
			this.CboColorModel.Name = "CboColorModel";
			this.CboColorModel.Size = new System.Drawing.Size(129, 22);
			this.CboColorModel.TabIndex = 9;
			this.CboColorModel.DrawItem += new DrawItemEventHandler(this.CboColorModel_DrawItem);
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(23, 253);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(53, 12);
			this.label7.TabIndex = 6;
			this.label7.Text = "色彩模式";
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(23, 211);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 12);
			this.label6.TabIndex = 5;
			this.label6.Text = "数据极性";
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(23, 169);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 4;
			this.label5.Text = "OE极性";
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(23, 127);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 12);
			this.label4.TabIndex = 3;
			this.label4.Text = "高度";
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 85);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "宽度";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "型号";
			this.groupBox1.Controls.Add(this.BtnLoadParam);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.LblRouting);
			this.groupBox1.Controls.Add(this.TxtModel);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.TxtHeight);
			this.groupBox1.Controls.Add(this.TxtOEPolarity);
			this.groupBox1.Controls.Add(this.TxtDataPolarity);
			this.groupBox1.Controls.Add(this.TxtWidth);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.CboColorModel);
			this.groupBox1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.groupBox1.Location = new System.Drawing.Point(19, 26);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(279, 453);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "控制卡参数";
			this.BtnLoadParam.Location = new System.Drawing.Point(119, 412);
			this.BtnLoadParam.Name = "BtnLoadParam";
			this.BtnLoadParam.Size = new System.Drawing.Size(129, 23);
			this.BtnLoadParam.TabIndex = 18;
			this.BtnLoadParam.Text = "加载";
			this.BtnLoadParam.UseVisualStyleBackColor = true;
			this.BtnLoadParam.Click += new EventHandler(this.BtnLoadParam_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 300);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 17;
			this.label1.Text = "走线方式";
			this.LblRouting.BackColor = System.Drawing.SystemColors.Window;
			this.LblRouting.Location = new System.Drawing.Point(23, 322);
			this.LblRouting.Name = "LblRouting";
			this.LblRouting.Size = new System.Drawing.Size(225, 68);
			this.LblRouting.TabIndex = 16;
			this.BtnClose.Location = new System.Drawing.Point(192, 498);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Size = new System.Drawing.Size(75, 23);
			this.BtnClose.TabIndex = 16;
			this.BtnClose.Text = "关闭";
			this.BtnClose.UseVisualStyleBackColor = true;
			this.BtnClose.Click += new EventHandler(this.BtnClose_Click);
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = Resources.loading;
			this.pictureBox1.Location = new System.Drawing.Point(132, 205);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(85, 85);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 19;
			this.pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(319, 534);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.BtnClose);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudCardParameter";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "控制卡信息";
			base.FormClosing += new FormClosingEventHandler(this.formCloudCardParameter_FormClosing);
			base.Load += new EventHandler(this.formCloudCardParameter_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
