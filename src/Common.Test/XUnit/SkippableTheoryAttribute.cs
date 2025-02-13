////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright file="SkippableTheoryAttribute.cs" company="StatementIQ.com">
// Copyright (c) 2020 StatementIQ.com. All rights reserved.
// </copyright>
// <author>StatementIQ</author>
// <date>5/15/2020</date>
// <summary>Implements the skippable theory attribute class</summary>
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Xunit;
using Xunit.Sdk;

namespace StatementIQ.Common.Test.XUnit
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Marks a test method as being a data theory. Data theories are tests which are fed various
    ///     bits of data from a data source, mapping to parameters on the test method. If the data source
    ///     contains multiple rows, then the test method is executed multiple times (once with each data
    ///     row). Data is provided by attributes which derive from Xunit.Sdk.DataAttribute (notably,
    ///     Xunit.InlineDataAttribute and Xunit.MemberDataAttribute). The test may produce a "skipped
    ///     test" result by calling
    ///     <see cref="SkipFact.If(bool, string)" /> or otherwise throwing a <see cref="SkipException" />.
    /// </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    /// <seealso cref="T:Xunit.TheoryAttribute" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [AttributeUsage(AttributeTargets.Method)]
    [XunitTestCaseDiscoverer("Xunit.Sdk.SkippableTheoryDiscoverer", "xunit.execution")]
    public class SkippableTheoryAttribute : TheoryAttribute
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableTheoryAttribute" /> class.
        /// </summary>
        /// <param name="skippingExceptions">
        ///     Exception types that, if thrown, should cause the test to register as skipped.
        /// </param>
        /// <seealso cref="P:Xunit.SkippableTheoryAttribute.CA1801" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma warning disable CA1801
        public SkippableTheoryAttribute(params Type[] skippingExceptions)
#pragma warning restore CA1801
        {
        }
    }
}