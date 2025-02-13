using System.Linq;
using MandateThat;

namespace StatementIQ.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static int Count<TSource>(this IQueryable<TSource> source,
            ISpecification<TSource> specification)
            where TSource : class
        {
            Mandate.That(source, nameof(source)).IsNotNull();
            Mandate.That(specification, nameof(specification)).IsNotNull();

            return source.Count(specification.IsSatisfied());
        }

        public static TSource Single<TSource>(this IQueryable<TSource> source, ISpecification<TSource> specification)
            where TSource : class
        {
            Mandate.That(source, nameof(source)).IsNotNull();
            Mandate.That(specification, nameof(specification)).IsNotNull();

            return source.Single(specification.IsSatisfied());
        }

        public static TSource SingleOrDefault<TSource>(this IQueryable<TSource> source,
            ISpecification<TSource> specification)
            where TSource : class
        {
            Mandate.That(source, nameof(source)).IsNotNull();
            Mandate.That(specification, nameof(specification)).IsNotNull();

            return source.SingleOrDefault(specification.IsSatisfied());
        }

        public static TSource First<TSource>(this IQueryable<TSource> source,
            ISpecification<TSource> specification)
            where TSource : class
        {
            Mandate.That(source, nameof(source)).IsNotNull();
            Mandate.That(specification, nameof(specification)).IsNotNull();

            return source.First(specification.IsSatisfied());
        }

        public static TSource FirstOrDefault<TSource>(this IQueryable<TSource> source,
            ISpecification<TSource> specification)
            where TSource : class
        {
            Mandate.That(source, nameof(source)).IsNotNull();
            Mandate.That(specification, nameof(specification)).IsNotNull();

            return source.FirstOrDefault(specification.IsSatisfied());
        }

        public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source,
            IProjection<TSource, TResult> projection)
            where TSource : class
        {
            Mandate.That(source, nameof(source)).IsNotNull();
            Mandate.That(projection, nameof(projection)).IsNotNull();

            return source.Select(projection.Projection());
        }

        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source,
            ISpecification<TSource> specification)
            where TSource : class
        {
            Mandate.That(source, nameof(source)).IsNotNull();
            Mandate.That(specification, nameof(specification)).IsNotNull();

            return source.Where(specification.IsSatisfied());
        }
    }
}