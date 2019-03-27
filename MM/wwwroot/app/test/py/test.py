#!/usr/bin/python
# -*- coding: utf-8 -*-

#import clr
#import System
import json

event = Cache.GetEvent('api')
db = Sdk.Sqlite

# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, tag = None, target = None, result = None):
	Engines.CleanPy('./test.py') #销毁自身脚本
	Sdk.Log.Clear()
	#Engines.CleanPy('/script/py/mm/api/update.py')
	#return ""
	#return "重重".encode("gb2312")
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(tag, target)
	else:
		return '调用函数方式错误'

# 执行主业务
# tag：标签
# target: 针对的目标对象
def Main(tag, path):
	oj = { "a": "123", "b":66, "m": None }
	req = event.GetReq(tag)
	return "123" + Engines.Tag + Sdk.Json.Dumps(oj) #Sdk.Text.Right(path, '/api') #path.Right(path, '/api') #
	#db.Dir = Engines.Dir
	#db.Init()

	#db.DelTable()
	#db.AddTableKey("nid")
	#db.DelCol("number")
	#bl = db.AddCol("numberS", "VARCHAR", 11)
	#print bl
	#db.AddTableKey("nid")
	#db.DelTable()
	#db.AddTable("id int primary key     not null,name           text    not null,age            int     not null,address        char(50),salary         real");
	#db.Add("`id`,`name`,`age`", "1,'小明',19")
	#db.Set("`name`='小明'", "`age`=20")
	# user = db.getfirstobj("`name`='小明'")
	# if user:
		# user.age = 30
		# user.address = "山村"
		# db.setobj(user, 'id')
	# return db.get("`name`='小明'")
	#db.Add("`number`", "15817188815")
	#Data.Table = "osl_number"
	#return Data.GetField()

#指令函数字典,用于获取对应函数
funDt = { "main": Main }