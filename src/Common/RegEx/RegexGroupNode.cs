namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A class representing a RegEx group node. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.RegexNode" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexGroupNode
        : RegexNode
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initialize an instance of <see cref="RegexGroupNode" />. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="capturing">        (Optional) Defines the group to be capturing or not. </param>
        /// <param name="name">             (Optional) Optional name of capture group. </param>
        /// <param name="options">          (Optional) Optional inline options for the group. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGroupNode.RegexGroupNode(string,bool,string,string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGroupNode(string pattern, bool capturing = true, string name = null, string options = null,
            int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(pattern, min, max, quantifierOption)
        {
            SetName(name);
            SetIsCapturingGroup(capturing);
            Options = options;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexGroupNode" />, and include another
        ///     <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="capturing">        (Optional) Defines the group to be capturing or not. </param>
        /// <param name="name">             (Optional) Optional name of capture group. </param>
        /// <param name="options">          (Optional) Optional inline options for the group. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGroupNode.RegexGroupNode(RegexNode,bool,string,string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGroupNode(RegexNode innerNode, bool capturing = true, string name = null, string options = null,
            int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(innerNode, min, max, quantifierOption)
        {
            SetName(name);
            SetIsCapturingGroup(capturing);
            Options = options;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the capturing group option is allowed. </summary>
        /// <value> True if this  is capturing group, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsCapturingGroup { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the name. </summary>
        /// <value> The name. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Name { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets options for controlling the operation via Options. </summary>
        /// <value> The options. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Options { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Set if <see cref="RegexGroupNode" /> is capturing. </summary>
        /// <remarks>
        ///     If <see cref="Name" /> is not null, <paramref name="value" /> will be ignored.
        /// </remarks>
        /// <param name="value">    . </param>
        /// <returns>
        ///     <see cref="RegexGroupNode" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGroupNode.SetIsCapturingGroup(bool)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGroupNode SetIsCapturingGroup(bool value)
        {
            IsCapturingGroup = !string.IsNullOrWhiteSpace(Name) || value;
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Set name of capturing group. </summary>
        /// <remarks>   <see cref="IsCapturingGroup" /> is set to <see cref="true" />. </remarks>
        /// <param name="value">    . </param>
        /// <returns>
        ///     <see cref="RegexGroupNode" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGroupNode.SetName(string)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGroupNode SetName(string value)
        {
            IsCapturingGroup = value != null;
            Name = value;
            return this;
        }
    }
}