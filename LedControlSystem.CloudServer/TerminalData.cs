using System;

namespace LedControlSystem.CloudServer
{
	public class TerminalData
	{
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

		public string Width
		{
			get;
			set;
		}

		public string Height
		{
			get;
			set;
		}

		public string Color
		{
			get;
			set;
		}

		public string Gray
		{
			get;
			set;
		}

		public string BackGray
		{
			get;
			set;
		}

		public string DeviceModel
		{
			get;
			set;
		}

		public string PhoneNumber
		{
			get;
			set;
		}

		public string DeviceVersion
		{
			get;
			set;
		}

		public string CommModel
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string ProductCategory
		{
			get;
			set;
		}

		public string ProductModel
		{
			get;
			set;
		}

		public string ProductSubmodel
		{
			get;
			set;
		}

		public string ProductExtra
		{
			get;
			set;
		}

		public string ProductModelDescription
		{
			get;
			set;
		}

		public bool Online
		{
			get;
			set;
		}

		public string Ip
		{
			get;
			set;
		}

		public string Port
		{
			get;
			set;
		}

		public string LastHeartbeat
		{
			get;
			set;
		}

		public string SignalStrength
		{
			get;
			set;
		}

		public string TotalCapacity
		{
			get;
			set;
		}

		public string TotalCapacityStr
		{
			get;
			set;
		}

		public string UsedCapacity
		{
			get;
			set;
		}

		public string UsedCapacityStr
		{
			get;
			set;
		}

		public string FreeCapacity
		{
			get;
			set;
		}

		public string FreeCapacityStr
		{
			get;
			set;
		}

		public void Copy(TerminalData data)
		{
			this.UpdatedAt = data.UpdatedAt;
			this.Width = data.Width;
			this.Height = data.Height;
			this.Color = data.Color;
			this.Gray = data.Gray;
			this.BackGray = data.BackGray;
			this.DeviceModel = data.DeviceModel;
			this.DeviceVersion = data.DeviceVersion;
			this.CommModel = data.CommModel;
			this.Online = data.Online;
		}
	}
}
