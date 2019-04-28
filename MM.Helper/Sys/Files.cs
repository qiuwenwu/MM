using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MM.Helper.Sys
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class Files
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 定点路径
        /// </summary>
        public string Dir       { get; set; }

        /// <summary>
        /// 拓展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>返回文件内容</returns>
        public string Load(string fileName, string encoding = "utf-8") {
            var file = fileName.ToFullName(Dir);
            if (File.Exists(file)) {
                return File.ReadAllText(file, Encoding.GetEncoding(encoding));
            }
            else {
                Ex = "文件不存在";
                return "";
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="content">文件内容</param>
        public void Save(string fileName, string content, string encoding = "utf-8")
        {
            File.WriteAllText(fileName.ToFullName(Dir), content, Encoding.GetEncoding(encoding));
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="newFileName">新文件位置</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public void Copy(string fileName, string newFileName)
        {
            File.Copy(fileName.ToFullName(Dir), newFileName.ToFullName(Dir));
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="newFileName">移动后的文件位置</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public void Move(string fileName, string newFileName)
        {
            var fullName = fileName.ToFullName(Dir);
            File.Copy(fullName, newFileName.ToFullName(Dir));
            File.Delete(fullName);
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
        /// 添加文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">内容</param>
        /// <returns>添加成功返回true，失败返回fales</returns>
        public bool Add(string fileName, string content)
        {
            var bl = false;
            if (!string.IsNullOrEmpty(fileName))
            {
                if (fileName.Contains("."))
                {
                    if (!string.IsNullOrEmpty(content))
                    {
                        File.AppendAllText(fileName.ToFullName(Dir), content);
                        bl = true;
                    }
                    else
                    {
                        Ex = "内容不能为空！";
                    }
                }
                else
                {
                    Ex = "添加的文件不能没有后缀名！";
                }
            }
            else
            {
                Ex = "添加的文件名不能为空";
            }
            return bl;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public void Del(string fileName)
        {
            File.Delete(fileName.ToFullName(Dir));
        }

        /// <summary>
        /// 修改文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">内容</param>
        /// <returns>修改成功返回true，失败返回fales</returns>
        public bool Set(string fileName, string content)
        {
            var bl = false;
            if (File.Exists(fileName))
            {
                File.WriteAllText(fileName.ToFullName(Dir), content);
                bl = true;
            }
            else
            {
                Ex = "文件不存在！";
            }
            return bl;
        }

        /// <summary>
        /// 获取目录下所有文件
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <param name="search">只搜索匹配的文件类型</param>
        /// <returns>返回文件列表</returns>
        public List<string> Get(string dir, string search = null)
        {
            List<string> list = new List<string>();
            var arr = Info(dir, search);
            foreach (var file in arr)
            {
                list.Add(file.Name);
            }
            return list;
        }

        /// <summary>
        /// 判断文件是否有
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>有则返回true，没有则返回false</returns>
        public bool Has(string file)
        {
            return File.Exists(file.ToFullName(Dir));
        }

        /// <summary>
        /// 读取内容
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回读取的内容</returns>
        public byte[] LoadByte(string fileName)
        {
            byte[] byteArr = null;
            if (!string.IsNullOrEmpty(fileName))
            {
                if (fileName.Contains("."))
                {
                    byteArr = File.ReadAllBytes(fileName);
                }
                else
                {
                    Ex = "读取的需为文件，而不是目录";
                }
            }
            else
            {
                Ex = "读取文件名不能为空";
            }
            return byteArr;
        }
        
        /// <summary>
        /// 获取目录下所有文件信息
        /// </summary>
        /// <param name="dir">目录路径 </param>
        /// <param name="search">只搜索匹配的文件类型</param>
        /// <returns>返回文件信息列表</returns>
        public List<FileInfo> Info(string dir, string search)
        {
            var info = Directory.CreateDirectory(dir);
            return info.GetFiles(search).ToList();
        }

        /// <summary>
        /// 获取文件名部分
        /// </summary>
        /// <param name="fileName">文件全名</param>
        /// <param name="ext">是否保留拓展名</param>
        /// <returns>返回文件名</returns>
        public string Name(string fileName, bool ext = true)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return "";
            }
            var arr = fileName.Split('\\');
            var name = arr[arr.Length - 1];
            if (!ext)
            {
                name = name.Split('.')[0];
            }
            return name;
        }
        
        /// <summary>
        /// 遍历读取文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="extension">拓展名</param>
        /// <returns>返回全名加内容模型列表</returns>
        public List<FilesModel> EachLoad(string path, string extension)
        {
            var fullPath = path.ToFullName(Dir);
            Extension = extension;
            return EachLoad(new DirectoryInfo(fullPath));
        }

        /// <summary>
        /// 遍历读取文件
        /// </summary>
        /// <param name="root">根路径</param>
        /// <returns>返回全名加内容模型列表</returns>
        private List<FilesModel> EachLoad(DirectoryInfo root)
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
                foreach (var m in lt)
                {
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
        private List<T> EachLoad<T>(DirectoryInfo root)
        {
            var list = new List<T>();

            // 追加获取文件
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
        /// 获取当前目录及所有子目录下的文件
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <param name="extension">只搜索匹配的文件类型</param>
        /// <returns>返回文件列表</returns>
        public List<string> EachGet(string path, string extension = null)
        {
            Extension = extension;
            return EachGet(new DirectoryInfo(path));
        }

        /// <summary>
        /// 获取当前目录及所有子目录下的文件
        /// </summary>
        /// <param name="root">当前目录</param>
        /// <returns>返回文件列表</returns>
        private List<string> EachGet(DirectoryInfo root)
        {
            var list = new List<string>();

            // 追加文件
            var files = root.GetFiles(Extension);
            foreach (var o in files)
            {
                list.Add(o.FullName);
            }

            // 递归遍历文件
            var dir = root.GetDirectories();
            foreach (var o in dir)
            {
                var lt = EachGet(o);
                foreach (var m in lt)
                {
                    list.Add(m);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns>返回文件路径</returns>
        public string ToDir(string fileName)
        {
            return fileName.ToDir();
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
