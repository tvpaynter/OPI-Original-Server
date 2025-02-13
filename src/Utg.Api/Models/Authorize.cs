
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Utg.Api.Models
{

        public class Authorize : Transaction
        {
            [JsonPropertyName("manual")] public bool Manual { get; set; }

            [JsonPropertyName("geoCode")] public GeoCode GeoCode { get; set; }

            [JsonPropertyName("poNumber")] public string PoNumber { get; set; }

            [JsonPropertyName("shipTo")] public Contact ShipTo { get; set; }

            [Required]
            [JsonPropertyName("account")]
            public AccountClear Account { get; set; }

            [JsonPropertyName("batchName")] public string BatchName { get; set; }

    }
}
