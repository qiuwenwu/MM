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

        /// <summary>
        /// 右合并 —— 返回右边所有及左边独有的
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dict">字典1</param>
        /// <param name="dictB">字典2</param>
        public Dictionary<string, T> Right<T>(Dictionary<string, T> dict, Dictionary<string, T> dictB)
        {
            return dict.Right(dictB);
        }

        /// <summary>
        /// 判断键是否存在
        /// </summary>
        /// <typeparam name="T">返回</typeparam>
        /// <param name="dict1">字典</param>
        /// <param name="key">键</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public bool Has<T>(Dictionary<string, T> dict1, string key)
        {
            return dict1.ContainsKey(key);
        }

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <typeparam name="T">返回</typeparam>
        /// <param name="dict1">字典</param>
        /// <param name="key">键</param>
        /// <param name="m">值</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public void AddOrSet<T>(Dictionary<string, T> dict1, string key, T m)
        {
            dict1.AddOrSet(key, m);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dt">字典</param>
        /// <param name="key">键</param>
        /// <returns>返回值</returns>
        public T Get<T>(Dictionary<string, T> dt, string key)
        {
            return dt.Get<T>(key);
        }
    }
}
