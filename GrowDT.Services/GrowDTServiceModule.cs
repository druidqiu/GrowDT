using System.Reflection;
using GrowDT.Modules;

namespace GrowDT.Services
{
    public sealed class GrowDTServiceModule : BaseModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            new AutoMappers.AutoMapperBootstrap().CreateMappings(Assembly.GetExecutingAssembly());
            IocManager.RegisterInterceptionType(Assembly.GetExecutingAssembly(), t => t.Name.EndsWith("Service"));
        }

        public override void PostInitialize()
        {
            
        }
    }
}
