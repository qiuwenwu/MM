using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MM.Helper.Data
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public class Json
    {
        private static readonly JsonSerializerSettings setting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        /// <summary>
        /// 新建Jarry对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>返回Jarry对象</returns>
        public JArray NewJArr(string jsonStr = "[]")
        {
            return JArray.Parse(jsonStr);
        }

        /// <summary>
        /// 新建JObject对象
        /// <param name="jsonStr">json字符串</param>
        /// </summary>
        /// <returns>返回JObject对象</returns>
        public JObject NewJObj(string jsonStr = "{}")
        {
            return JObject.Parse(jsonStr);
        }

        /// <summary>
        /// Url参数转Json
        /// </summary>
        /// <param name="urlQuery">Url参数</param>
        /// <param name="toLower">是否转为小写</param>
        /// <returns>返回Json对象</returns>
        public JObject FromUrl(string urlQuery, bool toLower = false)
        {
            return urlQuery.UrlToJson(toLower);
        }


        #region 操作json对象
        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>返回json对象</returns>
        public void Add(JObject jobj, string key, object value)
        {
            jobj.Add(key, value.ToString());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <returns>返回json对象</returns>
        public void Del(JObject jobj, string key)
        {
            jobj.Remove(key);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>返回json对象</returns>
        public void Set(JObject jobj, string key, object value)
        {
            jobj[key] = value.ToString();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="jobj">json对象</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>返回json对象</returns>
        public void AddOrSet(JObject jobj, string key, object value)
        {
            if (jobj.ContainsKey(key)) {
                jobj[key] = value.ToString();
            }
            else
            {
                jobj.Add(value.ToString());
            }
        }
        #endregion


        #region 操作json数组
        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="jarr">json对象</param>
        /// <param name="value">值</param>
        /// <returns>返回json对象</returns>
        public void Add(JArray jarr, object value)
        {
            jarr.Add(value);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="jarr">json对象</param>
        /// <param name="index">键</param>
        /// <returns>返回json对象</returns>
        public void Del(JArray jarr, int index)
        {
            jarr.Remove(index);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="jarr">json对象</param>
        /// <param name="index">键</param>
        /// <param name="value">值</param>
        /// <returns>返回json对象</returns>
        public void Set(JArray jarr, int index, object value)
        {
            jarr[index] = value.ToString();
        }
        #endregion


        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="format">是否格式化</param>
        /// <param name="noNull">是否去除空值</param>
        /// <returns>返回json字符串</returns>
        public string Dumps(object obj, bool format = false, bool noNull = true)
        {
            return obj.ToJson(format, noNull);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="str">json字符串</param>
        /// <returns>返回对象</returns>
        public object Loads(string str)
        {
            return JsonConvert.DeserializeObject(str);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="str">json字符串</param>
        /// <returns>返回对象</returns>
        public T Loads<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
