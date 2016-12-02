using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GrowDT.Domain;

namespace GrowDT.MemoryDb.Repositories
{
    public class MemoryRepository<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class,IEntity<TPrimaryKey>, IAggregateRoot
    {
        protected MemoryDatabase Database { get; private set; }

        protected List<TEntity> Table { get { return Database.Set<TEntity>(); } }

        private readonly MemoryPrimaryKeyGenerator<TPrimaryKey> _primaryKeyGenerator;

        public MemoryRepository()
        {
            Database = MemoryDatabaseFactory.GetInstance();
            _primaryKeyGenerator = new MemoryPrimaryKeyGenerator<TPrimaryKey>();
        }

        public override void Add(TEntity entity)
        {
            entity.Id = _primaryKeyGenerator.GetNext();
            Table.Add(entity);
        }

        public override void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var ent in entities)
            {
                Add(ent);
            }
        }

        public override void Update(TEntity entity)
        {
            var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, entity.Id));
            if (index >= 0)
            {
                Table[index] = entity;
            }
        }

        public override void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var ent in entities)
            {
                Update(ent);
            }
        }

        public override void BulkUpdateByExpression(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            throw new Exception("not support");
        }

        public override void BulkUpdate(IQueryable<TEntity> query, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            throw new Exception("not support");
        }

        public override void Delete(TEntity entity)
        {
            var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, entity.Id));
            if (index >= 0)
            {
                Table.RemoveAt(index);
            }
        }

        public override void BulkDelete(IQueryable<TEntity> query)
        {
            foreach (var ent in query.ToList())
            {
                Delete(ent);
            }
        }

        public override void BulkDeleteByExpression(Expression<Func<TEntity, bool>> filterExpression)
        {
            BulkDelete(GetAll().Where(filterExpression));
        }

        public override TEntity GetById(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(t => t.Id.Equals(id));
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        public override IQueryable<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] paths)
        {
            return GetAll();
        }
    }
}