namespace MM.API
{
    /// <summary>
    /// cookie模型
    /// </summary>
    public class CookieModel
    {
        /// <summary>
        /// cookie值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// cookie参数
        /// </summary>
        public CookieOptionsModel Options { get; set; }
    }

    /// <summary>
    /// cookie参数模型
    /// </summary>
    public class CookieOptionsModel
    {
        /// <summary>
        /// 作用域
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 指定路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 有效期时长
        /// </summary>
        public int Expires { get; set; } = 0;

        /// <summary>
        /// 是否安全模型
        /// </summary>
        public bool Secure { get; set; } = false;

        /// <summary>
        /// 同一地点，0任意、1宽松、2严谨
        /// </summary>
        public int SameSite { get; set; } = 0;

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool HttpOnly { get; set; } = false;
        /// <summary>
        /// 最大有效期
        /// </summary>
        public long MaxAge { get; set; } = 0;

        /// <summary>
        /// 是否强制cookie，默认为false
        /// </summary>
        public bool IsEssential { get; set; } = false;
    }
}
