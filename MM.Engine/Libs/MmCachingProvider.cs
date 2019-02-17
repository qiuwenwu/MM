using System;
using RazorEngine.Templating;
using System.Collections.Concurrent;
using System.Reflection;
using System.Dynamic;

namespace MM.Engine
{
    /// <summary>
    /// 默认缓存提供者(见ICachingProvider可见)。
    /// 这个实现做一个非常简单的内存缓存。
    /// 它可以处理时使用相同的模板有多个模型类型。
    /// </summary>
    public class MmCachingProvider : ICachingProvider
    {
        internal static ConcurrentDictionary<string, ConcurrentDictionary<Type, ICompiledTemplate>> _cache = new ConcurrentDictionary<string, ConcurrentDictionary<Type, ICompiledTemplate>>();
        private ConcurrentBag<Assembly> _assemblies = new ConcurrentBag<Assembly>();

        /// <summary>
        /// 实例化
        /// </summary>
        public MmCachingProvider()
        {
            TypeLoader = new TypeLoader(AppDomain.CurrentDomain, _assemblies);
        }

        /// <summary>
        /// 类加载器
        /// </summary>
        public TypeLoader TypeLoader { get; }

        /// <summary>
        /// 缓存模板
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="key">模板键</param>
        public void CacheTemplate(ICompiledTemplate template, ITemplateKey key)
        {
            var modelTypeKey = GetModelTypeKey(template.ModelType);
            CacheTemplateHelper(template, key, modelTypeKey);
            var typeArgs = template.TemplateType.BaseType.GetGenericArguments();
            if (typeArgs.Length > 0)
            {
                var alternativeKey = GetModelTypeKey(typeArgs[0]);
                if (alternativeKey != modelTypeKey)
                {
                    // 可能是模板 @model 指令。
                    CacheTemplateHelper(template, key, typeArgs[0]);
                }
            }
        }

        internal void InvalidateAll()
        {
            _cache.Clear();
        }

        /// <summary>
        /// 尝试检索模板
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="modelType">模型名称</param>
        /// <param name="template">模板</param>
        /// <returns>检索到返回true，否则返回false</returns>
        public bool TryRetrieveTemplate(ITemplateKey key, Type modelType, out ICompiledTemplate template)
        {
            template = null;
            var uniqueKey = key.GetUniqueKeyString();
            var modelTypeKey = GetModelTypeKey(modelType);
            if (!_cache.TryGetValue(uniqueKey, out ConcurrentDictionary<Type, ICompiledTemplate> dict))
            {
                return false;
            }
            return dict.TryGetValue(modelTypeKey, out template);
        }

        /// <summary>
        /// 缓存失效
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>设置成功返回true, 失败返回false</returns>
        internal bool InvalidateCache(ITemplateKey key)
        {
            var uniqueKey = key.GetUniqueKeyString();
            return InvalidateCache(uniqueKey);
        }

        /// <summary>
        /// 缓存失效
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>设置成功返回true, 失败返回false</returns>
        internal bool InvalidateCache(string key)
        {
            if (_cache.TryRemove(key, out ConcurrentDictionary<Type, ICompiledTemplate> dict))
            {
                return true;
            }
            return false;
        }

        #region 辅助函数
        /// <summary>
        /// 得到关键modelType中使用一个字典。
        /// </summary>
        public static Type GetModelTypeKey(Type modelType)
        {
            if (modelType == null ||
                typeof(IDynamicMetaObjectProvider).IsAssignableFrom(modelType))
            {
                return typeof(DynamicObject);
            }
            return modelType;
        }

        /// <summary>
        /// 缓存模板助手
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="templateKey">模板键</param>
        /// <param name="modelTypeKey">模板类型键</param>
        private void CacheTemplateHelper(ICompiledTemplate template, ITemplateKey templateKey, Type modelTypeKey)
        {
            var uniqueKey = templateKey.GetUniqueKeyString();
            _cache.AddOrUpdate(uniqueKey, key =>
            {
                // 添加新项
                _assemblies.Add(template.TemplateAssembly);
                var dict = new ConcurrentDictionary<Type, ICompiledTemplate>();
                dict.AddOrUpdate(modelTypeKey, template, (t, old) => template);
                return dict;
            }, (key, dict) =>
            {
                dict.AddOrUpdate(modelTypeKey, t =>
                {
                    // 添加新条目(模板没有编译给定类型)
                    _assemblies.Add(template.TemplateAssembly);
                    return template;
                }, (t, old) =>
                {
                    // 项目已经添加
                    return template;
                });
                return dict;
            });
        }
        #endregion

        /// <summary>
        /// 释放缓存
        /// </summary>
        public void Dispose()
        {
            _cache.Clear();
            TypeLoader.Dispose();
        }
    }
}
