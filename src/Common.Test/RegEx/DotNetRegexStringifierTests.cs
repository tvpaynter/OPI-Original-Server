using Moq;
using StatementIQ.Common.Test.XUnit;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dot net RegEx stringifier tests. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class DotNetRegexStringifierTests
    {
        /// <summary>   The mock repository. </summary>
        private readonly MockRepository mockRepository;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Test.RegEx.DotNetRegexStringifierTests
        ///     class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public DotNetRegexStringifierTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates dot net RegEx stringifier. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <returns>   The new dot net RegEx stringifier. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private DotNetRegexStringifier CreateDotNetRegexStringifier()
        {
            return new DotNetRegexStringifier();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts this  to a token string state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Trait("TDD", "RegEx Tests")]
        [SkippableFact]
        public void ToTokenString_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var dotNetRegexStringifier = CreateDotNetRegexStringifier();
            RegexToken token = default;

            // Act
            var result = dotNetRegexStringifier.ToTokenString(token);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts this  to a group string state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Trait("TDD", "RegEx Tests")]
        [SkippableFact]
        public void ToGroupString_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var dotNetRegexStringifier = CreateDotNetRegexStringifier();
            RegexGroupNode group = null;

            // Act
            var result = dotNetRegexStringifier.ToGroupString(group);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }
    }
}