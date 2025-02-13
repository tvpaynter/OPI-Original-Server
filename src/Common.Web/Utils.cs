using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace StatementIQ.Common.Web
{
    public static class Utils
    {
        public static string MaskedCardNumber(string cardNumber)
        {
            if (!string.IsNullOrEmpty(cardNumber) && cardNumber.Length >= 15)
            {
                var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);

                var requiredMask = new String('X', cardNumber.Length - 4);

                var maskedString = string.Concat(requiredMask, lastDigits);
                var maskedCardNumber = Regex.Replace(maskedString, ".{4}", "$0 ");
                return maskedCardNumber;
            }
            return cardNumber;
        }

        public static string GetRequestResponseBodyMasked(string requestBody)
        {
            int cardIndex;
            string card=string.Empty;
            try
            {
                dynamic jsonReqObject = JsonConvert.DeserializeObject(requestBody);
                if (jsonReqObject != null)
                {
                    string resultCount = Convert.ToString(jsonReqObject?.count);
                    if (!string.IsNullOrEmpty(resultCount))
                    {
                        requestBody = string.Empty;
                        return requestBody;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(jsonReqObject?.Pan)))
                    {
                        jsonReqObject.Pan = MaskedCardNumber(Convert.ToString(jsonReqObject.Pan));
                    }
                    else if (!string.IsNullOrEmpty(Convert.ToString(jsonReqObject?.RequestData)) || !string.IsNullOrEmpty(Convert.ToString(jsonReqObject?.requestData)))
                    {
                        var reqDtls = (string)jsonReqObject.RequestData;
                        reqDtls = String.IsNullOrEmpty(reqDtls) ? Convert.ToString(jsonReqObject.requestData) : reqDtls;
                        if (reqDtls.IndexOf("@T") > 0)
                        {
                            cardIndex = reqDtls.IndexOf("@T");
                            card = reqDtls.Substring(cardIndex + 2, 16);
                            card = !long.TryParse(card, out _) ? reqDtls.Substring(cardIndex + 2, 15) : card;
                        }
                        else if (reqDtls.IndexOf("AcctNum") > 0)
                        {
                            cardIndex = reqDtls.IndexOf("AcctNum");
                            card = reqDtls.Substring(cardIndex + 8, 16);
                            card = !long.TryParse(card, out _) ? reqDtls.Substring(cardIndex + 8, 15) : card;
                        }
                        else if (reqDtls.IndexOf("pan") > 0)
                        {
                            cardIndex = reqDtls.IndexOf("pan");
                            card = reqDtls.Substring(cardIndex + 6, 16);
                            card = !long.TryParse(card, out _) ? reqDtls.Substring(cardIndex + 6, 16) : card;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(jsonReqObject?.RequestData)))
                        {
                            jsonReqObject.RequestData = reqDtls.Replace(card, MaskedCardNumber(card));
                        }
                        else if (!string.IsNullOrEmpty(Convert.ToString(jsonReqObject?.requestData)))
                        {
                            jsonReqObject.requestData = reqDtls.Replace(card, MaskedCardNumber(card));
                        }
                    }
                    else if (!string.IsNullOrEmpty(Convert.ToString(jsonReqObject?.ResponseData)))
                    {
                        var resDtls = (string)jsonReqObject.ResponseData;
                        if (resDtls.IndexOf("AcctNum") > 0)
                        {
                            resDtls = resDtls.Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&");
                            cardIndex = resDtls.IndexOf("AcctNum");
                            card = resDtls.Substring(cardIndex + 8, 16);
                            card = !long.TryParse(card, out _) ? resDtls.Substring(cardIndex + 8, 15) : card;
                            jsonReqObject.ResponseData = resDtls.Replace(card, MaskedCardNumber(card));
                        }
                    }
                    requestBody = JsonConvert.SerializeObject(jsonReqObject);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return requestBody;
        }
    }
}
