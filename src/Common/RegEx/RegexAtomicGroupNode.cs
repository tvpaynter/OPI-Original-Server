namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A class which represents a RegEx atomic group node. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.RegexNode" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexAtomicGroupNode : RegexNode
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initialize an instance of <see cref="RegexAtomicGroupNode" />. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of characters which may occur. </param>
        /// <param name="max">              (Optional) Optional maximum number of characters which may occur. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexAtomicGroupNode.RegexAtomicGroupNode(string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexAtomicGroupNode(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexAtomicGroupNode" />, and include another
        ///     <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of characters which may occur. </param>
        /// <param name="max">              (Optional) Optional maximum number of characters which may occur. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexAtomicGroupNode.RegexAtomicGroupNode(RegexNode,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexAtomicGroupNode(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
        }
    }
}