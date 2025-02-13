using Moq;
using StatementIQ.Common.Test.XUnit;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A RegEx unicode category node tests. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexUnicodeCategoryNodeTests
    {
        /// <summary>   The mock repository. </summary>
        private readonly MockRepository mockRepository;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Test.RegEx.RegexUnicodeCategoryNodeTests
        ///     class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexUnicodeCategoryNodeTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates RegEx unicode category node. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <returns>   The new RegEx unicode category node. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private RegexUnicodeCategoryNode CreateRegexUnicodeCategoryNode()
        {
            return new RegexUnicodeCategoryNode(RegexUnicodeCategoryFlag.Zs);
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
            var regexUnicodeCategoryNode = CreateRegexUnicodeCategoryNode();

            // Act


            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }
    }
}