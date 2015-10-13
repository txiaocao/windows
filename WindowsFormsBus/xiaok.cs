using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsBus
{
	public class xiaok
	{
		public xiaok()
		{
		}
		/**
		* 通过HTTP获取数据
		*/
		public static String httpGet(String url)
		{
			HttpWebRequest webrequest =
				(HttpWebRequest)WebRequest.Create(url);

			webrequest.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.3 (KHTML, like Gecko) Version/8.0 Mobile/12A4345d Safari/600.1.4";
			HttpWebResponse myResponse = (HttpWebResponse)webrequest.GetResponse();
			StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
			string content = reader.ReadToEnd();
			//Console.WriteLine(content);
			return content;
		}
		/**
		* 正则文本匹配
		*/
		public static String[] match(String reg, String content)
		{
			Regex regex = new Regex(reg);

			var matches = regex.Matches(content);
			ArrayList arr = new ArrayList();

			foreach (Match item in matches)
			{
				arr.Add(item.Value);
			}
			string[] arrString = (string[])arr.ToArray(typeof(string));
			return arrString;
		}
		/**
		* 正则文本替换
		*/
		public static String replace(String reg, String content, String rep = "")
		{
			return Regex.Replace(content, reg, rep);

		}
		/**
		* 获取文件全部内容
		*/
		public static String file_get_contents(String path)
		{
			StreamReader txtStreamReader = new StreamReader(path);
			return txtStreamReader.ReadToEnd();
		}
		/**
		* 写出文件全部内容
		*/
		public static void file_put_contents(String path, String data)
		{
			StreamWriter sr;
			if (File.Exists(path)) //如果文件存在,则创建File.AppendText对象
			{
				sr = File.AppendText(path);
			}
			else  //如果文件不存在,则创建File.CreateText对象
			{
				sr = File.CreateText(path);
			}
			sr.WriteLine(data);
			sr.Close();

		}
	}
}
