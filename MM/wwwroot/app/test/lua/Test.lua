function Run(fun, param, paramB, paramC)
	Engines:CleanLua('./test.lua')	--卸载自身脚本
	Sdk.Log:Clear()
    A = Sdk.Text:Left("我很好啊","好")
	B = Sdk.Text:Right("我很好","很")
	ret = Sdk.Cmd:RunLua(Config.Dir..'hello.lua', 'Run', '参数1', '参数2')
	return ret..A..B.."123"..Engines.Tag
	--return "hello"..param..A..B.."比"..C..ret
end