using System;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A multiple tests. </summary>
    public class MultipleTests
    {
        /// <summary>   Multiple when null argument passed throws argument null exception. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Multiple Tests")]
        public void Multiple_WhenNullArgumentPassed_ThrowsArgumentNullException()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var argument = string.Empty;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => engine.Multiple(argument));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Multiple when null or empty value parameter is passed should throw argument exception.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Multiple Tests")]
        public void Multiple_WhenNullOrEmptyValueParameterIsPassed_ShouldThrowArgumentException()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            string value = null;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => engine.Multiple(value));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Multiple when parameter is given should match one or multiple values given.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Multiple Tests")]
        public void Multiple_WhenParamIsGiven_ShouldMatchOneOrMultipleValuesGiven()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var text = "testesting 123 yahoahoahou another test";
            var expectedExpression = "y(aho)+u";

            //Act
            engine.Add("y")
                .Multiple("aho")
                .Add("u");

            //Assert
            Assert.True(engine.Test(text));
            Assert.Equal(expectedExpression, engine.ToString());
        }
    }
}