using MM.Models;
using System;

namespace MM.Configs
{
    /// <summary>
    /// 公共配置
    /// </summary>
    public class ComConfig
    {
        /// <summary>
        /// 信息
        /// </summary>
        public InfoModel Info = new InfoModel();

        /// <summary>
        /// 脚本
        /// </summary>
        public ScriptModel Script { get; set; }

        /// <summary>
        /// 改变配置
        /// </summary>
        public void Change()
        {
            Info.Name = Info.Name.ToLower();
            Info.App = Info.App.ToLower();
            Info.Type = Info.Type.ToLower();
            if (Info.Dir == null && Script != null)
            {
                Info.Dir = Script.File.ToDir();
            }
        }
    }
}
