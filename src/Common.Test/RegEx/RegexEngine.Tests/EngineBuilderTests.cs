using System;
using System.Text.RegularExpressions;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   An engine builder tests. </summary>
    public class EngineBuilderTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Replace when called immediately after initialize should not throw null reference exception.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Theory]
        [InlineData("value")]
        [InlineData("")]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void Replace_WhenCalledImmediatelyAfterInitialize_ShouldNotThrowNullReferenceException(string value)
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var hasThrownNullReferenceEx = false;

            //Act
            try
            {
                engine.Replace(value);
            }
            catch (NullReferenceException)
            {
                hasThrownNullReferenceEx = true;
            }

            //Assert
            Assert.False(hasThrownNullReferenceEx);
        }

        [Theory]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        [InlineData('i')]
        [InlineData('s')]
        public void RemoveModifierTests(char modifier)
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            engine.UseOneLineSearchOption(false);
            var regex = engine.ToRegex();
            Assert.True((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now be present");
            //Act
            engine.UseOneLineSearchOption(true);
            //Assert
            regex = engine.ToRegex();
            Assert.False((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now have been removed");

            engine = engine.RemoveModifier(modifier);
        }

        [Theory]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        [InlineData(true)]
        [InlineData(false)]
        public void WithAnyCaseTests(bool enable)
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            engine.UseOneLineSearchOption(false);
            var regex = engine.ToRegex();
            Assert.True((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now be present");
            //Act
            engine.UseOneLineSearchOption(true);
            //Assert
            regex = engine.ToRegex();
            Assert.False((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now have been removed");

            engine = engine.WithAnyCase(enable);
        }

        [Theory]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        [InlineData(true)]
        [InlineData(false)]
        public void OneLineSearchOptionsTests(bool enable)
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            engine.UseOneLineSearchOption(false);
            var regex = engine.ToRegex();
            Assert.True((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now be present");
            //Act
            engine.UseOneLineSearchOption(true);
            //Assert
            regex = engine.ToRegex();
            Assert.False((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now have been removed");

            engine = engine.UseOneLineSearchOption(enable);
        }

        [Theory]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        [InlineData(RegexOptions.Compiled)]
        [InlineData(RegexOptions.CultureInvariant)]
        [InlineData(RegexOptions.ECMAScript)]
        [InlineData(RegexOptions.ExplicitCapture)]
        [InlineData(RegexOptions.IgnoreCase)]
        [InlineData(RegexOptions.IgnorePatternWhitespace)]
        [InlineData(RegexOptions.Multiline)]
        [InlineData(RegexOptions.None)]
        [InlineData(RegexOptions.RightToLeft)]
        [InlineData(RegexOptions.Singleline)]
        public void WithOptionsTests(RegexOptions options)
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            engine.UseOneLineSearchOption(false);
            var regex = engine.ToRegex();
            Assert.True((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now be present");
            //Act
            engine.UseOneLineSearchOption(true);
            //Assert
            regex = engine.ToRegex();
            Assert.False((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now have been removed");

            engine = engine.WithOptions(options);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Any when value parameter is null or empty should throw argument exception.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void Any_WhenValueParameterIsNullOrEmpty_ShouldThrowArgumentException()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            string value = null;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => engine.Any(value));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Any of when value parameter is null or empty should throw argument exception.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void AnyOf_WhenValueParameterIsNullOrEmpty_ShouldThrowArgumentException()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            string value = null;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => engine.AnyOf(value));
        }

        /// <summary>   Anything start of line anything end of line does match any thing. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void Anything_StartOfLineAnythingEndOfline_DoesMatchAnyThing()
        {
            var engine = EngineBuilder.DefaultExpression
                .StartOfLine()
                .Anything()
                .EndOfLine();

            var isMatch = engine.IsMatch("'!@#$%¨&*()__+{}'");
            Assert.True(isMatch, "should match anything");
        }

        /// <summary>   Line break when called returns expected expression. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void Br_WhenCalled_ReturnsExpectedExpression()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var text = $"testing with {Environment.NewLine} line break";

            //Act
            engine.Br();

            //Assert
            Assert.True(engine.Test(text));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Searches for the first when null parameter value is passed throws argument exception.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void Find_WhenNullParameterValueIsPassed_ThrowsArgumentException()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            string value = null;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => engine.Find(value));
        }

        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void Find_WhenValidParameterValueIsPassed_DoesNotThrowArgumentException()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var value = "JackAndJill";

            //Act
            //Assert
            Assert.NotNull(engine.Find(value));
        }

        /// <summary>   Line break when called returns expected expression. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void LineBreak_WhenCalled_ReturnsExpectedExpression()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var text = $"testing with {Environment.NewLine} line break";

            //Act
            engine.LineBreak();

            //Assert
            Assert.True(engine.Test(text));
        }

        /// <summary>   Tab when called returns expected expression. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void Tab_WhenCalled_ReturnsExpectedExpression()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var text = $"text that contains {@"\t"} a tab";

            //Act
            engine.Tab();

            //Assert
            Assert.True(engine.Test(text));
        }

        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void TestDefaultExpression_Something_Succeeds()
        {
            var engine = EngineBuilder.DefaultExpression.Something();
            Assert.True(engine != null);
            Assert.True(!string.Equals(engine.ToString(), string.Empty));
        }

        /// <summary>   Tests default expression succeeds. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void TestDefaultExpression_Succeeds()
        {
            var engine = EngineBuilder.DefaultExpression;
            Assert.True(engine != null);
            Assert.True(string.Equals(engine.ToString(), string.Empty));
        }

        /// <summary>   Testing if we have a valid URL. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void TestingIfWeHaveAValidURL()
        {
            var engine = EngineBuilder.DefaultExpression
                .StartOfLine()
                .Then("http")
                .Maybe("s")
                .Then("://")
                .Maybe("www.")
                .AnythingBut(" ")
                .EndOfLine();

            var testMe = "https://www.statementiq.com";

            Assert.True(engine.Test(testMe), "The URL is incorrect");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Use one line search option when called should change multi-line modifier.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void UseOneLineSearchOption_WhenCalled_ShouldChangeMultilineModifier()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            engine.UseOneLineSearchOption(false);
            var regex = engine.ToRegex();
            Assert.True((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now be present");
            //Act
            engine.UseOneLineSearchOption(true);
            //Assert
            regex = engine.ToRegex();
            Assert.False((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now have been removed");
        }

        /// <summary>   Word when called returns expected number of words. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Engine Builder Tests")]
        public void Word_WhenCalled_ReturnsExpectedNumberOfWords()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var text = "three words here";
            var expectedCount = 3;

            //Act
            engine.Word();
            var currentExpression = engine.ToRegex();
            var result = currentExpression.Matches(text).Count;

            //Assert
            Assert.Equal(expectedCount, result);
        }
    }
}