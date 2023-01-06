using System.Collections.Generic;

namespace GoReactive.Services
{
    /// <summary>
    /// Search results from query 
    /// </summary>
    public class DuckDuckGoSearchResult 
    {
        /// <summary>
        /// Topic summary containing HTML
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        /// Topic summary containing no HTML
        /// </summary>
        public string AbstractText { get; set; }

        /// <summary>
        /// Type of Answer, e.g. calc, color, digest, info, ip, iploc, phone, pw, rand, regexp, unicode, upc, or zip (see goodies & tech pages for examples).
        /// </summary>
        public string AnswerType { get; set; }

        /// <summary>
        /// Name of Abstract Source
        /// </summary>
        public string AbstractSource { get; set; }

        /// <summary>
        /// Dictionary definition (may differ from Abstract)
        /// </summary>
        public string Definition { get; set; }

        /// <summary>
        /// Name of Definition source
        /// </summary>
        public string DefinitionSource { get; set; }

        /// <summary>
        /// Name of topic that goes with Abstract
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Link to image that goes with Abstract
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Array of internal links to related topics associated with Abstract
        /// </summary>
        public List<DuckDuckGoQueryResult> RelatedTopics { get; set; }

        /// <summary>
        /// Response category, i.e. A (article), D (disambiguation), C (category), N (name), E (exclusive), or nothing.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// !bang redirect URL
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// Deep link to expanded definition page in DefinitionSource
        /// </summary>
        public string DefinitionUrl { get; set; }

        /// <summary>
        /// Instant answer
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Array of external links associated with Abstract
        /// </summary>
        public List<DuckDuckGoQueryResult> Results { get; set; }

        /// <summary>
        /// Deep link to the expanded topic page in AbstractSource
        /// </summary>
        public string AbstractUrl { get; set; }
    }
}