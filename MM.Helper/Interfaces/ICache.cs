using System;
using System.Collections.Generic;

namespace MM.Helper.Interfaces
{
    /// <summary>
    /// 缓存类接口
    /// </summary>
    public interface ICache
    {
        #region 操作
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>成功返回true，失败返回false</returns>
        void Del(string key);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="key">键关键词，为空则匹配所有</param>
        /// <param name="mode">查询方式：1、startWith匹配前缀；2、endWith匹配后缀；3、regex匹配正则表达式</param>
        /// <returns>返回查询结果集合</returns>
        Dictionary<string, object> Dict(string key = "", string mode = "startWith");

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Export(string file);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        object Get(string key);

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="key">键关键词，为空则匹配所有</param>
        /// <param name="mode">查询方式：1、startWith匹配前缀；2、endWith匹配后缀；3、regex匹配正则表达式</param>
        /// <returns>返回查询结果集合</returns>
        List<string> GetKeys(string key = "", string mode = "startWith");

        /// <summary>
        /// 判断值是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>有则返回true，没有则返false</returns>
        bool Has(string key);

        /// <summary>
        /// 获取或设置主键前缀
        /// </summary>
        /// <param name="key_prefix">键前缀名, 为空则获取前缀</param>
        string Head(string key_prefix = null);

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Import(string file);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        void Set(string key, object value);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="longTime">缓存时长</param>
        /// <returns>成功返回true，失败返回false</returns>
        void Set(string key, object value, int longTime);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="dateTime">到期时间</param>
        /// <returns>成功返回true，失败返回false</returns>
        void Set(string key, object value, string dateTime);

        /// <summary>
        /// 修改——通过函数式方式修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="funStr">函数式</param>
        /// <returns>成功返回true，失败返回false</returns>
        void SetEval(string key, string funStr);

        /// <summary>
        /// 修改——通过函数进行修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="fun">函数</param>
        /// <returns>成功返回true，失败返回false</returns>
        void SetFun(string key, Func<object, object> fun);
        #endregion
    }
}
