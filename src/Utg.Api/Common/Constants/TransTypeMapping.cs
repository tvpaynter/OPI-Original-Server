using System.Collections;
using Utg.Api.Models.TrxModels.TrXCreditRequest;

namespace Utg.Api.Common.Constants
{
    /// <summary>
    /// OPI Transaction Type
    /// </summary>
    public static class OPITransactionType
    {
        /// <summary>
        /// Pre Auth Const
        /// </summary>
        public const string PreAuth = "05";
        /// <summary>
        /// Incrmental Auth const
        /// </summary>
        public const string IncrementalAuth = "06";
        /// <summary>
        /// Sale Completion 
        /// </summary>
        public const string SaleCompletion = "07";
        /// <summary>
        /// Authorization Release Transaction
        /// </summary>
        public const string AuthRelease = "16";
        /// <summary>
        /// Void Transaction
        /// </summary>
        public const string Void = "08";
        /// <summary>
        /// Reversal Transaction
        /// </summary>
        public const string Reversal = "04";
        /// <summary>
        /// Reversal Transaction
        /// </summary>
        public const string TransactionInquiry = "20";
        /// <summary>
        /// Refund Transaction
        /// </summary>
        public const string Refund = "03";
    }
    /// <summary>
    /// Transaction Type mapping
    /// </summary>
    public static class TransTypeMapping
    {
        /// <summary>
        /// hashtable
        /// </summary>
        public static readonly Hashtable hashtable = new Hashtable
            {
                { OPITransactionType.PreAuth, TransAction.Auth },
                { OPITransactionType.IncrementalAuth, TransAction.Auth },
                { OPITransactionType.SaleCompletion, TransAction.Capture },
                { OPITransactionType.AuthRelease, TransAction.Reversal },
                { OPITransactionType.Void, TransAction.Void },
                { OPITransactionType.Reversal, TransAction.Query },
                { OPITransactionType.TransactionInquiry, TransAction.Query },
                { OPITransactionType.Refund, TransAction.Return }
            };
        /// <summary>
        /// Get Trx Transaction Type
        /// </summary>
        /// <param name="strOPITransactionType"></param>
        /// <returns></returns>
        public static TransAction GetTrxTransactionType(this string strOPITransactionType)
        {
            return (TransAction)hashtable[strOPITransactionType];
        }
    }
}
