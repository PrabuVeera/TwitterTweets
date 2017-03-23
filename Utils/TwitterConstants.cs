using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterTweets.Utils
{
    /// <summary>
    /// Contains the keys used to access the twitter api
    /// </summary>
    public static class TwitterConstants
    {
        // The keys to be used
        private const string OAuthConsumerKey = "gKjQjMgASK3PPNi1xd38HMidV";
        private const string OAuthConsumerSecret = "RtUyuTui56mZEMvrU9rZsJtZ1vqc1OuNOvtNscWS0sZB2yn7Bv";

        // Authentication Related Constants
        /// <summary>
        /// URL to be used get the oauth token
        /// </summary>
        public const string AUTHENTICATION_API_URL = "https://api.twitter.com/oauth2/token";

        /// <summary>
        /// URL used to get the tweets of the user.. Has to be formatted
        /// </summary>
        public const string TWEETS_GET_UNFORMATTED_URL = "https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1";

        /// <summary>
        /// Teh format of the date time string returned from twitter
        /// </summary>
        public const string TWEET_DATE_TIME_FORMAT = "ddd MMM dd HH:mm:ss zzz yyyy";


        #region GetBase64CredentialsForTwitterFromKey()
        /// <summary>
        /// Helper method to get the crendtials to be put in the 
        /// authentication header to get the access token from Twitter
        /// </summary>
        /// <returns></returns>
        public static string GetBase64CredentialsForTwitterFromKey()
        {
            //
            // Key:Secret should be base 64 encoded to make it work
            //
            string credential = string.Format("{0}:{1}", OAuthConsumerKey, OAuthConsumerSecret);

            //
            // Get the 
            //
            byte[] retValInBytes = System.Text.Encoding.UTF8.GetBytes(credential);
            string base64EncodedCredential = System.Convert.ToBase64String(retValInBytes);

            return base64EncodedCredential;
        }
        #endregion GetBase64CredentialsForTwitterFromKey()
    }
}