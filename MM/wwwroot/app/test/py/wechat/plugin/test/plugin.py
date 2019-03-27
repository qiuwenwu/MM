#!/usr/bin/python
# -*- coding: utf-8 -*-

import json


# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, tag = None, param = None, result = None):
	Engines.CleanPy('./plugin.py') #销毁自身脚本
	#Sdk.Log.Clear()
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(tag, param, result)
	else:
		return '调用函数方式错误'

# 执行指令
# name：名称
def Install(tag, param, result):
	return "安装成功"
	
# 执行指令
# name：名称
def Uninstall(tag, param, result):
	return "卸载成功"

# 执行指令
# name：名称
def Update(tag, param, result):
	return "更新成功"
	
# 执行指令
# name：名称
def Start(tag, param, result):
	print '开启微信功能'
	return "开启成功"
	
# 执行指令
# name：名称
def End(tag, param, result):
	return "关闭成功"
	
# 执行指令
# name：名称
def Init(tag, param, result):
	return "初始化成功"
	
# 执行指令
# name：名称
def Remove(tag, param, result):
	return "删除成功"
	
#指令函数字典,用于获取对应函数
funDt = { "install": Install, "uninstall": Uninstall, "Update": Update, "start": Start, "end": End, "init": Init, "remove": Remove }