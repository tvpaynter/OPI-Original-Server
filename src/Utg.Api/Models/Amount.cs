using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Utg.Api.Models
{
    public abstract class Amount
    {
        [Required]
        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }

        [Required]
        [JsonPropertyName("amount")]
        public decimal AmountTotal { get; set; }

        [JsonPropertyName("tipAmount")] public decimal TipAmount { get; set; }

        [JsonPropertyName("taxAmount")] public decimal TaxAmount { get; set; }
    }
}