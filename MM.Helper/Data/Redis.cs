using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Z.Expressions;

namespace MM.Helper.Data
{
    /// <summary>
    /// Redis帮助类
    /// </summary>
    public class Redis : ICache, IDisposable
    {
        #region 属性
        /// <summary>
        /// 默认连接字符串，当ConnStr为空时使用该连接
        /// </summary>
        public static string connStr_default = "asd123@127.0.0.1:6379";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Ex              { get; set; } = string.Empty;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnStr         { get; set; } = connStr_default;

        /// <summary>
        /// 连接的数据库序号
        /// </summary>
        public long DB                { get; set; } = 0;

        /// <summary>
        /// 键前缀
        /// </summary>
        private string _Prefix = "mm_";

        /// <summary>
        /// 汇集Redis客户端管理器
        /// </summary>
        private PooledRedisClientManager conn;

        /// <summary>
        /// Rdeis通讯接口
        /// </summary>
        private IRedisClient _Redis;
        #endregion


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
        /// <param name="connStr">链接字符串</param>
        public void Init(string connStr = null)
        {
            // 如果初始化参数为空，使用默认参数
            if (!string.IsNullOrEmpty(connStr)) {
                if (ConnStr.Contains(";"))
                {
                    var m = ToLink(connStr);
                    ConnStr = string.Format("{0}@{1}:{2}", m.Password, m.Server, m.Port);
                    if (long.TryParse(m.Database, out var db))
                    {
                        DB = db;
                    }
                }
                else
                {
                    ConnStr = connStr;
                }
            }
            conn = new PooledRedisClientManager(10000, 10, new string[] { ConnStr })
            {
                ConnectTimeout = 1200000 // 1000 * 60 * 20
            };
        }

        /// <summary>
        /// 开启连接
        /// </summary>
        public void Open() {
            _Redis = conn.GetClient();
            _Redis.Db = DB;
        }

        /// <summary>
        /// 转换链接
        /// </summary>
        /// <param name="_linkConfig">Redis链接配置</param>
        private static ServerConfig ToLink(string _linkConfig)
        {
            var m = new ServerConfig();
            var arr = _linkConfig.ToLower().Split(';');
            foreach (var o in arr)
            {
                if (o.Contains("server"))
                {
                    var ar = o.Split('=');
                    if (ar.Length > 1)
                    {
                        m.Server = ar[1];
                    }
                }
                else if (o.Contains("port"))
                {
                    var ar = o.Split('=');
                    if (ar.Length > 1)
                    {
                        var value = ar[1];
                        if (int.TryParse(value, out var n)) {
                            m.Port = n;
                        }
                    }
                }
                else if (o.Contains("password"))
                {
                    var ar = o.Split('=');
                    if (ar.Length > 1)
                    {
                        m.Password = ar[1];
                    }
                }
                else if (o.Contains("username"))
                {
                    var ar = o.Split('=');
                    if (ar.Length > 1)
                    {
                        m.Password = ar[1];
                    }
                }
                else if (o.Contains("database"))
                {
                    var ar = o.Split('=');
                    if (ar.Length > 1)
                    {
                        m.Database = ar[1];
                    }
                }
            }
            return m;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (_Redis != null)
            {
                _Redis.Dispose();
                _Redis = null;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_Redis != null)
            {
                _Redis.Dispose();
                _Redis = null;
            }
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }

        /// <summary>
        /// 结束函数
        /// </summary>
        ~Redis() {
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
            _Redis.Remove(key);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public void Set(string key, object value)
        {
            _Redis.Set(key, value);
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
            _Redis.Set(key, value, new TimeSpan(longTime));
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
            _Redis.Set(key, value, DateTime.Parse(dateTime));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        public object Get(string key)
        {
            return _Redis.Get<object>(key);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">键</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        public T Get<T>(string key)
        {
            return _Redis.Get<T>(key);
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
            foreach (var o in keys)
            {
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
            var arr = _Redis.GetAllKeys();
            if (arr.Count == 0)
            {
                return list;
            }
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
            bool bl = false;
            try
            {
                using (var client = conn.GetClient())
                {
                    bl = client.ContainsKey(_Prefix + key);
                }
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
            }
            return bl;
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
                    foreach (var o in dt)
                    {
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
            _Redis.Set(key, Eval.Execute(string.Format(funStr, value)));
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
            _Redis.Set(key, fun(value));
        }
        #endregion
    }

    /// <summary>
    /// 服务器
    /// </summary>
    public class ServerConfig
    {
        /// <summary>
        /// 主机地址/IP
        /// </summary>
        public string Server   { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port        { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public string Database { get; set; }
    }
}
