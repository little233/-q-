using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Native.Core.App
{
    public static class DataHelper
    {
        private static string jwttoken = "";

        public struct ImageUrl 
        {
            public ImageUrl(string p, string id, string url)
            {
                P = p;
                ID = id;
                URL = url;
            }
            public string P;
            public string ID;
            public string URL;
        }

        public static void AntiShielding(this Bitmap bmp)
        {
            Random r = new Random();
            int x = r.Next(0, bmp.Width);
            int y = r.Next(0, bmp.Height);

            Color c = bmp.GetPixel(x, y);
            if (c.A > 128)
            {
                bmp.SetPixel(x, y, Color.FromArgb(c.A - 1, c.R, c.G, c.B));
            }
            else
            {
                bmp.SetPixel(x, y, Color.FromArgb(c.A + 1, c.R, c.G, c.B));
            }
        }

        #region -- 中文数字转换 --
        /// <summary>
        /// 中文转数字
        /// </summary>
        /// <param name="src">中文数字 如：九亿八千七百六十五万四千三百二十一</param>
        /// <returns></returns>
        public static long Chinese2Num( this string src)
        {
            src = src.Replace("壹", "一")
                .Replace("贰", "二")
                .Replace("两", "二")
                .Replace("兩", "二")
                .Replace("叁", "三")
                .Replace("肆", "四")
                .Replace("伍", "五")
                .Replace("陆", "六")
                .Replace("陸", "六")
                .Replace("柒", "七")
                .Replace("捌", "八")
                .Replace("玖", "九")
                .Replace("拾", "十")
                .Replace("佰", "百")
                .Replace("仟", "千")
                .Replace("萬", "万");

            // 定义一个数组，用于接受分割字符串的结果
            string[] srcArr;
            // 定义计算结果
            int result = 0;
            // 如果字符串中包含'亿'则进行分割
            if (src.IndexOf("亿") != -1)
            {
                // 以字符'亿'分割源字符串
                srcArr = src.Split('亿');
                // 计算亿以上的数字
                result += Convert.ToInt32(Convert2Number(srcArr[0]) * Math.Pow(10, 8));
                // 如果剩余字符串中包括'万'，则再次进行分割
                if (src.IndexOf("万") != -1)
                {
                    // 以字符'万'分割源字符串
                    srcArr = srcArr[1].Split('万');
                    // 计算万以上亿以下的数字
                    result += Convert.ToInt32(Convert2Number(srcArr[0]) * Math.Pow(10, 4)) + Convert.ToInt32(Convert2Number(srcArr[1]));
                }
            }
            // 如果字符串中不包含字符'亿'
            else
            {
                // 如果源字符串中包括'万'，则以'万'字进行分割
                if (src.IndexOf("万") != -1)
                {
                    srcArr = src.Split('万');
                    result += Convert.ToInt32(Convert2Number(srcArr[0]) * Math.Pow(10, 4)) + Convert.ToInt32(Convert2Number(srcArr[1]));
                }
                else
                {
                    // 源文本为1万以下的数字
                    result += Convert.ToInt32(Convert2Number(src));
                }
            }
            return result;
        }

        /// <summary>
        /// 1万以内中文转数字
        /// </summary>
        /// <param name="src">源文本如：四千三百二十一</param>
        /// <returns></returns>
        public static long Convert2Number( this string src )
        {
            // 定义包含所有数字的字符串，用以判断字符是否为数字。
            string numberString = "零一二三四五六七八九十";
            // 定义单位字符串，用以判断字符是否为单位。
            string unitString = "零十百千";
            // 把数字字符串转换为char数组，方便截取。
            char[] charArr = src.Replace(" ", "").ToCharArray();
            // 返回结果
            int result = 0;
            // 如果源为空指针、空字符串、空白字符串 则返回0
            if (string.IsNullOrEmpty(src) || string.IsNullOrWhiteSpace(src))
            {
                return 0;
            }
            // 如果源的第一个字符不是数字 则返回0
            if (numberString.IndexOf(charArr[0]) == -1)
            {
                return 0;
            }
            if (src.Length > 1)  //至少两位才进入位数计算，避免单一个"十"字时无法识别或进入位数计算导致报错的问题
            {
                // 遍历字符数组
                for (int i = 0; i < charArr.Length; i++)
                {
                    // 遍历单位字符串
                    for (int j = 0; j < unitString.Length; j++)
                    {
                        // 如果字符为单位则进行计算
                        if (charArr[i] == unitString[j])
                        {
                            // 如果字符为非'零'字符，则计算出十位以上到万位以下数字的和
                            if (charArr[i] != '零')
                            {
                                result += Convert.ToInt32(int.Parse(numberString.IndexOf(charArr[i - 1]).ToString()) * Math.Pow(10, j));
                            }
                        }
                    }
                }
            }
            // 如果源文本末尾字符为'零'-'十'其中之一，则计算结果和个位数相加。
            if (numberString.IndexOf(charArr[charArr.Length - 1]) != -1)
            {
                result += numberString.IndexOf(charArr[charArr.Length - 1]);
            }
            // 返回计算结果。
            return result;
        }

        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <returns></returns>
        public static string NumberToChinese( this long number)
        {
            string res = string.Empty;
            string str = number.ToString();
            string schar = str.Substring(0, 1);
            switch (schar)
            {
                case "1":
                    res = "一";
                    break;
                case "2":
                    res = "二";
                    break;
                case "3":
                    res = "三";
                    break;
                case "4":
                    res = "四";
                    break;
                case "5":
                    res = "五";
                    break;
                case "6":
                    res = "六";
                    break;
                case "7":
                    res = "七";
                    break;
                case "8":
                    res = "八";
                    break;
                case "9":
                    res = "九";
                    break;
                default:
                    res = "零";
                    break;
            }
            if (str.Length > 1)
            {
                switch (str.Length)
                {
                    case 2:
                    case 6:
                        res += "十";
                        break;
                    case 3:
                    case 7:
                        res += "百";
                        break;
                    case 4:
                        res += "千";
                        break;
                    case 5:
                        res += "万";
                        break;
                    default:
                        res += "";
                        break;
                }
                res += NumberToChinese(int.Parse(str.Substring(1, str.Length - 1)));
            }
            return res;
        }
        #endregion -- 中文数字转换 --

        #region -- Https请求 --

        public static string GetHttpResponse(string url, int timeout = 60000)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;
            //request.ContentType = "text/html;charset=UTF-8";
            request.Timeout = timeout;
            request.Method = "GET";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers.Add("Accept-Language", "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.2; rv:12.0) Gecko/20100101 Firefox/12.0";

            request.ContentType = "application/x-www-form-urlencoded";

            //往头部加入自定义验证信息
            SetHeaderValue(request.Headers, "Authorization", jwttoken);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        /// <summary>
        /// post方法
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postString">post数据的字符串</param>
        /// <param name="timeout">超时时间，默认为60000毫秒（1分钟）</param>
        /// <returns></returns>
        public static string PostHttpResponse(string url, IDictionary<string, string> parameters, int timeout = 60000)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            request.Timeout = timeout;

            //往头部加入自定义验证信息

            SetHeaderValue(request.Headers, "Authorization", jwttoken);

            //如果需要POST数据
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = Encoding.GetEncoding("utf-8").GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            var resultStream = (request.GetResponse() as HttpWebResponse);
            string result = "";
            //获取响应内容
            using (StreamReader reader = new StreamReader(resultStream.GetResponseStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }


        /// <summary>
        /// post方法（使用该方法请求Api时，Api中参数前需要加[FromBody]）
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="PostData">post数据对象</param>
        /// <param name="timeout">超时时间，默认为60000毫秒（1分钟）</param>
        /// <returns></returns>
        public static string PostHttpResponse(string url, Object PostData, int timeout = 60000)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            request.Timeout = timeout;



            //往头部加入自定义验证信息

            SetHeaderValue(request.Headers, "Authorization", jwttoken);

            //如果需要POST数据
            string str = JsonConvert.SerializeObject(PostData);
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(str);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var resultStream = (request.GetResponse() as HttpWebResponse);
            string result = "";
            //获取响应内容
            using (StreamReader reader = new StreamReader(resultStream.GetResponseStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// 往头部加信息
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
        #endregion -- Https请求 --

        public static string GetRege( this string command, string head, string main, string foot)
        {
            string result = "";
            Regex All = new Regex(head + main + foot);
            foreach (Match First in All.Matches(command))
            {
                result = First.Value;

                Regex rxBegin = new Regex(head);
                foreach (Match mchKeyword in rxBegin.Matches(result))
                {
                    result = result.Substring(result.IndexOf(mchKeyword.Value.Trim()) + mchKeyword.Value.Trim().Length);  //去头
                    break;
                }
                Regex rxEnd = new Regex(foot);
                foreach (Match mchKeyword in rxEnd.Matches(result))
                {
                    result = result.Substring(0, result.Length - mchKeyword.Value.Length);  //去尾
                    break;
                }
                break;
            }
            return result;
        }
    }
}
