using RazorEngine.Templating;
using System;

namespace MM.Engine
{
    /// <summary>
    /// 基本模板类型
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public abstract class MmTemplateBase<T> : TemplateBase<T>
    {
        /// <summary>
        /// 模板主题风格
        /// </summary>
        public string Theme { get { return Cache._Theme; } set { if (!string.IsNullOrEmpty(value)) { Cache._Theme = value; } } }

        /// <summary>
        /// 当前文件路径
        /// </summary>
        public string Dir { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MmTemplateBase()
        {

        }

        /// <summary>
        /// 引用视图
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回视图</returns>
        public TemplateWriter View(string name)
        {
            var file = name.ToFullName(Dir);
            return Include(file);
        }

        /// <summary>
        /// 引用视图
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="model">模板</param>
        /// <param name="tp">模板类型</param>
        /// <returns>返回视图</returns>
        public TemplateWriter View(string name, object model, Type tp = null)
        {
            var file = name.ToFullName(Dir);
            return Include(file, model, tp);
        }
    }
}