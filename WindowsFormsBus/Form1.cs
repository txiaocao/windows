using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsBus
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

			

			String 公交分类 = xiaok.file_get_contents(@"data/公交分类.txt");

			String[] str = 公交分类.Split(new Char[] { ',' });

			foreach (string item in str)
			{
				分站点(item);
				Console.WriteLine(item);
			}

			//分站点("http://m.8684.cn/beijing_t_%E9%80%9A%E5%8B%A4%E7%BA%BF%E8%B7%AF");
			//Regex reg = new Regex(@"<a .*?</a>");

			//var mat = reg.Matches(html);

			//foreach (Match item in mat)
			//{
			//	Console.WriteLine(item.ToString());
			//}
		}


		private String 分站点(String 站点名称)
		{
			String url = "http://m.8684.cn/beijing_t_" + 站点名称;
            String html = xiaok.httpGet(url);
			var mat = xiaok.match(@"<ul class=""list borderNone.*?</ul>", html);
			//mat = xiaok.match(@"<a .*?</a>", html);
			var node = xiaok.match(@"<a.*?/a>", mat[0]);
			string node_string = string.Join(",", node);
			string node_string2 = xiaok.replace(@"<.*?>", node_string);
			xiaok.file_put_contents(@"data/" + 站点名称 + ".raw.txt", node_string);
			xiaok.file_put_contents(@"data/"+ 站点名称+".txt", node_string2);
			return "";
		}
	}
}
