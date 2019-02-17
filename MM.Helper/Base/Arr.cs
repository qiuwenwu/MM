using System.Collections.Generic;
using System.Linq;

namespace MM.Helper.Base
{
    /// <summary>
    /// 数组帮助类
    /// </summary>
    public class Arr
    {
        #region 数组
        /// <summary>
        /// 差集
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list1">列表1</param>
        /// <param name="list2">列表2</param>
        /// <returns>返回合并结果</returns>
        public T[] Except<T>(T[] list1, T[] list2)
        {
            var list = list1.Except(list2);
            return list.ToArray();
        }

        /// <summary>
        /// 交集
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list1">列表1</param>
        /// <param name="list2">列表2</param>
        /// <returns>返回新列表</returns>
        public T[] Intersect<T>(T[] list1, T[] list2)
        {
            var list = list1.Intersect(list2);
            return list.ToArray();
        }

        /// <summary>
        /// 并集
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list1">列表1</param>
        /// <param name="list2">列表2</param>
        /// <returns>返回新列表</returns>
        public T[] Union<T>(T[] list1, T[] list2)
        {
            var list = list1.Union(list2);
            return list.ToArray();
        }
        #endregion


        #region 列表
        /// <summary>
        /// 差集
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list1">列表1</param>
        /// <param name="list2">列表2</param>
        /// <returns>返回合并结果</returns>
        public List<T> Except<T>(List<T> list1, List<T> list2)
        {
            var list = list1.Except(list2);
            return list.ToList();
        }

        /// <summary>
        /// 交集
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list1">列表1</param>
        /// <param name="list2">列表2</param>
        /// <returns>返回新列表</returns>
        public List<T> Intersect<T>(List<T> list1, List<T> list2)
        {
            var list = list1.Intersect(list2);
            return list.ToList();
        }

        /// <summary>
        /// 并集
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list1">列表1</param>
        /// <param name="list2">列表2</param>
        /// <returns>返回新列表</returns>
        public List<T> Union<T>(List<T> list1, List<T> list2)
        {
            var list = list1.Union(list2);
            return list.ToList();
        }
        #endregion


        #region 列表拓展
        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <returns>返回所有对应键值</returns>
        public List<string> GetStr<T>(IEnumerable<T> list, string key)
        {
            return list.GetStr(key);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <returns>返回所有对应键值</returns>
        public List<int> GetInt<T>(IEnumerable<T> list, string key)
        {
            return list.GetInt(key);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <returns>返回所有对应键值</returns>
        public decimal Sum<T>(IEnumerable<T> list, string key)
        {
            return list.Sum(key);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <returns>返回所有对应键值</returns>
        public List<object> GetValues<T>(List<T> list, string key)
        {
            return list.GetValues(key);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="value">判断值</param>
        /// <returns>返回所有对应键值</returns>
        public List<T> Get<T>(IEnumerable<T> list, string key, object value)
        {
            return list.Get(key, value);
        }

        /// <summary>
        /// 获取值——第一个匹配对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="value">判断值</param>
        /// <returns>返回所有对应键值</returns>
        public T GetFirst<T>(IEnumerable<T> list, string key, object value)
        {
            return list.GetFirst(key, value);
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="m">值</param>
        /// <returns>返回所有对应键值</returns>s
        public bool Set<T>(List<T> list, string key, T m)
        {
            return list.Set(key, m);
        }

        /// <summary>
        /// 设置值——第一个匹配对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="m">值</param>
        /// <returns>返回所有对应键值</returns>
        public bool SetFirst<T>(List<T> list, string key, T m)
        {
            return list.SetFirst(key, m);
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="value">判断值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool Del<T>(List<T> list, string key, object value)
        {
            return list.Del(key, value);
        }

        /// <summary>
        /// 删除值——第一个匹配对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="value">判断值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool DelFirst<T>(List<T> list, string key, object value)
        {
            return list.DelFirst(key, value);
        }

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="key">对应键</param>
        /// <param name="m">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void AddOrSet<T>(List<T> list, string key, T m)
        {
            list.AddOrSet(key, m);
        }

        /// <summary>
        /// 追加对象（通过列表方式）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">当前列表</param>
        /// <param name="list_new">追加列表</param>
        public void Add<T>(List<T> list, IEnumerable<T> list_new)
        {
            foreach (var o in list_new)
            {
                list.Add(o);
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
        public bool Has<T>(IEnumerable<T> list, string key, object value)
        {
            return list.Has(key, value);
        }

        /// <summary>
        /// 拆分数组
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">列表或数组</param>
        /// <param name="size">查分大小</param>
        /// <returns>返回二维数组</returns>
        public List<List<T>> Split<T>(IEnumerable<T> list, int size)
        {
            return list.Split(size);
        }

        /// <summary>
        /// 获取前几个成员
        /// </summary>
        /// <param name="list">列表1</param>
        /// <param name="num">获取数</param>
        /// <returns>返回交集列表</returns>
        public List<T> Take<T>(IEnumerable<T> list, int num)
        {
            return list.Take(num).ToList();
        }

        /// <summary>
        /// 转字符串
        /// </summary>
        public string ToStr<T>(IEnumerable<T> list, string symbol = ",")
        {
            return list.ToStr(symbol);
        }

        /// <summary>
        /// 分割数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="symbol">分隔符</param>
        /// <returns>返回字符串</returns>
        public List<string> Split(string str, string symbol = ",")
        {
            return str.Split(symbol.ToCharArray()).ToList();
        }

        /// <summary>
        /// 分割数组
        /// </summary>
        /// <param name="list">列表</param>
        /// <returns>返回字符串</returns>
        public List<T> ToList<T>(IEnumerable<T> list)
        {
            return list.ToList();
        }

        /// <summary>
        /// 取成员数
        /// </summary>
        /// <param name="list">列表</param>
        /// <returns>返回成员数</returns>
        public int Count<T>(IEnumerable<T> list)
        {
            return list.Count();
        }
        #endregion
    }
}
