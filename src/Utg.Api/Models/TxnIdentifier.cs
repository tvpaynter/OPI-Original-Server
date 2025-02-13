using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utg.Api.Models
{
    public class TxnIdentifier
    {
        public bool IsNewTransaction { get; set; }
        public bool IsFinalAuth { get; set; }

        public bool IsIncrementalAuth { get; set; }
    }
}
