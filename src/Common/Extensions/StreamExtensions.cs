using System.IO;
using MandateThat;
using Newtonsoft.Json;

namespace StatementIQ.Extensions
{
    public static class StreamExtensions
    {
        private static readonly JsonSerializer JsonSerializer = new JsonSerializer
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        public static T Deserialize<T>(this Stream stream)
        {
            Mandate.That(stream, nameof(stream)).IsNotNull();

            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);

            return JsonSerializer.Deserialize<T>(jsonTextReader);
        }
    }
}