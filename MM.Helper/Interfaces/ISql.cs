using System.Collections.Generic;
using System.Threading.Tasks;

namespace MM.Helper.Data
{
    /// <summary>
    /// 数据库操作类接口
    /// </summary>
    interface ISql
    {
        #region 配置
        /// <summary>
        /// 获取错误提示
        /// </summary>
        /// <returns>返回错误提示</returns>
        string GetEx();

        /// <summary>
        /// 获取当前的SQL语句
        /// </summary>
        /// <returns>返回SQL语句</returns>
        string GetSql();

        /// <summary>
        /// 获取数据库名
        /// </summary>
        /// <returns>返回数据库名</returns>
        string GetDB();

        /// <summary>
        /// 设置分页大小——每页显示的数量
        /// </summary>
        /// <param name="size">数量</param>
        void SetSize(int size);

        /// <summary>
        /// 设置查询页 —— 分页后的结果第几页
        /// </summary>
        /// <param name="page">当前页</param>
        void SetPage(int page);

        /// <summary>
        /// 设置查询表
        /// </summary>
        /// <param name="table">查询表</param>
        void SetTable(string table);
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        void Init(string connStr = null);

        /// <summary>
        /// 开启数据库连接
        /// </summary>
        bool Open();

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        void Link(string connStr = null);

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void Close();

        /// <summary>
        /// 判断数据库是否连接
        /// </summary>
        /// <returns>连接成功返回true，失败返回false</returns>
        bool CheckLink();

        /// <summary>
        /// 释放资源
        /// </summary>
        void Dispose();
        #endregion


        #region 原始
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回执行成功条数</returns>
        int Execute(string sql);

        /// <summary>
        /// 增删改——异步
        /// </summary>
        /// <param name="sql">sql语句</param>
        Task<int> ExecuteAsync(string sql);

        /// <summary>
        /// 执行SQL语句-查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回查询结果字符串</returns>
        Task<IEnumerable<dynamic>> QueryAsync(string sql);

        /// <summary>
        /// 执行SQL语句-查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回查询结果字符串</returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql);

        /// <summary>
        /// 执行SQL语句-查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回项目集合</returns>
        List<dynamic> Query(string sql);

        /// <summary>
        /// 执行SQL语句-查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回项目</returns>
        dynamic QueryFirst(string sql);

        /// <summary>
        /// 执行SQL语句-查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回模型集合结果</returns>
        List<T> Query<T>(string sql);

        /// <summary>
        /// 执行SQL语句-查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回模型列表</returns>
        T QueryFirst<T>(string sql);

        /// <summary>
        /// 执行SQL语句-查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回项目集合</returns>
        string QueryStr(string sql);

        /// <summary>
        /// 执行SQL语句-查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回项目</returns>
        string QueryFirstStr(string sql);
        #endregion


        #region 弱类型
        /// <summary>
        /// 查
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="field">字段名</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回Json格式查询结果</returns>
        List<dynamic> Get(string whereStr, string field = "*", string orderBy = "");

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="field">字段名</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回Json格式查询结果</returns>
        dynamic GetFirst(string whereStr, string field = "*", string orderBy = "");

        /// <summary>
        /// 查找全部
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回Json格式查询结果</returns>
        List<dynamic> GetAll(string orderBy = "", string field = "*");

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="field">字段名</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回Json格式查询结果</returns>
        string GetStr(string whereStr, string field = "*", string orderBy = "");

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="field">字段名</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回Json格式查询结果</returns>
        string GetFirstStr(string whereStr, string field = "*", string orderBy = "");
        #endregion


        #region 泛型
        /// <summary>
        /// 查
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="field">字段名</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回模型结果</returns>
        T GetFirst<T>(string whereStr, string field = "*", string orderBy = "");

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="field">字段名</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回模型集合结果</returns>
        List<T> Get<T>(string whereStr, string field = "*", string orderBy = "");

        /// <summary>
        /// 查找全部
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns>返回模型集合</returns>
        List<T> GetAll<T>(string orderBy = "", string field = "*");
        #endregion


        #region 常用增删改操作
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="personStr">字段名</param>
        /// <param name="valueStr">对应字段值</param>
        /// <returns>添加成功返回true,失败返回false</returns>
        int Add(string personStr, string valueStr);

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="setStr">修改项</param>
        /// <returns>修改成功返回true,失败返回false</returns>
        int Set(string whereStr, string setStr);

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <param name="setStr">修改项</param>
        /// <returns>修改成功返回true,失败返回false</returns>
        Task<int> SetAsync(string whereStr, string setStr);

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="whereStr">查询条件</param>
        /// <returns>删除成功返回true,失败返回false</returns>
        int Del(string whereStr);

        /// <summary>
        /// 删除重复记录并留唯一
        /// </summary>
        /// <param name="whereStr">删除的条件语句</param>
        /// <param name="field">用作判断的字段</param>
        /// <param name="IDfield">ID字段</param>
        /// <param name="keep">保留方式，max或min</param>
        /// <returns>删除成功返回true，失败返回false</returns>
        int DelAsOne(string whereStr, string field, string IDfield, string keep = "max");

        /// <summary>
        /// 增加或更新
        /// </summary>
        /// <param name="personStr">字段名,需带主键</param>
        /// <param name="valueStr">对应字段值</param>
        /// <param name="setStr"></param>
        /// <returns>添加成功返回true,失败返回false</returns>
        int AddOrSet(string personStr, string valueStr, string setStr);

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="whereStr">条件</param>
        /// <param name="setStr">设置值</param>
        /// <returns>成功返回ture，失败返回false</returns>
        int AddOrSet(string whereStr, string setStr);
        #endregion
    }
}
