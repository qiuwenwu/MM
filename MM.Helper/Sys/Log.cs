using System;
using System.IO;
using System.Text;

namespace MM.Helper.Sys
{
    /// <summary>
    /// 日志输出帮助类
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 存储路径
        /// </summary>
        public string Dir { get; set; } = Cache._Path.Cache;
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>初始化成功返回true，失败返回false</returns>
        public bool Init()
        {
            var File = new Dir();
            return File.EachAdd(Dir);
        }

        /// <summary>
        /// 写出所有内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>写出成功返回true，失败返回false</returns>
        public bool Save(string content)
        {
            var file = Dir + "log.txt";
            try
            {
                File.WriteAllText(file, content, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Ex = file + "\n" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 读取所有内容
        /// </summary>
        /// <returns>读取成功返回内容，</returns>
        public string Read()
        {
            var file = Dir + "log.txt";
            if (File.Exists(file))
            {
                return File.ReadAllText(file, Encoding.UTF8);
            }
            else
            {
                Ex = file + "文件不存在！";
                return "";
            }
        }

        /// <summary>
        /// 清除所有信息
        /// </summary>
        public void Clear()
        {
            try
            {
                Console.Clear();
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
            }
        }

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="obj">写出的内容</param>
        /// <returns>写出成功返回true，失败返回false</returns>
        public void Output(object obj)
        {
            if (obj != null)
            {
                Console.WriteLine(obj);
            }
        }

        /// <summary>
        /// 写出文本行
        /// </summary>
        /// <param name="obj">写出的内容</param>
        /// <returns>写出成功返回true，失败返回false</returns>
        public bool WriteLine(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            try
            {
                File.AppendAllText(Dir + "log.txt", "\r\n" + obj.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
                return false;
            }
        }
    }
}
