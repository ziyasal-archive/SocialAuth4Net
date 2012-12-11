using System.Web;
using LinkedIn.OAuth;
using LinkedIn.OAuth.Model;

namespace SocialAuth4Net.Authenticators
{
    public sealed class LinkedInAuthenticator : AuthenticatorBase, IAuthenticator<LinkedInProfile>
    {
        public LinkedInAuthenticator(string authCode)
            : base(authCode)
        {

        }

        public LinkedInAuthenticator()
        {
            
        }

        public LinkedInProfile Authenticate(HttpRequest request = null)
        {
            LinkedInProfile result = null;
            OAuthToken authToken = OAuthManager.Current.GetTokenInCallback();
            LinkedInSession session = OAuthManager.Current.CompleteAuth(authToken);

            if (session.IsAuthorized())
            {
                result = session.GetProfile();
                OAuthManager.Current.Tokens.Clear();
            }

            return result;
        }

        public override string GetUrlParameters()
        {
            return string.Format("oauth_token={0}", OAuthManager.Current.CreateToken().Token);
        }
    }
}