#!/usr/bin/python
# -*- coding: utf-8 -*-

import json

# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, name = None, param = None, result = None):
	Engines.CleanPy('./update.py') #销毁自身脚本
	#Sdk.Log.Clear()
	#return ""
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(name, param)
	else:
		return '调用函数方式错误'

# 执行指令
# tag：标签
# param：参数
def Cmd(name, param):
	ret = ""
	name = None
	path = None
	arr = param.split(' ')
	if(len(arr) > 2):
		name = arr[1]
		path = arr[2]
	elif(len(arr) > 1):
		name = arr[1]
	if(param.find('all') != -1):
		ret = "更新成功！"
	elif(param.find('data') != -1):
		Cache.Clear()
		ret = "缓存数据已更新！"
	elif(param.find('event') != -1):
		Cache.UpdateEvent(name, path)
		Cache.SortEvent(name)
		ret = "事件更新成功！"
	elif(param.find('cmd') != -1):
		Cache.UpdateCmd(name, path)
		ret = "指令更新成功！"
	elif(param.find('task') != -1):
		Cache.UpdateTask(name, path)
		ret = "任务更新成功！"
	elif(param.find('plugin') != -1):
		Cache.UpdatePlugin(name, path)
		ret = "插件更新成功！"
	else:
		ret += "使用：update <命令>\n\n"
		ret += "当<命令>其中一个是:\n"
		ret += "all,data,event,cmd,task,plugin执行指令\n\n"
		ret += "update all 更新所有\n"
		ret += "update data 更新缓存数据\n"
		ret += "update event <名称> <路径>	更新事件\n"
		ret += "update cmd <名称> <路径>	更新指令\n"
		ret += "update task <名称> <路径>	更新任务\n"
		ret += "update plugin <名称> <路径>	更新插件\n"
	return ret
	
#指令函数字典,用于获取对应函数
funDt = { "cmd": Cmd }