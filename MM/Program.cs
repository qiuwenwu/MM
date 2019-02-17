using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MM
{
    /// <summary>
    /// 主程序类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="args">参数集合</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 创建web服务器
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>返回web服务器</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls(MainCache._Config.Urls)
                .UseStartup<Startup>();
    }
}
