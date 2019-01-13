using System.IO;

namespace MM.Helper
{
    /// <summary>
    /// 缓存对象
    /// </summary>
    public class Cache
    {
        /// <summary>
        /// 程序运行路径
        /// </summary>
        public static string _RunPath = Directory.GetCurrentDirectory();

        /// <summary>
        /// 运行路径
        /// </summary>
        public string RunPath { get { return _RunPath; } set { _RunPath = value; } }
    }
}
