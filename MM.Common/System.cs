namespace MM.Common
{
    /// <summary>
    /// 系统公共类
    /// </summary>
    public class System : ISystem
    {
        /// <summary>
        /// 引入
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>返回引入的类</returns>
        public object Import(string className) {
            return null;
        }
    }

    /// <summary>
    /// 系统公共类接口
    /// </summary>
    public interface ISystem
    {
        /// <summary>
        /// 引入
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>返回引入的类</returns>
        object Import(string className);
    }
}
