using System;
using System.Security;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class SecureStringExtensionsTests
    {
        [Fact]
        public void Should_Return_AppendString_With_Valid_String()
        {
            var target = new SecureString();
            var result = target.AppendString("xyz");

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Return_GetString_With_Valid_Values()
        {
            var letter = 'A';

            var target = new SecureString();
            target.AppendChar(letter);

            var result = target.GetString();

            Assert.Equal(letter.ToString(), result);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_AppendString_With_Empty_String()
        {
            var target = new SecureString();

            Assert.Throws<ArgumentException>(() => target.AppendString(null));
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_AppendString_With_Null_String()
        {
            var target = new SecureString();

            Assert.Throws<ArgumentException>(() => target.AppendString(null));
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_AppendString_With_WhiteSpace_String()
        {
            var target = new SecureString();

            Assert.Throws<ArgumentException>(() => target.AppendString(" "));
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_AppendString_With_Null_SecureString()
        {
            Assert.Throws<ArgumentNullException>(() => default(SecureString).AppendString("xyz"));
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_GetString_With_Null_SecureString()
        {
            Assert.Throws<ArgumentNullException>(() => default(SecureString).GetString());
        }
    }
}