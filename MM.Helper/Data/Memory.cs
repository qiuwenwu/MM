using Microsoft.Extensions.Caching.Memory;
using MM.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Z.Expressions;

namespace MM.Helper.Data
{
    /// <summary>
    /// 内存缓存帮助类
    /// </summary>
    public class Memory : ICache
    {
        /// <summary>
        /// 内存初始化
        /// </summary>
        static MemoryCache _Memory = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 键前缀
        /// </summary>
        public string _Prefix { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            _Memory = new MemoryCache(new MemoryCacheOptions());
        }

        #region 操作
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void Del(string key)
        {
            _Memory.Remove(key);
            DelKey(key);
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="key">键关键词，为空则匹配所有</param>
        /// <param name="mode">查询方式：1、startWith匹配前缀；2、endWith匹配后缀；3、regex匹配正则表达式</param>
        /// <returns>返回查询结果集合</returns>
        public Dictionary<string, object> Dict(string key = "", string mode = "startWith")
        {
            var dt = new Dictionary<string, object>();
            var keys = GetKeys(key, mode);
            foreach (var o in keys) {
                dt.Add(o, Get(o));
            }
            return dt;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool Export(string file)
        {

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        public object Get(string key)
        {
            return Get(key);
        }

        /// <summary>
        /// 获取所有键
        /// </summary>
        /// <param name="key">键关键词</param>
        /// <param name="mode">匹配方式，start匹配前缀，end匹配后缀，regex匹配正则</param>
        /// <returns>返回键列表</returns>
        public List<string> GetKeys(string key = "", string mode = "start")
        {
            var str = _Memory.Get<string>("memoryKeys");
            var arr = str.Split("~");
            var list = new List<string>();
            if (string.IsNullOrEmpty(key))
            {
                list = arr;
            }
            else if (mode == "start")
            {
                foreach (var o in arr)
                {
                    if (o.StartsWith(key))
                    {
                        list.Add(o);
                    }
                }
            }
            else if (mode == "end")
            {
                foreach (var o in arr)
                {
                    if (o.EndsWith(key))
                    {
                        list.Add(o);
                    }
                }
            }
            else
            {
                foreach (var o in arr)
                {
                    if (Regex.IsMatch(o, key))
                    {
                        list.Add(o);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 判断值是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>有则返回true，没有则返false</returns>
        public bool Has(string key)
        {
            return _Memory.TryGetValue(key, out var value);
        }

        /// <summary>
        /// 获取或设置主键前缀
        /// </summary>
        /// <param name="key_prefix">键前缀名, 为空则获取前缀</param>
        public string Head(string key_prefix = null)
        {
            if (key_prefix != null)
            {
                _Prefix = key_prefix;
            }
            return _Prefix;
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool Import(string file)
        {

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void Set(string key, object value)
        {
            _Memory.Set(key, value);
            SetKey(key);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="longTime">缓存时长</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void Set(string key, object value, int longTime)
        {
            _Memory.Set(key, value, new TimeSpan(longTime));
            SetKey(key);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="dateTime">到期时间</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void Set(string key, object value, string dateTime)
        {
            _Memory.Set(key, value, DateTime.Parse(dateTime));
            SetKey(key);
        }

        /// <summary>
        /// 修改——通过函数式方式修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="funStr">函数式</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void SetEval(string key, string funStr)
        {
            var value = _Memory.Get(key);
            _Memory.Set(key, Eval.Execute(string.Format(funStr, value)));
            SetKey(key);
        }

        /// <summary>
        /// 修改——通过函数进行修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="fun">函数</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void SetFun(string key, Func<object, object> fun)
        {
            var value = _Memory.Get(key);
            _Memory.Set(key, fun(value));
            SetKey(key);
        }

        private void SetKey(string key)
        {
            var keys = _Memory.Get<string>("memoryKeys");
            var str = "~" + keys;
            if (!str.Contains("~" + key + "~"))
            {
                str = keys + key + "~";
                _Memory.Set("memoryKeys", str);
            }
        }

        private void DelKey(string key)
        {
            var keys = _Memory.Get<string>("memoryKeys");
            var str = "~" + keys;
            _Memory.Set("memoryKeys", str.Replace("~" + key + "~", "~"));
        }
        #endregion
    }
}
