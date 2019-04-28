using MM.Helper.Models;
using MM.Helper.Net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MM.Helper.Data
{
    /// <summary>
    /// 验证类
    /// </summary>
    public class Param
    {
        private static readonly Https http = new Https();
        
        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="v">验证模型</param>
        /// <param name="value">验证值</param>
        /// <returns>验证通过返回null，否则返回错误提示</returns>
        public string Check(ParamModel v, object value) {
            var msg = "";
            // 对于多参数传递验证
            if (!string.IsNullOrEmpty(v.Split) && value is string) {
                var val = value.ToString();
                if (val.Contains(v.Split)) {
                    var arr = val.Split(v.Split);
                    foreach (var o in arr) {
                        msg = Check(v, o);
                        if (!string.IsNullOrEmpty(msg))
                        {
                            break;
                        }
                    }
                    return msg;
                }
            }
            string name = string.Format("{0}({1})",v.Title, v.Name);
            if (v.NotEmpty != null)
            {
                if (value == null || value.ToString() == "")
                {
                    return string.Format(v.NotEmpty.Message, name);
                }
            }
            if (value == null)
            {
                return null;
            }
            var err = false;

            var type = v.DataType.Format.ToLower();
            try
            {
                switch (type)
                {
                    case "array":
                        var list = value.ToArr();
                        if (v.SubParam != null && v.SubParam.Count > 0 && list.Count > 0)
                        {
                            msg = Check(v.SubParam, list[0].ToDict());
                        }
                        break;
                    case "object":
                        var dict = value.ToDict();
                        if (v.SubParam != null && v.SubParam.Count > 0 && dict.Count > 0)
                        {
                            msg = Check(v.SubParam, dict);
                        }
                        break;
                    case "string":
                        // 验证字符串长度范围
                        var str = value.ToString();
                        var l = v.StrLen;
                        if (l != null)
                        {
                            var len = str.Length;
                            var max = l.Max;
                            var min = l.Min;
                            if (min < 0 && max > 0)
                            {
                                // 当min小于0时，只判断max
                                if (max < len)
                                {
                                    msg = string.Format(l.Message_max, name, max);
                                }
                            }
                            else if (min > 0 && max <= 0) {
                                // 当max小于等于0时，只判断min
                                if (min > len)
                                {
                                    msg = string.Format(l.Message_min, name, min);
                                }
                            }
                            else if (max < len || len < min) {
                                msg = string.Format(l.Message, name, min, max);
                            }
                        }
                        if (string.IsNullOrEmpty(msg))
                        {
                            // 正则验证
                            var x = v.Regex;
                            if (x != null)
                            {
                                var text = x.Format;
                                if (!string.IsNullOrEmpty(text))
                                {
                                    var rx = new Regex(text);
                                    if (!rx.IsMatch(str))
                                    {
                                        msg = string.Format(x.Message, name);
                                    }
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(msg))
                        {
                            // 验证字符串后缀名
                            var e = v.Extension;
                            if (e != null)
                            {
                                var fs = e.Format.Split('|');
                                str = str.ToLower();
                                var yes = false;
                                foreach (var o in fs)
                                {
                                    if (str.EndsWith(o))
                                    {
                                        yes = true;
                                    }
                                }
                                if (!yes)
                                {
                                    msg = string.Format(e.Message, name, e.Format);
                                }
                            }
                        }
                        break;
                    case "int":
                    case "long":
                    case "float":
                    case "decimal":
                    case "double":
                        // 验证数字类型
                        var n = value.ToString();
                        if (type == "int" && !int.TryParse(n, out var n_int))
                        {
                            err = true;
                        }
                        else if (type == "long" && !long.TryParse(n, out var n_long))
                        {
                            err = true;
                        }
                        else if (type == "double" && !double.TryParse(n, out var n_double))
                        {
                            err = true;
                        }
                        else if (type == "float" && !float.TryParse(n, out var n_float))
                        {
                            err = true;
                        }
                        else if (type == "decimal" && !decimal.TryParse(n, out var n_decimal))
                        {
                            err = true;
                        }
                        else
                        {
                            // 验证数值范围
                            var num = value.ToDecimal();
                            var c = v.Range;
                            if (c != null)
                            {
                                var max = c.Max;
                                var min = c.Min;
                                if (min < 0 && max > 0)
                                {
                                    // 当min小于0时，只判断max
                                    if (max < num)
                                    {
                                        msg = string.Format(c.Message_max, name, max);
                                    }
                                }
                                else if (min > 0 && max <= 0)
                                {
                                    // 当max小于等于0时，只判断min
                                    if (min > num)
                                    {
                                        msg = string.Format(c.Message_min, name, min);
                                    }
                                }
                                else if (min > num || num > max)
                                {
                                    msg = string.Format(c.Message, name, min, max);
                                }
                            }
                        }
                        break;
                    case "bool":
                        var blStr = value.ToString().ToLower();
                        if (blStr != "true" && blStr != "false" && blStr != "1" && blStr != "0")
                        {
                            err = true;
                        }
                        break;
                    case "dateTime":
                    case "date":
                    case "time":
                        var d = v.DateTime;
                        if (DateTime.TryParse(value.ToString(), out var time))
                        {
                            var minT = d.Min;
                            var maxT = d.Max;
                            if (string.IsNullOrEmpty(minT) && !string.IsNullOrEmpty(maxT))
                            {
                                // 当min小于0时，只判断max
                                if (maxT.ToTime() < time)
                                {
                                    msg = string.Format(d.Message_max, name, maxT);
                                }
                            }
                            else if (!string.IsNullOrEmpty(minT) && string.IsNullOrEmpty(maxT))
                            {
                                // 当max小于等于0时，只判断min
                                if (minT.ToTime() > time)
                                {
                                    msg = string.Format(d.Message_min, name, minT);
                                }
                            }
                            else if (minT.ToTime() > time || time > maxT.ToTime()) {
                                msg = string.Format(d.Message, name, minT, maxT);
                            }
                        }
                        else
                        {
                            err = true;
                        };
                        break;
                    default:
                        err = true;
                        break;
                }
            }
            catch (Exception)
            {
                err = true;
            }
            if (err)
            {
                return string.Format(v.DataType.Message, name, v.DataType.Format);
            }
            else if(!string.IsNullOrEmpty(msg))
            {
                return msg;
            }

            // 远程验证
            var r = v.Remote;
            if (r != null)
            {
                var url = r.Url;
                if (!string.IsNullOrEmpty(url))
                {
                    var jobj = new JObject
                    {
                        { name, value.ToJson() }
                    };

                    var jsonStr = http.Post(url, jobj.ToString());
                    if (!string.IsNullOrEmpty(jsonStr))
                    {
                        try
                        {
                            var m = jsonStr.ToObj<ResModel>();
                            if (m.Error != null)
                            {
                                msg = m.Error.Message;
                                if (string.IsNullOrEmpty(msg))
                                {
                                    msg = string.Format(r.Message, name);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            msg = "序列化远程结果失败！\n" + ex.ToString();
                        }
                    }
                }
            }
            return msg;
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="checkDt">验证模型字典</param>
        /// <param name="paramDt">参数字典</param>
        /// <returns>验证通过返回null，否则返回错误提示</returns>
        public string Check(Dictionary<string, ParamModel> checkDt, Dictionary<string, object> paramDt)
        {
            var msg = "";
            foreach (var o in checkDt) {
                var key = o.Key.ToLower();
                var v = o.Value;
                var name = string.Format("{0}({1})", v.Title, v.Name);
                if (paramDt.ContainsKey(key))
                {
                    var val = paramDt[key];

                    // 判断是否相同
                    if (v.Identical != null) {
                        var field = v.Identical.Field;
                        if (!paramDt.ContainsKey(field) || val != paramDt[field])
                        {
                            msg = string.Format(v.Identical.Message, name, field);
                            break;
                        }
                    }

                    // 判断是否不同
                    if (v.Different != null)
                    {
                        var field = v.Different.Field;
                        if (paramDt.ContainsKey(field) && val == paramDt[field])
                        {
                            msg = string.Format(v.Different.Message, name, field);
                            break;
                        }
                    }

                    msg = Check(v, val);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        break;
                    }
                }
                else if(v.NotEmpty != null)
                {
                    msg = string.Format(v.NotEmpty.Message, name);
                    break;
                }

                if (v.Filter)
                {
                    paramDt.Remove(key);
                }
            }
            return msg;
        }

        /// <summary>
        /// 新建验证参数模型
        /// </summary>
        public ParamModel New() {
            return new ParamModel() { };
        }

        /// <summary>
        /// 新建验证参数
        /// </summary>
        public Dictionary<string, ParamModel> NewDict()
        {
            return new Dictionary<string, ParamModel>() { };
        }

        /// <summary>
        /// 验证模型示例
        /// </summary>
        /// <returns>返回验证模型示例</returns>
        public ParamModel Demo() {
            return new ParamModel() {
                Title = "测试参数",
                Name = "test",
                Description = "这是一个测试的参数模型",
                Filter = false,
                DataType = new DataTypeModel() { Format = "string" },
                DateTime = new DateTimeModel(),
                Different = new DifferentModel() { Field = "username" },
                Extension = new ExtensionModel() { Format = "xls|xlsx|csv" },
                Identical = new IdenticalModel() { Field = "password_confirm" },
                NotEmpty = new NotEmptyModel(),
                Range = new RangeModel() { Min = 0, Max = 100 },
                Remote = new RemoteModel() { Url = "/api/user_check" },
                Regex = new RegexModel() { Format = "[a-zA-Z0-9]+" },
                StrLen = new StrLenModel() { Min = 0, Max = 255 }
            };
        }

        /// <summary>
        /// 验证模型示例
        /// </summary>
        /// <returns>返回验证模型示例</returns>
        public Dictionary<string, ParamModel> DemoDict()
        {
            var dict = new Dictionary<string, ParamModel>() {
                { "username", new ParamModel(){
                        Title = "用户名",
                        Name = "username",
                        Description = "用户登录的名称",
                        DataType = new DataTypeModel() { Format = "string" },
                        NotEmpty = new NotEmptyModel(),
                        Remote = new RemoteModel() { Url = "/api/user_check" },
                        Regex = new RegexModel() { Format = "^[a-zA-Z0-9_]+$" },
                        StrLen = new StrLenModel() { Min = 5, Max = 16 }
                } },
                { "password", new ParamModel(){
                        Title = "密码",
                        Name = "password",
                        Description = "用户登录时的密码",
                        Filter = false,
                        DataType = new DataTypeModel() { Format = "string" },
                        Different = new DifferentModel() { Field = "username" },
                        Regex = new RegexModel() { Format = "^[a-zA-Z0-9]+$" },
                        StrLen = new StrLenModel() { Min = 5, Max = 16 }
                } },
                { "password_confirm", new ParamModel(){
                        Title = "确认密码",
                        Name = "password_confirm",
                        Description = "注册时二次输入密码，用于确认用户输入密码没有错误",
                        Filter = true, // 过滤
                        DataType = new DataTypeModel() { Format = "string" },
                        Identical = new IdenticalModel() { Field = "password" }
                } },
                { "phone", new ParamModel(){
                        Title = "手机",
                        Name = "phone",
                        Description = "用户的手机号码，可用于手机登录",
                        DataType = new DataTypeModel() { Format = "string" },
                        Regex = new RegexModel() { Format = "1[0-9]{10}" },
                        StrLen = new StrLenModel() { Min = 11, Max = 11, Message = "{0}长度必须为11位数字" }
                } },
                { "email", new ParamModel(){
                        Title = "电子邮箱",
                        Name = "email",
                        Description = "用户的电子邮箱，可以用于邮箱登录",
                        DataType = new DataTypeModel() { Format = "string" },
                        Extension = new ExtensionModel() { Format = "@qq.com|@163.com|@139.com", Message = "{0}仅支持{1}邮箱" },
                        Regex = new RegexModel() { Format = "[a-zA-Z0-9_]+@[a-zA-Z0-9.]+[a-zA-Z]+" }
                } },
                { "icon", new ParamModel(){
                        Title = "头像",
                        Name = "icon",
                        Description = "这是用户的头像，用于个性化识别用户",
                        DataType = new DataTypeModel() { Format = "string" },
                        Extension = new ExtensionModel() { Format = ".png|.gif|.jpg|.jpeg" },
                        Regex = new RegexModel() { Format = "[a-zA-Z0-9:/._]+" }
                } },
                { "age", new ParamModel(){
                        Title = "年龄",
                        Name = "age",
                        Description = "用户的年龄，用于交友搜索时",
                        DataType = new DataTypeModel() { Format = "int" },
                        Range = new RangeModel() { Min = 16, Max = 130 }
                } },
                { "birthday", new ParamModel(){
                        Title = "生日",
                        Name = "birthday",
                        Description = "这是一个测试的参数模型",
                        DataType = new DataTypeModel() { Format = "date" },
                        DateTime = new DateTimeModel() { Min = "1950-01-01", Max = "2019-02-10" },
                } }
            };
            return dict;
        }
    }
}
