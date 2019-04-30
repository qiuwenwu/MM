using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MM
{
    internal class Soa
    {
        internal static Task Run(HttpContext ct)
        {
            return ct.Response.WriteAsync("hello world!");
        }
    }
}