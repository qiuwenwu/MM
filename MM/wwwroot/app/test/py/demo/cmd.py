#!/usr/bin/python
# -*- coding: utf-8 -*-

import json

# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, name = None, param = None, result = None):
	Engines.CleanPy('./cmd.py') #销毁自身脚本
	Sdk.Log.Clear()
	#return ""
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(name, param)
	else:
		return '调用函数方式错误'

# 执行指令
# name：名称
def Cmd(name, param):
	return "测试调用指令"
	
#指令函数字典,用于获取对应函数
funDt = { "cmd": Cmd }