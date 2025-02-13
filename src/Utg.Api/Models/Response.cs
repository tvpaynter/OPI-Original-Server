using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Utg.Api.Models
{
    public class Response
    {
        [JsonPropertyName("approved")] public bool Approved { get; set; }

        [JsonPropertyName("responseCode")] public string ResponseCode { get; set; }

        [JsonPropertyName("responseDescription")]
        public string ResponseDescription { get; set; }

        [JsonPropertyName("authCode")] public string AuthCode { get; set; }

        [JsonPropertyName("transactionId")] public string TransactionId { get; set; }

        [JsonPropertyName("referenceId")] public string ReferenceId { get; set; }

        [JsonPropertyName("transactionType")] public string TransactionType { get; set; }

        [JsonPropertyName("transactionHash")] public string TransactionHash { get; set; }

        [JsonPropertyName("timestamp")] public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("entryMethod")] public string EntryMethod { get; set; }

        [JsonPropertyName("paymentType")] public string PaymentType { get; set; }

        [JsonPropertyName("maskedPan")] public string MaskedPan { get; set; }

        [JsonPropertyName("cardHolder")] public string CardHolder { get; set; }

        [JsonPropertyName("partialAuth")] public bool PartialAuth { get; set; }

        [JsonPropertyName("currencyCode")] public string CurrencyCode { get; set; }

        [JsonPropertyName("requestedAmount")] public decimal RequestedAmount { get; set; }

        [JsonPropertyName("authorizedAmount")] public decimal AuthorizedAmount { get; set; }

        [JsonPropertyName("surchargeAmount")] public decimal SurchargeAmount { get; set; }

        [JsonPropertyName("PAR")] public string PAR { get; set; }

        [JsonPropertyName("version")] public string Version { get; set; }

        [JsonPropertyName("token")] public string Token { get; set; }


        [JsonPropertyName("terminalTransactionId")]
        public long TerminalTransactionId { get; set; }
    }
}
