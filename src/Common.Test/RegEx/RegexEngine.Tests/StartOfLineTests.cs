using System.Text.RegularExpressions;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A start of line tests. </summary>
    public class StartOfLineTests
    {
        /// <summary>   Starts of line creates correct RegEx. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Start of Line Tests")]
        public void StartOfLine_CreatesCorrectRegex()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.StartOfLine();
            Assert.Equal("^", engine.ToString());
        }

        /// <summary>   Starts of line then HTTP maybe WWW does match HTTP in start. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Start of Line Tests")]
        public void StartOfLine_ThenHttpMaybeWww_DoesMatchHttpInStart()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.StartOfLine()
                .Then("http")
                .Maybe("www");

            var isMatch = Regex.IsMatch("http", engine.ToString());
            Assert.True(isMatch, "Should match http in start");
        }

        /// <summary>   Starts of line then HTTP maybe WWW does not match WWW in start. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Start of Line Tests")]
        public void StartOfLine_ThenHttpMaybeWww_DoesNotMatchWwwInStart()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.StartOfLine()
                .Then("http")
                .Maybe("www");

            var isMatch = engine.IsMatch("www");
            Assert.False(isMatch, "Should not match www in start");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Starts of line when placed in random call order should append at the beginning of the
        ///     expression.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Start of Line Tests")]
        public void StartOfLine_WhenPlacedInRandomCallOrder_ShouldAppendAtTheBeginningOfTheExpression()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add("test")
                .Add("ing")
                .StartOfLine();

            var text = "testing1234";
            Assert.True(engine.IsMatch(text), "Should match that the text starts with test");
        }
    }
}