using System;
using System.Text.RegularExpressions;
using Moq;
using StatementIQ.Common.Test.XUnit;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A RegEx generator tests. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexGeneratorTests
    {
        /// <summary>   The mock repository. </summary>
        private readonly MockRepository mockRepository;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Test.RegEx.RegexGeneratorTests class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGeneratorTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates RegEx generator. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <returns>   The new RegEx generator. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private RegexGenerator CreateRegexGenerator()
        {
            return new RegexGenerator();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets RegEx language state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void SetRegexLanguage_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            const RegexLanguage regexLanguage = default;

            // Act
            var result = regexGenerator.SetRegexLanguage(regexLanguage);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void Add_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            string pattern = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.Add(
                pattern,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds state under test expected behavior 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void Add_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNode innerNode = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.Add(
                innerNode,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds state under test expected behavior 2. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void Add_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNode node = null;

            // Act
            var result = regexGenerator.Add(node);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds group state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddGroup_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            string pattern = null;
            var capturing = false;
            string name = null;
            string options = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddGroup(
                pattern,
                capturing,
                name,
                options,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds group state under test expected behavior 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddGroup_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNode node = null;
            var capturing = false;
            string name = null;
            string options = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddGroup(
                node,
                capturing,
                name,
                options,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds group state under test expected behavior 2. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddGroup_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexGroupNode group = null;

            // Act
            var result = regexGenerator.AddGroup(group);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds atomic group state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddAtomicGroup_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            string pattern = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddAtomicGroup(
                pattern,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds atomic group state under test expected behavior 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddAtomicGroup_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNode innerNode = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddAtomicGroup(
                innerNode,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds atomic group state under test expected behavior 2. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddAtomicGroup_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexAtomicGroupNode atomicGroup = null;

            // Act
            var result = regexGenerator.AddAtomicGroup(atomicGroup);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds positive lookahead assertion state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddPositiveLookaheadAssertion_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            string pattern = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddPositiveLookaheadAssertion(
                pattern,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds positive lookahead assertion state under test expected behavior 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddPositiveLookaheadAssertion_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNode innerNode = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddPositiveLookaheadAssertion(
                innerNode,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds positive lookahead assertion state under test expected behavior 2. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddPositiveLookaheadAssertion_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexPositiveLookaheadAssertionNode positiveLookaheadAssertion = null;

            // Act
            var result = regexGenerator.AddPositiveLookaheadAssertion(positiveLookaheadAssertion);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds negative lookahead assertion state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddNegativeLookaheadAssertion_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            string pattern = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddNegativeLookaheadAssertion(
                pattern,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds negative lookahead assertion state under test expected behavior 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddNegativeLookaheadAssertion_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNode innerNode = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddNegativeLookaheadAssertion(
                innerNode,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds negative lookahead assertion state under test expected behavior 2. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddNegativeLookaheadAssertion_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNegativeLookaheadAssertionNode negativeLookaheadAssertion = null;

            // Act
            var result = regexGenerator.AddNegativeLookaheadAssertion(negativeLookaheadAssertion);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds positive lookbehind assertion state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddPositiveLookbehindAssertion_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            string pattern = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddPositiveLookbehindAssertion(
                pattern,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds positive lookbehind assertion state under test expected behavior 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddPositiveLookbehindAssertion_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNode innerNode = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddPositiveLookbehindAssertion(
                innerNode,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds positive lookbehind assertion state under test expected behavior 2. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddPositiveLookbehindAssertion_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexPositiveLookbehindAssertionNode positiveLookbehindAssertion = null;

            // Act
            var result = regexGenerator.AddPositiveLookbehindAssertion(positiveLookbehindAssertion);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds negative lookbehind assertion state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddNegativeLookbehindAssertion_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            string pattern = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddNegativeLookbehindAssertion(
                pattern,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds negative lookbehind assertion state under test expected behavior 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddNegativeLookbehindAssertion_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNode innerNode = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddNegativeLookbehindAssertion(
                innerNode,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds negative lookbehind assertion state under test expected behavior 2. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddNegativeLookbehindAssertion_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNegativeLookbehindAssertionNode negativeLookbehindAssertion = null;

            // Act
            var result = regexGenerator.AddNegativeLookbehindAssertion(negativeLookbehindAssertion);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds conditional state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddConditional_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            string pattern = null;
            string trueValue = null;
            string falseValue = null;
            string nameOrNumber = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddConditional(
                pattern,
                trueValue,
                falseValue,
                nameOrNumber,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds conditional state under test expected behavior 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddConditional_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexNode innerNode = null;
            string trueValue = null;
            string falseValue = null;
            string nameOrNumber = null;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddConditional(
                innerNode,
                trueValue,
                falseValue,
                nameOrNumber,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds conditional state under test expected behavior 2. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddConditional_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexConditionalNode conditional = null;

            // Act
            var result = regexGenerator.AddConditional(conditional);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds unicode category state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddUnicodeCategory_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexUnicodeCategoryFlag unicodeCategory = default;
            var negative = false;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddUnicodeCategory(
                unicodeCategory,
                negative,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds unicode category state under test expected behavior 1. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddUnicodeCategory_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            string unicodeDesignation = null;
            var negative = false;
            int? min = null;
            int? max = null;

            // Act
            var result = regexGenerator.AddUnicodeCategory(
                unicodeDesignation,
                negative,
                min,
                max);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds unicode category state under test expected behavior 2. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void AddUnicodeCategory_StateUnderTest_ExpectedBehavior2()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexUnicodeCategoryNode unicodeCategory = null;

            // Act
            var result = regexGenerator.AddUnicodeCategory(unicodeCategory);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Clears the state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void Clear_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();

            // Act
            var result = regexGenerator.Clear();

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
            var regexGenerator = CreateRegexGenerator();

            // Act
            var result = regexGenerator.ToString();

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates state under test expected behavior. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SkippableFact]
        [Trait("TDD", "RegEx Tests")]
        public void Create_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var regexGenerator = CreateRegexGenerator();
            RegexOptions? options = null;
            TimeSpan? matchTimeout = null;

            // Act
            var result = regexGenerator.Create(options, matchTimeout);

            // Assert
            Assert.True(false);
            mockRepository.VerifyAll();
        }
    }
}