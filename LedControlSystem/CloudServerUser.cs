using System;

namespace LedControlSystem
{
	public class CloudServerUser
	{
		public string UserName
		{
			get;
			set;
		}

		public string UserId
		{
			get;
			set;
		}

		public string NickName
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public string PhoneNumber
		{
			get;
			set;
		}

		public string CompanyName
		{
			get;
			set;
		}

		public string Address
		{
			get;
			set;
		}

		public string SessionId
		{
			get;
			set;
		}

		public string CreatedAt
		{
			get;
			set;
		}

		public string UpdateAt
		{
			get;
			set;
		}

		public CloudServerUser()
		{
			this.UserName = "";
			this.UserId = "";
			this.NickName = "";
			this.Email = "";
			this.PhoneNumber = "";
			this.CompanyName = "";
			this.Address = "";
			this.SessionId = "";
			this.CreatedAt = "";
			this.UpdateAt = "";
		}
	}
}
