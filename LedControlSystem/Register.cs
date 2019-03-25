using Microsoft.Win32;
using System;

namespace LedControlSystem
{
	public class Register
	{
		private string _subkey;

		private RegDomain _domain;

		private string _regeditkey;

		public string SubKey
		{
			set
			{
				this._subkey = value;
			}
		}

		public RegDomain Domain
		{
			set
			{
				this._domain = value;
			}
		}

		public string RegeditKey
		{
			set
			{
				this._regeditkey = value;
			}
		}

		public Register()
		{
			this._subkey = "software\\";
			this._domain = RegDomain.LocalMachine;
		}

		public Register(string subKey, RegDomain regDomain)
		{
			this._subkey = subKey;
			this._domain = regDomain;
		}

		public virtual void CreateSubKey()
		{
			if (this._subkey == string.Empty || this._subkey == null)
			{
				return;
			}
			RegistryKey regDomain = this.GetRegDomain(this._domain);
			if (!this.IsSubKeyExist())
			{
				regDomain.CreateSubKey(this._subkey);
			}
			regDomain.Close();
		}

		public virtual void CreateSubKey(string subKey)
		{
			if (subKey == string.Empty || subKey == null)
			{
				return;
			}
			RegistryKey regDomain = this.GetRegDomain(this._domain);
			if (!this.IsSubKeyExist(subKey))
			{
				regDomain.CreateSubKey(subKey);
			}
			regDomain.Close();
		}

		public virtual void CreateSubKey(RegDomain regDomain)
		{
			if (this._subkey == string.Empty || this._subkey == null)
			{
				return;
			}
			RegistryKey regDomain2 = this.GetRegDomain(regDomain);
			if (!this.IsSubKeyExist(regDomain))
			{
				regDomain2.CreateSubKey(this._subkey);
			}
			regDomain2.Close();
		}

		public virtual void CreateSubKey(string subKey, RegDomain regDomain)
		{
			if (subKey == string.Empty || subKey == null)
			{
				return;
			}
			RegistryKey regDomain2 = this.GetRegDomain(regDomain);
			if (!this.IsSubKeyExist(subKey, regDomain))
			{
				regDomain2.CreateSubKey(subKey);
			}
			regDomain2.Close();
		}

		public virtual bool IsSubKeyExist()
		{
			return !(this._subkey == string.Empty) && this._subkey != null && this.OpenSubKey(this._subkey, this._domain) != null;
		}

		public virtual bool IsSubKeyExist(string subKey)
		{
			return !(subKey == string.Empty) && subKey != null && this.OpenSubKey(subKey) != null;
		}

		public virtual bool IsSubKeyExist(RegDomain regDomain)
		{
			return !(this._subkey == string.Empty) && this._subkey != null && this.OpenSubKey(this._subkey, regDomain) != null;
		}

		public virtual bool IsSubKeyExist(string subKey, RegDomain regDomain)
		{
			return !(subKey == string.Empty) && subKey != null && this.OpenSubKey(subKey, regDomain) != null;
		}

		public virtual bool DeleteSubKey()
		{
			bool result = false;
			if (this._subkey == string.Empty || this._subkey == null)
			{
				return false;
			}
			RegistryKey regDomain = this.GetRegDomain(this._domain);
			if (this.IsSubKeyExist())
			{
				try
				{
					regDomain.DeleteSubKey(this._subkey);
					result = true;
				}
				catch
				{
					result = false;
				}
			}
			regDomain.Close();
			return result;
		}

		public virtual bool DeleteSubKey(string subKey)
		{
			bool result = false;
			if (subKey == string.Empty || subKey == null)
			{
				return false;
			}
			RegistryKey regDomain = this.GetRegDomain(this._domain);
			if (this.IsSubKeyExist())
			{
				try
				{
					regDomain.DeleteSubKey(subKey);
					result = true;
				}
				catch
				{
					result = false;
				}
			}
			regDomain.Close();
			return result;
		}

		public virtual bool DeleteSubKey(string subKey, RegDomain regDomain)
		{
			bool result = false;
			if (subKey == string.Empty || subKey == null)
			{
				return false;
			}
			RegistryKey regDomain2 = this.GetRegDomain(regDomain);
			if (this.IsSubKeyExist(subKey, regDomain))
			{
				try
				{
					regDomain2.DeleteSubKey(subKey);
					result = true;
				}
				catch
				{
					result = false;
				}
			}
			regDomain2.Close();
			return result;
		}

		public virtual bool IsRegeditKeyExist()
		{
			bool result = false;
			if (this._regeditkey == string.Empty || this._regeditkey == null)
			{
				return false;
			}
			if (this.IsSubKeyExist())
			{
				RegistryKey registryKey = this.OpenSubKey();
				string[] valueNames = registryKey.GetValueNames();
				string[] array = valueNames;
				for (int i = 0; i < array.Length; i++)
				{
					string strA = array[i];
					if (string.Compare(strA, this._regeditkey, true) == 0)
					{
						result = true;
						break;
					}
				}
				registryKey.Close();
			}
			return result;
		}

		public virtual bool IsRegeditKeyExist(string name)
		{
			bool result = false;
			if (name == string.Empty || name == null)
			{
				return false;
			}
			if (this.IsSubKeyExist())
			{
				RegistryKey registryKey = this.OpenSubKey();
				string[] valueNames = registryKey.GetValueNames();
				string[] array = valueNames;
				for (int i = 0; i < array.Length; i++)
				{
					string strA = array[i];
					if (string.Compare(strA, name, true) == 0)
					{
						result = true;
						break;
					}
				}
				registryKey.Close();
			}
			return result;
		}

		public virtual bool IsRegeditKeyExist(string name, string subKey)
		{
			bool result = false;
			if (name == string.Empty || name == null)
			{
				return false;
			}
			if (this.IsSubKeyExist())
			{
				RegistryKey registryKey = this.OpenSubKey(subKey);
				string[] valueNames = registryKey.GetValueNames();
				string[] array = valueNames;
				for (int i = 0; i < array.Length; i++)
				{
					string strA = array[i];
					if (string.Compare(strA, name, true) == 0)
					{
						result = true;
						break;
					}
				}
				registryKey.Close();
			}
			return result;
		}

		public virtual bool IsRegeditKeyExist(string name, string subKey, RegDomain regDomain)
		{
			bool result = false;
			if (name == string.Empty || name == null)
			{
				return false;
			}
			if (this.IsSubKeyExist())
			{
				RegistryKey registryKey = this.OpenSubKey(subKey, regDomain);
				string[] valueNames = registryKey.GetValueNames();
				string[] array = valueNames;
				for (int i = 0; i < array.Length; i++)
				{
					string strA = array[i];
					if (string.Compare(strA, name, true) == 0)
					{
						result = true;
						break;
					}
				}
				registryKey.Close();
			}
			return result;
		}

		public virtual bool WriteRegeditKey(object content)
		{
			bool result = false;
			if (this._regeditkey == string.Empty || this._regeditkey == null)
			{
				return false;
			}
			if (!this.IsSubKeyExist(this._subkey))
			{
				this.CreateSubKey(this._subkey);
			}
			RegistryKey registryKey = this.OpenSubKey(true);
			if (registryKey == null)
			{
				return false;
			}
			try
			{
				registryKey.SetValue(this._regeditkey, content);
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				registryKey.Close();
			}
			return result;
		}

		public virtual bool WriteRegeditKey(string name, object content)
		{
			bool result = false;
			if (name == string.Empty || name == null)
			{
				return false;
			}
			if (!this.IsSubKeyExist(this._subkey))
			{
				this.CreateSubKey(this._subkey);
			}
			RegistryKey registryKey = this.OpenSubKey(true);
			if (registryKey == null)
			{
				return false;
			}
			try
			{
				registryKey.SetValue(name, content);
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				registryKey.Close();
			}
			return result;
		}

		public virtual bool WriteRegeditKey(string name, object content, RegValueKind regValueKind)
		{
			bool result = false;
			if (name == string.Empty || name == null)
			{
				return false;
			}
			if (!this.IsSubKeyExist(this._subkey))
			{
				this.CreateSubKey(this._subkey);
			}
			RegistryKey registryKey = this.OpenSubKey(true);
			if (registryKey == null)
			{
				return false;
			}
			try
			{
				registryKey.SetValue(name, content, this.GetRegValueKind(regValueKind));
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				registryKey.Close();
			}
			return result;
		}

		public virtual object ReadRegeditKey()
		{
			object result = null;
			if (this._regeditkey == string.Empty || this._regeditkey == null)
			{
				return null;
			}
			if (this.IsRegeditKeyExist(this._regeditkey))
			{
				RegistryKey registryKey = this.OpenSubKey();
				if (registryKey != null)
				{
					result = registryKey.GetValue(this._regeditkey);
				}
				registryKey.Close();
			}
			return result;
		}

		public virtual object ReadRegeditKey(string name)
		{
			object result = null;
			if (name == string.Empty || name == null)
			{
				return null;
			}
			if (this.IsRegeditKeyExist(name))
			{
				RegistryKey registryKey = this.OpenSubKey();
				if (registryKey != null)
				{
					result = registryKey.GetValue(name);
				}
				registryKey.Close();
			}
			return result;
		}

		public virtual object ReadRegeditKey(string name, string subKey)
		{
			object result = null;
			if (name == string.Empty || name == null)
			{
				return null;
			}
			if (this.IsRegeditKeyExist(name))
			{
				RegistryKey registryKey = this.OpenSubKey(subKey);
				if (registryKey != null)
				{
					result = registryKey.GetValue(name);
				}
				registryKey.Close();
			}
			return result;
		}

		public virtual object ReadRegeditKey(string name, string subKey, RegDomain regDomain)
		{
			object result = null;
			if (name == string.Empty || name == null)
			{
				return null;
			}
			if (this.IsRegeditKeyExist(name))
			{
				RegistryKey registryKey = this.OpenSubKey(subKey, regDomain);
				if (registryKey != null)
				{
					result = registryKey.GetValue(name);
				}
				registryKey.Close();
			}
			return result;
		}

		public virtual bool DeleteRegeditKey()
		{
			bool result = false;
			if (this._regeditkey == string.Empty || this._regeditkey == null)
			{
				return false;
			}
			if (this.IsRegeditKeyExist(this._regeditkey))
			{
				RegistryKey registryKey = this.OpenSubKey(true);
				if (registryKey != null)
				{
					try
					{
						registryKey.DeleteValue(this._regeditkey);
						result = true;
					}
					catch
					{
						result = false;
					}
					finally
					{
						registryKey.Close();
					}
				}
			}
			return result;
		}

		public virtual bool DeleteRegeditKey(string name)
		{
			bool result = false;
			if (name == string.Empty || name == null)
			{
				return false;
			}
			if (this.IsRegeditKeyExist(name))
			{
				RegistryKey registryKey = this.OpenSubKey(true);
				if (registryKey != null)
				{
					try
					{
						registryKey.DeleteValue(name);
						result = true;
					}
					catch
					{
						result = false;
					}
					finally
					{
						registryKey.Close();
					}
				}
			}
			return result;
		}

		public virtual bool DeleteRegeditKey(string name, string subKey)
		{
			bool result = false;
			if (name == string.Empty || name == null || subKey == string.Empty || subKey == null)
			{
				return false;
			}
			if (this.IsRegeditKeyExist(name))
			{
				RegistryKey registryKey = this.OpenSubKey(subKey, true);
				if (registryKey != null)
				{
					try
					{
						registryKey.DeleteValue(name);
						result = true;
					}
					catch
					{
						result = false;
					}
					finally
					{
						registryKey.Close();
					}
				}
			}
			return result;
		}

		public virtual bool DeleteRegeditKey(string name, string subKey, RegDomain regDomain)
		{
			bool result = false;
			if (name == string.Empty || name == null || subKey == string.Empty || subKey == null)
			{
				return false;
			}
			if (this.IsRegeditKeyExist(name))
			{
				RegistryKey registryKey = this.OpenSubKey(subKey, regDomain, true);
				if (registryKey != null)
				{
					try
					{
						registryKey.DeleteValue(name);
						result = true;
					}
					catch
					{
						result = false;
					}
					finally
					{
						registryKey.Close();
					}
				}
			}
			return result;
		}

		protected RegistryKey GetRegDomain(RegDomain regDomain)
		{
			RegistryKey result;
			switch (regDomain)
			{
			case RegDomain.ClassesRoot:
				result = Registry.ClassesRoot;
				return result;
			case RegDomain.CurrentUser:
				result = Registry.CurrentUser;
				return result;
			case RegDomain.LocalMachine:
				result = Registry.LocalMachine;
				return result;
			case RegDomain.User:
				result = Registry.Users;
				return result;
			case RegDomain.CurrentConfig:
				result = Registry.CurrentConfig;
				return result;
			case RegDomain.PerformanceData:
				result = Registry.PerformanceData;
				return result;
			}
			result = Registry.LocalMachine;
			return result;
		}

		protected RegistryValueKind GetRegValueKind(RegValueKind regValueKind)
		{
			RegistryValueKind result;
			switch (regValueKind)
			{
			case RegValueKind.Unknown:
				result = RegistryValueKind.Unknown;
				break;
			case RegValueKind.String:
				result = RegistryValueKind.String;
				break;
			case RegValueKind.ExpandString:
				result = RegistryValueKind.ExpandString;
				break;
			case RegValueKind.Binary:
				result = RegistryValueKind.Binary;
				break;
			case RegValueKind.DWord:
				result = RegistryValueKind.DWord;
				break;
			case RegValueKind.MultiString:
				result = RegistryValueKind.MultiString;
				break;
			case RegValueKind.QWord:
				result = RegistryValueKind.QWord;
				break;
			default:
				result = RegistryValueKind.String;
				break;
			}
			return result;
		}

		protected virtual RegistryKey OpenSubKey()
		{
			if (this._subkey == string.Empty || this._subkey == null)
			{
				return null;
			}
			RegistryKey regDomain = this.GetRegDomain(this._domain);
			RegistryKey result = regDomain.OpenSubKey(this._subkey);
			regDomain.Close();
			return result;
		}

		protected virtual RegistryKey OpenSubKey(bool writable)
		{
			if (this._subkey == string.Empty || this._subkey == null)
			{
				return null;
			}
			RegistryKey regDomain = this.GetRegDomain(this._domain);
			RegistryKey result = regDomain.OpenSubKey(this._subkey, writable);
			regDomain.Close();
			return result;
		}

		protected virtual RegistryKey OpenSubKey(string subKey)
		{
			if (subKey == string.Empty || subKey == null)
			{
				return null;
			}
			RegistryKey regDomain = this.GetRegDomain(this._domain);
			RegistryKey result = regDomain.OpenSubKey(subKey);
			regDomain.Close();
			return result;
		}

		protected virtual RegistryKey OpenSubKey(string subKey, bool writable)
		{
			if (subKey == string.Empty || subKey == null)
			{
				return null;
			}
			RegistryKey regDomain = this.GetRegDomain(this._domain);
			RegistryKey result = regDomain.OpenSubKey(subKey, writable);
			regDomain.Close();
			return result;
		}

		protected virtual RegistryKey OpenSubKey(string subKey, RegDomain regDomain)
		{
			if (subKey == string.Empty || subKey == null)
			{
				return null;
			}
			RegistryKey regDomain2 = this.GetRegDomain(regDomain);
			RegistryKey result = regDomain2.OpenSubKey(subKey);
			regDomain2.Close();
			return result;
		}

		protected virtual RegistryKey OpenSubKey(string subKey, RegDomain regDomain, bool writable)
		{
			if (subKey == string.Empty || subKey == null)
			{
				return null;
			}
			RegistryKey regDomain2 = this.GetRegDomain(regDomain);
			RegistryKey result = regDomain2.OpenSubKey(subKey, writable);
			regDomain2.Close();
			return result;
		}
	}
}
