using System;

namespace MM
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class MainCache : Cache
    {
        /// <summary>
        /// 配置参数
        /// </summary>
        internal static Config _Config = new Config();

        /// <summary>
        /// 配置参数
        /// </summary>
        public Config Config           { get { return _Config; } set { _Config = value; } }

        /// <summary>
        /// 接口驱动
        /// </summary>
        public API.Drive Api           { get; set; } = new API.Drive();

        /// <summary>
        /// 事件驱动
        /// </summary>
        public Event.Drive Event       { get; set; } = new Event.Drive();

        /// <summary>
        /// 事件驱动
        /// </summary>
        public Plugin.Drive Plugin     { get; set; } = new Plugin.Drive();

        /// <summary>
        /// 事件驱动
        /// </summary>
        public Task.Drive Task         { get; set; } = new Task.Drive();
    }
}
