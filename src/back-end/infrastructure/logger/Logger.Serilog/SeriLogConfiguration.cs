using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace SuitSupply.Infrastructure.Logger.Serilog
{
    public static class SeriLogConfiguration
    {
        public static void Configure(string filePath = "")
        {
            var loggerConfiguration = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.ColoredConsole();
            if (!string.IsNullOrEmpty(filePath))
                loggerConfiguration.WriteTo.File(filePath, rollingInterval: RollingInterval.Day);
            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}
