////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="SkippableFactTestCase.cs" company="StatementIQ.com">
// Copyright (c) 2020 StatementIQ.com. All rights reserved.
// </copyright>
// <author>StatementIQ</author>
// <date>5/15/2020</date>
// <summary>Implements the skippable fact test case class</summary>
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft;
using Xunit.Abstractions;
using Xunit.Sdk;

#nullable enable
namespace StatementIQ.Common.Test.XUnit
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     A test case that interprets <see cref="SkipException" /> as a <see cref="TestSkipped" /> result.
    /// </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    /// <seealso cref="T:Xunit.Sdk.XunitTestCase" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SkippableFactTestCase : XunitTestCase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableFactTestCase" /> class,
        ///     to be called only by the deserializer.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete]
#pragma warning disable CS8618
        public SkippableFactTestCase()
#pragma warning restore CS8618
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableFactTestCase" /> class.
        /// </summary>
        /// <param name="skippingExceptionNames">
        ///     An array of the full names of the exception types
        ///     which should be interpreted as a skipped test-.
        /// </param>
        /// <param name="diagnosticMessageSink">    The diagnostic message sink. </param>
        /// <param name="defaultMethodDisplay">     The preferred test name derivation. </param>
        /// <param name="testMethod">               The test method. </param>
        /// <param name="testMethodArguments">      The test method arguments. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public SkippableFactTestCase(string[] skippingExceptionNames, IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod, object[]? testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, TestMethodDisplayOptions.None, testMethod,
                testMethodArguments)
        {
            Requires.NotNull(skippingExceptionNames, nameof(skippingExceptionNames));
            SkippingExceptionNames = skippingExceptionNames;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableFactTestCase" /> class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <param name="skippingExceptionNames">
        ///     An array of the full names of the exception types
        ///     which should be interpreted as a skipped test-.
        /// </param>
        /// <param name="diagnosticMessageSink">        The diagnostic message sink. </param>
        /// <param name="defaultMethodDisplay">         The preferred test name derivation. </param>
        /// <param name="defaultMethodDisplayOptions">
        ///     Default method display options to use (when not
        ///     customized).
        /// </param>
        /// <param name="testMethod">                   The test method. </param>
        /// <param name="testMethodArguments">          (Optional) The test method arguments. </param>
        /// <seealso
        ///     cref="M:Xunit.Sdk.SkippableFactTestCase.SkippableFactTestCase(string[],IMessageSink,TestMethodDisplay,TestMethodDisplayOptions,ITestMethod,object[]?)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public SkippableFactTestCase(string[] skippingExceptionNames, IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay, TestMethodDisplayOptions defaultMethodDisplayOptions,
            ITestMethod testMethod, object[]? testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testMethod,
                testMethodArguments)
        {
            Requires.NotNull(skippingExceptionNames, nameof(skippingExceptionNames));
            SkippingExceptionNames = skippingExceptionNames;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets an array of full names to exception types that should be interpreted as a skip result.
        /// </summary>
        /// <value> A list of names of the skipping exceptions. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal string[] SkippingExceptionNames { get; private set; }

        /// <inheritdoc />
        public override async Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink, IMessageBus messageBus,
            object[] constructorArguments, ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
        {
            var messageBusInterceptor = new SkippableTestMessageBus(messageBus, SkippingExceptionNames);
            var result = await base.RunAsync(diagnosticMessageSink, messageBusInterceptor, constructorArguments,
                aggregator, cancellationTokenSource).ConfigureAwait(false);
            result.Failed -= messageBusInterceptor.SkippedCount;
            result.Skipped += messageBusInterceptor.SkippedCount;
            return result;
        }

        /// <inheritdoc />
        public override void Serialize(IXunitSerializationInfo data)
        {
            Requires.NotNull(data, nameof(data));
            base.Serialize(data);
            data.AddValue(nameof(SkippingExceptionNames), SkippingExceptionNames);
        }

        /// <inheritdoc />
        public override void Deserialize(IXunitSerializationInfo data)
        {
            Requires.NotNull(data, nameof(data));
            base.Deserialize(data);
            SkippingExceptionNames = data.GetValue<string[]>(nameof(SkippingExceptionNames));
        }
    }
}