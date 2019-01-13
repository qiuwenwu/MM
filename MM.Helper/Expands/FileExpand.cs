using MM.Helper;

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
            var ret = "";
            if (fileName.StartsWith("./"))
            {
                ret = dir + fileName.Substring(2);
            }
            else if (fileName.StartsWith("../"))
            {
                ret = dir + fileName;
            }
            else if (fileName.StartsWith("/"))
            {
                ret = Cache._RunPath + fileName.Substring(1);
            }
            else
            {
                ret = Cache._RunPath + fileName;
            }
            return ret;
        }
    }
}
