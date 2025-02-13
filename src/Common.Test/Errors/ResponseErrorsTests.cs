using StatementIQ.Errors;
using Xunit;

namespace StatementIQ.Common.Test.Errors
{
    public class ResponseErrorsTests
    {
        [Fact]
        public void Should_Add_Error()
        {
            var target = new ResponseErrors();
            target.Add("404", "Not found");

            Assert.False(target.IsEmpty);
        }
    }
}