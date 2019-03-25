using LedControlSystem.LedControlSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace LedControlSystem.CloudServer
{
	public class CloudServerComm
	{
		private string cloudserver = "http://139.196.192.112:8080/zcc/api/1.0/";

		private int RequestTimeout = 2000;

		private string contentTypeData = "application/x-www-form-urlencoded";

		public string contentTypeFile = "multipart/form-data";

		public static CloudServerUser CurrentUser = new CloudServerUser();

		public IList<string> TransdataIdList = new List<string>();

		public IList<string> TerminalIdList = new List<string>();

		private string SetHttpWebRequestPost(string url, string StrcontentType, string body, bool IsHaveCookie, bool IsHaveContentType)
		{
			string result;
			try
			{
				HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
				httpWebRequest.Method = "POST";
				httpWebRequest.Timeout = this.RequestTimeout;
				if (IsHaveContentType)
				{
					httpWebRequest.ContentType = StrcontentType;
				}
				if (IsHaveCookie)
				{
					httpWebRequest.CookieContainer = new CookieContainer();
					Uri uri = new Uri(url);
					httpWebRequest.CookieContainer.SetCookies(uri, "JSESSIONID=" + CloudServerComm.CurrentUser.SessionId);
				}
				if (body != string.Empty)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(body);
					httpWebRequest.ContentLength = (long)bytes.Length;
					httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
				}
				HttpWebResponse httpWebResponse;
				try
				{
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				}
				catch (WebException ex)
				{
					httpWebResponse = (HttpWebResponse)ex.Response;
				}
				StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
				string text = string.Empty;
				text = streamReader.ReadToEnd();
				result = text;
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		private string SetHttpWebRequestDelete(string url)
		{
			string result;
			try
			{
				HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
				httpWebRequest.Method = "DELETE";
				httpWebRequest.Timeout = this.RequestTimeout;
				httpWebRequest.CookieContainer = new CookieContainer();
				Uri uri = new Uri(url);
				httpWebRequest.CookieContainer.SetCookies(uri, "JSESSIONID=" + CloudServerComm.CurrentUser.SessionId);
				HttpWebResponse httpWebResponse;
				try
				{
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				}
				catch (WebException ex)
				{
					httpWebResponse = (HttpWebResponse)ex.Response;
				}
				StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
				string text = string.Empty;
				text = streamReader.ReadToEnd();
				result = text;
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		private string SetHttpWebRequestGet(string url)
		{
			string result;
			try
			{
				HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
				httpWebRequest.Method = "GET";
				httpWebRequest.Timeout = this.RequestTimeout;
				httpWebRequest.ContentType = this.contentTypeData;
				httpWebRequest.CookieContainer = new CookieContainer();
				Uri uri = new Uri(url);
				httpWebRequest.CookieContainer.SetCookies(uri, "JSESSIONID=" + CloudServerComm.CurrentUser.SessionId);
				HttpWebResponse httpWebResponse;
				try
				{
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				}
				catch (WebException ex)
				{
					httpWebResponse = (HttpWebResponse)ex.Response;
				}
				StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
				string text = string.Empty;
				text = streamReader.ReadToEnd();
				result = text;
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		private string SetHttpWebRequestPut(string url, string body, bool IsHaveContentType)
		{
			string result;
			try
			{
				HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
				httpWebRequest.Method = "PUT";
				httpWebRequest.Timeout = this.RequestTimeout;
				if (IsHaveContentType)
				{
					httpWebRequest.ContentType = this.contentTypeData.ToString();
				}
				httpWebRequest.CookieContainer = new CookieContainer();
				Uri uri = new Uri(url);
				httpWebRequest.CookieContainer.SetCookies(uri, "JSESSIONID=" + CloudServerComm.CurrentUser.SessionId);
				if (body != string.Empty)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(body);
					httpWebRequest.ContentLength = (long)bytes.Length;
					httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
				}
				HttpWebResponse httpWebResponse;
				try
				{
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				}
				catch (WebException ex)
				{
					httpWebResponse = (HttpWebResponse)ex.Response;
				}
				StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
				string text = string.Empty;
				text = streamReader.ReadToEnd();
				result = text;
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		public bool API_Login_Administrator(string pUserName, string pPassword)
		{
			bool result;
			try
			{
				string requestUriString = this.cloudserver + "accounts/login";
				string text = "username=";
				text += pUserName;
				text = text + "&password=" + pPassword;
				HttpWebRequest httpWebRequest = WebRequest.Create(requestUriString) as HttpWebRequest;
				httpWebRequest.Method = "POST";
				httpWebRequest.Timeout = 500;
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				if (text != string.Empty)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(text);
					httpWebRequest.ContentLength = (long)bytes.Length;
					httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
				}
				HttpWebResponse httpWebResponse;
				try
				{
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				}
				catch (WebException ex)
				{
					httpWebResponse = (HttpWebResponse)ex.Response;
				}
				StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
				string jason = string.Empty;
				jason = streamReader.ReadToEnd();
				JArray jArray = this.JsonToJArray(jason);
				if (jArray == null)
				{
					result = false;
				}
				else if (jArray[0]["success"].ToObject<bool>())
				{
					JArray jArray2 = this.JsonToJArray("[" + jArray[0]["entity"].ToString() + "]");
					CloudServerComm.CurrentUser.UserName = jArray2[0]["username"].ToObject<string>();
					CloudServerComm.CurrentUser.UserId = jArray2[0]["id"].ToObject<string>();
					CloudServerComm.CurrentUser.SessionId = jArray2[0]["sessionId"].ToObject<string>();
					CloudServerComm.CurrentUser.PhoneNumber = jArray2[0]["phoneNumber"].ToObject<string>();
					CloudServerComm.CurrentUser.NickName = jArray2[0]["nickname"].ToObject<string>();
					CloudServerComm.CurrentUser.CompanyName = jArray2[0]["companyName"].ToObject<string>();
					CloudServerComm.CurrentUser.Address = jArray2[0]["address"].ToObject<string>();
					CloudServerComm.CurrentUser.Email = jArray2[0]["email"].ToObject<string>();
					CloudServerComm.CurrentUser.CreatedAt = jArray2[0]["createdAt"].ToObject<string>();
					CloudServerComm.CurrentUser.UpdateAt = jArray2[0]["updatedAt"].ToObject<string>();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public bool API_SignOut()
		{
			string url = this.cloudserver + "accounts/logout";
			string text = this.SetHttpWebRequestPost(url, this.contentTypeData, string.Empty, true, false);
			if (text != string.Empty)
			{
				JArray jArray = this.JsonToJArray(text);
				if (jArray == null)
				{
					return false;
				}
				if (jArray[0]["success"].ToObject<bool>())
				{
					return true;
				}
			}
			return false;
		}

		public bool API_UserLogin(string pAccountUserName, string pUserName, string pPassword)
		{
			bool result;
			try
			{
				string requestUriString = this.cloudserver + "users/login";
				string text = "accountUsername=";
				text += pAccountUserName;
				text = text + "&username=" + pUserName;
				text = text + "&password=" + pPassword;
				HttpWebRequest httpWebRequest = WebRequest.Create(requestUriString) as HttpWebRequest;
				httpWebRequest.Method = "POST";
				httpWebRequest.Timeout = 500;
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				if (text != string.Empty)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(text);
					httpWebRequest.ContentLength = (long)bytes.Length;
					httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
				}
				HttpWebResponse httpWebResponse;
				try
				{
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				}
				catch (WebException ex)
				{
					httpWebResponse = (HttpWebResponse)ex.Response;
				}
				StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
				string jason = string.Empty;
				jason = streamReader.ReadToEnd();
				JArray jArray = this.JsonToJArray(jason);
				if (jArray == null)
				{
					result = false;
				}
				else if (jArray[0]["success"].ToObject<bool>())
				{
					JArray jArray2 = this.JsonToJArray("[" + jArray[0]["entity"].ToString() + "]");
					CloudServerComm.CurrentUser.UserName = jArray2[0]["username"].ToObject<string>();
					CloudServerComm.CurrentUser.UserId = jArray2[0]["id"].ToObject<string>();
					CloudServerComm.CurrentUser.SessionId = jArray2[0]["sessionId"].ToObject<string>();
					CloudServerComm.CurrentUser.PhoneNumber = jArray2[0]["phoneNumber"].ToObject<string>();
					CloudServerComm.CurrentUser.NickName = jArray2[0]["nickname"].ToObject<string>();
					CloudServerComm.CurrentUser.Email = jArray2[0]["email"].ToObject<string>();
					CloudServerComm.CurrentUser.CreatedAt = jArray2[0]["createdAt"].ToObject<string>();
					CloudServerComm.CurrentUser.UpdateAt = jArray2[0]["updatedAt"].ToObject<string>();
					jArray2[0]["statusStr"].ToObject<string>();
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public IList<TerminalData> API_TerminalList()
		{
			IList<TerminalData> list = new List<TerminalData>();
			string httpWebRequestGet = this.cloudserver + "terminals";
			string text = this.SetHttpWebRequestGet(httpWebRequestGet);
			if (text != string.Empty)
			{
				JArray jArray = this.JsonToJArray(text);
				if (jArray == null)
				{
					return null;
				}
				JArray jArray2 = this.JsonToJArray("[" + jArray[0]["page"].ToString() + "]");
				JArray jArray3 = (JArray)JsonConvert.DeserializeObject(jArray2[0]["content"].ToString());
				if (jArray3.Count <= 0)
				{
					return null;
				}
				this.TerminalIdList.Clear();
				for (int i = 0; i < jArray3.Count; i++)
				{
					TerminalData terminalData = new TerminalData();
					if (jArray3[i]["contact"] != null)
					{
						JArray jArray4 = this.JsonToJArray("[" + jArray3[i]["contact"].ToString() + "]");
						jArray4[0]["id"].ToObject<string>();
						jArray4[0]["createdAt"].ToObject<string>();
						jArray4[0]["updatedAt"].ToObject<string>();
						jArray4[0]["contactName"].ToObject<string>();
						jArray4[0]["phoneNumber"].ToObject<string>();
						jArray4[0]["mobileNumber"].ToObject<string>();
						jArray4[0]["email"].ToObject<string>();
						jArray4[0]["faxNumber"].ToObject<string>();
						jArray4[0]["address"].ToObject<string>();
						jArray4[0]["comment"].ToObject<string>();
					}
					terminalData.Id = jArray3[i]["id"].ToObject<string>();
					terminalData.CreatedAt = jArray3[i]["createdAt"].ToObject<string>();
					terminalData.UpdatedAt = jArray3[i]["updatedAt"].ToObject<string>();
					terminalData.TerminalCode = jArray3[i]["terminalCode"].ToObject<string>();
					terminalData.TerminalName = jArray3[i]["terminalName"].ToObject<string>();
					terminalData.Width = jArray3[i]["width"].ToObject<string>();
					terminalData.Height = jArray3[i]["height"].ToObject<string>();
					terminalData.Color = jArray3[i]["color"].ToObject<string>();
					terminalData.Gray = jArray3[i]["gray"].ToObject<string>();
					terminalData.BackGray = jArray3[i]["backGray"].ToObject<string>();
					terminalData.DeviceModel = jArray3[i]["deviceModel"].ToObject<string>();
					terminalData.PhoneNumber = jArray3[i]["phoneNumber"].ToObject<string>();
					terminalData.DeviceVersion = jArray3[i]["deviceVersion"].ToObject<string>();
					terminalData.Description = jArray3[i]["description"].ToObject<string>();
					terminalData.LastHeartbeat = jArray3[i]["latitude"].ToObject<string>();
					terminalData.CommModel = jArray3[i]["commModel"].ToObject<string>();
					terminalData.ProductSubmodel = jArray3[i]["productSubmodel"].ToObject<string>();
					terminalData.ProductExtra = jArray3[i]["productExtra"].ToObject<string>();
					if (jArray3[i]["online"].ToObject<string>() == "False")
					{
						terminalData.Online = false;
					}
					else
					{
						terminalData.Online = true;
					}
					terminalData.Ip = jArray3[i]["ip"].ToObject<string>();
					terminalData.Port = jArray3[i]["port"].ToObject<string>();
					terminalData.LastHeartbeat = jArray3[i]["lastHeartbeat"].ToObject<string>();
					terminalData.SignalStrength = jArray3[i]["signalStrength"].ToObject<string>();
					terminalData.TotalCapacity = jArray3[i]["totalCapacity"].ToObject<string>();
					terminalData.UsedCapacity = jArray3[i]["usedCapacity"].ToObject<string>();
					terminalData.ProductModelDescription = jArray3[i]["productModelDescription"].ToObject<string>();
					terminalData.TotalCapacityStr = jArray3[i]["totalCapacityStr"].ToObject<string>();
					terminalData.UsedCapacityStr = jArray3[i]["usedCapacityStr"].ToObject<string>();
					terminalData.FreeCapacity = jArray3[i]["freeCapacity"].ToObject<string>();
					terminalData.FreeCapacityStr = jArray3[i]["freeCapacityStr"].ToObject<string>();
					if (terminalData.ProductModelDescription.IndexOf("单双色") > -1 || terminalData.ProductModelDescription.IndexOf("未知型号") > -1)
					{
						list.Add(terminalData);
						this.TerminalIdList.Add(terminalData.Id);
					}
				}
			}
			return list;
		}

		public IList<TransData> API_GetTransdatas(string terminalId)
		{
			IList<TransData> list = new List<TransData>();
			string httpWebRequestGet = this.cloudserver + "terminals/" + terminalId + "/transdatas";
			string text = this.SetHttpWebRequestGet(httpWebRequestGet);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return null;
			}
			this.TransdataIdList.Clear();
			if (jArray[0]["success"].ToObject<bool>())
			{
				JArray jArray2 = this.JsonToJArray("[" + jArray[0]["page"].ToString() + "]");
				JArray jArray3 = (JArray)JsonConvert.DeserializeObject(jArray2[0]["content"].ToString());
				for (int i = 0; i < jArray3.Count; i++)
				{
					TransData transData = new TransData();
					string item = jArray3[i]["id"].ToObject<string>();
					this.TransdataIdList.Add(item);
					transData.ID = jArray3[i]["id"].ToObject<string>();
					transData.CreatedAt = jArray3[i]["createdAt"].ToObject<DateTime>().ToString();
					transData.UpdatedAt = jArray3[i]["updatedAt"].ToObject<string>();
					transData.Description = jArray3[i]["description"].ToObject<string>();
					transData.Status = (TransDataStatus)jArray3[i]["status"].ToObject<int>();
					if (jArray3[i]["frameLength"].ToObject<string>() != null)
					{
						transData.FrameLength = jArray3[i]["frameLength"].ToObject<int>();
					}
					list.Add(transData);
				}
				return list;
			}
			return null;
		}

		public TerminalResponseResult API_PostTransdatas_DataBase64(string terminalId, string body)
		{
			string url = this.cloudserver + "terminals/" + terminalId + "/transdatas";
			string text = this.SetHttpWebRequestPost(url, this.contentTypeData, body, true, true);
			TerminalResponseResult terminalResponseResult = new TerminalResponseResult();
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				terminalResponseResult.ResultDisplay = formMain.ML.GetStr("formCloudServerSend_Dgv_Message_SendFailure");
				terminalResponseResult.Result = false;
				return terminalResponseResult;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				JArray jArray2 = this.JsonToJArray("[" + jArray[0]["entity"].ToString() + "]");
				terminalResponseResult.IsNeedReview = jArray2[0]["pendingTransData"].ToObject<bool>();
				terminalResponseResult.ReviewId = jArray2[0]["id"].ToObject<string>();
				terminalResponseResult.ResultDisplay = formMain.ML.GetStr("formCloudServerSend_Dgv_Message_SendSuccessed");
				terminalResponseResult.Result = true;
				return terminalResponseResult;
			}
			terminalResponseResult.ResultDisplay = formMain.ML.GetStr("formCloudServerSend_Dgv_Message_SendFailure");
			terminalResponseResult.Result = false;
			return terminalResponseResult;
		}

		public bool API_PostTransdatas_fileData(string terminalId, string path, string fileName)
		{
			string text = this.cloudserver + "terminals/" + terminalId + "/transdatas";
			HttpWebRequest httpWebRequest = WebRequest.Create(text) as HttpWebRequest;
			httpWebRequest.AllowAutoRedirect = true;
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = this.contentTypeFile;
			httpWebRequest.CookieContainer = new CookieContainer();
			Uri uri = new Uri(text);
			httpWebRequest.CookieContainer.SetCookies(uri, "JSESSIONID=" + CloudServerComm.CurrentUser.SessionId);
			string str = DateTime.Now.Ticks.ToString("X");
			byte[] bytes = Encoding.UTF8.GetBytes("\r\n--" + str + "\r\n");
			byte[] bytes2 = Encoding.UTF8.GetBytes("\r\n--" + str + "--\r\n");
			StringBuilder stringBuilder = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
			byte[] bytes3 = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			Stream requestStream = httpWebRequest.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			requestStream.Write(bytes3, 0, bytes3.Length);
			requestStream.Write(array, 0, array.Length);
			requestStream.Write(bytes2, 0, bytes2.Length);
			requestStream.Close();
			try
			{
				HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
				httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
				string jason = string.Empty;
				jason = streamReader.ReadToEnd();
				this.JsonToJArray(jason);
			}
			catch
			{
				Console.WriteLine("Is my fault");
				return false;
			}
			return true;
		}

		public bool API_DeleteTransdatas(string terminalId, string transDataId)
		{
			string httpWebRequestDelete = string.Concat(new string[]
			{
				this.cloudserver,
				"terminals/",
				terminalId,
				"/transdatas/",
				transDataId
			});
			string text = this.SetHttpWebRequestDelete(httpWebRequestDelete);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				jArray[0]["message"].ToObject<string>();
				return true;
			}
			return false;
		}

		public bool API_GetRequestList()
		{
			string httpWebRequestGet = this.cloudserver + "/pendingtransdatas";
			string text = this.SetHttpWebRequestGet(httpWebRequestGet);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return false;
			}
			if (jArray.Count > 0)
			{
				for (int i = 0; i < jArray.Count; i++)
				{
					JArray jArray2 = this.JsonToJArray("[" + jArray[i]["terminal"].ToString() + "]");
					jArray2[0]["id"].ToObject<string>();
					jArray2[0]["terminalCode"].ToObject<string>();
					jArray2[0]["terminalName"].ToObject<string>();
					jArray[i]["id"].ToObject<string>();
					jArray[i]["createdAt"].ToObject<string>();
					jArray[i]["updatedAt"].ToObject<string>();
					jArray[i]["group"].ToObject<string>();
					jArray[i]["description"].ToObject<string>();
					jArray[i]["frameLength"].ToObject<string>();
					jArray[i]["data"].ToObject<string>();
					jArray[i]["resources"].ToObject<string>();
					jArray[i]["status"].ToObject<string>();
					jArray[i]["createdBy"].ToObject<string>();
					jArray[i]["checkedBy"].ToObject<string>();
					jArray[i]["checkedAt"].ToObject<string>();
					jArray[i]["comment"].ToObject<string>();
				}
				return true;
			}
			return false;
		}

		public bool API_ThroughRequest(string requestId)
		{
			string url = this.cloudserver + "/pendingtransdatas/" + requestId + "/pass";
			string text = this.SetHttpWebRequestPut(url, string.Empty, false);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				jArray[0]["message"].ToObject<string>();
				return true;
			}
			return false;
		}

		public bool API_Login_Users(string accountUsername, string userName, string password)
		{
			string url = this.cloudserver + "users/login";
			string body = string.Concat(new string[]
			{
				"accountUsername=",
				accountUsername,
				"&username=",
				userName,
				"&password=",
				password
			});
			string text = this.SetHttpWebRequestPost(url, this.contentTypeData, body, true, true);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				jArray[0]["message"].ToObject<string>();
				jArray[0]["errorCode"].ToObject<string>();
				JArray jArray2 = this.JsonToJArray("[" + jArray[0]["entity"].ToString() + "]");
				jArray2[0]["id"].ToObject<string>();
				jArray2[0]["createdAt"].ToObject<string>();
				jArray2[0]["updatedAt"].ToObject<string>();
				jArray2[0]["username"].ToObject<string>();
				jArray2[0]["nickname"].ToObject<string>();
				jArray2[0]["email"].ToObject<string>();
				jArray2[0]["phoneNumber"].ToObject<string>();
				jArray2[0]["status"].ToObject<int>();
				jArray2[0]["sessionId"].ToObject<string>();
				jArray2[0]["statusStr"].ToObject<string>();
				return true;
			}
			return false;
		}

		public IList<SingleCmdRecord> API_GetTerminalCmd(string terminalDataId)
		{
			IList<SingleCmdRecord> list = new List<SingleCmdRecord>();
			string httpWebRequestGet = this.cloudserver + "terminals/" + terminalDataId + "/singlecommands?order=DESC&size=100";
			string text = this.SetHttpWebRequestGet(httpWebRequestGet);
			JArray jArray = this.JsonToJArray(text);
			JArray jArray2 = this.JsonToJArray("[" + jArray[0]["page"].ToString() + "]");
			JArray jArray3 = (JArray)JsonConvert.DeserializeObject(jArray2[0]["content"].ToString());
			if (jArray == null || text == string.Empty)
			{
				return null;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				for (int i = 0; i < jArray3.Count; i++)
				{
					list.Add(new SingleCmdRecord
					{
						Id = jArray3[i]["id"].ToObject<string>(),
						CreatedAt = jArray3[i]["createdAt"].ToObject<string>(),
						UpdatedAt = jArray3[i]["updatedAt"].ToObject<string>(),
						Description = jArray3[i]["description"].ToObject<string>(),
						Request = jArray3[i]["request"].ToObject<string>(),
						Response = jArray3[i]["response"].ToObject<string>()
					});
				}
				return list;
			}
			return null;
		}

		public IList<SingleCmdRecord> API_GetTerminalCmd(string terminalDataId, string parameterSize)
		{
			IList<SingleCmdRecord> list = new List<SingleCmdRecord>();
			string httpWebRequestGet = this.cloudserver + "terminals/" + terminalDataId + "/singlecommands?order=DESC";
			string text = this.SetHttpWebRequestGet(httpWebRequestGet);
			JArray jArray = this.JsonToJArray(text);
			JArray jArray2 = this.JsonToJArray("[" + jArray[0]["page"].ToString() + "]");
			JArray jArray3 = (JArray)JsonConvert.DeserializeObject(jArray2[0]["content"].ToString());
			if (jArray == null || text == string.Empty)
			{
				return null;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				for (int i = 0; i < jArray3.Count; i++)
				{
					list.Add(new SingleCmdRecord
					{
						Id = jArray3[i]["id"].ToObject<string>(),
						CreatedAt = jArray3[i]["createdAt"].ToObject<string>(),
						UpdatedAt = jArray3[i]["updatedAt"].ToObject<string>(),
						Description = jArray3[i]["description"].ToObject<string>(),
						Request = jArray3[i]["request"].ToObject<string>(),
						Response = jArray3[i]["response"].ToObject<string>()
					});
				}
				return list;
			}
			return null;
		}

		public bool API_SendSingleCmd(string terminalId, string body)
		{
			string url = this.cloudserver + "/terminals/" + terminalId + "/singlecommands";
			string text = this.SetHttpWebRequestPost(url, this.contentTypeData, body, true, true);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				jArray[0]["message"].ToObject<string>();
				return true;
			}
			return false;
		}

		public bool API_ReSendSingleCmd(string terminalId, string SingleCmdId)
		{
			string url = string.Concat(new string[]
			{
				this.cloudserver,
				"terminals/",
				terminalId,
				"/singlecommands",
				SingleCmdId,
				"/resend"
			});
			string text = this.SetHttpWebRequestPut(url, string.Empty, false);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				jArray[0]["message"].ToObject<string>();
				return true;
			}
			return false;
		}

		public bool API_InitSliceUpload(string filekey, string relatedId)
		{
			string url = this.cloudserver + "/upload/init";
			string body = "fileKey=" + filekey + "module=resource&relatedId=" + relatedId;
			string text = this.SetHttpWebRequestPost(url, this.contentTypeData, body, false, true);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				JArray jArray2 = this.JsonToJArray("[" + jArray[0]["entity"].ToString() + "]");
				jArray2[0]["id"].ToObject<string>();
				jArray2[0]["relatedId"].ToObject<string>();
				jArray2[0]["uploadId"].ToObject<string>();
				return true;
			}
			return false;
		}

		public bool API_SliceUpload(string filekey, string partNumber, byte[] data)
		{
			string url = this.cloudserver + "/upload";
			string body = "fileKey=" + filekey + "partNumber=" + partNumber;
			string text = this.SetHttpWebRequestPost(url, this.contentTypeData, body, false, false);
			JArray jArray = this.JsonToJArray(text);
			return jArray != null && !(text == string.Empty) && jArray[0]["success"].ToObject<bool>();
		}

		public bool API_CompletedSliceUpload(string filekey)
		{
			string url = this.cloudserver + "/upload/complete";
			string body = "fileKey=" + filekey;
			string text = this.SetHttpWebRequestPut(url, body, false);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				JArray jArray2 = this.JsonToJArray("[" + jArray[0]["entity"].ToString() + "]");
				jArray2[0]["downloadURL"].ToObject<string>();
				return true;
			}
			return false;
		}

		public IList<GroupOfCloudServer> API_GetGroupsList()
		{
			string httpWebRequestGet = this.cloudserver + "/groups";
			string text = this.SetHttpWebRequestGet(httpWebRequestGet);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				return null;
			}
			JArray jArray2 = this.JsonToJArray("[" + jArray[0]["page"].ToString() + "]");
			if (jArray[0]["success"].ToObject<bool>())
			{
				IList<GroupOfCloudServer> list = new List<GroupOfCloudServer>();
				for (int i = 0; i < jArray2.Count; i++)
				{
					JArray jArray3 = (JArray)JsonConvert.DeserializeObject(jArray2[i]["content"].ToString());
					for (int j = 0; j < jArray3.Count; j++)
					{
						GroupOfCloudServer groupOfCloudServer = new GroupOfCloudServer();
						groupOfCloudServer.Id = jArray3[j]["id"].ToObject<string>();
						groupOfCloudServer.CreatedAt = jArray3[j]["createdAt"].ToObject<string>();
						groupOfCloudServer.UpdatedAt = jArray3[j]["updatedAt"].ToObject<string>();
						groupOfCloudServer.GroupName = jArray3[j]["groupName"].ToObject<string>();
						groupOfCloudServer.CustomerName = jArray3[j]["customerName"].ToObject<string>();
						JArray jArray4 = this.JsonToJArray("[" + jArray3[j]["contact"].ToString() + "]");
						groupOfCloudServer.GroupContact = new GroupContactInfo
						{
							Id = jArray4[0]["id"].ToObject<string>(),
							CreatedAt = jArray4[0]["createdAt"].ToObject<string>(),
							UpdateAt = jArray4[0]["updatedAt"].ToObject<string>(),
							ContactName = jArray4[0]["contactName"].ToObject<string>(),
							PhoneNumber = jArray4[0]["phoneNumber"].ToObject<string>(),
							MoblieNumber = jArray4[0]["mobileNumber"].ToObject<string>(),
							Email = jArray4[0]["email"].ToObject<string>(),
							FaxNumber = jArray4[0]["faxNumber"].ToObject<string>(),
							Address = jArray4[0]["address"].ToObject<string>(),
							Comment = jArray4[0]["comment"].ToObject<string>()
						};
						JArray jArray5 = (JArray)JsonConvert.DeserializeObject(jArray3[j]["terminals"].ToString());
						for (int k = 0; k < jArray5.Count; k++)
						{
							GroupTerminal groupTerminal = new GroupTerminal();
							groupTerminal.Id = jArray5[k]["id"].ToObject<string>();
							groupTerminal.terminalCode = jArray5[k]["terminalCode"].ToObject<string>();
							groupTerminal.terminalName = jArray5[k]["terminalName"].ToObject<string>();
							groupOfCloudServer.GroupTerminals.Add(groupTerminal);
						}
						list.Add(groupOfCloudServer);
					}
				}
				return list;
			}
			return null;
		}

		public bool API_PostGroupTransdatas_DataBase64(string groupId, string body, out string SendResult)
		{
			string url = this.cloudserver + "groups/" + groupId + "/transdatas";
			string text = this.SetHttpWebRequestPost(url, this.contentTypeData, body, true, true);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				SendResult = formMain.ML.GetStr("formCloudServerSend_Dgv_Message_SendFailure");
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				jArray[0]["message"].ToObject<string>();
				SendResult = formMain.ML.GetStr("formCloudServerSend_Dgv_Message_SendSuccessed");
				return true;
			}
			SendResult = formMain.ML.GetStr("formCloudServerSend_Dgv_Message_SendFailure");
			return false;
		}

		public string API_GetReviewList()
		{
			string httpWebRequestGet = this.cloudserver + "pendingtransdatas";
			string text = this.SetHttpWebRequestGet(httpWebRequestGet);
			JArray jArray = this.JsonToJArray(text);
			JArray jArray2 = this.JsonToJArray("[" + jArray[0]["page"].ToString() + "]");
			if (jArray == null || text == string.Empty)
			{
				return "0";
			}
			JArray arg_83_0 = (JArray)JsonConvert.DeserializeObject(jArray2[0]["content"].ToString());
			if (jArray[0]["success"].ToObject<bool>())
			{
				return jArray2[0]["totalElements"].ToObject<string>();
			}
			return "0";
		}

		public IList<CloudReviewRecord> API_GetReviewList(string parameter = "20")
		{
			string httpWebRequestGet = this.cloudserver + "pendingtransdatas?size=" + parameter;
			string text = this.SetHttpWebRequestGet(httpWebRequestGet);
			JArray jArray = this.JsonToJArray(text);
			JArray jArray2 = this.JsonToJArray("[" + jArray[0]["page"].ToString() + "]");
			if (jArray == null || text == string.Empty)
			{
				return null;
			}
			IList<CloudReviewRecord> list = new List<CloudReviewRecord>();
			JArray jArray3 = (JArray)JsonConvert.DeserializeObject(jArray2[0]["content"].ToString());
			if (jArray[0]["success"].ToObject<bool>())
			{
				for (int i = 0; i < jArray3.Count; i++)
				{
					CloudReviewRecord cloudReviewRecord = new CloudReviewRecord();
					cloudReviewRecord.Id = jArray3[i]["id"].ToObject<string>();
					cloudReviewRecord.CreateAt = jArray3[i]["createdAt"].ToObject<string>();
					cloudReviewRecord.UpdateAt = jArray3[i]["updatedAt"].ToObject<string>();
					JArray jArray4 = this.JsonToJArray("[" + jArray3[i]["group"].ToString() + "]");
					if (jArray4.Count > 0)
					{
						cloudReviewRecord.Group = new CloudReviewGroupRecord
						{
							Id = jArray4[0]["id"].ToObject<string>(),
							GroupName = jArray4[0]["groupName"].ToObject<string>()
						};
					}
					else
					{
						cloudReviewRecord.Group = null;
					}
					cloudReviewRecord.Description = jArray3[i]["description"].ToObject<string>();
					cloudReviewRecord.FrameLength = jArray3[i]["frameLength"].ToObject<string>();
					cloudReviewRecord.Status = jArray3[i]["status"].ToObject<string>();
					JArray jArray5 = this.JsonToJArray(jArray3[i]["terminal"].ToString());
					if (jArray5.Count > 0)
					{
						cloudReviewRecord.TerminalId = jArray5[0]["id"].ToObject<string>();
						cloudReviewRecord.TerminalCode = jArray5[0]["terminalCode"].ToObject<string>();
						cloudReviewRecord.TerminalName = jArray5[0]["terminalName"].ToObject<string>();
					}
					list.Add(cloudReviewRecord);
				}
				return list;
			}
			return list;
		}

		public bool API_GetReviewContent(string reviewDataId)
		{
			string httpWebRequestGet = this.cloudserver + "pendingtransdatas/" + reviewDataId + "/data";
			this.SetHttpWebRequestGet(httpWebRequestGet);
			return true;
		}

		public bool API_PutReviewToReject(string reviewId, out string outresult)
		{
			string url = this.cloudserver + "pendingtransdatas/" + reviewId + "/reject";
			string text = this.SetHttpWebRequestPut(url, string.Empty, false);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				outresult = formMain.ML.GetStr("formCloudServerSend_Comm_Messge_RequestFailed");
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				outresult = jArray[0]["message"].ToObject<string>();
				return true;
			}
			outresult = jArray[0]["message"].ToObject<string>();
			return false;
		}

		public bool API_PutReviewToPass(string reviewId, out string outresult)
		{
			string url = this.cloudserver + "pendingtransdatas/" + reviewId + "/pass";
			string text = this.SetHttpWebRequestPut(url, string.Empty, false);
			JArray jArray = this.JsonToJArray(text);
			if (jArray == null || text == string.Empty)
			{
				outresult = formMain.ML.GetStr("formCloudServerSend_Comm_Messge_RequestFailed");
				return false;
			}
			if (jArray[0]["success"].ToObject<bool>())
			{
				outresult = jArray[0]["message"].ToObject<string>();
				return true;
			}
			outresult = jArray[0]["message"].ToObject<string>();
			return false;
		}

		private JArray JsonToJArray(string Jason)
		{
			if (!Jason.StartsWith("["))
			{
				Jason = "[" + Jason + "]";
			}
			return (JArray)JsonConvert.DeserializeObject(Jason);
		}

		public byte[] ByteArrayToDataBase64(IList<byte[]> bytedata, int datalen)
		{
			byte[] array = new byte[datalen * bytedata.Count];
			int num = 0;
			foreach (byte[] current in bytedata)
			{
				Array.Copy(current, 0, array, datalen * num, current.Length);
				num++;
			}
			return array;
		}
	}
}
