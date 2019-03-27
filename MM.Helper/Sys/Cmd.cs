using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MM.Helper.Sys
{
    /// <summary>
    /// 命令提示符帮助类
    /// </summary>
    public class Cmd
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex { get; set; } = "";

        /// <summary>
        /// python程序目录
        /// </summary>
        public static string python = "";
        /// <summary>
        /// node程序目录
        /// </summary>
        public static string node = "";
        /// <summary>
        /// lua脚本目录
        /// </summary>
        public static string lua = "";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>初始化成功返回true，失败返回false</returns>
        public bool Init()
        {
            var bl = new Dir().EachAdd(Cache.path.Cache);
            if (bl)
            {
                var NodeJsFile = Cache.path.Cache + "node.js";
                if (!File.Exists(NodeJsFile))
                {
                    File.WriteAllText(NodeJsFile, GetNodeJs(), Encoding.UTF8);
                }

                var pythonFile = Cache.path.Cache + "python.py";
                if (!File.Exists(pythonFile))
                {
                    File.WriteAllText(pythonFile, GetPython(), Encoding.UTF8);
                }

                var luaFile = Cache.path.Cache + "lua.lua";
                if (!File.Exists(luaFile))
                {
                    File.WriteAllText(luaFile, GetLua(), Encoding.UTF8);
                }
            }

            string sPath = Environment.GetEnvironmentVariable("path");
            var arr = sPath.Split(';');
            foreach (var o in arr)
            {
                var a = o.ToLower();
                if (a.Contains("\\nodejs"))
                {
                    node = o + "node.exe";
                }
                else if (a.Contains("python"))
                {
                    python = o + "python.exe";
                }
                else if (a.EndsWith("\\lua\\") || a.EndsWith("lua"))
                {
                    lua = o + "lua.exe";
                }
            }
            return bl;
        }

        /// <summary>
        /// 获取nodeJS脚本
        /// </summary>
        /// <returns>返回脚本代码</returns>
        private string GetNodeJs()
        {
            return @"const fs = require(##fs##); //引入文件类

var paramArr = process.argv.splice(2); //获取参数 param
var lh = paramArr.length;
if(lh > 1)
{
	var fileName = paramArr[0]; //获取文件名
	fs.exists(fileName, function(exists){
		var ret = ####;
		if(exists)
		{
			const m = require(fileName);
			if(m){
				if(lh > 1)
				{
					var funName = paramArr[1]; //获取函数名
					var cs = new m();
					//执行函数
					var func = cs[funName];
					if(func){
						if(lh === 4)
						{
							ret = func(paramArr[2], paramArr[3]);
						}
						else if(lh === 3)
						{
							ret = func(paramArr[2]);
						}
						else{
							ret = func();
						}
					}
					else{
						ret = '{##error## : 10000, ##msg##: ##The function name does not exist!## }'
					}
				}
				else if(model.Main)
				{
					ret = model.Main();
				}
			}
			else{
				ret = '{##error## : 10000, ##msg##: ##Instantiate objects failed!## }'
			}
		}
		else{
			ret = '{##error## : 10000, ##msg##: ##The file does not exist!## }'
		}
		console.log(ret);
	});
}
else{
	console.log('{##error## : 10000, ##msg##: ##File name cannot be empty!## }');
}
".Replace("##", "\"");
        }

        /// <summary>
        /// 获取python脚本
        /// </summary>
        /// <returns>返回脚本代码</returns>
        private string GetPython()
        {
            return @"#!/usr/bin/python
# _*_ coding:utf-8 _*_  
#  
# @Version : 1.0  
# @Time    : 2017/1/9 10:14
# @Author  : 邱文武
# @File    : python.py

import sys
import imp

def Run(param):
	lh = len(param)
	if(lh < 1):
		return '{~~error~~ : 10000, ~~msg~~: ~~File name cannot be empty!~~ }'
	if(lh < 2):
		return '{~~error~~ : 10000, ~~msg~~: ~~The function name cannot be empty!~~ }'

	fileFullName = param[0] # 文件全名
	funName = param[1] #函数名

	arr = fileFullName.split('\\')
	fileName = arr[len(arr) - 1] # 获取名称部分
	file_path = fileFullName[: -len('\\' + fileName)] # 获取路径部分

	a = imp.find_module(fileName.replace('.py', ''), [file_path]) # 查找模块
	mod = imp.load_module(fileName, a[0], a[1], a[2])

	text = ''
	for attr in dir(mod):
		if attr == funName:
			func = getattr(mod, funName) # 获取函数
			if lh > 3:
				text = func(param[2], param[3])
			elif lh > 2:
				text = func(param[2])
			else:
				text = func()
			bl = True
			break
	if(bl == False):
		text = '{~~error~~ : 10000, ~~msg~~: ~~Function does not exist!~~ }'
	return text
	
#执行主程序
ret = Run(sys.argv[1:])
print(ret)
".Replace("~~", "\"");
        }

        /// <summary>
        /// 获取Lua脚本
        /// </summary>
        /// <returns>返回脚本代码</returns>
        private string GetLua()
        {
            return @"function RunLua(file, fun, pa, pb)
	func = dofile(file);	--加载并编译运行脚本
    return func(fun, pa, pb); --Run(pa, pb);
end

ret = RunLua(arg[1], arg[2], arg[3], arg[4])
print(ret)";
        }

        /// <summary>
        /// 执行DOS命令，返回DOS命令的输出
        /// </summary>
        /// <param name="command">dos命令</param>
        /// <param name="seconds">等待命令执行的时间（单位：毫秒），如果设定为0，则无限等待</param>  
        /// <returns>返回DOS命令的输出</returns>
        public string Execute(string command, int seconds = 10)
        {
            return App("/C " + command, seconds);
        }

        /// <summary>
        /// 执行NodeJS脚本
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="fun">函数名</param>
        /// <param name="param">参数1</param>
        /// <param name="paramB">参数2</param>
        /// <returns>返回执行结果</returns>
        public string RunJs(string file, string fun, string param = null, string paramB = null)
        {
            if (string.IsNullOrEmpty(file))
            {
                Ex = "文件名不能为空！";
                return "";
            }
            else if (string.IsNullOrEmpty(fun))
            {
                Ex = "函数名不能为空！";
                return "";
            }
            var cmd = string.Format("\"{0}\" \"{1}\" ", Cache.path.Cache + "node.js", file);
            if (!string.IsNullOrEmpty(paramB))
            {
                cmd += string.Format("\"{0}\" \"{1}\" \"{2}\"", fun, param.Replace("\"", "\\\""), paramB.Replace("\"", "\\\""));
            }
            else if (!string.IsNullOrEmpty(param))
            {
                cmd += string.Format("\"{0}\" \"{1}\"", fun, param.Replace("\"", "\\\""));
            }
            else
            {
                cmd += string.Format("\"{0}\"", fun);
            }
            //return cmd;
            return Node(cmd, 0);
        }

        /// <summary>
        /// 执行Python脚本
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="fun">函数名</param>
        /// <param name="param">参数1</param>
        /// <param name="paramB">参数2</param>
        /// <param name="bl">是否编译</param>
        /// <returns>返回执行结果</returns>
        public string RunPython(string file, string fun, string param = null, string paramB = null, bool bl = false)
        {
            if (string.IsNullOrEmpty(file))
            {
                Ex = "文件名不能为空！";
                return "";
            }
            else if (string.IsNullOrEmpty(fun))
            {
                Ex = "函数名不能为空！";
                return "";
            }
            var cmd = string.Format("\"{0}\" \"{1}\" ", Cache.path.Cache + "python.py", file);
            if (!string.IsNullOrEmpty(paramB))
            {
                cmd += string.Format("\"{0}\" \"{1}\" \"{2}\"", fun, param.Replace("\"", "\\\""), paramB.Replace("\"", "\\\""));
            }
            else if (!string.IsNullOrEmpty(param))
            {
                cmd += string.Format("\"{0}\" \"{1}\"", fun, param.Replace("\"", "\\\""));
            }
            else
            {
                cmd += string.Format("\"{0}\"", fun);
            }
            if (bl)
            {
                return Python(cmd, 0);
            }
            else
            {
                return Python("-B " + cmd, 0);
            }
        }

        /// <summary>
        /// 执行Lua脚本
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="fun">函数名</param>
        /// <param name="param">参数1</param>
        /// <param name="paramB">参数2</param>
        /// <returns>返回执行结果</returns>
        public string RunLua(string file, string fun, string param, string paramB)
        {
            if (string.IsNullOrEmpty(file) || string.IsNullOrEmpty(fun))
            {
                return "";
            }
            var cmd = string.Format("\"{0}\" \"{1}\" ", Cache.path.Cache + "lua.lua", file);
            if (!string.IsNullOrEmpty(paramB))
            {
                cmd += string.Format("\"{0}\" \"{1}\" \"{2}\"", fun, param.Replace("\"", "\\\""), paramB.Replace("\"", "\\\""));
            }
            else if (!string.IsNullOrEmpty(param))
            {
                cmd += string.Format("\"{0}\" \"{1}\"", fun, param.Replace("\"", "\\\""));
            }
            else
            {
                cmd += string.Format("\"{0}\"", fun);
            }
            return Lua(cmd, 0);
        }

        /// <summary>
        /// 执行python
        /// </summary>
        /// <param name="command">脚本</param>
        /// <param name="seconds">等待时间</param>
        /// <returns>返回执行结果</returns>
        public string Python(string command, int seconds = 0)
        {
            return App(command, seconds, python);
        }

        /// <summary>
        /// 执行NodeJS
        /// </summary>
        /// <param name="command">NodeJS命令</param>
        /// <param name="seconds">等待时间</param>
        /// <returns>返回执行结果</returns>
        public string Node(string command, int seconds = 0)
        {
            return App(command, seconds, node);
        }

        /// <summary>
        /// 执行NodeJS
        /// </summary>
        /// <param name="command">NodeJS命令</param>
        /// <param name="seconds">等待时间</param>
        /// <returns>返回执行结果</returns>
        public string Js(string command, int seconds = 0)
        {
            return App(command, seconds, node);
        }

        /// <summary>
        /// 执行Npm
        /// </summary>
        /// <param name="command">Npm指令</param>
        /// <param name="seconds">等待时间</param>
        /// <returns>返回执行结果</returns>
        public string Npm(string command, int seconds = 0)
        {
            return Execute("npm " + command, seconds);
        }

        /// <summary>
        /// 执行Lua脚本
        /// </summary>
        /// <param name="command">lua指令</param>
        /// <param name="seconds">等待时间</param>
        /// <returns>返回执行结果</returns>
        public string Lua(string command, int seconds = 0)
        {
            return App(command, seconds, lua);
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="command">指令</param>
        /// <param name="seconds">等待时间</param>
        /// <param name="file">程序文件</param>
        /// <returns>返回执行结果</returns>
        public string App(string command, int seconds = 0, string file = "cmd.exe")
        {
            string output = ""; //输出字符串  
            if (!string.IsNullOrEmpty(command))
            {
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = file,      //设定需要执行的命令  
                    Arguments = command,//“/C”表示执行完命令后马上退出  
                    UseShellExecute = false,          //不使用系统外壳程序启动
                    RedirectStandardInput = false,          //不重定向输入
                    RedirectStandardOutput = true,           //重定向输出
                    CreateNoWindow = true            //不创建窗口
                };
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())    //开始进程  
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();  //这里无限等待进程结束  
                        }
                        else
                        {
                            process.WaitForExit(seconds * 1000);   //等待进程结束，等待时间为指定的毫秒  
                        }
                        output = process.StandardOutput.ReadToEnd();    //读取进程的输出  
                        if (output == null)
                        {
                            output = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Ex = ex.Message;
                }
                finally
                {
                    if (process != null)
                    {
                        process.Close();
                    }
                }
            }
            return output.Trim();
        }
    }
}
