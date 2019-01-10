using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// 列表拓展函数
    /// </summary>
    public static class ListExpand
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <returns>返回所有对应键值</returns>
        public static List<string> GetStr<T>(this IEnumerable<T> list, string key)
        {
            var lt = new List<string>();
            foreach (var o in list)
            {
                var obj = GetValue(o, key);
                if (obj != null)
                {
                    lt.Add(obj.ToString());
                }
            }
            return lt;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <returns>返回所有对应键值</returns>
        public static List<int> GetInt<T>(this IEnumerable<T> list, string key)
        {
            var lt = new List<int>();
            foreach (var o in list)
            {
                var obj = GetValue(o, key);
                if (obj != null && int.TryParse(obj.ToString(), out var n))
                {
                    lt.Add(n);
                }
            }
            return lt;
        }
        
        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <returns>返回所有对应键值</returns>
        public static decimal Sum<T>(this IEnumerable<T> list, string key)
        {
            var num = 0m;
            foreach (var o in list)
            {
                var obj = GetValue(o, key);
                if (obj != null)
                {
                    num += obj.ToDecimal();
                }
            }
            return num;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <returns>返回所有对应键值</returns>
        public static List<object> GetValues<T>(this List<T> list, string key)
        {
            var lt = new List<object>();
            foreach (var o in list)
            {
                var obj = GetValue(o, key);
                if (obj != null)
                {
                    lt.Add(obj);
                }
            }
            return lt;
        }

        private static object GetValue<T>(T o, string key)
        {
            object value = null;
            var ps = o.GetType().GetProperties();
            foreach (var p in ps)
            {
                if (p.Name.ToLower() == key)
                {
                    value = p.GetValue(o);
                    break;
                }
            }
            return value;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="value">判断值</param>
        /// <returns>返回所有对应键值</returns>
        public static List<T> Get<T>(this IEnumerable<T> list, string key, object value)
        {
            var lt = new List<T>();
            foreach (var o in list)
            {
                var obj = GetValue(o, key);
                if (obj == value)
                {
                    lt.Add(o);
                }
            }
            return lt;
        }

        /// <summary>
        /// 获取值——第一个匹配对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="value">判断值</param>
        /// <returns>返回所有对应键值</returns>
        public static T GetFirst<T>(this IEnumerable<T> list, string key, object value)
        {
            var m = default(T);
            foreach (var o in list)
            {
                var obj = GetValue(o, key);
                if (obj == value)
                {
                    m = o;
                    break;
                }
            }
            return m;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="m">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool Set<T>(this List<T> list, string key, T m)
        {
            var bl = false;
            var tp = m.GetType().GetProperty(key);
            var value = tp.GetValue(m);
            for (var i = 0; i < list.Count; i++)
            {
                var o = list[i];
                var obj = GetValue(o, key);
                if (obj == value)
                {
                    list[i] = m;
                    bl = true;
                }
            }
            return bl;
        }

        /// <summary>
        /// 设置值——第一个匹配对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="m">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool SetFirst<T>(this List<T> list, string key, T m)
        {
            var bl = false;
            var tp = m.GetType().GetProperty(key);
            var value = tp.GetValue(m);
            for (var i = 0; i < list.Count; i++)
            {
                var o = list[i];
                var obj = GetValue(o, key);
                if (obj == value)
                {
                    list[i] = m;
                    bl = true;
                    break;
                }
            }
            return bl;
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="value">判断值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool Del<T>(this List<T> list, string key, object value)
        {
            var bl = false;
            var arr = new List<int>();
            for (var i = 0; i < list.Count; i++)
            {
                var o = list[i];
                var obj = GetValue(o, key);
                if (obj == value)
                {
                    arr.Add(i);
                    bl = true;
                }
            }
            foreach (var idx in arr) {
                list.RemoveAt(idx);
            }
            return bl;
        }

        /// <summary>
        /// 删除值——第一个匹配对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="value">判断值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static bool DelFirst<T>(this List<T> list, string key, object value)
        {
            var bl = false;
            for (var i = 0; i < list.Count; i++)
            {
                var o = list[i];
                var obj = GetValue(o, key);
                if (obj == value)
                {
                    list.RemoveAt(i);
                    bl = true;
                    break;
                }
            }
            return bl;
        }

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="m">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public static void AddOrSet<T>(this List<T> list, string key, T m)
        {
            var bl = false;
            var tp = m.GetType().GetProperty(key);
            var value = tp.GetValue(m);
            for (var i = 0; i < list.Count; i++)
            {
                var o = list[i];
                var obj = GetValue(o, key);
                if (obj == value)
                {
                    list[i] = m;
                    bl = true;
                    break;
                }
            }
            if (!bl)
            {
                list.Add(m);
            }
        }

        /// <summary>
        /// 判断值是否已存在
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="value">判断值</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public static bool Has<T>(this IEnumerable<T> list, string key, object value)
        {
            var bl = false;
            foreach (var o in list)
            {
                var obj = GetValue(o, key);
                if (obj == value)
                {
                    bl = false;
                    break;
                }
            }
            return bl;
        }

        /// <summary>
        /// 拆分数组
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表或数组</param>
        /// <param name="size">查分大小</param>
        /// <returns>返回二维数组</returns>
        public static List<List<T>> Split<T>(this IEnumerable<T> list, int size)
        {
            List<List<T>> List = new List<List<T>>();

            List<T> arr = new List<T>();

            var lt = list.ToList();
            for (var i = 0; i < lt.Count; i++)
            {
                arr.Add(lt[i]);
                if ((i + 1) % size == 0)
                {
                    List.Add(arr);
                    arr = new List<T>();
                }
            }
            if (arr.Count > 0)
            {
                List.Add(arr);
            }
            return List;
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        public static string ToStr<T>(this IEnumerable<T> list, string symbol = ",")
        {
            var str = "";
            foreach (var o in list) {
                str += symbol + o;
            }
            return str.Substring(symbol.Length);
        }
    }
}
