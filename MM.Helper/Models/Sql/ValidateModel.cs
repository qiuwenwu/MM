namespace MM.Helper.Models
{
    /// <summary>
    /// 参数验证模型
    /// </summary>
    public class ValidateModel
    {
        /// <summary>
        /// 是否空值验证
        /// </summary>
        public NotEmptyModel NotEmpty     { get; set; }

        /// <summary>
        /// 时间验证
        /// </summary>
        public DateModel Date             { get; set; }

        /// <summary>
        /// 外网验证
        /// </summary>
        public RemoteModel Remote         { get; set; }

        /// <summary>
        /// 正则验证
        /// </summary>
        public RuleModel Regex            { get; set; }

        /// <summary>
        /// 字符串验证
        /// </summary>
        public RangeModel StringLength    { get; set; }

        /// <summary>
        /// 范围值验证
        /// </summary>
        public RangeModel Choice          { get; set; }

        /// <summary>
        /// 两个参数是否相同
        /// </summary>
        public RuleModel Identical        { get; set; }

        /// <summary>
        /// 两个参数是否不同
        /// </summary>
        public RuleModel Different        { get; set; }

        /// <summary>
        /// 后缀名验证
        /// </summary>
        public RuleModel Extension        { get; set; }
    }

    /// <summary>
    /// 时间验证模型
    /// </summary>
    public class DateModel
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 最小时间，例如：1970-01-01 00:00:00
        /// </summary>
        public string Min     { get; set; }

        /// <summary>
        /// 最大时间，例如：2019-12-31 00:00:00
        /// </summary>
        public string Max     { get; set; }
    }

    /// <summary>
    /// 数值范围验证模型
    /// </summary>
    public class RangeModel
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public int Min        { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public int Max        { get; set; }
    }

    /// <summary>
    /// 站外验证模型
    /// </summary>
    public class RemoteModel
    {
        /// <summary>
        /// 外网验证地址
        /// </summary>
        public string Url     { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 非空验证模型
    /// </summary>
    public class NotEmptyModel
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 规则验证模型（用于后缀名、特定格式、或正则验证）
    /// </summary>
    public class RuleModel
    {
        /// <summary>
        /// 格式
        /// </summary>
        public string Format  { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 外网验证地址
        /// </summary>
        public string Url     { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public int Min        { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public int Max        { get; set; }
    }

    /// <summary>
    /// 比较验证模型（用于校验两个参数相同或不同）
    /// </summary>
    public class EqualModel {
        /// <summary>
        /// 比较的字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Message { get; set; }
    }
}

/*
    "notEmpty": {
        "message": "{0}不能为空"
    },
    "date": {
        "format": "YYYY/MM/DD",
        "message": "格式不正确，必须为 年/月/日 格式"
    },
    "remote": {
        "url": "/api/user/check",
        "message": "{0}不可用"
    },
    "regex": {
        "format": "^[0-9]+$",
        "message": "{0}格式不正确"
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
