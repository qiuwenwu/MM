using System;
using System.Collections.Generic;

namespace MM.Task
{
    /// <summary>
    /// 任务驱动
    /// </summary>
    public class Drive : Common.Drive
    {
        public Drive()
        {
            Search = "task.json";
        }

        /// <summary>
        /// 任务字典
        /// </summary>
        public Dictionary<string, Helper> Dict { get; set; } = new Dictionary<string, Helper>();

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>删除成功返回true，失败返回false</returns>
        public bool Del(string key)
        {
            return Dict.Remove(key);
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
    }
}
