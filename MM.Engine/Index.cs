using MM.Helper.Infos;

namespace MM.Engine
{
    /// <summary>
    /// 脚本引擎索引目录
    /// </summary>
    public class Index
    {
        /// <summary>
        /// 当前目录
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; private set; }

        /// <summary>
        /// 新建Csharp脚本引擎帮助类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public CS CS()
        {
            return new CS(Dir);
        }

        /// <summary>
        /// 新建VB脚本引擎帮助类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public VBS VBS()
        {
            return new VBS(Dir);
        }

        /// <summary>
        /// 新建python脚本引擎帮助类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public PY PY()
        {
            return new PY(Dir);
        }

        /// <summary>
        /// 新建Csharp脚本引擎帮助类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public JS JS()
        {
            return new JS(Dir);
        }

        /// <summary>
        /// 新建lua脚本引擎帮助类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public LUA Lua()
        {
            return new LUA(Dir);
        }

        /// <summary>
        /// 新建Razor脚本引擎帮助类
        /// </summary>
        /// <returns>返回帮助类</returns>
        public TPL TPL()
        {
            return new TPL(Dir);
        }

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="appName">应用名</param>
        /// <param name="fun">函数名</param>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        /// <param name="param3">参数3</param>
        /// <returns>返回执行结果</returns>
        public object Run(string appName, object fun, object param1 = null, object param2 = null, object param3 = null) {
            IEngine eng = null;
            if (appName.EndsWith(".js"))
            {
                eng = new LUA();
            }
            else if (appName.EndsWith(".js"))
            {
                eng = new JS();
            }
            else if (appName.EndsWith(".cs"))
            {
                eng = new CS();
            }
            else if (appName.EndsWith(".lua"))
            {
                eng = new LUA();
            }
            else if (appName.EndsWith(".vbs"))
            {
                eng = new VBS();
            }
            else
            {
                Ex = "不支持的脚本类型！";
                return null;
            }
            var ret = eng.Run(appName, fun, param1, param2, param3);
            Ex = eng.GetEx();
            return ret;
        }

        /// <summary>
        /// 卸载脚本
        /// </summary>
        /// <param name="appName">应用名</param>
        /// <param name="waitTime">等待时长，单位：毫秒</param>
        /// <returns>卸载脚本</returns>
        public void Unload(string appName, int waitTime = 2000)
        {
            IEngine eng = null;
            if (appName.EndsWith(".js"))
            {
                eng = new LUA();
            }
            else if (appName.EndsWith(".js"))
            {
                eng = new JS();
            }
            else if (appName.EndsWith(".cs"))
            {
                eng = new CS();
            }
            else if (appName.EndsWith(".lua"))
            {
                eng = new LUA();
            }
            else if (appName.EndsWith(".vbs"))
            {
                eng = new VBS();
            }
            else
            {
                Ex = "错误的脚本类型！";
                return;
            }
            eng.Unload(appName, waitTime);
            Ex = eng.GetEx();
        }

        /// <summary>
        /// 引擎获取
        /// </summary>
        /// <param name="appName">应用名</param>
        /// <returns>返回引擎</returns>
        public dynamic Get(string appName) {
            IEngine eng = null;
            if (appName.EndsWith(".js"))
            {
                eng = new LUA();
            }
            else if (appName.EndsWith(".js"))
            {
                eng = new JS();
            }
            else if (appName.EndsWith(".cs"))
            {
                eng = new CS();
            }
            else if (appName.EndsWith(".lua"))
            {
                eng = new LUA();
            }
            else if (appName.EndsWith(".vbs"))
            {
                eng = new VBS();
            }
            else
            {
                Ex = "错误的脚本类型！";
                return null;
            }
            var dyn = eng.Get(appName);
            Ex = eng.GetEx();
            return dyn;
        }

        /// <summary>
        /// 查看版本信息
        /// </summary>
        /// <returns>返回版本信息模型</returns>
        public static DllInfo Info()
        {
            return new DllInfo() { Name = "超级美眉系列——脚本引擎帮助类库" };
        }
    }
}
