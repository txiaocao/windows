using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePublishHtml5
{
	class Program
	{
		private const string PATH_SPLIT_CHAR = "\\";
		static void Main(string[] args)
		{
            //String path2 = Process.GetCurrentProcess().MainModule.FileName;
            //DirectoryInfo dir = new DirectoryInfo(Path.GetTempPath());

            ////Shell.clearHtml(@"C:\Users\k\Documents\Visual Studio 2015\Projects\pubHTML5\ConsolePublishHtml5\bin\Debug\index.html");
            //Console.ReadKey();
            //string str = "程序{0}启动 {0}";
            //;
            //         Console.WriteLine(String.Format(str, 1));
            //Console.ReadKey();
            String CurrentDirectory = System.Environment.CurrentDirectory;
            //String maskdirname = new Date();
            String PublishDirectory = CurrentDirectory + PATH_SPLIT_CHAR + "publish";
            String CurrentPublishDirectory = Path.GetTempPath() + "publish." + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);

			Console.WriteLine("程序启动");

			if (Directory.Exists(PublishDirectory))
			{
				DirectoryInfo di = new DirectoryInfo(PublishDirectory);
				di.Delete(true);
			}
            

            
            Directory.CreateDirectory(CurrentPublishDirectory);

            Console.WriteLine("拷贝数据");

			//Console.WriteLine("/publish".LastIndexOf("publish"));
			Shell.CopyFiles(CurrentDirectory, CurrentPublishDirectory);

            Directory.CreateDirectory(PublishDirectory);
            // Console.ReadKey();
            Shell.CopyFiles(CurrentPublishDirectory, PublishDirectory);
            CurrentPublishDirectory = PublishDirectory;
            //Console.ReadKey();
            Console.WriteLine("文件检索");
			Shell.FilesList = new ArrayList();
			Shell.getFiles(CurrentPublishDirectory);

            Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft\Microsoft Ajax Minifier\AjaxMin.exe";
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;

            foreach (string path in Shell.FilesList)
			{
				string ext = path.Substring(path.LastIndexOf(".")+1);
				//Console.WriteLine(ext);
				//Console.ReadKey();
				if (ext == "js" || ext == "css")
				{
                    Console.WriteLine("JS/CSS：" + path);
                    String CMD = " \"{0}\" -o \"{0}\" ";
                    proc.StartInfo.Arguments = String.Format(CMD, path);
                    proc.Start();//启动 
                    proc.WaitForExit();

                    //String shellcmdtpl = "AjaxMin \"{0}\" -o \"{0}\" ";
                    //String shellcmd = String.Format(shellcmdtpl, path);
                    ////+ System.Environment.CurrentDirectory;
                    ////Console.WriteLine(shellcmd);
                    ////Console.ReadKey();
                    //Shell.RunCmd(shellcmd);
                }

				if (ext == "htm" || ext == "html")
				{
                    Console.WriteLine("Clear HTML："+ path);
					Shell.clearHtml(path);
                }
                if (ext == "exe")
                {
                    File.Delete(path);
                }
				//Console.WriteLine(path);
			}


            

            //String js = Application.StartupPath + "/test.js";
            //String cmd = "AjaxMin {0} -o {0}";
            //Console.Write(System.Environment.CurrentDirectory);

            //Console.ReadKey();

            //Shell.CopyFiles(CurrentPublishDirectory, PublishDirectory);
            String cmd = " explorer.exe " + PublishDirectory;
            Shell.RunCmd(cmd);
            Environment.Exit(0);
            //Console.ReadKey();

            //Shell.ShellExecute(this.Handle, "open", "file://" + Application.StartupPath, null, null, (int)Shell.ShowWindowCommands.SW_SHOW);

        }
	}
}
