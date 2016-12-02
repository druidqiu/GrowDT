namespace GrowDT.Application
{
    public static class AppConfigManagerFactory
    {
        private static IAppConfigManager _configer;

        public static void Initialize(IAppConfigManager configer)
        {
            _configer = configer;
        }

        public static IAppConfigManager GetAppConfigManager()
        {
            return _configer;
        }
    }
}
