﻿namespace MM.Crawler
{
    /// <summary>
    /// 抓包驱动
    /// </summary>
    public class Drive
    {
        /// <summary>
        /// 检索文件拓展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Drive()
        {
            Extension = "crawler.json";
        }
    }
}
