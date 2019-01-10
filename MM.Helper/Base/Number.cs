using System;
using System.Collections.Generic;

namespace MM.Helper.Base
{
    /// <summary>
    /// 号码帮助类
    /// </summary>
    public class Number
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 转小数型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回小数型</returns>
        public Decimal ToDecimal(string str)
        {
            return str.ToDecimal();
        }

        /// <summary>
        /// 转整型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回整型</returns>
        public int ToInt(string str)
        {
            return str.ToInt();
        }

        /// <summary>
        /// 转长整型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回长数型</returns>
        public long ToLong(string str)
        {
            return str.ToLong();
        }

        /// <summary>
        /// 数字转扑克
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="havaBoss">是否有王牌</param>
        /// <returns>返回扑克标记字符串</returns>
        public string ToPoker(int num, bool havaBoss = false)
        {
            if (havaBoss)
            {
                if (54 < num)
                {
                    return "";
                }
            }
            else
            {
                if (52 < num)
                {
                    return "";
                }
            }

            List<string> pokers = new List<string>();

            for (int i = 1; i <= 13; i++)
            {
                pokers.Add("黑桃" + ToPoker(i));
            }
            for (int i = 1; i <= 13; i++)
            {
                pokers.Add("红桃" + ToPoker(i));
            }
            for (int i = 1; i <= 13; i++)
            {
                pokers.Add("梅花" + ToPoker(i));
            }
            for (int i = 1; i <= 13; i++)
            {
                pokers.Add("方块" + ToPoker(i));
            }
            if (havaBoss)
            {
                pokers.Add("大王");
                pokers.Add("小王");
            }

            try
            {
                return pokers[num];
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
                return string.Empty;
            }
        }
    }
}
