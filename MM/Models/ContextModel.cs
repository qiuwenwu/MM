//using Microsoft.AspNetCore.Http;
//using System.Collections.Concurrent;
//using System.Collections.Generic;

//namespace MM.Models
//{
//    public class ContextModel
//    {
//        #region 
//        public ReqModel Req { get; internal set; } = new ReqModel();

//        /// <summary>
//        /// 获取请求参数
//        /// </summary>
//        /// <returns>返回请求参数</returns>
//        public ReqModel GetReq()
//        {
//            if (Req == null)
//            {
//                Req = new ApiReqModel();
//            }
//            return Req;
//        }

//        public ResModel Res { get; internal set; } = new ResModel();



//        /// <summary>
//        /// 获取响应结果
//        /// </summary>
//        /// <returns>返回响应结果</returns>
//        public ApiResModel GetRes() {
//            if (Res == null)
//            {
//                Res = new ApiResModel();
//            }
//            return Res;
//        }


//        /// <summary>
//        /// 获取Url参数 —— 字符串
//        /// </summary>
//        /// <returns>返回参数</returns>
//        public string GetQueryStr()
//        {
//            if (string.IsNullOrEmpty(Req.QueryStr))
//            {
//                Req.QueryStr = "";
//            }
//            return Req.QueryStr;
//        }

//        /// <summary>
//        /// 获取主体参数 —— 字符串
//        /// </summary>
//        /// <returns>返回参数</returns>
//        public string GetBodyStr()
//        {
//            if (string.IsNullOrEmpty(Req.BodyStr))
//            {
//                Req.QueryStr = "";
//            }
//            return "";
//        }

//        /// <summary>
//        /// 获取url参数 —— 字典
//        /// </summary>
//        /// <returns>返回参数</returns>
//        public Dictionary<string, object> GetQuery()
//        {
//            var dt = new Dictionary<string, object>();
//            return dt;
//        }

//        /// <summary>
//        /// 获取主体参数 —— 字典
//        /// </summary>
//        /// <returns>返回参数</returns>
//        public Dictionary<string, object> GetBody()
//        {
//            var dt = new Dictionary<string, object>();
//            return dt;
//        }

//        /// <summary>
//        /// 获取所有参数
//        /// </summary>
//        /// <returns>返回参数字典</returns>
//        public ConcurrentDictionary<string, object> GetParam()
//        {
//            return new ConcurrentDictionary<string, object>();
//        }
//        #endregion

//        public HttpContext Context { get; set; }
//    }
//}
