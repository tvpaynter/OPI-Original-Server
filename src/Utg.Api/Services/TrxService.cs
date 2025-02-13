using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Utg.Api.Common;
using Utg.Api.Common.Constants;
using Utg.Api.Common.Handlers;
using Utg.Api.Interfaces;
using Utg.Api.Models.OPIModels;
using Utg.Api.Models.TrxModels.TrXCreditResponse;
using Utg.Api.Validators;
using CreditRequest = Utg.Api.Models.TrxModels.TrXCreditRequest;
using CreditResponse = Utg.Api.Models.TrxModels.TrXCreditResponse;



namespace Utg.Api.Services
{
    /// <summary>
    /// Trx Service Class
    /// </summary>
    public class TrxService : IUTGService
    {
        private readonly ILogger<TrxService> _logger;
        private readonly HttpPostHandler _httpHandler;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// TrxService Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpHandler"></param>
        /// <param name="configuration"></param>
        public TrxService(ILogger<TrxService> logger, HttpPostHandler httpHandler, IConfiguration configuration)
        {
            _logger = logger;
            _httpHandler = httpHandler;
            _configuration = configuration;
        }
        /// <summary>
        /// Process OPI request  
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TransactionResponse> ProcessMessage(TransactionRequest request, CancellationToken cancellationToken = default)
        {
            new RequestValidator().ValidateModel(request);
            string trxRequest = BuildCredRequest(request);
            _logger.Log(LogLevel.Information, $"Request in TRX Format : {trxRequest}");
            var trxResponse = await _httpHandler.SendMessage(trxRequest);
            XDocument doc = XDocument.Parse(trxResponse);
            string parseReeponse = Utils.DecryptPayloadResponse(doc.Root.Element("Response").Value, _configuration["TrxSettings:EncryptionKey"], _configuration["TrxSettings:EncryptionIv"]);
            doc.Root.Element("Response").Value = parseReeponse;
            string xmlValue = HttpUtility.HtmlDecode(doc.ToString());
            _logger.Log(LogLevel.Information, $"Response from TRX : {xmlValue}");
            CreditResponse.Message xmlMessageObj = Utils.DeserializeToObject<CreditResponse.Message>(xmlValue);
            TransactionResponse response = BuildOPIResponse(xmlMessageObj, request);
            return response;
        }
        /// <summary>
        /// Build Cred Auth Response in OPI Format 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="opiRequest"></param>
        /// <returns></returns>
        private TransactionResponse BuildOPIResponse(CreditResponse.Message message, TransactionRequest opiRequest)
        {
            TransactionResponse transactionResponse = new()
            {
                SequenceNo = opiRequest.SequenceNo,
                TransType = opiRequest.TransType.ToString(),
                AuthCode = message.Response.Result.ApprovalCode,
                RespCode = message.Response.Result.ResponseCode,
                RespText = message.Response.Result.ResponseText,
                MerchantId = opiRequest.SiteId,
                TerminalId = opiRequest.POSInfo,
                IssuerId = message.Response?.Reference?.AccountBrand.ToString().ToUpper().GetIssuerID(),
                OfflineFlag = "N",
                
            };

            if (OPITransactionType.Reversal == opiRequest.TransType)
            {
                if (message.Response.QueryResponse?.Length > 0 && !string.IsNullOrEmpty(message.Response.QueryResponse[0].ReversalGuid))
                {
                    transactionResponse.RRN = message.Response.QueryResponse[0].ReversalGuid[3..];
                    transactionResponse.RespText = UTGConstants.OPIAlreadyReversedRespText;
                }
                else
                {
                    transactionResponse.RespCode = UTGConstants.TransNotFoundCode;
                    transactionResponse.RespText = UTGConstants.OPITransNotFoundRespText;
                }
            }
            else
            {
                transactionResponse.RespText = (message.Response.Result.ResponseCode != "X2") ? message.Response.Result.ResponseText : message.Response.Result.DebugInfo;
                transactionResponse.TransAmount = opiRequest.TransAmount;
                transactionResponse.TransToken = string.IsNullOrEmpty(opiRequest.TransToken) ? message.Response.Reference.Guid : opiRequest.TransToken;
                transactionResponse.RRN = message.Response.Reference.Guid[3..];
                if (transactionResponse.RespCode != UTGConstants.OPIApprovedRespCode)
                {
                    transactionResponse.RespCode = (message?.Response?.Result?.ResponseCode) switch
                    {
                        "X7" => UTGConstants.TransNotFoundCode,
                        _ => UTGConstants.OPIErrorRespCode,
                    };
                }
            }
            if (opiRequest.TransType == OPITransactionType.TransactionInquiry)
            {
                if (message.Response.QueryResponse?.Length > 0)
                {
                    transactionResponse.PAN = "XXXXXXXXXXX" + String.Format("{0:D4}", Convert.ToInt32(message.Response.QueryResponse[0].LastFour));
                    transactionResponse.ExpiryDate = Convert.ToInt32(DateTime.Now.AddYears(2).ToString("yy") + "12");
                    transactionResponse.EntryMode = 27;
                    transactionResponse.IssuerId = message.Response.QueryResponse[0].CardType.ToString().ToUpper().GetIssuerID();
                }
                else
                {
                    transactionResponse.RespCode = UTGConstants.TransNotFoundCode;
                    transactionResponse.RespText = UTGConstants.OPITransNotFoundRespText;
                }
            }
            if (transactionResponse.RespCode == UTGConstants.OPIApprovedRespCode)
            {
                transactionResponse.PrintData = BuildPrintData(message, opiRequest);
            }
            return transactionResponse;
        }

        private string BuildPrintData(Message message, TransactionRequest opiRequest)
        {
            StringBuilder printdatasb1 = new();
            printdatasb1.Append("##Merchant ID: " + opiRequest.SiteId);
            printdatasb1.Append("#Terminal ID: " + opiRequest.WSNo);
            printdatasb1.Append("#Card No. : XXXXXXXXXXX" + (opiRequest.TransType == OPITransactionType.TransactionInquiry ? 
                                                            String.Format("{0:D4}", Convert.ToInt32(message?.Response?.QueryResponse[0]?.LastFour)) : 
                                                            String.Format("{0:D4}", Convert.ToInt32(message?.Response?.Reference?.LastFour))));
            printdatasb1.Append("(C)#Expiry Date: XX/XX#Card Type: " + (opiRequest.TransType == OPITransactionType.TransactionInquiry ? 
                                                                       message?.Response?.QueryResponse[0]?.CardType : 
                                                                       message?.Response?.Reference?.AccountBrand));
            printdatasb1.Append("#Trans Type : " + opiRequest.TransType.GetTrxTransactionType());
            printdatasb1.Append("#Trans Time : " + message.Response.Reference.TranDate + " " + message.Response.Reference.TranTime);
            printdatasb1.Append("#Trace No.: " + opiRequest.SequenceNo);
            printdatasb1.Append("#RRN: " + message.Response.Reference.Guid[3..]);
            printdatasb1.Append("#Auth Code: " + message.Response.Result.ApprovalCode);
            printdatasb1.Append("##App Label : #AID: #AC: ");
            printdatasb1.Append("## BASE AMOUNT : USD" + string.Format("{0:0.00}", opiRequest.TransAmount));
            printdatasb1.Append("###TIP: _______________###TOTAL: __________________");
            string printdata1 = "<MTYPE>#<MNAME>#<MCITY>, <MSTATE>,MD##";
            StringBuilder printdatasb = new StringBuilder();
            printdatasb.Append(printdata1);
            printdatasb.Append(" Merchant Copy");
            printdatasb.Append(printdatasb1.ToString());
            printdatasb.Append("###Signature: ____________________###I agree to the terms of my#credit agreement.@ ");
            printdatasb.Append(printdata1);
            printdatasb.Append(" Customer Copy");
            printdatasb.Append(printdatasb1.ToString());
            printdatasb.Append("###Transaction Approved with Signature##I agree to the terms of my#credit agreement.");
            return printdatasb.ToString();
        }

        /// <summary>
        /// Build Cred Auth Request in Trx Format 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string BuildCredRequest(TransactionRequest request)
        {
            StringBuilder trxRequest = new();
            CreditRequest.Message credAuth = new();
            credAuth.Request = new CreditRequest.Request
            {
                Detail = GetDetailData(request)
            };
            trxRequest = trxRequest.Append(Utils.Serialize(credAuth.Request.Detail));
            trxRequest = trxRequest.Append(Utils.Serialize(GetUserDefinedData(request.SequenceNo)));

            switch (request.TransType)
            {
                //manual entry on pos
                case OPITransactionType.PreAuth:
                    trxRequest = trxRequest.Append(Utils.Serialize(GetIndustryData(request)));
                    trxRequest = trxRequest.Append(Utils.Serialize(GetAccountData(request)));
                    break;
                case OPITransactionType.IncrementalAuth:
                case OPITransactionType.SaleCompletion:
                    trxRequest = trxRequest.Append(Utils.Serialize(GetIndustryData(request)));
                    trxRequest = trxRequest.Append(Utils.Serialize(GetReferenceData(request.TransToken)));
                    break;
                case OPITransactionType.AuthRelease:
                    trxRequest = trxRequest.Append(Utils.Serialize(GetReferenceData(request.TransToken)));
                    break;
                case OPITransactionType.Void:
                case OPITransactionType.Refund:
                    trxRequest = trxRequest.Append(Utils.Serialize(GetReferenceData(request.TransToken[..3] + request.OriginalRRN)));
                    break;
                case OPITransactionType.Reversal:
                case OPITransactionType.TransactionInquiry:
                    trxRequest = trxRequest.Append(Utils.Serialize(GetQueryData(request)));
                    break;
            }

            string EncryptedRequest = Utils.EncryptPayloadRequest(trxRequest.ToString(), _configuration["TrxSettings:EncryptionKey"], _configuration["TrxSettings:EncryptionIv"]);
            credAuth.Authentication = GetAuthenticationData();
            string XMLFinal = Utils.Serialize(credAuth);
            XDocument doc = XDocument.Parse(XMLFinal);
            doc.Root.Element("Request").Value = EncryptedRequest;
            //prepare trx transaction request based upon received OPI message
            return doc.ToString();
        }

        private static CreditRequest.QueryRequest GetQueryData(TransactionRequest request)
        {
            DateTime date = DateTime.Parse(request.OriginalTime, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return new()
            {
                Detail = new()
                {
                    FromDate = date.AddDays(-1).ToString("MM\\/dd\\/yyyy"),
                    ToDate = date.ToString("MM\\/dd\\/yyyy")
                },
                UserDefined = GetUserDefinedData(request.SequenceNo)
            };
        }

        private static CreditRequest.UserDefined GetUserDefinedData(string sequenceNo)
        {
            var doc = new XmlDocument();
            doc.LoadXml("<SequenceNo>" + sequenceNo + "</SequenceNo>");
            var sequenceNoElement = doc.DocumentElement;
            var arraysequenceNoElement = new XmlElement[]
            {
                sequenceNoElement
            };
            return new CreditRequest.UserDefined
            {
                Any = arraysequenceNoElement
            };
        }

        private static CreditRequest.IndustryData GetIndustryData(TransactionRequest request)
        {
            string trxCheckInDate = string.Empty;
            string trxCheckOutDate = string.Empty;
            if (!string.IsNullOrEmpty(request.CheckInDate) && !string.IsNullOrEmpty(request.CheckOutDate))
            {
                DateTime CheckInDate = DateTime.ParseExact(request.CheckInDate, "yyyyMMdd", null);
                DateTime CheckOutDate = DateTime.ParseExact(request.CheckOutDate, "yyyyMMdd", null);
                System.Globalization.CultureInfo cinfo = new("en-US");
                trxCheckInDate = CheckInDate.ToString("MM/dd/yyyy", cinfo);
                trxCheckOutDate = CheckOutDate.ToString("MM/dd/yyyy", cinfo);
            }

            return new CreditRequest.IndustryData
            {
                Industry = CreditRequest.IndustryType.Hotel,
                Eci = CreditRequest.EciType.Item7,
                EciSpecified = true,
                CheckInDate = trxCheckInDate,
                CheckOutDate = trxCheckOutDate,
                MarketSpecificId = CreditRequest.MarketSpecificIdType.H
            };
        }

        private static CreditRequest.Reference GetReferenceData(string token)
        {
            return new CreditRequest.Reference
            {
                Guid = token
            };
        }

        private static CreditRequest.Detail GetDetailData(TransactionRequest request)
        {
            return request.TransType switch
            {
                OPITransactionType.Void => new CreditRequest.Detail
                {
                    TranType = CreditRequest.TransType.Credit,
                    TranAction = TransTypeMapping.GetTrxTransactionType(request.TransType)
                },
                OPITransactionType.Reversal => new CreditRequest.Detail
                {
                    TranType = CreditRequest.TransType.Credit,
                    TranAction = TransTypeMapping.GetTrxTransactionType(request.TransType)
                },
                OPITransactionType.TransactionInquiry => new CreditRequest.Detail
                {
                    TranType = CreditRequest.TransType.Credit,
                    TranAction = TransTypeMapping.GetTrxTransactionType(request.TransType)
                },
                _ => new CreditRequest.Detail
                {
                    TranType = CreditRequest.TransType.Credit,
                    TranAction = TransTypeMapping.GetTrxTransactionType(request.TransType),
                    Amount = Convert.ToDecimal(request.TransAmount),
                    CurrencyCode = CreditRequest.CurrencyCodeType.Item840
                },
            };
        }

        private static CreditRequest.Account GetAccountData(TransactionRequest request)
        {
            DateTime date = DateTime.ParseExact(request.ExpiryDate, "yyMM", CultureInfo.InvariantCulture);
            var expirydate = String.Format("{0}{1}", date.Month, date.ToString("yy"));
            return new CreditRequest.Account
            {
                Pan = request.Pan,
                Expiration = Convert.ToUInt32(expirydate),
                ExpirationSpecified = true
            };
        }

        private CreditRequest.Authentication GetAuthenticationData()
        {
            return new CreditRequest.Authentication
            {
                Client = Convert.ToUInt32(_configuration["TrxSettings:Client"]),
                Source = _configuration["TrxSettings:Source"]
            };
        }
    }

}
