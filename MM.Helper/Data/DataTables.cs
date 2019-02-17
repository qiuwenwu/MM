using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace MM.Helper.Data
{
    /// <summary>
    /// 数据表帮助类
    /// </summary>
    public class DataTables
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="type">类型名称</param>
        /// <returns>返回数据类型</returns>
        public Type GetType(string type)
        {
            Type tp = null;
            type = type.ToLower();
            switch (type)
            {
                case "number":
                case "int":
                    int a = 1;
                    tp = a.GetType();
                    break;
                case "long":
                    long b = 1;
                    tp = b.GetType();
                    break;
                case "str":
                case "string":
                    string c = "c";
                    tp = c.GetType();
                    break;
                case "float":
                    float e = 1.111F;
                    tp = e.GetType();
                    break;
                case "double":
                    double f = 1.11;
                    tp = f.GetType();
                    break;
                case "list":
                    var list = new List<object>();
                    tp = list.GetType();
                    break;
                case "arr":
                    var arr = new object[] { };
                    tp = arr.GetType();
                    break;
                case "dict":
                    var dt = new Dictionary<string, object>();
                    tp = dt.GetType();
                    break;
                default:
                    tp = null;
                    break;
            }
            return tp;
        }

        /// <summary>
        /// 分解数据表
        /// </summary>
        /// <param name="originalTab">需要分解的表</param>
        /// <param name="rowsNum">每个表包含的数据量</param>
        /// <returns>返回数据表集合</returns>
        public List<DataTable> Split(DataTable originalTab, int rowsNum)
        {
            //获取所需创建的表数量
            int tableNum = originalTab.Rows.Count / rowsNum;

            //获取数据余数
            int remainder = originalTab.Rows.Count % rowsNum;

            var ds = new List<DataTable>();

            //如果只需要创建1个表，直接将原始表存入DataSet
            if (tableNum == 0)
            {
                ds.Add(originalTab);
            }
            else
            {
                var dt = new DataTable();
                foreach (DataColumn dc in originalTab.Columns)
                {
                    dt.Columns.Add(dc.ColumnName, dc.DataType);
                }
                //保存原始列到新表中。
                for (int i = 0; i < tableNum; i++)
                {
                    var newDt = dt.Copy();
                    var index = i * rowsNum;    //循环的开始位置
                    var end = (i + 1) * rowsNum; //循环的结束位置
                    for (int j = index; j < end; j++)
                    {
                        newDt.ImportRow(originalTab.Rows[j]);
                    }
                    ds.Add(newDt);
                }
                if (remainder > 0)
                {
                    var newDt = dt.Copy();
                    var index = rowsNum * tableNum;    //循环的开始位置
                    var end = index + remainder;       //循环的结束位置
                    for (int k = index; k < end; k++)
                    {
                        newDt.ImportRow(originalTab.Rows[k]);
                    }
                    ds.Add(newDt);
                }
            }
            return ds;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="jArrStr">数据</param>
        /// <returns>返回数据表</returns>
        public DataTable ToTable(string jArrStr)
        {
            if (!string.IsNullOrEmpty(jArrStr))
            {
                try
                {
                    return JsonConvert.DeserializeObject<DataTable>(jArrStr);
                }
                catch (Exception ex)
                {
                    Ex = ex.Message;
                }
            }
            return null;
        }

        /// <summary>
        /// 设置数据表排序
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="arr">排序方式</param>
        /// <returns>返回数据表</returns>
        public DataTable Sort(DataTable dataTable, string[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                var key = arr[i];
                if (dataTable.Columns.Contains(key))
                {
                    dataTable.Columns[key].SetOrdinal(i);
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 设置数据表排序
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="jarrStr">排序方式（字符串）,多个要进行排序的字段用,分隔</param>
        /// <returns>返回数据表</returns>
        public DataTable Sort(DataTable dataTable, string jarrStr)
        {
            if (string.IsNullOrEmpty(jarrStr))
            {
                return dataTable;
            }
            else
            {
                var arr = jarrStr.Split(',');
                return Sort(dataTable, arr);
            }
        }

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="field">字段名</param>
        /// <param name="type">字段类型</param>
        /// <returns>返回添加后的数据表</returns>
        public DataTable Add(DataTable dataTable, string field, string type)
        {
            if (dataTable == null)
            {
                return null;
            }
            dataTable.Columns.Add(field, GetType(type));
            return dataTable;
        }

        /// <summary>
        /// 判断是否有字段
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="field">字段名</param>
        /// <returns>返回添加后的数据表</returns>
        public bool Has(DataTable dataTable, string field)
        {
            if (dataTable.Columns.Contains(field))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="field">字段名</param>
        /// <returns>返回删除后的数据表</returns>
        public DataTable Del(DataTable dataTable, string field)
        {
            if (dataTable == null)
            {
                return null;
            }
            dataTable.Columns.Remove(field);
            return dataTable;
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="index">索引</param>
        /// <returns>返回删除后的数据表</returns>
        public DataTable Del(DataTable dataTable, int index)
        {
            if (dataTable == null)
            {
                return null;
            }
            dataTable.Columns.RemoveAt(index);
            return dataTable;
        }
    }
}
