using System;
using System.Collections.Generic;

namespace StatementIQ.Common.Web.Filters
{
    public interface IValidatorsGetter
    {
        Dictionary<string, Type> GetValidators();
    }
}