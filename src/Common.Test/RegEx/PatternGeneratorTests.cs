using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A pattern generator tests. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class PatternGeneratorTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests combined groups pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void CombinedGroupsPatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"(?:[a-z]+?)(?<odd>13579)(?i:THIS)(\d+)?";

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup("[a-z]+?", false)
                .AddGroup("13579", true, "odd")
                .AddGroup("THIS", false, options: "i") // capturing group option does not matter.
                .AddGroup(@"\d+", true, min: 0, max: 1);

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple atomic group pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleAtomicGroupPatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"(?>(\w)\1+).\b";

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddAtomicGroup(@"(\w)\1+")
                .Add(@".\b");

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Tests simple capturing group with angle bracketed name pattern generation.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleCapturingGroupWithAngleBracketedNamePatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"(?<word>abcde)";
            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup(@"abcde", name: "word");
            // Important!
            //(regexGenerator.RegexLanguageStrategy.Stringifier as DotNetRegexStringifier).NamedCaptureGroupBracketOption = NamedCaptureGroupBracketOption.AngleBracket;

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple capturing group with apostrophed name pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleCapturingGroupWithApostrophedNamePatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"(?'word'abcde)";
            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup(@"abcde", name: "word");
            // Important!
            (regexGenerator.RegexLanguageStrategy.Stringifier as DotNetRegexStringifier).NamedCaptureGroupBracketOption
                = NamedCaptureGroupBracketOption.Apostrophe;

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple capturing group without name pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleCapturingGroupWithoutNamePatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"(abcde)";
            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup(@"abcde");

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
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

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddConditional("yes", "yess", "no");

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple negative lookahead pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleNegativeLookaheadPatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"(?!abc)";

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
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
            var expectedPattern = @"(?<!abc)";

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddNegativeLookbehindAssertion("abc");

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple non capturing group pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleNonCapturingGroupPatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"(?:abcde)";
            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddGroup(@"abcde", false);

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
            var expectedPattern = @"\d";
            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .Add(expectedPattern);

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
            var expectedPattern = @"(?=abc)";

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
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
            var expectedPattern = @"(?<=abc)";

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddPositiveLookbehindAssertion("abc");

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple unicode category by flag pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleUnicodeCategoryByFlagPatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"\p{Lo}+";

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddUnicodeCategory(RegexUnicodeCategoryFlag.Lo, min: 1);

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple unicode category by string pattern generation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleUnicodeCategoryByStringPatternGenerationTest()
        {
            // Arrange
            var expectedPattern = @"\p{Lo}+";

            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                .AddUnicodeCategory("Lo", min: 1);

            // Act
            var actualPattern = regexGenerator.ToString();

            // Assert
            Assert.Equal(expectedPattern, actualPattern);
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
                var expectedPattern = $@"\p{{{flag}}}+";

                var regexGenerator = new RegexGenerator(RegexLanguage.DotNet)
                    .AddUnicodeCategory(flag, min: 1);

                // Act
                var actualPattern = regexGenerator.ToString();

                // Assert
                Assert.Equal(expectedPattern, actualPattern);
            }
        }
    }
}