using System.Web;
using LinkedIn.OAuth.Model;
using SocialAuth4Net.Authenticators;
using SocialAuth4Net.Factory;
using SocialAuth4Net.Struct;
using TwitterOAuth;
using TwitterOAuth.Impl;
using TwitterOAuth.Interface;

namespace SocialAuth4Net.Manager
{
    public static class OAuthManager
    {
        public static string GetUrlParametersUsing<TAuthenticator>(string token) where TAuthenticator : AuthenticatorBase, new()
        {
            return AuthenticatorFactory.Create<TAuthenticator>(token).GetUrlParameters();
        }

        public static FacebookProfile GetAuthenticatedProfileForFacebook(HttpRequestBase request)
        {
            FacebookProfile result = new FacebookProfile();

            if (request.QueryString["code"] != null)
            {
                string code = request.QueryString["code"];

                if (!string.IsNullOrWhiteSpace(code))
                {
                    IAuthenticator<FacebookProfile> authenticator = AuthenticatorFactory.Create<FacebookAuthenticator>(code);
                    result = authenticator.Authenticate();

                }
            }

            return result;
        }

        public static TwitterBasicProfile GetAuthenticatedProfileForTwitter(HttpRequestBase request)
        {
            TwitterBasicProfile result = null;
            ITwitterOAuthManager oAuthManager = new TwitterOAuthManager();

            if (oAuthManager.CheckTwitterOAuthRequest(request.QueryString))
            {
                result = oAuthManager.Authenticate(request.QueryString);
            }
            return result;
        }
        public static LinkedInProfile GetAuthenticatedProfileForLinkedIn(HttpRequestBase request)
        {
            LinkedInProfile result = new LinkedInProfile();

            if (request.QueryString["oauth_verifier"] != null && request.QueryString["oauth_token"] != null)
            {

                string oauthVerifier = request.QueryString["oauth_verifier"];
                string oauthToken = request.QueryString["oauth_token"];

                if (!string.IsNullOrEmpty(oauthToken) && !string.IsNullOrEmpty(oauthVerifier))
                {
                    IAuthenticator<LinkedInProfile> authenticator = AuthenticatorFactory.Create<LinkedInAuthenticator>(oauthToken);
                    result = authenticator.Authenticate();
                }
            }

            return result;
        }
    }
}