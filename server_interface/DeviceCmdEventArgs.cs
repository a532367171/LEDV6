using HelloRemoting;
using System;

namespace server_interface
{
	public class DeviceCmdEventArgs : EventArgs
	{
		public IPC_SIMPLE_ANSWER isa;
	}
}
