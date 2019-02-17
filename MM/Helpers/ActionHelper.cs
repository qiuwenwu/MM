using MM.Configs;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MM.Helpers
{
    /// <summary>
    /// 行为帮助类
    /// </summary>
    public class ActionHelper
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 新建事件模型
        /// </summary>
        /// <returns>返回事件模型</returns>
        public EventConfig New()
        {
            return new EventConfig();
        }


        #region 执行前
        /// <summary>
        /// 执行前事件字典
        /// </summary>
        public Dictionary<string, EventConfig> BeforeDict { get; set; }

        /// <summary>
        /// 获取执行前事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <returns>返回执行前事件</returns>
        public EventConfig GetBefore(string name)
        {
            BeforeDict.TryGetValue(name, out EventConfig m);
            return m;
        }

        /// <summary>
        /// 设置执行前事件
        /// </summary>
        /// <param name="m">事件模型</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public void SetBefore(EventConfig m)
        {
            BeforeDict.AddOrSet(m.Info.Name, m);
        }

        /// <summary>
        /// 删除执行前事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <returns>删除成功返回true，是失败返回false</returns>
        public bool DelBefore(string name)
        {
            return BeforeDict.Remove(name);
        }

        /// <summary>
        /// 运行执行前事件
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public object RunBefore(string tag, string target, object ret = null)
        {
            return RunEvent(BeforeDict, tag, target, ret);
        }

        /// <summary>
        /// 运行执行前事件(任务)
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public Task<object> RunBeforeTask(string tag, string target, object ret = null)
        {
            return Task.Run(() => RunBefore(tag, target, ret));
        }

        /// <summary>
        /// 运行执行前事件(异步)
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunBeforeAsync(string tag, string target, object ret = null) {
            return await RunBeforeTask(tag, target, ret);
        }
        #endregion


        #region 主执行
        /// <summary>
        /// 主执行事件字典
        /// </summary>
        public Dictionary<string, EventConfig> MainDict { get; set; }

        /// <summary>
        /// 获取主执行事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <returns>返回主执行事件</returns>
        public EventConfig GetMain(string name)
        {
            MainDict.TryGetValue(name, out EventConfig m);
            return m;
        }

        /// <summary>
        /// 设置主执行事件
        /// </summary>
        /// <param name="m">事件模型</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public void SetMain(EventConfig m)
        {
            MainDict.AddOrSet(m.Info.Name, m);
        }

        /// <summary>
        /// 删除主执行事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <returns>删除成功返回true，是失败返回false</returns>
        public bool DelMain(string name)
        {
            return MainDict.Remove(name);
        }

        /// <summary>
        /// 运行主执行事件
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public object RunMain(string tag, string target, object ret = null)
        {
            return RunEvent(MainDict, tag, target, ret);
        }

        /// <summary>
        /// 运行主执行事件(任务)
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public Task<object> RunMainTask(string tag, string target, object ret = null)
        {
            return Task.Run(() => RunMain(tag, target, ret));
        }

        /// <summary>
        /// 运行主执行事件(异步)
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunMainAsync(string tag, string target, object ret = null)
        {
            return await RunMainTask(tag, target, ret);
        }
        #endregion


        #region 执行后
        /// <summary>
        /// 执行后事件字典
        /// </summary>
        public Dictionary<string, EventConfig> AfterDict { get; set; }

        /// <summary>
        /// 获取执行后事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <returns>返回执行后事件</returns>
        public EventConfig GetAfter(string name)
        {
            AfterDict.TryGetValue(name, out EventConfig m);
            return m;
        }

        /// <summary>
        /// 设置执行后事件
        /// </summary>
        /// <param name="m">事件模型</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public void SetAfter(EventConfig m)
        {
            AfterDict.AddOrSet(m.Info.Name, m);
        }

        /// <summary>
        /// 删除执行后事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <returns>删除成功返回true，是失败返回false</returns>
        public bool DelAfter(string name)
        {
            return AfterDict.Remove(name);
        }

        /// <summary>
        /// 运行执行后事件
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public object RunAfter(string tag, string target, object ret = null)
        {
            return RunEvent(AfterDict, tag, target, ret);
        }

        /// <summary>
        /// 运行执行后事件(任务)
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public Task<object> RunAfterTask(string tag, string target, object ret = null)
        {
            return Task.Run(() => RunAfter(tag, target, ret));
        }

        /// <summary>
        /// 运行执行后事件(异步)
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunAfterAsync(string tag, string target, object ret = null)
        {
            return await RunAfterTask(tag, target, ret);
        }
        #endregion


        /// <summary>
        /// 运行主执行事件
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public object Run(string tag, string target, object ret = null)
        {
            RunBeforeTask(tag, target, ret);
            ret = RunMain(tag, target, ret);
            RunAfterTask(tag, target, ret);
            return ret;
        }

        /// <summary>
        /// 运行主执行事件
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunAsync(string tag, string target, object ret = null)
        {
            await RunBeforeAsync(tag, target, ret);
            ret = await RunMainAsync(tag, target, ret);
            await RunAfterAsync(tag, target, ret);
            return ret;
        }
        
        /// <summary>
        /// 运行事件
        /// </summary>
        /// <param name="dict">事件字典</param>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="ret">上一次执行结果</param>
        /// <returns>返回执行结果</returns>
        private static object RunEvent(Dictionary<string, EventConfig> dict, string tag, string target, object ret = null)
        {
            if (ret != null)
            {
                foreach (var o in dict.Values)
                {
                    object result = null;
                    if (o.Mode == 0) // 完全匹配
                    {
                        if (target == o.Target)
                        {
                            result = o.Script.Run(tag, target, ret);
                        }
                    }
                    else if (o.Mode == 1)  // 前缀匹配
                    {
                        if (target.StartsWith(o.Target))
                        {
                            result = o.Script.Run(tag, target, ret);
                        }
                    }
                    else if (Regex.IsMatch(target, o.Target)) // 正则匹配
                    {
                        result = o.Script.Run(tag, target, ret);
                    }
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
        /// 获取事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="tense">时态</param>
        /// <returns>返回执行前事件</returns>
        public List<EventConfig> Get(string name, string tense = null)
        {
            List<EventConfig> list = new List<EventConfig>();
            if (string.IsNullOrEmpty(tense))
            {
                var m_before = GetBefore(name);
                if (m_before != null)
                {
                    list.Add(m_before);
                }
                var m_main = GetMain(name);
                if (m_main != null)
                {
                    list.Add(m_main);
                }
                var m_atfer = GetAfter(name);
                if (m_atfer != null)
                {
                    list.Add(m_atfer);
                }
            }
            else
            {
                EventConfig m = null;
                switch (tense.ToLower()) {
                    case "before":
                        m = GetBefore(name);
                        break;
                    case "main":
                        m = GetMain(name);
                        break;
                    case "after":
                        m = GetAfter(name);
                        break;
                }
                if (m != null)
                {
                    list.Add(m);
                }
            }
            return list;
        }

        /// <summary>
        /// 设置执行前事件
        /// </summary>
        /// <param name="m">事件模型</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public void Set(EventConfig m)
        {
            if (string.IsNullOrEmpty(m.Tense))
            {
                m.Tense = "main";
            }
            switch (m.Tense.ToLower())
            {
                case "before":
                    SetBefore(m);
                    break;
                case "main":
                    SetMain(m);
                    break;
                case "after":
                    SetAfter(m);
                    break;
            }
        }

        /// <summary>
        /// 删除执行前事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="tense">时态</param>
        /// <returns>删除成功返回true，是失败返回false</returns>
        public bool Del(string name, string tense = null)
        {
            var bl = false;
            if (string.IsNullOrEmpty(tense))
            {
                DelBefore(name);
                DelMain(name);
                DelAfter(name);
                bl = true;
            }
            else
            {
                switch (tense.ToLower())
                {
                    case "before":
                        bl = DelBefore(name);
                        break;
                    case "main":
                        bl = DelMain(name);
                        break;
                    case "after":
                        bl = DelAfter(name);
                        break;
                }
            }
            return bl;
        }
    }
}
