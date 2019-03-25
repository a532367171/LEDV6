using LedControlSystem.LedControlSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LedControlSystem
{
	internal static class Program
	{
		public static bool IsforeignTradeMode;

		[DllImport("LedTool.dll", CharSet = CharSet.Auto)]
		private static extern bool GetProcessCmdLine(uint piddwPID, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pCmdLine, ref uint dwBufLen);

		[STAThread]
		private static int Main(string[] Args)
		{
			int result = 0;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Control.CheckForIllegalCrossThreadCalls = false;
			Thread.Sleep(500);
			int num = 0;
			int num2 = 1;
			List<int> list = new List<int>();
			Process[] processes = Process.GetProcesses();
			int i = 0;
			while (i < processes.Length)
			{
				Process process = processes[i];
				if (process.ProcessName == "LED_System_TcpUdpRsServer")
				{
                    try
                    {
                        if (process.MainModule != null)
                        {
                            string fileName = process.MainModule.FileName;
                            if (!string.IsNullOrEmpty(fileName))
                            {
                                FileInfo fileInfo = new FileInfo(fileName);
                                if (fileInfo != null)
                                {
                                    string name = fileInfo.Name;
                                    if (Application.StartupPath == fileInfo.DirectoryName)
                                    {
                                        if (name.Equals("LED_System_TcpUdpRsServer.exe"))
                                        {
                                            list.Add(process.Id);
                                        }
                                        else if (name.Equals("LedControlSystem.exe"))
                                        {
                                            num++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (process.ProcessName == "LED_System_TcpUdpRsServer")
                        {
                            list.Add(process.Id);
                        }
                        else
                        {
                            string b = "LED Control System V6";
                            if (Program.IsforeignTradeMode)
                            {
                                b = "LED Control System V5";
                            }
                            if (process.MainWindowTitle == b)
                            {
                                num++;
                            }
                        }
                    }
                    i++;

                    continue;
                }
                if (process.ProcessName == "LedControlSystem")
				{

                    i++;
                    continue;
                }
              
				if (process.ProcessName == "LCSVM")
				{
					try
					{
						StringBuilder stringBuilder = new StringBuilder(512);
						uint num3 = 0u;
						bool processCmdLine = Program.GetProcessCmdLine((uint)process.Id, stringBuilder, ref num3);
						if (processCmdLine)
						{
							string text = string.Empty;
							if ((ulong)num3 > (ulong)((long)stringBuilder.Length))
							{
								byte[] bytes = Encoding.Default.GetBytes(stringBuilder.ToString());
								if ((long)bytes.Length >= (long)((ulong)num3))
								{
									text = Encoding.Default.GetString(bytes, 0, (int)num3);
								}
							}
							else
							{
								text = stringBuilder.ToString().Substring(0, (int)num3);
							}
							int num4 = text.LastIndexOf("\" ");
							if (num4 > -1)
							{
								string text2 = text.Substring(num4 + 2);
								if (text2.Equals("LED_System_TcpUdpRsServer.exe"))
								{
									list.Add(process.Id);
								}
								else if (text2.StartsWith("\"") && text2.EndsWith("\""))
								{
									FileInfo fileInfo2 = new FileInfo(text2.Substring(1, text2.Length - 2));
									if (fileInfo2 != null && fileInfo2.Name.Equals("LedControlSystem.exe"))
									{
										num++;
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
                i++;
            }
            if (num > num2)
			{
				MessageBox.Show("该程序已经在运行中!Already runing...", "LEDControlSystem", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				result = 1;
			}
			else
			{
				if (list.Count > 0)
				{
					foreach (int current in list)
					{
						Process.GetProcessById(current).Kill();
					}
				}
                //WindowsIdentity current2 = WindowsIdentity.GetCurrent();
                //WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current2);
                //if (!windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
                //{
                //    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                //    processStartInfo.UseShellExecute = true;
                //    processStartInfo.WorkingDirectory = Environment.CurrentDirectory;
                //    processStartInfo.FileName = Application.ExecutablePath;
                //    processStartInfo.Verb = "runas";
                //    try
                //    {
                //        Process.Start(processStartInfo);
                //    }
                //    catch
                //    {
                //        return result;
                //    }
                //    Application.Exit();
                //    return result;
                //}
                Application.Run(new formMain());
			}
			return result;
		}
	}
}
