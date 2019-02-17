using RazorEngine.Templating;
using System;
using System.Collections.Concurrent;

namespace MM.Engine
{
    /// <summary>
    /// 模板管理器
    /// </summary>
    public class MmTemplateManager : ITemplateManager
    {

        #region 字段
        private readonly Func<string, string> _resolver;

        private readonly ConcurrentDictionary<ITemplateKey, ITemplateSource> _dynamicTemplates = new ConcurrentDictionary<ITemplateKey, ITemplateSource>();
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public MmTemplateManager() : base() {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MmTemplateManager(Func<string, string> resolver)
        {
            _resolver = resolver ?? new Func<string, string>(name =>
            {
                throw new ArgumentException(string.Format("请设置一个模板管理器解决模板或添加模板 '{0}'!", name));
            });
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="source">源</param>
        public void AddDynamic(ITemplateKey key, ITemplateSource source)
        {
            _dynamicTemplates.AddOrUpdate(key, source, (k, oldSource) =>
            {
                return source;
            });
        }

        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="resolveType">解决类型</param>
        /// <param name="key">键</param>
        /// <returns>返回键</returns>
        public ITemplateKey GetKey(string name, ResolveType resolveType, ITemplateKey key)
        {
            // 如果你可以有不同的模板具有相同名称的根据
            // 上下文或resolveType你需要自己实现!
            // 否则你可以使用NameOnlyTemplateKey。
            return new NameOnlyTemplateKey(name, resolveType, key);
        }

        /// <summary>
        /// 移除键
        /// </summary>
        /// <param name="key">主键</param>
        public void RemoveDynamic(ITemplateKey key)
        {
            _dynamicTemplates.TryRemove(key, out ITemplateSource source);
        }

        /// <summary>
        /// 解决源
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回资源模板</returns>
        public ITemplateSource Resolve(ITemplateKey key)
        {
            if (_dynamicTemplates.TryGetValue(key, out ITemplateSource result))
            {
                return result;
            }
            var str = _resolver(key.Name);
            return new LoadedTemplateSource(str);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _dynamicTemplates.Clear();
        }
    }
}