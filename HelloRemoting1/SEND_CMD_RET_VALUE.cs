using System;
namespace HelloRemoting1
{
	public enum SEND_CMD_RET_VALUE : byte
	{
		ER_NONE_INIT,
		POST_TO_RS_SERVER_OK,
		IPC_communication_FAILED,
		ER_Product_ID_NO_EXIST,
		ER_PARA
	}
}
