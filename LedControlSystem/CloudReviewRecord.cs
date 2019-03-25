using System;

namespace LedControlSystem
{
	public class CloudReviewRecord
	{
		public string Id
		{
			get;
			set;
		}

		public string CreateAt
		{
			get;
			set;
		}

		public string UpdateAt
		{
			get;
			set;
		}

		public string TerminalId
		{
			get;
			set;
		}

		public string TerminalCode
		{
			get;
			set;
		}

		public string TerminalName
		{
			get;
			set;
		}

		public CloudReviewGroupRecord Group
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string FrameLength
		{
			get;
			set;
		}

		public string Status
		{
			get;
			set;
		}
	}
}
