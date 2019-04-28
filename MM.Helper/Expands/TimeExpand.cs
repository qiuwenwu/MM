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
        /// <param name="timestamp">时间戳</param>
        /// <returns>返回时间类型</returns>
        public static DateTime ToTime(this long timestamp)
        {
            var t = timestamp * 10000000 + 621355968000000000;
            return new DateTime(t).AddHours(8);
        }

        /// <summary>
        /// 时间字符串转时间类型
        /// </summary>
        /// <param name="str">时间字符串</param>
        /// <returns>返回时间类型</returns>
        public static DateTime ToTime(this string str)
        {
            return DateTime.Parse(str);
        }

        /// <summary>
        /// 增减时间
        /// </summary>
        /// <param name="time">当前时间</param>
        /// <param name="n">添加的时长</param>
        /// <param name="type">添加的类型</param>
        /// <returns>返回增减后的时间</returns>
        public static DateTime Add(this DateTime time, int n, string type = "second")
        {
            DateTime time_new;
            switch (type)
            {
                case "year":
                    time_new = time.AddYears(n);
                    break;
                case "month":
                    time_new = time.AddMonths(n);
                    break;
                case "day":
                    time_new = time.AddDays(n);
                    break;
                case "hour":
                    time_new = time.AddHours(n);
                    break;
                case "minute":
                    time_new = time.AddMinutes(n);
                    break;
                default:
                    time_new = time.AddSeconds(n);
                    break;
            }
            return time_new;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式 - 取反
        /// </summary>
        /// <param name="time">时间对象</param>
        /// <returns>返回时间戳</returns>
        public static long ToStampB(this DateTime time)
        {
            return (621355968000000000 - time.ToUniversalTime().Ticks) / 10000000;
        }

        /// <summary>
        /// 比较相差多少天
        /// </summary>
        /// <param name="dt1">时间1</param>
        /// <param name="dt2">时间2</param>
        /// <param name="timeType">时间差</param>
        /// <returns>返回相差天数</returns>
        public static long Interval(this DateTime dt1, DateTime dt2, string timeType = "second")
        {
            var time = dt1 - dt2;
            long n;
            switch (timeType)
            {
                case "day":
                    n = time.Ticks / 864000000000;
                    break;
                case "hour":
                    n = time.Ticks / 36000000000;
                    break;
                case "minute":
                    n = time.Ticks / 600000000;
                    break;
                default:
                    n = time.Ticks / 10000000;
                    break;
            }
            return n;
        }


        /// <summary>
        /// 时间转字符串
        /// </summary>
        /// <param name="time">时间类型</param>
        /// <param name="format">格式</param>
        /// <returns>返回时间格式字符串</returns>
        public static string ToStr(this DateTime time, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return time.ToString(format);
        }
    }
}
