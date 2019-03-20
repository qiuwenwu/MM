using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MM.Task
{
    /// <summary>
    /// 任务驱动
    /// </summary>
    public class Drive : Common.Drive
    {
        #region 任务帮助类
        /// <summary>
        /// 任务帮助字典，通过任务类型驱动字典
        /// </summary>
        public ConcurrentDictionary<string, Helper> Dict { get; set; } = new ConcurrentDictionary<string, Helper>();

        /// <summary>
        /// 获取任务帮助类
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <returns>返回任务帮助类</returns>
        public Helper Get(string app)
        {
            Dict.TryGetValue(app, out Helper m);
            return m;
        }

        /// <summary>
        /// 设置任务帮助类
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="m">任务帮助类</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public bool Set(string app, Helper m)
        {
            return Dict.AddOrUpdate(app, m, (key, value) => m) != null;
        }

        /// <summary>
        /// 删除任务帮助类
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string app)
        {
            return Dict.TryRemove(app, out Helper m);
        }
        #endregion


        #region 任务配置
        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="name">任务名称</param>
        /// <returns>返回执行前任务</returns>
        public Config Get(string app, string name)
        {
            var e = Get(app);
            if (e != null)
            {
                return e.Get(name);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="cg">字典配置</param>
        public void Set(Config cg)
        {
            if (string.IsNullOrEmpty(cg.Info.App))
            {
                cg.Info.App = "api";
            }
            cg.Info.App = cg.Info.App.ToLower();
            var app = cg.Info.App;
            if (!Dict.ContainsKey(app))
            {
                Dict.TryAdd(app, new Helper(cg));
            }
            if (Dict.TryGetValue(app, out var m))
            {
                m.Set(cg);
            }
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="name">任务名称</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string app, string name)
        {
            var bl = false;
            var e = Get(app);
            if (e != null)
            {
                bl = e.Del(name);
            }
            return bl;
        }
        #endregion
    }
}
