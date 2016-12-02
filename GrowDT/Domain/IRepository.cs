using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GrowDT.Domain
{
    public interface IRepository<T, TPrimaryKey> : IReadOnlyRepository<T, TPrimaryKey> where T : IEntity<TPrimaryKey>, IAggregateRoot
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void BulkUpdate(IQueryable<T> query, Expression<Func<T, T>> updateExpression);
        void BulkUpdateByExpression(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression);
        void Delete(T entity);
        void BulkDelete(IQueryable<T> query);
        void BulkDeleteByExpression(Expression<Func<T, bool>> filterExpression);
    }
}
