using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MM.Event;
using System.Threading.Tasks;

namespace MM
{
    /// <summary> 
    /// API扩展
    /// </summary>
    public static class UseApiExtensions
    {
        public static IApplicationBuilder UseApi(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiMiddleware>();
        }
    }

    public class ApiMiddleware
    {
        private readonly RequestDelegate _next;
        public ApiMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
        }

        /// <summary>
        /// 判断是否处理拓展，如果是Api
        /// </summary>
        /// <param name="ct">http上下文</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext ct)
        {
            // Console.WriteLine("先Api");
            var stated = ct.Response.HasStarted;
            //  如果已响应，不继续下面的操作。
            if (!stated)
            {
                await Soa.Run(ct);
            }
            else
            {
                //  执行之前装载的中间件
                await _next.Invoke(ct);
                //Console.WriteLine("后Api");
            }
        }
    }
}