using System;
namespace HelloRemoting
{
	public enum DEVICE_STATE : byte
	{
		ST_NONE,
		ST_ONLINE,
		ST_OFFLINE,
		ST_LED_POWER = 4,
		ST_PLAY = 8,
		ST_BUSY = 16,
		ST_IDLE = 32,
		ST_RUN_CMD = 64
	}
}
