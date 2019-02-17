using Newtonsoft.Json;
using System.Collections.Generic;

namespace MM.Helper.Models
{
    /// <summary>
    /// 参数模型
    /// </summary>
    public class ParamModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        public string Title             { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name              { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("description")]
        public string Description       { get; set; }

        /// <summary>
        /// 是否过滤该参数——当选择了过滤该参数，在验证后将会删除该参数。
        /// </summary>
        [JsonProperty("filter")]
        public bool Filter              { get; set; } = false;

        /// <summary>
        /// 分割字符串,用于多参数查询
        /// </summary>
        [JsonProperty("split")]
        public string Split             { get; set; } = "|";

        /// <summary>
        /// 是否空值验证
        /// </summary>
        [JsonProperty("dataType")]
        public DataTypeModel DataType   { get; set; } = new DataTypeModel();

        /// <summary>
        /// 是否空值验证
        /// </summary>
        [JsonProperty("notEmpty")]
        public NotEmptyModel NotEmpty   { get; set; }

        /// <summary>
        /// 时间验证
        /// </summary>
        [JsonProperty("dateTime")]
        public DateTimeModel DateTime   { get; set; }

        /// <summary>
        /// 远程验证
        /// </summary>
        [JsonProperty("remote")]
        public RemoteModel Remote       { get; set; }

        /// <summary>
        /// 正则验证
        /// </summary>
        [JsonProperty("remote")]
        public RegexModel Regex         { get; set; }

        /// <summary>
        /// 字符串验证
        /// </summary>
        [JsonProperty("strLen")]
        public StrLenModel StrLen       { get; set; }

        /// <summary>
        /// 范围值验证
        /// </summary>
        [JsonProperty("range")]
        public RangeModel Range         { get; set; }

        /// <summary>
        /// 两个参数是否相同
        /// </summary>
        [JsonProperty("identical")]
        public IdenticalModel Identical { get; set; }

        /// <summary>
        /// 两个参数是否不同
        /// </summary>
        [JsonProperty("different")]
        public DifferentModel Different { get; set; }

        /// <summary>
        /// 后缀名验证
        /// </summary>
        [JsonProperty("extension")]
        public ExtensionModel Extension { get; set; }

        /// <summary>
        /// 子验证模型，用于验证数组成员或对象
        /// </summary>
        [JsonProperty("subParam")]
        public Dictionary<string, ParamModel> SubParam { get; set; }
    }
}