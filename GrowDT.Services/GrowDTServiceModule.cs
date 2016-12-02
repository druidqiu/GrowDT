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
            IocManager.RegisterInterceptionType(Assembly.Load("GrowDT.Services"), t => t.Name.EndsWith("Service"));
        }

        public override void PostInitialize()
        {
            
        }
    }
}
