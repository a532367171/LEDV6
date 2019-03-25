using System;
using System.Collections.Generic;

namespace LedControlSystem.CloudServer
{
	public class GroupOfCloudServer
	{
		public IList<GroupTerminal> GroupTerminals = new List<GroupTerminal>();

		public string Id
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

		public string CustomerName
		{
			get;
			set;
		}

		public string GroupName
		{
			get;
			set;
		}

		public GroupContactInfo GroupContact
		{
			get;
			set;
		}
	}
}
