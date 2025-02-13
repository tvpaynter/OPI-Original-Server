using Microsoft.Extensions.Logging;
using Utg.Api.Common.Constants;
using Utg.Api.Exceptions;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Utg.Api.Validators
{
    public  class XmlValidator
    {
        private readonly static string targetNameSpce = "";
        private readonly static string xsdFolderPath = "/Common/Trx/";
        private readonly ILogger<XmlValidator> _logger;
        

        public XmlValidator(ILogger<XmlValidator> logger)
        {
            _logger = logger;
        }

       
        public  bool ValidateXml(string inputXml,string xsdName)
        {
            bool isValidXml;
            try
            {
                var xsdFilePath = new StringBuilder().Append(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Append(xsdFolderPath).Append(xsdName).ToString();
                
                if (File.Exists(xsdFilePath))
                {
                    XmlSchemaSet schema = new XmlSchemaSet();
                    schema.Add(targetNameSpce, xsdFilePath);
                    XDocument doc = XDocument.Load(new MemoryStream(Encoding.UTF8.GetBytes(inputXml)));
                    doc.Validate(schema, ValidationEventHandler);
                    isValidXml = true;
                }
                else
                {
                    throw new FileNotFoundException($"{xsdFilePath} not found");
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while validating the XML {ex?.StackTrace} {nameof(XmlValidator)} in a Method: {nameof(ValidateXml)}");
                
                if(ex.GetType().ToString() == "System.IO.FileNotFoundException")
                {
                    throw;
                }
                else
                {
                    throw new ValidationException(ApplicationErrorMessages.XmlValidationException); 
                }
                
            }
            return isValidXml;
        }
        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error)
                {
                    _logger.LogError(e.Message);
                    throw new ValidationException(e.Message);
                }
            }
        }
    }
}
