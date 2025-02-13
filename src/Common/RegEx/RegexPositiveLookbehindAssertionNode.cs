namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A class which represents a RegEx positive look-behind assertion node. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.RegexLookaroundAssertionNode" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexPositiveLookbehindAssertionNode : RegexLookaroundAssertionNode
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexPositiveLookbehindAssertionNode" />.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexPositiveLookbehindAssertionNode.RegexPositiveLookbehindAssertionNode(string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexPositiveLookbehindAssertionNode(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexPositiveLookbehindAssertionNode" />, and include
        ///     another <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexPositiveLookbehindAssertionNode.RegexPositiveLookbehindAssertionNode(RegexNode,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexPositiveLookbehindAssertionNode(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
        }
    }
}