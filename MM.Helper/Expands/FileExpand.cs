using MM.Helper;
using System.IO;

namespace System
{
    /// <summary>
    /// 文件拓展
    /// </summary>
    public static class FileExpand
    {
        /// <summary>
        /// 转为文件全名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dir">当前路径</param>
        /// <returns>返回文件全名</returns>
        public static string ToFullName(this string fileName, string dir = "")
        {
            var file = fileName.Replace("/", "\\");
            if (file.StartsWith(@".\"))
            {
                file = dir + file.Substring(2);
            }
            else if (file.StartsWith(@"..\"))
            {
                file = dir + @"..\" + file;
            }
            else if (file.StartsWith(@"\"))
            {
                file = Cache._Path.Web + file.Substring(1);
            }
            else if (file.StartsWith(@"~\"))
            {
                file = Cache._Path.Template + Cache._Theme + "\\" + file.Substring(2);
            }
            return file.ToLower();
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns>返回文件路径</returns>
        public static string ToDir(this string fileName) {
            return Path.GetFullPath(fileName);
        }
    }
}
