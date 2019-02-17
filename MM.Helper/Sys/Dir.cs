using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MM.Helper.Sys
{
    /// <summary>
    /// 目录帮助类
    /// </summary>
    public class Dir
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 遍历创建目录
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <returns>遍历创建成功返回true，失败返回false</returns>
        public bool Add(string dir) {
            var bl = false;
            try
            {
                Directory.CreateDirectory(dir);
                bl = true;
            }
            catch (Exception ex) {
                Ex = ex.ToString();
            }
            return bl;
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <param name="all">是否删除子目录和文件</param>
        /// <returns>如果存在则返回true，否则返回fales</returns>
        public void Del(string dir, bool all = false)
        {
            Directory.Delete(dir, all);
        }

        /// <summary>
        /// 修改目录名
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <param name="name">新名称</param>
        /// <returns>修改成功返回true，失败返回false</returns>
        public bool Set(string dir, string name)
        {
            var bl = false;
            if (!string.IsNullOrEmpty(dir) && !string.IsNullOrEmpty(name))
            {
                if (Directory.Exists(dir))
                {
                    var pa = Directory.GetParent(dir);
                    Directory.Move(dir, pa + "\\" + name);
                    bl = true;
                }
            }
            return bl;
        }

        /// <summary>
        /// 获取目录下所有文件及目录
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <param name="onlyDir">是否仅获取目录</param>
        /// <returns>返回所有子目录列表</returns>
        public List<string> Get(string dir, bool onlyDir = true)
        {
            List<string> list = new List<string>();
            var info = Directory.CreateDirectory(dir);
            if (info != null)
            {
                var arr = info.GetDirectories();
                foreach (var d in arr)
                {
                    list.Add(d.Name);
                }
            }
            if (!onlyDir) {
                var arr = info.GetFiles();
                foreach (var d in arr)
                {
                    list.Add(d.Name);
                }
            }
            return list;
        }

        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <returns>如果存在则返回true，否则返回fales</returns>
        public bool Has(string dir)
        {
            return Directory.Exists(dir);
        }

        /// <summary>
        /// 移动目录
        /// </summary>
        /// <param name="dir">源目录</param>
        /// <param name="dir2">移动后目录</param>
        /// <returns>移动成功返回true，失败返回false</returns>
        public bool Move(string dir, string dir2)
        {
            var bl = false;
            if (string.IsNullOrEmpty(dir))
            {
                Ex = "移动源目录不能为空！";
            }
            else if (string.IsNullOrEmpty(dir2))
            {
                Ex = "移动后目录不能为空！";
            }
            else
            {
                Directory.Move(dir, dir2);
                bl = true;
            }
            return bl;
        }

        /// <summary>
        /// 获取目录信息
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <returns>返回目录信息 </returns>
        public DirectoryInfo Info(string dir)
        {
            return Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// 获取所有子目录信息
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <returns>返回所有子目录信息列表</returns>
        public List<DirectoryInfo> SubInfo(string dir)
        {
            List<DirectoryInfo> list = new List<DirectoryInfo>();
            var info = Directory.CreateDirectory(dir);
            if (info != null)
            {
                list = info.GetDirectories().ToList();
            }
            return list;
        }

        /// <summary>
        /// 遍历创建目录
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <returns>遍历创建成功返回true，失败返回false</returns>
        public bool EachAdd(string dir)
        {
            try
            {
                var arr = dir.Split('\\');
                var ph = "";
                foreach (var a in arr)
                {
                    if (ph == "")
                    {
                        ph = a;
                    }
                    else
                    {
                        ph += "\\" + a;
                        if (!Directory.Exists(ph))
                        {
                            Directory.CreateDirectory(ph);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Ex = dir + "\n" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 遍历创建目录
        /// </summary>
        /// <param name="dir">目录路径</param>
        /// <returns>遍历创建成功返回true，失败返回false</returns>
        public List<string> EachGet(string dir)
        {
            return EachGet(Directory.CreateDirectory(dir));
        }

        /// <summary>
        /// 获取当前目录及所有子目录下的文件
        /// </summary>
        /// <param name="root">当前目录</param>
        /// <returns>返回文件列表</returns>
        private List<string> EachGet(DirectoryInfo root)
        {
            var list = new List<string>();

            // 递归遍历文件
            var dir = root.GetDirectories();
            foreach (var o in dir)
            {
                list.Add(o.FullName);
                var lt = EachGet(o);
                foreach (var m in lt)
                {
                    list.Add(m);
                }
            }
            return list;
        }
    }
}
