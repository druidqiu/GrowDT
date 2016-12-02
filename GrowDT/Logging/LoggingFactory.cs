namespace GrowDT.Logging
{
    public static class LoggingFactory
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        public static ILogger GetLogger()
        {
            return _logger ?? NullLogger.Instance;
        }

        //TODO:
        private static ILogger GetLogger(string loggerType)
        {
            return _logger;
        }
    }
}
