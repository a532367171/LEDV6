using LedModel;
using LedModel.Data;
using System;

namespace LedControlSystem
{
	public class Screen_Display_Class
	{
		private int screen_num;

		private string state_message;

		private int send_progress;

		private LedPanel panel_no;

		private string fault_message;

		private Process send_data;

		private bool isSendCompleted;

		private int resendCount;

		private int send_state;

		private DateTime lastRecvTime;

		public int Screen_Num
		{
			get
			{
				return this.screen_num;
			}
			set
			{
				this.screen_num = value;
			}
		}

		public string State_Message
		{
			get
			{
				return this.state_message;
			}
			set
			{
				this.state_message = value;
			}
		}

		public int Send_Progress
		{
			get
			{
				return this.send_progress;
			}
			set
			{
				this.send_progress = value;
			}
		}

		public string Fault_Message
		{
			get
			{
				return this.fault_message;
			}
			set
			{
				this.fault_message = value;
			}
		}

		public LedPanel Panel_NO
		{
			get
			{
				return this.panel_no;
			}
			set
			{
				this.panel_no = value;
			}
		}

		public Process Send_Data
		{
			get
			{
				return this.send_data;
			}
			set
			{
				this.send_data = value;
			}
		}

		public bool SendCompleted
		{
			get
			{
				return this.isSendCompleted;
			}
			set
			{
				this.isSendCompleted = value;
			}
		}

		public int ResendCount
		{
			get
			{
				return this.resendCount;
			}
			set
			{
				this.resendCount = value;
			}
		}

		public int Send_State
		{
			get
			{
				return this.send_state;
			}
			set
			{
				this.send_state = value;
			}
		}

		public DateTime LastRecvTime
		{
			get
			{
				return this.lastRecvTime;
			}
			set
			{
				this.lastRecvTime = value;
			}
		}

		public Screen_Display_Class()
		{
			this.screen_num = 0;
			this.state_message = string.Empty;
			this.send_progress = 0;
			this.panel_no = null;
			this.fault_message = string.Empty;
			this.send_data = null;
			this.isSendCompleted = false;
			this.resendCount = 0;
			this.send_state = 0;
		}

		public void Dispose()
		{
			this.state_message = null;
			this.fault_message = null;
			if (this.send_data != null)
			{
				this.send_data.Dispose();
				this.send_data = null;
			}
		}
	}
}
