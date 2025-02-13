using System;

namespace Utg.Api.Models
{
    public class TransactionDetailsViewModel
    {
        public long Id { get; set; }
        public long BatchId { get; set; }
        public long TransactionId { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public TransactionType TransactionType { get; set; }
        public ResponseType ResponseType { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public string IpAddress { get; set; }
        public string FiservHostName { get; set; }
        public decimal ApproveAmount { get; set; }
        public decimal? CapturedAmount { get; set; }
        public decimal? TipAmount { get; set; }
        public decimal Amount { get; set; }
    }
}