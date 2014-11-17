using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Credential;
using Newtonsoft.Json;
using AuthorizationContext = System.Web.Mvc.AuthorizationContext;

namespace TestApplication.Filters
{
    public class OAuthCookieFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext context)
        {
            base.OnAuthorization(context);

            if (context.Result is HttpUnauthorizedResult)
            {
                var cookie = context.RequestContext.HttpContext.Request.Cookies["OAuth"];

                if (cookie != null)
                {
                    context.RequestContext.HttpContext.User = new ClaimsPrincipal(

                        new MyClaimsIdentity(DecodeClaims(cookie))
                    );

                    context.Result = null;

                    return;
                }    

                context.Result = new RedirectResult("/Login");
            }
            
        }

        private IEnumerable<Claim> DecodeClaims(HttpCookie cookie)
        {
            var tokens = JsonConvert.DeserializeObject<Tokens>(cookie.Value);

            var token = new JwtSecurityToken(tokens.Id_Token);

            return token.Claims;
        }
    }

    class MyClaimsIdentity : ClaimsIdentity
    {
        
        public MyClaimsIdentity(IEnumerable<Claim> claims) : base(claims)
        {
            
        }

        public override bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }
    }
}