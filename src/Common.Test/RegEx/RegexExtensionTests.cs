using System;
using System.Text;
using StatementIQ.RegEx;
using Xunit;

namespace StatementIQ.Common.Test.RegEx
{
    public class ExtensionMethodTests
    {
        [Fact]
        [Trait("Regex Tests", "Extension Tests")]
        public void TestReplaceMany1()
        {
            var stringBuilder = new StringBuilder("abc");
            stringBuilder.ReplaceMany(new[] {"a", "c"}, new[] {"x", "cz"});
            Assert.Equal("xbcz", stringBuilder.ToString());
        }

        [Fact]
        [Trait("Regex Tests", "Extension Tests")]
        public void TestReplaceMany2()
        {
            var stringBuilder = new StringBuilder("abc");
            stringBuilder.ReplaceMany(new[] {"x", "y"}, new[] {"X", "Y"});
            Assert.Equal("abc", stringBuilder.ToString());
        }

        [Fact]
        [Trait("Regex Tests", "Extension Tests")]
        public void TestReplaceManyWithDifferentArgumentLength()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new StringBuilder("abc").ReplaceMany(new[] {"a", "b"}, new[] {"x"}));
        }

        [Fact]
        [Trait("Regex Tests", "Extension Tests")]
        public void TestReplaceManyWithNullArguments1()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new StringBuilder("abc").ReplaceMany(null, null));
        }

        [Fact]
        [Trait("Regex Tests", "Extension Tests")]
        public void TestReplaceManyWithNullArguments2()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new StringBuilder("abc").ReplaceMany(new[] {"a", "b"}, null));
        }

        [Fact]
        [Trait("Regex Tests", "Extension Tests")]
        public void TestReplaceManyWithNullArguments3()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new StringBuilder("abc").ReplaceMany(null, new[] {"a", "b"}));
        }

        [Fact]
        [Trait("Regex Tests", "Extension Tests")]
        public void TestReplaceManyWithNullExtensionObject()
        {
            StringBuilder builder = null;

            // ReSharper disable ExpressionIsAlwaysNull
            var ex = Assert.Throws<ArgumentNullException>(() => builder.ReplaceMany(new[] {"a"}, new[] {"b"}));
            // ReSharper restore ExpressionIsAlwaysNull
        }
    }
}