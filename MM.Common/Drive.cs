using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace MM.Common
{
    /// <summary>
    /// 公共驱动
    /// </summary>
    public class Drive
    {
        #region 请求
        /// <summary>
        /// 请求参数
        /// </summary>
        public ConcurrentDictionary<string, object> Req { get; set; }

        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <param name="tag">标签</param>
        /// <returns>返回请求参数</returns>
        public object GetReq(string tag)
        {
            Req.TryGetValue(tag, out object m);
            return m;
        }

        /// <summary>
        /// 设置请求参数
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="m">请求参数</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public bool SetReq(string tag, object m)
        {
            return Req.AddOrUpdate(tag, m, (key, value) => m) != null;
        }

        /// <summary>
        /// 删除请求参数
        /// </summary>
        /// <param name="tag">标签</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool DelReq(string tag)
        {
            return Req.TryRemove(tag, out _);
        }
        #endregion


        #region 响应
        /// <summary>
        /// 响应结果
        /// </summary>
        public ConcurrentDictionary<string, object> Res { get; set; }

        /// <summary>
        /// 获取响应结果
        /// </summary>
        /// <param name="tag">标签</param>
        /// <returns>返回响应结果</returns>
        public object GetRes(string tag)
        {
            Res.TryGetValue(tag, out object m);
            return m;
        }

        /// <summary>
        /// 设置响应结果
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="m">响应结果</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public bool SetRes(string tag, object m)
        {
            return Res.AddOrUpdate(tag, m, (key, value) => m) != null;
        }

        /// <summary>
        /// 删除响应结果
        /// </summary>
        /// <param name="tag">标签</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool DelRes(string tag)
        {
            return Res.TryRemove(tag, out _);
        }
        #endregion


        /// <summary>
        /// 新建标签
        /// </summary>
        /// <param name="head">标签头</param>
        /// <returns>返回标签</returns>
        public string NewTag(string head)
        {
            return head + DateTime.Now.ToUniversalTime().Ticks;
        }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="tag">标签</param>
        public void Clear(string tag)
        {
            if (!string.IsNullOrEmpty(tag))
            {
                DelReq(tag);
                DelRes(tag);
            }
            else
            {
                Req.Clear();
                Res.Clear();
            }
        }


        #region 加载配置
        /// <summary>
        /// 检索的目录
        /// </summary>
        public string Dir { get; set; } = Cache.runPath;

        /// <summary>
        /// 拓展名
        /// </summary>
        public string Extension { get; set; } = ".json";

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
        public Dictionary<string, T> EachLoad<T>()
        {
            return EachLoad<T>(new DirectoryInfo(Dir));
        }

        /// <summary>
        /// 遍历读取文件
        /// </summary>
        /// <param name="root">根路径</param>
        /// <returns>返回全名加内容模型列表</returns>
        private Dictionary<string, T> EachLoad<T>(DirectoryInfo root)
        {
            var list = new Dictionary<string, T>();

            // 追加文件
            var files = root.GetFiles("*" + Extension);
            foreach (var o in files)
            {
                var name = o.FullName;
                var content = File.ReadAllText(name);
                list.Add(o.FullName, content.Loads<T>());
            }


            // 递归遍历文件
            var dir = root.GetDirectories();
            foreach (var item in dir)
            {
                var lt = EachLoad<T>(item);
                foreach (var o in lt)
                {
                    list.Add(o.Key, o.Value);
                }
            }
            return list;
        }
        #endregion


    }
}
