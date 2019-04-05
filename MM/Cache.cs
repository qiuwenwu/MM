using System.IO;

namespace MM
{
    /// <summary>
    /// 缓存集合
    /// </summary>
    public class Cache : System.Cache
    {
        /// <summary>
        /// 配置模型
        /// </summary>
        public static ConfigModel Config   { get; set; } = new ConfigModel();

        /// <summary>
        /// 重定向——伪静态
        /// </summary>
        public static RewriteModel Rewrite { get; set; } = new RewriteModel();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Update() {
            // 创建检索路径
            if (!Directory.Exists(Path.Web))
            {
                Directory.CreateDirectory(Path.Web);
            }
        }
    }
}
