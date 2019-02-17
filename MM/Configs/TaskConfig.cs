namespace MM.Configs
{
    /// <summary>
    /// 任务配置
    /// </summary>
    public class TaskConfig : ComConfig
    {
        /// <summary>
        /// 休眠时长
        /// </summary>
        public int Sleep          { get; set; }

        /// <summary>
        /// 次数, -1为循环执行
        /// </summary>
        public int Times          { get; set; } = -1;
    }
}
