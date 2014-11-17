using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Credential;
using Credential.Google;
using Newtonsoft.Json;

namespace TestApplication.Controllers
{
    [AllowAnonymous]
    public class GoogleController : Controller
    {
        private readonly IOAuth2 _oAuth2;

        public GoogleController()
        {
            _oAuth2 = new GoogleOAuth(new GoogleOAuthContext
            {
                GET = new Uri("https://accounts.google.com/o/oauth2/auth"),
                POST = new Uri("https://accounts.google.com/o/oauth2/token"),
                ClientId = "381198347835-8r1ibnrlo5t1ml6775gf0kmc49alof9d.apps.googleusercontent.com",
                ClientSecret = "QIDfoV4xL5Do6wNCZIJrQYaZ",
                RedirectUri = "http://localhost:2521/google/callback",
                State = "wtf",
                Scope = new GoogleScope
                {
                    Email = true,
                    Profile = true,
                    OpenId = true,
                    Tasks = true
                }
            });
        }

        public ActionResult RequestCode()
        {
            return Redirect(_oAuth2.MakeAuthorizationServerUri().ToString());
        }

        public async Task<ActionResult> Callback()
        {
            var tokens = await _oAuth2.ExchangeCodeAsync(Request.QueryString);

            var cookie = new HttpCookie("Google")
            {
                Value = JsonConvert.SerializeObject(tokens)
            
            };

            Response.Cookies.Add(cookie);

            return Redirect("/");
        }
    }

}