using System.Data.Entity;
using GrowDT.Dependency;

namespace GrowDT.EntityFramework.Infrastructures
{
    public static class DbContextProviderFactory
    {
        private static IIocResolver _iocResolver;

        public static void Initialize(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public static IDbContextProvider<TDbContext> GetDbContextProvider<TDbContext>()
            where TDbContext : DbContext
        {
            return _iocResolver.Resolve<IDbContextProvider<TDbContext>>();
        }
    }
}
