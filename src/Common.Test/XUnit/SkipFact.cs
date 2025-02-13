////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="SkipFact.cs" company="StatementIQ.com">
// Copyright (c) 2020 StatementIQ.com. All rights reserved.
// </copyright>
// <author>StatementIQ</author>
// <date>5/15/2020</date>
// <summary>Implements the skip fact class</summary>
////////////////////////////////////////////////////////////////////////////////////////////////////

#nullable enable

namespace StatementIQ.Common.Test.XUnit
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Static methods for dynamically skipping tests identified with the
    ///     <see cref="SkippableFactAttribute" />.
    /// </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class SkipFact
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Throws an exception that results in a "Skipped" result for the test. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <exception cref="SkipException">    Thrown when a Skip error condition occurs. </exception>
        /// <param name="condition">
        ///     The condition that must evaluate to <c>true</c> for the test to
        ///     be skipped.
        /// </param>
        /// <param name="reason">       (Optional) The explanation for why the test is skipped. </param>
        /// <seealso cref="M:StatementIQ.Common.Test.XUnit.SkipFact.If(bool,string?)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void If(bool condition, string? reason = null)
        {
            if (condition) throw new SkipException(reason);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Throws an exception that results in a "Skipped" result for the test. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <param name="condition">
        ///     The condition that must evaluate to <c>false</c> for the test to
        ///     be skipped.
        /// </param>
        /// <param name="reason">       (Optional) The explanation for why the test is skipped. </param>
        /// <seealso cref="M:StatementIQ.Common.Test.XUnit.SkipFact.IfNot(bool,string?)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void IfNot(bool condition, string? reason = null)
        {
            If(!condition, reason);
        }
    }
}