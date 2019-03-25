using LedControlSystem.Fonts;
using LedModel;
using LedModel.Const;
using LedModel.Content;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class WeatherEditor : UserControl
	{
		private LedWeather weather;

		private LedPanel panel;

		private IList<LedProvince> provinceList;

		private bool isFontDone;

		private IContainer components;

		private Label lblAirQuality;

		private CheckBox chkAirQuality;

		private Label lblMeteorology;

		private Label lblTemperature;

		private Label lblCity;

		private CheckBox chkMeteorology;

		private CheckBox chkTemperature;

		private CheckBox chkCity;

		private Button btnFontUnderline;

		private Button btnFontItalic;

		private Label lblCityList;

		private Label lblFontFamily;

		private Button btnFontBold;

		private ComboBox cmbLineStyle;

		private ComboBox cmbFontSize;

		private Label lblFontSize;

		private ComboBox cmbFontFamily;

		private ComboBox cmbProvince;

		private ComboBox cmbCity;

		private ComboBox cmbDistrict;

		private ComboBox cmbDisplayStyle;

		private Label lblWindDirectionForce;

		private CheckBox chkWindDirectionForce;

		private Label lblDisaster;

		private CheckBox chkDisaster;

		private Label lblAirConditioningIndex;

		private CheckBox chkAirConditioningIndex;

		private Label lblDressingIndex;

		private CheckBox chkDressingIndex;

		private Label lblSendibleTemperatureIndex;

		private CheckBox chkSendibleTemperatureIndex;

		private Label lblColdIndex;

		private CheckBox chkColdIndex;

		private Label lblPollutionIndex;

		private CheckBox chkPollutionIndex;

		private Label lblCarwashIndex;

		private CheckBox chkCarwashIndex;

		private Label lblUltravioletRayIndex;

		private CheckBox chkUltravioletRayIndex;

		private Label lblExerciseIndex;

		private CheckBox chkExerciseIndex;

		private ComboBox cmbForeColor;

		public event LedGlobal.LedContentEvent UpdateEvent;

		public WeatherEditor()
		{
			this.InitializeComponent();
			formMain.updateWeatherProvinceEvent = (UpdateWeatherProvinceEvent)Delegate.Combine(formMain.updateWeatherProvinceEvent, new UpdateWeatherProvinceEvent(this.WeatherEditor_UpdateEvent));
		}

		public void Edit(LedWeather pWeather)
		{
			this.panel = formMain.ledsys.SelectedPanel;
			this.weather = pWeather;
			try
			{
				this.InitControl();
				this.Binding();
			}
			catch
			{
			}
		}

		private void WeatherEditor_UpdateEvent()
		{
			if (this.provinceList != null)
			{
				this.provinceList.Clear();
				this.provinceList = null;
			}
			this.cmbProvince.DataSource = null;
			this.cmbCity.DataSource = null;
			this.cmbDistrict.DataSource = null;
			this.InitControl();
			this.Binding();
		}

		private void InitControl()
		{
			if (!this.isFontDone)
			{
				this.cmbFontFamily.Items.Clear();
				IList<string> fontFamilies = new FontFamiliesEx().GetFontFamilies();
				if (fontFamilies != null && fontFamilies.Count > 0)
				{
					foreach (string current in fontFamilies)
					{
						this.cmbFontFamily.Items.Add(current);
					}
				}
				this.isFontDone = true;
			}
			this.cmbForeColor.Items.Clear();
			if (this.weather != null && this.panel != null)
			{
				IList<System.Drawing.Color> colorList = LedColorConst.GetColorList(this.panel.ColorMode);
				foreach (System.Drawing.Color current2 in colorList)
				{
					this.cmbForeColor.Items.Add(current2);
				}
			}
			if (this.cmbProvince.Items.Count == 0 || this.cmbProvince.DataSource == null)
			{
				if (formMain.isInitCityData)
				{
					this.provinceList = formMain.GetProvinceListFromFile();
					if (this.provinceList == null || this.provinceList.Count == 0)
					{
						this.provinceList = formMain.GetProvinceListFromResource();
					}
				}
				else if (LedGlobal.LedProvinceList != null)
				{
					this.provinceList = LedGlobal.LedProvinceList;
				}
				if (this.provinceList != null)
				{
					this.cmbProvince.DataSource = this.provinceList;
					this.cmbProvince.ValueMember = "ID";
					this.cmbProvince.DisplayMember = "Name";
				}
			}
		}

		private void Binding()
		{
			if (this.cmbFontFamily.Items.Count > 0)
			{
				int num = -1;
				if (this.weather != null && this.weather.Font != null && !string.IsNullOrEmpty(this.weather.Font.FamilyName))
				{
					for (int i = 0; i < this.cmbFontFamily.Items.Count; i++)
					{
						if (this.cmbFontFamily.Items[i].ToString() == this.weather.Font.FamilyName)
						{
							num = i;
							break;
						}
					}
				}
				if (num < 0)
				{
					for (int j = 0; j < this.cmbFontFamily.Items.Count; j++)
					{
						if (this.cmbFontFamily.Items[j].ToString() == formMain.DefalutForeignTradeFamilyName)
						{
							num = j;
							break;
						}
					}
				}
				this.cmbFontFamily.SelectedIndex = ((num < 0) ? 0 : num);
			}
			if (this.cmbFontSize.Items.Count > 0)
			{
				if (this.weather != null && this.weather.Font != null)
				{
					this.cmbFontSize.Text = this.weather.Font.Size.ToString();
				}
				else
				{
					this.cmbFontSize.SelectedIndex = 0;
				}
			}
			if (this.weather != null && this.weather.Font != null && this.weather.Font.Bold)
			{
				this.btnFontBold.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontBold.BackColor = this.BackColor;
			}
			if (this.weather != null && this.weather.Font != null && this.weather.Font.Italic)
			{
				this.btnFontItalic.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontItalic.BackColor = this.BackColor;
			}
			if (this.weather != null && this.weather.Font != null && this.weather.Font.Underline)
			{
				this.btnFontUnderline.BackColor = System.Drawing.Color.LightBlue;
			}
			else
			{
				this.btnFontUnderline.BackColor = this.BackColor;
			}
			if (this.cmbForeColor.Items.Count > 0)
			{
				int num2 = 0;
				if (this.weather != null)
				{
					num2 = formMain.FromColorToIndex(this.weather.ForeColor);
					int count = this.cmbForeColor.Items.Count;
					if (num2 > count - 1)
					{
						num2 = count - 1;
					}
				}
				this.cmbForeColor.SelectedIndex = num2;
			}
			if (this.cmbLineStyle.Items.Count > 0)
			{
				int num3 = 0;
				if (this.weather != null)
				{
					num3 = (int)this.weather.LineStyle;
					int count2 = this.cmbLineStyle.Items.Count;
					if (num3 > count2 - 1)
					{
						num3 = count2 - 1;
					}
				}
				this.cmbLineStyle.SelectedIndex = num3;
			}
			if (this.cmbProvince.Items.Count > 0)
			{
				if (this.weather != null && !string.IsNullOrEmpty(this.weather.ProvincesID))
				{
					this.cmbProvince.SelectedValue = this.weather.ProvincesID;
				}
				else
				{
					this.cmbProvince.SelectedIndex = 0;
					if (this.weather != null)
					{
						this.weather.ProvincesID = this.cmbProvince.SelectedValue.ToString();
					}
				}
			}
			bool flag = false;
			if (this.cmbCity.Items.Count == 0 || this.cmbCity.DataSource == null)
			{
				flag = true;
			}
			else if (this.weather != null)
			{
				bool flag2 = false;
				for (int k = 0; k < this.cmbCity.Items.Count; k++)
				{
					if (this.cmbCity.Items[k].GetType() == typeof(LedCity))
					{
						LedCity ledCity = this.cmbCity.Items[k] as LedCity;
						if (ledCity.ID.Length <= 3 || this.weather.CitiesID.Length <= 3 || !(ledCity.ID.Substring(0, 4) == this.weather.CitiesID.Substring(0, 4)))
						{
							break;
						}
						if (ledCity.ID == this.weather.CitiesID)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.cmbCity.DataSource = null;
				this.cmbCity.Items.Clear();
				if (this.weather != null)
				{
					IList<LedCity> dataSource = new List<LedCity>();
					for (int l = 0; l < this.provinceList.Count; l++)
					{
						if (this.provinceList[l].ID == this.weather.ProvincesID)
						{
							dataSource = this.provinceList[l].Cities;
							break;
						}
					}
					this.cmbCity.DataSource = dataSource;
					this.cmbCity.ValueMember = "ID";
					this.cmbCity.DisplayMember = "Name";
				}
			}
			if (this.cmbCity.Items.Count > 0)
			{
				if (!string.IsNullOrEmpty(this.weather.CitiesID))
				{
					this.cmbCity.SelectedValue = this.weather.CitiesID;
				}
				else
				{
					this.cmbCity.SelectedIndex = 0;
					this.weather.CitiesID = this.cmbCity.SelectedValue.ToString();
				}
			}
			bool flag3 = false;
			if (this.cmbDistrict.Items.Count == 0 || this.cmbDistrict.DataSource == null)
			{
				flag3 = true;
			}
			else if (this.weather != null)
			{
				bool flag4 = false;
				for (int m = 0; m < this.cmbDistrict.Items.Count; m++)
				{
					if (this.cmbDistrict.Items[m].GetType() == typeof(LedDistrict))
					{
						LedDistrict ledDistrict = this.cmbDistrict.Items[m] as LedDistrict;
						if (ledDistrict.ID.Length <= 3 || this.weather.DistrictsID.Length <= 3 || !(ledDistrict.ID.Substring(0, 4) == this.weather.DistrictsID.Substring(0, 4)))
						{
							break;
						}
						if (ledDistrict.ID == this.weather.DistrictsID)
						{
							flag4 = true;
							break;
						}
					}
				}
				if (!flag4)
				{
					flag3 = true;
				}
			}
			if (flag3)
			{
				this.cmbDistrict.DataSource = null;
				this.cmbDistrict.Items.Clear();
				if (this.weather != null && LedGlobal.LedProvinceList != null)
				{
					IList<LedDistrict> dataSource2 = new List<LedDistrict>();
					for (int n = 0; n < this.provinceList.Count; n++)
					{
						if (this.provinceList[n].ID == this.weather.ProvincesID)
						{
							IList<LedCity> cities = this.provinceList[n].Cities;
							for (int num4 = 0; num4 < cities.Count; num4++)
							{
								if (cities[num4].ID == this.weather.CitiesID)
								{
									dataSource2 = cities[num4].Districts;
									break;
								}
							}
						}
					}
					this.cmbDistrict.DataSource = dataSource2;
					this.cmbDistrict.ValueMember = "ID";
					this.cmbDistrict.DisplayMember = "Name";
				}
			}
			if (this.cmbDistrict.Items.Count > 0)
			{
				if (!string.IsNullOrEmpty(this.weather.DistrictsID))
				{
					this.cmbDistrict.SelectedValue = this.weather.DistrictsID;
				}
				else
				{
					this.cmbDistrict.SelectedIndex = 0;
					this.weather.DistrictsID = this.cmbDistrict.SelectedValue.ToString();
					this.weather.CityID = this.weather.DistrictsID;
					this.weather.CityName = this.cmbDistrict.Text;
				}
			}
			if (this.cmbDisplayStyle.Items.Count > 0)
			{
				int num5 = 0;
				if (this.weather != null)
				{
					num5 = (int)this.weather.DisplayStyle;
					int count3 = this.cmbDisplayStyle.Items.Count;
					if (num5 > count3 - 1)
					{
						num5 = count3 - 1;
					}
				}
				this.cmbDisplayStyle.SelectedIndex = num5;
			}
			if (this.weather != null)
			{
				this.chkCity.Checked = this.weather.CityEnabled;
			}
			else
			{
				this.chkCity.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkMeteorology.Checked = this.weather.MeteorologyEnabled;
			}
			else
			{
				this.chkMeteorology.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkTemperature.Checked = this.weather.TemperatureEnabled;
			}
			else
			{
				this.chkTemperature.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkAirQuality.Checked = this.weather.AirQualityEnabled;
			}
			else
			{
				this.chkAirQuality.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkWindDirectionForce.Checked = this.weather.WindDirectionForceEnabled;
			}
			else
			{
				this.chkWindDirectionForce.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkDisaster.Checked = this.weather.DisasterEnabled;
			}
			else
			{
				this.chkDisaster.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkSendibleTemperatureIndex.Checked = this.weather.SendibleTemperatureIndexEnabled;
			}
			else
			{
				this.chkSendibleTemperatureIndex.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkColdIndex.Checked = this.weather.ColdIndexEnabled;
			}
			else
			{
				this.chkColdIndex.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkPollutionIndex.Checked = this.weather.PollutionIndexEnabled;
			}
			else
			{
				this.chkPollutionIndex.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkCarwashIndex.Checked = this.weather.CarwashIndexEnabled;
			}
			else
			{
				this.chkCarwashIndex.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkAirConditioningIndex.Checked = this.weather.AirConditioningIndexEnabled;
			}
			else
			{
				this.chkAirConditioningIndex.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkDressingIndex.Checked = this.weather.DressingIndexEnabled;
			}
			else
			{
				this.chkDressingIndex.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkUltravioletRayIndex.Checked = this.weather.UltravioletRayIndexEnabled;
			}
			else
			{
				this.chkUltravioletRayIndex.Checked = false;
			}
			if (this.weather != null)
			{
				this.chkExerciseIndex.Checked = this.weather.ExerciseIndexEnabled;
				return;
			}
			this.chkExerciseIndex.Checked = false;
		}

		private void cmbFontFamily_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.weather.Font.FamilyName = comboBox.Text;
			this.Redraw();
		}

		private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			this.weather.Font.Size = float.Parse(comboBox.Text);
			this.Redraw();
		}

		private void cmbFontSize_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void cmbFontSize_TextChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			float num = 8f;
			if (!string.IsNullOrEmpty(comboBox.Text))
			{
				try
				{
					num = float.Parse(comboBox.Text);
				}
				catch
				{
				}
				if (num > 800f || (double)num == 0.0)
				{
					num = 8f;
					this.cmbFontSize.Text = ((int)num).ToString();
					return;
				}
			}
			this.weather.Font.Size = num;
			this.Redraw();
		}

		private void btnFontBold_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.weather.Font.Bold)
			{
				this.weather.Font.Bold = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.weather.Font.Bold = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.Redraw();
		}

		private void btnFontItalic_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.weather.Font.Italic)
			{
				this.weather.Font.Italic = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.weather.Font.Italic = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.Redraw();
		}

		private void btnFontUnderline_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (this.weather.Font.Underline)
			{
				this.weather.Font.Underline = false;
				button.BackColor = this.BackColor;
			}
			else
			{
				this.weather.Font.Underline = true;
				button.BackColor = System.Drawing.Color.LightBlue;
			}
			this.Redraw();
		}

		private void cmbForeColor_DrawItem(object sender, DrawItemEventArgs e)
		{
			System.Drawing.Graphics arg_06_0 = e.Graphics;
			System.Drawing.Rectangle bounds = e.Bounds;
			ComboBox comboBox = (ComboBox)sender;
			if (e.Index >= 0 && e.Index < comboBox.Items.Count)
			{
				System.Drawing.Color color = (System.Drawing.Color)comboBox.Items[e.Index];
				using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(color))
				{
					e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
					e.Graphics.FillRectangle(brush, new System.Drawing.Rectangle(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height - 2));
					e.DrawFocusRectangle();
				}
			}
		}

		private void cmbForeColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.ForeColor = formMain.FromIndexToColor(comboBox.SelectedIndex);
				this.Redraw();
			}
		}

		private void cmbLineStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.LineStyle = (LedWeatherLineStyle)comboBox.SelectedIndex;
				this.Redraw();
			}
		}

		private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.ProvincesID = comboBox.SelectedValue.ToString();
				this.cmbCity.DataSource = null;
				this.cmbCity.Items.Clear();
				IList<LedCity> dataSource = new List<LedCity>();
				for (int i = 0; i < LedGlobal.LedProvinceList.Count; i++)
				{
					if (LedGlobal.LedProvinceList[i].ID == this.weather.ProvincesID)
					{
						dataSource = LedGlobal.LedProvinceList[i].Cities;
						break;
					}
				}
				this.cmbCity.DataSource = dataSource;
				this.cmbCity.ValueMember = "ID";
				this.cmbCity.DisplayMember = "Name";
				if (this.cmbCity.Items.Count > 0)
				{
					this.cmbCity.SelectedIndex = 0;
					this.weather.CitiesID = this.cmbCity.SelectedValue.ToString();
				}
				this.cmbDistrict.DataSource = null;
				this.cmbDistrict.Items.Clear();
				IList<LedDistrict> dataSource2 = new List<LedDistrict>();
				for (int j = 0; j < LedGlobal.LedProvinceList.Count; j++)
				{
					if (LedGlobal.LedProvinceList[j].ID == this.weather.ProvincesID)
					{
						IList<LedCity> cities = LedGlobal.LedProvinceList[j].Cities;
						for (int k = 0; k < cities.Count; k++)
						{
							if (cities[k].ID == this.weather.CitiesID)
							{
								dataSource2 = cities[k].Districts;
								break;
							}
						}
					}
				}
				this.cmbDistrict.DataSource = dataSource2;
				this.cmbDistrict.ValueMember = "ID";
				this.cmbDistrict.DisplayMember = "Name";
				if (this.cmbDistrict.Items.Count > 0)
				{
					this.cmbDistrict.SelectedIndex = 0;
					this.weather.DistrictsID = this.cmbDistrict.SelectedValue.ToString();
					this.weather.CityID = this.weather.DistrictsID;
					this.weather.CityName = this.cmbDistrict.Text;
				}
				this.weather.Updated = true;
				this.Redraw();
			}
		}

		private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.CitiesID = comboBox.SelectedValue.ToString();
				this.cmbDistrict.DataSource = null;
				this.cmbDistrict.Items.Clear();
				IList<LedDistrict> dataSource = new List<LedDistrict>();
				for (int i = 0; i < LedGlobal.LedProvinceList.Count; i++)
				{
					if (LedGlobal.LedProvinceList[i].ID == this.weather.ProvincesID)
					{
						IList<LedCity> cities = LedGlobal.LedProvinceList[i].Cities;
						for (int j = 0; j < cities.Count; j++)
						{
							if (cities[j].ID == this.weather.CitiesID)
							{
								dataSource = cities[j].Districts;
								break;
							}
						}
					}
				}
				this.cmbDistrict.DataSource = dataSource;
				this.cmbDistrict.ValueMember = "ID";
				this.cmbDistrict.DisplayMember = "Name";
				if (this.cmbDistrict.Items.Count > 0)
				{
					this.cmbDistrict.SelectedIndex = 0;
					this.weather.DistrictsID = this.cmbDistrict.SelectedValue.ToString();
					this.weather.CityID = this.weather.DistrictsID;
					this.weather.CityName = this.cmbDistrict.Text;
				}
				this.weather.Updated = true;
				this.Redraw();
			}
		}

		private void cmbDistrict_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.DistrictsID = this.cmbDistrict.SelectedValue.ToString();
				this.weather.CityID = this.weather.DistrictsID;
				this.weather.CityName = this.cmbDistrict.Text;
				this.weather.Updated = true;
				this.Redraw();
			}
		}

		private void cmbDisplayStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (!comboBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.DisplayStyle = (LedWeatherDisplayStyle)comboBox.SelectedIndex;
				this.Redraw();
			}
		}

		private void chkDistrict_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.CityEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkMeteorology_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.MeteorologyEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkTemperature_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.TemperatureEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkAirQuality_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.AirQualityEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkWindDirectionForce_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.WindDirectionForceEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkDisaster_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.DisasterEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkSendibleTemperatureIndex_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.SendibleTemperatureIndexEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkColdIndex_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.ColdIndexEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkPollutionIndex_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.PollutionIndexEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkCarwashIndex_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.CarwashIndexEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkAirConditioningIndex_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.AirConditioningIndexEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkDressingIndex_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.DressingIndexEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkUltravioletRayIndex_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.UltravioletRayIndexEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void chkExerciseIndex_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			if (this.weather != null)
			{
				this.weather.ExerciseIndexEnabled = checkBox.Checked;
				this.Redraw();
			}
		}

		private void Redraw()
		{
			if (this.UpdateEvent != null)
			{
				this.UpdateEvent(LedContentEventType.Text, this);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.lblAirQuality = new Label();
			this.chkAirQuality = new CheckBox();
			this.lblMeteorology = new Label();
			this.lblTemperature = new Label();
			this.lblCity = new Label();
			this.chkMeteorology = new CheckBox();
			this.chkTemperature = new CheckBox();
			this.chkCity = new CheckBox();
			this.btnFontUnderline = new Button();
			this.btnFontItalic = new Button();
			this.lblCityList = new Label();
			this.lblFontFamily = new Label();
			this.btnFontBold = new Button();
			this.cmbLineStyle = new ComboBox();
			this.cmbFontSize = new ComboBox();
			this.lblFontSize = new Label();
			this.cmbFontFamily = new ComboBox();
			this.cmbProvince = new ComboBox();
			this.cmbCity = new ComboBox();
			this.cmbDistrict = new ComboBox();
			this.cmbDisplayStyle = new ComboBox();
			this.lblWindDirectionForce = new Label();
			this.chkWindDirectionForce = new CheckBox();
			this.lblDisaster = new Label();
			this.chkDisaster = new CheckBox();
			this.lblAirConditioningIndex = new Label();
			this.chkAirConditioningIndex = new CheckBox();
			this.lblDressingIndex = new Label();
			this.chkDressingIndex = new CheckBox();
			this.lblSendibleTemperatureIndex = new Label();
			this.chkSendibleTemperatureIndex = new CheckBox();
			this.lblColdIndex = new Label();
			this.chkColdIndex = new CheckBox();
			this.lblPollutionIndex = new Label();
			this.chkPollutionIndex = new CheckBox();
			this.lblCarwashIndex = new Label();
			this.chkCarwashIndex = new CheckBox();
			this.lblUltravioletRayIndex = new Label();
			this.chkUltravioletRayIndex = new CheckBox();
			this.lblExerciseIndex = new Label();
			this.chkExerciseIndex = new CheckBox();
			this.cmbForeColor = new ComboBox();
			base.SuspendLayout();
			this.lblAirQuality.Location = new System.Drawing.Point(2, 96);
			this.lblAirQuality.Name = "lblAirQuality";
			this.lblAirQuality.Size = new System.Drawing.Size(85, 12);
			this.lblAirQuality.TabIndex = 107;
			this.lblAirQuality.Text = "空气质量";
			this.lblAirQuality.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkAirQuality.AutoSize = true;
			this.chkAirQuality.ForeColor = System.Drawing.Color.White;
			this.chkAirQuality.ImeMode = ImeMode.NoControl;
			this.chkAirQuality.Location = new System.Drawing.Point(91, 95);
			this.chkAirQuality.Name = "chkAirQuality";
			this.chkAirQuality.Size = new System.Drawing.Size(15, 14);
			this.chkAirQuality.TabIndex = 106;
			this.chkAirQuality.UseVisualStyleBackColor = true;
			this.chkAirQuality.CheckedChanged += new EventHandler(this.chkAirQuality_CheckedChanged);
			this.lblMeteorology.Location = new System.Drawing.Point(111, 67);
			this.lblMeteorology.MinimumSize = new System.Drawing.Size(53, 12);
			this.lblMeteorology.Name = "lblMeteorology";
			this.lblMeteorology.Size = new System.Drawing.Size(85, 12);
			this.lblMeteorology.TabIndex = 104;
			this.lblMeteorology.Text = "天气";
			this.lblMeteorology.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblTemperature.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblTemperature.Location = new System.Drawing.Point(219, 67);
			this.lblTemperature.Name = "lblTemperature";
			this.lblTemperature.Size = new System.Drawing.Size(85, 12);
			this.lblTemperature.TabIndex = 103;
			this.lblTemperature.Text = "温度";
			this.lblTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblCity.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblCity.Location = new System.Drawing.Point(3, 67);
			this.lblCity.Name = "lblCity";
			this.lblCity.Size = new System.Drawing.Size(85, 12);
			this.lblCity.TabIndex = 102;
			this.lblCity.Text = "城市";
			this.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkMeteorology.AutoSize = true;
			this.chkMeteorology.ForeColor = System.Drawing.Color.White;
			this.chkMeteorology.ImeMode = ImeMode.NoControl;
			this.chkMeteorology.Location = new System.Drawing.Point(200, 66);
			this.chkMeteorology.Name = "chkMeteorology";
			this.chkMeteorology.Size = new System.Drawing.Size(15, 14);
			this.chkMeteorology.TabIndex = 98;
			this.chkMeteorology.UseVisualStyleBackColor = true;
			this.chkMeteorology.CheckedChanged += new EventHandler(this.chkMeteorology_CheckedChanged);
			this.chkTemperature.AutoSize = true;
			this.chkTemperature.ForeColor = System.Drawing.Color.White;
			this.chkTemperature.ImeMode = ImeMode.NoControl;
			this.chkTemperature.Location = new System.Drawing.Point(308, 66);
			this.chkTemperature.Name = "chkTemperature";
			this.chkTemperature.Size = new System.Drawing.Size(15, 14);
			this.chkTemperature.TabIndex = 97;
			this.chkTemperature.UseVisualStyleBackColor = true;
			this.chkTemperature.CheckedChanged += new EventHandler(this.chkTemperature_CheckedChanged);
			this.chkCity.AutoSize = true;
			this.chkCity.ForeColor = System.Drawing.Color.White;
			this.chkCity.ImeMode = ImeMode.NoControl;
			this.chkCity.Location = new System.Drawing.Point(92, 66);
			this.chkCity.Name = "chkCity";
			this.chkCity.Size = new System.Drawing.Size(15, 14);
			this.chkCity.TabIndex = 99;
			this.chkCity.UseVisualStyleBackColor = true;
			this.chkCity.CheckedChanged += new EventHandler(this.chkDistrict_CheckedChanged);
			this.btnFontUnderline.BackColor = System.Drawing.Color.White;
			this.btnFontUnderline.Cursor = Cursors.Default;
			this.btnFontUnderline.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
			this.btnFontUnderline.ForeColor = System.Drawing.Color.Black;
			this.btnFontUnderline.ImeMode = ImeMode.NoControl;
			this.btnFontUnderline.Location = new System.Drawing.Point(253, 7);
			this.btnFontUnderline.Name = "btnFontUnderline";
			this.btnFontUnderline.Size = new System.Drawing.Size(21, 21);
			this.btnFontUnderline.TabIndex = 94;
			this.btnFontUnderline.Text = "U";
			this.btnFontUnderline.UseVisualStyleBackColor = false;
			this.btnFontUnderline.Click += new EventHandler(this.btnFontUnderline_Click);
			this.btnFontItalic.BackColor = System.Drawing.Color.White;
			this.btnFontItalic.Cursor = Cursors.Default;
			this.btnFontItalic.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
			this.btnFontItalic.ForeColor = System.Drawing.Color.Black;
			this.btnFontItalic.ImeMode = ImeMode.NoControl;
			this.btnFontItalic.Location = new System.Drawing.Point(232, 7);
			this.btnFontItalic.Name = "btnFontItalic";
			this.btnFontItalic.Size = new System.Drawing.Size(21, 21);
			this.btnFontItalic.TabIndex = 95;
			this.btnFontItalic.Text = "I";
			this.btnFontItalic.UseVisualStyleBackColor = false;
			this.btnFontItalic.Click += new EventHandler(this.btnFontItalic_Click);
			this.lblCityList.BackColor = System.Drawing.Color.Transparent;
			this.lblCityList.Cursor = Cursors.Default;
			this.lblCityList.ForeColor = System.Drawing.Color.Black;
			this.lblCityList.ImeMode = ImeMode.NoControl;
			this.lblCityList.Location = new System.Drawing.Point(0, 36);
			this.lblCityList.Name = "lblCityList";
			this.lblCityList.Size = new System.Drawing.Size(32, 21);
			this.lblCityList.TabIndex = 89;
			this.lblCityList.Text = "城市";
			this.lblCityList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblFontFamily.BackColor = System.Drawing.Color.Transparent;
			this.lblFontFamily.Cursor = Cursors.Default;
			this.lblFontFamily.ForeColor = System.Drawing.Color.Black;
			this.lblFontFamily.ImeMode = ImeMode.NoControl;
			this.lblFontFamily.Location = new System.Drawing.Point(2, 10);
			this.lblFontFamily.Name = "lblFontFamily";
			this.lblFontFamily.Size = new System.Drawing.Size(30, 15);
			this.lblFontFamily.TabIndex = 88;
			this.lblFontFamily.Text = "字体";
			this.lblFontFamily.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnFontBold.BackColor = System.Drawing.Color.White;
			this.btnFontBold.Cursor = Cursors.Default;
			this.btnFontBold.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.btnFontBold.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Bold);
			this.btnFontBold.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnFontBold.ImeMode = ImeMode.NoControl;
			this.btnFontBold.Location = new System.Drawing.Point(211, 7);
			this.btnFontBold.Name = "btnFontBold";
			this.btnFontBold.Size = new System.Drawing.Size(21, 21);
			this.btnFontBold.TabIndex = 96;
			this.btnFontBold.Text = "B";
			this.btnFontBold.UseVisualStyleBackColor = false;
			this.btnFontBold.Click += new EventHandler(this.btnFontBold_Click);
			this.cmbLineStyle.Cursor = Cursors.Default;
			this.cmbLineStyle.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbLineStyle.FormattingEnabled = true;
			this.cmbLineStyle.ImeMode = ImeMode.On;
			this.cmbLineStyle.Items.AddRange(new object[]
			{
				"单行显示",
				"多行显示"
			});
			this.cmbLineStyle.Location = new System.Drawing.Point(335, 7);
			this.cmbLineStyle.MaxLength = 5;
			this.cmbLineStyle.Name = "cmbLineStyle";
			this.cmbLineStyle.Size = new System.Drawing.Size(95, 20);
			this.cmbLineStyle.TabIndex = 92;
			this.cmbLineStyle.SelectedIndexChanged += new EventHandler(this.cmbLineStyle_SelectedIndexChanged);
			this.cmbFontSize.Cursor = Cursors.Default;
			this.cmbFontSize.FormattingEnabled = true;
			this.cmbFontSize.ImeMode = ImeMode.On;
			this.cmbFontSize.Items.AddRange(new object[]
			{
				"8",
				"9",
				"10",
				"11",
				"12",
				"14",
				"16",
				"18",
				"20",
				"22",
				"24",
				"26",
				"28",
				"30",
				"32",
				"34",
				"36",
				"38",
				"40",
				"42",
				"45",
				"46",
				"48",
				"50",
				"52",
				"54",
				"56",
				"58",
				"60",
				"62",
				"64",
				"66",
				"68",
				"70",
				"72",
				"80",
				"90",
				"100",
				"110",
				"120",
				"130",
				"140",
				"150",
				"160",
				"170",
				"180",
				"190",
				"200"
			});
			this.cmbFontSize.Location = new System.Drawing.Point(160, 7);
			this.cmbFontSize.MaxLength = 5;
			this.cmbFontSize.Name = "cmbFontSize";
			this.cmbFontSize.Size = new System.Drawing.Size(47, 20);
			this.cmbFontSize.TabIndex = 91;
			this.cmbFontSize.Text = "14";
			this.cmbFontSize.SelectedIndexChanged += new EventHandler(this.cmbFontSize_SelectedIndexChanged);
			this.cmbFontSize.TextChanged += new EventHandler(this.cmbFontSize_TextChanged);
			this.cmbFontSize.KeyPress += new KeyPressEventHandler(this.cmbFontSize_KeyPress);
			this.lblFontSize.BackColor = System.Drawing.Color.Transparent;
			this.lblFontSize.Cursor = Cursors.Default;
			this.lblFontSize.ForeColor = System.Drawing.Color.Black;
			this.lblFontSize.ImeMode = ImeMode.NoControl;
			this.lblFontSize.Location = new System.Drawing.Point(129, 9);
			this.lblFontSize.Name = "lblFontSize";
			this.lblFontSize.Size = new System.Drawing.Size(35, 15);
			this.lblFontSize.TabIndex = 90;
			this.lblFontSize.Text = "字号";
			this.lblFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cmbFontFamily.Cursor = Cursors.Default;
			this.cmbFontFamily.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbFontFamily.FormattingEnabled = true;
			this.cmbFontFamily.ImeMode = ImeMode.On;
			this.cmbFontFamily.Location = new System.Drawing.Point(32, 7);
			this.cmbFontFamily.Name = "cmbFontFamily";
			this.cmbFontFamily.Size = new System.Drawing.Size(95, 20);
			this.cmbFontFamily.TabIndex = 93;
			this.cmbFontFamily.SelectedIndexChanged += new EventHandler(this.cmbFontFamily_SelectedIndexChanged);
			this.cmbProvince.Cursor = Cursors.Default;
			this.cmbProvince.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbProvince.FormattingEnabled = true;
			this.cmbProvince.ImeMode = ImeMode.On;
			this.cmbProvince.Location = new System.Drawing.Point(32, 37);
			this.cmbProvince.Name = "cmbProvince";
			this.cmbProvince.Size = new System.Drawing.Size(95, 20);
			this.cmbProvince.TabIndex = 108;
			this.cmbProvince.SelectedIndexChanged += new EventHandler(this.cmbProvince_SelectedIndexChanged);
			this.cmbCity.Cursor = Cursors.Default;
			this.cmbCity.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbCity.FormattingEnabled = true;
			this.cmbCity.ImeMode = ImeMode.On;
			this.cmbCity.Location = new System.Drawing.Point(133, 37);
			this.cmbCity.Name = "cmbCity";
			this.cmbCity.Size = new System.Drawing.Size(95, 20);
			this.cmbCity.TabIndex = 109;
			this.cmbCity.SelectedIndexChanged += new EventHandler(this.cmbCity_SelectedIndexChanged);
			this.cmbDistrict.Cursor = Cursors.Default;
			this.cmbDistrict.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDistrict.FormattingEnabled = true;
			this.cmbDistrict.ImeMode = ImeMode.On;
			this.cmbDistrict.Location = new System.Drawing.Point(234, 37);
			this.cmbDistrict.Name = "cmbDistrict";
			this.cmbDistrict.Size = new System.Drawing.Size(95, 20);
			this.cmbDistrict.TabIndex = 110;
			this.cmbDistrict.SelectedIndexChanged += new EventHandler(this.cmbDistrict_SelectedIndexChanged);
			this.cmbDisplayStyle.Cursor = Cursors.Default;
			this.cmbDisplayStyle.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDisplayStyle.FormattingEnabled = true;
			this.cmbDisplayStyle.ImeMode = ImeMode.On;
			this.cmbDisplayStyle.Items.AddRange(new object[]
			{
				"当天",
				"三天"
			});
			this.cmbDisplayStyle.Location = new System.Drawing.Point(335, 37);
			this.cmbDisplayStyle.Name = "cmbDisplayStyle";
			this.cmbDisplayStyle.Size = new System.Drawing.Size(95, 20);
			this.cmbDisplayStyle.TabIndex = 111;
			this.cmbDisplayStyle.SelectedIndexChanged += new EventHandler(this.cmbDisplayStyle_SelectedIndexChanged);
			this.lblWindDirectionForce.Location = new System.Drawing.Point(111, 96);
			this.lblWindDirectionForce.Name = "lblWindDirectionForce";
			this.lblWindDirectionForce.Size = new System.Drawing.Size(85, 12);
			this.lblWindDirectionForce.TabIndex = 113;
			this.lblWindDirectionForce.Text = "风向风力";
			this.lblWindDirectionForce.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkWindDirectionForce.AutoSize = true;
			this.chkWindDirectionForce.ForeColor = System.Drawing.Color.White;
			this.chkWindDirectionForce.ImeMode = ImeMode.NoControl;
			this.chkWindDirectionForce.Location = new System.Drawing.Point(200, 95);
			this.chkWindDirectionForce.Name = "chkWindDirectionForce";
			this.chkWindDirectionForce.Size = new System.Drawing.Size(15, 14);
			this.chkWindDirectionForce.TabIndex = 112;
			this.chkWindDirectionForce.UseVisualStyleBackColor = true;
			this.chkWindDirectionForce.CheckedChanged += new EventHandler(this.chkWindDirectionForce_CheckedChanged);
			this.lblDisaster.Location = new System.Drawing.Point(219, 96);
			this.lblDisaster.Name = "lblDisaster";
			this.lblDisaster.Size = new System.Drawing.Size(85, 12);
			this.lblDisaster.TabIndex = 115;
			this.lblDisaster.Text = "灾害预警";
			this.lblDisaster.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkDisaster.AutoSize = true;
			this.chkDisaster.ForeColor = System.Drawing.Color.White;
			this.chkDisaster.ImeMode = ImeMode.NoControl;
			this.chkDisaster.Location = new System.Drawing.Point(308, 95);
			this.chkDisaster.Name = "chkDisaster";
			this.chkDisaster.Size = new System.Drawing.Size(15, 14);
			this.chkDisaster.TabIndex = 114;
			this.chkDisaster.UseVisualStyleBackColor = true;
			this.chkDisaster.CheckedChanged += new EventHandler(this.chkDisaster_CheckedChanged);
			this.lblAirConditioningIndex.Location = new System.Drawing.Point(2, 154);
			this.lblAirConditioningIndex.Name = "lblAirConditioningIndex";
			this.lblAirConditioningIndex.Size = new System.Drawing.Size(85, 12);
			this.lblAirConditioningIndex.TabIndex = 117;
			this.lblAirConditioningIndex.Text = "空调指数";
			this.lblAirConditioningIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkAirConditioningIndex.AutoSize = true;
			this.chkAirConditioningIndex.ForeColor = System.Drawing.Color.White;
			this.chkAirConditioningIndex.ImeMode = ImeMode.NoControl;
			this.chkAirConditioningIndex.Location = new System.Drawing.Point(92, 153);
			this.chkAirConditioningIndex.Name = "chkAirConditioningIndex";
			this.chkAirConditioningIndex.Size = new System.Drawing.Size(15, 14);
			this.chkAirConditioningIndex.TabIndex = 116;
			this.chkAirConditioningIndex.UseVisualStyleBackColor = true;
			this.chkAirConditioningIndex.CheckedChanged += new EventHandler(this.chkAirConditioningIndex_CheckedChanged);
			this.lblDressingIndex.Location = new System.Drawing.Point(111, 154);
			this.lblDressingIndex.Name = "lblDressingIndex";
			this.lblDressingIndex.Size = new System.Drawing.Size(85, 12);
			this.lblDressingIndex.TabIndex = 119;
			this.lblDressingIndex.Text = "穿衣指数";
			this.lblDressingIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkDressingIndex.AutoSize = true;
			this.chkDressingIndex.ForeColor = System.Drawing.Color.White;
			this.chkDressingIndex.ImeMode = ImeMode.NoControl;
			this.chkDressingIndex.Location = new System.Drawing.Point(200, 153);
			this.chkDressingIndex.Name = "chkDressingIndex";
			this.chkDressingIndex.Size = new System.Drawing.Size(15, 14);
			this.chkDressingIndex.TabIndex = 118;
			this.chkDressingIndex.UseVisualStyleBackColor = true;
			this.chkDressingIndex.CheckedChanged += new EventHandler(this.chkDressingIndex_CheckedChanged);
			this.lblSendibleTemperatureIndex.Location = new System.Drawing.Point(2, 125);
			this.lblSendibleTemperatureIndex.Name = "lblSendibleTemperatureIndex";
			this.lblSendibleTemperatureIndex.Size = new System.Drawing.Size(85, 12);
			this.lblSendibleTemperatureIndex.TabIndex = 121;
			this.lblSendibleTemperatureIndex.Text = "体感温度指数";
			this.lblSendibleTemperatureIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkSendibleTemperatureIndex.AutoSize = true;
			this.chkSendibleTemperatureIndex.ForeColor = System.Drawing.Color.White;
			this.chkSendibleTemperatureIndex.ImeMode = ImeMode.NoControl;
			this.chkSendibleTemperatureIndex.Location = new System.Drawing.Point(92, 124);
			this.chkSendibleTemperatureIndex.Name = "chkSendibleTemperatureIndex";
			this.chkSendibleTemperatureIndex.Size = new System.Drawing.Size(15, 14);
			this.chkSendibleTemperatureIndex.TabIndex = 120;
			this.chkSendibleTemperatureIndex.UseVisualStyleBackColor = true;
			this.chkSendibleTemperatureIndex.CheckedChanged += new EventHandler(this.chkSendibleTemperatureIndex_CheckedChanged);
			this.lblColdIndex.Location = new System.Drawing.Point(111, 125);
			this.lblColdIndex.Name = "lblColdIndex";
			this.lblColdIndex.Size = new System.Drawing.Size(85, 12);
			this.lblColdIndex.TabIndex = 123;
			this.lblColdIndex.Text = "感冒指数";
			this.lblColdIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkColdIndex.AutoSize = true;
			this.chkColdIndex.ForeColor = System.Drawing.Color.White;
			this.chkColdIndex.ImeMode = ImeMode.NoControl;
			this.chkColdIndex.Location = new System.Drawing.Point(200, 124);
			this.chkColdIndex.Name = "chkColdIndex";
			this.chkColdIndex.Size = new System.Drawing.Size(15, 14);
			this.chkColdIndex.TabIndex = 122;
			this.chkColdIndex.UseVisualStyleBackColor = true;
			this.chkColdIndex.CheckedChanged += new EventHandler(this.chkColdIndex_CheckedChanged);
			this.lblPollutionIndex.Location = new System.Drawing.Point(220, 125);
			this.lblPollutionIndex.Name = "lblPollutionIndex";
			this.lblPollutionIndex.Size = new System.Drawing.Size(85, 12);
			this.lblPollutionIndex.TabIndex = 125;
			this.lblPollutionIndex.Text = "污染指数";
			this.lblPollutionIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkPollutionIndex.AutoSize = true;
			this.chkPollutionIndex.ForeColor = System.Drawing.Color.White;
			this.chkPollutionIndex.ImeMode = ImeMode.NoControl;
			this.chkPollutionIndex.Location = new System.Drawing.Point(308, 124);
			this.chkPollutionIndex.Name = "chkPollutionIndex";
			this.chkPollutionIndex.Size = new System.Drawing.Size(15, 14);
			this.chkPollutionIndex.TabIndex = 124;
			this.chkPollutionIndex.UseVisualStyleBackColor = true;
			this.chkPollutionIndex.CheckedChanged += new EventHandler(this.chkPollutionIndex_CheckedChanged);
			this.lblCarwashIndex.Location = new System.Drawing.Point(328, 125);
			this.lblCarwashIndex.Name = "lblCarwashIndex";
			this.lblCarwashIndex.Size = new System.Drawing.Size(85, 12);
			this.lblCarwashIndex.TabIndex = 127;
			this.lblCarwashIndex.Text = "洗车指数";
			this.lblCarwashIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkCarwashIndex.AutoSize = true;
			this.chkCarwashIndex.ForeColor = System.Drawing.Color.White;
			this.chkCarwashIndex.ImeMode = ImeMode.NoControl;
			this.chkCarwashIndex.Location = new System.Drawing.Point(417, 124);
			this.chkCarwashIndex.Name = "chkCarwashIndex";
			this.chkCarwashIndex.Size = new System.Drawing.Size(15, 14);
			this.chkCarwashIndex.TabIndex = 126;
			this.chkCarwashIndex.UseVisualStyleBackColor = true;
			this.chkCarwashIndex.CheckedChanged += new EventHandler(this.chkCarwashIndex_CheckedChanged);
			this.lblUltravioletRayIndex.Location = new System.Drawing.Point(220, 154);
			this.lblUltravioletRayIndex.Name = "lblUltravioletRayIndex";
			this.lblUltravioletRayIndex.Size = new System.Drawing.Size(85, 12);
			this.lblUltravioletRayIndex.TabIndex = 129;
			this.lblUltravioletRayIndex.Text = "紫外线指数";
			this.lblUltravioletRayIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkUltravioletRayIndex.AutoSize = true;
			this.chkUltravioletRayIndex.ForeColor = System.Drawing.Color.White;
			this.chkUltravioletRayIndex.ImeMode = ImeMode.NoControl;
			this.chkUltravioletRayIndex.Location = new System.Drawing.Point(308, 153);
			this.chkUltravioletRayIndex.Name = "chkUltravioletRayIndex";
			this.chkUltravioletRayIndex.Size = new System.Drawing.Size(15, 14);
			this.chkUltravioletRayIndex.TabIndex = 128;
			this.chkUltravioletRayIndex.UseVisualStyleBackColor = true;
			this.chkUltravioletRayIndex.CheckedChanged += new EventHandler(this.chkUltravioletRayIndex_CheckedChanged);
			this.lblExerciseIndex.Location = new System.Drawing.Point(328, 154);
			this.lblExerciseIndex.Name = "lblExerciseIndex";
			this.lblExerciseIndex.Size = new System.Drawing.Size(85, 12);
			this.lblExerciseIndex.TabIndex = 131;
			this.lblExerciseIndex.Text = "运动指数";
			this.lblExerciseIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkExerciseIndex.AutoSize = true;
			this.chkExerciseIndex.ForeColor = System.Drawing.Color.White;
			this.chkExerciseIndex.ImeMode = ImeMode.NoControl;
			this.chkExerciseIndex.Location = new System.Drawing.Point(417, 153);
			this.chkExerciseIndex.Name = "chkExerciseIndex";
			this.chkExerciseIndex.Size = new System.Drawing.Size(15, 14);
			this.chkExerciseIndex.TabIndex = 130;
			this.chkExerciseIndex.UseVisualStyleBackColor = true;
			this.chkExerciseIndex.CheckedChanged += new EventHandler(this.chkExerciseIndex_CheckedChanged);
			this.cmbForeColor.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmbForeColor.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbForeColor.FormattingEnabled = true;
			this.cmbForeColor.ImeMode = ImeMode.On;
			this.cmbForeColor.Items.AddRange(new object[]
			{
				"--",
				"--",
				"--",
				"--",
				"--",
				"--",
				"--",
				"--"
			});
			this.cmbForeColor.Location = new System.Drawing.Point(278, 8);
			this.cmbForeColor.Name = "cmbForeColor";
			this.cmbForeColor.Size = new System.Drawing.Size(51, 22);
			this.cmbForeColor.TabIndex = 132;
			this.cmbForeColor.DrawItem += new DrawItemEventHandler(this.cmbForeColor_DrawItem);
			this.cmbForeColor.SelectedIndexChanged += new EventHandler(this.cmbForeColor_SelectedIndexChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.cmbForeColor);
			base.Controls.Add(this.lblExerciseIndex);
			base.Controls.Add(this.chkExerciseIndex);
			base.Controls.Add(this.lblUltravioletRayIndex);
			base.Controls.Add(this.chkUltravioletRayIndex);
			base.Controls.Add(this.lblCarwashIndex);
			base.Controls.Add(this.chkCarwashIndex);
			base.Controls.Add(this.lblPollutionIndex);
			base.Controls.Add(this.chkPollutionIndex);
			base.Controls.Add(this.lblColdIndex);
			base.Controls.Add(this.chkColdIndex);
			base.Controls.Add(this.lblSendibleTemperatureIndex);
			base.Controls.Add(this.chkSendibleTemperatureIndex);
			base.Controls.Add(this.lblDressingIndex);
			base.Controls.Add(this.chkDressingIndex);
			base.Controls.Add(this.lblAirConditioningIndex);
			base.Controls.Add(this.chkAirConditioningIndex);
			base.Controls.Add(this.lblDisaster);
			base.Controls.Add(this.chkDisaster);
			base.Controls.Add(this.lblWindDirectionForce);
			base.Controls.Add(this.chkWindDirectionForce);
			base.Controls.Add(this.cmbDisplayStyle);
			base.Controls.Add(this.cmbDistrict);
			base.Controls.Add(this.cmbCity);
			base.Controls.Add(this.cmbProvince);
			base.Controls.Add(this.lblAirQuality);
			base.Controls.Add(this.chkAirQuality);
			base.Controls.Add(this.lblMeteorology);
			base.Controls.Add(this.lblTemperature);
			base.Controls.Add(this.lblCity);
			base.Controls.Add(this.chkMeteorology);
			base.Controls.Add(this.chkTemperature);
			base.Controls.Add(this.chkCity);
			base.Controls.Add(this.btnFontUnderline);
			base.Controls.Add(this.btnFontItalic);
			base.Controls.Add(this.lblCityList);
			base.Controls.Add(this.lblFontFamily);
			base.Controls.Add(this.btnFontBold);
			base.Controls.Add(this.cmbLineStyle);
			base.Controls.Add(this.cmbFontSize);
			base.Controls.Add(this.lblFontSize);
			base.Controls.Add(this.cmbFontFamily);
			base.Name = "WeatherEditor";
			base.Size = new System.Drawing.Size(437, 180);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
