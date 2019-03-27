// 插件名称：MM测试数据库操作插件
// 作者：超级美眉工作室

// 获取接口事件
var evt = Cache.GetEvent('api');

// 将调试输出转换为js常用函数形式
console.log = Sdk.Log.Output;


/// 脚本引擎执行函数
/// fun：函数名(string)
/// tag：标签(string)
/// target：目标(string)
/// result：上一次执行结果(object)
/// 返回：执行结果(object)
function Run(fun, tag, target, result)
{
	// 执行完卸载当前脚本
	Engines.CleanJs("./test.js");

	var ret = "";
	//ret += Engines.Tag;
	//ret += '12345' + tag + target + result;
	var txt = this[fun](tag, target, result);
	return ret + txt;
}

/// 脚本主接口函数
/// tag：标签(string)
/// target：目标(string)
/// result：上一次执行结果(object)
/// 返回：执行结果(object)
function main(tag, target, result){
	// 获取客户端传的所有信息
	var req = evt.GetReq(tag);

	// 清空调试输出内容
	// Sdk.Log.Clear();
	//console.log(Sdk.Json.Dumps(req, true));

	// 获取用户传的参数
	var param = req.GetParam();
	// 使用内置函数序列化参数
	var jsonStr = Sdk.Json.Dumps(param);
	// 使用js自带函数反序列化为对象
	var m = JSON.parse(jsonStr);

	//console.log(m.username);

	var a = Number(m.a);
	var b = Number(m.b);
	var num = a + b;

	//return num;
	// 创建视图模型
	var vm = { num: num };

	// 渲染视图
	var html = Tpl.View("./test.html", vm);

	// 如果渲染内容为空，判断是否模板错误
	if(!html)
	{
		html = Tpl.Ex;
	}

	// 返回视图型结果
	return html;

	//return tag + " " + target + " " + 123123;
}