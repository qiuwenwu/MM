using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace MM.Helper.Base
{
    /// <summary>
    /// 对象类
    /// </summary>
    public class Obj
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 获取对象类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回类型信息模型</returns>
        public Type GetType(object obj)
        {
            return obj.GetType();
        }

        /// <summary>
        /// 新建对象
        /// </summary>
        /// <param name="typeName">类型</param>
        /// <returns>返回对象型</returns>
        public object New(string typeName)
        {
            object obj = null;
            switch (typeName)
            {
                case "number":
                case "int":
                    obj = int.Parse("0");
                    break;
                case "long":
                    obj = long.Parse("0");
                    break;
                case "str":
                case "string":
                    obj = "";
                    break;
                case "float":
                    obj = float.Parse("0.0");
                    break;
                case "double":
                    obj = double.Parse("0.00");
                    break;
                case "list":
                    obj = new List<object>();
                    break;
                case "arr":
                    obj = new object[] { };
                    break;
                case "dict":
                    obj = new Dictionary<string, object>();
                    break;
                case "jarr":
                case "jarray":
                    obj = new JArray();
                    break;
                case "jobj":
                case "jobject":
                    obj = new JObject();
                    break;
                case "datatable":
                    obj = new DataTable();
                    break;
            }
            return obj;
        }

        /// <summary>
        /// 转为布尔型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回true/false</returns>
        public bool ToBool(object obj)
        {
            bool.TryParse(obj.ToString(), out bool bl);
            return bl;
        }

        /// <summary>
        /// 转为数字
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回数字型</returns>
        public int ToInt(object obj)
        {
            int.TryParse(obj.ToString(), out int n);
            return n;
        }

        /// <summary>
        /// 转为强名称对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="str">字符串</param>
        /// <returns>返回强名称对象</returns>
        public T ToObj<T>(string str)
        {
            return str.ToObj<T>();
        }

        /// <summary>
        /// 转为强名称对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="jToken">Json Token对象</param>
        /// <returns>返回强名称对象</returns>
        public T ToObj<T>(JToken jToken)
        {
            return jToken.ToObj<T>();
        }

        /// <summary>
        /// 转为强名称对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="jarr">json数组</param>
        /// <returns>返回强名称对象</returns>
        public T ToObj<T>(JArray jarr)
        {
            return jarr.ToObj<T>();
        }

        /// <summary>
        /// 转为强名称对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="jobj">json对象</param>
        /// <returns>返回强名称对象</returns>
        public T ToObj<T>(JObject jobj)
        {
            return jobj.ToObj<T>();
        }

        /// <summary>
        /// 转为强名称对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dict">字典</param>
        /// <returns>返回强名称对象</returns>
        public T ToObj<T>(Dictionary<string, object> dict) where T : new()
        {
            return dict.ToObj<T>();
        }

        /// <summary>
        /// 转为字典
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回强名称对象</returns>
        public Dictionary<string, object> ToDict(object obj)
        {
            return obj.ToDict();
        }
    }
}
