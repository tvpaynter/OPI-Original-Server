using System;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   something but tests. </summary>
    public class SomethingButTests
    {
        /// <summary>   Something but empty string as parameter does not match. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Something But Tests")]
        public void SomethingBut_EmptyStringAsParameter_DoesNotMatch()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression.SomethingBut("Test");
            var testString = string.Empty;

            // Act
            var isMatch = engine.IsMatch(testString);

            // Assert
            Assert.False(isMatch);
        }

        /// <summary>   Something but null as parameter throws. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Something But Tests")]
        public void SomethingBut_NullAsParameter_Throws()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression.SomethingBut("Test");
            string testString = null;

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => engine.IsMatch(testString) + " cannot be null");
        }

        /// <summary>   Something but test string starts correct does match. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Something But Tests")]
        public void SomethingBut_TestStringStartsCorrect_DoesMatch()
        {
            // Arrange
            const string START_STRING = "Test";
            var engine = EngineBuilder.DefaultExpression.SomethingBut(START_STRING);
            const string TEST_STRING = "Test string";

            // Act
            var isMatch = engine.IsMatch(TEST_STRING);

            // Assert
            Assert.True(isMatch, "Test string should not be empty and starts with \"" + START_STRING + "\".");
        }

        /// <summary>   Something but test string starts incorrect does not match. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Something But Tests")]
        public void SomethingBut_TestStringStartsIncorrect_DoesNotMatch()
        {
            // Arrange
            const string START_STRING = "Test";
            var engine = EngineBuilder.DefaultExpression.SomethingBut(START_STRING);
            const string TEST_STRING = "string";

            // Act
            var isMatch = engine.IsMatch(TEST_STRING);

            // Assert
            Assert.True(isMatch, "Test string starts with \"" + START_STRING + "\".");
        }
    }
}