using MM.Configs;
using MM.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MM.Drives
{
    /// <summary>
    /// 事件驱动
    /// </summary>
    public class EventDrive : Drive
    {
        #region 事件帮助类
        /// <summary>
        /// 事件帮助字典，通过事件类型驱动字典
        /// </summary>
        public ConcurrentDictionary<string, EventHelper> Dict { get; set; } = new ConcurrentDictionary<string, EventHelper>();

        /// <summary>
        /// 获取事件帮助类
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <returns>返回事件帮助类</returns>
        public EventHelper Get(string app)
        {
            Dict.TryGetValue(app, out EventHelper m);
            return m;
        }

        /// <summary>
        /// 设置事件帮助类
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="m">事件帮助类</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public bool Set(string app, EventHelper m)
        {
            return Dict.AddOrUpdate(app, m, (key, value) => m) != null;
        }

        /// <summary>
        /// 删除事件帮助类
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string app)
        {
            return Dict.TryRemove(app, out EventHelper m);
        }
        #endregion


        #region 事件配置
        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="name">事件名称</param>
        /// <param name="tense">时态</param>
        /// <param name="stage">阶段</param>
        /// <returns>返回执行前事件</returns>
        public List<EventConfig> Get(string app, string name, string tense = null, string stage = null)
        {
            List<EventConfig> list = new List<EventConfig>();
            var e = Get(app);
            if (e != null)
            {
                list = e.Get(name, tense, stage);
            }
            return list;
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="cg">字典配置</param>
        public void Set(EventConfig cg)
        {
            if (string.IsNullOrEmpty(cg.Info.App))
            {
                cg.Info.App = "api";
            }
            cg.Info.App = cg.Info.App.ToLower();
            var app = cg.Info.App;
            if (!Dict.ContainsKey(app))
            {
                Dict.TryAdd(app, new EventHelper(cg));
            }
            if (Dict.TryGetValue(app, out var m))
            {
                m.Set(cg);
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="name">事件名称</param>
        /// <param name="tense">时态</param>
        /// <param name="stage">阶段</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string app, string name, string tense = null, string stage = null)
        {
            var bl = false;
            var e = Get(app);
            if (e != null)
            {
                bl = e.Del(name, tense, stage);
            }
            return bl;
        }
        #endregion


        #region 加载
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="file">文件名</param>
        public void Load(string file)
        {
            var cg = Load<EventConfig>(file);
            Set(cg);
        }

        /// <summary>
        /// 批量加载事件
        /// </summary>
        /// <param name="list">文件名列表</param>
        public void EachLoad(List<string> list)
        {
            foreach (var o in list)
            {
                var cg = Load<EventConfig>(o);
                Set(cg);
            }
        }

        /// <summary>
        /// 批量加载事件
        /// </summary>
        /// <param name="dir">搜索目录</param>
        public void EachLoad(string dir)
        {
            Dir = dir;
            var list = EachLoad<EventConfig>();
            foreach (var cg in list)
            {
                Set(cg);
            }
        }
        #endregion


        #region 执行
        /// <summary>
        /// 执行事件（异步）
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunAsync(string app, string tag, string target)
        {
            object ret = "";
            if (Dict.ContainsKey(app))
            {
                ret = await Dict[app].RunAsync(tag, target);
            }
            return ret;
        }

        /// <summary>
        /// 执行事件（异步）
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="fun">函数</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunAsync(string app, string tag, string target, Func<string, string, object, object> fun)
        {
            object ret = "";
            if (Dict.ContainsKey(app))
            {
                ret = await Dict[app].RunAsync(tag, target, fun);
            }
            return ret;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <returns>返回执行结果</returns>
        public object Run(string app, string tag, string target)
        {
            object ret = "";
            if (Dict.ContainsKey(app))
            {
                ret = Dict[app].Run(tag, target);
            }
            return ret;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="fun">函数</param>
        /// <returns>返回执行结果</returns>
        public object Run(string app, string tag, string target, Func<string, string, object, object> fun)
        {
            object ret = "";
            if (Dict.ContainsKey(app))
            {
                ret = Dict[app].Run(tag, target, fun);
            }
            return ret;
        }
        #endregion
    }
}