using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MM
{
    /// <summary>
    /// 服务器
    /// </summary>
    public class Server
    {
        internal static Task Run(HttpContext context)
        {
            var ret = "Hello World!";
            return context.Response.WriteAsync(ret);
        }

        //internal Task RunTask(HttpContext ctx)
        //{
        //    var body = ""; // 预设响应结果

        //    var res = ctx.Response;
        //    if (!res.HasStarted)
        //    {
        //        var req = ctx.Request;
        //        var path = req.Path.Value;

        //        var evt = EventDt.Get(path); // 获取事件
        //        ApiConfig cg = ConfigDt.Get(path); // 获取Api配置

        //        var dt = Cache._ContextDt;
        //        var tag = NewTag(path); // 新建标签
        //        var bl = false;
        //        if (evt != null)
        //        {
        //            bl = dt.TryAdd(tag, new ContextModel() { Context = ctx });
        //            var oj = evt.Run(tag, path, null, (_tag, _path, _ret) => { return Run(_tag, _path, _ret, cg); });
        //            if (oj != null)
        //            {
        //                body = oj.ToString();
        //            }
        //        }
        //        else if (cg != null)
        //        {
        //            bl = dt.TryAdd(tag, new ContextModel() { Context = ctx });
        //            body = Run(tag, path, body, cg);
        //        }
        //        if (dt.ContainsKey(tag))
        //        {
        //            var resM = dt[tag].GetRes();
        //            body = resM.Body;
        //        }
        //        if (string.IsNullOrEmpty(body))
        //        {
        //            body = "";
        //            res.StatusCode = 404;
        //        }
        //        if (bl)
        //        {
        //            dt.Del(tag);
        //        }
        //    }
        //    return res.WriteAsync(body);
        //}
    }
}
