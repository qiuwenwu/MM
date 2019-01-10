namespace MM.Helper.Models
{
    /// <summary>
    /// 方法模型
    /// </summary>
    public class MethodModel
    {
        /// <summary>
        /// 必填字段，例如"`userName`,`password`"，为空则非必填
        /// </summary>
        public string Required      { get; set; }

        /// <summary>
        /// 获取的字段，用于查询、导入、导出时
        /// </summary>
        public string Field         { get; set; }
    }
}