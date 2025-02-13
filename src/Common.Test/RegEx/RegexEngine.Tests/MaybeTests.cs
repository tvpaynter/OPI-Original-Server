using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A maybe tests. </summary>
    public class MaybeTests
    {
        /// <summary>   Maybe when called uses common RegEx email. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Maybe Tests")]
        public void Maybe_WhenCalled_UsesCommonRegexEmail()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Maybe(CommonRegex.Email);

            Assert.True(engine.IsMatch("test@statementiq.com"), "Should match email address");
        }

        /// <summary>   Maybe when called uses common RegEx URL. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Maybe Tests")]
        public void Maybe_WhenCalled_UsesCommonRegexUrl()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Maybe(CommonRegex.Url);

            Assert.True(engine.IsMatch("http://www.google.com"), "Should match url address");
        }
    }
}