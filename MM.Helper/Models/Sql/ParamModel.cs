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
        public string Title           { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc            { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public object Value           { get; set; }

        /// <summary>
        /// 是否过滤
        /// </summary>
        public bool Filter            { get; set; } = false;

        /// <summary>
        /// 数据类型，String、Number、DateTime、Array、Object、
        /// </summary>
        public string Data_type       { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public string Form_type       { get; set; } = "";

        /// <summary>
        /// 子项
        /// </summary>
        public ParamModel Sub         { get; set; }

        /// <summary>
        /// 验证模型
        /// </summary>
        public ValidateModel Validate { get; set; }
    }
}

/*
    "title": "用户名",
    "value": "15817188815",
    "filter": false,
    "type": "string",
    "form_type": "input",
    "data_type": "digital",
    "sub": {},
    "model": {},
    "validate": {}
*/
