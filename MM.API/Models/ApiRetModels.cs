using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace MM.API
{
    /// <summary>
    /// 返回结果模型
    /// </summary>
    [XmlRoot("xml")]
    [Serializable]
    public class ApiRetModel
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElement("error")]
        [JsonProperty("error")]
        public ErrorModel Error { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        [XmlElement("data")]
        [JsonProperty("data")]
        public object Result { get; set; } = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiRetModel() {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiRetModel(int code, string message)
        {
            Error = new ErrorModel() { Code = code, Message = message };
        }
    }


    /// <summary>
    /// 错误模型
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        [XmlElement("code")]
        [JsonProperty("code")]
        public int Code { get; set; } = 100;

        /// <summary>
        /// 错误提示
        /// </summary>
        [XmlElement("message")]
        [JsonProperty("message")]
        public string Message { get; set; } = null;
    }
}
