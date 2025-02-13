using System;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    /// <summary>
    ///     Summary description for UnitTest1
    /// </summary>
    public class RegexQuantifierTests
    {
        [Theory]
        [Trait("RegEx Tests", "Quantifier Tests")]
        [InlineData(0, 1, true)]
        [InlineData(0, null, true)]
        [InlineData(1, null, true)]
        [InlineData(0, 0, true)]
        [InlineData(7, null, true)]
        [InlineData(0, 1, false)]
        [InlineData(0, null, false)]
        [InlineData(1, null, false)]
        [InlineData(0, 0, false)]
        [InlineData(7, null, false)]
        public void ToRegExPatternTests(int? min, int? max, bool lazy)
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.Custom(min, max, lazy);
            var pattern = greedyQuantifier.ToRegexPattern();
            Assert.NotNull(pattern);
            if (lazy) Assert.EndsWith("?", pattern, StringComparison.Ordinal);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestAtLeastMethodShouldReturnProperObject()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.AtLeast(5);
            Assert.Equal(5, greedyQuantifier.MinOccurrenceCount);
            Assert.Null(greedyQuantifier.MaxOccurrenceCount);
            Assert.False(greedyQuantifier.IsLazy);

            var lazyQuantifier1 = new RegexQuantifier();
            lazyQuantifier1 = lazyQuantifier1.AtLeast(5, false);
            Assert.Equal(5, lazyQuantifier1.MinOccurrenceCount);
            Assert.Null(lazyQuantifier1.MaxOccurrenceCount);
            Assert.False(lazyQuantifier1.IsLazy);

            var lazyQuantifier2 = new RegexQuantifier();
            lazyQuantifier2 = lazyQuantifier2.AtLeast(5, true);
            Assert.Equal(5, lazyQuantifier2.MinOccurrenceCount);
            Assert.Null(lazyQuantifier2.MaxOccurrenceCount);
            Assert.True(lazyQuantifier2.IsLazy);

            Assert.NotSame(greedyQuantifier, lazyQuantifier1);
            Assert.NotSame(greedyQuantifier, lazyQuantifier2);
            Assert.NotSame(lazyQuantifier1, lazyQuantifier2);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestCustomMethodShouldReturnProperObject()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.Custom(1, 9, false);
            Assert.Equal(1, greedyQuantifier.MinOccurrenceCount);
            Assert.Equal(9, greedyQuantifier.MaxOccurrenceCount);
            Assert.False(greedyQuantifier.IsLazy);

            var lazyQuantifier1 = new RegexQuantifier();
            lazyQuantifier1 = lazyQuantifier1.Custom(4, 3, true);
            Assert.Equal(4, lazyQuantifier1.MinOccurrenceCount);
            Assert.Equal(3, lazyQuantifier1.MaxOccurrenceCount);
            Assert.True(lazyQuantifier1.IsLazy);

            var lazyQuantifier2 = new RegexQuantifier();
            lazyQuantifier2 = lazyQuantifier2.Custom(2, null, true);
            Assert.Equal(2, lazyQuantifier2.MinOccurrenceCount);
            Assert.Null(lazyQuantifier2.MaxOccurrenceCount);
            Assert.True(lazyQuantifier2.IsLazy);

            Assert.NotSame(greedyQuantifier, lazyQuantifier1);
            Assert.NotSame(greedyQuantifier, lazyQuantifier2);
            Assert.NotSame(lazyQuantifier1, lazyQuantifier2);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestExactlyMethodShouldReturnProperObject()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.Exactly(5);
            Assert.Equal(5, greedyQuantifier.MinOccurrenceCount);
            Assert.Equal(5, greedyQuantifier.MaxOccurrenceCount);
            Assert.False(greedyQuantifier.IsLazy);

            var lazyQuantifier1 = new RegexQuantifier();
            lazyQuantifier1 = lazyQuantifier1.Exactly(3, true);
            Assert.Equal(3, lazyQuantifier1.MinOccurrenceCount);
            Assert.Equal(3, lazyQuantifier1.MaxOccurrenceCount);
            Assert.True(lazyQuantifier1.IsLazy);

            var lazyQuantifier2 = new RegexQuantifier();
            lazyQuantifier2 = lazyQuantifier2.Exactly(2, false);
            Assert.Equal(2, lazyQuantifier2.MinOccurrenceCount);
            Assert.Equal(2, lazyQuantifier2.MaxOccurrenceCount);
            Assert.False(lazyQuantifier2.IsLazy);

            Assert.NotSame(greedyQuantifier, lazyQuantifier1);
            Assert.NotSame(greedyQuantifier, lazyQuantifier2);
            Assert.NotSame(lazyQuantifier1, lazyQuantifier2);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestMinMaxOccurrenceCountPropertiesShouldNotAcceptNegativeInts1()
        {
            var greedyQuantifier = new RegexQuantifier();
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => greedyQuantifier.Custom(-1, 5, true));
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestMinMaxOccurrenceCountPropertiesShouldNotAcceptNegativeInts2()
        {
            var greedyQuantifier = new RegexQuantifier();
            var ex = Assert.Throws<ArgumentException>(() => greedyQuantifier.Custom(0, -2, true));
        }


        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestMinMaxOccurrenceCountPropertiesShouldNotAcceptNegativeInts3()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.Custom(1, 2, true);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => greedyQuantifier.MinOccurrenceCount = -1);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestMinMaxOccurrenceCountPropertiesShouldNotAcceptNegativeInts4()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.Custom(1, 2, true);
            var ex = Assert.Throws<ArgumentException>(() => greedyQuantifier.MaxOccurrenceCount = -1);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestMinMaxOccurrenceCountPropertiesZeroOrMoreShouldNotAcceptNegativeInts2()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.ZeroOrMore;
            Assert.Equal(0, greedyQuantifier.MinOccurrenceCount);
            Assert.Null(greedyQuantifier.MaxOccurrenceCount);
            Assert.False(greedyQuantifier.IsLazy);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestMinOccurrenceCountPropertyShouldNotAcceptNulls1()
        {
            var greedyQuantifier = new RegexQuantifier();
            var ex = Assert.Throws<ArgumentException>(() => greedyQuantifier.Custom(null, 2, true));
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestMinOccurrenceCountPropertyShouldNotAcceptNulls2()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.Custom(1, 2, true);
            var ex = Assert.Throws<ArgumentException>(() => greedyQuantifier.MinOccurrenceCount = null);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestNonePropertyShouldReturnNull()
        {
            var quantifier = new RegexQuantifier();
            quantifier = quantifier.None;
            Assert.Null(quantifier);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestOneOrMorePropertyShouldReturnProperObject()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.OneOrMore;
            Assert.Equal(1, greedyQuantifier.MinOccurrenceCount);
            Assert.Null(greedyQuantifier.MaxOccurrenceCount);
            Assert.False(greedyQuantifier.IsLazy);

            var lazyQuantifier = new RegexQuantifier();
            lazyQuantifier = lazyQuantifier.OneOrMoreLazy;
            Assert.Equal(1, lazyQuantifier.MinOccurrenceCount);
            Assert.Null(lazyQuantifier.MaxOccurrenceCount);
            Assert.True(lazyQuantifier.IsLazy);

            Assert.NotEqual(greedyQuantifier, lazyQuantifier);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestZeroOrMorePropertyShouldReturnProperObject()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.ZeroOrOne;
            Assert.Equal(0, greedyQuantifier.MinOccurrenceCount);
            Assert.NotNull(greedyQuantifier.MaxOccurrenceCount);
            Assert.False(greedyQuantifier.IsLazy);

            var lazyQuantifier = new RegexQuantifier();
            lazyQuantifier = lazyQuantifier.ZeroOrMoreLazy;
            Assert.Equal(0, lazyQuantifier.MinOccurrenceCount);
            Assert.Null(lazyQuantifier.MaxOccurrenceCount);
            Assert.True(lazyQuantifier.IsLazy);
            Assert.NotEqual(greedyQuantifier, lazyQuantifier);
        }

        [Fact]
        [Trait("RegEx Tests", "Quantifier Tests")]
        public void TestZeroOrOnePropertyShouldReturnProperObject()
        {
            var greedyQuantifier = new RegexQuantifier();
            greedyQuantifier = greedyQuantifier.ZeroOrOne;
            Assert.Equal(0, greedyQuantifier.MinOccurrenceCount);
            Assert.Equal(1, greedyQuantifier.MaxOccurrenceCount);
            Assert.False(greedyQuantifier.IsLazy);

            var lazyQuantifier = new RegexQuantifier();
            lazyQuantifier = lazyQuantifier.ZeroOrOneLazy;
            Assert.Equal(0, lazyQuantifier.MinOccurrenceCount);
            Assert.Equal(1, lazyQuantifier.MaxOccurrenceCount);
            Assert.True(lazyQuantifier.IsLazy);
            Assert.NotEqual(greedyQuantifier, lazyQuantifier);
        }
    }
}