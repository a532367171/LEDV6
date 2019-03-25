using System;

namespace LedControlSystem.CloudServer
{
	public class TerminalResponseResult
	{
		public bool Result
		{
			get;
			set;
		}

		public string ResultDisplay
		{
			get;
			set;
		}

		public bool IsNeedReview
		{
			get;
			set;
		}

		public string ReviewId
		{
			get;
			set;
		}
	}
}
