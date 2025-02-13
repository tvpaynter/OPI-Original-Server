namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A class representing a RegEx look-around assertion node. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.RegexNode" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public abstract class RegexLookaroundAssertionNode : RegexNode
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.RegexLookaroundAssertionNode class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Specifies the pattern. </param>
        /// <param name="min">              (Optional) The minimum. </param>
        /// <param name="max">              (Optional) The maximum. </param>
        /// <param name="quantifierOption"> (Optional) The quantifier option. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected RegexLookaroundAssertionNode(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.RegexLookaroundAssertionNode class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        The inner node. </param>
        /// <param name="min">              (Optional) The minimum number of occurrences. </param>
        /// <param name="max">              (Optional) The maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) The quantifier option. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected RegexLookaroundAssertionNode(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
        }
    }
}