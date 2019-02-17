using System;
using System.Collections.Generic;
using System.IO;

namespace MM.Drives
{
    /// <summary>
    /// 公共驱动
    /// </summary>
    public class Drive
    {
        #region 加载配置
        /// <summary>
        /// 检索的目录
        /// </summary>
        public string Dir { get; set; } = Cache.runPath;

        /// <summary>
        /// 拓展名
        /// </summary>
        public string Extension { get; set; } = "event.json";

        /// <summary>
        /// 加载
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="file">文件名</param>
        /// <returns>返回加载对象</returns>
        public T Load<T>(string file)
        {
            var content = File.ReadAllText(file);
            return content.Loads<T>();
        }

        /// <summary>
        /// 遍历读取文件
        /// </summary>
        /// <returns>返回全名加内容模型列表</returns>
        public List<T> EachLoad<T>()
        {
            return EachLoad<T>(new DirectoryInfo(Dir));
        }

        /// <summary>
        /// 遍历读取文件
        /// </summary>
        /// <param name="root">根路径</param>
        /// <returns>返回全名加内容模型列表</returns>
        private List<T> EachLoad<T>(DirectoryInfo root)
        {
            var list = new List<T>();

            // 追加文件
            var files = root.GetFiles(Extension);
            foreach (var o in files)
            {
                var name = o.FullName;
                var content = File.ReadAllText(name);
                list.Add(content.Loads<T>());
            }

            // 递归遍历文件
            var dir = root.GetDirectories();
            foreach (var o in dir)
            {
                var lt = EachLoad<T>(o);
                foreach (var m in lt)
                {
                    list.Add(m);
                }
            }
            return list;
        }
        #endregion
    }
}
