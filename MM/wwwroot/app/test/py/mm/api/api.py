#!/usr/bin/python
# -*- coding: utf-8 -*-

import json
event = Cache.GetEvent('api')
apiDt = Cache.ApiDt
#sp = Sdk.Speed

# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, tag = None, target = None, result = None):
	Engines.CleanPy('./api.py') #销毁自身脚本
	#Sdk.Log.Clear()
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(tag, target, result)
	else:
		return '调用函数方式错误'


# 执行主业务
# tag：标签
# path: 接口
# result: 上一次执行结果
# 返回: 执行结果
def Main(tag, path, result):
	ret = None
	if(apiDt.ContainsKey(path)):
		#获取请求上下文模型
		req = event.GetReq(tag)
		
		method = req.Method
		key = path + req.QueryString
		
		#如果有缓存则直接读取缓存
		if(method == "GET"):
			ret = Cache.Get("api_" + key)
		
		#如果没有缓存数据则重新执行
		if(ret == None):
			#获取api模型
			m = apiDt[path]
			
			#获取请求参数
			param = req.GetParam()
			paramDt = m.Param
			#是否允许含有其他参数
			# if(m.OtherParam != False):
				# if(m.FilterParam):
					# param = Data.Filter(param, paramDt)
				# else:
					# name = Data.NotParam(param, paramDt)
					# if(name != None):
						# return Data.ToRet("含有非法参数" + name, None, 10003)
			if(m.CheckParam):
				#验证请求参数是否正确
				msg = Data.CheckParam(param, paramDt, m.Sql)
				if(msg != ""):
					return Data.ToRet(msg, None, 10003)
			#sp.Start()
			#通过SQL模型执行SQL语句
			ret = Data.RunSql(m.Sql, param)
			#sp.Stop("执行Sql ")
			#判断是否需要缓存结果
			cache = m.Cache
			if (method == "GET" and cache > 0):
				Cache.Set("api_" + key, ret, cache)
	return ret

	
# 执行验证前事件函数
# tag：标签
# path: 接口
# result: 上一次执行结果
# 返回: None
def CheckBefore(tag, path, result):
	#print '验证前'
	return None


#指令函数字典,用于获取对应函数
funDt = { "main": Main, "checkbefore": CheckBefore }