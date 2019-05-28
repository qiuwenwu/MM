namespace MM.Crawler
{
    /// <summary>
    /// 抓包配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 抓取地址格式 例如:“http://www.elins.cn/home/article?id={id}”、“http://www.elins.cn/home/list?page={page}&size=10”、“http://www.elins.cn/home/news/{dateTime}/{id}.html”
        /// </summary>
        public string Url       { get; set; }

        /// <summary>
        /// 步值
        /// </summary>
        public NumberModel Step { get; set; } = new NumberModel();

        /// <summary>
        /// 上限
        /// </summary>
        public NumberModel Max  { get; set; } = new NumberModel() {  Date = 31, Id = 1000, Page = 100, Time = 60 };

        /// <summary>
        /// 类型。 list列表页、view页、form表单、file文件提交页
        /// </summary>
        public string Type      { get; set; } = "view";
    }

    /// <summary>
    /// 数值模型
    /// </summary>
    public class NumberModel
    {
        /// <summary>
        /// Id增减步值
        /// </summary>
        public int Id   { get; set; } = 1;

        /// <summary>
        /// 日期增减步值
        /// </summary>
        public int Date { get; set; } = 1;

        /// <summary>
        /// 时间增减步值
        /// </summary>
        public int Time { get; set; } = 1;

        /// <summary>
        /// 页码增减步值
        /// </summary>
        public int Page { get; set; } = 1;
    }
}
