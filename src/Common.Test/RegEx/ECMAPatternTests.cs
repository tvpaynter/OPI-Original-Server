using StatementIQ.RegEx;
using StatementIQ.RegEx.Exceptions;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An ecma pattern tests. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class ECMAPatternTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple atomic group pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleAtomicGroupPatternGenerationTest()
        {
            // Arrange
            var pattern = @"(?>(\w)\1+).\b";
            var expectedPattern = $"/{pattern}/";

            Assert.Throws<AtomicGroupNotSupportedException>(() =>
            {
                var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                    .AddAtomicGroup(@"(\w)\1+")
                    .Add(@".\b");

                // Act
                var actualPattern = regexGenerator.ToString();

                // Assert
                Assert.Equal(expectedPattern, actualPattern);
            });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple conditional pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleConditionalPatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"(?(?=yes)yess|no)";

            Assert.Throws<ConditionalNotSupportedException>(() =>
            {
                var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                    .AddConditional("yes", "yess", "no");

                // Act
                var actualPattern = regexGenerator.ToString();

                // Assert
                Assert.Equal(expectedPattern, actualPattern);
            });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple negative lookahead pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleNegativeLookaheadPatternGenerationTest()
        {
            // Arrange
            var pattern = @"(?!abc)";
            var expectedPattern = $"/{pattern}/";

            var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                .AddNegativeLookaheadAssertion("abc");

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple negative lookbehind pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleNegativeLookbehindPatternGenerationTest()
        {
            // Arrange
            var pattern = @"(?<!abc)";
            var expectedPattern = $"/{pattern}/";

            Assert.Throws<NegativeLookbehindAssertionNotSupportedException>(() =>
            {
                var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                    .AddNegativeLookbehindAssertion("abc");

                // Act
                var actualPattern = regexGenerator.ToString();

                // Assert
                Assert.Equal(expectedPattern, actualPattern);
            });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple non capturing group pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleNonCapturingGroupPatternGenerationTest()
        {
            // Arrange
            var pattern = @"(?:abcde)";
            var expectedPattern = $"/{pattern}/";
            var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                .AddGroup("abcde", false);

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimplePatternGenerationTest()
        {
            // Arrange
            var pattern = @"\d";
            var expectedPattern = $"/{pattern}/";
            var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                .Add(pattern);

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple positive lookahead pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimplePositiveLookaheadPatternGenerationTest()
        {
            // Arrange
            var pattern = @"(?=abc)";
            var expectedPattern = $"/{pattern}/";

            var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                .AddPositiveLookaheadAssertion("abc");

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple positive lookbehind pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimplePositiveLookbehindPatternGenerationTest()
        {
            // Arrange
            var pattern = @"(?<=abc)";
            var expectedPattern = $"/{pattern}/";

            Assert.Throws<PositiveLookbehindAssertionNotSupportedException>(() =>
            {
                var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                    .AddPositiveLookbehindAssertion("abc");

                // Act
                var actualPattern = regexGenerator.ToString();

                // Assert
                Assert.Equal(expectedPattern, actualPattern);
            });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple unicode category by flag pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleUnicodeCategoryByFlagPatternGenerationTest()
        {
            // Arrange
            var pattern = @"\p{Lo}+";
            var expectedPattern = $"/{pattern}/";

            Assert.Throws<UnicodeCategoryNotSupportedException>(() =>
            {
                var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                    .AddUnicodeCategory(RegexUnicodeCategoryFlag.Lo, min: 1);

                // Act
                var actualPattern = regexGenerator.ToString();

                // Assert
                Assert.Equal(expectedPattern, actualPattern);
            });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple unicode category by string pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleUnicodeCategoryByStringPatternGenerationTest()
        {
            // Arrange
            var pattern = @"\p{Lo}+";
            var expectedPattern = $"/{pattern}/";

            Assert.Throws<UnicodeCategoryNotSupportedException>(() =>
            {
                var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                    .AddUnicodeCategory("Lo", min: 1);

                // Act
                var actualPattern = regexGenerator.ToString();

                // Assert
                Assert.Equal(expectedPattern, actualPattern);
            });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests unicode category by flag pattern generations. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void UnicodeCategoryByFlagPatternGenerationsTest()
        {
            for (var flag = RegexUnicodeCategoryFlag.Lo;
                flag < RegexUnicodeCategoryFlag.Z;
                flag = (RegexUnicodeCategoryFlag) ((int) flag << 1))
            {
                // Arrange
                var pattern = $@"\p{{{flag}}}+";
                var expectedPattern = $"/{pattern}/";

                Assert.Throws<UnicodeCategoryNotSupportedException>(() =>
                {
                    var regexGenerator = new RegexGenerator(RegexLanguage.ECMAScript)
                        .AddUnicodeCategory(flag, min: 1);

                    // Act
                    var actualPattern = regexGenerator.ToString();

                    // Assert
                    Assert.Equal(expectedPattern, actualPattern);
                });
            }
        }
    }
}