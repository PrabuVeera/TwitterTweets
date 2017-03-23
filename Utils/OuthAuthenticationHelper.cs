using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TwitterTweets.Models;

namespace TwitterTweets.Utils
{
    /// <summary>
    /// Helper class to authenticate using OAuth2.0
    /// </summary>
    public static class OuthAuthenticationHelper
    {
        #region AccessTokenResult
        /// <summary>
        /// The access token result class
        /// </summary>
        public class AccessTokenResult 
        {
            /// <summary>
            /// The obtained access token to be used
            /// </summary>
            public OAuthAccessToken OAuthToken { get; set; }

            /// <summary>
            /// Error message if any
            /// </summary>
            public string ErroMessage { get; set; }

            /// <summary>
            /// Indciates if it has a valid access token
            /// </summary>
            public bool HasAuthToken
            {
                get
                {
                    return string.IsNullOrEmpty(ErroMessage) && OAuthToken != null && !string.IsNullOrEmpty(OAuthToken.Token_type)
                        && !string.IsNullOrEmpty(OAuthToken.Access_Token);
                }
            }

            public AccessTokenResult()
            {
                this.OAuthToken = null;
                this.ErroMessage = null;
            }
        }

        #endregion AccessTokenResult

        #region GetAccessToken()
        /// <summary>
        /// Gets the access token from twitter api using the credentials passed in
        /// </summary>
        /// <returns></returns>
        public static AccessTokenResult GetAccessToken()
        {
            AccessTokenResult accessTokenValue = new AccessTokenResult();

            /*
              post.Method = "POST";
            post.ContentType = "application/x-www-form-urlencoded";
            post.Headers[HttpRequestHeader.Authorization] = "Basic " + Base64Encode(credentials);
            var reqbody = Encoding.UTF8.GetBytes("grant_type=client_credentials");
             * */

            string authHeader = string.Format("Basic : {0}", TwitterConstants.GetBase64CredentialsForTwitterFromKey());

            var httpResult = HttpUtils.MakeHttpPostRequest(TwitterConstants.AUTHENTICATION_API_URL, authHeader, "application/x-www-form-urlencoded",
                "grant_type=client_credentials");

            //
            // If the status code is OK, parse the json and get the access token
            // 
            if (httpResult.ReplyStatusCode == System.Net.HttpStatusCode.OK)
            {
                JavaScriptSerializer serailizer = new JavaScriptSerializer();
                accessTokenValue.OAuthToken = serailizer.Deserialize<OAuthAccessToken>(httpResult.ResponseBody);
            }
            else
            {
                accessTokenValue.ErroMessage = string.Format("Access token cannot be retrieved. Obtained error code {0} from twitter with message {1}.", 
                    httpResult.ReplyStatusCode, httpResult.ResponseBody);
            }

            return accessTokenValue;
        }

        #endregion GetAccessToken()
    }
}