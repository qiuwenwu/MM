using System;
using System.Collections.Generic;
using System.Linq;

namespace MM.Helper.Data
{
    /// <summary>
    /// Mysql高级帮助类
    /// </summary>
    public class MysqlS : Mysql, ISqls
    {
        #region 拓展
        /// <summary>
        /// 统计记录个数
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="field">统计的字段</param>
        /// <returns>返回统计结果</returns>
        public List<dynamic> Count(string whereStr, string field = null)
        {
            string countField = "count(1) AS counts";
            if (!string.IsNullOrEmpty(field))
            {
                countField = field + "," + countField;
                field = string.Format(" GROUP BY {0}", field);
            }
            if (!string.IsNullOrEmpty(whereStr))
            {
                whereStr = "WHERE " + whereStr;
            }
            string sql = string.Format("SELECT {0} FROM `{1}` {2}{3}", countField, Table, whereStr, field);
            return Query(sql);
        }

        /// <summary>
        /// 正则查询
        /// </summary>
        /// <param name="whereField">字段</param>
        /// <param name="match">正则</param>
        /// <param name="field">获取的字段 *号表示获取全部字段</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回Json格式字符串</returns>
        public List<dynamic> Regexp(string whereField, string match, string field = "*", string orderBy = "")
        {
            if (orderBy != "")
            {
                orderBy = " ORDER BY " + orderBy;
            }
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2} REGEXP {3} {4};", field, Table, whereField, match, orderBy);
            return Query(sql);
        }
        #endregion

        /// <summary>
        /// 创建数据库
        /// </summary>
        public int AddDB(string db = null)
        {
            return Execute(string.Format("CREATE DATABASE IF NOT EXISTS {0} DEFAULT CHARSET utf8mb4 COLLATE utf8mb4_general_ci;", db));
        }

        #region 操作表
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="fieldAndType">字段名加字段类型 例如 number INT(11), 需设主键时在前面加Key</param>
        /// <param name="table">表名</param>
        /// <returns>创建成功返回ture，失败返回false</returns>
        public int AddTable(string fieldAndType, string table = null)
        {
            if (table != null)
            {
                Table = table;
            }
            string sql = string.Format("CREATE TABLE IF NOT EXISTS `{0}` ({1}); ", Table, fieldAndType);
            return Execute(sql);
        }

        /// <summary>
        /// 创建表带自动递增主键
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="table">表名</param>
        /// <returns>创建成功返回true，失败返回false</returns>
        public int AddTableKey(string key, string table = null)
        {
            return AddTable(key + " INTEGER PRIMARY KEY AUTOINCREMENT", table);
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns>创建成功返回ture，失败返回false</returns>
        public int DelTable(string table = null)
        {
            if (table != null)
            {
                Table = table;
            }
            string sql = string.Format("DROP TABLE IF EXISTS `{0}`; ", Table);
            return Execute(sql);
        }

        /// <summary>
        /// 获取所有表名
        /// </summary>
        /// <returns>获取成功返回所有表名</returns>
        public List<dynamic> GetTables()
        {
            return Query(string.Format("select table_name as `table` from information_schema.tables where table_schema='{0}' AND table_type='base table';", Database));
        }

        /// <summary>
        /// 查询表项数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="filed">字段</param>
        /// <returns>返回表项数值</returns>
        public int CountRow(string tableName = null, string filed = "*")
        {
            var n = -1;
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = Table;
            }
            try
            {
                string sql = string.Format("SELECT {0} FROM {1}", filed, tableName);
                n = Query(sql).Count();
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="col">列值</param>
        /// <returns>添加成功返回true，失败返回false</returns>
        public int AddCol(string col)
        {
            var sql = string.Format("ALTER TABLE `{0}` ADD COLUMN {1};", Table, col);
            return Execute(sql);
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="name">列名称</param>
        /// <param name="type">类型</param>
        /// <param name="len">长度</param>
        /// <returns>添加成功返回true，失败返回false</returns>
        public int AddCol(string name, string type, int len = 0)
        {
            var defaultVal = "";
            switch (type.ToLower())
            {
                case "datetime":
                    defaultVal = " default '1970-01-01 00:00:00'";
                    break;
                case "string":
                case "varchar":
                    defaultVal = " default ''";
                    break;
                case "date":
                    defaultVal = " default '1970-01-01'";
                    break;
                case "float":
                case "double":
                case "int":
                    defaultVal = " default 0";
                    break;
                case "boolean":
                    defaultVal = " default false";
                    break;
            }
            var col = "";
            if (len > 0)
            {
                col = string.Format("`{0}` {1}({2}){3}", name, type, len, defaultVal);
            }
            else
            {
                col = string.Format("`{0}` {1}{2}", name, type, defaultVal);
            }
            return AddCol(col);
        }

        /// <summary>
        /// 列名
        /// </summary>
        /// <param name="name">删除列</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        public int DelCol(string name)
        {
            var sql = string.Format("ALTER TABLE `{0}` DROP COLUMN `{1}`;", Table, name);
            return Execute(sql);
        }
        #endregion

    }
}
