using HelloRemoting;
using System;

namespace server_interface
{
	public class AutoFoundDeviceEventArgs : EventArgs
	{
		public string repair_message;

		public string product_id;

		public string net_id;

		public DEVICE_STATE state;

		public int signal_strength;

		public int program_number;

		public int cmd_id;
	}
}
