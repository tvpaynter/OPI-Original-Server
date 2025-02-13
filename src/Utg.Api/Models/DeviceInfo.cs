using System.Text.Json.Serialization;

namespace Utg.Api.Models
{
    public class DeviceInfo : Device
    {
        [JsonPropertyName("hostIP")] public string HostIP { get; set; }
    }
}