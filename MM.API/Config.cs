using MM.Helper.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MM.API
{
    /// <summary>
    /// API接口配置
    /// </summary>
    public class Config : Common.Config
    {
        #region 常用属性
        /// <summary>
        /// 接口路径
        /// </summary>
        [JsonProperty("path")]
        public string Path        { get; set; } = "/api";

        /// <summary>
        /// 传值方式
        /// </summary>
        [JsonProperty("method")]
        public string Method      { get; set; } = "get";

        /// <summary>
        /// 开发状态
        /// </summary>
        [JsonProperty("state")]
        public bool State         { get; set; } = true;

        /// <summary>
        /// 响应内容类型 application/json;charset=utf-8、 text/xml;charset=utf-8、 text/plain;charset=utf-8
        /// </summary>
        [JsonProperty("contentType")]
        public string ContentType { get; set; } = "application/json;charset=utf-8";
        #endregion

        /// <summary>
        /// 使用权限
        /// </summary>
        [JsonProperty("power")]
        public Power Power        { get; set; } = new Power();

        /// <summary>
        /// 参数
        /// </summary>
        [JsonProperty("paramDict")]
        public Dictionary<string, ParamModel> ParamDict = new Dictionary<string, ParamModel>();
    }
}
