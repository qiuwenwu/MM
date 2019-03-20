using Newtonsoft.Json;
using System.IO;

namespace MM.Helper.Models
{
    /// <summary>
    /// 路径模型
    /// </summary>
    public class PathModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="runPath">程序运行路径</param>
        public PathModel(string runPath)
        {
            if (runPath == null)
            {
                runPath = Directory.GetCurrentDirectory() + "\\";
            }
            Root = runPath;
            Cache = Root + "cache\\";
            Web = Root + "wwwroot\\";
            Script = Web + "script\\";
            Static = Web + "static\\";
            Template = Web + "template\\";
        }

        /// <summary>
        /// 程序根目录
        /// </summary>
        [JsonProperty("root")]
        public string Root     { get; set; }

        /// <summary>
        /// 站点目录
        /// </summary>
        [JsonProperty("web")]
        public string Web      { get; set; }

        /// <summary>
        /// 脚本目录
        /// </summary>
        [JsonProperty("script")]
        public string Script   { get; set; }

        /// <summary>
        /// 静态文件目录
        /// </summary>
        [JsonProperty("static")]
        public string Static   { get; set; }

        /// <summary>
        /// 模板文件目录
        /// </summary>
        [JsonProperty("template")]
        public string Template { get; set; }

        /// <summary>
        /// 缓存文件目录
        /// </summary>
        [JsonProperty("cache")]
        public string Cache    { get; set; }
    }
}
