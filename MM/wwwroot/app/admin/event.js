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
	var config = { 
		Main: "/app/admin/main.js", 
		Title: "后台管理系统",
		Keywords: "后台,管理,系统,站长,内容",
		Description: "用于站长管理网站相关内容"
	};
	var html = Tpl.View("~/app/index.html", config);
	
	if(html != ""){
		// 设置响应内容
		res.Body = html;
		return html;
	}
	return Tpl.Ex;
}