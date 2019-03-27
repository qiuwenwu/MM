using System.Collections.Generic;
using System.IO;

namespace MM.API
{
    /// <summary>
    /// 表单文件模型
    /// </summary>
    public class FormFileModel
    {
        /// <summary>
        /// 长度
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// 协议头
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string ContentDisposition { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 数据流
        /// </summary>
        public Stream Stream { get; set; }
    }
}
