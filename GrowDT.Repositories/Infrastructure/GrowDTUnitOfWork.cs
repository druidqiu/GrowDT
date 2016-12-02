using GrowDT.EntityFramework.Infrastructures;
using GrowDT.EntityFramework.Uow;

namespace GrowDT.Repositories.Infrastructure
{
    public class GrowDTUnitOfWork : EfUnitOfWork<GrowDTDbContext>
    {
        public GrowDTUnitOfWork(IDbContextProvider<GrowDTDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
