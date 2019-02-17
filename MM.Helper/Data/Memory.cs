using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        private MemoryCache _Memory = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 键前缀
        /// </summary>
        private string _Prefix = "mm_";

        /// <summary>
        /// 数据库
        /// </summary>
        public long DB { get; set; } = 0;

        #region 初始
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
        /// 设置当前数据库
        /// </summary>
        /// <param name="db">数据库索引</param>
        public void SetDB(long db)
        {
            DB = db;
        }

        /// <summary>
        /// 设置当前数据库
        /// </summary>
        public long GetDB()
        {
            return DB;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="linkStr">链接字符串</param>
        public void Init(string linkStr = null)
        {
            _Memory = new MemoryCache(new MemoryCacheOptions());
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _Memory.Dispose();
            _Memory = null;
        }

        /// <summary>
        /// 结束函数
        /// </summary>
        ~Memory()
        {
            Dispose();
        }
        #endregion


        #region 基础
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void Del(string key)
        {
            _Memory.Remove(key);
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
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        public object Get(string key)
        {
            return _Memory.Get(key);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">键</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        public T Get<T>(string key)
        {
            return _Memory.Get<T>(key);
        }
        #endregion


        #region 拓展
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
            var bl = false;
            var dt = Dict();
            if (dt.Count > 0)
            {
                bl = true;
            }
            File.WriteAllText(file, dt.ToJson(true, false));
            return bl;
        }

        /// <summary>
        /// 获取所有键
        /// </summary>
        /// <param name="key">键关键词</param>
        /// <param name="mode">匹配方式，start匹配前缀，end匹配后缀，regex匹配正则</param>
        /// <returns>返回键列表</returns>
        public List<string> GetKeys(string key = "", string mode = "start")
        {
            var list = new List<string>();
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var entries = _Memory.GetType().GetField("_entries", flags).GetValue(_Memory);
        
            if (!(entries is IDictionary dt))
            {
                return list;
            }
            if (string.IsNullOrEmpty(key))
            {
                foreach (var o in dt)
                {
                    list.Add(o.ToString());
                }
            }
            else if (mode == "start")
            {
                foreach (var o in dt)
                {
                    var k = o.ToString();
                    if (k.StartsWith(key))
                    {
                        list.Add(k);
                    }
                }
            }
            else if (mode == "end")
            {
                foreach (var o in dt)
                {
                    var k = o.ToString();
                    if (k.EndsWith(key))
                    {
                        list.Add(k);
                    }
                }
            }
            else
            {
                foreach (var o in dt)
                {
                    var k = o.ToString();
                    if (Regex.IsMatch(k, key))
                    {
                        list.Add(k);
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
        /// 导入
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="longTime">滑动过期时间</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool Import(string file, int longTime)
        {
            var bl = false;
            var str = File.ReadAllText(file);
            if (!string.IsNullOrEmpty(str))
            {
                var dt = str.Loads<Dictionary<string, object>>();
                if (dt.Count > 0)
                {
                    bl = true;
                    foreach (var o in dt)
                    {
                        Set(o.Key, o.Value, longTime);
                    }
                }
            }
            return bl;
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="dateTime">到期时间</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool Import(string file, string dateTime)
        {
            var bl = false;
            var str = File.ReadAllText(file);
            if (!string.IsNullOrEmpty(str))
            {
                var dt = str.Loads<Dictionary<string, object>>();
                if (dt.Count > 0)
                {
                    bl = true;
                    foreach (var o in dt) {
                        Set(o.Key, o.Value, dateTime);
                    }
                }
            }
            return bl;
        }

        /// <summary>
        /// 修改——通过函数式方式修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="funStr">函数式</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void SetEval(string key, string funStr)
        {
            var value = Get(key);
            _Memory.Set(key, Eval.Execute(string.Format(funStr, value)));
        }

        /// <summary>
        /// 修改——通过函数进行修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="fun">函数</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void SetFun(string key, Func<object, object> fun)
        {
            var value = Get(key);
            _Memory.Set(key, fun(value));
        }
        #endregion
    }
}
