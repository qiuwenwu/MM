using System.Collections.Generic;

namespace MM.Common.Engines
{
    /// <summary>
    /// 脚本引擎接口
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 遍历加载
        /// </summary>
        /// <param name="appList">应用列表</param>
        /// <returns>加载成功返回true，失败返回false</returns>
        bool EachLoad(List<string> appList);

        /// <summary>
        /// 载入脚本
        /// </summary>
        /// <param name="appName">应用名称</param>
        /// <param name="file">文件名</param>
        /// <returns>载入成功返回true，失败返回false</returns>
        bool Load(string appName, string file);

        /// <summary>
        /// 卸载脚本
        /// </summary>
        /// <param name="appName">应用名称</param>
        /// <returns>卸载成功返回true，失败返回false</returns>
        bool Unload(string appName);
        
        /// <summary>
        /// 执行脚本——无返回结果
        /// </summary>
        /// <param name="appName">应用名</param>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        /// <param name="param3">参数3</param>
        /// <param name="param4">参数4</param>
        void Execute(string appName, object param1 = null, object param2 = null, object param3 = null, object param4 = null);

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="appName">应用名</param>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        /// <param name="param3">参数3</param>
        /// <param name="param4">参数4</param>
        /// <returns>返回执行结果</returns>
        object Run(string appName, object param1 = null, object param2 = null, object param3 = null, object param4 = null);

        /// <summary>
        /// 执行脚本文件
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        /// <param name="param3">参数3</param>
        /// <param name="param4">参数4</param>
        /// <returns>返回执行结果</returns>
        object RunFile(string file, object param1 = null, object param2 = null, object param3 = null, object param4 = null);

        /// <summary>
        /// 执行脚本代码
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        /// <param name="param3">参数3</param>
        /// <param name="param4">参数4</param>
        /// <returns>返回执行结果</returns>
        object RunCode(string code, object param1 = null, object param2 = null, object param3 = null, object param4 = null);
    }
}
