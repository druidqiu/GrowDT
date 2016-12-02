using GrowDT.Application;
using GrowDT.Modules;
using GrowDT.Dependency;
using GrowDT.MvcHelper.Authorization;
using GrowDT.MvcHelper.Binders;
using GrowDT.MvcHelper.Customization;
using StackExchange.Profiling.EntityFramework6;

namespace GrowDT.MvcHelper
{
    public sealed class MvcHelperModule : BaseModule
    {
        public override void PreInitialize()
        {
            IocManager.RegisterType<IAppConfigManager, HttpConfigManager>(DependencyLifeStyle.Singleton);
        }

        public override void Initialize()
        {
            IocManager.RegisterType<IAuthProvider, FormsAuthProvider>(DependencyLifeStyle.Singleton);
        }

        public override void PostInitialize()
        {
            BinderManager.Bind();

            if (AppConfig.MiniProfilerEnable)
            {
                MiniProfilerEF6.Initialize();
            }
        }
    }
}
