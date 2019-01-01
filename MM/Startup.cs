using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MM
{
    /// <summary>
    /// 启动程序
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 此方法由运行时调用。使用这种方法将服务添加到容器中
        /// </summary>
        /// <param name="services">服务集合</param>
        public void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        ///  配置：此方法由运行时调用。使用这个方法来配置HTTP请求管道。
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <param name="env">主机环境</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
