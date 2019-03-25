using LedModel.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace LedControlSystem.LedControlSystem
{
	public static class GprsAdministrator
	{
		public static bool LoginSuccess = false;

		public static string Message = "";

		public static string Entity_UserType = "";

		public static string Entity_Sign = "";

		public static string Entity_ID = "";

		public static string Entity_Username = "";

		public static bool Entity_IsSupper = false;

		public static string Entity_PhoneNnumber = "";

		public static string Entity_Email = "";

		public static string Entity_Address = "";

		public static string Entity_CreateAt = "";

		public static string Entity_UpdateAt = "";

		public static string Entity_Password = "";

		public static string lastuserid;

		public static string lastUsername;

		public static string lastPassword;

		public static string laseDeviceID;

		public static bool API_AdminUnbindDevice(string pUserID, string pDeviceID)
		{
			string text = formMain.GprsServer + "terminal/unbindToUserByAdmin.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign;
			text = text + "&entity.id=" + pDeviceID;
			text = text + "&user.id=" + pUserID + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			return jArray[0]["success"].ToObject<bool>();
		}

		public static JArray API_GetUserDeviceList()
		{
			string text = formMain.GprsServer + "terminal/listByUser.json?";
			text = text + "user.id=" + GprsAdministrator.Entity_ID;
			text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&user.username=" + GprsAdministrator.Entity_Username;
			text = text + "&user.sign=" + GprsAdministrator.Entity_Sign + GprsAdministrator.GetTimeStamp();
			text += "&Rows=99999";
			string webClient = GprsAdministrator.GetWebClient(text);
			if (webClient == "")
			{
				return null;
			}
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			if (jArray[0]["success"].ToObject<bool>())
			{
				return GprsAdministrator.Json2JArray(jArray[0]["entities"].ToString());
			}
			return null;
		}

		public static JArray API_AdminGetUserInfo(string pUserName)
		{
			string text = formMain.GprsServer + "user/findByUsernameForAdmin.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign + GprsAdministrator.GetTimeStamp();
			text = text + "&entity.username=" + pUserName;
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			if (jArray[0]["success"].ToObject<bool>())
			{
				return GprsAdministrator.Json2JArray(jArray[0]["entity"].ToString());
			}
			return null;
		}

		public static JArray API_AdminGetUserDeviceList(string pUserID)
		{
			string text = formMain.GprsServer + "terminal/listByUserForAdmin.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign + GprsAdministrator.GetTimeStamp();
			text = text + "&user.id=" + pUserID;
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			if (jArray[0]["success"].ToObject<bool>())
			{
				return GprsAdministrator.Json2JArray(jArray[0]["entities"].ToString());
			}
			return null;
		}

		public static bool API_AdminBindDeviceToUser(string pUserID, string pDeviceID)
		{
			string text = formMain.GprsServer + "terminal/bindToUserByAdmin.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign;
			text = text + "&entity.id=" + pDeviceID;
			text = text + "&user.id=" + pUserID + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_ImportDevice(string pDeviceID, string pUserId, string pModel)
		{
			string text = formMain.GprsServer + "terminal/createByAdmin.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign;
			text = text + "&entity.deviceModel=" + pModel;
			text = text + "&entity.originalUserId=" + pUserId;
			text = text + "&entity.terminalCode=" + pDeviceID + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			if (jArray[0]["success"].ToObject<bool>())
			{
				JArray jArray2 = GprsAdministrator.Json2JArray(jArray[0]["entity"].ToString());
				GprsAdministrator.laseDeviceID = jArray2[0]["id"].ToString();
				return true;
			}
			return false;
		}

		public static bool API_CreateUser()
		{
			string text = formMain.GprsServer + "user/generateUser.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			if (jArray[0]["success"].ToObject<bool>())
			{
				JArray jArray2 = GprsAdministrator.Json2JArray(jArray[0]["entity"].ToString());
				GprsAdministrator.lastuserid = jArray2[0]["id"].ToString();
				GprsAdministrator.lastUsername = jArray2[0]["username"].ToString();
				GprsAdministrator.lastPassword = jArray2[0]["password"].ToString();
				return true;
			}
			return false;
		}

		public static bool API_ChangePassword(string pID, string pOldPassword, string pNewPassword)
		{
			string text = formMain.GprsServer + "admin/changePassword.json?";
			text = text + "entity.id=" + GprsAdministrator.Entity_ID;
			text = text + "&oldPassword=" + pOldPassword;
			text = text + "&newPassword=" + pNewPassword + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_UserChangePassword(string pID, string pOldPassword, string pNewPassword)
		{
			string text = formMain.GprsServer + "user/changePassword.json?";
			text = text + "entity.id=" + GprsAdministrator.Entity_ID;
			text = text + "&oldPassword=" + pOldPassword;
			text = text + "&newPassword=" + pNewPassword + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_RemoveAdmin(string pID)
		{
			string text = formMain.GprsServer + "admin/remove.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign;
			text = text + "&entity.id=" + pID + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_CreateNewAdministrator(string pName, string pPassword, string pPhoneNumber, string pEmail, string pAddress)
		{
			string text = formMain.GprsServer + "admin/create.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign;
			text = text + "&entity.username=" + pName;
			text = text + "&entity.password=" + pPassword;
			text = text + "&entity.phoneNumber=" + pPhoneNumber;
			text = text + "&entity.email=" + pEmail;
			text = text + "&entity.address=" + pAddress + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_SingleCommand(string pEntityID, byte[] pCmdData)
		{
			bool result;
			try
			{
				string url = formMain.GprsServer + "terminal/sendSingleCommandByUser.json";
				string text = "user.id=" + GprsAdministrator.Entity_ID;
				text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
				text = text + "&user.username=" + GprsAdministrator.Entity_Username;
				text = text + "&user.sign=" + GprsAdministrator.Entity_Sign;
				text = text + "&entity.id=" + pEntityID;
				text = text + "&base64=" + HttpUtility.UrlEncode(Convert.ToBase64String(pCmdData));
				GprsAdministrator.GetWebClient(url, text);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool API_EditAdministrator(string pID, string pName, string pPassword, string pPhoneNumber, string pEmail, string pAddress)
		{
			string text = formMain.GprsServer + "admin/update.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign;
			text = text + "&entity.id=" + pID;
			text = text + "&entity.username=" + pName;
			text = text + "&entity.phoneNumber=" + pPhoneNumber;
			text = text + "&entity.email=" + pEmail;
			text = text + "&entity.address=" + pAddress + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			return jArray[0]["success"].ToObject<bool>();
		}

		public static JArray API_GetAdminList()
		{
			string text = formMain.GprsServer + "admin/list.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign;
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			if (jArray[0]["success"].ToObject<bool>())
			{
				return GprsAdministrator.Json2JArray(jArray[0]["entities"].ToString());
			}
			return null;
		}

		public static JArray API_GetUserList(int pPage)
		{
			string text = formMain.GprsServer + "user/listByAdmin.json?";
			text = text + "admin.id=" + GprsAdministrator.Entity_ID;
			text = text + "&admin.username=" + GprsAdministrator.Entity_Username;
			text = text + "&admin.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&admin.sign=" + GprsAdministrator.Entity_Sign;
			text = text + "&page=" + pPage.ToString();
			text = text + "&rows=100" + GprsAdministrator.GetTimeStamp();
			string webClient = GprsAdministrator.GetWebClient(text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			if (jArray[0]["success"].ToObject<bool>())
			{
				return GprsAdministrator.Json2JArray(jArray[0]["entities"].ToString());
			}
			return null;
		}

		public static string GetTimeStamp()
		{
			DateTime now = DateTime.Now;
			return string.Concat(new string[]
			{
				"&Time=",
				now.Year.ToString("D2"),
				now.Month.ToString("D2"),
				now.Day.ToString("D2"),
				now.Hour.ToString("D2"),
				now.Minute.ToString("D2"),
				now.Second.ToString("D2"),
				now.Millisecond.ToString("D3")
			});
		}

		public static bool API_AdminLogin(string pUserName, string pPassword)
		{
			string url = formMain.GprsServer + "admin/login.json";
			string param = string.Concat(new string[]
			{
				"entity.username=",
				pUserName,
				"&entity.password=",
				pPassword,
				GprsAdministrator.GetTimeStamp()
			});
			string webClient = GprsAdministrator.GetWebClient(url, param);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			if (jArray == null)
			{
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				JArray jArray2 = GprsAdministrator.Json2JArray("[" + jArray[0]["entity"].ToString() + "]");
				GprsAdministrator.Entity_UserType = jArray2[0]["userType"].ToObject<string>();
				GprsAdministrator.Entity_Sign = jArray2[0]["sign"].ToObject<string>();
				GprsAdministrator.Entity_ID = jArray2[0]["id"].ToObject<string>();
				GprsAdministrator.Entity_Username = jArray2[0]["username"].ToObject<string>();
				GprsAdministrator.Entity_IsSupper = jArray2[0]["isSuper"].ToObject<bool>();
				GprsAdministrator.Entity_PhoneNnumber = jArray2[0]["phoneNumber"].ToObject<string>();
				GprsAdministrator.Entity_Email = jArray2[0]["email"].ToObject<string>();
				GprsAdministrator.Entity_Address = jArray2[0]["address"].ToObject<string>();
				GprsAdministrator.Entity_CreateAt = jArray2[0]["createdAt"].ToObject<string>();
				GprsAdministrator.Entity_UpdateAt = jArray2[0]["updatedAt"].ToObject<string>();
				return true;
			}
			GprsAdministrator.Entity_UserType = "";
			GprsAdministrator.Entity_Sign = "";
			GprsAdministrator.Entity_ID = "";
			GprsAdministrator.Entity_Username = "";
			GprsAdministrator.Entity_IsSupper = false;
			GprsAdministrator.Entity_PhoneNnumber = "";
			GprsAdministrator.Entity_Email = "";
			GprsAdministrator.Entity_Address = "";
			GprsAdministrator.Entity_CreateAt = "";
			GprsAdministrator.Entity_UpdateAt = "";
			GprsAdministrator.Message = jArray[0]["message"].ToString();
			return false;
		}

		public static bool API_UserLogin(string pUserName, string pPassword)
		{
			try
			{
				string url = string.Concat(new string[]
				{
					formMain.GprsServer,
					"user/login.json?entity.username=",
					pUserName,
					"&entity.password=",
					pPassword,
					GprsAdministrator.GetTimeStamp()
				});
				string webClient = GprsAdministrator.GetWebClient(url);
				JArray jArray = GprsAdministrator.Json2JArray(webClient);
				bool result;
				if (jArray == null)
				{
					result = false;
					return result;
				}
				if (jArray[0]["success"].ToObject<bool>())
				{
					JArray jArray2 = GprsAdministrator.Json2JArray("[" + jArray[0]["entity"].ToString() + "]");
					GprsAdministrator.Entity_UserType = jArray2[0]["userType"].ToObject<string>();
					GprsAdministrator.Entity_Sign = jArray2[0]["sign"].ToObject<string>();
					GprsAdministrator.Entity_ID = jArray2[0]["id"].ToObject<string>();
					GprsAdministrator.Entity_Username = jArray2[0]["username"].ToObject<string>();
					GprsAdministrator.Entity_Password = jArray2[0]["password"].ToObject<string>();
					GprsAdministrator.Entity_IsSupper = false;
					result = true;
					return result;
				}
				GprsAdministrator.Entity_UserType = "";
				GprsAdministrator.Entity_Sign = "";
				GprsAdministrator.Entity_ID = "";
				GprsAdministrator.Entity_Username = "";
				GprsAdministrator.Entity_IsSupper = false;
				GprsAdministrator.Entity_PhoneNnumber = "";
				GprsAdministrator.Entity_Email = "";
				GprsAdministrator.Entity_Address = "";
				GprsAdministrator.Entity_CreateAt = "";
				GprsAdministrator.Entity_UpdateAt = "";
				GprsAdministrator.Message = jArray[0]["message"].ToString();
				result = false;
				return result;
			}
			catch
			{
				GprsAdministrator.Message = formMain.ML.GetStr("GPRS_AccessError");
			}
			return false;
		}

		public static bool API_UserUpdateDeviceInfo(string pValueString)
		{
			string url = formMain.GprsServer + "terminal/updateByUser.json";
			string text = "user.id=" + GprsAdministrator.Entity_ID;
			text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&user.username=" + GprsAdministrator.Entity_Username;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"&user.sign=",
				GprsAdministrator.Entity_Sign,
				pValueString,
				GprsAdministrator.GetTimeStamp()
			});
			string webClient = GprsAdministrator.GetWebClient(url, text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_CheckUserAdvancedPassword(string pPassword)
		{
			string url = formMain.GprsServer + "user/verifyAdvancedPassword.json";
			string text = "user.id=" + GprsAdministrator.Entity_ID;
			text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&user.username=" + GprsAdministrator.Entity_Username;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"&user.sign=",
				GprsAdministrator.Entity_Sign,
				"&Time=",
				GprsAdministrator.GetTimeStamp()
			});
			text = text + "&entity.advancedPassword=" + pPassword;
			string webClient = GprsAdministrator.GetWebClient(url, text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_ChangeUserAdvancedPassword(string pOldPassword, string pNewPassword)
		{
			string url = formMain.GprsServer + "user/changeAdvancedPassword.json";
			string text = "user.id=" + GprsAdministrator.Entity_ID;
			text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&user.username=" + GprsAdministrator.Entity_Username;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"&user.sign=",
				GprsAdministrator.Entity_Sign,
				"&Time=",
				GprsAdministrator.GetTimeStamp()
			});
			text = text + "&oldAdvancedPassword=" + pOldPassword;
			text = text + "&newAdvancedPassword=" + pNewPassword;
			string webClient = GprsAdministrator.GetWebClient(url, text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_UserBindNewDevice(string pUserName, string pPassword)
		{
			string url = formMain.GprsServer + "terminal/bindToUserFromUser.json";
			string text = "user.id=" + GprsAdministrator.Entity_ID;
			text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&user.username=" + GprsAdministrator.Entity_Username;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"&user.sign=",
				GprsAdministrator.Entity_Sign,
				"&Time=",
				GprsAdministrator.GetTimeStamp()
			});
			text = text + "&sourceUsername=" + pUserName;
			text = text + "&sourcePassword=" + pPassword;
			string webClient = GprsAdministrator.GetWebClient(url, text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_UserUnBindNewDevice(string pDevideID)
		{
			string url = formMain.GprsServer + "terminal/unbindFromSelf.json";
			string text = "user.id=" + GprsAdministrator.Entity_ID;
			text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&user.username=" + GprsAdministrator.Entity_Username;
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"&user.sign=",
				GprsAdministrator.Entity_Sign,
				"&Time=",
				GprsAdministrator.GetTimeStamp()
			});
			text = text + "&entity.id=" + pDevideID;
			string webClient = GprsAdministrator.GetWebClient(url, text);
			JArray jArray = GprsAdministrator.Json2JArray(webClient);
			return jArray[0]["success"].ToObject<bool>();
		}

		public static bool API_UPLoadFile2M(string pDeviceID, string pFileName, int pPackgeLength)
		{
			bool result;
			try
			{
				pDeviceID = pDeviceID.Substring(0, pDeviceID.Length - 1);
				string url = formMain.GprsServer + "terminal/uploadBase64TransData2MultiTerminalByUser.json";
				string text = "user.id=" + GprsAdministrator.Entity_ID;
				text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
				text = text + "&user.username=" + GprsAdministrator.Entity_Username;
				text = text + "&user.sign=" + GprsAdministrator.Entity_Sign;
				text = text + "&entity.id=" + pDeviceID.Replace(",", "&entity.id=");
				text = text + "&entity.frameLength=" + pPackgeLength.ToString();
				FileStream fileStream = new FileStream(pFileName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, Convert.ToInt32(fileStream.Length));
				fileStream.Close();
				text = text + "&base64=" + HttpUtility.UrlEncode(Convert.ToBase64String(array));
				GprsAdministrator.GetWebClient(url, text);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool API_UPLoadFile2(string pDeviceID, string pFileName, int pPackgeLength)
		{
			bool result;
			try
			{
				string url = formMain.GprsServer + "terminal/uploadBase64TransDataByUser.json";
				string text = "user.id=" + GprsAdministrator.Entity_ID;
				text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
				text = text + "&user.username=" + GprsAdministrator.Entity_Username;
				text = text + "&user.sign=" + GprsAdministrator.Entity_Sign;
				text = text + "&entity.id=" + pDeviceID;
				text = text + "&entity.frameLength=" + pPackgeLength.ToString();
				FileStream fileStream = new FileStream(pFileName, FileMode.Open, FileAccess.Read);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, Convert.ToInt32(fileStream.Length));
				fileStream.Close();
				string str = HttpUtility.UrlEncode(Convert.ToBase64String(array));
				text = text + "&base64=" + str;
				GprsAdministrator.GetWebClient(url, text);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool API_UPLoadRoutingData(string pDeviceID, byte[] pRoutingData)
		{
			bool result;
			try
			{
				string url = formMain.GprsServer + "terminal/uploadBase64RoutingDataByUser.json";
				string text = "user.id=" + GprsAdministrator.Entity_ID;
				text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
				text = text + "&user.username=" + GprsAdministrator.Entity_Username;
				text = text + "&user.sign=" + GprsAdministrator.Entity_Sign;
				text = text + "&entity.id=" + pDeviceID;
				text = text + "&base64=" + HttpUtility.UrlEncode(Convert.ToBase64String(pRoutingData));
				GprsAdministrator.GetWebClient(url, text);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static byte[] API_DownLoadRoutingData(string pDeviceID)
		{
			string address = formMain.GprsServer + "terminal/downloadRoutingDataByUser.json";
			string text = "user.id=" + GprsAdministrator.Entity_ID;
			text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
			text = text + "&user.username=" + GprsAdministrator.Entity_Username;
			text = text + "&user.sign=" + GprsAdministrator.Entity_Sign;
			text = text + "&entity.id=" + pDeviceID;
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			return new WebClient
			{
				Headers = 
				{
					{
						"Content-Type",
						"application/x-www-form-urlencoded"
					}
				}
			}.UploadData(address, "POST", bytes);
		}

		public static bool API_DownFile(string pDeviceID, string pFile)
		{
			string address = formMain.GprsServer + "terminal/downloadTransData.json";
			string s = "&entity.id=" + pDeviceID;
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] array = new WebClient
			{
				Headers = 
				{
					{
						"Content-Type",
						"application/x-www-form-urlencoded"
					}
				}
			}.UploadData(address, "POST", bytes);
			Encoding.UTF8.GetString(array);
			FileStream fileStream = new FileStream(pFile, FileMode.Create, FileAccess.Write);
			fileStream.Write(array, 0, array.Length);
			fileStream.Close();
			return true;
		}

		public static byte[] API_DownSingleCommand(string pDeviceID, LedCmdType pCMD)
		{
			byte[] result;
			try
			{
				string address = formMain.GprsServer + "terminal/downloadSingleCommandByUser.json?timer=" + GprsAdministrator.GetTimeStamp();
				string text = "&user.id=" + GprsAdministrator.Entity_ID;
				text = text + "&user.userType=" + GprsAdministrator.Entity_UserType;
				text = text + "&user.username=" + GprsAdministrator.Entity_Username;
				text = text + "&user.sign=" + GprsAdministrator.Entity_Sign;
				text = text + "&entity.id=" + pDeviceID;
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				byte[] array = new WebClient
				{
					Headers = 
					{
						{
							"Content-Type",
							"application/x-www-form-urlencoded"
						}
					}
				}.UploadData(address, "POST", bytes);
				if (array.Length == 44)
				{
					byte[] array2 = new byte[1];
					result = array2;
				}
				else
				{
					result = array;
				}
			}
			catch
			{
				byte[] array3 = new byte[1];
				result = array3;
			}
			return result;
		}

		public static string POSTfile(string pPosturl, string pAdminID, string pAdminUsername, string pAdminUserType, string pSign, string pEntityID, string pFileName)
		{
			string str = "—————————" + DateTime.Now.Ticks.ToString("x");
			WebRequest webRequest = WebRequest.Create(pPosturl);
			webRequest.Method = "POST";
			webRequest.ContentType = "multipart/form-data";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("–" + str);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Disposition: form-data; name=\"admin.id\"");
			stringBuilder.Append("\r\n\r\n");
			stringBuilder.Append(pAdminID);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("–" + str);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Disposition: form-data; name=\"admin.username\"");
			stringBuilder.Append("\r\n\r\n");
			stringBuilder.Append(pAdminUsername);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("–" + str);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Disposition: form-data; name=\"admin.userType\"");
			stringBuilder.Append("\r\n\r\n");
			stringBuilder.Append(pAdminUserType);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("–" + str);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Disposition: form-data; name=\"admin.sign\"");
			stringBuilder.Append("\r\n\r\n");
			stringBuilder.Append(pSign);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("–" + str);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Disposition: form-data; name=\"entity.id\"");
			stringBuilder.Append("\r\n\r\n");
			stringBuilder.Append(pEntityID);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("–" + str);
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Disposition: form-data; name=\"transData\"; filename=\"" + pFileName + "\"");
			stringBuilder.Append("\r\n");
			stringBuilder.Append("Content-Type: image/pjpeg");
			stringBuilder.Append("\r\n\r\n");
			string s = stringBuilder.ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] bytes2 = Encoding.UTF8.GetBytes("\r\n–" + str + "–\r\n");
			FileStream fileStream = new FileStream(pFileName, FileMode.Open, FileAccess.Read);
			long contentLength = (long)bytes.Length + fileStream.Length + (long)bytes2.Length;
			webRequest.ContentLength = contentLength;
			Stream requestStream = webRequest.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			byte[] array = new byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
			int count;
			while ((count = fileStream.Read(array, 0, array.Length)) != 0)
			{
				requestStream.Write(array, 0, count);
			}
			requestStream.Write(bytes2, 0, bytes2.Length);
			requestStream.Close();
			WebResponse response = webRequest.GetResponse();
			StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
			string result = streamReader.ReadToEnd().Trim();
			streamReader.Close();
			if (response != null)
			{
				response.Close();
			}
			if (webRequest != null)
			{
			}
			return result;
		}

		public static JArray Json2JArray(string Jason)
		{
			if (!Jason.StartsWith("["))
			{
				Jason = "[" + Jason + "]";
			}
			return (JArray)JsonConvert.DeserializeObject(Jason);
		}

		private static string GetWebClient(string url)
		{
			string result;
			try
			{
				WebClient webClient = new WebClient();
				Stream stream = webClient.OpenRead(url);
				StreamReader streamReader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
				string text = streamReader.ReadToEnd();
				stream.Close();
				result = text;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private static string GetWebClient(string url, string param)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(param);
				byte[] bytes2 = new WebClient
				{
					Headers = 
					{
						{
							"Content-Type",
							"application/x-www-form-urlencoded"
						}
					}
				}.UploadData(url, "POST", bytes);
				string @string = Encoding.UTF8.GetString(bytes2);
				result = @string;
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
