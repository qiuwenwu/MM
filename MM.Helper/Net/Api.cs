using Newtonsoft.Json;
using System;

namespace MM.Helper.Net
{
    /// <summary>
    /// 接口请求
    /// </summary>
    public class Api : Https
    {
        /// <summary>
        /// 接口请求主机地址
        /// </summary>
        public string Host { get; set; } = "http://localhost:8001/api/";

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="path">请求路径</param>
        /// <param name="param">请求参数</param>
        /// <returns>返回请求结果</returns>
        public T PostApi<T>(string path, object param)
        {
            string pm = null;
            if (param != null)
            {
                if (param is string)
                {
                    pm = (string)param;
                }
                else
                {
                    pm = param.ToJson();
                }
            }
            var html = Post(path, pm);
            if (string.IsNullOrEmpty(html))
            {
                return default(T);
            }
            else
            {
                return html.Loads<T>();
            }
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="param">参数</param>
        /// <returns>返回请求结果</returns>
        public T GetApi<T>(string path, object param = null)
        {
            var html = "";
            if (param == null)
            {
                html = Get(path);
            }
            else
            {
                string query = "";
                var type = param.GetType();
                if (type.Name == "String")
                {
                    query = (string)param;
                }
                else
                {
                    foreach (var o in type.GetProperties())
                    {
                        var value = o.GetValue(param);
                        if (value != null)
                        {
                            var val = value.ToString();
                            if (val != "")
                            {
                                var key = o.Name;
                                // Console.WriteLine(key + " = " + val);
                                query += string.Format("&{0}={1}", key, UrlEncode(val));
                            }
                        }
                    }
                    if (query.StartsWith("&"))
                    {
                        query = query.Substring(1);
                    }
                }
                html = Get(path + "?" + query);
            }
            if (string.IsNullOrEmpty(html))
            {
                return default(T);
            }
            else
            {
                return html.Loads<T>();
            }
        }

        /// <summary>
        /// post通用接口
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="param">参数</param>
        /// <returns>返回响应结果</returns>
        public ResModel HttpPost(string path, object param)
        {
            var ret = PostApi<ResModel>(Host + path, param);
            if (ret == null)
            {
                ret = new ResModel() { Error = new ErrorModel() };
            }
            return ret;
        }

        /// <summary>
        /// get通用接口
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="param">参数</param>
        /// <returns>返回响应结果</returns>
        public ResModel HttpGet(string path, object param = null)
        {
            var ret = GetApi<ResModel>(Host + path, param);
            if (ret == null)
            {
                ret = new ResModel() { Error = new ErrorModel() };
            }
            return ret;
        }

        /// <summary>
        /// Rpc请求
        /// </summary>
        /// <param name="method">方法</param>
        /// <param name="param">参数</param>
        /// <param name="id">序号</param>
        /// <returns>返回响应结果模型</returns>
        public ResModel Rpc(string method, object param, string id = "")
        {
            var ret = PostApi<ResModel>(Host, new ReqModel() { ID = id, Method = method, Params = param });
            if (ret == null)
            {
                ret = new ResModel() { Error = new ErrorModel() };
            }
            return ret;
        }
    }

    /// <summary>
    /// Rpc请求模型
    /// </summary>
    public class ReqModel
    {
        /// <summary>
        /// rpc版本
        /// </summary>
        [JsonProperty("jsonrpc")]
        public string JsonRPC { get; set; }

        /// <summary>
        /// 请求序号
        /// </summary>
        [JsonProperty("id")]
        public string ID      { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [JsonProperty("method")]
        public string Method  { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [JsonProperty("params")]
        public object Params  { get; set; }
    }

    /// <summary>
    /// 响应模型
    /// </summary>
    public class ResModel
    {
        /// <summary>
        /// rpc版本
        /// </summary>
        [JsonProperty("jsonrpc")]
        public string JsonRPC   { get; set; }

        /// <summary>
        /// 请求序号
        /// </summary>
        [JsonProperty("id")]
        public string ID        { get; set; }

        /// <summary>
        /// 响应结果
        /// </summary>
        [JsonProperty("result")]
        public object Result    { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("error")]
        public ErrorModel Error { get; set; } 
    }

    /// <summary>
    /// 错误模型
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("code")]
        public int Code       { get; set; } = 10000;

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = "服务端业务逻辑错误";
    }
}
