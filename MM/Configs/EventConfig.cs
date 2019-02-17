namespace MM.Configs
{
    /// <summary>
    /// 事件配置
    /// </summary>
    public class EventConfig : ComConfig
    {
        /// <summary>
        /// 事件阶段
        /// </summary>
        public string Stage       { get; set; }

        /// <summary>
        /// 时态
        /// </summary>
        public string Tense       { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public string Target      { get; set; }

        /// <summary>
        /// 匹配方式
        /// </summary>
        public int Mode           { get; set; }

        /// <summary>
        /// 是否中断执行
        /// </summary>
        public bool End           { get; set; }
    }
}
