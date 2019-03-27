using System.Globalization;
using System.Web;

namespace Newtonsoft.Json.Linq
{
    /// <summary>
    /// Json拓展函数
    /// </summary>
    public static class JsonExpand
    {
        /// <summary>
        /// 转为decimal类型
        /// </summary>
        /// <param name="jtk">科学记数法JToken</param>
        /// <returns>返回decimal类型</returns>
        public static decimal ToDecimal(this JToken jtk)
        {
            return decimal.Parse(jtk.ToString(), NumberStyles.Float);
        }

        /// <summary>
        /// 改变jarr里面的科学记数法为Decimal值
        /// </summary>
        /// <param name="jtk">原Json数组</param>
        /// <returns>返回改变后的Json数组</returns>
        public static JArray ChangeJArr(this object jtk)
        {
            var jarr = (JArray)jtk;
            for (var i = 0; i < jarr.Count; i++)
            {
                var jobj = new JObject();
                JObject o = (JObject)jarr[i];
                foreach (var item in o)
                {
                    var key = item.Key;
                    var value = item.Value;
                    if (key == "amount")
                    {
                        var d = value.ToDecimal();
                        jobj.Add(key, d);
                    }
                    else if (key == "fee")
                    {
                        jobj.Add(key, value.ToDecimal());
                    }
                    else
                    {
                        jobj.Add(key, value);
                    }
                }
                jarr[i] = jobj;
            }
            return jarr;
        }

        /// <summary>
        /// 转为decimal类型
        /// </summary>
        /// <param name="jtk">科学记数法JToken</param>
        /// <returns>返回decimal类型</returns>
        public static long ToLong(this JToken jtk)
        {
            return long.Parse(jtk.ToString());
        }

        /// <summary>
        /// URL参数转Json对象
        /// </summary>
        /// <param name="urlQuery">URL参数字符串</param>
        /// <param name="toLower">是否转为小写</param>
        /// <returns>返回json对象</returns>
        public static JObject UrlToJson(this string urlQuery, bool toLower = true) {
            var json = new JObject();
            var arr = urlQuery.Split('&');
            if (toLower)
            {
                foreach (var o in arr)
                {
                    var ar = o.Split('=');
                    if (ar.Length > 1)
                    {
                        json.Add(ar[0].ToLower(), HttpUtility.UrlDecode(ar[1]));
                    }
                }
            }
            else
            {
                foreach (var o in arr)
                {
                    var ar = o.Split('=');
                    if (ar.Length > 1)
                    {
                        json.Add(ar[0], HttpUtility.UrlDecode(ar[1]));
                    }
                }
            }
            return json;
        }
    }
}
