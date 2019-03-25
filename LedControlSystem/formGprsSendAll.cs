using LedCommunication;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Const;
using LedModel.Content;
using LedModel.Data;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace LedControlSystem.LedControlSystem
{
	public class formGprsSendAll : Form
	{
		private static string formID = "formGprsSendAll";

		private formGPRS_Send gprsSenderForm;

		private IList<GprsContentInfo> panelList;

		private static System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(100, 100);

		private static System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(formGprsSendAll.bitmap);

		private static int[] sizeList = new int[208];

		private static LedDText textList = new LedDText();

		private static LedPanel panel = new LedPanel();

		private bool isThread;

		private bool isSending;

		private IContainer components;

		private ListBox listBox1;

		private TextEditor textEditor1;

		private Button button_SendAll;

		private StatusStrip statusStrip1;

		private ToolStripProgressBar toolStripProgressBar1;

		private System.Windows.Forms.Timer timer1;

		private ComboBox effectEntrySpeed;

		private Label effectLabelEntrySpeed;

		public static string FormID
		{
			get
			{
				return formGprsSendAll.formID;
			}
			set
			{
				formGprsSendAll.formID = value;
			}
		}

		public formGprsSendAll()
		{
			this.InitializeComponent();
		}

		public formGprsSendAll(IList<GprsContentInfo> pPanelList, formGPRS_Send pFormSender)
		{
			this.InitializeComponent();
			this.panelList = pPanelList;
			this.gprsSenderForm = pFormSender;
			this.Text = formMain.ML.GetStr("formGprsSendAll_FormText");
			this.button_SendAll.Text = formMain.ML.GetStr("formGprsSendAll_button_SendStart");
			this.effectLabelEntrySpeed.Text = formMain.ML.GetStr("formGprsSendAll_effectLabelEntrySpeed");
		}

		private void formGprsSendAll_Load(object sender, EventArgs e)
		{
			this.toolStripProgressBar1.Visible = false;
			this.isThread = false;
			string ppXmlPath = Application.StartupPath + "\\GprsAllSend.xml";
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.loadGprsHistory(ppXmlPath);
			this.textEditor1.UpdateEvent += new LedGlobal.LedContentEvent(this.textEditor1_UpdateEvent);
			this.textEditor1.Edit(formGprsSendAll.textList, formGprsSendAll.panel);
			this.loadPanelToList();
		}

		private void loadPanelToList()
		{
			this.listBox1.Items.Clear();
			for (int i = 0; i < this.panelList.Count; i++)
			{
				this.listBox1.Items.Add(string.Concat(new string[]
				{
					this.panelList[i].TerminalCode,
					"-",
					this.panelList[i].Status,
					"-(",
					this.panelList[i].Description,
					")"
				}));
			}
		}

		private void textEditor1_UpdateEvent(LedContentEventType type, object sender)
		{
			base.Enabled = false;
		}

		private void button_SendAll_Click(object sender, EventArgs e)
		{
			this.isThread = true;
			new Thread(new ThreadStart(this.SendAll))
			{
				IsBackground = true
			}.Start();
		}

		public void SendAll()
		{
			this.isSending = true;
			base.Invoke(new MethodInvoker(delegate
			{
				this.button_SendAll.Enabled = false;
				this.toolStripProgressBar1.Visible = true;
				this.toolStripProgressBar1.Value = 0;
			}));
			Thread.Sleep(600);
			if (this.isThread)
			{
				this.updateSizeList();
			}
			if (this.isThread)
			{
				this.startSendAll();
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.toolStripProgressBar1.Value = 100;
				this.toolStripProgressBar1.Visible = false;
				this.button_SendAll.Enabled = true;
			}));
			Thread.Sleep(500);
			this.isSending = false;
		}

		private void startSendAll()
		{
			int i;
			for (i = 0; i < this.panelList.Count; i++)
			{
				if (!this.isThread)
				{
					return;
				}
				this.SendSingle(this.panelList[i]);
				base.Invoke(new MethodInvoker(delegate
				{
					this.toolStripProgressBar1.Value = (i + 1) / this.panelList.Count * 100;
				}));
				Thread.Sleep(600);
			}
		}

		private void loadGprsHistory(string ppXmlPath)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				if (!File.Exists(ppXmlPath))
				{
					formGprsSendAll.textList = new LedDText();
				}
				else
				{
					xmlDocument.Load(ppXmlPath);
					XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/GPRSHistory/GPRSText");
					for (int i = 0; i < xmlNodeList.Count; i++)
					{
						formGprsSendAll.textList = new LedDText();
						formGprsSendAll.textList.EDText.Text = xmlNodeList[i].Attributes["Text"].Value.ToString();
						formGprsSendAll.textList.EDText.Font.FamilyName = xmlNodeList[i].Attributes["Font"].Value.ToString();
						formGprsSendAll.textList.EDText.Font.Bold = bool.Parse(xmlNodeList[i].Attributes["IsBold"].Value.ToString());
						formGprsSendAll.textList.EDText.Font.Italic = bool.Parse(xmlNodeList[i].Attributes["IsItalic"].Value.ToString());
						formGprsSendAll.textList.EDText.Font.Underline = bool.Parse(xmlNodeList[i].Attributes["IsUnderline"].Value.ToString());
						formGprsSendAll.textList.EffectsSetting.EntrySpeed = (byte)int.Parse(xmlNodeList[i].Attributes["EntrySpeed"].Value.ToString());
						this.effectEntrySpeed.Text = xmlNodeList[i].Attributes["EntrySpeed"].Value.ToString();
					}
				}
			}
			catch
			{
				formGprsSendAll.textList = new LedDText();
			}
		}

		private void saveGprsHistory(string pXmlPath)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement("GPRSHistory");
				xmlDocument.AppendChild(xmlElement);
				XmlElement xmlElement2 = xmlDocument.CreateElement("GPRSText");
				xmlElement2.SetAttribute("Font", formGprsSendAll.textList.EDText.Font.FamilyName);
				xmlElement2.SetAttribute("Text", formGprsSendAll.textList.EDText.Text);
				xmlElement2.SetAttribute("Color", formGprsSendAll.textList.EDText.ForeColor.Name);
				xmlElement2.SetAttribute("IsBold", formGprsSendAll.textList.EDText.Font.Bold.ToString());
				xmlElement2.SetAttribute("IsItalic", formGprsSendAll.textList.EDText.Font.Italic.ToString());
				xmlElement2.SetAttribute("IsUnderline", formGprsSendAll.textList.EDText.Font.Underline.ToString());
				xmlElement2.SetAttribute("EntrySpeed", formGprsSendAll.textList.EffectsSetting.EntrySpeed.ToString());
				xmlElement.AppendChild(xmlElement2);
				xmlDocument.Save(pXmlPath);
			}
			catch
			{
			}
		}

		private void SendSingle(object pObj)
		{
			try
			{
				GprsContentInfo gprsContentInfo = (GprsContentInfo)pObj;
				gprsContentInfo.FontSize = formGprsSendAll.getFontSizeByHeight(gprsContentInfo.Height, 0.1f);
				gprsContentInfo.Content = formGprsSendAll.textList.EDText.Text;
				gprsContentInfo.ContentFont = formGprsSendAll.textList.EDText.Font.GetFont((float)gprsContentInfo.FontSize);
				byte[] bytes = GprsAdministrator.API_DownLoadRoutingData(gprsContentInfo.Id);
				LedPanel ledPanel = LedPanel.Parse(bytes);
				gprsContentInfo.LedModel = ledPanel.CardType.ToString().Replace("_", "-");
				gprsContentInfo.Width = ledPanel.Width;
				gprsContentInfo.Height = ledPanel.Height;
				gprsContentInfo.Panel = ledPanel;
				LedItem ledItem = new LedItem();
				ledItem.Edge = formMain.NewEdgeData();
				LedDText ledDText = new LedDText();
				ledDText.EffectsSetting.EntryMode = 3;
				ledDText.EffectsSetting.EntrySpeed = formGprsSendAll.textList.EffectsSetting.EntrySpeed;
				ledDText.Edge = formMain.NewEdgeData();
				ledDText.EDText.Text = gprsContentInfo.Content;
				ledDText.EDText.ForeColor = LedColorConst.Red;
				ledDText.EDText.Font = new LedFont(gprsContentInfo.ContentFont);
				ledDText.EDText.Kerning = formGprsSendAll.textList.EDText.Kerning;
				ledDText.DrawMode = LedDrawMode.Full;
				System.Drawing.Size size = new System.Drawing.Size(gprsContentInfo.Width, gprsContentInfo.Height);
				ledItem.AddSubarea(new LedSubarea(0, 0, size.Width, size.Height, ledDText)
				{
					Type = LedSubareaType.PictureText
				});
				ledPanel.AddItem(ledItem);
				ledDText.PreviewDraw();
				gprsContentInfo.BitmapList = ledDText.BmpList;
				Process process = new Process();
				process.PanelBytes = ledPanel.ToBytes();
				process.BmpDataBytes = ledPanel.ToItemBmpDataBytes();
				process.ItemBytes = ledPanel.ToItemBytes();
				IList<byte> list = new List<byte>();
				IList<byte[]> list2 = protocol_GPRS.Send_Pack_GPRS(formMain.ledsys.SelectedPanel.CardAddress, 0, LedCmdType.Send_Begin, process, formMain.ledsys.SelectedPanel.ProtocolVersion);
				if (list2 != null && list2.Count > 0)
				{
					for (int i = 0; i < list2.Count; i++)
					{
						for (int j = 0; j < list2[i].Length; j++)
						{
							list.Add(list2[i][j]);
						}
					}
				}
				byte[] array = new byte[list.Count];
				list.CopyTo(array, 0);
				string text = Application.StartupPath + "\\" + gprsContentInfo.TerminalCode + "GPRS.zhd";
				FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write);
				fileStream.Write(array, 0, array.Length);
				fileStream.Close();
				GprsAdministrator.API_UPLoadFile2(gprsContentInfo.Id, text, 1052);
				this.gprsSenderForm.UpdateSendPanel(gprsContentInfo.Id);
				gprsContentInfo.Status = "已上传";
				this.removepanel(gprsContentInfo);
			}
			catch
			{
			}
		}

		public void removepanel(GprsContentInfo pPanel)
		{
			this.loadPanelToList();
		}

		private static int getFontSizeByHeight(int pHeight, float pPlus)
		{
			int num = pHeight + (int)(pPlus * (float)pHeight);
			for (int i = 0; i < 200; i++)
			{
				if (i == 199)
				{
					return i + 8;
				}
				if (num >= formGprsSendAll.sizeList[i + 8] && num < formGprsSendAll.sizeList[i + 9])
				{
					return i + 8;
				}
			}
			return 8;
		}

		public int getHeightByFotSize(int pSize)
		{
			return (int)formGprsSendAll.g.MeasureString("国", new System.Drawing.Font(formGprsSendAll.textList.EDText.Font.FamilyName, (float)pSize)).Height;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.updateSizeList();
		}

		private void updateSizeList()
		{
			for (int i = 0; i < 200; i++)
			{
				formGprsSendAll.sizeList[i + 8] = this.getHeightByFotSize(i + 8);
			}
		}

		private void formGprsSendAll_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.isSending)
			{
				e.Cancel = true;
				return;
			}
			this.isThread = false;
			string pXmlPath = Application.StartupPath + "\\GprsAllSend.xml";
			this.saveGprsHistory(pXmlPath);
		}

		private void effectEntrySpeed_SelectedIndexChanged(object sender, EventArgs e)
		{
			formGprsSendAll.textList.EffectsSetting.EntrySpeed = (byte)int.Parse(this.effectEntrySpeed.Text);
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
			this.listBox1 = new ListBox();
			this.button_SendAll = new Button();
			this.statusStrip1 = new StatusStrip();
			this.toolStripProgressBar1 = new ToolStripProgressBar();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.effectEntrySpeed = new ComboBox();
			this.effectLabelEntrySpeed = new Label();
			this.textEditor1 = new TextEditor();
			this.statusStrip1.SuspendLayout();
			base.SuspendLayout();
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(1, 1);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(239, 196);
			this.listBox1.TabIndex = 0;
			this.button_SendAll.Location = new System.Drawing.Point(734, 172);
			this.button_SendAll.Name = "button_SendAll";
			this.button_SendAll.Size = new System.Drawing.Size(75, 23);
			this.button_SendAll.TabIndex = 2;
			this.button_SendAll.Text = "开始发送";
			this.button_SendAll.UseVisualStyleBackColor = true;
			this.button_SendAll.Click += new EventHandler(this.button_SendAll_Click);
			this.statusStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripProgressBar1
			});
			this.statusStrip1.Location = new System.Drawing.Point(0, 200);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(821, 22);
			this.statusStrip1.TabIndex = 6;
			this.statusStrip1.Text = "statusStrip1";
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(800, 16);
			this.effectEntrySpeed.FormattingEnabled = true;
			this.effectEntrySpeed.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15",
				"16",
				"17",
				"18",
				"19",
				"20",
				"21",
				"22",
				"23",
				"24",
				"25",
				"26",
				"27",
				"28",
				"29",
				"30",
				"31",
				"32"
			});
			this.effectEntrySpeed.Location = new System.Drawing.Point(288, 174);
			this.effectEntrySpeed.Name = "effectEntrySpeed";
			this.effectEntrySpeed.Size = new System.Drawing.Size(80, 20);
			this.effectEntrySpeed.TabIndex = 10;
			this.effectEntrySpeed.SelectedIndexChanged += new EventHandler(this.effectEntrySpeed_SelectedIndexChanged);
			this.effectLabelEntrySpeed.AutoSize = true;
			this.effectLabelEntrySpeed.Location = new System.Drawing.Point(244, 177);
			this.effectLabelEntrySpeed.Name = "effectLabelEntrySpeed";
			this.effectLabelEntrySpeed.Size = new System.Drawing.Size(35, 12);
			this.effectLabelEntrySpeed.TabIndex = 9;
			this.effectLabelEntrySpeed.Text = "速度:";
			this.textEditor1.BackColor = System.Drawing.Color.Transparent;
			this.textEditor1.Location = new System.Drawing.Point(246, 1);
			this.textEditor1.Name = "textEditor1";
			this.textEditor1.Size = new System.Drawing.Size(575, 167);
			this.textEditor1.TabIndex = 1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(821, 222);
			base.Controls.Add(this.effectEntrySpeed);
			base.Controls.Add(this.effectLabelEntrySpeed);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.button_SendAll);
			base.Controls.Add(this.textEditor1);
			base.Controls.Add(this.listBox1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formGprsSendAll";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "GPRS群组发送";
			base.FormClosing += new FormClosingEventHandler(this.formGprsSendAll_FormClosing);
			base.Load += new EventHandler(this.formGprsSendAll_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
