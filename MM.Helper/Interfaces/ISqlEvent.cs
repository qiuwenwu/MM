using System;

namespace MM.Helper.Interfaces
{
    /// <summary>
    /// 数据库操作类接口
    /// </summary>
    public interface ISqlEvent
    {
        #region 预设
        /// <summary>
        /// 获取或设置主键前缀
        /// </summary>
        /// <param name="table_prefix">键前缀名, 为空则获取前缀</param>
        string Head(string table_prefix);
        #endregion


        #region 原始
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回执行成功条数</returns>
        int Execute(string sql);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回查询结果条数</returns>
        string Query(string sql);

        /// <summary>
        /// 增删改——异步
        /// </summary>
        /// <param name="sql">sql语句</param>
        void ExecuteAsync(string sql);
        #endregion


        #region 基础
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="fields">字段名字符串</param>
        /// <param name="values">字段值字符串</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Add(string fields, string values);

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="set">键值sql语句</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Add(string set);

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="where">查询sql语句</param>
        /// <param name="set">修改sql语句</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool AddOrSet(string where, string set);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">查询sql语句</param>
        /// <param name="num">删除的数量，0为删除所有</param>
        /// <param name="field">用作匹配删除的字段</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Del(string where, int num = 0, string field = "");

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Export(string file);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="where">查询sql语句</param>
        /// <returns>成功返回查询结果，失败返回null</returns>
        string Get(string where);

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Import(string file);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="where">查询sql语句</param>
        /// <param name="set">修改sql语句</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Set(string where, string set);

        /// <summary>
        /// 修改——通过函数式方式修改
        /// </summary>
        /// <param name="where">查询sql语句</param>
        /// <param name="funStr">函数式</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool SetEval(string where, string funStr);

        /// <summary>
        /// 修改——通过函数进行修改
        /// </summary>
        /// <param name="where">查询sql语句</param>
        /// <param name="fun">函数</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool SetFun(string where, Func<object, object> fun);
        #endregion


        #region 键值
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool KV_Add(string key, object value);

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool KV_AddOrSet(string key, object value);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool KV_Del(string key);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        object KV_Get(string key);

        /// <summary>
        /// 是否有该值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>有则返回true，没有则返false</returns>
        bool KV_Has(string key, string value);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="longTime">缓存时长</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool KV_Set(string key, object value);
        #endregion


        #region 对象
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj">添加对象</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Obj_Add(object obj);

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="whereObj">查询条件对象</param>
        /// <param name="setObj">增改条件对象</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Obj_AddOrSet(object whereObj, object setObj);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="whereObj">查询条件对象</param>
        /// <param name="num">删除符合条件的数量，0为删除所有</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Obj_Del(object whereObj, int num = 0);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>有则返回查询结果，没有则返回null</returns>
        object Obj_Get(string key, int num = 0);

        /// <summary>
        /// 是否有该值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>有则返回true，没有则返false</returns>
        bool Obj_Has(object whereObj);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true，失败返回false</returns>
        bool Obj_Set(string key, object value);
        #endregion


        #region 事件
        /// <summary>
        /// 获取或设置添加前事件
        /// </summary>
        /// <param name="fun">设置的事件钩子函数，为空则返回钩子函数</param>
        Func<object, object> Event_add_before(Func<object, object> fun);

        /// <summary>
        /// 获取或设置添加后事件
        /// </summary>
        /// <param name="fun">设置的事件钩子函数，为空则返回钩子函数</param>
        Func<object, object> Event_add_after(Func<object, object> fun);

        /// <summary>
        /// 获取或设置删除前事件
        /// </summary>
        /// <param name="fun">设置的事件钩子函数，为空则返回钩子函数</param>
        Func<object, object> Event_del_before(Func<object, object> fun);

        /// <summary>
        /// 获取或设置修改后事件
        /// </summary>
        /// <param name="fun">设置的事件钩子函数，为空则返回钩子函数</param>
        Func<object, object> Event_del_after(Func<object, object> fun);

        /// <summary>
        /// 获取或设置修改前事件
        /// </summary>
        /// <param name="fun">设置的事件钩子函数，为空则返回钩子函数</param>
        Func<object, object> Event_set_before(Func<object, object> fun);

        /// <summary>
        /// 获取或设置修改事件
        /// </summary>
        /// <param name="fun">设置的事件钩子函数，为空则返回钩子函数</param>
        Func<object, object> Event_set_after(Func<object, object> fun);

        /// <summary>
        /// 获取或设置查询前事件
        /// </summary>
        /// <param name="fun">设置的事件钩子函数，为空则返回钩子函数</param>
        Func<object, object> Event_get_before(Func<object, object> fun);

        /// <summary>
        /// 获取或设置查询后事件
        /// </summary>
        /// <param name="fun">设置的事件钩子函数，为空则返回钩子函数</param>
        Func<object, object> Event_get_after(Func<object, object> fun);
        #endregion
    }
}
