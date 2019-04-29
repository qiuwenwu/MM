using MM.Helper.Sys;

namespace MM.Plugin
{
    /// <summary>
    /// 任务帮助类
    /// </summary>
    public class Helper : Config
    {
        private static readonly Dir DirHelper = new Dir();

        #region 属性
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Msg   { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; } = "End";

        /// <summary>
        /// 执行顺序
        /// </summary>
        public int OrderBy { get; set; } = 100;
        #endregion


        #region 方法
        /// <summary>
        /// 执行插件
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ret">结果</param>
        /// <returns>返回执行结果</returns>
        public object Run(object message, object ret) {
            if (State == "Start")
            {
                return Script.Run("Run", message, ret, "");
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// 1.安装插件
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回安装结果</returns>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Install(string param = "")
        {
            if (State == "Update")
            {
                Msg = "插件更新中，请稍后再试！";
                return null;
            }
            else
            {
                return Script.Run("Install", param, "", "");
            }
        }

        /// <summary>
        /// 2.初始化
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Init(string param = "") {
            return Script.Run("Init", param, "", "");
        }

        /// <summary>
        /// 3.启动
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Start(string param = "")
        {
            if (State == "Update") {
                Msg = "插件更新中，请稍后再试！";
            }
            else if (State != "Start")
            {
                State = "Start";
                return Script.Run("Start", param, "", "");
            }
            return null;
        }

        /// <summary>
        /// 4.停止
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Stop(string param = "")
        {
            if (State == "Start")
            {
                State = "Stop";
                return Script.Run("Stop", param, "", "");
            }
            else
            {
                Msg = "插件未启动！";
                return null;
            }
        }

        /// <summary>
        /// 5.结束
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object End(string param = "")
        {
            State = "End";
            return Script.Run("End", param, "", "");
        }

        /// <summary>
        /// 5.1更新
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Update(string param = "")
        {
            if (State == "End")
            {
                State = "Update";
                return Script.Run("Update", param, "", "");
            }
            else
            {
                Msg = "需先结束运行，才能更新插件！";
                return null;
            }
        }

        /// <summary>
        /// 5.2更新完成时
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Updated(string param = "")
        {
            State = "Updated";
            return Script.Run("Updated", param, "", "");
        }

        /// <summary>
            /// 6.卸载插件
            /// </summary>
            /// <param name="param">参数</param>
            /// <returns>返回执行结果</returns>
        public object Uninstall(string param = "")
        {
            if (State == "End")
            {
                State = "Uninstall";
                return Script.Run("Uninstall", param, "", "");
            }
            else
            {
                Msg = "需先结束运行，才能卸载插件！";
                return null;
            }
        }

        /// <summary>
        /// 7.移除插件
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回执行结果</returns>
        public object Remove(string param = "") {
            Msg = null;
            if (State == "End" || State == "Uninstall")
            {
                var ret = Script.Run("Remove", param, "", "");
                DirHelper.Del(Info.Dir, true);
                return ret;
            }
            else
            {
                Msg = "需先结束或卸载插件，才能删除插件！";
                return null;
            }
        }
        #endregion

    }
}
