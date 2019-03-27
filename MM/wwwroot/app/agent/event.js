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
	Engines.CleanJs("./event.js");
	
	// 获取响应结果
	var res = evt.GetRes(tag);
	// 获取视图背包(全局变量)
	Tpl.ViewBag = res.ViewBag();
	
	// 使用模板引擎渲染
	var html = Tpl.View("~/agent/index.html", { a: "js" });
	
	if(html != ""){
		// 设置响应内容
		res.Body = html;
		return html;
	}
	return Tpl.Ex;
}