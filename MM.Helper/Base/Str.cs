using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MM.Helper.Base
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public class Str
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>为空返回true，非空返回false</returns>
        public bool IsNull(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 删首尾字符
        /// </summary>
        /// <param name="str">被过滤的字符串</param>
        /// <param name="start">头中含有的字符</param>
        /// <param name="end">尾中含有的字符</param>
        /// <returns>删除后的字符串</returns>
        public string Trim(string str, string start = null, string end = null)
        {
            if (start == null && end == null)
            {
                return str.Trim();
            }
            else if (start == null) //如果头为空则删尾
            {
                return str.TrimEnd(end.ToCharArray());
            }
            else if (end == null) //如果尾为空则删头
            {
                return str.TrimStart(start.ToCharArray());
            }
            else //如果均不为空则删首尾
            {
                return str.TrimStart(start.ToCharArray()).TrimEnd(end.ToCharArray());
            }
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="oldStr">需替换字符串</param>
        /// <param name="newStr">替换用的字符串 </param>
        /// <param name="num">替换次数</param>
        /// <param name="idx">替换的起始位置</param>
        /// <returns>返回替换后的字符串</returns>
        public string Replace(string str, string oldStr, string newStr, int num, int idx = 1)
        {
            return str.Replace(oldStr, newStr, num, idx);
        }

        /// <summary>
        /// 取文本左边内容
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="l">被取字符串</param>
        /// <param name="bl">如果没有左边内容，是否返回原有内容</param>
        /// <returns>返回左边内容</returns>
        public string Left(string str, string l, bool bl = false)
        {
            return str.Left(l, bl);
        }

        /// <summary>
        /// 取文本右边内容
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="r">索引字符串</param>
        /// <param name="bl">如果没有右边内容，是否返回原有内容</param>
        /// <returns>返回右边内容</returns>
        public string Right(string str, string r, bool bl = false)
        {
            return str.Right(r, bl);
        }

        /// <summary>
        /// 取文本中间内容
        /// </summary>
        /// <param name="str">原文本</param>
        /// <param name="l">左边文本</param>
        /// <param name="r">右边文本</param>
        /// <param name="bl">如果没有之间的内容，是否返回原有内容</param>
        /// <returns>返回中间文本内容</returns>
        public string Between(string str, string l, string r, bool bl = false)
        {
            return str.Between(l, r, bl);
        }

        /// <summary>
        /// 替换字符串标签
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="param">参数</param>
        /// <returns>返回连接后的字符串</returns>
        public string Format(string str, params object[] param)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return string.Format(str, param);
        }

        /// <summary>
        /// 替换掉所有字符
        /// </summary>
        /// <param name="str">被替换字符串</param>
        /// <param name="newStr">用作替换的字符串</param>
        /// <returns>替换成功返回替换后字符串</returns>
        public string ReplaceSymbol(string str, string newStr)
        {
            return str.ReplaceSymbol(newStr);
        }

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="rx">正则</param>
        /// <param name="newStr">替换后的字符</param>
        /// <returns>返回替换后的字符串</returns>
        public string ReplaceRx(string str, string rx, string newStr)
        {
            return str.ReplaceRx(rx, newStr);
        }

        /// <summary>
        /// 过滤不可见字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回新字符串</returns>
        public string ReplaceNull(string str)
        {
            return str.ReplaceNull();
        }

        /// <summary>
        /// 转为激活码
        /// </summary>
        /// <param name="str">被转换的字符串</param>
        /// <param name="length">分割长度</param>
        /// <param name="strat">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns>返回激活码格式字符串</returns>
        public string ToPIN(string str, int length, int strat, int end)
        {
            return str.ToPIN(length, strat, end);
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回字符串型</returns>
        public string ToStr(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString();
        }

        /// <summary>
        /// 转到小写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回小写字符串</returns>
        public string ToLower(string str)
        {
            return str.ToLower();
        }

        /// <summary>
        /// 转到大写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回大写字符串</returns>
        public string ToUpper(string str)
        {
            return str.ToUpper();
        }

        /// <summary>
        /// 分割文本
        /// </summary>
        /// <param name="str">被分割的字符串</param>
        /// <param name="symbol">分隔符</param>
        /// <returns>返回分割后的字符串数组</returns>
        public List<string> Split(string str, string symbol)
        {
            return str.Split(symbol);
        }

        /// <summary>
        /// 正则获取匹配的集合
        /// </summary>
        /// <param name="str">被匹配的文本</param>
        /// <param name="rx">正则</param>
        /// <returns>返回匹配的结果</returns>
        public MatchCollection Matches(string str, string rx)
        {
            return Regex.Matches(str, rx);
        }

        /// <summary>
        /// 获取字符
        /// </summary>
        /// <param name="str">被取的字符串</param>
        /// <param name="rx">正则数量</param>
        /// <param name="num">取出的数量</param>
        /// <returns>返回获取的列表</returns>
        public List<string> Get(string str, string rx, int num = 0)
        {
            return str.Get(rx, num);
        }

        /// <summary>
        /// 获取数字
        /// </summary>
        /// <param name="str">被取的字符串</param>
        /// <param name="num">取出的数量</param>
        /// <returns>返回获取的列表</returns>
        public List<int> GetNum(string str, int num = 0)
        {
            return str.GetNum(num);
        }

        /// <summary>
        /// 取英文
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="num">取出前几个</param>
        /// <returns>返回取出的字符串</returns>
        public List<string> GetEn(string str, int num = 0)
        {
            return str.GetEn(num);
        }

        /// <summary>
        /// 取汉字
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="num">取出前几个</param>
        /// <returns>返回取出的字符串</returns>
        public static List<string> GetCh(string str, int num = 0)
        {
            return str.GetCh(num);
        }

        /// <summary>
        /// 是否匹配正则
        /// </summary>
        /// <param name="str">被匹配的字符串</param>
        /// <param name="rx">正则表达式 </param>
        /// <returns>匹配返回true，不匹配返回false</returns>
        public bool IsMatch(string str, string rx)
        {
            return str.IsMatch(rx);
        }

        /// <summary>
        /// 是否数字英文
        /// </summary>
        /// <param name="str">判断的字符串</param>
        /// <returns>是则返回true，否则返回false</returns>
        public bool IsNumOrEn(string str)
        {
            return str.IsNumOrEn();
        }

        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是则返回true，否则返回false</returns>
        public bool IsNum(string str)
        {
            return str.IsNum();
        }

        /// <summary>
        /// 判断是否英文
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是则返回true，否则返回false</returns>
        public bool IsEn(string str)
        {
            return str.IsEn();
        }

        /// <summary>
        /// 判断是否为中文
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是中文返回true，不是返回false</returns>
        public bool IsCh(string str)
        {
            return str.IsCh();
        }

        /// <summary>
        /// 取匹配正则的字符串
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="rx">正则表达式</param>
        /// <param name="num">取出前几个</param>
        /// <returns>返回取出的字符串</returns>
        public List<string> Match(string str, string rx, int num = 0)
        {
            return str.Match(rx, num);
        }

        /// <summary>
        /// 取匹配正则的字符串
        /// </summary>
        /// <param name="str">被取字符串</param>
        /// <param name="rx">正则表达式</param>
        /// <returns>返回取出的字符串</returns>
        public string Rx(string str, string rx)
        {
            return str.Rx(rx);
        }

        /// <summary>
        /// 取相似度
        /// </summary>
        /// <param name="str1">文本1</param>
        /// <param name="str2">文本2</param>
        /// <returns>返回相似值0-100</returns>
        public int As2(string str1, string str2)
        {
            return str1.As2(str2);
        }

        /// <summary>
        /// 获取两个字符串的相似度
        /// </summary>
        /// <param name="str1">第一个字符串</param>
        /// <param name="str2">第二个字符串</param>
        /// <returns>返回双精度相似值</returns>
        public double As(string str1, string str2)
        {
            return str1.As(str2);
        }

        /// <summary> 
        /// 汉字转化为拼音首字母
        /// </summary> 
        /// <param name="str">汉字</param> 
        /// <returns>返回首字母</returns> 
        public string PinyinFirst(string str)
        {
            return str.PinyinFirst();
        }

        /// <summary> 
        /// 汉字转化为拼音
        /// </summary> 
        /// <param name="str">汉字</param> 
        /// <returns>返回全拼拼音</returns> 
        public string Pinyin(string str)
        {
            return str.Pinyin();
        }

        /// <summary>
        /// 字符串计算加减乘除
        /// </summary>
        /// <param name="text">表达式</param>
        /// <param name="vars">变量值，多个变量赋值用,分隔</param>
        /// <returns>返回计算结果</returns>
        public string Compute(string text, string vars = "xyz=1")
        {
            var dt = new Libs.ComputeLib();
            return dt.Compute(text, vars);
        }
    }
}
