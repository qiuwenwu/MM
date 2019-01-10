namespace MM.Helper.Models
{
    /// <summary>
    /// SQL并集模型
    /// </summary>
    public class UnionModel
    {
        /// <summary>
        /// 是否包含所有，不去重
        /// </summary>
        public bool All      { get; set; } = true;

        /// <summary>
        /// 关联的表名，多个表用“,”分隔
        /// </summary>
        public string Tables { get; set; }

        /// <summary>
        /// 联表规则，可带入公式
        /// </summary>
        public string Rules  { get; set; }
    }
}
