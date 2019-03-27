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
	Engines.CleanPy('./update.py') #销毁自身脚本
	#Sdk.Log.Clear()
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(tag, target)
	else:
		return '调用函数方式错误'

# 执行主业务
# tag：标签
# target: 针对的目标对象
def Api(tag, path):
	Cache.UpdateEvent('api') #更新api事件，存在BUG，在更新结束前访问API会导致系统崩溃, 非API事件则不会
	Cache.UpdateTask()
	Cache.UpdateApi()
	Cache.UpdateCmd()
	Cache.Clear()
	return "更新成功!"

#指令函数字典,用于获取对应函数
funDt = { "api": Api }