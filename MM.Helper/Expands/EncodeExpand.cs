using System.Text;
using System.Web;

namespace System
{
    /// <summary>
    /// 字符串拓展函数
    /// </summary>
    public static class EncodeExpand
    {
        #region web类
        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="str">被编码的字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string UrlEncode(this string str) {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="str">被解码的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str, Encoding.Default);
        }

        /// <summary>
        /// Html编码
        /// </summary>
        /// <param name="str">被编码的字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string HtmlEncode(this string str)
        {
            str = str.Replace(">", "&gt;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("\'", "&apos;");
            str = str.Replace("\n", "<br/>");
            return str;
        }

        /// <summary>
        /// Html解码
        /// </summary>
        /// <param name="str">被解码的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string HtmlDecode(this string str)
        {
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            str = str.Replace("&apos;", "\'");
            str = str.Replace("<br/>", "\n");
            return str;
        }

        /// <summary>
        /// Utf8编码
        /// </summary>
        /// <param name="str">被编码的字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string Utf8Encode(this string str)
        {
            return str.ToEncode(Encoding.UTF8, Encoding.Default);
        }

        /// <summary>
        /// Utf8解码
        /// </summary>
        /// <param name="str">被解码的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string Utf8Decode(this string str)
        {
            return str.ToEncode(Encoding.Default, Encoding.UTF8);
        }
        #endregion


        #region 文件类
        /// <summary>
        /// Unicode编码
        /// </summary>
        /// <param name="str">被编码的字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string UnicodeEncode(this string str)
        {
            return str.ToEncode(Encoding.Unicode, Encoding.Default);
        }

        /// <summary>
        /// Unicode解码
        /// </summary>
        /// <param name="str">被解码的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string UnicodeDecode(this string str)
        {
            return str.ToEncode(Encoding.Default, Encoding.Unicode);
        }

        /// <summary>
        /// ASCII编码
        /// </summary>
        /// <param name="str">被编码的字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string AsciiEncode(this string str)
        {
            return str.ToEncode(Encoding.ASCII, Encoding.Default);
        }

        /// <summary>
        /// ASCII解码
        /// </summary>
        /// <param name="str">被解码的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string AsciiDecode(this string str)
        {
            return str.ToEncode(Encoding.Default, Encoding.ASCII);
        }
       
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="str">被编码的字符串</param>
        /// <returns>返回编码后的字符串</returns>
        public static string Base64Encode(this string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        ///  Base64解码
        /// </summary>
        /// <param name="str">被解码的字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string Base64Decode(this string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.Default.GetString(bytes);
        }
        #endregion

        /// <summary>
        /// 转换编码方式
        /// </summary>
        /// <param name="str">被转码的字符串</param>
        /// <param name="to_encoding">转换后的编码方式</param>
        /// <param name="from_encoding">当前的编码方式</param>
        /// <returns>转码后的字符串</returns>
        public static string ToEncode(this string str, string to_encoding = "gb2312", string from_encoding = "utf8")
        {
            return str.ToEncode(Encoding.GetEncoding(to_encoding), Encoding.GetEncoding(from_encoding));
        }

        /// <summary>
        /// 转换编码方式
        /// </summary>
        /// <param name="str">被转码的字符串</param>
        /// <param name="to_encoding">转换后的编码方式</param>
        /// <param name="from_encoding">当前的编码方式</param>
        /// <returns>转码后的字符串</returns>
        public static string ToEncode(this string str, Encoding to_encoding, Encoding from_encoding)
        {
            byte[] by = from_encoding.GetBytes(str);
            by = Encoding.Convert(from_encoding, to_encoding, by);
            return to_encoding.GetString(by);
        }
    }
}
