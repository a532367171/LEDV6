using System;
namespace HelloRemoting
{
	public enum DEV_NET_MODE : byte
	{
		MODE_NONE,
		MODE_TCP_FIXED_IP,
		MODE_TCP_LOCALE_SERVER,
		MODE_UDP = 4,
		MODE_RS485 = 8,
		MODE_TCP_REMOTE_SERVER = 16
	}
}
