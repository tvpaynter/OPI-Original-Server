using Moq;
using StatementIQ.Common.Test.XUnit;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A RegEx group node tests. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexGroupNodeTests
    {
        /// <summary>   The mock repository. </summary>
        private readonly MockRepository mockRepository;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Test.RegEx.RegexGroupNodeTests class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGroupNodeTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates RegEx group node. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <returns>   The new RegEx group node. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private RegexGroupNode CreateRegexGroupNode()
        {
            return new RegexGroupNode("*.*");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets is capturing group state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void SetIsCapturingGroup_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGroupNode = CreateRegexGroupNode();
            var value = false;

            // Act
            var result = regexGroupNode.SetIsCapturingGroup(value);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets name state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void SetName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGroupNode = CreateRegexGroupNode();
            string value = null;

            // Act
            var result = regexGroupNode.SetName(value);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }
    }
}