using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace System
{
    /// <summary>
    /// 对象拓展函数
    /// </summary>
    public static class ObjectExpand
    {
        private static readonly JsonSerializerSettings jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include };

        private static readonly JsonSerializerSettings jsonSetting_null = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        /// <summary>
        /// 错误提示
        /// </summary>
        public static string Ex { get; private set; }

        /// <summary>
        /// 转为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="format">是否格式化</param>
        /// <param name="noNull">是否去除空值</param>
        /// <returns>返回序列化后的字符串</returns>
        public static string ToJson(this object obj, bool format = false, bool noNull = true)
        {
            string str = "{}";
            if (obj != null)
            {
                if (format)
                {
                    if (noNull)
                    {
                        str = JsonConvert.SerializeObject(obj, Formatting.Indented, jsonSetting_null);
                    }
                    else
                    {
                        str = JsonConvert.SerializeObject(obj, Formatting.Indented, jsonSetting);
                    }
                }
                else
                {
                    if (noNull)
                    {
                        str = JsonConvert.SerializeObject(obj, Formatting.None, jsonSetting_null);
                    }
                    else
                    {
                        str = JsonConvert.SerializeObject(obj, Formatting.None, jsonSetting);
                    }
                }

                if (str.Contains("__dict__"))
                {
                    var jObject = new JObject();
                    if (str.Contains("data"))
                    {
                        jObject = JObject.FromObject(obj);
                        var token = jObject["data"]["__dict__"];
                        jObject["data"] = token;

                    }
                    else
                    {
                        jObject = JObject.FromObject(obj);
                        var token = jObject["__dict__"];
                        jObject = (JObject)token;
                    }

                    if (!format)
                    {
                        var jobj = new JObject();
                        foreach (var o in jObject)
                        {
                            var value = o.Value;
                            if (value != null)
                            {
                                jobj.Add(o.Key, value);
                            }
                        }
                        jObject = jobj;
                    }
                    str = jObject.ToString();
                }
            }
            return str;
        }

        /// <summary>
        /// 转为强名称对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">原对象</param>
        /// <returns>返回强名称对象</returns>
        public static T ToObj<T>(this object obj)
        {
            var name = obj.GetType().Name.ToLower();
            if (name == "jobject" || name == "jtoken" || name == "jarray")
            {
                var jsonStr = obj.ToString();
                return JsonConvert.DeserializeObject<T>(jsonStr);
            }
            else if (name == "string")
            {
                return JsonConvert.DeserializeObject<T>(obj.ToString());
            }
            else
            {
                return (T)obj;
            }
        }

        /// <summary>
        /// 转为decimal类型
        /// </summary>
        /// <param name="str">科学记数法字符串</param>
        /// <returns>返回decimal类型</returns>
        public static decimal ToDecimal(this object str)
        {
            return decimal.Parse(str.ToString(), NumberStyles.Float);
        }

        /// <summary>
        /// 转为decimal类型
        /// </summary>
        /// <param name="str">科学记数法字符串</param>
        /// <returns>返回decimal类型</returns>
        public static int ToInt(this object str)
        {
            int.TryParse(str.ToString(), out var n);
            return n;
        }

        /// <summary>
        /// 转为decimal类型
        /// </summary>
        /// <param name="str">科学记数法字符串</param>
        /// <returns>返回decimal类型</returns>
        public static long ToLong(this object str)
        {
            long.TryParse(str.ToString(), out var n);
            return n;
        }


        #region xml
        /// <summary>
        /// 转为xml字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回xml字符串</returns>
        public static string ToXml(this object obj)
        {
            Type type = obj.GetType();
            MemoryStream stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);

            //序列化对象
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");  //清除前缀和命名空间
            xml.Serialize(stream, obj, ns);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string str = sr.ReadToEnd();
            sr.Dispose();
            stream.Dispose();
            return str;
        }

        /// <summary>
        /// 转为xml字符串——特别方法
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="obj">对象</param>
        /// <returns>返回xml字符串</returns>
        public static string ToXmlS(this object obj, string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                name = obj.GetType().Name;
            }
            var str = SerializerB(obj);
            return string.Format("<{0}>{1}\r</{2}>", name, str, name);
        }

        private static string XmlLine(string tag, object value)
        {
            var s = "";
            if (value != null)
            {
                var vName = value.GetType().Name.ToLower();
                if (vName == "string")
                {
                    if (value == null)
                    {
                        s = string.Format("<{0}></{1}>", tag, tag);
                    }
                    else
                    {
                        var str = value.ToString();
                        if (str == "")
                        {
                            s = string.Format("<{0}></{1}>", tag, tag);
                        }
                        else
                        {
                            s = string.Format("<{0}><![CDATA[{1}]]></{2}>", tag, str, tag);
                        }
                    }
                }
                else if (vName == "boolean")
                {
                    s = string.Format("<{0}>{1}</{2}>", tag, value.ToString().ToLower(), tag);
                }
                else if (vName == "int64" || vName == "int32" || vName == "double" || vName == "decimal" || vName == "single")
                {
                    s = string.Format("<{0}>{1}</{2}>", tag, value, tag);
                }
                else if (vName == "datetime")
                {
                    var dt = (DateTime)value;
                    s = string.Format("<{0}>{1}</{2}>", tag, dt.ToString("yyyy-MM-dd HH:mm:ss"), tag);
                }
                else if (vName == "jvalue")
                {
                    var str = value.ToString();
                    var bl = bool.TryParse(str, out bool vl);
                    if (bl)
                    {
                        s = string.Format("<{0}>{1}</{2}>", tag, vl, tag);
                    }
                    else
                    {
                        bl = int.TryParse(str, out int num);
                        if (bl)
                        {
                            s = string.Format("<{0}>{1}</{2}>", tag, num, tag);
                        }
                        else if (str.ToLower().IndexOf(".jtoken") != -1 || str.ToLower().IndexOf(".jobject") != -1 || str.ToLower().IndexOf(".jarray") != -1)
                        {
                            var txt = SerializerB(value);
                            s = string.Format("<{0}>{1}</{2}>", tag, txt, tag);
                        }
                        else
                        {
                            if (str == "")
                            {
                                s = string.Format("<{0}></{1}>", tag, tag);
                            }
                            else
                            {
                                s = string.Format("<{0}><![CDATA[{1}]]></{2}>", tag, str, tag);
                            }
                        }
                    }
                }
                else
                {
                    var str = SerializerB(value);
                    s = string.Format("<{0}>{1}</{2}>", tag, str, tag);
                }
            }
            return s;
        }

        /// <summary>
        /// 超级序列化——转体
        /// </summary>
        /// <param name="obj">对象模型</param>
        /// <returns>返回Xml格式字符串</returns>
        private static string SerializerB(object obj)
        {
            var ret = string.Empty;
            var type = obj.GetType();
            if (obj is JObject jobj)
            {
                var txt = "";
                foreach (var m in jobj)
                {
                    txt += "\r" + XmlLine(m.Key, m.Value);
                }
                return txt + "\r";
            }
            else if (obj is Array list)
            {
                var txt = "";
                var name = type.Name.Replace("[]", "");
                foreach (var o in list)
                {
                    var str = SerializerB(o);
                    txt += "\r" + string.Format("<{0}>{1}</{2}>", name, str, name);
                }
                return txt + "\r";
            }
            else if (obj is ArrayList lt)
            {
                var txt = "";
                var name = lt[0].GetType().Name;
                foreach (var o in lt)
                {
                    var str = SerializerB(o);
                    txt += "\r" + string.Format("<{0}>{1}</{2}>", name, str, name);
                }
                return txt + "\r";
            }
            else if (obj is IEnumerable<object> ls)
            {
                var txt = "";
                foreach (var o in ls)
                {
                    var name = o.GetType().Name;
                    var str = SerializerB(o);
                    txt += "\r" + string.Format("<{0}>{1}</{2}>", name, str, name);
                }
                return txt + "\r";
            }
            else if (obj is JArray jarr)
            {
                var txt = "";
                foreach (var o in jarr)
                {
                    var name = o.GetType().Name;
                    var str = SerializerB(o);
                    txt += "\r" + string.Format("<{0}>{1}</{2}>", name, str, name);
                }
                return txt + "\r";
            }
            else
            {
                PropertyInfo[] ps = type.GetProperties();  //获取所有属性
                foreach (PropertyInfo p in ps)
                {
                    var value = p.GetValue(obj, null);
                    if (value != null)
                    {
                        ret += "\r" + XmlLine(p.Name, value);
                    }
                }
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回Json数组</returns>
        public static JArray ToJArr(this object obj)
        {
            if (obj is string)
            {
                return JArray.Parse(obj.ToString());
            }
            else
            {
                return JArray.FromObject(obj);
            }
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回Json数组</returns>
        public static JObject ToJObj(this object obj)
        {
            if (obj is string)
            {
                return JObject.Parse(obj.ToString());
            }
            else
            {
                return JObject.FromObject(obj);
            }
        }
    }
}
