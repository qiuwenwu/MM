using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MM.Helper.Data
{
    /// <summary>
    /// ini配置帮助类
    /// </summary>
    public class Ini : IDisposable
    {
        #region 属性
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        /// <summary>
        /// 当前路径
        /// </summary>
        public string Dir      { get; set; } = "";

        /// <summary>
        /// INI文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Ex { get; private set; }
        #endregion


        #region 核心函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">配置文件</param>
        public Ini(string fileName = "app.ini")
        {
            Init(fileName);
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init(string fileName)
        {
            FileName = fileName.ToFullName(Dir);
        }

        #endregion


        #region 键值基本函数
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool Del(string section, string key){
            return Set(section, key, "");
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">默认值</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        public object Get(string section, string key, object value = null){
            var val = "";
            if (val != null)
            {
                val = value.ToString();
            }
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(section, key, val, Buffer, Buffer.GetUpperBound(0), FileName);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">默认值</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        public T Get<T>(string section, string key, object value = null)
        {
            return (T)Get(section, key, value);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        public bool Set(string section, string key, object value)
        {
            return WritePrivateProfileString(section, key, value.ToString(), FileName);
        }
        #endregion


        #region 键值
        /// <summary>
        /// 是否有该键
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键名</param>
        /// <returns>有则返回true，没有则返false</returns>
        public bool Has(string section, string key){
            return Get(section, key) != null;
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">默认值</param>
        /// <returns>返回数值</returns>
        public int GetInt(string section, string key, int value = 0)
        {
            var str = Get(section, key, value).ToString();
            int.TryParse(str, out int n);
            return n;
        }

        /// <summary>
        /// 获取文本值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">默认值</param>
        /// <returns>返回文本值</returns>
        public string GetStr(string section, string key, int value = 0)
        {
            return Get(section, key, value).ToString();
        }

        /// <summary>
        /// 获取文本值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">默认值</param>
        /// <returns>返回文本值</returns>
        public bool GetBool(string section, string key, int value = 0)
        {
            var str = Get(section, key, value).ToString();
            bool.TryParse(str, out bool bl);
            return bl;
        }

        /// <summary>
        /// 读取节下所有值
        /// </summary>
        /// <param name="section">节</param>
        public Dictionary<string, object> GetValues(string section)
        {
            var dt = new Dictionary<string, object>();
            var keys = GetKeys(section);
            foreach (string key in keys)
            {
                dt.Add(key, Get(section, key, ""));
            }
            return dt;
        }
        #endregion


        #region 键操作
        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="section">配置节</param>
        /// <param name="key">配置键</param>
        public bool DelKey(string section, string key)
        {
            return WritePrivateProfileString(section, key, null, FileName);
        }

        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="section">节</param>
        /// <returns>返回所有的键</returns>
        public List<string> GetKeys(string section)
        {
            Byte[] Buffer = new Byte[16384];
            int bufLen = GetPrivateProfileString(section, null, null, Buffer, Buffer.GetUpperBound(0), FileName);
            
            return GetStringsFromBuffer(Buffer, bufLen);
        }
        #endregion


        #region 节操作
        private List<string> GetStringsFromBuffer(Byte[] Buffer, int bufLen)
        {
            List<string> list = new List<string>();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        string s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        list.Add(s);
                        start = i + 1;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 从Ini文件中，读取所有节的名称
        /// </summary>
        public List<string> GetSections()
        {
            //Note:必须得用Bytes来实现，StringBuilder只能取到第一个Section
            byte[] Buffer = new byte[65535];
            int bufLen = GetPrivateProfileString(null, null, null, Buffer, Buffer.GetUpperBound(0), FileName);
            return GetStringsFromBuffer(Buffer, bufLen);
        }

        /// <summary>
        /// 删除节
        /// </summary>
        /// <param name="section">配置节</param>
        public void DelSection(string section)
        {
            WritePrivateProfileString(section, null, null, FileName);
        }
        #endregion

        /// <summary>
        /// 更新配置文件
        /// </summary>
        public void Update()
        {
            WritePrivateProfileString(null, null, null, FileName);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            Dir = null;
            Update();
        }

        /// <summary>
        /// 确保资源释放
        /// </summary>
        ~Ini()
        {
            Dispose();
        }
    }
}
