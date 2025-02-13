using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A capture tests. </summary>
    public class CaptureTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Begins capture and end capture add com or organization RegEx is as expected.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        [Trait("RegExEngine Tests", "Capture Tests")]
        public void BeginCaptureAndEndCapture_AddComOrOrg_RegexIsAsExpected()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression;

            // Act
            engine.BeginCapture()
                .Add("com")
                .Or("org")
                .EndCapture();

            // Assert
            Assert.Equal("((com)|(org))", engine.ToString());
        }

        /// <summary>   Begins capture and end capture duplicates identifier does match. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Capture Tests")]
        public void BeginCaptureAndEndCapture_DuplicatesIdentifier_DoesMatch()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression;
            const string TEST_STRING = "He said that that was the the correct answer.";

            // Act
            engine.BeginCapture()
                .Word()
                .EndCapture()
                .Add(@"\s", false)
                .BeginCapture()
                .Add(@"\1", false)
                .EndCapture();

            // Assert
            Assert.Equal(@"(\w+)\s(\1)", engine.ToString());
            Assert.True(engine.Test(TEST_STRING), "There is no duplicates in the textString.");
        }

        /// <summary>   Begins capture with name create RegEx group name as expected. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Capture Tests")]
        public void BeginCaptureWithName_CreateRegexGroupNameAsExpected()
        {
            // Arrange
            var engine = EngineBuilder.DefaultExpression;

            // Act
            engine.Add("COD")
                .BeginCapture("GroupNumber")
                .Any("0-9")
                .RepeatPrevious(3)
                .EndCapture()
                .Add("END");

            // Assert
            Assert.Equal(@"COD(?<GroupNumber>[0-9]{3})END", engine.ToString());
            Assert.Equal("123", engine.Capture("COD123END", "GroupNumber"));

            Assert.NotEqual("123", engine.Capture(string.Empty, "GroupNumber"));
        }
    }
}