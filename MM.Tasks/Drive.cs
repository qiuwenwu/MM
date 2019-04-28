using System;
using System.Collections.Generic;

namespace MM.Tasks
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
            Search = "*task.json";
        }

        /// <summary>
        /// 任务字典
        /// </summary>
        public Dictionary<string, Helper> Dict { get; set; } = new Dictionary<string, Helper>();


        #region 准备工作
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


        #region 操作
        /// <summary>
        /// 执行任务（会初始化对象，首次启动时使用）
        /// </summary>
        /// <param name="name">任务名称</param>
        public void Run(string name = null)
        {
            //  new MM.Helper.Sys.Files().Save("./log.txt", "有输出");
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    o.Run();
                }
            }
            else if(Dict.ContainsKey(name))
            {
                Dict[name].Run();
            }
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="name">任务名称</param>
        public void Start(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    o.Start();
                }
            }
            else if (Dict.ContainsKey(name))
            {
                Dict[name].Start();
            }
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="name">任务名称</param>
        public void Stop(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    o.Stop();
                }
            }
            else if (Dict.ContainsKey(name))
            {
                Dict[name].Stop();
            }
        }

        /// <summary>
        /// 结束任务
        /// </summary>
        /// <param name="name">任务名称</param>
        public void End(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    o.End();
                }
            }
            else if (Dict.ContainsKey(name))
            {
                Dict[name].End();
            }
        }

        /// <summary>
        /// 初始化任务
        /// </summary>
        /// <param name="name">任务名称</param>
        public void Init(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    o.Init();
                }
            }
            else if (Dict.ContainsKey(name))
            {
                Dict[name].Init();
            }
        }

        /// <summary>
        /// 重置任务
        /// </summary>
        /// <param name="name">任务名称</param>
        public void Reset(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in Dict.Values)
                {
                    o.Reset();
                }
            }
            else if (Dict.ContainsKey(name))
            {
                Dict[name].Reset();
            }
        }
        #endregion
        
    }
}
