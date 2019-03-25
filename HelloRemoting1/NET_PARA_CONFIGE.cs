using System;
namespace HelloRemoting1
{
	[Serializable]
	public class NET_PARA_CONFIGE
	{
		public int DO_CMD_TIME_OUT;
		public int SEND_PACK_DELAY_TIME;
		public int UDP_DEFAULT_LISTEN_PORT;
		public int TCP_DEFAULT_LISTEN_PORT;
		public int MAX_IN_TCP_CONNECT_NUM;
		public int MAX_OUT_TCP_CONNECT_NUM;
		public int UDP_SEND_BUFFER_SIZE;
		public int UDP_RECV_BUFFER_SIZE;
		public int TCP_SEND_BUFFER_SIZE;
		public int TCP_RECV_BUFFER_SIZE;
		public int COM_SEND_BUFFER_SIZE;
		public int COM_RECV_BUFFER_SIZE;
	}
}
