using LedControlSystem.Properties;
using LedModel;
using LedModel.Cloud;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem.LedControlSystem
{
	public class formLuminance : Form
	{
		private static string formID = "formLuminance";

		private formMain fm;

		private LedLuminance luminance;

		private bool isLoadingParam;

		private bool isGroup;

		private string operation;

		public bool isGPRS;

		private string levelEnd;

		public string tempDeviceID;

		public string tempDeviceCode;

		private IContainer components;

		private RadioButton manualRadioButton;

		private RadioButton autoRadioButton2;

		private RadioButton sensorRadioButton3;

		private Button button1;

		private Button button2;

		private CheckBox checkBox1;

		private DateTimePicker dateTimePicker1;

		private Label label1;

		private TrackBar trackBar1;

		private Label label8;

		private TrackBar trackBar8;

		private DateTimePicker dateTimePicker8;

		private CheckBox checkBox8;

		private Label label7;

		private TrackBar trackBar7;

		private DateTimePicker dateTimePicker7;

		private CheckBox checkBox7;

		private Label label6;

		private TrackBar trackBar6;

		private DateTimePicker dateTimePicker6;

		private CheckBox checkBox6;

		private Label label5;

		private TrackBar trackBar5;

		private DateTimePicker dateTimePicker5;

		private CheckBox checkBox5;

		private Label label4;

		private TrackBar trackBar4;

		private DateTimePicker dateTimePicker4;

		private CheckBox checkBox4;

		private Label label3;

		private TrackBar trackBar3;

		private DateTimePicker dateTimePicker3;

		private CheckBox checkBox3;

		private Label label2;

		private TrackBar trackBar2;

		private DateTimePicker dateTimePicker2;

		private CheckBox checkBox2;

		private Label label9;

		private GroupBox autoGroupBox;

		private TrackBar ManualtrackBar;

		private Label label10;

		private GroupBox manualGroupBox;

		public static string FormID
		{
			get
			{
				return formLuminance.formID;
			}
			set
			{
				formLuminance.formID = value;
			}
		}

		public void Language_Text()
		{
			this.Text = (this.isGroup ? this.operation : formMain.ML.GetStr("formLuminance_FormText"));
			this.manualGroupBox.Text = formMain.ML.GetStr("formLuminance_manualGroupBox");
			this.autoGroupBox.Text = formMain.ML.GetStr("formLuminance_GroupBox_auto");
			this.label10.Text = formMain.ML.GetStr("formLuminance_label_15level");
			this.label8.Text = formMain.ML.GetStr("formLuminance_label_15level");
			this.label7.Text = formMain.ML.GetStr("formLuminance_label_15level");
			this.label6.Text = formMain.ML.GetStr("formLuminance_label_15level");
			this.label5.Text = formMain.ML.GetStr("formLuminance_label_15level");
			this.label4.Text = formMain.ML.GetStr("formLuminance_label_15level");
			this.label3.Text = formMain.ML.GetStr("formLuminance_label_15level");
			this.label2.Text = formMain.ML.GetStr("formLuminance_label_15level");
			this.label1.Text = formMain.ML.GetStr("formLuminance_label_15level");
			this.checkBox1.Text = formMain.ML.GetStr("formLuminance_checkBox1");
			this.checkBox2.Text = formMain.ML.GetStr("formLuminance_checkBox2");
			this.checkBox3.Text = formMain.ML.GetStr("formLuminance_checkBox3");
			this.checkBox4.Text = formMain.ML.GetStr("formLuminance_checkBox4");
			this.checkBox5.Text = formMain.ML.GetStr("formLuminance_checkBox5");
			this.checkBox6.Text = formMain.ML.GetStr("formLuminance_checkBox6");
			this.checkBox7.Text = formMain.ML.GetStr("formLuminance_checkBox7");
			this.checkBox8.Text = formMain.ML.GetStr("formLuminance_checkBox8");
			this.sensorRadioButton3.Text = formMain.ML.GetStr("formLuminance_RadioButton_sensorRadio");
			this.autoRadioButton2.Text = formMain.ML.GetStr("formLuminance_RadioButton_autoRadio");
			this.manualRadioButton.Text = formMain.ML.GetStr("formLuminance_RadioButton_manual");
			this.label9.Text = formMain.ML.GetStr("formLuminance_label_prompt");
			this.button1.Text = formMain.ML.GetStr("formLuminance_button_Send");
			this.button2.Text = formMain.ML.GetStr("formLuminance_button_confirm");
		}

		public formLuminance(LedLuminance pLuminance, formMain pForm, string pOperation = "", bool group = false)
		{
			this.InitializeComponent();
			this.luminance = pLuminance;
			this.fm = pForm;
			this.operation = pOperation;
			this.isGroup = group;
			formMain.ML.NowFormID = formLuminance.formID;
			this.Language_Text();
			this.levelEnd = formMain.ML.GetStr("Display_LevelEnd");
			this.ShowLuminanceParam();
		}

		private void ShowLuminanceParam()
		{
			this.isLoadingParam = true;
			if (this.luminance.RegulateMode == 0)
			{
				this.manualRadioButton.Checked = true;
			}
			else if (this.luminance.RegulateMode == 1)
			{
				this.autoRadioButton2.Checked = true;
			}
			else
			{
				this.sensorRadioButton3.Checked = true;
			}
			this.ManualtrackBar.Value = (int)this.luminance.ManualValue;
			this.label10.Text = this.ManualtrackBar.Value.ToString() + this.levelEnd;
			if (LedCommon.GetBit(this.luminance.ValidSetting, 1))
			{
				this.checkBox1.Checked = true;
				this.trackBar1.Enabled = true;
				this.dateTimePicker1.Enabled = true;
				this.label1.Enabled = true;
			}
			if (LedCommon.GetBit(this.luminance.ValidSetting, 2))
			{
				this.checkBox2.Checked = true;
				this.trackBar2.Enabled = true;
				this.dateTimePicker2.Enabled = true;
				this.label2.Enabled = true;
			}
			if (LedCommon.GetBit(this.luminance.ValidSetting, 3))
			{
				this.checkBox3.Checked = true;
				this.trackBar3.Enabled = true;
				this.dateTimePicker3.Enabled = true;
				this.label3.Enabled = true;
			}
			if (LedCommon.GetBit(this.luminance.ValidSetting, 4))
			{
				this.checkBox4.Checked = true;
				this.trackBar4.Enabled = true;
				this.dateTimePicker4.Enabled = true;
				this.label4.Enabled = true;
			}
			if (LedCommon.GetBit(this.luminance.ValidSetting, 5))
			{
				this.checkBox5.Checked = true;
				this.trackBar5.Enabled = true;
				this.dateTimePicker5.Enabled = true;
				this.label5.Enabled = true;
			}
			if (LedCommon.GetBit(this.luminance.ValidSetting, 6))
			{
				this.checkBox6.Checked = true;
				this.trackBar6.Enabled = true;
				this.dateTimePicker6.Enabled = true;
				this.label6.Enabled = true;
			}
			if (LedCommon.GetBit(this.luminance.ValidSetting, 7))
			{
				this.checkBox7.Checked = true;
				this.trackBar7.Enabled = true;
				this.dateTimePicker7.Enabled = true;
				this.label7.Enabled = true;
			}
			if (LedCommon.GetBit(this.luminance.ValidSetting, 8))
			{
				this.checkBox8.Checked = true;
				this.trackBar8.Enabled = true;
				this.dateTimePicker8.Enabled = true;
				this.label8.Enabled = true;
			}
			DateTime value = new DateTime(1999, 12, 1, (int)this.luminance.TimingHour1, (int)this.luminance.TimingMinute1, 0, 0, DateTimeKind.Local);
			this.dateTimePicker1.Value = value;
			this.trackBar1.Value = (int)this.luminance.TimingValue1;
			this.label1.Text = this.luminance.TimingValue1.ToString() + this.levelEnd;
			value = new DateTime(1999, 12, 1, (int)this.luminance.TimingHour2, (int)this.luminance.TimingMinute2, 0, 0, DateTimeKind.Local);
			this.dateTimePicker2.Value = value;
			this.trackBar2.Value = (int)this.luminance.TimingValue2;
			this.label2.Text = this.luminance.TimingValue2.ToString() + this.levelEnd;
			value = new DateTime(1999, 12, 1, (int)this.luminance.TimingHour3, (int)this.luminance.TimingMinute3, 0, 0, DateTimeKind.Local);
			this.dateTimePicker3.Value = value;
			this.trackBar3.Value = (int)this.luminance.TimingValue3;
			this.label3.Text = this.luminance.TimingValue3.ToString() + this.levelEnd;
			value = new DateTime(1999, 12, 1, (int)this.luminance.TimingHour4, (int)this.luminance.TimingMinute4, 0, 0, DateTimeKind.Local);
			this.dateTimePicker4.Value = value;
			this.trackBar4.Value = (int)this.luminance.TimingValue4;
			this.label4.Text = this.luminance.TimingValue4.ToString() + this.levelEnd;
			value = new DateTime(1999, 12, 1, (int)this.luminance.TimingHour5, (int)this.luminance.TimingMinute5, 0, 0, DateTimeKind.Local);
			this.dateTimePicker5.Value = value;
			this.trackBar5.Value = (int)this.luminance.TimingValue5;
			this.label5.Text = this.luminance.TimingValue5.ToString() + this.levelEnd;
			value = new DateTime(1999, 12, 1, (int)this.luminance.TimingHour6, (int)this.luminance.TimingMinute6, 0, 0, DateTimeKind.Local);
			this.dateTimePicker6.Value = value;
			this.trackBar6.Value = (int)this.luminance.TimingValue6;
			this.label6.Text = this.luminance.TimingValue6.ToString() + this.levelEnd;
			value = new DateTime(1999, 12, 1, (int)this.luminance.TimingHour7, (int)this.luminance.TimingMinute7, 0, 0, DateTimeKind.Local);
			this.dateTimePicker7.Value = value;
			this.trackBar7.Value = (int)this.luminance.TimingValue7;
			this.label7.Text = this.luminance.TimingValue7.ToString() + this.levelEnd;
			value = new DateTime(1999, 12, 1, (int)this.luminance.TimingHour8, (int)this.luminance.TimingMinute8, 0, 0, DateTimeKind.Local);
			this.dateTimePicker8.Value = value;
			this.trackBar8.Value = (int)this.luminance.TimingValue8;
			this.label8.Text = this.luminance.TimingValue8.ToString() + this.levelEnd;
			this.isLoadingParam = false;
		}

		private void DisableAll()
		{
			this.checkBox1.Checked = false;
			this.trackBar1.Enabled = false;
			this.dateTimePicker1.Enabled = false;
			this.label1.Enabled = false;
			this.checkBox2.Checked = false;
			this.trackBar2.Enabled = false;
			this.dateTimePicker2.Enabled = false;
			this.label2.Enabled = false;
			this.checkBox3.Checked = false;
			this.trackBar3.Enabled = false;
			this.dateTimePicker3.Enabled = false;
			this.label3.Enabled = false;
			this.checkBox4.Checked = false;
			this.trackBar4.Enabled = false;
			this.dateTimePicker4.Enabled = false;
			this.label4.Enabled = false;
			this.checkBox5.Checked = false;
			this.trackBar5.Enabled = false;
			this.dateTimePicker5.Enabled = false;
			this.label5.Enabled = false;
			this.checkBox6.Checked = false;
			this.trackBar6.Enabled = false;
			this.dateTimePicker6.Enabled = false;
			this.label6.Enabled = false;
			this.checkBox7.Checked = false;
			this.trackBar7.Enabled = false;
			this.dateTimePicker7.Enabled = false;
			this.label7.Enabled = false;
			this.checkBox8.Checked = false;
			this.trackBar8.Enabled = false;
			this.dateTimePicker8.Enabled = false;
			this.label8.Enabled = false;
		}

		private void manualRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (radioButton.Checked)
			{
				this.manualGroupBox.BringToFront();
				this.manualGroupBox.Visible = true;
				this.manualGroupBox.Enabled = true;
				this.luminance.RegulateMode = 0;
			}
		}

		private void autoRadioButton2_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (radioButton.Checked)
			{
				this.autoGroupBox.BringToFront();
				this.autoGroupBox.Enabled = true;
				this.luminance.RegulateMode = 1;
			}
		}

		private void sensorRadioButton3_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (radioButton.Checked)
			{
				this.autoGroupBox.Enabled = false;
				this.manualGroupBox.Enabled = false;
				this.luminance.RegulateMode = 2;
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 1, true);
				this.dateTimePicker1.Enabled = true;
				this.trackBar1.Enabled = true;
				this.label1.Enabled = true;
				return;
			}
			this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 1, false);
			this.dateTimePicker1.Enabled = false;
			this.trackBar1.Enabled = false;
			this.label1.Enabled = false;
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			this.luminance.TimingHour1 = (byte)dateTimePicker.Value.Hour;
			this.luminance.TimingMinute1 = (byte)dateTimePicker.Value.Minute;
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			TrackBar trackBar = (TrackBar)sender;
			this.luminance.TimingValue1 = (byte)trackBar.Value;
			this.label1.Text = trackBar.Value.ToString() + this.levelEnd;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 2, true);
				this.dateTimePicker2.Enabled = true;
				this.trackBar2.Enabled = true;
				this.label2.Enabled = true;
				return;
			}
			this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 2, false);
			this.dateTimePicker2.Enabled = false;
			this.trackBar2.Enabled = false;
			this.label2.Enabled = false;
		}

		private void autoGroupBox_Enter(object sender, EventArgs e)
		{
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 3, true);
				this.dateTimePicker3.Enabled = true;
				this.trackBar3.Enabled = true;
				this.label3.Enabled = true;
				return;
			}
			this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 3, false);
			this.dateTimePicker3.Enabled = false;
			this.trackBar3.Enabled = false;
			this.label3.Enabled = false;
		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 4, true);
				this.dateTimePicker4.Enabled = true;
				this.trackBar4.Enabled = true;
				this.label4.Enabled = true;
				return;
			}
			this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 4, false);
			this.dateTimePicker4.Enabled = false;
			this.trackBar4.Enabled = false;
			this.label4.Enabled = false;
		}

		private void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 5, true);
				this.dateTimePicker5.Enabled = true;
				this.trackBar5.Enabled = true;
				this.label5.Enabled = true;
				return;
			}
			this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 5, false);
			this.dateTimePicker5.Enabled = false;
			this.trackBar5.Enabled = false;
			this.label5.Enabled = false;
		}

		private void checkBox6_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 6, true);
				this.dateTimePicker6.Enabled = true;
				this.trackBar6.Enabled = true;
				this.label6.Enabled = true;
				return;
			}
			this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 6, false);
			this.dateTimePicker6.Enabled = false;
			this.trackBar6.Enabled = false;
			this.label6.Enabled = false;
		}

		private void checkBox7_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 7, true);
				this.dateTimePicker7.Enabled = true;
				this.trackBar7.Enabled = true;
				this.label7.Enabled = true;
				return;
			}
			this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 7, false);
			this.dateTimePicker7.Enabled = false;
			this.trackBar7.Enabled = false;
			this.label7.Enabled = false;
		}

		private void checkBox8_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 8, true);
				this.dateTimePicker8.Enabled = true;
				this.trackBar8.Enabled = true;
				this.label8.Enabled = true;
				return;
			}
			this.luminance.ValidSetting = LedCommon.SetBit(this.luminance.ValidSetting, 8, false);
			this.dateTimePicker8.Enabled = false;
			this.trackBar8.Enabled = false;
			this.label8.Enabled = false;
		}

		private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			this.luminance.TimingHour2 = (byte)dateTimePicker.Value.Hour;
			this.luminance.TimingMinute2 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			this.luminance.TimingHour3 = (byte)dateTimePicker.Value.Hour;
			this.luminance.TimingMinute3 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			this.luminance.TimingHour4 = (byte)dateTimePicker.Value.Hour;
			this.luminance.TimingMinute4 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			this.luminance.TimingHour5 = (byte)dateTimePicker.Value.Hour;
			this.luminance.TimingMinute5 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			this.luminance.TimingHour6 = (byte)dateTimePicker.Value.Hour;
			this.luminance.TimingMinute6 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			this.luminance.TimingHour7 = (byte)dateTimePicker.Value.Hour;
			this.luminance.TimingMinute7 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			this.luminance.TimingHour8 = (byte)dateTimePicker.Value.Hour;
			this.luminance.TimingMinute8 = (byte)dateTimePicker.Value.Minute;
		}

		private void trackBar2_Scroll(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			TrackBar trackBar = (TrackBar)sender;
			this.luminance.TimingValue2 = (byte)trackBar.Value;
			this.label2.Text = trackBar.Value.ToString() + this.levelEnd;
		}

		private void trackBar3_Scroll(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			TrackBar trackBar = (TrackBar)sender;
			this.luminance.TimingValue3 = (byte)trackBar.Value;
			this.label3.Text = trackBar.Value.ToString() + this.levelEnd;
		}

		private void trackBar4_Scroll(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			TrackBar trackBar = (TrackBar)sender;
			this.luminance.TimingValue4 = (byte)trackBar.Value;
			this.label4.Text = trackBar.Value.ToString() + this.levelEnd;
		}

		private void trackBar5_Scroll(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			TrackBar trackBar = (TrackBar)sender;
			this.luminance.TimingValue5 = (byte)trackBar.Value;
			this.label5.Text = trackBar.Value.ToString() + this.levelEnd;
		}

		private void trackBar6_Scroll(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			TrackBar trackBar = (TrackBar)sender;
			this.luminance.TimingValue6 = (byte)trackBar.Value;
			this.label6.Text = trackBar.Value.ToString() + this.levelEnd;
		}

		private void trackBar7_Scroll(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			TrackBar trackBar = (TrackBar)sender;
			this.luminance.TimingValue7 = (byte)trackBar.Value;
			this.label7.Text = trackBar.Value.ToString() + this.levelEnd;
		}

		private void trackBar8_Scroll(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			TrackBar trackBar = (TrackBar)sender;
			this.luminance.TimingValue8 = (byte)trackBar.Value;
			this.label8.Text = trackBar.Value.ToString() + this.levelEnd;
		}

		private void ManualtrackBar_Scroll(object sender, EventArgs e)
		{
			if (this.isLoadingParam)
			{
				return;
			}
			TrackBar trackBar = (TrackBar)sender;
			this.luminance.ManualValue = (byte)trackBar.Value;
			this.label10.Text = trackBar.Value.ToString() + this.levelEnd;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
			LedCmdType ledCmdType = LedCmdType.Ctrl_Luminance;
			byte[] pData = this.luminance.ToBytes();
			if (selectedPanel.IsLSeries())
			{
				pData = this.luminance.ToLBytes();
			}
			if (this.isGPRS)
			{
				this.GprsSend(pData);
				return;
			}
			if (this.isGroup)
			{
				this.fm.GroupSendingSingle(ledCmdType, this.operation, this, this.luminance);
				return;
			}
			if (selectedPanel.GetType() == typeof(LedPanelCloud))
			{
				this.fm.CloudSendSingleCmdStart(ledCmdType, this.luminance, formMain.ML.GetStr("formLuminance_FormText"), (LedPanelCloud)selectedPanel, true, this);
				return;
			}
			this.fm.SendSingleCmdStart(ledCmdType, this.luminance, formMain.ML.GetStr("formLuminance_FormText"), selectedPanel, true, this);
		}

		private void GprsSend(byte[] pData)
		{
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Dispose();
		}

		private void formLuminance_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.manualGroupBox.Size = this.autoGroupBox.Size;
			this.manualGroupBox.Location = this.autoGroupBox.Location;
			this.autoGroupBox.BackColor = Template.GroupBox_BackColor;
			this.manualGroupBox.BackColor = Template.GroupBox_BackColor;
			this.BackColor = Template.GroupBox_BackColor;
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
			this.manualRadioButton = new RadioButton();
			this.autoRadioButton2 = new RadioButton();
			this.sensorRadioButton3 = new RadioButton();
			this.label8 = new Label();
			this.trackBar8 = new TrackBar();
			this.dateTimePicker8 = new DateTimePicker();
			this.checkBox8 = new CheckBox();
			this.label7 = new Label();
			this.trackBar7 = new TrackBar();
			this.dateTimePicker7 = new DateTimePicker();
			this.checkBox7 = new CheckBox();
			this.label6 = new Label();
			this.trackBar6 = new TrackBar();
			this.dateTimePicker6 = new DateTimePicker();
			this.checkBox6 = new CheckBox();
			this.label5 = new Label();
			this.trackBar5 = new TrackBar();
			this.dateTimePicker5 = new DateTimePicker();
			this.checkBox5 = new CheckBox();
			this.label4 = new Label();
			this.trackBar4 = new TrackBar();
			this.dateTimePicker4 = new DateTimePicker();
			this.checkBox4 = new CheckBox();
			this.label3 = new Label();
			this.trackBar3 = new TrackBar();
			this.dateTimePicker3 = new DateTimePicker();
			this.checkBox3 = new CheckBox();
			this.label2 = new Label();
			this.trackBar2 = new TrackBar();
			this.dateTimePicker2 = new DateTimePicker();
			this.checkBox2 = new CheckBox();
			this.label1 = new Label();
			this.trackBar1 = new TrackBar();
			this.dateTimePicker1 = new DateTimePicker();
			this.checkBox1 = new CheckBox();
			this.button1 = new Button();
			this.button2 = new Button();
			this.label9 = new Label();
			this.autoGroupBox = new GroupBox();
			this.ManualtrackBar = new TrackBar();
			this.label10 = new Label();
			this.manualGroupBox = new GroupBox();
			((ISupportInitialize)this.trackBar8).BeginInit();
			((ISupportInitialize)this.trackBar7).BeginInit();
			((ISupportInitialize)this.trackBar6).BeginInit();
			((ISupportInitialize)this.trackBar5).BeginInit();
			((ISupportInitialize)this.trackBar4).BeginInit();
			((ISupportInitialize)this.trackBar3).BeginInit();
			((ISupportInitialize)this.trackBar2).BeginInit();
			((ISupportInitialize)this.trackBar1).BeginInit();
			this.autoGroupBox.SuspendLayout();
			((ISupportInitialize)this.ManualtrackBar).BeginInit();
			this.manualGroupBox.SuspendLayout();
			base.SuspendLayout();
			this.manualRadioButton.AutoSize = true;
			this.manualRadioButton.BackColor = System.Drawing.Color.Transparent;
			this.manualRadioButton.Location = new System.Drawing.Point(69, 6);
			this.manualRadioButton.Name = "manualRadioButton";
			this.manualRadioButton.Size = new System.Drawing.Size(71, 16);
			this.manualRadioButton.TabIndex = 0;
			this.manualRadioButton.TabStop = true;
			this.manualRadioButton.Text = "手动调节";
			this.manualRadioButton.UseVisualStyleBackColor = false;
			this.manualRadioButton.CheckedChanged += new EventHandler(this.manualRadioButton_CheckedChanged);
			this.autoRadioButton2.AutoSize = true;
			this.autoRadioButton2.BackColor = System.Drawing.Color.Transparent;
			this.autoRadioButton2.Location = new System.Drawing.Point(210, 6);
			this.autoRadioButton2.Name = "autoRadioButton2";
			this.autoRadioButton2.Size = new System.Drawing.Size(71, 16);
			this.autoRadioButton2.TabIndex = 1;
			this.autoRadioButton2.TabStop = true;
			this.autoRadioButton2.Text = "自动调节";
			this.autoRadioButton2.UseVisualStyleBackColor = false;
			this.autoRadioButton2.CheckedChanged += new EventHandler(this.autoRadioButton2_CheckedChanged);
			this.sensorRadioButton3.AutoSize = true;
			this.sensorRadioButton3.BackColor = System.Drawing.Color.Transparent;
			this.sensorRadioButton3.Location = new System.Drawing.Point(363, 6);
			this.sensorRadioButton3.Name = "sensorRadioButton3";
			this.sensorRadioButton3.Size = new System.Drawing.Size(83, 16);
			this.sensorRadioButton3.TabIndex = 2;
			this.sensorRadioButton3.TabStop = true;
			this.sensorRadioButton3.Text = "传感器调节";
			this.sensorRadioButton3.UseVisualStyleBackColor = false;
			this.sensorRadioButton3.CheckedChanged += new EventHandler(this.sensorRadioButton3_CheckedChanged);
			this.label8.AutoSize = true;
			this.label8.Enabled = false;
			this.label8.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label8.ForeColor = System.Drawing.SystemColors.MenuText;
			this.label8.Location = new System.Drawing.Point(459, 319);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 19);
			this.label8.TabIndex = 31;
			this.label8.Text = "15级";
			this.trackBar8.Enabled = false;
			this.trackBar8.Location = new System.Drawing.Point(125, 318);
			this.trackBar8.Maximum = 15;
			this.trackBar8.Minimum = 1;
			this.trackBar8.Name = "trackBar8";
			this.trackBar8.Size = new System.Drawing.Size(336, 45);
			this.trackBar8.TabIndex = 30;
			this.trackBar8.Value = 1;
			this.trackBar8.Scroll += new EventHandler(this.trackBar8_Scroll);
			this.dateTimePicker8.CustomFormat = "HH:mm";
			this.dateTimePicker8.Enabled = false;
			this.dateTimePicker8.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker8.Location = new System.Drawing.Point(70, 322);
			this.dateTimePicker8.Name = "dateTimePicker8";
			this.dateTimePicker8.ShowUpDown = true;
			this.dateTimePicker8.Size = new System.Drawing.Size(54, 21);
			this.dateTimePicker8.TabIndex = 29;
			this.dateTimePicker8.ValueChanged += new EventHandler(this.dateTimePicker8_ValueChanged);
			this.checkBox8.Location = new System.Drawing.Point(6, 325);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(60, 16);
			this.checkBox8.TabIndex = 28;
			this.checkBox8.Text = "时间8";
			this.checkBox8.UseVisualStyleBackColor = true;
			this.checkBox8.CheckedChanged += new EventHandler(this.checkBox8_CheckedChanged);
			this.label7.AutoSize = true;
			this.label7.Enabled = false;
			this.label7.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label7.ForeColor = System.Drawing.SystemColors.MenuText;
			this.label7.Location = new System.Drawing.Point(459, 278);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 19);
			this.label7.TabIndex = 27;
			this.label7.Text = "15级";
			this.trackBar7.Enabled = false;
			this.trackBar7.Location = new System.Drawing.Point(125, 277);
			this.trackBar7.Maximum = 15;
			this.trackBar7.Minimum = 1;
			this.trackBar7.Name = "trackBar7";
			this.trackBar7.Size = new System.Drawing.Size(336, 45);
			this.trackBar7.TabIndex = 26;
			this.trackBar7.Value = 1;
			this.trackBar7.Scroll += new EventHandler(this.trackBar7_Scroll);
			this.dateTimePicker7.CustomFormat = "HH:mm";
			this.dateTimePicker7.Enabled = false;
			this.dateTimePicker7.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker7.Location = new System.Drawing.Point(70, 281);
			this.dateTimePicker7.Name = "dateTimePicker7";
			this.dateTimePicker7.ShowUpDown = true;
			this.dateTimePicker7.Size = new System.Drawing.Size(54, 21);
			this.dateTimePicker7.TabIndex = 25;
			this.dateTimePicker7.ValueChanged += new EventHandler(this.dateTimePicker7_ValueChanged);
			this.checkBox7.Location = new System.Drawing.Point(6, 284);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(60, 16);
			this.checkBox7.TabIndex = 24;
			this.checkBox7.Text = "时间7";
			this.checkBox7.UseVisualStyleBackColor = true;
			this.checkBox7.CheckedChanged += new EventHandler(this.checkBox7_CheckedChanged);
			this.label6.AutoSize = true;
			this.label6.Enabled = false;
			this.label6.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label6.ForeColor = System.Drawing.SystemColors.MenuText;
			this.label6.Location = new System.Drawing.Point(459, 236);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 19);
			this.label6.TabIndex = 23;
			this.label6.Text = "15级";
			this.trackBar6.Enabled = false;
			this.trackBar6.Location = new System.Drawing.Point(125, 235);
			this.trackBar6.Maximum = 15;
			this.trackBar6.Minimum = 1;
			this.trackBar6.Name = "trackBar6";
			this.trackBar6.Size = new System.Drawing.Size(336, 45);
			this.trackBar6.TabIndex = 22;
			this.trackBar6.Value = 1;
			this.trackBar6.Scroll += new EventHandler(this.trackBar6_Scroll);
			this.dateTimePicker6.CustomFormat = "HH:mm";
			this.dateTimePicker6.Enabled = false;
			this.dateTimePicker6.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker6.Location = new System.Drawing.Point(70, 239);
			this.dateTimePicker6.Name = "dateTimePicker6";
			this.dateTimePicker6.ShowUpDown = true;
			this.dateTimePicker6.Size = new System.Drawing.Size(54, 21);
			this.dateTimePicker6.TabIndex = 21;
			this.dateTimePicker6.ValueChanged += new EventHandler(this.dateTimePicker6_ValueChanged);
			this.checkBox6.Location = new System.Drawing.Point(6, 242);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(60, 16);
			this.checkBox6.TabIndex = 20;
			this.checkBox6.Text = "时间6";
			this.checkBox6.UseVisualStyleBackColor = true;
			this.checkBox6.CheckedChanged += new EventHandler(this.checkBox6_CheckedChanged);
			this.label5.AutoSize = true;
			this.label5.Enabled = false;
			this.label5.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label5.ForeColor = System.Drawing.SystemColors.MenuText;
			this.label5.Location = new System.Drawing.Point(459, 192);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 19);
			this.label5.TabIndex = 19;
			this.label5.Text = "15级";
			this.trackBar5.Enabled = false;
			this.trackBar5.Location = new System.Drawing.Point(125, 191);
			this.trackBar5.Maximum = 15;
			this.trackBar5.Minimum = 1;
			this.trackBar5.Name = "trackBar5";
			this.trackBar5.Size = new System.Drawing.Size(336, 45);
			this.trackBar5.TabIndex = 18;
			this.trackBar5.Value = 1;
			this.trackBar5.Scroll += new EventHandler(this.trackBar5_Scroll);
			this.dateTimePicker5.CustomFormat = "HH:mm";
			this.dateTimePicker5.Enabled = false;
			this.dateTimePicker5.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker5.Location = new System.Drawing.Point(70, 195);
			this.dateTimePicker5.Name = "dateTimePicker5";
			this.dateTimePicker5.ShowUpDown = true;
			this.dateTimePicker5.Size = new System.Drawing.Size(54, 21);
			this.dateTimePicker5.TabIndex = 17;
			this.dateTimePicker5.ValueChanged += new EventHandler(this.dateTimePicker5_ValueChanged);
			this.checkBox5.Location = new System.Drawing.Point(6, 198);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(60, 16);
			this.checkBox5.TabIndex = 16;
			this.checkBox5.Text = "时间5";
			this.checkBox5.UseVisualStyleBackColor = true;
			this.checkBox5.CheckedChanged += new EventHandler(this.checkBox5_CheckedChanged);
			this.label4.AutoSize = true;
			this.label4.Enabled = false;
			this.label4.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label4.ForeColor = System.Drawing.SystemColors.MenuText;
			this.label4.Location = new System.Drawing.Point(459, 147);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 19);
			this.label4.TabIndex = 15;
			this.label4.Text = "15级";
			this.trackBar4.Enabled = false;
			this.trackBar4.Location = new System.Drawing.Point(125, 146);
			this.trackBar4.Maximum = 15;
			this.trackBar4.Minimum = 1;
			this.trackBar4.Name = "trackBar4";
			this.trackBar4.Size = new System.Drawing.Size(336, 45);
			this.trackBar4.TabIndex = 14;
			this.trackBar4.Value = 1;
			this.trackBar4.Scroll += new EventHandler(this.trackBar4_Scroll);
			this.dateTimePicker4.CustomFormat = "HH:mm";
			this.dateTimePicker4.Enabled = false;
			this.dateTimePicker4.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker4.Location = new System.Drawing.Point(70, 150);
			this.dateTimePicker4.Name = "dateTimePicker4";
			this.dateTimePicker4.ShowUpDown = true;
			this.dateTimePicker4.Size = new System.Drawing.Size(54, 21);
			this.dateTimePicker4.TabIndex = 13;
			this.dateTimePicker4.ValueChanged += new EventHandler(this.dateTimePicker4_ValueChanged);
			this.checkBox4.Location = new System.Drawing.Point(6, 153);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(60, 16);
			this.checkBox4.TabIndex = 12;
			this.checkBox4.Text = "时间4";
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox4.CheckedChanged += new EventHandler(this.checkBox4_CheckedChanged);
			this.label3.AutoSize = true;
			this.label3.Enabled = false;
			this.label3.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label3.ForeColor = System.Drawing.SystemColors.MenuText;
			this.label3.Location = new System.Drawing.Point(459, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 19);
			this.label3.TabIndex = 11;
			this.label3.Text = "15级";
			this.trackBar3.Enabled = false;
			this.trackBar3.Location = new System.Drawing.Point(125, 101);
			this.trackBar3.Maximum = 15;
			this.trackBar3.Minimum = 1;
			this.trackBar3.Name = "trackBar3";
			this.trackBar3.Size = new System.Drawing.Size(336, 45);
			this.trackBar3.TabIndex = 10;
			this.trackBar3.Value = 1;
			this.trackBar3.Scroll += new EventHandler(this.trackBar3_Scroll);
			this.dateTimePicker3.CustomFormat = "HH:mm";
			this.dateTimePicker3.Enabled = false;
			this.dateTimePicker3.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker3.Location = new System.Drawing.Point(70, 105);
			this.dateTimePicker3.Name = "dateTimePicker3";
			this.dateTimePicker3.ShowUpDown = true;
			this.dateTimePicker3.Size = new System.Drawing.Size(54, 21);
			this.dateTimePicker3.TabIndex = 9;
			this.dateTimePicker3.ValueChanged += new EventHandler(this.dateTimePicker3_ValueChanged);
			this.checkBox3.Location = new System.Drawing.Point(6, 108);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(60, 16);
			this.checkBox3.TabIndex = 8;
			this.checkBox3.Text = "时间3";
			this.checkBox3.UseVisualStyleBackColor = true;
			this.checkBox3.CheckedChanged += new EventHandler(this.checkBox3_CheckedChanged);
			this.label2.AutoSize = true;
			this.label2.Enabled = false;
			this.label2.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label2.ForeColor = System.Drawing.SystemColors.MenuText;
			this.label2.Location = new System.Drawing.Point(459, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 19);
			this.label2.TabIndex = 7;
			this.label2.Text = "15级";
			this.trackBar2.Enabled = false;
			this.trackBar2.Location = new System.Drawing.Point(125, 58);
			this.trackBar2.Maximum = 15;
			this.trackBar2.Minimum = 1;
			this.trackBar2.Name = "trackBar2";
			this.trackBar2.Size = new System.Drawing.Size(336, 45);
			this.trackBar2.TabIndex = 6;
			this.trackBar2.Value = 1;
			this.trackBar2.Scroll += new EventHandler(this.trackBar2_Scroll);
			this.dateTimePicker2.CustomFormat = "HH:mm";
			this.dateTimePicker2.Enabled = false;
			this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker2.Location = new System.Drawing.Point(70, 62);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.ShowUpDown = true;
			this.dateTimePicker2.Size = new System.Drawing.Size(54, 21);
			this.dateTimePicker2.TabIndex = 5;
			this.dateTimePicker2.ValueChanged += new EventHandler(this.dateTimePicker2_ValueChanged);
			this.checkBox2.Location = new System.Drawing.Point(6, 65);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(60, 16);
			this.checkBox2.TabIndex = 4;
			this.checkBox2.Text = "时间2";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
			this.label1.AutoSize = true;
			this.label1.Enabled = false;
			this.label1.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label1.ForeColor = System.Drawing.SystemColors.MenuText;
			this.label1.Location = new System.Drawing.Point(459, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 19);
			this.label1.TabIndex = 3;
			this.label1.Text = "15级";
			this.trackBar1.Enabled = false;
			this.trackBar1.Location = new System.Drawing.Point(125, 13);
			this.trackBar1.Maximum = 15;
			this.trackBar1.Minimum = 1;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(336, 45);
			this.trackBar1.TabIndex = 2;
			this.trackBar1.Value = 1;
			this.trackBar1.Scroll += new EventHandler(this.trackBar1_Scroll);
			this.dateTimePicker1.CustomFormat = "HH:mm";
			this.dateTimePicker1.Enabled = false;
			this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(70, 17);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.ShowUpDown = true;
			this.dateTimePicker1.Size = new System.Drawing.Size(54, 21);
			this.dateTimePicker1.TabIndex = 1;
			this.dateTimePicker1.ValueChanged += new EventHandler(this.dateTimePicker1_ValueChanged);
			this.checkBox1.Location = new System.Drawing.Point(6, 20);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(60, 16);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "时间1";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
			this.button1.Location = new System.Drawing.Point(153, 426);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "发送";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new System.Drawing.Point(318, 426);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "确定";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(137, 406);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(239, 12);
			this.label9.TabIndex = 6;
			this.label9.Text = "如果使用U盘模式,请在设置完毕后点击确定!";
			this.autoGroupBox.Controls.Add(this.label8);
			this.autoGroupBox.Controls.Add(this.checkBox1);
			this.autoGroupBox.Controls.Add(this.trackBar1);
			this.autoGroupBox.Controls.Add(this.label4);
			this.autoGroupBox.Controls.Add(this.trackBar8);
			this.autoGroupBox.Controls.Add(this.checkBox5);
			this.autoGroupBox.Controls.Add(this.trackBar4);
			this.autoGroupBox.Controls.Add(this.dateTimePicker8);
			this.autoGroupBox.Controls.Add(this.dateTimePicker5);
			this.autoGroupBox.Controls.Add(this.dateTimePicker1);
			this.autoGroupBox.Controls.Add(this.dateTimePicker4);
			this.autoGroupBox.Controls.Add(this.checkBox8);
			this.autoGroupBox.Controls.Add(this.trackBar5);
			this.autoGroupBox.Controls.Add(this.label1);
			this.autoGroupBox.Controls.Add(this.checkBox4);
			this.autoGroupBox.Controls.Add(this.label7);
			this.autoGroupBox.Controls.Add(this.label5);
			this.autoGroupBox.Controls.Add(this.checkBox2);
			this.autoGroupBox.Controls.Add(this.label3);
			this.autoGroupBox.Controls.Add(this.trackBar7);
			this.autoGroupBox.Controls.Add(this.checkBox6);
			this.autoGroupBox.Controls.Add(this.dateTimePicker2);
			this.autoGroupBox.Controls.Add(this.trackBar3);
			this.autoGroupBox.Controls.Add(this.dateTimePicker7);
			this.autoGroupBox.Controls.Add(this.dateTimePicker6);
			this.autoGroupBox.Controls.Add(this.trackBar2);
			this.autoGroupBox.Controls.Add(this.dateTimePicker3);
			this.autoGroupBox.Controls.Add(this.checkBox7);
			this.autoGroupBox.Controls.Add(this.trackBar6);
			this.autoGroupBox.Controls.Add(this.label2);
			this.autoGroupBox.Controls.Add(this.checkBox3);
			this.autoGroupBox.Controls.Add(this.label6);
			this.autoGroupBox.Location = new System.Drawing.Point(10, 28);
			this.autoGroupBox.Name = "autoGroupBox";
			this.autoGroupBox.Size = new System.Drawing.Size(542, 366);
			this.autoGroupBox.TabIndex = 23;
			this.autoGroupBox.TabStop = false;
			this.autoGroupBox.Text = "自动调节";
			this.ManualtrackBar.Location = new System.Drawing.Point(6, 20);
			this.ManualtrackBar.Maximum = 15;
			this.ManualtrackBar.Minimum = 1;
			this.ManualtrackBar.Name = "ManualtrackBar";
			this.ManualtrackBar.Size = new System.Drawing.Size(412, 45);
			this.ManualtrackBar.TabIndex = 7;
			this.ManualtrackBar.Value = 1;
			this.ManualtrackBar.Scroll += new EventHandler(this.ManualtrackBar_Scroll);
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label10.ForeColor = System.Drawing.SystemColors.MenuText;
			this.label10.Location = new System.Drawing.Point(424, 23);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(48, 19);
			this.label10.TabIndex = 8;
			this.label10.Text = "15级";
			this.manualGroupBox.Controls.Add(this.label10);
			this.manualGroupBox.Controls.Add(this.ManualtrackBar);
			this.manualGroupBox.Location = new System.Drawing.Point(12, 28);
			this.manualGroupBox.Name = "manualGroupBox";
			this.manualGroupBox.Size = new System.Drawing.Size(540, 366);
			this.manualGroupBox.TabIndex = 24;
			this.manualGroupBox.TabStop = false;
			this.manualGroupBox.Text = "手动调节";
			this.manualGroupBox.Visible = false;
			base.ClientSize = new System.Drawing.Size(558, 453);
			base.Controls.Add(this.manualGroupBox);
			base.Controls.Add(this.autoGroupBox);
			base.Controls.Add(this.sensorRadioButton3);
			base.Controls.Add(this.autoRadioButton2);
			base.Controls.Add(this.manualRadioButton);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.button2);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formLuminance";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "亮度设置";
			base.Load += new EventHandler(this.formLuminance_Load);
			((ISupportInitialize)this.trackBar8).EndInit();
			((ISupportInitialize)this.trackBar7).EndInit();
			((ISupportInitialize)this.trackBar6).EndInit();
			((ISupportInitialize)this.trackBar5).EndInit();
			((ISupportInitialize)this.trackBar4).EndInit();
			((ISupportInitialize)this.trackBar3).EndInit();
			((ISupportInitialize)this.trackBar2).EndInit();
			((ISupportInitialize)this.trackBar1).EndInit();
			this.autoGroupBox.ResumeLayout(false);
			this.autoGroupBox.PerformLayout();
			((ISupportInitialize)this.ManualtrackBar).EndInit();
			this.manualGroupBox.ResumeLayout(false);
			this.manualGroupBox.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
