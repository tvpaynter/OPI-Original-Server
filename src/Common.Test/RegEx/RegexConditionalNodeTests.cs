using Moq;
using StatementIQ.Common.Test.XUnit;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A RegEx conditional node tests. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexConditionalNodeTests
    {
        /// <summary>   The mock repository. </summary>
        private readonly MockRepository mockRepository;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Test.RegEx.RegexConditionalNodeTests
        ///     class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexConditionalNodeTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates RegEx conditional node. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <returns>   The new RegEx conditional node. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private RegexConditionalNode CreateRegexConditionalNode()
        {
            return new RegexConditionalNode(new RegexNode("*.*"), "true", "false");
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
            var regexConditionalNode = CreateRegexConditionalNode();

            // Act


            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }
    }
}