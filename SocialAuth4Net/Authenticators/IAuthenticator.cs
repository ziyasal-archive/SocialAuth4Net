namespace SocialAuth4Net.Authenticators
{
    public interface IAuthenticator<out TResult> where TResult:new()
    {
        TResult Authenticate(System.Web.HttpRequest request=null);
        string GetUrlParameters();
    }
}