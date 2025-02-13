using Xunit;

namespace StatementIQ.Common.Test
{
    public class DistanceTests
    {
        [Fact]
        public void Should_Calculate_Distance_Between_Two_Points_In_Kilometers()
        {
            var lat1 = 90;
            var lon1 = 10;

            var lat2 = -90;
            var lon2 = -10;

            var result = Distance.Calculate(lat1, lon1, lat2, lon2, Distance.Unit.Kilometers);

            Assert.True(result > 0);
        }

        [Fact]
        public void Should_Calculate_Distance_Between_Two_Points_In_Miles()
        {
            var lat1 = 90;
            var lon1 = 10;

            var lat2 = -90;
            var lon2 = -10;

            var result = Distance.Calculate(lat1, lon1, lat2, lon2, Distance.Unit.Miles);

            Assert.True(result > 0);
        }
    }
}