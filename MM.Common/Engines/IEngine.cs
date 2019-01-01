namespace MM.Engine.Engines
{
    /// <summary>
    /// 脚本引擎接口
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 引入
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>返回引入的类</returns>
        object Import(string className);
    }
}
