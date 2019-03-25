using LedCommunication;
using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Enum;
using LedService.Cloud.Terminal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.Cloud
{
	public class formCloudTerminalParameter : Form
	{
		private const int maxSendCount = 5;

		private bool needtoClose;

		private string terminalID;

		private string parameterID;

		private IContainer components;

		private Button btnClose;

		private System.Windows.Forms.Timer timer1;

		private Label lblRouting;

		private Label txtRouting;

		private TextBox txtCardType;

		private Label lblCardType;

		private Label lblColorMode;

		private Label lblOEPolarity;

		private TextBox txtHeight;

		private TextBox txtOEPolarity;

		private TextBox txtDataPolarity;

		private TextBox txtWidth;

		private Label lblDataPolarity;

		private Label lblHeight;

		private Label lblWidth;

		private PictureBox picLoading;

		private PictureBox picColorMode;

		public formCloudTerminalParameter(string id)
		{
			this.InitializeComponent();
			this.txtCardType.ReadOnly = true;
			this.txtHeight.ReadOnly = true;
			this.txtWidth.ReadOnly = true;
			this.txtOEPolarity.ReadOnly = true;
			this.txtDataPolarity.ReadOnly = true;
			this.terminalID = id;
			this.DisplayLanuageText();
		}

		private void DisplayLanuageText()
		{
			this.Text = formMain.ML.GetStr("formCloudTerminalParameter_Form_TerminalInfo");
			this.lblCardType.Text = formMain.ML.GetStr("formCloudTerminalParameter_Label_CardType");
			this.lblWidth.Text = formMain.ML.GetStr("formCloudTerminalParameter_Label_Width");
			this.lblHeight.Text = formMain.ML.GetStr("formCloudTerminalParameter_Label_Height");
			this.lblOEPolarity.Text = formMain.ML.GetStr("formCloudTerminalParameter_Label_OEPolarity");
			this.lblDataPolarity.Text = formMain.ML.GetStr("formCloudTerminalParameter_Label_DataPolarity");
			this.lblColorMode.Text = formMain.ML.GetStr("formCloudTerminalParameter_Label_ColorMode");
			this.lblRouting.Text = formMain.ML.GetStr("formCloudTerminalParameter_Label_Routing");
			this.btnClose.Text = formMain.ML.GetStr("formCloudTerminalParameter_Button_Close");
		}

		private void formCloudTerminalParameter_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			Thread thread = new Thread(new ThreadStart(this.LoadTerminalParameter));
			thread.Start();
		}

		private void formCloudTerminalParameter_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void LoadTerminalParameter()
		{
			this.needtoClose = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(500);
			if (this.SendParameter())
			{
				Thread.Sleep(5000);
				this.GetParameter();
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(false);
			}));
			Thread.Sleep(300);
			this.needtoClose = true;
		}

		private bool SendParameter()
		{
			int i = 0;
			bool result = false;
			IList<byte[]> list = protocol_single_cmd.Send_Pack(0, 0, LedCmdType.Read_Panel_Parameter, null, false, null, string.Empty, 52);
			if (list == null || list.Count == 0)
			{
				return result;
			}
			while (i < 5)
			{
				i++;
				this.parameterID = string.Empty;
				bool flag = new SingleCommandService().Send(LedGlobal.CloudAccount.SessionID, this.terminalID, list[0], "加载", ref this.parameterID);
				if (flag)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private void GetParameter()
		{
			DateTime now = DateTime.Now;
			bool flag = false;
			LedPanel panel = null;
			while (!flag && (DateTime.Now - now).TotalSeconds < 60.0)
			{
				IList<SingleCommandInfo> list = new SingleCommandService().GetList(LedGlobal.CloudAccount.SessionID, this.terminalID);
				if (list != null)
				{
					foreach (SingleCommandInfo current in list)
					{
						if (current.ID == this.parameterID && current.ReceiveData != null && current.ReceiveData.Length > 0 && current.SendData != null && current.SendData.Length > 0)
						{
							IList<Unpack_Results> list2 = protocol_single_cmd.Rec_Unpack(current.ReceiveData, current.SendData);
							if (list2 != null && list2.Count > 0 && list2[0].UnpackedData.GetType() == typeof(LedPanel))
							{
								panel = (list2[0].UnpackedData as LedPanel);
								flag = true;
								break;
							}
						}
					}
				}
				Thread.Sleep(1000);
			}
			if (flag)
			{
				base.Invoke(new MethodInvoker(delegate
				{
					this.BindingParameter(panel);
				}));
				Thread.Sleep(200);
			}
		}

		private void BindingParameter(LedPanel panel)
		{
			if (panel == null)
			{
				return;
			}
			this.txtCardType.Text = panel.CardType.ToString();
			this.txtHeight.Text = panel.Height.ToString();
			this.txtWidth.Text = panel.Width.ToString();
			int scanTypeIndex = panel.RoutingSetting.ScanTypeIndex;
			int routingIndex = panel.RoutingSetting.RoutingIndex;
			if (panel.OEPolarity == 1)
			{
				this.txtOEPolarity.Text = formMain.ML.GetStr("formCloudCardParameter_HighLevel");
			}
			else
			{
				this.txtOEPolarity.Text = formMain.ML.GetStr("formCloudCardParameter_LowLevel");
			}
			if (panel.DataPolarity == 1)
			{
				this.txtDataPolarity.Text = formMain.ML.GetStr("formCloudCardParameter_HighLevel");
			}
			else
			{
				this.txtDataPolarity.Text = formMain.ML.GetStr("formCloudCardParameter_LowLevel");
			}
			this.picColorMode.Image = this.DrawColorMode(panel.ColorMode - LedColorMode.R);
			this.txtRouting.Text = "\r\n" + formRouting.GetRoutingString(scanTypeIndex, routingIndex);
		}

		private System.Drawing.Bitmap DrawColorMode(int index)
		{
			System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(new System.Drawing.Point(2, 3), new System.Drawing.Size(this.picColorMode.Width - 4, this.picColorMode.Height - 6));
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.picColorMode.Width, this.picColorMode.Height);
			System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
			graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			switch (index)
			{
			case 0:
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height - 2));
				break;
			case 1:
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 2, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(rectangle.X + rectangle.Width / 2, rectangle.Y, rectangle.Width / 2, rectangle.Height - 2));
				break;
			case 2:
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 2, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(rectangle.X + rectangle.Width / 2, rectangle.Y, rectangle.Width / 2, rectangle.Height - 2));
				break;
			case 3:
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(rectangle.X + rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(rectangle.X + rectangle.Width * 2 / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				break;
			case 4:
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(rectangle.X + rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(rectangle.X + rectangle.Width * 2 / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				break;
			case 5:
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(rectangle.X + rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(rectangle.X + rectangle.Width * 2 / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				break;
			case 6:
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(rectangle.X + rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(rectangle.X + rectangle.Width * 2 / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				break;
			case 7:
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(rectangle.X + rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(rectangle.X + rectangle.Width * 2 / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				break;
			case 8:
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(rectangle.X + rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), new System.Drawing.Rectangle(rectangle.X + rectangle.Width * 2 / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height - 2));
				break;
			}
			graphics.Dispose();
			return bitmap;
		}

		public void LoadingVisible(bool pBool)
		{
			if (base.Controls != null && base.Controls.Count > 0)
			{
				foreach (Control control in base.Controls)
				{
					if (control != null && control.Name != "picLoading")
					{
						control.Enabled = !pBool;
					}
				}
			}
			if (pBool)
			{
				this.picLoading.Visible = true;
				this.picLoading.BringToFront();
				return;
			}
			this.picLoading.Visible = false;
			this.picLoading.SendToBack();
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
			this.btnClose = new Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.lblRouting = new Label();
			this.txtRouting = new Label();
			this.txtCardType = new TextBox();
			this.lblCardType = new Label();
			this.lblColorMode = new Label();
			this.lblOEPolarity = new Label();
			this.txtHeight = new TextBox();
			this.txtOEPolarity = new TextBox();
			this.txtDataPolarity = new TextBox();
			this.txtWidth = new TextBox();
			this.lblDataPolarity = new Label();
			this.lblHeight = new Label();
			this.lblWidth = new Label();
			this.picLoading = new PictureBox();
			this.picColorMode = new PictureBox();
			((ISupportInitialize)this.picLoading).BeginInit();
			((ISupportInitialize)this.picColorMode).BeginInit();
			base.SuspendLayout();
			this.btnClose.Location = new System.Drawing.Point(180, 387);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 21;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.lblRouting.AutoSize = true;
			this.lblRouting.Location = new System.Drawing.Point(30, 276);
			this.lblRouting.Name = "lblRouting";
			this.lblRouting.Size = new System.Drawing.Size(53, 12);
			this.lblRouting.TabIndex = 35;
			this.lblRouting.Text = "走线方式";
			this.txtRouting.BackColor = System.Drawing.SystemColors.Window;
			this.txtRouting.Location = new System.Drawing.Point(30, 300);
			this.txtRouting.Name = "txtRouting";
			this.txtRouting.Size = new System.Drawing.Size(225, 70);
			this.txtRouting.TabIndex = 34;
			this.txtCardType.Location = new System.Drawing.Point(126, 21);
			this.txtCardType.Name = "txtCardType";
			this.txtCardType.Size = new System.Drawing.Size(129, 21);
			this.txtCardType.TabIndex = 33;
			this.lblCardType.AutoSize = true;
			this.lblCardType.Location = new System.Drawing.Point(30, 24);
			this.lblCardType.Name = "lblCardType";
			this.lblCardType.Size = new System.Drawing.Size(29, 12);
			this.lblCardType.TabIndex = 22;
			this.lblCardType.Text = "型号";
			this.lblColorMode.AutoSize = true;
			this.lblColorMode.Location = new System.Drawing.Point(30, 234);
			this.lblColorMode.Name = "lblColorMode";
			this.lblColorMode.Size = new System.Drawing.Size(53, 12);
			this.lblColorMode.TabIndex = 27;
			this.lblColorMode.Text = "色彩模式";
			this.lblOEPolarity.AutoSize = true;
			this.lblOEPolarity.Location = new System.Drawing.Point(30, 150);
			this.lblOEPolarity.Name = "lblOEPolarity";
			this.lblOEPolarity.Size = new System.Drawing.Size(41, 12);
			this.lblOEPolarity.TabIndex = 25;
			this.lblOEPolarity.Text = "OE极性";
			this.txtHeight.Location = new System.Drawing.Point(126, 105);
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(129, 21);
			this.txtHeight.TabIndex = 32;
			this.txtOEPolarity.Location = new System.Drawing.Point(126, 147);
			this.txtOEPolarity.Name = "txtOEPolarity";
			this.txtOEPolarity.Size = new System.Drawing.Size(129, 21);
			this.txtOEPolarity.TabIndex = 30;
			this.txtDataPolarity.Location = new System.Drawing.Point(126, 189);
			this.txtDataPolarity.Name = "txtDataPolarity";
			this.txtDataPolarity.Size = new System.Drawing.Size(129, 21);
			this.txtDataPolarity.TabIndex = 29;
			this.txtWidth.Location = new System.Drawing.Point(126, 63);
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.Size = new System.Drawing.Size(129, 21);
			this.txtWidth.TabIndex = 31;
			this.lblDataPolarity.AutoSize = true;
			this.lblDataPolarity.Location = new System.Drawing.Point(30, 192);
			this.lblDataPolarity.Name = "lblDataPolarity";
			this.lblDataPolarity.Size = new System.Drawing.Size(53, 12);
			this.lblDataPolarity.TabIndex = 26;
			this.lblDataPolarity.Text = "数据极性";
			this.lblHeight.AutoSize = true;
			this.lblHeight.Location = new System.Drawing.Point(30, 108);
			this.lblHeight.Name = "lblHeight";
			this.lblHeight.Size = new System.Drawing.Size(29, 12);
			this.lblHeight.TabIndex = 24;
			this.lblHeight.Text = "高度";
			this.lblWidth.AutoSize = true;
			this.lblWidth.Location = new System.Drawing.Point(30, 66);
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.Size = new System.Drawing.Size(29, 12);
			this.lblWidth.TabIndex = 23;
			this.lblWidth.Text = "宽度";
			this.picLoading.Image = Resources.loading;
			this.picLoading.Location = new System.Drawing.Point(111, 182);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(63, 58);
			this.picLoading.SizeMode = PictureBoxSizeMode.StretchImage;
			this.picLoading.TabIndex = 36;
			this.picLoading.TabStop = false;
			this.picColorMode.BackColor = System.Drawing.SystemColors.Control;
			this.picColorMode.BorderStyle = BorderStyle.FixedSingle;
			this.picColorMode.Location = new System.Drawing.Point(126, 230);
			this.picColorMode.Name = "picColorMode";
			this.picColorMode.Size = new System.Drawing.Size(129, 22);
			this.picColorMode.TabIndex = 37;
			this.picColorMode.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(284, 422);
			base.Controls.Add(this.picLoading);
			base.Controls.Add(this.picColorMode);
			base.Controls.Add(this.lblRouting);
			base.Controls.Add(this.txtRouting);
			base.Controls.Add(this.txtCardType);
			base.Controls.Add(this.lblCardType);
			base.Controls.Add(this.lblColorMode);
			base.Controls.Add(this.lblOEPolarity);
			base.Controls.Add(this.txtHeight);
			base.Controls.Add(this.txtOEPolarity);
			base.Controls.Add(this.txtDataPolarity);
			base.Controls.Add(this.txtWidth);
			base.Controls.Add(this.lblDataPolarity);
			base.Controls.Add(this.lblHeight);
			base.Controls.Add(this.lblWidth);
			base.Controls.Add(this.btnClose);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudTerminalParameter";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "终端信息";
			base.FormClosing += new FormClosingEventHandler(this.formCloudTerminalParameter_FormClosing);
			base.Load += new EventHandler(this.formCloudTerminalParameter_Load);
			((ISupportInitialize)this.picLoading).EndInit();
			((ISupportInitialize)this.picColorMode).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
