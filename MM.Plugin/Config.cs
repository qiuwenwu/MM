namespace MM.Plugin
{
    public class Config : Common.Config
    {
        /// <summary>
        /// 开关
        /// </summary>
        public bool OnOff { get; set; }

        /// <summary>
        /// 中断执行
        /// </summary>
        public bool End { get; set; }

        /// <summary>
        /// 执行顺序
        /// </summary>
        public int Order { get; set; }
    }
}
