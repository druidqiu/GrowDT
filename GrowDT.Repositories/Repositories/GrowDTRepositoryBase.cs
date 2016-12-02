using GrowDT.Domain;
using GrowDT.EntityFramework.Repositories;
using GrowDT.Repositories.Infrastructure;

namespace GrowDT.Repositories.Repositories
{
    public class GrowDTRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<GrowDTDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot
    {
    }
}