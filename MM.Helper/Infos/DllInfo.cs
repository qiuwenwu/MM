namespace MM.Helper.Infos
{
    /// <summary>
    /// 版本信息
    /// </summary>
    public class DllInfo
    {
        /// <summary>
        /// 版本信息
        /// </summary>
        public string Name         { get; set; } = "超级美眉系列——帮助类库";

        /// <summary>
        /// 版本信息
        /// </summary>
        public string Version      { get; set; } = "1.0";

        /// <summary>
        /// 作者
        /// </summary>
        public string Author       { get; set; } = "邱文武";

        /// <summary>
        /// 版权归属
        /// </summary>
        public string Copyright    { get; set; } = "此类库版权归 邱文武 所有";

        /// <summary>
        /// 联系信息
        /// </summary>
        public ContactInfo Contact { get; set; } = new ContactInfo();
    }
}
