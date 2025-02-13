using System;
using System.Text;
using MandateThat;

namespace StatementIQ.RegEx
{
    /// <summary>   A RegEx extension class. </summary>
    public static class RegexExtensions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A StringBuilder extension method that replaces many values. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have
        ///     unsupported or illegal values.
        /// </exception>
        /// <param name="builder">      The builder to act on. </param>
        /// <param name="oldValues">    The old values. </param>
        /// <param name="newValues">    The new values. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void ReplaceMany(this StringBuilder builder, string[] oldValues, string[] newValues)
        {
            Mandate.That(builder, nameof(builder)).IsNotNull();

            if (oldValues == null || newValues == null)
                throw new ArgumentNullException
                (
                    oldValues == null ? "oldValues" : "newValues",
                    "Search and replacement arrays should both be not-null."
                );

            if (oldValues.Length != newValues.Length)
                throw new ArgumentException("Search and replacement arrays should have equal lengths.");

            for (var i = 0; i < oldValues.Length; i++) builder.Replace(oldValues[i], newValues[i]);
        }
    }
}