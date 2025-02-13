using Xunit;

namespace StatementIQ.Common.Test
{
    public class SingletonTests
    {
        public class SingletonTester : Singleton<SingletonTester>
        {
            protected SingletonTester()
            {
            }
        }

        [Fact]
        public void Should_Singleton_For_Instance()
        {
            var instance1 = SingletonTester.Instance;
            var instance2 = SingletonTester.Instance;

            Assert.Equal(instance1, instance2);
        }
    }
}