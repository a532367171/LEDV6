using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LedControlSystem.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0"), CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string ProjectDir
		{
			get
			{
				return (string)this["ProjectDir"];
			}
			set
			{
				this["ProjectDir"] = value;
			}
		}

		[DefaultSettingValue("zh_CN"), UserScopedSetting, DebuggerNonUserCode]
		public string Language
		{
			get
			{
				return (string)this["Language"];
			}
			set
			{
				this["Language"] = value;
			}
		}

		[DefaultSettingValue("1024"), UserScopedSetting, DebuggerNonUserCode]
		public int FrameLength
		{
			get
			{
				return (int)this["FrameLength"];
			}
			set
			{
				this["FrameLength"] = value;
			}
		}

		[DefaultSettingValue("Classic1"), UserScopedSetting, DebuggerNonUserCode]
		public string Template
		{
			get
			{
				return (string)this["Template"];
			}
			set
			{
				this["Template"] = value;
			}
		}

		[DefaultSettingValue("100"), UserScopedSetting, DebuggerNonUserCode]
		public int DataStar
		{
			get
			{
				return (int)this["DataStar"];
			}
			set
			{
				this["DataStar"] = value;
			}
		}

		[DefaultSettingValue("16"), UserScopedSetting, DebuggerNonUserCode]
		public int FrameRate
		{
			get
			{
				return (int)this["FrameRate"];
			}
			set
			{
				this["FrameRate"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string ProjectName
		{
			get
			{
				return (string)this["ProjectName"];
			}
			set
			{
				this["ProjectName"] = value;
			}
		}

		[DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
		public bool UseGroup
		{
			get
			{
				return (bool)this["UseGroup"];
			}
			set
			{
				this["UseGroup"] = value;
			}
		}

		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool GprsRememberUser
		{
			get
			{
				return (bool)this["GprsRememberUser"];
			}
			set
			{
				this["GprsRememberUser"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string GPRSUsername
		{
			get
			{
				return (string)this["GPRSUsername"];
			}
			set
			{
				this["GPRSUsername"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string GPRSPassword
		{
			get
			{
				return (string)this["GPRSPassword"];
			}
			set
			{
				this["GPRSPassword"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string CloudServerUserName
		{
			get
			{
				return (string)this["CloudServerUserName"];
			}
			set
			{
				this["CloudServerUserName"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string CloudServerPassword
		{
			get
			{
				return (string)this["CloudServerPassword"];
			}
			set
			{
				this["CloudServerPassword"] = value;
			}
		}

		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool CloudServerRememberUser
		{
			get
			{
				return (bool)this["CloudServerRememberUser"];
			}
			set
			{
				this["CloudServerRememberUser"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string CloudServerFather
		{
			get
			{
				return (string)this["CloudServerFather"];
			}
			set
			{
				this["CloudServerFather"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string CloudServerChildUserName
		{
			get
			{
				return (string)this["CloudServerChildUserName"];
			}
			set
			{
				this["CloudServerChildUserName"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string CloudServerChildPassword
		{
			get
			{
				return (string)this["CloudServerChildPassword"];
			}
			set
			{
				this["CloudServerChildPassword"] = value;
			}
		}

		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool CloudServerIsAccount
		{
			get
			{
				return (bool)this["CloudServerIsAccount"];
			}
			set
			{
				this["CloudServerIsAccount"] = value;
			}
		}

		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool CloudServerRememberChild
		{
			get
			{
				return (bool)this["CloudServerRememberChild"];
			}
			set
			{
				this["CloudServerRememberChild"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string ProjectCloudName
		{
			get
			{
				return (string)this["ProjectCloudName"];
			}
			set
			{
				this["ProjectCloudName"] = value;
			}
		}

		[DefaultSettingValue("-1"), UserScopedSetting, DebuggerNonUserCode]
		public int RunMode
		{
			get
			{
				return (int)this["RunMode"];
			}
			set
			{
				this["RunMode"] = value;
			}
		}

		[DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
		public bool WiFiProductionTest
		{
			get
			{
				return (bool)this["WiFiProductionTest"];
			}
			set
			{
				this["WiFiProductionTest"] = value;
			}
		}
	}
}
