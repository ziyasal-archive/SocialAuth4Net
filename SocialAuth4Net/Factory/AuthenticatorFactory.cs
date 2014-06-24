using System;
using SocialAuth4Net.Authenticators;

namespace SocialAuth4Net.Factory
{
    public static class AuthenticatorFactory
    {
        public static TAuthenticator Create<TAuthenticator>(string authCode) where TAuthenticator : AuthenticatorBase ,new()
        {
            return !string.IsNullOrEmpty(authCode)
                       ? (TAuthenticator)Activator.CreateInstance(typeof(TAuthenticator), new object[] { authCode })
                       : new TAuthenticator();
        }
    }
}