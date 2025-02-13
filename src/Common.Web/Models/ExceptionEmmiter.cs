using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementIQ.Common.Web.Models
{
    public class ExceptionEmitter
    {
        public ExceptionEmitter()
        { }
        public string TraceId { get; set; }
        public string DateTime { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorType { get; set; }
        public string CorrelationId { get; set; }
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string RequestMethod { get; set; }
        public string ServiceName { get; set; }
        public string QueryString { get; set; }
        public string Client { get; set; }
        public string Release { get; set; }
        public string Environment { get; set; }

    }

}
