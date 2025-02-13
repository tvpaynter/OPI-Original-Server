using Utg.Api.Common.Constants;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Utg.Api.Models
{
    [KnownType(typeof(AccountClear))]
    public class Account
    {
        [JsonPropertyName("Type")]
        public PaymentInstrumentType InstrumentType { get; set; }
    }
}