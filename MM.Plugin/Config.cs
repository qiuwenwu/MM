using Newtonsoft.Json;

namespace MM.Plugin
{
    /// <summary>
    /// 插件配置
    /// </summary>
    public class Config : Common.Config
    {
        /// <summary>
        /// 开关
        /// </summary>
        [JsonProperty("onOff")]
        public bool OnOff   { get; set; } = true;

        /// <summary>
        /// 结束执行
        /// </summary>
        [JsonProperty("finish")]
        public bool Finish  { get; set; } = true;

        /// <summary>
        /// 执行顺序
        /// </summary>
        [JsonProperty("order")]
        public int Order    { get; set; } = 100;

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; } = "End";
    }
}
