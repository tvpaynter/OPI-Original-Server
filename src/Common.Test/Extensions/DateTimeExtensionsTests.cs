using System;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void Should_Return_EndOfWeek_With_Valid_Date()
        {
            var target = new DateTime(2020, 5, 29);
            var result = target.EndOfWeek(DayOfWeek.Monday);

            Assert.Equal(DayOfWeek.Monday, result.DayOfWeek);
        }

        [Fact]
        public void Should_Return_FromUnixTime_With_Valid_Double()
        {
            var result = DateTimeExtensions.FromUnixTime(10);

            Assert.NotEqual(DateTime.MinValue, result);
        }

        [Fact]
        public void Should_Return_FromUnixTime_With_Valid_String()
        {
            var result = DateTimeExtensions.FromUnixTime("10");

            Assert.NotEqual(DateTime.MinValue, result);
        }

        [Fact]
        public void Should_Return_SpecifyKind_With_Valid_Date()
        {
            var target = DateTime.Now;
            var result = target.SpecifyKind(DateTimeKind.Utc);

            Assert.NotEqual(DateTime.MinValue, result);
        }

        [Fact]
        public void Should_Return_StartOfWeek_With_Valid_Date()
        {
            var target = new DateTime(2020, 5, 29);
            var result = target.StartOfWeek(DayOfWeek.Monday);

            Assert.Equal(DayOfWeek.Monday, result.DayOfWeek);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_FromIso8601FormattedDateTime_With_Empty_String()
        {
            Assert.Throws<ArgumentException>(() => DateTimeExtensions.FromIso8601FormattedDateTime(string.Empty));
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_FromIso8601FormattedDateTime_With_Null_String()
        {
            Assert.Throws<ArgumentException>(() => DateTimeExtensions.FromIso8601FormattedDateTime(null));
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_FromIso8601FormattedDateTime_With_WhiteSpace_String()
        {
            Assert.Throws<ArgumentException>(() => DateTimeExtensions.FromIso8601FormattedDateTime(" "));
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_FromUnixTime_With_Empty_String()
        {
            Assert.Throws<ArgumentException>(() => DateTimeExtensions.FromUnixTime(string.Empty));
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_FromUnixTime_With_Null_String()
        {
            Assert.Throws<ArgumentException>(() => DateTimeExtensions.FromUnixTime(null));
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_FromUnixTime_With_WhiteSpace_String()
        {
            Assert.Throws<ArgumentException>(() => DateTimeExtensions.FromUnixTime(" "));
        }

        [Fact]
        public void Should_Throw_ArgumentOutOfRangeException_When_EndOfWeek_With_Min_Date()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => DateTime.MinValue.EndOfWeek(DayOfWeek.Monday));
        }

        [Fact]
        public void Should_Throw_ArgumentOutOfRangeException_When_StartOfWeek_With_Min_Date()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => DateTime.MinValue.StartOfWeek(DayOfWeek.Monday));
        }
    }
}