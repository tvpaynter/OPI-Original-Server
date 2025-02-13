using System;

namespace StatementIQ.Common.Test.Enums
{
    [Flags]
    public enum FlagsEnum
    {
        ValueA = 1,
        ValueC = 1 << 2
    }
}