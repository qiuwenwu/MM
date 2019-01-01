using MM.Engine.Engines;

namespace MM.Engine
{
    /// <summary>
    /// 索引类
    /// </summary>
    public class Index
    {
        /// <summary>
        /// 新建Python脚本引擎
        /// </summary>
        /// <returns>返回Python脚本引擎帮助类</returns>
        public PyEngine NewPY() {
            return new PyEngine();
        }

        /// <summary>
        /// 新建VB脚本引擎
        /// </summary>
        /// <returns>返回VB脚本引擎帮助类</returns>
        public VbEngine NewVBS()
        {
            return new VbEngine();
        }

        /// <summary>
        /// 新建JavaScript脚本引擎
        /// </summary>
        /// <returns>返回JavaScript脚本引擎帮助类</returns>
        public JsEngine NewJS()
        {
            return new JsEngine();
        }

        /// <summary>
        /// 新建Csharp脚本引擎
        /// </summary>
        /// <returns>返回Csharp脚本引擎帮助类</returns>
        public CsEngine NewCS()
        {
            return new CsEngine();
        }
    }
}
