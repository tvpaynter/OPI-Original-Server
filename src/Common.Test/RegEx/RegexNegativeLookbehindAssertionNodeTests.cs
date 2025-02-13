using Moq;
using StatementIQ.Common.Test.XUnit;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A RegEx negative lookbehind assertion node tests. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexNegativeLookbehindAssertionNodeTests
    {
        /// <summary>   The mock repository. </summary>
        private readonly MockRepository mockRepository;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Test.RegEx.RegexNegativeLookbehindAssertionNodeTests class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexNegativeLookbehindAssertionNodeTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates RegEx negative lookbehind assertion node. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <returns>   The new RegEx negative lookbehind assertion node. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private RegexNegativeLookbehindAssertionNode CreateRegexNegativeLookbehindAssertionNode()
        {
            return new RegexNegativeLookbehindAssertionNode(string.Empty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests method 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void TestMethod1()
        {
            // Arrange
            var regexNegativeLookbehindAssertionNode = CreateRegexNegativeLookbehindAssertionNode();

            // Act


            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }
    }
}