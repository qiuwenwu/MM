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
	Engines.CleanPy('./test.py') #销毁自身脚本
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
	#Cache.Del('a')
	#Cache.Set('a', 123, 1)
	#bl = Cache.Set('a', 666)
	#ret = Cache.Get('a')
	
	#加载配置
	# print Sdk.Json.Dumps(Config.VarDt)
	# Config.Load()
	# test = Config.Get('test')
	# bb = test + "666"
	# Config.Set('test6', bb)
	# Config.Save()
	# return Config.Get('test6')
	
	#dtStr = Sdk.Json.Dumps(Cache.EventHelperDt)
	#Cache.UpdateTask('update', 'test_update_task2')
	#Service.StartTask('update', 'test_update_task2')
	#print '执行了Main'
	#if(CanDoIt(path)):
		#print '执行了Main'
	# req = event.GetReq(tag)
	# url = req.Path + req.QueryString
	
	# ret = Cache.Get(url)
	# if ret == None:
		# ret = Sdk.Json.Dumps(req)
		# #res = event.GetRes(tag)
		# Cache.Set(url, ret)
	#res.Headers.Add("api", "123123")
	#res.SetCookie('test', 'hello word!', 1)
	return '九'

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