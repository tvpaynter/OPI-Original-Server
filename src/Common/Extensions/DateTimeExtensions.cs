using System;
using System.Globalization;
using MandateThat;

namespace StatementIQ.Extensions
{
    public static class DateTimeExtensions
    {
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            Mandate.That(dateTime, nameof(dateTime)).IsGreaterThanOrEqualTo(Epoch);

            var diff = dateTime.DayOfWeek - dayOfWeek;

            if (diff < 0) diff += 7;

            return dateTime.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            Mandate.That(dateTime, nameof(dateTime)).IsGreaterThanOrEqualTo(Epoch);

            var diff = dayOfWeek - dateTime.DayOfWeek;

            return diff == 0
                ? dateTime.Date
                : dateTime.AddDays(diff).Date;
        }

        /// <summary>
        ///     Converts ISO-8601 format (yyyy-MM-ddTHH:mm:ssZ) date time to <see cref="DateTime" />.
        /// </summary>
        /// <param name="iso8601DateTime">The iso 8601 formatted date time.</param>
        /// <returns>
        ///     Returns the <see cref="DateTime" /> equivalent to the ISO-8601 formatted date time.
        /// </returns>
        public static DateTime FromIso8601FormattedDateTime(string iso8601DateTime)
        {
            Mandate.That(iso8601DateTime, nameof(iso8601DateTime)).IsNotNullOrWhiteSpace();
            return DateTime.ParseExact(iso8601DateTime, "o", CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Converts a unix time string to a DateTime object.
        /// </summary>
        /// <param name="unixTime">The unix time.</param>
        /// <returns>The DateTime object.</returns>
        public static DateTime FromUnixTime(double unixTime)
        {
            return Epoch.AddSeconds(unixTime);
        }

        /// <summary>
        ///     Converts a unix time string to a DateTime object.
        /// </summary>
        /// <param name="unixTime">The string representation of the unix time.</param>
        /// <returns>The DateTime object.</returns>
        public static DateTime FromUnixTime(string unixTime)
        {
            Mandate.That(unixTime, nameof(unixTime)).IsNotNullOrWhiteSpace();
            return FromUnixTime(!double.TryParse(unixTime, out var d) ? 0D : d);
        }

        /// <summary>
        ///     Converts to specified <see cref="DateTime" /> to ISO-8601 format (yyyy-MM-ddTHH:mm:ssZ).
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>Returns the string representation of date time in ISO-8601 format (yyyy-MM-ddTHH:mm:ssZ).</returns>
        public static string ToIso8601FormattedDateTime(DateTime dateTime)
        {
            Mandate.That(dateTime, nameof(dateTime)).IsNot(default);
            return dateTime.ToString("o");
        }

        /// <summary>
        ///     Converts a DateTime object to unix time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The unix date time.</returns>
        public static double ToUnixTime(DateTime dateTime)
        {
            Mandate.That(dateTime, nameof(dateTime)).IsGreaterThanOrEqualTo(Epoch);
            return (dateTime.ToUniversalTime() - Epoch).TotalSeconds;
        }

        /// <summary>
        ///     Converts a DateTimeOffset object to unix time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The unix date time.</returns>
        public static double ToUnixTime(DateTimeOffset dateTime)
        {
            Mandate.That(dateTime.UtcDateTime, nameof(dateTime)).IsGreaterThanOrEqualTo(Epoch);
            return (dateTime.ToUniversalTime() - Epoch).TotalSeconds;
        }

        public static DateTime SpecifyKind(this DateTime dateTime, DateTimeKind kind)
        {
            DateTime.SpecifyKind(dateTime, kind);
            return dateTime;
        }

        public static DateTime? SpecifyKind(this DateTime? dateTime, DateTimeKind kind)
        {
            if (dateTime.HasValue) DateTime.SpecifyKind(dateTime.Value, kind);

            return dateTime;
        }
    }
}