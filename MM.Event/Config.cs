using Newtonsoft.Json;

namespace MM.Event
{
    /// <summary>
    /// 事件配置
    /// </summary>
    public class Config : Common.Config 
    {
        /// <summary>
        /// 缓存时长
        /// </summary>
        [JsonProperty("cache")]
        public int Cache     { get; set; } = 15;

        /// <summary>
        /// 事件阶段
        /// </summary>
        [JsonProperty("stage")]
        public string Stage  { get; set; }

        /// <summary>
        /// 时态
        /// </summary>
        [JsonProperty("tense")]
        public string Tense  { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; set; }

        /// <summary>
        /// 匹配方式
        /// </summary>
        [JsonProperty("mode")]
        public int Mode      { get; set; }

        /// <summary>
        /// 执行顺序
        /// </summary>
        [JsonProperty("order")]
        public int Order     { get; set; }

        /// <summary>
        /// 是否中断执行
        /// </summary>
        [JsonProperty("end")]
        public bool End      { get; set; }
    }
}
