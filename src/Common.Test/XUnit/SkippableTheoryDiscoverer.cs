////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="SkippableTheoryDiscoverer.cs" company="StatementIQ.com">
// Copyright (c) 2020 StatementIQ.com. All rights reserved.
// </copyright>
// <author>StatementIQ</author>
// <date>5/15/2020</date>
// <summary>Implements the skippable theory discoverer class</summary>
////////////////////////////////////////////////////////////////////////////////////////////////////


#nullable enable
namespace StatementIQ.Common.Test.XUnit
{
    using System.Collections.Generic;
    using Microsoft;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Transforms <see cref="SkippableTheoryAttribute" /> test theories into test cases.
    /// </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    /// <seealso cref="T:Xunit.Sdk.IXunitTestCaseDiscoverer" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SkippableTheoryDiscoverer : IXunitTestCaseDiscoverer
    {
        /// <summary>   The diagnostic message sink provided to the constructor. </summary>
        private readonly IMessageSink diagnosticMessageSink;

        /// <summary>   The complex theory discovery process that we wrap. </summary>
        private readonly TheoryDiscoverer theoryDiscoverer;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableTheoryDiscoverer" /> class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <param name="diagnosticMessageSink">    The message sink used to send diagnostic messages. </param>
        /// <seealso cref="M:Xunit.Sdk.SkippableTheoryDiscoverer.SkippableTheoryDiscoverer(IMessageSink)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public SkippableTheoryDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
            theoryDiscoverer = new TheoryDiscoverer(diagnosticMessageSink);
        }

        /// <inheritdoc />
        public virtual IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            Requires.NotNull(factAttribute, nameof(factAttribute));
            string[] skippingExceptionNames = SkippableFactDiscoverer.GetSkippableExceptionNames(factAttribute);
            var defaultMethodDisplay = discoveryOptions.MethodDisplayOrDefault();

            IEnumerable<IXunitTestCase>? basis = theoryDiscoverer.Discover(discoveryOptions, testMethod, factAttribute);
            foreach (var testCase in basis)
            {
                if (testCase is XunitTheoryTestCase)
                {
                    yield return new SkippableTheoryTestCase(skippingExceptionNames, diagnosticMessageSink,
                        defaultMethodDisplay, discoveryOptions.MethodDisplayOptionsOrDefault(), testCase.TestMethod);
                }
                else
                {
                    yield return new SkippableFactTestCase(skippingExceptionNames, diagnosticMessageSink,
                        defaultMethodDisplay, discoveryOptions.MethodDisplayOptionsOrDefault(), testCase.TestMethod,
                        testCase.TestMethodArguments);
                }
            }
        }
    }
}