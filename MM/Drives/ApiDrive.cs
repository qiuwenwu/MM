using MM.Configs;
using System.Collections.Generic;

namespace MM.Drives
{
    /// <summary>
    /// 接口驱动
    /// </summary>
    public class ApiDrive
    {
        #region 标配
        /// <summary>
        /// 完全匹配API配置字典
        /// </summary>
        public Dictionary<string, ApiConfig> AllDict   { get; set; } = new Dictionary<string, ApiConfig>();

        /// <summary>
        /// 前缀匹配API配置字典
        /// </summary>
        public Dictionary<string, ApiConfig> StartDict { get; set; } = new Dictionary<string, ApiConfig>();

        /// <summary>
        /// 正则匹配API配置字典
        /// </summary>
        public Dictionary<string, ApiConfig> RegexDict { get; set; } = new Dictionary<string, ApiConfig>();

        /// <summary>
        /// 设置接口
        /// </summary>
        /// <param name="m">接口模型</param>
        public void Set(ApiConfig m)
        {
            m.Change();
            RegexDict.AddOrSet(m.Path, m);
        }

        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="key">接口键</param>
        public bool Del(string key)
        {
            return RegexDict.Remove(key);
        }

        /// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="key">接口键</param>
        public ApiConfig Get(string key)
        {
            return RegexDict.Get(key);
        }

        /// <summary>
        /// 新建接口
        /// </summary>
        /// <returns>返回接口模型</returns>
        public ApiConfig New() {
            return new ApiConfig();
        }
        #endregion
    }
}
