using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePublishHtml5
{
	class Shell
	{
		private const string PATH_SPLIT_CHAR = "\\";
		public static ArrayList FilesList;


		public static void clearHtml(string sourceDir)
		{
			string str = xiaok.file_get_contents(sourceDir);
            str = xiaok.replace(@"\r", str);
            str = xiaok.replace(@"\n", str);
			str = xiaok.replace(@"  ", str);
			str = xiaok.replace(@"\t", str);
			str = xiaok.replace(@"<!--.*?-->", str);
			str = xiaok.replace(@"<!doctype html>", str, "<!doctype html>\n");
			File.Delete(sourceDir);
            xiaok.file_put_contents(sourceDir,str);
			//Console.WriteLine(str);
        }

		public static void CopyFiles(string sourceDir, string targetDir, bool overWrite = true, bool copySubDir = true)
		{
			//复制当前目录文件  
			foreach (string sourceFileName in Directory.GetFiles(sourceDir))
			{
				string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PATH_SPLIT_CHAR) + 1));

				if (File.Exists(targetFileName))
				{
					if (overWrite == true)
					{
						File.SetAttributes(targetFileName, FileAttributes.Normal);
						File.Copy(sourceFileName, targetFileName, overWrite);
					}
				}
				else
				{
					File.Copy(sourceFileName, targetFileName, overWrite);
				}
			}
			//复制子目录  
			if (copySubDir)
			{
				foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
				{
					//Console.WriteLine(sourceSubDir.LastIndexOf("publish"));
					//if (sourceSubDir.LastIndexOf("publish") == -1)
					//{
						string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
						if (!Directory.Exists(targetSubDir))
						{
							Directory.CreateDirectory(targetSubDir);
						}
						CopyFiles(sourceSubDir, targetSubDir, overWrite, true);
					//}

				}
			}
		}


		public static void getFiles(string sourceDir)
		{
			//ArrayList files = new ArrayList();
			//复制当前目录文件  
			foreach (string sourceFileName in Directory.GetFiles(sourceDir))
			{
				Shell.FilesList.Add(sourceFileName);
				//Console.WriteLine(sourceFileName);
			}

			foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
			{
				getFiles(sourceSubDir);

			}
		}


		// 执行CMD命令

		public static void RunCmd(string cmd)
		{
			Process proc = new Process();
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.FileName = "cmd.exe";
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.RedirectStandardError = true;
			proc.StartInfo.RedirectStandardInput = true;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.Start();
            proc.StandardInput.WriteLine(cmd);
            proc.WaitForExit();
            //proc.Close();
        }

		// 调用SHELL
		[DllImport("shell32.dll")]
		public extern static IntPtr ShellExecute(IntPtr hwnd,
												string lpOperation,
												string lpFile,
												string lpParameters,
												string lpDirectory,
												int nShowCmd
											   );
		public enum ShowWindowCommands : int
		{

			SW_HIDE = 0,
			SW_SHOWNORMAL = 1,
			SW_NORMAL = 1,
			SW_SHOWMINIMIZED = 2,
			SW_SHOWMAXIMIZED = 3,
			SW_MAXIMIZE = 3,
			SW_SHOWNOACTIVATE = 4,
			SW_SHOW = 5,
			SW_MINIMIZE = 6,
			SW_SHOWMINNOACTIVE = 7,
			SW_SHOWNA = 8,
			SW_RESTORE = 9,
			SW_SHOWDEFAULT = 10,
			SW_MAX = 10
		}
	}
}
