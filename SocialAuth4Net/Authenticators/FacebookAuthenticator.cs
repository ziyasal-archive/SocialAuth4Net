using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Facebook;
using SocialAuth4Net.Helper;
using SocialAuth4Net.Struct;

namespace SocialAuth4Net.Authenticators
{
    public sealed class FacebookAuthenticator : AuthenticatorBase, IAuthenticator<FacebookProfile>
    {
        private const string FB_APP_ID = "Fb-ApiKey";
        private const string FB_API_SECRET = "Fb-ApiSecret";
        private const string FB_REQUEST_URI = "Fb-RedirectUri";

        private Uri _redirectUri;

        public string AppID
        {
            get { return ConfigurationManager.AppSettings[FB_APP_ID]; }
        }

        public string AppSecret
        {
            get { return ConfigurationManager.AppSettings[FB_API_SECRET]; }
        }

        public Uri RedirectUri
        {
            get
            {
                return _redirectUri ??
                       (_redirectUri = new Uri(ConfigurationManager.AppSettings[FB_REQUEST_URI]));
            }
            set { _redirectUri = value; }
        }

        public FacebookAuthenticator()
        {

        }

        public FacebookAuthenticator(string authCode)
            : base(authCode)
        {
        }

        public FacebookProfile Authenticate(HttpRequest request = null)
        {
            FacebookOAuthClient facebookOAuthClient = new FacebookOAuthClient
            {
                AppId = AppID,
                AppSecret = AppSecret,
                RedirectUri = RedirectUri
            };

            Dictionary<string, object> oauthParams = new Dictionary<string, object> 
            {
                {
                    "redirect_uri", RedirectUri
                } 
            };

            FacebookAuthResult facebookAuthResult = SerilizationHelper.Deserialize<FacebookAuthResult>(facebookOAuthClient.ExchangeCodeForAccessToken(AuthCode, oauthParams).ToString());

            Token = facebookAuthResult.access_token;
            FacebookClient facebookClient = new FacebookClient(Token);

            return SerilizationHelper.Deserialize<FacebookProfile>(facebookClient.Get("/me").ToString());
        }

        public override string GetUrlParameters()
        {
            return string.Format("client_id={0}&redirect_uri={1}", AppID, RedirectUri);
        }
    }
}