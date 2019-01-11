using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace System
{
    /// <summary>
    /// 字符串拓展函数
    /// </summary>
    public static class StringExpand
    {
        /// <summary>
        /// 取文本左边内容
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="l">被取字符串</param>
        /// <param name="bl">如果没有左边内容，是否返回原有内容</param>
        /// <returns>返回左边内容</returns>
        public static string Left(this string str, string l, bool bl = false)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            try
            {
                int A = str.IndexOf(l);
                if (A < 0)
                {
                    if (bl)
                    {
                        return str;
                    }
                }
                else
                {
                    return str.Substring(0, A);
                }
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        /// <summary>
        /// 取文本右边内容
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="r">索引字符串</param>
        /// <param name="bl">如果没有右边内容，是否返回原有内容</param>
        /// <returns>返回右边内容</returns>
        public static string Right(this string str, string r, bool bl = false)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            try
            {
                int B = str.IndexOf(r);
                if (B < 0)
                {
                    if (bl)
                    {
                        return str;
                    }
                }
                else
                {
                    var index = B + r.Length;
                    return str.Substring(index, str.Length - index);
                }
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        /// <summary>
        /// 取文本中间内容
        /// </summary>
        /// <param name="str">原文本</param>
        /// <param name="l">左边文本</param>
        /// <param name="r">右边文本</param>
        /// <param name="bl">如果没有之间的内容，是否返回原有内容</param>
        /// <returns>返回中间文本内容</returns>
        public static string Between(this string str, string l, string r, bool bl = false)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            try
            {
                str = str.Right(l, bl);
                str = str.Left(r, bl);
            }
            catch (Exception)
            {
            }
            return str;
        }

        /// <summary>
        /// 转为json对象
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回json对象</returns>
        public static JObject ToJobj(this string str)
        {
            JObject ret = null;
            try
            {
                ret = JObject.Parse(str);
            }
            catch (Exception)
            {
            }
            return ret;
        }

        /// <summary>
        /// 返序列化
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>返回模型</returns>
        public static T Loads<T>(this string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 返序列化
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>返回模型</returns>
        public static object Loads(this string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject(jsonStr);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 转为xml字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回xml字符串</returns>
        public static string ToXml(this object obj)
        {
            string str = "";
            Type type = obj.GetType();
            MemoryStream stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces(new[] { Xml.XmlQualifiedName.Empty });
                ns.Add("", "");  //清除前缀和命名空间
                xml.Serialize(stream, obj, ns);
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                str = sr.ReadToEnd();
                sr.Dispose();
                stream.Dispose();
            }
            catch (Exception)
            {
            }
            return str;
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="oldStr">需替换字符串</param>
        /// <param name="newStr">替换用的字符串 </param>
        /// <param name="num">替换次数</param>
        /// <param name="idx">替换的起始位置</param>
        /// <returns>返回替换后的字符串</returns>
        public static string Replace(this string str, string oldStr, string newStr, int num, int idx = 1)
        {
            var s = "|||" + str.Replace(oldStr, "^");
            var array = s.Split('^');
            if (num == 0 || num > array.Length)
            {
                num = array.Length;
            }
            var index = 0;
            var i = 1;
            string ret = string.Empty;
            foreach (var o in array)
            {
                if (index >= idx && i <= num)
                {
                    if (string.IsNullOrEmpty(ret))
                    {
                        ret += o;
                    }
                    else
                    {
                        ret += newStr + o;
                    }
                    i++;
                }
                else
                {
                    if (string.IsNullOrEmpty(ret))
                    {
                        ret += o;
                    }
                    else
                    {
                        ret += oldStr + o;
                    }
                }
                index++;
            }
            return ret.TrimStart('|');
        }

        /// <summary>
        /// 替换掉所有字符
        /// </summary>
        /// <param name="str">被替换字符串</param>
        /// <param name="newStr">用作替换的字符串</param>
        /// <returns>替换成功返回替换后字符串</returns>
        public static string ReplaceSymbol(this string str, string newStr)
        {
            var list = new List<string>() { "【", "】", "[", "]", "^", "{", "}", "(", ")", "|", "~", "@", "#", "$", "%", "&", "*", ":", "<", ">", "?", "`", "-", "=", ";", "'", ",", ".", "/", "\\", "￥", "（", "）", "—", "+", "：", "；", "“", "”", "‘", "’", "、", "《", "》", "，", "。", "？", "!", "！" };
            foreach (var w in list)
            {
                str = str.Replace(w, newStr);
            }
            return Regex.Replace(str, @"((?=[\x21-\x7e]+)[^A-Za-z0-9])", newStr);
        }

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="rx">正则</param>
        /// <param name="newStr">替换后的字符</param>
        /// <returns>返回替换后的字符串</returns>
        public static string ReplaceRx(this string str, string rx, string newStr)
        {
            return Regex.Replace(str, rx, newStr);
        }

        /// <summary>
        /// 过滤不可见字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回新字符串</returns>
        public static string ReplaceNull(this string str)
        {
            return str.ReplaceRx("\\s", "");
        }

        /// <summary>
        /// 转为激活码
        /// </summary>
        /// <param name="str">被转换的字符串</param>
        /// <param name="length">分割长度</param>
        /// <param name="strat">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns>返回激活码格式字符串</returns>
        public static string ToPIN(this string str, int length, int strat, int end)
        {
            string result = Regex.Replace(str, @"(\w{" + length + "})", "$1-").Trim('-');
            if (result.Length > end && strat >= 0 && end - strat >= length)
            {
                result = result.Substring(strat, end);    //搜索从0开始,截取N位
            }
            return result;
        }

        /// <summary>
        /// 分割文本
        /// </summary>
        /// <param name="str">被分割的字符串</param>
        /// <param name="symbol">分隔符</param>
        /// <returns>返回分割后的字符串数组</returns>
        public static List<string> Split(this string str, string symbol)
        {
            return str.Split(symbol.ToCharArray()).ToList();
        }

        /// <summary>
        /// 获取字符
        /// </summary>
        /// <param name="str">被取的字符串</param>
        /// <param name="rx">正则数量</param>
        /// <param name="num">取出的数量</param>
        /// <returns>返回获取的列表</returns>
        public static List<string> Get(this string str, string rx, int num = 0) {
            var match = Regex.Matches(str, rx);
            List<string> list = new List<string>();
            if (match.Count < num || num < 1)
            {
                num = match.Count;
            }
            for (int i = 0; i < num; i++)
            {
                list.Add(match[i].Value);
            }
            return list;
        }

        /// <summary>
        /// 获取数字
        /// </summary>
        /// <param name="str">被取的字符串</param>
        /// <param name="num">取出的数量</param>
        /// <returns>返回获取的列表</returns>
        public static List<int> GetNum(this string str, int num = 0)
        {
            var match = Regex.Matches(str, "[0-9]+");
            List<int> list = new List<int>();
            if (match.Count < num || num < 1)
            {
                num = match.Count;
            }
            for (int i = 0; i < num; i++)
            {
                list.Add(int.Parse(match[i].Value));
            }
            return list;
        }

        /// <summary>
        /// 取英文
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="num">取出前几个</param>
        /// <returns>返回取出的字符串</returns>
        public static List<string> GetEn(this string str, int num = 0)
        {
            return str.Get("[a-zA-Z]+", num);
        }

        /// <summary>
        /// 取汉字
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="num">取出前几个</param>
        /// <returns>返回取出的字符串</returns>
        public static List<string> GetCh(this string str, int num = 0)
        {
            return str.Get("[\u4e00-\u9fa5]+", num);
        }

        /// <summary>
        /// 是否匹配正则
        /// </summary>
        /// <param name="str">被匹配的字符串</param>
        /// <param name="rx">正则表达式 </param>
        /// <returns>匹配返回true，不匹配返回false</returns>
        public static bool IsMatch(this string str, string rx)
        {
            return Regex.IsMatch(str, rx);
        }

        /// <summary>
        /// 是否数字英文
        /// </summary>
        /// <param name="str">判断的字符串</param>
        /// <returns>是则返回true，否则返回false</returns>
        public static bool IsNumOrEn(this string str)
        {
            return Regex.IsMatch(str, "^[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是则返回true，否则返回false</returns>
        public static bool IsNum(this string str)
        {
            return Regex.IsMatch(str, "^[0-9]+$");
        }

        /// <summary>
        /// 判断是否英文
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是则返回true，否则返回false</returns>
        public static bool IsEn(this string str)
        {
            return Regex.IsMatch(str, "^[a-zA-Z]+$");
        }

        /// <summary>
        /// 判断是否为中文
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是中文返回true，不是返回false</returns>
        public static bool IsCh(this string str)
        {
            return Regex.IsMatch(str, @"^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 取匹配正则的字符串
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="rx">正则表达式</param>
        /// <param name="num">取出前几个</param>
        /// <returns>返回取出的字符串</returns>
        public static List<string> Match(this string str, string rx, int num = 0)
        {
            var match = Regex.Matches(str, rx, RegexOptions.IgnoreCase);
            List<string> list = new List<string>();
            if (match.Count < num || num < 1)
            {
                num = match.Count;
            }
            foreach (Match m in match)
            {
                list.Add(m.Value);
            }
            return list;
        }

        /// <summary>
        /// 判断是否含数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>含数字返回true，不是返回false</returns>
        public static bool HasNum(this string str)
        {
            return Regex.IsMatch(str, @"[0-9]");
        }

        /// <summary>
        /// 判断是否含英文
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>含英文返回true，不是返回false</returns>
        public static bool HasEn(this string str)
        {
            return Regex.IsMatch(str, "[a-zA-Z]");
        }

        /// <summary>
        /// 判断是否含中文
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>含中文返回true，不是返回false</returns>
        public static bool HasCh(this string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 取匹配正则的字符串
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="rx">正则表达式</param>
        /// <returns>返回取出的字符串</returns>
        public static string Rx(this string str, string rx)
        {
            var ret = "";
            var match = Regex.Match(str, rx);
            if (match.Success)
            {
                if (match.Groups.Count > 1 && rx.Contains("("))
                {
                    ret = match.Groups[1].Value;
                }
                else if (match.Groups.Count > 0)
                {
                    ret = match.Groups[0].Value;
                }
            }
            return ret;
        }

        /// <summary>
        /// 取相似度
        /// </summary>
        /// <param name="text1">文本1</param>
        /// <param name="text2">文本2</param>
        /// <returns>返回相似值0-100</returns>
        public static int As2(this string text1, string text2)
        {
            double num = text1.As(text2) * 100;
            return (int)num;
        }

        /// <summary>
        /// 获取两个字符串的相似度
        /// </summary>
        /// <param name="text1">第一个字符串</param>
        /// <param name="text2">第二个字符串</param>
        /// <returns>返回双精度相似值</returns>
        public static double As(this string text1, string text2)
        {
            double Kq = 2;
            double Kr = 1;
            double Ks = 1;

            char[] ss = text1.ToCharArray();
            char[] st = text2.ToCharArray();

            //获取交集数量
            int q = ss.Intersect(st).Count();
            int s = ss.Length - q;
            int r = st.Length - q;

            return Kq * q / (Kq * q + Kr * r + Ks * s);
        }
    }
}
