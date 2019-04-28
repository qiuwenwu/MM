using Newtonsoft.Json;
using System.Collections.Generic;

namespace MM.Tasks
{
    /// <summary>
    /// 任务配置
    /// </summary>
    public class Config : Common.Config
    {
        /// <summary>
        /// 休眠时长(单位：毫秒)
        /// </summary>
        [JsonProperty("sleep")]
        public int Sleep { get; set; } = 60000;

        /// <summary>
        /// 次数, -1为循环执行
        /// </summary>
        [JsonProperty("times")]
        public int Times { get; set; } = -1;

        /// <summary>
        /// 时间格式
        /// </summary>
        [JsonProperty("timeFormat")]
        public string TimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 时间段
        /// </summary>
        [JsonProperty("timeList")]
        public List<string> TimeList { get; set; }

    }
}
