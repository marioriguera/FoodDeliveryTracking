namespace FoodDeliveryTracking.Services.Logger
{
    /// <summary>
    /// Defines an interface for a logger manager that provides methods for logging messages at various levels of severity.
    /// </summary>
    public interface ILoggerManager
    {
        /// <summary>
        /// Logs a trace-level message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogTrace(string message);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogWarn(string message);

        /// <summary>
        /// Logs a debug-level message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogDebug(string message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogError(string message);
    }
}
