using MM.Engine;

namespace MM.Models
{
    /// <summary>
    /// 脚本模型
    /// </summary>
    public class ScriptModel
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string File   { get; set; }

        /// <summary>
        /// 函数名
        /// </summary>
        public string Fun    { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        internal dynamic Mod {  get; set; }

        /// <summary>
        /// 脚本引擎
        /// </summary>
        private static Index Engine = new Index();

        /// <summary>
        /// 运行API
        /// </summary>
        /// <param name="fun">函数</param>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        /// <param name="param3">参数3</param>
        /// <returns>返回标签</returns>
        internal object Run(string fun, object param1 = null, object param2 = null, object param3 = null)
        {
            object ret = null;

            if (fun == null)
            {
                fun = Fun;
            }
            if (Mod != null)
            {
                ret = Mod.Run(fun, param1, param2, param3);
            }
            else
            {
                ret = Engine.Run(File, fun, param1, param2, param3);
                if (ret != null)
                {
                    Mod = Engine.Get(File);
                }
            }
            return ret;
        }

        /// <summary>
        /// 卸载脚本
        /// </summary>
        internal void Unload()
        {
            Mod = null;
            Engine.Unload(File);
        }
    }
}
