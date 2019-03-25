using System;
namespace HelloRemoting
{
	public enum RETURN_RESULT_STATE : byte
	{
		ER_NONE,
		SUCCESS = 128,
		ER_MODEL = 1,
		ER_VER,
		ER_WIFI_PWD = 4,
		ER_CONTROL_PWD = 8,
		ER_WRITE = 16,
		ER_CRC = 32,
		ER_TIMEOUT = 64
	}
}
