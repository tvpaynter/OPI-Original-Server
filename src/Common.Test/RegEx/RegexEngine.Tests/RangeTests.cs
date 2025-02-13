using System;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A range tests. </summary>
    public class RangeTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Range when array contains null parameter iterator is ignored and removed from list.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Range Tests")]
        public void Range_WhenArrayContainsNullParameter_ItIsIgnoredAndRemovedFromList()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            object[] inversedOrderArray = {1, null, null, 7};
            engine.Range(inversedOrderArray);
            var lookupString = "testing 5 testing";

            //Act
            var isMatch = engine.IsMatch(lookupString);

            //Assert
            Assert.True(isMatch);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Range when array parameter has only one value should throw argument out of range
        ///     exception.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Range Tests")]
        public void Range_WhenArrayParameterHasOnlyOneValue_ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            object[] value = {0};

            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => engine.Range(value));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Range when array parameter has values in reverse order returns correct result for correct
        ///     order.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Range Tests")]
        public void Range_WhenArrayParameterHasValuesInReverseOrder_ReturnsCorrectResultForCorrectOrder()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            object[] inversedOrderArray = {9, 2};
            engine.Range(inversedOrderArray);
            var lookupString = "testing 8 another test";

            //Act
            var isMatch = engine.IsMatch(lookupString);

            //Assert
            Assert.True(isMatch);
        }

        /// <summary>   Range when null parameter passed should throw argument null exception. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Range Tests")]
        public void Range_WhenNullParameterPassed_ShouldThrowArgumentNullException()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            object[] value = null;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => engine.Range(value));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Range when odd number of items in array should append last element with or clause.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Range Tests")]
        public void Range_WhenOddNumberOfItemsInArray_ShouldAppendLastElementWithOrClause()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var text = "abcd7sdadqascdaswde";
            object[] range = {1, 6, 7};

            //Act
            engine.Range(range);

            //Assert
            Assert.True(engine.IsMatch(text));
        }

        /// <summary>   Range when odd number of items in array should append with pipe. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Range Tests")]
        public void Range_WhenOddNumberOfItemsInArray_ShouldAppendWithPipe()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            object[] range = {1, 6, 7};
            var expectedExpression = "[1-6]|7";

            //Act
            engine.Range(range);

            //Assert
            Assert.Equal(expectedExpression, engine.ToString());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Range when too many items in array should throw argument out of range exception.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Range Tests")]
        public void Range_WhenTooManyItemsInArray_ShouldThrowArgumentOutOfRangeException()
        {
            var engine = EngineBuilder.DefaultExpression;
            object[] range = {1, 6, 7, 12};

            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => engine.Range(range));
        }
    }
}