using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using GrowDT.Domain;
using GrowDT.EntityFramework.Infrastructures;

namespace GrowDT.EntityFramework.Repositories
{
    public class EfRepositoryBase<TDbContext, TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class,IEntity<TPrimaryKey>, IAggregateRoot
        where TDbContext : DbContext,new()
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        protected virtual TDbContext Context { get { return _dbContextProvider.DbContext; } }
        protected virtual DbSet<TEntity> Table { get { return Context.Set<TEntity>(); } }

        protected EfRepositoryBase()
        {
            _dbContextProvider = DbContextProviderFactory.GetDbContextProvider<TDbContext>();
        }

        public override void Add(TEntity entity)
        {
            Table.Add(entity);
        }

        public override void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                Table.Add(entity);
            }
        }

        public override void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public override void UpdateRange(IEnumerable<TEntity> entities)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public override void BulkUpdateByExpression(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            Table.Where(filterExpression).Update(updateExpression);
        }

        public override void BulkUpdate(IQueryable<TEntity> query, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            query.Update(updateExpression);
        }

        public override void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public override void BulkDelete(IQueryable<TEntity> query)
        {
            query.Delete();
        }

        public override void BulkDeleteByExpression(Expression<Func<TEntity, bool>> filterExpression)
        {
            Table.Where(filterExpression).Delete();
        }

        public override TEntity GetById(TPrimaryKey id)
        {
            return Table.Find(id);
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table.AsNoTracking();
        }

        public override IQueryable<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] paths)
        {
            var query = GetAll();
            return paths.Aggregate(query, (current, path) => current.Include(path));
        }
    }
}
