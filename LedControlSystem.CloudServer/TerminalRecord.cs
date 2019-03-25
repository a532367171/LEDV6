using System;

namespace LedControlSystem.CloudServer
{
	public class TerminalRecord
	{
		public string TerminalID
		{
			get;
			set;
		}

		public string TerminalName
		{
			get;
			set;
		}

		public string TerminalDesc
		{
			get;
			set;
		}

		public string LastSendCmdId
		{
			get;
			set;
		}

		public string LastSendCmdMark
		{
			get;
			set;
		}

		public string LastCommand
		{
			get;
			set;
		}

		public string LastCmdSendStatus
		{
			get;
			set;
		}

		public string LastCmdSendTime
		{
			get;
			set;
		}

		public string LastSendProgramId
		{
			get;
			set;
		}

		public string LastProgram
		{
			get;
			set;
		}

		public string LastProgramSendStatus
		{
			get;
			set;
		}

		public string LastProgramGetTime
		{
			get;
			set;
		}
	}
}
