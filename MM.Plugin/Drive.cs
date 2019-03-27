using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MM.Plugin
{
    /// <summary>
    /// 插件驱动
    /// </summary>
    public class Drive : Common.Drive
    {
        #region 插件帮助类
        /// <summary>
        /// 插件帮助字典，通过插件类型驱动字典
        /// </summary>
        public ConcurrentDictionary<string, Helper> Dict { get; set; } = new ConcurrentDictionary<string, Helper>();

        /// <summary>
        /// 获取插件帮助类
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <returns>返回插件帮助类</returns>
        public Helper Get(string app)
        {
            Dict.TryGetValue(app, out Helper m);
            return m;
        }

        /// <summary>
        /// 设置插件帮助类
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="m">插件帮助类</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public bool Set(string app, Helper m)
        {
            return Dict.AddOrUpdate(app, m, (key, value) => m) != null;
        }

        /// <summary>
        /// 删除插件帮助类
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string app)
        {
            return Dict.TryRemove(app, out Helper m);
        }
        #endregion


        #region 插件配置
        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="name">插件名称</param>
        /// <returns>返回执行前插件</returns>
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
            cg.Change();
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
        /// 删除插件
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="name">插件名称</param>
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


        #region 加载
        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="file">文件名</param>
        public void Load(string file)
        {
            var cg = Load<Config>(file);
            Set(cg);
        }

        /// <summary>
        /// 批量加载插件
        /// </summary>
        /// <param name="list">文件名列表</param>
        public void EachLoad(List<string> list)
        {
            foreach (var o in list)
            {
                var cg = Load<Config>(o);
                Set(cg);
            }
        }

        /// <summary>
        /// 批量加载插件
        /// </summary>
        /// <param name="dir">搜索目录</param>
        public void EachLoad(string dir)
        {
            Dir = dir;
            var list = EachLoad<Config>();
            foreach (var cg in list)
            {
                Set(cg);
            }
        }
        #endregion


        #region 执行
        /// <summary>
        /// 执行插件
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="fun">标签</param>
        /// <param name="content">目标</param>
        /// <param name="name">名称</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunAsync(string app, string fun, string content, string name = "")
        {
            object ret = null;
            if (Dict.ContainsKey(app))
            {
                ret = await Dict[app].RunAsync(fun, content, name);
            }
            return ret;
        }

        /// <summary>
        /// 执行插件
        /// </summary>
        /// <param name="app">应用名称</param>
        /// <param name="fun">标签</param>
        /// <param name="content">目标</param>
        /// <param name="name">名称</param>
        /// <returns>返回执行结果</returns>
        public object Run(string app, string fun, string content, string name = "")
        {
            object ret = null;
            if (Dict.ContainsKey(app))
            {
                ret = Dict[app].Run(fun, content, name);
            }
            return ret;
        }
        #endregion
    }
}
