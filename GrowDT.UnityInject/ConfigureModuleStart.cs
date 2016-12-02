using GrowDT.EntityFramework;
using GrowDT.Modules;
using GrowDT.MvcHelper;
using GrowDT.Repositories;
using GrowDT.Services;

namespace GrowDT.UnityInject
{
    public static class ConfigureModuleStart
    {
        public static void Inject()
        {
            new KernalModule().PreInitialize();
            new EntityFrameworkModule().PreInitialize();
            new MvcHelperModule().PreInitialize();
            new GrowDTRepositoryModule().PreInitialize();
            new GrowDTServiceModule().PreInitialize();

            new KernalModule().Initialize();
            new EntityFrameworkModule().Initialize();
            new MvcHelperModule().Initialize();
            new GrowDTRepositoryModule().Initialize();
            new GrowDTServiceModule().Initialize();
        }

        public static void PostInject()
        {
            new KernalModule().PostInitialize();
            new EntityFrameworkModule().PostInitialize();
            new MvcHelperModule().PostInitialize();
            new GrowDTRepositoryModule().PostInitialize();
            new GrowDTServiceModule().PostInitialize();
        }
    }
}
