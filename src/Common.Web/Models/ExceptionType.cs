using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementIQ.Common.Web.Models
{
    public enum ExceptionType
    {
        CodeError,
        SessionError,
        BusinessError,
        ValidationError,
        AccessError,
        DBError,
        GenericError
    }
}
