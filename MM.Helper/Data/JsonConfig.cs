using Microsoft.Extensions.Configuration;

namespace MM.Helper.Datas
{
    /// <summary>
    /// json配置文件
    /// </summary>
    public class JsonConfig
    {
        /// <summary>
        /// 配置构建器
        /// </summary>
        public ConfigurationBuilder Builder { get; set; } = new ConfigurationBuilder();
    }
}
