using System;

namespace GrowDT.Logging
{
    public class NullLogger : ILogger
    {
        private static readonly ILogger _instance = new NullLogger();

        public static ILogger Instance
        {
            get { return _instance; }
        }

        public bool IsEnabled(LogLevel level)
        {
            return false;
        }

        public void Log(LogLevel level, string message)
        {
            
        }

        public void Log(LogLevel level, string message, Exception exception)
        {
            
        }
    }
}
