using MM.Helper.Models;
using MM.Models;
using System.Collections.Generic;

namespace MM.Configs
{
    /// <summary>
    /// 接口配置
    /// </summary>
    public class ApiConfig : ComConfig
    {
        #region 常用属性
        /// <summary>
        /// 缓存时长
        /// </summary>
        public int Cache          { get; set; } = 15;

        /// <summary>
        /// 是否允许访问
        /// </summary>
        public bool Scope         { get; set; } = true;

        /// <summary>
        /// 接口路径
        /// </summary>
        public string Path        { get; set; } = "/api";

        /// <summary>
        /// 接口重定向
        /// </summary>
        public string Redirect    { get; set; }

        /// <summary>
        /// 是否中断，不再执行其他脚本
        /// </summary>
        public bool End           { get; set; }

        /// <summary>
        /// 匹配方式：0完全匹配，1前缀匹配，2正则匹配
        /// </summary>
        public int Mode           { get; set; } = 0;
        #endregion

        /// <summary>
        /// 使用权限
        /// </summary>
        public PowerModel Power   { get; set; } = new PowerModel();

        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, ParamModel> ParamDt = new Dictionary<string, ParamModel>();
    }
}
