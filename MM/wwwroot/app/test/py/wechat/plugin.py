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

# 执行指令
# name：名称
def Cmd(tag, param, result):
	ret = ""
	name = None
	identifier = None
	arr = param.split(' ')
	if(param.find('start') != -1):
		ret = "插件启动成功！"
	elif(param.find('end') != -1):
		ret = "插件结束成功！"
	elif(param.find('install') != -1):
		ret = "插件安装成功！"
	elif(param.find('unstall') != -1):
		ret = "插件卸载成功！"
	elif(param.find('init') != -1):
		ret = "插件初始化成功！"
	elif(param.find('download') != -1):
		ret = "插件下载成功！"
	elif(param.find('update') != -1):
		ret = "插件更新成功！"
	elif(param.find('upload') != -1):
		ret = ""
	else:
		ret = "使用：wechat <命令>\n\n"
		ret += "当<命令>其中一个是:\n"
		ret += "upload,download,install,uninstall,init,info,help,update,remove,shop执行指令\n\n"
		ret += "wechat upload <插件名称>	上传插件\n"
		ret += "wechat download <插件名称>	下载插件\n"
		ret += "wechat install <插件名称>	安装插件\n"
		ret += "wechat uninstall <插件名称>	卸载插件\n"
		ret += "wechat start <插件名称>	启动插件\n"
		ret += "wechat end <插件名称>	结束插件\n"
		ret += "wechat init <插件名称>	插件初始化\n"
		ret += "wechat info <插件名称>	查看插件详情\n"
		ret += "wechat help <插件名称>	查看插件使用说明\n"
		ret += "wechat update <插件名称>	更新插件\n"
		ret += "wechat remove <插件名称>	删除插件\n"
		ret += "wechat shop <页码> <类型>	查看插件商城里的插件\n"
	return ret
	
#指令函数字典,用于获取对应函数
funDt = { "install": Install, "uninstall": Uninstall, "update": Update, "start": Start, "end": End, "init": Init, "remove": Remove, "cmd": Cmd }