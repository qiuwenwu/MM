using System;
using MM.Plugin;

namespace TestCore
{
    /// <summary>
    /// 插件测试
    /// </summary>
    public class Plugin_test
    {
        /// <summary>
        /// 运行脚本
        /// </summary>
        public static void Run()
        {
            // 1.构建索引器
            var indexer = new Indexer();
            // 2.实例化驱动
            var drive = indexer.Drive();

            // 3.更新配置和脚本
            drive.Initialize();
            Console.WriteLine(drive.Dict.ToJson());

            // 4.执行驱动
            var ret = drive.Install("demo", "0"); // 启动插件
            Console.WriteLine(ret);

            ret = drive.Init("demo", "1"); // 初始化插件
            Console.WriteLine(ret);

            ret = drive.Start("demo", "1"); // 启动插件
            Console.WriteLine(ret);

            ret = drive.Run("启动时，测试执行内容", "");
            Console.WriteLine(ret);

            ret = drive.Stop("demo", "2"); // 暂停插件
            Console.WriteLine(ret);
            ret = drive.Run("暂停时，测试执行内容", "");
            Console.WriteLine(ret);

            ret = drive.Update("demo", "2"); // 结束前，更新插件
            Console.WriteLine(ret);

            ret = drive.Uninstall("demo", "2"); // 结束前，卸载插件
            Console.WriteLine(ret);

            ret = drive.End("demo", "3"); // 结束插件
            Console.WriteLine(ret);
            ret = drive.Run("结束后，测试执行内容", ""); 
            Console.WriteLine(ret);

            ret = drive.Update("demo", "4"); // 结束后，更新插件
            Console.WriteLine(ret);
            ret = drive.Run("更新后，测试执行内容", ""); 
            Console.WriteLine(ret);

            ret = drive.Uninstall("demo", "5"); // 更新后，卸载插件
            Console.WriteLine(ret);

            ret = drive.Updated("demo", "4"); // 更新后，更新完成插件
            Console.WriteLine(ret);

            ret = drive.End("demo", "3"); // 结束插件
            Console.WriteLine(ret);

            ret = drive.Uninstall("demo", "5"); // 结束后，卸载插件
            Console.WriteLine(ret);

            //ret = drive.Remove("demo", "5"); // 卸载后，移除插件
            //Console.WriteLine(ret);

            drive.Save(); // 保存插件
        }
    }
}
