using System.Text.Json.Serialization;

namespace Utg.Api.Models
{
    public class Features
    {
        [JsonPropertyName("count")] public long Count { get; set; }

        [JsonPropertyName("results")] public object[] Results { get; set; }
    }
}