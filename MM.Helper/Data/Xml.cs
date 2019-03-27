using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MM.Helper.Data
{
    /// <summary>
    /// Xml帮助类
    /// </summary>
    public class Xml
    {
        /// <summary>
        /// 当前路径
        /// </summary>
        public string Dir { get; set; } = "";

        /// <summary>
        /// 写入设置
        /// </summary>
        public XmlWriterSettings settings = new XmlWriterSettings()
        {
            Encoding = Encoding.UTF8,
            OmitXmlDeclaration = true
        };

        #region XELement操作
        /// <summary>
        /// 加载xml文件
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>返回xml对象</returns>
        public XElement Load(string file)
        {
            return XElement.Load(file.ToFullName(Dir));
        }

        /// <summary>
        /// 保存xml文件
        /// </summary>
        /// <param name="file">文件名</param>
        ///  <param name="xml">xml对象</param>
        public void Save(string file, XElement xml)
        {
            xml.Save(file.ToFullName(Dir));
        }

        /// <summary>
        /// 保存xml文件
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="xmlStr">xml字符串</param>
        public void Save(string file, string xmlStr)
        {
            var xml = ToXmlS(xmlStr);
            xml.Save(file.ToFullName(Dir));
        }

        /// <summary>
        /// 新建xml对象
        /// </summary>
        /// <param name="rootname">根标签</param>
        /// <returns>返回xml对象</returns>
        public XElement New(string rootname = "xml")
        {
            return new XElement(rootname);
        }

        /// <summary>
        /// Xml对象转字符串
        /// </summary>
        /// <param name="xml">xml对象</param>
        /// <returns>返回xml字符串</returns>
        public string ToStr(XElement xml)
        {
            var reader = xml.CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
        }

        /// <summary>
        /// 通过xml字符串加载xml
        /// </summary>
        /// <param name="xmlStr">xml字符串</param>
        /// <returns>返回xml模型</returns>
        public XElement ToXml(string xmlStr)
        {
            return XElement.Parse(xmlStr);
        }

        ///<summary>
        ///替换html中的特殊字符
        ///</summary>
        ///<param name="theString">需要进行替换的文本</param>
        ///<returns>替换完的文本。</returns>
        private string Encode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "&apos;");
            theString = theString.Replace("\n", "<br/>");
            return theString;
        }

        /// <summary>
        /// 超级转Xml
        /// </summary>
        /// <param name="xmlStr">xml字符串</param>
        /// <returns>返回xml对象</returns>
        public XElement ToXmlS(string xmlStr)
        {
            var rx = @"<!\[CDATA\[.*?\]\]>";
            var matches = Regex.Matches(xmlStr, rx);
            for (var i = 0; i < matches.Count; i++)
            {
                var str = matches[i].Value.Replace("<![CDATA[", "").Replace("]]>", "");
                str = Encode(str);
                xmlStr = xmlStr.Replace(matches[i].Value, str);
            }
            return ToXml(xmlStr);
        }
        
        /// <summary>
        /// 模型转xml对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="m">模型</param>
        /// <returns>返回xml对象</returns>
        public XElement ToXml<T>(T m) where T : new() {
            XElement element = new XElement(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object propertyValue = property.GetValue(m, null);
                XElement innerElement = new XElement(property.Name, propertyValue);
                element.Add(innerElement);
            }
            return element;
        }
        #endregion


        #region 序列化&反序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回xml格式字符串</returns>
        public string Dumps(object obj)
        {
            return obj.ToXmlS();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="xml">xml字符串</param>
        /// <returns>返回xml格式字符串</returns>
        public object Loads(object obj, string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmlde = new XmlSerializer(obj.GetType());
                return xmlde.Deserialize(sr);
            }
        }

        /// <summary>
        /// 转换旧版xml
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <returns>转旧版xxml</returns>
        public string Change(string xml)
        {
            xml = xml.Trim(' ');    //删首尾空
            var rx = @">( )?(\s)?\w{4}-\w{2}-\w{2} \w{2}:\w{2}:\w{2}( )?(\s)?</";
            var match = Regex.Matches(xml, rx); //转换时间
            for (int i = 0; i < match.Count; i++)
            {
                var s = match[i];
                var arr = s.Value.Trim(' ').Split(' ', '<');
                var datetime = arr[0] + "T" + arr[1] + ".0000000+08:00</";
                xml = xml.Replace(s.Value, datetime);
            }
            rx = @"<[a-zA-Z0-9]+(\s)?/>";
            match = Regex.Matches(xml, rx); //单向标签转换
            for (int i = 0; i < match.Count; i++)
            {
                var value = match[i].Value;
                var tagL = value.Trim(' ').Replace("/", "");
                var tagR = tagL.Replace("<", "</");
                xml = xml.Replace(value, tagL + tagR);
            }
            rx = @">( )?(\s)?true( )?(\s)?</";
            match = Regex.Matches(xml, rx, RegexOptions.IgnoreCase); //转换真假值，将大写的TRUE或True等转为标准的true
            for (int i = 0; i < match.Count; i++)
            {
                var s = match[i];
                xml = xml.Replace(s.Value, ">true</");
            }
            rx = @"<!\[CDATA\[.*?\]\]>";
            var matches = Regex.Matches(xml, rx);
            if (matches.Count > 1)
            {
                for (var i = 0; i < matches.Count; i++)
                {
                    var str = matches[i].Value.Replace("<![CDATA[", "").Replace("]]>", "");
                    str = Encode(str);
                    xml = xml.Replace(matches[i].Value, str);
                }
            }
            return xml;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="xml">xml字符串</param>
        /// <returns>返回泛型</returns>
        public T Dumps<T>(string xml) where T : class
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(typeof(T));
                return (T)xmldes.Deserialize(sr);
            }
        }
        #endregion


        #region 文武自制xml序列化 和 反序列化
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="xml">xml格式字符串</param>
        /// <returns>返回泛型模型</returns>
        public T ToModel<T>(string xml) where T : new()
        {
            var obj = new T();
            var o = ToModel(obj, xml);
            if (o != null)
            {
                obj = (T)o;
            }
            return obj;
        }

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="obj">模型</param>
        /// <param name="xml">xml格式字符串</param>
        /// <returns>返回泛模型</returns>
        public object ToModel(object obj, string xml)
        {
            if (xml.Contains(@"<!\[CDATA\["))
            {
                xml = Change(xml);
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.FirstChild;
            return GetModel(obj, root);
        }

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="obj">模型</param>
        /// <param name="xml">xml格式字符串</param>
        /// <returns>返回泛模型</returns>
        public static object GetModel(object obj, XmlNode xml)
        {
            if (obj == null || xml == null)
            {
                return obj;
            }
            var type = obj.GetType();
            if (type.IsArray)   //判断对象是否数组
            {
                if (!xml.HasChildNodes) //判断xml是否存在子成员
                {
                    return obj;
                }
                var xList = xml.ChildNodes; //获取xml子成员列表
                var count = xList.Count;
                var etype = type.GetElementType();  //获取对象数组的单个对象类型
                var oj = Activator.CreateInstance(etype);   //实例化单个对象
                var arr = Array.CreateInstance(etype, count);  //创建一个新的数组
                for (int i = 0; i < count; i++)
                {
                    var x = xList[i];
                    arr.SetValue(GetModel(oj, x), i);
                }
                return arr;
            }
            else if (obj is IList list)
            {
                var p = type.GetProperty("Item");
                var oj = Activator.CreateInstance(p.PropertyType);   //实例化单个对象
                var xList = xml.ChildNodes; //获取xml子成员列表
                var count = xList.Count;
                for (int i = 0; i < count; i++)
                {
                    var xStr = xList[i];
                    var m = GetModel(oj, xStr);
                    list.Add(m);
                }
                return list;
            }
            PropertyInfo[] ps = type.GetProperties();  //获取所有属性
            foreach (PropertyInfo p in ps)
            {
                var vName = p.PropertyType.Name.ToLower();
                var o = xml[p.Name];
                if (o == null)
                {
                    o = xml[p.Name.ToLower()];
                }
                var str = "";
                if (o != null)
                {
                    str = o.InnerText;
                }
                var bl = false;
                if (!string.IsNullOrEmpty(str))
                {
                    if (vName == "string")
                    {
                        p.SetValue(obj, str);
                    }
                    else if (vName == "boolean")
                    {
                        bl = bool.TryParse(str, out bool bln);
                        if (bl)
                        {
                            p.SetValue(obj, bln);
                        }
                    }
                    else if (vName == "int64")
                    {
                        bl = long.TryParse(str, out long num);
                        if (bl)
                        {
                            p.SetValue(obj, num);
                        }
                    }
                    else if (vName == "double")
                    {
                        bl = double.TryParse(str, out double num);
                        if (bl)
                        {
                            p.SetValue(obj, num);
                        }
                    }
                    else if (vName == "int32")
                    {
                        bl = int.TryParse(str, out int num);
                        if (bl)
                        {
                            p.SetValue(obj, num);
                        }
                    }
                    else if (vName == "decimal")
                    {
                        bl = decimal.TryParse(str, out decimal num);
                        if (bl)
                        {
                            p.SetValue(obj, num);
                        }
                    }
                    else if (vName == "single")
                    {
                        bl = float.TryParse(str, out float num);
                        if (bl)
                        {
                            p.SetValue(obj, num);
                        }
                    }
                    else if (vName == "datetime")
                    {
                        bl = DateTime.TryParse(str, out DateTime datetime);
                        if (bl)
                        {
                            p.SetValue(obj, datetime);
                        }
                    }
                    else
                    {
                        var value = p.GetValue(obj, null);
                        value = GetModel(value, o);
                        p.SetValue(obj, value);
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// json字符串转xml字符串
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>返回xml格式字符串</returns>
        public string FromJson(string jsonStr)
        {
            var str = "{\"xml\":" + jsonStr + "}";
            XmlDocument doc1 = JsonConvert.DeserializeXmlNode(str);
            var xml = doc1.OuterXml;
            if (xml.IndexOf("/>") != -1)
            {
                var rx = @"<[a-zA-Z0-9]+(\s)?/>";
                var match = Regex.Matches(xml, rx); //单向标签转换
                for (int i = 0; i < match.Count; i++)
                {
                    var value = match[i].Value;
                    var tagL = value.Trim(' ').Replace("/", "");
                    var tagR = tagL.Replace("<", "</");
                    xml = xml.Replace(value, tagL + tagR);
                }
            }
            return xml;
        }
        #endregion


        #region 增删改查
        /// <summary>
        /// 取Xml
        /// </summary>
        /// <param name="xml">xml对象</param>
        /// <param name="key">xml节点名称</param>
        /// <returns></returns>
        public XElement Get(XElement xml, string key)
        {
            return xml.Element(key);
        }

        /// <summary>
        /// 取Xml子元素合集
        /// </summary>
        /// <param name="xml">xml对象</param>
        /// <returns>返回Xml子元素合集</returns>
        public List<XElement> GetChild(XElement xml)
        {
            return xml.Elements().ToList();
        }

        /// <summary>
        /// 设置Xml
        /// </summary>
        /// <param name="xml">xml对象</param>
        /// <param name="key">xml节点名称</param>
        /// <param name="value">增改的xml对象</param>
        /// <returns>设置成功返回true,失败返回false</returns>
        public void Set(XElement xml, string key, object value)
        {
            xml.SetElementValue(key, value);
        }

        /// <summary>
        /// 取属性值
        /// </summary>
        /// <param name="xml">xml对象</param>
        /// <param name="key">键</param>
        /// <returns>返回属性值</returns>
        public string GetAtt(XElement xml, string key)
        {
            return (string)xml.Attribute(key);
        }

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="xml">xml对象</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>修改成功返回true，失败返回false</returns>
        public void SetAtt(XElement xml, string key, object value)
        {
            xml.SetAttributeValue(key, value);
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="xml">xml对象</param>
        /// <returns>返回字符串值</returns>
        public string GetValue(XElement xml)
        {
            return xml.Value;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="xml">xml对象</param>
        /// <param name="value"></param>
        /// <returns>设置成功返回true，失败返回false</returns>
        public void SetValue(XElement xml, string value)
        {
            xml.Value = value;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xml">xml对象</param>
        /// <param name="key">键</param>
        /// <returns>返回删除后xml对象</returns>
        public XElement Del(XElement xml, string key)
        {
            XElement xchild = xml.Element(key);
            xchild.Remove();
            return xml;
        }
        #endregion
    }
}
