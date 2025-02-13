using Xunit;
using StatementIQ.Extensions;

namespace StatementIQ.Common.Test.Extensions
{
    public class LongExtensionsTests
    {
        [Fact]
        public void Should_Return_ToBase62_With_Valid_Number()
        {
            var target = 179UL;
            var result = target.ToBase62();

            Assert.NotEmpty(result);
        }
    }
}