using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Utg.Api.Common.Constants;
using Utg.Api.Exceptions;
using Utg.Api.Models.OPIModels;

namespace Utg.Api.Common
{
    public static  class Utils
    {
        public static T DeserializeToObject<T>(string xmlData) where T : class
        {
            XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            try
            {
                using (StreamReader sr = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(xmlData))))
                {
                    return (T)ser.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception raised in DeserializeToObject method : {ex.Message}");
                throw;
            }

        }
        public static string Serialize<T>(T dataToSerialize)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(dataToSerialize.GetType());
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, dataToSerialize, emptyNamespaces);
                var document = XDocument.Parse(stream.ToString());
                document.Descendants()
                        .Where(e => e.IsEmpty || String.IsNullOrWhiteSpace(e.Value))
                        .Remove();
                return document.ToString();
            }
        }
        public static XDocument GetMaskedRequest(string xmlRequest)
        {
            XDocument doc=new();
            try
            {
                doc = XDocument.Parse(xmlRequest);
                foreach (XElement element in doc.Descendants().Where(
                    e => e.Name.ToString().Contains("AcctNum")))
                {
                    element.Value = element.Value.MaskedCardNumber();
                }
                foreach (XElement element in doc.Descendants().Where(
                    e => e.Name.ToString().Contains("CCVData")))
                {
                    element.Value = "***";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception raised in GetMaskedRequest Method : {ex.Message}");
            }
            return doc;
        }
        public static string MaskedCardNumber(this string cardNumber)
        {
            if (!string.IsNullOrEmpty(cardNumber) && cardNumber.Length >= 15)
            {
                var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);

                var requiredMask = new String('X', cardNumber.Length-4);

                var maskedString = string.Concat(requiredMask, lastDigits);
                var maskedCardNumber = Regex.Replace(maskedString, ".{4}", "$0 ");
                return maskedCardNumber;
            }
            return cardNumber;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="opiRequest"></param>
        /// <returns></returns>
        public static TransactionResponse BuildErrorResponse(string errorCode, string errorMessage, TransactionRequest opiRequest)
        {
            TransactionResponse response = new();
            response.RespText = errorMessage;
            response.RespCode = errorCode;
            response.SequenceNo = opiRequest.SequenceNo;
            response.TransType = opiRequest.TransType;
            response.MerchantId = opiRequest.SiteId;
            response.RRN = "NA";
            return response;

        }
        public static string GetXMLData(object xmlMssage)
        {
            MemoryStream memoryStream = new();
            XmlSerializer xs = new(xmlMssage.GetType());
            XmlTextWriter xmlTextWriter = new(memoryStream, System.Text.Encoding.UTF8);
            xs.Serialize(xmlTextWriter, xmlMssage);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            UTF8Encoding encoding = new();
            string xmlString = encoding.GetString(memoryStream.ToArray());
            xmlString = xmlString.Substring(1, xmlString.Length - 1);
            return xmlString;
        }
        /// <summary>
        /// To encrypt te request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string EncryptPayloadRequest(string request,string key,string IV)
        {
            SymmetricAlgorithm CryptAlgorithm = (SymmetricAlgorithm)CryptoConfig.CreateFromName(UTGConstants.CryptoName);
            CryptAlgorithm.Key = ConvertHex(key);
            CryptAlgorithm.Padding = PaddingMode.Zeros;
            CryptAlgorithm.Mode = CipherMode.CBC;
            CryptAlgorithm.IV = ConvertHex(IV);
            ICryptoTransform icCryptor = CryptAlgorithm.CreateEncryptor();
            MemoryStream msCrypt = new(Encoding.ASCII.GetBytes(request));
            CryptoStream csCrypt = new(msCrypt, icCryptor, CryptoStreamMode.Write);
            byte[] bTemp = icCryptor.TransformFinalBlock(Encoding.ASCII.GetBytes(request), 0, Encoding.ASCII.GetBytes(request).Length);
            string result = Convert.ToBase64String(bTemp);

            return result;
        }
        /// <summary>
        ///  To Decrypt te request
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string DecryptPayloadResponse(string payload, string key, string IV)
        {
            byte[] payload1 = Convert.FromBase64String(payload);
            SymmetricAlgorithm CryptAlgorithm = (SymmetricAlgorithm)CryptoConfig.CreateFromName(UTGConstants.CryptoName);
            CryptAlgorithm.Key = ConvertHex(key);
            CryptAlgorithm.Padding = PaddingMode.Zeros;
            CryptAlgorithm.Mode = CipherMode.CBC;
            CryptAlgorithm.IV = ConvertHex(IV);
            ICryptoTransform icCryptor = CryptAlgorithm.CreateDecryptor();
            MemoryStream msCrypt = new MemoryStream(payload1);
            CryptoStream csCrypt = new CryptoStream(msCrypt, icCryptor, CryptoStreamMode.Read);
            byte[] bTemp = icCryptor.TransformFinalBlock(payload1, 0, payload1.Length);
            // store decoded/decrypted response data
            return Encoding.ASCII.GetString(bTemp).Trim('\0');
        }
        private static byte[] ConvertHex(string sHex)
        {
            byte[] yKey;

            try
            {
                // set byte length
                yKey = new byte[sHex.Length / 2];

                for (int i = 0, j = 0; j < yKey.Length; i += 2, j++)
                {
                    yKey[j] = Convert.ToByte(sHex.Substring(i, 2), 16);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

            return yKey;
        }
    }
}
