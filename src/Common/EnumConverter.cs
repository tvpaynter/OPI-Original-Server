using System;

namespace StatementIQ
{
    public static class EnumConverter
    {
        public static T ConvertTo<T>(this Enum enumValue) where T : struct, IConvertible
        {
            return Enum.Parse<T>(enumValue.ToString());
        }
    }
}