using Moq;
using StatementIQ.Common.Test.XUnit;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A RegEx node tests. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexNodeTests
    {
        /// <summary>   The mock repository. </summary>
        private readonly MockRepository mockRepository;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Test.RegEx.RegexNodeTests class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexNodeTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates RegEx node. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <returns>   The new RegEx node. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private RegexNode CreateRegexNode()
        {
            return new RegexNode("dumb");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets inner node state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void SetInnerNode_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexNode = CreateRegexNode();
            RegexNode value = null;

            // Act
            var result = regexNode.SetInnerNode(value);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets pattern state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void SetPattern_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexNode = CreateRegexNode();
            string value = null;

            // Act
            var result = regexNode.SetPattern(value);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts this  to a string state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void ToString_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexNode = CreateRegexNode();
            IRegexStringifier stringifier = null;

            // Act
            var result = regexNode.ToString(stringifier);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }
    }
}