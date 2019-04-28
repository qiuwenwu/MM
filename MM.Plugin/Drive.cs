using System;
using System.Collections.Generic;

namespace MM.Plugin
{
    /// <summary>
    /// 任务驱动
    /// </summary>
    public class Drive : Common.Drive
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Drive()
        {
            Search = "*plug.json";
        }

        /// <summary>
        /// 任务字典
        /// </summary>
        public Dictionary<string, Helper> Dict { get; set; } = new Dictionary<string, Helper>();


        #region 管理
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <returns>返回帮助器</returns>
        public Helper Get(string name)
        {
            if (Dict.ContainsKey(name))
            {
                return Dict[name];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string name)
        {
            End(name);
            return Dict.Remove(name);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="v">配置</param>
        public void Set(Helper v)
        {
            if (v.Info != null)
            {
                v.Change();
                var k = v.Info.Name;
                if (Dict.ContainsKey(k))
                {
                    Dict[k].End();
                    Dict[k] = v;
                    Dict[k].Init();
                }
                else
                {
                    v.Init();
                    Dict.Add(k, v);
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="path">搜索路径</param>
        public void Update(string path = null)
        {
            if (!string.IsNullOrEmpty(path))
            {
                Dir = path.ToFullName();
            }
            var dict = EachLoad<Helper>();
            foreach (var o in dict)
            {
                var p = o.Key.ToDir();
                o.Value.Change(p);
                Set(o.Value);
            }
        }

        /// <summary>
        /// 新建配置
        /// </summary>
        /// <returns>返回配置模型</returns>
        public Config New()
        {
            return new Config();
        }
        #endregion


        #region 执行
        /// <summary>
        /// 执行任务（会初始化对象，首次启动时使用）
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ret">上次执行结果</param>
        public object Run(object message, object ret)
        {
            foreach (var o in Dict.Values)
            {
                var result = o.Run(message, ret);
                if (result != null) {
                    ret = result;
                    if (o.Finish)
                    {
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 1.初始化
        /// </summary>
        /// <param name="name">任务名称</param>
        public object Init(string name = null, string param = "")
        {
            object ret = null;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    ret = o.Init(param);
                }
            }
            else if (Dict.ContainsKey(name))
            {
                ret = Dict[name].Init(param);
            }
            return ret;
        }

        /// <summary>
        /// 2.启动
        /// </summary>
        /// <param name="name">任务名称</param>
        public object Start(string name = null, string param = "")
        {
            object ret = null;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    ret = o.Start(param);
                }
            }
            else if (Dict.ContainsKey(name))
            {
                ret = Dict[name].Start(param);
            }
            return ret;
        }

        /// <summary>
        /// 3.暂停
        /// </summary>
        /// <param name="name">任务名称</param>
        public object Stop(string name = null, string param = "")
        {
            object ret = null;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    ret = o.Stop(param);
                }
            }
            else if (Dict.ContainsKey(name))
            {
                ret = Dict[name].Stop(param);
            }
            return ret;
        }

        /// <summary>
        /// 4.结束
        /// </summary>
        /// <param name="name">任务名称</param>
        public object End(string name = null, string param = "")
        {
            object ret = null;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    ret = o.End(param);
                }
            }
            else if (Dict.ContainsKey(name))
            {
                ret = Dict[name].End(param);
            }
            return ret;
        }

        /// <summary>
        /// 5.更新
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Update(string name = null, string param = "")
        {
            object ret = null;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    ret = o.Update(param);
                }
            }
            else if (Dict.ContainsKey(name))
            {
                ret = Dict[name].Update(param);
            }
            return ret;
        }

        /// <summary>
        /// 6.卸载插件
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Uninstall(string name = null, string param = "")
        {
            object ret = null;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    ret = o.Uninstall(param);
                }
            }
            else if (Dict.ContainsKey(name))
            {
                ret = Dict[name].Uninstall(param);
            }
            return ret;
        }

        /// <summary>
        /// 7.移除插件
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Remove(string name = null, string param = "")
        {
            object ret = null;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    ret = o.Remove(param);
                }
            }
            else if (Dict.ContainsKey(name))
            {
                ret = Dict[name].Remove(param);
            }
            return ret;
        }
        #endregion
    }
}
