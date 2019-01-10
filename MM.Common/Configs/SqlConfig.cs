using MM.Helper.Models;

namespace MM.Common.Configs
{
    /// <summary>
    /// 数据库配置模型
    /// </summary>
    public class SqlConfig
    {
        /// <summary>
        /// SQL基础
        /// </summary>
        public SqlModel Default   { get; set; } = new SqlModel();

        /// <summary>
        /// 差集
        /// </summary>
        public ExceptModel Except { get; set; }

        /// <summary>
        /// 全集
        /// </summary>
        public FullModel Full     { get; set; }

        /// <summary>
        /// 交集
        /// </summary>
        public JoinModel Inner    { get; set; }

        /// <summary>
        /// 左集
        /// </summary>
        public JoinModel Left     { get; set; }

        /// <summary>
        /// 右集
        /// </summary>
        public JoinModel Right    { get; set; }

        /// <summary>
        /// 连表
        /// </summary>
        public UnionModel Union   { get; set; } = new UnionModel();
    }
}
