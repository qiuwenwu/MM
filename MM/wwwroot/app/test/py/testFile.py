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
	Engines.CleanPy('./testFile.py') #销毁自身脚本
	#Engines.CleanPy('/script/py/mm/api/update.py')
	#Sdk.Log.Clear()
	#return ""
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(tag, target)
	else:
		return '调用函数方式错误'

# 是否需要执行
def CanDoIt(path):
	if(path.find('/api')):
		return False
	else:
		return True

# 验证之前
# tag：标签
# target: 针对的目标对象
def CheckBefore(tag, path):
	#if(CanDoIt(path)):
		#print '执行了CheckBefore'
	return ""

# 验证模型
# tag：标签
# target: 针对的目标对象
def Check(tag, path):
	if(CanDoIt(path)):
		return "测试"
	return ""

# 执行主业务前
# tag：标签
# target: 针对的目标对象
def MainBefore(tag, path):
	#if(CanDoIt(path)):
		#print '执行了MainBefore'
	return "123123"

# 执行主业务
# tag：标签
# target: 针对的目标对象
def Main(tag, path):
	return "function(){ return '123333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333' }"

# 渲染前
# tag：标签
# target: 针对的目标对象
def RenderBefore(tag, path):
	#if(CanDoIt(path)):
		#print '执行了RenderBefore'
	return "123123"

# 渲染
# tag：标签
# target: 针对的目标对象
def Render(tag, path):
	#if(CanDoIt(path)):
		#print '执行了Render'
	return ""
	
#指令函数字典,用于获取对应函数
funDt = { "checkbefore": CheckBefore, "check": Check, "mainbefore": MainBefore, "main": Main, "renderbefore": RenderBefore, "render": Render }