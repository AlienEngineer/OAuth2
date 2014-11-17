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
    public class AuthenticationController : Controller
    {
        private readonly IOAuth2 _oAuth2;

        public AuthenticationController()
        {
            _oAuth2 = new GoogleOAuth(new GoogleOAuthContext
            {
                GET = new Uri("https://accounts.google.com/o/oauth2/auth"),
                POST = new Uri("https://accounts.google.com/o/oauth2/token"),
                ClientId = "381198347835-8r1ibnrlo5t1ml6775gf0kmc49alof9d.apps.googleusercontent.com",
                ClientSecret = "QIDfoV4xL5Do6wNCZIJrQYaZ",
                RedirectUri = "http://localhost:2521/authentication/receivetokens",
                State = "wtf",
                Scope = new GoogleScope
                {
                    Email = true,
                    Profile = true
                }
            });
        }

        public ActionResult RequestCode()
        {
            return _oAuth2.Authenticate();
        }

        public async Task<ActionResult> ReceiveTokens()
        {
            var tokens = await _oAuth2.ExchangeCode(Request);

            var cookie = new HttpCookie("OAuth")
            {
                Value = JsonConvert.SerializeObject(tokens)
            
            };

            Response.Cookies.Add(cookie);

            return Redirect("/");
        }
    }

}