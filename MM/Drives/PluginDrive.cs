using MM.Configs;
using System.Collections.Concurrent;
using System.IO;

namespace MM.Drives
{
    /// <summary>
    /// 插件驱动
    /// </summary>
    public class PluginDrive : Drive
    {
        #region 插件配置
        /// <summary>
        /// 事件帮助字典，通过事件类型驱动字典
        /// </summary>
        public ConcurrentDictionary<string, PluginConfig> ConfigDt { get; set; } = new ConcurrentDictionary<string, PluginConfig>();

        /// <summary>
        /// 获取插件配置
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <returns>返回请求参数</returns>
        public PluginConfig Get(string type)
        {
            ConfigDt.TryGetValue(type, out PluginConfig m);
            return m;
        }

        /// <summary>
        /// 设置插件配置
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="m">请求参数</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public bool Set(string type, PluginConfig m)
        {
            m.Change();
            return ConfigDt.AddOrUpdate(type, m, (key, value) => m) != null;
        }

        /// <summary>
        /// 删除插件配置
        /// </summary>
        /// <param name="type">标签</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string type)
        {
            return ConfigDt.TryRemove(type, out PluginConfig m);
        }
        #endregion


        #region 应用处理函数
        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="fun">目标对象</param>
        /// <param name="content">传递的内容</param>
        /// <param name="ret">上一次执行结果</param>
        /// <param name="identifier">唯一标识，可为空，执行所有插件</param>
        /// <returns>返回执行结果</returns>
        public object Run(string fun, object content, object ret, string identifier = null)
        {
            if (!string.IsNullOrEmpty(identifier) && ConfigDt.ContainsKey(identifier))
            {
                var plug = ConfigDt[identifier].Script;
                var result = plug.Run(fun, content, ret);
                if (result != null)
                {
                    ret = result;
                }
            }
            else
            {
                foreach (var o in ConfigDt.Values)
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
        /// 安装插件
        /// </summary>
        /// <param name="identifier">唯一标识符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Install(string identifier, string param = "")
        {
            string ret = "";
            if (ConfigDt.ContainsKey(identifier))
            {
                var plug = ConfigDt[identifier].Script;
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
        /// <param name="identifier">唯一标识符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Init(string identifier, string param = "")
        {
            string ret = "";
            if (ConfigDt.ContainsKey(identifier))
            {
                var obj = ConfigDt[identifier].Script.Run("Init", param, "", "");
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
        /// <param name="identifier">唯一标识符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Start(string identifier, string param)
        {
            string ret = "";
            if (ConfigDt.ContainsKey(identifier))
            {
                var cg = ConfigDt[identifier];
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
        /// <param name="identifier">唯一标识符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string End(string identifier, string param)
        {
            string ret = "";
            if (ConfigDt.ContainsKey(identifier))
            {
                var cg = ConfigDt[identifier];
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
        /// <param name="identifier">唯一标识符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Uninstall(string identifier, string param)
        {
            string ret = "";
            if (ConfigDt.ContainsKey(identifier))
            {
                var cg = ConfigDt[identifier];
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
        /// <param name="identifier">唯一标识符</param>
        /// <param name="param">参数</param>
        /// <param name="sleep">休眠时长</param>
        /// <returns>返回安装结果</returns>
        public string Update(string identifier, string param, int sleep = 4)
        {
            string ret = "";
            if (ConfigDt.ContainsKey(identifier))
            {
                var cg = ConfigDt[identifier];
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
        /// <param name="identifier">唯一标识符</param>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        public string Remove(string identifier, string param)
        {
            string ret = "";
            if (ConfigDt.ContainsKey(identifier))
            {
                var cg = ConfigDt[identifier];
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
                }
            }
            else
            {
                ret = "删除失败！原因：插件未加载或不存在";
            }
            return ret;
        }

        ///// <summary>
        ///// 清理插件
        ///// </summary>
        ///// <returns>返回文件路径</returns>
        //public void Clear(string identifier = null)
        //{
        //    if (identifier == null)
        //    {
        //        foreach (var o in Dict.Values)
        //        {
        //            var file = o.Info.Dir;
        //        }
        //    }
        //    else
        //    {
        //        var dir = "";
        //        if (Dict.ContainsKey(identifier))
        //        {
        //            dir = Dict[identifier].Dir;
        //            engine.CleanApp(Dict[identifier].File);
        //            Dict.Remove(identifier);
        //        }
        //        else
        //        {
        //            foreach (var o in Dt)
        //            {
        //                engine.CleanApp(o.Value.File);
        //            }
        //            Dict.Clear();
        //        }
        //        return dir;
        //    }
        //}
        #endregion
    }
}
