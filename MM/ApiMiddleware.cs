using Microsoft.AspNetCore.Builder;

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
        
    }
}