
namespace StatementIQ.RegEx
{
    using MandateThat;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A class that represents a RegEx node. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexNode
    {
        /// <summary>   The inner node. </summary>
        private RegexNode _innerNode;

        /// <summary>   Specifies the pattern. </summary>
        private string _pattern;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initialize an instance of <see cref="RegexNode" />. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexNode.RegexNode(string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexNode(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            SetPattern(pattern);
            Minimum = min;
            Maximum = max;
            RegexQuantifierOption = quantifierOption;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexNode" />, and include another
        ///     <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexNode.RegexNode(RegexNode,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexNode(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            SetInnerNode(innerNode);
            Minimum = min;
            Maximum = max;
            RegexQuantifierOption = quantifierOption;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determine if the inner node is included. </summary>
        /// <value> True if this  is inner node included, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsInnerNodeIncluded { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   The included inner node. </summary>
        /// <exception cref="NotSupportedException">
        ///     Thrown when the requested operation is not
        ///     supported.
        /// </exception>
        /// <value> The inner node. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RegexNode InnerNode
        {
            get
            {
                Mandate.That(IsInnerNodeIncluded);
                return _innerNode;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Normal regex pattern. </summary>
        /// <exception cref="NotSupportedException">
        ///     Thrown when the requested operation is not
        ///     supported.
        /// </exception>
        /// <value> The pattern. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Pattern
        {
            get
            {
                Mandate.That(IsInnerNodeIncluded);
                return _pattern;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Minimum number of occurrences. </summary>
        /// <value> The minimum value. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int? Minimum { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Maximum number of occurrences. </summary>
        /// <value> The maximum value. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int? Maximum { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Quantifier options, such as Greedy, Lazy, or Possessive. </summary>
        /// <value> The RegEx quantifier option. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RegexQuantifierOption RegexQuantifierOption { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Set this <see cref="RegexNode" /> to include an inner node. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="value" /> cannot be the same as
        ///     <see cref="this" />.
        /// </exception>
        /// <param name="value">    An existing <see cref="RegexNode" /> to be included. </param>
        /// <returns>
        ///     <see cref="RegexNode" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexNode.SetInnerNode(RegexNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexNode SetInnerNode(RegexNode value)
        {
            Mandate.That(value != this);

            IsInnerNodeIncluded = true;
            _pattern = null;
            _innerNode = value;

            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Set this <see cref="RegexNode" /> to use a pattern string. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="value">    Pattern string. </param>
        /// <returns>
        ///     <see cref="RegexNode" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexNode.SetPattern(string)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexNode SetPattern(string value)
        {
            IsInnerNodeIncluded = false;
            _innerNode = null;
            _pattern = value;

            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Convert this  into a string representation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="stringifier">  The stringifier. </param>
        /// <returns>   A string that represents this. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToString(IRegexStringifier stringifier)
        {
            Mandate.That(stringifier != null);
            return stringifier.ToString(this);
        }
    }
}