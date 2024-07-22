﻿namespace Core.Services.Interfaces
{
    public interface ILoggerService
    {
        void LogInformation(string message);
        void LogError(string message);
        void Trace(string message);

    }
}