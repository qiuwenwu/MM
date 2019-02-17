using MM.Helper.Base;
using MM.Helper.Data;
using MM.Helper.Infos;
using MM.Helper.Net;
using MM.Helper.Sys;
using System;
using System.ComponentModel;
using System.Reflection;

namespace MM.Helper
{
    /// <summary>
    /// 索引目录
    /// </summary>
    public class Index
    {
        /// <summary>
        /// 当前路径
        /// </summary>
        public string Dir { get; set; }

        #region 基础类
        /// <summary>
        /// 数组类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Arr Arr()               { return new Arr(); }

        /// <summary>
        /// 颜色类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Colour Colour()         { return new Colour(); }

        /// <summary>
        /// 字典类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Dict Dict()             { return new Dict(); }

        /// <summary>
        /// 编码类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Encode Encode()         { return new Encode(); }

        /// <summary>
        /// 加密类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Encrypt Encrypt()       { return new Encrypt(); }

        /// <summary>
        /// 数字类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Num Num()               { return new Num(); }

        /// <summary>
        /// 对象类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Obj Obj()               { return new Obj(); }

        /// <summary>
        /// 随机类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Rand Rand()             { return new Rand(); }

        /// <summary>
        /// 字符串类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Str Str()               { return new Str(); }

        /// <summary>
        /// 时间类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Time Time()             { return new Time(); }
        #endregion


        #region 数据类
        /// <summary>
        /// Csv表格类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Csv Csv()               { return new Csv() { Dir = Dir }; }

        /// <summary>
        /// 数据表类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public DataTables DataTables() { return new DataTables(); }

        /// <summary>
        /// Excel表格类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Excel Excel()           { return new Excel() { Dir = Dir }; }

        /// <summary>
        /// Ini配置类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Ini Ini()               { return new Ini() { Dir = Dir }; }

        /// <summary>
        /// json数据操作类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Json Json()             { return new Json(); }

        /// <summary>
        /// 内存缓存类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Memory Memory()         { return new Memory(); }

        /// <summary>
        /// Mysql数据库操作类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Mysql Mysql()           { return new Mysql(); }

        /// <summary>
        /// Mysql数据库操作类（高级）
        /// </summary>
        /// <returns>返回帮助类</returns>
        public MysqlS MysqlS()         { return new MysqlS(); }

        /// <summary>
        /// 参数验证过滤类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Param Param()           { return new Param(); }

        /// <summary>
        /// Redis缓存类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Redis Redis()           { return new Redis(); }

        /// <summary>
        /// SQLite数据库操作类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public SQLite SQLite()         { return new SQLite() { Dir = Dir }; }

        /// <summary>
        /// SQLite数据库操作类（高级）
        /// </summary>
        /// <returns>返回帮助类</returns>
        public SQLiteS SQLiteS()       { return new SQLiteS() { Dir = Dir }; }

        /// <summary>
        /// 缓存操作类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Cache Cache()           { return new Cache(); }

        /// <summary>
        /// xml数据树操作类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Xml Xml()               { return new Xml() { Dir = Dir }; }
        #endregion


        #region 媒体操作类
        /// <summary>
        /// 文件操作类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Files File()            { return new Files() { Dir = Dir }; }

        /// <summary>
        /// 脚本指令操作类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Cmd Cmd()               { return new Cmd(); }

        /// <summary>
        /// 日志操作类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Log Log()               { return new Log() { Dir = Dir }; }

        /// <summary>
        /// 测速操作类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Speed Speed()           { return new Speed(); }
        #endregion


        #region 网络操作类
        /// <summary>
        /// Web Api请求
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Api Api()               { return new Api(); }

        /// <summary>
        /// 邮件发送类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Email Email()           { return new Email(); }

        /// <summary>
        /// Ftp传输类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Ftp Ftp()               { return new Ftp(); }

        /// <summary>
        /// 网页类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Html Html()             { return new Html(); }

        /// <summary>
        /// 网络请求类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public Https Https()           { return new Https(); }

        /// <summary>
        /// 消息加解密类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public MsgCrypt MsgCrypt()     { return new MsgCrypt(); }
        #endregion


        /// <summary>
        /// 查看对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>返回对象详情</returns>
        public string Look(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            var type = obj.GetType();
            PropertyInfo[] peroperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var text = "";
            foreach (PropertyInfo property in peroperties)
            {
                var name = property.Name;
                object[] objs = property.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objs.Length > 0)
                {
                    text = "\n" + string.Format("{0}: {1}", property.Name, ((DescriptionAttribute)objs[0]).Description);
                }
            }
            return text;
        }

        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <returns>返回版本信息模型</returns>
        public DllInfo Info()
        {
            return new DllInfo();
        }
    }
}
