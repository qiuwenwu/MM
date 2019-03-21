using MM.Helper.Infos;

namespace MM.Crawler
{
    /// <summary>
    /// 抓包索引目录
    /// </summary>
    public class Index
    {
        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <returns>返回版本信息模型</returns>
        public static DllInfo Info()
        {
            return new DllInfo() { Name = "超级美眉系列——抓包帮助类库" };
        }
    }
}
