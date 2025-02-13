using Moq;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A common RegEx tests. </summary>
    public class CommonRegexTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.RegexEngine.Tests.CommonRegexTests
        ///     class.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public CommonRegexTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        /// <summary>   The mock repository. </summary>
        private readonly MockRepository mockRepository;

        /// <summary>   Getting the common regex email expression name succeeds. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Common RegEx Tests")]
        public void GetCommonRegex_Email_Name_IsSuccessful()
        {
            // Arrange
            var name = CommonRegex.Email.Name;

            // Act

            // Assert
            Assert.True(!string.IsNullOrWhiteSpace(name));
            mockRepository.VerifyAll();
        }

        /// <summary>   Getting the common regex email expression value succeeds. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Common RegEx Tests")]
        public void GetCommonRegex_Email_Value_IsSuccessful()
        {
            // Arrange
            var value = CommonRegex.Email.Value;

            // Act

            // Assert
            Assert.True(value == 2);
            mockRepository.VerifyAll();
        }

        /// <summary>   Getting the common regex URL expression name succeeds. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Common RegEx Tests")]
        public void GetCommonRegex_Url_Name_IsSuccessful()
        {
            // Arrange
            var name = CommonRegex.Url.Name;

            // Act

            // Assert
            Assert.True(!string.IsNullOrWhiteSpace(name));
            mockRepository.VerifyAll();
        }

        /// <summary>   Getting the common regex URL expression value succeeds. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Common RegEx Tests")]
        public void GetCommonRegex_Url_Value_IsSuccessful()
        {
            // Arrange
            var value = CommonRegex.Url.Value;

            // Act

            // Assert
            Assert.True(value == 1);
            mockRepository.VerifyAll();
        }
    }
}