using Newtonsoft.Json;
using System.Collections.Generic;

namespace MM.Helper.Models
{
    /// <summary>
    /// SQL模型
    /// </summary>
    public class SqlModel
    {
        /// <summary>
        /// 增删改查判断的参数
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; } = "method";

        /// <summary>
        /// 数据表操作判断的参数
        /// </summary>
        [JsonProperty("table")]
        public string Table { get; set; } = "{0}";

        /// <summary>
        /// 查询当前页判断
        /// </summary>
        [JsonProperty("page")]
        public string Page { get; set; } = "page";

        /// <summary>
        /// 查询页数大小判断
        /// </summary>
        [JsonProperty("pageSize")]
        public string PageSize { get; set; } = "pagesize";

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty("sort")]
        public string Sort { get; set; } = "{0}";

        /// <summary>
        /// 默认排序
        /// </summary>
        [JsonProperty("sortDefault")]
        public string SortDefault { get; set; } = "";

        /// <summary>
        /// 查询的字段
        /// </summary>
        [JsonProperty("field")]
        public string Field { get; set; } = "{0}";

        /// <summary>
        /// 默认字段
        /// </summary>
        [JsonProperty("fieldDefault")]
        public string FieldDefault { get; set; }

        /// <summary>
        /// 是否统计查询字段
        /// </summary>
        [JsonProperty("count")]
        public string Count { get; set; } = "{0}";

        /// <summary>
        /// 分组
        /// </summary>
        [JsonProperty("groupBy")]
        public string GroupBy { get; set; } = "{0}";

        /// <summary>
        /// 多条件查询时参数分隔符
        /// </summary>
        [JsonProperty("separator")]
        public string Separator { get; set; } = "|";

        /// <summary>
        /// 预设添加、修改条件
        /// </summary>
        [JsonProperty("where")]
        public Dictionary<string, string> Where { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 查询项目
        /// </summary>
        [JsonProperty("item")]
        public Dictionary<string, string> Item { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 预设删除和查新方式
        /// </summary>
        [JsonProperty("query")]
        public Dictionary<string, string> Query { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 预设默认查询
        /// </summary>
        [JsonProperty("queryDefault")]
        public Dictionary<string, string> QueryDefault { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 更新方式
        /// </summary>
        [JsonProperty("update")]
        public Dictionary<string, string> Update { get; set; } = new Dictionary<string, string>();


        /// <summary>
        /// 过滤参数
        /// </summary>
        [JsonProperty("filter")]
        public string[] Filter { get; set; }

        /// <summary>
        /// 能否更改数据
        /// </summary>
        [JsonProperty("can")]
        public string Can { get; set; } = "add del set get export import";

        /// <summary>
        /// 是否转换
        /// </summary>
        [JsonProperty("convert")]
        public string Convert { get; set; } = "convert";

        /// <summary>
        /// 预设默认查询
        /// </summary>
        [JsonProperty("map")]
        public Dictionary<string, MapModel> Map { get; set; } = new Dictionary<string, MapModel>();

        /// <summary>
        /// 不可重复字段
        /// </summary>
        [JsonProperty("noRepeat")]
        public string NoRepeat { get; set; }

        /// <summary>
        /// 清除重复项
        /// </summary>
        [JsonProperty("delRepeat")]
        public DelRepeatModel DelRepeat { get; set; } = new DelRepeatModel();
    }

    /// <summary>
    /// 删除重复项模型
    /// </summary>
    public class DelRepeatModel {
        // SELECT `number` FROM `osl_number` GROUP BY `number` HAVING COUNT(`number`) > 1;
        // SELECT `nid` FROM `osl_number` WHERE `number` IN;

        /// <summary>
        /// 去重的字段
        /// </summary>
        [JsonProperty("groupBy")]
        public string GroupBy { get; set; } = "number";

        /// <summary>
        /// 排序（决定保留项）
        /// </summary>
        [JsonProperty("orderBy")]
        public string OrderBy { get; set; } = "`nid` ASC";

        /// <summary>
        /// 其他去重方法
        /// </summary>
        [JsonProperty("sql")]
        public string Sql { get; set; }

        /// <summary>
        /// 去重键名
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }
        //SELECT `number` FROM `osl_number` GROUP BY `number` HAVING COUNT(`number`) > 1;

    }
}
