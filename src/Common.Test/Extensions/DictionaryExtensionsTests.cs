using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void Should_Convert_Dictionary_To_ReadOnly()
        {
            var target = new Dictionary<string, string>
            {
                {"key", "value"},
                {"newkey", "newvalue"}
            };

            var result = DictionaryExtensions.AsReadOnly(target);

            Assert.True(result.GetType() == typeof(ReadOnlyDictionary<string, string>));
            Assert.Equal(target.Count, result.Count);
        }

        [Fact]
        public void Should_Return_MergeLeft()
        {
            var target = new Dictionary<string, string>
            {
                {"key", "value"},
                {"newkey", "newvalue"}
            };

            var result = target.MergeLeft(new Dictionary<string, string>
            {
                {"key2", "value2"}
            });

            Assert.NotEqual(target.Count, result.Count);
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_AsReadOnly_With_Null_Dictionary()
        {
            var target = default(Dictionary<string, string>);

            Assert.Throws<ArgumentNullException>(() => DictionaryExtensions.AsReadOnly(target));
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_MergeLeft_With_Null_Dictionary()
        {
            var target = default(Dictionary<string, string>);

            Assert.Throws<ArgumentNullException>(() => target.MergeLeft(default(Dictionary<string, string>)));
        }
    }
}