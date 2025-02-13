using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   An end of line tests. </summary>
    public class EndOfLineTests
    {
        /// <summary>   Ends of line add dot com end of line does not match slash in end. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "End of Line Tests")]
        public void EndOfLine_AddDotComEndOfLine_DoesNotMatchSlashInEnd()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add(".com").EndOfLine();

            var isMatch = engine.IsMatch("http://www.google.com/");
            Assert.False(isMatch, "Should not match '/' in end");
        }

        /// <summary>   Ends of line add dot com to end of line does match dot com in end. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "End of Line Tests")]
        public void EndOfLine_AddDotComtoEndOfLine_DoesMatchDotComInEnd()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add(".com").EndOfLine();

            var isMatch = engine.IsMatch("www.google.com");
            Assert.True(isMatch, "Should match '.com' in end");
        }
    }
}