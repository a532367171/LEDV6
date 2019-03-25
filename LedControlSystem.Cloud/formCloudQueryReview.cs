using LedControlSystem.LedControlSystem;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Cloud;
using LedModel.Enum;
using LedModel.Foundation;
using LedService.Cloud.Group;
using LedService.Cloud.Pending;
using LedService.Cloud.Terminal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem.Cloud
{
	public class formCloudQueryReview : Form
	{
		private bool needtoClose;

		private IContainer components;

		private ToolStrip tsMenu;

		private ToolStripButton tsbtnUser;

		private ToolStripButton tsbtnGroup;

		private ToolStripButton tsbtnTerminalParameter;

		private TreeView tvwTerminal;

		private TabControl tabFunction;

		private TabPage tpTransData;

		private TabPage tpPending;

		private RadioButton rdoTransDataException;

		private RadioButton rdoTransDataComplete;

		private RadioButton rdoTransDataWaiting;

		private RadioButton rdoTransDataAll;

		private Button btnTransDataRefresh;

		private Button btnTransDataDelete;

		private RadioButton rdoPendingAll;

		private RadioButton rdoPendingPass;

		private RadioButton rdoPendingReject;

		private RadioButton rdoPendingWaiting;

		private Button btnPendingRefresh;

		private Button btnPendingPass;

		private Button btnPendingReject;

		private PictureBox picLoading;

		private Label lblTerminal;

		private Button btnTerminalRefresh;

		private DataGridView dgvPending;

		private DataGridView dgvTransData;

		private DataGridViewTextBoxColumn ID;

		private DataGridViewTextBoxColumn dgvtxtcCreateTime;

		private DataGridViewTextBoxColumn dgvtxtcUpdateTime;

		private DataGridViewTextBoxColumn Description;

		private DataGridViewTextBoxColumn DataStatus;

		private DataGridViewTextBoxColumn dgvctxtcStatusValue;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumnIndex;

		private DataGridViewTextBoxColumn dgvtxtcTerminalGroupID;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumnUpdate;

		private DataGridViewTextBoxColumn dgvtxtcCreator;

		private DataGridViewTextBoxColumn dgvtxtcChecker;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dgvtxtcCheckStatus;

		public formCloudQueryReview()
		{
			this.InitializeComponent();
			this.DisplayLanuageText();
		}

		private void DisplayLanuageText()
		{
			this.Text = formMain.ML.GetStr("formCloudQueryReview_Form_CloudQueryReview");
			this.tsbtnUser.Text = formMain.ML.GetStr("formCloudQueryReview_ToolStripButton_User");
			this.tsbtnGroup.Text = formMain.ML.GetStr("formCloudQueryReview_ToolStripButton_Group");
			this.tsbtnTerminalParameter.Text = formMain.ML.GetStr("formCloudQueryReview_ToolStripButton_TerminalParameter");
			this.lblTerminal.Text = formMain.ML.GetStr("formCloudQueryReview_Label_Terminal");
			this.tpPending.Text = formMain.ML.GetStr("formCloudQueryReview_TabPage_Pending");
			this.rdoPendingAll.Text = formMain.ML.GetStr("formCloudQueryReview_RadioButton_PendingAll");
			this.rdoPendingWaiting.Text = formMain.ML.GetStr("formCloudQueryReview_RadioButton_PendingWaiting");
			this.rdoPendingPass.Text = formMain.ML.GetStr("formCloudQueryReview_RadioButton_PendingPass");
			this.rdoPendingReject.Text = formMain.ML.GetStr("formCloudQueryReview_RadioButton_PendingReject");
			this.btnPendingRefresh.Text = formMain.ML.GetStr("formCloudQueryReview_Button_PendingRefresh");
			this.btnPendingPass.Text = formMain.ML.GetStr("formCloudQueryReview_Button_PendingPass");
			this.btnPendingReject.Text = formMain.ML.GetStr("formCloudQueryReview_Button_PendingReject");
			this.dgvPending.Columns[2].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_PendingTerminalGroup");
			this.dgvPending.Columns[3].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_PendingCreateTime");
			this.dgvPending.Columns[4].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_PendingCheckTime");
			this.dgvPending.Columns[5].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_PendingCreator");
			this.dgvPending.Columns[6].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_PendingChecker");
			this.dgvPending.Columns[7].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_PendingState");
			this.tpTransData.Text = formMain.ML.GetStr("formCloudQueryReview_TabPage_TransData");
			this.rdoTransDataAll.Text = formMain.ML.GetStr("formCloudQueryReview_RadioButton_TransDataAll");
			this.rdoTransDataWaiting.Text = formMain.ML.GetStr("formCloudQueryReview_RadioButton_TransDataWaiting");
			this.rdoTransDataComplete.Text = formMain.ML.GetStr("formCloudQueryReview_RadioButton_TransDataComplete");
			this.rdoTransDataException.Text = formMain.ML.GetStr("formCloudQueryReview_RadioButton_TransDataException");
			this.btnTransDataRefresh.Text = formMain.ML.GetStr("formCloudQueryReview_Button_TransDataRefresh");
			this.btnTransDataDelete.Text = formMain.ML.GetStr("formCloudQueryReview_Button_TransDataDelete");
			this.dgvTransData.Columns[1].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_TransDataCreateTime");
			this.dgvTransData.Columns[2].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_TransDataUpdateTime");
			this.dgvTransData.Columns[3].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_TransDataDescription");
			this.dgvTransData.Columns[4].HeaderText = formMain.ML.GetStr("formCloudQueryReview_DataGridView_TransDataState");
		}

		private void formCloudQueryReview_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.rdoTransDataAll.Checked = true;
			this.rdoPendingAll.Checked = true;
			this.btnPendingPass.Enabled = true;
			this.btnPendingReject.Enabled = true;
			Thread thread = new Thread(new ThreadStart(this.LoadAll));
			thread.Start();
		}

		private void formCloudQueryReview_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.needtoClose)
			{
				e.Cancel = true;
			}
		}

		private void tsbtnUser_Click(object sender, EventArgs e)
		{
			formCloudUser formCloudUser = new formCloudUser();
			formCloudUser.ShowDialog(this);
		}

		private void tsbtnGroup_Click(object sender, EventArgs e)
		{
			formCloudGroup formCloudGroup = new formCloudGroup();
			formCloudGroup.ShowDialog(this);
		}

		private void tsbtnTerminalParameter_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwTerminal.SelectedNode;
			if (selectedNode == null)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Please_Select_Terminal"), formMain.ML.GetStr("Display_Prompt"));
				this.tvwTerminal.Focus();
				return;
			}
			if (selectedNode.Text.StartsWith(formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Offline")))
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Terminal_Is_Offline"), formMain.ML.GetStr("Display_Prompt"));
				this.tvwTerminal.Focus();
				return;
			}
			string text = selectedNode.Tag.ToString();
			string[] array = text.Split(new char[]
			{
				'|'
			});
			formCloudTerminalParameter formCloudTerminalParameter = new formCloudTerminalParameter(array[0]);
			formCloudTerminalParameter.ShowDialog(this);
		}

		private void tvwTerminal_MouseDown(object sender, MouseEventArgs e)
		{
			TreeNode nodeAt = this.tvwTerminal.GetNodeAt(e.Location);
			if (nodeAt == null)
			{
				return;
			}
			TreeNode selectedNode = this.tvwTerminal.SelectedNode;
			if (selectedNode == nodeAt)
			{
				return;
			}
			Thread thread = new Thread(new ThreadStart(this.LoadTransDataPending));
			thread.Start();
		}

		private void tvwTerminal_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;
		}

		private void btnTerminalRefresh_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(this.LoadAll));
			thread.Start();
		}

		private void rdoTransDataAll_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			Thread thread = new Thread(new ThreadStart(this.LoadTransData));
			thread.Start();
		}

		private void rdoTransDataWaiting_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			Thread thread = new Thread(new ThreadStart(this.LoadTransData));
			thread.Start();
		}

		private void rdoTransDataComplete_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			Thread thread = new Thread(new ThreadStart(this.LoadTransData));
			thread.Start();
		}

		private void rdoTransDataException_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			Thread thread = new Thread(new ThreadStart(this.LoadTransData));
			thread.Start();
		}

		private void btnTransDataRefresh_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(this.LoadTransData));
			thread.Start();
		}

		private void btnTransDataDelete_Click(object sender, EventArgs e)
		{
			if (this.dgvTransData.SelectedRows.Count == 0)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Please_Select_TransData"), formMain.ML.GetStr("Display_Prompt"));
				this.dgvTransData.Focus();
				return;
			}
			if (MessageBox.Show(this, formMain.ML.GetStr("Message_Are_You_Sure_To_Delete"), formMain.ML.GetStr("Display_Prompt"), MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			Thread thread = new Thread(new ThreadStart(this.TransDataDelete));
			thread.Start();
		}

		private void rdoPendingAll_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			this.btnPendingPass.Enabled = true;
			this.btnPendingReject.Enabled = true;
			Thread thread = new Thread(new ThreadStart(this.LoadPending));
			thread.Start();
		}

		private void rdoPendingWaiting_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			this.btnPendingPass.Enabled = true;
			this.btnPendingReject.Enabled = true;
			Thread thread = new Thread(new ThreadStart(this.LoadPending));
			thread.Start();
		}

		private void rdoPendingPass_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			this.btnPendingPass.Enabled = false;
			this.btnPendingReject.Enabled = false;
			Thread thread = new Thread(new ThreadStart(this.LoadPending));
			thread.Start();
		}

		private void rdoPendingReject_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			this.btnPendingPass.Enabled = false;
			this.btnPendingReject.Enabled = false;
			Thread thread = new Thread(new ThreadStart(this.LoadPending));
			thread.Start();
		}

		private void btnPendingRefresh_Click(object sender, EventArgs e)
		{
			Thread thread = new Thread(new ThreadStart(this.LoadPending));
			thread.Start();
		}

		private void btnPendingPass_Click(object sender, EventArgs e)
		{
			if (this.dgvPending.SelectedRows.Count == 0)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Please_Select_Pending_Item"), formMain.ML.GetStr("Display_Prompt"));
				this.dgvPending.Focus();
				return;
			}
			Thread thread = new Thread(new ThreadStart(this.PendingPass));
			thread.Start();
		}

		private void btnPendingReject_Click(object sender, EventArgs e)
		{
			if (this.dgvPending.SelectedRows.Count == 0)
			{
				MessageBox.Show(this, formMain.ML.GetStr("Message_Please_Select_Pending_Item"), formMain.ML.GetStr("Display_Prompt"));
				this.dgvPending.Focus();
				return;
			}
			Thread thread = new Thread(new ThreadStart(this.PendingReject));
			thread.Start();
		}

		private void LoadTerminal()
		{
			this.needtoClose = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(500);
			this.Terminal();
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(false);
				this.tvwTerminal.Focus();
			}));
			Thread.Sleep(300);
			this.needtoClose = true;
		}

		private void Terminal()
		{
			base.Invoke(new MethodInvoker(delegate
			{
				this.tvwTerminal.Nodes.Clear();
			}));
			Thread.Sleep(200);
			IList<string[]> terminalDescriptions = new List<string[]>();
			IList<GroupInfo> list = new GroupService().GetList(LedGlobal.CloudAccount.SessionID);
			IList<TerminalInfo> list2 = new TerminalService().GetList(LedGlobal.CloudAccount.SessionID);
			if (list2 != null)
			{
				foreach (TerminalInfo current in list2)
				{
					LedCardType cardType = LedCommon.GetCardType(current.CardTypeDescription);
					if (cardType != LedCardType.Null)
					{
						LedPanelState ledPanelState = (LedPanelState)Convert.ToInt32(current.Online);
						string text = string.Format("{0}{1}_{2}_{3}x{4}_{5}", new object[]
						{
							(ledPanelState == LedPanelState.Online) ? formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Online") : formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Offline"),
							current.Name,
							cardType.ToString(),
							current.Width,
							current.Height,
							current.DeviceID
						});
						string text2 = "{0}|{1}";
						string arg = string.Empty;
						if (list != null)
						{
							foreach (GroupInfo current2 in list)
							{
								bool flag = false;
								if (current2.Terminals != null)
								{
									foreach (string[] current3 in current2.Terminals)
									{
										if (current3 != null && current3.Length > 0 && current.ID == current3[0])
										{
											arg = current2.ID;
											flag = true;
											break;
										}
									}
								}
								if (flag)
								{
									break;
								}
							}
						}
						text2 = string.Format(text2, current.ID, arg);
						terminalDescriptions.Add(new string[]
						{
							text,
							text2
						});
					}
				}
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.BindingTerminal(terminalDescriptions);
			}));
			Thread.Sleep(300);
		}

		private void BindingTerminal(IList<string[]> terminals)
		{
			if (terminals != null)
			{
				int num = 0;
				string value = string.Empty;
				LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
				if (selectedPanel != null && selectedPanel.GetType() == typeof(LedPanelCloud))
				{
					value = selectedPanel.DeviceID;
				}
				foreach (string[] current in terminals)
				{
					if (current != null && current.Length > 1)
					{
						TreeNode treeNode = new TreeNode();
						treeNode.Text = current[0];
						treeNode.Tag = current[1];
						this.tvwTerminal.Nodes.Add(treeNode);
						if (!string.IsNullOrEmpty(value))
						{
							if (current[0].EndsWith(value))
							{
								this.tvwTerminal.SelectedNode = treeNode;
							}
						}
						else if (num == 0)
						{
							this.tvwTerminal.SelectedNode = treeNode;
						}
						num++;
					}
				}
			}
		}

		private void LoadTransData()
		{
			this.needtoClose = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(500);
			this.TransData();
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(false);
				this.tvwTerminal.Focus();
			}));
			Thread.Sleep(300);
			this.needtoClose = true;
		}

		private void TransData()
		{
			base.Invoke(new MethodInvoker(delegate
			{
				this.dgvTransData.Rows.Clear();
			}));
			Thread.Sleep(200);
			TreeNode selectedNode = this.tvwTerminal.SelectedNode;
			if (selectedNode != null)
			{
				string text = selectedNode.Tag.ToString();
				string[] array = text.Split(new char[]
				{
					'|'
				});
				if (array != null && array.Length > 0)
				{
					string terminalID = array[0];
					IList<string[]> transDataDescriptions = new List<string[]>();
					IList<TransDataInfo> list = new TransDataService().GetList(LedGlobal.CloudAccount.SessionID, terminalID);
					if (list != null)
					{
						foreach (TransDataInfo current in list)
						{
							if (current != null)
							{
								string text2 = string.Empty;
								switch (current.Status)
								{
								case -3:
									text2 = formMain.GetComboBoxString("formCloudQueryReview_RadioButton_TransData_State", 4);
									break;
								case -2:
									text2 = formMain.GetComboBoxString("formCloudQueryReview_RadioButton_TransData_State", 3);
									break;
								case -1:
									text2 = formMain.GetComboBoxString("formCloudQueryReview_RadioButton_TransData_State", 2);
									break;
								case 0:
									text2 = formMain.GetComboBoxString("formCloudQueryReview_RadioButton_TransData_State", 0);
									break;
								case 1:
									text2 = formMain.GetComboBoxString("formCloudQueryReview_RadioButton_TransData_State", 1);
									break;
								}
								transDataDescriptions.Add(new string[]
								{
									current.ID,
									current.CreateTime,
									current.UpdateTime,
									current.Description,
									text2,
									current.Status.ToString()
								});
							}
						}
					}
					base.Invoke(new MethodInvoker(delegate
					{
						this.BindingTransData(transDataDescriptions);
					}));
					Thread.Sleep(200);
				}
			}
		}

		private void BindingTransData(IList<string[]> transData)
		{
			if (transData != null)
			{
				foreach (string[] current in transData)
				{
					if (current != null && current.Length == this.dgvTransData.ColumnCount)
					{
						int num = int.Parse(current[current.Length - 1]);
						if (this.rdoTransDataWaiting.Checked)
						{
							if (num != 0)
							{
								continue;
							}
						}
						else if (this.rdoTransDataComplete.Checked)
						{
							if (num != 1)
							{
								continue;
							}
						}
						else if (this.rdoTransDataException.Checked && num > -1)
						{
							continue;
						}
						DataGridViewRow dataGridViewRow = new DataGridViewRow();
						dataGridViewRow.CreateCells(this.dgvTransData);
						for (int i = 0; i < current.Length; i++)
						{
							dataGridViewRow.Cells[i].Value = current[i];
						}
						this.dgvTransData.Rows.Add(dataGridViewRow);
					}
				}
				if (this.dgvTransData.RowCount > 0)
				{
					this.dgvTransData.Rows[0].Selected = false;
				}
			}
		}

		private void TransDataDelete()
		{
			this.needtoClose = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(500);
			TreeNode selectedNode = this.tvwTerminal.SelectedNode;
			if (selectedNode != null)
			{
				string text = selectedNode.Tag.ToString();
				string[] array = text.Split(new char[]
				{
					'|'
				});
				if (array != null && array.Length > 0)
				{
					string terminalID = array[0];
					TransDataService transDataService = new TransDataService();
					foreach (DataGridViewRow dataGridViewRow in this.dgvTransData.SelectedRows)
					{
						transDataService.Delete(LedGlobal.CloudAccount.SessionID, terminalID, dataGridViewRow.Cells[0].Value.ToString());
					}
					this.TransData();
				}
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(false);
			}));
			Thread.Sleep(500);
			this.needtoClose = true;
		}

		private void LoadPending()
		{
			this.needtoClose = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(300);
			this.Pending();
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(false);
				this.tvwTerminal.Focus();
			}));
			Thread.Sleep(300);
			this.needtoClose = true;
		}

		private void Pending()
		{
			base.Invoke(new MethodInvoker(delegate
			{
				this.dgvPending.Rows.Clear();
			}));
			Thread.Sleep(200);
			string status = string.Empty;
			if (this.rdoPendingWaiting.Checked)
			{
				status = "0";
			}
			else if (this.rdoPendingPass.Checked)
			{
				status = "1";
			}
			else if (this.rdoPendingReject.Checked)
			{
				status = "-1";
			}
			IList<string[]> pendingDescriptions = new List<string[]>();
			IList<PendingTransDataInfo> list = new PendingTransDataService().GetList(LedGlobal.CloudAccount.SessionID, status);
			if (list != null)
			{
				foreach (PendingTransDataInfo current in list)
				{
					if (current != null)
					{
						string text = string.Empty;
						string text2 = string.Empty;
						string text3 = string.Empty;
						if (current.Terminal != null && current.Terminal.Count > 0)
						{
							using (Dictionary<string, string>.Enumerator enumerator2 = current.Terminal.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									KeyValuePair<string, string> current2 = enumerator2.Current;
									if (current2.Key.ToLower() == "id")
									{
										text = current2.Value;
									}
									else if (current2.Key.ToLower() == "name")
									{
										text2 = current2.Value;
									}
								}
								goto IL_1E3;
							}
							goto IL_156;
						}
						goto IL_156;
						IL_1E3:
						switch (current.Status)
						{
						case -1:
							text3 = formMain.GetComboBoxString("formCloudQueryReview_RadioButton_Pending_State", 0);
							break;
						case 0:
							text3 = formMain.GetComboBoxString("formCloudQueryReview_RadioButton_Pending_State", 1);
							break;
						case 1:
							text3 = formMain.GetComboBoxString("formCloudQueryReview_RadioButton_Pending_State", 2);
							break;
						}
						pendingDescriptions.Add(new string[]
						{
							current.ID,
							text,
							text2,
							current.CreateTime,
							current.CheckTime,
							current.Creator,
							current.Checker,
							text3,
							current.Status.ToString()
						});
						continue;
						IL_156:
						if (current.Group != null && current.Group.Count > 0)
						{
							foreach (KeyValuePair<string, string> current3 in current.Group)
							{
								if (current3.Key.ToLower() == "id")
								{
									text = current3.Value;
								}
								else if (current3.Key.ToLower() == "name")
								{
									text2 = current3.Value;
								}
							}
							goto IL_1E3;
						}
						goto IL_1E3;
					}
				}
			}
			base.Invoke(new MethodInvoker(delegate
			{
				this.BindingPending(pendingDescriptions);
			}));
			Thread.Sleep(200);
		}

		private void BindingPending(IList<string[]> pendings)
		{
			TreeNode selectedNode = this.tvwTerminal.SelectedNode;
			if (selectedNode != null)
			{
				string text = selectedNode.Tag.ToString();
				string[] array = text.Split(new char[]
				{
					'|'
				});
				if (pendings != null)
				{
					foreach (string[] current in pendings)
					{
						if (current != null && current.Length == this.dgvPending.ColumnCount && array != null && array.Length > 1 && (array[0] == current[1] || array[1] == current[1]))
						{
							DataGridViewRow dataGridViewRow = new DataGridViewRow();
							dataGridViewRow.CreateCells(this.dgvPending);
							for (int i = 0; i < current.Length; i++)
							{
								dataGridViewRow.Cells[i].Value = current[i];
							}
							this.dgvPending.Rows.Add(dataGridViewRow);
						}
					}
					if (this.dgvPending.RowCount > 0)
					{
						this.dgvPending.Rows[0].Selected = false;
					}
				}
			}
		}

		private void PendingPass()
		{
			this.needtoClose = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(500);
			PendingTransDataService pendingTransDataService = new PendingTransDataService();
			foreach (DataGridViewRow dataGridViewRow in this.dgvPending.SelectedRows)
			{
				if (dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].Value.ToString() == "0")
				{
					pendingTransDataService.Pass(LedGlobal.CloudAccount.SessionID, dataGridViewRow.Cells[0].Value.ToString());
				}
			}
			this.Pending();
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(false);
			}));
			Thread.Sleep(500);
			this.needtoClose = true;
		}

		private void PendingReject()
		{
			this.needtoClose = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(500);
			PendingTransDataService pendingTransDataService = new PendingTransDataService();
			foreach (DataGridViewRow dataGridViewRow in this.dgvPending.SelectedRows)
			{
				if (dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].Value.ToString() == "0")
				{
					pendingTransDataService.Reject(LedGlobal.CloudAccount.SessionID, dataGridViewRow.Cells[0].Value.ToString());
				}
			}
			this.Pending();
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(false);
			}));
			Thread.Sleep(500);
			this.needtoClose = true;
		}

		private void LoadAll()
		{
			this.needtoClose = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(300);
			this.Terminal();
			this.TransData();
			this.Pending();
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(false);
				this.tvwTerminal.Focus();
			}));
			Thread.Sleep(100);
			this.needtoClose = true;
		}

		private void LoadTransDataPending()
		{
			this.needtoClose = false;
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(true);
			}));
			Thread.Sleep(300);
			this.TransData();
			this.Pending();
			base.Invoke(new MethodInvoker(delegate
			{
				this.LoadingVisible(false);
				this.tvwTerminal.Focus();
			}));
			Thread.Sleep(100);
			this.needtoClose = true;
		}

		public void LoadingVisible(bool pBool)
		{
			if (base.Controls != null && base.Controls.Count > 0)
			{
				foreach (Control control in base.Controls)
				{
					if (control != null && control.Name != "picLoading")
					{
						control.Enabled = !pBool;
					}
				}
			}
			if (pBool)
			{
				this.picLoading.Visible = true;
				this.picLoading.BringToFront();
				return;
			}
			this.picLoading.Visible = false;
			this.picLoading.SendToBack();
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			this.tsMenu = new ToolStrip();
			this.tsbtnUser = new ToolStripButton();
			this.tsbtnGroup = new ToolStripButton();
			this.tsbtnTerminalParameter = new ToolStripButton();
			this.tvwTerminal = new TreeView();
			this.tabFunction = new TabControl();
			this.tpTransData = new TabPage();
			this.dgvTransData = new DataGridView();
			this.btnTransDataRefresh = new Button();
			this.btnTransDataDelete = new Button();
			this.rdoTransDataException = new RadioButton();
			this.rdoTransDataComplete = new RadioButton();
			this.rdoTransDataWaiting = new RadioButton();
			this.rdoTransDataAll = new RadioButton();
			this.tpPending = new TabPage();
			this.dgvPending = new DataGridView();
			this.btnPendingRefresh = new Button();
			this.btnPendingPass = new Button();
			this.btnPendingReject = new Button();
			this.rdoPendingAll = new RadioButton();
			this.rdoPendingPass = new RadioButton();
			this.rdoPendingReject = new RadioButton();
			this.rdoPendingWaiting = new RadioButton();
			this.picLoading = new PictureBox();
			this.lblTerminal = new Label();
			this.btnTerminalRefresh = new Button();
			this.ID = new DataGridViewTextBoxColumn();
			this.dgvtxtcCreateTime = new DataGridViewTextBoxColumn();
			this.dgvtxtcUpdateTime = new DataGridViewTextBoxColumn();
			this.Description = new DataGridViewTextBoxColumn();
			this.DataStatus = new DataGridViewTextBoxColumn();
			this.dgvctxtcStatusValue = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumnIndex = new DataGridViewTextBoxColumn();
			this.dgvtxtcTerminalGroupID = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumnUpdate = new DataGridViewTextBoxColumn();
			this.dgvtxtcCreator = new DataGridViewTextBoxColumn();
			this.dgvtxtcChecker = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.dgvtxtcCheckStatus = new DataGridViewTextBoxColumn();
			this.tsMenu.SuspendLayout();
			this.tabFunction.SuspendLayout();
			this.tpTransData.SuspendLayout();
			((ISupportInitialize)this.dgvTransData).BeginInit();
			this.tpPending.SuspendLayout();
			((ISupportInitialize)this.dgvPending).BeginInit();
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.tsMenu.AutoSize = false;
			this.tsMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnUser,
				this.tsbtnGroup,
				this.tsbtnTerminalParameter
			});
			this.tsMenu.Location = new System.Drawing.Point(0, 0);
			this.tsMenu.Name = "tsMenu";
			this.tsMenu.Size = new System.Drawing.Size(884, 51);
			this.tsMenu.TabIndex = 25;
			this.tsMenu.Text = "toolStrip1";
			this.tsbtnUser.Image = Resources.Cloud_User;
			this.tsbtnUser.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.tsbtnUser.ImageScaling = ToolStripItemImageScaling.None;
			this.tsbtnUser.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUser.Name = "tsbtnUser";
			this.tsbtnUser.Size = new System.Drawing.Size(60, 48);
			this.tsbtnUser.Text = "用户信息";
			this.tsbtnUser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.tsbtnUser.TextImageRelation = TextImageRelation.ImageAboveText;
			this.tsbtnUser.Click += new EventHandler(this.tsbtnUser_Click);
			this.tsbtnGroup.Image = Resources.Cloud_Users;
			this.tsbtnGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.tsbtnGroup.ImageScaling = ToolStripItemImageScaling.None;
			this.tsbtnGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnGroup.Name = "tsbtnGroup";
			this.tsbtnGroup.Size = new System.Drawing.Size(60, 48);
			this.tsbtnGroup.Text = "群组信息";
			this.tsbtnGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.tsbtnGroup.TextImageRelation = TextImageRelation.ImageAboveText;
			this.tsbtnGroup.Click += new EventHandler(this.tsbtnGroup_Click);
			this.tsbtnTerminalParameter.Image = Resources.Cloud_infomation;
			this.tsbtnTerminalParameter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.tsbtnTerminalParameter.ImageScaling = ToolStripItemImageScaling.None;
			this.tsbtnTerminalParameter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnTerminalParameter.Name = "tsbtnTerminalParameter";
			this.tsbtnTerminalParameter.Size = new System.Drawing.Size(60, 48);
			this.tsbtnTerminalParameter.Text = "设备参数";
			this.tsbtnTerminalParameter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.tsbtnTerminalParameter.TextImageRelation = TextImageRelation.ImageAboveText;
			this.tsbtnTerminalParameter.Click += new EventHandler(this.tsbtnTerminalParameter_Click);
			this.tvwTerminal.HideSelection = false;
			this.tvwTerminal.Location = new System.Drawing.Point(2, 75);
			this.tvwTerminal.Name = "tvwTerminal";
			this.tvwTerminal.Size = new System.Drawing.Size(226, 374);
			this.tvwTerminal.TabIndex = 26;
			this.tvwTerminal.KeyDown += new KeyEventHandler(this.tvwTerminal_KeyDown);
			this.tvwTerminal.MouseDown += new MouseEventHandler(this.tvwTerminal_MouseDown);
			this.tabFunction.Controls.Add(this.tpPending);
			this.tabFunction.Controls.Add(this.tpTransData);
			this.tabFunction.Location = new System.Drawing.Point(232, 53);
			this.tabFunction.Name = "tabFunction";
			this.tabFunction.SelectedIndex = 0;
			this.tabFunction.Size = new System.Drawing.Size(650, 400);
			this.tabFunction.TabIndex = 27;
			this.tpTransData.Controls.Add(this.dgvTransData);
			this.tpTransData.Controls.Add(this.btnTransDataRefresh);
			this.tpTransData.Controls.Add(this.btnTransDataDelete);
			this.tpTransData.Controls.Add(this.rdoTransDataException);
			this.tpTransData.Controls.Add(this.rdoTransDataComplete);
			this.tpTransData.Controls.Add(this.rdoTransDataWaiting);
			this.tpTransData.Controls.Add(this.rdoTransDataAll);
			this.tpTransData.Location = new System.Drawing.Point(4, 22);
			this.tpTransData.Name = "tpTransData";
			this.tpTransData.Padding = new Padding(3);
			this.tpTransData.Size = new System.Drawing.Size(642, 374);
			this.tpTransData.TabIndex = 0;
			this.tpTransData.Text = "转发列表";
			this.tpTransData.UseVisualStyleBackColor = true;
			this.dgvTransData.AllowUserToAddRows = false;
			this.dgvTransData.AllowUserToResizeRows = false;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvTransData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvTransData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvTransData.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ID,
				this.dgvtxtcCreateTime,
				this.dgvtxtcUpdateTime,
				this.Description,
				this.DataStatus,
				this.dgvctxtcStatusValue
			});
			this.dgvTransData.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.dgvTransData.Location = new System.Drawing.Point(6, 56);
			this.dgvTransData.Name = "dgvTransData";
			this.dgvTransData.ReadOnly = true;
			this.dgvTransData.RowHeadersVisible = false;
			this.dgvTransData.RowTemplate.Height = 23;
			this.dgvTransData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvTransData.Size = new System.Drawing.Size(630, 310);
			this.dgvTransData.TabIndex = 58;
			this.btnTransDataRefresh.Image = Resources.refresh_L;
			this.btnTransDataRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnTransDataRefresh.Location = new System.Drawing.Point(490, 19);
			this.btnTransDataRefresh.Name = "btnTransDataRefresh";
			this.btnTransDataRefresh.Size = new System.Drawing.Size(70, 24);
			this.btnTransDataRefresh.TabIndex = 57;
			this.btnTransDataRefresh.Text = "刷新";
			this.btnTransDataRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnTransDataRefresh.UseVisualStyleBackColor = true;
			this.btnTransDataRefresh.Click += new EventHandler(this.btnTransDataRefresh_Click);
			this.btnTransDataDelete.Image = Resources.Delete;
			this.btnTransDataDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnTransDataDelete.Location = new System.Drawing.Point(566, 19);
			this.btnTransDataDelete.Name = "btnTransDataDelete";
			this.btnTransDataDelete.Size = new System.Drawing.Size(70, 24);
			this.btnTransDataDelete.TabIndex = 56;
			this.btnTransDataDelete.Text = "删除";
			this.btnTransDataDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnTransDataDelete.UseVisualStyleBackColor = true;
			this.btnTransDataDelete.Click += new EventHandler(this.btnTransDataDelete_Click);
			this.rdoTransDataException.AutoSize = true;
			this.rdoTransDataException.Location = new System.Drawing.Point(285, 23);
			this.rdoTransDataException.Name = "rdoTransDataException";
			this.rdoTransDataException.Size = new System.Drawing.Size(71, 16);
			this.rdoTransDataException.TabIndex = 2;
			this.rdoTransDataException.TabStop = true;
			this.rdoTransDataException.Text = "下载异常";
			this.rdoTransDataException.UseVisualStyleBackColor = true;
			this.rdoTransDataException.CheckedChanged += new EventHandler(this.rdoTransDataException_CheckedChanged);
			this.rdoTransDataComplete.AutoSize = true;
			this.rdoTransDataComplete.Location = new System.Drawing.Point(184, 23);
			this.rdoTransDataComplete.Name = "rdoTransDataComplete";
			this.rdoTransDataComplete.Size = new System.Drawing.Size(71, 16);
			this.rdoTransDataComplete.TabIndex = 2;
			this.rdoTransDataComplete.TabStop = true;
			this.rdoTransDataComplete.Text = "完成接收";
			this.rdoTransDataComplete.UseVisualStyleBackColor = true;
			this.rdoTransDataComplete.CheckedChanged += new EventHandler(this.rdoTransDataComplete_CheckedChanged);
			this.rdoTransDataWaiting.AutoSize = true;
			this.rdoTransDataWaiting.Location = new System.Drawing.Point(83, 23);
			this.rdoTransDataWaiting.Name = "rdoTransDataWaiting";
			this.rdoTransDataWaiting.Size = new System.Drawing.Size(71, 16);
			this.rdoTransDataWaiting.TabIndex = 2;
			this.rdoTransDataWaiting.TabStop = true;
			this.rdoTransDataWaiting.Text = "等待接收";
			this.rdoTransDataWaiting.UseVisualStyleBackColor = true;
			this.rdoTransDataWaiting.CheckedChanged += new EventHandler(this.rdoTransDataWaiting_CheckedChanged);
			this.rdoTransDataAll.AutoSize = true;
			this.rdoTransDataAll.Location = new System.Drawing.Point(6, 23);
			this.rdoTransDataAll.Name = "rdoTransDataAll";
			this.rdoTransDataAll.Size = new System.Drawing.Size(47, 16);
			this.rdoTransDataAll.TabIndex = 1;
			this.rdoTransDataAll.TabStop = true;
			this.rdoTransDataAll.Text = "全部";
			this.rdoTransDataAll.UseVisualStyleBackColor = true;
			this.rdoTransDataAll.CheckedChanged += new EventHandler(this.rdoTransDataAll_CheckedChanged);
			this.tpPending.Controls.Add(this.dgvPending);
			this.tpPending.Controls.Add(this.btnPendingRefresh);
			this.tpPending.Controls.Add(this.btnPendingPass);
			this.tpPending.Controls.Add(this.btnPendingReject);
			this.tpPending.Controls.Add(this.rdoPendingAll);
			this.tpPending.Controls.Add(this.rdoPendingPass);
			this.tpPending.Controls.Add(this.rdoPendingReject);
			this.tpPending.Controls.Add(this.rdoPendingWaiting);
			this.tpPending.Location = new System.Drawing.Point(4, 22);
			this.tpPending.Name = "tpPending";
			this.tpPending.Padding = new Padding(3);
			this.tpPending.Size = new System.Drawing.Size(642, 374);
			this.tpPending.TabIndex = 1;
			this.tpPending.Text = "审核列表";
			this.tpPending.UseVisualStyleBackColor = true;
			this.dgvPending.AllowUserToAddRows = false;
			this.dgvPending.AllowUserToResizeRows = false;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dgvPending.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvPending.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvPending.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumnIndex,
				this.dgvtxtcTerminalGroupID,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumnUpdate,
				this.dgvtxtcCreator,
				this.dgvtxtcChecker,
				this.dataGridViewTextBoxColumn5,
				this.dgvtxtcCheckStatus
			});
			this.dgvPending.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.dgvPending.Location = new System.Drawing.Point(6, 56);
			this.dgvPending.Name = "dgvPending";
			this.dgvPending.ReadOnly = true;
			this.dgvPending.RowHeadersVisible = false;
			this.dgvPending.RowTemplate.Height = 23;
			this.dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvPending.Size = new System.Drawing.Size(630, 310);
			this.dgvPending.TabIndex = 59;
			this.btnPendingRefresh.Image = Resources.refresh_L;
			this.btnPendingRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPendingRefresh.Location = new System.Drawing.Point(414, 19);
			this.btnPendingRefresh.Name = "btnPendingRefresh";
			this.btnPendingRefresh.Size = new System.Drawing.Size(70, 24);
			this.btnPendingRefresh.TabIndex = 58;
			this.btnPendingRefresh.Text = "刷新";
			this.btnPendingRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnPendingRefresh.UseVisualStyleBackColor = true;
			this.btnPendingRefresh.Click += new EventHandler(this.btnPendingRefresh_Click);
			this.btnPendingPass.Image = Resources.pass_button;
			this.btnPendingPass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPendingPass.Location = new System.Drawing.Point(490, 19);
			this.btnPendingPass.Name = "btnPendingPass";
			this.btnPendingPass.Size = new System.Drawing.Size(70, 24);
			this.btnPendingPass.TabIndex = 56;
			this.btnPendingPass.Text = "通过";
			this.btnPendingPass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnPendingPass.UseVisualStyleBackColor = true;
			this.btnPendingPass.Click += new EventHandler(this.btnPendingPass_Click);
			this.btnPendingReject.Image = Resources.refuse_button;
			this.btnPendingReject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPendingReject.Location = new System.Drawing.Point(566, 19);
			this.btnPendingReject.Name = "btnPendingReject";
			this.btnPendingReject.Size = new System.Drawing.Size(70, 24);
			this.btnPendingReject.TabIndex = 57;
			this.btnPendingReject.Text = "驳回";
			this.btnPendingReject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnPendingReject.UseVisualStyleBackColor = true;
			this.btnPendingReject.Click += new EventHandler(this.btnPendingReject_Click);
			this.rdoPendingAll.AutoSize = true;
			this.rdoPendingAll.Location = new System.Drawing.Point(6, 23);
			this.rdoPendingAll.Name = "rdoPendingAll";
			this.rdoPendingAll.Size = new System.Drawing.Size(47, 16);
			this.rdoPendingAll.TabIndex = 52;
			this.rdoPendingAll.Text = "全部";
			this.rdoPendingAll.UseVisualStyleBackColor = true;
			this.rdoPendingAll.CheckedChanged += new EventHandler(this.rdoPendingAll_CheckedChanged);
			this.rdoPendingPass.AutoSize = true;
			this.rdoPendingPass.ImeMode = ImeMode.NoControl;
			this.rdoPendingPass.Location = new System.Drawing.Point(180, 23);
			this.rdoPendingPass.Name = "rdoPendingPass";
			this.rdoPendingPass.Size = new System.Drawing.Size(71, 16);
			this.rdoPendingPass.TabIndex = 55;
			this.rdoPendingPass.Text = "审核通过";
			this.rdoPendingPass.UseVisualStyleBackColor = true;
			this.rdoPendingPass.CheckedChanged += new EventHandler(this.rdoPendingPass_CheckedChanged);
			this.rdoPendingReject.AutoSize = true;
			this.rdoPendingReject.ImeMode = ImeMode.NoControl;
			this.rdoPendingReject.Location = new System.Drawing.Point(285, 23);
			this.rdoPendingReject.Name = "rdoPendingReject";
			this.rdoPendingReject.Size = new System.Drawing.Size(83, 16);
			this.rdoPendingReject.TabIndex = 54;
			this.rdoPendingReject.Text = "审核未通过";
			this.rdoPendingReject.UseVisualStyleBackColor = true;
			this.rdoPendingReject.CheckedChanged += new EventHandler(this.rdoPendingReject_CheckedChanged);
			this.rdoPendingWaiting.AutoSize = true;
			this.rdoPendingWaiting.ImeMode = ImeMode.NoControl;
			this.rdoPendingWaiting.Location = new System.Drawing.Point(87, 23);
			this.rdoPendingWaiting.Name = "rdoPendingWaiting";
			this.rdoPendingWaiting.Size = new System.Drawing.Size(59, 16);
			this.rdoPendingWaiting.TabIndex = 53;
			this.rdoPendingWaiting.Text = "待审核";
			this.rdoPendingWaiting.UseVisualStyleBackColor = true;
			this.rdoPendingWaiting.CheckedChanged += new EventHandler(this.rdoPendingWaiting_CheckedChanged);
			this.picLoading.Image = Resources.loading;
			this.picLoading.Location = new System.Drawing.Point(411, 197);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(63, 58);
			this.picLoading.SizeMode = PictureBoxSizeMode.StretchImage;
			this.picLoading.TabIndex = 37;
			this.picLoading.TabStop = false;
			this.lblTerminal.AutoSize = true;
			this.lblTerminal.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.lblTerminal.Location = new System.Drawing.Point(7, 58);
			this.lblTerminal.Name = "lblTerminal";
			this.lblTerminal.Size = new System.Drawing.Size(53, 12);
			this.lblTerminal.TabIndex = 38;
			this.lblTerminal.Text = "设备列表";
			this.btnTerminalRefresh.Image = Resources.refresh_L;
			this.btnTerminalRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnTerminalRefresh.Location = new System.Drawing.Point(76, 53);
			this.btnTerminalRefresh.Name = "btnTerminalRefresh";
			this.btnTerminalRefresh.Size = new System.Drawing.Size(25, 20);
			this.btnTerminalRefresh.TabIndex = 39;
			this.btnTerminalRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnTerminalRefresh.UseVisualStyleBackColor = true;
			this.btnTerminalRefresh.Click += new EventHandler(this.btnTerminalRefresh_Click);
			this.ID.HeaderText = "ID";
			this.ID.Name = "ID";
			this.ID.ReadOnly = true;
			this.ID.Visible = false;
			this.ID.Width = 280;
			this.dgvtxtcCreateTime.HeaderText = "创建时间";
			this.dgvtxtcCreateTime.Name = "dgvtxtcCreateTime";
			this.dgvtxtcCreateTime.ReadOnly = true;
			this.dgvtxtcCreateTime.Width = 130;
			this.dgvtxtcUpdateTime.HeaderText = "发送时间";
			this.dgvtxtcUpdateTime.Name = "dgvtxtcUpdateTime";
			this.dgvtxtcUpdateTime.ReadOnly = true;
			this.dgvtxtcUpdateTime.Width = 130;
			this.Description.HeaderText = "描述";
			this.Description.Name = "Description";
			this.Description.ReadOnly = true;
			this.Description.Width = 205;
			this.DataStatus.HeaderText = "状态";
			this.DataStatus.Name = "DataStatus";
			this.DataStatus.ReadOnly = true;
			this.DataStatus.Width = 162;
			this.dgvctxtcStatusValue.HeaderText = "状态值";
			this.dgvctxtcStatusValue.Name = "dgvctxtcStatusValue";
			this.dgvctxtcStatusValue.ReadOnly = true;
			this.dgvctxtcStatusValue.Visible = false;
			this.dataGridViewTextBoxColumnIndex.HeaderText = "ID";
			this.dataGridViewTextBoxColumnIndex.Name = "dataGridViewTextBoxColumnIndex";
			this.dataGridViewTextBoxColumnIndex.ReadOnly = true;
			this.dataGridViewTextBoxColumnIndex.Visible = false;
			this.dataGridViewTextBoxColumnIndex.Width = 70;
			this.dgvtxtcTerminalGroupID.HeaderText = "设备/分组ID";
			this.dgvtxtcTerminalGroupID.Name = "dgvtxtcTerminalGroupID";
			this.dgvtxtcTerminalGroupID.ReadOnly = true;
			this.dgvtxtcTerminalGroupID.Visible = false;
			this.dataGridViewTextBoxColumn3.HeaderText = "设备/分组";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.HeaderText = "申请创建时间";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 130;
			this.dataGridViewTextBoxColumnUpdate.HeaderText = "申请答复时间";
			this.dataGridViewTextBoxColumnUpdate.Name = "dataGridViewTextBoxColumnUpdate";
			this.dataGridViewTextBoxColumnUpdate.ReadOnly = true;
			this.dataGridViewTextBoxColumnUpdate.Width = 130;
			this.dgvtxtcCreator.HeaderText = "申请人";
			this.dgvtxtcCreator.Name = "dgvtxtcCreator";
			this.dgvtxtcCreator.ReadOnly = true;
			this.dgvtxtcCreator.Width = 85;
			this.dgvtxtcChecker.HeaderText = "答复人";
			this.dgvtxtcChecker.Name = "dgvtxtcChecker";
			this.dgvtxtcChecker.ReadOnly = true;
			this.dgvtxtcChecker.Width = 85;
			this.dataGridViewTextBoxColumn5.HeaderText = "审核状态";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Width = 97;
			this.dgvtxtcCheckStatus.HeaderText = "审核状态值";
			this.dgvtxtcCheckStatus.Name = "dgvtxtcCheckStatus";
			this.dgvtxtcCheckStatus.ReadOnly = true;
			this.dgvtxtcCheckStatus.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(884, 453);
			base.Controls.Add(this.btnTerminalRefresh);
			base.Controls.Add(this.lblTerminal);
			base.Controls.Add(this.picLoading);
			base.Controls.Add(this.tabFunction);
			base.Controls.Add(this.tvwTerminal);
			base.Controls.Add(this.tsMenu);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formCloudQueryReview";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "云服务查询/审核";
			base.FormClosing += new FormClosingEventHandler(this.formCloudQueryReview_FormClosing);
			base.Load += new EventHandler(this.formCloudQueryReview_Load);
			this.tsMenu.ResumeLayout(false);
			this.tsMenu.PerformLayout();
			this.tabFunction.ResumeLayout(false);
			this.tpTransData.ResumeLayout(false);
			this.tpTransData.PerformLayout();
			((ISupportInitialize)this.dgvTransData).EndInit();
			this.tpPending.ResumeLayout(false);
			this.tpPending.PerformLayout();
			((ISupportInitialize)this.dgvPending).EndInit();
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
