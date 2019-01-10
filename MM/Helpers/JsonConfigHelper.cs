using Microsoft.Extensions.Configuration;

namespace MM.Helpers
{
    /// <summary>
    /// json配置文件
    /// </summary>
    public class JsonConfigHelper
    {
        /// <summary>
        /// 配置构建器
        /// </summary>
        public ConfigurationBuilder Builder { get; set; } = new ConfigurationBuilder();
    }
}
