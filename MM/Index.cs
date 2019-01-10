using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MM
{
    /// <summary>
    /// 索引类
    /// </summary>
    public class Index
    {
        internal static void Init()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static Task Run(HttpContext context)
        {
            return context.Response.WriteAsync("Hello World!");
        }
    }
}
