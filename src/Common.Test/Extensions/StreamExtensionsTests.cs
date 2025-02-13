using System;
using System.IO;
using System.Text;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class StreamExtensionsTests
    {
        [Serializable]
        public class Person
        {
            public string Name { get; set; }
        }

        [Fact]
        public void Should_Return_Deserialize_With_Valid_Values()
        {
            var json = "{ Name: 'John Doe' }";

            using var stream = new MemoryStream(Encoding.ASCII.GetBytes(json));
            var result = stream.Deserialize<Person>();

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_Deserialize_With_Null_Stream()
        {
            Assert.Throws<ArgumentNullException>(() => default(Stream).Deserialize<object>());
        }
    }
}