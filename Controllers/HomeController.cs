using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TwitterTweets.Models;
using TwitterTweets.Utils;

namespace TwitterTweets.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Message = "";
            return View();
        }

        //[HttpPost]
        public ActionResult SearchTweetsForUser(SearchEntity user)
        {

            if (!string.IsNullOrEmpty(user.UserName) )
            {
                TwitterSearchResult tweetSearchResult = GetTweets(user.UserName, user.NumberOfTweets); 

                if (string.IsNullOrEmpty(tweetSearchResult.ErrorMessage))
                {
                    ViewData[TwitterSearchResult.PARAM_NAME] = tweetSearchResult.Tweets;
                }
                else
                {
                    ModelState.AddModelError("", tweetSearchResult.ErrorMessage);
                }

            }
            return View("Output");
        }



        /// <summary>
        /// gets the tweets
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public TwitterSearchResult GetTweets(string userName, int count)
        {
            TwitterSearchResult retVal = new TwitterSearchResult();
            try
            {
                // Get the access
                var aToken = OuthAuthenticationHelper.GetAccessToken();
                if (aToken.HasAuthToken)
                {

                    string url = string.Format(TwitterConstants.TWEETS_GET_UNFORMATTED_URL,
                              count, userName);
                    // using the access toke get the tweets
                    var result = HttpUtils.MakeHttpGetRequest(url, aToken.OAuthToken.GetAuthenticationHeaderValue(),
                        "application/json");

                    // only id repsosne is ok proceed
                    if (result.ReplyStatusCode == HttpStatusCode.OK)
                    {
                        retVal.Tweets = new JavaScriptSerializer().Deserialize<Tweet[]>(result.ResponseBody);
                    }
                    else
                    {
                        retVal.ErrorMessage = result.ResponseBody;
                    }
                }
            }
            catch (Exception exc)
            {
                retVal.ErrorMessage = exc.Message;
            }
            return retVal;
            
        }


        
    }
}
