using LedCommunication;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formWifi : Form
	{
		private static string formID = "formWifi";

		private static string saveFilename = "zh-w.zhw";

		private IContainer components;

		private Button button1;

		private FolderBrowserDialog folderBrowserDialog1;

		private Label label_Info_OffOn;

		private Label label_Info_Luminance;

		private Label label_Info_Scantype;

		private Label label_info_Panel;

		private Label label4_OffOn;

		private Label label1_PanelParam;

		private Label label3_Lumiance;

		private Label label2_Scantype;

		private Button button_Cancel;

		private TextBox textBox1;

		private Button button2;

		private Label label1;

		public static string FormID
		{
			get
			{
				return formWifi.formID;
			}
			set
			{
				formWifi.formID = value;
			}
		}

		public formWifi()
		{
			this.InitializeComponent();
			formMain.ML.NowFormID = formWifi.formID;
			this.Text = formMain.ML.GetStr("formWifi_FormText");
			this.label1_PanelParam.Text = formMain.ML.GetStr("formWifi_label_PanelParam");
			this.label2_Scantype.Text = formMain.ML.GetStr("formWifi_label_Scantype");
			this.label3_Lumiance.Text = formMain.ML.GetStr("formWifi_label_Lumiance");
			this.label4_OffOn.Text = formMain.ML.GetStr("formWifi_label_OFFandON");
			this.label1.Text = formMain.ML.GetStr("formWifi_label_SavePath");
			this.button1.Text = formMain.ML.GetStr("formWifi_button_confirm");
			this.button_Cancel.Text = formMain.ML.GetStr("formWifi_button_Cancel");
		}

		private void formWifi_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.label_info_Panel.Text = formUSBWrite.GetPanelParamString(formMain.ledsys.SelectedPanel);
			this.label_Info_Scantype.Text = "1/" + formMain.ledsys.SelectedPanel.RoutingSetting.ScanTypeIndex.ToString() + "," + formRouting.GetRoutingString(formMain.ledsys.SelectedPanel.RoutingSetting.ScanTypeIndex, formMain.ledsys.SelectedPanel.RoutingSetting.RoutingIndex);
			this.label_Info_Luminance.Text = formUSBWrite.GetLuminanceString(formMain.ledsys.SelectedPanel);
			if (formMain.ledsys.SelectedPanel.TimerSwitch.Enabled)
			{
				this.label_Info_OffOn.Text = formMain.ML.GetStr("Display_Enable");
				return;
			}
			this.label_Info_OffOn.Text = formMain.ML.GetStr("Display_Disable");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.textBox1.Text != "")
			{
				this.saveSinglePanelWifiData(this.textBox1.Text + "\\" + formWifi.saveFilename);
				return;
			}
			MessageBox.Show(formMain.ML.GetStr("NETCARD_message_Choose_Save_Path"));
		}

		private void saveSinglePanelWifiData(string pSavePath)
		{
			LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
			if (selectedPanel == null)
			{
				MessageBox.Show(formMain.ML.GetStr("NETCARD_message_Save_Failed"));
				return;
			}
			ProcessWiFi processWiFi = new ProcessWiFi();
			processWiFi.PanelBytes = selectedPanel.ToBytes();
			processWiFi.TimerSwitchBytes = selectedPanel.TimerSwitch.ToBytes();
			processWiFi.LuminanceBytes = selectedPanel.Luminance.ToBytes();
			processWiFi.BmpDataBytes = selectedPanel.ToItemBmpDataBytes();
			processWiFi.ItemBytes = selectedPanel.ToItemBytes();
			protocol_data_integration protocol_data_integration = new protocol_data_integration();
			byte[] array = protocol_data_integration.WritingData_WIFI_Pack(processWiFi);
			if (array != null)
			{
				FileStream fileStream = new FileStream(pSavePath, FileMode.Create);
				fileStream.Write(array, 0, array.Length);
				fileStream.Close();
				MessageBox.Show(formMain.ML.GetStr("NETCARD_message_Save_Successed"));
				base.Close();
				return;
			}
			MessageBox.Show(formMain.ML.GetStr("NETCARD_message_Save_Failed"));
		}

		private void button_Cancel_Click(object sender, EventArgs e)
		{
			base.Dispose();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				{
					this.textBox1.Text = this.folderBrowserDialog1.SelectedPath;
				}
			}
			catch
			{
				this.folderBrowserDialog1.SelectedPath = formMain.DesktopPath;
				if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				{
					this.textBox1.Text = this.folderBrowserDialog1.SelectedPath;
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
			this.button1 = new Button();
			this.folderBrowserDialog1 = new FolderBrowserDialog();
			this.label_Info_OffOn = new Label();
			this.label_Info_Luminance = new Label();
			this.label_Info_Scantype = new Label();
			this.label_info_Panel = new Label();
			this.label4_OffOn = new Label();
			this.label1_PanelParam = new Label();
			this.label3_Lumiance = new Label();
			this.label2_Scantype = new Label();
			this.button_Cancel = new Button();
			this.textBox1 = new TextBox();
			this.button2 = new Button();
			this.label1 = new Label();
			base.SuspendLayout();
			this.button1.Location = new System.Drawing.Point(152, 274);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label_Info_OffOn.Location = new System.Drawing.Point(12, 198);
			this.label_Info_OffOn.Name = "label_Info_OffOn";
			this.label_Info_OffOn.Size = new System.Drawing.Size(307, 12);
			this.label_Info_OffOn.TabIndex = 30;
			this.label_Info_Luminance.Location = new System.Drawing.Point(12, 149);
			this.label_Info_Luminance.Name = "label_Info_Luminance";
			this.label_Info_Luminance.Size = new System.Drawing.Size(344, 12);
			this.label_Info_Luminance.TabIndex = 29;
			this.label_Info_Scantype.Location = new System.Drawing.Point(12, 101);
			this.label_Info_Scantype.Name = "label_Info_Scantype";
			this.label_Info_Scantype.Size = new System.Drawing.Size(344, 12);
			this.label_Info_Scantype.TabIndex = 28;
			this.label_info_Panel.Location = new System.Drawing.Point(12, 28);
			this.label_info_Panel.Name = "label_info_Panel";
			this.label_info_Panel.Size = new System.Drawing.Size(344, 39);
			this.label_info_Panel.TabIndex = 27;
			this.label4_OffOn.AutoSize = true;
			this.label4_OffOn.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label4_OffOn.Location = new System.Drawing.Point(12, 177);
			this.label4_OffOn.Name = "label4_OffOn";
			this.label4_OffOn.Size = new System.Drawing.Size(70, 12);
			this.label4_OffOn.TabIndex = 26;
			this.label4_OffOn.Text = "自动开关机";
			this.label1_PanelParam.AutoSize = true;
			this.label1_PanelParam.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label1_PanelParam.Location = new System.Drawing.Point(12, 9);
			this.label1_PanelParam.Name = "label1_PanelParam";
			this.label1_PanelParam.Size = new System.Drawing.Size(57, 12);
			this.label1_PanelParam.TabIndex = 23;
			this.label1_PanelParam.Text = "基本屏参";
			this.label3_Lumiance.AutoSize = true;
			this.label3_Lumiance.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label3_Lumiance.Location = new System.Drawing.Point(12, 130);
			this.label3_Lumiance.Name = "label3_Lumiance";
			this.label3_Lumiance.Size = new System.Drawing.Size(57, 12);
			this.label3_Lumiance.TabIndex = 25;
			this.label3_Lumiance.Text = "亮度调节";
			this.label2_Scantype.AutoSize = true;
			this.label2_Scantype.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label2_Scantype.Location = new System.Drawing.Point(12, 82);
			this.label2_Scantype.Name = "label2_Scantype";
			this.label2_Scantype.Size = new System.Drawing.Size(57, 12);
			this.label2_Scantype.TabIndex = 24;
			this.label2_Scantype.Text = "扫描方式";
			this.button_Cancel.Location = new System.Drawing.Point(264, 274);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.button_Cancel.TabIndex = 31;
			this.button_Cancel.Text = "取消";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new EventHandler(this.button_Cancel_Click);
			this.textBox1.Location = new System.Drawing.Point(94, 231);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(321, 21);
			this.textBox1.TabIndex = 32;
			this.button2.Location = new System.Drawing.Point(421, 231);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(42, 23);
			this.button2.TabIndex = 33;
			this.button2.Text = "...";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label1.Location = new System.Drawing.Point(12, 234);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 12);
			this.label1.TabIndex = 34;
			this.label1.Text = "保存路径";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(491, 319);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.button_Cancel);
			base.Controls.Add(this.label_Info_OffOn);
			base.Controls.Add(this.label_Info_Luminance);
			base.Controls.Add(this.label_Info_Scantype);
			base.Controls.Add(this.label_info_Panel);
			base.Controls.Add(this.label4_OffOn);
			base.Controls.Add(this.label1_PanelParam);
			base.Controls.Add(this.label3_Lumiance);
			base.Controls.Add(this.label2_Scantype);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formWifi";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "写入WIFI电脑数据";
			base.Load += new EventHandler(this.formWifi_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
