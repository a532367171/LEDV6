using System;
using System.Windows.Forms;

namespace LedControlSystem
{
	public class DataGridViewProgressColumn : DataGridViewImageColumn
	{
		public DataGridViewProgressColumn()
		{
			this.CellTemplate = new DataGridViewProgressCell();
		}
	}
}
