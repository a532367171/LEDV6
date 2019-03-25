using LedModel;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LedControlSystem.LedControlSystem
{
	public class GprsContentInfo
	{
		private string id;

		private string description;

		private string terminalCode;

		private int width;

		private int height;

		private string ledModel;

		private IList<System.Drawing.Bitmap> bitmapList;

		private byte[] bitData;

		private LedPanel panel;

		private string content;

		private string status = formMain.ML.GetStr("formGprsSendAll_Not_uploaded");

		private int fontSize;

		private System.Drawing.Font contentFont;

		public string Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		public string Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				this.fontSize = value;
			}
		}

		public System.Drawing.Font ContentFont
		{
			get
			{
				return this.contentFont;
			}
			set
			{
				this.contentFont = value;
			}
		}

		public LedPanel Panel
		{
			get
			{
				return this.panel;
			}
			set
			{
				this.panel = value;
			}
		}

		public IList<System.Drawing.Bitmap> BitmapList
		{
			get
			{
				return this.bitmapList;
			}
			set
			{
				this.bitmapList = value;
			}
		}

		public byte[] BitData
		{
			get
			{
				return this.bitData;
			}
			set
			{
				this.bitData = value;
			}
		}

		public string LedModel
		{
			get
			{
				return this.ledModel;
			}
			set
			{
				this.ledModel = value;
			}
		}

		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		public string TerminalCode
		{
			get
			{
				return this.terminalCode;
			}
			set
			{
				this.terminalCode = value;
			}
		}

		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}
	}
}
