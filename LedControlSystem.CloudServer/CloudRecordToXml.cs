using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace LedControlSystem.CloudServer
{
	public class CloudRecordToXml
	{
		public static string LoginedUser = string.Empty;

		public static string CloudRecordPath = Application.StartupPath + "\\CloudServer\\CloudRecords.xml";

		public XmlDocument xmlDoc = new XmlDocument();

		public Dictionary<string, TerminalRecord> TerminalRecords = new Dictionary<string, TerminalRecord>();

		public Dictionary<string, RecordUpdateProgramMark> ProgramMarkUpdate = new Dictionary<string, RecordUpdateProgramMark>();

		public Dictionary<string, RecordUpdateCmdMark> CmdMarkUpdate = new Dictionary<string, RecordUpdateCmdMark>();

		public CloudRecordToXml()
		{
			this.xmlDoc.Load(CloudRecordToXml.CloudRecordPath);
		}

		public bool AddNewUserRecord()
		{
			if (!(CloudRecordToXml.LoginedUser != string.Empty))
			{
				return false;
			}
			XmlNode xmlNode = this.xmlDoc.SelectSingleNode("/CloudComm/User[@username='" + CloudRecordToXml.LoginedUser + "']");
			if (xmlNode != null)
			{
				return true;
			}
			XmlNode xmlNode2 = this.xmlDoc.SelectSingleNode("/CloudComm");
			XmlNode xmlNode3 = this.xmlDoc.CreateElement("User");
			XmlAttribute xmlAttribute = this.xmlDoc.CreateAttribute("username");
			xmlAttribute.Value = CloudRecordToXml.LoginedUser;
			xmlNode3.Attributes.Append(xmlAttribute);
			xmlNode2.AppendChild(xmlNode3);
			this.xmlDoc.Save(CloudRecordToXml.CloudRecordPath);
			return false;
		}

		public void AddNewTerminalRecord(TerminalRecord terminalRecord, bool IsProgramOrCmdUpdate)
		{
			this.AddNewUserRecord();
			XmlNode xmlNode = this.xmlDoc.SelectSingleNode("/CloudComm/User/Record[@TerminalID='" + terminalRecord.TerminalID + "']");
			if (xmlNode == null)
			{
				XmlNode xmlNode2 = this.xmlDoc.SelectSingleNode("/CloudComm/User[@username='" + CloudRecordToXml.LoginedUser + "']");
				XmlNode xmlNode3 = this.xmlDoc.CreateElement("Record");
				XmlAttribute xmlAttribute = this.xmlDoc.CreateAttribute("TerminalID");
				xmlAttribute.Value = terminalRecord.TerminalID;
				xmlNode3.Attributes.Append(xmlAttribute);
				XmlAttribute xmlAttribute2 = this.xmlDoc.CreateAttribute("TerminalName");
				xmlAttribute2.Value = terminalRecord.TerminalName;
				xmlNode3.Attributes.Append(xmlAttribute2);
				XmlAttribute xmlAttribute3 = this.xmlDoc.CreateAttribute("TerminalDesc");
				xmlAttribute3.Value = terminalRecord.TerminalDesc;
				xmlNode3.Attributes.Append(xmlAttribute3);
				XmlAttribute xmlAttribute4 = this.xmlDoc.CreateAttribute("LastSendCmdId");
				xmlAttribute4.Value = terminalRecord.LastSendCmdId;
				xmlNode3.Attributes.Append(xmlAttribute4);
				XmlAttribute xmlAttribute5 = this.xmlDoc.CreateAttribute("LastSendCmdMark");
				xmlAttribute5.Value = terminalRecord.LastSendCmdMark;
				xmlNode3.Attributes.Append(xmlAttribute5);
				XmlAttribute xmlAttribute6 = this.xmlDoc.CreateAttribute("LastCommand");
				xmlAttribute6.Value = terminalRecord.LastCommand;
				xmlNode3.Attributes.Append(xmlAttribute6);
				XmlAttribute xmlAttribute7 = this.xmlDoc.CreateAttribute("LastCmdSendStatus");
				xmlAttribute7.Value = terminalRecord.LastCmdSendStatus;
				xmlNode3.Attributes.Append(xmlAttribute7);
				XmlAttribute xmlAttribute8 = this.xmlDoc.CreateAttribute("LastCmdSendTime");
				xmlAttribute8.Value = terminalRecord.LastCmdSendTime;
				xmlNode3.Attributes.Append(xmlAttribute8);
				XmlAttribute xmlAttribute9 = this.xmlDoc.CreateAttribute("LastSendProgramId");
				xmlAttribute9.Value = terminalRecord.LastSendProgramId;
				xmlNode3.Attributes.Append(xmlAttribute9);
				XmlAttribute xmlAttribute10 = this.xmlDoc.CreateAttribute("LastProgram");
				xmlAttribute10.Value = terminalRecord.LastProgram;
				xmlNode3.Attributes.Append(xmlAttribute10);
				XmlAttribute xmlAttribute11 = this.xmlDoc.CreateAttribute("LastProgramSendStatus");
				xmlAttribute11.Value = terminalRecord.LastProgramSendStatus;
				xmlNode3.Attributes.Append(xmlAttribute11);
				XmlAttribute xmlAttribute12 = this.xmlDoc.CreateAttribute("LastProgramGetTime");
				xmlAttribute12.Value = terminalRecord.LastProgramGetTime;
				xmlNode3.Attributes.Append(xmlAttribute12);
				xmlNode2.AppendChild(xmlNode3);
			}
			else
			{
				XmlElement xmlElement = (XmlElement)xmlNode;
				xmlElement.SetAttribute("TerminalName", terminalRecord.TerminalName);
				xmlElement.SetAttribute("TerminalDesc", terminalRecord.TerminalDesc);
				if (!IsProgramOrCmdUpdate)
				{
					xmlElement.SetAttribute("LastSendCmdId", terminalRecord.LastSendCmdId);
					xmlElement.SetAttribute("LastSendCmdMark", terminalRecord.LastSendCmdMark);
					xmlElement.SetAttribute("LastCommand", terminalRecord.LastCommand);
					xmlElement.SetAttribute("LastCmdSendStatus", terminalRecord.LastCmdSendStatus);
					xmlElement.SetAttribute("LastCmdSendTime", terminalRecord.LastCmdSendTime);
				}
				else
				{
					xmlElement.SetAttribute("LastSendProgramId", terminalRecord.LastSendProgramId);
					xmlElement.SetAttribute("LastProgram", terminalRecord.LastProgram);
					xmlElement.SetAttribute("LastProgramSendStatus", terminalRecord.LastProgramSendStatus);
					xmlElement.SetAttribute("LastProgramGetTime", terminalRecord.LastProgramGetTime);
				}
			}
			this.xmlDoc.Save(CloudRecordToXml.CloudRecordPath);
		}

		public void ReadCloudRecord()
		{
			XmlElement documentElement = this.xmlDoc.DocumentElement;
			XmlNodeList elementsByTagName = documentElement.GetElementsByTagName("User");
			this.TerminalRecords.Clear();
			foreach (XmlNode xmlNode in elementsByTagName)
			{
				string attribute = ((XmlElement)xmlNode).GetAttribute("username");
				XmlNodeList childNodes = xmlNode.ChildNodes;
				if (attribute == CloudRecordToXml.LoginedUser)
				{
					foreach (XmlNode xmlNode2 in childNodes)
					{
						TerminalRecord terminalRecord = new TerminalRecord();
						terminalRecord.TerminalID = ((XmlElement)xmlNode2).GetAttribute("TerminalID");
						terminalRecord.TerminalName = ((XmlElement)xmlNode2).GetAttribute("TerminalName");
						terminalRecord.TerminalDesc = ((XmlElement)xmlNode2).GetAttribute("TerminalDesc");
						terminalRecord.LastSendCmdId = ((XmlElement)xmlNode2).GetAttribute("LastSendCmdId");
						terminalRecord.LastSendCmdMark = ((XmlElement)xmlNode2).GetAttribute("LastSendCmdMark");
						terminalRecord.LastCommand = ((XmlElement)xmlNode2).GetAttribute("LastCommand");
						terminalRecord.LastCmdSendStatus = ((XmlElement)xmlNode2).GetAttribute("LastCmdSendStatus");
						terminalRecord.LastCmdSendTime = ((XmlElement)xmlNode2).GetAttribute("LastCmdSendTime");
						terminalRecord.LastSendProgramId = ((XmlElement)xmlNode2).GetAttribute("LastSendProgramId");
						terminalRecord.LastProgram = ((XmlElement)xmlNode2).GetAttribute("LastProgram");
						terminalRecord.LastProgramSendStatus = ((XmlElement)xmlNode2).GetAttribute("LastProgramSendStatus");
						terminalRecord.LastProgramGetTime = ((XmlElement)xmlNode2).GetAttribute("LastProgramGetTime");
						this.TerminalRecords.Add(terminalRecord.TerminalID, terminalRecord);
						RecordUpdateProgramMark recordUpdateProgramMark = new RecordUpdateProgramMark();
						recordUpdateProgramMark.Mark = terminalRecord.LastProgram;
						RecordUpdateCmdMark recordUpdateCmdMark = new RecordUpdateCmdMark();
						recordUpdateCmdMark.Mark = terminalRecord.LastSendCmdMark;
						if (terminalRecord.LastProgramSendStatus == "1" || terminalRecord.LastProgramSendStatus == "2")
						{
							recordUpdateProgramMark.IsUpdated = true;
							this.ProgramMarkUpdate.Add(terminalRecord.TerminalID, recordUpdateProgramMark);
						}
						else
						{
							recordUpdateProgramMark.IsUpdated = false;
							this.ProgramMarkUpdate.Add(terminalRecord.TerminalID, recordUpdateProgramMark);
						}
						if (terminalRecord.LastCmdSendStatus == "2")
						{
							recordUpdateCmdMark.IsUpdated = false;
							this.CmdMarkUpdate.Add(terminalRecord.TerminalID, recordUpdateCmdMark);
						}
						else
						{
							recordUpdateCmdMark.IsUpdated = true;
							this.CmdMarkUpdate.Add(terminalRecord.TerminalID, recordUpdateCmdMark);
						}
					}
				}
			}
		}
	}
}
