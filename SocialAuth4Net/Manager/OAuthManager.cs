using System.Collections.Specialized;
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
        public static string GetUrlParametersFor<TAuthenticator>(string token) where TAuthenticator : AuthenticatorBase, new()
        {
            return AuthenticatorFactory.CreateAuthenticator<TAuthenticator>(token).GetUrlParameters();
        }

        public static FacebookProfile GetAuthenticatedProfileForFacebook(NameValueCollection queryStringCollection)
        {
            FacebookProfile result = new FacebookProfile();

            if (queryStringCollection["code"] != null)
            {
                string code = queryStringCollection["code"];

                if (!string.IsNullOrWhiteSpace(code))
                {
                    IAuthenticator<FacebookProfile> authenticator =
                        AuthenticatorFactory.CreateAuthenticator<FacebookAuthenticator>(code);
                    result = authenticator.Authenticate();

                }
            }

            return result;
        }

        public static TwitterBasicProfile GetAuthenticatedProfileForTwitter(NameValueCollection queryStringCollection)
        {
            TwitterBasicProfile result = null;
             ITwitterOAuthManager oAuthManager = new TwitterOAuthManager();

            if (oAuthManager.CheckTwitterOAuthRequest(queryStringCollection))
            {
                result= oAuthManager.Authenticate(queryStringCollection);
            }
            return result;
        }
        public static LinkedInProfile GetAuthenticatedProfileForLinkedIn(NameValueCollection queryStringCollection)
        {
            LinkedInProfile result = new LinkedInProfile();

            if (queryStringCollection["oauth_verifier"] != null && queryStringCollection["oauth_token"] != null)
            {

                string oauthVerifier = queryStringCollection["oauth_verifier"];
                string oauthToken = queryStringCollection["oauth_token"];

                if (!string.IsNullOrEmpty(oauthToken) && !string.IsNullOrEmpty(oauthVerifier))
                {
                    IAuthenticator<LinkedInProfile> authenticator =
                        AuthenticatorFactory.CreateAuthenticator<LinkedInAuthenticator>(oauthToken);
                    result = authenticator.Authenticate();
                }
            }

            return result;
        }
    }
}