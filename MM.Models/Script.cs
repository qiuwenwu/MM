using Newtonsoft.Json;

namespace MM.Models
{
    /// <summary>
    /// 脚本模型
    /// </summary>
    public class Script
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty("file")]
        public string File { get; set; }

        /// <summary>
        /// 函数名
        /// </summary>
        [JsonProperty("fun")]
        public string Fun  { get; set; }
    }
}
