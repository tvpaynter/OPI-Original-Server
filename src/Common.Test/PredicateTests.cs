using System;
using Faker;
using Xunit;

namespace StatementIQ.Common.Test
{
    public class PredicateTests
    {
        [Fact]
        public void Should_Return_TryCache_With_Invalid_Func_Handling_Error()
        {
            var errorMessage = "Error";

            var result = Predicate.TryCache(() => { throw new Exception(errorMessage); },
                () => { return errorMessage; });

            Assert.Equal(errorMessage, result);
        }

        [Fact]
        public void Should_Return_TryCache_With_Valid_Action()
        {
            var name = default(string);

            Predicate.TryCache(() => { name = Name.FullName(); });

            Assert.NotNull(name);
        }

        [Fact]
        public void Should_Return_TryCache_With_Valid_Func()
        {
            var result = Predicate.TryCache(() => { return Name.FullName(); });

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Return_TryCache_With_Valid_Func_Handling_Error()
        {
            var result = Predicate.TryCache(() => { return Name.FullName(); },
                () => { return "Error"; });

            Assert.NotNull(result);
        }
    }
}