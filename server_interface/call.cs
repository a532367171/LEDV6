using HelloRemoting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace server_interface
{
	[Serializable]
	public class call : UI_Interface
	{
		public static Dictionary<string, DEVICE_INFO> m_devList;

		public static Form m_thisPtr;

		public static IPC_Class myipc = null;

		public static bool is_IPC_ready_flag;

		public static ushort m_last_run_cmd_id = 0;

		public static long cmd_order = 0L;

		private static string ipc_recv_id = "IPC_ZHUWEI_20160605_B";

		private static string ipc_send_id = "IPC_ZHUWEI_20160605_A";

		private static string ipc_recv_id_intl = "IPC_ZHUWEI_20160605_B_INTL";

		private static string ipc_send_id_intl = "IPC_ZHUWEI_20160605_A_INTL";

		public static event EventHandler<DeviceCmdEventArgs> OnDeviceCmdReturnResult;

		public static event EventHandler<SendFileEventArgs> OnSendFilePosChanged;

		public static event EventHandler<EventArgs> OnIPCReadyOK;

		public static event EventHandler<AutoFoundDeviceEventArgs> OnAutoFoundDevice;

		public int DO_CMD_TIME_OUT
		{
			get;
			set;
		}

		public int SEND_PACK_DELAY_TIME
		{
			get;
			set;
		}

		public int UDP_DEFAULT_LISTEN_PORT
		{
			get;
			set;
		}

		public int TCP_DEFAULT_LISTEN_PORT
		{
			get;
			set;
		}

		public int MAX_IN_TCP_CONNECT_NUM
		{
			get;
			set;
		}

		public int MAX_OUT_TCP_CONNECT_NUM
		{
			get;
			set;
		}

		public int UDP_SEND_BUFFER_SIZE
		{
			get;
			set;
		}

		public int UDP_RECV_BUFFER_SIZE
		{
			get;
			set;
		}

		public int TCP_SEND_BUFFER_SIZE
		{
			get;
			set;
		}

		public int TCP_RECV_BUFFER_SIZE
		{
			get;
			set;
		}

		public int COM_SEND_BUFFER_SIZE
		{
			get;
			set;
		}

		public int COM_RECV_BUFFER_SIZE
		{
			get;
			set;
		}

		public bool IS_FOREIGNTRADE_MODE
		{
			get;
			set;
		}

		public call()
		{
			this.DO_CMD_TIME_OUT = 5;
			this.SEND_PACK_DELAY_TIME = 200;
			this.UDP_DEFAULT_LISTEN_PORT = 58258;
			this.TCP_DEFAULT_LISTEN_PORT = 58258;
			this.MAX_IN_TCP_CONNECT_NUM = 255;
			this.MAX_OUT_TCP_CONNECT_NUM = 255;
			this.UDP_SEND_BUFFER_SIZE = 2048;
			this.UDP_RECV_BUFFER_SIZE = 16777216;
			this.TCP_SEND_BUFFER_SIZE = 2048;
			this.TCP_RECV_BUFFER_SIZE = 8192;
			this.COM_SEND_BUFFER_SIZE = 2048;
			this.COM_RECV_BUFFER_SIZE = 8192;
		}

		public Dictionary<string, DEVICE_INFO> init(Form thisPtr)
		{
			Dictionary<string, DEVICE_INFO> result;
			if (global_static_var.is_init_ok)
			{
				MessageBox.Show("编码逻辑错误：通信服务器的调用接口重复初始化。");
				result = null;
			}
			else
			{
				call.m_thisPtr = thisPtr;
				call.is_IPC_ready_flag = false;
				call.myipc = new IPC_Class();
				call.m_devList = null;
				call.m_devList = new Dictionary<string, DEVICE_INFO>();
				global_static_var.is_init_ok = true;
				result = call.m_devList;
			}
			return result;
		}

		public void enable_ipc_comm_async()
		{
			Console.WriteLine("启动后台通信服务调用接口...");
			Console.WriteLine("工作目录：" + Application.StartupPath);
			try
			{
				this.start_ipc_comm();
			}
			catch (Exception ex)
			{
				Console.WriteLine("*********初始化IPC通信时出错：" + ex.Message, "[报错信息]");
			}
		}

		public void uninit()
		{
			if (global_static_var.is_init_ok)
			{
				Console.WriteLine("IPC发信：通知后台运行的SERVER退出。");
				call.is_IPC_ready_flag = false;
				if (!this.Is_server_IDLE())
				{
					this.CallIPCSend(IPC_NOTIFY_CMD.RQ_CANCEL_PREV_CMD);
					this.CallIPCSend(IPC_NOTIFY_CMD.RQ_CLEAR_CMD_QUEUE);
				}
				this.CallIPCSend(IPC_NOTIFY_CMD.RQ_EXIT);
				call.m_devList = null;
				call.myipc.uninit_ipc_channel();
				call.myipc = null;
				global_static_var.is_init_ok = false;
				GC.Collect();
			}
		}

		public SEND_CMD_RET_VALUE send_cmd_to_device_async(byte cmdid, object data, string product_id)
		{
			SEND_CMD_RET_VALUE result;
			if (!global_static_var.is_init_ok)
			{
				result = SEND_CMD_RET_VALUE.ER_NONE_INIT;
			}
			else if (!call.m_devList.ContainsKey(product_id))
			{
				result = SEND_CMD_RET_VALUE.ER_Product_ID_NO_EXIST;
			}
			else if (-1 == this.CallIPCSend(new IPC_SIMPLE_REQUEST
			{
				cmd_id = (ushort)cmdid,
				dev_object = data,
				fileName = null,
				product_id = product_id,
				order = call.cmd_order += 1L
			}))
			{
				result = SEND_CMD_RET_VALUE.IPC_communication_FAILED;
			}
			else
			{
				result = SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK;
			}
			return result;
		}

		public SEND_CMD_RET_VALUE send_cmd_to_device_async(byte cmdid, object data, List<string> product_id_keys)
		{
			SEND_CMD_RET_VALUE result;
			if (!global_static_var.is_init_ok)
			{
				result = SEND_CMD_RET_VALUE.ER_NONE_INIT;
			}
			else
			{
				foreach (string current in product_id_keys)
				{
					if (!call.m_devList.ContainsKey(current))
					{
						result = SEND_CMD_RET_VALUE.ER_Product_ID_NO_EXIST;
						return result;
					}
				}
				IPC_SIMPLE_REQUEST iPC_SIMPLE_REQUEST = new IPC_SIMPLE_REQUEST();
				iPC_SIMPLE_REQUEST.cmd_id = (ushort)cmdid;
				iPC_SIMPLE_REQUEST.dev_object = data;
				iPC_SIMPLE_REQUEST.fileName = null;
				if (product_id_keys != null)
				{
					iPC_SIMPLE_REQUEST.product_ids = new string[product_id_keys.Count];
					product_id_keys.CopyTo(iPC_SIMPLE_REQUEST.product_ids, 0);
				}
				for (int i = 0; i < iPC_SIMPLE_REQUEST.product_ids.Length; i++)
				{
					bool flag = false;
					if (!call.m_devList.ContainsKey(iPC_SIMPLE_REQUEST.product_ids[i]))
					{
						flag = true;
					}
					DEVICE_INFO dEVICE_INFO;
					if (call.m_devList.TryGetValue(iPC_SIMPLE_REQUEST.product_ids[i], out dEVICE_INFO))
					{
						if (dEVICE_INFO.is_disable_flag)
						{
							flag = true;
						}
					}
					if (flag)
					{
						iPC_SIMPLE_REQUEST.product_ids[i] = null;
					}
				}
				iPC_SIMPLE_REQUEST.order = (call.cmd_order += 1L);
				if (-1 == this.CallIPCSend(iPC_SIMPLE_REQUEST))
				{
					result = SEND_CMD_RET_VALUE.IPC_communication_FAILED;
				}
				else
				{
					result = SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK;
				}
			}
			return result;
		}

		public void Cancel_prev_long_cmd()
		{
			this.CallIPCSend(IPC_NOTIFY_CMD.RQ_CANCEL_PREV_CMD);
		}

		public void Clear_do_cmd_queue()
		{
			this.CallIPCSend(IPC_NOTIFY_CMD.RQ_CLEAR_CMD_QUEUE);
		}

		public bool Is_server_IDLE()
		{
			int num = this.CallIPCSend(IPC_NOTIFY_CMD.QY_IS_IDLE);
			return 1 == num || -1 == num;
		}

		private void SYNC_DEV_LIST()
		{
			if (global_static_var.is_init_ok)
			{
				if (!call.is_IPC_ready_flag)
				{
					Console.WriteLine("同步设备列表报错：在IPC通信还未准备好时，发生了通信请求。");
				}
				else
				{
					this.CallIPCSend(new IPC_SIMPLE_REQUEST
					{
						cmd_id = 4337,
						dev_object = call.m_devList
					});
				}
			}
		}

		public SEND_CMD_RET_VALUE add_device_to_list_async(string device_ip, ushort device_map_port = 58258)
		{
			SEND_CMD_RET_VALUE result;
			if (!global_static_var.is_init_ok)
			{
				result = SEND_CMD_RET_VALUE.ER_NONE_INIT;
			}
			else if (!call.is_IPC_ready_flag)
			{
				Console.WriteLine("在IPC通信还未准备好时，发生了通信请求。");
				result = SEND_CMD_RET_VALUE.IPC_communication_FAILED;
			}
			else if (device_ip == null || device_ip == string.Empty)
			{
				result = SEND_CMD_RET_VALUE.ER_PARA;
			}
			else if (-1 == this.CallIPCSend(new IPC_SIMPLE_REQUEST
			{
				cmd_id = 4339,
				dev_object = new IP_MESSAGE_CUSTOM
				{
					map_port = device_map_port,
					device_ip = device_ip
				}
			}))
			{
				result = SEND_CMD_RET_VALUE.IPC_communication_FAILED;
			}
			else
			{
				result = SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK;
			}
			return result;
		}

		public bool del_device_from_list(string product_id)
		{
			bool result;
			if (!global_static_var.is_init_ok)
			{
				result = false;
			}
			else if (!call.is_IPC_ready_flag)
			{
				Console.WriteLine("在IPC通信还未准备好时，发生了通信请求。");
				result = false;
			}
			else if (product_id == null || product_id == string.Empty)
			{
				result = false;
			}
			else if (!call.m_devList.ContainsKey(product_id))
			{
				result = false;
			}
			else if (call.m_devList.Remove(product_id))
			{
				this.CallIPCSend(new IPC_SIMPLE_REQUEST
				{
					cmd_id = 4340,
					product_id = product_id,
					dev_object = call.m_devList
				});
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public void restart_tcp_listen(ushort port)
		{
			this.CallIPCSend(new IPC_SIMPLE_REQUEST
			{
				dev_object = port,
				cmd_id = 4349
			});
		}

		public void restart_udp_listen(ushort port)
		{
			this.CallIPCSend(new IPC_SIMPLE_REQUEST
			{
				dev_object = port,
				cmd_id = 4350
			});
		}

		public void update_device_list_statue_async()
		{
			if (global_static_var.is_init_ok)
			{
				if (call.is_IPC_ready_flag)
				{
					this.CallIPCSend(new IPC_SIMPLE_REQUEST
					{
						cmd_id = 4338
					});
				}
			}
		}

		public SEND_CMD_RET_VALUE find_device_async()
		{
			SEND_CMD_RET_VALUE result;
			if (!global_static_var.is_init_ok)
			{
				result = SEND_CMD_RET_VALUE.ER_NONE_INIT;
			}
			else if (!call.is_IPC_ready_flag)
			{
				Console.WriteLine("在IPC通信还未准备好时，发生了通信请求。");
				result = SEND_CMD_RET_VALUE.IPC_communication_FAILED;
			}
			else if (-1 == this.CallIPCSend(new IPC_SIMPLE_REQUEST
			{
				cmd_id = 45,
				dev_object = call.m_devList
			}))
			{
				result = SEND_CMD_RET_VALUE.IPC_communication_FAILED;
			}
			else
			{
				result = SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK;
			}
			return result;
		}

		private void trigger_HeartReturnResult(IPC_SIMPLE_ANSWER isa)
		{
			if (global_static_var.is_init_ok)
			{
				if (call.OnAutoFoundDevice != null)
				{
					AutoFoundDeviceEventArgs arg = new AutoFoundDeviceEventArgs();
					if (!isa.is_cmd_failed_flag)
					{
						DEVICE_INFO di = isa.return_object as DEVICE_INFO;
						call.m_thisPtr.Invoke(new MethodInvoker(delegate
						{
							if (call.m_devList.ContainsKey(di.product_id))
							{
								call.m_devList.Remove(di.product_id);
							}
							call.m_devList.Add(di.product_id, di);
						}));
						arg.cmd_id = (int)isa.cmd_id;
						arg.net_id = di.net_id;
						arg.product_id = di.product_id;
						arg.state = di.state;
						arg.signal_strength = di.signal_strength;
						arg.program_number = di.program_number;
						if (isa.cmd_id == 4341)
						{
							arg.repair_message = "心跳引发的设备状态更新！";
						}
						else if (isa.cmd_id == 4342)
						{
							arg.repair_message = "心跳引发的新设备发现！";
						}
						else if (isa.cmd_id == 4343)
						{
							arg.repair_message = "长时间没有心跳，认为此设备已离线。";
						}
					}
					else if (isa.cmd_id == 4342)
					{
						arg.repair_message = isa.er_message;
						arg.net_id = "";
						arg.product_id = "";
						arg.state = DEVICE_STATE.ST_NONE;
						arg.signal_strength = 0;
						arg.program_number = 0;
						arg.cmd_id = (int)isa.cmd_id;
					}
					call.m_thisPtr.BeginInvoke(new MethodInvoker(delegate
					{
						if (call.OnAutoFoundDevice != null)
						{
							call.OnAutoFoundDevice(this, arg);
						}
					}));
				}
			}
		}

		private void trigger_DeviceCmdReturnResult(IPC_SIMPLE_ANSWER isa)
		{
			if (global_static_var.is_init_ok)
			{
				if (call.OnDeviceCmdReturnResult != null)
				{
					this.tb_cl_cmd(ref isa);
					DeviceCmdEventArgs arg = new DeviceCmdEventArgs();
					arg.isa = isa;
					call.m_thisPtr.BeginInvoke(new MethodInvoker(delegate
					{
						if (call.OnDeviceCmdReturnResult != null)
						{
							call.OnDeviceCmdReturnResult(this, arg);
						}
					}));
				}
			}
		}

		private void trigger_SendPosChanged(IPC_SIMPLE_ANSWER isa)
		{
			if (global_static_var.is_init_ok)
			{
				if (call.OnSendFilePosChanged != null)
				{
					SendFileEventArgs arg = new SendFileEventArgs();
					arg.filename = isa.send_fileName;
					arg.send_pos = isa.pos;
					arg.product_id = isa.product_id;
					arg.is_send_failed_flag = isa.is_cmd_failed_flag;
					arg.is_send_over = isa.is_cmd_over_flag;
					if (isa.is_cmd_failed_flag)
					{
						arg.msg = isa.er_message;
					}
					else
					{
						arg.msg = isa.repair_message;
					}
					call.m_thisPtr.BeginInvoke(new MethodInvoker(delegate
					{
						if (call.OnSendFilePosChanged != null)
						{
							call.OnSendFilePosChanged(this, arg);
						}
					}));
				}
			}
		}

		private void trigger_IPCReadyOKEvent()
		{
			if (global_static_var.is_init_ok)
			{
				if (call.OnIPCReadyOK != null)
				{
					EventArgs e = new EventArgs();
					call.OnIPCReadyOK(this, e);
				}
			}
		}

		private void tb_cl_cmd(ref IPC_SIMPLE_ANSWER aisa)
		{
			IPC_SIMPLE_ANSWER isa = aisa;
			if (isa.cmd_id == 45)
			{
				if (!isa.is_cmd_over_flag && !isa.is_cmd_failed_flag)
				{
					call.m_thisPtr.Invoke(new MethodInvoker(delegate
					{
						DEVICE_INFO dEVICE_INFO = isa.return_object as DEVICE_INFO;
						if (call.m_devList.ContainsKey(isa.product_id))
						{
							call.m_devList.Remove(isa.product_id);
						}
						call.m_devList.Add(dEVICE_INFO.product_id, dEVICE_INFO);
					}));
				}
			}
			else if (isa.cmd_id == 4338)
			{
				if (isa.is_cmd_over_flag && !isa.is_cmd_failed_flag)
				{
					call.m_thisPtr.Invoke(new MethodInvoker(delegate
					{
						Dictionary<string, DEVICE_INFO> dictionary = isa.return_object as Dictionary<string, DEVICE_INFO>;
						foreach (DEVICE_INFO current in call.m_devList.Values)
						{
							if (current.cur_use_net_mode == 1 || current.cur_use_net_mode == 8)
							{
								DEVICE_INFO dEVICE_INFO;
								if (dictionary.TryGetValue(current.product_id, out dEVICE_INFO))
								{
									current.state = dEVICE_INFO.state;
								}
							}
						}
					}));
				}
				else if (!isa.is_cmd_over_flag && !isa.is_cmd_failed_flag)
				{
					call.m_thisPtr.Invoke(new MethodInvoker(delegate
					{
						DEVICE_INFO dEVICE_INFO;
						if (isa.product_id != null && call.m_devList.TryGetValue(isa.product_id, out dEVICE_INFO))
						{
							DEVICE_INFO expr_32 = dEVICE_INFO;
							expr_32.state |= DEVICE_STATE.ST_ONLINE;
						}
					}));
				}
			}
			else if (isa.cmd_id == 4339)
			{
				if (isa.is_cmd_over_flag && !isa.is_cmd_failed_flag)
				{
					call.m_thisPtr.Invoke(new MethodInvoker(delegate
					{
						DEVICE_INFO dEVICE_INFO = isa.return_object as DEVICE_INFO;
						if (call.m_devList.ContainsKey(isa.product_id))
						{
							call.m_devList.Remove(isa.product_id);
						}
						call.m_devList.Add(dEVICE_INFO.product_id, dEVICE_INFO);
					}));
				}
			}
			aisa = isa;
		}

		private void IPC_send_cfg_prar()
		{
			NET_PARA_CONFIGE nET_PARA_CONFIGE = new NET_PARA_CONFIGE();
			nET_PARA_CONFIGE.DO_CMD_TIME_OUT = this.DO_CMD_TIME_OUT;
			nET_PARA_CONFIGE.MAX_IN_TCP_CONNECT_NUM = this.MAX_IN_TCP_CONNECT_NUM;
			nET_PARA_CONFIGE.MAX_OUT_TCP_CONNECT_NUM = this.MAX_OUT_TCP_CONNECT_NUM;
			nET_PARA_CONFIGE.SEND_PACK_DELAY_TIME = this.SEND_PACK_DELAY_TIME;
			nET_PARA_CONFIGE.TCP_DEFAULT_LISTEN_PORT = this.TCP_DEFAULT_LISTEN_PORT;
			nET_PARA_CONFIGE.TCP_RECV_BUFFER_SIZE = this.TCP_RECV_BUFFER_SIZE;
			nET_PARA_CONFIGE.TCP_SEND_BUFFER_SIZE = this.TCP_SEND_BUFFER_SIZE;
			nET_PARA_CONFIGE.UDP_DEFAULT_LISTEN_PORT = this.UDP_DEFAULT_LISTEN_PORT;
			nET_PARA_CONFIGE.UDP_RECV_BUFFER_SIZE = this.UDP_RECV_BUFFER_SIZE;
			nET_PARA_CONFIGE.UDP_SEND_BUFFER_SIZE = this.UDP_SEND_BUFFER_SIZE;
			nET_PARA_CONFIGE.COM_RECV_BUFFER_SIZE = this.COM_RECV_BUFFER_SIZE;
			nET_PARA_CONFIGE.COM_SEND_BUFFER_SIZE = this.COM_SEND_BUFFER_SIZE;
			this.CallIPCSend(new IPC_SIMPLE_REQUEST
			{
				cmd_id = 4345,
				dev_object = nET_PARA_CONFIGE
			});
		}

		public void OnIPCRecv(object obj)
		{
			if (obj.GetType() == typeof(IPC_NOTIFY_CMD))
			{
				Console.WriteLine("I 收到server发来的IPC通知命令：" + (IPC_NOTIFY_CMD)obj);
				if (IPC_NOTIFY_CMD.AW_ENABLE_COMM == (IPC_NOTIFY_CMD)obj)
				{
					if (!call.is_IPC_ready_flag)
					{
						call.m_thisPtr.BeginInvoke(new MethodInvoker(delegate
						{
							this.IPC_send_cfg_prar();
							call.is_IPC_ready_flag = true;
							this.trigger_IPCReadyOKEvent();
						}));
					}
				}
				else if (IPC_NOTIFY_CMD.AW_SERVER_BUSY == (IPC_NOTIFY_CMD)obj)
				{
					this.trigger_DeviceCmdReturnResult(new IPC_SIMPLE_ANSWER
					{
						cmd_id = call.m_last_run_cmd_id,
						is_cmd_failed_flag = true,
						er_code = 65281,
						er_message = "后台服务器忙，因为上一条指令是排它执行的指令，所以要收到它的OVER事件后，才能继续发送命令。"
					});
				}
			}
			else
			{
				IPC_SIMPLE_ANSWER iPC_SIMPLE_ANSWER = obj as IPC_SIMPLE_ANSWER;
				if (iPC_SIMPLE_ANSWER.cmd_id == 4342 || iPC_SIMPLE_ANSWER.cmd_id == 4341 || iPC_SIMPLE_ANSWER.cmd_id == 4343)
				{
					Console.WriteLine("收到心跳通信");
					this.trigger_HeartReturnResult(iPC_SIMPLE_ANSWER);
				}
				else if (iPC_SIMPLE_ANSWER.cmd_id == 6)
				{
					this.trigger_SendPosChanged(iPC_SIMPLE_ANSWER);
				}
				else
				{
					this.trigger_DeviceCmdReturnResult(iPC_SIMPLE_ANSWER);
				}
			}
		}

		private void start_ipc_comm()
		{
			string recv_id = call.ipc_recv_id;
			string send_id = call.ipc_send_id;
			if (this.IS_FOREIGNTRADE_MODE)
			{
				recv_id = call.ipc_recv_id_intl;
				send_id = call.ipc_send_id_intl;
			}
			call.myipc.init_ipc_recv(recv_id);
			call.myipc.init_ipc_send(send_id);
			call.myipc.IPCRecv.OnRecvDataEvent += new RecvDataEventHandler(this.OnIPCRecv);
		}

		private int CallIPCSend(object obj)
		{
			if (obj.GetType() == typeof(IPC_SIMPLE_REQUEST))
			{
				call.m_last_run_cmd_id = (obj as IPC_SIMPLE_REQUEST).cmd_id;
			}
			int result;
			try
			{
				call.myipc.IPCSend.SendObj(obj);
				int num;
				call.myipc.IPCSend.getRetsult(out num);
				result = num;
			}
			catch (Exception ex)
			{
				Console.WriteLine("---------IPC发送出错：" + ex.Message);
				result = -1;
			}
			return result;
		}

		public void Enable_HEART_Processing(bool is_ON)
		{
			this.CallIPCSend(new IPC_SIMPLE_REQUEST
			{
				cmd_id = 4351,
				product_id = null,
				dev_object = is_ON
			});
		}

		public void set_all_device_pwd(string pwd)
		{
			if (pwd == null)
			{
				pwd = "";
			}
			foreach (DEVICE_INFO current in call.m_devList.Values)
			{
				current.password = pwd;
			}
			this.CallIPCSend(new IPC_SIMPLE_REQUEST
			{
				cmd_id = 4344,
				product_id = null,
				dev_object = pwd
			});
		}

		public void set_device_pwd(string product_id, string pwd)
		{
			if (product_id != null && !(product_id == string.Empty))
			{
				if (pwd == null)
				{
					pwd = "";
				}
				DEVICE_INFO dEVICE_INFO;
				if (call.m_devList.TryGetValue(product_id, out dEVICE_INFO))
				{
					dEVICE_INFO.password = pwd;
				}
				this.CallIPCSend(new IPC_SIMPLE_REQUEST
				{
					cmd_id = 4344,
					product_id = product_id,
					dev_object = pwd
				});
			}
		}

		public bool add_device_to_list_nocheck(DEVICE_INFO add_di)
		{
			bool result;
			if (add_di.product_id == null || add_di.product_id == "")
			{
				result = false;
			}
			else if (call.m_devList.ContainsKey(add_di.product_id))
			{
				result = false;
			}
			else
			{
				call.m_devList.Add(add_di.product_id, add_di);
				this.CallIPCSend(new IPC_SIMPLE_REQUEST
				{
					cmd_id = 4346,
					product_id = add_di.product_id,
					dev_object = add_di
				});
				result = true;
			}
			return result;
		}

		public bool modi_device_info_nocheck(string product_id, DEVICE_INFO new_di)
		{
			bool result;
			if (product_id == null || product_id == "" || product_id != new_di.product_id)
			{
				result = false;
			}
			else if (!call.m_devList.ContainsKey(new_di.product_id))
			{
				result = false;
			}
			else if (call.m_devList.Remove(product_id))
			{
				call.m_devList.Add(product_id, new_di);
				this.CallIPCSend(new IPC_SIMPLE_REQUEST
				{
					cmd_id = 4347,
					product_id = product_id,
					dev_object = new_di
				});
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool Enable_one_device(string product_id, bool is_enable_flag)
		{
			bool result;
			if (product_id == null || product_id == "")
			{
				result = false;
			}
			else if (!call.m_devList.ContainsKey(product_id))
			{
				result = false;
			}
			else
			{
				DEVICE_INFO dEVICE_INFO;
				if (call.m_devList.TryGetValue(product_id, out dEVICE_INFO))
				{
					dEVICE_INFO.is_disable_flag = !is_enable_flag;
				}
				this.CallIPCSend(new IPC_SIMPLE_REQUEST
				{
					cmd_id = 4348,
					product_id = product_id,
					dev_object = dEVICE_INFO
				});
				result = true;
			}
			return result;
		}

		public void TestFunc()
		{
			this.add_xj_dev();
		}

		public bool save_device_list()
		{
			bool result;
			if (!global_static_var.is_init_ok)
			{
				result = false;
			}
			else
			{
				try
				{
					FileStream fileStream = new FileStream("serializeDeviceList3.bin", FileMode.Create);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(fileStream, call.m_devList);
					fileStream.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine("save file er:" + ex.Message);
					result = false;
					return result;
				}
				result = true;
			}
			return result;
		}

		public bool load_device_list()
		{
			bool result;
			if (!global_static_var.is_init_ok)
			{
				result = false;
			}
			else if (!call.is_IPC_ready_flag)
			{
				result = false;
			}
			else
			{
				try
				{
					FileStream fileStream = new FileStream("serializeDeviceList3.bin", FileMode.Open);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					if (fileStream.CanRead && fileStream.Length > 0L)
					{
						Dictionary<string, DEVICE_INFO> dictionary = binaryFormatter.Deserialize(fileStream) as Dictionary<string, DEVICE_INFO>;
						if (dictionary.Count > 0)
						{
							call.m_devList.Clear();
							foreach (KeyValuePair<string, DEVICE_INFO> current in dictionary)
							{
								current.Value.state = DEVICE_STATE.ST_NONE;
								call.m_devList.Add(current.Key, current.Value);
							}
							this.SYNC_DEV_LIST();
						}
					}
					fileStream.Close();
				}
				catch
				{
					result = false;
					return result;
				}
				result = true;
			}
			return result;
		}

		private void add_xj_dev()
		{
			DEVICE_INFO dEVICE_INFO = new DEVICE_INFO();
			dEVICE_INFO.product_id = "66666666";
			dEVICE_INFO.net_id = "测试用虚拟卡3#";
			dEVICE_INFO.state = DEVICE_STATE.ST_ONLINE;
			dEVICE_INFO.FIXED_IP = "192.168.1.133";
			dEVICE_INFO.TCP_out_dst_Port = 58258;
			dEVICE_INFO.MAC = "112233445566";
			dEVICE_INFO.cur_use_net_mode = 1;
			if (!call.m_devList.ContainsKey(dEVICE_INFO.product_id))
			{
				call.m_devList.Add(dEVICE_INFO.product_id, dEVICE_INFO);
			}
			dEVICE_INFO = new DEVICE_INFO();
			dEVICE_INFO.product_id = "88888888";
			dEVICE_INFO.net_id = "测试用虚拟卡2#";
			dEVICE_INFO.state = DEVICE_STATE.ST_ONLINE;
			dEVICE_INFO.FIXED_IP = "192.168.1.166";
			dEVICE_INFO.TCP_out_dst_Port = 58258;
			dEVICE_INFO.cur_use_net_mode = 1;
			dEVICE_INFO.MAC = "665544332211";
			if (!call.m_devList.ContainsKey(dEVICE_INFO.product_id))
			{
				call.m_devList.Add(dEVICE_INFO.product_id, dEVICE_INFO);
			}
			this.SYNC_DEV_LIST();
		}

		public SEND_CMD_RET_VALUE send_file_async(string filefullpath, string product_id)
		{
			SEND_CMD_RET_VALUE result;
			if (!global_static_var.is_init_ok)
			{
				result = SEND_CMD_RET_VALUE.ER_NONE_INIT;
			}
			else if (!call.m_devList.ContainsKey(product_id))
			{
				result = SEND_CMD_RET_VALUE.ER_Product_ID_NO_EXIST;
			}
			else if (-1 == this.CallIPCSend(new IPC_SIMPLE_REQUEST
			{
				cmd_id = 6,
				dev_object = null,
				fileName = filefullpath,
				product_id = product_id,
				order = call.cmd_order += 1L
			}))
			{
				result = SEND_CMD_RET_VALUE.IPC_communication_FAILED;
			}
			else
			{
				result = SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK;
			}
			return result;
		}

		public SEND_CMD_RET_VALUE send_file_async(string filefullpath, List<string> product_id_keys)
		{
			SEND_CMD_RET_VALUE result;
			if (!global_static_var.is_init_ok)
			{
				result = SEND_CMD_RET_VALUE.ER_NONE_INIT;
			}
			else
			{
				foreach (string current in product_id_keys)
				{
					if (!call.m_devList.ContainsKey(current))
					{
						result = SEND_CMD_RET_VALUE.ER_Product_ID_NO_EXIST;
						return result;
					}
				}
				IPC_SIMPLE_REQUEST iPC_SIMPLE_REQUEST = new IPC_SIMPLE_REQUEST();
				iPC_SIMPLE_REQUEST.cmd_id = 6;
				iPC_SIMPLE_REQUEST.dev_object = null;
				iPC_SIMPLE_REQUEST.fileName = filefullpath;
				if (product_id_keys != null)
				{
					iPC_SIMPLE_REQUEST.product_ids = new string[product_id_keys.Count];
					product_id_keys.CopyTo(iPC_SIMPLE_REQUEST.product_ids, 0);
				}
				iPC_SIMPLE_REQUEST.order = (call.cmd_order += 1L);
				if (-1 == this.CallIPCSend(iPC_SIMPLE_REQUEST))
				{
					result = SEND_CMD_RET_VALUE.IPC_communication_FAILED;
				}
				else
				{
					result = SEND_CMD_RET_VALUE.POST_TO_RS_SERVER_OK;
				}
			}
			return result;
		}
	}
}
