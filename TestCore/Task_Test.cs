using MM.Task;
using System;

namespace TestCore
{
    /// <summary>
    /// 测试任务
    /// </summary>
    public class Task_Test
    {
        /// <summary>
        /// 运行脚本
        /// </summary>
        public static void Run() {
            var indexer = new Indexer();
            //Indexer.Demo();
            var drive = indexer.Drive();
            drive.Update();
            Console.WriteLine(drive.Dict.ToJson());
        }
    }
}
