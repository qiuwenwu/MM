namespace MM.Models
{
    /// <summary>
    /// 信息模型
    /// </summary>
    public class InfoModel
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string App { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 使用说明
        /// </summary>
        public string Help  { get; set; }

        /// <summary>
        /// 目录
        /// </summary>
        public string Dir { get; set; }
    }
}