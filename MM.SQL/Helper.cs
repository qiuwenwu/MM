using MM.Helper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MM.SQL
{
    /// <summary>
    /// SQL帮助类
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; set; } = string.Empty;

        #region 辅助
        /// <summary>
        /// 是否含有某些参数
        /// </summary>
        /// <param name="paramDt">参数集合</param>
        /// <param name="where">字典</param>
        /// <returns>含有返回true，不含有返回false</returns>
        public static bool HasParam(Dictionary<string, object> paramDt, Dictionary<string, string> where)
        {
            var bl = false;
            foreach (var key in where.Keys)
            {
                if (paramDt.ContainsKey(key))
                {
                    bl = true;
                    break;
                }
            }
            return bl;
        }

        /// <summary>
        /// 判断是否含有其他参数
        /// </summary>
        /// <param name="paramDt">参数集合</param>
        /// <param name="sqlDt">字典</param>
        /// <returns>含有返回true，不含有返回false</returns>
        public string NotParam(Dictionary<string, object> paramDt, Dictionary<string, ParamModel> sqlDt)
        {
            string name = null;
            foreach (var key in paramDt.Keys)
            {
                if (!sqlDt.ContainsKey(key))
                {
                    name = key;
                    break;
                }
            }
            return name;
        }

        /// <summary>
        /// 过滤参数
        /// </summary>
        /// <param name="paramDt">附加Sql语句</param>
        /// <param name="sqlDt">过滤器参数字典</param>
        /// <returns>返回sql拼接语</returns>
        public static string FilterQuery(Dictionary<string, string> paramDt, Dictionary<string, object> sqlDt)
        {
            var sql = "";
            if (sqlDt.Count == 0)
            {
                foreach (var val in paramDt.Values)
                {
                    sql += " AND " + val;
                }
            }
            else
            {
                foreach (var o in paramDt)
                {
                    if (!sqlDt.ContainsKey(o.Key))
                    {
                        sql += " AND " + o.Value;
                    }
                }
            }
            return sql;
        }

        /// <summary>
        /// 过滤参数
        /// </summary>
        /// <param name="paramDt">附加Sql语句</param>
        /// <param name="sqlDt">过滤器参数字典</param>
        /// <returns>返回sql拼接语</returns>
        public Dictionary<string, object> Filter(Dictionary<string, object> paramDt, Dictionary<string, ParamModel> sqlDt)
        {
            var dt = new Dictionary<string, object>();
            foreach (var o in paramDt)
            {
                var key = o.Key;
                if (sqlDt.ContainsKey(key))
                {
                    dt.Add(key, o.Value);
                }
            }
            return dt;
        }

        /// <summary>
        /// 过滤参数
        /// </summary>
        /// <param name="paramDt">参数字典</param>
        /// <param name="sql">sql对象</param>
        /// <returns>返回参数Json</returns>
        public static Dictionary<string, object> Filter(Dictionary<string, object> paramDt, SqlModel sql)
        {
            if (!string.IsNullOrEmpty(sql.Count))
            {
                var count = "count";
                if (sql.Count != "{0}")
                {
                    count = sql.Count;
                }
                paramDt.Remove(count);
            }
            paramDt.Remove("method");

            var field = "field";
            if (sql.Field != "{0}")
            {
                field = sql.Field;
            }
            paramDt.Remove(field);

            var table = "table";
            if (sql.Table != "{0}")
            {
                table = sql.Table;
            }
            paramDt.Remove(table);

            var sort = "sort";
            if (sql.Sort != "{0}")
            {
                sort = sql.Sort;
            }
            paramDt.Remove(sort);
            paramDt.Remove(sql.Page);
            paramDt.Remove(sql.PageSize);
            return paramDt;
        }
        #endregion
    }
}
