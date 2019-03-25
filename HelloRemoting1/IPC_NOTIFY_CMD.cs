using System;
namespace HelloRemoting1
{
	public enum IPC_NOTIFY_CMD
	{
		AW_ENABLE_COMM,
		RQ_COMM,
		RQ_EXIT,
		AW_SERVER_BUSY,
		RQ_CANCEL_PREV_CMD,
		RQ_CLEAR_CMD_QUEUE,
		QY_IS_IDLE
	}
}
