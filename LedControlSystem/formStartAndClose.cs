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
	public class formStartAndClose : Form
	{
		private static string formID = "formStartAndClose";

		private formMain fm;

		private string operation;

		private LedTimerSwitch startAndClose;

		private bool isLoadParam;

		private bool isGroup;

		private IContainer components;

		private CheckBox useTimercheckBox8;

		private Button button1;

		private DateTimePicker dateTimePicker1;

		private Label label1;

		private DateTimePicker dateTimePicker4;

		private Label label4;

		private DateTimePicker dateTimePicker3;

		private Label label3;

		private DateTimePicker dateTimePicker2;

		private Label label2;

		private DateTimePicker dateTimePicker5;

		private Label label5;

		private DateTimePicker dateTimePicker6;

		private Label label6;

		private DateTimePicker dateTimePicker7;

		private Label label7;

		private DateTimePicker dateTimePicker8;

		private Label label8;

		private CheckBox startCheckBox1;

		private CheckBox startCheckBox7;

		private CheckBox startCheckBox6;

		private CheckBox startCheckBox5;

		private CheckBox startCheckBox4;

		private CheckBox startCheckBox3;

		private CheckBox startCheckBox2;

		private CheckBox closeCheckBox1;

		private CheckBox closeCheckBox7;

		private CheckBox closeCheckBox6;

		private CheckBox closeCheckBox5;

		private CheckBox closeCheckBox4;

		private CheckBox closeCheckBox3;

		private CheckBox closeCheckBox2;

		private Button button2;

		private GroupBox groupBox2;

		private GroupBox groupBox3;

		public static string FormID
		{
			get
			{
				return formStartAndClose.formID;
			}
			set
			{
				formStartAndClose.formID = value;
			}
		}

		public void Display_Text_Lan()
		{
			this.Text = (this.isGroup ? this.operation : formMain.ML.GetStr("formStartAndClose_FormText"));
			this.useTimercheckBox8.Text = formMain.ML.GetStr("formStartAndClose_checkBox_useTimer");
			this.groupBox2.Text = formMain.ML.GetStr("formStartAndClose_groupBox_AutoPowerONsetting");
			this.groupBox3.Text = formMain.ML.GetStr("formStartAndClose_groupBox_AutoPowerOFFsetting");
			this.startCheckBox2.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Monday");
			this.startCheckBox3.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Tuesday");
			this.startCheckBox4.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Wednesday");
			this.startCheckBox5.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Thursday");
			this.startCheckBox6.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Friday");
			this.startCheckBox7.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Saturday");
			this.startCheckBox1.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_sunday");
			this.closeCheckBox2.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Monday");
			this.closeCheckBox3.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Tuesday");
			this.closeCheckBox4.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Wednesday");
			this.closeCheckBox5.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Thursday");
			this.closeCheckBox6.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Friday");
			this.closeCheckBox7.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_Saturday");
			this.closeCheckBox1.Text = formMain.ML.GetStr("formStartAndClose_DisplayCheckBox_sunday");
			this.label1.Text = formMain.ML.GetStr("formStartAndClose_label_START_TIME1");
			this.label2.Text = formMain.ML.GetStr("formStartAndClose_label_START_TIME2");
			this.label3.Text = formMain.ML.GetStr("formStartAndClose_label_START_TIME3");
			this.label4.Text = formMain.ML.GetStr("formStartAndClose_label_START_TIME4");
			this.label5.Text = formMain.ML.GetStr("formStartAndClose_label_CLOSE_TIME4");
			this.label6.Text = formMain.ML.GetStr("formStartAndClose_label_CLOSE_TIME3");
			this.label7.Text = formMain.ML.GetStr("formStartAndClose_label_CLOSE_TIME2");
			this.label8.Text = formMain.ML.GetStr("formStartAndClose_label_CLOSE_TIME1");
			this.button2.Text = formMain.ML.GetStr("formStartAndClose_button_send");
			this.button1.Text = formMain.ML.GetStr("formStartAndClose_button_confirm");
		}

		public formStartAndClose(LedTimerSwitch pStart, formMain pForm, string pOperation = "", bool group = false)
		{
			this.InitializeComponent();
			this.startAndClose = pStart;
			this.fm = pForm;
			this.operation = pOperation;
			this.isGroup = group;
			this.Display_Text_Lan();
			if (this.startAndClose.Enabled)
			{
				this.groupBox2.Enabled = true;
				this.groupBox3.Enabled = true;
			}
			else
			{
				this.groupBox2.Enabled = false;
				this.groupBox3.Enabled = false;
			}
			this.ShowStartAndColoseParam();
			base.ShowDialog();
			formMain.ML.NowFormID = formStartAndClose.formID;
		}

		private void ShowStartAndColoseParam()
		{
			this.isLoadParam = true;
			if (this.startAndClose.Enabled)
			{
				this.useTimercheckBox8.Checked = true;
			}
			else
			{
				this.useTimercheckBox8.Checked = false;
			}
			if (LedCommon.GetBit(this.startAndClose.OpenWeek, 1))
			{
				this.startCheckBox1.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.OpenWeek, 2))
			{
				this.startCheckBox2.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.OpenWeek, 3))
			{
				this.startCheckBox3.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.OpenWeek, 4))
			{
				this.startCheckBox4.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.OpenWeek, 5))
			{
				this.startCheckBox5.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.OpenWeek, 6))
			{
				this.startCheckBox6.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.OpenWeek, 7))
			{
				this.startCheckBox7.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.CloseWeek, 1))
			{
				this.closeCheckBox1.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.CloseWeek, 2))
			{
				this.closeCheckBox2.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.CloseWeek, 3))
			{
				this.closeCheckBox3.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.CloseWeek, 4))
			{
				this.closeCheckBox4.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.CloseWeek, 5))
			{
				this.closeCheckBox5.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.CloseWeek, 6))
			{
				this.closeCheckBox6.Checked = true;
			}
			if (LedCommon.GetBit(this.startAndClose.CloseWeek, 7))
			{
				this.closeCheckBox7.Checked = true;
			}
			DateTime value = new DateTime(1999, 12, 1, (int)this.startAndClose.OpenHour1, (int)this.startAndClose.OpenMinute1, 0);
			this.dateTimePicker1.Value = value;
			value = new DateTime(1999, 12, 1, (int)this.startAndClose.OpenHour2, (int)this.startAndClose.OpenMinute2, 0);
			this.dateTimePicker2.Value = value;
			value = new DateTime(1999, 12, 1, (int)this.startAndClose.OpenHour3, (int)this.startAndClose.OpenMinute3, 0);
			this.dateTimePicker3.Value = value;
			value = new DateTime(1999, 12, 1, (int)this.startAndClose.OpenHour4, (int)this.startAndClose.OpenMinute4, 0);
			this.dateTimePicker4.Value = value;
			value = new DateTime(1999, 12, 1, (int)this.startAndClose.CloseHour1, (int)this.startAndClose.CloseMinute1, 0);
			this.dateTimePicker8.Value = value;
			value = new DateTime(1999, 12, 1, (int)this.startAndClose.CloseHour2, (int)this.startAndClose.CloseMinute2, 0);
			this.dateTimePicker7.Value = value;
			value = new DateTime(1999, 12, 1, (int)this.startAndClose.CloseHour3, (int)this.startAndClose.CloseMinute3, 0);
			this.dateTimePicker6.Value = value;
			value = new DateTime(1999, 12, 1, (int)this.startAndClose.CloseHour4, (int)this.startAndClose.CloseMinute4, 0);
			this.dateTimePicker5.Value = value;
			this.isLoadParam = false;
		}

		private void useTimercheckBox8_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Checked)
			{
				this.groupBox2.Enabled = true;
				this.groupBox3.Enabled = true;
				this.startAndClose.Enabled = true;
				return;
			}
			this.groupBox2.Enabled = false;
			this.groupBox3.Enabled = false;
			this.startAndClose.Enabled = false;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Dispose();
		}

		private void startCheckBox1_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.OpenWeek = LedCommon.SetBit(this.startAndClose.OpenWeek, 1, checkBox.Checked);
			this.closeCheckBox1.Checked = checkBox.Checked;
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 1, checkBox.Checked);
		}

		private void startCheckBox2_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.OpenWeek = LedCommon.SetBit(this.startAndClose.OpenWeek, 2, checkBox.Checked);
			this.closeCheckBox2.Checked = checkBox.Checked;
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 2, checkBox.Checked);
		}

		private void startCheckBox3_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.OpenWeek = LedCommon.SetBit(this.startAndClose.OpenWeek, 3, checkBox.Checked);
			this.closeCheckBox3.Checked = checkBox.Checked;
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 3, checkBox.Checked);
		}

		private void startCheckBox4_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.OpenWeek = LedCommon.SetBit(this.startAndClose.OpenWeek, 4, checkBox.Checked);
			this.closeCheckBox4.Checked = checkBox.Checked;
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 4, checkBox.Checked);
		}

		private void startCheckBox5_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.OpenWeek = LedCommon.SetBit(this.startAndClose.OpenWeek, 5, checkBox.Checked);
			this.closeCheckBox5.Checked = checkBox.Checked;
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 5, checkBox.Checked);
		}

		private void startCheckBox6_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.OpenWeek = LedCommon.SetBit(this.startAndClose.OpenWeek, 6, checkBox.Checked);
			this.closeCheckBox6.Checked = checkBox.Checked;
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 6, checkBox.Checked);
		}

		private void startCheckBox7_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.OpenWeek = LedCommon.SetBit(this.startAndClose.OpenWeek, 7, checkBox.Checked);
			this.closeCheckBox7.Checked = checkBox.Checked;
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 7, checkBox.Checked);
		}

		private void closeCheckBox1_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 1, checkBox.Checked);
		}

		private void closeCheckBox2_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 2, checkBox.Checked);
		}

		private void closeCheckBox3_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 3, checkBox.Checked);
		}

		private void closeCheckBox4_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 4, checkBox.Checked);
		}

		private void closeCheckBox5_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 5, checkBox.Checked);
		}

		private void closeCheckBox6_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 6, checkBox.Checked);
		}

		private void closeCheckBox7_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (this.isLoadParam)
			{
				return;
			}
			this.startAndClose.CloseWeek = LedCommon.SetBit(this.startAndClose.CloseWeek, 7, checkBox.Checked);
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			if (!dateTimePicker.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			if (dateTimePicker.Value.Hour * 3600 + dateTimePicker.Value.Minute * 60 - (this.dateTimePicker8.Value.Hour * 3600 + this.dateTimePicker8.Value.Minute * 60) > 0)
			{
				dateTimePicker.Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day, (int)this.startAndClose.OpenHour1, (int)this.startAndClose.OpenMinute1, dateTimePicker.Value.Second);
				MessageBox.Show(formMain.ML.GetStr("formStartAndClose_message_startlimit_1"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			this.startAndClose.OpenHour1 = (byte)dateTimePicker.Value.Hour;
			this.startAndClose.OpenMinute1 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
		{
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			if (!dateTimePicker.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			if (dateTimePicker.Value.Hour * 3600 + dateTimePicker.Value.Minute * 60 - (this.dateTimePicker7.Value.Hour * 3600 + this.dateTimePicker7.Value.Minute * 60) > 0)
			{
				dateTimePicker.Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day, (int)this.startAndClose.OpenHour2, (int)this.startAndClose.OpenMinute2, dateTimePicker.Value.Second);
				MessageBox.Show(formMain.ML.GetStr("formStartAndClose_message_startlimit_2"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			this.startAndClose.OpenHour2 = (byte)dateTimePicker.Value.Hour;
			this.startAndClose.OpenMinute2 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
		{
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			if (!dateTimePicker.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			if (dateTimePicker.Value.Hour * 3600 + dateTimePicker.Value.Minute * 60 - (this.dateTimePicker6.Value.Hour * 3600 + this.dateTimePicker6.Value.Minute * 60) > 0)
			{
				dateTimePicker.Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day, (int)this.startAndClose.OpenHour3, (int)this.startAndClose.OpenMinute3, dateTimePicker.Value.Second);
				MessageBox.Show(formMain.ML.GetStr("formStartAndClose_message_startlimit_3"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			this.startAndClose.OpenHour3 = (byte)dateTimePicker.Value.Hour;
			this.startAndClose.OpenMinute3 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
		{
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			if (!dateTimePicker.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			if (dateTimePicker.Value.Hour * 3600 + dateTimePicker.Value.Minute * 60 - (this.dateTimePicker5.Value.Hour * 3600 + this.dateTimePicker5.Value.Minute * 60) > 0)
			{
				dateTimePicker.Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day, (int)this.startAndClose.OpenHour4, (int)this.startAndClose.OpenMinute4, dateTimePicker.Value.Second);
				MessageBox.Show(formMain.ML.GetStr("formStartAndClose_message_startlimit_4"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			this.startAndClose.OpenHour4 = (byte)dateTimePicker.Value.Hour;
			this.startAndClose.OpenMinute4 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
		{
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			if (!dateTimePicker.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			if (dateTimePicker.Value.Hour * 3600 + dateTimePicker.Value.Minute * 60 - (this.dateTimePicker1.Value.Hour * 3600 + this.dateTimePicker1.Value.Minute * 60) < 0)
			{
				dateTimePicker.Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day, (int)this.startAndClose.CloseHour1, (int)this.startAndClose.CloseMinute1, dateTimePicker.Value.Second);
				MessageBox.Show(formMain.ML.GetStr("formStartAndClose_message_Endlimit_1"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			this.startAndClose.CloseHour1 = (byte)dateTimePicker.Value.Hour;
			this.startAndClose.CloseMinute1 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
		{
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			if (!dateTimePicker.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			if (dateTimePicker.Value.Hour * 3600 + dateTimePicker.Value.Minute * 60 - (this.dateTimePicker2.Value.Hour * 3600 + this.dateTimePicker2.Value.Minute * 60) < 0)
			{
				dateTimePicker.Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day, (int)this.startAndClose.CloseHour2, (int)this.startAndClose.CloseMinute2, dateTimePicker.Value.Second);
				MessageBox.Show(formMain.ML.GetStr("formStartAndClose_message_Endlimit_2"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			this.startAndClose.CloseHour2 = (byte)dateTimePicker.Value.Hour;
			this.startAndClose.CloseMinute2 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
		{
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			if (!dateTimePicker.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			if (dateTimePicker.Value.Hour * 3600 + dateTimePicker.Value.Minute * 60 - (this.dateTimePicker3.Value.Hour * 3600 + this.dateTimePicker3.Value.Minute * 60) < 0)
			{
				dateTimePicker.Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day, (int)this.startAndClose.CloseHour3, (int)this.startAndClose.CloseMinute3, dateTimePicker.Value.Second);
				MessageBox.Show(formMain.ML.GetStr("formStartAndClose_message_Endlimit_3"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			this.startAndClose.CloseHour3 = (byte)dateTimePicker.Value.Hour;
			this.startAndClose.CloseMinute3 = (byte)dateTimePicker.Value.Minute;
		}

		private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
		{
			DateTimePicker dateTimePicker = (DateTimePicker)sender;
			if (!dateTimePicker.Focused)
			{
				return;
			}
			if (this.isLoadParam)
			{
				return;
			}
			if (dateTimePicker.Value.Hour * 3600 + dateTimePicker.Value.Minute * 60 - (this.dateTimePicker4.Value.Hour * 3600 + this.dateTimePicker4.Value.Minute * 60) < 0)
			{
				dateTimePicker.Value = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, dateTimePicker.Value.Day, (int)this.startAndClose.CloseHour4, (int)this.startAndClose.CloseMinute4, dateTimePicker.Value.Second);
				MessageBox.Show(formMain.ML.GetStr("formStartAndClose_message_Endlimit_4"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OK);
				return;
			}
			this.startAndClose.CloseHour4 = (byte)dateTimePicker.Value.Hour;
			this.startAndClose.CloseMinute4 = (byte)dateTimePicker.Value.Minute;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
			LedCmdType ledCmdType = LedCmdType.Ctrl_Timer_Switch;
			if (this.isGroup)
			{
				this.fm.GroupSendingSingle(ledCmdType, this.operation, this, this.startAndClose);
				return;
			}
			if (selectedPanel.GetType() == typeof(LedPanelCloud))
			{
				this.fm.CloudSendSingleCmdStart(ledCmdType, this.startAndClose, formMain.ML.GetStr("NETCARD_message_automatic_switching_machine"), (LedPanelCloud)selectedPanel, true, this);
				return;
			}
			this.fm.SendSingleCmdStart(ledCmdType, this.startAndClose, formMain.ML.GetStr("NETCARD_message_automatic_switching_machine"), selectedPanel, true, this);
		}

		private void formStartAndClose_Load(object sender, EventArgs e)
		{
			base.Height = this.groupBox3.Height + 102;
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.BackColor = Template.GroupBox_BackColor;
			formMain.ML.NowFormID = formStartAndClose.formID;
			this.Display_Text_Lan();
		}

		private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}

		private void dateTimePicker8_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
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
			this.useTimercheckBox8 = new CheckBox();
			this.button1 = new Button();
			this.startCheckBox1 = new CheckBox();
			this.dateTimePicker4 = new DateTimePicker();
			this.startCheckBox7 = new CheckBox();
			this.label4 = new Label();
			this.dateTimePicker3 = new DateTimePicker();
			this.startCheckBox6 = new CheckBox();
			this.label3 = new Label();
			this.startCheckBox5 = new CheckBox();
			this.dateTimePicker2 = new DateTimePicker();
			this.label2 = new Label();
			this.startCheckBox4 = new CheckBox();
			this.dateTimePicker1 = new DateTimePicker();
			this.label1 = new Label();
			this.startCheckBox3 = new CheckBox();
			this.startCheckBox2 = new CheckBox();
			this.closeCheckBox1 = new CheckBox();
			this.closeCheckBox7 = new CheckBox();
			this.closeCheckBox6 = new CheckBox();
			this.closeCheckBox5 = new CheckBox();
			this.closeCheckBox4 = new CheckBox();
			this.closeCheckBox3 = new CheckBox();
			this.closeCheckBox2 = new CheckBox();
			this.dateTimePicker5 = new DateTimePicker();
			this.label5 = new Label();
			this.dateTimePicker6 = new DateTimePicker();
			this.label6 = new Label();
			this.dateTimePicker7 = new DateTimePicker();
			this.label7 = new Label();
			this.dateTimePicker8 = new DateTimePicker();
			this.label8 = new Label();
			this.button2 = new Button();
			this.groupBox2 = new GroupBox();
			this.groupBox3 = new GroupBox();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			base.SuspendLayout();
			this.useTimercheckBox8.AutoSize = true;
			this.useTimercheckBox8.BackColor = System.Drawing.Color.Transparent;
			this.useTimercheckBox8.Location = new System.Drawing.Point(12, 6);
			this.useTimercheckBox8.Name = "useTimercheckBox8";
			this.useTimercheckBox8.Size = new System.Drawing.Size(108, 16);
			this.useTimercheckBox8.TabIndex = 2;
			this.useTimercheckBox8.Text = "启用定时开关机";
			this.useTimercheckBox8.UseVisualStyleBackColor = false;
			this.useTimercheckBox8.CheckedChanged += new EventHandler(this.useTimercheckBox8_CheckedChanged);
			this.button1.Location = new System.Drawing.Point(230, 257);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.startCheckBox1.AutoSize = true;
			this.startCheckBox1.Location = new System.Drawing.Point(15, 64);
			this.startCheckBox1.Name = "startCheckBox1";
			this.startCheckBox1.Size = new System.Drawing.Size(60, 16);
			this.startCheckBox1.TabIndex = 14;
			this.startCheckBox1.Text = "星期天";
			this.startCheckBox1.UseVisualStyleBackColor = true;
			this.startCheckBox1.CheckedChanged += new EventHandler(this.startCheckBox1_CheckedChanged);
			this.dateTimePicker4.CustomFormat = "HH:mm";
			this.dateTimePicker4.Enabled = false;
			this.dateTimePicker4.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker4.Location = new System.Drawing.Point(96, 183);
			this.dateTimePicker4.Name = "dateTimePicker4";
			this.dateTimePicker4.ShowUpDown = true;
			this.dateTimePicker4.Size = new System.Drawing.Size(82, 21);
			this.dateTimePicker4.TabIndex = 7;
			this.dateTimePicker4.Visible = false;
			this.dateTimePicker4.ValueChanged += new EventHandler(this.dateTimePicker4_ValueChanged);
			this.startCheckBox7.AutoSize = true;
			this.startCheckBox7.Location = new System.Drawing.Point(147, 42);
			this.startCheckBox7.Name = "startCheckBox7";
			this.startCheckBox7.Size = new System.Drawing.Size(60, 16);
			this.startCheckBox7.TabIndex = 13;
			this.startCheckBox7.Text = "星期六";
			this.startCheckBox7.UseVisualStyleBackColor = true;
			this.startCheckBox7.CheckedChanged += new EventHandler(this.startCheckBox7_CheckedChanged);
			this.label4.AutoSize = true;
			this.label4.Enabled = false;
			this.label4.Location = new System.Drawing.Point(13, 187);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "开机时间4";
			this.label4.Visible = false;
			this.dateTimePicker3.CustomFormat = "HH:mm";
			this.dateTimePicker3.Enabled = false;
			this.dateTimePicker3.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker3.Location = new System.Drawing.Point(96, 156);
			this.dateTimePicker3.Name = "dateTimePicker3";
			this.dateTimePicker3.ShowUpDown = true;
			this.dateTimePicker3.Size = new System.Drawing.Size(82, 21);
			this.dateTimePicker3.TabIndex = 5;
			this.dateTimePicker3.Visible = false;
			this.dateTimePicker3.ValueChanged += new EventHandler(this.dateTimePicker3_ValueChanged);
			this.startCheckBox6.AutoSize = true;
			this.startCheckBox6.Location = new System.Drawing.Point(81, 42);
			this.startCheckBox6.Name = "startCheckBox6";
			this.startCheckBox6.Size = new System.Drawing.Size(60, 16);
			this.startCheckBox6.TabIndex = 12;
			this.startCheckBox6.Text = "星期五";
			this.startCheckBox6.UseVisualStyleBackColor = true;
			this.startCheckBox6.CheckedChanged += new EventHandler(this.startCheckBox6_CheckedChanged);
			this.label3.AutoSize = true;
			this.label3.Enabled = false;
			this.label3.Location = new System.Drawing.Point(13, 160);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "开机时间3";
			this.label3.Visible = false;
			this.startCheckBox5.AutoSize = true;
			this.startCheckBox5.Location = new System.Drawing.Point(15, 42);
			this.startCheckBox5.Name = "startCheckBox5";
			this.startCheckBox5.Size = new System.Drawing.Size(60, 16);
			this.startCheckBox5.TabIndex = 11;
			this.startCheckBox5.Text = "星期四";
			this.startCheckBox5.UseVisualStyleBackColor = true;
			this.startCheckBox5.CheckedChanged += new EventHandler(this.startCheckBox5_CheckedChanged);
			this.dateTimePicker2.CustomFormat = "HH:mm";
			this.dateTimePicker2.Enabled = false;
			this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker2.Location = new System.Drawing.Point(96, 129);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.ShowUpDown = true;
			this.dateTimePicker2.Size = new System.Drawing.Size(82, 21);
			this.dateTimePicker2.TabIndex = 3;
			this.dateTimePicker2.Visible = false;
			this.dateTimePicker2.ValueChanged += new EventHandler(this.dateTimePicker2_ValueChanged);
			this.label2.AutoSize = true;
			this.label2.Enabled = false;
			this.label2.Location = new System.Drawing.Point(13, 133);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "开机时间2";
			this.label2.Visible = false;
			this.startCheckBox4.AutoSize = true;
			this.startCheckBox4.Location = new System.Drawing.Point(147, 20);
			this.startCheckBox4.Name = "startCheckBox4";
			this.startCheckBox4.Size = new System.Drawing.Size(60, 16);
			this.startCheckBox4.TabIndex = 10;
			this.startCheckBox4.Text = "星期三";
			this.startCheckBox4.UseVisualStyleBackColor = true;
			this.startCheckBox4.CheckedChanged += new EventHandler(this.startCheckBox4_CheckedChanged);
			this.dateTimePicker1.CustomFormat = "HH:mm";
			this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(96, 102);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.ShowUpDown = true;
			this.dateTimePicker1.Size = new System.Drawing.Size(82, 21);
			this.dateTimePicker1.TabIndex = 1;
			this.dateTimePicker1.ValueChanged += new EventHandler(this.dateTimePicker1_ValueChanged);
			this.dateTimePicker1.KeyPress += new KeyPressEventHandler(this.dateTimePicker1_KeyPress);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 106);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "开机时间1";
			this.startCheckBox3.AutoSize = true;
			this.startCheckBox3.Location = new System.Drawing.Point(81, 20);
			this.startCheckBox3.Name = "startCheckBox3";
			this.startCheckBox3.Size = new System.Drawing.Size(60, 16);
			this.startCheckBox3.TabIndex = 8;
			this.startCheckBox3.Text = "星期二";
			this.startCheckBox3.UseVisualStyleBackColor = true;
			this.startCheckBox3.CheckedChanged += new EventHandler(this.startCheckBox3_CheckedChanged);
			this.startCheckBox2.AutoSize = true;
			this.startCheckBox2.Location = new System.Drawing.Point(15, 20);
			this.startCheckBox2.Name = "startCheckBox2";
			this.startCheckBox2.Size = new System.Drawing.Size(60, 16);
			this.startCheckBox2.TabIndex = 9;
			this.startCheckBox2.Text = "星期一";
			this.startCheckBox2.UseVisualStyleBackColor = true;
			this.startCheckBox2.CheckedChanged += new EventHandler(this.startCheckBox2_CheckedChanged);
			this.closeCheckBox1.AutoSize = true;
			this.closeCheckBox1.Enabled = false;
			this.closeCheckBox1.Location = new System.Drawing.Point(17, 64);
			this.closeCheckBox1.Name = "closeCheckBox1";
			this.closeCheckBox1.Size = new System.Drawing.Size(60, 16);
			this.closeCheckBox1.TabIndex = 21;
			this.closeCheckBox1.Text = "星期天";
			this.closeCheckBox1.UseVisualStyleBackColor = true;
			this.closeCheckBox1.CheckedChanged += new EventHandler(this.closeCheckBox1_CheckedChanged);
			this.closeCheckBox7.AutoSize = true;
			this.closeCheckBox7.Enabled = false;
			this.closeCheckBox7.Location = new System.Drawing.Point(149, 42);
			this.closeCheckBox7.Name = "closeCheckBox7";
			this.closeCheckBox7.Size = new System.Drawing.Size(60, 16);
			this.closeCheckBox7.TabIndex = 20;
			this.closeCheckBox7.Text = "星期六";
			this.closeCheckBox7.UseVisualStyleBackColor = true;
			this.closeCheckBox7.CheckedChanged += new EventHandler(this.closeCheckBox7_CheckedChanged);
			this.closeCheckBox6.AutoSize = true;
			this.closeCheckBox6.Enabled = false;
			this.closeCheckBox6.Location = new System.Drawing.Point(83, 42);
			this.closeCheckBox6.Name = "closeCheckBox6";
			this.closeCheckBox6.Size = new System.Drawing.Size(60, 16);
			this.closeCheckBox6.TabIndex = 19;
			this.closeCheckBox6.Text = "星期五";
			this.closeCheckBox6.UseVisualStyleBackColor = true;
			this.closeCheckBox6.CheckedChanged += new EventHandler(this.closeCheckBox6_CheckedChanged);
			this.closeCheckBox5.AutoSize = true;
			this.closeCheckBox5.Enabled = false;
			this.closeCheckBox5.Location = new System.Drawing.Point(17, 42);
			this.closeCheckBox5.Name = "closeCheckBox5";
			this.closeCheckBox5.Size = new System.Drawing.Size(60, 16);
			this.closeCheckBox5.TabIndex = 18;
			this.closeCheckBox5.Text = "星期四";
			this.closeCheckBox5.UseVisualStyleBackColor = true;
			this.closeCheckBox5.CheckedChanged += new EventHandler(this.closeCheckBox5_CheckedChanged);
			this.closeCheckBox4.AutoSize = true;
			this.closeCheckBox4.Enabled = false;
			this.closeCheckBox4.Location = new System.Drawing.Point(149, 20);
			this.closeCheckBox4.Name = "closeCheckBox4";
			this.closeCheckBox4.Size = new System.Drawing.Size(60, 16);
			this.closeCheckBox4.TabIndex = 17;
			this.closeCheckBox4.Text = "星期三";
			this.closeCheckBox4.UseVisualStyleBackColor = true;
			this.closeCheckBox4.CheckedChanged += new EventHandler(this.closeCheckBox4_CheckedChanged);
			this.closeCheckBox3.AutoSize = true;
			this.closeCheckBox3.Enabled = false;
			this.closeCheckBox3.Location = new System.Drawing.Point(83, 20);
			this.closeCheckBox3.Name = "closeCheckBox3";
			this.closeCheckBox3.Size = new System.Drawing.Size(60, 16);
			this.closeCheckBox3.TabIndex = 15;
			this.closeCheckBox3.Text = "星期二";
			this.closeCheckBox3.UseVisualStyleBackColor = true;
			this.closeCheckBox3.CheckedChanged += new EventHandler(this.closeCheckBox3_CheckedChanged);
			this.closeCheckBox2.AutoSize = true;
			this.closeCheckBox2.Enabled = false;
			this.closeCheckBox2.Location = new System.Drawing.Point(17, 20);
			this.closeCheckBox2.Name = "closeCheckBox2";
			this.closeCheckBox2.Size = new System.Drawing.Size(60, 16);
			this.closeCheckBox2.TabIndex = 16;
			this.closeCheckBox2.Text = "星期一";
			this.closeCheckBox2.UseVisualStyleBackColor = true;
			this.closeCheckBox2.CheckedChanged += new EventHandler(this.closeCheckBox2_CheckedChanged);
			this.dateTimePicker5.CustomFormat = "HH:mm";
			this.dateTimePicker5.Enabled = false;
			this.dateTimePicker5.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker5.Location = new System.Drawing.Point(98, 182);
			this.dateTimePicker5.Name = "dateTimePicker5";
			this.dateTimePicker5.ShowUpDown = true;
			this.dateTimePicker5.Size = new System.Drawing.Size(82, 21);
			this.dateTimePicker5.TabIndex = 7;
			this.dateTimePicker5.Visible = false;
			this.dateTimePicker5.ValueChanged += new EventHandler(this.dateTimePicker5_ValueChanged);
			this.label5.AutoSize = true;
			this.label5.Enabled = false;
			this.label5.Location = new System.Drawing.Point(15, 186);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 12);
			this.label5.TabIndex = 6;
			this.label5.Text = "关机时间4";
			this.label5.Visible = false;
			this.dateTimePicker6.CustomFormat = "HH:mm";
			this.dateTimePicker6.Enabled = false;
			this.dateTimePicker6.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker6.Location = new System.Drawing.Point(98, 155);
			this.dateTimePicker6.Name = "dateTimePicker6";
			this.dateTimePicker6.ShowUpDown = true;
			this.dateTimePicker6.Size = new System.Drawing.Size(82, 21);
			this.dateTimePicker6.TabIndex = 5;
			this.dateTimePicker6.Visible = false;
			this.dateTimePicker6.ValueChanged += new EventHandler(this.dateTimePicker6_ValueChanged);
			this.label6.AutoSize = true;
			this.label6.Enabled = false;
			this.label6.Location = new System.Drawing.Point(15, 159);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(59, 12);
			this.label6.TabIndex = 4;
			this.label6.Text = "关机时间3";
			this.label6.Visible = false;
			this.dateTimePicker7.CustomFormat = "HH:mm";
			this.dateTimePicker7.Enabled = false;
			this.dateTimePicker7.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker7.Location = new System.Drawing.Point(98, 128);
			this.dateTimePicker7.Name = "dateTimePicker7";
			this.dateTimePicker7.ShowUpDown = true;
			this.dateTimePicker7.Size = new System.Drawing.Size(82, 21);
			this.dateTimePicker7.TabIndex = 3;
			this.dateTimePicker7.Visible = false;
			this.dateTimePicker7.ValueChanged += new EventHandler(this.dateTimePicker7_ValueChanged);
			this.label7.AutoSize = true;
			this.label7.Enabled = false;
			this.label7.Location = new System.Drawing.Point(15, 132);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(59, 12);
			this.label7.TabIndex = 2;
			this.label7.Text = "关机时间2";
			this.label7.Visible = false;
			this.dateTimePicker8.CustomFormat = "HH:mm";
			this.dateTimePicker8.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker8.Location = new System.Drawing.Point(98, 101);
			this.dateTimePicker8.Name = "dateTimePicker8";
			this.dateTimePicker8.ShowUpDown = true;
			this.dateTimePicker8.Size = new System.Drawing.Size(82, 21);
			this.dateTimePicker8.TabIndex = 1;
			this.dateTimePicker8.ValueChanged += new EventHandler(this.dateTimePicker8_ValueChanged);
			this.dateTimePicker8.KeyPress += new KeyPressEventHandler(this.dateTimePicker8_KeyPress);
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(15, 105);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(59, 12);
			this.label8.TabIndex = 0;
			this.label8.Text = "关机时间1";
			this.button2.Location = new System.Drawing.Point(128, 257);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 8;
			this.button2.Text = "发送";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.groupBox2.Controls.Add(this.startCheckBox1);
			this.groupBox2.Controls.Add(this.startCheckBox3);
			this.groupBox2.Controls.Add(this.startCheckBox2);
			this.groupBox2.Controls.Add(this.startCheckBox5);
			this.groupBox2.Controls.Add(this.dateTimePicker4);
			this.groupBox2.Controls.Add(this.dateTimePicker2);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.startCheckBox7);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.startCheckBox6);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.startCheckBox4);
			this.groupBox2.Controls.Add(this.dateTimePicker1);
			this.groupBox2.Controls.Add(this.dateTimePicker3);
			this.groupBox2.Location = new System.Drawing.Point(5, 28);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(216, 223);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "自动开机设置";
			this.groupBox3.Controls.Add(this.closeCheckBox1);
			this.groupBox3.Controls.Add(this.closeCheckBox2);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.closeCheckBox7);
			this.groupBox3.Controls.Add(this.dateTimePicker5);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.dateTimePicker6);
			this.groupBox3.Controls.Add(this.closeCheckBox6);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.dateTimePicker8);
			this.groupBox3.Controls.Add(this.closeCheckBox3);
			this.groupBox3.Controls.Add(this.closeCheckBox5);
			this.groupBox3.Controls.Add(this.dateTimePicker7);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.closeCheckBox4);
			this.groupBox3.Location = new System.Drawing.Point(230, 28);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(216, 223);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "自动关机设置";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(452, 287);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.useTimercheckBox8);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formStartAndClose";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "自动开关机设置";
			base.Load += new EventHandler(this.formStartAndClose_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
