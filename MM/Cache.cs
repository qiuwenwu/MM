using MM.Drives;

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
        public Config Config { get { return _Config; } set { _Config = value; } }

        /// <summary>
        /// 接口驱动
        /// </summary>
        public ApiDrive Api       { get; set; } = new ApiDrive();

        /// <summary>
        /// 指令驱动
        /// </summary>
        public CmdDrive Cmd       { get; set; } = new CmdDrive();

        /// <summary>
        /// RPC指令驱动
        /// </summary>
        public RpcDrive Rpc       { get; set; } = new RpcDrive();

        /// <summary>
        /// 事件驱动
        /// </summary>
        public EventDrive Event       { get; set; } = new EventDrive();

        /// <summary>
        /// 插件驱动字典
        /// </summary>
        public Dictionary<string, PluginDrive> PluginDt { get; set; } = new Dictionary<string, PluginDrive>();

        /// <summary>
        /// 任务驱动字典
        /// </summary>
        public Dictionary<string, TaskDrive> TaskDt     { get; set; } = new Dictionary<string, TaskDrive>();
    }
}
