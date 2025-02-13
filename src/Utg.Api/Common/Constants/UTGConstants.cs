using System.Collections;

namespace Utg.Api.Common.Constants
{
    /// <summary>
    /// UTG Constants
    /// </summary>
    public static class UTGConstants
    {
        public static readonly char STX = (char)2;
        public static readonly char ETX = (char)3;
        public static readonly string PreAuth = "PREAUTH";
        public static readonly string CryptoName = "Rijndael";
        public static readonly string OPIErrorRespCode = "06";
        public static readonly string OPIErrorRespText = "ERROR";
        public static readonly string NotReachable = "HOST NOT REACHABLE";
        public static readonly int PingTimeout = 100;
        public static readonly string TransNotFoundCode = "25";
        public static readonly string OPIApprovedRespCode = "00";
        public static readonly string OPIAlreadyReversedRespText = "ALREADY REVERSED";
        public static readonly string OPITransNotFoundRespText = "TRANSACTION NOT FOUND";
        public static readonly string OPITimeOutRespCode = "99";
        public static readonly string OPITimeOutRespText = "TRANSACTION TIME OUT";

    }
    /// <summary>
    /// Issuer Id Constant
    /// </summary>
    public static class IssuerId
    {
        /// <summary>
        /// hashtable
        /// </summary>
        public static readonly Hashtable hashtable = new Hashtable
            {
                { "VISA","01" },
                { "MASTERCARD", "02" },
                { "AMEX", "03" },
                { "DISCOVER", "12" },
                { "UNIONPAY","06"},
                { "JCB","05"}
            };
        /// <summary>
        /// Get Trx Transaction Type
        /// </summary>
        /// <param name="strOPITransactionType"></param>
        /// <returns></returns>
        public static string GetIssuerID(this string strOPITransactionType)
        {
            return hashtable.Contains(strOPITransactionType) ? hashtable[strOPITransactionType].ToString() : "11";
        }
    }
}
