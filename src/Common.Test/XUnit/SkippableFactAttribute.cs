using System;
using Xunit;
using Xunit.Sdk;

namespace StatementIQ.Common.Test.XUnit
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Attribute that is applied to a method to indicate that it is a fact that should be run by the
    ///     test runner. The test may produce a "skipped test" result by calling
    ///     <see cref="SkipFact.If(bool, string)" /> or otherwise throwing a <see cref="SkipException" />.
    /// </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    /// <seealso cref="T:Xunit.FactAttribute" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [AttributeUsage(AttributeTargets.Method)]
    [XunitTestCaseDiscoverer("Xunit.Sdk.SkippableFactDiscoverer", "xunit.execution")]
    public class SkippableFactAttribute : FactAttribute
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the <see cref="SkippableFactAttribute" /> class.
        /// </summary>
        /// <param name="skippingExceptions">
        ///     Exception types that, if thrown, should cause the test to register as skipped.
        /// </param>
        /// <seealso cref="P:Xunit.SkippableFactAttribute.CA1801" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma warning disable CA1801
        public SkippableFactAttribute(params Type[] skippingExceptions)
#pragma warning restore CA1801
        {
        }
    }
}