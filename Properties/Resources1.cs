//using System;
//using System.CodeDom.Compiler;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Drawing;
//using System.Globalization;
//using System.Resources;
//using System.Runtime.CompilerServices;

//namespace LedControlSystem.Properties
//{
//	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
//	internal class Resources
//	{
//		//private static ResourceManager resourceMan;

//		private static CultureInfo resourceCulture;

//		[EditorBrowsable(EditorBrowsableState.Advanced)]
//		internal static ResourceManager ResourceManager
//		{
//			get
//			{
//				if (object.ReferenceEquals(Resources.resourceMan, null))
//				{
//					ResourceManager resourceManager = new ResourceManager("LedControlSystem.Properties.Resources", typeof(Resources).Assembly);
//					Resources.resourceMan = resourceManager;
//				}
//				return Resources.resourceMan;
//			}
//		}

//		[EditorBrowsable(EditorBrowsableState.Advanced)]
//		internal static CultureInfo Culture
//		{
//			get
//			{
//				return Resources.resourceCulture;
//			}
//			set
//			{
//				Resources.resourceCulture = value;
//			}
//		}

//		internal static System.Drawing.Icon AppIcon
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("AppIcon", Resources.resourceCulture);
//				return (System.Drawing.Icon)@object;
//			}
//		}

//		internal static System.Drawing.Icon AppIconV5
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("AppIconV5", Resources.resourceCulture);
//				return (System.Drawing.Icon)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap arrow_down
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("arrow_down", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap arrow_left
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("arrow_left", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap BackSelect
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("BackSelect", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap chevron
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("chevron", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap cloud_download
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("cloud_download", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Cloud_infomation
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Cloud_infomation", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Cloud_LoginInfo
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Cloud_LoginInfo", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Cloud_User
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Cloud_User", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Cloud_Users
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Cloud_Users", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap convert
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("convert", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap cross
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("cross", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap datasource
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("datasource", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Delete
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Delete", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static byte[] Edges
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Edges", Resources.resourceCulture);
//				return (byte[])@object;
//			}
//		}

//		internal static byte[] eflist
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("eflist", Resources.resourceCulture);
//				return (byte[])@object;
//			}
//		}

//		internal static System.Drawing.Bitmap ElementEdit_Down
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("ElementEdit_Down", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap ElementEdit_Left
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("ElementEdit_Left", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap ElementEdit_Right
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("ElementEdit_Right", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap ElementEdit_Up
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("ElementEdit_Up", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Find_Device
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Find_Device", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Find_Device_Stop
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Find_Device_Stop", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap folder_horizontal
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("folder_horizontal", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap image_add
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("image_add", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap information
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("information", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap information_share_program
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("information_share_program", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap key
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("key", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Led_Off
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Led_Off", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Led_On
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Led_On", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Led_On_Difference
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Led_On_Difference", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Led_On_New
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Led_On_New", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap list
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("list", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap loading
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("loading", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap Material_PreviewNull
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("Material_PreviewNull", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static byte[] MaterialResources
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("MaterialResources", Resources.resourceCulture);
//				return (byte[])@object;
//			}
//		}

//		internal static System.Drawing.Bitmap mobile
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("mobile", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap monitor
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("monitor", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static string movieBackgroundName
//		{
//			get
//			{
//				return Resources.ResourceManager.GetString("movieBackgroundName", Resources.resourceCulture);
//			}
//		}

//		internal static string movieBackgroundParamAndValue
//		{
//			get
//			{
//				return Resources.ResourceManager.GetString("movieBackgroundParamAndValue", Resources.resourceCulture);
//			}
//		}

//		internal static string movieBackgroundXML
//		{
//			get
//			{
//				return Resources.ResourceManager.GetString("movieBackgroundXML", Resources.resourceCulture);
//			}
//		}

//		internal static System.Drawing.Bitmap pass_button
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("pass_button", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap play
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("play", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static string ProjectDir
//		{
//			get
//			{
//				return Resources.ResourceManager.GetString("ProjectDir", Resources.resourceCulture);
//			}
//		}

//		internal static byte[] provincelist
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("provincelist", Resources.resourceCulture);
//				return (byte[])@object;
//			}
//		}

//		internal static System.Drawing.Bitmap PublicText_Add
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("PublicText_Add", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap PublicText_Del
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("PublicText_Del", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap refresh_L
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("refresh_L", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap refuse_button
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("refuse_button", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap runmode_debug
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("runmode_debug", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap send_cmd
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("send_cmd", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap send_Group
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("send_Group", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap send_L
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("send_L", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap send_R
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("send_R", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap software_update
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("software_update", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap start_cn
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("start_cn", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap start_en
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("start_en", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap startV5_en
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("startV5_en", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap stop
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("stop", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap user
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("user", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap user_levelTop
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("user_levelTop", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap V3_ZH_U0
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("V3_ZH_U0", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap V3_ZH_U1
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("V3_ZH_U1", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap V3_ZH_U2
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("V3_ZH_U2", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap V3_ZH_U3
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("V3_ZH_U3", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap WIFI_Signal_0
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("WIFI_Signal_0", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap WIFI_Signal_1
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("WIFI_Signal_1", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap WIFI_Signal_2
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("WIFI_Signal_2", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap WIFI_Signal_3
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("WIFI_Signal_3", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap WIFI_Signal_4
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("WIFI_Signal_4", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap WIFI_Signal_5
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("WIFI_Signal_5", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static System.Drawing.Bitmap WordColor
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("WordColor", Resources.resourceCulture);
//				return (System.Drawing.Bitmap)@object;
//			}
//		}

//		internal static byte[] zhGridModel
//		{
//			get
//			{
//				object @object = Resources.ResourceManager.GetObject("zhGridModel", Resources.resourceCulture);
//				return (byte[])@object;
//			}
//		}

//		internal Resources()
//		{
//		}
//	}
//}
