using AxShockwaveFlashObjects;
using LedModel;
using LedModel.Const;
using LedModel.Content;
using LedModel.Foundation;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace LedControlSystem
{
	public class AnimationMaker : UserControl
	{
		public delegate IntPtr FlaWndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		public enum TernaryRasterOperations : uint
		{
			SRCCOPY = 13369376u,
			SRCPAINT = 15597702u,
			SRCAND = 8913094u,
			SRCINVERT = 6684742u,
			SRCERASE = 4457256u,
			NOTSRCCOPY = 3342344u,
			NOTSRCERASE = 1114278u,
			MERGECOPY = 12583114u,
			MERGEPAINT = 12255782u,
			PATCOPY = 15728673u,
			PATPAINT = 16452105u,
			PATINVERT = 5898313u,
			DSTINVERT = 5570569u,
			BLACKNESS = 66u,
			WHITENESS = 16711778u,
			CAPTUREBLT = 1073741824u
		}

		private const int GWL_WNDPROC = -4;

		private const string spliter = "`";

		private const char split_String = '^';

		private IntPtr OldWndProc = IntPtr.Zero;

		private AnimationMaker.FlaWndProc Wpr;

		public static string tempDir = Environment.CurrentDirectory = Environment.GetEnvironmentVariable("TEMP");

		private LedAnimation animation;

		private LedBackground background;

		private bool isLoadingParam;

		private System.Drawing.Size displaySize;

		private bool isGenerate;

		public int GetCount;

		private bool isBackgroundModel;

		public int PageCount = 1000;

		public int StopCount;

		private int finalSpace;

		private static string[] noStayEffect = new string[]
		{
			"Explode",
			"Bomb"
		};

		private IContainer components;

		private AxShockwaveFlash player;

		public event AnimationEvent Event;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, AnimationMaker.FlaWndProc wndProc);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		private IntPtr EFFWndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
		{
			IntPtr result;
			try
			{
				if (msg == 516)
				{
					result = (IntPtr)0;
				}
				else
				{
					result = AnimationMaker.CallWindowProc(this.OldWndProc, hWnd, msg, wParam, lParam);
				}
			}
			catch
			{
				result = hWnd;
			}
			return result;
		}

		public AnimationMaker(System.Drawing.Size pSize, LedAnimation pAnimation)
		{
			this.InitializeComponent();
			pAnimation.EAnimation.PageCount = 0;
			base.Size = pSize;
			this.displaySize = pSize;
			this.animation = pAnimation;
			this.Wpr = new AnimationMaker.FlaWndProc(this.EFFWndProc);
			this.OldWndProc = AnimationMaker.SetWindowLong(this.player.Handle, -4, this.Wpr);
			this.ChangeEffect();
		}

		public AnimationMaker(System.Drawing.Size pSize, LedBackground pBackground)
		{
			if (pSize.Width > 1000)
			{
				pSize.Width = 1000;
			}
			this.isBackgroundModel = true;
			this.InitializeComponent();
			base.Size = pSize;
			this.displaySize = pSize;
			this.Wpr = new AnimationMaker.FlaWndProc(this.EFFWndProc);
			this.OldWndProc = AnimationMaker.SetWindowLong(this.player.Handle, -4, this.Wpr);
			this.background = pBackground;
			this.background.PageCount = 0;
		}

		public void Clear()
		{
			bool arg_06_0 = this.isBackgroundModel;
		}

		public void SetMovie(string pPath)
		{
			this.player.Movie = pPath;
		}

		public void ChangeEffect()
		{
			if (this.isBackgroundModel)
			{
				try
				{
					LedEffects dataByName = AnimationMaker.GetDataByName(this.background.EffectsValueName);
					FileStream fileStream = new FileStream(AnimationMaker.tempDir + "\\efl.zhs", FileMode.Create);
					FileStream fileStream2 = new FileStream(AnimationMaker.tempDir + "\\efl1.zhs", FileMode.Create);
					StreamWriter streamWriter = new StreamWriter(AnimationMaker.tempDir + "\\back.xml", false);
					fileStream.Write(dataByName.OriginalData, 0, dataByName.OriginalData.Length);
					fileStream2.Write(dataByName.OriginalData, 0, dataByName.OriginalData.Length);
					string text = dataByName.PropertyXml.Replace("Movie.Back.Width", (this.displaySize.Width + 20).ToString());
					text = text.Replace("Movie.Back.Height", (this.displaySize.Height + 10).ToString());
					int num = text.IndexOf("Param.Back.Number");
					if (num > 0)
					{
						string text2 = text.Substring(num, 20);
						string backgroundNumber = AnimationMaker.GetBackgroundNumber(this.background.EffectsValueName, text2, this.displaySize);
						text = text.Replace(text2, backgroundNumber);
						streamWriter.Write(text);
					}
					else
					{
						streamWriter.Write(text);
					}
					streamWriter.Close();
					fileStream.Close();
					fileStream2.Close();
					this.player.Size = new System.Drawing.Size(this.player.Width + 3, this.player.Height);
					try
					{
						this.player.Movie = AnimationMaker.tempDir + "\\efl.zhs";
					}
					catch
					{
						this.player.Movie = AnimationMaker.tempDir + "\\efl1.zhs";
					}
					return;
				}
				catch
				{
					return;
				}
			}
			LedEffects dataByName2 = AnimationMaker.GetDataByName(this.animation.AnimationEffectsSetting.Name);
			FileStream fileStream3 = new FileStream(AnimationMaker.tempDir + "\\efl.zhs", FileMode.Create);
			FileStream fileStream4 = new FileStream(AnimationMaker.tempDir + "\\efl1.zhs", FileMode.Create);
			fileStream3.Write(dataByName2.OriginalData, 0, dataByName2.OriginalData.Length);
			fileStream4.Write(dataByName2.OriginalData, 0, dataByName2.OriginalData.Length);
			fileStream3.Close();
			fileStream4.Close();
			File.Delete(AnimationMaker.tempDir + "\\back.xml");
			try
			{
				this.player.Movie = AnimationMaker.tempDir + "\\efl1.zhs";
			}
			catch
			{
				this.player.Movie = AnimationMaker.tempDir + "\\efl.zhs";
			}
			this.RefrshAnimation(false);
		}

		public void changeMaterial()
		{
			if (this.isBackgroundModel)
			{
				try
				{
					this.player.Location = new System.Drawing.Point(0, 0);
					this.player.Size = new System.Drawing.Size(this.displaySize.Width + 10, this.displaySize.Height + 10);
					this.player.Movie = LedCommonConst.MaterialSwfPath + this.background.MaterialName;
					this.player.Scale(new System.Drawing.SizeF((float)(this.displaySize.Width + 10), (float)(this.displaySize.Height + 10)));
					this.player.ScaleMode = 2;
					this.player.Play();
					return;
				}
				catch
				{
					return;
				}
			}
			LedEffects dataByName = AnimationMaker.GetDataByName(this.animation.AnimationEffectsSetting.Name);
			FileStream fileStream = new FileStream(AnimationMaker.tempDir + "\\efl.zhs", FileMode.Create);
			FileStream fileStream2 = new FileStream(AnimationMaker.tempDir + "\\efl1.zhs", FileMode.Create);
			fileStream.Write(dataByName.OriginalData, 0, dataByName.OriginalData.Length);
			fileStream2.Write(dataByName.OriginalData, 0, dataByName.OriginalData.Length);
			fileStream.Close();
			fileStream2.Close();
			File.Delete(AnimationMaker.tempDir + "\\back.xml");
			try
			{
				this.player.Movie = AnimationMaker.tempDir + "\\efl1.zhs";
			}
			catch
			{
				this.player.Movie = AnimationMaker.tempDir + "\\efl.zhs";
			}
			this.RefrshAnimation(false);
		}

		public static string GetBackgroundNumber(string pEffectName, string pOldNumber, System.Drawing.Size pSize)
		{
			pOldNumber.Substring(17, 3);
			int num = pSize.Height * pSize.Width;
			return (num / 3072 + 10).ToString();
		}

		public static LedEffects GetDataByName(string pName)
		{
			for (int i = 0; i < LedGlobal.LedEffectsList.Count; i++)
			{
				if (LedGlobal.LedEffectsList[i].Name == pName)
				{
					return LedGlobal.LedEffectsList[i];
				}
			}
			return null;
		}

		private static bool IsEffectNoStay(string pEffect)
		{
			string[] array = AnimationMaker.noStayEffect;
			for (int i = 0; i < array.Length; i++)
			{
				string a = array[i];
				if (a == pEffect)
				{
					return true;
				}
			}
			return false;
		}

		public void RefrshAnimation(bool pGenerate)
		{
			this.isGenerate = pGenerate;
			if (this.isBackgroundModel)
			{
				return;
			}
			System.Drawing.Bitmap image = new System.Drawing.Bitmap(10, 10);
			System.Drawing.Graphics.FromImage(image);
			System.Drawing.Size stringToBitmapSize = LedGraphics.GetStringToBitmapSize("国", this.animation.EAnimation.Font.GetFont());
			System.Drawing.Size stringToBitmapSize2 = LedGraphics.GetStringToBitmapSize("w", this.animation.EAnimation.Font.GetFont());
			int num = stringToBitmapSize.Width * 100;
			int num2 = stringToBitmapSize2.Width * 110;
			int num3 = (int)((float)(num / 100) * -0.06f);
			num3 += this.animation.AnimationEffectsSetting.Kerning;
			this.finalSpace = num3;
			if (this.isLoadingParam)
			{
				return;
			}
			System.Drawing.Size size = this.displaySize;
			string text = "<invoke name=\"addText\" returntype=\"xml\"><arguments><string>";
			text = text + this.GetMovieString() + "`";
			text = text + size.Width.ToString() + "`";
			text = text + size.Height.ToString() + "`";
			text = text + this.TransFont(this.animation.EAnimation.Font.FamilyName) + "`";
			text = text + (int)((double)this.animation.EAnimation.Font.Size / 72.0 * 96.0) + "`";
			text = text + (int)this.animation.EAnimation.Align + "`";
			text = text + num3.ToString() + "`";
			text = text + this.animation.EAnimation.VerticalOffset + "`";
			text = text + this.animation.EAnimation.HorizontalOffset + "`";
			text = text + AnimationMaker.GetColorRGB(this.animation.EAnimation.ForeColor) + "`";
			text = text + AnimationMaker.GetColorRGB(this.animation.EAnimation.ForeColor) + "`";
			text = text + AnimationMaker.GetColorRGB(this.animation.EAnimation.ForeColor) + "`";
			text = text + this.GetboolStr(this.animation.EAnimation.ColorRandomized) + "`";
			text = text + this.GetboolStr(this.animation.EAnimation.Font.Bold) + "`";
			text = text + this.GetboolStr(this.animation.EAnimation.Font.Italic) + "`";
			text = text + this.GetboolStr(this.animation.EAnimation.Font.Underline) + "`";
			text = text + this.GetboolStr(pGenerate) + "`";
			text = text + this.animation.EAnimation.Speed + "`";
			text = text + this.GetAlphaString(this.animation.EAnimation.Alpha) + "`";
			text = text + AnimationMaker.GetColorRGB(this.animation.EAnimation.BackColor) + "`";
			text = text + this.animation.Background.EffectsValueName + ".swf`";
			text = text + this.GetboolStr(this.animation.Background.Enabled) + "`";
			if (AnimationMaker.IsEffectNoStay(this.animation.AnimationEffectsSetting.Name))
			{
				text += "0`";
			}
			else
			{
				text += "2000`";
			}
			text = text + num.ToString() + "`";
			text = text + num2.ToString() + "`";
			text += "</string></arguments></invoke>";
			try
			{
				this.player.CallFunction(text);
			}
			catch
			{
			}
		}

		private string TransFont(string pFont)
		{
			bool flag = false;
			if (pFont.StartsWith("@"))
			{
				flag = true;
			}
			pFont = pFont.Replace("@", "");
			pFont = pFont.Replace("华文细黑", "STXihei");
			pFont = pFont.Replace("华文楷体", "STKaiti");
			pFont = pFont.Replace("华文宋体", "STSong");
			pFont = pFont.Replace("华文中宋", "STZhongsong");
			pFont = pFont.Replace("华文仿宋", "STFangsong");
			pFont = pFont.Replace("方正舒体", "FZShuTi");
			pFont = pFont.Replace("方正姚体", "FZYaoti");
			pFont = pFont.Replace("方正超粗黑简体", "FZChaoCuHei-M10S");
			pFont = pFont.Replace("华文彩云", "STCaiyun");
			pFont = pFont.Replace("华文琥珀", "STHupo");
			pFont = pFont.Replace("华文隶书", "STLiti");
			pFont = pFont.Replace("华文行楷", "STXingkai");
			pFont = pFont.Replace("华文新魏", "STXinwe");
			pFont = pFont.Replace("仿宋_GB2312", "FangSong_GB2312");
			pFont = pFont.Replace("楷体_GB2312", "KaiTi_GB2312");
			pFont = pFont.Replace("微軟正黑", "Microsoft JhengHei");
			pFont = pFont.Replace("微软雅黑", "Microsoft YaHei");
			pFont = pFont.Replace("黑体", "SimHei");
			pFont = pFont.Replace("宋体", "SimSun");
			pFont = pFont.Replace("新宋体", "NSimSun");
			pFont = pFont.Replace("仿宋", "FangSong");
			pFont = pFont.Replace("楷体", "KaiTi");
			pFont = pFont.Replace("隶书", "LiSu");
			pFont = pFont.Replace("幼圆", "YouYuan");
			pFont = pFont.Replace("隶书", "LiSu");
			if (flag)
			{
				pFont = "@" + pFont;
			}
			return pFont;
		}

		private string GetAlphaString(int pAlpha)
		{
			if (pAlpha == 100)
			{
				return "1.0";
			}
			return "0." + pAlpha.ToString("D2");
		}

		public void Dis()
		{
			this.player.Dispose();
		}

		public string GetboolStr(bool pTrue)
		{
			if (pTrue)
			{
				return "true";
			}
			return "";
		}

		public static string GetColorRGB(System.Drawing.Color pColor)
		{
			return "0x" + pColor.R.ToString("X2") + pColor.G.ToString("X2") + pColor.B.ToString("X2");
		}

		public static string GetColorRGBOnlyColor(System.Drawing.Color pColor)
		{
			return "#" + pColor.R.ToString("X2") + pColor.G.ToString("X2") + pColor.B.ToString("X2");
		}

		public string GetMovieString()
		{
			System.Drawing.Font font = this.animation.EAnimation.Font.GetFont();
			this.PageCount = 0;
			string text = "";
			System.Drawing.Bitmap image = new System.Drawing.Bitmap(10, 10);
			System.Drawing.Graphics.FromImage(image);
			if (this.animation.EAnimation.Text == "")
			{
				this.animation.EAnimation.Text = " ";
			}
			int num = this.animation.EAnimation.Text.Length;
			int num2 = 0;
			if (num <= 0)
			{
				return text;
			}
			int i = 1;
			while (i <= num)
			{
				if (LedGraphics.GetStringToBitmapSize(this.animation.EAnimation.Text.Substring(num2, i), font).Width + (i - 1) * this.finalSpace > this.displaySize.Width)
				{
					if (i == 1)
					{
						i = 1;
					}
					else
					{
						i--;
					}
					if (num == i)
					{
						text += this.animation.EAnimation.Text.Substring(num2, i);
						this.PageCount++;
					}
					else
					{
						text = text + this.animation.EAnimation.Text.Substring(num2, i) + "^";
						this.PageCount++;
					}
					num -= i;
					num2 += i;
				}
				else
				{
					i++;
				}
			}
			text += this.animation.EAnimation.Text.Substring(num2, num);
			this.PageCount++;
			return text;
		}

		private static string GetParam(string pValue)
		{
			int num = pValue.IndexOf("^");
			if (num == -1)
			{
				return pValue;
			}
			return pValue.Substring(0, num);
		}

		public void Make()
		{
			if (this.isBackgroundModel)
			{
				this.background.Draw(AnimationMaker.GetImageOfControl(this.player, this.displaySize));
			}
			else
			{
				this.animation.Draw(AnimationMaker.GetImageOfControl(this.player, this.displaySize));
			}
			this.GetCount++;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
		}

		[DllImport("gdi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, AnimationMaker.TernaryRasterOperations dwRop);

		public static System.Drawing.Bitmap GetImageOfControl(Control control, System.Drawing.Size pSize)
		{
			int width = pSize.Width;
			int height = pSize.Height;
			System.Drawing.Graphics graphics = control.CreateGraphics();
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height, graphics);
			System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap);
			IntPtr hdc = graphics.GetHdc();
			IntPtr hdc2 = graphics2.GetHdc();
			AnimationMaker.BitBlt(hdc2, 0, 0, width, height, hdc, 0, 0, AnimationMaker.TernaryRasterOperations.SRCCOPY);
			graphics.ReleaseHdc(hdc);
			graphics2.ReleaseHdc(hdc2);
			graphics.Dispose();
			if (pSize.Width > 950)
			{
				graphics2.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.Rectangle(bitmap.Width - 5, 0, 4, bitmap.Height));
			}
			graphics2.Dispose();
			return bitmap;
		}

		private void player_Call(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(e.request);
			XmlAttributeCollection attributes = xmlDocument.FirstChild.Attributes;
			string innerText = attributes.Item(0).InnerText;
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("arguments");
			string a;
			if ((a = innerText) != null)
			{
				if (!(a == "sendText"))
				{
					if (!(a == "Some_Other_Command"))
					{
						return;
					}
				}
				else if (elementsByTagName[0].InnerText == "ChangeText" && this.isGenerate)
				{
					this.StopCount++;
					if (this.StopCount == this.PageCount)
					{
						this.Event(AnimationEventType.Stop);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AnimationMaker));
			this.player = new AxShockwaveFlash();
			((ISupportInitialize)this.player).BeginInit();
			base.SuspendLayout();
			this.player.Enabled = true;
			this.player.Location = new System.Drawing.Point(0, 0);
			this.player.Name = "player";
			this.player.OcxState = (AxHost.State)componentResourceManager.GetObject("player.OcxState");
			this.player.Size = new System.Drawing.Size(1000, 500);
			this.player.TabIndex = 0;
			this.player.FlashCall += new _IShockwaveFlashEvents_FlashCallEventHandler(this.player_Call);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.player);
			base.Name = "AnimationMaker";
			base.Size = new System.Drawing.Size(152, 82);
			((ISupportInitialize)this.player).EndInit();
			base.ResumeLayout(false);
		}
	}
}
