using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using System.Linq;

namespace MM
{
    /// <summary>
    /// 启动程序类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置接口模型</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置接口模型
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///  此方法由运行时调用。使用这种方法将服务添加到容器中。
        /// </summary>
        /// <param name="services">服务器接口</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 设置压缩
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
        }

        /// <summary>
        /// 此方法由运行时调用。使用这个方法来配置HTTP请求管道。
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <param name="env">托管环境</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // 引用压缩
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //// 处理RPC指令
            //app.Map("/rpc", builder =>
            //{
            //    builder.Run(context =>
            //    {
            //        context.Response.ContentType = "application/json; charset=UTF-8";
            //        return context.Response.WriteAsync("Hello World!");
            //        //return context.Response.SendFileAsync("testfile1kb.txt");
            //    });
            //});

            //// 处理api接口
            //app.Map("/api", builder =>
            //{
            //    builder.Run(context =>
            //    {
            //        context.Response.ContentType = "application/json; charset=UTF-8";
            //        return context.Response.WriteAsync("Hello World!");
            //    });
            //});

            // 处理网页或其他事务
            app.Run(async (context) =>
            {
                await Server.Run(context);
            });
        }
    }
}
