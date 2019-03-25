using System;
using System.Collections.Generic;
namespace HelloRemoting1
{
	[Serializable]
	public class IPC_SIMPLE_ANSWER
	{
		public bool is_cmd_over_flag;
		public bool is_cmd_failed_flag;
		public int er_code;
		public string er_message;
		public List<string> er_device_ip_mac_list;
		public string repair_message;
		public ushort cmd_id;
		public string[] product_ids;
		public string product_id;
		public string send_fileName;
		public int pos;
		public object return_object;
	}
}
