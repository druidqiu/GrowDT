using GrowDT.Dependency;
using GrowDT.Domain;
using GrowDT.MemoryDb.Repositories;
using GrowDT.MemoryDb.Uow;
using GrowDT.Models.Entities;
using GrowDT.Modules;

namespace GrowDT.Repositories
{
    public sealed class GrowDTRepositoryModule : BaseModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            #region Ef
            ////IocManager.RegisterType(Assembly.Load("GrowDT.Repositories"), t => typeof(IDependency).IsAssignableFrom(t));
            //IocManager.RegisterInterceptionType(Assembly.Load("GrowDT.Repositories"), t => t.Name.EndsWith("Repository"));
            //IocManager.RegisterInterceptionType(typeof(IReadOnlyRepository<,>), typeof(GrowDTRepositoryBase<,>), DependencyLifeStyle.Transient);
            //IocManager.RegisterInterceptionType(typeof(IRepository<,>), typeof(GrowDTRepositoryBase<,>), DependencyLifeStyle.Transient);
            //IocManager.RegisterInterceptionType<IUnitOfWork, GrowDTUnitOfWork>(DependencyLifeStyle.Transient);
            #endregion Ef

            #region Memory
            IocManager.RegisterInterceptionType(typeof(IReadOnlyRepository<,>), typeof(MemoryRepository<,>), DependencyLifeStyle.Transient);
            IocManager.RegisterInterceptionType(typeof(IRepository<,>), typeof(MemoryRepository<,>), DependencyLifeStyle.Transient);
            IocManager.RegisterInterceptionType<IUnitOfWork, MemoryDbUnitOfWork>(DependencyLifeStyle.Transient);
            #endregion Memory
        }

        public override void PostInitialize()
        {
            #region Ef
            ////Database.SetInitializer<QxrTestDbContext>(null);
            //Database.SetInitializer(new GrowDTDbInitializer());
            #endregion Ef

            #region Memory
            var _userRepository =  IocManager.Resolve<IRepository<User, int>>();
            _userRepository.Add(new User {Code = "admin", Name = "administrator"});
            #endregion Memeory
        }
    }
}
