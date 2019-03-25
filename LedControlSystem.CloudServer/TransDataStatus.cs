using System;

namespace LedControlSystem.CloudServer
{
	public enum TransDataStatus
	{
		WaitingForReception,
		CompleteReception,
		ExceptionFrameRequest = -1,
		ExceptionFullStorage = -2,
		ExceptionWrongFormat = -3
	}
}
