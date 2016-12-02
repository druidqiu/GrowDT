using GrowDT.Dependency;
using GrowDT.EntityFramework.DbContextStorage;
using GrowDT.EntityFramework.Infrastructures;
using GrowDT.Modules;

namespace GrowDT.EntityFramework
{
    public sealed class EntityFrameworkModule : BaseModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            IocManager.RegisterType<IDbContextStorage, HttpDbContextStorage>(DependencyLifeStyle.Singleton);
            IocManager.RegisterType(typeof (IDbContextProvider<>), typeof (SimpleDbContextProvider<>), DependencyLifeStyle.Singleton);
            DbContextProviderFactory.Initialize(IocManager.Resolve<IIocManager>());
        }

        public override void PostInitialize()
        {

        }
    }
}
