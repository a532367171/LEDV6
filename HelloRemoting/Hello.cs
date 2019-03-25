using System;
namespace HelloRemoting
{
	public class Hello : MarshalByRefObject, ISendBusiness
	{
		private static int retResult;
		public event RecvDataEventHandler OnRecvDataEvent;
		public void setRetsult(int v)
		{
			Hello.retResult = v;
		}
		public void getRetsult(out int _retResult)
		{
			_retResult = Hello.retResult;
		}
		public Hello()
		{
			Hello.retResult = 0;
		}
		~Hello()
		{
		}
		public override object InitializeLifetimeService()
		{
			return null;
		}
		public void SendObj(object obj)
		{
			if (this.OnRecvDataEvent != null)
			{
				this.OnRecvDataEvent(obj);
			}
		}
	}
}
