using NLog;

namespace FoodDeliveryTracking.Services.Logger.Implementations
{
    /// <summary>
    /// Implementation of the ILoggerManager interface using NLog for logging messages at various levels of severity.
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        /// <inheritdoc />
        public void LogTrace(string message) => logger.Trace(message);

        /// <inheritdoc />
        public void LogDebug(string message) => logger.Debug(message);

        /// <inheritdoc />
        public void LogError(string message) => logger.Error(message);

        /// <inheritdoc />
        public void LogInfo(string message) => logger.Info(message);

        /// <inheritdoc />
        public void LogWarn(string message) => logger.Warn(message);
    }
}
