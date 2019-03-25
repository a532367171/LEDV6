using LedControlSystem.Properties;
using LedModel;
using LedModel.Cloud;
using LedModel.Enum;
using LedModel.Foundation;
using LedModel.Share;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LedControlSystem.LedControlSystem
{
	public class formPanelSelectForItem : Form
	{
		private bool needSetRelateCheck = true;

		private LedItem originalItem;

		private LedPanel originalPanel;

		private ExecuteMode mode;

		private string itemName;

		private IContainer components;

		private TreeViewEnhanced tvwPanel;

		private Button btnCancel;

		private Button btnOK;

		private CheckBox chkSelectAll;

		private Label lblHint;

		private ImageList imgGroupPanel;

		private bool CloudLogin
		{
			get
			{
				return LedGlobal.CloudAccount != null && !string.IsNullOrEmpty(LedGlobal.CloudAccount.UserName);
			}
		}

		public formPanelSelectForItem(LedItem item)
		{
			this.InitializeComponent();
			this.originalItem = item;
			this.mode = ExecuteMode.CopyItem;
			this.DisplayLanuageText();
		}

		public formPanelSelectForItem(LedPanel panel)
		{
			this.InitializeComponent();
			this.originalPanel = panel;
			this.mode = ExecuteMode.CopyPanelItems;
			this.DisplayLanuageText();
		}

		public formPanelSelectForItem(LedItem item, string name)
		{
			this.InitializeComponent();
			this.originalItem = item;
			this.itemName = name;
			this.mode = ExecuteMode.ShareItem;
			this.DisplayLanuageText();
		}

		private void DisplayLanuageText()
		{
			if (this.mode == ExecuteMode.CopyItem)
			{
				this.Text = formMain.ML.GetStr("formPanelSelectForItem_FormText_CopyItem");
			}
			else if (this.mode == ExecuteMode.CopyPanelItems)
			{
				this.Text = formMain.ML.GetStr("formPanelSelectForItem_FormText_CopyPanelItems");
			}
			else if (this.mode == ExecuteMode.ShareItem)
			{
				this.Text = formMain.ML.GetStr("formPanelSelectForItem_FormText_ShareItem");
			}
			this.chkSelectAll.Text = formMain.ML.GetStr("formPanelSelectForItem_CheckBox_SelectAll");
			this.lblHint.Text = formMain.ML.GetStr("formPanelSelectForItem_Label_Hint");
			this.btnOK.Text = formMain.ML.GetStr("formPanelSelectForItem_Button_OK");
			this.btnCancel.Text = formMain.ML.GetStr("formPanelSelectForItem_Button_Cancel");
		}

		private void formCopyItemSelect_Load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
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
			LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
			foreach (LedPanel current in formMain.ledsys.Panels)
			{
				if (!(current.GetType() != typeof(LedPanelCloud)) && selectedPanel != null && selectedPanel.Width == current.Width && selectedPanel.Height == current.Height && selectedPanel.ColorMode == current.ColorMode && (this.mode <= ExecuteMode.CopyItem || !(selectedPanel.ID == current.ID)))
				{
					LedPanelCloud ledPanelCloud = (LedPanelCloud)current;
					if (string.IsNullOrEmpty(text) || ledPanelCloud.Group.Contains(text))
					{
						TreeNode treeNode = new TreeNode(ledPanelCloud.TextName);
						if (ledPanelCloud.State == LedPanelState.Online)
						{
							treeNode.ImageIndex = 1;
							treeNode.SelectedImageIndex = 1;
							if ((current.PortType == LedPortType.Ethernet && current.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer) || (current.PortType == LedPortType.GPRS && current.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer))
							{
								treeNode.Text = formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Online") + ledPanelCloud.TextName;
							}
						}
						else
						{
							treeNode.ImageIndex = 2;
							treeNode.SelectedImageIndex = 2;
							if ((current.PortType == LedPortType.Ethernet && current.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer) || (current.PortType == LedPortType.GPRS && current.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer))
							{
								treeNode.Text = formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Offline") + ledPanelCloud.TextName;
							}
						}
						treeNode.Checked = true;
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
			this.chkSelectAll.Checked = flag;
		}

		private void LoadTreeView()
		{
			this.tvwPanel.Nodes.Clear();
			LedPanel selectedPanel = formMain.ledsys.SelectedPanel;
			LedProject ledsys = formMain.ledsys;
			IList<LedGroup> groups = formMain.ledsys.Groups;
			IList<LedPanel> panels = formMain.ledsys.Panels;
			bool @checked = true;
			int i = 0;
			while (i < groups.Count)
			{
				LedGroup ledGroup = groups[i];
				if (this.CloudLogin || ledsys.Cloud.LoginState != LedCloudLoginState.Login || string.IsNullOrEmpty(ledsys.Cloud.Account.UserName) || !(ledsys.Cloud.Account.UserName == ledGroup.Name) || ledGroup.CreationMethod != LedCreationMethod.Cloud)
				{
					goto IL_115;
				}
				bool flag = false;
				foreach (LedPanel current in ledsys.Panels)
				{
					if (current.GetType() == typeof(LedPanel) && current.Group == ledGroup.ID)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					goto IL_115;
				}
				IL_5DC:
				i++;
				continue;
				IL_115:
				string text = string.Empty;
				bool flag2 = ledGroup.GetType() == typeof(LedGroupCloud);
				TreeNode treeNode = new TreeNode();
				if (flag2)
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
						foreach (LedGroup current2 in ledGroupCloud.Subgroups)
						{
							ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(current2.Name);
							if (selectedIndex == num)
							{
								toolStripMenuItem3.Checked = true;
								arg = toolStripMenuItem3.Text;
								text = current2.ID;
							}
							toolStripMenuItem3.Tag = current2.ID;
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
				bool flag3 = false;
				foreach (LedPanel current3 in panels)
				{
					if ((this.CloudLogin || !(current3.GetType() == typeof(LedPanelCloud))) && selectedPanel != null && selectedPanel.Width == current3.Width && selectedPanel.Height == current3.Height && selectedPanel.ColorMode == current3.ColorMode && (this.mode <= ExecuteMode.CopyItem || !(selectedPanel.ID == current3.ID)))
					{
						bool flag4 = false;
						if (flag2)
						{
							if (current3.GetType() != typeof(LedPanelCloud))
							{
								continue;
							}
							LedPanelCloud ledPanelCloud = (LedPanelCloud)current3;
							flag3 = true;
							if (string.IsNullOrEmpty(text) || ledPanelCloud.Group.Contains(text))
							{
								flag4 = true;
							}
						}
						else
						{
							if (current3.GetType() != typeof(LedPanel))
							{
								continue;
							}
							if (text.Equals(current3.Group))
							{
								flag4 = true;
							}
						}
						if (flag4)
						{
							TreeNode treeNode2 = new TreeNode(current3.TextName);
							if (current3.State == LedPanelState.Online)
							{
								treeNode2.ImageIndex = 1;
								treeNode2.SelectedImageIndex = 1;
								if ((current3.PortType == LedPortType.Ethernet && (current3.EthernetCommunicaitonMode == LedEthernetCommunicationMode.LocalServer || current3.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer)) || (current3.PortType == LedPortType.GPRS && current3.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer))
								{
									treeNode2.Text = formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Online") + current3.TextName;
								}
							}
							else
							{
								treeNode2.ImageIndex = 2;
								treeNode2.SelectedImageIndex = 2;
								if ((current3.PortType == LedPortType.Ethernet && (current3.EthernetCommunicaitonMode == LedEthernetCommunicationMode.LocalServer || current3.EthernetCommunicaitonMode == LedEthernetCommunicationMode.CloudServer)) || (current3.PortType == LedPortType.GPRS && current3.GPRSCommunicaitonMode == LedGPRSCommunicationMode.CloudServer))
								{
									treeNode2.Text = formMain.ML.GetStr("formMain_TreeView_Node_Prefix_Offline") + current3.TextName;
								}
							}
							treeNode2.Checked = true;
							treeNode2.Tag = current3;
							treeNode.Nodes.Add(treeNode2);
						}
					}
				}
				if (treeNode.Nodes.Count == 0)
				{
					treeNode.Checked = false;
					@checked = false;
				}
				if ((treeNode.Nodes != null && treeNode.Nodes.Count > 0) || flag3)
				{
					this.tvwPanel.Nodes.Add(treeNode);
					goto IL_5DC;
				}
				goto IL_5DC;
			}
			if (this.tvwPanel.Nodes.Count == 0)
			{
				@checked = false;
			}
			this.chkSelectAll.Checked = @checked;
			this.tvwPanel.ExpandAll();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				int num = 0;
				foreach (TreeNode treeNode in this.tvwPanel.Nodes)
				{
					if (treeNode.Nodes != null)
					{
						foreach (TreeNode treeNode2 in treeNode.Nodes)
						{
							if (treeNode2 != null && treeNode2.Checked && (treeNode2.Tag.GetType() == typeof(LedPanel) || treeNode2.Tag.GetType() == typeof(LedPanelCloud)))
							{
								num++;
							}
						}
					}
				}
				if (num == 0)
				{
					MessageBox.Show(this, formMain.ML.GetStr("formPanelSelectForItem_Message_PleaseSelectPanel"));
				}
				else
				{
					if (this.mode == ExecuteMode.ShareItem)
					{
						this.originalItem.SharedItemOfPanelIDs.Clear();
					}
					foreach (TreeNode treeNode3 in this.tvwPanel.Nodes)
					{
						if (treeNode3.Nodes != null)
						{
							foreach (TreeNode treeNode4 in treeNode3.Nodes)
							{
								if (treeNode4 != null && treeNode4.Checked && (treeNode4.Tag.GetType() == typeof(LedPanel) || treeNode4.Tag.GetType() == typeof(LedPanelCloud)))
								{
									LedPanel ledPanel = (LedPanel)treeNode4.Tag;
									if (this.mode == ExecuteMode.CopyItem)
									{
										LedItem ledItem = new LedItem();
										if (ledPanel.Items == null)
										{
											ledPanel.Items = new List<LedItem>();
										}
										int num2 = ledPanel.Items.Count;
										if (num2 > 0)
										{
											foreach (LedItem current in ledPanel.Items)
											{
												if (num2 < current.No)
												{
													num2 = current.No;
												}
											}
										}
										num2++;
										ledItem.TextName = formMain.ML.GetStr("Display_Item") + num2;
										ledItem.ValueName = ledItem.TextName;
										ledItem.No = num2;
										ledItem.ParentPanel = ledPanel;
										if (this.originalItem != null)
										{
											ledItem.Copy(this.originalItem);
										}
										ledPanel.AddItem(ledItem);
									}
									else if (this.mode == ExecuteMode.CopyPanelItems)
									{
										ledPanel.CopyItems(this.originalPanel, formMain.ML.GetStr("Display_Item"));
									}
									else if (this.mode == ExecuteMode.ShareItem)
									{
										LedSharedItem ledSharedItem = new LedSharedItem();
										if (ledPanel.Items == null)
										{
											ledPanel.Items = new List<LedItem>();
										}
										ledSharedItem.Share(this.originalItem);
										ledSharedItem.TextName = this.itemName;
										ledPanel.AddSharedItem(ledSharedItem);
										this.originalItem.SharedItemOfPanelIDs.Add(ledPanel.ID);
									}
								}
							}
						}
					}
					if (this.mode == ExecuteMode.ShareItem)
					{
						this.originalItem.TextName = this.itemName;
						this.originalItem.Shared = true;
						formMain.ledsys.SharedItemNoCounter = 1;
					}
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, formMain.ML.GetStr("formPanelSelectForItem_Message_ExceptionError") + ex.Message);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
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
			if (this.chkSelectAll.Checked != @checked)
			{
				this.chkSelectAll.Checked = @checked;
			}
			this.needSetRelateCheck = true;
		}

		private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formPanelSelectForItem));
			this.imgGroupPanel = new ImageList(this.components);
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.chkSelectAll = new CheckBox();
			this.lblHint = new Label();
			this.tvwPanel = new TreeViewEnhanced();
			base.SuspendLayout();
			this.imgGroupPanel.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imgGroupPanel.ImageStream");
			this.imgGroupPanel.TransparentColor = System.Drawing.Color.Transparent;
			this.imgGroupPanel.Images.SetKeyName(0, "group.ico");
			this.imgGroupPanel.Images.SetKeyName(1, "panel.ico");
			this.imgGroupPanel.Images.SetKeyName(2, "paneloff.ico");
			this.imgGroupPanel.Images.SetKeyName(3, "applications-stack.png");
			this.imgGroupPanel.Images.SetKeyName(4, "applications-blue.png");
			this.btnCancel.Location = new System.Drawing.Point(191, 390);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(57, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnOK.Location = new System.Drawing.Point(118, 390);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(57, 23);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "确认";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.Location = new System.Drawing.Point(7, 394);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(72, 16);
			this.chkSelectAll.TabIndex = 5;
			this.chkSelectAll.Text = "选择全部";
			this.chkSelectAll.UseVisualStyleBackColor = true;
			this.chkSelectAll.CheckedChanged += new EventHandler(this.chkSelectAll_CheckedChanged);
			this.lblHint.ForeColor = System.Drawing.Color.Blue;
			this.lblHint.Location = new System.Drawing.Point(5, 363);
			this.lblHint.Name = "lblHint";
			this.lblHint.Size = new System.Drawing.Size(253, 25);
			this.lblHint.TabIndex = 9;
			this.lblHint.Text = "仅支持相同屏参";
			this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tvwPanel.CheckBoxes = true;
			this.tvwPanel.ImageIndex = 0;
			this.tvwPanel.ImageList = this.imgGroupPanel;
			this.tvwPanel.Location = new System.Drawing.Point(0, 0);
			this.tvwPanel.Name = "tvwPanel";
			this.tvwPanel.SelectedImageIndex = 0;
			this.tvwPanel.Size = new System.Drawing.Size(258, 361);
			this.tvwPanel.TabIndex = 8;
			this.tvwPanel.AfterCheck += new TreeViewEventHandler(this.tvwPanel_AfterCheck);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(258, 420);
			base.Controls.Add(this.lblHint);
			base.Controls.Add(this.tvwPanel);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.chkSelectAll);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formPanelSelectForItem";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "复制节目";
			base.Load += new EventHandler(this.formCopyItemSelect_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
