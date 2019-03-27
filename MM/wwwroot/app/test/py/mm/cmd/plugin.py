#!/usr/bin/python
# -*- coding: utf-8 -*-

import json
plug = Cache.PluginHelperDt['system']

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
		return funDt.get(fun)(tag, param)
	else:
		return '调用函数方式错误'

# 执行指令
# tag：标签
# param：参数
def Cmd(tag, param):
	#print Sdk.Json.Dumps(plug)
	ret = "插件"
	name = ''
	identifier = ''
	content = ''
	arr = param.split(' ')
	if(len(arr) > 2):
		content = arr[2]
		identifier = arr[1]
		name = arr[0]
	elif(len(arr) > 1):
		identifier = arr[1]
		name = arr[0]
	elif(len(arr) > 0):
		name = arr[0]
	if(name == 'start'):
		ret = plug.Start(identifier, content)
	elif(name == 'end'):
		ret = plug.End(identifier, content)
	elif(name == 'install'):
		ret = plug.Install(identifier, content)
	elif(name == 'unstall'):
		ret = plug.Unstall(identifier, content)
	elif(name == 'init'):
		ret = plug.Init(identifier, content)
	elif(name == 'download'):
		ret = "下载成功！"
	elif(name == 'upload'):
		ret = "上传成功！"
	elif(name == 'update'):
		ret = plug.Update(identifier, content)
	elif(name == 'remove'):
		ret = plug.Remove(identifier, content)
	elif(name == 'list'):
		print '进入'
		ret = plug.GetList()
	elif(name == 'shop'):
		ret = "查看插件商城！"
	elif(name == 'info' != -1):
		ret = "查看插件信息！"
	elif(name == 'help' != -1):
		ret = "插件使用说明！"
	else:
		ret = "使用：plugin <命令>\n\n"
		ret += "当<命令>其中一个是:\n"
		ret += "upload,download,install,uninstall,init,info,help,update,remove,shop执行指令\n\n"
		ret += "plugin upload <插件名称>	上传插件\n"
		ret += "plugin download <插件名称>	下载插件\n"
		ret += "plugin install <插件名称>	安装插件\n"
		ret += "plugin uninstall <插件名称>	卸载插件\n"
		ret += "plugin start <插件名称>	启动插件\n"
		ret += "plugin end <插件名称>	结束插件\n"
		ret += "plugin init <插件名称>	插件初始化\n"
		ret += "plugin info <插件名称>	查看插件详情\n"
		ret += "plugin help <插件名称>	查看插件使用说明\n"
		ret += "plugin update <插件名称>	更新插件\n"
		ret += "plugin remove <插件名称>	删除插件\n"
		ret += "plugin list	获取插件列表\n"
		ret += "plugin shop <页码> <类型>	查看插件商城里的插件\n"
	return ret
	
#指令函数字典,用于获取对应函数
funDt = { "cmd": Cmd }