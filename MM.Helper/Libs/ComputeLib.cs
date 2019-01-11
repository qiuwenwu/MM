using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace MM.Helper.Libs
{
    /// <summary>
    /// 计算代码片段
    /// </summary>
    public class ComputeLib
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 文本计算
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="vars">变量字符串</param>
        /// <returns>返回计算结果字符串</returns>
        public string Compute(string text, string vars = "xyz=1")
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            text = Rx(text);
            text = Algebra(text, vars);
            string rx = @"\[(.*?)\]";
            var match = Regex.Matches(text, rx);
            for (int i = 0; i < match.Count; i++)
            {
                var str = match[i].Value;
                text = text.Replace(str, Expression(str.Between("[", "]"), vars));
            }
            rx = @"\{(.*?)\}";
            match = Regex.Matches(text, rx);
            for (int i = 0; i < match.Count; i++)
            {
                var str = match[i].Value;
                text = text.Replace(str, Expression(str.Between("{", "}"), vars));
            }
            return Expression(text, vars);
        }

        /// <summary>
        /// 带未知数的四则运算，公式表达式：((x+y)*y+100)*(x+y+price)  未知数赋值字符串格式："x=3,y=5,price=10.2"  
        /// </summary>
        /// <param name="equation">四则运算公式字符串表达式</param>
        /// <param name="vars">未知数赋值字符串</param>
        /// <returns>返回计算结果字符串</returns>
        public string Expression(string equation, string vars = "xyz=1")
        {
            if (string.IsNullOrEmpty(equation))
            {
                return string.Empty;
            }
            string result = "";
            DataTable dt = new DataTable();
            try
            {
                equation = Algebra(equation, vars);
                result = dt.Compute(equation, "false").ToString();
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
                Console.WriteLine(ex);
                result = "公式错误";
            }
            return result;
        }

        /// <summary>
        /// 代数
        /// </summary>
        /// <param name="equation">方程式</param>
        /// <param name="vars">变量字符串</param>
        /// <returns>返回计算结果字符串</returns>
        public string Algebra(string equation, string vars = "xyz=1")
        {
            if (string.IsNullOrEmpty(equation))
            {
                return string.Empty;
            }
            string[] avars = vars.Split(',');
            //设置参数
            Dictionary<string, string> dicvars = new Dictionary<string, string>();

            for (int i = 0; i < avars.Count(); i++)
            {
                string s = avars[i];
                string[] vs = s.Split('=');
                dicvars.Add(vs[0], vs[1]);
            }

            List<KeyValuePair<string, string>> varsorder = dicvars.OrderByDescending(c => c.Key.ToString().Length).ToList();

            foreach (KeyValuePair<string, string> kvp in varsorder)
            {
                equation = equation.Replace(kvp.Key.ToString(), kvp.Value);
            }
            return equation;
        }

        /// <summary>
        /// 正则算术式——将式子变为电脑计算规范的算数式子
        /// </summary>
        /// <param name="equation">算术式</param>
        /// <returns>返回正则结果</returns>
        private string Rx(string equation)
        {
            try
            {
                if (string.IsNullOrEmpty(equation))
                {
                    return string.Empty;
                }
                string rx = "[0-9]+[a-zA-Z]+";
                var match = Regex.Matches(equation, rx);
                for (int i = 0; i < match.Count; i++)
                {
                    string str = match[i].Value;
                    List<string> list = Split(str);
                    string newStr = "";
                    foreach (var s in list)
                    {
                        if (IsNum(s))
                        {
                            newStr += s;
                        }
                        else
                        {
                            newStr += "*" + s;
                        }
                    }
                    equation = equation.Replace(str, newStr);
                }
                return equation;
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 逐字分割
        /// </summary>
        /// <param name="str">被分割的字符串</param>
        /// <returns>返回分割后的字符串数组</returns>
        private List<string> Split(string str)
        {
            List<string> strList = new List<string>();
            string word = "";
            while (str.Length > 0)
            {
                word = str.Substring(0, 1);
                strList.Add(word);
                str = GetRight(str, word, true);
            }
            return strList;
        }

        /// <summary>
        /// 取文本右边内容
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="r">取文本右边</param>
        /// <param name="bl">右边为空时是否取全内容</param>
        /// <returns>右边内容</returns>
        private string GetRight(string str, string r, bool bl = false)
        {
            try
            {
                int B = str.IndexOf(r);
                if (B < 0)
                {
                    if (bl)
                    {
                        return str;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    var index = B + r.Length;
                    return str.Substring(index, str.Length - index);
                }
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是数字返回true，不是返回false</returns>
        private bool IsNum(string str)
        {
            if (str == null)
            {
                return false;
            }
            string rx = @"^[0-9]+$";
            return Regex.IsMatch(str, rx);
        }
    }
}
