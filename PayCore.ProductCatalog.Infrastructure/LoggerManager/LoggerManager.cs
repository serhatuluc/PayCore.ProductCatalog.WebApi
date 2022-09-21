using Serilog;
using PayCore.ProductCatalog.Application.Interfaces.Log;
using System;
using Serilog.Events;

namespace PayCore.ProductCatalog.Infrastructure
{
    public class LoggerManager : ILoggerManager
    {
        //It will log to a file is placed to webAPI solution 
        private static ILogger logger = Log.Logger = new LoggerConfiguration()
             .WriteTo.File(
                 path: "logs\\log-.txt",
                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                 rollingInterval: RollingInterval.Day,
                 restrictedToMinimumLevel: LogEventLevel.Information
             ).CreateLogger();

        public void LogDebug(Exception ex,string message)
        {
            logger.Debug(ex,message);
        }
        public void LogError(Exception ex,string message)
        {
            logger.Error(ex,message);
        }

        public void LogInfo(Exception ex,string message)
        {
            logger.Information(ex,message);
        }
        public void LogWarn(Exception ex,string message)
        {
            logger.Warning(ex,message);
        }
    }
}
