using Newtonsoft.Json;

namespace MM.Helper.Models
{
    /// <summary>
    /// 时间验证模型
    /// </summary>
    public class DateTimeModel
    {
        /// <summary>
        /// 最小时间，例如：1970-01-01 00:00:00
        /// </summary>
        [JsonProperty("min")]
        public string Min         { get; set; } = "1970-01-01 00:00:00";

        /// <summary>
        /// 最大时间，例如：2019-12-31 00:00:00
        /// </summary>
        [JsonProperty("max")]
        public string Max         { get; set; } // = DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss");

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message     { get; set; } = "{0}格式不正确，必须为“年-月-日 时:分:秒”格式，并且在{1}到{2}之间";

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message_min")]
        public string Message_min { get; set; } = "{0}格式不正确，必须为“年-月-日 时:分:秒”格式，并且是{1}之后的时间";

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message_max")]
        public string Message_max { get; set; } = "{0}格式不正确，必须为“年-月-日 时:分:秒”格式，并且是{1}之前的时间";
    }

    /// <summary>
    /// 字符串长度验证模型
    /// </summary>
    public class StrLenModel
    {
        /// <summary>
        /// 最小值
        /// </summary>
        [JsonProperty("min")]
        public int Min            { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        [JsonProperty("max")]
        public int Max            { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message     { get; set; } = "{0}必须在{1}-{2}个字符长度之间";

        /// <summary>
        /// 错误提示——最小
        /// </summary>
        [JsonProperty("message_min")]
        public string Message_min { get; set; } = "{0}必须小于{1}个字符";

        /// <summary>
        /// 错误提示——最大
        /// </summary>
        [JsonProperty("message_max")]
        public string Message_max { get; set; } = "{0}必须大于{1}个字符";
    }

    /// <summary>
    /// 数值范围验证模型
    /// </summary>
    public class RangeModel
    {
        /// <summary>
        /// 最小值
        /// </summary>
        [JsonProperty("min")]
        public decimal Min        { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        [JsonProperty("max")]
        public decimal Max        { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message     { get; set; } = "{0}数值范围必须在{1}-{2}之间";

        /// <summary>
        /// 错误提示——最小
        /// </summary>
        [JsonProperty("message_min")]
        public string Message_min { get; set; } = "{0}必须小于{1}";

        /// <summary>
        /// 错误提示——最大
        /// </summary>
        [JsonProperty("message_max")]
        public string Message_max { get; set; } = "{0}必须大于{1}";
    }

    /// <summary>
    /// 站外验证模型
    /// </summary>
    public class RemoteModel
    {
        /// <summary>
        /// 外网验证地址
        /// </summary>
        [JsonProperty("url")]
        public string Url     { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = "{0}不可用";
    }

    /// <summary>
    /// 非空验证模型
    /// </summary>
    public class NotEmptyModel
    {
        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = "{0}不能为空";
    }

    /// <summary>
    /// 规则验证模型（用于后缀名、特定格式、或正则验证）
    /// </summary>
    public class RegexModel
    {
        /// <summary>
        /// 格式
        /// </summary>
        [JsonProperty("format")]
        public string Format  { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = "{0}格式不正确";
    }

    /// <summary>
    /// 验证两个参数是否相同
    /// </summary>
    public class IdenticalModel
    {
        /// <summary>
        /// 比较字段
        /// </summary>
        [JsonProperty("field")]
        public string Field   { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = "{0}必须与{1}相同";
    }

    /// <summary>
    /// 验证两个参数是否不同
    /// </summary>
    public class DifferentModel
    {
        /// <summary>
        /// 比较字段
        /// </summary>
        [JsonProperty("field")]
        public string Field   { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = "{0}不能与{1}相同";
    }

    /// <summary>
    /// 验证参数后缀名
    /// </summary>
    public class ExtensionModel
    {
        /// <summary>
        /// 格式
        /// </summary>
        [JsonProperty("format")]
        public string Format  { get; set; } = "jpeg|png|gif|svg";

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = "{0}后缀名必须为{1}";
    }

    /// <summary>
    /// 验证参数类型 
    /// </summary>
    public class DataTypeModel
    {
        /// <summary>
        /// 格式，array、object、string、bool、dateTime、date、time、float、decimal、double、long、int
        /// </summary>
        [JsonProperty("format")]
        public string Format  { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = "{0}数据类型必须为{1}型";
    }

    /// <summary>
    /// 规则验证模型（用于后缀名、特定格式、或正则验证）
    /// </summary>
    public class RuleModel
    {
        /// <summary>
        /// 格式
        /// </summary>
        [JsonProperty("format")]
        public string Format  { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 外网验证地址
        /// </summary>
        [JsonProperty("url")]
        public string Url     { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        [JsonProperty("min")]
        public int Min        { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        [JsonProperty("max")]
        public int Max        { get; set; }
    }

    /// <summary>
    /// 比较验证模型（用于校验两个参数相同或不同）
    /// </summary>
    public class EqualModel
    {
        /// <summary>
        /// 比较的字段
        /// </summary>
        [JsonProperty("field")]
        public string Field   { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

/*
    "notEmpty": {
        "message": 
    },
    "date": {
        "format": "YYYY/MM/DD",
        "message": 
    },
    "remote": {
        "url": "/api/user/check",
        "message": 
    },
    "regex": {
        "format": "^[0-9]+$",
        "message": 
    },
    "stringLength": {
        "min": 0,
        "max": 255,
        "message": "{0}必须在{1}-{2}个字符长度之间"
    },
    "choice": {
        "min": 0,
        "max": 255,
        "message": "{0}必须在{1}-{2}的数值范围之间"
    },
    "identical": {
        "field": "password",
        "message": "{0}必须与{1}相同"
    },
    "different": {
        "field": "password",
        "message": "{0}不能和{1}相同"
    },
    "extension": {
        "format": "jpeg|png|gif",
        "message": "{0}后缀名必须为{1}"
    }
*/
