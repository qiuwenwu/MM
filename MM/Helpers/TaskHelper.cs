using MM.Configs;
using MM.Models;
using System.Collections.Concurrent;
using System.Timers;

namespace MM.Helpers
{
    /// <summary>
    /// 任务帮助类
    /// </summary>
    public class TaskHelper
    {
        private ConcurrentDictionary<string, Timer> _TimerDt = new ConcurrentDictionary<string, Timer>();

        /// <summary>
        /// 应用名
        /// </summary>
        public string App { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskHelper(string app = "")
        {
            App = app;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cg">任务配置</param>
        public TaskHelper(TaskConfig cg)
        {
            App = cg.Info.App;
            Set(cg);
        }

        #region 标配
        /// <summary>
        /// 任务配置字典
        /// </summary>
        public ConcurrentDictionary<string, TaskConfig> ConfigDt { get; set; } = new ConcurrentDictionary<string, TaskConfig>();

        /// <summary>
        /// 设置任务
        /// </summary>
        /// <param name="m">任务模型</param>
        public bool Set(TaskConfig m)
        {
            m.Change();
            return ConfigDt.AddOrUpdate(m.Info.Name, m, (key, value) => m) != null;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="key">任务键</param>
        public bool Del(string key)
        {
            return ConfigDt.TryRemove(key, out var m);
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="key">任务键</param>
        public TaskConfig Get(string key)
        {
            ConfigDt.TryGetValue(key, out var m);
            return m;
        }

        /// <summary>
        /// 新建任务
        /// </summary>
        /// <returns>返回任务模型</returns>
        public TaskConfig New()
        {
            return new TaskConfig();
        }
        #endregion


        /// <summary>
        /// 重置任务
        /// </summary>
        public void Reset()
        {
            End();
            _TimerDt.Clear();

            foreach (var o in ConfigDt)
            {
                Load(o.Value);
            }
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in _TimerDt)
                {
                    o.Value.Start();
                }
            }
            else if (_TimerDt.ContainsKey(name))
            {
                _TimerDt[name].Start();
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in _TimerDt)
                {
                    o.Value.Stop();
                }
            }
            else if (_TimerDt.ContainsKey(name))
            {
                _TimerDt[name].Stop();
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        public void End(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                foreach (var o in _TimerDt)
                {
                    o.Value.Dispose();
                }
                _TimerDt.Clear();
            }
            else if (_TimerDt.ContainsKey(name))
            {
                _TimerDt[name].Dispose();
                _TimerDt.TryRemove(name, out var t);
            }
        }

        /// <summary>
        /// 加载
        /// </summary>
        public void Load(TaskConfig cg)
        {
            var tr = new Timer(cg.Sleep);
            var t = new TaskRuner(cg.Script);
            tr.Elapsed += new ElapsedEventHandler(t.Run);
            tr.AutoReset = true;    //false是执行一次，true是一直执行
            _TimerDt.AddOrUpdate(cg.Info.Name, tr, (key, value) => tr);
            Set(cg);
        }

        /// <summary>
        /// 加载
        /// </summary>
        public void Load(string name)
        {
            if (ConfigDt.TryGetValue(name, out var cg))
            {
                End(name);
                Load(cg);
            }
        }

        /// <summary>
        /// 卸载
        /// </summary>
        public void Unload(string name = null)
        {
            End(name);
            if (string.IsNullOrEmpty(name))
            {
                ConfigDt.Clear();
            }
            else
            {
                ConfigDt.TryRemove(name, out var cg);
            }
        }
    }

    /// <summary>
    /// 任务执行器
    /// </summary>
    public class TaskRuner
    {
        /// <summary>
        /// 脚本模型
        /// </summary>
        private ScriptModel _Script;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="script">脚本模型</param>
        public TaskRuner(ScriptModel script)
        {
            _Script = script;
        }

        /// <summary>
        /// 运行
        /// </summary>
        internal void Run(object sender, ElapsedEventArgs e)
        {
            _Script.Run("Task");
        }
    }
}
