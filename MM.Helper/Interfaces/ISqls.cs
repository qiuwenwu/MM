using System.Collections.Generic;

namespace MM.Helper.Data
{
    /// <summary>
    /// 数据库操作类接口
    /// </summary>
    interface ISqls
    {
        #region 拓展
        /// <summary>
        /// 正则查询
        /// </summary>
        /// <param name="whereField">字段</param>
        /// <param name="match">正则</param>
        /// <param name="field">获取的字段 *号表示获取全部字段</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回Json格式字符串</returns>
        List<dynamic> Regexp(string whereField, string match, string field = "*", string orderBy = "");

        /// <summary>
        /// 统计记录个数
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="field">统计的字段</param>
        /// <returns>返回统计结果</returns>
        List<dynamic> Count(string whereStr, string field = null);
        #endregion

        /// <summary>
        /// 创建数据库
        /// </summary>
        int AddDB(string db = null);

        #region 操作表
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="fieldAndType">字段名加字段类型 例如 number INT(11), 需设主键时在前面加Key</param>
        /// <param name="table">表名</param>
        /// <returns>创建成功返回ture，失败返回false</returns>
        int AddTable(string fieldAndType, string table = null);

        /// <summary>
        /// 创建表带自动递增主键
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="table">表名</param>
        /// <returns>创建成功返回true，失败返回false</returns>
        int AddTableKey(string key, string table = null);

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns>创建成功返回ture，失败返回false</returns>
        int DelTable(string table = null);

        /// <summary>
        /// 获取所有表名
        /// </summary>
        /// <returns>获取成功返回所有表名</returns>
        List<dynamic> GetTables();

        /// <summary>
        /// 查询表项数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="filed">字段</param>
        /// <returns>返回表项数值</returns>
        int CountRow(string tableName = null, string filed = "*");

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="col">列值</param>
        /// <returns>添加成功返回true，失败返回false</returns>
        int AddCol(string col);

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="name">列名称</param>
        /// <param name="type">类型</param>
        /// <param name="len">长度</param>
        /// <returns>添加成功返回true，失败返回false</returns>
        int AddCol(string name, string type, int len = 0);

        /// <summary>
        /// 列名
        /// </summary>
        /// <param name="name">删除列</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        int DelCol(string name);
        #endregion
    }
}
