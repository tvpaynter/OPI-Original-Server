﻿////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="SkippableTheoryTestCase.cs" company="StatementIQ.com">
// Copyright (c) 2020 StatementIQ.com. All rights reserved.
// </copyright>
// <author>StatementIQ</author>
// <date>5/15/2020</date>
// <summary>Implements the skippable theory test case class</summary>
////////////////////////////////////////////////////////////////////////////////////////////////////

#nullable enable
namespace StatementIQ.Common.Test.XUnit
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft;
    using Xunit.Abstractions;
    using Xunit.Sdk;
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A theory test case that will wrap the message bus. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    /// <seealso cref="T:Xunit.Sdk.XunitTheoryTestCase" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SkippableTheoryTestCase : XunitTheoryTestCase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableTheoryTestCase" /> class,
        ///     to be called only by the deserializer.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer", true)]
#pragma warning disable CS8618
        public SkippableTheoryTestCase()
#pragma warning restore CS8618
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableTheoryTestCase" /> class.
        /// </summary>
        /// <value> The net 45. </value>
        /// <seealso cref="P:Xunit.Sdk.SkippableTheoryTestCase.NET45" />
        /// ###
        /// <param name="skippingExceptionNames">
        ///     An array of the full names of the exception types
        ///     which should be interpreted as a skipped test-.
        /// </param>
        /// ###
        /// <param name="diagnosticMessageSink">    The diagnostic message sink. </param>
        /// ###
        /// <param name="defaultMethodDisplay">     The preferred test name derivation. </param>
        /// ###
        /// <param name="testMethod">               The test method. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public SkippableTheoryTestCase(string[] skippingExceptionNames, IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod)
            : base(diagnosticMessageSink, defaultMethodDisplay, TestMethodDisplayOptions.None, testMethod)
        {
            Requires.NotNull(skippingExceptionNames, nameof(skippingExceptionNames));
            SkippingExceptionNames = skippingExceptionNames;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableTheoryTestCase" /> class.
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
        /// <seealso
        ///     cref="M:Xunit.Sdk.SkippableTheoryTestCase.SkippableTheoryTestCase(string[],IMessageSink,TestMethodDisplay,TestMethodDisplayOptions,ITestMethod)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public SkippableTheoryTestCase(string[] skippingExceptionNames, IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay, TestMethodDisplayOptions defaultMethodDisplayOptions,
            ITestMethod testMethod)
            : base(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testMethod)
        {
            Requires.NotNull(skippingExceptionNames, nameof(skippingExceptionNames));
            SkippingExceptionNames = skippingExceptionNames;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets an array of the full names of the exception types which should be interpreted as a
        ///     skipped test.
        /// </summary>
        /// <value> A list of names of the skipping exceptions. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal string[] SkippingExceptionNames { get; private set; }

        /// <inheritdoc />
        public override async Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink, IMessageBus messageBus,
            object[] constructorArguments, ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
        {
            Requires.NotNull(diagnosticMessageSink, nameof(diagnosticMessageSink));
            Requires.NotNull(messageBus, nameof(messageBus));
            Requires.NotNull(constructorArguments, nameof(constructorArguments));
            Requires.NotNull(aggregator, nameof(aggregator));
            Requires.NotNull(cancellationTokenSource, nameof(cancellationTokenSource));

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