using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A functionality tests. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class FunctionalityTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a node quantifier test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="quantifierOption"> The quantifier option. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Theory]
        [InlineData(RegexQuantifierOption.Greedy)]
        [InlineData(RegexQuantifierOption.Lazy)]
        [InlineData(RegexQuantifierOption.Possessive)]
        public void AddNodeQuantifierTest(RegexQuantifierOption quantifierOption)
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.Add(@"a", 1, 10, quantifierOption);

            // Assert
            Assert.Equal(
                $@"a{{1,10}}{regexGenerator.RegexLanguageStrategy.Stringifier.ToQuantifierOptionString(quantifierOption)}",
                regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds atomic group test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddAtomicGroupTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddAtomicGroup(@"a");

            // Assert
            Assert.Equal(@"(?>a)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds capturing group test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddCapturingGroupTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddGroup(@"a");

            // Assert
            Assert.Equal(@"(a)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds conditional test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddConditionalTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddConditional(@"a", "b", "c");

            // Assert
            Assert.Equal(@"(?(?=a)b|c)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds named capturing group test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddNamedCapturingGroupTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddGroup(@"a", name: "a");

            // Assert
            Assert.Equal(@"(?<a>a)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds negative lookahead assertion test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddNegativeLookaheadAssertionTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddNegativeLookaheadAssertion(@"a");

            // Assert
            Assert.Equal(@"(?!a)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds negative lookbehind assertion test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddNegativeLookbehindAssertionTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddNegativeLookbehindAssertion(@"a");

            // Assert
            Assert.Equal(@"(?<!a)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds node test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddNodeTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.Add(@"a");

            // Assert
            Assert.Equal(@"a", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds non capturing group test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddNonCapturingGroupTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddGroup(@"a", false);

            // Assert
            Assert.Equal(@"(?:a)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds positive lookahead assertion test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddPositiveLookaheadAssertionTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddPositiveLookaheadAssertion(@"a");

            // Assert
            Assert.Equal(@"(?=a)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds positive lookbehind assertion test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddPositiveLookbehindAssertionTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddPositiveLookbehindAssertion(@"a");

            // Assert
            Assert.Equal(@"(?<=a)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds unicode category by flag test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddUnicodeCategoryByFlagTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddUnicodeCategory(RegexUnicodeCategoryFlag.L);

            // Assert
            Assert.Equal(@"\p{L}", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds unicode category by string test. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void AddUnicodeCategoryByStringTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();

            // Act
            regexGenerator.AddUnicodeCategory("L");

            // Assert
            Assert.Equal(@"\p{L}", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests composite add nested node. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void CompositeAddNestedNodeTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();
            var node1 = new RegexNode(@"[abc]");
            var nested1 = new RegexGroupNode(node1, false, min: 1, max: null,
                quantifierOption: RegexQuantifierOption.Lazy);
            var node2 = new RegexConditionalNode(@"a", "b", "c");
            var nested2 = new RegexGroupNode(node2, true, "a");

            // Act
            regexGenerator.AddNegativeLookbehindAssertion(nested1)
                .AddGroup(nested2);

            // Assert
            Assert.Equal(@"(?<!(?:[abc])+?)(?<a>(?(?=a)b|c))", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests simple add nested node. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void SimpleAddNestedNodeTest()
        {
            // Arrange
            var regexGenerator = new RegexGenerator();
            var node = new RegexNode(@"a");

            // Act
            regexGenerator.AddGroup(node);

            // Assert
            Assert.Equal(@"(a)", regexGenerator.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests 1. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void Test1()
        {
            var regexGenerator = new RegexGenerator(RegexLanguage.DotNet);
            regexGenerator.AddGroup(@"(?<SettlementDiscount>SETTLEMENT/DISCOUNT)", true, "SettlementDiscountGroup",
                null, 1, 5, RegexQuantifierOption.Lazy);
            var result = regexGenerator.ToString();

            Assert.NotNull(result);
        }
    }
}