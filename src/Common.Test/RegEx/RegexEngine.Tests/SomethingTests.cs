using System;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   something tests. </summary>
    public class SomethingTests
    {
        /// <summary>   Something empty string as parameter does not match. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Something Tests")]
        public void Something_EmptyStringAsParameter_DoesNotMatch()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression.Something();
            var testString = string.Empty;

            // Act
            var isMatch = engine.IsMatch(testString);

            // Assert
            Assert.False(isMatch, "Test string should be empty.");
        }

        /// <summary>   Something null as parameter throws. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Something Tests")]
        public void Something_NullAsParameter_Throws()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression.Something();
            string testString = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => engine.IsMatch(testString));
        }

        /// <summary>   Something some string as parameter does match. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Something Tests")]
        public void Something_SomeStringAsParameter_DoesMatch()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression.Something();
            const string TEST_STRING = "Test string";

            // Act
            var isMatch = engine.IsMatch(TEST_STRING);

            // Assert
            Assert.True(isMatch, "Test string should not be empty.");
        }
    }
}