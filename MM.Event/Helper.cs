using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MM.Event
{
    /// <summary>
    /// 事件帮助类
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Helper()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cg">事件配置</param>
        public Helper(Config cg)
        {
            if (cg != null)
            {
                Set(cg);
            }
        }

        /// <summary>
        /// 验证阶段
        /// </summary>
        public ActionHelper CheckEvent { get; set; } = new ActionHelper();

        /// <summary>
        /// 执行阶段
        /// </summary>
        public ActionHelper RunEvent { get; set; } = new ActionHelper();

        /// <summary>
        /// 渲染阶段
        /// </summary>
        public ActionHelper RenderEvent { get; set; } = new ActionHelper();


        #region 执行
        /// <summary>
        /// 执行事件（异步）
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunAsync(string tag, string target)
        {
            var ret = await CheckEvent.RunAsync(tag, target);
            if (ret == null)
            {
                ret = await RunEvent.RunAsync(tag, target, ret);
            }
            var result = await RenderEvent.RunAsync(tag, target, ret);
            if (result != null)
            {
                ret = result;
            }
            return ret;
        }

        /// <summary>
        /// 执行事件（异步）
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="fun">函数</param>
        /// <returns>返回执行结果</returns>
        public async Task<object> RunAsync(string tag, string target, Func<string, string, object, object> fun)
        {
            var ret = await CheckEvent.RunAsync(tag, target);
            if (ret == null)
            {
                ret = await RunEvent.RunAsync(tag, target, ret);
                if (ret == null)
                {
                    ret = fun(tag, target, ret);
                }
            }
            var result = await RenderEvent.RunAsync(tag, target, ret);
            if (result != null)
            {
                ret = result;
            }
            return ret;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <returns>返回执行结果</returns>
        public object Run(string tag, string target)
        {
            var ret = CheckEvent.Run(tag, target);
            if (ret == null)
            {
                ret = RunEvent.Run(tag, target, ret);
            }
            var result = RenderEvent.Run(tag, target, ret);
            if (result != null)
            {
                ret = result;
            }
            return ret;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="target">目标</param>
        /// <param name="fun">函数</param>
        /// <returns>返回执行结果</returns>
        public object Run(string tag, string target, Func<string, string, object, object> fun)
        {
            var ret = CheckEvent.Run(tag, target);
            if (ret == null)
            {
                ret = RunEvent.Run(tag, target, ret);
                if (ret == null)
                {
                    ret = fun(tag, target, ret);
                }
            }
            var result = RenderEvent.Run(tag, target, ret);
            if (result != null)
            {
                ret = result;
            }
            return ret;
        }
        #endregion


        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="tense">时态</param>
        /// <param name="stage">阶段</param>
        /// <returns>返回执行前事件</returns>
        public List<Config> Get(string name, string tense = null, string stage = null)
        {
            List<Config> list = new List<Config>();
            if (string.IsNullOrEmpty(stage))
            {
                list.Add(CheckEvent.Get(name, tense));
                list.Add(RunEvent.Get(name, tense));
                list.Add(RenderEvent.Get(name, tense));
            }
            else
            {
                switch (stage.ToLower())
                {
                    case "check":
                        list.Add(CheckEvent.Get(name, tense));
                        break;
                    case "run":
                        list.Add(RunEvent.Get(name, tense));
                        break;
                    case "render":
                        list.Add(RenderEvent.Get(name, tense));
                        break;
                }
            }
            return list;
        }

        /// <summary>
        /// 设置执行前事件
        /// </summary>
        /// <param name="cg">事件配置模型</param>
        /// <returns>设置成功返回true，是失败返回false</returns>
        public void Set(Config cg)
        {
            if (string.IsNullOrEmpty(cg.Stage))
            {
                cg.Stage = "run";
            }
            cg.Stage = cg.Stage.ToLower();
            switch (cg.Stage)
            {
                case "check":
                    CheckEvent.Set(cg);
                    break;
                case "run":
                    RunEvent.Set(cg);
                    break;
                case "render":
                    RenderEvent.Set(cg);
                    break;
            }
        }

        /// <summary>
        /// 删除执行前事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="tense">时态</param>
        /// <param name="stage">阶段</param>
        /// <returns>删除成功返回true，是失败返回false</returns>
        public bool Del(string name, string tense = null, string stage = null)
        {
            var bl = false;
            if (string.IsNullOrEmpty(stage))
            {
                CheckEvent.Del(name, tense);
                RunEvent.Del(name, tense);
                RenderEvent.Del(name, tense);
                bl = true;
            }
            else
            {
                switch (stage.ToLower())
                {
                    case "check":
                        CheckEvent.Del(name, tense);
                        break;
                    case "run":
                        RunEvent.Del(name, tense);
                        break;
                    case "render":
                        RenderEvent.Del(name, tense);
                        break;
                }
            }
            return bl;
        }
    }
}
