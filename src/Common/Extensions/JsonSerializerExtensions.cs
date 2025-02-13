using System;
using System.IO;
using MandateThat;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace StatementIQ.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static byte[] SerializeToBson<TValue>(this JsonSerializer serializer, TValue value)
            where TValue : class
        {
            Mandate.That(serializer, nameof(serializer)).IsNotNull();

            if (value == null) return new byte[0];

            using var memoryStream = new MemoryStream();
            var writer = new BsonDataWriter(memoryStream);

            serializer.Serialize(writer, value);

            var objectDataAsStream = memoryStream.ToArray();

            return objectDataAsStream;
        }

        public static TValue DeserializeFromBson<TValue>(this JsonSerializer serializer, byte[] stream,
            DateTimeKind dateTimeKindHandling = DateTimeKind.Utc) where TValue : class
        {
            Mandate.That(serializer, nameof(serializer)).IsNotNull();

            if (stream == null) return null;

            using var memoryStream = new MemoryStream(stream);

            var reader = new BsonDataReader(memoryStream)
            {
                DateTimeKindHandling = dateTimeKindHandling
            };

            var result = serializer.Deserialize<TValue>(reader);

            return result;
        }
    }
}