public class Script : mmEngine.ClassBase
{
	public object Run(object fun = null, object param = null, object paramB = null, object paramC = null)
	{
		// 卸载自身脚本
		Engines.CleanCs("./test.cs");
		Sdk.Log.Clear();
		var pm = " 大家好3";
		if(param != null)
		{
			pm += param.ToString();
		}
		// var m = new Model() { A = 1, B = "你好" };
		// var ret = Data.ToRet("", m.ToJson());
		return Sdk.Text.Left(pm, "3") + "12354" + Engines.Tag;
	}
}

public class Model
{
	public int A { get; set;}
	public string B { get; set;}
}
/*
	使用说明：
	ClassHelper	基于mmEngine.ClassHelper, 为常用类库可根据需求集成。
	mmData命名空间保存数据，Cache开头为缓存数据，Config开头为配置数据，ConfigData开头为配置属性。
*/