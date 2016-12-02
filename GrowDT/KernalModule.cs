using GrowDT.Application;
using GrowDT.Dependency;
using GrowDT.Logging;
using GrowDT.Modules;
using GrowDT.Utility.Email;

namespace GrowDT
{
    public sealed class KernalModule : BaseModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            AppConfigManagerFactory.Initialize(IocManager.Resolve<IAppConfigManager>());
            IocManager.RegisterType<ILogger, Log4NetAdapter>(DependencyLifeStyle.Singleton);
            LoggingFactory.Initialize(IocManager.Resolve<ILogger>());

            IocManager.RegisterType<IEmailService, SmtpMailService>(DependencyLifeStyle.Singleton);
            EmailServiceFactory.Initialize(IocManager.Resolve<IEmailService>());
        }

        public override void PostInitialize()
        {

        }
    }
}
