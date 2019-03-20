using System.Threading;

namespace MM.Task
{
    /// <summary>
    /// 任务配置
    /// </summary>
    public class Config : Common.Config
    {
        /// <summary>
        /// 休眠时长
        /// </summary>
        public int Sleep { get; set; }

        /// <summary>
        /// 次数, -1为循环执行
        /// </summary>
        public int Times { get; set; } = -1;

        /// <summary>
        /// 计时器
        /// </summary>
        public Timer Timer { get; set; }
    }
}
