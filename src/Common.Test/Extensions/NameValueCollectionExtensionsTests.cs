using System.Collections.Specialized;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class NameValueCollectionExtensionsTests
    {
        [Fact]
        public void Should_Convert_NameValueCollection_To_Dictionary()
        {
            var target = new NameValueCollection();
            target.Add("key", "value");
            target.Add("newkey", "newvalue");

            var result = target.ToDictionary();

            Assert.Equal(target.Count, result.Count);
        }
    }
}