using LedControlSystem.Cloud;
using LedControlSystem.Properties;
using LedModel;
using LedModel.Cloud;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formPanelSelectForGroupSending : Form
	{
		public bool isCloud;

		private bool isSingleCommand;

		private string operation;

		private static string formID = "formPanelSelectForGroupSending";

		private bool needSetRelateCheck = true;

		private IContainer components;

		private CheckBox checkBox_selectedall;

		private Button button_Sure;

		private Button button_cancel;

		private ImageList imageList_choose;

		private TreeViewEnhanced tvwPanel;

		private RadioButton rdoLocal;

		private RadioButton rdoCloud;

		private Panel pnlMode;

		public static string FormID
		{
			get
			{
				return formPanelSelectForGroupSending.formID;
			}
			set
			{
				formPanelSelectForGroupSending.formID = value;
			}
		}

		private bool IsCloudLogin
		{
			get
			{
				return LedGlobal.CloudAccount != null && !string.IsNullOrEmpty(LedGlobal.CloudAccount.UserName);
			}
		}

		public void Diplay_lanuage_Text()
		{
			this.Text = this.operation;
			this.rdoLocal.Text = formMain.ML.GetStr("formPanelSelectForGroupSending_RadioButton_Local");
			this.rdoCloud.Text = formMain.ML.GetStr("formPanelSelectForGroupSending_RadioButton_Cloud");
			this.checkBox_selectedall.Text = formMain.ML.GetStr("formPanelSelectForGroupSending_checkBox_selectedall");
			this.button_Sure.Text = formMain.ML.GetStr("formPanelSelectForGroupSending_button_Sure");
			this.button_cancel.Text = formMain.ML.GetStr("formPanelSelectForGroupSending_button_cancel");
		}

		public formPanelSelectForGroupSending(bool pSingleCommand, string pOperation)
		{
			this.InitializeComponent();
			this.isSingleCommand = pSingleCommand;
			this.operation = pOperation;
			this.Diplay_lanuage_Text();
		}

		private void formPanelSelectForGroupSending_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.isCloud = false;
			this.pnlMode.Visible = false;
			this.rdoLocal.Checked = true;
			this.rdoCloud.Checked = false;
			if (this.IsCloudLogin)
			{
				this.pnlMode.Visible = true;
				LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
				if (selectedPanel != null && selectedPanel.GetType() == typeof(LedPanelCloud))
				{
					this.rdoLocal.Checked = false;
					this.rdoCloud.Checked = true;
				}
			}
			this.LoadTreeView();
		}

		private void tsmiGroupCloud_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.tvwPanel.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
			if (toolStripMenuItem.Checked)
			{
				return;
			}
			if (selectedNode.Tag.GetType() != typeof(LedGroupCloud))
			{
				return;
			}
			LedGroupCloud ledGroupCloud = (LedGroupCloud)selectedNode.Tag;
			string text = string.Empty;
			if (toolStripMenuItem.Tag != null)
			{
				string text2 = toolStripMenuItem.Tag.ToString();
				if (text2 != "0")
				{
					text = text2;
				}
			}
			bool flag = true;
			selectedNode.Nodes.Clear();
			foreach (LedPanel current in formMain.ledsys.Panels)
			{
				if (!(current.GetType() != typeof(LedPanelCloud)))
				{
					LedPanelCloud ledPanelCloud = (LedPanelCloud)current;
					if (string.IsNullOrEmpty(text) || ledPanelCloud.Group.Contains(text))
					{
						TreeNode treeNode = new TreeNode(ledPanelCloud.TextName);
						if (ledPanelCloud.State == LedPanelState.Online)
						{
							treeNode.ImageIndex = 1;
							treeNode.SelectedImageIndex = 1;
							treeNode.Checked = true;
							if ((current.PortType == LedPortType.Ethernet && current.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer) || (current.PortType == LedPortType.GPRS && current.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer))
							{
								treeNode.Text = formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Online") + ledPanelCloud.TextName;
							}
						}
						else
						{
							treeNode.ImageIndex = 2;
							treeNode.SelectedImageIndex = 2;
							treeNode.Checked = false;
							flag = false;
							if ((current.PortType == LedPortType.Ethernet && current.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer) || (current.PortType == LedPortType.GPRS && current.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer))
							{
								treeNode.Text = formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Offline") + ledPanelCloud.TextName;
							}
						}
						treeNode.Tag = ledPanelCloud;
						selectedNode.Nodes.Add(treeNode);
					}
				}
			}
			ContextMenuStrip contextMenuStrip = selectedNode.ContextMenuStrip;
			if (contextMenuStrip != null)
			{
				foreach (ToolStripItem toolStripItem in contextMenuStrip.Items)
				{
					if (toolStripItem.GetType() == typeof(ToolStripMenuItem))
					{
						((ToolStripMenuItem)toolStripItem).Checked = false;
					}
				}
			}
			toolStripMenuItem.Checked = true;
			if (selectedNode.Nodes.Count == 0)
			{
				flag = false;
			}
			selectedNode.Checked = flag;
			selectedNode.Text = string.Format("{0}({1})", ledGroupCloud.Name, toolStripMenuItem.Text);
			selectedNode.Expand();
			if (flag && this.tvwPanel.Nodes != null && this.tvwPanel.Nodes.Count > 0)
			{
				foreach (TreeNode treeNode2 in this.tvwPanel.Nodes)
				{
					if (treeNode2 != selectedNode && treeNode2.Nodes != null)
					{
						foreach (TreeNode treeNode3 in treeNode2.Nodes)
						{
							if (!treeNode3.Checked)
							{
								flag = false;
								break;
							}
						}
						if (!flag)
						{
							break;
						}
					}
				}
			}
			this.checkBox_selectedall.Checked = flag;
		}

		private void LoadTreeView()
		{
			this.tvwPanel.Nodes.Clear();
			IList<LedGroup> groups = formMain.ledsys.Groups;
			IList<LedPanel> panels = formMain.ledsys.Panels;
			bool @checked = this.rdoLocal.Checked;
			bool checked2 = true;
			int i = 0;
			while (i < groups.Count)
			{
				LedGroup ledGroup = groups[i];
				string text = string.Empty;
				bool flag = ledGroup.GetType() == typeof(LedGroupCloud);
				if (@checked)
				{
					if (!flag)
					{
						goto IL_82;
					}
				}
				else if (flag)
				{
					goto IL_82;
				}
				IL_558:
				i++;
				continue;
				IL_82:
				TreeNode treeNode = new TreeNode();
				if (flag)
				{
					string arg = string.Empty;
					LedGroupCloud ledGroupCloud = (LedGroupCloud)ledGroup;
					ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
					int selectedIndex = ledGroupCloud.SelectedIndex;
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(formMain.ML.GetStr("formMain_TreeView_Node_ContextMenuStrip_Item_Cloud_Group_All"));
					if (selectedIndex == -2)
					{
						toolStripMenuItem.Checked = true;
						arg = toolStripMenuItem.Text;
					}
					toolStripMenuItem.Tag = 0;
					toolStripMenuItem.Click += new EventHandler(this.tsmiGroupCloud_Click);
					contextMenuStrip.Items.Add(toolStripMenuItem);
					ToolStripSeparator value = new ToolStripSeparator();
					contextMenuStrip.Items.Add(value);
					ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(formMain.ML.GetStr("formMain_TreeView_Node_ContextMenuStrip_Item_Cloud_Group_Ungroup"));
					if (selectedIndex == -1)
					{
						toolStripMenuItem2.Checked = true;
						arg = toolStripMenuItem2.Text;
						text = ledGroupCloud.ID;
					}
					toolStripMenuItem2.Tag = ledGroupCloud.ID;
					toolStripMenuItem2.Click += new EventHandler(this.tsmiGroupCloud_Click);
					contextMenuStrip.Items.Add(toolStripMenuItem2);
					ToolStripSeparator value2 = new ToolStripSeparator();
					contextMenuStrip.Items.Add(value2);
					if (ledGroupCloud.Subgroups != null)
					{
						int num = 0;
						foreach (LedGroup current in ledGroupCloud.Subgroups)
						{
							ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(current.Name);
							if (selectedIndex == num)
							{
								toolStripMenuItem3.Checked = true;
								arg = toolStripMenuItem3.Text;
								text = current.ID;
							}
							toolStripMenuItem3.Tag = current.ID;
							toolStripMenuItem3.Click += new EventHandler(this.tsmiGroupCloud_Click);
							contextMenuStrip.Items.Add(toolStripMenuItem3);
							num++;
						}
					}
					treeNode.Text = string.Format("{0}({1})", ledGroup.Name, arg);
					treeNode.ContextMenuStrip = contextMenuStrip;
				}
				else
				{
					treeNode.Text = ledGroup.Name;
					text = ledGroup.ID;
				}
				treeNode.ImageIndex = 0;
				treeNode.SelectedImageIndex = 0;
				treeNode.Tag = ledGroup;
				treeNode.Checked = true;
				foreach (LedPanel current2 in panels)
				{
					if (current2.PortType != LedPortType.USB && (current2.PortType != LedPortType.GPRS || (current2.GPRSCommunicaitonMode != LedGPRSCommunicationMode.GprsServer && !@checked && !(current2.GetType() == typeof(LedPanel)) && this.IsCloudLogin)))
					{
						if (current2.PortType == LedPortType.Ethernet)
						{
							if (current2.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer)
							{
								if (current2.GetType() == typeof(LedPanel) || @checked)
								{
									continue;
								}
								if (!this.IsCloudLogin)
								{
									continue;
								}
							}
							else if (!@checked)
							{
								continue;
							}
						}
						bool flag2 = false;
						if (flag)
						{
							if (current2.GetType() != typeof(LedPanelCloud))
							{
								continue;
							}
							LedPanelCloud ledPanelCloud = (LedPanelCloud)current2;
							if (string.IsNullOrEmpty(text) || ledPanelCloud.Group.Contains(text))
							{
								flag2 = true;
							}
						}
						else
						{
							if (current2.GetType() != typeof(LedPanel))
							{
								continue;
							}
							if (text.Equals(current2.Group))
							{
								flag2 = true;
							}
						}
						if (flag2)
						{
							TreeNode treeNode2 = new TreeNode(current2.TextName);
							if (current2.State == LedPanelState.Online)
							{
								treeNode2.ImageIndex = 1;
								treeNode2.SelectedImageIndex = 1;
								treeNode2.Checked = true;
								if ((current2.PortType == LedPortType.Ethernet && current2.EthernetCommunicaitonMode == LedEthernetCommunicationMode.LocalServer) || (current2.GetType() == typeof(LedPanelCloud) && (current2.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer || (current2.PortType == LedPortType.GPRS && current2.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer))))
								{
									treeNode2.Text = formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Online") + current2.TextName;
								}
							}
							else
							{
								treeNode2.ImageIndex = 2;
								treeNode2.SelectedImageIndex = 2;
								treeNode2.Checked = false;
								checked2 = false;
								if ((current2.PortType == LedPortType.Ethernet && current2.EthernetCommunicaitonMode == LedEthernetCommunicationMode.LocalServer) || (current2.GetType() == typeof(LedPanelCloud) && (current2.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer || (current2.PortType == LedPortType.GPRS && current2.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer))))
								{
									treeNode2.Text = formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Offline") + current2.TextName;
								}
							}
							treeNode2.Tag = current2;
							treeNode.Nodes.Add(treeNode2);
						}
					}
				}
				if (treeNode.Nodes != null && treeNode.Nodes.Count > 0)
				{
					this.tvwPanel.Nodes.Add(treeNode);
					goto IL_558;
				}
				goto IL_558;
			}
			if (this.tvwPanel.Nodes.Count == 0)
			{
				checked2 = false;
			}
			this.checkBox_selectedall.Checked = checked2;
			this.tvwPanel.ExpandAll();
		}

		private void FooChild(TreeNode node)
		{
			bool @checked = node.Checked;
			if (node.Nodes.Count > 0)
			{
				foreach (TreeNode treeNode in node.Nodes)
				{
					if (treeNode != null)
					{
						treeNode.Checked = @checked;
						this.FooChild(treeNode);
					}
				}
			}
		}

		private void FooParent(TreeNode node)
		{
			if (node.Parent != null)
			{
				int num = 0;
				foreach (TreeNode treeNode in node.Parent.Nodes)
				{
					if (treeNode.Checked)
					{
						num = 1;
						break;
					}
				}
				if (num == 0)
				{
					TreeNode parent = node.Parent;
					parent.Checked = false;
					this.FooParent(parent);
					return;
				}
				TreeNode parent2 = node.Parent;
				if (!parent2.Checked)
				{
					parent2.Checked = true;
				}
				this.FooParent(parent2);
			}
		}

		private void checkBox_selectedall_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (!checkBox.Focused)
			{
				return;
			}
			bool @checked = checkBox.Checked;
			foreach (TreeNode treeNode in this.tvwPanel.Nodes)
			{
				if (treeNode.Checked != @checked)
				{
					treeNode.Checked = @checked;
				}
				if (treeNode.Nodes != null)
				{
					foreach (TreeNode treeNode2 in treeNode.Nodes)
					{
						if (treeNode2.Checked != @checked)
						{
							treeNode2.Checked = @checked;
						}
					}
				}
			}
		}

		private void button_Sure_Click(object sender, EventArgs e)
		{
			try
			{
				int num = 0;
				int num2 = 0;
				bool flag = false;
				bool flag2 = false;
				if (this.isCloud)
				{
					if (this.isSingleCommand)
					{
						if (formCloudGroupSendingSingle.screenSendGroup == null)
						{
							formCloudGroupSendingSingle.screenSendGroup = new List<Screen_Display_Class>();
						}
						formCloudGroupSendingSingle.screenSendGroup.Clear();
					}
					else
					{
						if (formCloudGroupSending.screenSendGroup == null)
						{
							formCloudGroupSending.screenSendGroup = new List<Screen_Display_Class>();
						}
						formCloudGroupSending.screenSendGroup.Clear();
					}
				}
				else if (this.isSingleCommand)
				{
					if (formGroupSendingSingle.screenSendGroup == null)
					{
						formGroupSendingSingle.screenSendGroup = new List<Screen_Display_Class>();
					}
					formGroupSendingSingle.screenSendGroup.Clear();
				}
				else
				{
					if (formGroupSending.screen_send_group == null)
					{
						formGroupSending.screen_send_group = new List<Screen_Display_Class>();
					}
					formGroupSending.screen_send_group.Clear();
				}
				foreach (TreeNode treeNode in this.tvwPanel.Nodes)
				{
					if (treeNode.Nodes != null)
					{
						foreach (TreeNode treeNode2 in treeNode.Nodes)
						{
							if (treeNode2 != null && treeNode2.Checked && (treeNode2.Tag.GetType() == typeof(LedPanel) || treeNode2.Tag.GetType() == typeof(LedPanelCloud)))
							{
								LedPanel ledPanel = (LedPanel)treeNode2.Tag;
								Screen_Display_Class screen_Display_Class = new Screen_Display_Class();
								num = (screen_Display_Class.Screen_Num = num + 1);
								screen_Display_Class.Panel_NO = ledPanel;
								screen_Display_Class.Send_Progress = 0;
								screen_Display_Class.State_Message = formMain.ML.GetStr("formPanelSelectForGroupSending_message_ReadyToStart");
								if (ledPanel.PortType == LedPortType.Ethernet && ledPanel.EthernetCommunicaitonMode == LedEthernetCommunicationMode.Directly)
								{
									flag = true;
								}
								if (this.isCloud)
								{
									if (this.isSingleCommand)
									{
										formCloudGroupSendingSingle.screenSendGroup.Add(screen_Display_Class);
										if (ledPanel.State == LedPanelState.Offline)
										{
											flag2 = true;
										}
									}
									else
									{
										screen_Display_Class.Send_State = 1;
										formCloudGroupSending.screenSendGroup.Add(screen_Display_Class);
									}
								}
								else if (this.isSingleCommand)
								{
									formGroupSendingSingle.screenSendGroup.Add(screen_Display_Class);
								}
								else
								{
									formGroupSending.screen_send_group.Add(screen_Display_Class);
								}
								num2++;
							}
						}
					}
				}
				if (num2 == 0)
				{
					MessageBox.Show(this, formMain.ML.GetStr("Message_Please_Select_One_Panel_At_Least"));
				}
				else if (!flag || MessageBox.Show(this, formMain.ML.GetStr("NETCARD_message_Prompt_Send_SingleCard"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
				{
					if (!flag2 || MessageBox.Show(this, formMain.ML.GetStr("Message_Selected_Terminal_Is_Offline_And_Continue_Sending_May_Not_Send_To_Terminal"), formMain.ML.GetStr("NETCARD_message_prompt"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
					{
						base.DialogResult = DialogResult.OK;
						base.Close();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void tvwPanel_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = true;
		}

		private void tvwPanel_AfterCheck(object sender, TreeViewEventArgs e)
		{
			TreeView treeView = (TreeView)sender;
			if (!treeView.Focused)
			{
				return;
			}
			if (!this.needSetRelateCheck)
			{
				return;
			}
			this.needSetRelateCheck = false;
			TreeNode node = e.Node;
			if (node == null)
			{
				return;
			}
			if (node.Level == 0)
			{
				this.FooChild(node);
			}
			else
			{
				this.FooParent(node);
			}
			treeView.SelectedNode = node;
			bool @checked = node.Checked;
			if (node.Level == 0)
			{
				IEnumerator enumerator = treeView.Nodes.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						TreeNode treeNode = (TreeNode)enumerator.Current;
						if (treeNode.Checked != @checked)
						{
							if (@checked)
							{
								@checked = treeNode.Checked;
								break;
							}
							break;
						}
					}
					goto IL_162;
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			foreach (TreeNode treeNode2 in treeView.Nodes)
			{
				bool flag = false;
				if (treeNode2 != null && treeNode2.Nodes.Count > 0)
				{
					foreach (TreeNode treeNode3 in treeNode2.Nodes)
					{
						if (treeNode3.Checked != @checked)
						{
							if (@checked)
							{
								@checked = treeNode3.Checked;
							}
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
			IL_162:
			if (this.checkBox_selectedall.Checked != @checked)
			{
				this.checkBox_selectedall.Checked = @checked;
			}
			this.needSetRelateCheck = true;
		}

		private void rdoLocal_CheckedChanged(object sender, EventArgs e)
		{
			this.isCloud = false;
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			this.LoadTreeView();
		}

		private void rdoCloud_CheckedChanged(object sender, EventArgs e)
		{
			this.isCloud = true;
			RadioButton radioButton = (RadioButton)sender;
			if (!radioButton.Focused)
			{
				return;
			}
			this.LoadTreeView();
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formPanelSelectForGroupSending));
			this.imageList_choose = new ImageList(this.components);
			this.checkBox_selectedall = new CheckBox();
			this.button_Sure = new Button();
			this.button_cancel = new Button();
			this.tvwPanel = new TreeViewEnhanced();
			this.rdoLocal = new RadioButton();
			this.rdoCloud = new RadioButton();
			this.pnlMode = new Panel();
			this.pnlMode.SuspendLayout();
			base.SuspendLayout();
			this.imageList_choose.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList_choose.ImageStream");
			this.imageList_choose.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList_choose.Images.SetKeyName(0, "group.ico");
			this.imageList_choose.Images.SetKeyName(1, "panel.ico");
			this.imageList_choose.Images.SetKeyName(2, "paneloff.ico");
			this.imageList_choose.Images.SetKeyName(3, "applications-stack.png");
			this.imageList_choose.Images.SetKeyName(4, "applications-blue.png");
			this.checkBox_selectedall.AutoSize = true;
			this.checkBox_selectedall.Location = new System.Drawing.Point(7, 394);
			this.checkBox_selectedall.Name = "checkBox_selectedall";
			this.checkBox_selectedall.Size = new System.Drawing.Size(72, 16);
			this.checkBox_selectedall.TabIndex = 1;
			this.checkBox_selectedall.Text = "选择全部";
			this.checkBox_selectedall.UseVisualStyleBackColor = true;
			this.checkBox_selectedall.CheckedChanged += new EventHandler(this.checkBox_selectedall_CheckedChanged);
			this.button_Sure.Location = new System.Drawing.Point(118, 390);
			this.button_Sure.Name = "button_Sure";
			this.button_Sure.Size = new System.Drawing.Size(57, 23);
			this.button_Sure.TabIndex = 2;
			this.button_Sure.Text = "确认";
			this.button_Sure.UseVisualStyleBackColor = true;
			this.button_Sure.Click += new EventHandler(this.button_Sure_Click);
			this.button_cancel.Location = new System.Drawing.Point(191, 390);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(57, 23);
			this.button_cancel.TabIndex = 3;
			this.button_cancel.Text = "取消";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new EventHandler(this.button_cancel_Click);
			this.tvwPanel.CheckBoxes = true;
			this.tvwPanel.ImageIndex = 0;
			this.tvwPanel.ImageList = this.imageList_choose;
			this.tvwPanel.Location = new System.Drawing.Point(0, 0);
			this.tvwPanel.Name = "tvwPanel";
			this.tvwPanel.SelectedImageIndex = 0;
			this.tvwPanel.Size = new System.Drawing.Size(258, 361);
			this.tvwPanel.TabIndex = 4;
			this.tvwPanel.AfterCheck += new TreeViewEventHandler(this.tvwPanel_AfterCheck);
			this.rdoLocal.AutoSize = true;
			this.rdoLocal.Location = new System.Drawing.Point(7, 5);
			this.rdoLocal.Name = "rdoLocal";
			this.rdoLocal.Size = new System.Drawing.Size(47, 16);
			this.rdoLocal.TabIndex = 5;
			this.rdoLocal.TabStop = true;
			this.rdoLocal.Text = "本地";
			this.rdoLocal.UseVisualStyleBackColor = true;
			this.rdoLocal.CheckedChanged += new EventHandler(this.rdoLocal_CheckedChanged);
			this.rdoCloud.AutoSize = true;
			this.rdoCloud.Location = new System.Drawing.Point(83, 5);
			this.rdoCloud.Name = "rdoCloud";
			this.rdoCloud.Size = new System.Drawing.Size(35, 16);
			this.rdoCloud.TabIndex = 6;
			this.rdoCloud.TabStop = true;
			this.rdoCloud.Text = "云";
			this.rdoCloud.UseVisualStyleBackColor = true;
			this.rdoCloud.CheckedChanged += new EventHandler(this.rdoCloud_CheckedChanged);
			this.pnlMode.Controls.Add(this.rdoCloud);
			this.pnlMode.Controls.Add(this.rdoLocal);
			this.pnlMode.Location = new System.Drawing.Point(0, 363);
			this.pnlMode.Name = "pnlMode";
			this.pnlMode.Size = new System.Drawing.Size(258, 25);
			this.pnlMode.TabIndex = 7;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(258, 420);
			base.Controls.Add(this.pnlMode);
			base.Controls.Add(this.tvwPanel);
			base.Controls.Add(this.button_cancel);
			base.Controls.Add(this.button_Sure);
			base.Controls.Add(this.checkBox_selectedall);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formPanelSelectForGroupSending";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "群组发送节目";
			base.Load += new EventHandler(this.formPanelSelectForGroupSending_Load);
			this.pnlMode.ResumeLayout(false);
			this.pnlMode.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
