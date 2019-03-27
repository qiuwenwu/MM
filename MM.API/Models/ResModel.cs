using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MM.API
{
    /// <summary>
    /// 响应模型
    /// </summary>
    public class ResModel
    {
        /// <summary>
        /// 协议头
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Cookies
        /// </summary>
        public Dictionary<string, CookieModel> Cookies { get; set; } = new Dictionary<string, CookieModel>();
        /// <summary>
        /// 响应状态
        /// </summary>
        public int Status { get; set; } = 0;
        /// <summary>
        /// 主体内容
        /// </summary>
        public string Body { get; set; } = "";
        /// <summary>
        /// 回调地址
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 响应结果
        /// </summary>
        public ApiRetModel Ret { get; set; }

        /// <summary>
        /// 全局变量
        /// </summary>
        public ConcurrentDictionary<string, object> ValDt { get; set; } = new ConcurrentDictionary<string, object>();

        private const string V = "\n\n";
        private string _ContentType = "";
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType
        {
            get { return _ContentType; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _ContentType = "text/plain; charset=utf-8";
                }
                else
                {
                    switch (value)
                    {
                        case "text":
                            _ContentType = "text/plain; charset=utf-8";
                            break;
                        case "html":
                            _ContentType = "text/html; charset=utf-8";
                            break;
                        case "xml":
                            _ContentType = "text/xml; charset=utf-8";
                            break;
                        case "json":
                            _ContentType = "application/json; charset=utf-8";
                            break;
                        default:
                            _ContentType = value;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        /// <param name="expires">有效期，默认为0不限期</param>
        public void SetCookie(string key, string value, int expires = 0)
        {
            if (Cookies.ContainsKey(key))
            {
                if (expires > 0)
                {
                    if (Cookies[key].Options != null)
                    {
                        Cookies[key].Value = value;
                        Cookies[key].Options.Expires = expires;
                    }
                    else
                    {
                        Cookies.Add(key, new CookieModel() { Value = value, Options = new CookieOptionsModel() { Expires = expires } });
                    }
                }
                else
                {
                    Cookies[key].Value = value;
                }
            }
            else if (expires > 0)
            {
                Cookies.Add(key, new CookieModel() { Value = value, Options = new CookieOptionsModel() { Expires = expires } });
            }
            else
            {
                Cookies.Add(key, new CookieModel() { Value = value });
            }
        }

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="key">键名</param>
        public void DelCookie(string key)
        {
            if (Cookies.ContainsKey(key))
            {
                Cookies[key].Value = null;
            }
            else
            {
                Cookies.Add(key, new CookieModel() { Value = null });
            }
        }

        /// <summary>
        /// 设置结果
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="msg">消息类型</param>
        /// <param name="error">错误码</param>
        /// <returns>返回设置后的字符串</returns>
        public string SetRet(object data = null, string msg = "", int error = 0)
        {
            if (Ret == null)
            {
                Ret = new ApiRetModel(error, msg);
            }
            else if (data != null)
            {
                Ret.Result = data;
            }

            string ret = "";
            if (string.IsNullOrEmpty(ContentType))
            {
                ret = Ret.ToJson();
            }
            else
            {
                if (ContentType.Contains("json"))
                {
                    ret = Ret.ToJson();
                }
                else if (ContentType.Contains("xml"))
                {
                    ret = Ret.ToXmlS();
                }
                else
                {
                    var err = Ret.Error;
                    if (err != null)
                    {
                        ret += "error: " + err.Code + V;
                        ret += "msg: " + err.Message + V;
                    }
                    ret += "data: " + Ret.Result;
                }
            }
            return ret;
        }

        #region 全局变量增删改查
        /// <summary>
        /// 获取变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="val">变量值</param>
        /// <returns>返回变量值</returns>
        public object GetVal(string name, object val = null)
        {
            if (ValDt.TryGetValue(name, out object value))
            {
                if (value == null)
                {
                    value = val;
                }
            }
            else
            {
                value = val;
            }
            return value;
        }

        /// <summary>
        /// 修改变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="val">变量值</param>
        /// <returns>修改成功返回true，失败返回false</returns>
        public bool SetVal(string name, object val)
        {
            var obj = ValDt.AddOrUpdate(name, val, (key, value) => val);
            return obj != null;
        }

        /// <summary>
        /// 删除变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool DelVal(string name) => ValDt.TryRemove(name, out _);

        /// <summary>
        /// 判断变量是否存在
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public bool HasVal(string name) => ValDt.ContainsKey(name);
        #endregion
    }
}
