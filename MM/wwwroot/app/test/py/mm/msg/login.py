#!/usr/bin/python
# -*- coding: utf-8 -*-

import json

# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, name = None, param = None, result = None):
	Engines.CleanPy('./login.py') #销毁自身脚本
	#Sdk.Log.Clear()
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(name, param)
	else:
		return '调用函数方式错误'

# 执行指令
# name：名称
def Main(name, param):
	ret = "123123"
	return ret
	
#指令函数字典,用于获取对应函数
funDt = { "main": Main }