namespace StatementIQ.RegEx
{
    /// <summary>   A RegEx meta characters. </summary>
    public static class RegexMetaChars
    {
        /// <summary>   any character. </summary>
        public const string AnyCharacter = ".";

        /// <summary>   The line start. </summary>
        public const string LineStart = "^";

        /// <summary>   The line end. </summary>
        public const string LineEnd = "$";

        /// <summary>   The string start. </summary>
        public const string StringStart = "\\A";

        /// <summary>   The word boundary. </summary>
        public const string WordBoundary = "\\b";

        /// <summary>   The non word boundary. </summary>
        public const string NonWordBoundary = "\\B";

        /// <summary>   The digit. </summary>
        public const string Digit = "\\d";

        /// <summary>   The non digit. </summary>
        public const string NonDigit = "\\D";

        /// <summary>   The escape. </summary>
        public const string Escape = "\\e";

        /// <summary>   The form feed. </summary>
        public const string FormFeed = "\\f";

        /// <summary>   A match specifying the consecutive. </summary>
        public const string ConsecutiveMatch = "\\G";

        /// <summary>   The new line. </summary>
        public const string NewLine = "\\n";

        /// <summary>   The carriage return. </summary>
        public const string CarriageReturn = "\\r";

        /// <summary>   The white space. </summary>
        public const string WhiteSpace = "\\s";

        /// <summary>   The nonwhite space. </summary>
        public const string NonwhiteSpace = "\\S";

        /// <summary>   The tab. </summary>
        public const string Tab = "\\t";

        /// <summary>   The vertical tab. </summary>
        public const string VerticalTab = "\\v";

        /// <summary>   The word character. </summary>
        public const string WordCharacter = "\\w";

        /// <summary>   The non word character. </summary>
        public const string NonWordCharacter = "\\W";

        /// <summary>   The string end. </summary>
        public const string StringEnd = "\\Z";
    }
}