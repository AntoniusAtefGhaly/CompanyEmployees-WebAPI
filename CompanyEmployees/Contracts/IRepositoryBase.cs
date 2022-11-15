using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);

        T GetById(Guid Id, bool trackChanges);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

        T GetFirstInclude(Expression<Func<T, bool>> expression, Expression<Func<T, object>> includeExpression, bool trackChanges);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}