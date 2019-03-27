#!/usr/bin/python
# -*- coding: utf-8 -*-

import json
event = Cache.GetEvent('api')

# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, tag = None, target = None, result = None):
	#Engines.CleanPy('./cmd.py') #销毁自身脚本
	#Sdk.Log.Clear()
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(tag, target)
	else:
		return '调用函数方式错误'

# 执行指令
# tag: 标签
# target: 针对的目标对象
def Api(tag, target):
	req = event.GetReq(tag)
	paramStr = req.GetParamStr()
	pm = json.loads(paramStr)
	res = event.GetRes(tag)
	ret = ""
	content = ""
	if(pm.has_key("content")):
		content = pm["content"]
	if content == "":
		ret = res.SetRet(None, "content参数不能为空", 10000)
	else:
		username = None
		ret = Service.RunCmd(content, username)
	return ret
	
#指令函数字典,用于获取对应函数
funDt = { "api": Api }