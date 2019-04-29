using MM.Helper.Infos;
using System;
using System.IO;
using System.Text;

namespace MM.Plugin
{
    /// <summary>
    /// 插件索引目录
    /// </summary>
    public class Indexer
    {
        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <returns>返回版本信息模型</returns>
        public static DllInfo Info()
        {
            return new DllInfo() { Name = "超级美眉系列——插件框架" };
        }

        /// <summary>
        /// 实例化驱动
        /// </summary>
        /// <returns>返回驱动函数类</returns>
        public Drive Drive()
        {
            return new Drive();
        }

        /// <summary>
        /// 实例化帮助类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Helper Helper()
        {
            return new Helper();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <returns>返回配置模型</returns>
        public Config Config()
        {
            return new Config();
        }

        /// <summary>
        /// 创建示例
        /// </summary>
        public void Demo(string path = "demo_plug.json")
        {
            var m = new Config() { Script = new Common.Script() { File = "./demo_plug.py", Fun = "Run" }, Info = new Common.Info() { App = "mm", Desc = "这是一个测试的插件", Dir = "./", Name = "demo", Title = "示例插件", Group = "春节活动组", Type = "活动类" } };
            File.WriteAllText(path.ToFullName(), m.ToJson(true), Encoding.UTF8);
        }
    }
}
