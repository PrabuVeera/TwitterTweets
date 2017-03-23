using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterTweets.Models
{
    public class TwitterSearchResult
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public const string PARAM_NAME = "SearchResult";


        /// <summary>
        /// All tweets
        /// </summary>
        public Tweet[] Tweets { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TwitterSearchResult()
        {
            Tweets = new Tweet[] { };
            ErrorMessage = null;
        }
    }
}