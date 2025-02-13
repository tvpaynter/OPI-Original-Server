using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Utg.Api.Models
{
    public class Device
    {
        [Required] [JsonPropertyName("type")] public string Type { get; set; }

        [Required]
        [JsonPropertyName("terminalName")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("terminalIdentifier")]
        public string TerminalIdentifier { get; set; }

        [JsonPropertyName("settings")] public Dictionary<string, object> Settings { get; set; }
    }
}