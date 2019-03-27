#!/usr/bin/python
# -*- coding: utf-8 -*-

import json
import copy

event = Cache.GetEvent('api')
Text = Sdk.Text
Dumps = Sdk.Json.Dumps

# 执行函数
# fun：调用的函数
# tag：时效标签
# target: 针对的目标对象
# result: 上一个请求结果
def Run(fun = None, tag = None, target = None, result = None):
	Engines.CleanPy('./developer.py') #销毁自身脚本,一次性加载返回调用，采用每执行一次调用一次的方式
	Sdk.Log.Clear()
	fun = fun.lower()
	if(funDt.has_key(fun)): #判断是否具有该函数参数
		return funDt.get(fun)(tag, target, result)
	else:
		return '调用函数方式错误'

def Main(tag, target, result):
	req = event.GetReq(tag)
	param = req.GetParam()
	jsonStr = Dumps(param, True)
	jobj = json.loads(jsonStr)
	cmd = "help"
	if(jobj.has_key("cmd")):
		cmd = jobj["cmd"]
		del jobj["cmd"]
	if(cmd == "help"):
		return Help(tag,jobj)
	else:
		return Create(tag,jobj)

def Help(tag,jobj):
	dt = Cache.ApiDt
	ret = ""
	if(jobj.has_key("name")):
		name = jobj["name"]
		for kv in dt:
			o = kv.Value
			ne = o.Name
			if(ne == name):
				ostr = Dumps(o)
				op = json.loads(ostr)
				del op["sql"]
				del op["cache"]
				del op["paramPath"]
				del op["sqlPath"]
				ret = Dumps(op, True)
				break
	else:
		arr = []
		for o in dt:
			ne = o.Value.Name
			if(ne != None and ne != ""):
				m = {"path":o.Value.Path, "name":ne, "title":o.Value.Title}
				arr.append(m)
		ret = Dumps(arr, True)
	return ret

def Create(tag,jobj):
	ret = ""
	msg = ""
	oldStr = "*"
	newStr = ""
	if(jobj.has_key("oldstr")):
		oldStr = str(jobj["oldstr"])
	if(jobj.has_key("newstr")):
		newStr = str(jobj["newstr"])
	if(jobj.has_key("table")):
		table = jobj["table"]
		if(table != ""):
			te = ""
			if(table.find("_") != -1):
				te = Text.Right(table, "_")
			else:
				te = table + ""
			path = pa + "\\" + te
			if(jobj.has_key("update")):
				Sdk.File.DelDir(path,True)
			Sdk.File.DelDir(path,True)
			bl = fieldDef(te.replace(oldStr,newStr),table)
			j = {"table":table,"bl":bl}
			ret = json.dumps(j)
		else:
			msg = "没有数据表"
	else:
		if(jobj.has_key("update")):
			Sdk.File.DelDir(pa,True)
		Sdk.File.AddDir(pa)
		table = Data.GetTables()
		if(table != ""):
			arr = json.loads(table)
			jn = []
			for a in arr:
				table = a["table"]
				te = ""
				if(table.find("_") != -1):
					te = Text.Right(table, "_")
				else:
					te = table + ""
				bl = fieldDef(te.replace(oldStr,newStr),table)
				j = {"table":table,"bl":bl}
				jn.append(j)
			ret = json.dumps(jn)
		else:
			msg = "没有数据表"
	return Data.ToRet(msg,ret)

def fieldDef(te, table):
	path = pa + "\\" + te
	Sdk.File.AddDir(path)
	n = Cache.NewApi()
	n.Path = "/api/" + te.replace("_","/")
	n.ParamPath = "./param"
	n.Name = te
	n.SqlPath = "./sql"
	jobjStr = Sdk.Json.Serializer(n)
	bl = Sdk.File.Save(path + "\\api.json",jobjStr)
	
	if(bl):
		m = Data.NewSql()
		dt = Data.NewParamDt()
		field = Data.GetField(table)
		arr = json.loads(field)
		fieldStr = ""
		for a in arr:
			m = setSql(m, a)
			u = setParam(a)
			name = a["name"]
			dt.Add(name, u)
			fieldStr = fieldStr + ",`" + name + "`"
		bjStr = Dumps(dt, True)
		Sdk.File.Save(path + "\\param.json", bjStr)
		m.Table = table
		m.FieldDefault = fieldStr.replace(",", "", 1)
		jsonStr = Dumps(m, True)
		bl = Sdk.File.Save(path + "\\sql.json", jsonStr) #保存为文件
	return bl

def setSql(m, a):
	tp = a["type"]
	if(tp.find("int") != -1):
		if(a["key"] == "PRI"):
			m.Where.Add(a["name"],"`" + a["name"] + "` = {0}")
			m.SortDefault = "`" + a["name"] + "` DESC"
	elif(a["type"].find("time") != -1):
		m.Query.Add(a["name"] + "_min","`" + a["name"] + "` >= '{0}'")
		m.Query.Add(a["name"] + "_max","`" + a["name"] + "` <= '{0}'")
		m.Update.Add(a["name"],"`" + a["name"] + "` = '{0}'")
	else:
		m.Query.Add(a["name"],"`" + a["name"] + "` like '%{0}%'")
		m.Update.Add(a["name"],"`" + a["name"] + "` = '{0}'")
	return m
	
def setParam(a):
	tp = a["type"]
	u = Data.NewParam()
	tip = a["comment"]
	if(tip.find("：") != -1):
		u.Title = Text.Left(tip, "：") #标题
		u.Description =	Text.Right(tip, "：") #描述
	else:
		u.Title = tip
	v = Data.NewValidate() #验证模型
	if(a["key"] != ""):
		v.Only = True
	if(tp.find("tinyint") != -1):
		v.Type = "bool" #布尔型
	elif(tp.find("time") != -1):
		v.Type = "time" #日期时间型
	elif(tp.find("int") != -1):
		v.Type = "int" #数字型
		length = Text.Between(tp,"(",")")
		if(length != None and length != ""):
			v.Max = 10 * int(length) - 1
	else:
		v.Type = "string" #字符串型
		lh = Text.Between(tp,"(",")")
		if(lh != None and lh != ""):
			v.MaxLength = int(lh)
		if(a["default"] == None):
			v.NotEmpty = False
		else:
			v.NotEmpty = True
	u.Validate = v
	return u

pa = Cache.ScriptPath + "py\\developer\\com"

#指令函数字典,用于获取对应函数
funDt = { "main": Main }