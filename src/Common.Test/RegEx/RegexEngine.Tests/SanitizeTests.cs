using System;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   Sanitize tests. </summary>
    public class SanitizeTests
    {
        /// <summary>   Sanitize add characters that should be escaped returns escaped string. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Sanitize Tests")]
        public void Sanitize_AddCharactersThatShouldBeEscaped_ReturnsEscapedString()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var value = "*+?";
            var result = string.Empty;
            var expected = @"\*\+\?";

            //Act
            result = engine.Sanitize(value);

            //Assert
            Assert.Equal(expected, result);
        }

        /// <summary>   Sanitize handles null string. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Sanitize Tests")]
        public void Sanitize_Handles_Null_String()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            string value = null;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => engine.Sanitize(value) + " cannot be null");
        }
    }
}