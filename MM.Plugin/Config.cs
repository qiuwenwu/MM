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
        public bool OnOff { get; set; }

        /// <summary>
        /// 执行顺序
        /// </summary>
        [JsonProperty("order")]
        public int Order  { get; set; }

        /// <summary>
        /// 是否中断执行
        /// </summary>
        [JsonProperty("end")]
        public bool End   { get; set; }
    }
}
