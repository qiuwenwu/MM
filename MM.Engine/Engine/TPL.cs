using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MM.Engine
{
    /// <summary>
    /// Razor模板引擎帮助类
    /// </summary>
    public class TPL
    {

        /// <summary>
        /// 模板主题风格
        /// </summary>
        public string Theme { get { return System.Cache._Theme; } set { if (!string.IsNullOrEmpty(value)) { System.Cache._Theme = value; } } }


        /// <summary>
        /// 模板路径
        /// </summary>
        public string Dir { get; set; } = System.Cache.path.Template;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Ex { get; private set; } = string.Empty;

        private static IRazorEngineService razor = RazorEngine.Engine.Razor;

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            SetManager();
            //添加文件修改监控，以便在cshtml文件修改时重新编译该文件
            Watcher.Path = System.Cache.path.Web;
            Watcher.IncludeSubdirectories = true;
            Watcher.Filter = "*.*html";
            Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            Watcher.Created += new FileSystemEventHandler(OnChanged);
            Watcher.Changed += new FileSystemEventHandler(OnChanged);
            Watcher.Deleted += new FileSystemEventHandler(OnChanged);
            Watcher.EnableRaisingEvents = true;

            var config = new TemplateServiceConfiguration()
            {
                Namespaces = new HashSet<string>() { "Microsoft.CSharp", "MM.Engine" }, // 引入命名空间
                Language = Language.CSharp,
                EncodedStringFactory = new HtmlEncodedStringFactory(),
                DisableTempFileLocking = true,
                TemplateManager = Mg,
                BaseTemplateType = typeof(MmTemplateBase<>),
                CachingProvider = Cache
            };
            Cache.InvalidateAll();
            razor = RazorEngineService.Create(config);
        }

        private static void SetManager()
        {
            Mg = new MmTemplateManager(InFunc);
        }

        private static string InFunc(string arg)
        {
            var fe = arg.ToFullName();
            if (File.Exists(fe))
            {
                return File.ReadAllText(fe, System.Text.Encoding.UTF8);
            }
            return "";
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            string key = e.FullPath.ToLower();
            Cache.InvalidateCache(key);
        }



        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            Cache.InvalidateAll();
        }

        #region 视图背包
        /// <summary>
        /// 视图背包
        /// </summary>
        public static DynamicViewBag viewBag = new DynamicViewBag();

        /// <summary>
        /// 新建视图背包
        /// </summary>
        public DynamicViewBag NewViewBag_default()
        {
            viewBag = new DynamicViewBag(viewBag);
            return viewBag;
        }

        /// <summary>
        /// 视图背包添加成员
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Add_default(string key, string value)
        {
            viewBag.AddValue(key, value);
        }

        /// <summary>
        /// 视图背包添加字典
        /// </summary>
        /// <param name="dt">字典类型</param>
        public void Add_default(Dictionary<string, object> dt)
        {
            viewBag.AddDictionary(dt);
        }

        /// <summary>
        /// 获取所有成员名称
        /// </summary>
        public List<string> GetKeys_default()
        {
            var arr = viewBag.GetDynamicMemberNames();
            if (arr != null)
            {
                return arr.ToList();
            }
            return new List<string>();
        }
        #endregion

        #region 视图背包
        /// <summary>
        /// 视图背包
        /// </summary>
        public DynamicViewBag ViewBag { get; set; } = new DynamicViewBag();

        /// <summary>
        /// 布局
        /// </summary>
        public static Regex LayoutEx { get; } = new Regex("Layout\\s*=\\s*@?\"(\\S*)\";");

        /// <summary>
        /// 缓存提供程序
        /// </summary>
        public static MmCachingProvider Cache { get; set; } = new MmCachingProvider();
        /// <summary>
        /// 文件监听程序
        /// </summary>
        public static FileSystemWatcher Watcher { get; set; } = new FileSystemWatcher();
        /// <summary>
        /// 模板管理器
        /// </summary>
        public static MmTemplateManager Mg { get; set; }

        /// <summary>
        /// 新建视图背包
        /// </summary>
        public DynamicViewBag NewViewBag()
        {
            ViewBag = new DynamicViewBag(viewBag);
            return ViewBag;
        }

        /// <summary>
        /// 新建字典
        /// </summary>
        /// <returns>返回新字典</returns>
        public Dictionary<string, object> NewDt()
        {
            return new Dictionary<string, object>();
        }

        /// <summary>
        /// 视图背包添加成员
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Add(string key, string value)
        {
            ViewBag.AddValue(key, value);
        }

        /// <summary>
        /// 视图背包添加字典
        /// </summary>
        /// <param name="dt">字典类型</param>
        public void Add(Dictionary<string, object> dt)
        {
            ViewBag.AddDictionary(dt);
        }

        /// <summary>
        /// 获取所有成员名称
        /// </summary>
        public List<string> GetKeys()
        {
            var arr = ViewBag.GetDynamicMemberNames();
            if (arr != null)
            {
                return arr.ToList();
            }
            return new List<string>();
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dir">模板路径</param>
        public TPL(string dir = "")
        {
            Dir = dir;
            NewViewBag();
        }

        #region 文本渲染,适用于web客户端
        /// <summary>
        /// 预编译模板
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="template">模板内容</param>
        public void Compile(string key, string template)
        {
            razor.Compile(template, key);
        }

        /// <summary>
        /// 渲染数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="template">模板</param>
        /// <param name="model">数据模型</param>
        /// <param name="tp">模板类型</param>
        /// <returns>返回渲染后的字符串</returns>
        public string Format(string key, string template, object model = null, Type tp = null)
        {
            var ret = "";
            try
            {
                var keyString = key.Replace(System.Cache.runPath, "");
                ITemplateKey km = razor.GetKey(keyString);
                var bl = razor.IsTemplateCached(km, tp);
                if (bl)
                {
                    ret = razor.Run(km, tp, model, ViewBag);
                }
                else
                {
                    ret = razor.RunCompile(template, km, tp, model, ViewBag);
                }
            }
            catch (Exception ex)
            {
                Ex = ex.ToString();
            }
            return ret;
        }

        /// <summary>
        /// 渲染数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="template">模板</param>
        /// <param name="model">数据模型</param>
        public string FormatS(string key, LoadedTemplateSource template, object model)
        {
            var ret = "";
            try
            {
                var km = razor.GetKey(key);
                Type tp = null;
                var bl = razor.IsTemplateCached(km, tp);
                if (bl)
                {
                    ret = razor.Run(km, tp, model, ViewBag);
                }
                else
                {
                    ret = razor.RunCompile(template, km, tp, model, ViewBag);
                }
            }
            catch (Exception ex)
            {
                Ex = ex.ToString();
            }
            return ret;
        }

        /// <summary>
        /// 加载模板资源
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="templateFile">模板文件</param>
        /// <returns>返回模板资源模型</returns>
        public LoadedTemplateSource LoadTpl(string template, string templateFile = null)
        {
            return new LoadedTemplateSource(template, templateFile);
        }

        /// <summary>
        /// 模板渲染
        /// </summary>
        /// <param name="file">模板文件</param>
        /// <returns>返回渲染后文本</returns>
        public string Load(string file)
        {
            var fe = file.ToFullName(Dir);
            if (File.Exists(fe))
            {
                return File.ReadAllText(fe, System.Text.Encoding.UTF8);
            }
            Ex = "模板文件不存在";
            return "";
        }

        /// <summary>
        /// 模板渲染
        /// </summary>
        /// <param name="file">文件全名</param>
        /// <param name="model">模型</param>
        /// <returns>返回渲染后文本</returns>
        public string View(string file, object model = null)
        {
            file = file.ToFullName(Dir);
            var text = Load(file);
            if (!string.IsNullOrEmpty(text))
            {
                return Format(file, text, model);
            }
            return "";
        }
        #endregion

        /// <summary>
        /// 释放模板帮助类
        /// </summary>
        public void Dispose()
        {
            Cache.Dispose();
            Mg.Dispose();
        }
    }
}
