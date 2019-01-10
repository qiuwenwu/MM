namespace System
{
    /// <summary>
    /// 时间拓展函数
    /// </summary>
    public static class TimeExpand
    {
        /// <summary>
        /// 转为秒数
        /// </summary>
        /// <param name="time">时间模型</param>
        /// <returns>返回秒数</returns>
        public static long ToStamp(this DateTime time)
        {
            return (time.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        /// <summary>
        /// 时间戳转时间类型
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns>返回时间类型</returns>
        public static DateTime ToTime(this long timeStamp)
        {
            var t = timeStamp * 10000000 + 621355968000000000;
            return new DateTime(t).AddHours(8);
        }

        /// <summary>
        /// 时间字符串转时间类型
        /// </summary>
        /// <param name="timeStr">时间字符串</param>
        /// <returns>返回时间类型</returns>
        public static DateTime ToTime(this string timeStr)
        {
            return DateTime.Parse(timeStr);
        }
    }
}
