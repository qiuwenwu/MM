using MM.Helper.Infos;

namespace MM.SQL
{
    /// <summary>
    /// 数据库边界操作索引目录
    /// </summary>
    public class Indexer
    {
        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <returns>返回版本信息模型</returns>
        public static DllInfo Info()
        {
            return new DllInfo() { Name = "超级美眉系列——数据库帮助类库" };
        }
    }
}
