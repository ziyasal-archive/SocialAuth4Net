namespace SocialAuth4Net.Authenticators
{
    public abstract class AuthenticatorBase
    {
        protected AuthenticatorBase()
        {
        }
        protected AuthenticatorBase(string authCode)
        {
            AuthCode = authCode;
        }

        public string Token { get; protected set; }
        public string AuthCode { get; protected set; }
        public abstract string GetUrlParameters();
    }
}