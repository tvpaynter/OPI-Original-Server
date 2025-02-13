using Moq;
using StatementIQ.Common.Test.XUnit;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An ecma script RegEx stringifier tests. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class ECMAScriptRegexStringifierTests
    {
        /// <summary>   The mock repository. </summary>
        private readonly MockRepository mockRepository;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Test.RegEx.ECMAScriptRegexStringifierTests
        ///     class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public ECMAScriptRegexStringifierTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates ecma script RegEx stringifier. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <returns>   The new ecma script RegEx stringifier. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private ECMAScriptRegexStringifier CreateECMAScriptRegexStringifier()
        {
            return new ECMAScriptRegexStringifier();
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
            var eCMAScriptRegexStringifier = CreateECMAScriptRegexStringifier();
            RegexNode node = null;

            // Act
            var result = eCMAScriptRegexStringifier.ToString(node);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }
    }
}