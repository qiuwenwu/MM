using System;
using System.Collections.Generic;
using System.Linq;

namespace MM.Helper.Base
{
    /// <summary>
    /// 随机数帮助类
    /// </summary>
    public class Rand
    {
        #region 
        //随机数对象
        private Random _random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 生成一个指定范围的随机整数，该随机数范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public int Int(int minNum = 1, int maxNum = 999999)
        {
            return _random.Next(minNum, maxNum);
        }

        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        public double Double()
        {
            return _random.NextDouble();
        }

        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public List<T> Arr<T>(IEnumerable<T> arr)
        {
            var list = arr.ToList();
            //交换的次数,这里使用数组的长度作为交换次数
            int count = list.Count;

            //开始交换
            for (int i = 0; i < count; i++)
            {
                //生成两个随机数位置
                int num1 = Int(0, list.Count);
                int num2 = Int(0, list.Count);

                //定义临时变量
                T temp = list[num1];

                //交换两个随机数位置的值
                list[num1] = list[num2];
                list[num2] = temp;
            }
            return list;
        }

        /// <summary>
        /// 从字符串里随机得到，规定个数的字符串
        /// </summary>
        /// <param name="str">全字符</param>
        /// <param name="length">长度</param>
        /// <param name="symbol">分隔符</param>
        /// <returns>返回规定个数字符串</returns>
        private string ForStr(string str, int length, string symbol = "")
        {
            var ret = "";
            string[] arr = str.Split(symbol.ToCharArray());
            var max = arr.Length - 1;

            for (var i = 0; i < length; i++)
            {
                var idx = _random.Next(0, max);
                ret += symbol + arr[idx];
            }

            return ret.Substring(symbol.Length);
        }

        /// <summary>
        /// 随机数——指定返回获取随机数
        /// </summary>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        /// <returns>返回一个数字</returns>
        public int Num(int min, int max)
        {
            return _random.Next(min, max + 1);
        }

        /// <summary>
        /// 置随机数 —— 按长度
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns>返回一个随机数</returns>
        public int Num(int length)
        {
            string str = "";
            str += _random.Next(1, 9);
            var len = length - 1;
            while (str.Length < len)
            {
                str += _random.Next(0, 9);
            }
            return int.Parse(str);
        }

        /// <summary>
        /// 置随机数 —— 按长度
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <param name="noRepeat">数字是否可重复</param>
        /// <returns>返回指定长度的随机数</returns>
        public int Num(int length, bool noRepeat)
        {
            if (noRepeat)
            {
                if (length > 10)
                {
                    Ex = "长度不能大于10";
                    return -1;
                }
                string str = "";
                str += _random.Next(1, 9);
                var len = length - 1;
                while (str.Length < len)
                {
                    var o = _random.Next(0, 9).ToString();
                    if (!str.Contains(o))
                    {
                        str += o;
                    }
                }
                return int.Parse(str);
            }
            else
            {
                return Num(length);
            }
        }
        #endregion

        /// <summary>
        /// 无重复随机数
        /// </summary>
        /// <param name="count">代码数</param>
        /// <returns>返回随机数</returns>
        public string NumNoRepeat(int count)
        {
            var str = "";
            if (count > 10) {
                return null;
            }

            var max = count - 1;
            var bl = true;
            while(bl)
            {
                int num = _random.Next(0, max);
                if (!str.Contains(num.ToString()))
                {
                    str += num;
                    if (str.Length == count)
                    {
                        bl = false;
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// 随机数字和字母
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>返回随机数字和字母字符串</returns>
        public string NumAndEn(int length)
        {
            return ForStr("0123456789ABCDEFGHIJKMLNOPQRSTUVWXYZ", length);
        }

        /// <summary>
        /// 随机密码
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>返回密码字符串</returns>
        public string Password(int length)
        {
            return ForStr("0123456789ABCDEFGHIJKMLNOPQRSTUVWXYZabcdefghijkmlnopqrstuvwxyz", length);
        }

        /// <summary>
        /// 随机字母
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>返回英文字符串</returns>
        public string En(int length)
        {
            return ForStr("ABCDEFGHIJKMLNOPQRSTUVWXYZ", length);
        }

        /// <summary>
        /// 随机数字
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>返回数字字符串</returns>
        public string Number(int length)
        {
            return ForStr("0123456789", length);
        }
        
        /// <summary>
        /// 随机硬币 正面、反面
        /// </summary>
        /// <returns></returns>
        public string Coin()
        {
            int o = _random.Next(1, 2);
            string ret = string.Empty;
            switch (o)
            {
                case 1:
                    ret = "正面";
                    break;
                case 2:
                    ret = "反面";
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 随机石头、剪刀、布
        /// </summary>
        /// <returns></returns>
        public string RPS()
        {
            int o = _random.Next(0, 3);
            string ret = string.Empty;
            switch (o)
            {
                case 0:
                    ret = "石头";
                    break;
                case 1:
                    ret = "剪刀";
                    break;
                case 2:
                    ret = "布";
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 随机取扑克牌
        /// </summary>
        /// <param name="max">总牌数</param>
        /// <returns>返回一张扑克</returns>
        public string Poker(int max = 52)
        {
            var text = new Number();
            return text.ToPoker(Num(max));
        }

        /// <summary>
        /// 随机取扑克牌
        /// </summary>
        /// <param name="num">要取得的扑克数</param>
        /// <param name="max">总牌数</param>
        /// <returns>返回扑克牌列表</returns>
        public List<string> Poker(int num, int max = 52)
        {
            List<string> pokers = new List<string>();
            while (pokers.Count < num)
            {
                var poker = Poker(max);
                if (!pokers.Contains(poker))
                {
                    pokers.Add(poker);
                }
            }
            return pokers;
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        /// <param name="min">最小数</param>
        /// <param name="max">最大数</param>
        /// <returns></returns>
        public List<int> Upset(int min, int max = 54)
        {
            var list = new List<int> ();
            for(var i = 0; i < max; i++) {
                list.Add(i);
            }
            return Arr(list);
        }
    }
}
