using System.Web.Mvc;
using SocialAuth4Net.Authenticators;
using SocialAuth4Net.Manager;
using SocialAuth4Net.Struct;

namespace SocialAuth4Net.Web.Controllers
{
    public class FacebookController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.UrlParameters = OAuthManager.GetUrlParametersFor<FacebookAuthenticator>(string.Empty);
            return View();
        }

        public ActionResult Authenticate()
        {
            FacebookProfile facebookProfile = OAuthManager.GetAuthenticatedProfileForFacebook(Request.QueryString);
            if (!string.IsNullOrEmpty(facebookProfile.id))
            {
                return Content(facebookProfile.id + "<br/>" + facebookProfile.first_name + "<br/>" +
                        facebookProfile.last_name);
            }

            return Content("");
        }
    }
}