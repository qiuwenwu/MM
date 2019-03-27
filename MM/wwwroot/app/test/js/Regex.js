//#!/usr/bin/env node

const regexgen = require('regexgen');

function Run(param, paramB)
{
	if(!param){
		console.log('参数不能为空')
		return ""
	}
	var json = JSON.parse(param);
	if(!json)
	{
		console.log('参数必须为json格式字符串')
		return ""
	}
 	var arr = [];
	for(var k in json)
	{
		arr = json[k];
		break;
	}
	if(arr.length < 2){
		console.log('参数个数必须大于2')
		return ""
	}
	var ret = regexgen(arr, paramB);  //判断的数组和附加正则
	return ret.toString();
}