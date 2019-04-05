namespace MM.Task
{
    /// <summary>
    /// 任务配置
    /// </summary>
    public class Config : Common.Config
    {
        /// <summary>
        /// 休眠时长(单位：毫秒)
        /// </summary>
        public int Sleep { get; set; } = 60000;

        /// <summary>
        /// 次数, -1为循环执行
        /// </summary>
        public int Times { get; set; } = -1;
    }
}
