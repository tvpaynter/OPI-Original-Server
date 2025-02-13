using System;
using System.Text;
using Faker;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class StringBuilderExtensionsTests
    {
        [Fact]
        public void Should_Return_CompactWhiteSpaces_With_Empty_StringBuilder()
        {
            var target = new StringBuilder();
            target.CompactWhiteSpaces();

            Assert.Equal(0, target.ToString().Length);
        }

        [Fact]
        public void Should_Return_CompactWhiteSpaces_With_Valid_StringBuilder()
        {
            var target = new StringBuilder();
            target.Append(Name.FullName());
            target.Append(" ");
            target.Append(Name.FullName());
            target.Append("   ");

            target.CompactWhiteSpaces();

            Assert.False(target.ToString().EndsWith(" "));
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_CompactWhiteSpaces_With_Null_StringBuilder()
        {
            Assert.Throws<ArgumentNullException>(() => default(StringBuilder).CompactWhiteSpaces());
        }
    }
}