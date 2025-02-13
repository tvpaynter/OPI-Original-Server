using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementIQ.Common.Web.Logging.LogHelper
{
    public enum ExceptionType
    {
        CodeError,
        SessionError,
        BusinessError,
        ValidationError,
        AccessError,
        GenericError
    }

    public class ExceptionEmitter
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public ExceptionType ErrorType { get; set; }
        public string TraceId { get; set; }
        public string DateTime { get; set; }
    }
}
