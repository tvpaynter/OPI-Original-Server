using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Utg.Api.Models
{
    public abstract class Transaction : Amount
    {
        [JsonPropertyName("device")] public DeviceInfo Device { get; set; }

        [JsonPropertyName("employeeId")] public string EmployeeId { get; set; }

        [JsonPropertyName("industryData")] public Dictionary<string, object> IndustryData { get; set; }

        [JsonPropertyName("invoiceNumber")] public string InvoiceNumber { get; set; }

        [JsonPropertyName("tip")] public bool Tip { get; set; }

        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("userFields")] public Dictionary<string, string> UserFields { get; set; }
    }
}