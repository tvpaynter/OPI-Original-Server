using System;
using StatementIQ.Helpers;
using Xunit;

namespace StatementIQ.Common.Test.Helpers
{
    public class AssemblyHelperTests
    {
        [Fact]
        public void Should_Return_LoadAssembliesToCurrentDomain()
        {
            AssemblyHelper.LoadAssembliesToCurrentDomain();

            Assert.True(AppDomain.CurrentDomain.GetAssemblies().Length > 0);
        }
    }
}