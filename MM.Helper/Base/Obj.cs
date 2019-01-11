using System;
using System.Collections.Generic;
using System.Text;

namespace MM.Helper.Base
{
    /// <summary>
    /// 对象类
    /// </summary>
    public class Obj
    {
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
    }
}
