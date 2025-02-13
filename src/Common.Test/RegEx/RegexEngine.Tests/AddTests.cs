using System;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   An add tests. </summary>
    public class AddTests
    {
        /// <summary>   Adds dot com does not match google com without dot. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Add Tests")]
        public void Add_AddDotCom_DoesNotMatchGoogleComWithoutDot()
        {
            //Arrange
            var verbEx = EngineBuilder.DefaultExpression;
            verbEx.Add(".com");

            //Act
            var isMatch = verbEx.IsMatch("http://www.googlecom/");

            //Assert
            Assert.False(isMatch, "Should not match 'ecom'");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Adds when null string passed as parameter should throw null argument exception.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Add Tests")]
        public void Add_WhenNullStringPassedAsParameter_ShouldThrowNullArgumentException()
        {
            //Arrange
            var verbEx = EngineBuilder.DefaultExpression;
            string value = null;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => verbEx.Add(value));
        }
    }
}