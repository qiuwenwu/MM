using System.Collections.Generic;

namespace MM
{
    /// <summary>
    /// 站点配置模型
    /// </summary>
    public class ConfigModel
    {
        /// <summary>
        /// 是否开启调试模式
        /// </summary>
        public bool IsDebug         { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string Name          { get; set; }

        /// <summary>
        ///  监听域名地址
        /// </summary>
        public string[] Urls        { get; set; } = new string[] { };
        
        /// <summary>
        /// https端口号，使用HTTPS情况
        /// </summary>
        public int HttpsPort        { get; set; }
    }

    /// <summary>
    /// 重定向模型
    /// </summary>
    public class RewriteModel
    {
        /// <summary>
        /// 开启或关闭，true为开启
        /// </summary>
        public bool OnOff { get; set; } = false;

        /// <summary>
        /// 自定义规则
        /// </summary>
        public Dictionary<string, string> Rules { get; set; } = new Dictionary<string, string>()   {
            { @"^(.*)/article-(\d+)-(\d+)\.html$", "{$1}/article?mod=view&aid={$2}&page={$3}" },
            { @"^(.*)/channel-(\d+)-(\d+)\.html$", "{$1}/channel?mod=channel&cid={$2}&page={$3}" }
        };

        /// <summary>
        /// 拓展名
        /// </summary>
        public string Extension { get; set; } = "*rewrite.json";

        /// <summary>
        /// 更新伪静态配置
        /// </summary>
        public void Update()
        {
            // var file = System.Cache.path.Config + @"Rewrite.json";
        }
    }
}
