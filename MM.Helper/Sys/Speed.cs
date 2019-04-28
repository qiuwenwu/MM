using System;
using System.Diagnostics;

namespace MM.Helper.Sys
{
    /// <summary>
    /// 性能测试帮助类
    /// </summary>
    public class Speed
    {
        static readonly Stopwatch sw = new Stopwatch();

        /// <summary>
        /// 重新启动
        /// </summary>
        public void Start()
        {
            sw.Restart();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop(string name = "")
        {
            sw.Stop();
            Console.WriteLine(name + sw.Elapsed.TotalMilliseconds + "ms");
            sw.Restart();
        }

        /// <summary>
        /// 结束
        /// </summary>
        public void End(string name = "")
        {
            sw.Stop();
            Console.WriteLine(name + sw.Elapsed.TotalMilliseconds + "ms");
        }
    }
}
