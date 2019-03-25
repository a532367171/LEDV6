using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace LedControlSystem
{
	internal class DataGridViewProgressCell : DataGridViewImageCell
	{
		private static System.Drawing.Image emptyImage;

		static DataGridViewProgressCell()
		{
			DataGridViewProgressCell.emptyImage = new System.Drawing.Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		}

		public DataGridViewProgressCell()
		{
			this.ValueType = typeof(int);
		}

		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			return DataGridViewProgressCell.emptyImage;
		}

		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			int num;
			if (value != null)
			{
				num = (int)value;
			}
			else
			{
				num = 0;
			}
			float num2 = (float)num / 100f;
			new System.Drawing.SolidBrush(cellStyle.BackColor);
			System.Drawing.Brush brush = new System.Drawing.SolidBrush(cellStyle.ForeColor);
			base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.ContentForeground);
			if ((double)num2 >= 0.0)
			{
				g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(50, 185, 50)), cellBounds.X + 4, cellBounds.Y + 2, Convert.ToInt32(num2 * (float)cellBounds.Width - 6f), cellBounds.Height - 6);
				g.DrawString(num.ToString() + "%", cellStyle.Font, brush, (float)(cellBounds.X + 6), (float)(cellBounds.Y + 2));
				return;
			}
			if (base.DataGridView.CurrentRow.Index == rowIndex)
			{
				g.DrawString(num.ToString() + "%", cellStyle.Font, new System.Drawing.SolidBrush(cellStyle.SelectionForeColor), (float)(cellBounds.X + 6), (float)(cellBounds.Y + 2));
				return;
			}
			g.DrawString(num.ToString() + "%", cellStyle.Font, brush, (float)(cellBounds.X + 6), (float)(cellBounds.Y + 2));
		}
	}
}
