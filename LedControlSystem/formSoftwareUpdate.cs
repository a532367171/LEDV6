using LedControlSystem.Properties;
using LedService.SoftwareUpdate;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formSoftwareUpdate : Form
	{
		private string downloadUrl = string.Empty;

		private IContainer components;

		private Button btnClose;

		private TextBox txtVersion;

		private Button btnCheckUpdate;

		private Button btnDownloadAndInstall;

		private ProgressBar pgbDoing;

		private PictureBox picSoftwareUpdate;

		private Label lblState;

		[DllImport("user32.dll")]
		public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

		public formSoftwareUpdate()
		{
			this.InitializeComponent();
		}

		private void formSoftwareUpdate_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.picSoftwareUpdate.SizeMode = PictureBoxSizeMode.StretchImage;
			this.picSoftwareUpdate.Image = Resources.software_update;
			this.btnDownloadAndInstall.Visible = false;
			this.pgbDoing.Style = ProgressBarStyle.Blocks;
			this.lblState.Text = formMain.ML.GetStr("formSoftwareUpdate_Label_NoUpdate");
			this.btnDownloadAndInstall.Text = formMain.ML.GetStr("formSoftwareUpdate_Btn_DownloadAndInstall");
			this.btnCheckUpdate.Text = formMain.ML.GetStr("formSoftwareUpdate_Btn_CheckForUpdates");
			this.btnClose.Text = formMain.ML.GetStr("formSoftwareUpdate_Btn_Close");
			this.Text = formMain.ML.GetStr("formSoftwareUpdate_FormText");
		}

		private void btnDownloadAndInstall_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "LedUpdate";
				bool flag = false;
				int num = -1;
				Process[] processesByName = Process.GetProcessesByName(text);
				if (processesByName != null && processesByName.Length > 0)
				{
					for (int i = 0; i < processesByName.Length; i++)
					{
						Process process = processesByName[i];
						try
						{
							if (process.MainModule != null)
							{
								string fileName = process.MainModule.FileName;
								if (!string.IsNullOrEmpty(fileName))
								{
									FileInfo fileInfo = new FileInfo(fileName);
									if (fileInfo != null)
									{
										string arg_62_0 = fileInfo.Name;
										if (Application.StartupPath == fileInfo.DirectoryName)
										{
											flag = true;
											num = i;
											break;
										}
									}
								}
							}
						}
						catch
						{
							if (process.MainWindowTitle == string.Format("LED Update {0}", formMain.ledTitleVersion))
							{
								flag = true;
								num = i;
								break;
							}
						}
					}
				}
				if (flag)
				{
					if (num > -1)
					{
						IntPtr mainWindowHandle = processesByName[num].MainWindowHandle;
						formSoftwareUpdate.SwitchToThisWindow(mainWindowHandle, true);
						base.Close();
					}
				}
				else
				{
					string fileName2 = string.Format("{0}.exe", text);
					Process.Start(new ProcessStartInfo
					{
						Arguments = this.downloadUrl + " " + Settings.Default.Language,
						FileName = fileName2
					});
					Thread.Sleep(100);
					Application.Exit();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, formMain.ML.GetStr("formSoftwareUpdate_Message_DownloadAndInstallFailedAndReason") + ex.Message, formMain.ML.GetStr("Display_Failed"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void btnCheckUpdate_Click(object sender, EventArgs e)
		{
			this.btnDownloadAndInstall.Visible = false;
			this.btnCheckUpdate.Enabled = false;
			this.lblState.Text = formMain.ML.GetStr("formSoftwareUpdate_Label_GettingUpdates");
			this.pgbDoing.Style = ProgressBarStyle.Marquee;
			this.downloadUrl = string.Empty;
			new Thread(new ThreadStart(this.CheckUpdate))
			{
				IsBackground = true
			}.Start();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void CheckUpdate()
		{
			try
			{
				VersionService versionService = new VersionService();
				string text = formMain.ledVersion.Substring(1);
				string majorVersion = text.Substring(0, text.IndexOf('.'));
				string s = text.Substring(text.LastIndexOf('.') + 1);
				VersionInfo versionInfo2;
				if (formMain.IsforeignTradeMode)
				{
					versionInfo2 = versionService.GetInternationalVersionInfo(majorVersion);
				}
				else
				{
					versionInfo2 = versionService.GetVersionInfo(majorVersion);
				}
				string versionInfo = string.Empty;
				string labelInfo = string.Empty;
				if (versionInfo2 != null && int.Parse(versionInfo2.Version.Substring(versionInfo2.Version.LastIndexOf('.') + 1)) > int.Parse(s))
				{
					this.downloadUrl = versionInfo2.DownloadUrl;
					labelInfo = formMain.ML.GetStr("formSoftwareUpdate_Label_AvailableForDownload");
					versionInfo = string.Format("\r\n  LED Control System V{0} " + formMain.ML.GetStr("formSoftwareUpdate_Label_Published_DownloadToGet") + "\r\n{1}", versionInfo2.Version, versionInfo2.Information.Replace("\n", "\r\n").Replace("\t", "  "));
				}
				else
				{
					labelInfo = formMain.ML.GetStr("formSoftwareUpdate_Label_NoUpdate");
				}
				Thread.Sleep(5000);
				base.Invoke(new MethodInvoker(delegate
				{
					this.lblState.Text = labelInfo;
					this.txtVersion.Text = versionInfo;
					this.pgbDoing.Style = ProgressBarStyle.Blocks;
					if (!string.IsNullOrEmpty(versionInfo))
					{
						this.btnDownloadAndInstall.Visible = true;
					}
					this.btnCheckUpdate.Enabled = true;
				}));
			}
			catch (Exception ex)
			{
				//Exception ex2;
				//Exception ex = ex2;
				//formSoftwareUpdate <>4__this = this;
				//base.Invoke(new MethodInvoker(delegate
				//{
				//	<>4__this.lblState.Text = formMain.ML.GetStr("formSoftwareUpdate_Label_UpdateFailedAndReason") + ex.Message;
				//}));
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
			this.btnClose = new Button();
			this.txtVersion = new TextBox();
			this.btnCheckUpdate = new Button();
			this.btnDownloadAndInstall = new Button();
			this.pgbDoing = new ProgressBar();
			this.picSoftwareUpdate = new PictureBox();
			this.lblState = new Label();
			((ISupportInitialize)this.picSoftwareUpdate).BeginInit();
			base.SuspendLayout();
			this.btnClose.Location = new System.Drawing.Point(623, 263);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(100, 25);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.txtVersion.Location = new System.Drawing.Point(22, 79);
			this.txtVersion.Multiline = true;
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.ReadOnly = true;
			this.txtVersion.Size = new System.Drawing.Size(701, 165);
			this.txtVersion.TabIndex = 1;
			this.btnCheckUpdate.Location = new System.Drawing.Point(506, 263);
			this.btnCheckUpdate.Name = "btnCheckUpdate";
			this.btnCheckUpdate.Size = new System.Drawing.Size(100, 25);
			this.btnCheckUpdate.TabIndex = 2;
			this.btnCheckUpdate.Text = "检查更新";
			this.btnCheckUpdate.UseVisualStyleBackColor = true;
			this.btnCheckUpdate.Click += new EventHandler(this.btnCheckUpdate_Click);
			this.btnDownloadAndInstall.Location = new System.Drawing.Point(390, 263);
			this.btnDownloadAndInstall.Name = "btnDownloadAndInstall";
			this.btnDownloadAndInstall.Size = new System.Drawing.Size(100, 25);
			this.btnDownloadAndInstall.TabIndex = 3;
			this.btnDownloadAndInstall.Text = "下载并安装";
			this.btnDownloadAndInstall.UseVisualStyleBackColor = true;
			this.btnDownloadAndInstall.Click += new EventHandler(this.btnDownloadAndInstall_Click);
			this.pgbDoing.Location = new System.Drawing.Point(22, 50);
			this.pgbDoing.Name = "pgbDoing";
			this.pgbDoing.Size = new System.Drawing.Size(701, 23);
			this.pgbDoing.TabIndex = 4;
			this.picSoftwareUpdate.Location = new System.Drawing.Point(22, 12);
			this.picSoftwareUpdate.Name = "picSoftwareUpdate";
			this.picSoftwareUpdate.Size = new System.Drawing.Size(32, 32);
			this.picSoftwareUpdate.TabIndex = 5;
			this.picSoftwareUpdate.TabStop = false;
			this.lblState.Location = new System.Drawing.Point(60, 12);
			this.lblState.Name = "lblState";
			this.lblState.Size = new System.Drawing.Size(663, 32);
			this.lblState.TabIndex = 6;
			this.lblState.Text = "没有更新";
			this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(744, 302);
			base.Controls.Add(this.lblState);
			base.Controls.Add(this.picSoftwareUpdate);
			base.Controls.Add(this.pgbDoing);
			base.Controls.Add(this.btnDownloadAndInstall);
			base.Controls.Add(this.btnCheckUpdate);
			base.Controls.Add(this.txtVersion);
			base.Controls.Add(this.btnClose);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formSoftwareUpdate";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "软件更新";
			base.Load += new EventHandler(this.formSoftwareUpdate_Load);
			((ISupportInitialize)this.picSoftwareUpdate).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
