SocialAuth4Net
==============
SocialAuth4Net is an OAuth wrapper library for popular social platforms.
Following platforms are included;

* Facebook
* LinkedIn
* Twitter

[NuGet Package](https://nuget.org/packages/SocialAuth4Net)

# Basic Usage
##Facebook MVC Sample

**Setup config**
```xml
<add key="Fb-ApiKey" value="your facebook api key"/>
<add key="Fb-ApiSecret" value="your fb api secret"/>
<add key ="Fb-RedirectUri" value="your redirect url"/>
```
**Info :** Your redirect url Where do you want to process authentication operation..

**Setup Link**<br/>
_Controller Index Action_
```csharp
public ActionResult Index()
{
    ViewBag.UrlParameters = OAuthManager.GetUrlParametersFor<FacebookAuthenticator>(string.Empty);
    return View();
 }
```
_Index View Markup_
```html
<a href="https://www.facebook.com/dialog/oauth?@ViewBag.UrlParameters">
   <img src="@Url.Content("~/Content/themes/base/images/fblogin.png")"/>
</a>
```
**After Facebook Redirected**<br/>
_Controller Authenticate Action_
```csharp
public ActionResult Authenticate()
 {
    FacebookProfile facebookProfile = OAuthManager.GetAuthenticatedProfileForFacebook(Request.QueryString);
    if (!string.IsNullOrEmpty(facebookProfile.id))
    {
      return Content(facebookProfile.id + "<br/>" + facebookProfile.first_name + "<br/>" + facebookProfile.last_name);
    }

    return Content("");
}
```
