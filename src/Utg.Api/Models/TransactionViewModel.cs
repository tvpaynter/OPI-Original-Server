using System;

namespace Utg.Api.Models
{
    public class TransactionViewModel
    {
        public string Arn { get; set; }
        public long MerchantId { get; set; }
        public string MerchantName { get; set; }
        public TransactionType TransactionType { get; set; }
        public ResponseType ResponseType { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
        public DateTime OpenAt { get; set; }
        public DateTime? CloseAt { get; set; }
        public DateTime? SettledAt { get; set; }
        public Currency Currency { get; set; }
        public string Pan { get; set; }
        public string TokenizedPan { get; set; }
        public PaymentInstrumentType InstrumentType { get; set; }
        public string ExpirationDate { get; set; }
        public long BatchId { get; set; }
        public long? referenceGUID { get; set; }

        public TransactionInformationViewModel BillTo { get; set; }
        public TransactionInformationViewModel ShipTo { get; set; }
    }
}