#!/usr/bin/python
# -*- coding: utf-8 -*-

import json

# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, name = None, param = None, result = None):
	#Engines.CleanPy('./task.py') #销毁自身脚本
	#Sdk.Log.Clear()
	#return ""
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(name)
	else:
		return '调用函数方式错误'

# 执行主业务
# name：名称
def Task(name):
	print '执行了示例任务'
	#Sdk.Log.WriteLine('执行了示例任务')
	return None

# 执行指令
# name：名称
def Cmd(name):
	return None
	
#指令函数字典,用于获取对应函数
funDt = { "cmd": Cmd, "task": Task }