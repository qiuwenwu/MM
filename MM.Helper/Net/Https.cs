using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace MM.Helper.Net
{
    /// <summary>
    /// Http请求帮助类
    /// </summary>
    public class Https
    {
        /// <summary>
        /// 协议头
        /// </summary>
        public Dictionary<string, object> Headers = new Dictionary<string, object>();

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="query">参数</param>
        /// <param name="encode">编码方式</param>
        /// <returns>返回编码后的字符串</returns>
        public string UrlEncode(string query, string encode = null)
        {
            var code = Encoding.Default;
            if (!string.IsNullOrEmpty(encode))
            {
                code = Encoding.GetEncoding(encode);
            }
            return HttpUtility.UrlEncode(query, code);
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="query">参数</param>
        /// <param name="encode">解码方式</param>
        /// <returns>返回解码后的字符串</returns>
        public string UrlDecode(string query, string encode = null)
        {
            var code = Encoding.Default;
            if (!string.IsNullOrEmpty(encode))
            {
                code = Encoding.GetEncoding(encode);
            }
            return HttpUtility.UrlDecode(query, code);
        }

        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="method">方法</param>
        /// <param name="url">请求网址</param>
        /// <param name="query">url参数</param>
        /// <param name="body">post请求参数</param>
        /// <param name="contentType">请求内容类型</param>
        /// <returns>返回请求结果</returns>
        public string Http(string method, string url, string query, string body = "", string contentType = "application/json; charset=UTF-8")
        {
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (0 < query.Length)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + query;
                }
                else
                {
                    url = url + "?" + query;
                }
            }

            if (url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = method;

            if (Headers.Count > 0)
            {
                foreach (var o in Headers)
                {
                    httpRequest.Headers.Add(o.Key, o.Value.ToString());
                }
            }

            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = contentType;
            if (0 < body.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(body);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            //Console.WriteLine(httpResponse.StatusCode);
            //Console.WriteLine(httpResponse.Method);
            //Console.WriteLine(httpResponse.Headers);
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            var ret = reader.ReadToEnd();
            //Console.WriteLine(ret);
            return ret;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="body">post主体参数</param>
        /// <param name="contentType">请求内容类型</param>
        /// <returns>返回请求结果</returns>
        public string Post(string url, string body, string contentType = "application/json; charset=UTF-8")
        {
            return Http("POST", url, "", body, contentType);
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求网址</param>
        /// <param name="query">url参数</param>
        /// <param name="contentType">请求内容类型</param>
        /// <returns>返回请求结果</returns>
        public string Get(string url, string query = "", string contentType = "text/plain; charset=UTF-8")
        {
            return Http("GET", url, query, "", contentType);
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="pms">参数</param>
        /// <returns>返回格式化后的字符串</returns>
        public string Format(string str, params object[] pms)
        {
            var list = new List<object>();
            foreach (var o in pms)
            {
                if (o == null)
                {
                    list.Add(o);
                }
                else
                {
                    list.Add(UrlEncode(o.ToString()));
                }
            }
            return string.Format(str, list.ToArray());
        }
    }
}
