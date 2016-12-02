using System;
using System.Linq;
using System.Linq.Expressions;

namespace GrowDT.Domain
{
    public interface IReadOnlyRepository<T, TPrimaryKey> where T : IEntity<TPrimaryKey>, IAggregateRoot
    {
        T GetById(TPrimaryKey id);
        T Get(Expression<Func<T, bool>> filterExpression);
        IQueryable<T> GetAll();
        IQueryable<T> GetMany(Expression<Func<T, bool>> filterExpression);
        IQueryable<T> GetAllInclude(params Expression<Func<T, object>>[] paths);
    }
}
