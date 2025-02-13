////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="SkippableFactDiscoverer.cs" company="StatementIQ.com">
// Copyright (c) 2020 StatementIQ.com. All rights reserved.
// </copyright>
// <author>StatementIQ</author>
// <date>5/15/2020</date>
// <summary>Implements the skippable fact discoverer class</summary>
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft;
using Xunit.Abstractions;
using Xunit.Sdk;

#nullable enable

namespace StatementIQ.Common.Test.XUnit
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Transforms <see cref="SkippableFactAttribute" /> test methods into test cases.
    /// </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    /// <seealso cref="T:Xunit.Sdk.IXunitTestCaseDiscoverer" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SkippableFactDiscoverer : IXunitTestCaseDiscoverer
    {
        /// <summary>   The diagnostic message sink provided to the constructor. </summary>
        private readonly IMessageSink diagnosticMessageSink;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableFactDiscoverer" /> class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <param name="diagnosticMessageSink">    The message sink used to send diagnostic messages. </param>
        /// <seealso cref="M:StatementIQ.Common.Test.XUnit.SkippableFactDiscoverer.SkippableFactDiscoverer(IMessageSink)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public SkippableFactDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        /// <inheritdoc />
        public virtual IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            Requires.NotNull(factAttribute, nameof(factAttribute));
            string[] skippingExceptionNames = GetSkippableExceptionNames(factAttribute);
            yield return new SkippableFactTestCase(skippingExceptionNames, diagnosticMessageSink,
                discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(),
                testMethod);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Translates the types of exceptions that should be considered as "skip" exceptions into their
        ///     full names.
        /// </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <param name="factAttribute">    The <see cref="SkippableFactAttribute" />. </param>
        /// <returns>   An array of full names of types. </returns>
        /// <seealso cref="M:StatementIQ.Common.Test.XUnit.SkippableFactDiscoverer.GetSkippableExceptionNames(IAttributeInfo)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string[] GetSkippableExceptionNames(IAttributeInfo factAttribute)
        {
            Requires.NotNull(factAttribute, nameof(factAttribute));
            var firstArgument = (object[]) factAttribute.GetConstructorArguments().FirstOrDefault();
            var skippingExceptions = firstArgument?.Cast<Type>().ToArray() ?? Type.EmptyTypes;
            Array.Resize(ref skippingExceptions, skippingExceptions.Length + 1);
            skippingExceptions[skippingExceptions.Length - 1] = typeof(SkipException);

            var skippingExceptionNames = skippingExceptions.Select(ex => ex.FullName).ToArray();
            return skippingExceptionNames!;
        }
    }
}