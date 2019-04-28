using System;
using System.Globalization;

namespace MM.Helper.Base
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public class Time
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns>返回当前时间</returns>
        public DateTime Now()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns>返回当前时间</returns>
        public string Str()
        {
            return DateTime.Now.ToStr();
        }

        /// <summary>
        /// 取当前时间的UNIX时间戳
        /// </summary>
        /// <returns>UNIX时间戳</returns>
        public long Stamp()
        {
            var time = DateTime.Now;
            return time.ToStamp();
        }

        /// <summary>
        /// 转为秒数
        /// </summary>
        /// <param name="time">时间模型</param>
        /// <returns>返回秒数</returns>
        public long ToStamp(DateTime time)
        {
            return time.ToStamp();
        }

        /// <summary>
        /// 时间戳转时间类型
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <returns>返回时间类型</returns>
        public DateTime ToTime(long timestamp)
        {
            return timestamp.ToTime();
        }

        /// <summary>
        /// 时间字符串转时间类型
        /// </summary>
        /// <param name="str">时间字符串</param>
        /// <returns>返回时间类型</returns>
        public DateTime ToTime(string str)
        {
            return str.ToTime();
        }

        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="d1">要参与计算的其中一个日期</param>
        /// <param name="d2">要参与计算的另一个日期</param>
        /// <returns>一个表示日期间隔的TimeSpan类型</returns>
        public TimeSpan IntervalS(DateTime d1, DateTime d2)
        {
            return d1 - d2;
        }

        /// <summary>
        /// 字符串转时间
        /// </summary>
        /// <param name="str">时间字符串</param>
        /// <returns>返回时间</returns>
        public DateTime ToTimeS(string str)
        {
            DateTime dt;
            if (str.IndexOf("-") != -1)
            {
                return Convert.ToDateTime(str);
            }
            else if (str.IndexOf("/") != -1)
            {
                DateTimeFormatInfo dtFormat = new DateTimeFormatInfo()
                {
                    ShortDatePattern = "yyyy/MM/dd"
                };
                dt = Convert.ToDateTime(str, dtFormat);
            }
            else
            {
                dt = DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.CurrentCulture);
            }
            return dt;
        }

        /// <summary>
        /// 时间转字符串
        /// </summary>
        /// <param name="time">时间类型</param>
        /// <param name="format">格式</param>
        /// <returns>返回时间格式字符串</returns>
        public string ToStr(DateTime time, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return time.ToString(format);
        }

        /// <summary>
        /// 比较相差多少天
        /// </summary>
        /// <param name="dt1">时间1</param>
        /// <param name="dt2">时间2</param>
        /// <param name="timeType">时间差</param>
        /// <returns>返回相差天数</returns>
        public long Interval(DateTime dt1, DateTime dt2, string timeType = "second")
        {
            return dt1.Interval(dt2, timeType);
        }
    }
}
