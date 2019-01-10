using System.Collections.Generic;

namespace MM.Helper.Base
{
    /// <summary>
    /// 字典帮助类
    /// </summary>
    public class Dict
    {
        /// <summary>
        /// 左合并
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dict">字典1</param>
        /// <param name="dictB">字典2</param>
        public Dictionary<string, T> Left<T>(Dictionary<string, T> dict, Dictionary<string, T> dictB)
        {
            return dict.Left(dictB);
        }

        /// <summary>
        /// 获取键列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dt">字典</param>
        /// <returns>返回键列表</returns>
        public List<string> GetKeys<T>(Dictionary<string, T> dt) {
            return dt.GetKeys();
        }
    }
}
