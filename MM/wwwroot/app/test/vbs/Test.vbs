public function Run(fun, param, paramB, paramC)
	Engines.CleanVbs("./test.vbs")
	Dim str
	str = "hello" + Sdk.Text.Left("大家好","好") + param + "比" + Engines.Tag
	Run = str
end function