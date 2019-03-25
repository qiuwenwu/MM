using Newtonsoft.Json;

namespace MM.Helper.Models
{
    /// <summary>
    /// 数据表地图模型
    /// </summary>
    public class MapModel
    {
        /// <summary>
        /// 转换类型。db数据表类型，array数组型
        /// </summary>
        [JsonProperty("type")]
        public string Type   { get; set; } = "db";

        /// <summary>
        /// 数据表名称。 当转换类型是db类型时需要
        /// </summary>
        [JsonProperty("table")]
        public string Table  { get; set; } = "mm_user";

        /// <summary>
        /// 显示的字段名称
        /// </summary>
        [JsonProperty("Name")]
        public string Name   { get; set; } = "用户";

        /// <summary>
        /// 转换的主键字段
        /// </summary>
        [JsonProperty("ID")]
        public string ID     { get; set; } = "uid";

        /// <summary>
        /// 转换的名称字段
        /// </summary>
        [JsonProperty("field")]
        public string Field  { get; set; } = "name";

        /// <summary>
        /// 转换值。当转换方式为数组型时需要
        /// </summary>
        [JsonProperty("value")]
        public string Value  { get; set; } = "是,否"; //未设定,出售中,待处理

        /// <summary>
        /// 查询条件
        /// </summary>
        [JsonProperty("select")]
        public string Select { get; set; } = "";
    }
}
