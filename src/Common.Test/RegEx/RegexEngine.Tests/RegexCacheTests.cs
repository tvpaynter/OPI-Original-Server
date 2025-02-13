using Moq;
using StatementIQ.RegEx.RegexEngine;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A RegEx cache tests. </summary>
    public class RegexCacheTests
    {
        /// <summary>   The mock repository. </summary>
        private MockRepository mockRepository;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.RegexEngine.Tests.RegexCacheTests
        ///     class.
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexCacheTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates RegEx cache. </summary>
        /// <returns>   The new RegEx cache. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private RegexCache CreateRegexCache()
        {
            return new RegexCache();
        }
    }
}