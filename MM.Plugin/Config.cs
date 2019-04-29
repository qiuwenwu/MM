using Newtonsoft.Json;

namespace MM.Plugin
{
    /// <summary>
    /// 插件配置
    /// </summary>
    public class Config : Common.Config
    {
        /// <summary>
        /// 结束执行
        /// </summary>
        [JsonProperty("finish")]
        public bool Finish  { get; set; } = true;
    }

    /// <summary>
    /// 配置缓存
    /// </summary>
    public class Config_cache {
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; } = "End";

        /// <summary>
        /// 执行顺序
        /// </summary>
        [JsonProperty("orderBy")]
        public int OrderBy { get; set; } = 100;
    }
}
