using StatementIQ.Exceptions;
using Xunit;

namespace StatementIQ.Common.Test.Exceptions
{
    public class InvalidSystemClockExceptionTests
    {
        [Fact]
        public void Should_Create_InvalidSystemClockException_With_Message()
        {
            var message = "Invalid clock";
            var exception = new InvalidSystemClockException(message);

            Assert.Equal(message, exception.Message);
        }
    }
}