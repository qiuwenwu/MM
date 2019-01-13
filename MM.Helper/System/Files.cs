using System;
using System.Collections.Generic;
using System.IO;

namespace MM.Helper.System
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class Files : IDisposable
    {
        /// <summary>
        /// 定点路径
        /// </summary>
        public string Dir       { get; set; }

        /// <summary>
        /// 拓展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 遍历读取文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="extension">拓展名</param>
        /// <returns>返回全名加内容模型列表</returns>
        public List<FilesModel> EachLoad(string path, string extension)
        {
            var fullPath = ToFullName(path);
            return EachLoad(new DirectoryInfo(path));
        }

        /// <summary>
        /// 遍历读取文件
        /// </summary>
        /// <param name="root">根路径</param>
        /// <returns>返回全名加内容模型列表</returns>
        public List<FilesModel> EachLoad(DirectoryInfo root)
        {
            var list = new List<FilesModel>();

            // 追加文件
            var files = root.GetFiles(Extension);
            foreach (var o in files)
            {
                var name = o.FullName;
                var content = File.ReadAllText(name);
                var m = new FilesModel() { FullName = name, Content = content };
                list.Add(m);
            }

            // 递归遍历文件
            var dir = root.GetDirectories();
            foreach (var o in dir)
            {
               var lt = EachLoad(o);
                foreach (var m in lt) {
                    list.Add(m);
                }
            }

            return list;
        }

        /// <summary>
        /// 遍历读取文件
        /// </summary>
        /// <param name="path">根路径</param>
        /// <param name="extension">拓展名</param>
        /// <returns>返回全名加内容模型列表</returns>
        public List<T> EachLoad<T>(string path, string extension)
        {
            Extension = extension;
            return EachLoad<T>(new DirectoryInfo(path));
        }

        /// <summary>
        /// 遍历读取文件
        /// </summary>
        /// <param name="root">根路径</param>
        /// <returns>返回全名加内容模型列表</returns>
        public List<T> EachLoad<T>(DirectoryInfo root)
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

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回文件内容</returns>
        public string Load(string fileName) {
            return File.ReadAllText(ToFullName(fileName));
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">文件内容</param>
        public void Save(string fileName, string content)
        {
            File.WriteAllText(ToFullName(fileName), content);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public void Delete(string fileName)
        {
            File.Delete(ToFullName(fileName));
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="newFileName">新文件位置</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public void Copy(string fileName, string newFileName)
        {
            File.Copy(ToFullName(fileName), ToFullName(newFileName));
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="newFileName">移动后的文件位置</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public void Move(string fileName, string newFileName)
        {
            var fullName = ToFullName(fileName);
            File.Copy(fullName, ToFullName(newFileName));
            File.Delete(fullName);
        }

        /// <summary>
        /// 转为文件全名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回文件全名</returns>
        public string ToFullName(string fileName)
        {
            return fileName.ToFullName(Dir);
        }

        /// <summary>
        /// 转为文件全名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dir">基础路径</param>
        /// <returns>返回文件全名</returns>
        public string ToFullName(string fileName, string dir)
        {
            return fileName.ToFullName(dir);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            Dir = null;
        }
    }

    /// <summary>
    /// 文件模型
    /// </summary>
    public class FilesModel
    {
        /// <summary>
        /// 文件全名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        public string Content { get; set; }
    }
}
