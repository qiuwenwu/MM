using System;
using System.Collections.Generic;

namespace MM.API
{
    public class Drive : Common.Drive
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Drive() {
            Extension = "api.json";
        }

        #region 标配
        /// <summary>
        /// 完全匹配API配置字典
        /// </summary>
        public Dictionary<string, Config> Dict { get; set; } = new Dictionary<string, Config>();

        /// <summary>
        /// 设置接口
        /// </summary>
        /// <param name="m">接口模型</param>
        public void Set(Config m)
        {
            Dict.AddOrSet(m.Path, m);
        }

        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="key">接口键</param>
        public bool Del(string key)
        {
            return Dict.Remove(key);
        }

        /// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="key">接口键</param>
        public Config Get(string key)
        {
            return Dict.Get(key);
        }

        /// <summary>
        /// 新建接口
        /// </summary>
        /// <returns>返回接口模型</returns>
        public Config New()
        {
            return new Config();
        }
        #endregion
    }
}
