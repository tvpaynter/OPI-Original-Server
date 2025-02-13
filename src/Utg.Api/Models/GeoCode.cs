using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Utg.Api.Models
{
    public class GeoCode
    {
        [JsonPropertyName("longitude")] public decimal Longitude { get; set; }

        [JsonPropertyName("latitude")] public decimal Latitude { get; set; }
    }
}
