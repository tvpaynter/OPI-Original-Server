namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A class that represents a RegEx negative look-behind assertion node. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.RegexLookaroundAssertionNode" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexNegativeLookbehindAssertionNode : RegexLookaroundAssertionNode
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexNegativeLookbehindAssertionNode" />.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexNegativeLookbehindAssertionNode.RegexNegativeLookbehindAssertionNode(string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexNegativeLookbehindAssertionNode(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexNegativeLookbehindAssertionNode" />, and include
        ///     another <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexNegativeLookbehindAssertionNode.RegexNegativeLookbehindAssertionNode(RegexNode,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexNegativeLookbehindAssertionNode(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
        }
    }
}