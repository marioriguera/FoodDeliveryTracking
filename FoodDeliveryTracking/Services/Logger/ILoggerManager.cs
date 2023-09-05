﻿namespace FoodDeliveryTracking.Services.Logger
{
    public interface ILoggerManager
    {
        void LogTrace(string message);
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
