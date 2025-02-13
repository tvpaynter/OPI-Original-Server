using System;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   An add modifier tests. </summary>
    public class AddModifierTests
    {
        /// <summary>   Adds modifier add modifier i removes case. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Add Modifier Tests")]
        public void AddModifier_AddModifierI_RemovesCase()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add("teststring").AddModifier('i');

            Assert.True(engine.IsMatch("TESTSTRING"));
        }

        /// <summary>   Adds modifier add modifier m multiline. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Add Modifier Tests")]
        public void AddModifier_AddModifierM_Multiline()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var text = $"testing with {Environment.NewLine} line break";

            //Act
            engine.AddModifier('m');

            //Assert
            Assert.True(engine.Test(text));
        }

        /// <summary>   Adds modifier add modifier s single line. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Add Modifier Tests")]
        public void AddModifier_AddModifierS_SingleLine()
        {
            //Arrange
            var engine = EngineBuilder.DefaultExpression;
            var testString = "First string" + Environment.NewLine + "Second string";

            //Act
            engine.Add("First string").Anything().Then("Second string");

            //Assert
            Assert.False(
                engine.IsMatch(testString),
                "The dot matches a single character, except line break characters.");

            engine.AddModifier('s');
            Assert.True(
                engine.IsMatch(testString),
                "The dot matches a single character and line break characters.");
        }

        /// <summary>   Adds modifier add modifier x coordinate ignore whitspace. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Add Modifier Tests")]
        public void AddModifier_AddModifierX_IgnoreWhitspace()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.Add("test string").AddModifier('x');

            Assert.True(engine.IsMatch("test string #comment"));
        }
    }
}