using MM.Helper.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace MM.API
{
    /// <summary>
    /// 请求模型
    /// </summary>
    public class ReqModel
    {
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 内容长度
        /// </summary>
        public long ContentLength { get; set; }
        /// <summary>
        /// 基础路径
        /// </summary>
        public string PathBase { get; set; }
        /// <summary>
        /// 是否https
        /// </summary>
        public bool IsHttps { get; set; }
        /// <summary>
        /// 作用域
        /// </summary>
        public string Scheme { get; set; }
        /// <summary>
        /// 请求方式post或get
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 请求的路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 主机地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 请求来源IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 请求来的内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 请求参数（字符串）
        /// </summary>
        public string QueryString { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public Dictionary<string, string> Query { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 表单参数
        /// </summary>
        public Dictionary<string, string> Form { get; set; }
        /// <summary>
        /// 协议头
        /// </summary>
        public Dictionary<string, string> Header { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Cookies
        /// </summary>
        public Dictionary<string, string> Cookies { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 请求的文件
        /// </summary>
        public List<FormFileModel> Files { get; set; }
        /// <summary>
        /// 请求的端口
        /// </summary>
        public int Port { get; set; } = 80;
        /// <summary>
        /// 请求的参数（整合）
        /// </summary>
        public Dictionary<string, object> Param { get; set; }

        /// <summary>
        /// 身份验证
        /// </summary>
        public bool Auth { get; set; } = false;
        /// <summary>
        /// 是否具有权限
        /// </summary>
        public bool Power { get; set; } = true;
        /// <summary>
        /// 规则
        /// </summary>
        public bool Rules { get; set; } = true;
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 有类型内容为表单的
        /// </summary>
        public bool HasFormContentType { get; set; } = false;

        public Xml Xml { get; set; } = new Xml();

        /// <summary>
        /// 获取所有参数
        /// </summary>
        /// <returns>返回参数字典</returns>
        public Dictionary<string, object> GetParam()
        {
            if (Param == null)
            {
                Param = SetParam();
            }
            return Param;
        }

        private Dictionary<string, object> SetParam()
        {
            var dt = new Dictionary<string, object>();
            var str = Body.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                if (str.IndexOf("<") == 0 && str.IndexOf("</") != -1)
                {
                    var xml = Xml.ToXml(str);
                    var jObj = xml.ToJObj();
                    foreach (var o in jObj)
                    {
                        var key = o.Key.ToLower();
                        dt.Remove(key);
                        dt.Add(key, o.Value);
                    }
                }
                else if ((str.StartsWith("{") && str.EndsWith("}")) || (str.StartsWith("[") && str.EndsWith("]")))
                {
                    var jObj = str.ToJObj();
                    if (jObj != null)
                    {
                        foreach (var o in jObj)
                        {
                            var key = o.Key.ToLower();
                            dt.Remove(key);
                            dt.Add(key, o.Value);
                        }
                    }
                }
                else if (str.IndexOf("&") != -1)
                {
                    var jObj = str.UrlToJson();
                    foreach (var o in jObj)
                    {
                        var key = o.Key.ToLower();
                        dt.Remove(key);
                        dt.Add(key, o.Value.ToString());
                    }
                }
            }
            if (Form != null)
            {
                foreach (var o in Form)
                {
                    var key = o.Key.ToLower();
                    dt.Remove(key);
                    dt.Add(key, o.Value);
                }
            }
            if (Query != null)
            {
                foreach (var o in Query)
                {
                    var key = o.Key.ToLower();
                    dt.Remove(key);
                    dt.Add(key, o.Value);
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取所有参数（字符串）
        /// </summary>
        /// <returns>返回参数字典</returns>
        public string GetParamStr()
        {
            return GetParam().ToJson();
        }
    }
}
