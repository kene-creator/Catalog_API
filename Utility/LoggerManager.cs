using Catalog_API.Interface;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = NLog.ILogger;

namespace Catalog_API.Utility
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}