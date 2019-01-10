using MM.Common.Models;
using MM.Helper.Models;
using System.Collections.Concurrent;

namespace MM.Data.Configs
{
    /// <summary>
    /// 接口配置模型
    /// </summary>
    public class ApiConfig
    {
        /// <summary>
        /// 开关
        /// </summary>
        public bool OnOff         { get; set; } = true;

        /// <summary>
        /// 缓存时长
        /// </summary>
        public int Cache          { get; set; } = 15;

        /// <summary>
        /// 是否允许访问
        /// </summary>
        public bool Scope         { get; set; } = true;

        /// <summary>
        /// 匹配方式：0完全匹配，1前缀匹配，2正则匹配
        /// </summary>
        public int Mode           { get; set; } = 0;

        /// <summary>
        /// 接口路径
        /// </summary>
        public string Path        { get; set; } = "/api";

        /// <summary>
        /// 接口重定向
        /// </summary>
        public string Redirect    { get; set; }

        /// <summary>
        /// 驱动的脚本
        /// </summary>
        public ScriptModel Script { get; set; }

        /// <summary>
        /// 接口信息
        /// </summary>
        public InfoModel Info     { get; set; } = new InfoModel();

        /// <summary>
        /// 使用权限
        /// </summary>
        public PowerModel Power   { get; set; } = new PowerModel();

        /// <summary>
        /// 参数集合
        /// </summary>
        public ConcurrentDictionary<string, ParamModel> ParamDt { get; set; } = new ConcurrentDictionary<string, ParamModel>();
    }
}
