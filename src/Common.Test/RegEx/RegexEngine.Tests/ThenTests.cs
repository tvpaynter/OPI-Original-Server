using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A then tests. </summary>
    public class ThenTests
    {
        /// <summary>   Then engine builder email does match email. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Then Tests")]
        public void Then_EngineBuilderEmail_DoesMatchEmail()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.StartOfLine().Then(CommonRegex.Email);

            var isMatch = engine.IsMatch("test@statementiq.com");
            Assert.True(isMatch, "Should match email address");
        }

        /// <summary>   Then engine builder email does not match URL. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Then Tests")]
        public void Then_EngineBuilderEmail_DoesNotMatchUrl()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.StartOfLine().Then(CommonRegex.Email);

            var isMatch = engine.IsMatch("http://www.statementiq.com");
            Assert.False(isMatch, "Should not match url address");
        }

        /// <summary>   Then engine builder URL does match URL. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Then Tests")]
        public void Then_EngineBuilderUrl_DoesMatchUrl()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.StartOfLine()
                .Then(CommonRegex.Url);

            Assert.True(engine.IsMatch("http://www.statementiq.com"), "Should match url address");
            Assert.True(engine.IsMatch("https://www.statementiq.com"), "Should match url address");
            Assert.True(engine.IsMatch("http://statementiq.com"), "Should match url address");
        }

        /// <summary>   Then engine builder URL does not match email. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Then Tests")]
        public void Then_EngineBuilderUrl_DoesNotMatchEmail()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.StartOfLine().Then(CommonRegex.Url);

            Assert.False(engine.IsMatch("test@statementiq.com"), "Should not match email address");
        }
    }
}