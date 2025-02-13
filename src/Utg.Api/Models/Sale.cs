using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Utg.Api.Models
{
    public class Sale : Transaction
    {
        [JsonPropertyName("poNumber")]
        public string PoNumber { get; set; }

        [JsonPropertyName("shipTo")]
        public ShipTo ShipTo { get; set; }

        [Required]
        [JsonPropertyName("account")]
        public AccountClear Account { get; set; }

        [JsonPropertyName("descriptor")]
        public DescriptorViewModel Descriptor { get; set; }

        [JsonPropertyName("batchName")]
        public string BatchName { get; set; }

        [JsonPropertyName("geoCode")]
        public GeoCode GeoCode { get; set; }
    }
}
