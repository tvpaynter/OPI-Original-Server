using System;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class ExceptionExtensionsTests
    {
        [Fact]
        public void Should_GetExceptionDetails_For_Standard_Exception()
        {
            var target = new ArgumentNullException("param");
            var result = target.GetExceptionDetails();

            Assert.False(string.IsNullOrEmpty(result));
        }
    }
}