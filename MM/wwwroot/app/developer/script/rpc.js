// 获取接口事件
var evt = Cache.GetEvent('api');

// 将调试输出转换为js常用函数形式
console.log = Sdk.Log.Output;

/// 入口函数
/// fun：函数
/// tag：标签
/// path：路径
/// ret：上次请求结果
function Run(fun, tag, path, ret)
{
	// 执行完卸载当前脚本
	Engines.CleanJs("./rpc.js");
	
	// 获取响应结果
	var req = evt.GetReq(tag);
	
	return req;
}