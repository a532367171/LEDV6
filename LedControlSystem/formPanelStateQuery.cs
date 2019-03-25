using LedControlSystem.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem
{
	public class formPanelStateQuery : Form
	{
		private IContainer components;

		private Label lblTimerSwitchState;

		private TextBox txtTimerSwitchState;

		private Label lblLuminanceMode;

		private TextBox txtLuminanceMode;

		private Label lblPanelLockState;

		private TextBox txtPanelLockState;

		private Label lblItemCount;

		private TextBox txtItemCount;

		private Label lblCardState;

		private TextBox txtCardState;

		private Label lblCardTime;

		private TextBox txtCardTime;

		private Label lblTemperatureSensor1;

		private TextBox txtTemperatureSensor1;

		private Label lblHumiditySensor;

		private TextBox txtHumiditySensor;

		private Label lblNoiseSensor;

		private TextBox txtNoiseSensor;

		private Label lblPowerState;

		private TextBox txtPowerState;

		private Label lblLuminanceLevel;

		private TextBox txtLuminanceLevel;

		private Label lblItemPlaying;

		private TextBox txtItemPlaying;

		private Label lblItemLockState;

		private TextBox txtItemLockState;

		private Label lblTemperatureSensor2;

		private TextBox txtTemperatureSensor2;

		private Button btnQuery;

		private PictureBox picLoading;

		public formPanelStateQuery()
		{
			this.InitializeComponent();
		}

		private void formPanelStateQuery_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.picLoading.Visible = false;
			this.btnQuery.Focus();
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
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
			this.lblTimerSwitchState = new Label();
			this.txtTimerSwitchState = new TextBox();
			this.lblLuminanceMode = new Label();
			this.txtLuminanceMode = new TextBox();
			this.lblPanelLockState = new Label();
			this.txtPanelLockState = new TextBox();
			this.lblItemCount = new Label();
			this.txtItemCount = new TextBox();
			this.lblCardState = new Label();
			this.txtCardState = new TextBox();
			this.lblCardTime = new Label();
			this.txtCardTime = new TextBox();
			this.lblTemperatureSensor1 = new Label();
			this.txtTemperatureSensor1 = new TextBox();
			this.lblHumiditySensor = new Label();
			this.txtHumiditySensor = new TextBox();
			this.lblNoiseSensor = new Label();
			this.txtNoiseSensor = new TextBox();
			this.lblPowerState = new Label();
			this.txtPowerState = new TextBox();
			this.lblLuminanceLevel = new Label();
			this.txtLuminanceLevel = new TextBox();
			this.lblItemPlaying = new Label();
			this.txtItemPlaying = new TextBox();
			this.lblItemLockState = new Label();
			this.txtItemLockState = new TextBox();
			this.lblTemperatureSensor2 = new Label();
			this.txtTemperatureSensor2 = new TextBox();
			this.btnQuery = new Button();
			this.picLoading = new PictureBox();
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.lblTimerSwitchState.AutoSize = true;
			this.lblTimerSwitchState.Location = new System.Drawing.Point(15, 28);
			this.lblTimerSwitchState.Name = "lblTimerSwitchState";
			this.lblTimerSwitchState.Size = new System.Drawing.Size(77, 12);
			this.lblTimerSwitchState.TabIndex = 0;
			this.lblTimerSwitchState.Text = "定时开关状态";
			this.txtTimerSwitchState.Location = new System.Drawing.Point(130, 25);
			this.txtTimerSwitchState.Name = "txtTimerSwitchState";
			this.txtTimerSwitchState.Size = new System.Drawing.Size(100, 21);
			this.txtTimerSwitchState.TabIndex = 1;
			this.lblLuminanceMode.AutoSize = true;
			this.lblLuminanceMode.Location = new System.Drawing.Point(15, 57);
			this.lblLuminanceMode.Name = "lblLuminanceMode";
			this.lblLuminanceMode.Size = new System.Drawing.Size(77, 12);
			this.lblLuminanceMode.TabIndex = 0;
			this.lblLuminanceMode.Text = "亮度调整模式";
			this.txtLuminanceMode.Location = new System.Drawing.Point(130, 54);
			this.txtLuminanceMode.Name = "txtLuminanceMode";
			this.txtLuminanceMode.Size = new System.Drawing.Size(100, 21);
			this.txtLuminanceMode.TabIndex = 1;
			this.lblPanelLockState.AutoSize = true;
			this.lblPanelLockState.Location = new System.Drawing.Point(15, 115);
			this.lblPanelLockState.Name = "lblPanelLockState";
			this.lblPanelLockState.Size = new System.Drawing.Size(77, 12);
			this.lblPanelLockState.TabIndex = 0;
			this.lblPanelLockState.Text = "屏幕锁定状态";
			this.txtPanelLockState.Location = new System.Drawing.Point(130, 112);
			this.txtPanelLockState.Name = "txtPanelLockState";
			this.txtPanelLockState.Size = new System.Drawing.Size(100, 21);
			this.txtPanelLockState.TabIndex = 1;
			this.lblItemCount.AutoSize = true;
			this.lblItemCount.Location = new System.Drawing.Point(15, 86);
			this.lblItemCount.Name = "lblItemCount";
			this.lblItemCount.Size = new System.Drawing.Size(41, 12);
			this.lblItemCount.TabIndex = 0;
			this.lblItemCount.Text = "节目数";
			this.txtItemCount.Location = new System.Drawing.Point(130, 83);
			this.txtItemCount.Name = "txtItemCount";
			this.txtItemCount.Size = new System.Drawing.Size(100, 21);
			this.txtItemCount.TabIndex = 1;
			this.lblCardState.AutoSize = true;
			this.lblCardState.Location = new System.Drawing.Point(15, 144);
			this.lblCardState.Name = "lblCardState";
			this.lblCardState.Size = new System.Drawing.Size(65, 12);
			this.lblCardState.TabIndex = 0;
			this.lblCardState.Text = "控制器状态";
			this.txtCardState.Location = new System.Drawing.Point(130, 141);
			this.txtCardState.Name = "txtCardState";
			this.txtCardState.Size = new System.Drawing.Size(100, 21);
			this.txtCardState.TabIndex = 1;
			this.lblCardTime.AutoSize = true;
			this.lblCardTime.Location = new System.Drawing.Point(15, 173);
			this.lblCardTime.Name = "lblCardTime";
			this.lblCardTime.Size = new System.Drawing.Size(53, 12);
			this.lblCardTime.TabIndex = 0;
			this.lblCardTime.Text = "控制时间";
			this.txtCardTime.Location = new System.Drawing.Point(130, 170);
			this.txtCardTime.Name = "txtCardTime";
			this.txtCardTime.Size = new System.Drawing.Size(342, 21);
			this.txtCardTime.TabIndex = 1;
			this.lblTemperatureSensor1.AutoSize = true;
			this.lblTemperatureSensor1.Location = new System.Drawing.Point(15, 202);
			this.lblTemperatureSensor1.Name = "lblTemperatureSensor1";
			this.lblTemperatureSensor1.Size = new System.Drawing.Size(71, 12);
			this.lblTemperatureSensor1.TabIndex = 0;
			this.lblTemperatureSensor1.Text = "温度传感器1";
			this.txtTemperatureSensor1.Location = new System.Drawing.Point(130, 199);
			this.txtTemperatureSensor1.Name = "txtTemperatureSensor1";
			this.txtTemperatureSensor1.Size = new System.Drawing.Size(100, 21);
			this.txtTemperatureSensor1.TabIndex = 1;
			this.lblHumiditySensor.AutoSize = true;
			this.lblHumiditySensor.Location = new System.Drawing.Point(15, 231);
			this.lblHumiditySensor.Name = "lblHumiditySensor";
			this.lblHumiditySensor.Size = new System.Drawing.Size(65, 12);
			this.lblHumiditySensor.TabIndex = 0;
			this.lblHumiditySensor.Text = "湿度传感器";
			this.txtHumiditySensor.Location = new System.Drawing.Point(130, 228);
			this.txtHumiditySensor.Name = "txtHumiditySensor";
			this.txtHumiditySensor.Size = new System.Drawing.Size(100, 21);
			this.txtHumiditySensor.TabIndex = 1;
			this.lblNoiseSensor.AutoSize = true;
			this.lblNoiseSensor.Location = new System.Drawing.Point(255, 231);
			this.lblNoiseSensor.Name = "lblNoiseSensor";
			this.lblNoiseSensor.Size = new System.Drawing.Size(65, 12);
			this.lblNoiseSensor.TabIndex = 0;
			this.lblNoiseSensor.Text = "噪音传感器";
			this.txtNoiseSensor.Location = new System.Drawing.Point(372, 228);
			this.txtNoiseSensor.Name = "txtNoiseSensor";
			this.txtNoiseSensor.Size = new System.Drawing.Size(100, 21);
			this.txtNoiseSensor.TabIndex = 1;
			this.lblPowerState.AutoSize = true;
			this.lblPowerState.Location = new System.Drawing.Point(255, 28);
			this.lblPowerState.Name = "lblPowerState";
			this.lblPowerState.Size = new System.Drawing.Size(77, 12);
			this.lblPowerState.TabIndex = 0;
			this.lblPowerState.Text = "当前开关状态";
			this.txtPowerState.Location = new System.Drawing.Point(372, 25);
			this.txtPowerState.Name = "txtPowerState";
			this.txtPowerState.Size = new System.Drawing.Size(100, 21);
			this.txtPowerState.TabIndex = 1;
			this.lblLuminanceLevel.AutoSize = true;
			this.lblLuminanceLevel.Location = new System.Drawing.Point(255, 57);
			this.lblLuminanceLevel.Name = "lblLuminanceLevel";
			this.lblLuminanceLevel.Size = new System.Drawing.Size(77, 12);
			this.lblLuminanceLevel.TabIndex = 0;
			this.lblLuminanceLevel.Text = "当前亮度等级";
			this.txtLuminanceLevel.Location = new System.Drawing.Point(372, 54);
			this.txtLuminanceLevel.Name = "txtLuminanceLevel";
			this.txtLuminanceLevel.Size = new System.Drawing.Size(100, 21);
			this.txtLuminanceLevel.TabIndex = 1;
			this.lblItemPlaying.AutoSize = true;
			this.lblItemPlaying.Location = new System.Drawing.Point(255, 86);
			this.lblItemPlaying.Name = "lblItemPlaying";
			this.lblItemPlaying.Size = new System.Drawing.Size(77, 12);
			this.lblItemPlaying.TabIndex = 0;
			this.lblItemPlaying.Text = "当前播放节目";
			this.txtItemPlaying.Location = new System.Drawing.Point(372, 83);
			this.txtItemPlaying.Name = "txtItemPlaying";
			this.txtItemPlaying.Size = new System.Drawing.Size(100, 21);
			this.txtItemPlaying.TabIndex = 1;
			this.lblItemLockState.AutoSize = true;
			this.lblItemLockState.Location = new System.Drawing.Point(255, 115);
			this.lblItemLockState.Name = "lblItemLockState";
			this.lblItemLockState.Size = new System.Drawing.Size(77, 12);
			this.lblItemLockState.TabIndex = 0;
			this.lblItemLockState.Text = "节目锁定状态";
			this.txtItemLockState.Location = new System.Drawing.Point(372, 112);
			this.txtItemLockState.Name = "txtItemLockState";
			this.txtItemLockState.Size = new System.Drawing.Size(100, 21);
			this.txtItemLockState.TabIndex = 1;
			this.lblTemperatureSensor2.AutoSize = true;
			this.lblTemperatureSensor2.Location = new System.Drawing.Point(255, 202);
			this.lblTemperatureSensor2.Name = "lblTemperatureSensor2";
			this.lblTemperatureSensor2.Size = new System.Drawing.Size(71, 12);
			this.lblTemperatureSensor2.TabIndex = 0;
			this.lblTemperatureSensor2.Text = "温度传感器2";
			this.txtTemperatureSensor2.Location = new System.Drawing.Point(372, 199);
			this.txtTemperatureSensor2.Name = "txtTemperatureSensor2";
			this.txtTemperatureSensor2.Size = new System.Drawing.Size(100, 21);
			this.txtTemperatureSensor2.TabIndex = 1;
			this.btnQuery.Location = new System.Drawing.Point(397, 265);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new System.Drawing.Size(75, 23);
			this.btnQuery.TabIndex = 2;
			this.btnQuery.Text = "查询";
			this.btnQuery.UseVisualStyleBackColor = true;
			this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.picLoading.Image = Resources.loading;
			this.picLoading.Location = new System.Drawing.Point(216, 121);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(63, 58);
			this.picLoading.SizeMode = PictureBoxSizeMode.StretchImage;
			this.picLoading.TabIndex = 37;
			this.picLoading.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(495, 300);
			base.Controls.Add(this.picLoading);
			base.Controls.Add(this.btnQuery);
			base.Controls.Add(this.txtItemCount);
			base.Controls.Add(this.lblItemCount);
			base.Controls.Add(this.txtTemperatureSensor2);
			base.Controls.Add(this.lblTemperatureSensor2);
			base.Controls.Add(this.txtItemLockState);
			base.Controls.Add(this.lblItemLockState);
			base.Controls.Add(this.txtItemPlaying);
			base.Controls.Add(this.lblItemPlaying);
			base.Controls.Add(this.txtLuminanceLevel);
			base.Controls.Add(this.lblLuminanceLevel);
			base.Controls.Add(this.txtPowerState);
			base.Controls.Add(this.lblPowerState);
			base.Controls.Add(this.txtNoiseSensor);
			base.Controls.Add(this.lblNoiseSensor);
			base.Controls.Add(this.txtHumiditySensor);
			base.Controls.Add(this.lblHumiditySensor);
			base.Controls.Add(this.txtTemperatureSensor1);
			base.Controls.Add(this.lblTemperatureSensor1);
			base.Controls.Add(this.txtCardTime);
			base.Controls.Add(this.lblCardTime);
			base.Controls.Add(this.txtCardState);
			base.Controls.Add(this.lblCardState);
			base.Controls.Add(this.txtPanelLockState);
			base.Controls.Add(this.lblPanelLockState);
			base.Controls.Add(this.txtLuminanceMode);
			base.Controls.Add(this.lblLuminanceMode);
			base.Controls.Add(this.txtTimerSwitchState);
			base.Controls.Add(this.lblTimerSwitchState);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formPanelStateQuery";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "查询显示屏状态";
			base.Load += new EventHandler(this.formPanelStateQuery_Load);
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
