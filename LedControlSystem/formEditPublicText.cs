using LedControlSystem.Properties;
using LedModel;
using LedModel.Element.PictureText;
using LedModel.Foundation;
using LedModel.Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZHUI;

namespace LedControlSystem.LedControlSystem
{
	public class formEditPublicText : Form
	{
		public formMain FM_formEdit_PublicText;

		private Dictionary<string, string> publictext_noEdit = new Dictionary<string, string>();

		private IList<Panel_Item_PublicText> Panel_Item_PublicText_Members_IList = new List<Panel_Item_PublicText>();

		private Dictionary<string, IList<Panel_Item_PublicText>> Panel_Item_PublicText_Members_Dic = new Dictionary<string, IList<Panel_Item_PublicText>>();

		private IContainer components;

		private DataGridView Public_Text_Group;

		private ToolStrip Public_Text_Setting_toolStrip;

		private ToolStripButton Add_PublicText_toolStripButton;

		private ToolStripButton Del_PublicText_toolStripButton;

		private DataGridView Panel_PublicText_dataGridView;

		private Label label_Desc_PublicText;

		private RichTextBox Public_Text_richTextBox;

		private Label label_Edit_PublicText;

		private DataGridViewTextBoxColumn publictext_N0;

		private DataGridViewTextBoxColumn publictext_Name;

		private DataGridViewButtonColumn Rename_PublicText;

		private DataGridViewTextBoxColumn panel_name_publictext;

		private DataGridViewTextBoxColumn Item_PublicText;

		private DataGridViewTextBoxColumn panel_info_desc;

		private DataGridViewTextBoxColumn publictext_number;

		private Button button_publictext_confirm;

		private Button button_publictext_cancel;

		private Label label_PublicText_List;

		private ZhGroupBox zhGroupBox1;

		public formEditPublicText()
		{
			this.InitializeComponent();
		}

		public formEditPublicText(formMain fm)
		{
			this.InitializeComponent();
			this.FM_formEdit_PublicText = fm;
			this.Text = formMain.ML.GetStr("formEdit_Public_Text_FormText");
			this.Add_PublicText_toolStripButton.Text = formMain.ML.GetStr("formEdit_Public_Text_ToolStripSetting_Add");
			this.Del_PublicText_toolStripButton.Text = formMain.ML.GetStr("formEdit_Public_Text_ToolStripSetting_Delete");
			this.publictext_N0.HeaderText = formMain.ML.GetStr("formEdit_Public_Text_DataGridView_Group_No");
			this.publictext_Name.HeaderText = formMain.ML.GetStr("formEdit_Public_Text_DataGridView_Group_Name");
			this.Rename_PublicText.HeaderText = formMain.ML.GetStr("formEdit_Public_Text_DataGridView_Group_Edit");
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.NullValue = formMain.ML.GetStr("formReName_FormText");
			this.Rename_PublicText.DefaultCellStyle = dataGridViewCellStyle;
			this.panel_name_publictext.HeaderText = formMain.ML.GetStr("formEdit_Public_Text_DataGridView_Panel_PanelName");
			this.Item_PublicText.HeaderText = formMain.ML.GetStr("formEdit_Public_Text_DataGridView_Panel_ItemName");
			this.panel_info_desc.HeaderText = formMain.ML.GetStr("formEdit_Public_Text_DataGridView_Panel_CardType");
			this.publictext_number.HeaderText = formMain.ML.GetStr("formEdit_Public_Text_DataGridView_Panel_Count");
			this.label_PublicText_List.Text = formMain.ML.GetStr("Mar_PublicText");
			this.label_Desc_PublicText.Text = formMain.ML.GetStr("formEdit_Public_Text_label_Desc_PublicText");
			this.label_Edit_PublicText.Text = formMain.ML.GetStr("formEdit_Public_Text_label_Edit_PublicText");
			this.button_publictext_confirm.Text = formMain.ML.GetStr("Global_Messagebox_OK");
			this.button_publictext_cancel.Text = formMain.ML.GetStr("Global_Messagebox_Cancel");
		}

		private void Public_Text_Group_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			string name = this.Public_Text_Group.Columns[e.ColumnIndex].Name;
			if (e.RowIndex < 0)
			{
				return;
			}
			string text = this.Public_Text_Group.Rows[e.RowIndex].Cells[1].Value.ToString();
			if (name == "Rename_PublicText")
			{
				formReName formReName = new formReName();
				formReName.name_text_str = text;
				formReName.Set_Text_Name();
				formReName.ShowDialog();
				if (formReName.rename_result)
				{
					LedPublicText ledPublicText = new LedPublicText();
					ledPublicText = formMain.Ledsys.GetPublicText(text);
					formMain.Ledsys.RenamePublicText(text, formReName.name_text_str);
					this.ReName_load_List(text, formReName.name_text_str);
					this.Public_Text_Group.Rows[e.RowIndex].Cells[1].Value = formReName.name_text_str;
					this.publictext_noEdit.Remove(text);
					this.publictext_noEdit.Add(formReName.name_text_str, ledPublicText.Text);
				}
			}
		}

		public void ReName_load_List(string str_key_source, string str_key_purpose)
		{
			IList<Panel_Item_PublicText> list = new List<Panel_Item_PublicText>();
			try
			{
				foreach (Panel_Item_PublicText current in this.Panel_Item_PublicText_Members_Dic[str_key_source])
				{
					list.Add(new Panel_Item_PublicText
					{
						Card_Type = current.Card_Type,
						No = current.No,
						Num = current.Num,
						Panel_Name = current.Panel_Name,
						Item_Name = current.Item_Name,
						PublicText_KEY = str_key_purpose
					});
				}
				this.Panel_Item_PublicText_Members_Dic.Remove(str_key_source);
				this.Panel_Item_PublicText_Members_Dic.Add(str_key_purpose, list);
			}
			catch
			{
			}
		}

		private void Public_Text_Group_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, this.Public_Text_Group.RowHeadersWidth, e.RowBounds.Height);
			TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), this.Public_Text_Group.RowHeadersDefaultCellStyle.Font, bounds, this.Public_Text_Group.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
		}

		private void formEdit_Public_Text_load(object sender, EventArgs e)
		{
			if (Program.IsforeignTradeMode)
			{
				base.Icon = Resources.AppIconV5;
			}
			else
			{
				base.Icon = Resources.AppIcon;
			}
			this.publictext_noEdit.Clear();
			foreach (LedPublicText current in formMain.Ledsys.PublicTexts)
			{
				this.publictext_noEdit.Add(current.Name, current.Text);
			}
			if (formMain.Ledsys.PublicTexts.Count == 0)
			{
				this.Public_Text_richTextBox.Enabled = false;
			}
			else
			{
				this.Public_Text_richTextBox.Enabled = true;
			}
			this.load_PublicText();
		}

		private void formEdit_Public_Text_close(object sender, FormClosingEventArgs e)
		{
			base.Dispose();
		}

		private void Add_PublicText_toolStripButton_Click(object sender, EventArgs e)
		{
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			dataGridViewRow.CreateCells(this.Public_Text_Group);
			for (int i = 1; i < 9999; i++)
			{
				bool flag = false;
				string text = formMain.ML.GetStr("Mar_PublicText") + i.ToString();
				foreach (LedPublicText current in formMain.Ledsys.PublicTexts)
				{
					if (current.Name == text)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					dataGridViewRow.Cells[1].Value = text;
					LedPublicText ledPublicText = new LedPublicText();
					ledPublicText.Name = text;
					ledPublicText.Text = string.Empty;
					formMain.Ledsys.PublicTexts.Add(ledPublicText);
					this.Panel_Item_PublicText_Members_Dic.Add(text, new List<Panel_Item_PublicText>());
					this.publictext_noEdit.Add(text, string.Empty);
					break;
				}
			}
			this.Public_Text_Group.Rows.Add(dataGridViewRow);
		}

		private void Del_PublicText_toolStripButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, formMain.ML.GetStr("formEdit_Public_Text_MessageBox_DeletePublicText"), formMain.ML.GetStr("UpdateButton_Delete"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK && this.Public_Text_Group.Rows.Count > 0)
			{
				try
				{
					foreach (DataGridViewRow dataGridViewRow in this.Public_Text_Group.SelectedRows)
					{
						LedPublicText publicText = formMain.Ledsys.GetPublicText(dataGridViewRow.Cells[1].Value.ToString());
						formMain.Ledsys.PublicTexts.Remove(publicText);
						formMain.Ledsys.RemovePublicText(publicText.Name);
						this.Panel_Item_PublicText_Members_Dic.Remove(this.Public_Text_Group.SelectedRows[0].Cells[1].Value.ToString());
						this.publictext_noEdit.Remove(this.Public_Text_Group.SelectedRows[0].Cells[1].Value.ToString());
						this.Public_Text_Group.Rows.Remove(dataGridViewRow);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void Public_Text_select_Changed(object sender, EventArgs e)
		{
			if (this.Public_Text_Group.RowCount != 0)
			{
				this.Public_Text_richTextBox.Enabled = true;
				this.Public_Text_richTextBox.Text = this.publictext_noEdit[this.Public_Text_Group.SelectedRows[0].Cells[1].Value.ToString()];
				this.Panel_PublicText_dataGridView.Rows.Clear();
				using (IEnumerator<Panel_Item_PublicText> enumerator = this.Panel_Item_PublicText_Members_Dic[this.Public_Text_Group.SelectedRows[0].Cells[1].Value.ToString()].GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Panel_Item_PublicText current = enumerator.Current;
						DataGridViewRow dataGridViewRow = new DataGridViewRow();
						dataGridViewRow.CreateCells(this.Panel_PublicText_dataGridView);
						dataGridViewRow.Cells[0].Value = current.Panel_Name;
						dataGridViewRow.Cells[1].Value = current.Item_Name;
						dataGridViewRow.Cells[2].Value = current.Card_Type;
						dataGridViewRow.Cells[3].Value = current.Num;
						this.Panel_PublicText_dataGridView.Rows.Add(dataGridViewRow);
					}
					return;
				}
			}
			this.Public_Text_richTextBox.Enabled = false;
			this.Public_Text_richTextBox.Text = string.Empty;
			this.Panel_PublicText_dataGridView.Rows.Clear();
		}

		private bool ElementCondition(Panel_Item_PublicText panel_item_publictexts, int count, out int j)
		{
			for (int i = 0; i < count; i++)
			{
				if (this.Panel_Item_PublicText_Members_IList[i].Panel_Name == panel_item_publictexts.Panel_Name && this.Panel_Item_PublicText_Members_IList[i].Item_Name == panel_item_publictexts.Item_Name && this.Panel_Item_PublicText_Members_IList[i].No == panel_item_publictexts.No && this.Panel_Item_PublicText_Members_IList[i].Card_Type == panel_item_publictexts.Card_Type && this.Panel_Item_PublicText_Members_IList[i].PublicText_KEY == panel_item_publictexts.PublicText_KEY)
				{
					j = i;
					return true;
				}
			}
			j = 0;
			return false;
		}

		private void RichTextBox_PublicText_TextChanged(object sender, EventArgs e)
		{
			RichTextBox richTextBox = (RichTextBox)sender;
			if (!richTextBox.Focused)
			{
				return;
			}
			if (richTextBox.Text != formMain.Ledsys.GetPublicText(this.Public_Text_Group.SelectedRows[0].Cells[1].Value.ToString()).Text)
			{
				if (richTextBox.TextLength > 0)
				{
					if (this.publictext_noEdit.ContainsKey(this.Public_Text_Group.SelectedRows[0].Cells[1].Value.ToString()))
					{
						this.publictext_noEdit[this.Public_Text_Group.SelectedRows[0].Cells[1].Value.ToString()] = richTextBox.Text;
						return;
					}
				}
				else
				{
					this.publictext_noEdit[this.Public_Text_Group.SelectedRows[0].Cells[1].Value.ToString()] = string.Empty;
				}
			}
		}

		private void load_PublicText()
		{
			int num = 0;
			this.Panel_Item_PublicText_Members_IList.Clear();
			this.Panel_Item_PublicText_Members_Dic.Clear();
			foreach (LedPublicText current in formMain.Ledsys.PublicTexts)
			{
				foreach (LedPanel current2 in formMain.Ledsys.Panels)
				{
					num++;
					foreach (LedItem current3 in current2.Items)
					{
						Panel_Item_PublicText panel_Item_PublicText = new Panel_Item_PublicText();
						panel_Item_PublicText.Panel_Name = current2.TextName;
						panel_Item_PublicText.Card_Type = current2.CardType.ToString();
						panel_Item_PublicText.No = num;
						panel_Item_PublicText.Item_Name = current3.TextName;
						foreach (LedSubarea current4 in current3.Subareas)
						{
							foreach (LedContent current5 in current4.Contents)
							{
								foreach (LedElement current6 in current5.Elements)
								{
									if (current6.GetType() == typeof(LedEPText) && (current6 as LedEPText).PublicText.Name == current.Name)
									{
										panel_Item_PublicText.PublicText_KEY = current.Name;
										int count = this.Panel_Item_PublicText_Members_IList.Count;
										if (this.Panel_Item_PublicText_Members_IList.Count == 0)
										{
											panel_Item_PublicText.Num = 1;
											this.Panel_Item_PublicText_Members_IList.Add(panel_Item_PublicText);
										}
										else
										{
											int index = 0;
											if (this.ElementCondition(panel_Item_PublicText, count, out index))
											{
												this.Panel_Item_PublicText_Members_IList[index].Num++;
											}
											else
											{
												panel_Item_PublicText.Num = 1;
												this.Panel_Item_PublicText_Members_IList.Add(panel_Item_PublicText);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			foreach (LedPublicText current7 in formMain.Ledsys.PublicTexts)
			{
				IList<Panel_Item_PublicText> list = new List<Panel_Item_PublicText>();
				this.Panel_Item_PublicText_Members_Dic.Add(current7.Name, list);
				foreach (Panel_Item_PublicText current8 in this.Panel_Item_PublicText_Members_IList)
				{
					if (current7.Name == current8.PublicText_KEY)
					{
						list.Add(current8);
					}
				}
			}
			foreach (LedPublicText current9 in formMain.Ledsys.PublicTexts)
			{
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.Public_Text_Group);
				dataGridViewRow.Cells[1].Value = current9.Name;
				this.Public_Text_Group.Rows.Add(dataGridViewRow);
			}
		}

		private void button_publictext_confirm_Click(object sender, EventArgs e)
		{
			foreach (string current in this.publictext_noEdit.Keys)
			{
				if (formMain.Ledsys.GetPublicText(current) != null && formMain.Ledsys.GetPublicText(current).Text != this.publictext_noEdit[current])
				{
					formMain.Ledsys.GetPublicText(current).Text = this.publictext_noEdit[current];
				}
			}
			base.Dispose();
		}

		private void button_publictext_cancel_Click(object sender, EventArgs e)
		{
			base.Dispose();
		}

		private void richTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.V)
			{
				try
				{
					string text = Clipboard.GetText();
					this.Public_Text_richTextBox.SelectedText = text;
					e.Handled = true;
				}
				catch
				{
				}
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			this.Public_Text_Group = new DataGridView();
			this.publictext_N0 = new DataGridViewTextBoxColumn();
			this.publictext_Name = new DataGridViewTextBoxColumn();
			this.Rename_PublicText = new DataGridViewButtonColumn();
			this.Public_Text_Setting_toolStrip = new ToolStrip();
			this.Add_PublicText_toolStripButton = new ToolStripButton();
			this.Del_PublicText_toolStripButton = new ToolStripButton();
			this.Panel_PublicText_dataGridView = new DataGridView();
			this.panel_name_publictext = new DataGridViewTextBoxColumn();
			this.Item_PublicText = new DataGridViewTextBoxColumn();
			this.panel_info_desc = new DataGridViewTextBoxColumn();
			this.publictext_number = new DataGridViewTextBoxColumn();
			this.label_Desc_PublicText = new Label();
			this.Public_Text_richTextBox = new RichTextBox();
			this.label_Edit_PublicText = new Label();
			this.button_publictext_confirm = new Button();
			this.button_publictext_cancel = new Button();
			this.label_PublicText_List = new Label();
			this.zhGroupBox1 = new ZhGroupBox();
			((ISupportInitialize)this.Public_Text_Group).BeginInit();
			this.Public_Text_Setting_toolStrip.SuspendLayout();
			((ISupportInitialize)this.Panel_PublicText_dataGridView).BeginInit();
			this.zhGroupBox1.SuspendLayout();
			base.SuspendLayout();
			this.Public_Text_Group.AllowUserToAddRows = false;
			this.Public_Text_Group.AllowUserToResizeRows = false;
			this.Public_Text_Group.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.Public_Text_Group.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.Public_Text_Group.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Public_Text_Group.Columns.AddRange(new DataGridViewColumn[]
			{
				this.publictext_N0,
				this.publictext_Name,
				this.Rename_PublicText
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.Public_Text_Group.DefaultCellStyle = dataGridViewCellStyle2;
			this.Public_Text_Group.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.Public_Text_Group.Location = new System.Drawing.Point(13, 23);
			this.Public_Text_Group.Name = "Public_Text_Group";
			this.Public_Text_Group.RowHeadersVisible = false;
			this.Public_Text_Group.RowTemplate.Height = 23;
			this.Public_Text_Group.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.Public_Text_Group.Size = new System.Drawing.Size(323, 216);
			this.Public_Text_Group.TabIndex = 15;
			this.Public_Text_Group.CellClick += new DataGridViewCellEventHandler(this.Public_Text_Group_CellClick);
			this.Public_Text_Group.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.Public_Text_Group_RowPostPaint);
			this.Public_Text_Group.SelectionChanged += new EventHandler(this.Public_Text_select_Changed);
			this.publictext_N0.HeaderText = "序号";
			this.publictext_N0.Name = "publictext_N0";
			this.publictext_N0.ReadOnly = true;
			this.publictext_N0.Width = 60;
			this.publictext_Name.HeaderText = "公共文本名称";
			this.publictext_Name.Name = "publictext_Name";
			this.publictext_Name.ReadOnly = true;
			this.publictext_Name.Width = 200;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.NullValue = "重命名";
			this.Rename_PublicText.DefaultCellStyle = dataGridViewCellStyle3;
			this.Rename_PublicText.HeaderText = "编辑";
			this.Rename_PublicText.Name = "Rename_PublicText";
			this.Rename_PublicText.Width = 60;
			this.Public_Text_Setting_toolStrip.AutoSize = false;
			this.Public_Text_Setting_toolStrip.GripStyle = ToolStripGripStyle.Hidden;
			this.Public_Text_Setting_toolStrip.Items.AddRange(new ToolStripItem[]
			{
				this.Add_PublicText_toolStripButton,
				this.Del_PublicText_toolStripButton
			});
			this.Public_Text_Setting_toolStrip.Location = new System.Drawing.Point(0, 0);
			this.Public_Text_Setting_toolStrip.Name = "Public_Text_Setting_toolStrip";
			this.Public_Text_Setting_toolStrip.Size = new System.Drawing.Size(783, 49);
			this.Public_Text_Setting_toolStrip.TabIndex = 16;
			this.Public_Text_Setting_toolStrip.Text = "toolStrip1";
			this.Add_PublicText_toolStripButton.Image = Resources.PublicText_Add;
			this.Add_PublicText_toolStripButton.ImageScaling = ToolStripItemImageScaling.None;
			this.Add_PublicText_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Add_PublicText_toolStripButton.Margin = new Padding(10, 1, 0, 2);
			this.Add_PublicText_toolStripButton.Name = "Add_PublicText_toolStripButton";
			this.Add_PublicText_toolStripButton.Size = new System.Drawing.Size(35, 46);
			this.Add_PublicText_toolStripButton.Text = "添加";
			this.Add_PublicText_toolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.Add_PublicText_toolStripButton.TextImageRelation = TextImageRelation.ImageAboveText;
			this.Add_PublicText_toolStripButton.Click += new EventHandler(this.Add_PublicText_toolStripButton_Click);
			this.Del_PublicText_toolStripButton.Image = Resources.PublicText_Del;
			this.Del_PublicText_toolStripButton.ImageScaling = ToolStripItemImageScaling.None;
			this.Del_PublicText_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Del_PublicText_toolStripButton.Margin = new Padding(10, 1, 0, 2);
			this.Del_PublicText_toolStripButton.Name = "Del_PublicText_toolStripButton";
			this.Del_PublicText_toolStripButton.Size = new System.Drawing.Size(35, 46);
			this.Del_PublicText_toolStripButton.Text = "删除";
			this.Del_PublicText_toolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.Del_PublicText_toolStripButton.TextImageRelation = TextImageRelation.ImageAboveText;
			this.Del_PublicText_toolStripButton.Click += new EventHandler(this.Del_PublicText_toolStripButton_Click);
			this.Panel_PublicText_dataGridView.AllowUserToAddRows = false;
			this.Panel_PublicText_dataGridView.AllowUserToResizeRows = false;
			this.Panel_PublicText_dataGridView.BackgroundColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
			this.Panel_PublicText_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.Panel_PublicText_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Panel_PublicText_dataGridView.Columns.AddRange(new DataGridViewColumn[]
			{
				this.panel_name_publictext,
				this.Item_PublicText,
				this.panel_info_desc,
				this.publictext_number
			});
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
			this.Panel_PublicText_dataGridView.DefaultCellStyle = dataGridViewCellStyle5;
			this.Panel_PublicText_dataGridView.GridColor = System.Drawing.SystemColors.ButtonFace;
			this.Panel_PublicText_dataGridView.Location = new System.Drawing.Point(359, 22);
			this.Panel_PublicText_dataGridView.Name = "Panel_PublicText_dataGridView";
			this.Panel_PublicText_dataGridView.ReadOnly = true;
			this.Panel_PublicText_dataGridView.RowHeadersVisible = false;
			this.Panel_PublicText_dataGridView.RowTemplate.Height = 23;
			this.Panel_PublicText_dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.Panel_PublicText_dataGridView.Size = new System.Drawing.Size(413, 433);
			this.Panel_PublicText_dataGridView.TabIndex = 17;
			this.panel_name_publictext.HeaderText = "显示屏名称";
			this.panel_name_publictext.Name = "panel_name_publictext";
			this.panel_name_publictext.ReadOnly = true;
			this.panel_name_publictext.Width = 140;
			this.Item_PublicText.HeaderText = "节目名称";
			this.Item_PublicText.Name = "Item_PublicText";
			this.Item_PublicText.ReadOnly = true;
			this.panel_info_desc.HeaderText = "型号";
			this.panel_info_desc.Name = "panel_info_desc";
			this.panel_info_desc.ReadOnly = true;
			this.publictext_number.HeaderText = "数量";
			this.publictext_number.Name = "publictext_number";
			this.publictext_number.ReadOnly = true;
			this.publictext_number.Width = 70;
			this.label_Desc_PublicText.AutoSize = true;
			this.label_Desc_PublicText.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label_Desc_PublicText.Location = new System.Drawing.Point(380, 7);
			this.label_Desc_PublicText.Name = "label_Desc_PublicText";
			this.label_Desc_PublicText.Size = new System.Drawing.Size(135, 12);
			this.label_Desc_PublicText.TabIndex = 18;
			this.label_Desc_PublicText.Text = "当前公共文本应用屏幕";
			this.label_Desc_PublicText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Public_Text_richTextBox.BackColor = System.Drawing.Color.White;
			this.Public_Text_richTextBox.ForeColor = System.Drawing.Color.Black;
			this.Public_Text_richTextBox.Location = new System.Drawing.Point(13, 270);
			this.Public_Text_richTextBox.Name = "Public_Text_richTextBox";
			this.Public_Text_richTextBox.Size = new System.Drawing.Size(323, 185);
			this.Public_Text_richTextBox.TabIndex = 19;
			this.Public_Text_richTextBox.Text = "";
			this.Public_Text_richTextBox.TextChanged += new EventHandler(this.RichTextBox_PublicText_TextChanged);
			this.Public_Text_richTextBox.KeyDown += new KeyEventHandler(this.richTextBox_KeyDown);
			this.label_Edit_PublicText.AutoSize = true;
			this.label_Edit_PublicText.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label_Edit_PublicText.Location = new System.Drawing.Point(17, 255);
			this.label_Edit_PublicText.Name = "label_Edit_PublicText";
			this.label_Edit_PublicText.Size = new System.Drawing.Size(57, 12);
			this.label_Edit_PublicText.TabIndex = 20;
			this.label_Edit_PublicText.Text = "文本编辑";
			this.button_publictext_confirm.Location = new System.Drawing.Point(198, 466);
			this.button_publictext_confirm.Name = "button_publictext_confirm";
			this.button_publictext_confirm.Size = new System.Drawing.Size(58, 23);
			this.button_publictext_confirm.TabIndex = 21;
			this.button_publictext_confirm.Text = "确认";
			this.button_publictext_confirm.UseVisualStyleBackColor = true;
			this.button_publictext_confirm.Click += new EventHandler(this.button_publictext_confirm_Click);
			this.button_publictext_cancel.Location = new System.Drawing.Point(278, 466);
			this.button_publictext_cancel.Name = "button_publictext_cancel";
			this.button_publictext_cancel.Size = new System.Drawing.Size(58, 23);
			this.button_publictext_cancel.TabIndex = 22;
			this.button_publictext_cancel.Text = "取消";
			this.button_publictext_cancel.UseVisualStyleBackColor = true;
			this.button_publictext_cancel.Click += new EventHandler(this.button_publictext_cancel_Click);
			this.label_PublicText_List.AutoSize = true;
			this.label_PublicText_List.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label_PublicText_List.Location = new System.Drawing.Point(16, 7);
			this.label_PublicText_List.Name = "label_PublicText_List";
			this.label_PublicText_List.Size = new System.Drawing.Size(57, 12);
			this.label_PublicText_List.TabIndex = 24;
			this.label_PublicText_List.Text = "公共文本";
			this.label_PublicText_List.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.zhGroupBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.zhGroupBox1.Controls.Add(this.Public_Text_Group);
			this.zhGroupBox1.Controls.Add(this.label_PublicText_List);
			this.zhGroupBox1.Controls.Add(this.Panel_PublicText_dataGridView);
			this.zhGroupBox1.Controls.Add(this.button_publictext_cancel);
			this.zhGroupBox1.Controls.Add(this.label_Desc_PublicText);
			this.zhGroupBox1.Controls.Add(this.button_publictext_confirm);
			this.zhGroupBox1.Controls.Add(this.Public_Text_richTextBox);
			this.zhGroupBox1.Controls.Add(this.label_Edit_PublicText);
			this.zhGroupBox1.Location = new System.Drawing.Point(0, 53);
			this.zhGroupBox1.Name = "zhGroupBox1";
			this.zhGroupBox1.Size = new System.Drawing.Size(783, 507);
			this.zhGroupBox1.TabIndex = 25;
			this.zhGroupBox1.TextColor = System.Drawing.Color.Black;
			this.zhGroupBox1.TextFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(783, 560);
			base.Controls.Add(this.zhGroupBox1);
			base.Controls.Add(this.Public_Text_Setting_toolStrip);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = Resources.AppIcon;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formEditPublicText";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "公共文本编辑";
			base.FormClosing += new FormClosingEventHandler(this.formEdit_Public_Text_close);
			base.Load += new EventHandler(this.formEdit_Public_Text_load);
			((ISupportInitialize)this.Public_Text_Group).EndInit();
			this.Public_Text_Setting_toolStrip.ResumeLayout(false);
			this.Public_Text_Setting_toolStrip.PerformLayout();
			((ISupportInitialize)this.Panel_PublicText_dataGridView).EndInit();
			this.zhGroupBox1.ResumeLayout(false);
			this.zhGroupBox1.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
