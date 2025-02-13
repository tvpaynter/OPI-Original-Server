using System;
using System.Linq.Expressions;

namespace StatementIQ.Data
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> IsSatisfied();
    }
}