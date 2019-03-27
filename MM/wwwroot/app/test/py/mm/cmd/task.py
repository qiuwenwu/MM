#!/usr/bin/python
# -*- coding: utf-8 -*-

import json


# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, tag = None, param = None, result = None):
	#Engines.CleanPy('./task.py') #销毁自身脚本
	#Sdk.Log.Clear()
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(tag, param)
	else:
		return '调用函数方式错误'

# 执行指令
# tag：标签
# param：参数
def Cmd(tag, param):
	ret = ""
	name = None
	identifier = None
	arr = param.split(' ')
	if(len(arr) > 2):
		name = arr[1]
		identifier = arr[2]
	elif(len(arr) > 1):
		name = arr[1]
	if(param.find('start') != -1):
		Service.StartTask(name, identifier)
		ret = "任务已开启"
	elif(param.find('stop') != -1):
		Service.StopTask(name, identifier)
		ret = "任务已暂停"
	elif(param.find('end') != -1):
		Service.EndTask(name, identifier)
		ret = "任务已结束"
	elif(param.find('reset') != -1):
		Service.ResetTask(name, identifier)
		ret = "任务已重置"
	else:
		ret += "使用：task <命令>\n\n"
		ret += "当<命令>其中一个是:\n"
		ret += "start,stop,end,reset执行指令\n\n"
		ret += "task start <任务名称> <任务进程标识>	开启任务\n"
		ret += "task stop <任务名称> <任务进程标识>	暂停任务\n"
		ret += "task end <任务名称> <任务进程标识>	结束任务\n"
		ret += "task reset <任务名称> <任务进程标识>	重置任务\n"
	return ret
	
#指令函数字典,用于获取对应函数
funDt = { "cmd": Cmd }