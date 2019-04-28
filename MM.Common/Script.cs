using MM.Engine;
using MM.Helper.Sys;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MM.Common
{
    /// <summary>
    /// 脚本模型
    /// </summary>
    public class Script
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty("file")]
        public string File { get; set; }

        /// <summary>
        /// 函数名
        /// </summary>
        [JsonProperty("fun")]
        public string Fun  { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        internal dynamic Mod;

        /// <summary>
        /// 脚本引擎
        /// </summary>
        private readonly Indexer Eng = new Indexer();

        /// <summary>
        /// 运行API
        /// </summary>
        /// <param name="fun">函数</param>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        /// <param name="param3">参数3</param>
        /// <returns>返回标签</returns>
        public object Run(string fun, object param1 = null, object param2 = null, object param3 = null)
        {
            if (string.IsNullOrEmpty(fun))
            {
                fun = Fun;
            }

            object ret;
            if (Mod != null)
            {
                ret = Mod.Main(fun, param1, param2, param3);
            }
            else
            {
                ret = Eng.Run(File, fun, param1, param2, param3);
                if (ret != null)
                {
                    Mod = Eng.Get(File);
                }
            }
            return ret;
        }

        /// <summary>
        /// 卸载脚本
        /// </summary>
        public void Unload()
        {
            Mod = null;
            Eng.Unload(File);
        }
    }
}
