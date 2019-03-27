using System.IO;

namespace MM
{
    /// <summary>
    /// 缓存集合
    /// </summary>
    public class Cache : Data.Cache
    {       
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
