#!/usr/bin/python
# -*- coding: utf-8 -*-

import json
event = Cache.GetEvent('api')
wechatDt = Cache.WechatHelperDt

# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, tag = None, target = None, result = None):
	Engines.CleanPy('./event.py') #销毁自身脚本
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(tag, target)
	else:
		return '调用函数方式错误'

# 验证
# tag：标签
# target: 针对的目标对象
def Check(tag, path):
	return ""

# 执行
# tag：标签
# target: 针对的目标对象
def Main(tag, path):
	ret = ""
	clientID = path.replace('/api/wechat', '', 1).replace('/', '', 1)
	if(clientID != ""):
		req = event.GetReq(tag)
		query = req.QueryString
		body = req.Body
		if(query == "" and body == ""):
			ret = "缺少参数timestamp、nonce、msg_signature或signature、echostr"
		else:
			if(wechatDt.ContainsKey(clientID) == False):
				#从数据库获取微信公众号配置
				Data.Table = "mm_wechat_app"
				m = Data.GetS('clientID', 'mm')
				
				if(m != None):
					#新建一个微信公众号帮助类并做配置
					wechat = Cache.NewWechat()
					wechat.SetConfig(m)
					#添加到缓存中
					Cache.SetWechat(clientID, wechat)
			
			# 判断是否有该客户的微信帮助类
			if(wechatDt.ContainsKey(clientID)):
				wechat = wechatDt[clientID]
				
				# 调用微信公众号插件
				ret = wechat.RunPlugin(query, body, tag)
				ret = wechat.Ex
			else:
				ret = "当前客户端未申请公众号接入，无法调用该接口!"
	else:
		ret = "欢迎使用《超级美眉》微信公众号接口\n如果您要使用该接口，可以访问/wechat申请账号，填写微信公众号配置，就可以使用了。"
	return ret

# 渲染
# tag：标签
# target: 针对的目标对象
def Render(tag, path):
	return ""
	
#指令函数字典,用于获取对应函数
funDt = { "check": Check, "main": Main, "render": Render }