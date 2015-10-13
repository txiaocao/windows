using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bing3v
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.Show();
			timer2.Start();
			
			//获取结果
			//string strRst = process.StandardOutput.ReadToEnd();
			//label1.Text = strRst;
		}

		private void Form1_ResizeEnd(object sender, EventArgs e)
		{
			
		}

		private void richTextBox1_Resize(object sender, EventArgs e)
		{

		}

		private void Form1_Resize(object sender, EventArgs e)
		{

			richTextBox1.Width = this.Width;
			richTextBox1.Height = this.Height;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			Application.Exit();
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			StreamReader reader = null;
			string php = "\"" + System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "php\\php.exe" + "\"";

			//实例一个process类
			Process process = new Process();
			//设定程序名
			process.StartInfo.FileName = "cmd.exe";
			//关闭Shell的使用
			process.StartInfo.UseShellExecute = false;
			//重新定向标准输入，输入，错误输出
			process.StartInfo.RedirectStandardInput = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			//设置cmd窗口不显示
			process.StartInfo.CreateNoWindow = true;
			//开始
			process.Start();


			//输入命令，退出
			//process.StandardOutput.Read();
			process.StandardInput.WriteLine(php + " php/bing.php");
			//process.StandardInput.WriteLine("netstat");
			process.StandardInput.WriteLine("exit");

			reader = process.StandardOutput;//截取输出流
			string line = reader.ReadLine();//每次读取一行
			while (!reader.EndOfStream)
			{
				if (line.Length > 0)
				{
					richTextBox1.AppendText(line + "\n");
				}
				line = reader.ReadLine();

			}
			timer2.Stop();
			richTextBox1.AppendText("执行完毕，3秒后自动退出\n");
			timer1.Start();
		}
	}
}
