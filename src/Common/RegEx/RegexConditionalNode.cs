namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A RegEx conditional node. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.RegexNode" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexConditionalNode : RegexNode
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initialize an instance of <see cref="RegexConditionalNode" />. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="trueValue">        Value of successful match. </param>
        /// <param name="falseValue">       Value of failed match. </param>
        /// <param name="nameOrNumber">     (Optional) Name or number for backtracking. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences which could occur. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences which could occur. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexConditionalNode.RegexConditionalNode(string,string,string,string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexConditionalNode(string pattern, string trueValue, string falseValue, string nameOrNumber = null,
            int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
            NameOrNumber = nameOrNumber;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexConditionalNode" />, and include another
        ///     <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="trueValue">        Value of successful match. </param>
        /// <param name="falseValue">       Value of failed match. </param>
        /// <param name="nameOrNumber">     (Optional) Name or number for backtracking. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexConditionalNode.RegexConditionalNode(RegexNode,string,string,string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexConditionalNode(RegexNode innerNode, string trueValue, string falseValue,
            string nameOrNumber = null, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
            NameOrNumber = nameOrNumber;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the value which represents a True value. </summary>
        /// <value> The true value. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string TrueValue { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the value which represents a False value. </summary>
        /// <value> The false value. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string FalseValue { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name or number. </summary>
        /// <value> The name or number. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string NameOrNumber { get; set; }
    }
}