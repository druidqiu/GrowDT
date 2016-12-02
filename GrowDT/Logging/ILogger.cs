using System;

namespace GrowDT.Logging
{
    [Flags]
    public enum LogLevel
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }

    public interface ILogger
    {
        bool IsEnabled(LogLevel level);
        void Log(LogLevel level, string message);
        void Log(LogLevel level,string message, Exception exception);
    }
}
