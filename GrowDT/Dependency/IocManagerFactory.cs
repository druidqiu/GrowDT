namespace GrowDT.Dependency
{
    public static class IocManagerFactory
    {
        private static IIocManager _iocManager;

        public static void Initialize(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public static IIocManager GetInstance()
        {
            return _iocManager;
        }
    }
}
