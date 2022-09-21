using System;

namespace PayCore.ProductCatalog.Application.Interfaces.Log
{
    public interface ILoggerManager
    {
        void LogInfo(Exception ex,string message);
        void LogWarn(Exception ex,string message);
        void LogDebug(Exception ex,string message);
        void LogError(Exception ex,string message);
    }
}
