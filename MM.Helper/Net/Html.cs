using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace MM.Helper.Net
{
    /// <summary>
    /// Html解析帮助类
    /// </summary>
    public class Html : ArticleHelper
    {
        #region 网页正则
        private static readonly string Alist = "<a[\\s\\S]+?href[=\"']([\\s\\S]+?)[\"'\\s+][\\s\\S]+?>([\\s\\S]+?)</a>";

        private static readonly string ImgList = "<img[\\s\\S]*?src=[\"']([\\s\\S]*?)[\"'][\\s\\S]*?>([\\s\\S]*?)>";

        private static readonly string Nscript = "<nscript[\\s\\S]*?>[\\s\\S]*?</nscript>";

        private static readonly string Style = "<style[\\s\\S]*?>[\\s\\S]*?</style>";

        private static readonly string Script = "<script[\\s\\S]*?>[\\s\\S]*?</script>";

        private static readonly string Tag = "<[\\s\\S]*?>";

        private static readonly string NewLine = Environment.NewLine;

        private static readonly string HtmlEnconding = "<meta[^<]*charset=([^<]*)[\"']";

        private static readonly string AllHtml = "([\\s\\S]*?)";

        private static readonly string HtmlTitle = "<title>([\\s\\S]*?)</title>";

        private static readonly string HtmlAuthor = "<meta name=\"author\" content=\"([\\s\\S]*?)\">";

        private static readonly string HtmlDescription = "<meta name=\"description\" content=\"([\\s\\S]*?)\">";

        private static readonly string HtmlKeywords = "<meta name=\"keywords\" content=\"([\\s\\S]*?)\">";
        #endregion


        #region 提取
        /// <summary>
        /// 获取网页中所有A链接
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <param name="baseUrl">基础网址</param>
        /// <returns>返回所有A链接对象模型</returns>
        public List<AItem> GetA(string html, string baseUrl = "")
        {
            List<AItem> list = null;
            if (Regex.IsMatch(html, Alist, RegexOptions.IgnoreCase))
            {
                list = new List<AItem>();
                foreach (Match match in Regex.Matches(html, Alist, RegexOptions.IgnoreCase))
                {
                    AItem aItem = null;
                    if (match.Groups.Count > 2)
                    {
                        aItem = new AItem
                        {
                            Href = baseUrl + match.Groups[1].Value,
                            Text = NoHtml(match.Groups[2].Value),
                            Html = match.Value,
                            Type = AType.Text
                        };
                        List<ImgItem> imgList = GetImg(aItem.Text);
                        if (imgList != null && imgList.Count > 0)
                        {
                            aItem.Type = AType.Img;
                            aItem.Img = imgList[0];
                        }
                        list.Add(aItem);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取网页所有图片
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <param name="baseUrl">基础网址</param>
        /// <returns>返回图片信息模型列表</returns>
        public List<ImgItem> GetImg(string html, string baseUrl = "")
        {
            List<ImgItem> list = null;
            var matchs = Regex.Matches(html, ImgList, RegexOptions.IgnoreCase);
            if (Regex.IsMatch(html, ImgList, RegexOptions.IgnoreCase))
            {
                list = new List<ImgItem>();
                if (string.IsNullOrEmpty(baseUrl))
                {
                    foreach (Match match in matchs)
                    {
                        var groups = match.Groups;
                        if (groups.Count > 1)
                        {
                            ImgItem imgItem = new ImgItem
                            {
                                Src = baseUrl + groups[1].Value,
                                Html = match.Value
                            };
                            list.Add(imgItem);
                        }
                    }
                }
                else
                {
                    foreach (Match match in matchs)
                    {
                        var groups = match.Groups;
                        if (groups.Count > 1)
                        {
                            var val = groups[1].Value;
                            var src = baseUrl + val;
                            ImgItem imgItem = new ImgItem
                            {
                                Src = src,
                                Html = match.Value.Replace(val, src)
                            };
                            list.Add(imgItem);
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取网页标题部分
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <returns>返回网页标题</returns>
        public string Title(string html)
        {
            return GetContent(html, HtmlTitle);
        }

        /// <summary>
        /// 获取网页编码方式
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <returns>返回网页标题</returns>
        public string Enconde(string html)
        {
            return GetContent(html, HtmlEnconding);
        }

        /// <summary>
        /// 获取网页描述
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <returns>返回网页标题</returns>
        public string Description(string html)
        {
            return GetContent(html, HtmlDescription);
        }

        /// <summary>
        /// 获取网页开发者
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <returns>返回网页标题</returns>
        public string Author(string html)
        {
            return GetContent(html, HtmlAuthor);
        }

        /// <summary>
        /// 获取网页关键词
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <returns>返回网页标题</returns>
        public string Keywords(string html)
        {
            return GetContent(html, HtmlKeywords);
        }

        /// <summary>
        /// 取某HTML标签之间能的内容
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <param name="s">开始标签</param>
        /// <param name="e">结束标签</param>
        /// <returns>返回标签之间的内容</returns>
        public string Between(string html, string s, string e)
        {
            string pattern = string.Format("{0}{1}{2}", s, AllHtml, e);
            string result;
            if (Regex.IsMatch(html, pattern, RegexOptions.IgnoreCase))
            {
                Match match = Regex.Match(html, pattern, RegexOptions.IgnoreCase);
                if (match != null && match.Groups.Count > 0)
                {
                    result = match.Groups[1].Value.Trim();
                    return result;
                }
            }
            result = string.Empty;
            return result;
        }

        /// <summary>
        /// 获取网页中某个部分的内容
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>返回网页标题</returns>
        public string GetContent(string html, string regex)
        {
            string result = string.Empty;
            if (Regex.IsMatch(html, regex))
            {
                result = Regex.Match(html, regex).Groups[1].Value.Trim();
            }
            return result;
        }
        #endregion


        #region 创建html
        /// <summary>
        /// 新建链接列表
        /// </summary>
        /// <returns>返回A链接列表</returns>
        public List<AItem> NewAList()
        {
            return new List<AItem>();
        }

        /// <summary>
        /// 新建图片列表
        /// </summary>
        /// <returns>返回A链接列表</returns>
        public List<ImgItem> NewImgList()
        {
            return new List<ImgItem>();
        }
        #endregion


        #region 下载
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="imgSrc">图片</param>
        /// <param name="path">保存路径</param>
        /// <returns>下载图片</returns>
        public bool DownloadImg(string imgSrc, string path)
        {
            WebClient wc = new WebClient();
            var bl = false;
            try
            {
                wc.DownloadFile(imgSrc, path + GetFileName(imgSrc));
                bl = true;
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
            }
            return bl;
        }

        /// <summary>
        /// 下载所有图片
        /// </summary>
        /// <param name="list">图片列表</param>
        /// <param name="path">保存路径</param>
        /// <returns>下载图片</returns>
        public bool DownloadImg(List<ImgItem> list, string path)
        {
            WebClient wc = new WebClient();
            var bl = false;
            try
            {
                foreach (var o in list)
                {
                    var file = o.Src;
                    wc.DownloadFile(file, path + GetFileName(file));
                }
                bl = true;
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
            }
            return bl;
        }

        /// <summary>
        /// 下载所有图片
        /// </summary>
        /// <param name="list">图片列表</param>
        /// <param name="path">保存路径</param>
        /// <param name="url">新的访问地址</param>
        /// <returns>下载图片</returns>
        public List<ImgItem> DownloadImgS(List<ImgItem> list, string path, string url)
        {
            WebClient wc = new WebClient();
            List<ImgItem> lt = null;
            try
            {
                for (var i = 0; i < list.Count; i++)
                {
                    var o = list[i];
                    var file = o.Src;
                    var name = GetFileName(file);
                    if (!string.IsNullOrEmpty(name))
                    {
                        list[i].NewHtml = o.Html.Replace(file, url + name);
                        wc.DownloadFile(file, path + name);
                    }
                }
                lt = list;
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
            }
            return lt;
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="file">获取文件名</param>
        /// <returns>返回文件名</returns>
        public string GetFileName(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                return "";
            }
            var name = "";
            if (file.Contains("."))
            {
                var arr = file.Split('/');
                name = arr[arr.Length - 1];
                if (name.Contains("?"))
                {
                    name = name.Split('?')[0];
                }
            }
            if (string.IsNullOrEmpty(name))
            {
                name = DateTime.Now.Ticks.ToString();
            }
            if (!name.Contains("."))
            {
                var fl = file.ToLower();
                if (fl.Contains("gif"))
                {
                    name += ".gif";
                }
                else if (fl.Contains("png"))
                {
                    name += ".png";
                }
                else if (fl.Contains(".svg"))
                {
                    name += ".png";
                }
                else if (fl.Contains("bmp"))
                {
                    name += ".png";
                }
                else
                {
                    name += ".jpg";
                }
            }
            return name;
        }

        /// <summary>
        /// 获取指定对象文本
        /// </summary>
        /// <param name="html">html内容</param>
        /// <param name="tag">标签</param>
        /// <returns>返回文本列表</returns>
        public List<string> GetTag(string html, string tag)
        {
            var str = string.Format("<{0}.*>(.*)</{0}>", tag);
            return GetMatch(html, str);
        }

        /// <summary>
        /// 取匹配正则的字符串
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="rx">正则表达式</param>
        /// <param name="num">取出前几个</param>
        /// <returns>返回取出的字符串</returns>
        private static List<string> GetMatch(string str, string rx, int num = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new List<string>();
            }
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
        #endregion


        #region 过滤
        /// <summary>
        /// 过滤次要的html
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <returns>返回html</returns>
        public string FilterHtml(string html)
        {
            html = Regex.Replace(html, Nscript, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, Style, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, Script, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            html = Regex.Replace(html, Tag, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return html;
        }

        ///<summary>   
        ///清除HTML标记   
        ///</summary>   
        ///<param name="Htmlstring">包括HTML的源码</param>   
        ///<returns>已经去除后的文字</returns>
        public string NoHtml(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            //删除HTML   
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            Htmlstring = regex.Replace(Htmlstring, "");
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");

            return Htmlstring;
        }

        /// <summary>
        /// 移除html标签
        /// </summary>
        /// <param name="html">html文本</param>
        /// <param name="length">取出长度</param>
        /// <returns>字符串</returns>
        public string ReplaceTag(string html, int length = 0)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }
            try
            {
                string strText = Regex.Replace(html, "<[^>]+>", "");
                strText = Regex.Replace(strText, "&[^;]+;", "");

                if (length > 0 && strText.Length > length)
                {
                    return strText.Substring(0, length);
                }
                else
                {
                    return strText;
                }
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 替换新行
        /// </summary>
        /// <param name="html">网页内容</param>
        /// <returns>返回替换后内容</returns>
        public string ReplaceNewLine(string html)
        {
            return Regex.Replace(html, NewLine, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        #endregion


        #region 私有字段
        private static int delay = 1000;
        #endregion

        #region 公有属性
        /// <summary>
        /// 网络延迟
        /// </summary>
        public static int NetworkDelay
        {
            get
            {
                Random r = new Random();
                return (r.Next(delay, delay * 2));
            }
            set
            {
                delay = value;
            }
        }
        /// <summary>
        /// 最大尝试数
        /// </summary>
        public static int MaxTry { get; set; } = 300;
        #endregion

        #region
        ///<summary>
        ///替换html中的特殊字符
        ///</summary>
        ///<param name="theString">需要进行替换的文本</param>
        ///<returns>替换完的文本。</returns>
        public string Encode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "&apos;");
            theString = theString.Replace("\n", "<br/>");
            return theString;
        }

        ///<summary>
        ///恢复html中的特殊字符
        ///</summary>
        ///<param name="theString">需要恢复的文本</param>
        ///<returns>恢复好的文本。</returns>
        public string Decode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&apos;", "\'");
            theString = theString.Replace("<br/>", "\n");
            return theString;
        }

        /// <summary>
        /// 获取页面的链接正则
        /// </summary>
        public string GetHref(string HtmlCode)
        {
            string MatchVale = "";
            string Reg = @"(h|H)(r|R)(e|E)(f|F) *= *('|"")?((\w|\\|\/|\.|:|-|_)+)[\S]*";
            foreach (Match m in Regex.Matches(HtmlCode, Reg))
            {
                MatchVale += (m.Value).ToLower().Replace("href=", "").Trim() + "|";
            }
            return MatchVale;
        }

        /// <summary>
        /// 匹配页面的图片地址
        /// </summary>
        /// <param name="HtmlCode">html代码</param>
        /// <param name="imgHttp">要补充的http://路径信息</param>
        public string GetImgSrc(string HtmlCode, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"<img.+?>";
            foreach (Match m in Regex.Matches(HtmlCode.ToLower(), Reg))
            {
                MatchVale += GetImg((m.Value).ToLower().Trim(), imgHttp) + "|";
            }

            return MatchVale;
        }

        /// <summary>
        /// 匹配<img src="" />中的图片路径实际链接
        /// </summary>
        /// <param name="ImgString">图片字符串</param>
        /// <param name="imgHttp">图片链接地址。如http://localhost/</param>
        /// <returns>返回完整链接的图片地址</returns>
        public string GetImgOne(string ImgString, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"src=.+\.(bmp|jpg|gif|png)";
            foreach (Match m in Regex.Matches(ImgString.ToLower(), Reg))
            {
                MatchVale += (m.Value).ToLower().Trim().Replace("src=", "");
            }
            if (MatchVale.IndexOf(".net") != -1 || MatchVale.IndexOf(".com") != -1 || MatchVale.IndexOf(".org") != -1 || MatchVale.IndexOf(".cn") != -1 || MatchVale.IndexOf(".cc") != -1 || MatchVale.IndexOf(".info") != -1 || MatchVale.IndexOf(".biz") != -1 || MatchVale.IndexOf(".tv") != -1)
                return (MatchVale);
            else
                return (imgHttp + MatchVale);
        }

        /// <summary>
        /// 压缩HTML输出
        /// </summary>
        public string ZipHtml(string Html)
        {
            Html = Regex.Replace(Html, @">\s+?<", "><");//去除HTML中的空白字符
            Html = Regex.Replace(Html, @"\r\n\s*", "");
            Html = Regex.Replace(Html, @"<body([\s|\S]*?)>([\s|\S]*?)</body>", @"<body$1>$2</body>", RegexOptions.IgnoreCase);
            return Html;
        }

        /// <summary>
        /// 过滤指定HTML标签
        /// </summary>
        /// <param name="s_TextStr">要过滤的字符</param>
        /// <param name="html_Str">a img p div</param>
        public string DelHtml(string s_TextStr, string html_Str)
        {
            string rStr = "";
            if (!string.IsNullOrEmpty(s_TextStr))
            {
                rStr = Regex.Replace(s_TextStr, "<" + html_Str + "[^>]*>", "", RegexOptions.IgnoreCase);
                rStr = Regex.Replace(rStr, "</" + html_Str + ">", "", RegexOptions.IgnoreCase);
            }
            return rStr;
        }
        #endregion
    }

    /// <summary>
    /// 网页A链接模型
    /// </summary>
    public class AItem
    {
        /// <summary>
        /// 链接
        /// </summary>
		public string Href
        {
            get;
            set;
        }

        /// <summary>
        /// 文本内容
        /// </summary>
		public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 图片
        /// </summary>
		public ImgItem Img
        {
            get;
            set;
        }

        /// <summary>
        /// html内容
        /// </summary>
		public string Html
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
		public AType Type
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 图片项
    /// </summary>
    public class ImgItem
    {
        /// <summary>
        /// 图片地址
        /// </summary>
		public string Src
        {
            get;
            set;
        }

        /// <summary>
        /// 图片html
        /// </summary>
		public string Html
        {
            get;
            set;
        }

        /// <summary>
        /// 新图片html
        /// </summary>
        public string NewHtml
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 内容类型
    /// </summary>
    public enum AType
    {
        /// <summary>
        /// 文本
        /// </summary>
		Text,
        /// <summary>
        /// 图片
        /// </summary>
        Img
    }
}
