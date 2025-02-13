////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="SkippableTestMessageBus.cs" company="StatementIQ.com">
// Copyright (c) 2020 StatementIQ.com. All rights reserved.
// </copyright>
// <author>StatementIQ</author>
// <date>5/15/2020</date>
// <summary>Implements the skippable test message bus class</summary>
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Linq;
using Microsoft;
using Xunit.Abstractions;
using Xunit.Sdk;

#nullable enable

namespace StatementIQ.Common.Test.XUnit
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Intercepts test results on the message bus and re-interprets
    ///     <see cref="SkipException" /> as a <see cref="TestSkipped" /> result.
    /// </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    /// <seealso cref="T:Xunit.Sdk.IMessageBus" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SkippableTestMessageBus : IMessageBus
    {
        /// <summary>   The original message bus to which all messages should be forwarded. </summary>
        private readonly IMessageBus inner;

        /// <summary>   True if is disposed, false if not. </summary>
        private bool isDisposed;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableTestMessageBus" /> class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <param name="inner">
        ///     The original message bus to which all messages should
        ///     be forwarded.
        /// </param>
        /// <param name="skippingExceptionNames">
        ///     An array of the full names of the exception types
        ///     which should be interpreted as a skipped test-.
        /// </param>
        /// <seealso cref="M:Xunit.Sdk.SkippableTestMessageBus.SkippableTestMessageBus(IMessageBus,string[])" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public SkippableTestMessageBus(IMessageBus inner, string[] skippingExceptionNames)
        {
            Requires.NotNull(inner, nameof(inner));
            Requires.NotNull(skippingExceptionNames, nameof(skippingExceptionNames));

            this.inner = inner;
            SkippingExceptionNames = skippingExceptionNames;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the number of tests that have been dynamically skipped. </summary>
        /// <value> The number of skipped. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int SkippedCount { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets an array of full names to exception types that should be interpreted as a skip result.
        /// </summary>
        /// <value> A list of names of the skipping exceptions. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal string[] SkippingExceptionNames { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Disposes the inner message bus. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <seealso cref="M:Xunit.Sdk.SkippableTestMessageBus.Dispose()" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public bool QueueMessage(IMessageSinkMessage message)
        {
            if (message is TestFailed failed)
            {
                var outerException = failed.ExceptionTypes.FirstOrDefault();
                var skipTest = false;
                string? skipReason = null;

                switch (outerException)
                {
                    case string _ when ShouldSkipException(outerException):
                        skipTest = true;
                        skipReason = failed.Messages?.FirstOrDefault();
                        break;
                    case "Xunit.Sdk.ThrowsException" when failed.ExceptionTypes.Length > 1:
                        outerException = failed.ExceptionTypes[1];
                        if (ShouldSkipException(outerException))
                        {
                            skipTest = true;
                            skipReason = failed.Messages?.Length > 1 ? failed.Messages[1] : null;
                        }

                        break;
                }

                if (skipTest)
                {
                    SkippedCount++;
                    return inner.QueueMessage(new TestSkipped(failed.Test, skipReason));
                }
            }

            return inner.QueueMessage(message);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The bulk of the clean-up code is implemented in Dispose(bool). </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <param name="disposing">    If the managed resources should be disposed. </param>
        /// <seealso cref="M:Xunit.Sdk.SkippableTestMessageBus.Dispose(bool)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing) inner.Dispose();

            isDisposed = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determine if we should skip the exception. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <param name="exceptionType">    Type of the exception. </param>
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool ShouldSkipException(string exceptionType)
        {
            return Array.IndexOf(SkippingExceptionNames, exceptionType) >= 0;
        }
    }
}