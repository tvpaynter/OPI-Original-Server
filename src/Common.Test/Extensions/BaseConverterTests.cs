using System;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class BaseConverterTests
    {
        [Fact]
        public void Should_ConvertFromBaseString_With_Valid_String_And_Digits()
        {
            var target = "BCAXAAZZXYZCABCXXXBZZZYCCBCXAXAAZZXBYXBBXXABAXBYBCCCA";
            var result = target.ConvertFromBaseString("ABCZYX");

            Assert.False(result.IsZero);
        }

        [Fact]
        public void Should_ConvertToBaseString_With_Valid_Bytes()
        {
            var target = "TestAJust";
            var bytes = new byte[target.Length * sizeof(char)];
            Buffer.BlockCopy(target.ToCharArray(), 0, bytes, 0, bytes.Length);

            var result = bytes.ConvertToBaseString("ABCZYX", 2);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Return_False_When_TryConvertFromBase62String_With_Valid_Values()
        {
            var target = "BCAXAAZZXYZCABCXXXBZZZYCCBCXAXAAZZXBYXBBXXABAXBYBCCCA";
            var result = target.TryConvertFromBase62String("ABC", out _);

            Assert.False(result);
        }

        [Fact]
        public void Should_Return_True_When_TryConvertFromBase62String_With_Valid_Values()
        {
            var target = "BCAXAAZZXYZCABCXXXBZZZYCCBCXAXAAZZXBYXBBXXABAXBYBCCCA";
            var result = target.TryConvertFromBase62String("ABCZYX", out _);

            Assert.True(result);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_ConvertToBaseString_With_Empty_String()
        {
            Assert.Throws<ArgumentException>(() => { new byte[1].ConvertToBaseString(string.Empty, 1); });
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_ConvertFromBaseString_Null_Digits()
        {
            var target = "BCAXAAZZXYZCABCXXXBZZZYCCBCXAXAAZZXBYXBBXXABAXBYBCCCA";

            Assert.Throws<ArgumentNullException>(() => target.ConvertFromBaseString(null));
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_ConvertFromBaseString_With_Null_String()
        {
            Assert.Throws<ArgumentNullException>(() => default(string).ConvertFromBaseString("ABC"));
        }

        [Fact]
        public void Should_Throw_ArgumentOutOfRangeException_When_ConvertFromBaseString_Empty_Digits()
        {
            var target = "BCAXAAZZXYZCABCXXXBZZZYCCBCXAXAAZZXBYXBBXXABAXBYBCCCA";

            Assert.Throws<ArgumentOutOfRangeException>(() => target.ConvertFromBaseString(string.Empty));
        }

        [Fact]
        public void Should_Throw_ArgumentOutOfRangeException_When_ConvertFromBaseString_Insufficient_Digits()
        {
            var target = "BCAXAAZZXYZCABCXXXBZZZYCCBCXAXAAZZXBYXBBXXABAXBYBCCCA";

            Assert.Throws<ArgumentOutOfRangeException>(() => target.ConvertFromBaseString("1"));
        }

        [Fact]
        public void Should_Throw_ArgumentOutOfRangeException_When_ConvertToBaseString_With_Len_Less_Than_Two_Length()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { new byte[1].ConvertToBaseString("1", 1); });
        }

        [Fact]
        public void Should_Throw_Ex_When_ConvertFromBaseString_With_Valid_String_But_Invalid_Digits()
        {
            var target = "BCAXAAZZXYZCABCXXXBZZZYCCBCXAXAAZZXBYXBBXXABAXBYBCCCA";

            Assert.Throws<FormatException>(() => target.ConvertFromBaseString("ABC"));
        }

        [Fact]
        public void Should_Throw_FormatException_When_ConvertFromBaseString_With_Empty_String()
        {
            Assert.Throws<FormatException>(() => string.Empty.ConvertFromBaseString("ABC"));
        }
    }
}