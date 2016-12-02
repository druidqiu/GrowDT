using System;
using GrowDT.Application;
using log4net;
using log4net.Config;

namespace GrowDT.Logging
{
    public class Log4NetAdapter : ILogger
    {
        private readonly ILog _log;

        public Log4NetAdapter()
        {
            XmlConfigurator.Configure(new Uri(AppConfig.Log4NetConfigUrl));
            _log = LogManager.GetLogger(AppConfig.Log4NetName);
        }

        public bool IsEnabled(LogLevel level)
        {
            var isEnabled = false;
            switch (level)
            {
                case LogLevel.Debug:
                    isEnabled = _log.IsDebugEnabled;
                    break;
                case LogLevel.Information:
                    isEnabled =  _log.IsInfoEnabled;
                    break;
                case LogLevel.Warning:
                    isEnabled = _log.IsWarnEnabled;
                    break;
                case LogLevel.Error:
                    isEnabled = _log.IsErrorEnabled;
                    break;
                case LogLevel.Fatal:
                    isEnabled = _log.IsFatalEnabled;
                    break;
            }

            return isEnabled;
        }



        public void Log(LogLevel level, string message)
        {
            if (!IsEnabled(level))
            {
                return;
            }

            switch (level)
            {
                case LogLevel.Debug:
                    _log.Debug(message);
                    break;
                case LogLevel.Information:
                    _log.Info(message);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message);
                    break;
                case LogLevel.Error:
                    _log.Error(message);
                    break;
                case LogLevel.Fatal:
                    _log.Fatal(message);
                    break;
            }
        }

        public void Log(LogLevel level, string message, Exception exception)
        {
            if (!IsEnabled(level))
            {
                return;
            }

            switch (level)
            {
                case LogLevel.Debug:
                    _log.Debug(message, exception);
                    break;
                case LogLevel.Information:
                    _log.Info(message, exception);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message, exception);
                    break;
                case LogLevel.Error:
                    _log.Error(message, exception);
                    break;
                case LogLevel.Fatal:
                    _log.Fatal(message, exception);
                    break;
            }
        }
    }
}
