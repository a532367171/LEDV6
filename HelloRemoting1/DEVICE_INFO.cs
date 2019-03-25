using System;
namespace HelloRemoting1
{
	[Serializable]
	public class DEVICE_INFO
	{
		public string product_id;
		public string description;
		public DEVICE_STATE state;
		public bool is_have_password;
		public string password;
		public bool is_disable_flag;
		public string comm_mode;
		public DEV_NET_MODE dev_net_mode;
		public byte cur_use_net_mode;
		public string MAC;
		public string ComPort_name;
		public int BaudRate;
		public ushort Rs485_Adr;
		public byte comport_Lock_RW_flag;
		public bool is_comport_wait_answer_flag;
		public string FIXED_IP;
		public string TCP_in_src_IP;
		public ushort TCP_out_dst_Port;
		public ushort TCP_in_src_Port;
		public ushort UDP_out_dst_Port;
		public ushort UDP_in_src_Port;
		public string net_id;
		public bool is_have_heart_throb;
		public int signal_strength;
		public int program_number;
		public int tci_key_id_AddOne;
		public int com_array_id;
		public DEVICE_SPECIFY_FLAG device_sflag;
		public byte firmware_comm_ver;
		public bool is_enforce_use_UDP_p2p;
		public ushort UDP_for_p2p_out_dst_Port;
	}
}
