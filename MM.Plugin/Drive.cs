using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Msg { get; set; }

        #region 管理
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="name">插件名称</param>
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
        /// <param name="name">插件名称</param>
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
                var k = v.Info.Name;
                if (Dict.ContainsKey(k))
                {
                    Dict[k] = v;
                }
                else
                {
                    Dict.Add(k, v);
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="path">搜索路径</param>
        public void UpdateConfig(string path = null)
        {
            if (!string.IsNullOrEmpty(path))
            {
                Dir = path.ToFullName();
            }
            var dict = EachLoad<Helper>();
            foreach (var o in dict)
            {
                o.Value.Change(o.Key);
                Set(o.Value);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="path">检索路径</param>
        public void Initialize(string path = null) {
            UpdateConfig(path);
            Load();
            OrderBy();
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        public void Load() {
            var file = "./cache/".ToFullName() + App + "_plug_cache.json";
            if (File.Exists(file))
            {
                var content = File.ReadAllText(file);
                var dict = content.ToObj<Dictionary<string, Config_cache>>();

                foreach (var item in Dict)
                {
                    var k = item.Key;
                    if (dict.ContainsKey(k))
                    {
                        var cg = dict[k];
                        item.Value.State = cg.State;
                        item.Value.OrderBy = cg.OrderBy;
                    }
                }
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public void Save()
        {
            var dict = new Dictionary<string, Config_cache>();
            foreach (var item in Dict)
            {
                var o = item.Value;
                var obj = new Config_cache() { State = o.State, OrderBy = o.OrderBy };
                dict.Add(item.Key, obj);
            }
            var dir = "./cache/".ToFullName();
           
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var file = dir + App + "_plug_cache.json";

            File.WriteAllText(file.ToFullName(), dict.ToJson(true));
        }

        /// <summary>
        /// 排序
        /// </summary>
        public void OrderBy() {
            var dic = Dict.OrderBy(s1 => s1.Value.OrderBy).ToList();
            Dict.Clear();
            for (var i = 0; i < dic.Count; i++)
            {
                var kv = dic[i];
                Dict.Add(kv.Key, kv.Value);
            }
        }
        #endregion


        #region 执行
        /// <summary>
        /// 执行插件
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
        /// 0.安装
        /// </summary>
        /// <param name="name">插件名称</param>
        public object Install(string name = null, string param = "")
        {
            object ret = null;
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    ret = o.Install(param);
                }
            }
            else if (Dict.ContainsKey(name))
            {
                var obj = Dict[name];
                ret = obj.Install(param);
                Msg = obj.Msg;
            }
            return ret;
        }

        /// <summary>
        /// 1.初始化
        /// </summary>
        /// <param name="name">插件名称</param>
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
                var obj = Dict[name];
                ret = obj.Init(param);
                Msg = obj.Msg;
            }
            return ret;
        }

        /// <summary>
        /// 2.启动
        /// </summary>
        /// <param name="name">插件名称</param>
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
                var obj = Dict[name];
                ret = obj.Start(param);
                Msg = obj.Msg;
            }
            return ret;
        }

        /// <summary>
        /// 3.暂停
        /// </summary>
        /// <param name="name">插件名称</param>
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
                var obj = Dict[name];
                ret = obj.Stop(param);
                Msg = obj.Msg;
            }
            return ret;
        }

        /// <summary>
        /// 4.结束
        /// </summary>
        /// <param name="name">插件名称</param>
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

                var obj = Dict[name];
                ret = obj.End(param);
                Msg = obj.Msg;
            }
            return ret;
        }

        /// <summary>
        /// 5.1更新
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
                var obj = Dict[name];
                ret = obj.Update(param);
                Msg = obj.Msg;
            }
            return ret;
        }

        /// <summary>
        /// 5.2更新完成时
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Updated(string name, string param = "")
        {
            object ret = null;
            if (Dict.ContainsKey(name))
            {
                var o = Dict[name];
                UpdateConfig(o.Info.Dir);
                if (Dict.ContainsKey(name)) {
                    var obj = Dict[name];
                    ret = obj.Updated(param);
                    Msg = obj.Msg;
                }
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
                var obj = Dict[name];
                ret = obj.Uninstall(param);
                Msg = obj.Msg;
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
                var list = new List<string>();
                foreach (var item in Dict)
                {
                    var o = item.Value;
                    ret = o.Remove(param);
                    Msg = o.Msg;
                    if (string.IsNullOrEmpty(Msg))
                    {
                        list.Add(item.Key);
                    }
                }
                foreach (var k in list)
                {
                    Dict.Remove(k);
                }
            }
            else if (Dict.ContainsKey(name))
            {
                var obj = Dict[name];
                ret = obj.Remove(param);
                Msg = obj.Msg;
                if (string.IsNullOrEmpty(Msg)) {
                    Dict.Remove(name);
                }
            }
            return ret;
        }
        #endregion
    }
}
