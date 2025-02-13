using System;

namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A class which represents a RegEx unicode category node. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.RegexNode" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexUnicodeCategoryNode : RegexNode
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexUnicodeCategoryNode" /> with flag
        ///     <see cref="RegexUnicodeCategoryFlag" />.
        ///     <para>
        ///         Internally call <see cref="Enum.ToString()" />. Therefore, only use one value from
        ///         <see cref="RegexUnicodeCategoryFlag" /> while using this constructor;
        ///         Use <see cref="RegexUnicodeCategoryNode(string, bool, int?, int?, RegexQuantifierOption)" />
        ///         instead for named unicode block.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <code>
        /// (RegexUnicodeCategoryFlag.Lu | RegexUnicodeCategoryFlag.Ll).ToString() // will generate "Lu, Ll", which is a invalid form.
        /// </code>
        /// </remarks>
        /// <param name="unicodeCategory">  Value from <see cref="RegexUnicodeCategoryFlag" />. </param>
        /// <param name="negative">
        ///     (Optional) Optional value to determine if negate the
        ///     category.
        /// </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexUnicodeCategoryNode.RegexUnicodeCategoryNode(RegexUnicodeCategoryFlag,bool,int?,int?,RegexQuantifierOption)" />
        /// <seealso cref="RegexUnicodeCategoryNode(string, bool, int?, int?, RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexUnicodeCategoryNode(RegexUnicodeCategoryFlag unicodeCategory, bool negative = false,
            int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(unicodeCategory.ToString(), min, max, quantifierOption)
        {
            Negative = negative;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initialize an instance of <see cref="RegexUnicodeCategoryNode" /> with unicode designation or
        ///     name.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="unicodeDesignation">   Unicode designation or named block. </param>
        /// <param name="negative">
        ///     (Optional) Optional value to determine if negate the
        ///     category.
        /// </param>
        /// <param name="min">                  (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">                  (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption">     (Optional) Optional quantifier option. </param>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexUnicodeCategoryNode.RegexUnicodeCategoryNode(string,bool,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexUnicodeCategoryNode(string unicodeDesignation, bool negative = false, int? min = null,
            int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
            : base(unicodeDesignation, min, max, quantifierOption)
        {
            Negative = negative;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determine if category is negate. </summary>
        /// <value> True if negative, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool Negative { get; set; }
    }
}