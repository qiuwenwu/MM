using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Xml.Serialization;

namespace System
{
    /// <summary>
    /// 字符串拓展函数
    /// </summary>
    public static class StringExpand
    {
        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="oldStr">需替换字符串</param>
        /// <param name="newStr">替换用的字符串 </param>
        /// <param name="num">替换次数</param>
        /// <param name="ix">替换的起始位置</param>
        /// <returns>返回替换后的字符串</returns>
        public static string Replace(this string str, string oldStr, string newStr, int num, int ix = 1)
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
                if (index >= ix && i <= num)
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
    }
}
