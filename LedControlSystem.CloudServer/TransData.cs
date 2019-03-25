using System;

namespace LedControlSystem.CloudServer
{
	public class TransData
	{
		public string ID
		{
			get;
			set;
		}

		public string CreatedAt
		{
			get;
			set;
		}

		public string UpdatedAt
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public TransDataStatus Status
		{
			get;
			set;
		}

		public int FrameLength
		{
			get;
			set;
		}

		public string Data
		{
			get;
			set;
		}
	}
}
