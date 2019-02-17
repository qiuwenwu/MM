using MM.Configs;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace MM.Helpers
{
    /// <summary>
    /// 插件帮助类
    /// </summary>
    public class PluginHelper : Helper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PluginHelper()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cg">配置模型</param>
        public PluginHelper(PluginConfig cg)
        {
            Set(cg);
        }

        #region 插件配置
        /// <summary>
        /// 事件帮助字典，通过事件名称驱动字典
        /// </summary>
        public ConcurrentDictionary<string, PluginConfig> ConfigDict { get; set; } = new ConcurrentDictionary<string, PluginConfig>();

        /// <summary>
        /// 获取插件配置
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <returns>返回请求参数</returns>
        public PluginConfig Get(string name)
        {
            ConfigDict.TryGetValue(name, out PluginConfig m);
            return m;
        }

        /// <summary>
        /// 设置插件配置
        /// </summary>
        /// <param name="m">请求参数</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public bool Set(PluginConfig m)
        {
            m.Change();
            return ConfigDict.AddOrUpdate(m.Info.Name, m, (key, value) => m) != null;
        }

        /// <summary>
        /// 删除插件配置
        /// </summary>
        /// <param name="name">插件名称</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string name)
        {
            return ConfigDict.TryRemove(name, out PluginConfig m);
        }
        #endregion


        #region 应用处理函数
        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="fun">目标对象</param>
        /// <param name="content">传递的内容</param>
        /// <param name="name">插件名称，可为空，执行所有插件</param>
        /// <returns>返回执行结果</returns>
        public object Run(string fun, object content, string name = null)
        {
            object ret = null;
            if (!string.IsNullOrEmpty(name) && ConfigDict.ContainsKey(name))
            {
                var plug = ConfigDict[name].Script;
                var result = plug.Run(fun, content, ret);
                if (result != null)
                {
                    ret = result;
                }
            }
            else
            {
                foreach (var o in ConfigDict.Values)
                {
                    var plug = o.Script;
                    var result = plug.Run(fun, content, ret);
                    if (result != null)
                    {
                        ret = result;
                        if (o.End)
                        {
                            break;
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="fun">目标对象</param>
        /// <param name="content">传递的内容</param>
        /// <param name="name">插件名称，可为空，执行所有插件</param>
        /// <returns>返回执行结果</returns>
        public Task<object> RunTask(string fun, object content, string name)
        {
            return Task.Run(() => Run(fun, content, name));
        }

        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="fun">目标对象</param>
        /// <param name="content">传递的内容</param>
        /// <param name="name">插件名称，可为空，执行所有插件</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunAsync(string fun, object content, string name)
        {
            return await RunTask(fun, content, name);
        }

        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="name">插件名称符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Install(string name, string param = "")
        {
            string ret = "";
            if (ConfigDict.ContainsKey(name))
            {
                var plug = ConfigDict[name].Script;
                var obj = plug.Run("Install", param, "", "");
                if (obj != null)
                {
                    ret = obj.ToString();
                }
                else
                {
                    ret = "安装成功！";
                }
            }
            else
            {
                ret = "安装失败！原因：插件未加载或不存在";
            }
            return ret;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name">插件名称符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Init(string name, string param = "")
        {
            string ret = "";
            if (ConfigDict.ContainsKey(name))
            {
                var obj = ConfigDict[name].Script.Run("Init", param, "", "");
                if (obj != null)
                {
                    ret = obj.ToString();
                }
                else
                {
                    ret = "初始化成功！";
                }
            }
            else
            {
                ret = "初始化失败！原因：插件未加载或不存在";
            }
            return ret;
        }

        /// <summary>
        /// 开始插件
        /// </summary>
        /// <param name="name">插件名称符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Start(string name, string param = "")
        {
            string ret = "";
            if (ConfigDict.ContainsKey(name))
            {
                var cg = ConfigDict[name];
                if (cg.OnOff)
                {
                    return "启动失败！原因：插件已启动";
                }
                else
                {
                    var obj = cg.Script.Run("Start", param, "", "");
                    if (obj != null)
                    {
                        ret = obj.ToString();
                    }
                    else
                    {
                        ret = "启动成功！";
                    }
                }
            }
            else
            {
                ret = "启动失败！原因：插件未加载或不存在";
            }
            return ret;
        }

        /// <summary>
        /// 结束插件
        /// </summary>
        /// <param name="name">插件名称符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string End(string name, string param = "")
        {
            string ret = "";
            if (ConfigDict.ContainsKey(name))
            {
                var cg = ConfigDict[name];
                if (!cg.OnOff)
                {
                    return "结束失败！原因：插件已结束";
                }
                else
                {
                    var obj = cg.Script.Run("End", param, "", "");
                    if (obj != null)
                    {
                        ret = obj.ToString();
                    }
                    else
                    {
                        ret = "结束成功！";
                    }
                }
            }
            else
            {
                ret = "结束失败！原因：插件未加载或不存在";
            }
            return ret;
        }

        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="name">插件名称符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Uninstall(string name, string param = "")
        {
            string ret = "";
            if (ConfigDict.ContainsKey(name))
            {
                var cg = ConfigDict[name];
                if (cg.OnOff)
                {
                    return "卸载失败！原因：插件处于启动状态";
                }
                else
                {
                    var obj = cg.Script.Run("Uninstall", param, "", "");
                    if (obj != null)
                    {
                        ret = obj.ToString();
                    }
                    else
                    {
                        ret = "卸载成功！";
                    }
                }
            }
            else
            {
                ret = "卸载失败！原因：插件未加载或不存在";
            }
            return ret;
        }

        /// <summary>
        /// 更新插件
        /// </summary>
        /// <param name="name">插件名称符</param>
        /// <param name="param">参数</param>
        /// <param name="sleep">休眠时长</param>
        /// <returns>返回安装结果</returns>
        public string Update(string name, string param = "", int sleep = 4)
        {
            string ret = "";
            if (ConfigDict.ContainsKey(name))
            {
                var cg = ConfigDict[name];
                if (cg.OnOff)
                {
                    return "更新失败！原因：插件处于启动状态";
                }
                else
                {
                    var obj = cg.Script.Run("Update", param, "", "");
                    if (obj != null)
                    {
                        ret = obj.ToString();
                    }
                    else
                    {
                        ret = "更新成功！";
                    }
                }
            }
            else
            {
                ret = "更新失败！原因：插件未加载或不存在";
            }
            return ret;
        }

        /// <summary>
        /// 移除插件
        /// </summary>
        /// <param name="name">插件名称符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Remove(string name, string param = "")
        {
            string ret = "";
            if (ConfigDict.ContainsKey(name))
            {
                var cg = ConfigDict[name];
                if (cg.OnOff)
                {
                    return "删除失败！原因：插件处于启动状态";
                }
                else
                {
                    var obj = cg.Script.Run("Remove", param, "", "");
                    if (obj != null)
                    {
                        ret = obj.ToString();
                    }
                    else
                    {
                        ret = "删除成功！";
                    }
                    File.Delete(cg.Info.Dir);
                }
            }
            else
            {
                ret = "删除失败！原因：插件未加载或不存在";
            }
            return ret;
        }

        /// <summary>
        /// 清理应用
        /// </summary>
        /// <returns>返回文件路径</returns>
        public void Clear()
        {
            foreach (var o in ConfigDict.Values)
            {
                Remove(o.Info.Name);
            }
        }
        #endregion
    }
}
