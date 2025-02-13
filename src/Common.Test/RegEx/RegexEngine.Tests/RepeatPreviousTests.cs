using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A repeat previous tests. </summary>
    public class RepeatPreviousTests
    {
        /// <summary>   Repeat previous when between two and four A's are added the string is as expected. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Repeat Previous Tests")]
        public void RepeatPrevious_WhenBetweenTwoAndFourAAreAdded_IsAsExpected()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression;

            // Act
            engine.BeginCapture()
                .Add("A")
                .RepeatPrevious(2, 4)
                .EndCapture();

            // Assert
            Assert.Equal("(A{2,4})", engine.ToString());
        }

        /// <summary>   Repeat previous when three A's are added the string is as expected. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Repeat Previous Tests")]
        public void RepeatPrevious_WhenThreeAsAreAdded_IsAsExpected()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression;

            // Act
            engine.BeginCapture()
                .Add("A")
                .RepeatPrevious(3)
                .EndCapture();

            // Assert
            Assert.Equal("(A{3})", engine.ToString());
        }
    }
}