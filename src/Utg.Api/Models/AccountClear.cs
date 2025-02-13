using System.Text.Json.Serialization;


namespace Utg.Api.Models
{
    public class AccountClear : Account
    {
        [JsonPropertyName("billTo")] public Contact BillTo { get; set; }

        /// <example>4012000098765439</example>
        [JsonPropertyName("pan")]
        public string Pan { get; set; }

        /// <example>1220</example>
        [JsonPropertyName("expirationDate")]
        public string ExpirationDate { get; set; }

        /// <example>999</example>
        [JsonPropertyName("cvv")]
        public string Cvv { get; set; }

        [JsonPropertyName("trackData")] public string TrackData { get; set; }

        [JsonPropertyName("pinBlock")] public string PinBlock { get; set; }

        [JsonPropertyName("pinPadSerialNum")] public string PinPadSerialNum { get; set; }

        [JsonPropertyName("cryptogram")] public byte[] Cryptogram { get; set; }

        [JsonPropertyName("PAR")] public string PAR { get; set; }

        [JsonPropertyName("version")] public string Version { get; set; }

        [JsonPropertyName("token")] public string Token { get; set; }

        [JsonPropertyName("batchId")] public long BatchId { get; set; }
    }
}