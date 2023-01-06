namespace GoReactive.Services
{
    /// <summary>
    /// Result returned from the query
    /// </summary>
    public class DuckDuckGoQueryResult
    {
        /// <summary>
        /// HTML link(s) to related topic(s) or external site(s)
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Icon associated with related topic(s) or FirstUrl
        /// </summary>
        public DuckDuckGoIcon Icon { get; set; }

        /// <summary>
        /// First URL in Result
        /// </summary>
        public string FirstUrl { get; set; }

        /// <summary>
        /// Text from first URL
        /// </summary>
        public string Text { get; set; }
    }
}