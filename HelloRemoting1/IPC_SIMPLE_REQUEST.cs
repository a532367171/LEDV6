using System;
namespace HelloRemoting1
{
	[Serializable]
	public class IPC_SIMPLE_REQUEST
	{
		public ushort cmd_id;
		public string[] product_ids;
		public string product_id;
		public long order;
		public string fileName;
		public object dev_object;
	}
}
