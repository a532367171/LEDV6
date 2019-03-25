using AxShockwaveFlashObjects;
using LedControlSystem.Properties;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formStart : Form
	{
		public string formid = "formStart";

		public string value = "<invoke name=\"addText\" returntype=\"xml\"><arguments><string>Welcome`64`16`SimSun`10`1`-3`0`0`0xFF0000`0xFF0000`0xFF0000``true````10`1.0`0x000000`爱心.swf``2000`1200`770`</string></arguments></invoke>";

		private int num;

		private IContainer components;

		private Label label2;

		private Timer timer1;

		private AxShockwaveFlash axShockwaveFlash1;

		public formStart()
		{
			try
			{
				this.InitializeComponent();
				if (Program.IsforeignTradeMode)
				{
					base.Icon = Resources.AppIconV5;
				}
				else
				{
					base.Icon = Resources.AppIcon;
				}
				this.axShockwaveFlash1.Movie = Application.StartupPath + "\\Test.swf";
				this.axShockwaveFlash1.CallFunction(this.value);
			}
			catch (Exception ex)
            {
				this.CheckFlashOCXReg();
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.num++;
		}

		private void formStart_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			if (formMain.IsforeignTradeMode)
			{
				this.BackgroundImage = Resources.startV5_en;
			}
			else
			{
				string a = formMain.ReadConfig();
				if (string.IsNullOrEmpty(a))
				{
					a = formMain.GetLanguageFromSystemBackLCID();
				}
				if (a == "zh_CN" || a == "zh_TW")
				{
					//this.BackgroundImage = Properties.Resources.start_cn;
                    this.BackgroundImage = Resources.start_cn;
                }
				else
				{
					this.BackgroundImage = Resources.start_en;
				}
			}
			this.timer1.Start();
		}

		private bool CheckRegistredOcx(string sClSID)
		{
			RegistryKey classesRoot = Registry.ClassesRoot;
			return classesRoot.OpenSubKey(sClSID) != null;
		}

		private void CheckFlashOCXReg()
		{
			try
			{
				string text = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\";
				string str = Application.StartupPath + "\\";
				string str2 = "Flash32_11_8_800_168.ocx";
				string text2 = "regocx.bat";
				string str3 = "unregocx.bat";
				string text3 = str + str2;
				string text4 = str + text2;
				string text5 = str + str3;
				if (File.Exists(text3) && !File.Exists(text + str2))
				{
					File.Copy(text3, text + str2, true);
				}
				if (File.Exists(text4))
				{
					File.Copy(text4, text + text2, true);
				}
				if (File.Exists(text5))
				{
					File.Copy(text5, text + str3, true);
				}
				if (File.Exists(text + str2) && File.Exists(text + text2))
				{
					Process process = new Process();
					process.StartInfo.WorkingDirectory = text;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardError = true;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.FileName = text2;
					process.Start();
					process.StandardOutput.ReadToEnd();
					process.StandardError.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formStart));
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(51, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(361, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "loading...正在准备资源,正在载入工程,正在初始化界面";
            this.label2.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // axShockwaveFlash1
            // 
            this.axShockwaveFlash1.Enabled = true;
            this.axShockwaveFlash1.Location = new System.Drawing.Point(336, 29);
            this.axShockwaveFlash1.Name = "axShockwaveFlash1";
            this.axShockwaveFlash1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash1.OcxState")));
            this.axShockwaveFlash1.Size = new System.Drawing.Size(192, 192);
            this.axShockwaveFlash1.TabIndex = 2;
            this.axShockwaveFlash1.Visible = false;
            // 
            // formStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(628, 325);
            this.Controls.Add(this.axShockwaveFlash1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.formStart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
