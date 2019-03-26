using System;
using System.Drawing;
using System.Globalization;
using System.Xml;

namespace ZHUI
{
	public static class Template
	{
		public static string TemplatePath = "D:\\Template\\";

		public static Bitmap Main_LeftTop;

		public static Bitmap Main_RightTop;

		public static Bitmap Main_TopBackground;

		public static bool SmoothCorner = true;

		public static Bitmap GroupBox_Top;

		public static Bitmap GroupBox_Bottom;

		public static Bitmap GroupBox_Left;

		public static Bitmap GroupBox_Right;

		public static Bitmap GroupBox_LeftTop;

		public static Bitmap GroupBox_LeftBottom;

		public static Bitmap GroupBox_RightTop;

		public static Bitmap GroupBox_RightBottom;

		public static Bitmap Main_Close;

		public static Bitmap Main_Close_on;

		public static Bitmap Main_Max;

		public static Bitmap Main_Max_on;

		public static Bitmap Main_Min;

		public static Bitmap Main_Min_on;

		public static Bitmap Main_Border_Bottom;

		public static Bitmap Main_Border_Left;

		public static Bitmap Main_Border_LeftBottom;

		public static Bitmap Main_Border_LeftTop;

		public static Bitmap Main_Border_Right;

		public static Bitmap Main_Border_RightBottom;

		public static Bitmap Main_Border_RightTop;

		public static Bitmap Main_Border_Top;

		public static Bitmap Navigator_Background;

		public static Bitmap Navigator_BackgroundOn;

		public static Bitmap Navigator_Spliter;

		public static Bitmap ToolButton_Save;

		public static Bitmap ToolButton_Dial;

		public static Bitmap ToolButton_Open;

		public static Bitmap ToolButton_Counter;

		public static Bitmap ToolButton_Send;

		public static Bitmap ToolButton_Clock;

		public static Bitmap ToolButton_Text;

		public static Bitmap ToolButton_Play;

		public static Bitmap ToolButton_FindDevice;

		public static Bitmap ToolButton_FindDeviceWifi;

		public static Bitmap ToolButton_Timing;

		public static Bitmap ToolButton_Temperature;

		public static Bitmap ToolButton_Marquee;

		public static Bitmap ToolButton_Delete;

		public static Bitmap ToolButton_Close;

		public static Bitmap ToolButton_Item;

		public static Bitmap ToolButton_Start;

		public static Bitmap ToolButton_Lunar;

		public static Bitmap ToolButton_Luminance;

		public static Bitmap ToolButton_Animation;

		public static Bitmap ToolButton_USB;

		public static Bitmap ToolButton_Noise;

		public static Bitmap ToolButton_Humidity;

		public static Bitmap ToolButton_Score;

		public static Bitmap ToolButton_SendAll;

		public static Bitmap ToolButton_Weather;

		public static Bitmap ToolButton_String;

		public static string FontFaimily;

		public static Font LogoFont;

		public static Font MenuFont;

		public static Font NavigatorFont;

		public static Font TreeviewFont;

		public static Brush NavigatorBrush;

		public static Color GroupBox_BackColor;

		public static Color GroupBox_BroderColor;

		public static Color Panel_BroderColor;

		public static Color NavigatorColor;

		public static Color Main_LogoColor;

		public static Color TreeViewColor;

		public static Color MenuItemBackcolor;

		public static Color MenuForecolor;

		public static Color MenuMouseEnterColor;

		public static Color MainForm_BackColor;

		private static XmlDocument docRes = new XmlDocument();

		public static void Load(string pTemplateName)
		{
			try
			{
				string str = Template.TemplatePath + pTemplateName + "\\";
				Template.docRes.Load(str + "Config.xml");
				Template.FontFaimily = Template.GetStr("FontFamily");
				string[] array = Template.GetStr("FontSize").Split(new char[]
				{
					','
				});
				Template.LogoFont = new Font(Template.FontFaimily, float.Parse(array[0]), FontStyle.Bold, GraphicsUnit.Pixel);
				Template.MenuFont = new Font(Template.FontFaimily, float.Parse(array[1]), FontStyle.Bold, GraphicsUnit.Pixel);
				Template.NavigatorFont = new Font(Template.FontFaimily, float.Parse(array[2]), FontStyle.Bold, GraphicsUnit.Pixel);
				Template.TreeviewFont = new Font(Template.FontFaimily, float.Parse(array[3]), FontStyle.Bold, GraphicsUnit.Pixel);
				Template.DisposeMain();
				Template.Main_LeftTop = new Bitmap(str + "Main_LeftTop.png");
				Template.Main_RightTop = new Bitmap(str + "Main_RightTop.png");
				Template.Main_TopBackground = new Bitmap(str + "Main_TopBackground.png");
				Template.Main_Close = new Bitmap(str + "Main_Close.png");
				Template.Main_Close_on = new Bitmap(str + "Main_Close_on.png");
				Template.Main_Max = new Bitmap(str + "Main_Max.png");
				Template.Main_Max_on = new Bitmap(str + "Main_Max_on.png");
				Template.Main_Min = new Bitmap(str + "Main_Min.png");
				Template.Main_Min_on = new Bitmap(str + "Main_Min_on.png");
				Template.Main_Border_Bottom = new Bitmap(str + "Main_Border_Bottom.png");
				Template.Main_Border_Left = new Bitmap(str + "Main_Border_Left.png");
				Template.Main_Border_LeftBottom = new Bitmap(str + "Main_Border_LeftBottom.png");
				Template.Main_Border_LeftTop = new Bitmap(str + "Main_Border_LeftTop.png");
				Template.Main_Border_Right = new Bitmap(str + "Main_Border_Right.png");
				Template.Main_Border_RightBottom = new Bitmap(str + "Main_Border_RightBottom.png");
				Template.Main_Border_RightTop = new Bitmap(str + "Main_Border_RightTop.png");
				Template.Main_Border_Top = new Bitmap(str + "Main_Border_Top.png");
				Template.Navigator_Background = new Bitmap(str + "Navigator_Background.png");
				Template.Navigator_BackgroundOn = new Bitmap(str + "Navigator_BackgroundOn.png");
				Template.Navigator_Spliter = new Bitmap(str + "Navigator_Spliter.png");
				Template.DisposeGroupBox();
				Template.GroupBox_Top = new Bitmap(str + "GroupBox_Top.png");
				Template.GroupBox_Bottom = new Bitmap(str + "GroupBox_Bottom.png");
				Template.GroupBox_Left = new Bitmap(str + "GroupBox_Left.png");
				Template.GroupBox_Right = new Bitmap(str + "GroupBox_Right.png");
				Template.GroupBox_LeftTop = new Bitmap(str + "GroupBox_LeftTop.png");
				Template.GroupBox_LeftBottom = new Bitmap(str + "GroupBox_LeftBottom.png");
				Template.GroupBox_RightTop = new Bitmap(str + "GroupBox_RightTop.png");
				Template.GroupBox_RightBottom = new Bitmap(str + "GroupBox_RightBottom.png");
				Template.GroupBox_BackColor = Template.colorHx16toRGB(Template.GetStr("GroupBox_BackColor"));
				Template.GroupBox_BroderColor = Template.colorHx16toRGB(Template.GetStr("GroupBox_BroderColor"));
				Template.Panel_BroderColor = Template.colorHx16toRGB(Template.GetStr("Panel_BroderColor"));
				Template.NavigatorColor = Template.colorHx16toRGB(Template.GetStr("NagivatorColor"));
				Template.MainForm_BackColor = Template.colorHx16toRGB(Template.GetStr("MainForm_BackColor"));
				Template.Main_LogoColor = Template.colorHx16toRGB(Template.GetStr("Main_LogoColor"));
				Template.TreeViewColor = Template.colorHx16toRGB(Template.GetStr("TreeViewColor"));
				Template.MenuItemBackcolor = Template.colorHx16toRGB(Template.GetStr("MenuItemBackcolor"));
				Template.MenuForecolor = Template.colorHx16toRGB(Template.GetStr("MenuForecolor"));
				Template.MenuMouseEnterColor = Template.colorHx16toRGB(Template.GetStr("MenuMouseEnterColor"));
				Template.DisposeToolButton();
				Template.ToolButton_Save = new Bitmap(str + "ToolButton_Save.png");
				Template.ToolButton_Dial = new Bitmap(str + "ToolButton_Dial.png");
				Template.ToolButton_Open = new Bitmap(str + "ToolButton_Open.png");
				Template.ToolButton_Counter = new Bitmap(str + "ToolButton_Counter.png");
				Template.ToolButton_Send = new Bitmap(str + "ToolButton_Send.png");
				Template.ToolButton_Close = new Bitmap(str + "ToolButton_Close.png");
				Template.ToolButton_Item = new Bitmap(str + "ToolButton_Item.png");
				Template.ToolButton_Start = new Bitmap(str + "ToolButton_Start.png");
				Template.ToolButton_Luminance = new Bitmap(str + "ToolButton_Luminance.png");
				Template.ToolButton_Delete = new Bitmap(str + "ToolButton_Delete.png");
				Template.ToolButton_Clock = new Bitmap(str + "ToolButton_Clock.png");
				Template.ToolButton_Marquee = new Bitmap(str + "ToolButton_Marquee.png");
				Template.ToolButton_Temperature = new Bitmap(str + "ToolButton_Temperature.png");
				Template.ToolButton_Timing = new Bitmap(str + "ToolButton_Timing.png");
				Template.ToolButton_FindDevice = new Bitmap(str + "ToolButton_FindDevice.png");
				Template.ToolButton_FindDeviceWifi = new Bitmap(str + "ToolButton_FindDeviceWifi.png");
				Template.ToolButton_Play = new Bitmap(str + "ToolButton_Play.png");
				Template.ToolButton_Text = new Bitmap(str + "ToolButton_Text.png");
				Template.ToolButton_Animation = new Bitmap(str + "ToolButton_Animation.png");
				Template.ToolButton_Lunar = new Bitmap(str + "ToolButton_Lunar.png");
				Template.ToolButton_USB = new Bitmap(str + "ToolButton_USB.png");
				Template.ToolButton_Noise = new Bitmap(str + "ToolButton_Noise.png");
				Template.ToolButton_Humidity = new Bitmap(str + "ToolButton_Humidity.png");
				Template.ToolButton_Score = new Bitmap(str + "ToolButton_Score.png");
				Template.ToolButton_SendAll = new Bitmap(str + "ToolButton_SendAll.png");
				Template.ToolButton_Weather = new Bitmap(str + "ToolButton_Weather.png");
				Template.ToolButton_String = new Bitmap(str + "ToolButton_String.png");
				Template.NavigatorBrush = new SolidBrush(Template.NavigatorColor);
				string str2 = Template.GetStr("SmoothCorner");
				if (str2 == "true")
				{
					Template.SmoothCorner = true;
				}
				else
				{
					Template.SmoothCorner = false;
				}
			}
			catch
			{
			}
		}

		private static void DisposeMain()
		{
			if (Template.Main_LeftTop != null)
			{
				Template.Main_LeftTop.Dispose();
				Template.Main_LeftTop = null;
			}
			if (Template.Main_RightTop != null)
			{
				Template.Main_RightTop.Dispose();
				Template.Main_RightTop = null;
			}
			if (Template.Main_TopBackground != null)
			{
				Template.Main_TopBackground.Dispose();
				Template.Main_TopBackground = null;
			}
			if (Template.Main_Close != null)
			{
				Template.Main_Close.Dispose();
				Template.Main_Close = null;
			}
			if (Template.Main_Close_on != null)
			{
				Template.Main_Close_on.Dispose();
				Template.Main_Close_on = null;
			}
			if (Template.Main_Max != null)
			{
				Template.Main_Max.Dispose();
				Template.Main_Max = null;
			}
			if (Template.Main_Max_on != null)
			{
				Template.Main_Max_on.Dispose();
				Template.Main_Max_on = null;
			}
			if (Template.Main_Min != null)
			{
				Template.Main_Min.Dispose();
				Template.Main_Min = null;
			}
			if (Template.Main_Min_on != null)
			{
				Template.Main_Min_on.Dispose();
				Template.Main_Min_on = null;
			}
			if (Template.Main_Border_Bottom != null)
			{
				Template.Main_Border_Bottom.Dispose();
				Template.Main_Border_Bottom = null;
			}
			if (Template.Main_Border_Left != null)
			{
				Template.Main_Border_Left.Dispose();
				Template.Main_Border_Left = null;
			}
			if (Template.Main_Border_LeftBottom != null)
			{
				Template.Main_Border_LeftBottom.Dispose();
				Template.Main_Border_LeftBottom = null;
			}
			if (Template.Main_Border_LeftTop != null)
			{
				Template.Main_Border_LeftTop.Dispose();
				Template.Main_Border_LeftTop = null;
			}
			if (Template.Main_Border_Right != null)
			{
				Template.Main_Border_Right.Dispose();
				Template.Main_Border_Right = null;
			}
			if (Template.Main_Border_RightBottom != null)
			{
				Template.Main_Border_RightBottom.Dispose();
				Template.Main_Border_RightBottom = null;
			}
			if (Template.Main_Border_RightTop != null)
			{
				Template.Main_Border_RightTop.Dispose();
				Template.Main_Border_RightTop = null;
			}
			if (Template.Main_Border_Top != null)
			{
				Template.Main_Border_Top.Dispose();
				Template.Main_Border_Top = null;
			}
			if (Template.Navigator_Background != null)
			{
				Template.Navigator_Background.Dispose();
				Template.Navigator_Background = null;
			}
			if (Template.Navigator_BackgroundOn != null)
			{
				Template.Navigator_BackgroundOn.Dispose();
				Template.Navigator_BackgroundOn = null;
			}
			if (Template.Navigator_Spliter != null)
			{
				Template.Navigator_Spliter.Dispose();
				Template.Navigator_Spliter = null;
			}
		}

		private static void DisposeGroupBox()
		{
			if (Template.GroupBox_Top != null)
			{
				Template.GroupBox_Top.Dispose();
				Template.GroupBox_Top = null;
			}
			if (Template.GroupBox_Bottom != null)
			{
				Template.GroupBox_Bottom.Dispose();
				Template.GroupBox_Bottom = null;
			}
			if (Template.GroupBox_Left != null)
			{
				Template.GroupBox_Left.Dispose();
				Template.GroupBox_Left = null;
			}
			if (Template.GroupBox_Right != null)
			{
				Template.GroupBox_Right.Dispose();
				Template.GroupBox_Right = null;
			}
			if (Template.GroupBox_LeftTop != null)
			{
				Template.GroupBox_LeftTop.Dispose();
				Template.GroupBox_LeftTop = null;
			}
			if (Template.GroupBox_LeftBottom != null)
			{
				Template.GroupBox_LeftBottom.Dispose();
				Template.GroupBox_LeftBottom = null;
			}
			if (Template.GroupBox_RightTop != null)
			{
				Template.GroupBox_RightTop.Dispose();
				Template.GroupBox_RightTop = null;
			}
			if (Template.GroupBox_RightBottom != null)
			{
				Template.GroupBox_RightBottom.Dispose();
				Template.GroupBox_RightBottom = null;
			}
		}

		private static void DisposeToolButton()
		{
			if (Template.ToolButton_Save != null)
			{
				Template.ToolButton_Save.Dispose();
				Template.ToolButton_Save = null;
			}
			if (Template.ToolButton_Dial != null)
			{
				Template.ToolButton_Dial.Dispose();
				Template.ToolButton_Dial = null;
			}
			if (Template.ToolButton_Open != null)
			{
				Template.ToolButton_Open.Dispose();
				Template.ToolButton_Open = null;
			}
			if (Template.ToolButton_Counter != null)
			{
				Template.ToolButton_Counter.Dispose();
				Template.ToolButton_Counter = null;
			}
			if (Template.ToolButton_Send != null)
			{
				Template.ToolButton_Send.Dispose();
				Template.ToolButton_Send = null;
			}
			if (Template.ToolButton_Close != null)
			{
				Template.ToolButton_Close.Dispose();
				Template.ToolButton_Close = null;
			}
			if (Template.ToolButton_Item != null)
			{
				Template.ToolButton_Item.Dispose();
				Template.ToolButton_Item = null;
			}
			if (Template.ToolButton_Start != null)
			{
				Template.ToolButton_Start.Dispose();
				Template.ToolButton_Start = null;
			}
			if (Template.ToolButton_Luminance != null)
			{
				Template.ToolButton_Luminance.Dispose();
				Template.ToolButton_Luminance = null;
			}
			if (Template.ToolButton_Delete != null)
			{
				Template.ToolButton_Delete.Dispose();
				Template.ToolButton_Delete = null;
			}
			if (Template.ToolButton_Clock != null)
			{
				Template.ToolButton_Clock.Dispose();
				Template.ToolButton_Clock = null;
			}
			if (Template.ToolButton_Marquee != null)
			{
				Template.ToolButton_Marquee.Dispose();
				Template.ToolButton_Marquee = null;
			}
			if (Template.ToolButton_Temperature != null)
			{
				Template.ToolButton_Temperature.Dispose();
				Template.ToolButton_Temperature = null;
			}
			if (Template.ToolButton_Timing != null)
			{
				Template.ToolButton_Timing.Dispose();
				Template.ToolButton_Timing = null;
			}
			if (Template.ToolButton_FindDevice != null)
			{
				Template.ToolButton_FindDevice.Dispose();
				Template.ToolButton_FindDevice = null;
			}
			if (Template.ToolButton_FindDeviceWifi != null)
			{
				Template.ToolButton_FindDeviceWifi.Dispose();
				Template.ToolButton_FindDeviceWifi = null;
			}
			if (Template.ToolButton_Play != null)
			{
				Template.ToolButton_Play.Dispose();
				Template.ToolButton_Play = null;
			}
			if (Template.ToolButton_Text != null)
			{
				Template.ToolButton_Text.Dispose();
				Template.ToolButton_Text = null;
			}
			if (Template.ToolButton_Animation != null)
			{
				Template.ToolButton_Animation.Dispose();
				Template.ToolButton_Animation = null;
			}
			if (Template.ToolButton_Lunar != null)
			{
				Template.ToolButton_Lunar.Dispose();
				Template.ToolButton_Lunar = null;
			}
			if (Template.ToolButton_USB != null)
			{
				Template.ToolButton_USB.Dispose();
				Template.ToolButton_USB = null;
			}
			if (Template.ToolButton_Noise != null)
			{
				Template.ToolButton_Noise.Dispose();
				Template.ToolButton_Noise = null;
			}
			if (Template.ToolButton_Humidity != null)
			{
				Template.ToolButton_Humidity.Dispose();
				Template.ToolButton_Humidity = null;
			}
			if (Template.ToolButton_Score != null)
			{
				Template.ToolButton_Score.Dispose();
				Template.ToolButton_Score = null;
			}
			if (Template.ToolButton_SendAll != null)
			{
				Template.ToolButton_SendAll.Dispose();
				Template.ToolButton_SendAll = null;
			}
			if (Template.ToolButton_Weather != null)
			{
				Template.ToolButton_Weather.Dispose();
				Template.ToolButton_Weather = null;
			}
			if (Template.ToolButton_String != null)
			{
				Template.ToolButton_String.Dispose();
				Template.ToolButton_String = null;
			}
		}

		private static string GetStr(string pID)
		{
			string result;
			try
			{
				XmlNode xmlNode = Template.docRes.SelectSingleNode("/Root/Record[@ID='" + pID + "']");
				if (xmlNode == null)
				{
					result = "#000000";
				}
				else
				{
					result = xmlNode.InnerText;
				}
			}
			catch
			{
				result = "#000000";
			}
			return result;
		}

		public static Color colorHx16toRGB(string strHxColor)
		{
			Color result;
			try
			{
				if (strHxColor.Length == 0)
				{
					result = Color.FromArgb(0, 0, 0);
				}
				else
				{
					result = Color.FromArgb(int.Parse(strHxColor.Substring(1, 2), NumberStyles.AllowHexSpecifier), int.Parse(strHxColor.Substring(3, 2), NumberStyles.AllowHexSpecifier), int.Parse(strHxColor.Substring(5, 2), NumberStyles.AllowHexSpecifier));
				}
			}
			catch
			{
				result = Color.FromArgb(0, 0, 0);
			}
			return result;
		}
	}
}
