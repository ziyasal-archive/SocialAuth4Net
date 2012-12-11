using System.Web.Mvc;
using LinkedIn.OAuth.Model;
using SocialAuth4Net.Authenticators;
using SocialAuth4Net.Manager;

namespace SocialAuth4Net.Web.Controllers
{
    public class LinkedinController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.UrlParameters = OAuthManager.GetUrlParametersFor<LinkedInAuthenticator>(string.Empty);
            return View();
        }


        public ActionResult LinkedinAuthenticated()
        {
            ViewResult result = View("LinkedinError");
            LinkedInProfile linkedInProfile = OAuthManager.GetAuthenticatedProfileForLinkedIn(Request.QueryString);

            if (linkedInProfile != null)
            {
                result = View(linkedInProfile);
            }

            return result;
        }
    }
}