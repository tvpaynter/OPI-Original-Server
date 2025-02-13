using System;
using System.Numerics;
using System.Text;
using MandateThat;
using StatementIQ.Properties;

namespace StatementIQ.Extensions
{
    public static class BaseConverterExtensions
    {
        public static string ConvertToBaseString(this byte[] valueAsArray, string digits, int pad)
        {
            Mandate.That(digits, nameof(digits)).IsNotNullOrEmpty();

            if (digits.Length < 2)
                throw new ArgumentOutOfRangeException(nameof(digits), Resources.BaseConverter_OutOfRangeMessage);

            var value = new BigInteger(valueAsArray);
            var isNeg = value < 0;
            value = isNeg ? -value : value;

            var sb = new StringBuilder(pad + (isNeg ? 1 : 0));

            do
            {
                value = BigInteger.DivRem(value, digits.Length, out var rem);
                sb.Append(digits[(int) rem]);
            } while (value > 0);

            if (sb.Length < pad) sb.Append(digits[0], pad - sb.Length);

            if (isNeg) sb.Append('-');

            for (int i = 0, j = sb.Length - 1; i < j; i++, j--)
            {
                var t = sb[i];
                sb[i] = sb[j];
                sb[j] = t;
            }

            return sb.ToString();
        }

        public static BigInteger ConvertFromBaseString(this string s, string digits)
        {
            switch (Parse(s, digits, out var result))
            {
                case ParseCode.FormatError:
                    throw new FormatException(Resources.BaseConverter_FormatExceptionMessage);
                case ParseCode.NullString:
                    throw new ArgumentNullException(nameof(s));
                case ParseCode.NullDigits:
                    throw new ArgumentNullException(nameof(digits));
                case ParseCode.InsufficientDigits:
                    throw new ArgumentOutOfRangeException(nameof(digits), Resources.BaseConverter_OutOfRangeMessage);
                case ParseCode.Overflow:
                    throw new OverflowException();
            }

            return result;
        }

        public static bool TryConvertFromBase62String(this string s, string digits, out BigInteger result)
        {
            return Parse(s, digits, out result) == ParseCode.Success;
        }

        private static ParseCode Parse(this string input, string digits, out BigInteger result)
        {
            result = 0;

            var validateResult = ValidateInputAndDigits(input, digits);

            if (validateResult != ParseCode.Success) return validateResult;

            var i = 0;

            while (i < input.Length && char.IsWhiteSpace(input[i])) ++i;

            if (i >= input.Length) return ParseCode.FormatError;

            BigInteger sign = 1;

            switch (input[i])
            {
                case '+':
                    ++i;
                    break;

                case '-':
                    ++i;
                    sign = -1;
                    break;
            }

            if (i >= input.Length) return ParseCode.FormatError;

            while (i < input.Length)
            {
                var n = digits.IndexOf(input[i]);

                if (n < 0) return ParseCode.FormatError;

                var oldResult = result;
                result = result * digits.Length + n;

                if (result < oldResult) return ParseCode.Overflow;

                ++i;
            }

            while (i < input.Length && char.IsWhiteSpace(input[i])) ++i;

            if (i < input.Length) return ParseCode.FormatError;

            if (sign < 0) result = -result;

            return ParseCode.Success;
        }

        private static ParseCode ValidateInputAndDigits(string input, string digits)
        {
            if (input == null) return ParseCode.NullString;

            if (digits == null) return ParseCode.NullDigits;

            if (digits.Length < 2) return ParseCode.InsufficientDigits;

            return ParseCode.Success;
        }

        private enum ParseCode
        {
            Success,
            NullString,
            NullDigits,
            InsufficientDigits,
            Overflow,
            FormatError
        }
    }
}