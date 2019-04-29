using Newtonsoft.Json;
using System;

namespace MM.Common
{
    /// <summary>
    /// 公共配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 信息
        /// </summary>
        [JsonProperty("info")]
        public Info Info = new Info();

        /// <summary>
        /// 脚本
        /// </summary>
        [JsonProperty("script")]
        public Script Script { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [JsonProperty("file")]
        public string File   { get; set; }

        /// <summary>
        /// 改变配置
        /// </summary>
        public void Change(string file = "")
        {
            var dir = "";
            if (!string.IsNullOrEmpty(file))
            {
                File = file;
                dir = file.ToDir();
            }
            Info.App = Info.App.ToLower();
            Info.Type = Info.Type.ToLower();
            Info.Name = Info.Name.ToLower();
            if (Script != null)
            {
                Script.File = Script.File.ToFullName(dir);
                if (Info != null)
                {
                    if (Info.Dir == null)
                    {
                        Info.Dir = Script.File.ToDir();

                    }
                    Info.Dir = Info.Dir.ToFullName(dir);
                }
            }
        }
    }
}

