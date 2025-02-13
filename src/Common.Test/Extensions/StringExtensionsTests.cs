using System;
using Faker;
using StatementIQ.Extensions;
using StatementIQ.Helpers;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void Should_Return_CompactWhiteSpaces_With_Valid_String()
        {
            var target = string.Concat(Name.FullName(), "  ");
            var result = target.CompactWhiteSpaces();

            Assert.False(result.EndsWith(" "));
        }

        [Fact]
        public void Should_Return_FromBase62_With_Valid_Value()
        {
            var target = "b";
            var result = target.FromBase62();

            Assert.True(result > -1);
        }

        [Fact]
        public void Should_Return_GetAllDigital_With_Valid_Value()
        {
            var target = "b$%*123";
            var result = target.GetAllDigital();

            Assert.True(result.Length == 3);
        }

        [Fact]
        public void Should_Return_RemoveSpecialCharacters_With_Valid_Value()
        {
            var target = "b$%*";
            var result = target.RemoveSpecialCharacters();

            Assert.True(result.Length == 1);
        }

        [Fact]
        public void Should_Return_ToCamelCase()
        {
            var target = Name.FullName().ToLower();
            var result = target.ToCamelCase();

            Assert.NotEqual(target, result);
        }

        [Fact]
        public void Should_Return_ToCamelCase_With_Two_Length_String()
        {
            var target = "Ab";
            var result = target.ToCamelCase();

            Assert.NotEqual(target, result);
        }

        [Fact]
        public void Should_Return_ToPascalCase()
        {
            var target = Name.FullName().ToLower();
            var result = target.ToPascalCase();

            Assert.NotEqual(target, result);
        }

        [Fact]
        public void Should_Return_ToPascalCase_With_Two_Length_String()
        {
            var target = "Ab";
            var result = target.ToPascalCase();

            Assert.Equal(target, result);
        }

        [Fact]
        public void Should_Return_ToSecureString_With_Valid_String()
        {
            var target = Name.First();
            var result = target.ToSecureString();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_CompactWhiteSpaces_With_Empty_String()
        {
            Assert.Throws<ArgumentException>(() => string.Empty.CompactWhiteSpaces());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_CompactWhiteSpaces_With_Null_String()
        {
            Assert.Throws<ArgumentException>(() => default(string).CompactWhiteSpaces());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_CompactWhiteSpaces_With_WhiteSpace_String()
        {
            Assert.Throws<ArgumentException>(() => " ".CompactWhiteSpaces());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_FromBase62_With_Empty_String()
        {
            Assert.Throws<ArgumentException>(() => string.Empty.FromBase62());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_FromBase62_With_Null_String()
        {
            Assert.Throws<ArgumentException>(() => default(string).FromBase62());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_FromBase62_With_WhiteSpace_String()
        {
            Assert.Throws<ArgumentException>(() => " ".FromBase62());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_GetAllDigital_With_Empty_String()
        {
            Assert.Throws<ArgumentException>(() => string.Empty.GetAllDigital());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_GetAllDigital_With_Null_String()
        {
            Assert.Throws<ArgumentException>(() => default(string).GetAllDigital());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_GetAllDigital_With_WhiteSpace_String()
        {
            Assert.Throws<ArgumentException>(() => " ".GetAllDigital());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_RemoveSpecialCharacters_With_Empty_String()
        {
            Assert.Throws<ArgumentException>(() => string.Empty.RemoveSpecialCharacters());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_RemoveSpecialCharacters_With_Null_String()
        {
            Assert.Throws<ArgumentException>(() => default(string).RemoveSpecialCharacters());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_RemoveSpecialCharacters_With_WhiteSpace_String()
        {
            Assert.Throws<ArgumentException>(() => " ".RemoveSpecialCharacters());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_ToSecureString_With_Empty_String()
        {
            Assert.Throws<ArgumentException>(() => string.Empty.ToSecureString());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_ToSecureString_With_Null_String()
        {
            Assert.Throws<ArgumentException>(() => default(string).ToSecureString());
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_ToSecureString_With_WhiteSpace_String()
        {
            Assert.Throws<ArgumentException>(() => " ".ToSecureString());
        }
    }
}