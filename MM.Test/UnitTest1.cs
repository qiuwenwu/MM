using Serilog;
using Xunit;
using System;

using MM.Helper.Base;
using Xunit.Abstractions;

namespace MM.Test
{
    public class UnitTest1
    {
        public UnitTest1(ITestOutputHelper output)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.TestOutput(output, Serilog.Events.LogEventLevel.Verbose)
               .CreateLogger();
        }

        [Fact]
        public void Test1()
        {
            var col = new Colour();
            var color = col.ToHx16(255, 0, 0);
            Log.Debug(color);
            Log.Debug(col.ToRGB(color).ToJson());
            Assert.True(color != "");
        }
    }
}
