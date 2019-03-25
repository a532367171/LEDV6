using System;

namespace server_interface
{
	public class SendFileEventArgs : EventArgs
	{
		public string product_id;

		public int send_pos;

		public string filename;

		public bool is_send_failed_flag;

		public bool is_send_over;

		public int last_sub_order;

		public string msg;
	}
}
