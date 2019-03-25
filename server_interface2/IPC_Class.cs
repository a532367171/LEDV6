using HelloRemoting;
using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;

namespace server_interface
{
	[Serializable]
	public class IPC_Class
	{
		public Hello IPCRecv;

		public Hello IPCSend;

		public IpcChannel chan = null;

		public void uninit_ipc_channel()
		{
			if (this.chan != null)
			{
				ChannelServices.UnregisterChannel(this.chan);
				this.chan = null;
			}
		}

		public bool init_ipc_recv(string recv_id)
		{
			bool result;
			if (null != this.IPCRecv)
			{
				result = true;
			}
			else
			{
				BinaryServerFormatterSinkProvider binaryServerFormatterSinkProvider = new BinaryServerFormatterSinkProvider();
				binaryServerFormatterSinkProvider.TypeFilterLevel = TypeFilterLevel.Full;
				BinaryClientFormatterSinkProvider clientSinkProvider = new BinaryClientFormatterSinkProvider();
				IDictionary dictionary = new Hashtable();
				dictionary["portName"] = recv_id;
				this.chan = new IpcChannel(dictionary, clientSinkProvider, binaryServerFormatterSinkProvider);
				ChannelServices.RegisterChannel(this.chan, false);
				RemotingConfiguration.RegisterWellKnownServiceType(typeof(Hello), "remote_obj", WellKnownObjectMode.Singleton);
				this.IPCRecv = (Hello)Activator.GetObject(typeof(Hello), "ipc://" + recv_id + "/remote_obj");
				if (null == this.IPCRecv)
				{
					Console.WriteLine("IPC recv 无法定位到服务器。");
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		public bool init_ipc_send(string send_id)
		{
			bool result;
			if (null != this.IPCSend)
			{
				result = true;
			}
			else
			{
				this.IPCSend = (Hello)Activator.GetObject(typeof(Hello), "ipc://" + send_id + "/remote_obj");
				if (null == this.IPCSend)
				{
					Console.WriteLine("IPC send 无法定位到服务器。");
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}
	}
}
