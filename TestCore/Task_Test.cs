using MM.Tasks;
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
            // 1.构建索引器
            var indexer = new Indexer();
            // 2.实例化驱动
            var drive = indexer.Drive();

            // 3.更新配置和脚本
            drive.Update();
            Console.WriteLine(drive.Dict.ToJson());

            // 4.执行驱动
            drive.Start();
        }
    }
}
