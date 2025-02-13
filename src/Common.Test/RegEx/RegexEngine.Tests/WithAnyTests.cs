using System.Text.RegularExpressions;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A with any case tests. </summary>
    public class WithAnyCaseTests
    {
        /// <summary>   With any case add www with any case does match www. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "With Any Tests")]
        public void WithAnyCase_AddwwwWithAnyCase_DoesMatchwWw()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add("www").WithAnyCase();

            var isMatch = engine.IsMatch("wWw");
            Assert.True(isMatch, "Should match any case");
        }

        /// <summary>   With any case add www with any case false does not match www. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "With Any Tests")]
        public void WithAnyCase_AddwwwWithAnyCaseFalse_DoesNotMatchwWw()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add("www").WithAnyCase(false);

            var isMatch = engine.IsMatch("wWw");
            Assert.False(isMatch, "Should not match any case");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     With any case sets correct ignore case RegEx option and has multi line regular expression
        ///     option as default.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "With Any Tests")]
        public void WithAnyCase_SetsCorrectIgnoreCaseRegexOptionAndHasMultiLineRegexOptionAsDefault()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.WithAnyCase();

            var regex = engine.ToRegex();
            Assert.True((regex.Options & RegexOptions.IgnoreCase) != 0, "RegexOptions should have ignoreCase");
            Assert.True((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should have MultiLine as default");
        }
    }
}