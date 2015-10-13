using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;


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
		webrequest.Headers.Add("HTTP_CLIENT_IP",getRandomIP());
		webrequest.Headers.Add("HTTP_X_FORWARDED_FOR", getRandomIP());
		webrequest.Headers.Add("Cookie", "BAIDUID=7BCA2F7649641FF1772EE25471F3D0D7:FG=1; BIDUPSID=7BCA2F7649641FF1772EE25471F3D0D7; PSTM=1434445807; BDSFRCVID=VNAsJeCCYwUWvvRlAXR3hyfdjeKK0gOTHx0c1xrAxW3KN1-UyMQ-EG0PtU8ki50-j7EnogKK0gOTH65P; H_BDCLCKID_SF=tJ-tVCI5fIvjHnO15tK_-PuV-U702t_XKKOLV-O62-Okeq8CD6OjBTtrXUvCXTb32T5h0ncHJC85V-Q2y5jHhUIlDqju04rpWGOJXPQ62hTpsIJMyxnS0J84Q2cBtMQXaKviaKJEBMb1fRoMe6KWjjc-jNAs5-J2aTTasJOEKJOsjJrpq4bohjPZX4QeBtQm0a5f-x3KbfoSEfn-DP7Bb40qbtRiJPb9QgbOon8hMPoNJM0RyUIaqqL-XJLL0x-jLTnPVnvVJCtVjC3Hh4nJyUnQbPnnBTKtLnQ2QJ8BJCIKhDJP; UBI=fi_PncwhpxZ%7ETaJczDvWIx0oYq4jVcfHcKM; H_PS_PSSID=1439_14413_14511_14594_14431_14831_12867_14622_13201_14668_14870_12723_14549_14626_14485_14920_14903_11633_10633_14931; HOSUPPORT=1");
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
	public static void log(String data, String prefix="normal")
	{
		DateTime dateValue = DateTime.Now;
		String log = dateValue.ToString();
		log += " ";
		log += data;
		String path = string.Format("{0:yyyy-MM-dd}", dateValue);
		path += "." + prefix;
		path += ".log";
		file_put_contents(path, log);
	}
	public static String getRandomIP()
	{
		Random d = new Random(DateTime.Now.Second);
		String IP = "";

		int num = d.Next(0,255);

		IP = string.Format("{0}.{1}.{2}.{3}", d.Next(0,255).ToString(), d.Next(0, 255).ToString(), d.Next(0, 255).ToString(), d.Next(0, 255).ToString());
		return IP;
	}

	public static String getRandomPhone()
	{
		Random d = new Random(DateTime.Now.Millisecond);
		String phone = "";

		int num = d.Next(0, 255);

		phone = string.Format("186{0}{1}", d.Next(1000, 9999).ToString(), d.Next(1000, 9999).ToString());
		return phone;
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
