using System;
using System.Linq.Expressions;

namespace StatementIQ.Data
{
    public interface IProjection<TEntity, TResult>
        where TEntity : class
    {
        Expression<Func<TEntity, TResult>> Projection();
    }
}