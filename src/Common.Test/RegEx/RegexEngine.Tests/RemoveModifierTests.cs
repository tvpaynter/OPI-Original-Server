using System.Text.RegularExpressions;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   Remove modifier tests. </summary>
    public class RemoveModifierTests
    {
        /// <summary>   Removes the modifier remove modifier i removes case. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Remove modifier Tests")]
        public void RemoveModifier_RemoveModifierI_RemovesCase()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.AddModifier('i');

            engine.RemoveModifier('i');
            var regex = engine.ToRegex();
            Assert.False((regex.Options & RegexOptions.IgnoreCase) != 0, "RegexOptions should now have been removed");
        }

        /// <summary>   Removes the modifier remove modifier m removes multi-line as default. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Remove modifier Tests")]
        public void RemoveModifier_RemoveModifierM_RemovesMultilineAsDefault()
        {
            var engine = EngineBuilder.DefaultExpression;
            var regex = engine.ToRegex();
            Assert.True((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should have MultiLine as default");

            engine.RemoveModifier('m');
            regex = engine.ToRegex();

            Assert.False((regex.Options & RegexOptions.Multiline) != 0, "RegexOptions should now have been removed");
        }

        /// <summary>   Removes the modifier remove modifier x coordinate removes case. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Remove modifier Tests")]
        public void RemoveModifier_RemoveModifierX_RemovesCase()
        {
            var engine = EngineBuilder.DefaultExpression;
            engine.AddModifier('x');

            engine.RemoveModifier('x');
            var regex = engine.ToRegex();
            Assert.False((regex.Options & RegexOptions.IgnorePatternWhitespace) != 0,
                "RegexOptions should now have been removed");
        }
    }
}