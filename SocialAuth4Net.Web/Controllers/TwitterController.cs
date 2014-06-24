using System.Web.Mvc;
using SocialAuth4Net.Authenticators;
using SocialAuth4Net.Manager;
using TwitterOAuth;

namespace SocialAuth4Net.Web.Controllers
{
    public class TwitterController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.UrlParameters = OAuthManager.GetUrlParametersUsing<TwitterAuthenticator>(string.Empty);
            return View();
        }

        public ActionResult Authenticate()
        {
            TwitterBasicProfile twitterBasicProfile = OAuthManager.GetAuthenticatedProfileForTwitter(Request);
            if (!string.IsNullOrEmpty(twitterBasicProfile.Id))
            {
                return Content(twitterBasicProfile.Id + "<br/>" + twitterBasicProfile.ScreenName + "<br/>" + twitterBasicProfile.Description);
            }

            return Content("");
        }
    }
}