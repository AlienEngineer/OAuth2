using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Credential;
using Credential.GitHub;
using Newtonsoft.Json;

namespace TestApplication.Controllers
{
    [AllowAnonymous]
    public class GitHubController : Controller
    {
        private readonly IOAuth2 _oAuth2;

        public GitHubController()
        {
            _oAuth2 = new GitHubOAuth(new GitHubOAuthContext
            {
                GET = new Uri("https://github.com/login/oauth/authorize"),
                POST = new Uri("https://github.com/login/oauth/access_token"),
                ClientId = "c47c852759362b832923",
                ClientSecret = "19828b75a317b4d2ab768bc2372bdd987c9a93df",
                RedirectUri = "http://localhost:2521/github/receivetokens",
                State = "wtf",
                Scope = new GitHubScope
                {
                    User = true,
                    Repo = true,
                    Email = true
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

            var cookie = new HttpCookie("Github")
            {
                Value = JsonConvert.SerializeObject(tokens)

            };

            Response.Cookies.Add(cookie);

            return Redirect("/");
        }
    }
}