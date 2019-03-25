using LedModel;
using LedModel.Content;
using LedModel.Data;
using LedModel.Enum;
using LedModel.Foundation;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LedControlSystem.LedControlSystem
{
	public class DataOperating
	{
		public string faultMessage = string.Empty;

		private string overlappingItem = "";

		private bool hasNullItem;

		private bool isALlItemClosed = true;

		private bool isAnimation;

		public bool CheckData(LedPanel panel)
		{
			bool flag = false;
			try
			{
				if (panel.PortType == LedPortType.USB)
				{
					this.faultMessage = this.overlappingItem + formMain.ML.GetStr("formMain_SendData_SelectedUsb");
					bool result = flag;
					return result;
				}
				if (panel.Items.Count == 0)
				{
					this.faultMessage = this.overlappingItem + formMain.ML.GetStr("Prompt_NoItem");
					bool result = flag;
					return result;
				}
				if (this.CheckSubareaOverLapping(panel))
				{
					this.faultMessage = this.overlappingItem + formMain.ML.GetStr("Prompt_ItemSubareaOverlap");
					bool result = flag;
					return result;
				}
				if (this.isALlItemClosed)
				{
					this.faultMessage = this.overlappingItem + formMain.ML.GetStr("Prompt_AllItemClosed");
					bool result = flag;
					return result;
				}
				if (this.hasNullItem)
				{
					this.faultMessage = this.overlappingItem + formMain.ML.GetStr("Prompt_HasNullIitem");
					bool result = flag;
					return result;
				}
				if (this.CheckEmptyMarquee(panel))
				{
					this.faultMessage = formMain.ML.GetStr("Prompt_HasEmptyMarquee");
					bool result = flag;
					return result;
				}
				if (this.CheckLunarSubarea(panel))
				{
					this.faultMessage = formMain.ML.GetStr("Prompt_NotSupportLunarCalendar");
					bool result = flag;
					return result;
				}
				if (this.CheckStringSubarea(panel))
				{
					this.faultMessage = formMain.ML.GetStr("Prompt_NotSupportString");
					bool result = flag;
					return result;
				}
				string empty = string.Empty;
				if (this.CheckRepeatStringSubarea(panel, ref empty))
				{
					this.faultMessage = empty;
					bool result = flag;
					return result;
				}
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public bool CheckAnimationAndBackground(LedPanel _this_Panel)
		{
			bool result = false;
			for (int i = 0; i < _this_Panel.Items.Count; i++)
			{
				LedItem ledItem = _this_Panel.Items[i];
				if (ledItem.Background.Enabled)
				{
					result = true;
					break;
				}
				for (int j = 0; j < ledItem.Subareas.Count; j++)
				{
					LedSubarea ledSubarea = ledItem.Subareas[j];
					if (ledSubarea.Type == LedSubareaType.Animation)
					{
						result = true;
						break;
					}
					if (ledSubarea.Type == LedSubareaType.PictureText)
					{
						for (int k = 0; k < ledSubarea.Contents.Count; k++)
						{
							LedPictureText ledPictureText = (LedPictureText)ledSubarea.Contents[k];
							if (ledPictureText.PictureTextType == LedPictureTextType.Animation)
							{
								result = true;
								break;
							}
							if (ledPictureText.Background.Enabled)
							{
								result = true;
								break;
							}
						}
					}
					else if (ledSubarea.Type == LedSubareaType.Subtitle)
					{
						LedDText ledDText = (LedDText)ledSubarea.SelectedContent;
						if (ledDText.Background.Enabled)
						{
							result = true;
							break;
						}
					}
					else if (ledSubarea.SelectedContent.Background.Enabled)
					{
						result = true;
						break;
					}
				}
			}
			this.isAnimation = result;
			return result;
		}

		public bool GenerateData(LedPanel panel, ref Process process)
		{
			bool result = false;
			try
			{
				for (int i = 0; i < panel.Items.Count; i++)
				{
					for (int j = 0; j < panel.Items[i].Subareas.Count; j++)
					{
						if (panel.Items[i].Subareas[j].Type == LedSubareaType.Subtitle)
						{
							LedDText ledDText = (LedDText)panel.Items[i].Subareas[j].SelectedContent;
							if (ledDText.DoNeedDrawingFull)
							{
								ledDText.DrawMode = LedDrawMode.Full;
								ledDText.Draw();
								ledDText.DrawMode = LedDrawMode.Part;
							}
						}
						else if (panel.Items[i].Subareas[j].Type == LedSubareaType.PictureText)
						{
							LedSubarea ledSubarea = panel.Items[i].Subareas[j];
							for (int k = 0; k < ledSubarea.Contents.Count; k++)
							{
								LedPictureText ledPictureText = (LedPictureText)ledSubarea.Contents[k];
								if (ledPictureText.DoNeedDrawingFull || (ledPictureText.LastDrawn != null && ledPictureText.GetSize() != ledPictureText.LastDrawn.Size))
								{
									ledPictureText.DrawMode = LedDrawMode.Full;
									ledPictureText.Draw();
									ledPictureText.DrawMode = LedDrawMode.Part;
								}
							}
						}
					}
				}
				if (this.isAnimation)
				{
					this.GetAnimationAndBackground(panel);
				}
				process = new Process();
				if (panel.IsLSeries())
				{
					process.PanelBytes = panel.ToLBytes();
					process.ItemTimerLBytes = panel.ToItemTimerLByte();
					process.ItemStartLBytes = panel.ToItemStartLBytes();
					process.ItemLBytes = panel.ToItemLBytes();
				}
				else
				{
					process.PanelBytes = panel.ToBytes();
					process.BmpDataBytes = panel.ToItemBmpDataBytes();
					process.ItemBytes = panel.ToItemBytes();
				}
				result = true;
			}
			catch
			{
				process = null;
				result = false;
			}
			return result;
		}

		private bool CheckSubareaOverLapping(LedPanel _this_Panel)
		{
			bool result = false;
			try
			{
				this.overlappingItem = "";
				for (int i = 0; i < _this_Panel.Items.Count; i++)
				{
					if (_this_Panel.Items[i].Subareas.Count == 0)
					{
						this.hasNullItem = true;
					}
					if (!_this_Panel.Items[i].PlaySetting.Closed)
					{
						this.isALlItemClosed = false;
					}
					foreach (LedSubarea current in _this_Panel.Items[i].Subareas)
					{
						if (current.IsOverlapping())
						{
							this.overlappingItem = this.overlappingItem + _this_Panel.Items[i].TextName + "、";
							result = true;
							break;
						}
					}
				}
				if (!string.IsNullOrEmpty(this.overlappingItem))
				{
					this.overlappingItem = this.overlappingItem.Substring(0, this.overlappingItem.Length - 1);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private bool CheckEmptyMarquee(LedPanel _this_Panel)
		{
			for (int i = 0; i < _this_Panel.Items.Count; i++)
			{
				foreach (LedSubarea current in _this_Panel.Items[i].Subareas)
				{
					if (current.Type == LedSubareaType.PictureText && current.Contents.Count == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool CheckLunarSubarea(LedPanel _this_Panel)
		{
			bool flag = false;
			bool flag2 = _this_Panel.IsLSeries();
			for (int i = 0; i < _this_Panel.Items.Count; i++)
			{
				foreach (LedSubarea current in _this_Panel.Items[i].Subareas)
				{
					if (!flag2 && current.Type == LedSubareaType.Lunar)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		private bool CheckStringSubarea(LedPanel _this_Panel)
		{
			bool flag = false;
			bool flag2 = _this_Panel.IsLSeries();
			for (int i = 0; i < _this_Panel.Items.Count; i++)
			{
				foreach (LedSubarea current in _this_Panel.Items[i].Subareas)
				{
					if (!flag2 && current.Type == LedSubareaType.String)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		private bool CheckRepeatStringSubarea(LedPanel _this_Panel, ref string msg)
		{
			bool result = false;
			msg = string.Empty;
			List<string[]> list = new List<string[]>();
			foreach (LedItem current in _this_Panel.Items)
			{
				foreach (LedSubarea current2 in current.Subareas)
				{
					if (current2.Type == LedSubareaType.String)
					{
						list.Add(new string[]
						{
							current.TextName,
							current2.SelectedContent.ID
						});
					}
				}
			}
			List<int> list2 = new List<int>();
			for (int i = 0; i < list.Count - 1; i++)
			{
				if (!list2.Contains(i))
				{
					string[] array = list[i];
					string text = string.Empty;
					for (int j = i + 1; j < list.Count; j++)
					{
						if (!list2.Contains(j))
						{
							string[] array2 = list[j];
							if (array[1] == array2[1])
							{
								list2.Add(j);
								if (!string.IsNullOrEmpty(text))
								{
									text += "、";
								}
								text += array2[0];
							}
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						msg += string.Format(formMain.ML.GetStr("Message_Repeat_String_Subarea"), text.Contains(array[0]) ? text : string.Format("{0}、{1}", array[0], text), array[1], "\r\n");
					}
				}
			}
			if (!string.IsNullOrEmpty(msg))
			{
				result = true;
			}
			return result;
		}

		private void GetAnimationAndBackground(LedPanel _this_Panel)
		{
			for (int i = 0; i < _this_Panel.Items.Count; i++)
			{
				LedItem ledItem = _this_Panel.Items[i];
				if (ledItem.Background.Enabled)
				{
					this.MakeAnimation(new System.Drawing.Size(_this_Panel.Width, _this_Panel.Height), ledItem.Background);
				}
				for (int j = 0; j < ledItem.Subareas.Count; j++)
				{
					LedSubarea ledSubarea = ledItem.Subareas[j];
					if (ledSubarea.Type == LedSubareaType.Animation)
					{
						LedAnimation ledAnimation = (LedAnimation)ledSubarea.SelectedContent;
						this.MakeAnimation(ledAnimation);
						if (ledAnimation.Background.Enabled)
						{
							this.MakeAnimation(ledSubarea.Size, ledAnimation.Background);
						}
					}
					else if (ledSubarea.Type == LedSubareaType.PictureText)
					{
						for (int k = 0; k < ledSubarea.Contents.Count; k++)
						{
							LedPictureText ledPictureText = (LedPictureText)ledSubarea.Contents[k];
							if (ledPictureText.PictureTextType == LedPictureTextType.Animation)
							{
								this.MakeAnimation((LedAnimation)ledPictureText);
							}
							if (ledPictureText.Background.Enabled)
							{
								this.MakeAnimation(ledSubarea.Size, ledPictureText.Background);
							}
						}
					}
					else if (ledSubarea.Type == LedSubareaType.Subtitle)
					{
						LedDText ledDText = (LedDText)ledSubarea.SelectedContent;
						if (ledDText.Background.Enabled)
						{
							this.MakeAnimation(ledSubarea.Size, ledDText.Background);
						}
					}
					else if (ledSubarea.SelectedContent.Background.Enabled)
					{
						this.MakeAnimation(ledSubarea.Size, ledSubarea.SelectedContent.Background);
					}
				}
			}
		}

		private void MakeAnimation(System.Drawing.Size pSize, LedBackground pBackground)
		{
			if (pBackground == null)
			{
				return;
			}
			if (pBackground.Changed && !pBackground.CustomChecked)
			{
				formAnimationMaker formAnimationMaker = new formAnimationMaker();
				formAnimationMaker.Make(pSize, pBackground);
				pBackground.Changed = false;
				formAnimationMaker.Dispose();
			}
		}

		public void MakeAnimation(LedAnimation pAnimation)
		{
			if (pAnimation.Changed)
			{
				formAnimationMaker formAnimationMaker = new formAnimationMaker();
				formAnimationMaker.Make(pAnimation.GetSize(), pAnimation);
				pAnimation.Changed = false;
				formAnimationMaker.Dispose();
			}
		}
	}
}
