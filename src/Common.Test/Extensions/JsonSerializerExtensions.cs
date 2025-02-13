using System;
using Newtonsoft.Json;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class JsonSerializerExtensions
    {
        [Fact]
        public void Should_Throw_ArgumentNullException_When_SerializeToBson_With_Null_Serializer()
        {
            Assert.Throws<ArgumentNullException>(() => default(JsonSerializer).SerializeToBson(new object()));
        }

        [Fact]
        public void Should_Return_Null_When_SerializeToBson_With_Null_Value()
        {
            var serializer = new JsonSerializer();
            var result = serializer.SerializeToBson<object>(null);

            Assert.Empty(result);
        }

        [Fact]
        public void Should_SerializeToBson_With_Valid_Values()
        {
            var target = new
            {
                FirstName = "John",
                Lastname = "Doe"
            };

            var serializer = new JsonSerializer();
            var result = serializer.SerializeToBson(target);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Throw_ArgumentNullException_When_DeserializeFromBson_With_Null_Serializer()
        {
            Assert.Throws<ArgumentNullException>(() => default(JsonSerializer).DeserializeFromBson<object>(new byte[1]));
        }

        [Fact]
        public void Should_Return_Null_When_DeserializeFromBson_With_Null_Value()
        {
            var serializer = new JsonSerializer();
            var result = serializer.DeserializeFromBson<object>(null);

            Assert.Null(result);
        }

        [Fact]
        public void Should_DeserializeFromBson_With_Valid_Values()
        {
            var target = new
            {
                FirstName = "John",
                Lastname = "Doe"
            };

            var serializer = new JsonSerializer();
            var bson = serializer.SerializeToBson(target);
            var result = serializer.DeserializeFromBson<dynamic>(bson);

            Assert.NotNull(result);
        }
    }
}