using System;
using System.Timers;

namespace MM.Task
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
        /// 初始化
        /// </summary>
        public void Init() {
            Times_cache = Times;
            Timer = new Timer(Sleep);
            Timer.Elapsed += new ElapsedEventHandler(Run);
            Timer.AutoReset = true;    //false是执行一次，true是一直执行
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sender">发送参数</param>
        /// <param name="e">事件参数</param>
        private void Run(object sender, ElapsedEventArgs e)
        {
            if (Times_cache == 0)
            {
                End();
                return;
            }
            Script.Run("", "", "");
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
