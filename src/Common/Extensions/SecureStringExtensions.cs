using System;
using System.Runtime.InteropServices;
using System.Security;
using MandateThat;

namespace StatementIQ.Extensions
{
    public static class SecureStringExtensions
    {
        public static SecureString AppendString(this SecureString s, string src)
        {
            Mandate.That(s, nameof(s)).IsNotNull();
            Mandate.That(src, nameof(src)).IsNotNullOrWhiteSpace();

            foreach (var c in src) s.AppendChar(c);

            return s;
        }

        public static string GetString(this SecureString value)
        {
            Mandate.That(value, nameof(value)).IsNotNull();

            var valuePtr = IntPtr.Zero;

            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}