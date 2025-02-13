using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Faker;
using StatementIQ.Data;
using StatementIQ.Data.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Data.Extensions
{
    public class QueryableExtensionsTests
    {
        public class NameSpecification : ISpecification<string>
        {
            public NameSpecification(string name)
            {
                Name = name;
            }

            public string Name { get; }

            public Expression<Func<string, bool>> IsSatisfied()
            {
                Expression<Func<string, bool>> expression = s => s == Name;
                return expression;
            }
        }

        public class NameProjection : IProjection<string, string>
        {
            public Expression<Func<string, string>> Projection()
            {
                Expression<Func<string, string>> projection = s => s;
                return projection;
            }
        }

        [Fact]
        public void Should_Return_Count_With_Valid_Values()
        {
            var name = Name.First();

            var target = new List<string>
            {
                name,
                Name.First()
            }.AsQueryable();

            var result = target.Count(new NameSpecification(name));

            Assert.Equal(1, result);
        }

        [Fact]
        public void Should_Return_First_With_Valid_Values()
        {
            var name = Name.First();

            var target = new List<string>
            {
                name,
                Name.First()
            }.AsQueryable();

            var result = target.First(new NameSpecification(name));

            Assert.Equal(name, result);
        }

        [Fact]
        public void Should_Return_FirstOrDefault_With_Valid_Values()
        {
            var name = Name.First();

            var target = new List<string>
            {
                name,
                Name.First()
            }.AsQueryable();

            var result = target.FirstOrDefault(new NameSpecification(name));

            Assert.Equal(name, result);
        }

        [Fact]
        public void Should_Return_Select_With_Valid_Values()
        {
            var name = Name.First();

            var target = new List<string>
            {
                name,
                Name.First()
            }.AsQueryable();

            var result = target.Select(new NameProjection());

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Return_Single_With_Valid_Values()
        {
            var name = Name.First();

            var target = new List<string>
            {
                name,
                Name.First()
            }.AsQueryable();

            var result = target.Single(new NameSpecification(name));

            Assert.Equal(name, result);
        }

        [Fact]
        public void Should_Return_SingleOrDefault_With_Valid_Values()
        {
            var name = Name.First();

            var target = new List<string>
            {
                name,
                Name.First()
            }.AsQueryable();

            var result = target.SingleOrDefault(new NameSpecification(name));

            Assert.Equal(name, result);
        }

        [Fact]
        public void Should_Return_Where_With_Valid_Values()
        {
            var name = Name.First();

            var target = new List<string>
            {
                name,
                Name.First()
            }.AsQueryable();

            var result = target.Where(new NameSpecification(name)).First();

            Assert.Equal(name, result);
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Count_With_Null_Queryable()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                default(IQueryable<string>).Count(default(ISpecification<string>));
            });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Count_With_Null_Specification()
        {
            var target = new List<string>().AsQueryable();

            Assert.Throws<ArgumentNullException>(() => { target.Count(default(ISpecification<string>)); });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_First_With_Null_Queryable()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                default(IQueryable<string>).First(default(ISpecification<string>));
            });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_First_With_Null_Specification()
        {
            var target = new List<string>().AsQueryable();

            Assert.Throws<ArgumentNullException>(() => { target.First(default(ISpecification<string>)); });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_FirstOrDefault_With_Null_Queryable()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                default(IQueryable<string>).FirstOrDefault(default(ISpecification<string>));
            });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_FirstOrDefault_With_Null_Specification()
        {
            var target = new List<string>().AsQueryable();

            Assert.Throws<ArgumentNullException>(() => { target.FirstOrDefault(default(ISpecification<string>)); });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Select_With_Null_Projection()
        {
            var target = new List<string>().AsQueryable();

            Assert.Throws<ArgumentNullException>(() => { target.Select(default(IProjection<string, string>)); });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Select_With_Null_Queryable()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                default(IQueryable<string>).Select(default(IProjection<string, string>));
            });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Single_With_Null_Queryable()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                default(IQueryable<string>).Single(default(ISpecification<string>));
            });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Single_With_Null_Specification()
        {
            var target = new List<string>().AsQueryable();

            Assert.Throws<ArgumentNullException>(() => { target.Single(default(ISpecification<string>)); });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_SingleOrDefault_With_Null_Queryable()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                default(IQueryable<string>).SingleOrDefault(default(ISpecification<string>));
            });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_SingleOrDefault_With_Null_Specification()
        {
            var target = new List<string>().AsQueryable();

            Assert.Throws<ArgumentNullException>(() => { target.SingleOrDefault(default(ISpecification<string>)); });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Where_With_Null_Queryable()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                default(IQueryable<string>).Where(default(ISpecification<string>));
            });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_Where_With_Null_Specification()
        {
            var target = new List<string>().AsQueryable();

            Assert.Throws<ArgumentNullException>(() => { target.Where(default(ISpecification<string>)); });
        }
    }
}