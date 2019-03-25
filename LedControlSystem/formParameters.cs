using LedControlSystem.Properties;
using LedModel;
using LedModel.Enum;
using LedService.Const;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem
{
	public class formParameters : Form
	{
		private IContainer components;

		private TabControl tabModule;

		private TabPage tpCard;

		private TextBox txtCardCloudServerAddress;

		private Label lblCardCloudServerAccessMode;

		private RadioButton rdoRelease;

		private RadioButton rdoDebug;

		private Label lblRunMode;

		private RadioButton rdoCardIP;

		private RadioButton rdoCardDomain;

		private Label lblCardCloudServerAddress;

		private TabPage tpCloud;

		private TextBox txtWeatherServerIPAddress;

		private Label lblWeatherServerIPAddress;

		private TextBox txtCloudServerIPAddress;

		private Label lblCloudServerIPAddress;

		private GroupBox grpBasic;

		private Label lblWiFiProductionTest;

		private RadioButton rdoWiFiProductionTestOpen;

		private RadioButton rdoWiFiProductionTestClose;

		private GroupBox grbWiFiProductionTest;

		public formParameters()
		{
			this.InitializeComponent();
		}

		private void formParameters_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			switch (Settings.Default.RunMode)
			{
			case 0:
				this.rdoDebug.Checked = true;
				this.rdoRelease.Checked = false;
				break;
			case 1:
				this.rdoDebug.Checked = false;
				this.rdoRelease.Checked = true;
				break;
			default:
				this.rdoDebug.Checked = false;
				this.rdoRelease.Checked = true;
				break;
			}
			if (LedGlobal.CloudServerAccessMode == LedCloudServerAccessMode.Domain)
			{
				this.rdoCardDomain.Checked = true;
				this.rdoCardIP.Checked = false;
				this.txtCardCloudServerAddress.Text = LedGlobal.CloudServerDomainName;
			}
			else
			{
				this.rdoCardDomain.Checked = false;
				this.rdoCardIP.Checked = true;
				this.txtCardCloudServerAddress.Text = LedGlobal.CloudServerIPAddress;
			}
			this.txtCloudServerIPAddress.Text = CommonConst.CloudServer;
			this.txtWeatherServerIPAddress.Text = CommonConst.WeatherServer;
			bool wiFiProductionTest = Settings.Default.WiFiProductionTest;
			this.rdoWiFiProductionTestOpen.Checked = wiFiProductionTest;
			this.rdoWiFiProductionTestClose.Checked = !wiFiProductionTest;
		}

		private void formParameters_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.rdoDebug.Checked)
			{
				Settings.Default.RunMode = 0;
			}
			else
			{
				Settings.Default.RunMode = 1;
			}
			Settings.Default.WiFiProductionTest = this.rdoWiFiProductionTestOpen.Checked;
			Settings.Default.Save();
		}

		private void rdoRelease_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			this.rdoCardDomain.Checked = true;
			this.rdoCardIP.Checked = false;
			LedGlobal.CloudServerAccessMode = LedCloudServerAccessMode.Domain;
			LedGlobal.CloudServerIPAddress = string.Empty;
			LedGlobal.CloudServerDomainName = "shc.zhonghangled.com";
			this.txtCardCloudServerAddress.Text = LedGlobal.CloudServerDomainName;
			CommonConst.CloudServer = "http://shc.zhonghangled.com";
			CommonConst.WeatherServer = "http://shc.zhonghangled.com";
			CommonConst.Weather_Forecast7d = "/zcc/api/1.0/weather/forecast7d";
			CommonConst.Weather_Observe = "/zcc/api/1.0/weather/observe";
			CommonConst.Weather_Weatherindex = "/zcc/api/1.0/weather/weatherindex";
			CommonConst.Weather_Air = "/zcc/api/1.0/weather/air";
			CommonConst.Weather_AQI = "/zcc/api/1.0/weather/aqi";
			CommonConst.Weather_Alarminfo = "/zcc/api/1.0/weather/alarminfo";
			CommonConst.Weather_Citylist = "/zcc/api/1.0/weather/cities";
			this.txtCloudServerIPAddress.Text = CommonConst.CloudServer;
			this.txtWeatherServerIPAddress.Text = CommonConst.WeatherServer;
		}

		private void rdoDebug_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			this.rdoCardDomain.Checked = false;
			this.rdoCardIP.Checked = true;
			LedGlobal.CloudServerAccessMode = LedCloudServerAccessMode.Ip;
			LedGlobal.CloudServerIPAddress = "123.56.112.5";
			LedGlobal.CloudServerDomainName = string.Empty;
			this.txtCardCloudServerAddress.Text = LedGlobal.CloudServerIPAddress;
			CommonConst.CloudServer = "http://123.56.112.5";
			CommonConst.WeatherServer = "http://123.56.112.5";
			CommonConst.Weather_Forecast7d = "/zcc/api/1.0/weather/forecast7d";
			CommonConst.Weather_Observe = "/zcc/api/1.0/weather/observe";
			CommonConst.Weather_Weatherindex = "/zcc/api/1.0/weather/weatherindex";
			CommonConst.Weather_Air = "/zcc/api/1.0/weather/air";
			CommonConst.Weather_AQI = "/zcc/api/1.0/weather/aqi";
			CommonConst.Weather_Alarminfo = "/zcc/api/1.0/weather/alarminfo";
			CommonConst.Weather_Citylist = "/zcc/api/1.0/weather/cities";
			this.txtCloudServerIPAddress.Text = CommonConst.CloudServer;
			this.txtWeatherServerIPAddress.Text = CommonConst.WeatherServer;
		}

		private void rdoWiFiProductionTestOpen_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			LedGlobal.IsWiFiProductionTest = radioButton.Checked;
		}

		private void rdoWiFiProductionTestClose_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			LedGlobal.IsWiFiProductionTest = !radioButton.Checked;
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
			this.tabModule = new TabControl();
			this.tpCard = new TabPage();
			this.rdoCardIP = new RadioButton();
			this.rdoCardDomain = new RadioButton();
			this.txtCardCloudServerAddress = new TextBox();
			this.lblCardCloudServerAddress = new Label();
			this.lblCardCloudServerAccessMode = new Label();
			this.tpCloud = new TabPage();
			this.txtWeatherServerIPAddress = new TextBox();
			this.lblWeatherServerIPAddress = new Label();
			this.txtCloudServerIPAddress = new TextBox();
			this.lblCloudServerIPAddress = new Label();
			this.rdoRelease = new RadioButton();
			this.rdoDebug = new RadioButton();
			this.lblRunMode = new Label();
			this.grpBasic = new GroupBox();
			this.lblWiFiProductionTest = new Label();
			this.rdoWiFiProductionTestOpen = new RadioButton();
			this.rdoWiFiProductionTestClose = new RadioButton();
			this.grbWiFiProductionTest = new GroupBox();
			this.tabModule.SuspendLayout();
			this.tpCard.SuspendLayout();
			this.tpCloud.SuspendLayout();
			this.grpBasic.SuspendLayout();
			this.grbWiFiProductionTest.SuspendLayout();
			base.SuspendLayout();
			this.tabModule.Controls.Add(this.tpCard);
			this.tabModule.Controls.Add(this.tpCloud);
			this.tabModule.Location = new System.Drawing.Point(2, 53);
			this.tabModule.Name = "tabModule";
			this.tabModule.SelectedIndex = 0;
			this.tabModule.Size = new System.Drawing.Size(328, 141);
			this.tabModule.TabIndex = 0;
			this.tpCard.Controls.Add(this.rdoCardIP);
			this.tpCard.Controls.Add(this.rdoCardDomain);
			this.tpCard.Controls.Add(this.txtCardCloudServerAddress);
			this.tpCard.Controls.Add(this.lblCardCloudServerAddress);
			this.tpCard.Controls.Add(this.lblCardCloudServerAccessMode);
			this.tpCard.Location = new System.Drawing.Point(4, 22);
			this.tpCard.Name = "tpCard";
			this.tpCard.Padding = new Padding(3);
			this.tpCard.Size = new System.Drawing.Size(320, 115);
			this.tpCard.TabIndex = 0;
			this.tpCard.Text = "控制卡参数";
			this.tpCard.UseVisualStyleBackColor = true;
			this.rdoCardIP.AutoSize = true;
			this.rdoCardIP.Enabled = false;
			this.rdoCardIP.Location = new System.Drawing.Point(221, 20);
			this.rdoCardIP.Name = "rdoCardIP";
			this.rdoCardIP.Size = new System.Drawing.Size(35, 16);
			this.rdoCardIP.TabIndex = 3;
			this.rdoCardIP.TabStop = true;
			this.rdoCardIP.Text = "IP";
			this.rdoCardIP.UseVisualStyleBackColor = true;
			this.rdoCardDomain.AutoSize = true;
			this.rdoCardDomain.Enabled = false;
			this.rdoCardDomain.Location = new System.Drawing.Point(146, 20);
			this.rdoCardDomain.Name = "rdoCardDomain";
			this.rdoCardDomain.Size = new System.Drawing.Size(47, 16);
			this.rdoCardDomain.TabIndex = 2;
			this.rdoCardDomain.TabStop = true;
			this.rdoCardDomain.Text = "域名";
			this.rdoCardDomain.UseVisualStyleBackColor = true;
			this.txtCardCloudServerAddress.Location = new System.Drawing.Point(146, 55);
			this.txtCardCloudServerAddress.Name = "txtCardCloudServerAddress";
			this.txtCardCloudServerAddress.ReadOnly = true;
			this.txtCardCloudServerAddress.Size = new System.Drawing.Size(152, 21);
			this.txtCardCloudServerAddress.TabIndex = 1;
			this.lblCardCloudServerAddress.AutoSize = true;
			this.lblCardCloudServerAddress.Location = new System.Drawing.Point(21, 58);
			this.lblCardCloudServerAddress.Name = "lblCardCloudServerAddress";
			this.lblCardCloudServerAddress.Size = new System.Drawing.Size(89, 12);
			this.lblCardCloudServerAddress.TabIndex = 0;
			this.lblCardCloudServerAddress.Text = "云服务器地址：";
			this.lblCardCloudServerAccessMode.AutoSize = true;
			this.lblCardCloudServerAccessMode.Location = new System.Drawing.Point(21, 22);
			this.lblCardCloudServerAccessMode.Name = "lblCardCloudServerAccessMode";
			this.lblCardCloudServerAccessMode.Size = new System.Drawing.Size(101, 12);
			this.lblCardCloudServerAccessMode.TabIndex = 0;
			this.lblCardCloudServerAccessMode.Text = "云服务访问模式：";
			this.tpCloud.Controls.Add(this.txtWeatherServerIPAddress);
			this.tpCloud.Controls.Add(this.lblWeatherServerIPAddress);
			this.tpCloud.Controls.Add(this.txtCloudServerIPAddress);
			this.tpCloud.Controls.Add(this.lblCloudServerIPAddress);
			this.tpCloud.Location = new System.Drawing.Point(4, 22);
			this.tpCloud.Name = "tpCloud";
			this.tpCloud.Size = new System.Drawing.Size(320, 115);
			this.tpCloud.TabIndex = 1;
			this.tpCloud.Text = "云服务参数";
			this.tpCloud.UseVisualStyleBackColor = true;
			this.txtWeatherServerIPAddress.Location = new System.Drawing.Point(130, 56);
			this.txtWeatherServerIPAddress.Name = "txtWeatherServerIPAddress";
			this.txtWeatherServerIPAddress.ReadOnly = true;
			this.txtWeatherServerIPAddress.Size = new System.Drawing.Size(171, 21);
			this.txtWeatherServerIPAddress.TabIndex = 3;
			this.lblWeatherServerIPAddress.AutoSize = true;
			this.lblWeatherServerIPAddress.Location = new System.Drawing.Point(21, 59);
			this.lblWeatherServerIPAddress.Name = "lblWeatherServerIPAddress";
			this.lblWeatherServerIPAddress.Size = new System.Drawing.Size(101, 12);
			this.lblWeatherServerIPAddress.TabIndex = 2;
			this.lblWeatherServerIPAddress.Text = "天气服务IP地址：";
			this.txtCloudServerIPAddress.Location = new System.Drawing.Point(130, 19);
			this.txtCloudServerIPAddress.Name = "txtCloudServerIPAddress";
			this.txtCloudServerIPAddress.ReadOnly = true;
			this.txtCloudServerIPAddress.Size = new System.Drawing.Size(171, 21);
			this.txtCloudServerIPAddress.TabIndex = 1;
			this.lblCloudServerIPAddress.AutoSize = true;
			this.lblCloudServerIPAddress.Location = new System.Drawing.Point(21, 22);
			this.lblCloudServerIPAddress.Name = "lblCloudServerIPAddress";
			this.lblCloudServerIPAddress.Size = new System.Drawing.Size(89, 12);
			this.lblCloudServerIPAddress.TabIndex = 0;
			this.lblCloudServerIPAddress.Text = "云服务IP地址：";
			this.rdoRelease.AutoSize = true;
			this.rdoRelease.Checked = true;
			this.rdoRelease.Location = new System.Drawing.Point(86, 17);
			this.rdoRelease.Name = "rdoRelease";
			this.rdoRelease.Size = new System.Drawing.Size(71, 16);
			this.rdoRelease.TabIndex = 1;
			this.rdoRelease.TabStop = true;
			this.rdoRelease.Text = "发布模式";
			this.rdoRelease.UseVisualStyleBackColor = true;
			this.rdoRelease.CheckedChanged += new EventHandler(this.rdoRelease_CheckedChanged);
			this.rdoDebug.AutoSize = true;
			this.rdoDebug.Location = new System.Drawing.Point(176, 17);
			this.rdoDebug.Name = "rdoDebug";
			this.rdoDebug.Size = new System.Drawing.Size(71, 16);
			this.rdoDebug.TabIndex = 2;
			this.rdoDebug.Text = "测试模式";
			this.rdoDebug.UseVisualStyleBackColor = true;
			this.rdoDebug.CheckedChanged += new EventHandler(this.rdoDebug_CheckedChanged);
			this.lblRunMode.AutoSize = true;
			this.lblRunMode.Location = new System.Drawing.Point(6, 19);
			this.lblRunMode.Name = "lblRunMode";
			this.lblRunMode.Size = new System.Drawing.Size(65, 12);
			this.lblRunMode.TabIndex = 3;
			this.lblRunMode.Text = "运行模式：";
			this.grpBasic.Controls.Add(this.lblRunMode);
			this.grpBasic.Controls.Add(this.rdoRelease);
			this.grpBasic.Controls.Add(this.rdoDebug);
			this.grpBasic.Location = new System.Drawing.Point(2, 2);
			this.grpBasic.Name = "grpBasic";
			this.grpBasic.Size = new System.Drawing.Size(324, 45);
			this.grpBasic.TabIndex = 4;
			this.grpBasic.TabStop = false;
			this.lblWiFiProductionTest.AutoSize = true;
			this.lblWiFiProductionTest.Location = new System.Drawing.Point(6, 18);
			this.lblWiFiProductionTest.Name = "lblWiFiProductionTest";
			this.lblWiFiProductionTest.Size = new System.Drawing.Size(89, 12);
			this.lblWiFiProductionTest.TabIndex = 5;
			this.lblWiFiProductionTest.Text = "WiFi生产测试：";
			this.rdoWiFiProductionTestOpen.AutoSize = true;
			this.rdoWiFiProductionTestOpen.Location = new System.Drawing.Point(122, 16);
			this.rdoWiFiProductionTestOpen.Name = "rdoWiFiProductionTestOpen";
			this.rdoWiFiProductionTestOpen.Size = new System.Drawing.Size(47, 16);
			this.rdoWiFiProductionTestOpen.TabIndex = 6;
			this.rdoWiFiProductionTestOpen.TabStop = true;
			this.rdoWiFiProductionTestOpen.Text = "开启";
			this.rdoWiFiProductionTestOpen.UseVisualStyleBackColor = true;
			this.rdoWiFiProductionTestOpen.CheckedChanged += new EventHandler(this.rdoWiFiProductionTestOpen_CheckedChanged);
			this.rdoWiFiProductionTestClose.AutoSize = true;
			this.rdoWiFiProductionTestClose.Location = new System.Drawing.Point(200, 16);
			this.rdoWiFiProductionTestClose.Name = "rdoWiFiProductionTestClose";
			this.rdoWiFiProductionTestClose.Size = new System.Drawing.Size(47, 16);
			this.rdoWiFiProductionTestClose.TabIndex = 6;
			this.rdoWiFiProductionTestClose.TabStop = true;
			this.rdoWiFiProductionTestClose.Text = "关闭";
			this.rdoWiFiProductionTestClose.UseVisualStyleBackColor = true;
			this.rdoWiFiProductionTestClose.CheckedChanged += new EventHandler(this.rdoWiFiProductionTestClose_CheckedChanged);
			this.grbWiFiProductionTest.Controls.Add(this.lblWiFiProductionTest);
			this.grbWiFiProductionTest.Controls.Add(this.rdoWiFiProductionTestClose);
			this.grbWiFiProductionTest.Controls.Add(this.rdoWiFiProductionTestOpen);
			this.grbWiFiProductionTest.Location = new System.Drawing.Point(2, 195);
			this.grbWiFiProductionTest.Name = "grbWiFiProductionTest";
			this.grbWiFiProductionTest.Size = new System.Drawing.Size(325, 45);
			this.grbWiFiProductionTest.TabIndex = 7;
			this.grbWiFiProductionTest.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(329, 242);
			base.Controls.Add(this.grbWiFiProductionTest);
			base.Controls.Add(this.grpBasic);
			base.Controls.Add(this.tabModule);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formParameters";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "参数配置";
			base.FormClosing += new FormClosingEventHandler(this.formParameters_FormClosing);
			base.Load += new EventHandler(this.formParameters_Load);
			this.tabModule.ResumeLayout(false);
			this.tpCard.ResumeLayout(false);
			this.tpCard.PerformLayout();
			this.tpCloud.ResumeLayout(false);
			this.tpCloud.PerformLayout();
			this.grpBasic.ResumeLayout(false);
			this.grpBasic.PerformLayout();
			this.grbWiFiProductionTest.ResumeLayout(false);
			this.grbWiFiProductionTest.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
