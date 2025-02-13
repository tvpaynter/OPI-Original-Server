using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Utg.Api.Models.OPIModels
{
	public class TransactionResponse
	{
		private decimal? _amount;
		public string SequenceNo { get; set; }
		public string TransType { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? TransAmount 
		{
			get
			{
				return _amount != null ? Convert.ToDecimal(string.Format("{0:0}", _amount * 100)) : null;
			}
			set
			{
				_amount = value;	
			}
		}

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int OtherAmount { get; set; }
		public string PAN { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int ExpiryDate { get; set; }
		public string TransToken { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int EntryMode { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public int Balance { get; set; }
		public string IssuerId { get; set; }
		public string RespCode { get; set; }
		public string RespText { get; set; }
		public string AuthCode { get; set; }
		public string RRN { get; set; }
		public string OfflineFlag { get; set; }
		public string MerchantId { get; set; }
		public string TerminalId { get; set; }
		public string PrintData { get; set; }
		public string DCCIndicator { get; set; }
		public string AltReceiptInfo { get; set; }
		public string CardholderName { get; set; }
		public string APP { get; set; }
		public string AID { get; set; }
		public string TVR { get; set; }
		public string TSI { get; set; }
		public string TC { get; set; }
		public string OtherInfo { get; set; }
		public string ProviderID { get; set; }
		public string ServiceChargeTtl { get; set; }
		public string AutoServiceChargeTtl { get; set; }
		public string NonRevenueServiceChargeTtl { get; set; }
		public string DiscountTtl { get; set; }
		public string CheckEmployeeCheckName { get; set; }
		public string Covers { get; set; }
		public string CheckName { get; set; }
		public string OrderType { get; set; }
		public string TableTextAndGroup { get; set; }
		public string TipAmount { get; set; }
	}
}
