namespace MM.Helper.Models
{
    /// <summary>
    /// SQL基础模型
    /// </summary>
    public class SqlModel
    {
        /// <summary>
        /// 操作的数据表
        /// </summary>
        public string Table       { get; set; }

        /// <summary>
        /// 添加数据，为空则不支持添加数据
        /// </summary>
        public MethodModel Add    { get; set; }

        /// <summary>
        /// 修改数据，为空则不支持修改数据
        /// </summary>
        public MethodModel Set    { get; set; }

        /// <summary>
        /// 获取数据，为空则不支持获取数据
        /// </summary>
        public MethodModel Get    { get; set; }

        /// <summary>
        /// 删除数据，为空则不支持删除数据
        /// </summary>
        public MethodModel Del    { get; set; }

        /// <summary>
        /// 查询列表，为空则不支持查询列表
        /// </summary>
        public MethodModel List   { get; set; }

        /// <summary>
        /// 导入数据，为空则不支持导入数据
        /// </summary>
        public MethodModel Import { get; set; }

        /// <summary>
        /// 导出数据，为空则不支持导出数据
        /// </summary>
        public MethodModel Export { get; set; }
    }
}
/*
    "table": "user",
    "method": {
        "add": {
            "required": "`userName`,`password`",
            "callback": {
                "file": "/user.py",
                "fun": "add"
            }
        },
        "del": {
            "required": "`uid`"
        },
        "set": {
            "required": "*"
        },
        "get": {
            "required": "*",
            "field": "`uid`,`userName`,`nickName`,`group`,`sex`"
        },
        "list": {
            "required": "*",
            "field": "`uid`,`nickName`,`group`"
        },
        "import": {
            "required": "*",
            "field": "`uid`,`userName`,`nickName`,`group`,`sex`"
        },
        "export": {
            "required": "*",
            "field": "`uid`,`userName`,`nickName`,`group`,`sex`"
        }
*/
