function Run(fun, param, paramB)
{
	Service.CleanJs('Test\\Test');
	//var cmd = Sdk.Json.ToJson(Cache.GetCmd()).ToString();
	var bl = Cache.SaveCmd();
    return paramB + ' ' + param;
}