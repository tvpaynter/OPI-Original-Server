using System;

namespace Utg.Api.Models.OPIModels
{
    public class TransactionRequest
    {
        private decimal? _amount;
        private string expiryDate;

        public string SequenceNo { get; set; }
        public string TransType { get; set; }
        public decimal? TransAmount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value != null ? Convert.ToDecimal(string.Format("{0:0.00}", value / 100)) : null;
            }
        }
        public string TransCurrency { get; set; }
        public string TaxAmount { get; set; }
        public DateTime TransDateTime { get; set; }
        public int PartialAuthFlag { get; set; }
        public int SAF { get; set; }
        public string CardPresent { get; set; }
        public string SiteId { get; set; }
        public string WSNo { get; set; }
        public string Operator { get; set; }
        public string GuestNo { get; set; }
        public string ChargeInfo { get; set; }
        public int IndustryCode { get; set; }
        public string ProxyInfo { get; set; }
        public string POSInfo { get; set; }
        public string TransToken { get; set; }
        public string Pan { get; set; }
        public string ExpiryDate
        {
            get
            {
                return expiryDate;
            }
            set
            {
                //DateTime date = DateTime.ParseExact(value, "yyMM", CultureInfo.InvariantCulture);
                expiryDate = value;//String.Format("{0}{1}", date.Month, date.ToString("yy"));
            }
        }

        public string IssuerId { get; set; }
        public string LodgingCode { get; set; }
        public string RoomRate { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string AuthCode { get; set; }
        public string NewIncrementalAuth { get; set; }
        public string OriginalRRN { get; set; }
        public string OtherAmount { get; set; }
        public string ServiceChargeTtl { get; set; }
        public string AutoServiceChargeTtl { get; set; }
        public string NonRevenueServiceChargeTtl { get; set; }
        public string DiscountTtl { get; set; }
        public string CheckEmployeeCheckName { get; set; }
        public string Covers { get; set; }
        public string CheckName { get; set; }
        public string OrderType { get; set; }
        public string TableTextAndGroup { get; set; }
        public string OriginalType { get; set; }
        public string OriginalTime { get; set; }
    }

}
