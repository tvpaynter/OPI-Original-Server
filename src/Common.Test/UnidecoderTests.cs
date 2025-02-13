using Faker;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test
{
    public class UnidecoderTests
    {
        [Fact]
        public void Should_Return_Unidecode_With_Valid_Char()
        {
            var result = 'L'.Unidecode();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Return_Unidecode_With_Valid_String()
        {
            var result = Name.First().Unidecode();

            Assert.NotNull(result);
        }
    }
}