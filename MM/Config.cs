using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MM
{
    /// <summary>
    /// 配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 是否开启调试模式
        /// </summary>
        public static bool isDebug = false;
        /// <summary>
        /// 是否开启调试模式
        /// </summary>
        public bool IsDebug { get { return isDebug; } set { isDebug = value; } }

        /// <summary>
        /// 应用名称
        /// </summary>
        public static string appName = "mm";
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get { return appName; } set { appName = value; } }

        /// <summary>
        /// 是否开启伪静态
        /// </summary>
        public static bool rewriteEngine = false;
        /// <summary>
        /// 是否开启伪静态
        /// </summary>
        public bool RewriteEngine { get { return rewriteEngine; } set { rewriteEngine = value; } }

        /// <summary>
        ///  监听域名地址
        /// </summary>
        public static string[] urls = new string[] { };
        /// <summary>
        ///  监听域名地址
        /// </summary>
        public string[] Urls { get { return urls; } set { urls = value; } }

        /// <summary>
        /// 伪静态
        /// </summary>
        public static Dictionary<string, string> rewriteDt = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 伪静态
        /// </summary>
        public Dictionary<string, string> RewriteDt { get { return rewriteDt; } set { rewriteDt = value; } }
        
        /// <summary>
        /// 目录
        /// </summary>
        public string Dir { get; set; } = "";

        /// <summary>
        /// 应用变量字典
        /// </summary>
        public Dictionary<string, object> VarDt { get; set; } = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// https端口
        /// </summary>
        public static int httpsPort = 0;
        /// <summary>
        /// https端口
        /// </summary>
        public int HttpsPort { get { return httpsPort; } set { httpsPort = value; } }

        /// <summary>
        /// 更新配置
        /// </summary>
        public void Update()
        {
            //创建相关目录
            CreateDirectory();
            UpdateSystem();
            UpdateError();
            UpdateRewrite();
        }

        /// <summary>
        /// 创建新目录
        /// </summary>
        public void CreateDirectory()
        {
            var Dy = System.Cache.path.Config;
            if (!Directory.Exists(Dy))
            {
                Directory.CreateDirectory(Dy);
            }
        }
        
        /// <summary>
        /// 更新错误提示字典
        /// </summary>
        public void UpdateError()
        {

        }

        /// <summary>
        /// 更新系统配置
        /// </summary>
        public void UpdateSystem()
        {
        }

        /// <summary>
        /// 更新伪静态配置
        /// </summary>
        public void UpdateRewrite()
        {
            var file = System.Cache.path.Config + @"Rewrite.json";
            if (!File.Exists(file))
            {
                var dt = new Dictionary<string, string>() {
                {"^(.*)/article-(\\d+)-(\\d+)\\.html$", @"{$1}/article?mod=view&aid={$2}&page={$3}"},
                {"^(.*)/channel-(\\d+)-(\\d+)\\.html$", "{$1}/channel?mod=channel&cid={$2}&page={$3}"}
            };
                File.WriteAllText(file, dt.ToJson(), Encoding.UTF8);
            }
            if (File.Exists(file))
            {
                string jsonStr = File.ReadAllText(file);
                var obj = jsonStr.Loads<Dictionary<string, string>>();
                EachSet(rewriteDt, obj);
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="file">文件路径 </param>
        /// <returns>返回文件</returns>
        private string LoadFile(string file)
        {
            try
            {
                if (!file.Contains(":"))
                {
                    file = string.Format(@"{0}config\{1}.json", Dir, file);
                }
                if (!File.Exists(file))
                {
                    File.WriteAllText(file, "{}", Encoding.UTF8);
                }
                else
                {
                    return File.ReadAllText(file);
                }
            }
            catch (Exception ex)
            {
                Ex = ex.ToString();
            }
            return "{}";
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="text">字符串型文件内容</param>
        /// <returns>保存成功返回true，失败返回false</returns>
        private bool SaveFile(string fileName, string text)
        {
            var file = string.Format(@"{0}config\{1}.json", Dir, fileName);
            File.WriteAllText(file, text, Encoding.UTF8);
            if (File.Exists(file))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="name">文件名</param>
        /// <returns>返回字符串型文件内容</returns>
        public void Load(string name = "var")
        {
            if (VarDt.Count == 0)
            {
                var str = LoadFile(name);
                try
                {
                    var dt = str.Loads<Dictionary<string, object>>();
                    if (dt != null)
                    {
                        EachSetVar(dt);
                    }
                }
                catch (Exception ex)
                {
                    Ex = ex.ToString();
                }
            }
        }

        /// <summary>
        /// 读取语言
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">默认值</param>
        /// <returns>如果结果非空，返回结果，否则返回默认值</returns>
        public object Get(string key, object value = null)
        {
            if (VarDt.ContainsKey(key))
            {
                return VarDt[key];
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Set(string key, object value)
        {
            if (VarDt.ContainsKey(key))
            {
                VarDt[key] = value;
            }
            else
            {
                VarDt.Add(key, value);
            }
        }

        /// <summary>
        /// 保存语言包
        /// </summary>
        /// <returns>保存成功返回true，失败返回false</returns>
        public bool Save(string name = "var")
        {
            try
            {
                var str = VarDt.ToJson();
                return SaveFile(name, str);
            }
            catch (Exception ex)
            {
                Ex = ex.ToString();
            }
            return false;
        }

        /// <summary>
        /// 遍历设置变量
        /// </summary>
        /// <param name="vardt">变量字典</param>
        public void EachSetVar(Dictionary<string, object> vardt) {
            VarDt.Clear();
            foreach (var o in vardt) {
                VarDt.Add(o.Key, o.Value);
            }
        }

        /// <summary>
        /// 遍历设置变量
        /// </summary>
        /// <param name="dt_old">原变量字典</param>
        /// <param name="dt_new">新变量字典</param>
        public void EachSet(Dictionary<string, string> dt_old, Dictionary<string, string> dt_new)
        {
            dt_old.Clear();
            foreach (var o in dt_new)
            {
                dt_old.Add(o.Key, o.Value);
            }
        }
    }
}