using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace MM.Helper.Data
{
    /// <summary>
    /// Csv文件帮助类
    /// </summary>
    public class Csv
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 当前路径
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// 导出报表为Csv
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="dt">DataTable</param>
        /// <param name="hasTitle">是否含表头</param>
        /// <returns>保存成功返回true，失败返回false</returns>
        public bool Save(string file, DataTable dt, bool hasTitle = true)
        {
            try
            {
                string strBufferLine = "";
                StreamWriter strmWriterObj = new StreamWriter(file, false, Encoding.UTF8);
                if (hasTitle)
                {
                    string columname = "";
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            columname += ",";
                        }
                        columname += dt.Columns[i].ColumnName;
                    }
                    strmWriterObj.WriteLine(columname);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                        {
                            strBufferLine += ",";
                        }
                        strBufferLine += dt.Rows[i][j].ToString();
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
                return true;
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 将Json保存为Excel
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="jArr">json对象数组</param>
        /// <param name="hasTitle">是否有标题</param>
        /// <returns>保存成功返回true，失败返回false</returns>
        public bool Save(string fileName, JArray jArr, bool hasTitle = true)
        {
            if (jArr == null)
            {
                return false;
            }
            else
            {
                return Save(fileName.ToFullName(Dir), jArr.ToString(), hasTitle);
            }
        }

        /// <summary>
        /// 将Json保存为Excel
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="jArrStr">json字符串</param>
        /// <param name="hasTitle">是否有标题</param>
        /// <returns>保存成功返回true，失败返回false</returns>
        public bool Save(string fileName, string jArrStr, bool hasTitle = true)
        {
            if (!string.IsNullOrEmpty(jArrStr) && jArrStr != "[]")
            {
                try
                {
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jArrStr);
                    return Save(fileName.ToFullName(Dir), dataTable, hasTitle);
                }
                catch (Exception ex)
                {
                    Ex = ex.Message;
                }
            }
            return false;
        }

        /// <summary>
        /// 将Csv读入DataTable
        /// </summary>
        /// <param name="fileName">csv文件路径</param>
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        /// <returns>返回数据库表</returns>
        public DataTable Load(string fileName, int n = 0)
        {
            DataTable dt = new DataTable();
            StreamReader reader = new StreamReader(fileName.ToFullName(Dir), Encoding.UTF8, false);
            int m = 0;
            reader.Peek();
            while (reader.Peek() > 0)
            {
                m += 1;
                string str = reader.ReadLine();
                if (m >= n + 1)
                {
                    string[] split = str.Split(',');

                    DataRow dr = dt.NewRow();
                    int i;
                    for (i = 0; i < split.Length; i++)
                    {
                        dr[i] = split[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// 执行导入
        /// </summary>
        /// <param name="fileName">文件名。绝对路径</param>
        /// <param name="typeName">返回的Table名称</param>
        /// <returns>DataTable</returns>
        public DataTable Load(string fileName, string typeName)
        {
            if (string.IsNullOrEmpty(fileName)) return null;

            string line = string.Empty;
            bool isReplace;
            int subBegion;
            int subEnd;
            DataTable table = new DataTable(typeName);
            using (StreamReader sr = new StreamReader(fileName.ToFullName(Dir), Encoding.Default))
            {
                //创建与数据源对应的数据列 
                line = sr.ReadLine();
                string[] split = line.Split(',');
                foreach (string colname in split)
                {
                    table.Columns.Add(colname, Type.GetType("System.String"));
                }
                //将数据填入数据表 
                int j = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    subEnd = 0;
                    subBegion = 0;
                    if (line.IndexOf('\"') > 0)
                    {
                        isReplace = true;
                    }
                    else
                    {
                        isReplace = false;
                    }
                    string itemString = string.Empty;
                    while (isReplace)
                    {
                        subBegion = line.IndexOf('\"');
                        subEnd = line.Length - 1;
                        if (line.Length - 1 > subBegion)
                        {
                            subEnd = line.IndexOf('\"', subBegion + 1);
                        }

                        if (subEnd - subBegion > 0)
                        {
                            itemString = line.Substring(subBegion, subEnd - subBegion + 1);
                            string oldItemString = itemString;
                            itemString = itemString.Replace(',', '|').Replace("\"", string.Empty);
                            line = line.Replace(oldItemString, itemString);
                        }

                        if (line.IndexOf('\"') == -1)
                        {
                            isReplace = false;
                        }
                    }
                    j = 0;
                    DataRow row = table.NewRow();
                    split = line.Split(',');
                    foreach (string colname in split)
                    {
                        row[j] = colname.Replace('|', ',');
                        j++;
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }
}
