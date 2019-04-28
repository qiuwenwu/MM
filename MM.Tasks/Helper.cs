using System;
using System.Timers;

namespace MM.Tasks
{
    /// <summary>
    /// 任务帮助类
    /// </summary>
    public class Helper : Config
    {
        /// <summary>
        /// 计时器
        /// </summary>
        private Timer Timer { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int Times_cache { get; set; }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void Run() {
            if (Timer == null)
            {
                Init();
            }
            Start();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init() {
            Times_cache = Times;
            Timer = new Timer(Sleep);
            if (TimeList == null || TimeList.Count == 0)
            {
                Timer.Elapsed += new ElapsedEventHandler(Run_interval);
            }
            else
            {
                Timer.Elapsed += new ElapsedEventHandler(Run_time);
            }
            Timer.AutoReset = true;    // false是执行一次，true是一直执行
        }


        /// <summary>
        /// 时间段执行
        /// </summary>
        /// <param name="sender">发送参数</param>
        /// <param name="e">事件参数</param>
        private void Run_time(object sender, ElapsedEventArgs e)
        {
            if (Times_cache == 0)
            {
                End();
                return;
            }
            var timeStr = DateTime.Now.ToString(TimeFormat);
            foreach (var t in TimeList) {
                if (timeStr == t)
                {
                    Script.Run("", "", "", "");
                    Times_cache--;
                }
            }
        }

        /// <summary>
        /// 时间间隔执行
        /// </summary>
        /// <param name="sender">发送参数</param>
        /// <param name="e">事件参数</param>
        private void Run_interval(object sender, ElapsedEventArgs e)
        {
            if (Times_cache == 0)
            {
                End();
                return;
            }
            Script.Run("", "", "", "");
            Times_cache--;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset() {
            End();
            Times_cache = Times;
            Init();
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (Timer != null)
            {
                Timer.Start();
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (Timer != null)
            {
                Timer.Stop();
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        public void End()
        {
            if (Timer != null)
            {
                Script.Unload();
                Timer.Close();
                Timer.Dispose();
                Timer = null;
            }
        }
    }
}
