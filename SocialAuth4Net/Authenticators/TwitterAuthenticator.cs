using System.Web;
using LinkedIn.OAuth;
using TwitterOAuth;
using TwitterOAuth.Impl;
using TwitterOAuth.Interface;

namespace SocialAuth4Net.Authenticators
{
    public sealed class TwitterAuthenticator : AuthenticatorBase, IAuthenticator<TwitterBasicProfile>
    {
        public TwitterAuthenticator(string authCode)
            : base(authCode)
        {

        }

        public TwitterAuthenticator()
        {
            
        }


        public TwitterBasicProfile Authenticate(HttpRequest request = null)
        {
            ITwitterOAuthManager oAuthManager = new TwitterOAuthManager();
            TwitterBasicProfile result = oAuthManager.Authenticate(request.QueryString);
            return result;
        }

        public override string GetUrlParameters()
        {
            ITwitterOAuthManager oAuthManager = new TwitterOAuthManager();
            return oAuthManager.GetUrlParameters();
        }
    }
}