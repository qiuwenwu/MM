namespace System.Collections.Generic
{
    /// <summary>
    /// 字典拓展函数
    /// </summary>
    public static class DictExpand
    {
        /// <summary>
        /// 左合并 —— 只返回左边字典有的
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dict1">字典1</param>
        /// <param name="dict2">字典2</param>
        /// <returns>返回新字典</returns>
        public static Dictionary<string, T> Left<T>(this Dictionary<string, T> dict1, Dictionary<string, T> dict2)
        {
            if (dict2 != null)
            {
                foreach (var o in dict2)
                {
                    var key = o.Key;
                    if (dict1.ContainsKey(key))
                    {
                        dict1[key] = o.Value;
                    }
                }
            }
            return dict1;
        }

        /// <summary>
        /// 获取键列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dt">字典</param>
        /// <returns>返回键列表</returns>
        public static List<string> GetKeys<T>(this Dictionary<string, T> dt)
        {
            var lt = new List<string>();
            foreach (var k in dt.Keys)
            {
                lt.Add(k);
            }
            return lt;
        }

        /// <summary>
        /// 右合并 —— 返回右边所有及左边独有的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict1"></param>
        /// <param name="dict2"></param>
        /// <returns>返回新字典</returns>
        public static Dictionary<string, T> Right<T>(this Dictionary<string, T> dict1, Dictionary<string, T> dict2)
        {
            if (dict2 != null)
            {
                foreach (var o in dict2)
                {
                    var key = o.Key;
                    if (dict1.ContainsKey(key))
                    {
                        dict1[key] = o.Value;
                    }
                    else
                    {
                        dict1.Add(key, o.Value);
                    }
                }
            }
            return dict1;
        }

        /// <summary>
        /// 判断键是否存在
        /// </summary>
        /// <typeparam name="T">返回</typeparam>
        /// <param name="dict1">字典</param>
        /// <param name="key">键</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool Has<T>(this Dictionary<string, T> dict1, string key)
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
        public static void AddOrSet<T>(this Dictionary<string, T> dict1, string key, T m)
        {
            if (dict1.ContainsKey(key))
            {
                dict1[key] = m;
            }
            else
            {
                dict1.Add(key, m);
            }
        }
    }
}
