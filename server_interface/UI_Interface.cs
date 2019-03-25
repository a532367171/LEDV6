using HelloRemoting;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace server_interface
{
	public interface UI_Interface
	{
		int DO_CMD_TIME_OUT
		{
			get;
			set;
		}

		int SEND_PACK_DELAY_TIME
		{
			get;
			set;
		}

		int UDP_DEFAULT_LISTEN_PORT
		{
			get;
			set;
		}

		int TCP_DEFAULT_LISTEN_PORT
		{
			get;
			set;
		}

		int MAX_IN_TCP_CONNECT_NUM
		{
			get;
			set;
		}

		int MAX_OUT_TCP_CONNECT_NUM
		{
			get;
			set;
		}

		int UDP_SEND_BUFFER_SIZE
		{
			get;
			set;
		}

		int UDP_RECV_BUFFER_SIZE
		{
			get;
			set;
		}

		int TCP_SEND_BUFFER_SIZE
		{
			get;
			set;
		}

		int TCP_RECV_BUFFER_SIZE
		{
			get;
			set;
		}

		int COM_SEND_BUFFER_SIZE
		{
			get;
			set;
		}

		int COM_RECV_BUFFER_SIZE
		{
			get;
			set;
		}

		bool IS_FOREIGNTRADE_MODE
		{
			get;
			set;
		}

		Dictionary<string, DEVICE_INFO> init(Form thisPtr);

		void uninit();

		void enable_ipc_comm_async();

		bool save_device_list();

		bool load_device_list();

		void restart_tcp_listen(ushort port);

		void restart_udp_listen(ushort port);

		bool add_device_to_list_nocheck(DEVICE_INFO add_di);

		bool modi_device_info_nocheck(string product_id, DEVICE_INFO new_di);

		bool Enable_one_device(string product_id, bool is_enable_flag);

		SEND_CMD_RET_VALUE add_device_to_list_async(string device_ip, ushort device_map_port = 58258);

		bool del_device_from_list(string product_id);

		void update_device_list_statue_async();

		SEND_CMD_RET_VALUE find_device_async();

		SEND_CMD_RET_VALUE send_file_async(string filefullpath, string product_id);

		SEND_CMD_RET_VALUE send_file_async(string filefullpath, List<string> product_id_keys);

		void set_all_device_pwd(string pwd);

		void set_device_pwd(string product_id, string pwd);

		SEND_CMD_RET_VALUE send_cmd_to_device_async(byte cmdid, object data, string product_id);

		SEND_CMD_RET_VALUE send_cmd_to_device_async(byte cmdid, object data, List<string> product_id_keys);

		void Cancel_prev_long_cmd();

		void Clear_do_cmd_queue();

		bool Is_server_IDLE();

		void Enable_HEART_Processing(bool is_ON);

		void TestFunc();
	}
}
