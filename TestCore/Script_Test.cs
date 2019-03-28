using System;
using MM.Engine;

namespace TestCore
{
    public class Script_Test
    {
        public static void Run()
        {
            var f = "lua"; // 支持lua 、 python 、csharp三种脚本
            Console.WriteLine("开始");
            var Indexer = new Indexer();
            // var ret = Indexer.RunFile(Cache.runPath + "script\\test." + f, "fun", "a", "b", "c");
             var ret = Indexer.RunFile("f:\\mm\\testcore\\bin\\debug\\netcoreapp3.0\\script\\demo_task.py", "fun", "a", "b", "c");

            Console.WriteLine(f + "脚本测试");
            Console.WriteLine(ret);
            Console.WriteLine(Indexer.Ex);
        }
    }
}
