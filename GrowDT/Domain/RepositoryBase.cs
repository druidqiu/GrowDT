using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GrowDT.Domain
{
    public abstract class RepositoryBase<T, TPrimaryKey> : IRepository<T, TPrimaryKey> where T : class,IEntity<TPrimaryKey>, IAggregateRoot
    {
        public abstract void Add(T entity);

        public abstract void AddRange(IEnumerable<T> entities);

        public abstract void Update(T entity);

        public abstract void UpdateRange(IEnumerable<T> entities);

        public abstract void BulkUpdateByExpression(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression);

        public abstract void BulkUpdate(IQueryable<T> query, Expression<Func<T, T>> updateExpression);

        public abstract void Delete(T entity);

        public abstract void BulkDelete(IQueryable<T> query);

        public abstract void BulkDeleteByExpression(Expression<Func<T, bool>> filterExpression);

        public abstract T GetById(TPrimaryKey id);

        public virtual T Get(Expression<Func<T, bool>> filterExpression)
        {
            return GetAll().Where(filterExpression).FirstOrDefault();
        }

        public abstract IQueryable<T> GetAll();

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> filterExpression)
        {
            return GetAll().Where(filterExpression);
        }

        public abstract IQueryable<T> GetAllInclude(params Expression<Func<T, object>>[] paths);
    }
}
