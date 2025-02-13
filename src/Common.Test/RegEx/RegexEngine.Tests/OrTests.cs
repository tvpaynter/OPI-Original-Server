using StatementIQ.RegEx.RegexEngine;
using Xunit;
using Xunit.Abstractions;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   An or tests. </summary>
    public class OrTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.RegexEngine.Tests.OrTests class.
        /// </summary>
        /// <param name="testOutputHelper"> The test output helper. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public OrTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        /// <summary>   The test output helper. </summary>
        private readonly ITestOutputHelper _testOutputHelper;

        /// <summary>   Or add com or organization does match com and organization. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Or Tests")]
        public void Or_AddComOrOrg_DoesMatchComAndOrg()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add("com").Or("org");

            _testOutputHelper.WriteLine(engine.ToString());
            Assert.True(engine.IsMatch("org"), "Should match 'org'");
            Assert.True(engine.IsMatch("com"), "Should match 'com'");
        }

        /// <summary>   Or add com or organization RegEx is as expected. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Or Tests")]
        public void Or_AddComOrOrg_RegexIsAsExpected()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add("com").Or("org");

            Assert.Equal("(com)|(org)", engine.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Or regex engine URL or regex engine email does match email and URL.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Or Tests")]
        public void Or_VerbalExpressionsUrlOrVerbalExpressionEmail_DoesMatchEmailAndUrl()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add(CommonRegex.Url)
                .Or(CommonRegex.Email);

            Assert.True(engine.IsMatch("test@github.com"), "Should match email address");
            Assert.True(engine.IsMatch("http://www.google.com"), "Should match url address");
        }
    }
}