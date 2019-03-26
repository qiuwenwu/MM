using System;
using MM.Engine;

namespace TestCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = "lua";
            Console.WriteLine("开始");
            var Indexer = new Indexer();
            var ret = Indexer.RunFile(Cache.runPath + "script\\test." + f, "fun", "a", "b", "c");
            Console.WriteLine(f + "脚本测试");
            Console.WriteLine(ret);
            Console.WriteLine(Indexer.Ex);
        }
    }
}
